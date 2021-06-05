using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Autocad Related Imports*/
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;

namespace Khal_Deafting
{
    class ProfileTable
    {
        public List<double> distance { get; set; }
        public List<double> existing_bed_level { get; set; }
        public List<double> existing_lb_level { get; set; }
        public List<double> existing_rb_level { get; set; }
        public List<double> design_bed_level { get; set; }
        public List<double> design_bed_width { get; set; }

        public double draftingWidth { get; set; }

        public ProfileTable(List<double> xvalues, List<double> lbank, List<double> rbank, List<double> existing_bed,

             List<double> design_bed,double draftingWidth)
        {
            this.distance = xvalues;
            this.existing_bed_level = existing_bed;
            this.existing_lb_level = lbank;
            this.existing_rb_level = rbank;
            this.design_bed_level = design_bed;
            this.draftingWidth = draftingWidth;
        
        
        }

        public string[,] getTableData()
            {
            int n = this.distance.Count;

            string[,] str = new string[6, n+1];
            //setting the table labels
            str[0, 0] = "Design Bed Width";
            str[1, 0] = "Design Bed Level";
            str[2, 0] = "Existing  Bed Level";
            str[3, 0] = "Existing  Right Bank";
            str[4, 0] = "Existing  Left Bank";
            str[5, 0] = "Distance";
            for (int i = 0; i < n; i++)
            {
                str[0, i+1] = "10";
                str[1, i+1] = Math.Round(this.design_bed_level[i],3).ToString();
                str[2, i+1] = Math.Round(this.existing_bed_level[i], 3).ToString();
                str[3, i+1] = Math.Round(this.existing_rb_level[i], 3).ToString();
                str[4, i+1] = Math.Round(this.existing_lb_level[i], 3).ToString();
                str[5, i+1] = Math.Round(this.distance[i], 3).ToString();

            }
            return str;

        }



        public void drawTable(double xod, double yod)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                try {
                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    Table tb = new Table();
                    //tb.TableStyle = db.Tablestyle;
                    // Use a nested loop to add and format each cell
                    double w = this.draftingWidth;
                    string[,] str = this.getTableData();
                    edt.WriteMessage("\n Sucessuflly returned from data creation....");
                    int n = this.distance.Count;
                    tb.SetSize(6, n +1);
                    tb.Position = new Point3d(xod, yod, 0);
                    tb.Columns[0].Width = w * 0.18;
                    for (int j = 1; j < n; j++)
                    {
                        tb.Columns[j].Width = (w * 0.68/n);

                    }
                        tb.Rows[0].Height = w * 0.03;
                    tb.Rows[1].Height = w * 0.03;
                    tb.Rows[2].Height = w * 0.03;
                    tb.Rows[3].Height = w * 0.03;
                    tb.Rows[4].Height = w * 0.03;
                    tb.Rows[5].Height = w * 0.03;
                   
                    for (int j = 0; j < n; j++)

                        {

                        for (int i = 0; i < 6; i++)

                        {   
                            
                            edt.WriteMessage("\n Sucessuflly set text height.......");
                          
                            if (j==0)
                            {
                                tb.Cells[i, j].TextHeight = w * 0.008;
                                tb.Cells[i, j].TextString = str[i, j];                                
                                tb.Cells[i, j].Alignment = CellAlignment.MiddleCenter;
                                tb.Cells[i, j].Contents[0].Rotation =0;

                            }
                            
                            else
                            {
                                tb.Cells[i, j].TextHeight = w * 0.006;
                                tb.Cells[i, j].TextString = str[i, j];
                                tb.Cells[i, j].Alignment = CellAlignment.MiddleCenter;
                                tb.Cells[i, j].Contents[0].Rotation = Math.PI / 2;
                            }


                        }

                    }
                    //tb.GenerateLayout();
                    edt.WriteMessage("\n Sucessuflly set cell properties.......");
                    btr.AppendEntity(tb);
                    trans.AddNewlyCreatedDBObject(tb, true);

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n" + ex.Message);
                    edt.WriteMessage("\n" + ex.Source);
                    edt.WriteMessage("\n" + ex.HResult);

                }

                trans.Commit();
            }

           

        }
        public void drawTableWithLayer(double xod, double yod,string sLayerName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    LayerTable acLyrTbl;
                    acLyrTbl = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    if (acLyrTbl.Has(sLayerName))
                    {
                        db.Clayer = acLyrTbl[sLayerName];


                    }




                    Table tb = new Table();
                    //tb.TableStyle = db.Tablestyle;
                    // Use a nested loop to add and format each cell
                    double w = this.draftingWidth;
                    string[,] str = this.getTableData();
                    edt.WriteMessage("\n Sucessuflly returned from data creation....");
                    int n = this.distance.Count;
                    tb.SetSize(6, n + 1);
                    tb.Position = new Point3d(xod, yod, 0);
                    tb.Columns[0].Width = w * 0.18;
                    for (int j = 1; j < n; j++)
                    {
                        tb.Columns[j].Width = (w * 0.68 / n);

                    }
                    tb.Rows[0].Height = w * 0.03;
                    tb.Rows[1].Height = w * 0.03;
                    tb.Rows[2].Height = w * 0.03;
                    tb.Rows[3].Height = w * 0.03;
                    tb.Rows[4].Height = w * 0.03;
                    tb.Rows[5].Height = w * 0.03;

                    for (int j = 0; j < n; j++)

                    {

                        for (int i = 0; i < 6; i++)

                        {

                            edt.WriteMessage("\n Sucessuflly set text height.......");

                            if (j == 0)
                            {
                                tb.Cells[i, j].TextHeight = w * 0.008;
                                tb.Cells[i, j].TextString = str[i, j];
                                tb.Cells[i, j].Alignment = CellAlignment.MiddleCenter;
                                tb.Cells[i, j].Contents[0].Rotation = 0;

                            }

                            else
                            {
                                tb.Cells[i, j].TextHeight = w * 0.006;
                                tb.Cells[i, j].TextString = str[i, j];
                                tb.Cells[i, j].Alignment = CellAlignment.MiddleCenter;
                                tb.Cells[i, j].Contents[0].Rotation = Math.PI / 2;
                            }


                        }

                    }
                    //tb.GenerateLayout();
                    edt.WriteMessage("\n Sucessuflly set cell properties.......");
                    btr.AppendEntity(tb);
                    trans.AddNewlyCreatedDBObject(tb, true);

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n" + ex.Message);
                    edt.WriteMessage("\n" + ex.Source);
                    edt.WriteMessage("\n" + ex.HResult);

                }

                trans.Commit();
            }



        }

    }
}
