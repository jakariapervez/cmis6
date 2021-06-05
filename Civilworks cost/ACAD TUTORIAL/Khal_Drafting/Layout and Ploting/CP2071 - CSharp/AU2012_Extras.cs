// Standard .NET namespaces
using System;
using System.IO;

// Main AutoCAD namespaces
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.GraphicsInterface;

namespace CP2071_CSharp
{
    class AU2012_Extras
    {
        [CommandMethod("LayoutCreateFrom")]
        public static void LayoutCreateFrom()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            string newLayoutName = "copiedLayout";
            string copyLayoutName = "Layout1";

            // Get the layout and plot settings of the named pagesetup
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Reference the Layout Manager
                LayoutManager acLayoutMgr = LayoutManager.Current;

                // Create the new layout based on the existing layout
                acLayoutMgr.CopyLayout(copyLayoutName, newLayoutName);

                // Open the layout
                Layout acLayout = acTrans.GetObject(acLayoutMgr.GetLayoutId(newLayoutName), 
                                                    OpenMode.ForWrite) as Layout;

                // Set the copied layout current
                acLayoutMgr.CurrentLayout = newLayoutName;

                // Output some information related to the layout object
                acDoc.Editor.WriteMessage("\nTab Order: " + acLayout.TabOrder + 
                                          "\nTab Selected: " + acLayout.TabSelected + 
                                          "\nBlock Table Record ID: " + acLayout.BlockTableRecordId.ToString());

                // Save the changes made
                acTrans.Commit();
            }
        }

        [CommandMethod("LayoutRemove")]
        public static void LayoutRemove()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            string layoutName = "Layout1";

            // Get the layout and plot settings of the named pagesetup
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Reference the Layout Manager
                LayoutManager acLayoutMgr = LayoutManager.Current;

                // Remove layout from drawing
                acLayoutMgr.DeleteLayout(layoutName);

                // Save the changes made
                acTrans.Commit();
            }

            // Regen the display so the new layout and correct order is displayed
            Application.DocumentManager.MdiActiveDocument.Editor.Regen();
        }

        [CommandMethod("LayoutRename")]
        public static void LayoutRename()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            string layoutName = "Layout2";
            string newLayoutName = "renamedLayout";

            // Get the layout and plot settings of the named pagesetup
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Reference the Layout Manager
                LayoutManager acLayoutMgr = LayoutManager.Current;

                // Remove layout from drawing
                acLayoutMgr.RenameLayout(layoutName, newLayoutName);

                // Save the changes made
                acTrans.Commit();
            }

            // Regen the display so the new layout and correct order is displayed
            Application.DocumentManager.MdiActiveDocument.Editor.Regen();
        }

        [CommandMethod("LayoutReorder")]
        public static void LayoutReorder()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            string layoutName = "Layout3";

            // Get the layout and plot settings of the named pagesetup
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Reference the Layout Manager
                LayoutManager acLayoutMgr = LayoutManager.Current;

                // Create a new layout
                acLayoutMgr.CreateLayout(layoutName);

                // Get the layouts in the drawing
                DBDictionary lays = acTrans.GetObject(acCurDb.LayoutDictionaryId,
                                                      OpenMode.ForRead) as DBDictionary;

                // Step through each layout

                foreach (DBDictionaryEntry item in lays)
                {
                    // Open the layout
                    Layout acLayout = acTrans.GetObject(item.Value,
                                                        OpenMode.ForWrite) as Layout;

                    // Set the order based on the layout name
                    switch (acLayout.LayoutName)
                    {
                        case "Layout1":
                            acLayout.TabOrder = 2;
                            break;
                        case "Layout2":
                            acLayout.TabOrder = 1;
                            break;
                        case "Layout3":
                            acLayout.TabOrder = 0;
                            break;
                    }
                }

                // Save the changes made
                acTrans.Commit();

                // Regen the display so the new layout and correct order is displayed
                Application.DocumentManager.MdiActiveDocument.Editor.Regen();
            }
        }

        // Copy plot settings from layout to a pagesetup
        [CommandMethod("PageSetupCopyFromLayout")]
        public static void PageSetupCopyFromLayout()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Specify the layout and pagesetup name (both layout and pagesetup must exist in the drawing)
            string layoutName = "Layout1";
            string pagesetupName = "MyPagesetup";

            // Get the layout and plot settings of the named pagesetup
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Reference the Layout Manager
                LayoutManager acLayoutMgr = LayoutManager.Current;

                // Get the layout
                Layout acLayout = acTrans.GetObject(acLayoutMgr.GetLayoutId(layoutName),
                                                    OpenMode.ForRead) as Layout;

                // Get the PlotInfo from the layout
                PlotInfo acPlInfo = new PlotInfo();
                acPlInfo.Layout = acLayout.ObjectId;

                DBDictionary plSets = acTrans.GetObject(acCurDb.PlotSettingsDictionaryId,
                                                        OpenMode.ForRead) as DBDictionary;
                PlotSettings plSet = default(PlotSettings);

                // Check to see if the pagesetup already exists

                if (plSets.Contains(pagesetupName) == false)
                {
                    // Create a new PlotSettings object: True - model space, False - named layout
                    plSet = new PlotSettings(acLayout.ModelType);

                    // Copy the plot settings from the layout
                    plSet.CopyFrom(acLayout);

                    // Since the settinsg are coming from a layout, a name needs to be assigned to the plot settings
                    plSet.PlotSettingsName = pagesetupName;

                    // Add the plot settings to the dictionary and database
                    plSet.AddToPlotSettingsDictionary(acCurDb);
                    acTrans.AddNewlyCreatedDBObject(plSet, true);
                }
                else
                {
                    // Get the existing plot settings that has the same name
                    plSet = plSets.GetAt(pagesetupName).GetObject(OpenMode.ForWrite) as PlotSettings;

                    // Copy the plot settings to the existing pagesetup
                    plSet.CopyFrom(acLayout);

                    // Set the name for the pagesetup as it is removed by copy the layout settings
                    plSet.PlotSettingsName = pagesetupName;
                }

                // Save the changes made
                acTrans.Commit();
            }
        }

        [CommandMethod("PageSetupImportFromDrawing")]
        public static void PageSetupImportFromDrawing()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Specify the pagesetup and drawing file to work with
            string pagesetupName = "DWF6_ARCH D_LAND";
            string filename = "C:\\Program Files\\Autodesk\\AutoCAD 2013\\Sample\\Sheet Sets\\Architectural\\A-01.dwg";

            // Create a new database object and open the drawing into memory
            Database acExDb = new Database(false, true);
            acExDb.ReadDwgFile(filename, FileOpenMode.OpenForReadAndAllShare, true, "");

            PlotSettings plSetEx = null;

            // Get the plot settings of the named pagesetup from the external drawing file
            using (Transaction acTrans = acExDb.TransactionManager.StartTransaction())
            {
                DBDictionary plSetsEx = acTrans.GetObject(acExDb.PlotSettingsDictionaryId,
                                                          OpenMode.ForRead) as DBDictionary;

                // Check to see if the pagesetup exists in the external drawing
                if (plSetsEx.Contains(pagesetupName) == true)
                {
                    plSetEx = plSetsEx.GetAt(pagesetupName).GetObject(OpenMode.ForRead) as PlotSettings;
                }

                // Discard the changes made
                acTrans.Abort();
            }

            // If the pagesetup existed in the external drawing, then proceed
            if ((plSetEx == null) == false)
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    DBDictionary plSets = acTrans.GetObject(acCurDb.PlotSettingsDictionaryId,
                                                            OpenMode.ForRead) as DBDictionary;
                    PlotSettings plSet = default(PlotSettings);

                    // Check to see if the pagesetup exists in the current drawing, if so create the new pagesetup
                    if (plSets.Contains(pagesetupName) == false)
                    {
                        // Create a new PlotSettings object: True - model space, False - named layout
                        plSet = new PlotSettings(false);
                        plSet.PlotSettingsName = pagesetupName;
                        plSet.AddToPlotSettingsDictionary(acCurDb);
                        acTrans.AddNewlyCreatedDBObject(plSet, true);
                    }
                    else
                    {
                        // Pagesetup exists, so lets update it
                        plSet = plSets.GetAt(pagesetupName).GetObject(OpenMode.ForWrite) as PlotSettings;
                    }

                    // Copy the settings from the pagesetup in the external drawing to the current drawing
                    plSet.CopyFrom(plSetEx);

                    // Save the changes made
                    acTrans.Commit();
                }
            }
            else
            {
                acDoc.Editor.WriteMessage("\nPage setup '" + pagesetupName + 
                                          "' could not be imported from '" + filename + "'.");
            }

            // Close the external drawing file
            acExDb.Dispose();
        }

        [CommandMethod("PublishOverrideCustomWrite")]
        public static void PublishOverride()
        {
            using (DsdEntryCollection dsdDwgFiles = new DsdEntryCollection())
            {
                // Add first drawing file
                using (DsdEntry dsdDwgFile1 = new DsdEntry())
                {

                    // Set the file name and layout
                    dsdDwgFile1.DwgName = "C:\\Program Files\\Autodesk\\AutoCAD 2013\\Sample\\" + 
                                          "Sheet Sets\\Architectural\\A-01.dwg";
                    dsdDwgFile1.Layout = "MAIN AND SECOND FLOOR PLAN";
                    dsdDwgFile1.Title = "A-01 MAIN AND SECOND FLOOR PLAN";

                    // Set the page setup override
                    dsdDwgFile1.Nps = "";
                    dsdDwgFile1.NpsSourceDwg = "";

                    dsdDwgFiles.Add(dsdDwgFile1);
                }

                // Add second drawing file
                using (DsdEntry dsdDwgFile2 = new DsdEntry())
                {

                    // Set the file name and layout
                    dsdDwgFile2.DwgName = "C:\\Program Files\\Autodesk\\AutoCAD 2013\\Sample\\" + 
                                          "Sheet Sets\\Architectural\\A-02.dwg";
                    dsdDwgFile2.Layout = "ELEVATIONS";
                    dsdDwgFile2.Title = "A-02 ELEVATIONS";

                    // Set the page setup override
                    dsdDwgFile2.Nps = "";
                    dsdDwgFile2.NpsSourceDwg = "";

                    dsdDwgFiles.Add(dsdDwgFile2);
                }

                string fileNameDSD = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + 
                                     "\\batchdrawings2.dsd";

                // Create the drawing set description (DSD) file
                StreamWriter sw = File.CreateText(fileNameDSD);
                sw.WriteLine("[DWF6Version]");
                sw.WriteLine("Ver=1");
                sw.WriteLine("[DWF6MinorVersion]");
                sw.WriteLine("MinorVer=1");

                foreach (DsdEntry dsdDwgFile_loopVariable in dsdDwgFiles)
                {
                    sw.WriteLine("[DWF6Sheet:" + dsdDwgFile_loopVariable.Title + "]");
                    sw.WriteLine("DWG=" + dsdDwgFile_loopVariable.DwgName);
                    sw.WriteLine("Layout=" + dsdDwgFile_loopVariable.Layout);
                    sw.WriteLine("Setup=");
                    sw.WriteLine("OriginalSheetPath=" + dsdDwgFile_loopVariable.DwgName);
                    sw.WriteLine("Has Plot Port=0");
                    sw.WriteLine("Has3DDWF=0");
                }

                sw.WriteLine("[Target]");
                sw.WriteLine("Type=1");
                sw.WriteLine("DWF=" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + 
                             "\\MyPublish2.dwf");
                sw.WriteLine("OUT=" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + 
                             "\\");
                sw.WriteLine("PWD=");
                sw.WriteLine("[AutoCAD Block Data]");
                sw.WriteLine("IncludeBlockInfo=0");
                sw.WriteLine("BlockTmplFilePath=");
                sw.WriteLine("[SheetSet Properties]");
                sw.WriteLine("IsSheetSet=FALSE");
                sw.WriteLine("IsHomogeneous=FALSE");
                sw.WriteLine("SheetSet Name=");
                sw.WriteLine("NoOfCopies=1");
                sw.WriteLine("PlotStampOn=FALSE");
                sw.WriteLine("ViewFile=TRUE");
                sw.WriteLine("JobID=0");
                sw.WriteLine("SelectionSetName=");
                sw.WriteLine("AcadProfile=<<Unnamed Profile>>");
                sw.WriteLine("CategoryName=");
                sw.WriteLine("LogFilePath=");
                sw.WriteLine("IncludeLayer=TRUE");
                sw.WriteLine("LineMerge=FALSE");
                sw.WriteLine("CurrentPrecision=For Architecture");
                sw.WriteLine("PromptForDwfName=TRUE");
                sw.WriteLine("PwdProtectPublishedDWF=FALSE");
                sw.WriteLine("PromptForPwd=FALSE");
                sw.WriteLine("RepublishingMarkups=FALSE");
                sw.WriteLine("DSTPath=");
                sw.WriteLine("PublishSheetSetMetadata=FALSE");
                sw.WriteLine("PublishSheetMetadata=FALSE");
                sw.WriteLine("3DDWFOptions=0 0");

                sw.Flush();
                sw.Close();

                try
                {
                    // Get the DWF6 ePlot.pc3 and use it as a 
                    // device override for all the layouts
                    PlotConfig acPlCfg = null;

                    foreach (PlotConfigInfo acPlCfgInfo in PlotConfigManager.Devices)
                    {
                        if (acPlCfgInfo.DeviceName.ToUpper() == "DWF6 EPLOT.PC3")
                        {
                            acPlCfg = PlotConfigManager.SetCurrentConfig(acPlCfgInfo.DeviceName);
                            break;
                        }
                    }

                    using (DsdData dsdDataFile = new DsdData())
                    {
                        dsdDataFile.ReadDsd(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + 
                                            "\\batchdrawings2.dsd");

                        Application.Publisher.PublishExecute(dsdDataFile, acPlCfg);
                    }

                }
                catch (Autodesk.AutoCAD.Runtime.Exception es)
                {
                    System.Windows.Forms.MessageBox.Show(es.Message);
                }
            }
        }
    }
}
