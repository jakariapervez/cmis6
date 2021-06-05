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

namespace Khal_Deafting
{
   public  class Mytransformation

    {

        public Point2d Scale(Point2d p, double sx, double sy)
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
}
