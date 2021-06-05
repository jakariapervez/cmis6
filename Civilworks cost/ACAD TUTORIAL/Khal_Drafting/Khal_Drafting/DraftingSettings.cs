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
  public class DraftingSettings
    {
        public double[] maxmin_y(List<double> lbank, List<double> rbank, List<double> cl, List<double> design_level)
        {
            double[] myresult = new double[2];
            List<double> maxyvalues = new List<double>();
            maxyvalues.Add(design_level.Max());
            maxyvalues.Add(cl.Max());
            maxyvalues.Add(lbank.Max());
            maxyvalues.Add(rbank.Max());
            double maximum_y_offset = maxyvalues.Max();
            List<double> minyvalues = new List<double>();
            minyvalues.Add(design_level.Min());
            minyvalues.Add(cl.Min());
            minyvalues.Add(rbank.Min());
            minyvalues.Add(lbank.Min());
            myresult[0] = minyvalues.Min();
            myresult[1] = maxyvalues.Max();
            return myresult;
        }
        public double[] maximum_yoffset(List<double>yvalues)
        {
         double []   myresult = new double[3];
            myresult[0] = Math.Ceiling(yvalues.Max());
            myresult[1] = Math.Floor(yvalues.Min());
            myresult[2] = myresult[0] - myresult[1];

            return myresult;
        
        }
        public double[] calcualte_drafting_paameters_xsection(List<double> xvalues,List<double> yvalues,double sheet_width)
        {
            double[] myresult = new double[6];
            double drafting_width= 0.4 * sheet_width;
            double drafting_height = (0.2 * sheet_width)/Math.Sqrt(2);
            double profile_width = xvalues.Max() - xvalues.Min();
            double sx = Math.Floor(drafting_width / profile_width);
            myresult[0] = Math.Ceiling(yvalues.Max());//maximum yvalues
            myresult[1] = Math.Floor(yvalues.Min()); //mimimum yvalues
            myresult[2] = myresult[0] - myresult[1];//yrange
            double sy =Math.Floor (drafting_height / myresult[2]);
            myresult[3] = sx;
            myresult[4] = sy;
            myresult[5] = drafting_width;
            return myresult;
        }
        public double[] calcualte_drafting_paameters_Lsection(List<double> xvalues,List<double> lbank, List<double> rbank, List<double> cl, List<double> design_level,DrawingSheet ds)
        {
            double[] myresult = new double[6];
            double sheet_width = ds.width;
            double drafting_width = 0.85 * sheet_width;
            double drafting_height = (0.13 * sheet_width);
            double profile_width = xvalues.Max() - xvalues.Min();
            double ratio = drafting_width / profile_width;
            double sx;
            if (ratio <= 1)
            { sx = 1; }
            else
            {
                sx = Math.Floor(drafting_width / profile_width);
            }


            
            double[] maxmin_yvalues = maxmin_y(lbank, rbank, cl, design_level);
            
            myresult[0] = Math.Ceiling(maxmin_yvalues.Max());//maximum yvalues
            myresult[1] = Math.Floor(maxmin_yvalues.Min()); //mimimum yvalues
            myresult[2] = myresult[0] - myresult[1];//yrange
            double sy = Math.Floor(drafting_height / myresult[2]);
            myresult[3] = sx;
            myresult[4] = sy;
            myresult[5] = profile_width;
            return myresult;
        }
    }
}
