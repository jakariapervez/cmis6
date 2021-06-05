// Standard .NET namespaces
using System;
using System.IO;

// Main AutoCAD namespaces
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.PlottingServices;

// AutoCAD Sheet Set API
using ACSMCOMPONENTS19Lib;

namespace CP2071_CSharp
{
    class AU2012_Publishing
    {
        // Publishes all the drawings in a sheet set to a multi-sheet DWF
        [CommandMethod("PublishSheetSet")]
        public void PublishSheetSet()
        {
            // Get and set the BackgroundPlot system variable value
            object backPlot = Application.GetSystemVariable("BACKGROUNDPLOT");
            Application.SetSystemVariable("BACKGROUNDPLOT", 0);

            // DSD filename
            string dsdFilename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\batchdrawings.dsd";

            // Delete the DSD file if it exists
            if (File.Exists(dsdFilename) == true)
                File.Delete(dsdFilename);

            // Get a reference to the Sheet Set Manager object
            IAcSmSheetSetMgr sheetSetManager = new AcSmSheetSetMgr();

            // Open a Sheet Set file
            AcSmDatabase sheetSetDatabase = sheetSetManager.OpenDatabase("C:\\Program Files\\Autodesk\\AutoCAD 2013\\Sample\\Sheet Sets" + 
                                                                         "\\Architectural\\IRD Addition.dst", false);

            AcSmSheetSet sheetSet = sheetSetDatabase.GetSheetSet();

            IAcSmEnumPersist enumerator = default(IAcSmEnumPersist);
            IAcSmPersist itemSheetSet = default(IAcSmPersist);

            using (DsdEntryCollection dsdDwgFiles = new DsdEntryCollection())
            {

                // Get the enumerator for the objects in the sheet set
                enumerator = sheetSetDatabase.GetEnumerator();
                itemSheetSet = enumerator.Next();

                // Step through the objects in the sheet set
                while ((itemSheetSet != null))
                {
                    // Increment the counter of the object is a sheet

                    if (itemSheetSet.GetTypeName() == "AcSmSheet")
                    {
                        AcSmSheet sheet = itemSheetSet as AcSmSheet;

                        // Add drawing file
                        if (File.Exists(sheet.GetLayout().GetFileName()) == true)
                        {
                            using (DsdEntry dsdDwgFile = new DsdEntry())
                            {

                                // Set the file name and layout
                                dsdDwgFile.DwgName = sheet.GetLayout().GetFileName();
                                dsdDwgFile.Layout = sheet.GetLayout().GetName();
                                dsdDwgFile.Title = sheet.GetName();

                                // Set the page setup override
                                dsdDwgFile.Nps = "";
                                dsdDwgFile.NpsSourceDwg = "";

                                dsdDwgFiles.Add(dsdDwgFile);
                            }
                        }
                    }

                    // Get next object
                    itemSheetSet = enumerator.Next();
                }

                if (dsdDwgFiles.Count > 0)
                {
                    // Set the properties for the DSD file and then write it out
                    using (DsdData dsdFileData = new DsdData())
                    {

                        // Set the version of the DWF 6 format
                        dsdFileData.MajorVersion = 1;
                        dsdFileData.MinorVersion = 1;

                        // Set the target information for publishing
                        dsdFileData.DestinationName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + sheetSetDatabase.GetSheetSet().GetName() + ".dwf";
                        dsdFileData.ProjectPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        // Set the type of output that should be generated
                        dsdFileData.SheetType = SheetType.MultiDwf;

                        // Set the drawings that should be added to the publication
                        dsdFileData.SetDsdEntryCollection(dsdDwgFiles);

                        // Sheet set properties
                        dsdFileData.IsSheetSet = true;
                        dsdFileData.SheetSetName = sheetSetDatabase.GetName();
                        dsdFileData.SelectionSetName = "";
                        dsdFileData.IsHomogeneous = false;

                        // Set the general publishing properties
                        dsdFileData.CategoryName = "";
                        dsdFileData.NoOfCopies = 1;
                        dsdFileData.LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\myBatch.txt";
                        dsdFileData.Password = "";
                        dsdFileData.PlotStampOn = false;
                        dsdFileData.PromptForDwfName = false;

                        // Set the DWF 3D options
                        dsdFileData.Dwf3dOptions.GroupByXrefHierarchy = false;
                        dsdFileData.Dwf3dOptions.PublishWithMaterials = false;

                        // Create the DSD file
                        dsdFileData.WriteDsd(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\batchdrawings.dsd");

                        // Track the progress with a Progress dialog
                        using (PlotProgressDialog acPlProgDlg = new PlotProgressDialog(false, dsdDwgFiles.Count, false))
                        {

                            // Define the status messages to display 
                            // when plotting starts
                            acPlProgDlg.set_PlotMsgString(PlotMessageIndex.DialogTitle, "Plot Progress");
                            acPlProgDlg.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage, "Cancel Job");
                            acPlProgDlg.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage, "Cancel Sheet");
                            acPlProgDlg.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption, "Sheet Set Progress");
                            acPlProgDlg.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption, "Sheet Progress");

                            // Set the plot progress range
                            acPlProgDlg.LowerPlotProgressRange = 0;
                            acPlProgDlg.UpperPlotProgressRange = 100;
                            acPlProgDlg.PlotProgressPos = 0;

                            acPlProgDlg.LowerSheetProgressRange = 0;
                            acPlProgDlg.UpperSheetProgressRange = 100;

                            acPlProgDlg.IsVisible = true;

                            try
                            {
                                // Publish the drawing using the DSD in the foreground
                                Application.Publisher.PublishDsd(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\batchdrawings.dsd", acPlProgDlg);

                            }
                            catch (Autodesk.AutoCAD.Runtime.Exception es)
                            {
                                System.Windows.Forms.MessageBox.Show(es.Message);
                            }
                            finally
                            {
                                acPlProgDlg.Destroy();
                                acPlProgDlg.Dispose();
                            }
                        }
                    }
                }
            }

            // Close the sheet set
            sheetSetManager.Close(sheetSetDatabase);

            // Restore the previous value for the BackgroundPlot system variable
            Application.SetSystemVariable("BACKGROUNDPLOT", backPlot);
        }

        // Publishes a few drawings in the background to PDF file
        [CommandMethod("PublishPDF")]
        public static void PublishPDF()
        {
            using (DsdEntryCollection dsdDwgFiles = new DsdEntryCollection())
            {

                // Add first drawing file
                using (DsdEntry dsdDwgFile1 = new DsdEntry())
                {

                    // Set the file name and layout
                    dsdDwgFile1.DwgName = "C:\\Program Files\\Autodesk\\AutoCAD 2013\\Sample\\" + "Sheet Sets\\Architectural\\A-01.dwg";
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
                    dsdDwgFile2.DwgName = "C:\\Program Files\\Autodesk\\AutoCAD 2013\\Sample\\" + "Sheet Sets\\Architectural\\A-02.dwg";
                    dsdDwgFile2.Layout = "ELEVATIONS";
                    dsdDwgFile2.Title = "A-02 ELEVATIONS";

                    // Set the page setup override
                    dsdDwgFile2.Nps = "";
                    dsdDwgFile2.NpsSourceDwg = "";

                    dsdDwgFiles.Add(dsdDwgFile2);
                }

                // Set the properties for the DSD file and then write it out
                using (DsdData dsdFileData = new DsdData())
                {

                    // Set the target information for publishing
                    dsdFileData.DestinationName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyPublish2.pdf";
                    dsdFileData.ProjectPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
                    dsdFileData.SheetType = SheetType.MultiPdf;

                    // Set the drawings that should be added to the publication
                    dsdFileData.SetDsdEntryCollection(dsdDwgFiles);


                    // Set the general publishing properties
                    dsdFileData.LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\myBatch.txt";

                    // Create the DSD file
                    dsdFileData.WriteDsd(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\batchdrawings2.dsd");

                    try
                    {
                        // Publish the specified drawing files in the DSD file, and
                        // honor the behavior of the BACKGROUNDPLOT system variable
                        using (DsdData dsdDataFile = new DsdData())
                        {
                            dsdDataFile.ReadDsd(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\batchdrawings2.dsd");

                            // Get the DWG to PDF.pc3 and use it as a 
                            // device override for all the layouts
                            PlotConfig acPlCfg = PlotConfigManager.SetCurrentConfig("DWG to PDF.PC3");

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
}
