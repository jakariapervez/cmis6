using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace TitleBlocks
{
    public class TBlock
    {
        // Properties for Labels
        private string projectNameLabel;
        private string drawingTitleLabel;
        private string addressLabel;
        private string projectNoLabel;
        private string clientRefLabel;
        private string drawingDateLabel;
        private string drawingScaleLabel;
        private string revisionNoLabel;
        private string drawingNoLabel;

        // Properties for the actual drawing/project info
        public string ProjectName { get; set; }
        public string DrawingTitle { get; set; }
        public string Address { get; set; }
        public string ProjectNo { get; set; }
        public string ClientRef { get; set; }
        public string DrawingDate { get; set; }
        public string DrawingScale { get; set; }
        public string RevisionNo { get; set; }
        public string DrawingNo { get; set; }

        public TBlock()
        {
            // Labels
            projectNameLabel = "Project Name";
            drawingTitleLabel = "Dwg Title";
            addressLabel = "Address";
            projectNoLabel = "Proj. No";
            clientRefLabel = "Client Ref";
            drawingDateLabel = "Date";
            drawingScaleLabel = "Scale";
            revisionNoLabel = "Rev No.";
            drawingNoLabel = "Dwg. No";

            // Default Values
            ProjectName = "Default Project Name";
            DrawingTitle = "Default Drawing Title";
            Address = "Default Location";
            ProjectNo = "2019-##";
            ClientRef = "";
            DrawingDate = DateTime.Today.ToShortDateString();
            RevisionNo = "00";
            DrawingNo = "A#.#";
            DrawingScale = "1:###";
        }

        public void DrawTitleBlock(int height, int width)
        {
            // Get the Document and Database object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                // Draw the outer border based on the height and width
                Polyline pl = new Polyline();
                pl.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                pl.AddVertexAt(1, new Point2d(0, height), 0, 0, 0);
                pl.AddVertexAt(2, new Point2d(width, height), 0, 0, 0);
                pl.AddVertexAt(3, new Point2d(width, 0), 0, 0, 0);
                pl.Closed = true;                

                btr.AppendEntity(pl);
                trans.AddNewlyCreatedDBObject(pl, true);

                // Draw a line from the right border at 10% to the left
                Line ln = new Line();
                int titleWidth = Convert.ToInt32(width * 0.1);            // Titleblock space is 10% of the paper width
                ln.ColorIndex = 1;
                ln.StartPoint = new Point3d((width - titleWidth), 0, 0);
                ln.EndPoint = new Point3d((width - titleWidth), height, 0);
                btr.AppendEntity(ln);
                trans.AddNewlyCreatedDBObject(ln, true);

                // Draw the Horizontal Partition Lines. Start from the bottom
                Line hLn1 = new Line();
                hLn1.ColorIndex = 2;
                int titleHeight = Convert.ToInt32(height * 0.02);
                hLn1.StartPoint = new Point3d((width - titleWidth), titleHeight, 0);
                hLn1.EndPoint = new Point3d(width, titleHeight, 0);
                btr.AppendEntity(hLn1);
                trans.AddNewlyCreatedDBObject(hLn1, true);

                Line hLn2 = new Line();
                hLn2.ColorIndex = 3;
                int titleHeight2 = Convert.ToInt32(height * 0.04);
                hLn2.StartPoint = new Point3d((width - titleWidth), titleHeight2, 0);
                hLn2.EndPoint = new Point3d(width, titleHeight2, 0);
                btr.AppendEntity(hLn2);
                trans.AddNewlyCreatedDBObject(hLn2, true);

                Line hLn3 = new Line();
                hLn3.ColorIndex = 4;
                int titleHeight3 = Convert.ToInt32(height * 0.06);
                hLn3.StartPoint = new Point3d((width - titleWidth), titleHeight3, 0);
                hLn3.EndPoint = new Point3d(width, titleHeight3, 0);
                btr.AppendEntity(hLn3);
                trans.AddNewlyCreatedDBObject(hLn3, true);

                Line hLn4 = new Line();
                hLn4.ColorIndex = 5;
                int titleHeight4 = Convert.ToInt32(height * 0.08);
                hLn4.StartPoint = new Point3d((width - titleWidth), titleHeight4, 0);
                hLn4.EndPoint = new Point3d(width, titleHeight4, 0);
                btr.AppendEntity(hLn4);
                trans.AddNewlyCreatedDBObject(hLn4, true);

                Line hLn5 = new Line();
                hLn5.ColorIndex = 6;
                int titleHeight5 = Convert.ToInt32(height * 0.10);
                hLn5.StartPoint = new Point3d((width - titleWidth), titleHeight5, 0);
                hLn5.EndPoint = new Point3d(width, titleHeight5, 0);
                btr.AppendEntity(hLn5);
                trans.AddNewlyCreatedDBObject(hLn5, true);

                // Next draw all the vertical Partition Lines. Starting from the right
                Line vLn1 = new Line();
                vLn1.ColorIndex = 8;
                int titleWidth1 = Convert.ToInt32(width * 0.02);
                vLn1.StartPoint = new Point3d((width - titleWidth1), 0, 0);
                vLn1.EndPoint = new Point3d((width - titleWidth1), titleHeight, 0);
                btr.AppendEntity(vLn1);
                trans.AddNewlyCreatedDBObject(vLn1, true);

                Line vLn2 = new Line();
                vLn2.ColorIndex = 9;
                int titleWidth2 = Convert.ToInt32(width * 0.04);
                vLn2.StartPoint = new Point3d((width - titleWidth2), 0, 0);
                vLn2.EndPoint = new Point3d((width - titleWidth2), titleHeight2, 0);
                btr.AppendEntity(vLn2);
                trans.AddNewlyCreatedDBObject(vLn2, true);

                Line vLn3 = new Line();                
                vLn3.ColorIndex = 10;
                int titleWidth3 = Convert.ToInt32(width * 0.07);
                vLn3.StartPoint = new Point3d((width - titleWidth3), 0, 0);
                vLn3.EndPoint = new Point3d((width - titleWidth3), titleHeight2, 0);
                btr.AppendEntity(vLn3);
                trans.AddNewlyCreatedDBObject(vLn3, true);


                // Draw the Text labels
                MText txProj = new MText();
                txProj.Contents = this.projectNameLabel;
                txProj.TextHeight = height * 0.0025;
                double xCoord = (width - titleWidth + (titleWidth * 0.02));
                double yCoord = (titleHeight4 - (titleHeight4 * 0.02));
                Point3d insPt = new Point3d(xCoord, yCoord, 0);
                txProj.Location = insPt;
                btr.AppendEntity(txProj);
                trans.AddNewlyCreatedDBObject(txProj, true);

                MText txDwg = new MText();
                txDwg.Contents = this.drawingTitleLabel;
                txDwg.TextHeight = height * 0.0025;
                yCoord = (titleHeight3 - (titleHeight3 * 0.02));
                insPt = new Point3d(xCoord, yCoord, 0);
                txDwg.Location = insPt;
                btr.AppendEntity(txDwg);
                trans.AddNewlyCreatedDBObject(txDwg, true);

                MText txAddr = new MText();
                txAddr.Contents = this.addressLabel;
                txAddr.TextHeight = height * 0.0025;
                yCoord = (titleHeight2 - (titleHeight2 * 0.02));
                insPt = new Point3d(xCoord, yCoord, 0);
                txAddr.Location = insPt;
                btr.AppendEntity(txAddr);
                trans.AddNewlyCreatedDBObject(txAddr, true);

                MText txProjNo = new MText();
                txProjNo.Contents =  this.projectNoLabel;
                txProjNo.TextHeight = height * 0.0025;
                xCoord = (width - titleWidth3 + (titleWidth3 * 0.02));
                yCoord = (titleHeight2 - (titleHeight2 * 0.02));
                insPt = new Point3d(xCoord, yCoord, 0);
                txProjNo.Location = insPt;
                btr.AppendEntity(txProjNo);
                trans.AddNewlyCreatedDBObject(txProjNo, true);

                MText txClientRef = new MText();
                txClientRef.Contents = this.clientRefLabel;
                txClientRef.TextHeight = height * 0.0025;
                xCoord = (width - titleWidth2 + (titleWidth2 * 0.02));
                insPt = new Point3d(xCoord, yCoord, 0);
                txClientRef.Location = insPt;
                btr.AppendEntity(txClientRef);
                trans.AddNewlyCreatedDBObject(txClientRef, true);

                MText txDrawingDate = new MText();
                txDrawingDate.Contents = this.drawingDateLabel;
                txDrawingDate.TextHeight = height * 0.0025;
                titleHeight = Convert.ToInt32(height * 0.02);
                xCoord = (width - titleWidth + (titleWidth * 0.02));
                yCoord = (titleHeight - (titleHeight * 0.02));
                insPt = new Point3d(xCoord, yCoord, 0);
                txDrawingDate.Location = insPt;
                btr.AppendEntity(txDrawingDate);
                trans.AddNewlyCreatedDBObject(txDrawingDate, true);

                MText txScale = new MText();
                txScale.Contents = this.drawingScaleLabel;
                txScale.TextHeight = height * 0.0025;
                xCoord = (width - titleWidth3 + (titleWidth3 * 0.02));
                insPt = new Point3d(xCoord, yCoord, 0);
                txScale.Location = insPt;
                btr.AppendEntity(txScale);
                trans.AddNewlyCreatedDBObject(txScale, true);

                MText txRevNo = new MText();
                txRevNo.Contents = this.revisionNoLabel;
                txRevNo.TextHeight = height * 0.0025;
                xCoord = (width - titleWidth2 + (titleWidth2 * 0.02));
                insPt = new Point3d(xCoord, yCoord, 0);
                txRevNo.Location = insPt;
                btr.AppendEntity(txRevNo);
                trans.AddNewlyCreatedDBObject(txRevNo, true);

                MText txDwgNo = new MText();
                txDwgNo.Contents = this.drawingNoLabel;
                txDwgNo.TextHeight = height * 0.0025;
                xCoord = (width - titleWidth1 + (titleWidth1 * 0.02));
                insPt = new Point3d(xCoord, yCoord, 0);
                txDwgNo.Location = insPt;
                btr.AppendEntity(txDwgNo);
                trans.AddNewlyCreatedDBObject(txDwgNo, true);


                // Create the Drawing Info Texts
                MText txProjName = new MText();
                txProjName.Contents = this.ProjectName;
                txProjName.TextHeight = height * 0.0075;
                xCoord = (width - titleWidth + (titleWidth * 0.20));
                yCoord = titleHeight4 - (titleHeight4 * 0.1);
                insPt = new Point3d(xCoord, yCoord, 0);
                txProjName.Location = insPt;
                btr.AppendEntity(txProjName);
                trans.AddNewlyCreatedDBObject(txProjName, true);

                MText txDwgTitle = new MText();
                txDwgTitle.Contents = this.DrawingTitle;
                txDwgTitle.TextHeight = height * 0.0075;
                yCoord = titleHeight3 - (titleHeight3 * 0.1);
                insPt = new Point3d(xCoord, yCoord, 0);
                txDwgTitle.Location = insPt;
                btr.AppendEntity(txDwgTitle);
                trans.AddNewlyCreatedDBObject(txDwgTitle, true);

                MText txAddress = new MText();
                txAddress.Contents = this.Address;                
                txAddress.TextHeight = height * 0.004;
                xCoord = (width - titleWidth + (titleWidth * 0.01));
                yCoord = titleHeight2 - (titleHeight2 * 0.2);
                insPt = new Point3d(xCoord, yCoord, 0);
                txAddress.Location = insPt;
                btr.AppendEntity(txAddress);
                trans.AddNewlyCreatedDBObject(txAddress, true);

                MText txProjectNum = new MText();
                txProjectNum.Contents = this.ProjectNo;
                txProjectNum.TextHeight = height * 0.004;
                xCoord = (width - titleWidth3 + (titleWidth3 * 0.1));
                yCoord = titleHeight2 - (titleHeight2 * 0.2);
                insPt = new Point3d(xCoord, yCoord, 0);
                txProjectNum.Location = insPt;
                btr.AppendEntity(txProjectNum);
                trans.AddNewlyCreatedDBObject(txProjectNum, true);

                MText txProjDrawingDate = new MText();
                txProjDrawingDate.Contents = this.DrawingDate;
                txProjDrawingDate.TextHeight = height * 0.004;
                titleHeight = Convert.ToInt32(height * 0.02);
                xCoord = (width - titleWidth + (titleWidth * 0.03));
                yCoord = titleHeight - (titleHeight * 0.4);
                insPt = new Point3d(xCoord, yCoord, 0);
                txProjDrawingDate.Location = insPt;
                btr.AppendEntity(txProjDrawingDate);
                trans.AddNewlyCreatedDBObject(txProjDrawingDate, true);

                MText txDwgScale = new MText();
                txDwgScale.Contents = this.DrawingScale;
                txDwgScale.TextHeight = height * 0.004;
                titleHeight = Convert.ToInt32(height * 0.02);
                xCoord = (width - titleWidth3 + (titleWidth3 * 0.15));
                yCoord = titleHeight - (titleHeight * 0.4);
                insPt = new Point3d(xCoord, yCoord, 0);
                txDwgScale.Location = insPt;
                btr.AppendEntity(txDwgScale);
                trans.AddNewlyCreatedDBObject(txDwgScale, true);

                MText txRevNum = new MText();
                txRevNum.Contents = this.RevisionNo;
                txRevNum.TextHeight = height * 0.004;
                titleHeight = Convert.ToInt32(height * 0.02);
                xCoord = (width - titleWidth2 + (titleWidth2 * 0.2));
                yCoord = titleHeight - (titleHeight * 0.4);
                insPt = new Point3d(xCoord, yCoord, 0);
                txRevNum.Location = insPt;
                btr.AppendEntity(txRevNum);
                trans.AddNewlyCreatedDBObject(txRevNum, true);

                MText txDwgNum = new MText();
                txDwgNum.Contents = this.DrawingNo;
                txDwgNum.TextHeight = height * 0.004;
                titleHeight = Convert.ToInt32(height * 0.02);
                xCoord = (width - titleWidth1 + (titleWidth1 * 0.15));
                yCoord = titleHeight - (titleHeight * 0.4);
                insPt = new Point3d(xCoord, yCoord, 0);
                txDwgNum.Location = insPt;
                btr.AppendEntity(txDwgNum);
                trans.AddNewlyCreatedDBObject(txDwgNum, true);

                // Execute Zoom all
                doc.SendStringToExecute("._zoom _all ", false, false, false);

                // Commit the transaction
                trans.Commit();
            }
        }
    }
}
