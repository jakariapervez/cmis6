// Standard .NET namespaces
using System;
using System.Runtime.InteropServices;

// Main AutoCAD namespaces
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD;

namespace CP2071_CSharp
{
    class AU2012_Viewports
    {
        // Used to set a viewport current
        [DllImport("accore.dll", CallingConvention = CallingConvention.Cdecl,
         EntryPoint = "?acedSetCurrentVPort@@YA?AW4ErrorStatus@Acad@@PEBVAcDbViewport@@@Z")]
        extern static private int acedSetCurrentVPort(IntPtr AcDbVport);

        // Used to create a rectangular and nonrectangular viewports
        [CommandMethod("ViewportsCreateFloating")]
        public void ViewportsCreateFloating()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl= acTrans.GetObject(acCurDb.BlockTableId,
                                                       OpenMode.ForRead) as BlockTable;

                // Open the Block table record Paper space for write
                BlockTableRecord acBlkTblRec =
                    acTrans.GetObject(acBlkTbl[BlockTableRecord.PaperSpace],
                                      OpenMode.ForWrite) as BlockTableRecord;

                // Switch to the previous Paper space layout
                Application.SetSystemVariable("TILEMODE", 0);
                acDoc.Editor.SwitchToPaperSpace();

                // Remove any viewports that already exist
                foreach (ObjectId objId in acBlkTblRec)
                {
                    DBObject dbObj = acTrans.GetObject(objId,
                                                       OpenMode.ForRead) as DBObject;

                    // Remove any viewports in the block
                    Autodesk.AutoCAD.DatabaseServices.Viewport acVport = 
                        dbObj as Autodesk.AutoCAD.DatabaseServices.Viewport;

                    if (acVport != null)
                    {
                        dbObj.UpgradeOpen();
                        dbObj.Erase(true);
                    }
                }

                // Create a Viewport
                using (Autodesk.AutoCAD.DatabaseServices.Viewport acVport1 = 
                           new Autodesk.AutoCAD.DatabaseServices.Viewport())
                {
                    // Set the center point and size of the viewport
                    acVport1.CenterPoint = new Point3d(3.75, 4, 0);
                    acVport1.Width = 7.5;
                    acVport1.Height = 7.5;

                    // Lock the viewport
                    acVport1.Locked = true;

                    // Set the scale to 1" = 4'
                    acVport1.CustomScale = 48;

                    // Set visual style
                    DBDictionary vStyles =
                        acTrans.GetObject(acCurDb.VisualStyleDictionaryId,
                                          OpenMode.ForRead) as DBDictionary;

                    acVport1.SetShadePlot(ShadePlotType.VisualStyle,
                                          vStyles.GetAt("Sketchy"));

                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acVport1);
                    acTrans.AddNewlyCreatedDBObject(acVport1, true);

                    // Change the view direction and enable the viewport
                    acVport1.ViewDirection = new Vector3d(-1, -1, 1);
                    acVport1.On = true;

                    // Create a rectangular viewport to change to a non-rectangular viewport
                    using (Autodesk.AutoCAD.DatabaseServices.Viewport acVport2 = 
                               new Autodesk.AutoCAD.DatabaseServices.Viewport())
                    {
                        acVport2.CenterPoint = new Point3d(9, 6.5, 0);
                        acVport2.Width = 2.5;
                        acVport2.Height = 2.5;

                        // Set the scale to 1" = 8'
                        acVport2.CustomScale = 96;

                        // Set render preset
                        DBDictionary namedObjs =
                            acTrans.GetObject(acCurDb.NamedObjectsDictionaryId,
                                              OpenMode.ForRead) as DBDictionary;

                        // Check to see if the Render Settings dictionary already exists
                        DBDictionary renderSettings; 
                        if (namedObjs.Contains("ACAD_RENDER_PLOT_SETTINGS") == true)
                        {
                            renderSettings = acTrans.GetObject(
                                namedObjs.GetAt("ACAD_RENDER_PLOT_SETTINGS"),
                                OpenMode.ForWrite) as DBDictionary;
                        }
                        else
                        {
                            // If it does not exist, create it and add it to the drawing
                            namedObjs.UpgradeOpen();
                            renderSettings = new DBDictionary();
                            namedObjs.SetAt("ACAD_RENDER_PLOT_SETTINGS", renderSettings);
                            acTrans.AddNewlyCreatedDBObject(renderSettings, true);
                        }

                        // Create the new render preset, based on the settings
                        // of the Medium render preset
                        MentalRayRenderSettings renderSetting = new MentalRayRenderSettings();  // Removed using statement because of passting ByRef.

                        GetDefaultRenderPreset(ref renderSetting, "Medium");

                        renderSetting.Name = "Medium";
                        renderSettings.SetAt("Medium", renderSetting);
                        acTrans.AddNewlyCreatedDBObject(renderSetting, true);

                        acVport2.SetShadePlot(ShadePlotType.RenderPreset,
                                                renderSetting.ObjectId);
                        renderSetting.Dispose();

                        // Create a circle
                        using (Circle acCirc = new Circle())
                        {
                            acCirc.Center = acVport2.CenterPoint;
                            acCirc.Radius = 1.25;

                            // Add the new object to the block table record and the transaction
                            acBlkTblRec.AppendEntity(acCirc);
                            acTrans.AddNewlyCreatedDBObject(acCirc, true);

                            // Clip the viewport using the circle  
                            acVport2.NonRectClipEntityId = acCirc.ObjectId;
                            acVport2.NonRectClipOn = true;
                        }

                        // Add the new object to the block table record and the transaction
                        acBlkTblRec.AppendEntity(acVport2);
                        acTrans.AddNewlyCreatedDBObject(acVport2, true);

                        // Change the view direction
                        acVport2.ViewDirection = new Vector3d(0, 0, 1);

                        // Enable the viewport
                        acVport2.On = true;
                    }

                    // Activate model space
                    acDoc.Editor.SwitchToModelSpace();

                    // Set the new viewport current via an imported ObjectARX function
                    acedSetCurrentVPort(acVport1.UnmanagedObject);
                }

                // Save the new objects to the database
                acTrans.Commit();
            }
        }

        // Method used to populate a MentalRayRenderSettings object with the
        // same settings used by the standard render presets
        private static void GetDefaultRenderPreset(
                       ref MentalRayRenderSettings renderPreset,
                       string name)
        {
            // Set the values common to multiple default render presets
            renderPreset.BackFacesEnabled = false;
            renderPreset.DiagnosticBackgroundEnabled = false;
            renderPreset.DiagnosticBSPMode =
                DiagnosticBSPMode.Depth;
            renderPreset.DiagnosticGridMode =
                new MentalRayRenderSettingsTraitsDiagnosticGridModeParameter(
                    DiagnosticGridMode.Object, (float)10.0);

            renderPreset.DiagnosticMode =
                DiagnosticMode.Off;
            renderPreset.DiagnosticPhotonMode =
                DiagnosticPhotonMode.Density;
            renderPreset.DisplayIndex = 0;
            renderPreset.EnergyMultiplier = (float)1.0;
            renderPreset.ExportMIEnabled = false;
            renderPreset.ExportMIFileName = "";
            renderPreset.FGRayCount = 100;

            // FGSampleRadius cannot be set, it returns invalid input
            renderPreset.FGSampleRadiusState =
                new MentalRayRenderSettingsTraitsBoolParameter(
                    false, false, false);

            renderPreset.FinalGatheringEnabled = false;
            renderPreset.FinalGatheringMode =
                FinalGatheringMode.FinalGatherOff;
            renderPreset.GIPhotonsPerLight = 1000;
            renderPreset.GISampleCount = 500;
            renderPreset.GISampleRadius = 1.0;
            renderPreset.GISampleRadiusEnabled = false;
            renderPreset.GlobalIlluminationEnabled = false;
            renderPreset.LightLuminanceScale = 1500.0;
            renderPreset.MaterialsEnabled = true;
            renderPreset.MemoryLimit = 1048;

            renderPreset.PhotonTraceDepth =
                new MentalRayRenderSettingsTraitsTraceParameter(
                    5, 5, 5);
            renderPreset.PreviewImageFileName = "";
            renderPreset.RayTraceDepth =
                new MentalRayRenderSettingsTraitsTraceParameter(
                    3, 3, 3);
            renderPreset.RayTracingEnabled = false;
            renderPreset.Sampling =
                new MentalRayRenderSettingsTraitsIntegerRangeParameter(
                    -2, -1);
            renderPreset.SamplingContrastColor =
                new MentalRayRenderSettingsTraitsFloatParameter(
                    (float)0.1, (float)0.1, (float)0.1, (float)0.1);
            renderPreset.SamplingFilter =
                new MentalRayRenderSettingsTraitsSamplingParameter(
                    Filter.Box, 1.0, 1.0);

            renderPreset.ShadowMapsEnabled = false;
            renderPreset.ShadowMode = ShadowMode.Simple;
            renderPreset.ShadowSamplingMultiplier =
                ShadowSamplingMultiplier.SamplingMultiplierZero;
            renderPreset.ShadowsEnabled = true;
            renderPreset.TextureSampling = false;
            renderPreset.TileOrder = TileOrder.Hilbert;
            renderPreset.TileSize = 32;

            switch (name.ToUpper()) {
                // Assigns the values to match the Draft render preset
		        case "DRAFT":
                    renderPreset.Description =
                        "The lowest rendering quality which entails no raytracing, " +
                        "no texture filtering and force 2-sided is inactive.";
                    renderPreset.Name = "Draft";
                    break;
                case "LOW":
                    renderPreset.Description =
                        "Rendering quality is improved over Draft. " +
                        "Low anti-aliasing and a raytracing depth of 3 " +
                        "reflection/refraction are processed.";
                    renderPreset.Name = "Low";

                    renderPreset.RayTracingEnabled = true;

                    renderPreset.Sampling =
                        new MentalRayRenderSettingsTraitsIntegerRangeParameter(
                            -1, 0);
                    renderPreset.SamplingContrastColor =
                        new MentalRayRenderSettingsTraitsFloatParameter(
                            (float)0.1, (float)0.1, (float)0.1, (float)0.1);
                    renderPreset.SamplingFilter =
                        new MentalRayRenderSettingsTraitsSamplingParameter(
                            Filter.Triangle, 2.0, 2.0);

                    renderPreset.ShadowSamplingMultiplier =
                        ShadowSamplingMultiplier.SamplingMultiplierOneFourth;
                    break;
                case "MEDIUM":
                    renderPreset.BackFacesEnabled = true;
                    renderPreset.Description =
                        "Rendering quality is improved over Low to include " +
                        "texture filtering and force 2-sided is active. " +
                        "Moderate anti-aliasing and a raytracing depth of " +
                        "5 reflections/refractions are processed.";

                    renderPreset.FGRayCount = 200;
                    renderPreset.FinalGatheringMode =
                        FinalGatheringMode.FinalGatherAuto;
                    renderPreset.GIPhotonsPerLight = 10000;

                    renderPreset.Name = "Medium";
                    renderPreset.RayTraceDepth =
                        new MentalRayRenderSettingsTraitsTraceParameter(
                            5, 5, 5);
                    renderPreset.RayTracingEnabled = true;
                    renderPreset.Sampling =
                        new MentalRayRenderSettingsTraitsIntegerRangeParameter(
                            0, 1);
                    renderPreset.SamplingContrastColor =
                        new MentalRayRenderSettingsTraitsFloatParameter(
                            (float)0.05, (float)0.05, (float)0.05, (float)0.05);
                    renderPreset.SamplingFilter =
                        new MentalRayRenderSettingsTraitsSamplingParameter(
                            Filter.Gauss, 3.0, 3.0);

                    renderPreset.ShadowSamplingMultiplier =
                        ShadowSamplingMultiplier.SamplingMultiplierOneHalf;
                    renderPreset.TextureSampling = true;
                    break;
                case "HIGH":
                    renderPreset.BackFacesEnabled = true;
                    renderPreset.Description =
                        "Rendering quality is improved over Medium. " +
                        "High anti-aliasing and a raytracing depth of 7 " +
                        "reflections/refractions are processed.";

                    renderPreset.FGRayCount = 500;
                    renderPreset.FinalGatheringMode =
                        FinalGatheringMode.FinalGatherAuto;
                    renderPreset.GIPhotonsPerLight = 10000;

                    renderPreset.Name = "High";
                    renderPreset.RayTraceDepth =
                        new MentalRayRenderSettingsTraitsTraceParameter(
                            7, 7, 7);
                    renderPreset.RayTracingEnabled = true;
                    renderPreset.Sampling =
                        new MentalRayRenderSettingsTraitsIntegerRangeParameter(
                            0, 2);
                    renderPreset.SamplingContrastColor =
                        new MentalRayRenderSettingsTraitsFloatParameter(
                            (float)0.05, (float)0.05, (float)0.05, (float)0.05);
                    renderPreset.SamplingFilter =
                        new MentalRayRenderSettingsTraitsSamplingParameter(
                            Filter.Mitchell, 4.0, 4.0);

                    renderPreset.ShadowSamplingMultiplier =
                        ShadowSamplingMultiplier.SamplingMultiplierOne;
                    renderPreset.TextureSampling = true;
                    break;
                case "PRESENTATION":
                    renderPreset.BackFacesEnabled = true;
                    renderPreset.Description =
                        "Rendering quality is improved over High. " +
                        "Very high anti-aliasing and a raytracing depth of 9 " +
                        "reflections/refractions are processed.";

                    renderPreset.FGRayCount = 1000;
                    renderPreset.FinalGatheringMode =
                        FinalGatheringMode.FinalGatherAuto;
                    renderPreset.GIPhotonsPerLight = 10000;

                    renderPreset.Name = "Presentation";
                    renderPreset.RayTraceDepth =
                        new MentalRayRenderSettingsTraitsTraceParameter(
                            9, 9, 9);
                    renderPreset.RayTracingEnabled = true;
                    renderPreset.Sampling =
                        new MentalRayRenderSettingsTraitsIntegerRangeParameter(
                            1, 2);
                    renderPreset.SamplingContrastColor =
                        new MentalRayRenderSettingsTraitsFloatParameter(
                            (float)0.05, (float)0.05, (float)0.05, (float)0.05);
                    renderPreset.SamplingFilter =
                        new MentalRayRenderSettingsTraitsSamplingParameter(
                            Filter.Lanczos, 4.0, 4.0);

                    renderPreset.ShadowSamplingMultiplier =
                        ShadowSamplingMultiplier.SamplingMultiplierOne;
                    renderPreset.TextureSampling = true;
                    break;
            }
        }
    }
}
