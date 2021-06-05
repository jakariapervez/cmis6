using System;
using System.Collections.Generic;
using System.IO;
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
/*Exel File Related Import*/
using OfficeOpenXml;
namespace Khal_Deafting
{



   public class XcelInput
    {
        public ExcelPackage package { get; set; }
        public XcelInput()
        {

            OpenFileDialog ofd = new OpenFileDialog("Slect xlsx file", null,
                    "xls;xlsx", "ExcelFileToLink", OpenFileDialog.OpenFileDialogFlags.DoNotTransferRemoteFiles);
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(ofd.Filename);
                package = new ExcelPackage(fileInfo);


            }




        }

        public List<String> getHeadings()
        {
            List<String> mylist = new List<String>();
            ExcelWorksheet wsheet = this.package.Workbook.Worksheets["Headers"];
            int rows = wsheet.Dimension.Rows;
            int cols = wsheet.Dimension.Columns;
            for (int i = 2; i <= rows; i++)
            {
                string xcontent = wsheet.Cells[i, 2].Value.ToString();
                mylist.Add(xcontent);

            }
            return mylist;

        }
        public Tuple<List<String>, List<String>, List<String>, List<double>, List<double>, List<double>,List<String>> getSheetInfo(Editor edt)
        {

            List<String> title = new List<String>();
            List<string> drawing_no = new List<string>();
            List<string> darwing_date = new List<string>();
            List<double> xorigins = new List<double>();
            List<double> yorigins = new List<double>();
            List<double> sheetwidths = new List<double>();
            List<String> drawing_sheet_no = new List<String>();
            ExcelWorksheet wsheet = this.package.Workbook.Worksheets["Drawing_Sheet"];
            int rows = wsheet.Dimension.Rows;
            int cols = wsheet.Dimension.Columns;
            for (int i = 2; i <= rows; i++)
            {
                try

                {
                    string mytitle = wsheet.Cells[i, 2].Value.ToString();
                    string dno = wsheet.Cells[i, 3].Value.ToString();
                    string mydate = wsheet.Cells[i, 4].Value.ToString();

                    string xorigin = wsheet.Cells[i, 5].Value.ToString();
                    string yorigin = wsheet.Cells[i, 6].Value.ToString();
                    string sheetwidth = wsheet.Cells[i, 7].Value.ToString();
                    string drawing_sh_no= wsheet.Cells[i, 8].Value.ToString();
                    title.Add(mytitle);
                    drawing_no.Add(dno);
                    darwing_date.Add(mydate);
                    xorigins.Add(double.Parse(xorigin));
                    yorigins.Add(double.Parse(yorigin));
                    sheetwidths.Add(double.Parse(sheetwidth));
                    drawing_sheet_no.Add(drawing_sh_no);
                    /*   edt.WriteMessage("\n title=" + mytitle+" drrawing_no="+dno
                           +" date="+mydate+" xroigin="+xorigin+" yorigin="+yorigin+
                           " sheet width="+sheetwidth

                           ); */




                }

                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error Encounterd:" + ex.Message);


                }

            }

            return new Tuple<List<String>, List<String>, List<String>,
                List<double>, List<double>, List<double>,List<String>>(title, drawing_no, darwing_date, xorigins, yorigins, sheetwidths, drawing_sheet_no);

        }
        public Tuple<List<double>, List<double>, List<double>, List<double>, List<Double>> getLongSectionData(Editor edt)
        {
            List<double> xvalues = new List<double>();
            List<double> CL_RL = new List<double>();
            List<double> LB_RL = new List<double>();
            List<double> RB_RL = new List<double>();
            List<double> DL_RL = new List<double>();
            ExcelWorksheet mysheet = this.package.Workbook.Worksheets["LS"];
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

            return new Tuple<List<double>, List<double>, List<double>, List<double>, List<Double>>(xvalues, CL_RL, LB_RL, RB_RL, DL_RL);

        }
        public List<XSection> getXsectionData(Editor edt,List<DrawingSheet> drawingSheets)
        {
            List<XSection> mylist = new List<XSection>();
            DraftingSettings mysettings = new DraftingSettings();
            ExcelWorksheet wsheet = this.package.Workbook.Worksheets["Xsection"];
            int rows = wsheet.Dimension.Rows;
            int cols = wsheet.Dimension.Columns;

            for (int i = 2; i <= rows; i++)
            {
                try 
                {

                    string xcontent = wsheet.Cells[i, 2].Value.ToString();
                    var myvalues = this.getGeometricData(edt, xcontent);
                    List<double> xvalues = myvalues.Item1;
                    List<double> yvalues = myvalues.Item2;
                    edt.WriteMessage("\n section no=" + xcontent);
                    edt.WriteMessage("\n displaying xvalues ");
                    this.displayList(edt, xvalues);
                    edt.WriteMessage("\n displaying yvalues ");
                    this.displayList(edt, yvalues);
                    /*getting drswing sheets*/
                    
                    
                    /*Reding Design Parameters*/
                    edt.WriteMessage("\n reding design parameters");
                    string design_parameter = wsheet.Cells[i, 3].Value.ToString();
                    double dl_rl = double.Parse(design_parameter);
                    design_parameter = wsheet.Cells[i, 4].Value.ToString();
                    double cl_x = double.Parse(design_parameter);
                    design_parameter = wsheet.Cells[i, 5].Value.ToString();
                    double bottom_width= double.Parse(design_parameter);
                    design_parameter = wsheet.Cells[i, 6].Value.ToString();
                    double ls = double.Parse(design_parameter);
                    design_parameter = wsheet.Cells[i, 7].Value.ToString();
                    double rs = double.Parse(design_parameter);
                    edt.WriteMessage("\n design RL=" + dl_rl + " B=" + bottom_width
                       + " CL at m=" + cl_x + " left side slope 1:"
                       + ls + " right side slope 1:" + rs);
                    /*Reding Drawing Sheet and Drawing Location*/
                    string ds_index_content = wsheet.Cells[i, 8].Value.ToString();
                    int sheet_index = int.Parse(ds_index_content);
                    DrawingSheet ds = drawingSheets[sheet_index - 1];
                    double dw = ds.width;
                    string drawing_location = wsheet.Cells[i, 9].Value.ToString();                    
                    edt.WriteMessage("\n drawing sheet width=" + dw +" Drawing Location ="+drawing_location);
                    double[] result = mysettings.calcualte_drafting_paameters_xsection(xvalues, yvalues, 0.5 * dw);
                    double geometic_origin_x=xvalues.Min();//geometric orign of cross section value that will be trated as zero of xcoordinate
                    double geometic_origin_y = yvalues.Min();//geometric orign of cross section value that will be trated as zero of ycoordinate
                    double scale_x = result[3];//xscale factor for drafting;
                    double scale_y = result[4];//yscale factor for drafting
                    double xspan = xvalues.Max() - xvalues.Min();// width of drawing along xaxis
                    /*Reading Annotation Parameters*/
                    // string section_name,double sec_chainage,double app_start,double app_end,double app_start_rl,double app_end_rl
                    string annotations_param = wsheet.Cells[i,10].Value.ToString();
                    string section_name = annotations_param;
                    annotations_param = wsheet.Cells[i, 11].Value.ToString();
                    double sec_chainage = double.Parse(annotations_param);
                    annotations_param = wsheet.Cells[i, 12].Value.ToString();
                    double app_start = double.Parse(annotations_param);
                    annotations_param = wsheet.Cells[i, 13].Value.ToString();
                    double app_end = double.Parse(annotations_param);
                    annotations_param = wsheet.Cells[i, 14].Value.ToString();
                    double app_start_rl = double.Parse(annotations_param);
                    annotations_param = wsheet.Cells[i, 15].Value.ToString();
                    double app_end_rl = double.Parse(annotations_param);
                    /*
                    XSection mycross_section = new XSection(xvalues, yvalues, geometic_origin_x,
                        geometic_origin_y, scale_x, scale_y, xspan, dw, dl_rl, cl_x, bottom_width, ls, rs,
                        ds, drawing_location);
                    */
                    XSection mycross_section = new XSection(xvalues, yvalues, geometic_origin_x,
                       geometic_origin_y, scale_x, scale_y, xspan, dw, dl_rl, cl_x, bottom_width, ls, rs,ds, drawing_location,
                      section_name, sec_chainage, app_start, app_end, app_start_rl, app_end_rl);

                    mylist.Add(mycross_section);                    
                    // mycross_section.drawXsection();
                    // mycross_section.drawXsection();                 

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n "+ex.ToString());
                    edt.WriteMessage("\n"+ex.Source);
                
                }
               

            }
            return mylist;
        }
        public Tuple<List<double>, List<double>> getGeometricData(Editor edt,string xsectionno)
        {
            edt.WriteMessage("\n xsection=" + xsectionno);
            List<double> xvalues = new List<double>();
            List<double> yvalues = new List<double>();
            ExcelWorksheet wsheet = this.package.Workbook.Worksheets["Combined"];
            edt.WriteMessage("\n sheeet name=" + wsheet.Name);
            int rows = wsheet.Dimension.Rows;
            int cols = wsheet.Dimension.Columns;

            for (int i = 2; i <= rows; i++)
            {

                try

                {
                    
                    string xcontent = wsheet.Cells[i, 2].Value.ToString();
                    edt.WriteMessage("\n sid" + xcontent);
                    if (xcontent== xsectionno)
                    {
                      string distant_conten= wsheet.Cells[i, 4].Value.ToString();
                      string rl_content = wsheet.Cells[i, 5].Value.ToString();
                      xvalues.Add(double.Parse(distant_conten));
                      yvalues.Add(double.Parse(rl_content));


                    }




                }

                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error Encounterd:" + ex.Message);
                    edt.WriteMessage(ex.Source);


                }




















               
                
            }

            Tuple<List<double>, List<double>> mytuple = new Tuple<List<double>, List<double>>(xvalues,yvalues);
            return mytuple;
        }
        public void displayList(Editor edt, List<double> mylist)
        {
            string mystring = "\n [";
            foreach (double x in mylist)
            {
             mystring +=Math.Round(x,3,MidpointRounding.ToEven).ToString() + ",";            
            
            }
            mystring += "]";
            edt.WriteMessage(mystring);
        
        }

       
    }
}
