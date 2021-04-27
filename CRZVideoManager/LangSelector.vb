Public Class LangSelector

    Private Sub LangSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DomainUpDown1.Text = "Idioma / Language" Then
            MsgBox("Elige un Idioma" & vbCrLf & "Select a Language", MsgBoxStyle.Critical, "Worcome Security")
        Else
            If DomainUpDown1.SelectedItem = "Español(España)" Then
                Idioma.Español.LANG_Español()
                Debugger.Espanglish = "ESP"
            ElseIf DomainUpDown1.SelectedItem = "English(Unites States)" Then
                Idioma.Ingles.LANG_English()
                Debugger.Espanglish = "ENG"
            End If
            Debugger.SaveConfig()
            My.Settings.Reload()
            OW_Manager.Show()
            Me.Hide()
        End If
    End Sub
End Class