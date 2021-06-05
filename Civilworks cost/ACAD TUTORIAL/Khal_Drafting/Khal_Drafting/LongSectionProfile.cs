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
    public class LongSectionProfile:Profile

    {
      
        
        public LongSectionProfile(List<double> xvalues,
            List<double> yvalues, double xog, double yog, double sx,
            double sy, double profileLength, double draftingWidth)
            :base(xvalues,yvalues,xog, yog, sx, sy, profileLength, draftingWidth)
        {
           // double xmin=this.geometric_x.Min()
            
        }
        public  void drawProfileWithLayer(double xod, double yod, string sLayerName,double bed_level_location)
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
        public override void drawDistanceLabel(double xod,double yod,double ylable_loc)        
        {

            string atext;
           Point2d gog = new Point2d(this.geometric_origin_x, this.geometric_origin_y);
           Point2d dog = new Point2d(xod,yod);
           double sx = this.xscale_fator;
           double sy = this.yscale_factor;
           double w = this.draftingWidth;
           Point2d aloc;
           double text_rotation = 2 * Math.Atan(1);
           foreach (double d in this.geometric_x)
            {
                atext = Math.Round(d/1000,3,MidpointRounding.ToEven).ToString("N3");
                aloc = new Point2d(d, ylable_loc);
                 Label_Annotation mylabel = new Label_Annotation(atext, aloc, gog, dog, false, sx, sy, w);
                mylabel.displayAnnotaion();
               // mylabel.drawAnnotation(text_rotation, 1);
                mylabel.drawAnnotation(text_rotation, 1, 0.007*w) ;

            }
        
        
        
        
        }

        public override void drawRLlabel(double xod, double yod, double ylable_loc)
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
                mylabel.drawAnnotation(text_rotation, 1,0.007*w);


            }




        }

        public override void drawBorderForDataLabel(double xod,double yod,double sheetWidth,List<double> horizontal_loc  )
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                try {
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
                    pl.AddVertexAt(1, new Point2d(xod+ 0.89 * w, yod), 0, 0, 0);
                    pl.AddVertexAt(2, new Point2d(xod + 0.89 * w, yod-0.175*w), 0, 0, 0);
                    pl.AddVertexAt(3, new Point2d(xod - 0.09 * w, yod - 0.175 * w), 0, 0, 0);
                    pl.Closed = true;
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);
                    /*Drawing Horizonal Separator*/
                    foreach (double ylabel in horizontal_loc)
                    {
                        pl = new Polyline();
                        double drafting_y =(ylabel - this.geometric_origin_y) * this.yscale_factor;
                        pl.AddVertexAt(0, new Point2d(xod - 0.09 * w,yod- ylabel), 0, 0, 0);
                        pl.AddVertexAt(1, new Point2d(xod+ 0.89 * w, yod - ylabel), 0, 0, 0);
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
                    insPt = new Point3d(xod-0.045*w,yod-0.0125*w, 0);
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

    }
}
