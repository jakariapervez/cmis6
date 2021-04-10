using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

/*Autocad Related Imports*/
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Geometry;

namespace Khal_Deafting

{
    






    public class DrawKhal

    {

        public ExcelPackage GetExcelFile(string filepath) 
        
        {
            FileInfo fileInfo = new FileInfo(filepath);

            ExcelPackage package = new ExcelPackage(fileInfo);
            return package;

        }
        public Tuple<List<double>, List<double>, List<double>, List<double>,List<Double>> getLongSectionData(ExcelWorksheet mysheet,Editor edt)
        {
            List<double> xvalues = new List<double>();
            List<double> CL_RL = new List<double>();
            List<double> LB_RL = new List<double>();
            List<double> RB_RL = new List<double>();
            List<double> DL_RL = new List<double>();
            int rows = mysheet.Dimension.Rows;
            int columns = mysheet.Dimension.Columns;
            for (int i = 2; i <= rows; i++)
            {

             
                /*   for (int j = 1; j <= columns; j++)
                   {

                       string content =mysheet.Cells[i, j].Value.ToString();
                       edt.WriteMessage(content + "\n");
                   }*/
                try

                {
                    string xcontent = mysheet.Cells[i, 2].Value.ToString();
                    edt.WriteMessage(xcontent + "\n");
                    string Cl_content = mysheet.Cells[i, 3].Value.ToString();
                    edt.WriteMessage(Cl_content + "\n");
                    string lb_content = mysheet.Cells[i, 4].Value.ToString();
                    edt.WriteMessage(lb_content + "\n");
                    string rb_content = mysheet.Cells[i, 5].Value.ToString();
                    edt.WriteMessage(rb_content + "\n");
                    string dl_content = mysheet.Cells[i, 6].Value.ToString();
                    edt.WriteMessage(dl_content + "\n");
                    xvalues.Add(double.Parse(xcontent));
                    CL_RL.Add(double.Parse(Cl_content));
                    LB_RL.Add(double.Parse(lb_content));
                   RB_RL.Add(double.Parse(rb_content));
                    DL_RL.Add(double.Parse(dl_content));





                }

                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error Encounterd:" + ex.Message);
                    

                }
              

            }

            return new Tuple<List<double>, List<double>, List<double>, List<double>,List<Double>>(xvalues, CL_RL, LB_RL, RB_RL, DL_RL);

        }
        
        
        public string SelectSpredsheet() 
        
        {
            OpenFileDialog ofd = new OpenFileDialog("Slect xlsx file",null,
                "xls;xlsx", "ExcelFileToLink", OpenFileDialog.OpenFileDialogFlags.DoNotTransferRemoteFiles);
            System.Windows.Forms.DialogResult dr= ofd.ShowDialog();
            if (dr != System.Windows.Forms.DialogResult.OK)
                return "";
            else
                return ofd.Filename;
            

     

        }
        
        
        public Polyline DrawProfile( List<double> xvalues, 
            List<double> yvalues,double xog,double yog,double sx,
            double sy,double xod,double yod )

        {
            Polyline pl = new Polyline();
            Int32 no_of_points = xvalues.Count;
            Mytransformation trs = new Mytransformation();
            for (int i = 0; i < no_of_points; i++)

            {
                Point2d mypoint = new Point2d(xvalues[i], yvalues[i]);
                Point2d tog = trs.Translate(mypoint,-xog,-yog);
                Point2d scaledPoint = trs.Scale(tog, sx, sy);
                Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);
                pl.AddVertexAt(i, draftingPoint, 0, 0, 0);

            }

                pl.SetDatabaseDefaults();
            return pl;

        }
       
      

    public Tuple<Document,Database,Editor> GetDocumentReference()

        {
           
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;


            return new Tuple<Document, Database, Editor>(doc, db, edt);
        
        }

    }
    public class Driver
    {
      //  [CommandMethod ("DRAW_KHAL")]
        public void DrawMyKhal () 
        {
            DrawKhal mykhal = new DrawKhal();
            Tuple <Document, Database, Editor> myreferences = mykhal.GetDocumentReference();
            Document doc = myreferences.Item1;
            Database db= myreferences.Item2;
            Editor edt = myreferences.Item3;
            edt.WriteMessage("Sucessfully Initiated Khal drawing......");
            string excelpath = mykhal.SelectSpredsheet();
            edt.WriteMessage("youhave selected:" + excelpath);
            ExcelPackage package =mykhal.GetExcelFile(excelpath);
            ExcelWorksheet lssheet = package.Workbook.Worksheets["LS"];
            Tuple<List<double>, List<double>, List<double>, List<double>,List<Double>> mydata =mykhal.getLongSectionData(lssheet, edt);
            List<double> xvalues = mydata.Item1;
            List<double> clvalues= mydata.Item2;
            List<double> lbvalues = mydata.Item3;
            List<double> rbvalues = mydata.Item4;
            List<double> dlvalues = mydata.Item5;
            edt.WriteMessage(mydata.Item1[0].ToString());
            using (Transaction trans=db.TransactionManager.StartTransaction()) 
            {
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    //Polyline cl = new Polyline();

                    /*
                    Polyline lb = new Polyline();
                    Polyline rb = new Polyline();

                    Int32 no_of_points = xvalues.Count;
                    double x0 = xvalues[0];
                    double xl = xvalues[no_of_points-1];
                    double length_x = xl - x0;
                    double n1;
                    double n2;
                    n2 = clvalues.Max();
                    n1 = clvalues.Min();
                    double dely1 =  n2-n1 ;
                    n2 = lbvalues.Max();
                    n1 = lbvalues.Min();
                    double dely2 = n2 - n1;
                    
                    n2 = rbvalues.Max();
                    n1 = rbvalues.Min();
                    double dely3 = n2 - n1;
                    var  dely_values = new List<double> { dely1, dely2, dely3 };
                    double dely = dely_values.Max();
                    edt.WriteMessage("\n min y diff=" + dely.ToString());
                    edt.WriteMessage("\n xdiff=" + length_x.ToString());
                    double AR = (length_x / dely);
                    double hscale_factor_muliplier = 0.5 * (1 / (Math.Sqrt(2)));
                    int ySF = (int)Math.Floor( length_x/ (dely* hscale_factor_muliplier));
                    edt.WriteMessage("\n yfactor="+ySF);
                    for (int i=0;i< no_of_points-1; i++) 
                    {
                    
                    //cl.AddVertexAt(i,new Point2d(xvalues[i]-x0, clvalues[i]* 100),0,0,0);
                    lb.AddVertexAt(i, new Point2d(xvalues[i] - x0, lbvalues[i]* 100), 0, 0, 0);
                    rb.AddVertexAt(i, new Point2d(xvalues[i] - x0, rbvalues[i]*100), 0, 0, 0);
                   
                    }
                    */
                    //cl.SetDatabaseDefaults();
                   


                    Polyline cl = mykhal.DrawProfile(xvalues, clvalues,
                       28410, 0, 1, 100, 200, 200);
                    btr.AppendEntity(cl);
                    trans.AddNewlyCreatedDBObject(cl, true);
                    Polyline lb = mykhal.DrawProfile(xvalues, lbvalues,
                       28410, 0, 1, 100, 200, 200);                    
                    btr.AppendEntity(lb);
                    trans.AddNewlyCreatedDBObject(lb, true);
                    Polyline rb = mykhal.DrawProfile(xvalues, rbvalues,
                       28410, 0, 1, 100, 200, 200);                    
                    btr.AppendEntity(rb);
                    trans.AddNewlyCreatedDBObject(rb, true);
                   Polyline dl = mykhal.DrawProfile(xvalues, dlvalues,28410, 0, 1, 100, 200, 200);
                   btr.AppendEntity(dl);
                   trans.AddNewlyCreatedDBObject(dl, true);
                   trans.Commit();



                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n" + ex.Message);
                    trans.Abort();
                }
            


            }
            XcelInput myinput = new XcelInput();
            var myheadings = myinput.getHeadings();
            DrawingSheet myblock = new DrawingSheet();
            myblock.setHeadings(myheadings);
            myblock.TopLeft = new Point2d(400, 400);
            myblock.setDimension(4719);
            myblock.DrawTitleBlock3();
            //myblock.DrawTitleBlock(210, 827);
           
                edt.WriteMessage("\n"+myheadings[0].ToString());
        }
        

        [CommandMethod("KHAL_DRAFTING")]
        public void DrawMyKhal2()
        {
            
            DrawKhal mykhal = new DrawKhal();
            Tuple<Document, Database, Editor> myreferences = mykhal.GetDocumentReference();
            Document doc = myreferences.Item1;
            Database db = myreferences.Item2;
            Editor edt = myreferences.Item3;
            DraftingSettings mysettings = new DraftingSettings();
            try
            {
                string excelpath = mykhal.SelectSpredsheet();
                edt.WriteMessage("youhave selected:" + excelpath);
                ExcelPackage package = mykhal.GetExcelFile(excelpath);
                ExcelWorksheet lssheet = package.Workbook.Worksheets["LS"];
                Tuple<List<double>, List<double>,
                    List<double>, List<double>, 
                    List<Double>> mydata = mykhal.getLongSectionData(lssheet, edt);
                List<double> xvalues = mydata.Item1;
                List<double> clvalues = mydata.Item2;
                List<double> lbvalues = mydata.Item3;
                List<double> rbvalues = mydata.Item4;
                List<double> dlvalues = mydata.Item5;
                edt.WriteMessage(mydata.Item1[0].ToString());
                /*Getting Sheet Information*/
                XcelInput myinput = new XcelInput();
                var myheadings = myinput.getHeadings();
                var sheetInfo = myinput.getSheetInfo(edt);
                var sheetTitles = sheetInfo.Item1;
                var drawing_nos = sheetInfo.Item2;
                var drawing_date = sheetInfo.Item3;
                List<double> xorigins = sheetInfo.Item4;
                List<double> yorigins = sheetInfo.Item5;
                List<double> sheetWidths = sheetInfo.Item6;
                int sheetNo = sheetWidths.Count;
                List<DrawingSheet> mydrawingSheets = new List<DrawingSheet>();

                for (int i = 1; i < sheetNo; i++)
                {
                    DrawingSheet myblock = new DrawingSheet();
                    myblock.TopLeft = new Point2d(xorigins[i], yorigins[i]);
                    myblock.setDimension(sheetWidths[i]);
                    List<string> myagrs = new List<string>();
                    myheadings.Add(sheetTitles[i]);
                    myheadings.Add(drawing_nos[i]);
                    myheadings.Add(drawing_date[i]);
                    myblock.setHeadings(myheadings);
                    myagrs.Add(sheetTitles[i]);
                    myagrs.Add(drawing_nos[i]);
                    myagrs.Add(drawing_date[i]);
                    myblock.setVaiableHeadings(myagrs);
                    edt.WriteMessage(myblock.drawingNo);
                    edt.WriteMessage(myblock.DrawingDate);
                    myblock.DrawTitleBlock3();
                    myblock.DrawRevisonBlock();
                    mydrawingSheets.Add(myblock);

                }
                edt.WriteMessage("Sucessfully completed Title Block");
                /*Finding the maximum values for yoffset*/
                List<double> maxyvalues = new List<double>();
                maxyvalues.Add(clvalues.Max() - clvalues.Min());
                maxyvalues.Add(lbvalues.Max() - lbvalues.Min());
                maxyvalues.Add(rbvalues.Max() - rbvalues.Min());
                double maximum_y_offset = maxyvalues.Max();

                DrawingSheet ds = mydrawingSheets[1];
                Point2d draftingOrigin = ds.DrawingOriginA();
                double dw = ds.width;
                edt.WriteMessage("\n Draeing CL of Long Profile................");
                Profile myprofile = new Profile(xvalues, clvalues, 28410, 0, 1, 100,4360, dw);
                string[] mylabels = { "Existing Bed Level", "Outfall" };
                double[] mylocations = { 1.75, 0 };
                myprofile.setLabelParameter(mylabels, mylocations);
                edt.WriteMessage("\n Sucessfully Set Drawing Labels................");
                myprofile.fixProfileColor(157);
                myprofile.setLineWeight(12);
                myprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y-maximum_y_offset* myprofile.yscale_factor);

                edt.WriteMessage("\n LB of Long Profile................");
                Profile lbprofile = new Profile(xvalues, lbvalues, 28410, 0, 1, 100,4260,dw);
                string [] mylabels2 = { "Existing Left Bank" };
                double [] mylocations2 = { 8 };
                lbprofile.setLabelParameter(mylabels2, mylocations2);
                lbprofile.fixProfileColor(211);
                lbprofile.setLineWeight(8);
                lbprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor);
                edt.WriteMessage("\n RB of Long Profile................");
                Profile rbprofile = new Profile(xvalues, rbvalues, 28410, 0, 1, 100,4360,dw);
                string[] mylabels3 = { "Existing Right Bank" };
                double[] mylocations3 = { 2.5 };
                rbprofile.setLabelParameter(mylabels3, mylocations3);
                rbprofile.fixProfileColor(107);
                rbprofile.setLineWeight(8);
                rbprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * rbprofile.yscale_factor);
                edt.WriteMessage("\n DL of Long Profile................");
                Profile dlprofile = new Profile(xvalues, dlvalues, 28410, 0, 1, 100,4260,dw);
                string[] mylabels4 = { "Design Bed Level" };
                double[] mylocations4 = {1.33 };
                dlprofile.setLabelParameter(mylabels4, mylocations4);
                dlprofile.fixProfileColor(155);
                dlprofile.setLineWeight(8);
                dlprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor);
                //mykhal.DrawProfile(xvalues, clvalues,   28410, 0, 1, 100, 200, 200);
                


                //darwing the vertical scale
                Point2d vscale_location = new Point2d(draftingOrigin.X - dw * 0.03, draftingOrigin.Y);               
                double[] max_min_yavalue = mysettings.maxmin_y(lbvalues, rbvalues, clvalues, dlvalues);
                edt.WriteMessage("\n minimum=" + max_min_yavalue[0].ToString());
                edt.WriteMessage("\n maximum=" + max_min_yavalue[1].ToString());
                Vscale myver_scale = new Vscale(28410, 0,1,100,dw,28350, max_min_yavalue[1],max_min_yavalue[0]);
                myver_scale.drawVscale(draftingOrigin.X, draftingOrigin.Y- maximum_y_offset * dlprofile.yscale_factor);
                /*Drawing Table*/
                ProfileTable mytable = new ProfileTable(xvalues, lbvalues, rbvalues, clvalues, dlvalues, dw);
                mytable.drawTable(vscale_location.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor);


                // myver_scale.drawVscale(draftingOrigin.Y - maximum_y_offset * myprofile.yscale_factor);
                //new Vscale(10, -1, vscale_location, dlprofile.yscale_factor);
                // myver_scale.drawVscale();
            }
            catch(System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.Message);
                edt.WriteMessage(ex.ToString());
            }
            
           
           
           
            
           
           
           
            
           
           
            
           
            
            
            //myblock.DrawTitleBlock(210, 827);

           
        }

    }
}
