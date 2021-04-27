Public Class SaveChangesDialog
    Private Sub SaveChangesDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If OW_Manager.NewChanges = 0 Then
            If Debugger.Espanglish = "ESP" Then
                Label3.Text = "Ninguno"
            ElseIf Debugger.Espanglish = "ENG" Then
                Label3.Text = "Nothing"
            End If
        Else
            Label3.Text = OW_Manager.NewChanges
        End If
        If Debugger.DarkMode = True Then
            Debugger.ActiveTheme(False)
        Else
            Debugger.ActiveTheme(True)
        End If
        If Debugger.NoShowWithoutChanges = True Then
            CheckBox1.CheckState = CheckState.Checked
        Else
            CheckBox1.CheckState = CheckState.Unchecked
        End If
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    End Sub

    Private Sub SaveAndExit_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            OW_Manager.GuardaArchivosDelListBoxPOTGs()
            OW_Manager.GuardaArchivosDelListBoxDESTACADOs()
            OW_Manager.GuardaArchivosDelListBoxGAMEPLAYs()
            OW_Manager.GuardaArchivosDelListBoxFAVORITOs()
            Threading.Thread.Sleep(150)
            If Debugger.CambioLista = False Then
                OW_Manager.Dispose()
                Debugger.Close()
            ElseIf Debugger.CambioLista = True Then
                OW_Manager.Dispose()
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al guardar los Cambios antes de Salir")
        End Try
    End Sub

    Private Sub ExitWithoutSave_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Debugger.CambioLista = False Then
            OW_Manager.Dispose()
            Debugger.Close()
        ElseIf Debugger.CambioLista = True Then
            OW_Manager.Dispose()
            Me.Close()
        End If
    End Sub

    Private Sub Volver_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OW_Manager.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            OW_Manager.GuardaArchivosDelListBoxPOTGs()
            OW_Manager.GuardaArchivosDelListBoxDESTACADOs()
            OW_Manager.GuardaArchivosDelListBoxGAMEPLAYs()
            OW_Manager.GuardaArchivosDelListBoxFAVORITOs()
            Threading.Thread.Sleep(150)
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al guardar los Cambios antes de Salir")
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            Debugger.NoShowWithoutChanges = True
        Else
            Debugger.NoShowWithoutChanges = False
        End If
        Debugger.SaveConfig()
    End Sub
End Class