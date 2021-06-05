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
        public List<bool> label_without_arrow;
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
        public void setLabelParameteWithXvalues(string[] labels, double[] labelLocations_x,
            double[] labelLocations_y)
        {

            for (int i = 0; i <= labels.Length - 1; i++)

            {
                profileLables.Add(labels[i]);
                if (this.geometric_x.Contains(labelLocations_x[i]) == true)
                {
                   int idx = this.geometric_x.IndexOf(labelLocations_x[i]);
                    // Point2d p = new Point2d(this.geometric_x[idx], this.geometric_y[idx]);
                    Point2d p = new Point2d(labelLocations_x[i], labelLocations_y[i]);
                   this.pofileLabelLocation.Add(p);

                }
                else 
                {
                    int found = this.geometric_x.BinarySearch(labelLocations_x[i]);
                    int idx = (~found) - 1;
                    // Point2d p = new Point2d(this.geometric_x[idx], this.geometric_y[idx]);
                    Point2d p = new Point2d(labelLocations_x[i], labelLocations_y[i]);
                   this. pofileLabelLocation.Add(p);
                }

                //List<double> mybox = this.getBoundingBox();
                // double x = mybox[0] + (this.profileLength / labelLocations_x[i]);
                //
                // int found = (~index1) - 1;
                //double xvalue = this.geometric_x[found];

                //  pofileLabelLocation.Add(new Point2d(this.geometric_x[found], this.geometric_y[found]));
               
               // pofileLabelLocation.Add(p);



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
        public virtual void drawProfileWithLayer(double xod, double yod,string sLayerName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                /*Changing Layers*/
                LayerTable acLyrTbl;
                acLyrTbl = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (acLyrTbl.Has(sLayerName))
                {
                    db.Clayer = acLyrTbl[sLayerName];


                }
                
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
                //pl.LineWeight = LineWeight.LineWeight040;
                btr.AppendEntity(pl);
                trans.AddNewlyCreatedDBObject(pl, true);
                List<double> mybox = this.getBoundingBox();
                MText mylabel;
                Leader myleader;
                int totalLabels = this.profileLables.Count;
                sLayerName = "Long Section Labels";
                if (acLyrTbl.Has(sLayerName))
                {
                    db.Clayer = acLyrTbl[sLayerName];


                }
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
        public void drawProfile_WithLineType(double xod, double yod, string sLineTypeName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                //Load Linetype at first

                LinetypeTable acLineTypeTable;
                acLineTypeTable = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                if (acLineTypeTable.Has(sLineTypeName) == false)
                {
                    db.LoadLineTypeFile(sLineTypeName, "acad.lin");
                
                }

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
                pl.Linetype = sLineTypeName;
                //pl.LinetypeScale = 100;
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
                            Point2d mypoint = new Point2d(this.pofileLabelLocation[i].X, this.pofileLabelLocation[i].Y);
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
                          // myleader = this.drawLeader(p2, p1);
                           //edt.WriteMessage("\n Sucessfully Returned From Draw Leader Function ");
                           //btr.AppendEntity(myleader);
                           //trans.AddNewlyCreatedDBObject(myleader, true);
                          // myleader.Annotation = mylabel.ObjectId;
                           //myleader.EvaluateLeader();

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
        public virtual  void drawRLlabel(double xod, double yod, double ylable_loc)
        {

            string atext;
            Point2d gog = new Point2d(this.geometric_origin_x, this.geometric_origin_y);
            Point2d dog = new Point2d(xod, yod);
            double sx = this.xscale_fator;
            double sy = this.yscale_factor;
            double w = this.draftingWidth;
            Point2d aloc;
            double text_rotation = 2 * Math.Atan(1);
            foreach (double d in this.geometric_y)
            {
                atext = d.ToString("N3");
                int idx = this.geometric_y.IndexOf(d);
                double x = this.geometric_x[idx];
                aloc = new Point2d(x, ylable_loc);
                Label_Annotation mylabel = new Label_Annotation(atext, aloc, gog, dog, false, sx, sy, w);
                mylabel.displayAnnotaion();
                mylabel.drawAnnotation(text_rotation, 1, 0.007 * w);


            }




        }
        public virtual void drawRLlabelForXsection(double xod, double yod, double ylable_loc)
        {

            string atext;
            Point2d gog = new Point2d(this.geometric_origin_x, this.geometric_origin_y);
            Point2d dog = new Point2d(xod, yod);
            double sx = this.xscale_fator;
            double sy = this.yscale_factor;
            double w = this.draftingWidth;
            Point2d aloc;
            double text_rotation = 2 * Math.Atan(1);
            foreach (double d in this.geometric_y)
            {
                atext = d.ToString("N3");
                int idx = this.geometric_y.IndexOf(d);
                double x = this.geometric_x[idx];
                aloc = new Point2d(x, ylable_loc);
                Label_Annotation mylabel = new Label_Annotation(atext, aloc, gog, dog, false, sx, sy, w);
                mylabel.displayAnnotaion();
                mylabel.drawAnnotation(text_rotation, 1, 0.005 * w);


            }




        }
        public virtual void drawBorderForDataLabel(double xod, double yod, double sheetWidth, List<double> horizontal_loc)
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
                    /*Changing Layers*/
                    string sLayerName = "Long Section Border";
                    LayerTable acLyrTbl;
                    acLyrTbl = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    if (acLyrTbl.Has(sLayerName))
                    {
                        db.Clayer = acLyrTbl[sLayerName];


                    }

                    /*drawing outer border*/
                    double w = sheetWidth;
                    Polyline pl = new Polyline();
                    pl.AddVertexAt(0, new Point2d(xod - 0.09 * w, yod), 0, 0, 0);
                    pl.AddVertexAt(1, new Point2d(xod + 0.89 * w, yod), 0, 0, 0);
                    pl.AddVertexAt(2, new Point2d(xod + 0.89 * w, yod - 0.175 * w), 0, 0, 0);
                    pl.AddVertexAt(3, new Point2d(xod - 0.09 * w, yod - 0.175 * w), 0, 0, 0);
                    pl.Closed = true;
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
                    /*Drawing Horizonal Separator*/
                    foreach (double ylabel in horizontal_loc)
                    {
                        pl = new Polyline();
                        double drafting_y = (ylabel - this.geometric_origin_y) * this.yscale_factor;
                        pl.AddVertexAt(0, new Point2d(xod - 0.09 * w, yod - ylabel), 0, 0, 0);
                        pl.AddVertexAt(1, new Point2d(xod + 0.89 * w, yod - ylabel), 0, 0, 0);
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);
                    }
                    /*Adding Vertical for Bed Width*/
                    // pl = new Polyline();                    
                    // pl.AddVertexAt(0, new Point2d(xod - 0.09 * w, yod - 0.0), 0, 0, 0);
                    // pl.AddVertexAt(1, new Point2d(xod + 0.89 * w, yod - ylabel), 0, 0, 0);
                    //  btr.AppendEntity(pl);
                    //  trans.AddNewlyCreatedDBObject(pl, true);

                    /*Drawing Vertical Line*/
                    pl = new Polyline();
                    pl.AddVertexAt(0, new Point2d(xod, yod), 0, 0, 0);
                    pl.AddVertexAt(1, new Point2d(xod, yod - 0.175 * w), 0, 0, 0);
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
                    /*Drawing Text for Data Label of Long Section */
                    Point3d insPt;
                    MText txtLabel;
                    //heading for bedwidth
                    txtLabel = new MText();
                    txtLabel.Contents = "Design Bed Width(m)";
                    txtLabel.TextHeight = w * 0.006;
                    insPt = new Point3d(xod - 0.045 * w, yod - 0.0125 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);
                    //heading for bed level
                    txtLabel = new MText();
                    txtLabel.Contents = "Design Bed Level(m)";
                    txtLabel.TextHeight = w * 0.006;
                    insPt = new Point3d(xod - 0.045 * w, yod - 0.0375 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);
                    //heading for Existing Bed Level
                    txtLabel = new MText();
                    txtLabel.Contents = "Existing Bed Level";
                    txtLabel.TextHeight = w * 0.006;
                    insPt = new Point3d(xod - 0.045 * w, yod - 0.065 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);
                    //heading for Existing Right Bank
                    txtLabel = new MText();
                    txtLabel.Contents = "Existing Left Bank";
                    txtLabel.TextHeight = w * 0.006;
                    insPt = new Point3d(xod - 0.045 * w, yod - 0.095 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);
                    //heading for Existing Left Bank
                    txtLabel = new MText();
                    txtLabel.Contents = "Existing Right Bank";
                    txtLabel.TextHeight = w * 0.006;
                    insPt = new Point3d(xod - 0.045 * w, yod - 0.125 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);
                    //heading for Distance
                    txtLabel = new MText();
                    txtLabel.Contents = "Distance(Km)";
                    txtLabel.TextHeight = w * 0.006;
                    insPt = new Point3d(xod - 0.045 * w, yod - 0.1575 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);
                    //commiting transaction
                    trans.Commit();

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n" + ex.Message);
                    edt.WriteMessage("\n" + ex.Source);
                    edt.WriteMessage("\n" + ex.HResult);

                }




            }




        }
        public virtual void drawDistanceLabel(double xod, double yod, double ylable_loc)
        {

            string atext;
            Point2d gog = new Point2d(this.geometric_origin_x, this.geometric_origin_y);
            Point2d dog = new Point2d(xod, yod);
            double sx = this.xscale_fator;
            double sy = this.yscale_factor;
            double w = this.draftingWidth;
            Point2d aloc;
            double text_rotation = 2 * Math.Atan(1);
            foreach (double d in this.geometric_x)
            {
                atext = Math.Round(d / 1000, 3, MidpointRounding.ToEven).ToString("N3");
                aloc = new Point2d(d, ylable_loc);
                Label_Annotation mylabel = new Label_Annotation(atext, aloc, gog, dog, false, sx, sy, w);
                mylabel.displayAnnotaion();
                // mylabel.drawAnnotation(text_rotation, 1);
                mylabel.drawAnnotation(text_rotation, 1, 0.007 * w);

            }




        }
        public virtual void drawDistanceLabelForXsection(double xod, double yod, double ylable_loc)
        {

            string atext;
            Point2d gog = new Point2d(this.geometric_origin_x, this.geometric_origin_y);
            Point2d dog = new Point2d(xod, yod);
            double sx = this.xscale_fator;
            double sy = this.yscale_factor;
            double w = this.draftingWidth;
            Point2d aloc;
            double text_rotation = 2 * Math.Atan(1);
            foreach (double d in this.geometric_x)
            {
                atext = Math.Round(d, 3, MidpointRounding.ToEven).ToString("N3");
                aloc = new Point2d(d, ylable_loc);
                Label_Annotation mylabel = new Label_Annotation(atext, aloc, gog, dog, false, sx, sy, w);
                mylabel.displayAnnotaion();
                // mylabel.drawAnnotation(text_rotation, 1);
                mylabel.drawAnnotation(text_rotation, 1, 0.005 * w);

            }




        }
        public List<Point2d> getBorderPoints(string drawing_location,DrawingSheet ds)
        {
         List<Point2d> border_points = new List<Point2d>();
            Point2d top_left = ds.TopLeft;
            Point2d p1, p2, p3, p4;
            double w = ds.width;
            double L = this.profileLength*this.xscale_fator;
            switch (drawing_location)
            {               
                case "A":
                    p1 = new Point2d(top_left.X+0.01*w,top_left.Y-0.25*w);
                    p2 = new Point2d(top_left.X + 0.45 * w , top_left.Y - 0.25 * w);
                    p3 = new Point2d(top_left.X + 0.45 * w , top_left.Y - 0.35 * w);
                    p4 = new Point2d(top_left.X + 0.1, top_left.Y - 0.35 * w);
                    break;

                case "B":
                    p1 = new Point2d(top_left.X + 0.51 * w, top_left.Y - 0.25 * w);
                    p2 = new Point2d(top_left.X + 51 * w + L, top_left.Y - 0.25 * w);
                    p3 = new Point2d(top_left.X + 0.51 * w + L, top_left.Y - 0.35 * w);
                    p4 = new Point2d(top_left.X + 0.1, top_left.Y - 0.35 * w);
                    break;
                case "C":
                    p1 = new Point2d(top_left.X + 0.01 * w, top_left.Y - 0.75 * w);
                    p2 = new Point2d(top_left.X + 0.1 * w + L, top_left.Y - 0.75 * w);
                    p3 = new Point2d(top_left.X + 0.1 * w + L, top_left.Y - 0.85 * w);
                    p4 = new Point2d(top_left.X + 0.1, top_left.Y - 0.85 * w);
                    break;
                default:
                    p1 = new Point2d(top_left.X + 0.01 * w, top_left.Y - 0.25 * w);
                    p2 = new Point2d(top_left.X + 0.1 * w + L, top_left.Y - 0.25 * w);
                    p3 = new Point2d(top_left.X + 0.1 * w + L, top_left.Y - 0.35 * w);
                    p4 = new Point2d(top_left.X + 0.1, top_left.Y - 0.35 * w);
                    break;

            }
            border_points.Add(p1);
            border_points.Add(p2);
            border_points.Add(p3);
            border_points.Add(p4);
            return border_points;
        }
        public virtual void drawBorderForDataLabelForXsection(double xod, double yod, double sheetWidth, List<double> horizontal_loc,string drawing_location,DrawingSheet ds)
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
                    /*Changing Layers*/
                    string sLayerName = "Cross section Data Border";
                    LayerTable acLyrTbl;
                    acLyrTbl = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    if (acLyrTbl.Has(sLayerName))
                    {
                        db.Clayer = acLyrTbl[sLayerName];


                    }

                    /*drawing outer border*/
                    double w = this.draftingWidth;
                    Mytransformation trs = new Mytransformation();
                    //drawing exterior border
                    double yog = this.geometric_origin_y;
                    double xog = this.geometric_origin_x;
                    double sx = this.xscale_fator;
                    double sy = this.yscale_factor;
                    Polyline pl = new Polyline();
                    Point2d gog = new Point2d(xog, yog);
                    Point2d dog = new Point2d(xod, yod);
                    Point2d pivot_g = new Point2d(xog, yog-0.14*w/sy);
                    Point2d pivot_d = trs.Geometric2DraftingCoordinate(pivot_g, gog,dog, sx, sy);
                    double L = this.profileLength * this.xscale_fator;
                    pl.AddVertexAt(0, new Point2d(pivot_d.X-0.09*w,pivot_d.Y), 0, 0, 0);
                    pl.AddVertexAt(1, new Point2d(pivot_d.X+L+0.01*w, pivot_d.Y), 0, 0, 0);
                    pl.AddVertexAt(2, new Point2d(pivot_d.X + L + 0.01 * w, pivot_d.Y-0.11*w), 0, 0, 0);
                    pl.AddVertexAt(3, new Point2d(pivot_d.X - 0.09 * w, pivot_d.Y - 0.11 * w), 0, 0, 0);
                    pl.Closed = true;
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
                    //drawing vertical for heading separator
                    pl = new Polyline();
                    pl.AddVertexAt(0, new Point2d(pivot_d.X, pivot_d.Y), 0, 0, 0);
                    pl.AddVertexAt(1, new Point2d(pivot_d.X, pivot_d.Y-0.11*w), 0, 0, 0);
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
                    //drawing first horizontal line for design RL
                    pl = new Polyline();
                    pl.AddVertexAt(0, new Point2d(pivot_d.X - 0.09 * w, pivot_d.Y-0.03*w), 0, 0, 0);
                    pl.AddVertexAt(1, new Point2d(pivot_d.X + L + 0.01 * w, pivot_d.Y - 0.03* w), 0, 0, 0);
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
                    //drawing first horizontal line for Existing RL
                    pl = new Polyline();
                    pl.AddVertexAt(0, new Point2d(pivot_d.X - 0.09 * w, pivot_d.Y - 0.07 * w), 0, 0, 0);
                    pl.AddVertexAt(1, new Point2d(pivot_d.X + L + 0.01 * w, pivot_d.Y - 0.07 * w), 0, 0, 0);
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
                    
                    //Drawing Text for Data Label of Long Section 
                    Point3d insPt;
                    MText txtLabel;
                    //heading for Design RL
                    txtLabel = new MText();
                    txtLabel.Contents = "Design Bed Level in (m-Pwd)";
                    txtLabel.TextHeight = w * 0.006;
                    insPt = new Point3d(pivot_d.X - 0.045 * w, pivot_d.Y - 0.015 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    txtLabel.Width = 0.08 * w;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);
                    //heading for Existing Bed Level
                    txtLabel = new MText();
                    txtLabel.Contents = "Existing Bed Level in (m-Pwd)";
                    txtLabel.TextHeight = w * 0.006;
                    insPt = new Point3d(pivot_d.X - 0.045 * w, pivot_d.Y - 0.05 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    txtLabel.Width = 0.08 * w;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);
                    //heading for Distance
                    txtLabel = new MText();
                    txtLabel.Contents = "Distance(m)";
                    txtLabel.TextHeight = w * 0.006;
                    txtLabel.Width=0.08*w;
                    insPt = new Point3d(pivot_d.X - 0.045 * w, pivot_d.Y - 0.09 * w, 0);
                    txtLabel.Location = insPt;
                    txtLabel.Attachment = AttachmentPoint.MiddleCenter;
                    btr.AppendEntity(txtLabel);
                    trans.AddNewlyCreatedDBObject(txtLabel, true);


                    trans.Commit();

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n" + ex.Message);
                    edt.WriteMessage("\n" + ex.Source);
                    edt.WriteMessage("\n" + ex.HResult);

                }




            }




        }
    }
}

