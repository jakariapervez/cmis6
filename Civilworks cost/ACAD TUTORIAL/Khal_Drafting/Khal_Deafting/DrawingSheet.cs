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

namespace Khal_Deafting

{
    public class DrawingSheet

    {
        // Properties for Labels
        /*
        private string projectNameLabel;
        private string drawingTitleLabel;
        private string addressLabel;
        private string projectNoLabel;
        private string clientRefLabel;
        private string drawingDateLabel;
        private string drawingScaleLabel;
        private string revisionNoLabel;
        private string drawingNoLabel;
        */

        // Properties for the actual drawing/project info
        public string OrganizationName { get; set; }
        public string Design_office { get; set; }
        public string ProjectName { get; set; }
        public string Work_name { get; set; }
        public string Designed_by { get; set; }
        public string Recommended_by { get; set; }
        public string Approved_by { get; set; }
        public string sheetTitle { get; set; }

        public string drawingNo { get; set; }



        public string DrawingTitle { get; set; }
        public string Address { get; set; }
        public string ProjectNo { get; set; }
        public string ClientRef { get; set; }
        public string DrawingDate { get; set; }
        public string DrawingScale { get; set; }
        public string RevisionNo { get; set; }
        public string DrawingNo { get; set; }
        public Point2d TopLeft { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public DrawingSheet()
        {
            // Labels


            // Default Values
            ProjectName = "Default Project Name";
            DrawingTitle = "Default Drawing Title";
            Address = "Default Location";
            ProjectNo = "2019-##";
            // ClientRef = "";
            // DrawingDate = DateTime.Today.ToShortDateString();
            // RevisionNo = "00";
            // DrawingNo = "A#.#";
            // DrawingScale = "1:###";
        }
        public void setHeadings(List<string> args)
        {
            this.OrganizationName = args[0];
            this.Design_office = args[1];
            this.ProjectName = args[2];
            this.Work_name = args[3];
            this.Designed_by = args[4];
            this.Recommended_by = args[5];
            this.Approved_by = args[6];



        }
        public void setVaiableHeadings(List<string> args)
        {

            this.sheetTitle = args[0];
            this.DrawingDate = args[2];
            this.drawingNo = args[1];

        }
        public void setDimension(double width)
        {
            this.width = (int)width;
            this.height = (int)(this.width / Math.Sqrt(2));


        }



        public void DrawTitleBlock2(int width)
        {
            int height = (int)(width / Math.Sqrt(2));
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                double x0 = this.TopLeft.X;
                double y0 = this.TopLeft.Y;
                // Draw the outer border based on the height and width
                Polyline pl = new Polyline();
                pl.AddVertexAt(0, new Point2d(x0, y0), 0, 0, 0);
                pl.AddVertexAt(1, new Point2d(x0 + width, y0), 0, 0, 0);
                pl.AddVertexAt(2, new Point2d(x0 + width, y0 - height), 0, 0, 0);
                pl.AddVertexAt(3, new Point2d(x0, y0 - height), 0, 0, 0);
                pl.Closed = true;

                btr.AppendEntity(pl);
                trans.AddNewlyCreatedDBObject(pl, true);
                //Draw Border of Title Block
                Polyline title_block_border = new Polyline();
                title_block_border.AddVertexAt(0, new Point2d(x0 + 0.7 * width, y0 - height), 0, 0, 0);
                title_block_border.AddVertexAt(1, new Point2d(x0 + 0.7 * width, y0 - height + 0.24 * width), 0, 0, 0);
                title_block_border.AddVertexAt(2, new Point2d(x0 + width, y0 - height + 0.24 * width), 0, 0, 0);
                btr.AppendEntity(title_block_border);
                trans.AddNewlyCreatedDBObject(title_block_border, true);
                //First Horizonal Line

                Polyline L1 = new Polyline();
                L1.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - height + 0.225 * width), 0, 0, 0);
                L1.AddVertexAt(1, new Point2d(x0 + width, y0 - height * 0.785), 0, 0, 0);
                btr.AppendEntity(L1);
                trans.AddNewlyCreatedDBObject(L1, true);
                //Second Horizontal Line
                Polyline L2 = new Polyline();
                L2.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - height * 0.81), 0, 0, 0);
                L2.AddVertexAt(1, new Point2d(x0 + width, y0 - height * 0.81), 0, 0, 0);
                btr.AppendEntity(L2);
                trans.AddNewlyCreatedDBObject(L2, true);
                //Third Horizontal Line
                Polyline L3 = new Polyline();
                L3.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - height * 0.84), 0, 0, 0);
                L3.AddVertexAt(1, new Point2d(x0 + width, y0 - height * 0.84), 0, 0, 0);
                btr.AppendEntity(L3);
                trans.AddNewlyCreatedDBObject(L3, true);
                //Fourth Horizontal Line
                Polyline L4 = new Polyline();
                L4.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - height * 0.9), 0, 0, 0);
                L4.AddVertexAt(1, new Point2d(x0 + width, y0 - height * 0.9), 0, 0, 0);
                btr.AppendEntity(L4);
                trans.AddNewlyCreatedDBObject(L4, true);
                //Fifth Horizontal Line
                Polyline L5 = new Polyline();
                L5.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - height * 0.91), 0, 0, 0);
                L5.AddVertexAt(1, new Point2d(x0 + width, y0 - height * 0.91), 0, 0, 0);
                btr.AppendEntity(L5);
                trans.AddNewlyCreatedDBObject(L5, true);
                // Sixth Horizontal Line
                Polyline L6 = new Polyline();
                L6.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - height * 0.99), 0, 0, 0);
                L6.AddVertexAt(1, new Point2d(x0 + width, y0 - height * 0.99), 0, 0, 0);
                btr.AppendEntity(L6);
                trans.AddNewlyCreatedDBObject(L6, true);
                // First Vertical Divider
                Polyline V1 = new Polyline();
                V1.AddVertexAt(0, new Point2d(x0 + width * 0.85, y0 - height * 0.91), 0, 0, 0);
                V1.AddVertexAt(1, new Point2d(x0 + width * 0.85, y0 - height * 0.99), 0, 0, 0);
                btr.AppendEntity(V1);
                trans.AddNewlyCreatedDBObject(V1, true);
                // Draw the Originization
                MText txtOrganization = new MText();
                txtOrganization.Contents = this.OrganizationName;
                txtOrganization.TextHeight = width * 0.008;
                double xCoord = x0 + 0.75 * width;
                double yCoord = y0 - height * 0.772;
                Point3d insPt = new Point3d(xCoord, yCoord, 0);
                txtOrganization.Location = insPt;
                btr.AppendEntity(txtOrganization);
                trans.AddNewlyCreatedDBObject(txtOrganization, true);
                // Draw the Design Circle
                MText txtDesignOffice = new MText();
                txtDesignOffice.Contents = this.Design_office;
                txtDesignOffice.TextHeight = width * 0.008;
                xCoord = x0 + 0.71 * width;
                yCoord = y0 - height * 0.787;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtDesignOffice.Location = insPt;
                btr.AppendEntity(txtDesignOffice);
                trans.AddNewlyCreatedDBObject(txtDesignOffice, true);
                // Draw the Project Name 
                MText txtProjectName = new MText();
                txtProjectName.Contents = this.ProjectName;
                txtProjectName.TextHeight = width * 0.0075;

                xCoord = x0 + 0.71 * width;
                yCoord = y0 - height * 0.812;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtProjectName.Location = insPt;
                txtProjectName.Width = 0.28 * width;
                btr.AppendEntity(txtProjectName);
                trans.AddNewlyCreatedDBObject(txtProjectName, true);
                // Draw the Work Name 
                MText txtWorkName = new MText();
                txtWorkName.Contents = this.Work_name;
                txtWorkName.TextHeight = width * 0.008;

                xCoord = x0 + 0.71 * width;
                yCoord = y0 - height * 0.85;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtWorkName.Location = insPt;
                txtWorkName.Width = 0.28 * width;

                btr.AppendEntity(txtWorkName);
                trans.AddNewlyCreatedDBObject(txtWorkName, true);








                doc.SendStringToExecute("._zoom _all ", false, false, false);
                trans.Commit();

            }



        }
        public void DrawTitleBlock3()
        {
            int height = this.height;
            int width = this.width;

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                double x0 = this.TopLeft.X;
                double y0 = this.TopLeft.Y;
                int w = width;
                int h = height;
                // Draw the outer border based on the height and width
                Polyline pl = new Polyline();
                pl.AddVertexAt(0, new Point2d(x0, y0), 0, 0, 0);
                pl.AddVertexAt(1, new Point2d(x0 + w, y0), 0, 0, 0);
                pl.AddVertexAt(2, new Point2d(x0 + w, y0 - h), 0, 0, 0);
                pl.AddVertexAt(3, new Point2d(x0, y0 - h), 0, 0, 0);
                pl.Closed = true;

                btr.AppendEntity(pl);
                trans.AddNewlyCreatedDBObject(pl, true);
                //Draw Border of Title Block
                Polyline title_block_border = new Polyline();
                title_block_border.AddVertexAt(0, new Point2d(x0 + 0.7 * w, y0 - h), 0, 0, 0);
                title_block_border.AddVertexAt(1, new Point2d(x0 + 0.7 * w, y0 - h + 0.24 * w), 0, 0, 0);
                title_block_border.AddVertexAt(2, new Point2d(x0 + width, y0 - h + 0.24 * w), 0, 0, 0);
                btr.AppendEntity(title_block_border);
                trans.AddNewlyCreatedDBObject(title_block_border, true);
                //First Horizonal Line

                Polyline L1 = new Polyline();
                L1.AddVertexAt(0, new Point2d(x0 + w * 0.7, y0 - h + 0.225 * w), 0, 0, 0);
                L1.AddVertexAt(1, new Point2d(x0 + w, y0 - h + 0.225 * w), 0, 0, 0);
                btr.AppendEntity(L1);
                trans.AddNewlyCreatedDBObject(L1, true);
                //Second Horizontal Line
                Polyline L2 = new Polyline();
                L2.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - h + 0.20 * w), 0, 0, 0);
                L2.AddVertexAt(1, new Point2d(x0 + width, y0 - h + 0.20 * w), 0, 0, 0);
                btr.AppendEntity(L2);
                trans.AddNewlyCreatedDBObject(L2, true);
                //Third Horizontal Line
                Polyline L3 = new Polyline();
                L3.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - h + 0.17 * w), 0, 0, 0);
                L3.AddVertexAt(1, new Point2d(x0 + width, y0 - h + 0.17 * w), 0, 0, 0);
                btr.AppendEntity(L3);
                trans.AddNewlyCreatedDBObject(L3, true);
                //Fourth Horizontal Line
                Polyline L4 = new Polyline();
                L4.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - h + 0.11 * w), 0, 0, 0);
                L4.AddVertexAt(1, new Point2d(x0 + width, y0 - h + 0.11 * w), 0, 0, 0);
                btr.AppendEntity(L4);
                trans.AddNewlyCreatedDBObject(L4, true);
                //Fifth Horizontal Line
                Polyline L5 = new Polyline();
                L5.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - h + 0.095 * w), 0, 0, 0);
                L5.AddVertexAt(1, new Point2d(x0 + width, y0 - h + 0.095 * w), 0, 0, 0);
                btr.AppendEntity(L5);
                trans.AddNewlyCreatedDBObject(L5, true);
                // Sixth Horizontal Line
                Polyline L6 = new Polyline();
                L6.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - h + 0.055 * w), 0, 0, 0);
                L6.AddVertexAt(1, new Point2d(x0 + width * 0.85, y0 - h + 0.055 * w), 0, 0, 0);
                btr.AppendEntity(L6);
                trans.AddNewlyCreatedDBObject(L6, true);
                // Sixth Horizontal Line
                Polyline L7 = new Polyline();
                L7.AddVertexAt(0, new Point2d(x0 + width * 0.7, y0 - h + 0.015 * w), 0, 0, 0);
                L7.AddVertexAt(1, new Point2d(x0 + width, y0 - h + 0.015 * w), 0, 0, 0);
                btr.AppendEntity(L7);
                trans.AddNewlyCreatedDBObject(L7, true);
                // First Vertical Divider
                Polyline V1 = new Polyline();
                V1.AddVertexAt(0, new Point2d(x0 + width * 0.85, y0 - h + 0.095 * w), 0, 0, 0);
                V1.AddVertexAt(1, new Point2d(x0 + width * 0.85, y0 - h + 0.015 * w), 0, 0, 0);
                btr.AppendEntity(V1);
                trans.AddNewlyCreatedDBObject(V1, true);
                // First Vertical Divider
                Polyline V2 = new Polyline();
                V2.AddVertexAt(0, new Point2d(x0 + width * 0.80, y0 - h + 0.015 * w), 0, 0, 0);
                V2.AddVertexAt(1, new Point2d(x0 + width * 0.80, y0 - h), 0, 0, 0);
                btr.AppendEntity(V2);
                trans.AddNewlyCreatedDBObject(V2, true);






                // Draw the Originization
                MText txtOrganization = new MText();
                txtOrganization.Contents = this.OrganizationName;
                txtOrganization.TextHeight = width * 0.008;
                double xCoord = x0 + 0.85 * width;
                double yCoord = y0 - h + 0.2325 * w;
                Point3d insPt = new Point3d(xCoord, yCoord, 0);
                txtOrganization.Location = insPt;
                txtOrganization.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtOrganization);
                trans.AddNewlyCreatedDBObject(txtOrganization, true);
                // Draw the Design Circle
                MText txtDesignOffice = new MText();
                txtDesignOffice.Contents = this.Design_office;
                txtDesignOffice.TextHeight = width * 0.008;
                xCoord = x0 + 0.85 * width;
                yCoord = y0 - h + 0.2125 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtDesignOffice.Location = insPt;
                txtDesignOffice.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtDesignOffice);
                trans.AddNewlyCreatedDBObject(txtDesignOffice, true);
                // Draw the Project Name 
                MText txtProjectName = new MText();
                txtProjectName.Contents = this.ProjectName;
                txtProjectName.TextHeight = width * 0.0075;

                xCoord = x0 + 0.85 * width;
                yCoord = y0 - h + 0.1850 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtProjectName.Location = insPt;
                txtProjectName.Width = 0.28 * width;
                txtProjectName.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtProjectName);
                trans.AddNewlyCreatedDBObject(txtProjectName, true);
                // Draw the Work Name 
                MText txtWorkName = new MText();
                txtWorkName.Contents = this.Work_name;
                txtWorkName.TextHeight = width * 0.008;
                xCoord = x0 + 0.85 * width;
                yCoord = y0 - h + 0.14 * w; ;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtWorkName.Location = insPt;
                txtWorkName.Width = 0.28 * width;
                txtWorkName.Attachment = AttachmentPoint.MiddleCenter;

                btr.AppendEntity(txtWorkName);
                trans.AddNewlyCreatedDBObject(txtWorkName, true);
                //Draw Sheettitle
                MText txtSheetTitle = new MText();
                txtSheetTitle.Contents = this.sheetTitle;
                txtSheetTitle.TextHeight = width * 0.008;
                xCoord = x0 + 0.85 * width;
                yCoord = y0 - h + 0.1025 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtSheetTitle.Location = insPt;
                txtSheetTitle.Width = 0.28 * width;
                txtSheetTitle.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtSheetTitle);
                trans.AddNewlyCreatedDBObject(txtSheetTitle, true);
                //draw Designer
                MText txtDesigner = new MText();
                txtDesigner.Contents = "DESIGNED BY:\n\n" + this.Designed_by;
                txtDesigner.TextHeight = width * 0.006;
                xCoord = x0 + 0.71 * width;
                yCoord = y0 - h + 0.075 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtDesigner.Location = insPt;
                txtDesigner.Width = 0.14 * w;
                txtDesigner.Attachment = AttachmentPoint.MiddleLeft;
                btr.AppendEntity(txtDesigner);
                trans.AddNewlyCreatedDBObject(txtDesigner, true);
                //draw Riviewer
                MText txtReviewer = new MText();
                txtReviewer.Contents = "CHECKED AND RECOMENDED BY:\n\n" + this.Recommended_by;
                txtReviewer.TextHeight = width * 0.006;
                xCoord = x0 + 0.71 * width;
                yCoord = y0 - h + 0.035 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtReviewer.Location = insPt;
                txtReviewer.Width = 0.14 * w;
                txtReviewer.Attachment = AttachmentPoint.MiddleLeft;
                btr.AppendEntity(txtReviewer);
                trans.AddNewlyCreatedDBObject(txtReviewer, true);
                //draw Riviewer
                MText txtApprover = new MText();
                txtApprover.Contents = "APPROVED BY:\n\n\n\n\n\n" + this.Approved_by;
                txtApprover.TextHeight = width * 0.006;
                xCoord = x0 + 0.86 * width;
                yCoord = y0 - h + 0.055 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtApprover.Location = insPt;
                txtApprover.Width = 0.14 * w;
                txtApprover.Attachment = AttachmentPoint.MiddleLeft;
                btr.AppendEntity(txtApprover);
                trans.AddNewlyCreatedDBObject(txtApprover, true);
                //draw DrawingDate
                MText txtDrawigDate = new MText();
                txtDrawigDate.Contents = "Date:" + this.DrawingDate;
                txtDrawigDate.TextHeight = width * 0.008;
                xCoord = x0 + 0.75 * width;
                yCoord = y0 - h + 0.0075 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtDrawigDate.Location = insPt;
                txtDrawigDate.Width = 0.14 * w;
                txtDrawigDate.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtDrawigDate);
                trans.AddNewlyCreatedDBObject(txtDrawigDate, true);
                //draw DrawingNumber
                MText txtDrawigNo = new MText();
                txtDrawigNo.Contents = "Drawing No:" + this.drawingNo;
                txtDrawigNo.TextHeight = width * 0.008;
                xCoord = x0 + 0.90 * width;
                yCoord = y0 - h + 0.0075 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtDrawigNo.Location = insPt;
                txtDrawigNo.Width = 0.22 * w;
                txtDrawigNo.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtDrawigNo);
                trans.AddNewlyCreatedDBObject(txtDrawigNo, true);





                doc.SendStringToExecute("._zoom _all ", false, false, false);
                trans.Commit();

            }



        }
        public Point2d DrawingOriginA()
        {
            double x0 = this.TopLeft.X;
            double y0 = this.TopLeft.Y;
            double w = this.width;
            Point2d a = new Point2d(x0 + 0.05 * w, y0 - 0.05 * w);
            return a;
        }
        public void DrawRevisonBlock()
        {
            int height = this.height;
            int width = this.width;

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                double x0 = this.TopLeft.X;
                double y0 = this.TopLeft.Y;
                int w = width;
                int h = height;
                //Draw Border of Revisopn Block

                Polyline title_block_border = new Polyline();
                title_block_border.AddVertexAt(0, new Point2d(x0 + 0.79 * w, y0 - h + 0.24 * w), 0, 0, 0);
                title_block_border.AddVertexAt(1, new Point2d(x0 + 0.79 * w, y0 - h + 0.35 * w), 0, 0, 0);
                title_block_border.AddVertexAt(2, new Point2d(x0 + width, y0 - h +0.35*w), 0, 0, 0);
                btr.AppendEntity(title_block_border);
                trans.AddNewlyCreatedDBObject(title_block_border, true);
                //First Horizonal Line

                Polyline L1 = new Polyline();
                L1.AddVertexAt(0, new Point2d(x0 + w * 0.79, y0 - h + 0.32 * w), 0, 0, 0);
                L1.AddVertexAt(1, new Point2d(x0 + w, y0 - h + 0.32 * w), 0, 0, 0);
                btr.AppendEntity(L1);
                trans.AddNewlyCreatedDBObject(L1, true);
                //Second Horizontal Line
                Polyline L2 = new Polyline();
                L2.AddVertexAt(0, new Point2d(x0 + width * 0.79, y0 - h + 0.26 * w), 0, 0, 0);
                L2.AddVertexAt(1, new Point2d(x0 + width, y0 - h + 0.26 * w), 0, 0, 0);
                btr.AppendEntity(L2);
                trans.AddNewlyCreatedDBObject(L2, true);
                //Third Horizontal Line
                Polyline L3 = new Polyline();
                L3.AddVertexAt(0, new Point2d(x0 + width * 0.79, y0 - h + 0.2525 * w), 0, 0, 0);
                L3.AddVertexAt(1, new Point2d(x0 + width, y0 - h + 0.2525 * w), 0, 0, 0);
                btr.AppendEntity(L3);
                trans.AddNewlyCreatedDBObject(L3, true);
               
                // First Vertical Divider
                Polyline V1 = new Polyline();
                V1.AddVertexAt(0, new Point2d(x0 + width * 0.80, y0 - h + 0.35 * w), 0, 0, 0);
                V1.AddVertexAt(1, new Point2d(x0 + width * 0.80, y0 - h + 0.35 * w), 0, 0, 0);
                btr.AppendEntity(V1);
                trans.AddNewlyCreatedDBObject(V1, true);
                
               // First Second Divider
               Polyline V2 = new Polyline();
               V2.AddVertexAt(0, new Point2d(x0 + width * 0.84, y0 - h + 0.35 * w), 0, 0, 0);
               V2.AddVertexAt(1, new Point2d(x0 + width * 0.84, y0 - h + 0.32 * w), 0, 0, 0);
               btr.AppendEntity(V2);
               trans.AddNewlyCreatedDBObject(V2, true);
               // First Third Divider
               Polyline V3 = new Polyline();
               V3.AddVertexAt(0, new Point2d(x0 + width * 0.88, y0 - h + 0.35 * w), 0, 0, 0);
               V3.AddVertexAt(1, new Point2d(x0 + width * 0.88, y0 - h + 0.32 * w), 0, 0, 0);
               btr.AppendEntity(V3);
               trans.AddNewlyCreatedDBObject(V3, true);
               // First Fourth Divider
               Polyline V4 = new Polyline();
               V4.AddVertexAt(0, new Point2d(x0 + width * 0.92, y0 - h + 0.35 * w), 0, 0, 0);
               V4.AddVertexAt(1, new Point2d(x0 + width * 0.92, y0 - h + 0.32 * w), 0, 0, 0);
               btr.AppendEntity(V4);
               trans.AddNewlyCreatedDBObject(V4, true);
               // First Fifth Divider
               Polyline V5 = new Polyline();
               V5.AddVertexAt(0, new Point2d(x0 + width * 0.96, y0 - h + 0.35 * w), 0, 0, 0);
               V5.AddVertexAt(1, new Point2d(x0 + width * 0.96, y0 - h + 0.32 * w), 0, 0, 0);
               btr.AppendEntity(V5);
               trans.AddNewlyCreatedDBObject(V5, true);

               // Sixth Vertical Divider
               Polyline V6 = new Polyline();
               V6.AddVertexAt(0, new Point2d(x0 + width * 0.80, y0 - h + 0.26 * w), 0, 0, 0);
               V6.AddVertexAt(1, new Point2d(x0 + width * 0.80, y0 - h + 0.2525 * w), 0, 0, 0);
               btr.AppendEntity(V6);
               trans.AddNewlyCreatedDBObject(V6, true);
               //  7th Divider
               Polyline V7 = new Polyline();
               V7.AddVertexAt(0, new Point2d(x0 + width * 0.84, y0 - h + 0.26 * w), 0, 0, 0);
               V7.AddVertexAt(1, new Point2d(x0 + width * 0.84, y0 - h + 0.2525 * w), 0, 0, 0);
               btr.AppendEntity(V7);
               trans.AddNewlyCreatedDBObject(V7, true);
               // 8th Divider
               Polyline V8 = new Polyline();
               V8.AddVertexAt(0, new Point2d(x0 + width * 0.88, y0 - h + 0.26 * w), 0, 0, 0);
               V8.AddVertexAt(1, new Point2d(x0 + width * 0.88, y0 - h + 0.2525 * w), 0, 0, 0);
               btr.AppendEntity(V8);
               trans.AddNewlyCreatedDBObject(V8, true);
               // 9th Divider
               Polyline V9 = new Polyline();
               V9.AddVertexAt(0, new Point2d(x0 + width * 0.92, y0 - h + 0.26 * w), 0, 0, 0);
               V9.AddVertexAt(1, new Point2d(x0 + width * 0.92, y0 - h + 0.2525 * w), 0, 0, 0);
               btr.AppendEntity(V9);
               trans.AddNewlyCreatedDBObject(V9, true);
               // 10th Divider
               Polyline V10 = new Polyline();
               V10.AddVertexAt(0, new Point2d(x0 + width * 0.96, y0 - h + 0.26 * w), 0, 0, 0);
               V10.AddVertexAt(1, new Point2d(x0 + width * 0.96, y0 - h + 0.2525 * w), 0, 0, 0);
               btr.AppendEntity(V10);
               trans.AddNewlyCreatedDBObject(V10, true);
                /*Drawing Revison Texts*/
                //draw Date
                double xCoord, yCoord;
                Point3d insPt;
                MText txtDate = new MText();
                txtDate.Contents = "DATE";
                txtDate.TextHeight = width * 0.005;
                xCoord = x0 + 0.82 * w;
                yCoord = y0 - h + 0.25625 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtDate.Location = insPt;
                txtDate.Width = 0.04 * w;
                txtDate.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtDate);
                trans.AddNewlyCreatedDBObject(txtDate, true);
                //draw Prepared                
                MText txtPrep = new MText();
                txtPrep.Contents = "PREPARED";
                txtPrep.TextHeight = width * 0.005;
                xCoord = x0 + 0.86 * w;
                yCoord = y0 - h + 0.25625 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtPrep.Location = insPt;
                txtPrep.Width = 0.04 * w;
                txtPrep.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtPrep);
                trans.AddNewlyCreatedDBObject(txtPrep, true);
                //draw CHD                
                MText txtChd = new MText();
                txtChd.Contents = "CHD";
                txtChd.TextHeight = width * 0.005;
                xCoord = x0 + 0.90 * w;
                yCoord = y0 - h + 0.25625 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtChd.Location = insPt;
                txtChd.Width = 0.04 * w;
                txtChd.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtChd);
                trans.AddNewlyCreatedDBObject(txtChd, true);
                //draw REM                
                MText txtRem = new MText();
                txtRem.Contents = "REM";
                txtRem.TextHeight = width * 0.005;
                xCoord = x0 + 0.94 * w;
                yCoord = y0 - h + 0.25625 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtRem.Location = insPt;
                txtRem.Width = 0.04 * w;
                txtRem.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtRem);
                trans.AddNewlyCreatedDBObject(txtRem, true);
                //draw APPROVED                
                MText txtApprov = new MText();
                txtApprov.Contents = "APPROVED";
                txtApprov.TextHeight = width * 0.005;
                xCoord = x0 + 0.98 * w;
                yCoord = y0 - h + 0.25625 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtApprov.Location = insPt;
                txtApprov.Width = 0.04 * w;
                txtApprov.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtApprov);
                trans.AddNewlyCreatedDBObject(txtApprov, true);
                //draw REVISION               
                //txtRev.Contents = "{\fTimes New Roman|b1|i0|c0|p18;REVISION }";
                MText txtRev = new MText();
                txtRev.Contents = "REVISION ";
                txtRev.TextHeight = width * 0.008;
                xCoord = x0 + 0.895 * w;
                yCoord = y0 - h + 0.24625 * w;
                insPt = new Point3d(xCoord, yCoord, 0);
                txtRev.Location = insPt;
                txtRev.Width = 0.21 * w;
                txtRev.Attachment = AttachmentPoint.MiddleCenter;
                btr.AppendEntity(txtRev);
                trans.AddNewlyCreatedDBObject(txtRev, true);
               





                doc.SendStringToExecute("._zoom _all ", false, false, false);
                Application.SetSystemVariable("LWDISPLAY", 1);
                trans.Commit();

            }








        }

    }
}

