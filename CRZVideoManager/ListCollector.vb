Public Class ListCollector

    Private Sub FileCollector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Debugger.DarkMode = True Then
            Debugger.ActiveTheme(False)
        Else
            Debugger.ActiveTheme(True)
        End If
        If Debugger.Lista = "L1" Then
            Button1.Enabled = False
            Button2.Enabled = True
            Button3.Enabled = True
        ElseIf Debugger.Lista = "L2" Then
            Button1.Enabled = True
            Button2.Enabled = False
            Button3.Enabled = True
        ElseIf Debugger.Lista = "L3" Then
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = False
        End If
        NumericUpDown1.Value = Debugger.UndexList
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OW_Manager.NotifyIcon1.ShowBalloonTip(1, "Cambio de Lista", "Cambio a Lista 1 Correctamente!", ToolTipIcon.Info)
        Debugger.CambioLista = True
        Debugger.Lista = "L1"
        Debugger.SaveConfig()
        Debugger.CambioDeLista()
        Threading.Thread.Sleep(50)
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OW_Manager.NotifyIcon1.ShowBalloonTip(1, "Cambio de Lista", "Cambio a Lista 2 Correctamente!", ToolTipIcon.Info)
        Debugger.CambioLista = True
        Debugger.Lista = "L2"
        Debugger.SaveConfig()
        Debugger.CambioDeLista()
        Threading.Thread.Sleep(50)
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OW_Manager.NotifyIcon1.ShowBalloonTip(1, "Cambio de Lista", "Cambio a Lista 3 Correctamente!", ToolTipIcon.Info)
        Debugger.CambioLista = True
        Debugger.Lista = "L3"
        Debugger.SaveConfig()
        Debugger.CambioDeLista()
        Threading.Thread.Sleep(50)
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        OW_Manager.NotifyIcon1.ShowBalloonTip(1, "Cambio de Lista", "Cambio a Lista " & NumericUpDown1.Value & " Correctamente!", ToolTipIcon.Info)
        Debugger.CambioLista = True
        Debugger.Lista = "L" & NumericUpDown1.Value
        Debugger.UndexList = NumericUpDown1.Value
        Debugger.SaveConfig()
        Debugger.CambioDeLista()
        Threading.Thread.Sleep(50)
        Me.Close()
    End Sub
End Class