' Standard .NET namespaces
Imports System.IO

' Main AutoCAD namespaces
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.PlottingServices

' AutoCAD Sheet Set API
Imports ACSMCOMPONENTS19Lib

Public Class AU2012_Publishing

    ' Publishes all the drawings in a sheet set to a multi-sheet DWF
    <CommandMethod("PublishSheetSet")> _
    Public Sub PublishSheetSet()
        ' Get and set the BackgroundPlot system variable value
        Dim backPlot As Object = Application.GetSystemVariable("BACKGROUNDPLOT")
        Application.SetSystemVariable("BACKGROUNDPLOT", 0)

        ' DSD filename
        Dim dsdFilename As String = Environment.GetFolderPath( _
                                    Environment.SpecialFolder.MyDocuments) & _
                                    "\batchdrawings.dsd"

        ' Delete the DSD file if it exists
        If File.Exists(dsdFilename) = True Then File.Delete(dsdFilename)

        ' Get a reference to the Sheet Set Manager object
        Dim sheetSetManager As IAcSmSheetSetMgr = New AcSmSheetSetMgr

        ' Open a Sheet Set file
        Dim sheetSetDatabase As AcSmDatabase = _
            sheetSetManager.OpenDatabase("C:\Program Files\Autodesk\AutoCAD 2013\Sample\Sheet Sets\" & _
                                         "Architectural\IRD Addition.dst", False)

        Dim sheetSet As AcSmSheetSet = sheetSetDatabase.GetSheetSet()

        Dim enumerator As IAcSmEnumPersist
        Dim itemSheetSet As IAcSmPersist

        Using dsdDwgFiles As New DsdEntryCollection()

            ' Get the enumerator for the objects in the sheet set
            enumerator = sheetSetDatabase.GetEnumerator()
            itemSheetSet = enumerator.Next()

            ' Step through the objects in the sheet set
            Do While Not itemSheetSet Is Nothing
                ' Increment the counter of the object is a sheet
                If itemSheetSet.GetTypeName() = "AcSmSheet" Then

                    Dim sheet As AcSmSheet = itemSheetSet

                    ' Add drawing file
                    If File.Exists(sheet.GetLayout().GetFileName()) = True Then
                        Using dsdDwgFile As DsdEntry = New DsdEntry()

                            ' Set the file name and layout
                            dsdDwgFile.DwgName = sheet.GetLayout().GetFileName()
                            dsdDwgFile.Layout = sheet.GetLayout().GetName()
                            dsdDwgFile.Title = sheet.GetName()

                            ' Set the page setup override
                            dsdDwgFile.Nps = ""
                            dsdDwgFile.NpsSourceDwg = ""

                            dsdDwgFiles.Add(dsdDwgFile)
                        End Using
                    End If
                End If

                ' Get next object
                itemSheetSet = enumerator.Next()
            Loop

            If dsdDwgFiles.Count > 0 Then
                ' Set the properties for the DSD file and then write it out
                Using dsdFileData As DsdData = New DsdData()

                    ' Set the version of the DWF 6 format
                    dsdFileData.MajorVersion = 1
                    dsdFileData.MinorVersion = 1

                    ' Set the target information for publishing
                    dsdFileData.DestinationName = Environment.GetFolderPath( _
                        Environment.SpecialFolder.MyDocuments) & "\" & _
                        sheetSetDatabase.GetSheetSet.GetName() & ".dwf"
                    dsdFileData.ProjectPath = Environment.GetFolderPath( _
                        Environment.SpecialFolder.MyDocuments)

                    ' Set the type of output that should be generated
                    dsdFileData.SheetType = SheetType.MultiDwf

                    ' Set the drawings that should be added to the publication
                    dsdFileData.SetDsdEntryCollection(dsdDwgFiles)

                    ' Sheet set properties
                    dsdFileData.IsSheetSet = True
                    dsdFileData.SheetSetName = sheetSetDatabase.GetName()
                    dsdFileData.SelectionSetName = ""
                    dsdFileData.IsHomogeneous = False

                    ' Set the general publishing properties
                    dsdFileData.CategoryName = ""
                    dsdFileData.NoOfCopies = 1
                    dsdFileData.LogFilePath = Environment.GetFolderPath( _
                        Environment.SpecialFolder.MyDocuments) & "\myBatch.txt"
                    dsdFileData.Password = ""
                    dsdFileData.PlotStampOn = False
                    dsdFileData.PromptForDwfName = False

                    ' Set the DWF 3D options
                    dsdFileData.Dwf3dOptions.GroupByXrefHierarchy = False
                    dsdFileData.Dwf3dOptions.PublishWithMaterials = False

                    ' Create the DSD file
                    dsdFileData.WriteDsd(Environment.GetFolderPath( _
                        Environment.SpecialFolder.MyDocuments) & _
                        "\batchdrawings.dsd")

                    ' Track the progress with a Progress dialog
                    Using acPlProgDlg As PlotProgressDialog = _
                        New PlotProgressDialog(False, dsdDwgFiles.Count, False)

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

                        acPlProgDlg.LowerSheetProgressRange = 0
                        acPlProgDlg.UpperSheetProgressRange = 100

                        acPlProgDlg.IsVisible = True

                        Try
                            ' Publish the drawing using the DSD in the foreground
                            Application.Publisher.PublishDsd(Environment.GetFolderPath( _
                                Environment.SpecialFolder.MyDocuments) & _
                                          "\batchdrawings.dsd", acPlProgDlg)

                        Catch es As Autodesk.AutoCAD.Runtime.Exception
                            MsgBox(es.Message)
                        Finally
                            acPlProgDlg.Destroy()
                            acPlProgDlg.Dispose()
                        End Try
                    End Using
                End Using
            End If
        End Using

        ' Close the sheet set
        sheetSetManager.Close(sheetSetDatabase)

        ' Restore the previous value for the BackgroundPlot system variable
        Application.SetSystemVariable("BACKGROUNDPLOT", backPlot)
    End Sub

    ' Publishes a few drawings in the background to PDF file
    <CommandMethod("PublishPDF")> _
    Public Shared Sub PublishPDF()
        Using dsdDwgFiles As New DsdEntryCollection

            ' Add first drawing file
            Using dsdDwgFile1 As New DsdEntry

                ' Set the file name and layout
                dsdDwgFile1.DwgName = "C:\Program Files\Autodesk\AutoCAD 2013\Sample\" & _
                    "Sheet Sets\Architectural\A-01.dwg"
                dsdDwgFile1.Layout = "MAIN AND SECOND FLOOR PLAN"
                dsdDwgFile1.Title = "A-01 MAIN AND SECOND FLOOR PLAN"

                ' Set the page setup override
                dsdDwgFile1.Nps = ""
                dsdDwgFile1.NpsSourceDwg = ""

                dsdDwgFiles.Add(dsdDwgFile1)
            End Using

            ' Add second drawing file
            Using dsdDwgFile2 As New DsdEntry

                ' Set the file name and layout
                dsdDwgFile2.DwgName = "C:\Program Files\Autodesk\AutoCAD 2013\Sample\" & _
                    "Sheet Sets\Architectural\A-02.dwg"
                dsdDwgFile2.Layout = "ELEVATIONS"
                dsdDwgFile2.Title = "A-02 ELEVATIONS"

                ' Set the page setup override
                dsdDwgFile2.Nps = ""
                dsdDwgFile2.NpsSourceDwg = ""

                dsdDwgFiles.Add(dsdDwgFile2)
            End Using

            ' Set the properties for the DSD file and then write it out
            Using dsdFileData As New DsdData

                ' Set the target information for publishing
                dsdFileData.DestinationName = Environment.GetFolderPath( _
                    Environment.SpecialFolder.MyDocuments) & "\MyPublish2.pdf"
                dsdFileData.ProjectPath = Environment.GetFolderPath( _
                    Environment.SpecialFolder.MyDocuments) & "\"
                dsdFileData.SheetType = SheetType.MultiPdf

                ' Set the drawings that should be added to the publication
                dsdFileData.SetDsdEntryCollection(dsdDwgFiles)


                ' Set the general publishing properties
                dsdFileData.LogFilePath = Environment.GetFolderPath( _
                    Environment.SpecialFolder.MyDocuments) & "\myBatch.txt"

                ' Create the DSD file
                dsdFileData.WriteDsd(Environment.GetFolderPath( _
                                     Environment.SpecialFolder.MyDocuments) & _
                                     "\batchdrawings2.dsd")

                Try
                    ' Publish the specified drawing files in the DSD file, and
                    ' honor the behavior of the BACKGROUNDPLOT system variable
                    Using dsdDataFile As New DsdData
                        dsdDataFile.ReadDsd(Environment.GetFolderPath( _
                                            Environment.SpecialFolder.MyDocuments) & _
                                            "\batchdrawings2.dsd")

                        ' Get the DWG to PDF.pc3 and use it as a 
                        ' device override for all the layouts
                        Dim acPlCfg As PlotConfig = _
                            PlotConfigManager.SetCurrentConfig("DWG to PDF.PC3")

                        Application.Publisher.PublishExecute(dsdDataFile, acPlCfg)
                    End Using

                Catch es As Autodesk.AutoCAD.Runtime.Exception
                    MsgBox(es.Message)
                End Try
            End Using
        End Using
    End Sub
End Class
