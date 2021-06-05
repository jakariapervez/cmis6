
// Main AutoCAD namespaces
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace CP2071_CSharp
{
    class AU2012_Layouts
    {
        // List all the layouts in the current drawing
        [CommandMethod("LayoutList")]
        public void LayoutList()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Get the layout dictionary of the current database
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary lays = 
                    acTrans.GetObject(acCurDb.LayoutDictionaryId, 
                    OpenMode.ForRead) as DBDictionary;

                acDoc.Editor.WriteMessage("\nLayouts:");

                // Step through and list each named layout and Model
                foreach (DBDictionaryEntry item in lays)
                {
                    acDoc.Editor.WriteMessage("\n  " + item.Key);
                }

            // Abort the changes to the database
            acTrans.Abort();
            }
        }

        // Create a new layout with the LayoutManager
        [CommandMethod("LayoutCreate")]
        public void LayoutCreate()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Get the layout and plot settings of the named pagesetup
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Reference the Layout Manager
                LayoutManager acLayoutMgr = LayoutManager.Current;

                // Create the new layout with default settings
                ObjectId objID = acLayoutMgr.CreateLayout("newLayout");

                // Open the layout
                Layout acLayout = acTrans.GetObject(objID,
                                                    OpenMode.ForRead) as Layout;

                // Set the layout current if it is not already
                if (acLayout.TabSelected == false)
                {
                    acLayoutMgr.CurrentLayout = acLayout.LayoutName;
                }

                // Output some information related to the layout object
                acDoc.Editor.WriteMessage("\nTab Order: " + acLayout.TabOrder +
                                          "\nTab Selected: " + acLayout.TabSelected +
                                          "\nBlock Table Record ID: " +
                                          acLayout.BlockTableRecordId.ToString());

                // Save the changes made
                acTrans.Commit();
            }
        }

        // Import a layout from an external drawing
        [CommandMethod("LayoutImport")]
        public void LayoutImport()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Specify the layout name and drawing file to work with
            string layoutName = "MAIN AND SECOND FLOOR PLAN";
            string filename = "C:\\Program Files\\Autodesk\\AutoCAD 2013\\" +
                              "Sample\\Sheet Sets\\Architectural\\A-01.dwg";

            // Create a new database object and open the drawing into memory
            Database acExDb = new Database(false, true);
            acExDb.ReadDwgFile(filename, FileOpenMode.OpenForReadAndAllShare, true, "");

            // Create a transaction for the external drawing
            using (Transaction acTransEx = acExDb.TransactionManager.StartTransaction())
            {
                // Get the layouts dictionary
                DBDictionary layoutsEx =
                    acTransEx.GetObject(acExDb.LayoutDictionaryId, 
                                        OpenMode.ForRead) as DBDictionary;

                // Check to see if the layout exists in the external drawing
                if (layoutsEx.Contains(layoutName) == true)
                {

                    // Get the layout and block objects from the external drawing
                    Layout layEx =
                        layoutsEx.GetAt(layoutName).GetObject(OpenMode.ForRead) as Layout;
                    BlockTableRecord blkBlkRecEx =
                        acTransEx.GetObject(layEx.BlockTableRecordId,
                                            OpenMode.ForRead) as BlockTableRecord;

                    // Get the objects from the block associated with the layout
                    ObjectIdCollection idCol = new ObjectIdCollection();
                    foreach (ObjectId id in blkBlkRecEx)
                    {
                        idCol.Add(id);
                    }

                    // Create a transaction for the current drawing
                    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                    {
                        // Get the block table and create a new block
                        // then copy the objects between drawings
                        BlockTable blkTbl =
                            acTrans.GetObject(acCurDb.BlockTableId, 
                                              OpenMode.ForWrite) as BlockTable;

                        using (BlockTableRecord blkBlkRec = new BlockTableRecord())
                        {
                            int layoutCount = layoutsEx.Count - 1;

                            blkBlkRec.Name = "*Paper_Space" + layoutCount.ToString();
                            blkTbl.Add(blkBlkRec);
                            acTrans.AddNewlyCreatedDBObject(blkBlkRec, true);
                            acExDb.WblockCloneObjects(idCol,
                                                      blkBlkRec.ObjectId,
                                                      new IdMapping(),
                                                      DuplicateRecordCloning.Ignore,
                                                      false);

                            // Create a new layout and then copy properties between drawings
                            DBDictionary layouts =
                                acTrans.GetObject(acCurDb.LayoutDictionaryId,
                                                  OpenMode.ForWrite) as DBDictionary;

                            using (Layout lay = new Layout())
                            {
                                lay.LayoutName = layoutName;
                                lay.AddToLayoutDictionary(acCurDb, blkBlkRec.ObjectId);
                                acTrans.AddNewlyCreatedDBObject(lay, true);
                                lay.CopyFrom(layEx);

                                DBDictionary plSets =
                                    acTrans.GetObject(acCurDb.PlotSettingsDictionaryId,
                                                      OpenMode.ForRead) as DBDictionary;

                                // Check to see if a named page setup was assigned to the layout,
                                // if so then copy the page setup settings
                                if (lay.PlotSettingsName != "")
                                {
                                    // Check to see if the page setup exists
                                    if (plSets.Contains(lay.PlotSettingsName) == false)
                                    {
                                        plSets.UpgradeOpen();

                                        using (PlotSettings plSet = new PlotSettings(lay.ModelType))
                                        {
                                            plSet.PlotSettingsName = lay.PlotSettingsName;
                                            plSet.AddToPlotSettingsDictionary(acCurDb);
                                            acTrans.AddNewlyCreatedDBObject(plSet, true);

                                            DBDictionary plSetsEx =
                                                acTransEx.GetObject(acExDb.PlotSettingsDictionaryId,
                                                                    OpenMode.ForRead) as DBDictionary;

                                            PlotSettings plSetEx =
                                                plSetsEx.GetAt(lay.PlotSettingsName).GetObject(
                                                               OpenMode.ForRead) as PlotSettings;

                                            plSet.CopyFrom(plSetEx);
                                        }
                                    }
                                }
                            }
                        }

                        // Regen the drawing to get the layout tab to display
                        acDoc.Editor.Regen();

                        // Save the changes made
                        acTrans.Commit();
                    }
                }
                else
                {
                    // Display a message if the layout could not be found in the specified drawing
                    acDoc.Editor.WriteMessage("\nLayout '" + layoutName +
                                              "' could not be imported from '" + filename + "'.");
                }

                // Discard the changes made to the external drawing file
                acTransEx.Abort();
            }

            // Close the external drawing file
            acExDb.Dispose();
        }
    }
}
