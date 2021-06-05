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
    class Profile2
    {
        public List<double> geometric_x { get; set; }
        public List<double> geometric_y { get; set; }
        public double geometic_origin_x { get; set; }
        public double geometic_origin_y { get; set; }
        public double drafting_width;
        public Profile2(List<double> xvalues, List<double> yvalues, double drafting_width)
        {
        
        
        
        }
    }
}
