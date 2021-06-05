using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;


namespace Khal_Deafting
{



    class XcelInput
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
        public Tuple<List<String>, List<String>, List<String>, List<double>, List<double>, List<double>> getSheetInfo(Editor edt)
        {

            List<String> title = new List<String>();
            List<string> drawing_no = new List<string>();
            List<string> darwing_date = new List<string>();
            List<double> xorigins = new List<double>();
            List<double> yorigins = new List<double>();
            List<double> sheetwidths = new List<double>();
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
                    title.Add(mytitle);
                    drawing_no.Add(dno);
                    darwing_date.Add(mydate);
                    xorigins.Add(double.Parse(xorigin));
                    yorigins.Add(double.Parse(yorigin));
                    sheetwidths.Add(double.Parse(sheetwidth));
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
                List<double>, List<double>, List<double>>(title, drawing_no, darwing_date, xorigins, yorigins, sheetwidths);

        }
    }
}
