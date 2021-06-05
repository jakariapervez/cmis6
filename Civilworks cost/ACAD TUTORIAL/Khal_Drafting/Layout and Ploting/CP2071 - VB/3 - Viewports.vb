' Standard .NET namespaces
Imports System.Runtime.InteropServices

' Main AutoCAD namespaces
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.GraphicsInterface
Imports Autodesk.AutoCAD

Public Class AU2012_Viewports
    ' Used to set a viewport current
    <DllImport("accore.dll", CallingConvention:=CallingConvention.Cdecl, _
     EntryPoint:="?acedSetCurrentVPort@@YA?AW4ErrorStatus@Acad@@PEBVAcDbViewport@@@Z")> _
    Public Shared Function acedSetCurrentVPort(ByVal AcDbVport As IntPtr) As IntPtr
    End Function

    ' Used to create a rectangular and nonrectangular viewports
    <CommandMethod("ViewportsCreateFloating")> _
    Public Sub ViewportsCreateFloating()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            ' Open the Block table for read
            Dim acBlkTbl As BlockTable = acTrans.GetObject(acCurDb.BlockTableId, _
                                                           OpenMode.ForRead)

            ' Open the Block table record Paper space for write
            Dim acBlkTblRec As BlockTableRecord = _
                acTrans.GetObject(acBlkTbl(BlockTableRecord.PaperSpace), _
                                  OpenMode.ForWrite)

            ' Switch to the previous Paper space layout
            Application.SetSystemVariable("TILEMODE", 0)
            acDoc.Editor.SwitchToPaperSpace()

            ' Remove any viewports that already exist
            For Each objId As ObjectId In acBlkTblRec
                Dim dbObj As DBObject = acTrans.GetObject(objId, OpenMode.ForRead)

                ' Remove any viewports in the block
                If TypeOf dbObj Is DatabaseServices.Viewport Then
                    dbObj.UpgradeOpen()
                    dbObj.Erase(True)
                End If
            Next

            ' Create a Viewport
            Using acVport1 As DatabaseServices.Viewport = New DatabaseServices.Viewport()
                ' Set the center point and size of the viewport
                acVport1.CenterPoint = New Point3d(3.75, 4, 0)
                acVport1.Width = 7.5
                acVport1.Height = 7.5

                ' Lock the viewport
                acVport1.Locked = True

                ' Set the scale to 1" = 4'
                acVport1.CustomScale = 48

                ' Set visual style
                Dim vStyles As DBDictionary = _
                    acTrans.GetObject(acCurDb.VisualStyleDictionaryId, _
                                      OpenMode.ForRead)

                acVport1.SetShadePlot(ShadePlotType.VisualStyle, _
                                      vStyles.GetAt("Sketchy"))

                ' Add the new object to the block table record and the transaction
                acBlkTblRec.AppendEntity(acVport1)
                acTrans.AddNewlyCreatedDBObject(acVport1, True)

                ' Change the view direction and enable the viewport
                acVport1.ViewDirection = New Vector3d(-1, -1, 1)
                acVport1.On = True

                ' Create a rectangular viewport to change to a non-rectangular viewport
                Using acVport2 As DatabaseServices.Viewport = New DatabaseServices.Viewport()
                    acVport2.CenterPoint = New Point3d(9, 6.5, 0)
                    acVport2.Width = 2.5
                    acVport2.Height = 2.5

                    ' Set the scale to 1" = 8'
                    acVport2.CustomScale = 96

                    ' Set render preset
                    Dim namedObjs As DBDictionary = _
                        acTrans.GetObject(acCurDb.NamedObjectsDictionaryId, _
                                          OpenMode.ForRead)

                    ' Check to see if the Render Settings dictionary already exists
                    Dim renderSettings As DBDictionary
                    If namedObjs.Contains("ACAD_RENDER_PLOT_SETTINGS") = True Then
                        renderSettings = acTrans.GetObject( _
                            namedObjs.GetAt("ACAD_RENDER_PLOT_SETTINGS"), _
                            OpenMode.ForWrite)
                    Else
                        ' If it does not exist, create it and add it to the drawing
                        namedObjs.UpgradeOpen()
                        renderSettings = New DBDictionary
                        namedObjs.SetAt("ACAD_RENDER_PLOT_SETTINGS", renderSettings)
                        acTrans.AddNewlyCreatedDBObject(renderSettings, True)
                    End If

                    ' Create the new render preset, based on the settings 
                    ' of the Medium render preset
                    Using renderSetting As New MentalRayRenderSettings
                        GetDefaultRenderPreset(renderSetting, "Medium")

                        renderSetting.Name = "Medium"
                        renderSettings.SetAt("Medium", renderSetting)
                        acTrans.AddNewlyCreatedDBObject(renderSetting, True)

                        acVport2.SetShadePlot(ShadePlotType.RenderPreset, _
                                              renderSetting.ObjectId)
                    End Using

                    ' Create a circle
                    Using acCirc As Circle = New Circle()
                        acCirc.Center = acVport2.CenterPoint
                        acCirc.Radius = 1.25

                        ' Add the new object to the block table record and the transaction
                        acBlkTblRec.AppendEntity(acCirc)
                        acTrans.AddNewlyCreatedDBObject(acCirc, True)

                        ' Clip the viewport using the circle  
                        acVport2.NonRectClipEntityId = acCirc.ObjectId
                        acVport2.NonRectClipOn = True
                    End Using

                    ' Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acVport2)
                    acTrans.AddNewlyCreatedDBObject(acVport2, True)

                    ' Change the view direction
                    acVport2.ViewDirection = New Vector3d(0, 0, 1)

                    ' Enable the viewport
                    acVport2.On = True
                End Using

                ' Activate model space
                acDoc.Editor.SwitchToModelSpace()

                ' Set the new viewport current via an imported ObjectARX function
                acedSetCurrentVPort(acVport1.UnmanagedObject)
            End Using

            ' Save the new objects to the database
            acTrans.Commit()
        End Using
    End Sub

    ' Method used to populate a MentalRayRenderSettings object with the
    ' same settings used by the standard render presets
    Private Shared Sub GetDefaultRenderPreset( _
                       ByRef renderPreset As MentalRayRenderSettings, _
                       ByVal name As String)
        ' Set the values common to multiple default render presets
        renderPreset.BackFacesEnabled = False
        renderPreset.DiagnosticBackgroundEnabled = False
        renderPreset.DiagnosticBSPMode = _
            DiagnosticBSPMode.Depth
        renderPreset.DiagnosticGridMode = _
            New MentalRayRenderSettingsTraitsDiagnosticGridModeParameter( _
                DiagnosticGridMode.Object, 10.0)

        renderPreset.DiagnosticMode = _
            DiagnosticMode.Off
        renderPreset.DiagnosticPhotonMode = _
            DiagnosticPhotonMode.Density
        renderPreset.DisplayIndex = 0
        renderPreset.EnergyMultiplier = 1.0
        renderPreset.ExportMIEnabled = False
        renderPreset.ExportMIFileName = ""
        renderPreset.FGRayCount = 100

        ' FGSampleRadius cannot be set, it returns invalid input
        renderPreset.FGSampleRadiusState = _
            New MentalRayRenderSettingsTraitsBoolParameter( _
                False, False, False)

        renderPreset.FinalGatheringEnabled = False
        renderPreset.FinalGatheringMode = _
            FinalGatheringMode.FinalGatherOff
        renderPreset.GIPhotonsPerLight = 1000
        renderPreset.GISampleCount = 500
        renderPreset.GISampleRadius = 1.0
        renderPreset.GISampleRadiusEnabled = False
        renderPreset.GlobalIlluminationEnabled = False
        renderPreset.LightLuminanceScale = 1500.0
        renderPreset.MaterialsEnabled = True
        renderPreset.MemoryLimit = 1048

        renderPreset.PhotonTraceDepth = _
            New MentalRayRenderSettingsTraitsTraceParameter( _
                5, 5, 5)
        renderPreset.PreviewImageFileName = ""
        renderPreset.RayTraceDepth = _
            New MentalRayRenderSettingsTraitsTraceParameter( _
                3, 3, 3)
        renderPreset.RayTracingEnabled = False
        renderPreset.Sampling = _
            New MentalRayRenderSettingsTraitsIntegerRangeParameter( _
                -2, -1)
        renderPreset.SamplingContrastColor = _
            New MentalRayRenderSettingsTraitsFloatParameter( _
                0.1, 0.1, 0.1, 0.1)
        renderPreset.SamplingFilter = _
            New MentalRayRenderSettingsTraitsSamplingParameter( _
                Filter.Box, 1.0, 1.0)

        renderPreset.ShadowMapsEnabled = False
        renderPreset.ShadowMode = ShadowMode.Simple
        renderPreset.ShadowSamplingMultiplier = _
            ShadowSamplingMultiplier.SamplingMultiplierZero
        renderPreset.ShadowsEnabled = True
        renderPreset.TextureSampling = False
        renderPreset.TileOrder = TileOrder.Hilbert
        renderPreset.TileSize = 32

        Select Case name.ToUpper()
            ' Assigns the values to match the Draft render preset
            Case "DRAFT"
                renderPreset.Description = _
                    "The lowest rendering quality which entails no raytracing, " & _
                    "no texture filtering and force 2-sided is inactive."
                renderPreset.Name = "Draft"
            Case ("LOW")
                renderPreset.Description = _
                    "Rendering quality is improved over Draft. " & _
                    "Low anti-aliasing and a raytracing depth of 3 " & _
                    "reflection/refraction are processed."
                renderPreset.Name = "Low"

                renderPreset.RayTracingEnabled = True

                renderPreset.Sampling = _
                    New MentalRayRenderSettingsTraitsIntegerRangeParameter( _
                        -1, 0)
                renderPreset.SamplingContrastColor = _
                    New MentalRayRenderSettingsTraitsFloatParameter( _
                        0.1, 0.1, 0.1, 0.1)
                renderPreset.SamplingFilter = _
                    New MentalRayRenderSettingsTraitsSamplingParameter( _
                        Filter.Triangle, 2.0, 2.0)

                renderPreset.ShadowSamplingMultiplier = _
                    ShadowSamplingMultiplier.SamplingMultiplierOneFourth
            Case "MEDIUM"
                renderPreset.BackFacesEnabled = True
                renderPreset.Description = _
                    "Rendering quality is improved over Low to include " & _
                    "texture filtering and force 2-sided is active. " & _
                    "Moderate anti-aliasing and a raytracing depth of " & _
                    "5 reflections/refractions are processed."

                renderPreset.FGRayCount = 200
                renderPreset.FinalGatheringMode = _
                    FinalGatheringMode.FinalGatherAuto
                renderPreset.GIPhotonsPerLight = 10000

                renderPreset.Name = "Medium"
                renderPreset.RayTraceDepth = _
                    New MentalRayRenderSettingsTraitsTraceParameter( _
                        5, 5, 5)
                renderPreset.RayTracingEnabled = True
                renderPreset.Sampling = _
                    New MentalRayRenderSettingsTraitsIntegerRangeParameter( _
                        0, 1)
                renderPreset.SamplingContrastColor = _
                    New MentalRayRenderSettingsTraitsFloatParameter( _
                        0.05, 0.05, 0.05, 0.05)
                renderPreset.SamplingFilter = _
                    New MentalRayRenderSettingsTraitsSamplingParameter( _
                        Filter.Gauss, 3.0, 3.0)

                renderPreset.ShadowSamplingMultiplier = _
                    ShadowSamplingMultiplier.SamplingMultiplierOneHalf
                renderPreset.TextureSampling = True
            Case "HIGH"
                renderPreset.BackFacesEnabled = True
                renderPreset.Description = _
                    "Rendering quality is improved over Medium. " & _
                    "High anti-aliasing and a raytracing depth of 7 " & _
                    "reflections/refractions are processed."

                renderPreset.FGRayCount = 500
                renderPreset.FinalGatheringMode = _
                    FinalGatheringMode.FinalGatherAuto
                renderPreset.GIPhotonsPerLight = 10000

                renderPreset.Name = "High"
                renderPreset.RayTraceDepth = _
                    New MentalRayRenderSettingsTraitsTraceParameter( _
                        7, 7, 7)
                renderPreset.RayTracingEnabled = True
                renderPreset.Sampling = _
                    New MentalRayRenderSettingsTraitsIntegerRangeParameter( _
                        0, 2)
                renderPreset.SamplingContrastColor = _
                    New MentalRayRenderSettingsTraitsFloatParameter( _
                        0.05, 0.05, 0.05, 0.05)
                renderPreset.SamplingFilter = _
                    New MentalRayRenderSettingsTraitsSamplingParameter( _
                        Filter.Mitchell, 4.0, 4.0)

                renderPreset.ShadowSamplingMultiplier = _
                    ShadowSamplingMultiplier.SamplingMultiplierOne
                renderPreset.TextureSampling = True
            Case "PRESENTATION"
                renderPreset.BackFacesEnabled = True
                renderPreset.Description = _
                    "Rendering quality is improved over High. " & _
                    "Very high anti-aliasing and a raytracing depth of 9 " & _
                    "reflections/refractions are processed."

                renderPreset.FGRayCount = 1000
                renderPreset.FinalGatheringMode = _
                    FinalGatheringMode.FinalGatherAuto
                renderPreset.GIPhotonsPerLight = 10000

                renderPreset.Name = "Presentation"
                renderPreset.RayTraceDepth = _
                    New MentalRayRenderSettingsTraitsTraceParameter( _
                        9, 9, 9)
                renderPreset.RayTracingEnabled = True
                renderPreset.Sampling = _
                    New MentalRayRenderSettingsTraitsIntegerRangeParameter( _
                        1, 2)
                renderPreset.SamplingContrastColor = _
                    New MentalRayRenderSettingsTraitsFloatParameter( _
                        0.05, 0.05, 0.05, 0.05)
                renderPreset.SamplingFilter = _
                    New MentalRayRenderSettingsTraitsSamplingParameter( _
                        Filter.Lanczos, 4.0, 4.0)

                renderPreset.ShadowSamplingMultiplier = _
                    ShadowSamplingMultiplier.SamplingMultiplierOne
                renderPreset.TextureSampling = True
        End Select
    End Sub
End Class
