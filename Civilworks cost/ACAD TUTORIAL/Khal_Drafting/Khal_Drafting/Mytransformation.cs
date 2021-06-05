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
       public Point2d Drafting2GeometricCoordinate(Point2d p,Point2d geometric_origin,
           Point2d drafting_origin,double sx,double sy)
        {

            double xnew = (p.X - drafting_origin.X) / sx + geometric_origin.X;
            double ynew = (p.Y - drafting_origin.Y) / sy + geometric_origin.Y;
            return new Point2d(xnew, ynew);
        }
        public Point2d Geometric2DraftingCoordinate(Point2d p, Point2d geometric_origin,
            Point2d drafting_origin, double sx, double sy)
        {
            double xnew = (p.X - geometric_origin.X) *sx + drafting_origin.X;
            double ynew = (p.Y - geometric_origin.Y) * sy + drafting_origin.Y;
            return new Point2d(xnew, ynew);


        }

    }
}
