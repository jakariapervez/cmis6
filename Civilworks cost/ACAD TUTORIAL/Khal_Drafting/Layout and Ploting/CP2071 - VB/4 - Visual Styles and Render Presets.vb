' Main AutoCAD namespaces
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.GraphicsInterface

Public Class AU2012_VisualStyles_RenderPresets
    ' Lists the available visual styles
    <CommandMethod("VisualStyleList")> _
    Public Shared Sub VisualStyleList()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            Dim vStyles As DBDictionary = _
                acTrans.GetObject(acCurDb.VisualStyleDictionaryId, _
                                  OpenMode.ForRead)

            ' Output a message to the Command Line history
            acDoc.Editor.WriteMessage(vbLf & "Visual styles: ")

            ' Step through the dictionary
            For Each entry As DBDictionaryEntry In vStyles
                ' Get the dictionary entry
                Dim vStyle As DBVisualStyle = _
                    vStyles.GetAt(entry.Key).GetObject(OpenMode.ForRead)

                ' If the visual style is not marked for internal use then output its name
                If vStyle.InternalUseOnly = False Then
                    ' Output the name of the visual style
                    acDoc.Editor.WriteMessage(vbLf & "  " & vStyle.Name)
                End If
            Next
        End Using
    End Sub

    ' Creates a new visual style
    <CommandMethod("VisualStyleCreate")> _
    Public Shared Sub VisualStyleCreate()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            Dim vStyles As DBDictionary = _
                acTrans.GetObject(acCurDb.VisualStyleDictionaryId, _
                                  OpenMode.ForRead)

            Try
                ' Check to see if the "MyVS" exists or not
                Dim vStyle As DBVisualStyle
                If vStyles.Contains("MyVS") = True Then
                    vStyle = acTrans.GetObject(vStyles.GetAt("MyVS"), _
                                               OpenMode.ForWrite)
                Else
                    vStyles.UpgradeOpen()

                    ' Create the visual style
                    vStyle = New DBVisualStyle
                    vStyles.SetAt("MyVS", vStyle)

                    ' Add the visual style to the dictionary
                    acTrans.AddNewlyCreatedDBObject(vStyle, True)
                End If

                ' Set the description of the visual style
                vStyle.Description = "My Visual Style"
                vStyle.Type = VisualStyleType.Custom

                ' Face Settings (Opacity, Face Style, Lighting Quality, Color, 
                '                Monochrome color, Opacity, and Material Display)
                vStyle.SetTrait(VisualStyleProperty.FaceModifier, _
                                VSFaceModifiers.FaceOpacityFlag, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.FaceLightingModel, _
                                VSFaceLightingModel.Gooch, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.FaceLightingQuality, _
                                VSFaceLightingQuality.PerPixelLighting, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.FaceColorMode, _
                                VSFaceColorMode.ObjectColor, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.FaceMonoColor, _
                                Color.FromColorIndex(ColorMethod.ByAci, 1), _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.FaceOpacity, 0.5, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.DisplayStyle, _
                                VSDisplayStyles.MaterialsFlag + _
                                VSDisplayStyles.TexturesFlag, _
                                VisualStyleOperation.Set)

                ' Lighting (Enable Highlight Intensity, 
                '           Highlight Intensity, and Shadow Display)
                vStyle.SetTrait(VisualStyleProperty.FaceModifier, _
                                vStyle.GetTrait(VisualStyleProperty.FaceModifier).Int + _
                                VSFaceModifiers.SpecularFlag, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.DisplayStyle, _
                                vStyle.GetTrait(VisualStyleProperty.DisplayStyle).Int + _
                                VSDisplayStyles.LightingFlag, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.FaceSpecular, _
                                45.0, VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.DisplayShadowType, _
                                VSDisplayShadowType.Full, _
                                VisualStyleOperation.Set)

                ' Environment Settings (Backgrounds)
                vStyle.SetTrait(VisualStyleProperty.DisplayStyle, _
                                vStyle.GetTrait(VisualStyleProperty.DisplayStyle).Int + _
                                VSDisplayStyles.BackgroundsFlag, _
                                VisualStyleOperation.Set)

                ' Edge Settings (Show, Number of Lines, Color, and Always on Top)
                vStyle.SetTrait(VisualStyleProperty.EdgeModel, VSEdgeModel.Isolines, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeIsolines, _
                                6, VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeColor, _
                                Color.FromColorIndex(ColorMethod.ByAci, 2), _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeModifier, _
                                vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int + _
                                VSEdgeModifiers.AlwaysOnTopFlag, _
                                VisualStyleOperation.Set)

                ' Occluded Edges (Show, Color, and Linetype)
                If Not (vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int And _
                        VSEdgeStyles.ObscuredFlag) > 0 Then
                    vStyle.SetTrait(VisualStyleProperty.EdgeStyle, _
                                    vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int + _
                                    VSEdgeStyles.ObscuredFlag, _
                                    VisualStyleOperation.Set)
                End If
                vStyle.SetTrait(VisualStyleProperty.EdgeObscuredColor, _
                                Color.FromColorIndex(ColorMethod.ByAci, 3), _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeObscuredLinePattern, _
                                VSEdgeLinePattern.DoubleMediumDash, _
                                VisualStyleOperation.Set)

                ' Intersection Edges (Color and Linetype)
                If Not (vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int And _
                        VSEdgeStyles.IntersectionFlag) > 0 Then
                    vStyle.SetTrait(VisualStyleProperty.EdgeStyle, _
                                    vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int + _
                                    VSEdgeStyles.IntersectionFlag, _
                                    VisualStyleOperation.Set)
                End If
                vStyle.SetTrait(VisualStyleProperty.EdgeIntersectionColor, _
                                Color.FromColorIndex(ColorMethod.ByAci, 4), _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeIntersectionLinePattern, _
                                VSEdgeLinePattern.ShortDash, _
                                VisualStyleOperation.Set)

                ' Silhouette Edges (Color and Width)
                If Not (vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int And _
                        VSEdgeStyles.SilhouetteFlag) > 0 Then
                    vStyle.SetTrait(VisualStyleProperty.EdgeStyle, _
                                    vStyle.GetTrait(VisualStyleProperty.EdgeStyle).Int + _
                                    VSEdgeStyles.SilhouetteFlag, _
                                    VisualStyleOperation.Set)
                End If
                vStyle.SetTrait(VisualStyleProperty.EdgeSilhouetteColor, _
                                Color.FromColorIndex(ColorMethod.ByAci, 5), _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeSilhouetteWidth, 2, _
                                VisualStyleOperation.Set)

                ' Edge Modifiers (Enable Line Extensions, Enable Jitter, 
                '                 Line Extensions, Jitter, Crease Angle, 
                '                 and Halo Gap)
                If Not (vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int And _
                        VSEdgeModifiers.EdgeOverhangFlag) > 0 Then
                    vStyle.SetTrait(VisualStyleProperty.EdgeModifier, _
                                    vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int + _
                                    VSEdgeModifiers.EdgeOverhangFlag, _
                                    VisualStyleOperation.Set)
                End If
                If Not (vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int And _
                        VSEdgeModifiers.EdgeJitterFlag) > 0 Then
                    vStyle.SetTrait(VisualStyleProperty.EdgeModifier, _
                                    vStyle.GetTrait(VisualStyleProperty.EdgeModifier).Int + _
                                    VSEdgeModifiers.EdgeJitterFlag, _
                                    VisualStyleOperation.Set)
                End If
                vStyle.SetTrait(VisualStyleProperty.EdgeOverhang, 3, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeJitterAmount, _
                                VSEdgeJitterAmount.JitterMedium, _
                                VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeCreaseAngle, _
                                0.3, VisualStyleOperation.Set)
                vStyle.SetTrait(VisualStyleProperty.EdgeHaloGap, _
                                5, VisualStyleOperation.Set)
            Catch es As Autodesk.AutoCAD.Runtime.Exception
                MsgBox(es.Message)
            Finally
                acTrans.Commit()
            End Try
        End Using
    End Sub

    ' Utility to output a property value
    ' Usage: OutputVSPropValue(vStyle, VisualStyleProperty.EdgeHaloGap)
    Private Shared Sub OutputVSPropValue(vStyle As DBVisualStyle, _
                                         vsProp As VisualStyleProperty)
        ' Get the current document
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument

        Dim gIntVar As Autodesk.AutoCAD.GraphicsInterface.Variant = vStyle.GetTrait(vsProp)

        acDoc.Editor.WriteMessage(vbLf & gIntVar.Type.ToString() & ": ")

        If gIntVar.Type = Autodesk.AutoCAD.GraphicsInterface.VariantType.Boolean Then
            acDoc.Editor.WriteMessage(gIntVar.Boolean.ToString())
        ElseIf gIntVar.Type = Autodesk.AutoCAD.GraphicsInterface.VariantType.Color Then
            acDoc.Editor.WriteMessage(gIntVar.Color.ColorIndex.ToString())
        ElseIf gIntVar.Type = Autodesk.AutoCAD.GraphicsInterface.VariantType.Double Then
            acDoc.Editor.WriteMessage(gIntVar.Double.ToString())
        ElseIf gIntVar.Type = Autodesk.AutoCAD.GraphicsInterface.VariantType.Int Then
            acDoc.Editor.WriteMessage(gIntVar.Int.ToString())
        ElseIf gIntVar.Type = Autodesk.AutoCAD.GraphicsInterface.VariantType.String Then
            acDoc.Editor.WriteMessage(gIntVar.String.ToString())
        End If
    End Sub

    ' Lists the available render presets
    <CommandMethod("RenderPresetsList")> _
    Public Shared Sub RenderPresetsList()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            Dim namedObjs As DBDictionary = _
                acTrans.GetObject(acCurDb.NamedObjectsDictionaryId, _
                                  OpenMode.ForRead)

            ' Output a message to the Command Line history
            acDoc.Editor.WriteMessage(vbLf & "Default render presets: ")

            ' List the default render presets that are defined 
            ' as part of the application
            acDoc.Editor.WriteMessage(vbLf & "  Draft")
            acDoc.Editor.WriteMessage(vbLf & "  Low")
            acDoc.Editor.WriteMessage(vbLf & "  Medium")
            acDoc.Editor.WriteMessage(vbLf & "  High")
            acDoc.Editor.WriteMessage(vbLf & "  Presentation")

            ' Check to see if the "ACAD_RENDER_SETTINGS" named dictionary exists
            If namedObjs.Contains("ACAD_RENDER_SETTINGS") = True Then
                ' Open the named dictionary
                Dim renderSettings As DBDictionary = _
                    acTrans.GetObject(namedObjs.GetAt("ACAD_RENDER_SETTINGS"), _
                                      OpenMode.ForRead)

                ' Output a message to the Command Line history
                acDoc.Editor.WriteMessage(vbLf & "Custom render presets: ")

                ' Step through and list each of the custom render presets
                For Each entry As DBDictionaryEntry In renderSettings
                    Dim renderSetting As MentalRayRenderSettings = _
                        acTrans.GetObject(entry.Value, OpenMode.ForRead)

                    ' Output the name of the custom render preset
                    acDoc.Editor.WriteMessage(vbLf & "  " & renderSetting.Name)
                Next
            Else
                ' If no custom render presets exist, then output the following message
                acDoc.Editor.WriteMessage(vbLf & _
                                          "No custom render presets available.")
            End If

            ' Check to see if the "ACAD_RENDER_PLOT_SETTINGS" named dictionary exists
            If namedObjs.Contains("ACAD_RENDER_PLOT_SETTINGS") = True Then
                ' Open the named dictionary
                Dim renderSettings As DBDictionary = _
                    acTrans.GetObject(namedObjs.GetAt("ACAD_RENDER_PLOT_SETTINGS"), _
                                      OpenMode.ForRead)

                ' Output a message to the Command Line history
                acDoc.Editor.WriteMessage(vbLf & "Custom render plot presets: ")

                ' Step through and list each of the custom render presets
                For Each entry As DBDictionaryEntry In renderSettings
                    Dim renderSetting As MentalRayRenderSettings = _
                        acTrans.GetObject(entry.Value, OpenMode.ForRead)

                    ' Output the name of the custom render preset
                    acDoc.Editor.WriteMessage(vbLf & "  " & renderSetting.Name)
                Next
            Else
                ' If no custom render plot presets exist, then output the following message
                acDoc.Editor.WriteMessage(vbLf & _
                                          "No custom render plot presets available.")
            End If

            ' Discard any changes
            acTrans.Abort()
        End Using
    End Sub

    ' Creates a new render preset
    <CommandMethod("RenderPresetsCreate")> _
    Public Shared Sub RenderPresetCreate()
        ' Get the current document and database, and start a transaction
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
            Dim namedObjs As DBDictionary = _
                acTrans.GetObject(acCurDb.NamedObjectsDictionaryId, OpenMode.ForRead)

            Try
                ' Check to see if the Render Settings dictionary already exists
                Dim renderSettings As DBDictionary
                If namedObjs.Contains("ACAD_RENDER_SETTINGS") = True Then
                    renderSettings = _
                        acTrans.GetObject(namedObjs.GetAt("ACAD_RENDER_SETTINGS"), _
                                          OpenMode.ForWrite)
                Else
                    ' If it does not exist, create it and add it to the drawing
                    namedObjs.UpgradeOpen()
                    renderSettings = New DBDictionary
                    namedObjs.SetAt("ACAD_RENDER_SETTINGS", renderSettings)
                    acTrans.AddNewlyCreatedDBObject(renderSettings, True)
                End If

                ' Create the new render preset, based on 
                ' the settings of the Medium render preset
                If renderSettings.Contains("MyPreset") = False Then
                    Using renderSetting As New MentalRayRenderSettings
                        GetDefaultRenderPreset(renderSetting, "Medium")

                        renderSetting.Name = "MyPreset"
                        renderSetting.Description = "Custom new render preset"
                        renderSettings.SetAt("MyPreset", renderSetting)
                        acTrans.AddNewlyCreatedDBObject(renderSetting, True)
                    End Using
                End If

                ' Set the new render preset current
                Application.UIBindings.RenderEngine.CurrentRenderPresetName = _
                    "MyPreset"

            Catch es As Autodesk.AutoCAD.Runtime.Exception
                MsgBox(es.Message)
            Finally
                acTrans.Commit()
            End Try
        End Using
    End Sub

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

    <CommandMethod("RenderPresetsSetCurrent")> _
    Public Shared Sub RenderPresetsSetCurrent()
        ' Set the Draft render preset current
        Application.UIBindings.RenderEngine.CurrentRenderPresetName = "Draft"
    End Sub
End Class
