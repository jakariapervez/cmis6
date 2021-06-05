
using System;

// Main AutoCAD namespaces
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.GraphicsInterface;

namespace CP2071_CSharp
{
    class AU2012_VisualStyles_RenderPresets
    {
        // Lists the available visual styles
        [CommandMethod("VisualStyleList")]
        public static void VisualStyleList()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary vStyles = acTrans.GetObject(acCurDb.VisualStyleDictionaryId, 
                                                         OpenMode.ForRead) as DBDictionary;

                // Output a message to the Command Line history
                acDoc.Editor.WriteMessage("\nVisual styles: ");

                // Step through the dictionary
                foreach (DBDictionaryEntry entry in vStyles)
                {
                    // Get the dictionary entry
                    DBVisualStyle vStyle = vStyles.GetAt(entry.Key).GetObject(OpenMode.ForRead) as DBVisualStyle;

                    // If the visual style is not marked for internal use then output its name
                    if (vStyle.InternalUseOnly == false)
                    {
                        // Output the name of the visual style
                        acDoc.Editor.WriteMessage("\n  " + vStyle.Name);
                    }
                }
            }
        }

        // Creates a new visual style
        [CommandMethod("VisualStyleCreate")]
        public static void VisualStyleCreate()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary vStyles = acTrans.GetObject(acCurDb.VisualStyleDictionaryId, 
                                                         OpenMode.ForRead) as DBDictionary;

                try
                {
                    // Check to see if the "MyVS" exists or not
                    DBVisualStyle vStyle = default(DBVisualStyle);
                    if (vStyles.Contains("MyVS") == true)
                    {
                        vStyle = acTrans.GetObject(vStyles.GetAt("MyVS"), OpenMode.ForWrite) as DBVisualStyle;
                    }
                    else
                    {
                        vStyles.UpgradeOpen();

                        // Create the visual style
                        vStyle = new DBVisualStyle();
                        vStyles.SetAt("MyVS", vStyle);

                        // Add the visual style to the dictionary
                        acTrans.AddNewlyCreatedDBObject(vStyle, true);
                    }

                    // Set the description of the visual style
                    vStyle.Description = "My Visual Style";
                    vStyle.Type = VisualStyleType.Custom;

                    // Face Settings (Opacity, Face Style, Lighting Quality, Color, 
                    //                Monochrome color, Opacity, and Material Display)
                    vStyle.SetTrait(VisualStyleProperty.FaceModifier, (int)VSFaceModifiers.FaceOpacityFlag, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.FaceLightingModel, (int)VSFaceLightingModel.Gooch, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.FaceLightingQuality, (int)VSFaceLightingQuality.PerPixelLighting, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.FaceColorMode, (int)VSFaceColorMode.ObjectColor, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.FaceMonoColor, Color.FromColorIndex(ColorMethod.ByAci, 1), VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.FaceOpacity, 0.5, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.DisplayStyle, (int)VSDisplayStyles.MaterialsFlag + (int)VSDisplayStyles.TexturesFlag, VisualStyleOperation.Set);

                    // Lighting (Enable Highlight Intensity, 
                    //           Highlight Intensity, and Shadow Display)
                    vStyle.SetTrait(VisualStyleProperty.FaceModifier, vStyle.GetTrait(VisualStyleProperty.FaceModifier).Int + (int)VSFaceModifiers.SpecularFlag, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.DisplayStyle, vStyle.GetTrait(VisualStyleProperty.DisplayStyle).Int + (int)VSDisplayStyles.LightingFlag, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.FaceSpecular, 45.0, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.DisplayShadowType, (int)VSDisplayShadowType.Full, VisualStyleOperation.Set);

                    // Environment Settings (Backgrounds)
                    vStyle.SetTrait(VisualStyleProperty.DisplayStyle, vStyle.GetTrait(VisualStyleProperty.DisplayStyle).Int + (int)VSDisplayStyles.BackgroundsFlag, VisualStyleOperation.Set);

                    // Edge Settings (Show, Number of Lines, Color, and Always on Top)
                    vStyle.SetTrait(VisualStyleProperty.EdgeModel, (int)VSEdgeModel.Isolines, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeIsolines, 6, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeColor, Color.FromColorIndex(ColorMethod.ByAci, 2), VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeModifier, vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int + (int)VSEdgeModifiers.AlwaysOnTopFlag, VisualStyleOperation.Set);

                    // Occluded Edges (Show, Color, and Linetype)
                    if (!((vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int & (int)VSEdgeStyles.ObscuredFlag) > 0))
                    {
                        vStyle.SetTrait(VisualStyleProperty.EdgeStyle, vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int + (int)VSEdgeStyles.ObscuredFlag, VisualStyleOperation.Set);
                    }
                    vStyle.SetTrait(VisualStyleProperty.EdgeObscuredColor, Color.FromColorIndex(ColorMethod.ByAci, 3), VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeObscuredLinePattern, (int)VSEdgeLinePattern.DoubleMediumDash, VisualStyleOperation.Set);

                    // Intersection Edges (Color and Linetype)
                    if (!((vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int & (int)VSEdgeStyles.IntersectionFlag) > 0))
                    {
                        vStyle.SetTrait(VisualStyleProperty.EdgeStyle, vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int + (int)VSEdgeStyles.IntersectionFlag, VisualStyleOperation.Set);
                    }
                    vStyle.SetTrait(VisualStyleProperty.EdgeIntersectionColor, Color.FromColorIndex(ColorMethod.ByAci, 4), VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeIntersectionLinePattern, (int)VSEdgeLinePattern.ShortDash, VisualStyleOperation.Set);

                    // Silhouette Edges (Color and Width)
                    if (!((vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int & (int)VSEdgeStyles.SilhouetteFlag) > 0))
                    {
                        vStyle.SetTrait(VisualStyleProperty.EdgeStyle, vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int + (int)VSEdgeStyles.SilhouetteFlag, VisualStyleOperation.Set);
                    }
                    vStyle.SetTrait(VisualStyleProperty.EdgeSilhouetteColor, Color.FromColorIndex(ColorMethod.ByAci, 5), VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeSilhouetteWidth, 2, VisualStyleOperation.Set);

                    // Edge Modifiers (Enable Line Extensions, Enable Jitter, 
                    //                 Line Extensions, Jitter, Crease Angle, 
                    //                 and Halo Gap)
                    if (!((vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int & (int)VSEdgeModifiers.EdgeOverhangFlag) > 0))
                    {
                        vStyle.SetTrait(VisualStyleProperty.EdgeModifier, vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int + (int)VSEdgeModifiers.EdgeOverhangFlag, VisualStyleOperation.Set);
                    }
                    if (!((vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int & (int)VSEdgeModifiers.EdgeJitterFlag) > 0))
                    {
                        vStyle.SetTrait(VisualStyleProperty.EdgeModifier, vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int + (int)VSEdgeModifiers.EdgeJitterFlag, VisualStyleOperation.Set);
                    }
                    vStyle.SetTrait(VisualStyleProperty.EdgeOverhang, 3, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeJitterAmount, (int)VSEdgeJitterAmount.JitterMedium, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeCreaseAngle, 0.3, VisualStyleOperation.Set);
                    vStyle.SetTrait(VisualStyleProperty.EdgeHaloGap, 5, VisualStyleOperation.Set);
                }
                catch (Autodesk.AutoCAD.Runtime.Exception es)
                {
                    System.Windows.Forms.MessageBox.Show(es.Message);
                }
                finally
                {
                    acTrans.Commit();
                }
            }
        }

        // Utility to output a property value
        // Usage: OutputVSPropValue(vStyle, VisualStyleProperty.EdgeHaloGap)
        private static void OutputVSPropValue(DBVisualStyle vStyle, VisualStyleProperty vsProp)
        {
            // Get the current document
            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            Autodesk.AutoCAD.GraphicsInterface.Variant gIntVar = vStyle.GetTrait(vsProp);

            acDoc.Editor.WriteMessage("\n" + gIntVar.Type.ToString() + ": ");

            if (gIntVar.Type == Autodesk.AutoCAD.GraphicsInterface.VariantType.Boolean)
            {
                acDoc.Editor.WriteMessage(gIntVar.Boolean.ToString());
            }
            else if (gIntVar.Type == Autodesk.AutoCAD.GraphicsInterface.VariantType.Color)
            {
                acDoc.Editor.WriteMessage(gIntVar.Color.ColorIndex.ToString());
            }
            else if (gIntVar.Type == Autodesk.AutoCAD.GraphicsInterface.VariantType.Double)
            {
                acDoc.Editor.WriteMessage(gIntVar.Double.ToString());
            }
            else if (gIntVar.Type == Autodesk.AutoCAD.GraphicsInterface.VariantType.Int)
            {
                acDoc.Editor.WriteMessage(gIntVar.Int.ToString());
            }
            else if (gIntVar.Type == Autodesk.AutoCAD.GraphicsInterface.VariantType.String)
            {
                acDoc.Editor.WriteMessage(gIntVar.String.ToString());
            }
        }

        // Lists the available render presets
        [CommandMethod("RenderPresetsList")]
        public static void RenderPresetsList()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary namedObjs = acTrans.GetObject(acCurDb.NamedObjectsDictionaryId,
                                                           OpenMode.ForRead) as DBDictionary;

                // Output a message to the Command Line history
                acDoc.Editor.WriteMessage("\nDefault render presets: ");

                // List the default render presets that are defined 
                // as part of the application
                acDoc.Editor.WriteMessage("\n  Draft");
                acDoc.Editor.WriteMessage("\n  Low");
                acDoc.Editor.WriteMessage("\n  Medium");
                acDoc.Editor.WriteMessage("\n  High");
                acDoc.Editor.WriteMessage("\n  Presentation");

                // Check to see if the "ACAD_RENDER_SETTINGS" named dictionary exists
                if (namedObjs.Contains("ACAD_RENDER_SETTINGS") == true)
                {
                    // Open the named dictionary
                    DBDictionary renderSettings = acTrans.GetObject(namedObjs.GetAt("ACAD_RENDER_SETTINGS"),
                                                                    OpenMode.ForRead) as DBDictionary;

                    // Output a message to the Command Line history
                    acDoc.Editor.WriteMessage("\nCustom render presets: ");

                    // Step through and list each of the custom render presets
                    foreach (DBDictionaryEntry entry in renderSettings)
                    {
                        MentalRayRenderSettings renderSetting = acTrans.GetObject(entry.Value, 
                                                       OpenMode.ForRead) as MentalRayRenderSettings;

                        // Output the name of the custom render preset
                        acDoc.Editor.WriteMessage("\n  " + renderSetting.Name);
                    }
                }
                else
                {
                    // If no custom render presets exist, then output the following message
                    acDoc.Editor.WriteMessage("\nNo custom render presets available.");
                }

                // Check to see if the "ACAD_RENDER_PLOT_SETTINGS" named dictionary exists
                if (namedObjs.Contains("ACAD_RENDER_PLOT_SETTINGS") == true)
                {
                    // Open the named dictionary
                    DBDictionary renderSettings = 
                        acTrans.GetObject(namedObjs.GetAt("ACAD_RENDER_PLOT_SETTINGS"), 
                                                          OpenMode.ForRead) as DBDictionary;

                    // Output a message to the Command Line history
                    acDoc.Editor.WriteMessage("\nCustom render plot presets: ");

                    // Step through and list each of the custom render presets
                    foreach (DBDictionaryEntry entry in renderSettings)
                    {
                        MentalRayRenderSettings renderSetting = 
                            acTrans.GetObject(entry.Value, OpenMode.ForRead) as MentalRayRenderSettings;

                        // Output the name of the custom render preset
                        acDoc.Editor.WriteMessage("\n  " + renderSetting.Name);
                    }
                }
                else
                {
                    // If no custom render plot presets exist, then output the following message
                    acDoc.Editor.WriteMessage("\nNo custom render plot presets available.");
                }

                // Discard any changes
                acTrans.Abort();
            }
        }

        // Creates a new render preset
        [CommandMethod("RenderPresetsCreate")]
        public static void RenderPresetCreate()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary namedObjs = acTrans.GetObject(acCurDb.NamedObjectsDictionaryId,
                                                           OpenMode.ForRead) as DBDictionary;

                try
                {
                    // Check to see if the Render Settings dictionary already exists
                    DBDictionary renderSettings = default(DBDictionary);
                    if (namedObjs.Contains("ACAD_RENDER_SETTINGS") == true)
                    {
                        renderSettings = acTrans.GetObject(namedObjs.GetAt("ACAD_RENDER_SETTINGS"), 
                                                           OpenMode.ForWrite) as DBDictionary;
                    }
                    else
                    {
                        // If it does not exist, create it and add it to the drawing
                        namedObjs.UpgradeOpen();
                        renderSettings = new DBDictionary();
                        namedObjs.SetAt("ACAD_RENDER_SETTINGS", renderSettings);
                        acTrans.AddNewlyCreatedDBObject(renderSettings, true);
                    }

                    // Create the new render preset, based on 
                    // the settings of the Medium render preset
                    if (renderSettings.Contains("MyPreset") == false)
                    {
                        MentalRayRenderSettings renderSetting = new MentalRayRenderSettings();
                        GetDefaultRenderPreset(ref renderSetting, "Medium");

                        renderSetting.Name = "MyPreset";
                        renderSetting.Description = "Custom new render preset";
                        renderSettings.SetAt("MyPreset", renderSetting);
                        acTrans.AddNewlyCreatedDBObject(renderSetting, true);

                        renderSetting.Dispose();
                    }

                    // Set the new render preset current
                    Application.UIBindings.RenderEngine.CurrentRenderPresetName = "MyPreset";

                }
                catch (Autodesk.AutoCAD.Runtime.Exception es)
                {
                    System.Windows.Forms.MessageBox.Show(es.Message);
                }
                finally
                {
                    acTrans.Commit();
                }
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

            switch (name.ToUpper())
            {
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

        [CommandMethod("RenderPresetsSetCurrent")]
        public static void RenderPresetsSetCurrent()
        {
            // Set the Draft render preset current
            Application.UIBindings.RenderEngine.CurrentRenderPresetName = "Draft";
        }
    }
}
