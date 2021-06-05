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
    class Label_Annotarion_With_Circle:Label_Annotation
    {
        public double radius;
        public Label_Annotarion_With_Circle
            (string annotation_text,
            Point2d anotation_location, Point2d Geometric_origin,
            Point2d Drafting_origin, bool withLeader,
            double sx, double sy, double drafting_width, double cirle_radius)

            :base(annotation_text, anotation_location, Geometric_origin, Drafting_origin, 
            withLeader, sx,sy, drafting_width )
        {
        this.radius=cirle_radius;

    }
        public void  drawAnnotationsWithCircle (double text_rotation, int alignment)
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
                try
                {
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
                    //*Drawing Circle Around Label*/
                    Circle mycircle = new Circle();
                    mycircle.SetDatabaseDefaults();
                    mycircle.Center = insPt;
                    mycircle.Radius = this.radius;                  
                    btr.AppendEntity(mycircle);
                    trans.AddNewlyCreatedDBObject(mycircle, true);



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
