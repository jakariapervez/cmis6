// Main AutoCAD namespaces
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace CP2071_CSharp
{
    class AU2012_Plotters_PlotStyles
    {
        // Lists the available plotters (plot configuration [PC3] files)
        [CommandMethod("PlotterList")]
        public static void PlotterList()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            acDoc.Editor.WriteMessage("\nPlot devices: ");

            foreach (string plotDevice in PlotSettingsValidator.Current.GetPlotDeviceList())
            {
                // Output the names of the available plotter devices
                acDoc.Editor.WriteMessage("\n  " + plotDevice);
            }
        }

        // Lists the available media sizes for a specified plot configuration (PC3) file
        [CommandMethod("PlotterMediaList")]
        public static void PlotterMediaList()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            using (PlotSettings plSet = new PlotSettings(true))
            {
                PlotSettingsValidator acPlSetVdr = PlotSettingsValidator.Current;

                // Set the Plotter and page size
                acPlSetVdr.SetPlotConfigurationName(plSet, "DWF6 ePlot.pc3", "ANSI_A_(8.50_x_11.00_Inches)");

                acDoc.Editor.WriteMessage("\nAvailable media sizes: ");

                foreach (string mediaName in acPlSetVdr.GetCanonicalMediaNameList(plSet))
                {
                    // Output the names of the available media for the specified device
                    acDoc.Editor.WriteMessage("\n  " + mediaName);
                }
            }
        }

        // Lists the available plot styles
        [CommandMethod("PlotStyleList")]
        public static void PlotStyleList()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            acDoc.Editor.WriteMessage("\nPlot styles: ");

            foreach (string plotStyle in PlotSettingsValidator.Current.GetPlotStyleSheetList())
            {
                // Output the names of the available plot styles
                acDoc.Editor.WriteMessage("\n  " + plotStyle);
            }
        }
    }
}
