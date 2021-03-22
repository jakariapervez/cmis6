using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Autodesk.AutoCAD.Windows;

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
            List<String> mylist=new List<String>();
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
    }
}
