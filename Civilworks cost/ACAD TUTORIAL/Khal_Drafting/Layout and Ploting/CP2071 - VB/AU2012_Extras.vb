' Standard .NET namespaces
Imports System.IO

' Main AutoCAD namespaces
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.PlottingServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.GraphicsInterface

Public Class AU2012_Extras
    <CommandMethod("LayoutCreateFrom")> _
    Public Shared Sub LayoutCreateFrom()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Dim newLayoutName As String = "copiedLayout"
        Dim copyLayoutName As String = "Layout1"

        ' Get the layout and plot settings of the named pagesetup
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Create the new layout based on the existing layout
            acLayoutMgr.CopyLayout(copyLayoutName, newLayoutName)

            ' Open the layout
            Dim acLayout As Layout = acTrans.GetObject(acLayoutMgr.GetLayoutId(newLayoutName), _
                                                       OpenMode.ForWrite)

            ' Set the copied layout current
            acLayoutMgr.CurrentLayout = newLayoutName

            ' Output some information related to the layout object
            acDoc.Editor.WriteMessage(vbLf & "Tab Order: " & acLayout.TabOrder & _
                                      vbLf & "Tab Selected: " & acLayout.TabSelected & _
                                      vbLf & "Block Table Record ID: " & acLayout.BlockTableRecordId.ToString())

            ' Save the changes made
            acTrans.Commit()
        End Using
    End Sub

    <CommandMethod("LayoutRemove")> _
    Public Shared Sub LayoutRemove()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Dim layoutName As String = "Layout1"

        ' Get the layout and plot settings of the named pagesetup
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Remove layout from drawing
            acLayoutMgr.DeleteLayout(layoutName)

            ' Save the changes made
            acTrans.Commit()
        End Using

        ' Regen the display so the new layout and correct order is displayed
        Application.DocumentManager.MdiActiveDocument.Editor.Regen()
    End Sub

    <CommandMethod("LayoutRename")> _
    Public Shared Sub LayoutRename()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Dim layoutName As String = "Layout2"
        Dim newLayoutName As String = "renamedLayout"

        ' Get the layout and plot settings of the named pagesetup
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Remove layout from drawing
            acLayoutMgr.RenameLayout(layoutName, newLayoutName)

            ' Save the changes made
            acTrans.Commit()
        End Using

        ' Regen the display so the new layout and correct order is displayed
        Application.DocumentManager.MdiActiveDocument.Editor.Regen()
    End Sub

    <CommandMethod("LayoutReorder")> _
    Public Shared Sub LayoutReorder()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Dim layoutName As String = "Layout3"

        ' Get the layout and plot settings of the named pagesetup
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Create a new layout
            acLayoutMgr.CreateLayout(layoutName)

            ' Get the layouts in the drawing
            Dim lays As DBDictionary = acTrans.GetObject(acCurDb.LayoutDictionaryId, OpenMode.ForRead)

            ' Step through each layout
            For Each item As DBDictionaryEntry In lays

                ' Open the layout
                Dim acLayout As Layout = acTrans.GetObject(item.Value, _
                                                           OpenMode.ForWrite)

                ' Set the order based on the layout name
                Select Case acLayout.LayoutName
                    Case "Layout1"
                        acLayout.TabOrder = 2
                    Case "Layout2"
                        acLayout.TabOrder = 1
                    Case "Layout3"
                        acLayout.TabOrder = 0
                End Select
            Next

            ' Save the changes made
            acTrans.Commit()

            ' Regen the display so the new layout and correct order is displayed
            Application.DocumentManager.MdiActiveDocument.Editor.Regen()
        End Using
    End Sub

    ' Copy plot settings from layout to a pagesetup
    <CommandMethod("PageSetupCopyFromLayout")> _
    Public Shared Sub PageSetupCopyFromLayout()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        ' Specify the layout and pagesetup name (both layout and pagesetup must exist in the drawing)
        Dim layoutName As String = "Layout1"
        Dim pagesetupName As String = "MyPagesetup"

        ' Get the layout and plot settings of the named pagesetup
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Get the layout
            Dim acLayout As Layout
            acLayout = acTrans.GetObject(acLayoutMgr.GetLayoutId(layoutName), _
                                         OpenMode.ForRead)

            ' Get the PlotInfo from the layout
            Dim acPlInfo As PlotInfo = New PlotInfo()
            acPlInfo.Layout = acLayout.ObjectId

            Dim plSets As DBDictionary = acTrans.GetObject(acCurDb.PlotSettingsDictionaryId, OpenMode.ForRead)
            Dim plSet As PlotSettings

            ' Check to see if the pagesetup already exists
            If plSets.Contains(pagesetupName) = False Then

                ' Create a new PlotSettings object: True - model space, False - named layout
                plSet = New PlotSettings(acLayout.ModelType)

                ' Copy the plot settings from the layout
                plSet.CopyFrom(acLayout)

                ' Since the settinsg are coming from a layout, a name needs to be assigned to the plot settings
                plSet.PlotSettingsName = pagesetupName

                ' Add the plot settings to the dictionary and database
                plSet.AddToPlotSettingsDictionary(acCurDb)
                acTrans.AddNewlyCreatedDBObject(plSet, True)
            Else
                ' Get the existing plot settings that has the same name
                plSet = plSets.GetAt(pagesetupName).GetObject(OpenMode.ForWrite)

                ' Copy the plot settings to the existing pagesetup
                plSet.CopyFrom(acLayout)

                ' Set the name for the pagesetup as it is removed by copy the layout settings
                plSet.PlotSettingsName = pagesetupName
            End If

            ' Save the changes made
            acTrans.Commit()
        End Using
    End Sub

    <CommandMethod("PageSetupImportFromDrawing")> _
    Public Shared Sub PageSetupImportFromDrawing()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        ' Specify the pagesetup and drawing file to work with
        Dim pagesetupName As String = "DWF6_ARCH D_LAND"
        Dim filename As String = "C:\Program Files\Autodesk\AutoCAD 2013\Sample\Sheet Sets\Architectural\A-01.dwg"

        ' Create a new database object and open the drawing into memory
        Dim acExDb As Database = New Database(False, True)
        acExDb.ReadDwgFile(filename, FileOpenMode.OpenForReadAndAllShare, True, "")

        Dim plSetEx As PlotSettings = Nothing

        ' Get the plot settings of the named pagesetup from the external drawing file
        Using acTrans As Transaction = acExDb.TransactionManager.StartTransaction()
            Dim plSetsEx As DBDictionary = acTrans.GetObject(acExDb.PlotSettingsDictionaryId, OpenMode.ForRead)

            ' Check to see if the pagesetup exists in the external drawing
            If plSetsEx.Contains(pagesetupName) = True Then
                plSetEx = plSetsEx.GetAt(pagesetupName).GetObject(OpenMode.ForRead)
            End If

            ' Discard the changes made
            acTrans.Abort()
        End Using

        ' If the pagesetup existed in the external drawing, then proceed
        If IsNothing(plSetEx) = False Then
            Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
                Dim plSets As DBDictionary = acTrans.GetObject(acCurDb.PlotSettingsDictionaryId, OpenMode.ForRead)
                Dim plSet As PlotSettings

                ' Check to see if the pagesetup exists in the current drawing, if so create the new pagesetup
                If plSets.Contains(pagesetupName) = False Then
                    ' Create a new PlotSettings object: True - model space, False - named layout
                    plSet = New PlotSettings(False)
                    plSet.PlotSettingsName = pagesetupName
                    plSet.AddToPlotSettingsDictionary(acCurDb)
                    acTrans.AddNewlyCreatedDBObject(plSet, True)
                Else
                    ' Pagesetup exists, so lets update it
                    plSet = plSets.GetAt(pagesetupName).GetObject(OpenMode.ForWrite)
                End If

                ' Copy the settings from the pagesetup in the external drawing to the current drawing
                plSet.CopyFrom(plSetEx)

                ' Save the changes made
                acTrans.Commit()
            End Using
        Else
            acDoc.Editor.WriteMessage(vbLf & "Page setup '" & pagesetupName & "' could not be imported from '" & filename & "'.")
        End If

        ' Close the external drawing file
        acExDb.Dispose()
    End Sub

    <CommandMethod("PublishOverrideCustomWrite")> _
    Public Shared Sub PublishOverride()
        Using dsdDwgFiles As New DsdEntryCollection()
            ' Add first drawing file
            Using dsdDwgFile1 As DsdEntry = New DsdEntry()

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
            Using dsdDwgFile2 As DsdEntry = New DsdEntry()

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

            Dim fileNameDSD As String = Environment.GetFolderPath( _
                Environment.SpecialFolder.MyDocuments) & "\batchdrawings2.dsd"

            ' Create the drawing set description (DSD) file
            Dim sw As StreamWriter = File.CreateText(fileNameDSD)
            sw.WriteLine("[DWF6Version]")
            sw.WriteLine("Ver=1")
            sw.WriteLine("[DWF6MinorVersion]")
            sw.WriteLine("MinorVer=1")

            For Each dsdDwgFile As DsdEntry In dsdDwgFiles
                sw.WriteLine("[DWF6Sheet:" + dsdDwgFile.Title & "]")
                sw.WriteLine("DWG=" + dsdDwgFile.DwgName)
                sw.WriteLine("Layout=" + dsdDwgFile.Layout)
                sw.WriteLine("Setup=")
                sw.WriteLine("OriginalSheetPath=" & dsdDwgFile.DwgName)
                sw.WriteLine("Has Plot Port=0")
                sw.WriteLine("Has3DDWF=0")
            Next

            sw.WriteLine("[Target]")
            sw.WriteLine("Type=1")
            sw.WriteLine("DWF=" & Environment.GetFolderPath( _
                         Environment.SpecialFolder.MyDocuments) & "\MyPublish2.dwf")
            sw.WriteLine("OUT=" & Environment.GetFolderPath( _
                         Environment.SpecialFolder.MyDocuments) & "\")
            sw.WriteLine("PWD=")
            sw.WriteLine("[AutoCAD Block Data]")
            sw.WriteLine("IncludeBlockInfo=0")
            sw.WriteLine("BlockTmplFilePath=")
            sw.WriteLine("[SheetSet Properties]")
            sw.WriteLine("IsSheetSet=FALSE")
            sw.WriteLine("IsHomogeneous=FALSE")
            sw.WriteLine("SheetSet Name=")
            sw.WriteLine("NoOfCopies=1")
            sw.WriteLine("PlotStampOn=FALSE")
            sw.WriteLine("ViewFile=TRUE")
            sw.WriteLine("JobID=0")
            sw.WriteLine("SelectionSetName=")
            sw.WriteLine("AcadProfile=<<Unnamed Profile>>")
            sw.WriteLine("CategoryName=")
            sw.WriteLine("LogFilePath=")
            sw.WriteLine("IncludeLayer=TRUE")
            sw.WriteLine("LineMerge=FALSE")
            sw.WriteLine("CurrentPrecision=For Architecture")
            sw.WriteLine("PromptForDwfName=TRUE")
            sw.WriteLine("PwdProtectPublishedDWF=FALSE")
            sw.WriteLine("PromptForPwd=FALSE")
            sw.WriteLine("RepublishingMarkups=FALSE")
            sw.WriteLine("DSTPath=")
            sw.WriteLine("PublishSheetSetMetadata=FALSE")
            sw.WriteLine("PublishSheetMetadata=FALSE")
            sw.WriteLine("3DDWFOptions=0 0")

            sw.Flush()
            sw.Close()

            Try
                ' Get the DWF6 ePlot.pc3 and use it as a 
                ' device override for all the layouts
                Dim acPlCfg As PlotConfig = Nothing

                For Each acPlCfgInfo As PlotConfigInfo In PlotConfigManager.Devices
                    If acPlCfgInfo.DeviceName.ToUpper() = "DWF6 EPLOT.PC3" Then
                        acPlCfg = PlotConfigManager.SetCurrentConfig(acPlCfgInfo.DeviceName)
                        Exit For
                    End If
                Next

                Using dsdDataFile As DsdData = New DsdData
                    dsdDataFile.ReadDsd(Environment.GetFolderPath( _
                                        Environment.SpecialFolder.MyDocuments) & _
                                    "\batchdrawings2.dsd")

                    Application.Publisher.PublishExecute(dsdDataFile, acPlCfg)
                End Using

            Catch es As Autodesk.AutoCAD.Runtime.Exception
                MsgBox(es.Message)
            End Try
        End Using
    End Sub
End Class
