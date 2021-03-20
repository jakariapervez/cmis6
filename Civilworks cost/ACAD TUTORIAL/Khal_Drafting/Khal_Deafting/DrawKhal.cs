﻿using System;
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
    public class Mytransformation
    {

        public Point2d Scale(Point2d p,double sx,double sy)
        {
            Point2d p2 = new Point2d(p.X * sx, p.Y * sy);
            return p2;
        
        }
        public Point2d Translate(Point2d p, double dx, double dy)
        {
            Point2d p2 = new Point2d(p.X + dx, p.Y + dy);
            return p2;

        }





    }
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
        [CommandMethod ("DRAW_KHAL")]
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

        }
    
    
    }
}
