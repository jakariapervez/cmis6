
' Main AutoCAD namespaces
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.PlottingServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD

Public Class AU2012_PageSetups

    ' Lists the available page setups
    <CommandMethod("PageSetupList")> _
    Public Shared Sub PageSetupList()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        ' Start a transaction
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            Dim plSettings As DBDictionary = _
                acTrans.GetObject(acCurDb.PlotSettingsDictionaryId, OpenMode.ForRead)

            acDoc.Editor.WriteMessage(vbLf & "Page Setups: ")

            ' List each named page setup
            For Each item As DBDictionaryEntry In plSettings
                acDoc.Editor.WriteMessage(vbLf & "  " & item.Key)
            Next

            ' Abort the changes to the database
            acTrans.Abort()
        End Using
    End Sub

    ' Creates a new page setup or edits the page set if it exists
    <CommandMethod("PageSetupCreateEdit")> _
    Public Shared Sub PageSetupCreate()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()

            Dim plSets As DBDictionary = _
                acTrans.GetObject(acCurDb.PlotSettingsDictionaryId, OpenMode.ForRead)
            Dim vStyles As DBDictionary = _
                acTrans.GetObject(acCurDb.VisualStyleDictionaryId, OpenMode.ForRead)

            Dim acPlSet As PlotSettings
            Dim createNew As Boolean = False

            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Get the current layout and output its name in the Command Line window
            Dim acLayout As Layout = _
                acTrans.GetObject(acLayoutMgr.GetLayoutId(acLayoutMgr.CurrentLayout), _
                                  OpenMode.ForRead)

            ' Check to see if the page setup exists
            If plSets.Contains("MyPageSetup") = False Then
                createNew = True

                ' Create a new PlotSettings object: 
                '    True - model space, False - named layout
                acPlSet = New PlotSettings(acLayout.ModelType)
                acPlSet.CopyFrom(acLayout)

                acPlSet.PlotSettingsName = "MyPageSetup"
                acPlSet.AddToPlotSettingsDictionary(acCurDb)
                acTrans.AddNewlyCreatedDBObject(acPlSet, True)
            Else
                acPlSet = plSets.GetAt("MyPageSetup").GetObject(OpenMode.ForWrite)
            End If

            Dim acPlSetVdr As PlotSettingsValidator = PlotSettingsValidator.Current

            ' Update the PlotSettings object
            Try
                ' Set the Plotter and page size
                acPlSetVdr.SetPlotConfigurationName(acPlSet, _
                                                    "DWF6 ePlot.pc3", _
                                                    "ANSI_B_(17.00_x_11.00_Inches)")

                ' Set to plot to the current display
                If acLayout.ModelType = False Then
                    acPlSetVdr.SetPlotType(acPlSet, _
                                           DatabaseServices.PlotType.Layout)
                Else
                    acPlSetVdr.SetPlotType(acPlSet, _
                                           DatabaseServices.PlotType.Extents)

                    acPlSetVdr.SetPlotCentered(acPlSet, True)
                End If

                ' Use SetPlotWindowArea with PlotType.Window
                'acPlSetVdr.SetPlotWindowArea(plSet, _
                '                             New Extents2d(New Point2d(0.0, 0.0), _
                '                             New Point2d(9.0, 12.0)))

                ' Use SetPlotViewName with PlotType.View
                'acPlSetVdr.SetPlotViewName(plSet, "MyView")

                ' Set the plot offset
                acPlSetVdr.SetPlotOrigin(acPlSet, _
                                         New Point2d(0, 0))

                ' Set the plot scale
                acPlSetVdr.SetUseStandardScale(acPlSet, True)
                acPlSetVdr.SetStdScaleType(acPlSet, StdScaleType.ScaleToFit)
                acPlSetVdr.SetPlotPaperUnits(acPlSet, PlotPaperUnit.Inches)
                acPlSet.ScaleLineweights = True

                ' Specify if plot styles should be displayed on the layout
                acPlSet.ShowPlotStyles = True

                ' Rebuild plotter, plot style, and canonical media lists 
                ' (must be called before setting the plot style)
                acPlSetVdr.RefreshLists(acPlSet)

                ' Specify the shaded viewport options
                acPlSet.ShadePlot = PlotSettingsShadePlotType.AsDisplayed

                acPlSet.ShadePlotResLevel = ShadePlotResLevel.Normal

                ' Specify the plot options
                acPlSet.PrintLineweights = True
                acPlSet.PlotTransparency = False
                acPlSet.PlotPlotStyles = True
                acPlSet.DrawViewportsFirst = True

                ' Use only on named layouts - Hide paperspace objects option
                ' plSet.PlotHidden = True

                ' Specify the plot orientation
                acPlSetVdr.SetPlotRotation(acPlSet, PlotRotation.Degrees000)

                ' Set the plot style
                If acCurDb.PlotStyleMode = True Then
                    acPlSetVdr.SetCurrentStyleSheet(acPlSet, "acad.ctb")
                Else
                    acPlSetVdr.SetCurrentStyleSheet(acPlSet, "acad.stb")
                End If

                ' Zoom to show the whole paper
                acPlSetVdr.SetZoomToPaperOnUpdate(acPlSet, True)
            Catch es As Autodesk.AutoCAD.Runtime.Exception
                MsgBox(es.Message)
            End Try

            ' Save the changes made
            acTrans.Commit()

            If createNew = True Then
                acPlSet.Dispose()
            End If
        End Using
    End Sub

    ' Assigns a page setup to a layout
    <CommandMethod("PageSetupAssignToLayout")> _
    Public Shared Sub PageSetupAssignToLayout()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Get the current layout and output its name in the Command Line window
            Dim acLayout As Layout = _
                acTrans.GetObject(acLayoutMgr.GetLayoutId(acLayoutMgr.CurrentLayout), _
                                  OpenMode.ForRead)

            Dim acPlSet As DBDictionary = _
                acTrans.GetObject(acCurDb.PlotSettingsDictionaryId, OpenMode.ForRead)

            ' Check to see if the page setup exists
            If acPlSet.Contains("MyPageSetup") = True Then
                Dim plSet As PlotSettings = _
                    acPlSet.GetAt("MyPageSetup").GetObject(OpenMode.ForRead)

                ' Update the layout
                acLayout.UpgradeOpen()
                acLayout.CopyFrom(plSet)

                ' Save the new objects to the database
                acTrans.Commit()
            Else
                ' Ignore the changes made
                acTrans.Abort()
            End If
        End Using

        ' Update the display
        acDoc.Editor.Regen()
    End Sub

    ' Changes the plot settings for a layout directly
    <CommandMethod("LayoutChangePlotSettings")> _
    Public Shared Sub LayoutChangePlotSettings()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Get the current layout and output its name in the Command Line window
            Dim acLayout As Layout = _
                acTrans.GetObject(acLayoutMgr.GetLayoutId(acLayoutMgr.CurrentLayout), _
                                  OpenMode.ForRead)

            ' Output the name of the current layout and its device
            acDoc.Editor.WriteMessage(vbLf & "Current layout: " & _
                                        acLayout.LayoutName)

            acDoc.Editor.WriteMessage(vbLf & "Current device name: " & _
                                        acLayout.PlotConfigurationName)

            ' Get a copy of the PlotSettings from the layout
            Dim acPlSet As PlotSettings = New PlotSettings(acLayout.ModelType)
            acPlSet.CopyFrom(acLayout)

            ' Update the PlotConfigurationName property of the PlotSettings object
            Dim acPlSetVdr As PlotSettingsValidator = PlotSettingsValidator.Current
            acPlSetVdr.SetPlotConfigurationName(acPlSet, "DWG To PDF.pc3", _
                                                "ANSI_B_(11.00_x_17.00_Inches)")

            ' Zoom to show the whole paper
            acPlSetVdr.SetZoomToPaperOnUpdate(acPlSet, True)

            ' Update the layout
            acLayout.UpgradeOpen()
            acLayout.CopyFrom(acPlSet)

            ' Output the name of the new device assigned to the layout
            acDoc.Editor.WriteMessage(vbLf & "New device name: " & _
                                        acLayout.PlotConfigurationName)

            ' Save the new objects to the database
            acTrans.Commit()
        End Using

        ' Update the display
        acDoc.Editor.Regen()
    End Sub
End Class
