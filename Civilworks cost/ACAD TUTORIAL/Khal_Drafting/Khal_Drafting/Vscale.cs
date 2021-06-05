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
   public class Vscale : Profile
    {
        public double vscale_xloc;
        public Vscale(double xog, double yog, double sx, double sy, double draftingWidth,
            double vscale_xloc, double ymax, double ymin) : base(xog, yog, sx, sy, draftingWidth)

        {
            /*
             this.geometric_origin_x = xog;
             this.geometric_origin_y = yog;
             this.xscale_fator = sx;
             this.yscale_factor = sy;
             this.vscale_xloc = vscale_xloc;
             this.geometric_x = new List<double>();
             this.geometric_y = new List<double>();
            */
            int y1 = (int)Math.Floor(ymin);
            int y2 = (int)Math.Ceiling(ymax);
            this.vscale_xloc = vscale_xloc;
            Tuple<List<double>, List<double>> myvalues = CalculateMajorTics(y2, y1);
            this.geometric_x = myvalues.Item2;
            this.geometric_y = myvalues.Item1;

        }

        private Tuple<List<double>, List<double>> CalculateMajorTics(int upper_scale, int lower_scale)
        {
            List<double> mymajor_tics = new List<double>();
            List<double> mytic_xvalue = new List<double>();
            for (int i = lower_scale; i <= upper_scale; i++)
            {
                mymajor_tics.Add(i);
                mytic_xvalue.Add(this.vscale_xloc);




            }

            return new Tuple<List<double>, List<double>>(mymajor_tics, mytic_xvalue);

        }
        private Polyline drawMajorTic(double xod,double yod,double tick_y)
        { double w = this.draftingWidth;
            Polyline pl = new Polyline();
            Mytransformation trs = new Mytransformation();
            Point2d mypoint = new Point2d(this.geometric_x[0],tick_y);
            Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
            Point2d scaledPoint = trs.Scale(tog, this.xscale_fator, this.yscale_factor);
            Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);
            pl.AddVertexAt(0, draftingPoint, 0, 0, 0);
             mypoint = new Point2d(this.geometric_x[0]-w*0.005, tick_y);
            double sx;
            if (this.xscale_fator > 1)
            { sx = 1; }
            else
            {
                sx = this.xscale_fator;
            }
            tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
            scaledPoint = trs.Scale(tog, sx, this.yscale_factor);
             draftingPoint = trs.Translate(scaledPoint, xod, yod);
            pl.AddVertexAt(1, draftingPoint, 0, 0, 0);
            pl.SetDatabaseDefaults();
            return pl;

        }
        private Polyline drawMinorTic(double xod, double yod, double tick_y)
        {
            double w = this.draftingWidth;
            Polyline pl = new Polyline();
            Mytransformation trs = new Mytransformation();
            Point2d mypoint = new Point2d(this.geometric_x[0], tick_y);
            Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
            Point2d scaledPoint = trs.Scale(tog, this.xscale_fator, this.yscale_factor);
            Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);
            pl.AddVertexAt(0, draftingPoint, 0, 0, 0);
            double sx;
            if (this.xscale_fator > 1)
            { sx = 1; }
            else
            {
                sx = this.xscale_fator;
            }
            mypoint = new Point2d(this.geometric_x[0] - w * 0.003, tick_y);
            tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
            scaledPoint = trs.Scale(tog, sx, this.yscale_factor);
            draftingPoint = trs.Translate(scaledPoint, xod, yod);
            pl.AddVertexAt(1, draftingPoint, 0, 0, 0);
            pl.SetDatabaseDefaults();
            return pl;

        }

        private MText drawMajorTicLabl(double xod, double yod, double tick_y)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            MText txtLabel = new MText();
            List<double> mybox = this.getBoundingBox();
            txtLabel.Contents = Math.Round(tick_y,3).ToString();
            txtLabel.Contents = txtLabel.Contents + ".00";
            try
            {



                double w = this.draftingWidth;
                txtLabel.TextHeight = w * 0.008;
                Mytransformation trs = new Mytransformation();
                double sx;
                if (this.xscale_fator > 1)
                { sx = 1; }
                else
                {
                    sx = this.xscale_fator;
                }
                Point2d mypoint = new Point2d(this.geometric_x[0] - w * 0.008, tick_y);
                Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
                Point2d scaledPoint = trs.Scale(tog, sx, this.yscale_factor);
                Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);


                double xCoord = draftingPoint.X;
                double yCoord = draftingPoint.Y;
                Point3d insPt = new Point3d(xCoord, yCoord, 0);
                txtLabel.Location = insPt;
                txtLabel.Attachment = AttachmentPoint.MiddleRight;
                return txtLabel;
            }

            catch (System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.Message);
                edt.WriteMessage("\n" + ex.Source);
                edt.WriteMessage("\n" + ex.ToString());
                return txtLabel;

            }



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
                try
                {
                    Polyline pl = new Polyline();
                    Int32 no_of_points = this.geometric_x.Count;
                    Mytransformation trs = new Mytransformation();
                    for (int i = 0; i < no_of_points; i++)

                    {
                        Point2d mypoint = new Point2d(this.geometric_x[i], this.geometric_y[i]);
                        Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
                        Point2d scaledPoint = trs.Scale(tog, this.xscale_fator, this.yscale_factor);
                        Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);
                        pl.AddVertexAt(i, draftingPoint, 0, 0, 0);


                    }
                    pl.SetDatabaseDefaults();
                    // pl.Color = this.profilecolor;
                    //  pl.LineWeight = LineWeight.LineWeight040;
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
                    MText label;

                    foreach (double y in this.geometric_y)
                    {
                        pl = this.drawMajorTic(xod, yod, y);
                        label = this.drawMajorTicLabl(xod, yod, y);
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);
                        btr.AppendEntity(label);
                        trans.AddNewlyCreatedDBObject(label, true);

                    }
                    double y1, y2, y3;
                    for (int i = 0; i < this.geometric_y.Count - 1; i++)
                    {
                        y1 = this.geometric_y[i] + 0.25;
                        pl = this.drawMinorTic(xod, yod, y1);
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);
                        y2 = this.geometric_y[i] + 0.5;
                        pl = this.drawMinorTic(xod, yod, y2);
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);
                        y3 = this.geometric_y[i] + 0.75;
                        pl = this.drawMinorTic(xod, yod, y3);
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);
                    }
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n" + ex.Message);
                    edt.WriteMessage("\n" + ex.Source);
                    edt.WriteMessage("\n" + ex.ToString());

                }

                /**/






                trans.Commit();
            }


        }


    }
}

