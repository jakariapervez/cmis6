using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khal_Deafting
{
    class GeometryHelper
    {

        public double[] findSLEquationFrom2coordinats(double x1, double y1, double x2, double y2)
        {
            double[] coef = new double[3];
            double a = -(y2 - y1);
            double b = (x2 - x1);
            double c = -a * x1 - b * y1;
            coef[0] = a;
            coef[1] = b;
            coef[2] = c;
            return coef;


        }
        public double[] findIntersection2Lines(double x1, double y1, double x2, double y2, 
            double x3, double y3, double x4, double y4)
        {
            double[] result = new double[2];
            double x,y;
            double[] line1 = this.findSLEquationFrom2coordinats(x1, y1, x2, y2);
            double[] line2 = this.findSLEquationFrom2coordinats(x3, y3, x4, y4);
            double a1 = line1[0];
            double b1 = line1[1];
            double c1 = line1[2];
            double a2 = line2[0];
            double b2 = line2[1];
            double c2 = line2[2];
            double determinant = a1 * b2 - a2 * b1;
            if (determinant != 0)
            {
                x = (b1 * c2 - b2 * c1) / determinant;
                y = (c1 * a2 - c2 * a1) / determinant;

            }
            else
            {
                x = double.PositiveInfinity;
                y = double.PositiveInfinity;
            
            
            }
            result[0] = x;
            result[1] = y;
            return result;
        
        }

    }
 
}
