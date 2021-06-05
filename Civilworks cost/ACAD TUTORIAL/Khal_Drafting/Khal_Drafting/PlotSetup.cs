using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
/*using special import for viewport creation*/
using System.Runtime.InteropServices;

namespace Khal_Deafting
{

   


    public class PlotSetup

    {
 
        // Used to set a viewport current
        [DllImport("accore.dll", CallingConvention = CallingConvention.Cdecl,
         EntryPoint = "?acedSetCurrentVPort@@YA?AW4ErrorStatus@Acad@@PEBVAcDbViewport@@@Z")]
       // extern static private int acedSetCurrentVPort(IntPtr AcDbVport);
        extern static private int acedSetCurrentVPort(IntPtr AcDbVport);
        public  void CreateFloatingViewport()

        {

            // Get the current document and database, and start a transaction

            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            Database acCurDb = acDoc.Database;
            Editor edt = acDoc.Editor;


            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())

            {
                try 
                {


                /* Open the Block table for read*/

                BlockTable acBlkTbl;

                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,

                                             OpenMode.ForRead) as BlockTable;
                    // Open the Block table record Paper space for write
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.PaperSpace],

                                                    OpenMode.ForWrite) as BlockTableRecord;
                    // Switch to the previous Paper space layout
                    Application.SetSystemVariable("TILEMODE", 0);
                    acDoc.Editor.SwitchToPaperSpace();
                    // Create a Viewport
                    Viewport acVport = new Viewport();
                    acVport.SetDatabaseDefaults();
                    acVport.CenterPoint = new Point3d(3.25, 3, 0);
                    acVport.Width = 6;
                    acVport.Height = 5;
                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acVport);
                    acTrans.AddNewlyCreatedDBObject(acVport, true);
                    // Change the view direction
                    acVport.ViewDirection = new Vector3d(1, 1, 1);
                    // Enable the viewport
                    acVport.On = true;
                    // Activate model space in the viewport
                    acDoc.Editor.SwitchToModelSpace();
                    // Set the new viewport current via an imported ObjectARX function
                    acedSetCurrentVPort(acVport.UnmanagedObject);
                    // Save the new objects to the database
                    acTrans.Commit();







                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n " + ex.Source);
                }
                
            }

        }

        public  ObjectId CreateAndMakeLayoutCurrent(string name)

        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            LayoutManager acLayoutMgr;
            acLayoutMgr = LayoutManager.Current;
            //get the layout id
            var lid = acLayoutMgr.GetLayoutId(name);
            //if layout does not exist we create it.
            if(!lid.IsValid)
                {
                    lid = acLayoutMgr.CreateLayout(name);

                }
            acLayoutMgr.CurrentLayout = name;     
            return lid;
           
        }
        /*Applying Plot Settings to Current Layout*/
        /*Apply plot settings to the provided layout*/
        public void SetPlotSettings(Layout lay, string pageSize, string styleSheet, 
            string device)

        {
            using (var ps = new PlotSettings(lay.ModelType))
            {
                ps.CopyFrom(lay);
                var psv = PlotSettingsValidator.Current;
                // Set the device
                var devs = psv.GetPlotDeviceList();
                if (devs.Contains(device))
                {
                    psv.SetPlotConfigurationName(ps, device, null);
                    psv.RefreshLists(ps);
                }
                // Set the media name/size
                var mns = psv.GetCanonicalMediaNameList(ps);
                if (mns.Contains(pageSize))
                {
                    psv.SetCanonicalMediaName(ps, pageSize);

                }
                // Set the pen settings
                var ssl = psv.GetPlotStyleSheetList();
                if (ssl.Contains(styleSheet))
                {
                    psv.SetCurrentStyleSheet(ps, styleSheet);

                }
                /*settoing Plot units to milimeter*/
                
                // Copy the PlotSettings data back to the Layout
                var upgraded = false;
                if (!lay.IsWriteEnabled)

                {

                    lay.UpgradeOpen();
                    upgraded = true;
                }
                lay.CopyFrom(ps);
                if (upgraded)
                {
                    lay.DowngradeOpen();
                }

            }
            
        }
        public void deleteViewPorts(string acLyoutName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using(Transaction trans=db.TransactionManager.StartTransaction())
            {
                try              
                
                {
                    LayoutManager acLayoutMgr = LayoutManager.Current;
                    DBDictionary LayoutDict = trans.GetObject(db.LayoutDictionaryId,
                        OpenMode.ForRead) as DBDictionary;
                    Layout CurrenLo = trans.GetObject((ObjectId)LayoutDict[acLyoutName], OpenMode.ForRead) as Layout;
                    BlockTableRecord btr = trans.GetObject(CurrenLo.BlockTableRecordId, OpenMode.ForRead) as BlockTableRecord;
                    foreach (ObjectId ID in btr)
                    {
                        Viewport VP = trans.GetObject(ID, OpenMode.ForRead) as Viewport;
                        if (VP != null)
                        {
                            VP.UpgradeOpen();
                            VP.Erase();
                        }

                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n" + ex.ToString());
                    edt.WriteMessage("\n " + ex.Source);
                
                }



            }



        }
        public  Extents2d GetMaximumExtents(Layout lay)

        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            try 
            {

                var div = lay.PlotPaperUnits == PlotPaperUnit.Inches ? 25.4 : 1.0;
                edt.WriteMessage("papaer units= " + div.ToString());
                var min = lay.PlotPaperMargins.MinPoint;
                edt.WriteMessage("\n xmin=" + min.X + " ymin=" + min.Y);
                //  var max = lay.PlotPaperMargins.MaxPoint.GetAsVector();
                var max = lay.PlotPaperSize;
                edt.WriteMessage("\n xmax=" + max.X + " ymax=" + max.Y);



            }
            catch (System.Exception ex)
            {
                edt.WriteMessage("\n " + ex.Source);
            
            }
            
          






            return new Extents2d();

        }

        public  void createLayout(string layoutName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            try
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    //creating a layout and making it current
                    LayoutManager acLayoutMgr;
                    acLayoutMgr = LayoutManager.Current;
                    /**/
                    //get the layout id
                    var lid = acLayoutMgr.GetLayoutId(layoutName);
                    //if layout does not exist we create it.
                    if (!lid.IsValid)
                    {
                        lid = acLayoutMgr.CreateLayout(layoutName);

                    }
                    acLayoutMgr.CurrentLayout = layoutName;



                    //opening the created layout
                    Layout mylayout;
                    mylayout = trans.GetObject(lid, OpenMode.ForWrite) as Layout;

                    this.SetPlotSettings(mylayout, "A3", "monochrome.ctb", "DWF6 ePlot.pc3");
                    //getiing extent of the layout 
                  //  var ext = new Extents2d();
                   // ext = this.GetMaximumExtents(mylayout);
                   //this.deleteViewPorts(layoutName);
                   // this.CreateFloatingViewport();
                    //commiting transaction
                    trans.Commit();
                }

            }
            catch(System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.ToString());
                edt.WriteMessage("\n" + ex.Source);

            }



        }

        public void createLayout(string layoutName,DrawingSheet ds)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            try
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    //creating a layout and making it current
                    LayoutManager acLayoutMgr;
                    acLayoutMgr = LayoutManager.Current;
                    /**/
                    //get the layout id
                    var lid = acLayoutMgr.GetLayoutId(layoutName);
                    //if layout does not exist we create it.
                    if (!lid.IsValid)
                    {
                        lid = acLayoutMgr.CreateLayout(layoutName);

                    }
                    acLayoutMgr.CurrentLayout = layoutName;



                    //opening the created layout
                    Layout mylayout;
                    mylayout = trans.GetObject(lid, OpenMode.ForWrite) as Layout;

                    this.SetPlotSettings(mylayout, "A3", "monochrome.ctb", "DWF6 ePlot.pc3");
                    /*Getting Block Table Record for current Layout*/
                    BlockTableRecord btr = trans.GetObject(mylayout.BlockTableRecordId, OpenMode.ForRead) as BlockTableRecord;
                    /*Begining of Zoom View port Code #####################*/
                    foreach (ObjectId obj in btr)
                    {
                        Viewport vp = trans.GetObject(obj, OpenMode.ForRead) as Viewport;
                        if (vp != null)
                        {
                            edt.WriteMessage("\nnumber of Viewport = {0}", vp.Number);
                            this.displayViewport(vp);
                            /*getting  aspect ratio of viewport*/
                            double mScrRatio;
                            mScrRatio = (vp.Width / vp.Height);
                            /*getting extent of the sheet */
                            Point3d mMaxExt = ds.sheetExtents.MaxPoint;
                            Point3d mMinExt = ds.sheetExtents.MinPoint;
                            edt.WriteMessage("\n max.X=" + mMaxExt.X + " min_X=" + mMinExt.X);
                            Extents3d mExtents = new Extents3d();
                            mExtents.Set(mMinExt, mMaxExt);
                            // prepare Matrix for DCS to WCS transformation
                            Matrix3d matWCS2DCS;
                            matWCS2DCS = Matrix3d.PlaneToWorld(vp.ViewDirection);
                            matWCS2DCS = Matrix3d.Displacement(vp.ViewTarget - Point3d.Origin) * matWCS2DCS;
                            matWCS2DCS = Matrix3d.Rotation(-vp.TwistAngle, vp.ViewDirection, vp.ViewTarget) * matWCS2DCS;
                            matWCS2DCS = matWCS2DCS.Inverse();
                            // tranform the extents to the DCS
                            // defined by the viewdir
                            mExtents.TransformBy(matWCS2DCS);
                            //width of the extents in current view
                            double mWidth;
                            mWidth = (mExtents.MaxPoint.X - mExtents.MinPoint.X);
                            // height of the extents in current view
                            double mHeight;
                            mHeight = (mExtents.MaxPoint.Y - mExtents.MinPoint.Y);
                            // get the view center point
                            Point2d mCentPt = new Point2d(
                                ((mExtents.MaxPoint.X + mExtents.MinPoint.X) * 0.5),
                                ((mExtents.MaxPoint.Y + mExtents.MinPoint.Y) * 0.5)
                                                        );
                            // check if the width 'fits' in current window,
                            // if not then get the new height as
                            // per the viewports aspect ratio
                            if (mWidth > (mHeight * mScrRatio))
                                mHeight = mWidth / mScrRatio;
                            // set the viewport parameters
                            if (vp.Number == 2)
                            {
                                vp.UpgradeOpen();
                               // set the view height - adjusted by 1%
                                vp.ViewHeight = mHeight * 1.01;
                                // set the view center
                                vp.ViewCenter = mCentPt;
                                vp.Visible = true;
                                vp.On = true;
                                vp.UpdateDisplay();
                                edt.SwitchToModelSpace();
                                Application.SetSystemVariable("CVPORT", vp.Number);
                            }
                            if (vp.Number == 3)
                            {
                                vp.UpgradeOpen();
                                vp.ViewHeight = mHeight * 1.25;
                                //set the view center                                
                                vp.ViewCenter = mCentPt;
                                vp.Visible = true;
                                vp.On = true;
                                vp.UpdateDisplay();
                                edt.SwitchToModelSpace();
                                Application.SetSystemVariable("CVPORT", vp.Number);

                            }



                        }


                    }



                    /*End of Zoom View port Code #####################*/
                    //getiing extent of the layout 
                    //  var ext = new Extents2d();
                    // ext = this.GetMaximumExtents(mylayout);
                    //this.deleteViewPorts(layoutName);
                    // this.CreateFloatingViewport();
                    //commiting transaction
                    trans.Commit();
                }

            }
            catch (System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.ToString());
                edt.WriteMessage("\n" + ex.Source);

            }



        }
        public void createLayout2(string layoutName, DrawingSheet ds)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            try
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    //creating a layout and making it current
                    LayoutManager acLayoutMgr;
                    acLayoutMgr = LayoutManager.Current;
                    /**/
                    //get the layout id
                    var lid = acLayoutMgr.GetLayoutId(layoutName);
                    //if layout does not exist we create it.
                    if (!lid.IsValid)
                    {
                        lid = acLayoutMgr.CreateLayout(layoutName);

                    }
                    acLayoutMgr.CurrentLayout = layoutName;



                    //opening the created layout
                    Layout mylayout;
                    mylayout = trans.GetObject(lid, OpenMode.ForWrite) as Layout;

                    this.SetPlotSettings(mylayout, "A3", "monochrome.ctb", "FoxitPhantomPDFPrinter.pc3");
                    /*Getting Block Table Record for current Layout*/
                    BlockTableRecord btr = trans.GetObject(mylayout.BlockTableRecordId, OpenMode.ForRead) as BlockTableRecord;
                    this.ViewportsCreateFloating();


                    /*Begining of Zoom View port Code #####################*/
                    foreach (ObjectId obj in btr)
                    {
                        Viewport vp = trans.GetObject(obj, OpenMode.ForRead) as Viewport;
                        if (vp != null)
                        {
                            edt.WriteMessage("\nnumber of Viewport = {0}", vp.Number);
                            this.displayViewport(vp);
                            /*getting  aspect ratio of viewport*/
                            double mScrRatio;
                            mScrRatio = (vp.Width / vp.Height);
                            /*getting extent of the sheet */
                            Point3d mMaxExt = ds.sheetExtents.MaxPoint;
                            Point3d mMinExt = ds.sheetExtents.MinPoint;
                            edt.WriteMessage("\n max.X=" + mMaxExt.X + " min_X=" + mMinExt.X);
                            Extents3d mExtents = new Extents3d();
                            mExtents.Set(mMinExt, mMaxExt);
                            // prepare Matrix for DCS to WCS transformation
                            Matrix3d matWCS2DCS;
                            matWCS2DCS = Matrix3d.PlaneToWorld(vp.ViewDirection);
                            matWCS2DCS = Matrix3d.Displacement(vp.ViewTarget - Point3d.Origin) * matWCS2DCS;
                            matWCS2DCS = Matrix3d.Rotation(-vp.TwistAngle, vp.ViewDirection, vp.ViewTarget) * matWCS2DCS;
                            matWCS2DCS = matWCS2DCS.Inverse();
                            // tranform the extents to the DCS
                            // defined by the viewdir
                            mExtents.TransformBy(matWCS2DCS);
                            //width of the extents in current view
                            double mWidth;
                            mWidth = (mExtents.MaxPoint.X - mExtents.MinPoint.X);
                            // height of the extents in current view
                            double mHeight;
                            mHeight = (mExtents.MaxPoint.Y - mExtents.MinPoint.Y);
                            // get the view center point
                            Point2d mCentPt = new Point2d(
                                ((mExtents.MaxPoint.X + mExtents.MinPoint.X) * 0.5),
                                ((mExtents.MaxPoint.Y + mExtents.MinPoint.Y) * 0.5)
                                                        );
                            // check if the width 'fits' in current window,
                            // if not then get the new height as
                            // per the viewports aspect ratio
                            if (mWidth > (mHeight * mScrRatio))
                                mHeight = mWidth / mScrRatio;
                            // set the viewport parameters
                            if (vp.Number == 2)
                            {
                                vp.UpgradeOpen();
                                // set the view height - adjusted by 1%
                                vp.ViewHeight = mHeight * 1.01;
                                // set the view center
                                vp.ViewCenter = mCentPt;
                                vp.Visible = true;
                                vp.On = true;
                                vp.UpdateDisplay();
                                edt.SwitchToModelSpace();
                                Application.SetSystemVariable("CVPORT", vp.Number);
                            }
                            if (vp.Number == 3)
                            {
                                vp.UpgradeOpen();
                                vp.ViewHeight = mHeight * 1.25;
                                //set the view center                                
                                vp.ViewCenter = mCentPt;
                                vp.Visible = true;
                                vp.On = true;
                                vp.UpdateDisplay();
                                edt.SwitchToModelSpace();
                                Application.SetSystemVariable("CVPORT", vp.Number);

                            }



                        }


                    }



                    /*End of Zoom View port Code #####################*/
                    //getiing extent of the layout 
                    //  var ext = new Extents2d();
                    // ext = this.GetMaximumExtents(mylayout);
                    //this.deleteViewPorts(layoutName);
                    // this.CreateFloatingViewport();
                    //commiting transaction
                    trans.Commit();
                   
                }

            }
            catch (System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.ToString());
                edt.WriteMessage("\n" + ex.Source);

            }



        }
        public void ViewportsCreateFloating()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Editor edt = acDoc.Editor;
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {

                try {
                    // Open the Block table for read
                    BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
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
                        acVport1.CenterPoint = new Point3d(8.25, 5.85, 0);
                        acVport1.Width = 14.85;
                        acVport1.Height = 10.53;

                        // Lock the viewport
                        acVport1.Locked = true;

                        // Set the scale to 1" = 4'
                        // acVport1.CustomScale = 48;

                        // Set visual style
                        /*
                          DBDictionary vStyles =
                              acTrans.GetObject(acCurDb.VisualStyleDictionaryId,
                                                OpenMode.ForRead) as DBDictionary;

                          acVport1.SetShadePlot(ShadePlotType.VisualStyle,
                                                vStyles.GetAt("Sketchy"));
                            */

                        // Add the new object to the block table record and the transaction
                        acBlkTblRec.AppendEntity(acVport1);
                        acTrans.AddNewlyCreatedDBObject(acVport1, true);

                        // Change the view direction and enable the viewport
                        acVport1.ViewDirection = new Vector3d(0, 0, 1);
                        acVport1.On = true;

                        // Activate model space
                       // acDoc.Editor.SwitchToModelSpace();

                        // Set the new viewport current via an imported ObjectARX function
                      //  acedSetCurrentVPort(acVport1.UnmanagedObject);
                    }

                    // Save the new objects to the database
                    acTrans.Commit();
                }
                catch (System.Exception ex) 
                
                {
                    edt.WriteMessage("\n" + ex.Source);
                }
            }
        }
        public void displayViewport(Viewport vp)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            edt.WriteMessage("\n width=" + vp.Width+"height="+vp.Height);
           // edt.WriteMessage("\n vie width="  + "view height=" + vp.Height);
        }

    }
}
