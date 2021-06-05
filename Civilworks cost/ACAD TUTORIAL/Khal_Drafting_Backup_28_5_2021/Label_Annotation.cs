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

   public class Label_Annotation
    {
        public Point2d drafting_orgin;
        public Point2d geometric_orgin;
        public Point2d text_Location;
        public double xscale_factor;
        public double yscale_factor;
        public string text_content;
        public bool hasLeader;
       // public Point2d pointer;
        public double Draftingwidth;
        public double text_Rotation_Angle;       
        public int alignment_index;

       public  Label_Annotation(string annotation_text,
            Point2d anotation_location,Point2d Geometric_origin,
            Point2d Drafting_origin,bool withLeader,
            double sx,double sy,double drafting_width)
        {
            this.text_content = annotation_text;
            this.text_Location =anotation_location;
            this.geometric_orgin = Geometric_origin;
            this.drafting_orgin = Drafting_origin;
            this.xscale_factor = sx;
            this.yscale_factor = sy;
            this.hasLeader = withLeader;
            this.Draftingwidth = drafting_width;



            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            edt.WriteMessage("\n testing constructor...");
            this.displayPoint("text Location",anotation_location);
            this.displayPoint("text Location", this.text_Location);
            this.displayPoint("geometric_origin", Geometric_origin);
            this.displayPoint("geometric_origin", this.geometric_orgin);
            this.displayPoint("drafting_origin", Drafting_origin);
            this.displayPoint("drafting_origin", this.drafting_orgin);
            this.displayAnnotaion();

        }
        public void displayPoint(string point_name, Point2d p)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            edt.WriteMessage("\n"+point_name+" x="+p.X+"y="+p.Y);
        }

        private void populateAlignment()
        { 
        
        
        }
        public void displayAnnotaion()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            edt.WriteMessage("\n displaying annotaion oject information......");
            edt.WriteMessage("\n geometric origin x=" + this.geometric_orgin.X + "y=" + this.geometric_orgin.Y);
            edt.WriteMessage("\n drafting origin x=" + this.drafting_orgin.X + "y=" + this.drafting_orgin.Y);
            edt.WriteMessage("\n Text Location x=" + this.text_Location.X + "y=" + this.text_Location.Y);
            edt.WriteMessage("\n sx=" + this.xscale_factor + "y=" + this.yscale_factor);
        }
        public void drawAnnotation(double text_rotation,int alignment)
        {
            /*
             alignment 1=midle.center;
            alignment 2=
             
             */



            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                try {
                    this.displayAnnotaion();
                    MText txtLabel = new MText();
                    txtLabel.Contents = this.text_content;
                    double w = this.Draftingwidth;
                    txtLabel.TextHeight = w * 0.008;
                    /* Calculating Drfting Location*/
                    Mytransformation trs = new Mytransformation();
                    Point2d tog = trs.Translate(this.text_Location, -this.geometric_orgin.X, -this.geometric_orgin.Y);
                    //edt.WriteMessage("\n x" + draftingOrigin.X + " y=" + draftingOrigin.Y);
                    Point2d scaledPoint = trs.Scale(tog, this.xscale_factor, this.yscale_factor);
                    Point2d draftingPoint = trs.Translate(scaledPoint, this.drafting_orgin.X, this.drafting_orgin.Y);
                    edt.WriteMessage("\n drafting x=" + draftingPoint.X + " y=" + draftingPoint.Y);



                    Point3d insPt = new Point3d(draftingPoint.X, draftingPoint.Y, 0);
                    txtLabel.Location = insPt;
                    
                    if (alignment == 1)
                    {
                        txtLabel.Attachment = AttachmentPoint.TopLeft;

                    }
                    else if (alignment == 2)
                    {
                        txtLabel.Attachment = AttachmentPoint.TopCenter;

                    }
                    else if (alignment == 3)
                    {
                        txtLabel.Attachment = AttachmentPoint.MiddleCenter;

                    }
                    else
                    {
                        txtLabel.Attachment = AttachmentPoint.TopRight;

                    }
                    txtLabel.Rotation = text_rotation;
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
