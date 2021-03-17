using System;
using System.IO; //Fileinfo
using OfficeOpenXml; // Epplus 
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
                    string path = "E:/Website_24_11_2020/cmis6/cmis6/Civilworks cost/Survey Data Processing/Khal_Input.xlsx";
                    FileInfo fileInfo = new FileInfo(path);

                    ExcelPackage package = new ExcelPackage(fileInfo);
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["LS"];
                    int rows = worksheet.Dimension.Rows;
                    int columns = worksheet.Dimension.Columns;
                    for (int i = 1; i <= rows; i++)
                    {
                        for (int j = 1; j <= columns; j++)
                        {

                            string content = worksheet.Cells[i, j].Value.ToString();
                            ed.WriteMessage( content+"\n");
                        }
                    }
                    
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
