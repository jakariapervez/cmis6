
' Main AutoCAD namespaces
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry

Public Class AU2012_Layouts
    ' List all the layouts in the current drawing
    <CommandMethod("LayoutList")> _
    Public Shared Sub LayoutList()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        ' Get the layout dictionary of the current database
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            Dim lays As DBDictionary = _
                acTrans.GetObject(acCurDb.LayoutDictionaryId, OpenMode.ForRead)

            acDoc.Editor.WriteMessage(vbLf & "Layouts:")

            ' Step through and list each named layout and Model
            For Each item As DBDictionaryEntry In lays
                acDoc.Editor.WriteMessage(vbLf & "  " & item.Key)
            Next

            ' Abort the changes to the database
            acTrans.Abort()
        End Using
    End Sub

    ' Create a new layout with the LayoutManager
    <CommandMethod("LayoutCreate")> _
    Public Shared Sub LayoutCreate()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        ' Get the layout and plot settings of the named pagesetup
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Reference the Layout Manager
            Dim acLayoutMgr As LayoutManager = LayoutManager.Current

            ' Create the new layout with default settings
            Dim objID As ObjectId = acLayoutMgr.CreateLayout("newLayout")

            ' Open the layout
            Dim acLayout As Layout = acTrans.GetObject(objID, _
                                                       OpenMode.ForRead)

            ' Set the layout current if it is not already
            If acLayout.TabSelected = False Then
                acLayoutMgr.CurrentLayout = acLayout.LayoutName
            End If

            ' Output some information related to the layout object
            acDoc.Editor.WriteMessage(vbLf & "Tab Order: " & acLayout.TabOrder & _
                                      vbLf & "Tab Selected: " & acLayout.TabSelected & _
                                      vbLf & "Block Table Record ID: " & _
                                      acLayout.BlockTableRecordId.ToString())

            ' Save the changes made
            acTrans.Commit()
        End Using
    End Sub

    ' Import a layout from an external drawing
    <CommandMethod("LayoutImport")> _
    Public Shared Sub LayoutImport()
        ' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        ' Specify the layout name and drawing file to work with
        Dim layoutName As String = "MAIN AND SECOND FLOOR PLAN"
        Dim filename As String = "C:\Program Files\Autodesk\AutoCAD 2013\" & _
                                 "Sample\Sheet Sets\Architectural\A-01.dwg"

        ' Create a new database object and open the drawing into memory
        Dim acExDb As Database = New Database(False, True)
        acExDb.ReadDwgFile(filename, FileOpenMode.OpenForReadAndAllShare, True, "")

        ' Create a transaction for the external drawing
        Using acTransEx As Transaction = acExDb.TransactionManager.StartTransaction()

            ' Get the layouts dictionary
            Dim layoutsEx As DBDictionary = _
                acTransEx.GetObject(acExDb.LayoutDictionaryId, OpenMode.ForRead)

            ' Check to see if the layout exists in the external drawing
            If layoutsEx.Contains(layoutName) = True Then

                ' Get the layout and block objects from the external drawing
                Dim layEx As Layout = _
                    layoutsEx.GetAt(layoutName).GetObject(OpenMode.ForRead)
                Dim blkBlkRecEx As BlockTableRecord = _
                    acTransEx.GetObject(layEx.BlockTableRecordId, OpenMode.ForRead)

                ' Get the objects from the block associated with the layout
                Dim idCol As ObjectIdCollection = New ObjectIdCollection()
                For Each id As ObjectId In blkBlkRecEx
                    idCol.Add(id)
                Next

                ' Create a transaction for the current drawing
                Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()

                    ' Get the block table and create a new block
                    ' then copy the objects between drawings
                    Dim blkTbl As BlockTable = _
                        acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForWrite)

                    Using blkBlkRec As New BlockTableRecord
                        blkBlkRec.Name = "*Paper_Space" & CStr(layoutsEx.Count() - 1)
                        blkTbl.Add(blkBlkRec)
                        acTrans.AddNewlyCreatedDBObject(blkBlkRec, True)
                        acExDb.WblockCloneObjects(idCol, _
                                                  blkBlkRec.ObjectId, _
                                                  New IdMapping(), _
                                                  DuplicateRecordCloning.Ignore, _
                                                  False)

                        ' Create a new layout and then copy properties between drawings
                        Dim layouts As DBDictionary = _
                            acTrans.GetObject(acCurDb.LayoutDictionaryId, OpenMode.ForWrite)

                        Using lay As New Layout
                            lay.LayoutName = layoutName
                            lay.AddToLayoutDictionary(acCurDb, blkBlkRec.ObjectId)
                            acTrans.AddNewlyCreatedDBObject(lay, True)
                            lay.CopyFrom(layEx)

                            Dim plSets As DBDictionary = _
                                acTrans.GetObject( _
                                    acCurDb.PlotSettingsDictionaryId, _
                                    OpenMode.ForRead)

                            ' Check to see if a named page setup was assigned to the layout,
                            ' if so then copy the page setup settings
                            If lay.PlotSettingsName <> "" Then

                                ' Check to see if the page setup exists
                                If plSets.Contains(lay.PlotSettingsName) = False Then
                                    plSets.UpgradeOpen()

                                    Using plSet As New PlotSettings(lay.ModelType)

                                        plSet.PlotSettingsName = lay.PlotSettingsName
                                        plSet.AddToPlotSettingsDictionary(acCurDb)
                                        acTrans.AddNewlyCreatedDBObject(plSet, True)

                                        Dim plSetsEx As DBDictionary = _
                                            acTransEx.GetObject( _
                                                acExDb.PlotSettingsDictionaryId, _
                                                OpenMode.ForRead)

                                        Dim plSetEx As PlotSettings = _
                                            plSetsEx.GetAt( _
                                                lay.PlotSettingsName).GetObject( _
                                                OpenMode.ForRead)

                                        plSet.CopyFrom(plSetEx)
                                    End Using
                                End If
                            End If
                        End Using
                    End Using

                    ' Regen the drawing to get the layout tab to display
                    acDoc.Editor.Regen()

                    ' Save the changes made
                    acTrans.Commit()
                End Using
            Else
                ' Display a message if the layout could not be found in the specified drawing
                acDoc.Editor.WriteMessage(vbLf & "Layout '" & layoutName & _
                                          "' could not be imported from '" & filename & "'.")
            End If

            ' Discard the changes made to the external drawing file
            acTransEx.Abort()
        End Using

        ' Close the external drawing file
        acExDb.Dispose()
    End Sub
End Class
