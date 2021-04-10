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
    class Vscale2
    {
        public double max_y { get; set; }
        public double min_y { get; set; }
        public double xscale_fator { get; set; }
        public double yscale_factor { get; set; }
        public Point2d zero_value_location { get; set; }
        public List<double> major_tic;
        public List<double> minor_tic;
        public double geometric_origin_x { get; set; }
        public double geometric_origin_y { get; set; }
        public double vscale_xlocation;
        


        public Vscale2(double ymax,double ymin,Point2d dorigin,double yscale,double xscale,double xog,double yog)
        {
            this.max_y = ymax;
            this.min_y = ymin;
            this.geometric_origin_x = xog;
            this.geometric_origin_y = yog;
            this.vscale_xlocation = xog * 0.98;
            this.xscale_fator = xscale;
            this.yscale_factor = yscale;
            //this.geometric_xlocation = xloc_vscale;
            //this.zero_value_location = dorigin;

            this.major_tic = new List<double>();
            this.minor_tic = new List<double>();
            int y1 =(int) Math.Floor(ymin);
            int y2 = (int)Math.Ceiling(ymax);
           // this.y_scacle_factor = yscale;
            this.CalculateMajorTics(y2, y1);
        }
        private void CalculateMajorTics(int upper_scale, int lower_scale)
         {

            for (int i = lower_scale; i < upper_scale; i++)
            {
                this.major_tic.Add(i);
            
            
            
            
            }
        
        
        }
        private void getYcoordanteFromTic()
        { 
        
        
        
        }
        public void drawVscale(double xod, double yod)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {

                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                try{
                    double x0 = this.zero_value_location.X;
                    double y0 = this.zero_value_location.Y;
                    //drawing the main sacle polline
                    Polyline pl = new Polyline();
                    Int32 no_of_points = this.major_tic.Count;
                    Mytransformation trs = new Mytransformation();
                    for (int i = 0; i < no_of_points; i++)
                    {
                        Point2d mypoint = new Point2d(this.vscale_xlocation, this.major_tic[i]);
                        Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
                        Point2d scaledPoint = trs.Scale(tog, this.xscale_fator, this.yscale_factor);
                        Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);
                        pl.AddVertexAt(i, draftingPoint, 0, 0, 0);

                    }
                       
                    
                   // pl.AddVertexAt(0, mypoint, 0, 0, 0);
                    //Point2d mypoint = new Point2d(0,this.);
                    // Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
                    // Point2d scaledPoint = trs.Scale(tog, this.xscale_fator, this.yscale_factor);
                    //Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);
                    // pl.AddVertexAt(i, draftingPoint, 0, 0, 0);
                    pl.SetDatabaseDefaults();
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
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
