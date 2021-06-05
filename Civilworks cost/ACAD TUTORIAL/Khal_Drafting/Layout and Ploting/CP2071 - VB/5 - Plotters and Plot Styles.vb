
' Main AutoCAD namespaces
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices

Public Class AU2012_Plotters_PlotStyles

    ' Lists the available plotters (plot configuration [PC3] files)
    <CommandMethod("PlotterList")> _
    Public Shared Sub PlotterList()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument

        acDoc.Editor.WriteMessage(vbLf & "Plot devices: ")

        For Each plotDevice As String In PlotSettingsValidator.Current.GetPlotDeviceList()
            ' Output the names of the available plotter devices
            acDoc.Editor.WriteMessage(vbLf & "  " & plotDevice)
        Next
    End Sub

    ' Lists the available media sizes for a specified plot configuration (PC3) file
    <CommandMethod("PlotterMediaList")> _
    Public Shared Sub PlotterMediaList()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument

        Using plSet As PlotSettings = New PlotSettings(True)
            Dim acPlSetVdr As PlotSettingsValidator = PlotSettingsValidator.Current

            ' Set the Plotter and page size
            acPlSetVdr.SetPlotConfigurationName(plSet, "DWF6 ePlot.pc3", _
                                                "ANSI_A_(8.50_x_11.00_Inches)")

            acDoc.Editor.WriteMessage(vbLf & "Available media sizes: ")

            For Each mediaName As String In acPlSetVdr.GetCanonicalMediaNameList(plSet)
                ' Output the names of the available media for the specified device
                acDoc.Editor.WriteMessage(vbLf & "  " & mediaName)
            Next
        End Using
    End Sub

    ' Lists the available plot styles
    <CommandMethod("PlotStyleList")> _
    Public Shared Sub PlotStyleList()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument

        acDoc.Editor.WriteMessage(vbLf & "Plot styles: ")

        For Each plotStyle As String In PlotSettingsValidator.Current.GetPlotStyleSheetList()
            ' Output the names of the available plot styles
            acDoc.Editor.WriteMessage(vbLf & "  " & plotStyle)
        Next
    End Sub
End Class
