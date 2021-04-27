Public Class OW_BackUp
    Dim DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\PS4VideoManager\OW_VideoManager"
    Dim BackUpOn As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\"
    Dim NameBackUpFolder As String = "[BackUP]PS4_VideoManager_Overwatch"

    Private Sub OW_BackUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Debugger.DarkMode = True Then
            Debugger.ActiveTheme(False)
        Else
            Debugger.ActiveTheme(True)
        End If
    End Sub

#Region "CrearBackUp"
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Try
            If My.Computer.FileSystem.FileExists(BackUpOn & NameBackUpFolder) = True Then
                My.Computer.FileSystem.DeleteDirectory(BackUpOn, FileIO.DeleteDirectoryOption.DeleteAllContents)
                Threading.Thread.Sleep(50)
                My.Computer.FileSystem.CopyDirectory(DIRCommons, BackUpOn & NameBackUpFolder, True)
                If Debugger.Espanglish = "ESP" Then
                    MsgBox("Copia de Seguridad Creada!" & vbCrLf & "Fue creada en su Escritorio", MsgBoxStyle.Information, "Crear Copia de Seguridad")
                ElseIf Debugger.Espanglish = "ENG" Then
                    MsgBox("Backup created! " & vbCrLf & " It was created on your Desktop", MsgBoxStyle.Information, "Create BackUp")
                End If
                OW_Manager.NotifyIcon1.ShowBalloonTip(1, "Copias de Seguridad", "Copia de Seguridad Creada!", ToolTipIcon.Info)
                Process.Start(BackUpOn & NameBackUpFolder)
            ElseIf My.Computer.FileSystem.FileExists(BackUpOn & NameBackUpFolder) = False Then
                My.Computer.FileSystem.CopyDirectory(DIRCommons, BackUpOn & NameBackUpFolder, True)
                If Debugger.Espanglish = "ESP" Then
                    MsgBox("Copia de Seguridad Creada!" & vbCrLf & "Fue creada en su Escritorio", MsgBoxStyle.Information, "Crear Copia de Seguridad")
                ElseIf Debugger.Espanglish = "ENG" Then
                    MsgBox("Backup created! " & vbCrLf & " It was created on your Desktop", MsgBoxStyle.Information, "Create BackUp")
                End If
                OW_Manager.NotifyIcon1.ShowBalloonTip(1, "Copias de Seguridad", "Copia de Seguridad Creada!", ToolTipIcon.Info)
                Process.Start(BackUpOn & NameBackUpFolder)
            End If
            Button1.Enabled = True
            Me.Close()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Crear Copia de Seguridad")
        End Try
    End Sub
#End Region

#Region "LeerBackUp"
    Private Sub Panel1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel1.DragDrop
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                If MessageBox.Show("Esto eliminara la base de datos del Programa para leer la copia de seguridad" & vbCrLf & "This will remove the Program database to read the backup copy" & vbCrLf & "Do you want to continue?" & vbCrLf & "", "Abrir Copia de Seguridad | Load BackUp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    My.Computer.FileSystem.DeleteDirectory(DIRCommons, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    Threading.Thread.Sleep("50")
                    My.Computer.FileSystem.CreateDirectory(DIRCommons)
                    Threading.Thread.Sleep("50")
                    Dim strRutaArchivos() As String
                    Dim i As Integer
                    strRutaArchivos = e.Data.GetData(DataFormats.FileDrop)
                    For i = 0 To strRutaArchivos.Length - 1
                        My.Computer.FileSystem.CopyDirectory(strRutaArchivos(i), DIRCommons)
                    Next
                    If Debugger.Espanglish = "ESP" Then
                        MsgBox("La copia de seguridad fue leida correctamente!", MsgBoxStyle.Information, "Abrir Copia de Seguridad")
                    ElseIf Debugger.Espanglish = "ENG" Then
                        MsgBox("The backup was read correctly!", MsgBoxStyle.Information, "Load BackUp")
                    End If
                    OW_Manager.NotifyIcon1.ShowBalloonTip(1, "Copias de Seguridad", "Copia de Seguridad Leida!", ToolTipIcon.Info)
                    Debugger.Restart()
                Else
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Abrir Copia de Seguridad")
        End Try
    End Sub

    Private Sub Panel1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
#End Region
End Class
'El Programa es capaz de Crear una Copia de Seguridad y la Almacena automaticamente en la Carpeta Escritorio del Usuario.