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
    class DraftingSettings
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

    }
}
