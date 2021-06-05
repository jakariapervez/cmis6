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
using Autodesk.AutoCAD.PlottingServices;
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
        public List<DrawingSheet> prepareDrawingSheet(XcelInput inputfile)
        {

            /**/
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            List<DrawingSheet> mylist = new List<DrawingSheet>();
            var myheadings = inputfile.getHeadings();
            var sheetInfo = inputfile.getSheetInfo(edt);
            
            var sheetTitles = sheetInfo.Item1;
            var drawing_nos = sheetInfo.Item2;
            var drawing_date = sheetInfo.Item3;
            List<double> xorigins = sheetInfo.Item4;
            List<double> yorigins = sheetInfo.Item5;
            List<double> sheetWidths = sheetInfo.Item6;
            var sheet_nos = sheetInfo.Item7;
            int sheetNo = sheetWidths.Count;
           

            for (int i = 1; i < sheetNo; i++)
            {
                DrawingSheet myblock = new DrawingSheet();
                myblock.TopLeft = new Point2d(xorigins[i], yorigins[i]);
                myblock.setDimension(sheetWidths[i]);
                List<string> myargs = new List<string>();
                myheadings.Add(sheetTitles[i]);
                myheadings.Add(drawing_nos[i]);
                myheadings.Add(drawing_date[i]);
                myblock.setHeadings(myheadings);
                myargs.Add(sheetTitles[i]);
                myargs.Add(drawing_nos[i]);
                myargs.Add(drawing_date[i]);
                myargs.Add(sheet_nos[i]);
                myblock.setVaiableHeadings(myargs);
                edt.WriteMessage(myblock.drawingNo);
                edt.WriteMessage(myblock.DrawingDate);
                myblock.DrawTitleBlock3();
                myblock.DrawRevisonBlock();
                mylist.Add(myblock);

            }
            edt.WriteMessage("Sucessfully completed Title Block");
            return mylist;
        
        }
        public void drawLongSection3(XcelInput inputfile, DrawingSheet ds)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            try {

                Tuple<List<double>, List<double>,
                        List<double>, List<double>,
                        List<Double>> mydata = inputfile.getLongSectionData(edt);
                List<double> xvalues = mydata.Item1;
                List<double> clvalues = mydata.Item2;
                List<double> lbvalues = mydata.Item3;
                List<double> rbvalues = mydata.Item4;
                List<double> dlvalues = mydata.Item5;
                /*Finding the Drafting Parameters*/
                DraftingSettings dfs = new DraftingSettings();
                var result = dfs.calcualte_drafting_paameters_Lsection(xvalues,
                    lbvalues, rbvalues, clvalues, dlvalues, ds);

                double xorigin = xvalues.Min();
                double yorigin = result[1];
                double sx = result[3];
                double sy = result[4];
                double profile_width = result[5];

                List<double> maxyvalues = new List<double>();
                maxyvalues.Add(clvalues.Max() - clvalues.Min());
                maxyvalues.Add(lbvalues.Max() - lbvalues.Min());
                maxyvalues.Add(rbvalues.Max() - rbvalues.Min());
                double maximum_y_offset = maxyvalues.Max();
                Point2d draftingOrigin = ds.DrawingOriginA();
                double dw = ds.width;
                edt.WriteMessage("\n Drawing CL of Long Profile................");
               // LongSectionProfile myprofile2;
                Profile myprofile;
                myprofile = new LongSectionProfile(xvalues, clvalues, xorigin, yorigin, sx, sy, profile_width, dw);
             //  Profile myprofile = new Profile(xvalues, clvalues, 28410, 0, 1, 100, 4360, dw);
                string[] mylabels = { "Outfall", "Existing Bed Level" };
                double[] mylocations = { 0, 1.75 };
                myprofile.setLabelParameter(mylabels, mylocations);
                edt.WriteMessage("\n Sucessfully Set Drawing Labels................");
                myprofile.fixProfileColor(157);
                myprofile.setLineWeight(12);
                // myprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * myprofile.yscale_factor);
                //Labels for Distance
               // edt.WriteMessage("\n now writing distances.........................................");
              //  myprofile.drawDistanceLabel(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * myprofile.yscale_factor);
                string sLayerName = "Existing Bed Level for Long Section";
                myprofile.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * myprofile.yscale_factor, sLayerName);
               
              //  
                edt.WriteMessage("\n sucessfully drawn existing centerline.......");
                edt.WriteMessage("\n Drawing LB of Long Profile................");
                Profile lbprofile = new Profile(xvalues, lbvalues, 28410, 0, 1, 100, 4260, dw);
                string[] mylabels2 = { "Existing Left Bank" };
                double[] mylocations2 = { 8 };
                lbprofile.setLabelParameter(mylabels2, mylocations2);
                lbprofile.fixProfileColor(211);
                lbprofile.setLineWeight(8);
                // lbprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor);
                sLayerName = "Existing Left Bank of Long section"; ;
                lbprofile.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor, sLayerName);
              //  lbprofile.drawProfilewithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor,sLayerName);
                /*  lbprofile.drawProfile_WithLineType(draftingOrigin.X, 
                      draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor, "ACAD_ISO04W100"); */


                edt.WriteMessage("\n Drawing RB of Long Profile................");
                Profile rbprofile = new Profile(xvalues, rbvalues, 28410, 0, 1, 100,4360,dw);
                string[] mylabels3 = { "Existing Right Bank" };
                double[] mylocations3 = { 2.5 };
                rbprofile.setLabelParameter(mylabels3, mylocations3);
                rbprofile.fixProfileColor(107);
                rbprofile.setLineWeight(8);
                sLayerName = "Existing Right Bank of Long Section";
                rbprofile.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor, sLayerName);
               // rbprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * rbprofile.yscale_factor);
                /*rbprofile.drawProfile_WithLineType(draftingOrigin.X,
                    draftingOrigin.Y - maximum_y_offset * rbprofile.yscale_factor,
                    "ACAD_ISO05W100"); */
                
                edt.WriteMessage("\n Drawing Design RL of Long Profile................");
                Profile dlprofile = new Profile(xvalues, dlvalues, 28410, 0, 1, 100,4260,dw);
                string[] mylabels4 = { "Design Bed Level" };
                double[] mylocations4 = {1.33 };
                dlprofile.setLabelParameter(mylabels4, mylocations4);
                dlprofile.fixProfileColor(155);
                dlprofile.setLineWeight(8);
                sLayerName = "Design Bed Level Long Section"; 
                dlprofile.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor,sLayerName);
               // dlprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor);

                //darwing the vertical scale
                DraftingSettings mysettings = new DraftingSettings();
                Point2d vscale_location = new Point2d(draftingOrigin.X - dw * 0.03, draftingOrigin.Y);               
                double[] max_min_yavalue = mysettings.maxmin_y(lbvalues, rbvalues, clvalues, dlvalues);
                edt.WriteMessage("\n minimum=" + max_min_yavalue[0].ToString());
                edt.WriteMessage("\n maximum=" + max_min_yavalue[1].ToString());
                Vscale myver_scale = new Vscale(28410, 0,1,100,dw,28410-dw*0.01, max_min_yavalue[1],max_min_yavalue[0]);
                myver_scale.drawVscale(draftingOrigin.X, draftingOrigin.Y- maximum_y_offset * dlprofile.yscale_factor);
                /*Drawing Table*/

                //  ProfileTable mytable = new ProfileTable(xvalues, lbvalues, rbvalues, clvalues, dlvalues, dw);
                //   sLayerName = "Long Section Data Table";
                /*
                   mytable.drawTableWithLayer(vscale_location.X, 
                       draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor-0.005*dw,sLayerName);
                */
               

            }
            catch (System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.Message);
                edt.WriteMessage("\n" + ex.Source);
                edt.WriteMessage("\n" + ex.StackTrace);
                edt.WriteMessage("\n" + ex.ToString());
            }


        }
        public void drawLongSection(XcelInput inputfile, DrawingSheet ds)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            try
            {

                Tuple<List<double>, List<double>,
                        List<double>, List<double>,
                        List<Double>> mydata = inputfile.getLongSectionData(edt);
                List<double> xvalues = mydata.Item1;
                List<double> clvalues = mydata.Item2;
                List<double> lbvalues = mydata.Item3;
                List<double> rbvalues = mydata.Item4;
                List<double> dlvalues = mydata.Item5;
                /*Finding the Drafting Parameters*/
                DraftingSettings dfs = new DraftingSettings();
                var result = dfs.calcualte_drafting_paameters_Lsection(xvalues,
                    lbvalues, rbvalues, clvalues, dlvalues, ds);

                double xorigin = xvalues.Min();
                double yorigin = result[1];
                double sx = result[3];
                double sy = result[4];
                double profile_width = result[5];

                List<double> maxyvalues = new List<double>();
                maxyvalues.Add(clvalues.Max() - clvalues.Min());
                maxyvalues.Add(lbvalues.Max() - lbvalues.Min());
                maxyvalues.Add(rbvalues.Max() - rbvalues.Min());
                double maximum_y_offset = maxyvalues.Max();
                Point2d draftingOrigin = ds.DrawingOriginA();
                double dw = ds.width;
                edt.WriteMessage("\n Drawing CL of Long Profile................");
                // LongSectionProfile myprofile2;
                Profile myprofile;
                myprofile = new LongSectionProfile(xvalues, clvalues, xorigin, yorigin, sx, sy, profile_width, dw);
                //  Profile myprofile = new Profile(xvalues, clvalues, 28410, 0, 1, 100, 4360, dw);
                string[] mylabels = { "Outfall", "Existing Bed Level" };
                double[] mylocations = { 0, 1.75 };
                myprofile.setLabelParameter(mylabels, mylocations);
                edt.WriteMessage("\n Sucessfully Set Drawing Labels................");
                myprofile.fixProfileColor(157);
                myprofile.setLineWeight(12);
                // myprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * myprofile.yscale_factor);
                //Labels for Distance
                // edt.WriteMessage("\n now writing distances.........................................");
                //  myprofile.drawDistanceLabel(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * myprofile.yscale_factor);
                string sLayerName = "Existing Bed Level for Long Section";
                myprofile.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * myprofile.yscale_factor, sLayerName);

                //  
                edt.WriteMessage("\n sucessfully drawn existing centerline.......");
                edt.WriteMessage("\n Drawing LB of Long Profile................");
                Profile lbprofile = new Profile(xvalues, lbvalues, 28410, 0, 1, 100, 4260, dw);
                string[] mylabels2 = { "Existing Left Bank" };
                double[] mylocations2 = { 8 };
                lbprofile.setLabelParameter(mylabels2, mylocations2);
                lbprofile.fixProfileColor(211);
                lbprofile.setLineWeight(8);
                // lbprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor);
                sLayerName = "Existing Left Bank of Long section"; ;
                lbprofile.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor, sLayerName);
                //  lbprofile.drawProfilewithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor,sLayerName);
                /*  lbprofile.drawProfile_WithLineType(draftingOrigin.X, 
                      draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor, "ACAD_ISO04W100"); */


                edt.WriteMessage("\n Drawing RB of Long Profile................");
                Profile rbprofile = new Profile(xvalues, rbvalues, 28410, 0, 1, 100, 4360, dw);
                string[] mylabels3 = { "Existing Right Bank" };
                double[] mylocations3 = { 2.5 };
                rbprofile.setLabelParameter(mylabels3, mylocations3);
                rbprofile.fixProfileColor(107);
                rbprofile.setLineWeight(8);
                sLayerName = "Existing Right Bank of Long Section";
                rbprofile.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor, sLayerName);
                // rbprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * rbprofile.yscale_factor);
                /*rbprofile.drawProfile_WithLineType(draftingOrigin.X,
                    draftingOrigin.Y - maximum_y_offset * rbprofile.yscale_factor,
                    "ACAD_ISO05W100"); */

                edt.WriteMessage("\n Drawing Design RL of Long Profile................");
                Profile dlprofile = new Profile(xvalues, dlvalues, 28410, 0, 1, 100, 4260, dw);
                string[] mylabels4 = { "Design Bed Level" };
                double[] mylocations4 = { 1.33 };
                dlprofile.setLabelParameter(mylabels4, mylocations4);
                dlprofile.fixProfileColor(155);
                dlprofile.setLineWeight(8);
                sLayerName = "Design Bed Level Long Section";
                dlprofile.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor, sLayerName);
                // dlprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor);

                //darwing the vertical scale
                DraftingSettings mysettings = new DraftingSettings();
                Point2d vscale_location = new Point2d(draftingOrigin.X - dw * 0.03, draftingOrigin.Y);
                double[] max_min_yavalue = mysettings.maxmin_y(lbvalues, rbvalues, clvalues, dlvalues);
                edt.WriteMessage("\n minimum=" + max_min_yavalue[0].ToString());
                edt.WriteMessage("\n maximum=" + max_min_yavalue[1].ToString());
                Vscale myver_scale = new Vscale(28410, 0, 1, 100, dw, 28410 - dw * 0.01, max_min_yavalue[1], max_min_yavalue[0]);
                myver_scale.drawVscale(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor);
                /*Drawing Table*/

                //  ProfileTable mytable = new ProfileTable(xvalues, lbvalues, rbvalues, clvalues, dlvalues, dw);
                //   sLayerName = "Long Section Data Table";
                /*
                   mytable.drawTableWithLayer(vscale_location.X, 
                       draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor-0.005*dw,sLayerName);
                */


            }
            catch (System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.Message);
                edt.WriteMessage("\n" + ex.Source);
                edt.WriteMessage("\n" + ex.StackTrace);
                edt.WriteMessage("\n" + ex.ToString());
            }


        }
        public void displayLongsectionDraftingParameter(Editor edt, double[] result)
        {
            edt.WriteMessage("\n ymax=" + result[0] + 
                "ymin=" + result[1]+"yrange="+result[2]);
            edt.WriteMessage("\n sx=" + result[3] + "sy=" + result[4]+"profile_width="+result[5]);
        }
        public void drawLongSection2(XcelInput inputfile, DrawingSheet ds)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            try
            {

                Tuple<List<double>, List<double>,
                        List<double>, List<double>,
                        List<Double>> mydata = inputfile.getLongSectionData(edt);
                List<double> xvalues = mydata.Item1;
                List<double> clvalues = mydata.Item2;
                List<double> lbvalues = mydata.Item3;
                List<double> rbvalues = mydata.Item4;
                List<double> dlvalues = mydata.Item5;
                /*Finding the Drafting Parameters*/
                DraftingSettings dfs = new DraftingSettings();
                var result = dfs.calcualte_drafting_paameters_Lsection(xvalues,
                    lbvalues, rbvalues, clvalues, dlvalues, ds);

                double xorigin = xvalues.Min();
                double yorigin = result[1];
                double ymax = result[0];
                double sx = result[3];
                double sy = result[4];
                double profile_width = result[5];               
                double maximum_y_offset =result[2];
                
                edt.WriteMessage("\n ymax=" + result[0] +
                    "ymin=" + result[1] + "yrange=" + result[2]);
                edt.WriteMessage("\n sx=" + result[3] + "sy=" + result[4] + "profile_width=" + result[5]);
               // this.displayLongsectionDraftingParameter(edt, result);
                Point2d draftingOrigin = ds.DrawingOriginA();
                /*detrimining drafting origin coordinates*/
                double xod = draftingOrigin.X;
                double yod = draftingOrigin.Y - maximum_y_offset * sy;
                double dw = ds.width;
               
                LongSectionProfile myprofile;

                // drawing existing bed level..............
                edt.WriteMessage("\n Drawing CL of Long Profile................");               
                myprofile = new LongSectionProfile(xvalues, clvalues, xorigin, yorigin, sx, sy, profile_width, dw);
                //  Profile myprofile = new Profile(xvalues, clvalues, 28410, 0, 1, 100, 4360, dw);
                string[] mylabels = { "Existing Bed Level", "Outfall" };
                double[] mylocations = { 4.5, 0 };
                myprofile.setLabelParameter(mylabels, mylocations);
                edt.WriteMessage("\n Sucessfully Set Drawing Labels................");
                myprofile.fixProfileColor(157);
                myprofile.setLineWeight(12);                
                string sLayerName = "Existing Bed Level for Long Section";
                myprofile.drawProfileWithLayer(xod,yod,sLayerName);                
                edt.WriteMessage("\n sucessfully drawn existing centerline.......");



                edt.WriteMessage("\n Drawing LB of Long Profile................");
                LongSectionProfile lbprofile = new LongSectionProfile(xvalues, lbvalues, xorigin, yorigin, sx, sy, profile_width, dw); ;
                string[] mylabels2 = { "Existing Left Bank" };
                double[] mylocations2 = { 8 };
                lbprofile.setLabelParameter(mylabels2, mylocations2);
                lbprofile.fixProfileColor(211);
                lbprofile.setLineWeight(8);
                // lbprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor);
                sLayerName = "Existing Left Bank of Long section"; ;
                lbprofile.drawProfileWithLayer(xod,yod, sLayerName);
                //  lbprofile.drawProfilewithLayer(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor,sLayerName);
                /*  lbprofile.drawProfile_WithLineType(draftingOrigin.X, 
                      draftingOrigin.Y - maximum_y_offset * lbprofile.yscale_factor, "ACAD_ISO04W100"); */


                edt.WriteMessage("\n Drawing RB of Long Profile................");
                LongSectionProfile rbprofile = new LongSectionProfile(xvalues, rbvalues, xorigin, yorigin, sx, sy, profile_width, dw);
                string[] mylabels3 = { "Existing Right Bank" };
                double[] mylocations3 = { 2.5 };
                rbprofile.setLabelParameter(mylabels3, mylocations3);
                rbprofile.fixProfileColor(107);
                rbprofile.setLineWeight(8);
                sLayerName = "Existing Right Bank of Long Section";
                rbprofile.drawProfileWithLayer(xod, yod, sLayerName);
                // rbprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * rbprofile.yscale_factor);
                /*rbprofile.drawProfile_WithLineType(draftingOrigin.X,
                    draftingOrigin.Y - maximum_y_offset * rbprofile.yscale_factor,
                    "ACAD_ISO05W100"); */

                edt.WriteMessage("\n Drawing Design RL of Long Profile................");
                LongSectionProfile dlprofile = new LongSectionProfile(xvalues, dlvalues, xorigin, yorigin, sx, sy, profile_width, dw); 
                string[] mylabels4 = { "Design Bed Level" };
                double[] mylocations4 = { 1.33 };
                dlprofile.setLabelParameter(mylabels4, mylocations4);
                dlprofile.fixProfileColor(155);
                dlprofile.setLineWeight(8);
                sLayerName = "Design Bed Level Long Section";
                dlprofile.drawProfileWithLayer(xod, yod, sLayerName);
                // dlprofile.drawProfile(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor);






                //darwing the vertical scale
                DraftingSettings mysettings = new DraftingSettings();
                Point2d vscale_location = new Point2d(draftingOrigin.X - dw * 0.03,
                    draftingOrigin.Y);//vscale location in drafting  coordinates  
                Mytransformation trs = new Mytransformation();
                Point2d vscale_location_geomatric = trs.Drafting2GeometricCoordinate(vscale_location,
                    new Point2d(xorigin, yorigin), new Point2d(xod, yod), sx, sy);
                edt.WriteMessage("\n d_x=" + vscale_location.X + "d_y="
                    + vscale_location.X+"g_x"+ vscale_location_geomatric.X+"g_y"+ vscale_location_geomatric.Y);
                double[] max_min_yavalue = mysettings.maxmin_y(lbvalues, rbvalues, clvalues, dlvalues);
                edt.WriteMessage("\n minimum=" + max_min_yavalue[0].ToString());
                edt.WriteMessage("\n maximum=" + max_min_yavalue[1].ToString());
                // xorigin, yorigin, sx, sy, profile_width, dw
                double vx_location = draftingOrigin.X - 0.02 * dw;
                Vscale myver_scale = new Vscale(xorigin, yorigin, sx, sy, dw, vscale_location_geomatric.X, ymax, yorigin);
                myver_scale.drawVscale(draftingOrigin.X, draftingOrigin.Y - maximum_y_offset * dlprofile.yscale_factor);
                //drawing data label.....................
                // first draw distance
                List<double> horizontal_loc = new List<double>();
                //extract topleft of drawing sheet...........
                Point2d top_left = ds.TopLeft;
                double ylocation_label =yorigin+( - 0.175 * dw/sy);  
                //writing distance label
                myprofile.drawDistanceLabel(xod, yod, ylocation_label);
                //writing left bank RL
                ylocation_label = yorigin + (-0.14 * dw / sy);
                horizontal_loc.Add(0.14*dw);
                lbprofile.drawRLlabel(xod, yod, ylocation_label);
                //writing right bank RL
                ylocation_label = yorigin + (-0.11 * dw / sy);
                horizontal_loc.Add(0.11 * dw);
                rbprofile.drawRLlabel(xod, yod, ylocation_label);
                //writing center line existing bank RL
                ylocation_label = yorigin + (-0.08 * dw / sy);
                horizontal_loc.Add(0.08 * dw);                
                myprofile.drawRLlabel(xod, yod, ylocation_label);
               // writing design label
               ylocation_label = yorigin + (-0.05 * dw / sy);
                horizontal_loc.Add(0.05 * dw);
                horizontal_loc.Add(0.025 * dw);
                dlprofile.drawRLlabel(xod, yod, ylocation_label);
               myprofile.drawBorderForDataLabel(xod,yod,dw, horizontal_loc);
            }
            catch (System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.Message);
                edt.WriteMessage("\n" + ex.Source);
                edt.WriteMessage("\n" + ex.StackTrace);
                edt.WriteMessage("\n" + ex.ToString());
            }


        }

        public void drawXsections(XcelInput inputfile, List<DrawingSheet> drawingSheets)
        {


            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            List<XSection> mysectios= inputfile.getXsectionData(edt, drawingSheets);
            foreach (XSection xs in mysectios)
            {
                xs.drawXsection();
            
            
            }


        }
        public void prepareDrawingSheetForPrinting(List<DrawingSheet> mysheets)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            int n = mysheets.Count;
            int i = 1;
            string sheetName;
            
            try 
            {
                foreach (DrawingSheet ds in mysheets)
                {

                  PlotSetup myplotSetup = new PlotSetup();
                  sheetName = "SHEET-" + i;
                  myplotSetup.createLayout2(sheetName, ds);
                  edt.WriteMessage("\n sheet name=" + sheetName);
                  i++;
                }


            }
            catch (System.Exception ex)
                {
                edt.WriteMessage("\n" + ex.Message);
            
                }
           

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
                /*Setting up drawing environments.........................*/
                DrawingEnvironment myenvironemt = new DrawingEnvironment();
                myenvironemt.createLayers();
                XcelInput myinput = new XcelInput();
                List<DrawingSheet> mydrawingSheets = this.prepareDrawingSheet(myinput);
          //  this.drawLongSection(myinput, mydrawingSheets[2]);
             // this.drawLongSection2(myinput, mydrawingSheets[2]);

                this.drawXsections(myinput, mydrawingSheets);
                /*Testing Long Section*/
                Tuple<List<double>, List<double>,
                         List<double>, List<double>,
                         List<Double>> mydata = myinput.getLongSectionData(edt);
                List<double> xvalues = mydata.Item1;
                List<double> clvalues = mydata.Item2;
                List<double> lbvalues = mydata.Item3;
                List<double> rbvalues = mydata.Item4;
                List<double> dlvalues = mydata.Item5;
                LSection mylong_section = new LSection(xvalues, clvalues, 
                    dlvalues, lbvalues, rbvalues, mydrawingSheets[0]);
                mylong_section.drawLongsection();

                // LongSectionProfile myprofile2 = new LongSectionProfile(xvalues, clvalues, xmin, ymin, 1, 100, 4360, dw);
                // Point2d origin_A = mydrawingSheets[2].DrawingOriginA();
                // myprofile2.drawDistanceLabel(origin_A.X,origin_A.Y);





                Commands mycoomand = new Commands();
                PlotSetup myplotSetup = new PlotSetup();
              //  this.prepareDrawingSheetForPrinting(mydrawingSheets);
               //string sheetName = "SHEET-" + 1;
               // myplotSetup.createLayout2(sheetName, mydrawingSheets[3]);
               // sheetName = "SHEET-" + 2;
               // myplotSetup.createLayout2(sheetName, mydrawingSheets[2]);
               // this.prepareDrawingSheetForPrinting(mydrawingSheets);
               // mycoomand.CreateLayout();
               // mycoomand.CreateLayout("S-3", mydrawingSheets[3]);
               //  myinput.getXsectionData(edt,mydrawingSheets);




                /*Drawing Xsection
                 
                ##################################################################################
                ##################################################################################
                 */
                /*
                Point2d draftingOrigin;
                DrawingSheet ds = mydrawingSheets[1];
                double dw = ds.width;
                
                List<double> sec100_x = new List<double> { 0,8,10,13,15,17.3,20,22,23,26};
                List<double> sec100_y = new List<double> {3, 3.29, 4.18, 5.2, 5.25, 5.12, 4.25, 3.52, 3.28, 2.89 };
                ds= mydrawingSheets[2];
                dw = ds.width;
                double dl_rl = 1.5;
                double cl_x = 15;
                double bottom_width = 6.0;
                double ls = 1.5;
                double rs = 1.5;
                // Point2d pivot_point = ds.DrawingOriginA();
                Point2d pivot_point = ds.DrawingOriginC();
                double[] result = mysettings.calcualte_drafting_paameters_xsection(sec100_x,sec100_y,0.5*dw);
                //draftingOrigin = new Point2d(pivot_point.X, pivot_point.Y - result[2] * 10);
                draftingOrigin = ds.DrawingOriginC();
                XSection myxsection = new XSection(sec100_x, sec100_y, sec100_x.Min(), result[1], result[3],result[4], result[5], dw, dl_rl, cl_x, bottom_width,ls,rs);
                edt.WriteMessage("\n sx=" + result[3]);
                edt.WriteMessage("\n sy=" + result[4]);
                string [] mylabels5 = { "LB", "CL","RB" };
                double [] mylocations5 = {0, 2, 1 };
                //drawing existing bed_level
               // myxsection.existing_bedlevel.setLabelParameter(mylabels5, mylocations5);
                myxsection.existing_bedlevel.fixProfileColor(255);
                myxsection.existing_bedlevel.setLineWeight(8);
                myxsection.existing_bedlevel.drawProfile(draftingOrigin.X, draftingOrigin.Y- result[2]* result[4]);
                //drawing design bed_level

               // myxsection.design_bedlevel.setLabelParameter(mylabels5, mylocations5);
                myxsection.design_bedlevel.fixProfileColor(255);
                myxsection.design_bedlevel.setLineWeight(8);
                myxsection.design_bedlevel.drawProfile(draftingOrigin.X, draftingOrigin.Y - result[2] * result[4]);
                myxsection.vertical_scale.drawVscale(draftingOrigin.X, draftingOrigin.Y - result[2] * result[4]);
                //drawing excavation boundary
                myxsection.drawExcavation(draftingOrigin.X, draftingOrigin.Y - result[2] * result[4]);
                //drawing data Table
                myxsection.data_table.drawTable(draftingOrigin.X, draftingOrigin.Y - (result[2]+1) * result[4]);
                //testing stright line equations
                GeometryHelper mygeom = new GeometryHelper();
                double[] myresult = mygeom.findIntersection2Lines(12,1.5, 2.4375, 7.875, 8, 3.39, 10, 4.18);
                edt.WriteMessage("\n inttersection point x=" + myresult[0].ToString() + "y=" + myresult[1].ToString());
                //double[] myresult5 = myxsection.findLeftInterSectionPoint(cl_x, bottom_width, dl_rl, ls, 5.25);
                //edt.WriteMessage("\n index=" + myresult5[0].ToString() + "x=" + myresult5[1].ToString());
                edt.WriteMessage("\n testing finding left slope point");
                double[] myresult5= myxsection.findLeftInterSectionPoint(cl_x, bottom_width, dl_rl, ls, 5.25);
                edt.WriteMessage("\n left intersection point  x=" + myresult5[0].ToString() + " y=" + myresult5[1].ToString());
                //testing drawing from xsection class
                //Drawing First Xsection in drawing sheet;
                 ds = mydrawingSheets[3];
                string drawing_location = "A";
                myxsection = new XSection(sec100_x, sec100_y, sec100_x.Min(), result[1], result[3], result[4], result[5], dw, dl_rl, cl_x, bottom_width, ls, rs,ds, drawing_location);
                edt.WriteMessage("\n Drawing from Xsection Class............................... ");
              //  myxsection.drawXsection();
                //Drawing Second Xsection in drawing sheet;
                drawing_location = "B";
                myxsection = new XSection(sec100_x, sec100_y, sec100_x.Min(), result[1], result[3], result[4], result[5], dw, dl_rl, cl_x, bottom_width, ls, rs, ds, drawing_location);
                edt.WriteMessage("\n Drawing from Xsection Class............................... ");
               // myxsection.drawXsection();
                //Drawing Third Xsection in drawing sheet;
                drawing_location = "C";
                myxsection = new XSection(sec100_x, sec100_y, sec100_x.Min(), result[1], result[3], result[4], result[5], dw, dl_rl, cl_x, bottom_width, ls, rs, ds, drawing_location);
                edt.WriteMessage("\n Drawing from Xsection Class............................... ");
               // myxsection.drawXsection();
                */

            }
            catch (System.Exception ex)
            {
                edt.WriteMessage("\n" + ex.Message);
                edt.WriteMessage(ex.ToString());
                edt.WriteMessage("\n" + ex.ToString());
            }
            
           
           
           
            
           
           
           
            
           
           
            
           
            
            
            //myblock.DrawTitleBlock(210, 827);

           
        }
        [CommandMethod("ChangePlotSetting")]
        public static void ChangePlotSetting()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {

                //refernce the layout Manager
                LayoutManager acLayoutMgr;
                acLayoutMgr = LayoutManager.Current;
                //get the current layout and output its name in command line
                Layout acLayout;
                acLayout = trans.GetObject(acLayoutMgr.GetLayoutId(acLayoutMgr.CurrentLayout), OpenMode.ForRead) as Layout;
                edt.WriteMessage("\n Current Device Name:"+acLayout.PlotConfigurationName);
                trans.Commit();
            }
        
        
        
        }
        [CommandMethod("ToggleSpace")]
        public static void ToggleSpace()

        {

            // Get the current document

            Document acDoc = Application.DocumentManager.MdiActiveDocument;



            // Get the current values of CVPORT and TILEMODE

            object oCvports = Application.GetSystemVariable("CVPORT");

            object oTilemode = Application.GetSystemVariable("TILEMODE");



            // Check to see if the Model layout is active, TILEMODE is 1 when

            // the Model layout is active

            if (System.Convert.ToInt16(oTilemode) == 0)

            {

                // Check to see if Model space is active in a viewport,

                // CVPORT is 2 if Model space is active 

                if (System.Convert.ToInt16(oCvports) == 2)

                {

                    acDoc.Editor.SwitchToPaperSpace();

                }

                else

                {

                    acDoc.Editor.SwitchToModelSpace();

                }

            }

            else

            {

                // Switch to the previous Paper space layout

                Application.SetSystemVariable("TILEMODE", 0);

            }

        }
    }
}
