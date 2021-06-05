' Main AutoCAD namespaces
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.PlottingServices
Imports Autodesk.AutoCAD

Public Class AU2012_Plotting
    ' Plots the current layout to a DWF file
    <CommandMethod("PlotLayout")> _
    Public Shared Sub PlotLayout()
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

            ' Get the PlotInfo from the layout
            Using acPlInfo As PlotInfo = New PlotInfo()
                acPlInfo.Layout = acLayout.ObjectId

                ' Get a copy of the PlotSettings from the layout
                Using acPlSet As PlotSettings = New PlotSettings(acLayout.ModelType)
                    acPlSet.CopyFrom(acLayout)

                    ' Update the PlotSettings object
                    Dim acPlSetVdr As PlotSettingsValidator = _
                        PlotSettingsValidator.Current

                    ' Set the plot type
                    acPlSetVdr.SetPlotType(acPlSet, _
                                           DatabaseServices.PlotType.Extents)

                    ' Set the plot scale
                    acPlSetVdr.SetUseStandardScale(acPlSet, True)
                    acPlSetVdr.SetStdScaleType(acPlSet, StdScaleType.ScaleToFit)

                    ' Center the plot
                    acPlSetVdr.SetPlotCentered(acPlSet, True)

                    ' Set the plot device to use
                    acPlSetVdr.SetPlotConfigurationName(acPlSet, "DWF6 ePlot.pc3", _
                                                        "ANSI_A_(8.50_x_11.00_Inches)")

                    ' Set the plot info as an override since it will
                    ' not be saved back to the layout
                    acPlInfo.OverrideSettings = acPlSet

                    ' Validate the plot info
                    Using acPlInfoVdr As PlotInfoValidator = New PlotInfoValidator()
                        acPlInfoVdr.MediaMatchingPolicy = MatchingPolicy.MatchEnabled
                        acPlInfoVdr.Validate(acPlInfo)

                        ' Check to see if a plot is already in progress
                        If PlotFactory.ProcessPlotState = _
                            ProcessPlotState.NotPlotting Then

                            Using acPlEng As PlotEngine = _
                                PlotFactory.CreatePublishEngine()

                                ' Track the plot progress with a Progress dialog
                                Using acPlProgDlg As PlotProgressDialog = _
                                    New PlotProgressDialog(False, 1, True)

                                    Using (acPlProgDlg)
                                        ' Define the status messages to display 
                                        ' when plotting starts
                                        acPlProgDlg.PlotMsgString( _
                                            PlotMessageIndex.DialogTitle) = "Plot Progress"
                                        acPlProgDlg.PlotMsgString( _
                                            PlotMessageIndex.CancelJobButtonMessage) = _
                                                                   "Cancel Job"
                                        acPlProgDlg.PlotMsgString( _
                                            PlotMessageIndex.CancelSheetButtonMessage) = _
                                                                   "Cancel Sheet"
                                        acPlProgDlg.PlotMsgString( _
                                            PlotMessageIndex.SheetSetProgressCaption) = _
                                                                   "Sheet Set Progress"
                                        acPlProgDlg.PlotMsgString( _
                                            PlotMessageIndex.SheetProgressCaption) = _
                                                                   "Sheet Progress"

                                        ' Set the plot progress range
                                        acPlProgDlg.LowerPlotProgressRange = 0
                                        acPlProgDlg.UpperPlotProgressRange = 100
                                        acPlProgDlg.PlotProgressPos = 0

                                        ' Display the Progress dialog
                                        acPlProgDlg.OnBeginPlot()
                                        acPlProgDlg.IsVisible = True

                                        ' Start to plot the layout
                                        acPlEng.BeginPlot(acPlProgDlg, Nothing)

                                        ' Define the plot output
                                        acPlEng.BeginDocument(acPlInfo, _
                                                              acDoc.Name, _
                                                              Nothing, _
                                                              1, _
                                                              True, _
                                                              "c:\myplot")

                                        ' Display information about the current plot
                                        acPlProgDlg.PlotMsgString( _
                                            PlotMessageIndex.Status) = _
                                            "Plotting: " & acDoc.Name & _
                                            " - " & acLayout.LayoutName

                                        ' Set the sheet progress range
                                        acPlProgDlg.OnBeginSheet()
                                        acPlProgDlg.LowerSheetProgressRange = 0
                                        acPlProgDlg.UpperSheetProgressRange = 100
                                        acPlProgDlg.SheetProgressPos = 0

                                        ' Plot the first sheet/layout
                                        Using acPlPageInfo As PlotPageInfo = _
                                            New PlotPageInfo()
                                            acPlEng.BeginPage(acPlPageInfo, _
                                                              acPlInfo, _
                                                              True, _
                                                              Nothing)
                                        End Using

                                        acPlEng.BeginGenerateGraphics(Nothing)
                                        acPlEng.EndGenerateGraphics(Nothing)

                                        ' Finish plotting the sheet/layout
                                        acPlEng.EndPage(Nothing)
                                        acPlProgDlg.SheetProgressPos = 100
                                        acPlProgDlg.OnEndSheet()

                                        ' Finish plotting the document
                                        acPlEng.EndDocument(Nothing)

                                        ' Finish the plot
                                        acPlProgDlg.PlotProgressPos = 100
                                        acPlProgDlg.OnEndPlot()
                                        acPlEng.EndPlot(Nothing)
                                    End Using
                                End Using
                            End Using
                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub
End Class
