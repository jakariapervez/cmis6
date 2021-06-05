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
    class LSection
    {
        //parameters
        /// <summary>
        /// xod:xvalue of origin for drafting
        /// yod:yvalue of origin for drafting
        /// sx:xscale fctor
        /// sy:yscale facto
        /// xog:geomatric origin of profile in x
        /// yog:geomatric origin of profile in y 
        /// </summary>


        public LongSectionProfile design_section;
        public LongSectionProfile existing_section;
        public LongSectionProfile left_bank;
        public LongSectionProfile right_bank;
        public Vscale vertical_axis;
        /// <summary>
        /// drafting realted parameter
        /// </summary>
        public double sx { get; set; }
        public double sy { get; set; }
        double xod { get; set; }
        double yod { get; set; }
        public double xog { get; set; }
        public double yog { get; set; }
        public double profile_length { get; set; }
        public double drafting_width { get; set; }
        public double maximum_y_range;
        public DrawingSheet ds;
        public LSection(List<double> xvalues, List<double> clvalues,
            List<double> dlvalues, List<double> lbvalues, List<double> rbvalues, DrawingSheet ds)
        {
            /*Finding the Drafting Parameters*/
            DraftingSettings dfs = new DraftingSettings();
            var result = dfs.calcualte_drafting_paameters_Lsection(xvalues,
                lbvalues, rbvalues, clvalues, dlvalues, ds);
            double xorigin = xvalues.Min();
            double yorigin = result[1];
            double ymax = result[0];
            double sx = result[3];
            double sy = result[4];
            double profile_length = result[5];
            double maximum_y_offset = result[2];
            Point2d draftingOrigin = ds.DrawingOriginA();
            /*detrimining drafting origin coordinates*/
            double xod = draftingOrigin.X;
            double yod = draftingOrigin.Y - maximum_y_offset * sy;
            double dw = ds.width;
            //setting drawing sheet
            this.ds = ds;
            //setting parameters for the Longsection
            this.xod = xod;
            this.yod = yod;
            this.xog = xorigin;
            this.yog = yorigin;
            this.sx = sx;
            this.sy = sy;
            this.maximum_y_range = maximum_y_offset;
            this.profile_length = profile_length;
            this.drafting_width = dw;
            //setting parameters for profile
            ///existing RL at cL
            this.existing_section = new LongSectionProfile(xvalues, clvalues, this.xog,
                this.yog, this.sx, this.sy, this.profile_length, drafting_width);
            string[] mylabels = { "Existing Bed Level", "Outfall" };
            double[] mylocations = { 4.5, 0 };
            this.existing_section.setLabelParameter(mylabels, mylocations);
            ///design RL at cL
            this.design_section = new LongSectionProfile(xvalues, dlvalues, this.xog,
                this.yog, this.sx, this.sy, this.profile_length, drafting_width);
            string[] mylabels4 = { "Design Bed Level" };
            double[] mylocations4 = { 1.33 };
           this.design_section.setLabelParameter(mylabels4, mylocations4);
            ///Existing RL at Left Bank
            this.left_bank = new LongSectionProfile(xvalues, lbvalues, this.xog,
                this.yog, this.sx, this.sy, this.profile_length, drafting_width);
            string[] mylabels2 = { "Existing Left Bank" };
            double[] mylocations2 = { 8 };
            this.left_bank.setLabelParameter(mylabels2, mylocations2);
            ///Existing RB at Left Bank
            this.right_bank = new LongSectionProfile(xvalues, rbvalues, this.xog,
                this.yog, this.sx, this.sy, this.profile_length, drafting_width);
            string[] mylabels3 = { "Existing Right Bank" };
            double[] mylocations3 = { 2.5 };
            this.right_bank.setLabelParameter(mylabels3, mylocations3);
            //vertical axis for profile
            Point2d vscale_location = new Point2d(draftingOrigin.X - dw * 0.03,
                   draftingOrigin.Y);//vscale location in drafting  coordinates  
            Mytransformation trs = new Mytransformation();
            Point2d vscale_location_geomatric = trs.Drafting2GeometricCoordinate(vscale_location,
                new Point2d(xorigin, yorigin), new Point2d(xod, yod), sx, sy);
            // double vAxis_loc= draftingOrigin.X - 0.02 * dw;
            this.vertical_axis = new Vscale(this.xog, this.yog, this.sx, this.sy, dw,
                vscale_location_geomatric.X, ymax, yorigin);
           

        }
        public void drawLongsection()
        {
            //draw existing section
            string sLayerName = "Existing Bed Level for Long Section";
            this.existing_section.drawProfileWithLayer(this.xod, this.yod, sLayerName);
            //draw design section
            sLayerName = "Design Bed Level Long Section";
            this.design_section.drawProfileWithLayer(this.xod, this.yod, sLayerName);
            //draw Left Bank 
            sLayerName = "Existing Left Bank of Long section"; ;
            this.left_bank.drawProfileWithLayer(xod, yod, sLayerName);
            //draw Right Bank 
            sLayerName = "Existing Right Bank of Long Section";
            this.right_bank.drawProfileWithLayer(xod, yod, sLayerName);
            //drawing vAxis
           this.vertical_axis.drawVscale(this.xod,this.yod );
            //drawing data label.....................
            // first draw distance
            List<double> horizontal_loc = new List<double>();
            //extract topleft of drawing sheet...........
            Point2d top_left = this.ds.TopLeft;
            double dw = this.drafting_width;
            double ylocation_label = yog + (-0.175 * dw / sy);
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
            
        }

    }


}
