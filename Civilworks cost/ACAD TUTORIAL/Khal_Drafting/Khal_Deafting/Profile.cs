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
    public class Profile
    {
        public List<double> geometric_x { get; set; }
        public List<double> geometric_y { get; set; }
        public List<double> drawing_x { get; set; }
        public List<double> drawing_y { get; set; }
        public double geometric_origin_x { get; set; }
        public double geometric_origin_y { get; set; }
        public double drawing_origin_x { get; set; }
        public double drawing_origin_y { get; set; }
        public double xscale_fator { get; set; }
        public double yscale_factor { get; set; }
        public Color profilecolor { get; set; }
        public double profileLength { get; set; }
        public LineWeight profileWeight;
        public List<string> profileLables;
        public List<Point2d> pofileLabelLocation;
        public double draftingWidth {get;set;}
        public Profile(List<double> xvalues,
            List<double> yvalues, double xog, double yog, double sx,
            double sy,double profileLength,double draftingWidth)
        {
            this.geometric_x = xvalues;
            this.geometric_y = yvalues;
            this.geometric_origin_x = xog;
            this.geometric_origin_y = yog;
            this.xscale_fator = sx;
            this.yscale_factor = sy;
            this.profileLength = profileLength;
            this.draftingWidth = draftingWidth;
            this.profileLables = new List<string>();
            this.pofileLabelLocation = new List<Point2d>();





        }
        public Profile(
            double xog, double yog, double sx,
           double sy,  double draftingWidth)
        {
           
            this.geometric_origin_x = xog;
            this.geometric_origin_y = yog;
            this.xscale_fator = sx;
            this.yscale_factor = sy;
           // this.profileLength = profileLength;
            this.draftingWidth = draftingWidth;
            this.profileLables = new List<string>();
            this.pofileLabelLocation = new List<Point2d>();





        }

        public void fixProfileColor(short colorIndex)
        {
            //this.profilecolor = Color.FromRgb(255, 0, 0);
            this.profilecolor = Color.FromColorIndex(ColorMethod.ByColor, colorIndex);
            //string mybook = Color.FromNames(1,);
            //this.profilecolor=Color.FromNames()
            //this.profilecolor=Color.FromDictionaryName()


        }
        public void setLineWeight(int index)
        {

            List<LineWeight> mylineweight_list = new List<LineWeight>();
            mylineweight_list.Add(LineWeight.LineWeight000);
            mylineweight_list.Add(LineWeight.LineWeight005);
            mylineweight_list.Add(LineWeight.LineWeight009);
            mylineweight_list.Add(LineWeight.LineWeight013);
            mylineweight_list.Add(LineWeight.LineWeight015);
            mylineweight_list.Add(LineWeight.LineWeight018);
            mylineweight_list.Add(LineWeight.LineWeight020);
            mylineweight_list.Add(LineWeight.LineWeight025);
            mylineweight_list.Add(LineWeight.LineWeight030);
            mylineweight_list.Add(LineWeight.LineWeight035);
            mylineweight_list.Add(LineWeight.LineWeight040);
            mylineweight_list.Add(LineWeight.LineWeight040);
            mylineweight_list.Add(LineWeight.LineWeight050);
            mylineweight_list.Add(LineWeight.LineWeight053);
            mylineweight_list.Add(LineWeight.LineWeight060);
            mylineweight_list.Add(LineWeight.LineWeight070);
            mylineweight_list.Add(LineWeight.LineWeight080);
            mylineweight_list.Add(LineWeight.LineWeight090);
            mylineweight_list.Add(LineWeight.LineWeight100);
            mylineweight_list.Add(LineWeight.LineWeight106);
            mylineweight_list.Add(LineWeight.LineWeight120);
            mylineweight_list.Add(LineWeight.LineWeight140);
            mylineweight_list.Add(LineWeight.LineWeight158);
            mylineweight_list.Add(LineWeight.LineWeight200);
            mylineweight_list.Add(LineWeight.LineWeight211);
            this.profileWeight = mylineweight_list[index];




        }
        public List<double> getBoundingBox()
        {
            List<double> mybox = new List<double>();
            mybox.Add(this.geometric_x.Min());
            mybox.Add(this.geometric_y.Min());
            mybox.Add(this.geometric_x.Max());
            mybox.Add(this.geometric_y.Max());
            return mybox;
        
        }
        public void setLabelParameter(string[] labels,double [] labelLocations)
        {
       
            for (int i = 0; i <= labels.Length-1; i++)

            {
                profileLables.Add(labels[i]);
                if (labelLocations[i] == 0)
                    { 
                    pofileLabelLocation.Add(new Point2d(this.geometric_x[0], this.geometric_y[0]));
                
                
                }
                else
                {
                    List<double> mybox = this.getBoundingBox();
                    double x = mybox[0]+(this.profileLength / labelLocations[i]);
                    int index1 = this.geometric_x.BinarySearch(x);
                    int found = (~index1) - 1;
                    double xvalue = this.geometric_x[found];

                    pofileLabelLocation.Add(new Point2d(this.geometric_x[found],this.geometric_y[found]));
                
                }
                

            }


        }

        public MText drawProfileLabel(string label,double xloc,double yloc)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            MText txtLabel = new MText();
            List<double> mybox = this.getBoundingBox();
            txtLabel.Contents = label;
            try {

              
               
                double w = this.draftingWidth;
                txtLabel.TextHeight = w * 0.008;
                double xCoord = xloc;
                double yCoord = yloc;
                Point3d insPt = new Point3d(xCoord, yCoord, 0);
                txtLabel.Location = insPt;
                txtLabel.Attachment = AttachmentPoint.TopLeft;
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
        public Leader drawLeader(Point2d p1,Point2d p2)
        {

            double w = this.draftingWidth;
            Leader myleader = new Leader();
            myleader.SetDatabaseDefaults();
           
            myleader.AppendVertex(new Point3d(p1.X, p1.Y, 0));
            myleader.AppendVertex(new Point3d(p2.X, p2.Y, 0));
            myleader.AppendVertex(new Point3d(p2.X, p1.Y+100, 0));
            myleader.HasArrowHead = true;
            myleader.Dimasz = myleader.Dimasz * (w/50); // leader size
            return myleader;
        
        
        }

        public double getYFromX(double x)
        {


            int index = this.geometric_x.BinarySearch(x);
            int found = (~index) - 1;
            double yvalue = this.geometric_y[found];
            return yvalue;
        
        }
        
        
        public void drawProfile(double xod,double yod)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
              using (Transaction trans= doc.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                Polyline pl = new Polyline();
                Int32 no_of_points = this.geometric_x.Count;
                Mytransformation trs = new Mytransformation();
                for (int i = 0; i < no_of_points; i++)

                {
                    Point2d mypoint = new Point2d(this.geometric_x[i],this.geometric_y[i]);
                    Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
                    Point2d scaledPoint = trs.Scale(tog, this.xscale_fator, this.yscale_factor);
                    Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);
                    pl.AddVertexAt(i, draftingPoint, 0, 0, 0);


                }
                pl.SetDatabaseDefaults();
                pl.Color = this.profilecolor;
                pl.LineWeight = LineWeight.LineWeight040;
                btr.AppendEntity(pl);
                trans.AddNewlyCreatedDBObject(pl, true);
                List<double> mybox = this.getBoundingBox();
                MText mylabel;
                Leader myleader;
              int totalLabels = this.profileLables.Count;
                if (totalLabels != 0)
                {
                    for (int i = 0; i < totalLabels; i++)
                    {

                        try {
                            Point2d mypoint = new Point2d(this.pofileLabelLocation[i].X, mybox[3]+1.1);
                            Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
                            Point2d scaledPoint = trs.Scale(tog, this.xscale_fator, this.yscale_factor);
                            Point2d p1 = trs.Translate(scaledPoint, xod, yod);
                            // myxvalue=
                            Point2d mypoint2= this.pofileLabelLocation[i];
                            /*
                            if (this.pofileLabelLocation[i] == 0)
                            {
                                 mypoint2 = new Point2d(mybox[0],
                                      this.geometric_y[0]);
                            }
                            else
                            {
                               mypoint2 = new Point2d(this.pofileLabelLocation[i],
                                   this.getYFromX(this.pofileLabelLocation[i]));

                            }
                            */

                            Point2d tog2 = trs.Translate(mypoint2, -this.geometric_origin_x, -this.geometric_origin_y);
                            Point2d scaledPoint2 = trs.Scale(tog2, this.xscale_fator, this.yscale_factor);
                            Point2d p2 = trs.Translate(scaledPoint2, xod, yod);
                            mylabel = drawProfileLabel(this.profileLables[i], p1.X,p1.Y);
                            edt.WriteMessage("\n" + this.profileLables[i].ToString().ToUpper());


                            btr.AppendEntity(mylabel);
                            trans.AddNewlyCreatedDBObject(mylabel, true);
                            myleader = this.drawLeader(p2, p1);
                            edt.WriteMessage("\n Sucessfully Returned From Draw Leader Function " );
                            btr.AppendEntity(myleader);
                           trans.AddNewlyCreatedDBObject(myleader, true);
                           myleader.Annotation = mylabel.ObjectId;
                          myleader.EvaluateLeader();

                        }
                        
                        catch (System.Exception ex)
                        {
                            edt.WriteMessage("\n" + ex.Message);
                            edt.WriteMessage("\n" + ex.Source);
                            edt.WriteMessage("\n" + ex.HResult);

                        }

                    }





                }
             



                trans.Commit();
            }
        
        
        }
        public void drawProfile_Without_Color(double xod, double yod)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
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
                pl.Color = this.profilecolor;
                pl.LineWeight = LineWeight.LineWeight040;
                btr.AppendEntity(pl);
                trans.AddNewlyCreatedDBObject(pl, true);
                List<double> mybox = this.getBoundingBox();
                MText mylabel;
                Leader myleader;
                int totalLabels = this.profileLables.Count;
                if (totalLabels != 0)
                {
                    for (int i = 0; i < totalLabels; i++)
                    {

                        try
                        {
                            Point2d mypoint = new Point2d(this.pofileLabelLocation[i].X, mybox[3] + 1.1);
                            Point2d tog = trs.Translate(mypoint, -this.geometric_origin_x, -this.geometric_origin_y);
                            Point2d scaledPoint = trs.Scale(tog, this.xscale_fator, this.yscale_factor);
                            Point2d p1 = trs.Translate(scaledPoint, xod, yod);
                            // myxvalue=
                            Point2d mypoint2 = this.pofileLabelLocation[i];
                            /*
                            if (this.pofileLabelLocation[i] == 0)
                            {
                                 mypoint2 = new Point2d(mybox[0],
                                      this.geometric_y[0]);
                            }
                            else
                            {
                               mypoint2 = new Point2d(this.pofileLabelLocation[i],
                                   this.getYFromX(this.pofileLabelLocation[i]));

                            }
                            */

                            Point2d tog2 = trs.Translate(mypoint2, -this.geometric_origin_x, -this.geometric_origin_y);
                            Point2d scaledPoint2 = trs.Scale(tog2, this.xscale_fator, this.yscale_factor);
                            Point2d p2 = trs.Translate(scaledPoint2, xod, yod);
                            mylabel = drawProfileLabel(this.profileLables[i], p1.X, p1.Y);
                            edt.WriteMessage("\n" + this.profileLables[i].ToString().ToUpper());


                            btr.AppendEntity(mylabel);
                            trans.AddNewlyCreatedDBObject(mylabel, true);
                            myleader = this.drawLeader(p2, p1);
                            edt.WriteMessage("\n Sucessfully Returned From Draw Leader Function ");
                            btr.AppendEntity(myleader);
                            trans.AddNewlyCreatedDBObject(myleader, true);
                            myleader.Annotation = mylabel.ObjectId;
                            myleader.EvaluateLeader();

                        }

                        catch (System.Exception ex)
                        {
                            edt.WriteMessage("\n" + ex.Message);
                            edt.WriteMessage("\n" + ex.Source);
                            edt.WriteMessage("\n" + ex.HResult);

                        }

                    }





                }




                trans.Commit();
            }


        }
    }
}
