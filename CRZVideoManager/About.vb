Public Class About

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://elcris009.comule.com/Download_PS4VideoManager.html")
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        MsgBox("Seras enviado al Formulario de Soporte en linea" & vbCrLf & "You will be redirected to the Online Support Form", MsgBoxStyle.Information, "Online Support System")
        Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSe_OotTVusEoBPDA87sXjSI58wtCuEFK3BxM1e4kJLqvtB_pQ/viewform?usp=sf_link")
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim Webrowser As New WebBrowser
        Webrowser.ScriptErrorsSuppressed = True
        Webrowser.Navigate("https://www.dropbox.com/s/z9wlzvlxw5im0ou/PS4VideoManager%20Proyect.zip?dl=1")
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Process.Start("http://elcris009.comule.com/")
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Process.Start("http://worcomestudios.comule.com/")
    End Sub

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Debugger.DarkMode = True Then
            CheckBox1.CheckState = CheckState.Checked
        Else
            CheckBox1.CheckState = CheckState.Unchecked
        End If
        If Debugger.OfflineMode = True Then
            CheckBox2.CheckState = CheckState.Checked
        Else
            CheckBox2.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LangSelector.Show()
        LangSelector.Focus()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            Debugger.DarkMode = True
            Debugger.ActiveTheme(False)
        Else
            Debugger.DarkMode = False
            Debugger.ActiveTheme(True)
        End If
        Debugger.SaveConfig()
        My.Settings.Reload()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Checked Then
            Debugger.OfflineMode = True
        ElseIf CheckBox2.CheckState = CheckState.Unchecked Then
            Debugger.OfflineMode = False
        End If
        Debugger.SaveConfig()
        My.Settings.Reload()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs)
        Process.Start("https://www.youtube.com/channel/UCTEJ5nb5NoLD-qxodX5KRyA")
    End Sub
End Class