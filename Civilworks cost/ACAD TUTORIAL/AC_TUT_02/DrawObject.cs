using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace AC_TUT_02
{
   public  class DrawObject
    {
        [CommandMethod("DrawLine")]
        public void DrawLine() 
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace],OpenMode.ForWrite) as BlockTableRecord;
                    //send message to user
                    ed.WriteMessage("Trying to Write Line");
                    Point3d pt1 = new Point3d(0, 0, 0);
                    Point3d pt2 = new Point3d(1000, 1000, 0);
                    Line ln = new Line(pt1, pt2);
                    ln.ColorIndex = 1;// red color
                    btr.AppendEntity(ln);
                    trans.AddNewlyCreatedDBObject(ln, true);
                    trans.Commit();
                }
                catch (System.Exception ex)
                {

                    ed.WriteMessage("Error Encountered:" + ex.Message);
                    trans.Abort();
                }

            
            }
        }

    }
}
