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
   public class XSection
    {
        public Profile existing_bedlevel;
        public Profile design_bedlevel;
        public Profile excavation_boundary;
        public Vscale vertical_scale;
        public double ymax; // required for drawing space calcualtion 
        public double ymin; // required for drawings space calculation
        public double left_intersection_x;
        public double right_intersection_x;
        public double yscale_factor;
        public double xscale_factor;
        
        public CrossSectionDataTable data_table;
        public DrawingSheet dsheet;
        public string drawing_location;
        public Point2d drafting_origin;
        public double draftingwidth;
        public List<Point2d> leaderlocation;
        public List<String> leaderText;
        //public List<Point2d> crticalPoints;
        public Point2d x;
        public double excavation_area;
        //section applicabilty parameter
        public string sectionname;
        public double starting_elevation;
        public double end_elevation;
        public double starting_application;
        public double end_application;
        public double chaiange;
        //design related parameter
        public double leftslope;
        public double rightslope;
        public double bed_width;
        public double bottom_rl;
        public XSection(List<double> xvalues,
            List<double> yvalues, double xog, double yog, double sx,
            double sy, double profileLength, double draftingWidth,
            double design_level, double cl, double base_width, double leftSlope, double rightSlope)
        {

            this.xscale_factor = sx;
            this.existing_bedlevel = new Profile(xvalues, yvalues,
                xog, yog, sx, sy, profileLength, draftingWidth);
            Tuple<List<double>, List<double>, int,int> mydata = this.claculate_design_bed_level(design_level,
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
            
            //setinng hatch boundary
            List<double> hatch_bounday_x = new List<double>();
            List<double> hatch_bounday_y = new List<double>();
            hatch_bounday_x.Add(this.design_bedlevel.geometric_x[0]);
            hatch_bounday_y.Add(this.design_bedlevel.geometric_y[0]);
            //populating existing bed level in hatch boundary
            int sindex = mydata.Item3;
            int findex = mydata.Item4;
            for (int i = sindex; i <= findex; i++)
            {
                hatch_bounday_x.Add(this.existing_bedlevel.geometric_x[i]);
                hatch_bounday_y.Add(this.existing_bedlevel.geometric_y[i]);

            }
            //populating  design level in hatch boundary
            int n = this.design_bedlevel.geometric_x.Count - 1;
            for (int j = n; j >= 0; j--)
            {
                hatch_bounday_x.Add(this.design_bedlevel.geometric_x[j]);
                hatch_bounday_y.Add(this.design_bedlevel.geometric_y[j]);

            }
            this.excavation_boundary = new Profile(hatch_bounday_x, hatch_bounday_y,
                    xog, yog, sx, sy, profileLength, draftingWidth);
            //initializing cross section data table
            this.data_table = new CrossSectionDataTable(xvalues, yvalues, this.design_bedlevel.geometric_y, draftingWidth*0.5);

        }
        public XSection(List<double> xvalues,
        List<double> yvalues, double xog, double yog, double sx,
        double sy, double profileLength, double draftingWidth,
        double design_level, double cl, double base_width, double leftSlope, double rightSlope,
        DrawingSheet ds,string drawing_location)
        {
            this.xscale_factor = sx;
            this.existing_bedlevel = new Profile(xvalues, yvalues,
                xog, yog, sx, sy, profileLength, draftingWidth);
            
            Tuple<List<double>, List<double>, int, int> mydata = this.claculate_design_bed_level(design_level,
                cl, base_width, leftSlope, rightSlope, yvalues.Max());
            List<double> design_x = mydata.Item1;
            List<double> design_y = mydata.Item2;
            this.design_bedlevel = new Profile(design_x, design_y,
                xog, yog, sx, sy, profileLength, draftingWidth);
            double vscale_xloc = xog - draftingWidth * 0.02;
            List<double> max_values = new List<double>();
            List<double> min_values = new List<double>();

            //Augmented existing values with design level
           Tuple<List<double>, List<double>> mydata2 = this.addDesignXtoExistingValues(xvalues,
                yvalues, design_x, design_y); 

           this.existing_bedlevel.geometric_x = mydata2.Item1;
          this.existing_bedlevel.geometric_y = mydata2.Item2;

            double y1, y2, y3, y4, ymax, ymin;
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
            this.ymax = ymax;
            this.ymin = ymin;
            this.yscale_factor = sy;
            this.vertical_scale = new Vscale(xog, yog, sx, sy, draftingWidth, xog, ymax, ymin);
            //setinng hatch boundary
            List<double> hatch_bounday_x = new List<double>();
            List<double> hatch_bounday_y = new List<double>();
            hatch_bounday_x.Add(this.design_bedlevel.geometric_x[0]);
            hatch_bounday_y.Add(this.design_bedlevel.geometric_y[0]);

            //populating existing bed level in hatch boundary
           // int sindex = mydata.Item3;
            //int findex = mydata.Item4;
            int sindex = this.existing_bedlevel.geometric_x.IndexOf(this.design_bedlevel.geometric_x[0]);
            int findex = this.existing_bedlevel.geometric_x.IndexOf(this.design_bedlevel.geometric_x.Last());
            for (int i = sindex-1; i <= findex-1; i++)
            {
                hatch_bounday_x.Add(this.existing_bedlevel.geometric_x[i]);
                hatch_bounday_y.Add(this.existing_bedlevel.geometric_y[i]);

            }
            //populating  design level in hatch boundary
            int n = this.design_bedlevel.geometric_x.Count - 1;
            for (int j = n; j >= 0; j--)
            {
                hatch_bounday_x.Add(this.design_bedlevel.geometric_x[j]);
                hatch_bounday_y.Add(this.design_bedlevel.geometric_y[j]);

            }
            this.excavation_boundary = new Profile(hatch_bounday_x, hatch_bounday_y,
                    xog, yog, sx, sy, profileLength, draftingWidth);





            
            this.dsheet = ds;
            if (drawing_location == "A")
            {
                this.drafting_origin = this.dsheet.DrawingOriginA();


            }
            else if (drawing_location == "B")
            {
                this.drafting_origin = this.dsheet.DrawingOriginB();

            }
            else
            {

                this.drafting_origin = this.dsheet.DrawingOriginC();

            }
            //setting design related parameter
            this.leftslope = leftSlope;
            this.rightslope = rightSlope;
            this.bed_width = base_width;
            this.bottom_rl = design_level;
            this.draftingwidth = draftingWidth;
            this.calculateExcavationArea(); //calcualting excavation area for later use
            string sec_name = "Typical section of Katakhali Khal";
            sec_name = sec_name.ToUpper();

            this.setApplicabilityParameter(1.71, 1.70, 0, 0.150, 0.075,sec_name);
            //initializing cross section data table
            List<double> design_rl_for_table = prepare_dessign_rl_fortable();
            this.data_table = new CrossSectionDataTable(xvalues, yvalues, design_rl_for_table, draftingWidth);
            
        }
        public XSection(List<double> xvalues,
      List<double> yvalues, double xog, double yog, double sx,
      double sy, double profileLength, double draftingWidth,
      double design_level, double cl, double base_width, double leftSlope, double rightSlope,
      DrawingSheet ds, string drawing_location,
      string section_name,double sec_chainage,double app_start,double app_end,double app_start_rl,double app_end_rl)
        {
            this.xscale_factor = sx;
            this.existing_bedlevel = new Profile(xvalues, yvalues,
                xog, yog, sx, sy, profileLength, draftingWidth);

            Tuple<List<double>, List<double>, int, int> mydata = this.claculate_design_bed_level(design_level,
                cl, base_width, leftSlope, rightSlope, yvalues.Max());
            List<double> design_x = mydata.Item1;
            List<double> design_y = mydata.Item2;
            this.design_bedlevel = new Profile(design_x, design_y,
                xog, yog, sx, sy, profileLength, draftingWidth);
            double vscale_xloc = xog - draftingWidth * 0.02;
            List<double> max_values = new List<double>();
            List<double> min_values = new List<double>();

            //Augmented existing values with design level
            Tuple<List<double>, List<double>> mydata2 = this.addDesignXtoExistingValues(xvalues,
                 yvalues, design_x, design_y);

            this.existing_bedlevel.geometric_x = mydata2.Item1;
            this.existing_bedlevel.geometric_y = mydata2.Item2;

            double y1, y2, y3, y4, ymax, ymin;
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
            this.ymax = ymax;
            this.ymin = ymin;
            this.yscale_factor = sy;
            this.vertical_scale = new Vscale(xog, yog, sx, sy, draftingWidth, xog, ymax, ymin);
            //setinng hatch boundary
            List<double> hatch_bounday_x = new List<double>();
            List<double> hatch_bounday_y = new List<double>();
            hatch_bounday_x.Add(this.design_bedlevel.geometric_x[0]);
            hatch_bounday_y.Add(this.design_bedlevel.geometric_y[0]);

            //populating existing bed level in hatch boundary
            // int sindex = mydata.Item3;
            //int findex = mydata.Item4;
            int sindex = this.existing_bedlevel.geometric_x.IndexOf(this.design_bedlevel.geometric_x[0]);
            int findex = this.existing_bedlevel.geometric_x.IndexOf(this.design_bedlevel.geometric_x.Last());
            for (int i = sindex - 1; i <= findex - 1; i++)
            {
                hatch_bounday_x.Add(this.existing_bedlevel.geometric_x[i]);
                hatch_bounday_y.Add(this.existing_bedlevel.geometric_y[i]);

            }
            //populating  design level in hatch boundary
            int n = this.design_bedlevel.geometric_x.Count - 1;
            for (int j = n; j >= 0; j--)
            {
                hatch_bounday_x.Add(this.design_bedlevel.geometric_x[j]);
                hatch_bounday_y.Add(this.design_bedlevel.geometric_y[j]);

            }
            this.excavation_boundary = new Profile(hatch_bounday_x, hatch_bounday_y,
                    xog, yog, sx, sy, profileLength, draftingWidth);






            this.dsheet = ds;
            this.drawing_location = drawing_location;
            if (drawing_location == "A")
            {
                this.drafting_origin = this.dsheet.DrawingOriginA();


            }
            else if (drawing_location == "B")
            {
                this.drafting_origin = this.dsheet.DrawingOriginB();

            }
            else
            {

                this.drafting_origin = this.dsheet.DrawingOriginC();

            }
            //setting design related parameter
            this.leftslope = leftSlope;
            this.rightslope = rightSlope;
            this.bed_width = base_width;
            this.bottom_rl = design_level;
            this.draftingwidth = draftingWidth;
            this.calculateExcavationArea(); //calcualting excavation area for later use
            string sec_name = "Typical section of Katakhali Khal";
            sec_name = sec_name.ToUpper();
           // string section_name,double app_start,double app_end,double app_start_rl,double app_end_rl
            this.setApplicabilityParameter(app_start_rl,app_end_rl, app_start, app_end, sec_chainage, sec_name);
            //initializing cross section data table
            List<double> design_rl_for_table = prepare_dessign_rl_fortable();

            this.data_table = new CrossSectionDataTable(xvalues, yvalues, design_rl_for_table, draftingWidth);

        }
        private Tuple<List<double>, List<double>, int,int> claculate_design_bed_level(double design_level, double cl, double base_width, double leftSlope, double rightSlope,double ymax) 
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
            int sindex= (int)leftPoint[2];
            double[] rightPoint = findRightInterSectionPoint(cl, base_width, design_level,
                rightSlope, ymax);
            int n = design_x.Count - 1;
            design_x[n] = rightPoint[0];
            design_y[n]= rightPoint[1];
            //adding midpoints to the profile
            double[] result1 = findMidPoint(design_x[0], design_y[0], design_x[1], design_y[1]);
            double[] result2 = findMidPoint(design_x[3], design_y[3], design_x[4], design_y[4]);
            design_x.Insert(1, result1[0]);
            design_y.Insert(1, result1[1]);
            design_x.Insert(5, result2[0]);
            design_y.Insert(5, result2[1]);
            int findex = (int)rightPoint[2];
            return new Tuple<List<double>,List<double>, int,int>(design_x,design_y,sindex,findex);

    }

        public List<double> prepare_dessign_rl_fortable2()
        {
            List<double> mylist = new List<double>();
            foreach (double x in this.existing_bedlevel.geometric_x)
            {
                if (this.design_bedlevel.geometric_x.Contains(x))
                {
                    int i = this.design_bedlevel.geometric_x.IndexOf(x);
                    mylist.Add(this.design_bedlevel.geometric_y[i]);
                }
                else
                {
                    mylist.Add(-9999);
                }
            
            }
            return mylist;
        
        }

        public List<double> prepare_dessign_rl_fortable()
        {
            
            List<double> mylist = new List<double>();
            /*adding left bank*/
            double x = this.design_bedlevel.geometric_x[0];
            double y = this.design_bedlevel.geometric_y[0];
            int i1 = this.existing_bedlevel.geometric_x.IndexOf(x);
            for (int i = 0; i < i1; i++)
            {
                mylist.Add(-9999);


            }
            mylist.Add(y);
            double x2= this.design_bedlevel.geometric_x[2];
            int i2= this.existing_bedlevel.geometric_x.IndexOf(x2); 
            
            for (int i = i1+1; i < i2; i++)
            {
                double value;
                value = y -( (this.existing_bedlevel.geometric_x[i] - x)/this.leftslope);
                mylist.Add(value);
            }
            double x3 = this.design_bedlevel.geometric_x[4];
            int i3 = this.existing_bedlevel.geometric_x.IndexOf(x3);
            for (int i = i2; i < i3; i++)
            {
                mylist.Add(this.bottom_rl);
            }
            double x4= this.design_bedlevel.geometric_x[6];
            double y4 = this.design_bedlevel.geometric_y[6];
            int i4 = this.existing_bedlevel.geometric_x.IndexOf(x4);
            for (int i = i3; i < i4; i++)
            {
                double value = this.bottom_rl + ((this.existing_bedlevel.geometric_x[i] -this.design_bedlevel.geometric_x[4]) / this.rightslope);
                mylist.Add(value);
            }
            mylist.Add(y4);
            int n = this.existing_bedlevel.geometric_x.Count;
            for (int i = i4 + 1; i < n; i++)
            {
                mylist.Add(-9999);
            }
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            int m = mylist.Count;
            if (m == n)
            {
                edt.WriteMessage("\n m" + m + "==" + n);

            }
            else
            {
                edt.WriteMessage("\n m" + m + "!=" + n);
            }
            return mylist;

        }
        public double[] findLeftInterSectionPoint(double cl,double base_width,double bed_level,double leftSlope,double ymax)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            double[] result = new double[3];
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
                        result[2] = i;
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
            double[] result = new double[3];
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
                        result[2] = i;
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
        public double[] findMidPoint(double x1, double y1, double x2, double y2)
        {
            double[] result = new double[2];
            result[0] = (x1 + x2) / 2;
            result[1] = (y1 + y2) / 2;
            return result;
        }
        public void drawExcavation(double xod, double yod,string sLayerName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                /*Setting Appropiate Layer for drawing excavation*/
                LayerTable acLyrTbl;
                acLyrTbl = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (acLyrTbl.Has(sLayerName))
                {
                    db.Clayer = acLyrTbl[sLayerName];


                }

                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                Polyline pl = new Polyline();
                Int32 no_of_points = this.excavation_boundary.geometric_x.Count;
                Mytransformation trs = new Mytransformation();
                for (int i = 0; i < no_of_points; i++)
                {
                    Point2d mypoint = new Point2d(this.excavation_boundary.geometric_x[i], this.excavation_boundary.geometric_y[i]);
                    Point2d tog = trs.Translate(mypoint, -this.excavation_boundary.geometric_origin_x, -this.excavation_boundary.geometric_origin_y);
                    Point2d scaledPoint = trs.Scale(tog, this.excavation_boundary.xscale_fator, this.excavation_boundary.yscale_factor);
                    Point2d draftingPoint = trs.Translate(scaledPoint, xod, yod);
                    pl.AddVertexAt(i, draftingPoint, 0, 0, 0);
                }
                pl.Closed = true;
                pl.SetDatabaseDefaults();                
                btr.AppendEntity(pl);
                trans.AddNewlyCreatedDBObject(pl, true);
                


                // Adds hatch boundaries to an object id collection
                ObjectIdCollection myobjects = new ObjectIdCollection();
                myobjects.Add(pl.ObjectId);
                Hatch myhatch = new Hatch();
                btr.AppendEntity(myhatch);
                trans.AddNewlyCreatedDBObject(myhatch, true);
                // Set the properties of the hatch object
                // Associative must be set after the hatch object is appended to the 
                // block table record and before AppendLoop
                myhatch.SetDatabaseDefaults();
                myhatch.SetHatchPattern(HatchPatternType.PreDefined, "ANSI31");                
                myhatch.PatternScale = this.excavation_boundary.yscale_factor;
                myhatch.Associative = true;
                myhatch.AppendLoop(HatchLoopTypes.Outermost, myobjects);               
                myhatch.EvaluateHatch(true);
                // Increase the pattern scale 

                myhatch.PatternScale = myhatch.PatternScale + this.excavation_boundary.yscale_factor;
                myhatch.SetHatchPattern(myhatch.PatternType, myhatch.PatternName);
                myhatch.EvaluateHatch(true);


                trans.Commit();
            }

        }
        public void calculateExcavationArea()
        {
            Polyline pl = new Polyline();
            Point2d p;
            Int32 no_of_points = this.excavation_boundary.geometric_x.Count;
            for (int i = 0; i < no_of_points; i++)
            {   
                p=new Point2d(this.excavation_boundary.geometric_x[i], this.excavation_boundary.geometric_y[i]);
                pl.AddVertexAt(i, p, 0, 0, 0);
            }
            pl.Closed = true;
            this.excavation_area = pl.Area;
            
        }
        public void setApplicabilityParameter(double st_elev,double end_elev,double start_app,
            double end_app,double sec_chaiange,string sec_name)
        {
            this.sectionname = sec_name;
            this.starting_elevation = st_elev;
            this.end_elevation = end_elev;
            this.starting_application = start_app;
            this.end_application = end_app;
            this.chaiange = sec_chaiange;
        
        }
        private Tuple<List<double>, List<double>> addDesignXtoExistingValues(List<double> xvalues,
            List<double> yvalues,List<double> design_x,List<double> design_y)
        {
            //List<double> design_x = new List<double>();
           // List<double> design_y = new List<double>();
           foreach(double x in design_x)
            {
                if (xvalues.Contains(x)== false)
                {
                    int index = this.findInsertionIndex(xvalues, x);
                    
                    double y = this.interpolateYvalue(xvalues[index], yvalues[index],
                        xvalues[index + 1], yvalues[index + 1], x);
                    xvalues.Insert(index + 1, x);
                    yvalues.Insert(index + 1, y);

                }


            }
            return new Tuple<List<double>, List<double>>(xvalues,yvalues);



        }
        int findInsertionIndex(List<double>mylist,double value)
        {
            int index = mylist.BinarySearch(value);
            int found = (~index) - 1;
            return found;
        }
        public double interpolateYvalue(double x1, double y1, double x2, double y2,double x)
        {



            double y;
            double slope = (y2 - y1) / (x2 - x1);
            double delx = (x - x1);
            y = y1 + slope * delx;
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            edt.WriteMessage("\n x1=" + x1 + "y1=" + y1 + "x2=" + x2 + "y2=" + y2+"x="+x+
                "y="+y+"slope="+slope+"delx="+delx);
            return y;
        
        }
        public void setLeaderProperties()
        {

            this.leaderlocation = new List<Point2d>();
            this.leaderText = new List<String>();
            this.leaderText.Add("Existing Section");
            this.leaderText.Add("Design Section");
            this.leaderText.Add("Earth Cutting");



        }
        public void addCriticalLocation()
        {

            



        }

        public void DrawCL()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {

                //getting dotted lline type
                LinetypeTable acLineTypTable;
                LayerTable acLyrTbl;
                acLineTypTable = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                acLyrTbl = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                string sLayerName = "CL of Xsection";
                if (acLyrTbl.Has(sLayerName) == true)
                {
                    db.Clayer = acLyrTbl[sLayerName];
                }
                string sLineTypeName = "DASHED";
                if (acLineTypTable.Has(sLineTypeName) == false)
                {
                    db.LoadLineTypeFile(sLineTypeName, "acad.lin");
                
                }
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                //creating polyline
                Polyline pl = new Polyline();
                //adding points to empty polyline
                List<Point2d> mypoints = new List<Point2d>();
                mypoints.Add(new Point2d(this.design_bedlevel.geometric_x[3], this.design_bedlevel.geometric_y[3]));
                int idx = this.existing_bedlevel.geometric_x.IndexOf(this.design_bedlevel.geometric_x[3]);
                mypoints.Add(new Point2d(this.existing_bedlevel.geometric_x[idx], this.existing_bedlevel.geometric_y[idx]));
                Mytransformation trs = new Mytransformation();
                Point2d draftingOrigin = new Point2d(this.drafting_origin.X, this.drafting_origin.Y - (this.ymax - this.ymin) * this.yscale_factor);
                int i = 0;
                foreach (Point2d p in mypoints)
                {
                    Point2d tog = trs.Translate(p, -this.existing_bedlevel.geometric_origin_x, -this.existing_bedlevel.geometric_origin_y);
                    Point2d scaledPoint = trs.Scale(tog, this.existing_bedlevel.xscale_fator, this.existing_bedlevel.yscale_factor);
                    Point2d draftingPoint = trs.Translate(scaledPoint,draftingOrigin.X, draftingOrigin.Y);
                    pl.AddVertexAt(i, draftingPoint, 0, 0, 0);
                    i++;
                }
                pl.SetDatabaseDefaults();
              //  pl.Linetype = sLineTypeName;
               // pl.LinetypeScale = 100;
               // pl.LineWeight = LineWeight.LineWeight040;
                btr.AppendEntity(pl);
                trans.AddNewlyCreatedDBObject(pl, true);
                trans.Commit();
            }
            


        }

        public Point2d geometricToDraftingCoordinates(Point2d geometric_point,Point2d geometric_origin,Point2d drafting_origin,
            double sx,double sy)
        {
            Mytransformation trs = new Mytransformation();
            /*trans lating points to geometic origin say a point has coordinate(5,5) with geometic origin at (2,1)
              result of this opertion will be  new coordinates wrt origin at (2,1) i.e (3, 4)*/
            Point2d reduced_point = trs.Translate(geometric_point, -geometric_origin.X, -geometric_origin.Y);
            /*Scaling points as per drawing scale if hscacle=5,vscale=10 new coordinates will be (15,40) */
            Point2d scaledPoint = trs.Scale(reduced_point, sx, sy);
            Point2d draftingPoint = trs.Translate(scaledPoint, drafting_origin.X, drafting_origin.Y);
            return draftingPoint;

        }

        public Label_Annotation createAnnotations(string annotation_text,Point2d text_location,
            Point2d drafting_origin,Point2d geometric_origin)
        {



            Label_Annotation myannotations = new Label_Annotation
                    (annotation_text, text_location, new Point2d(0, 0), drafting_origin, false,1, this.yscale_factor, this.draftingwidth);
            return myannotations;
        }

        public void AnnotateXsectionWithLeader()
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                /*Setting Layer for Data Table*/
                LayerTable acLyrTbl;
                string sLayerName = "Cross Section Labels"; 
                acLyrTbl = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (acLyrTbl.Has(sLayerName))
                {
                    db.Clayer = acLyrTbl[sLayerName];


                }

                trans.Commit();

            }



           // edt.WriteMessage("\n we are in albel annotations...");
            Point2d draftingOrigin = new Point2d(this.drafting_origin.X, this.drafting_origin.Y - (this.ymax - this.ymin) * this.yscale_factor);
           // edt.WriteMessage("\n drafting origin x"+ draftingOrigin.X+ " y="+ draftingOrigin.Y);
            Point2d geometric_origin = new Point2d(this.existing_bedlevel.geometric_origin_x, this.existing_bedlevel.geometric_origin_y);
            string annotation_text = "Existing Section";
            
          //  edt.WriteMessage("\n text_location x" + text_Location.X + " y=" + text_Location.Y);
          //  edt.WriteMessage("\n sx=" + this.xscale_factor + "sy=" + this.yscale_factor);
            //finding the appripitae point for existing section
            int i = this.existing_bedlevel.geometric_x.IndexOf(this.design_bedlevel.geometric_x[0]);
            Point2d text_Location = new Point2d(this.existing_bedlevel.geometric_x[i-1], this.ymax * 1.25);
            Point2d arrow_tip = new Point2d(this.existing_bedlevel.geometric_x[i-1],
                this.existing_bedlevel.geometric_y[i-1]);
            Label_Annotation_With_Leader myannotations3 = new Label_Annotation_With_Leader
                (annotation_text,text_Location,geometric_origin,draftingOrigin,false,
                this.xscale_factor,this.yscale_factor,this.draftingwidth,arrow_tip);
          //  edt.WriteMessage("\n text location of object x=" + myannotations.text_Location.X, "y=" + myannotations.text_Location.Y);
            myannotations3.drawAnnotationsWithLeader(0,4);
            /*Writing design section*/
            annotation_text = "Design Section";
            text_Location = new Point2d(this.design_bedlevel.geometric_x[5], this.ymax * 1.1);
            arrow_tip = new Point2d(this.design_bedlevel.geometric_x[5], this.design_bedlevel.geometric_y[5]);
            myannotations3 = new Label_Annotation_With_Leader
                (annotation_text, text_Location, geometric_origin, draftingOrigin, false,
                this.xscale_factor, this.yscale_factor, this.draftingwidth, arrow_tip);

            myannotations3.drawAnnotationsWithLeader(0, 1);
            /*
            Label_Annotation myannotations = new Label_Annotation
                (annotation_text, text_Location, geometric_origin, draftingOrigin, false,
                this.xscale_factor, this.yscale_factor, this.draftingwidth);
            */


            /*Writing slope*/
            annotation_text = "1:1.5";
            text_Location = new Point2d(this.design_bedlevel.geometric_x[5]-0.25, 
            this.design_bedlevel.geometric_y[5]-0.125);
            double myrotation = Math.Atan(1 / 1.5);
            myrotation = Math.Atan((1 * this.yscale_factor) / (1.5 * this.xscale_factor));
          Label_Annotation  myannotations = new Label_Annotation
               (annotation_text, text_Location, geometric_origin, draftingOrigin, false,
               this.xscale_factor, this.yscale_factor, this.draftingwidth);
            myannotations.drawAnnotation(myrotation, 2);
            /*Writing slope*/
            annotation_text = "1:1.5";
            text_Location = new Point2d(this.design_bedlevel.geometric_x[1]-0.25,
            this.design_bedlevel.geometric_y[1]-0.125); //0.25 is added as pading to seperate text and object
            myrotation = Math.Atan((-1*this.yscale_factor )/ (1.5*this.xscale_factor));
            myannotations = new Label_Annotation
               (annotation_text, text_Location, geometric_origin, draftingOrigin, false,
               this.xscale_factor, this.yscale_factor, this.draftingwidth);
            myannotations.drawAnnotation(myrotation, 2);
            /*Writing Base width*/
            annotation_text = "B="+this.bed_width.ToString("N2");
            text_Location = new Point2d(this.design_bedlevel.geometric_x[3],
            this.design_bedlevel.geometric_y[3]-0.25); //0.25 is added as pading to seperate text and object
            //myrotation = Math.Atan((-1 * this.yscale_factor) / (1.5 * this.xscale_factor));
            myrotation = 0;
            myannotations = new Label_Annotation
               (annotation_text, text_Location, geometric_origin, draftingOrigin, false,
               this.xscale_factor, this.yscale_factor, this.draftingwidth);
            myannotations.drawAnnotation(myrotation, 2);

            //writing left bank             
            double[] result = this.findMidPoint(this.existing_bedlevel.geometric_x[0],
             this.existing_bedlevel.geometric_y[0], this.existing_bedlevel.geometric_x[1],
             this.existing_bedlevel.geometric_y[1]);
            
            annotation_text = "L/B";
            text_Location = new Point2d(result[0],result[1]+0.5);
            myrotation = 0;
            double myradius = this.draftingwidth * 0.012;
            Label_Annotarion_With_Circle myannotations2 = new Label_Annotarion_With_Circle(annotation_text, text_Location, geometric_origin, draftingOrigin, false,
               this.xscale_factor, this.yscale_factor, this.draftingwidth, myradius);
            myannotations2.drawAnnotationsWithCircle(myrotation, 3);
            //writing right bank
            int n = this.existing_bedlevel.geometric_x.Count - 1;
             result = this.findMidPoint(this.existing_bedlevel.geometric_x[n-1],
             this.existing_bedlevel.geometric_y[n-1], this.existing_bedlevel.geometric_x[n],
             this.existing_bedlevel.geometric_y[n]);
            annotation_text = "R/B";
            text_Location = new Point2d(result[0], result[1] + 0.5);
            myrotation = 0;           
             myannotations2 = new Label_Annotarion_With_Circle(annotation_text, text_Location, geometric_origin, draftingOrigin, false,
               this.xscale_factor, this.yscale_factor, this.draftingwidth, myradius);
            myannotations2.drawAnnotationsWithCircle(myrotation, 3);
            //writing exvation area
            double area = Math.Round(this.excavation_area,2, MidpointRounding.ToEven);
            annotation_text = "Earth Excavation=" +" "+area+"cum/m";
            i = this.existing_bedlevel.geometric_x.IndexOf(this.design_bedlevel.geometric_x[3]);

            result = this.findMidPoint(this.existing_bedlevel.geometric_x[i+1], this.existing_bedlevel.geometric_y[i+1],
               this.design_bedlevel.geometric_x[3], this.design_bedlevel.geometric_y[3]);
            text_Location = new Point2d(this.design_bedlevel.geometric_x[3], this.ymax * 1.2);
            arrow_tip = new Point2d(result[0], result[1]);
            myannotations3 = new Label_Annotation_With_Leader
                (annotation_text, text_Location, geometric_origin, draftingOrigin, false,
                this.xscale_factor, this.yscale_factor, this.draftingwidth, arrow_tip);
            myannotations3.drawAnnotationsWithLeader(0, 1);
            //writing Bed Elevation vaiations
            annotation_text = "Bed Level Varies from \n EL"+Math.Round(this.starting_elevation,2,MidpointRounding.ToEven)
                +" to EL "+Math.Round(this.end_elevation,2,MidpointRounding.ToEven);
           // text_Location = new Point2d(result[0], result[1] + 0.5);
            result = this.findMidPoint(this.design_bedlevel.geometric_x[5], this.design_bedlevel.geometric_y[5],
                this.design_bedlevel.geometric_x[6], this.design_bedlevel.geometric_y[6]);
            //text_Location = new Point2d(result[0], result[1]);
            int index = this.existing_bedlevel.geometric_x.IndexOf(this.design_bedlevel.geometric_x[6]) + 1;
            text_Location = new Point2d(this.existing_bedlevel.geometric_x[index], this.existing_bedlevel.geometric_y.Min() - 0.25);
            myannotations = new Label_Annotation
               (annotation_text, text_Location, geometric_origin, draftingOrigin, false,
               this.xscale_factor, this.yscale_factor, this.draftingwidth);
            myannotations.drawAnnotation(myrotation, 1);
            //writing section applicability
         
            annotation_text = this.sectionname + " AT KM " + Math.Round(this.chaiange, 3, MidpointRounding.ToEven) + "\n"
                + "Applicable form KM " + Math.Round(this.starting_application, 3, MidpointRounding.ToEven) + " to KM " +
                Math.Round(this.end_application, 3, MidpointRounding.ToEven);

            /*
         result = this.findMidPoint(this.design_bedlevel.geometric_x[5], this.design_bedlevel.geometric_y[5],
             this.design_bedlevel.geometric_x[6], this.design_bedlevel.geometric_y[6]);
            */
            // w * 0.12;
            draftingOrigin = new Point2d(this.drafting_origin.X, this.drafting_origin.Y - (this.ymax + 1.5 - this.ymin) * this.yscale_factor);
            double x_coord =0.25* (this.draftingwidth / this.xscale_factor);
            double y_coord =this.existing_bedlevel.geometric_origin_y -this.draftingwidth*0.12/this.yscale_factor-2.5;
            // double y_coord = this.existing_bedlevel.geometric_origin_y-(this.ymax-this.ymin)- this.draftingwidth * 0.12/this.yscale_factor;
            // text_Location = new Point2d(x_coord,y_coord);
            // text_Location = new Point2d(this.design_bedlevel.geometric_x[3], (draftingOrigin.Y/this.yscale_factor)+0.25);
            // text_Location = new Point2d(x_coord, (draftingOrigin.Y / this.yscale_factor) + 0.25);
           
            text_Location = new Point2d(x_coord, y_coord);
            myannotations = new Label_Annotation
            (annotation_text, text_Location, geometric_origin, draftingOrigin, false,
            this.xscale_factor, this.yscale_factor, this.draftingwidth);
         myannotations.drawAnnotation(myrotation, 3);
            // myannotations.dr(myrotation, 1);
            edt.WriteMessage("\n dog x=" + this.drafting_origin.X + "y=" + this.drafting_origin.Y);
            edt.WriteMessage("\n gog x=" + this.existing_bedlevel.geometric_origin_x + "y=" + this.existing_bedlevel.geometric_origin_y);
            edt.WriteMessage("\n x_coord=" + x_coord+"y_coord=" + y_coord);
        }
     private int getLeftBankPlacementIndex() 

     {
         double x = this.design_bedlevel.geometric_x[0];
         int i = this.existing_bedlevel.geometric_x.IndexOf(x)-1;
         if (i >= 0)
         { return i; }
         else
         { return 0; }
     }
     private int getRightBankPlacementIndex()

     {
         double x = this.design_bedlevel.geometric_x[0];
         int n = this.existing_bedlevel.geometric_x.Count;
         int i = this.existing_bedlevel.geometric_x.IndexOf(x)+1;
         if (i >=n )
         {
             return n - 1;

         }
         else
         {
             return i;
         }


     }



     public Leader drawLeader(Point2d p1, Point2d p2)
     {

         double w = this.draftingwidth;
         Leader myleader = new Leader();
         myleader.SetDatabaseDefaults();

         myleader.AppendVertex(new Point3d(p1.X, p1.Y, 0));
         myleader.AppendVertex(new Point3d(p2.X, p2.Y, 0));
         myleader.AppendVertex(new Point3d(p2.X, p1.Y + 100, 0));
         myleader.HasArrowHead = true;
         myleader.Dimasz = myleader.Dimasz * (w / 30); // leader size
         return myleader;


     }
     

     public void drawXsection()
     {
         this.existing_bedlevel.fixProfileColor(255);
         this.existing_bedlevel.setLineWeight(8);
         Point2d draftingOrigin = new Point2d(this.drafting_origin.X, this.drafting_origin.Y - (this.ymax - this.ymin) * this.yscale_factor);
            // string[] mylabels2 = { "Existing Section","Design Section" };
            //  double[] mylocations2 = {this.design_bedlevel.geometric_x[1], this.design_bedlevel.geometric_x[5] };
            // List<double> bbox = this.existing_bedlevel.getBoundingBox();
            //  double ymax = bbox[3];
            //  double[] label_locy2 = {ymax*1.1,ymax*0.75 };
            //this.existing_bedlevel.setLabelParameteWithXvalues(mylabels2, mylocations2, label_locy2);
            //this.existing_bedlevel.setLabelParameter(mylabels2, mylocations2);
            // this.existing_bedlevel.drawProfile_WithLineType(draftingOrigin.X,draftingOrigin.Y,"DASHED" );
            string sLayerName = "Existing_Bed_Level for Xsection"; 
            this.existing_bedlevel.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y,sLayerName);
         // myxsection.design_bedlevel.setLabelParameter(mylabels5, mylocations5);
         this.design_bedlevel.fixProfileColor(255);
         this.design_bedlevel.setLineWeight(8); 
         this.vertical_scale.drawVscale(draftingOrigin.X, draftingOrigin.Y);
            //drawing excavation boundary
            sLayerName = "Excavation Area";
         this.drawExcavation(draftingOrigin.X, draftingOrigin.Y,sLayerName);


        // string[] mylabels3 = { "Design Section" };
        // double[] mylocations3 = { this.design_bedlevel.geometric_x[5] };
        // double[] labellocy3 = { 1.01 * ymax };
        // this.design_bedlevel.setLabelParameteWithXvalues(mylabels3, mylocations3, labellocy3);
        
            
            //drawing data Table
            /*
         draftingOrigin = new Point2d(this.drafting_origin.X, this.drafting_origin.Y - (this.ymax+2.5 - this.ymin) * this.yscale_factor);
         sLayerName = "Cross Section Data Table"; ;
         this.data_table.drawTableWithLayer(draftingOrigin.X, draftingOrigin.Y,sLayerName);
         draftingOrigin = new Point2d(this.drafting_origin.X, this.drafting_origin.Y - (this.ymax - this.ymin) * this.yscale_factor);
          */
            //drawing design bed level
            
            sLayerName = "Design_Bed_Level for Xsection";
          this.design_bedlevel.drawProfileWithLayer(draftingOrigin.X, draftingOrigin.Y,sLayerName);
            //drawing centerline 
            this.DrawCL();
         //drawing leader
         // this.AnnotateXsectionWithLeader();
         // Point2d p1 = new Point2d(this.existing_bedlevel.geometric_x[5], this.existing_bedlevel.geometric_y[5]);
         //Point2d p2 = new Point2d(this.existing_bedlevel.geometric_x[5], this.de);

         /*Testing Label Paremeter*/
          this.AnnotateXsectionWithLeader();
            //drawing data label
            //extract topleft of drawing sheet...........
            Point2d top_left = this.dsheet.TopLeft;            
            double dw = this.draftingwidth;
            double xog = this.existing_bedlevel.geometric_origin_x;
            double yog = this.existing_bedlevel.geometric_origin_y;
            double sx = this.xscale_factor;
            double sy = this.yscale_factor;
            double ylocation_label;
            double xod = this.drafting_origin.X;
            double yod = this.drafting_origin.Y;
            List<double> horizontal_loc = new List<double>();
            //writing distance
            ylocation_label = yog + (-0.25 * dw / sy);
            this.existing_bedlevel.drawDistanceLabelForXsection(xod, yod, ylocation_label);
            //writing existing bedlevel
            ylocation_label = yog + (-0.21 * dw / sy);
            horizontal_loc.Add(0.21 * dw);
            this.existing_bedlevel.drawRLlabelForXsection(xod, yod, ylocation_label);
            //writing design bed level
            ylocation_label = yog + (-0.17 * dw / sy);
            horizontal_loc.Add(0.17 * dw);
            this.design_bedlevel.drawRLlabelForXsection(xod, yod, ylocation_label);
            ///drawing border for data
            // yod = this.drafting_origin.Y;
            this.existing_bedlevel.drawBorderForDataLabelForXsection
                (xod,yod ,this.draftingwidth, horizontal_loc,this.drawing_location, this.dsheet);
            //drawing border for datalabel
           // this.existing_bedlevel.drawBorderForDataLabel(xod, yod, dw, horizontal_loc);
            /*

            //writing distance label
            this.existing_section.drawDistanceLabel(xod, yod, ylocation_label);



            //writing left bank RL
            ylocation_label = yog + (-0.14 * dw / sy);
            horizontal_loc.Add(0.14 * dw);
            this.left_bank.drawRLlabel(xod, yod, ylocation_label);
            //writing right bank RL
            ylocation_label = yog + (-0.11 * dw / sy);
            horizontal_loc.Add(0.11 * dw);
            this.right_bank.drawRLlabel(xod, yod, ylocation_label);
            //writing center line existing bank RL
            ylocation_label = yog + (-0.08 * dw / sy);
            horizontal_loc.Add(0.08 * dw);
            this.existing_section.drawRLlabel(xod, yod, ylocation_label);
            // writing design label
            ylocation_label = yog + (-0.05 * dw / sy);
            horizontal_loc.Add(0.05 * dw);
            horizontal_loc.Add(0.025 * dw);
            this.design_section.drawRLlabel(xod, yod, ylocation_label);
            this.existing_section.drawBorderForDataLabel(xod, yod, dw, horizontal_loc);

            */


            //  this.existing_bedlevel.drawDistanceLabel(draftingOrigin.X, draftingOrigin.Y, -5);


        }
    }
}
