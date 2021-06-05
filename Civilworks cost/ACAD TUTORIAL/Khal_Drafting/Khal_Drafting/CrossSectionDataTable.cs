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
using System.Drawing;//imports for font size

namespace Khal_Deafting
{
    public class CrossSectionDataTable
    {
        public List<double> distance { get; set; }
        public List<double> existing_bed_level { get; set; }      
       
        public List<double> design_bed_level { get; set; }
        public double design_bedwidth { get; set; }

        public double draftingWidth { get; set; }

        public CrossSectionDataTable(List<double> xvalues, List<double> existing_bed,

             List<double> design_bed, double draftingWidth)
        {
            this.distance = xvalues;
            this.existing_bed_level = existing_bed;           
            this.design_bed_level = design_bed;
            this.draftingWidth = draftingWidth;


        }

        public string[,] getTableData()
        {
            int n = this.distance.Count;

            string[,] str = new string[3, n + 1];
            //setting the table labels
            
            str[0, 0] = "Design Bed Level in m-PWD";
            str[1, 0] = "Existing  Bed Level in m-PWD";            
            str[2, 0] = "Distance";
            for (int i = 0; i < n; i++)
            {
                if (this.design_bed_level[i] == -9999)
                {
                    str[0, i + 1] = "***";
                }
                else
                {

                    str[0, i + 1] = Math.Round(this.design_bed_level[i], 3).ToString(); ;
                }
                

                str[1, i + 1] = Math.Round(this.existing_bed_level[i], 3).ToString();
                str[2, i + 1] = Math.Round(this.distance[i], 3).ToString();
                
               

            }
            return str;

        }

        public int getTextWidthFromFont(int FontHeight)
        {
            int columnwidth;
            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    System.Drawing.Font usefont = new System.Drawing.Font("Arial", FontHeight);
                    SizeF txtwidth = new SizeF();
                    Single widesttxt = 0.0F;
                    
                    
                    List<string> checkwidths = new List<string>() {  "100.325" };
                    foreach (string s in checkwidths)
                    {
                        txtwidth = g.MeasureString(s, usefont);
                        Single twidth = txtwidth.Width;
                        if (twidth > widesttxt)
                            widesttxt = twidth;
                    }

                     columnwidth = (int)(Math.Ceiling(widesttxt / 10.0d) * 10) + 10;
                    

                }
                

            }
            return columnwidth;
        }

        public void drawTable(double xod, double yod)
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

                    Table tb = new Table();

                    //tb.TableStyle = db.Tablestyle;
                    // Use a nested loop to add and format each cell
                   
                    double w = this.draftingWidth;
                    double twidth = 0.4 * w;
                    string[,] str = this.getTableData();
                    edt.WriteMessage("\n Sucessuflly returned from data creation....");
                    int n = this.distance.Count;
                    tb.SetSize(3, n+1);
                    tb.Position = new Point3d(xod, yod, 0);
                    tb.Columns[0].Width = 0.3*twidth; //20% for row headings
                    for (int j = 1; j <= n; j++)
                    {
                        tb.Columns[j].Width = (twidth*0.7 / n);

                    }

                  //int row_height=this.getTextWidthFromFont((int)((int) 0.007*w));
                    //edt.WriteMessage("\n row height" + row_height.ToString());
                    // tb.Rows[0].Height = w * 0.03;
                    //  tb.Rows[1].Height = w * 0.03;
                    //  tb.Rows[2].Height = w * 0.03;
                    tb.Rows[0].Height = w * 0.04;
                    tb.Rows[1].Height = w * 0.04;
                    tb.Rows[2].Height = w * 0.04;

                    for (int j = 0; j < n; j++)

                    {

                        for (int i = 0; i <=2; i++)

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
                                tb.Cells[i, j].TextHeight = w * 0.008;
                                tb.Cells[i, j].TextString = str[i, j];
                                tb.Cells[i, j].Alignment = CellAlignment.MiddleCenter;
                               // tb.Cells[i, j].Contents[0].Rotation = 90;
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
                    edt.WriteMessage("\n" + ex.ToString());

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
                    /*Setting Layer for Data Table*/
                    LayerTable acLyrTbl;
                    acLyrTbl = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    if (acLyrTbl.Has(sLayerName))
                    {
                        db.Clayer = acLyrTbl[sLayerName];


                    }


                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    Table tb = new Table();

                    //tb.TableStyle = db.Tablestyle;
                    // Use a nested loop to add and format each cell

                    double w = this.draftingWidth;
                    double twidth = 0.4 * w;
                    string[,] str = this.getTableData();
                    edt.WriteMessage("\n Sucessuflly returned from data creation....");
                    int n = this.distance.Count;
                    tb.SetSize(3, n + 1);
                    tb.Position = new Point3d(xod, yod, 0);
                    tb.Columns[0].Width = 0.3 * twidth; //20% for row headings
                    for (int j = 1; j <= n; j++)
                    {
                        tb.Columns[j].Width = (twidth * 0.7 / n);

                    }

                    //int row_height=this.getTextWidthFromFont((int)((int) 0.007*w));
                    //edt.WriteMessage("\n row height" + row_height.ToString());
                    // tb.Rows[0].Height = w * 0.03;
                    //  tb.Rows[1].Height = w * 0.03;
                    //  tb.Rows[2].Height = w * 0.03;
                    tb.Rows[0].Height = w * 0.04;
                    tb.Rows[1].Height = w * 0.04;
                    tb.Rows[2].Height = w * 0.04;

                    for (int j = 0; j < n; j++)

                    {

                        for (int i = 0; i <= 2; i++)

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
                                tb.Cells[i, j].TextHeight = w * 0.008;
                                tb.Cells[i, j].TextString = str[i, j];
                                tb.Cells[i, j].Alignment = CellAlignment.MiddleCenter;
                                // tb.Cells[i, j].Contents[0].Rotation = 90;
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
                    edt.WriteMessage("\n" + ex.ToString());

                }

                trans.Commit();
            }



        }

    }
}
