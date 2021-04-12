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
    class XSection
    {
        public Profile existing_bedlevel;
        public Profile design_bedlevel;
        public Vscale vertical_scale;
        public XSection(List<double> xvalues,
            List<double> yvalues, double xog, double yog, double sx,
            double sy, double profileLength, double draftingWidth,
            double design_level, double cl, double base_width, double leftSlope, double rightSlope)
        {
            this.existing_bedlevel = new Profile(xvalues, yvalues,
                xog, yog, sx, sy, profileLength, draftingWidth);
            Tuple<List<double>, List<double>> mydata = this.claculate_design_bed_level(design_level,
                cl,base_width,leftSlope,rightSlope,yvalues.Max());
            List<double> design_x = mydata.Item1;
            List<double> design_y = mydata.Item2;
            this.design_bedlevel = new Profile(design_x, design_y ,
                xog, yog, sx, sy, profileLength, draftingWidth);
            double vscale_xloc = xog - draftingWidth * 0.02;
            List<double> max_values = new List<double>();
            List<double> min_values = new List<double>();
            
            double y1, y2, y3, y4,ymax,ymin;
            y1 = this.existing_bedlevel.geometric_y.Max();
            y2 = this.design_bedlevel.geometric_y.Max();
            max_values.Add(y1);
            max_values.Add(y2);
            y3 = this.existing_bedlevel.geometric_y.Min();
            y4 = this.design_bedlevel.geometric_y.Min();
            min_values.Add(y3);
            min_values.Add(y4);

            ymax = Math.Ceiling(max_values.Max());
            ymin = Math.Floor(min_values.Min());
            this.vertical_scale = new Vscale(xog, yog, sx, sy,draftingWidth, xog, ymax, ymin);



        }

        private Tuple<List<double>, List<double>> claculate_design_bed_level(double design_level, double cl, double base_width, double leftSlope, double rightSlope,double ymax) 
            {
            List<double> design_x = new List<double>();
            List<double> design_y = new List<double>();
            double ytest = 1.5 * ymax;
            double x1 = cl - 0.5 * base_width - leftSlope * (ytest - design_level);
            design_x.Add(x1);
            design_y.Add(ytest);
            double x2 = cl - 0.5 * base_width;
            design_x.Add(x2);
            design_y.Add(design_level);
            double x3 = cl;
            design_x.Add(x3);
            design_y.Add(design_level);
            double x4 = cl + 0.5 * base_width;
            design_x.Add(x4);
            design_y.Add(design_level);
            double x5= cl + 0.5 * base_width + rightSlope * (ytest - design_level);
            design_x.Add(x5);
            design_y.Add(ytest);
            
            double[] leftPoint = this.findLeftInterSectionPoint(cl, base_width, design_level, 
                leftSlope, ymax);
            design_x[0] = leftPoint[0];
            design_y[0]= leftPoint[1];
            double[] rightPoint = findRightInterSectionPoint(cl, base_width, design_level,
                rightSlope, ymax);
            int n = design_x.Count - 1;
            design_x[n] = rightPoint[0];
            design_y[n]= rightPoint[1];

            return new Tuple<List<double>,List<double>>(design_x,design_y);

    }

        public double[] findLeftInterSectionPoint(double cl,double base_width,double bed_level,double leftSlope,double ymax)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            double[] result = new double[2];
            try {
               
                double x1 = cl - 0.5 * base_width;
                double y1 = bed_level;
                double x2 = x1 - (1.5 * ymax - y1) * leftSlope;
                double y2 = 1.5 * ymax;
                edt.WriteMessage("\n X1=" + x1.ToString() +
                    " Y1=" + y1.ToString() + " X2=" + x2.ToString() + " Y2=" + y2.ToString());
                int test_index = this.existing_bedlevel.geometric_x.BinarySearch(x1);
                int findex;
               
                if (test_index < 0)
                {
                    findex = (~test_index);
                }
                else
                {
                    findex = test_index + 1;
                }
                edt.WriteMessage("\n test_index=" + test_index.ToString() + " findex=" + findex.ToString());
                GeometryHelper mygeom = new GeometryHelper();
                result[0] = findex;
                result[1] = this.existing_bedlevel.geometric_x[findex];
                double x3, y3, x4, y4, x, y;
                x = double.PositiveInfinity;
                y = double.PositiveInfinity;
                int iterarion = 1;
                for (int i = findex; i >= 1; i--)
                {
                    edt.WriteMessage("\n iterarion=" + iterarion.ToString());
                    x4 = this.existing_bedlevel.geometric_x[i];
                    y4 = this.existing_bedlevel.geometric_y[i];
                    x3 = this.existing_bedlevel.geometric_x[i - 1];
                    y3 = this.existing_bedlevel.geometric_y[i - 1];
                    edt.WriteMessage("\n X3=" + x3.ToString() +
                   " Y3=" + y3.ToString() + " X4=" + x4.ToString() + " Y4=" + y4.ToString());
                    double[] result5 = mygeom.findIntersection2Lines(x1, y1, x2, y2, x3, y3, x4, y4);
                    x = result5[0];
                    y = result5[1];
                    iterarion++;
                    //edt.WriteMessage("\n i=" + i.ToString() + " x=" + x.ToString());
                    if (x <= x4 && x >= x3)
                    {

                        edt.WriteMessage("\n Found appripiate left point breaking the loop");
                        break;

                    }

                }
                result[0] = x;
                result[1] = y;
            }
            catch(SystemException ex) 
            {
                edt.WriteMessage("\n " + ex.Source);
            
            }
            
            return result;
        }
        public double[] findRightInterSectionPoint(double cl, double base_width, double bed_level, double rightSlope, double ymax)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            double[] result = new double[2];
            try
            {

                double x1 = cl + 0.5 * base_width;
                double y1 = bed_level;
                double x2 = x1 + (1.5 * ymax - y1) * rightSlope;
                double y2 = 1.5 * ymax;
                edt.WriteMessage("\n X1=" + x1.ToString() +
                    " Y1=" + y1.ToString() + " X2=" + x2.ToString() + " Y2=" + y2.ToString());
                int test_index = this.existing_bedlevel.geometric_x.BinarySearch(x1);
                int n = this.existing_bedlevel.geometric_x.Count;
                int sindex;

                if (test_index < 0)
                {
                    sindex = (~test_index)-1;
                }
                else
                {
                    sindex = test_index - 1;
                }
                edt.WriteMessage("\n test_index=" + test_index.ToString() + " sindex=" + sindex.ToString());
                GeometryHelper mygeom = new GeometryHelper();
                result[0] = sindex;
                result[1] = this.existing_bedlevel.geometric_x[sindex];
                double x3, y3, x4, y4, x, y;
                x = double.PositiveInfinity;
                y = double.PositiveInfinity;
                int iterarion = 1;
                for (int i = sindex; i<n; i++)
                {
                    edt.WriteMessage("\n iterarion=" + iterarion.ToString());
                    x4 = this.existing_bedlevel.geometric_x[i+1];
                    y4 = this.existing_bedlevel.geometric_y[i+1];
                    x3 = this.existing_bedlevel.geometric_x[i];
                    y3 = this.existing_bedlevel.geometric_y[i];
                    edt.WriteMessage("\n X3=" + x3.ToString() +
                   " Y3=" + y3.ToString() + " X4=" + x4.ToString() + " Y4=" + y4.ToString());
                    double[] result5 = mygeom.findIntersection2Lines(x1, y1, x2, y2, x3, y3, x4, y4);
                    x = result5[0];
                    y = result5[1];
                    iterarion++;
                    //edt.WriteMessage("\n i=" + i.ToString() + " x=" + x.ToString());
                    if (x <= x4 && x >= x3)
                    {

                        edt.WriteMessage("\n Found appripiate left point breaking the loop");
                        break;

                    }

                }
                result[0] = x;
                result[1] = y;
            }
            catch (SystemException ex)
            {
                edt.WriteMessage("\n " + ex.Source);

            }

            return result;
        }


    }
}
