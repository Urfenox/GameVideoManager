Imports System.IO
Public Class OW_Manager
#Region "Uses"
    Dim DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\PS4VideoManager" 'Define la Ruta Raiz de la Aplicacion
    Dim ListaUso As String  'Define la Lista que se Usara
    Dim DIROverwatch As String  'Define la Ruta de Acceso de las Categorias de Overwatch
    Dim DIRLePOTGs As String  'Define la Ruta de Acceso de Jugadas de la Partida
    Dim DIRLeDestacados As String  'Define la Ruta de Acceso de Destacados
    Dim DIRLeGameplays As String  'Define la Ruta de Acceso de Gameplays
    Dim DIRLeFavoritos As String 'Define la Ruta de Acceso de Favoritos
    Dim DirVideoPatch As String 'Define la Carpeta en donde esta almacenado un Video
    Dim AutoPlay As Boolean = False 'Define si el usuario quiere una reproduccion continua de la categoria
    Dim AutoPlayListSelected As String = Nothing 'Define la Lista que esta viendo el Usuario
    Public NewChanges As Integer = 0 'Define el numero de cambios que se han realizado

    Public MemoryListPOTG As New ArrayList 'Declaro una lista en memoria para los videos de la categoria Jugadas de la Partida
    Public MemoryListHightlights As New ArrayList 'Declaro una lista en memoria para los videos de la categoria Destacados
    Public MemoryListGameplays As New ArrayList 'Declaro una lista en memoria para los videos de la categoria Gameplays
    Public MemoryListFavorites As New ArrayList 'Declaro una lista en memoria para los videos de la categoria Favoritos
    Public ContadorPOTG As Integer = -1 'Declaro un contador para navegar en la lista en memoria para Jugadas de la Partida
    Public ContadorHightlights As Integer = -1 'Declaro un contador para navegar en la lista en memoria para Destacados
    Public ContadorGameplays As Integer = -1 'Declaro un contador para navegar en la lista en memoria para Gameplays
    Public ContadorFavorites As Integer = -1 'Declaro un contador para navegar en la lista en memoria para Favoritos
    Dim FPOSICIONI, FPOSICIONF As Integer 'Declaro una posicion inicial y final (Para mover items en la lista Favoritos)
    Dim PPOSICIONI, PPOSICIONF As Integer 'Declaro una posicion inicial y final (Para mover items en la lista POTG)
    Dim HPOSICIONI, HPOSICIONF As Integer 'Declaro una posicion inicial y final (Para mover items en la lista Hightlights)
    Dim GPOSICIONI, GPOSICIONF As Integer 'Declaro una posicion inicial y final (Para mover items en la lista Gameplays)
    Dim Array As String = Nothing 'Array libre para cualquier uso, (Debe ser limpiada al Terminar de Usar)
#End Region
    'Explicar el codigo es una perdida de tiempo. (y leerlo tambien.) xD

    Private Sub OW_Manager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ListaUso = Debugger.Lista 'Define la Lista que se Usara
            DIROverwatch = DIRCommons & "\OW_VideoManager" 'Define la Ruta de Acceso de las Categorias de Overwatch
            DIRLePOTGs = DIROverwatch & "\" & ListaUso & "_POTGs.txt" 'Define la Ruta de Acceso de Jugadas de la Partida
            DIRLeDestacados = DIROverwatch & "\" & ListaUso & "_Destacados.txt" 'Define la Ruta de Acceso de Destacados
            DIRLeGameplays = DIROverwatch & "\" & ListaUso & "_Gameplays.txt" 'Define la Ruta de Acceso de Gameplays
            DIRLeFavoritos = DIROverwatch & "\" & ListaUso & "_Favoritos.txt" 'Define la Ruta de Acceso de Favoritos
            If Debugger.DarkMode = True Then
                Debugger.ActiveTheme(False)
            Else
                Debugger.ActiveTheme(True)
            End If
            Debugger.CambioLista = False
            If Debugger.Espanglish = "ESP" Then
                Idioma.Español.LANG_Español()
                Me.Text = Debugger.Juego & " | Administrador de Video"
                Label1.Text = Debugger.Juego & " Video Manager"
            ElseIf Debugger.Espanglish = "ENG" Then
                Idioma.Ingles.LANG_English()
                Me.Text = Debugger.Juego & " | Video Manager"
                Label1.Text = Debugger.Juego & " Video Manager"
            End If
            If Debugger.Lista = "L1" Then
                If Debugger.Espanglish = "ESP" Then
                    Label18.Text = "Lista 1 en uso"
                ElseIf Debugger.Espanglish = "ENG" Then
                    Label18.Text = "List 1 in use"
                End If
            ElseIf Debugger.Lista = "L2" Then
                If Debugger.Espanglish = "ESP" Then
                    Label18.Text = "Lista 2 en uso"
                ElseIf Debugger.Espanglish = "ENG" Then
                    Label18.Text = "List 2 in use"
                End If
            ElseIf Debugger.Lista = "L3" Then
                If Debugger.Espanglish = "ESP" Then
                    Label18.Text = "Lista 3 en uso"
                ElseIf Debugger.Espanglish = "ENG" Then
                    Label18.Text = "List 3 in use"
                End If
            Else
                If Debugger.Espanglish = "ESP" Then
                    Label18.Text = Debugger.Lista & " en uso"
                ElseIf Debugger.Espanglish = "ENG" Then
                    Label18.Text = Debugger.Lista & " in use"
                End If
            End If
            If Debugger.Lista = "L1" Then
                Label3.Text = Debugger.Categoria1Lista1
                Label4.Text = Debugger.Categoria2Lista1
                Label5.Text = Debugger.Categoria3Lista1
            ElseIf Debugger.Lista = "L2" Then
                Label3.Text = Debugger.Categoria1Lista2
                Label4.Text = Debugger.Categoria2Lista2
                Label5.Text = Debugger.Categoria3Lista2
            ElseIf Debugger.Lista = "L3" Then
                Label3.Text = Debugger.Categoria1Lista3
                Label4.Text = Debugger.Categoria2Lista3
                Label5.Text = Debugger.Categoria3Lista3
            End If
            InicioComun()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub OW_Manager_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Esta Cerrando Video Manager", "Video Manager esta a un Click de Cerrarse" & vbCrLf & "Cambios Realizados: " & NewChanges, ToolTipIcon.Warning)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "This Closing Video Manager", "Video Manager is a Click to Close" & vbCrLf & "Changes Made: " & NewChanges, ToolTipIcon.Warning)
            End If
            If Debugger.NoShowWithoutChanges = True Then
                If NewChanges = 0 Then
                    'cerrado sin guardar
                    If Debugger.CambioLista = False Then
                        Me.Dispose()
                        Debugger.Close()
                    ElseIf Debugger.CambioLista = True Then
                        Me.Dispose()
                        Me.Close()
                    End If
                Else
                    SaveChangesDialog.ShowDialog()
                    e.Cancel = True
                End If
            Else
                SaveChangesDialog.ShowDialog()
                e.Cancel = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Cerrar el Programa")
        End Try
    End Sub

#Region "Form"

    Private Sub OW_Manager_HelpRequested(ByVal sender As Object, ByVal hlpevent As System.Windows.Forms.HelpEventArgs) Handles Me.HelpRequested
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Eazy Help System", "Sera dirigido a la Pagina Web", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Eazy Help System", "You will be directed to the Website", ToolTipIcon.Info)
        End If
        Process.Start("http://elcris009.comule.com/Download_PS4VideoManager.html")
        If Debugger.Espanglish = "ESP" Then
            MsgBox("Overwatch Video Manager" & vbCrLf & "Creado por: Cristopher Cáceres 'ElCris009'" & vbCrLf & "Con el Apoyo de: Worcome Studios" & vbCrLf & vbCrLf & "Aplicacion para Administrar y Organizar tus Videos de Overwatch provenientes de la plataforma PlayStation.", MsgBoxStyle.Information, "About")
        ElseIf Debugger.Espanglish = "ENG" Then
            MsgBox("Overwatch Video Manager" & vbCrLf & "Created by: Cristopher Cáceres 'ElCris009'" & vbCrLf & "With the Support of: Worcome Studios" & vbCrLf & vbCrLf & "Application to Manage and Organize your Overwatch Videos from the PlayStation platform.", MsgBoxStyle.Information, "About")
        End If
    End Sub
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            Me.TopMost = True
        ElseIf CheckBox1.CheckState = CheckState.Unchecked Then
            Me.TopMost = False
        End If
    End Sub
    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click
        Debugger.StartSecureForm(OW_BackUp)
    End Sub
    Private Sub SaveChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            GuardaArchivosDelListBoxPOTGs()
            GuardaArchivosDelListBoxDESTACADOs()
            GuardaArchivosDelListBoxGAMEPLAYs()
            GuardaArchivosDelListBoxFAVORITOs()
            Threading.Thread.Sleep(1000)
            NewChanges = 0
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Cambios guardados", "Cambios Guardados Correctamente", ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Changes saved", "Saved Changes Correctly", ToolTipIcon.Info)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Guardar los Cambios")
        End Try
    End Sub

    Sub InicioComun()
        Try
            If Debugger.Lista = "L1" Then
                ListaUso = "L1"
            ElseIf Debugger.Lista = "L2" Then
                ListaUso = "L2"
            ElseIf Debugger.Lista = "L3" Then
                ListaUso = "L3"
            Else
                ListaUso = Debugger.Lista
            End If
            Threading.Thread.Sleep(50)
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()
            ListBox3.Items.Clear()
            ListBox4.Items.Clear()
            If My.Computer.FileSystem.DirectoryExists(DIRCommons) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            If My.Computer.FileSystem.DirectoryExists(DIROverwatch) = False Then
                My.Computer.FileSystem.CreateDirectory(DIROverwatch)
            End If
            If My.Computer.FileSystem.FileExists(DIRLePOTGs) = False Then
                My.Computer.FileSystem.WriteAllText(DIRLePOTGs, Nothing, False)
            End If
            If My.Computer.FileSystem.FileExists(DIRLeDestacados) = False Then
                My.Computer.FileSystem.WriteAllText(DIRLeDestacados, Nothing, False)
            End If
            If My.Computer.FileSystem.FileExists(DIRLeGameplays) = False Then
                My.Computer.FileSystem.WriteAllText(DIRLeGameplays, Nothing, False)
            End If
            If My.Computer.FileSystem.FileExists(DIRLeFavoritos) = False Then
                My.Computer.FileSystem.WriteAllText(DIRLeFavoritos, Nothing, False)
            End If
            Threading.Thread.Sleep(50)
            AbreArchivoParaListBoxPOTGs()
            AbreArchivoParaListBoxDESTACADOs()
            AbreArchivoParaListBoxGAMEPLAYs()
            AbreArchivoParaListBoxFAVORITOs()
            Threading.Thread.Sleep(50)
            If Debugger.MarcaPOTG = "None" Then
            Else
                ListBox1.SelectedItem = Debugger.MarcaPOTG
                Debugger.MarcaPOTG = "None"
            End If
            If Debugger.MarcaHighlights = "None" Then
            Else
                ListBox2.SelectedItem = Debugger.MarcaHighlights
                Debugger.MarcaHighlights = "None"
            End If
            If Debugger.MarcaGameplay = "None" Then
            Else
                ListBox3.SelectedItem = Debugger.MarcaGameplay
                Debugger.MarcaGameplay = "None"
            End If
            If Debugger.MarcaFavorites = "None" Then
            Else
                ListBox4.SelectedItem = Debugger.MarcaFavorites
                Debugger.MarcaFavorites = "None"
            End If
            Debugger.SaveConfig()
            NewChanges = 0
            If Debugger.OfflineMode = False Then
                FeedLoading()
            End If
            Timer1.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Iniciar el Programa")
        End Try
    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click
        ListCollector.Show()
    End Sub
    Dim frmSizeSwitch As String = "862"
    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label17.Click
        If frmSizeSwitch = "862" Then
            Label17.Text = "<"
            Me.Width = "1440"
            Me.CenterToScreen()
            frmSizeSwitch = "1440"
        ElseIf frmSizeSwitch = "1440" Then
            Label17.Text = ">"
            Me.Width = "862"
            Me.CenterToScreen()
            frmSizeSwitch = "862"
        End If
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If AutoPlay = False Then
                If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsStopped Then
                    If frmSizeSwitch = "1440" Then
                        Label17.Text = ">"
                        Me.Width = "862"
                        Me.CenterToScreen()
                        frmSizeSwitch = "862"
                    End If
                End If
            ElseIf AutoPlay = True Then
                If AutoPlayListSelected = "ListBox1" Then
                    If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsStopped Then
                        ListBox1.SelectedIndex = ListBox1.SelectedIndex + 1
                        Dim a As Integer = ListBox1.SelectedIndex
                        ListBox1.SelectedIndex = a
                        ContadorPOTG = ListBox1.SelectedIndex
                        AxWindowsMediaPlayer1.URL = MemoryListPOTG(ContadorPOTG)
                    End If
                End If
                If AutoPlayListSelected = "ListBox2" Then
                    If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsStopped Then
                        ListBox2.SelectedIndex = ListBox2.SelectedIndex + 1
                        Dim a As Integer = ListBox2.SelectedIndex
                        ListBox2.SelectedIndex = a
                        ContadorHightlights = ListBox2.SelectedIndex
                        AxWindowsMediaPlayer1.URL = MemoryListHightlights(ContadorHightlights)
                    End If
                End If
                If AutoPlayListSelected = "ListBox3" Then
                    If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsStopped Then
                        ListBox3.SelectedIndex = ListBox3.SelectedIndex + 1
                        Dim a As Integer = ListBox3.SelectedIndex
                        ListBox3.SelectedIndex = a
                        ContadorGameplays = ListBox3.SelectedIndex
                        AxWindowsMediaPlayer1.URL = MemoryListGameplays(ContadorGameplays)
                    End If
                End If
                If AutoPlayListSelected = "ListBox4" Then
                    If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsStopped Then
                        ListBox4.SelectedIndex = ListBox4.SelectedIndex + 1
                        Dim a As Integer = ListBox4.SelectedIndex
                        ListBox4.SelectedIndex = a
                        AxWindowsMediaPlayer1.URL = ListBox4.SelectedItem
                        ContadorFavorites = ListBox4.SelectedIndex
                        AxWindowsMediaPlayer1.URL = MemoryListFavorites(ContadorFavorites)
                    End If
                End If
            End If
        Catch ex As Exception
            CheckBox2.CheckState = CheckState.Unchecked
        End Try
    End Sub

    '=================Inicio Copiar Nombre=================
    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click
        My.Computer.Clipboard.SetText(Label14.Text)
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Copiado al Portapapeles", "Se copio el Texto del Video a su Portapapeles", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Copied to the Clipboard", "Copy the Video Title to your Clipboard", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        My.Computer.Clipboard.SetText(Label8.Text)
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Copiado al Portapapeles", "Se copio el Texto del Video a su Portapapeles", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Copied to the Clipboard", "Copy the Video Title to your Clipboard", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        My.Computer.Clipboard.SetText(Label7.Text)
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Copiado al Portapapeles", "Se copio el Texto del Video a su Portapapeles", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Copied to the Clipboard", "Copy the Video Title to your Clipboard", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        My.Computer.Clipboard.SetText(Label6.Text)
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Copiado al Portapapeles", "Se copio el Texto del Video a su Portapapeles", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Copied to the Clipboard", "Copy the Video Title to your Clipboard", ToolTipIcon.Info)
        End If
    End Sub

    '=================Inicio Abrir ubicacion=================
    Private Sub AbrirEnElExploradorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirEnElExploradorToolStripMenuItem.Click
        Dim path As String = MemoryListPOTG(ContadorPOTG)
        Process.Start("explorer.exe", "/select, " & path)
    End Sub
    Private Sub AbrirEnElExploradorToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirEnElExploradorToolStripMenuItem1.Click
        Dim path As String = MemoryListHightlights(ContadorHightlights)
        Process.Start("explorer.exe", "/select, " & path)
    End Sub
    Private Sub AbrirEnElExploradorToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirEnElExploradorToolStripMenuItem2.Click
        Dim path As String = MemoryListGameplays(ContadorGameplays)
        Process.Start("explorer.exe", "/select, " & path)
    End Sub
    Private Sub AbrirEnElExploradorToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirEnElExploradorToolStripMenuItem3.Click
        Dim path As String = MemoryListFavorites(ContadorFavorites)
        Process.Start("explorer.exe", "/select, " & path)
    End Sub

    Sub Limpiar()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        Label9.Text = Nothing
        Label10.Text = Nothing
        Label11.Text = Nothing
        Label13.Text = Nothing
        NewChanges = NewChanges + 1
    End Sub

    Private Sub BuscarLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label19.Click
        Try
            Dim NombreVideo As String = InputBox("Ingrese Nombre del Video" & vbCrLf & "Ingrese la extencion del archivo" & vbCrLf & "Ej: " & Debugger.Juego & "1234123123.mp4", "Buscar Video por el Nombre")
            If NombreVideo = Nothing Then
            Else
                ListBox1.SelectedItem = NombreVideo
                ListBox2.SelectedItem = NombreVideo
                ListBox3.SelectedItem = NombreVideo
                If Debugger.Espanglish = "ESP" Then
                    NotifyIcon1.ShowBalloonTip(1, "Elemento Encontrado", "Se inicio la Reproduccion del elemento encontrado", ToolTipIcon.Info)
                ElseIf Debugger.Espanglish = "ENG" Then
                    NotifyIcon1.ShowBalloonTip(1, "Found Element", "Playback of the found element was started", ToolTipIcon.Info)
                End If
            End If
        Catch ex As Exception
            MsgBox("No se pudo encontrar :(", MsgBoxStyle.Information, "Buscar Video por el Nombre")
        End Try
    End Sub
    Dim IsExternPlayer As Boolean = False
    '---AUTOPLAY---
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Checked Then
            AutoPlay = True
        ElseIf CheckBox2.CheckState = CheckState.Unchecked Then
            AutoPlay = False
        End If
    End Sub
    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click
        About.Show()
        About.Focus()
    End Sub

    '=================Inicio Separadores=================
    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label23.Click
        Dim TextBoxInput = InputBox("Escribe el nombre del Separador", "Agregar un Separador", "---> " & DateAndTime.Today)
        If TextBoxInput = Nothing Then
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Separador Descartado", "Se Cancelo al Agregado del Separador" & TextBoxInput, ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Discarded Separator", "The Separator Attachment is Canceled" & TextBoxInput, ToolTipIcon.Info)
            End If
        Else
            ListBox3.Items.Add(TextBoxInput)
            MemoryListGameplays.Add(TextBoxInput)
            NewChanges = NewChanges + 1
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Separador Agregado", "Se agrego un Separador para 'Gameplays' con el nombre: " & TextBoxInput, ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Separator Added", "Added a Separator for 'Gameplays' with the name: " & TextBoxInput, ToolTipIcon.Info)
            End If
        End If
    End Sub
    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label22.Click
        Dim TextBoxInput = InputBox("Escribe el nombre del Separador", "Agregar un Separador", "---> " & DateAndTime.Today)
        If TextBoxInput = Nothing Then
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Separador Descartado", "Se Cancelo al Agregado del Separador" & TextBoxInput, ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Discarded Separator", "The Separator Attachment is Canceled" & TextBoxInput, ToolTipIcon.Info)
            End If
        Else
            ListBox2.Items.Add(TextBoxInput)
            MemoryListHightlights.Add(TextBoxInput)
            NewChanges = NewChanges + 1
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Separador Agregado", "Se agrego un Separador para 'Destacados' con el nombre: " & TextBoxInput, ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Separator Added", "Added a Separator for 'Hightlights' with the name: " & TextBoxInput, ToolTipIcon.Info)
            End If
        End If
    End Sub
    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label21.Click
        Dim TextBoxInput = InputBox("Escribe el nombre del Separador", "Agregar un Separador", "---> " & DateAndTime.Today)
        If TextBoxInput = Nothing Then
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Separador Descartado", "Se Cancelo al Agregado del Separador" & TextBoxInput, ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Discarded Separator", "The Separator Attachment is Canceled" & TextBoxInput, ToolTipIcon.Info)
            End If
        Else
            ListBox1.Items.Add(TextBoxInput)
            MemoryListGameplays.Add(TextBoxInput)
            NewChanges = NewChanges + 1
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Separador Agregado", "Se agrego un Separador para 'Jugadas de la Partida' con el nombre: " & TextBoxInput, ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Separator Added", "Added a Separator for 'Play of the Game' with the name: " & TextBoxInput, ToolTipIcon.Info)
            End If
        End If
    End Sub
    Private Sub AgregarSeparadorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarSeparadorToolStripMenuItem.Click
        Dim TextBoxInput = InputBox("Escribe el nombre del Separador", "Agregar un Separador", "---> " & DateAndTime.Today)
        If TextBoxInput = Nothing Then
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Separador Descartado", "Se Cancelo al Agregado del Separador" & TextBoxInput, ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Discarded Separator", "The Separator Attachment is Canceled" & TextBoxInput, ToolTipIcon.Info)
            End If
        Else
            ListBox4.Items.Add(TextBoxInput)
            MemoryListFavorites.Add(TextBoxInput)
            NewChanges = NewChanges + 1
            If Debugger.Espanglish = "ESP" Then
                NotifyIcon1.ShowBalloonTip(1, "Separador Agregado", "Se agrego un Separador para 'Favoritos' con el nombre: " & TextBoxInput, ToolTipIcon.Info)
            ElseIf Debugger.Espanglish = "ENG" Then
                NotifyIcon1.ShowBalloonTip(1, "Separator Added", "Added a Separator for 'Favorites' with the name: " & TextBoxInput, ToolTipIcon.Info)
            End If
        End If
    End Sub

    Sub FeedLoading()
        Try
            If Debugger.OfflineMode = False Then
                If My.Computer.FileSystem.FileExists(DIROverwatch & "\FeedText.ini") = True Then
                    My.Computer.FileSystem.DeleteFile(DIROverwatch & "\FeedText.ini")
                End If
            End If
            If My.Computer.Network.IsAvailable = False Then
                Console.WriteLine("El Computador no esta Conectado a un Punto de Acceso de Internet")
            ElseIf My.Computer.Network.IsAvailable = True Then
                If Debugger.OfflineMode = False Then
                    My.Computer.Network.DownloadFile("http://elcris009.comule.com/Recursos/OWVideoManagerAPP/Feed_OWVideoManager.WorCODE", DIROverwatch & "\FeedText.ini")
                    Threading.Thread.Sleep(150)
                End If
                If My.Computer.FileSystem.FileExists(DIROverwatch & "\FeedText.ini") = False Then
                    If Debugger.OfflineMode = False Then
                        FeedLoading()
                    End If
                ElseIf My.Computer.FileSystem.FileExists(DIROverwatch & "\FeedText.ini") = True Then
                    Try
                        Dim Feed_Español As String
                        Dim Feed_English As String
                        Dim AppStatus As String
                        Dim ServerVersion As String
                        Dim MessageESP As String
                        Dim MessageENG As String
                        Dim CriticalUpdate As String
                        Dim Lines = System.IO.File.ReadAllLines(DIROverwatch & "\FeedText.ini")
                        Feed_Español = Lines(1).Split(">"c)(1).Trim()
                        Feed_English = Lines(2).Split(">"c)(1).Trim()
                        AppStatus = Lines(3).Split(">"c)(1).Trim()
                        ServerVersion = Lines(4).Split(">"c)(1).Trim()
                        MessageESP = Lines(5).Split(">"c)(1).Trim()
                        MessageENG = Lines(6).Split(">"c)(1).Trim()
                        CriticalUpdate = Lines(7).Split(">"c)(1).Trim()
                        If AppStatus = "True" Then
                            If Debugger.Espanglish = "ESP" Then
                                FeedLabel.Text = Feed_Español
                                If MessageESP = "None" Then
                                Else
                                    MsgBox(MessageESP, MsgBoxStyle.Information, "Mensaje del Servidor")
                                End If
                                If ServerVersion = My.Application.Info.Version.ToString Then
                                Else
                                    If CriticalUpdate = "True" Then
                                        MsgBox("Debes actualizar esta aplicacion", MsgBoxStyle.Critical, "Sistema de Emergencia")
                                        Debugger.CriticalUpdates = True
                                        Debugger.SaveConfig()
                                        End
                                    Else
                                        Debugger.CriticalUpdates = False
                                        Debugger.SaveConfig()
                                        NotifyIcon1.ShowBalloonTip(1, "Actualizaciones", "¡Hay una Actualizacion disponible!", ToolTipIcon.Info)
                                    End If
                                End If
                            ElseIf Debugger.Espanglish = "ENG" Then
                                FeedLabel.Text = Feed_English
                                If MessageENG = "None" Then
                                Else
                                    MsgBox(MessageENG, MsgBoxStyle.Information, "Server Message")
                                End If
                                If ServerVersion = My.Application.Info.Version.ToString Then
                                Else
                                    If CriticalUpdate = "True" Then
                                        MsgBox("You need to update this application", MsgBoxStyle.Critical, "Emergency System")
                                        Debugger.CriticalUpdates = True
                                        Debugger.SaveConfig()
                                        End
                                    Else
                                        Debugger.CriticalUpdates = False
                                        Debugger.SaveConfig()
                                        NotifyIcon1.ShowBalloonTip(1, "Updates", "New version available!", ToolTipIcon.Info)
                                    End If
                                End If
                            End If
                        ElseIf AppStatus = False Then
                            If ServerVersion = My.Application.Info.Version.ToString Then
                            Else
                                If CriticalUpdate = "True" Then
                                    MsgBox("You need to update this application", MsgBoxStyle.Critical, "Emergency System")
                                    Debugger.CriticalUpdates = True
                                    Debugger.SaveConfig()
                                    End
                                Else
                                    Debugger.CriticalUpdates = False
                                    Debugger.SaveConfig()
                                End If
                            End If
                            If Debugger.Espanglish = "ESP" Then
                                MsgBox("Aplicacion desactivada por orden del Servidor", MsgBoxStyle.Critical, "Aplicacion Suspendida")
                                MsgBox("Sistema de Emergencia" & vbCrLf & MessageESP, MsgBoxStyle.Information, "Mensaje del Servidor")
                            ElseIf Debugger.Espanglish = "ENG" Then
                                MsgBox("Application deactivated by order of the Server", MsgBoxStyle.Critical, "Suspended Application")
                                MsgBox("Emergency System" & vbCrLf & MessageENG, MsgBoxStyle.Information, "Server Message")
                            End If
                            End
                        End If
                    Catch ex As Exception
                        Console.WriteLine("[OW_Manager@FeedUpdate(Reader)]Error: " & ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("[OW_Manager@FeedUpdate]Error: " & ex.Message)
        End Try
    End Sub

#End Region
#Region "Drag&Drop"
    Private Sub Panel1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel1.DragDrop
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim strRutaArchivos() As String
                Dim i As Integer
                strRutaArchivos = e.Data.GetData(DataFormats.FileDrop)
                Dim StringPassed As String
                For i = 0 To strRutaArchivos.Length - 1
                    StringPassed = strRutaArchivos(i).ToString
                    MemoryListPOTG.Add(StringPassed)
                    StringPassed = StringPassed.Remove(0, StringPassed.LastIndexOf("\") + 1)
                    ListBox1.Items.Add(StringPassed)
                    NewChanges = NewChanges + 1
                    If Debugger.Espanglish = "ESP" Then
                        NotifyIcon1.ShowBalloonTip(1, "Nuevo Item Agregado", "El Item fue Agregado a 'Jugadas de la Partida'", ToolTipIcon.Info)
                    ElseIf Debugger.Espanglish = "ENG" Then
                        NotifyIcon1.ShowBalloonTip(1, "New Item Added", "Item was added to 'Play of the Game'", ToolTipIcon.Info)
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Panel1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Private Sub Panel2_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel2.DragDrop
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim strRutaArchivos() As String
                Dim i As Integer
                strRutaArchivos = e.Data.GetData(DataFormats.FileDrop)
                Dim StringPassed As String
                For i = 0 To strRutaArchivos.Length - 1
                    StringPassed = strRutaArchivos(i).ToString
                    MemoryListHightlights.Add(StringPassed)
                    StringPassed = StringPassed.Remove(0, StringPassed.LastIndexOf("\") + 1)
                    ListBox2.Items.Add(StringPassed)
                    NewChanges = NewChanges + 1
                    If Debugger.Espanglish = "ESP" Then
                        NotifyIcon1.ShowBalloonTip(1, "Nuevo Item Agregado", "El Item fue Agregado a 'Destacados'", ToolTipIcon.Info)
                    ElseIf Debugger.Espanglish = "ENG" Then
                        NotifyIcon1.ShowBalloonTip(1, "New Item Added", "Item was added to 'Highlights'", ToolTipIcon.Info)
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Panel2_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Private Sub Panel3_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel3.DragDrop
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim strRutaArchivos() As String
                Dim i As Integer
                strRutaArchivos = e.Data.GetData(DataFormats.FileDrop)
                Dim StringPassed As String
                For i = 0 To strRutaArchivos.Length - 1
                    StringPassed = strRutaArchivos(i).ToString
                    MemoryListGameplays.Add(StringPassed)
                    StringPassed = StringPassed.Remove(0, StringPassed.LastIndexOf("\") + 1)
                    ListBox3.Items.Add(StringPassed)
                    NewChanges = NewChanges + 1
                    If Debugger.Espanglish = "ESP" Then
                        NotifyIcon1.ShowBalloonTip(1, "Nuevo Item Agregado", "El Item fue Agregado a 'Gameplays'", ToolTipIcon.Info)
                    ElseIf Debugger.Espanglish = "ENG" Then
                        NotifyIcon1.ShowBalloonTip(1, "New Item Added", "Item was added to 'Gameplays'", ToolTipIcon.Info)
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Panel3_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel3.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
#End Region
#Region "FileReader&Write"
    Sub GuardaArchivosDelListBoxPOTGs()
        Dim rutaFichero As String
        Dim i As Integer
        Try
            rutaFichero = Path.Combine(Application.StartupPath, DIRLePOTGs)
            Dim fichero As New IO.StreamWriter(rutaFichero)
            For i = 0 To MemoryListPOTG.Count - 1
                fichero.WriteLine(MemoryListPOTG.Item(i))
            Next
            fichero.Close()
            'MsgBox("'Jugadas de la Partida' Guardadas Correctamente", MsgBoxStyle.Information, "Guardado de Ficheros")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Guardar Ficheros")
        End Try
    End Sub
    Sub GuardaArchivosDelListBoxDESTACADOs()
        Dim rutaFichero As String
        Dim i As Integer
        Try
            rutaFichero = Path.Combine(Application.StartupPath, DIRLeDestacados)
            Dim fichero As New IO.StreamWriter(rutaFichero)
            For i = 0 To MemoryListHightlights.Count - 1
                fichero.WriteLine(MemoryListHightlights.Item(i))
            Next
            fichero.Close()
            'MsgBox("'Destacados' Guardados Correctamente", MsgBoxStyle.Information, "Guardado de Ficheros")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Guardar Ficheros")
        End Try
    End Sub
    Sub GuardaArchivosDelListBoxGAMEPLAYs()
        Dim rutaFichero As String
        Dim i As Integer
        Try
            rutaFichero = Path.Combine(Application.StartupPath, DIRLeGameplays)
            Dim fichero As New IO.StreamWriter(rutaFichero)
            For i = 0 To MemoryListGameplays.Count - 1
                fichero.WriteLine(MemoryListGameplays.Item(i))
            Next
            fichero.Close()
            'MsgBox("'Gameplays' Guardados Correctamente", MsgBoxStyle.Information, "Guardado de Ficheros")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Guardar Ficheros")
        End Try
    End Sub
    Sub GuardaArchivosDelListBoxFAVORITOs()
        Dim rutaFichero As String
        Dim i As Integer
        Try
            rutaFichero = Path.Combine(Application.StartupPath, DIRLeFavoritos)
            Dim fichero As New IO.StreamWriter(rutaFichero)
            For i = 0 To MemoryListFavorites.Count - 1
                fichero.WriteLine(MemoryListFavorites.Item(i))
            Next
            fichero.Close()
            'MsgBox("'Gameplays' Guardados Correctamente", MsgBoxStyle.Information, "Guardado de Ficheros")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Guardar Ficheros")
        End Try
    End Sub

    Sub AbreArchivoParaListBoxPOTGs()
        Try
            Dim rutaFichero As String = IO.Path.Combine(DIRLePOTGs)
            If IO.File.Exists(rutaFichero) = True Then
                Dim fichero As New IO.StreamReader(rutaFichero)
                Dim StringPassed As String
                While (fichero.Peek() > -1)
                    StringPassed = fichero.ReadLine
                    MemoryListPOTG.Add(StringPassed)
                    StringPassed = StringPassed.Remove(0, StringPassed.LastIndexOf("\") + 1)
                    ListBox1.Items.Add(StringPassed)
                End While
                fichero.Close()
                'MsgBox("'Jugadas de la Partida' Agregadas Correctamente", MsgBoxStyle.Information, "Agregado de Ficheros")
            End If
            Label9.Text = ListBox1.Items.Count
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Agregar los Ficheros")
        End Try
    End Sub
    Sub AbreArchivoParaListBoxDESTACADOs()
        Try
            Dim rutaFichero As String = IO.Path.Combine(DIRLeDestacados)
            If IO.File.Exists(rutaFichero) = True Then
                Dim fichero As New IO.StreamReader(rutaFichero)
                Dim StringPassed As String
                While (fichero.Peek() > -1)
                    StringPassed = fichero.ReadLine
                    MemoryListHightlights.Add(StringPassed)
                    StringPassed = StringPassed.Remove(0, StringPassed.LastIndexOf("\") + 1)
                    ListBox2.Items.Add(StringPassed)
                End While
                fichero.Close()
                'MsgBox("'Destacados' Agregados Correctamente", MsgBoxStyle.Information, "Agregado de Ficheros")
            End If
            Label10.Text = ListBox2.Items.Count
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Agregar los Ficheros")
        End Try
    End Sub
    Sub AbreArchivoParaListBoxGAMEPLAYs()
        Try
            Dim rutaFichero As String = IO.Path.Combine(DIRLeGameplays)
            If IO.File.Exists(rutaFichero) = True Then
                Dim fichero As New IO.StreamReader(rutaFichero)
                Dim StringPassed As String
                While (fichero.Peek() > -1)
                    StringPassed = fichero.ReadLine
                    MemoryListGameplays.Add(StringPassed)
                    StringPassed = StringPassed.Remove(0, StringPassed.LastIndexOf("\") + 1)
                    ListBox3.Items.Add(StringPassed)
                End While
                fichero.Close()
                'MsgBox("'Gameplays' Agregados Correctamente", MsgBoxStyle.Information, "Agregado de Ficheros")
            End If
            Label11.Text = ListBox3.Items.Count
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Agregar los Ficheros")
        End Try
    End Sub
    Sub AbreArchivoParaListBoxFAVORITOs()
        Try
            Dim rutaFichero As String = IO.Path.Combine(DIRLeFavoritos)
            If IO.File.Exists(rutaFichero) = True Then
                Dim fichero As New IO.StreamReader(rutaFichero)
                Dim StringPassed As String
                While (fichero.Peek() > -1)
                    StringPassed = fichero.ReadLine
                    MemoryListFavorites.Add(StringPassed)
                    StringPassed = StringPassed.Remove(0, StringPassed.LastIndexOf("\") + 1)
                    ListBox4.Items.Add(StringPassed)
                End While
                fichero.Close()
                'MsgBox("'Gameplays' Agregados Correctamente", MsgBoxStyle.Information, "Agregado de Ficheros")
            End If
            Label13.Text = ListBox4.Items.Count
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Agregar los Ficheros")
        End Try
    End Sub
#End Region
#Region "ListBoxes"
    Private Sub ListBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                MemoryListPOTG.RemoveAt(ContadorPOTG)
                ListBox1.Items.Remove(ListBox1.SelectedItem)
                NewChanges = NewChanges + 1
                If Debugger.Espanglish = "ESP" Then
                    NotifyIcon1.ShowBalloonTip(1, "Item Eliminado", "El Item fue removido de la Categoria 'Jugadas de la Partida'", ToolTipIcon.Info)
                ElseIf Debugger.Espanglish = "ENG" Then
                    NotifyIcon1.ShowBalloonTip(1, "Item Removed", "The Item was removed from the Category 'Play of the Game'", ToolTipIcon.Info)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Eliminar Fichero")
        End Try
    End Sub
    Private Sub ListBox1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDoubleClick
        Try
            ContadorPOTG = ListBox1.SelectedIndex
            Process.Start(MemoryListPOTG(ContadorPOTG))
            IsExternPlayer = True
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox2.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                MemoryListHightlights.RemoveAt(ContadorHightlights)
                ListBox2.Items.Remove(ListBox2.SelectedItem)
                NewChanges = NewChanges + 1
                If Debugger.Espanglish = "ESP" Then
                    NotifyIcon1.ShowBalloonTip(1, "Item Eliminado", "El Item fue removido de la Categoria 'Destacados'", ToolTipIcon.Info)
                ElseIf Debugger.Espanglish = "ENG" Then
                    NotifyIcon1.ShowBalloonTip(1, "Item Removed", "The Item was removed from the Category 'Highlights'", ToolTipIcon.Info)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Eliminar Fichero")
        End Try
    End Sub
    Private Sub ListBox2_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox2.MouseDoubleClick
        Try
            ContadorHightlights = ListBox2.SelectedIndex
            Process.Start(MemoryListHightlights(ContadorHightlights))
            IsExternPlayer = True
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox3.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                MemoryListGameplays.RemoveAt(ContadorGameplays)
                ListBox3.Items.Remove(ListBox3.SelectedItem)
                NewChanges = NewChanges + 1
                If Debugger.Espanglish = "ESP" Then
                    NotifyIcon1.ShowBalloonTip(1, "Item Eliminado", "El Item fue removido de la Categoria 'Gameplays'", ToolTipIcon.Info)
                ElseIf Debugger.Espanglish = "ENG" Then
                    NotifyIcon1.ShowBalloonTip(1, "Item Removed", "The Item was removed from the Category 'Gameplays'", ToolTipIcon.Info)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Eliminar Fichero")
        End Try
    End Sub
    Private Sub ListBox3_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox3.MouseDoubleClick
        Try
            ContadorGameplays = ListBox3.SelectedIndex
            Process.Start(MemoryListGameplays(ContadorGameplays))
            IsExternPlayer = True
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox4.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                MemoryListFavorites.RemoveAt(ContadorFavorites)
                ListBox4.Items.Remove(ListBox4.SelectedItem)
                NewChanges = NewChanges + 1
                If Debugger.Espanglish = "ESP" Then
                    NotifyIcon1.ShowBalloonTip(1, "Item Eliminado", "El Item fue removido de la Categoria 'Favoritos'", ToolTipIcon.Info)
                ElseIf Debugger.Espanglish = "ENG" Then
                    NotifyIcon1.ShowBalloonTip(1, "Item Removed", "The Item was removed from the Category 'Favorites'", ToolTipIcon.Info)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Eliminar Fichero")
        End Try
    End Sub
    Private Sub ListBox4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox4.DoubleClick
        Try
            ContadorFavorites = ListBox4.SelectedIndex
            Process.Start(MemoryListFavorites(ContadorFavorites))
            IsExternPlayer = True
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            ContadorPOTG = ListBox1.SelectedIndex
            AxWindowsMediaPlayer1.URL = MemoryListPOTG(ContadorPOTG)
            IsExternPlayer = False
            LoadNamePOTG()
            'Visualizer.ViewFrom(ListBox1.SelectedItem)
            AutoPlayListSelected = "ListBox1"
            If frmSizeSwitch = "862" Then
                Label17.Text = "<"
                Me.Width = "1440"
                Me.CenterToScreen()
                frmSizeSwitch = "1440"
            End If
        Catch ex As Exception
            Console.WriteLine("Error al Abrir Previsualizador: " & ex.Message)
        End Try
    End Sub
    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Try
            ContadorHightlights = ListBox2.SelectedIndex
            AxWindowsMediaPlayer1.URL = MemoryListHightlights(ContadorHightlights)
            IsExternPlayer = False
            LoadNameDESTACADO()
            'Visualizer.ViewFrom(ListBox2.SelectedItem)
            AutoPlayListSelected = "ListBox2"
            If frmSizeSwitch = "862" Then
                Label17.Text = "<"
                Me.Width = "1440"
                Me.CenterToScreen()
                frmSizeSwitch = "1440"
            End If
        Catch ex As Exception
            Console.WriteLine("Error al Abrir Previsualizador: " & ex.Message)
        End Try
    End Sub
    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Try
            ContadorGameplays = ListBox3.SelectedIndex
            AxWindowsMediaPlayer1.URL = MemoryListGameplays(ContadorGameplays)
            IsExternPlayer = False
            LoadNameGAMEPLAY()
            'Visualizer.ViewFrom(ListBox3.SelectedItem)
            AutoPlayListSelected = "ListBox3"
            If frmSizeSwitch = "862" Then
                Label17.Text = "<"
                Me.Width = "1440"
                Me.CenterToScreen()
                frmSizeSwitch = "1440"
            End If
        Catch ex As Exception
            Console.WriteLine("Error al Abrir Previsualizador: " & ex.Message)
        End Try
    End Sub
    Private Sub ListBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox4.SelectedIndexChanged
        Try
            ContadorFavorites = ListBox4.SelectedIndex
            AxWindowsMediaPlayer1.URL = MemoryListFavorites(ContadorFavorites)
            IsExternPlayer = False
            LoadNameFAVORITO()
            'Visualizer.ViewFrom(ListBox4.SelectedItem)
            AutoPlayListSelected = "ListBox4"
            If frmSizeSwitch = "862" Then
                Label17.Text = "<"
                Me.Width = "1440"
                Me.CenterToScreen()
                frmSizeSwitch = "1440"
            End If
        Catch ex As Exception
            Console.WriteLine("Error al Abrir Previsualizador: " & ex.Message)
        End Try
    End Sub

    Private Sub ListBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDown
        PPOSICIONI = ListBox1.SelectedIndex
    End Sub
    Private Sub ListBox1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseUp
        PPOSICIONF = ListBox1.SelectedIndex
        If PPOSICIONF <> PPOSICIONI Then
            If PPOSICIONF < PPOSICIONI Then
                MemoryListPOTG.Insert(PPOSICIONF, MemoryListPOTG(PPOSICIONI))
                MemoryListPOTG.RemoveAt(PPOSICIONI + 1)
            Else
                MemoryListPOTG.Insert(PPOSICIONF + 1, MemoryListPOTG(PPOSICIONI))
                MemoryListPOTG.RemoveAt(PPOSICIONI)
            End If
            ListBox1.Items.Clear()
            For Each ELEMENTO In MemoryListPOTG
                ELEMENTO = ELEMENTO.Remove(0, ELEMENTO.LastIndexOf("\") + 1)
                ListBox1.Items.Add(ELEMENTO)
            Next
        End If
    End Sub

    Private Sub ListBox2_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox2.MouseDown
        HPOSICIONI = ListBox2.SelectedIndex
    End Sub
    Private Sub ListBox2_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox2.MouseUp
        HPOSICIONF = ListBox2.SelectedIndex
        If HPOSICIONF <> HPOSICIONI Then
            If HPOSICIONF < HPOSICIONI Then
                MemoryListHightlights.Insert(HPOSICIONF, MemoryListHightlights(HPOSICIONI))
                MemoryListHightlights.RemoveAt(HPOSICIONI + 1)
            Else
                MemoryListHightlights.Insert(HPOSICIONF + 1, MemoryListHightlights(HPOSICIONI))
                MemoryListHightlights.RemoveAt(HPOSICIONI)
            End If
            ListBox2.Items.Clear()
            For Each ELEMENTO In MemoryListHightlights
                ELEMENTO = ELEMENTO.Remove(0, ELEMENTO.LastIndexOf("\") + 1)
                ListBox2.Items.Add(ELEMENTO)
            Next
        End If
    End Sub

    Private Sub ListBox3_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox3.MouseDown
        GPOSICIONI = ListBox3.SelectedIndex
    End Sub
    Private Sub ListBox3_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox3.MouseUp
        GPOSICIONF = ListBox3.SelectedIndex
        If GPOSICIONF <> GPOSICIONI Then
            If GPOSICIONF < GPOSICIONI Then
                MemoryListHightlights.Insert(GPOSICIONF, MemoryListHightlights(GPOSICIONI))
                MemoryListHightlights.RemoveAt(GPOSICIONI + 1)
            Else
                MemoryListHightlights.Insert(HPOSICIONF + 1, MemoryListHightlights(GPOSICIONI))
                MemoryListHightlights.RemoveAt(GPOSICIONI)
            End If
            ListBox3.Items.Clear()
            For Each ELEMENTO In MemoryListHightlights
                ELEMENTO = ELEMENTO.Remove(0, ELEMENTO.LastIndexOf("\") + 1)
                ListBox3.Items.Add(ELEMENTO)
            Next
        End If
    End Sub

    Private Sub ListBox4_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox4.MouseDown
        FPOSICIONI = ListBox4.SelectedIndex
    End Sub
    Private Sub ListBox4_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox4.MouseUp
        FPOSICIONF = ListBox4.SelectedIndex
        If FPOSICIONF <> FPOSICIONI Then
            If FPOSICIONF < FPOSICIONI Then
                MemoryListFavorites.Insert(FPOSICIONF, MemoryListFavorites(FPOSICIONI))
                MemoryListFavorites.RemoveAt(FPOSICIONI + 1)
            Else
                MemoryListFavorites.Insert(FPOSICIONF + 1, MemoryListFavorites(FPOSICIONI))
                MemoryListFavorites.RemoveAt(FPOSICIONI)
            End If
            ListBox4.Items.Clear()
            For Each ELEMENTO In MemoryListFavorites
                ELEMENTO = ELEMENTO.Remove(0, ELEMENTO.LastIndexOf("\") + 1)
                ListBox4.Items.Add(ELEMENTO)
            Next
        End If
    End Sub
#End Region
#Region "NameLoad"
    Sub LoadNamePOTG()
        Try
            Dim NOMBRE As String = ListBox1.SelectedItem
            NOMBRE = NOMBRE.Remove(0, NOMBRE.LastIndexOf("\") + 1)
            NOMBRE = NOMBRE.Replace(".mp4", "")
            Label6.Text = NOMBRE
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Leer el Nombre")
        End Try
    End Sub
    Sub LoadNameDESTACADO()
        Try
            Dim NOMBRE As String = ListBox2.SelectedItem
            NOMBRE = NOMBRE.Remove(0, NOMBRE.LastIndexOf("\") + 1)
            NOMBRE = NOMBRE.Replace(".mp4", "")
            Label7.Text = NOMBRE
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Leer el Nombre")
        End Try
    End Sub
    Sub LoadNameGAMEPLAY()
        Try
            Dim NOMBRE As String = ListBox3.SelectedItem
            NOMBRE = NOMBRE.Remove(0, NOMBRE.LastIndexOf("\") + 1)
            NOMBRE = NOMBRE.Replace(".mp4", "")
            Label8.Text = NOMBRE
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Leer el Nombre")
        End Try
    End Sub
    Sub LoadNameFAVORITO()
        Try
            Dim NOMBRE As String = ListBox4.SelectedItem
            NOMBRE = NOMBRE.Remove(0, NOMBRE.LastIndexOf("\") + 1)
            NOMBRE = NOMBRE.Replace(".mp4", "")
            Label14.Text = NOMBRE
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al Leer el Nombre")
        End Try
    End Sub
#End Region
#Region "ContextMenu"
    '=================Inicio Menu General=================
    Private Sub RecargarTodoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecargarTodoToolStripMenuItem.Click
        InicioComun()
    End Sub
    Private Sub EliminarTodoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarTodoToolStripMenuItem.Click
        ListBox1.Items.Clear()
        MemoryListPOTG.Clear()
        ListBox2.Items.Clear()
        MemoryListHightlights.Clear()
        ListBox3.Items.Clear()
        MemoryListGameplays.Clear()
        ListBox4.Items.Clear()
        MemoryListFavorites.Clear()
        NewChanges = NewChanges + 1
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Categorias limpiadas", "Todas las Categorias fueron Limpiadas", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Categories cleaned", "All Categories were Cleaned", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub SalirSinGuardarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirSinGuardarToolStripMenuItem.Click
        End
    End Sub
    Private Sub GuardarYSalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarYSalirToolStripMenuItem.Click
        Me.Close()
    End Sub

    '=================Inicio Menu Jugadas de la Partida=================
    Private Sub RecargarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecargarToolStripMenuItem.Click
        ListBox1.Items.Clear()
        MemoryListPOTG.Clear()
        AbreArchivoParaListBoxPOTGs()
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Categorias Actualizada", "La Categoria 'Jugadas de la Partida' se Actualizo", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Categories Updated", "The Category 'Play of the Game' is updated", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub BorrarItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarItemToolStripMenuItem.Click
        MemoryListPOTG.RemoveAt(ContadorPOTG)
        ListBox1.Items.Remove(ListBox1.SelectedItem)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub BorrarTodosLosItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarTodosLosItemsToolStripMenuItem.Click
        ListBox1.Items.Clear()
        MemoryListPOTG.Clear()
        NewChanges = NewChanges + 1
    End Sub
    Private Sub AgregarAFavoritosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgregarAFavoritosToolStripMenuItem.Click
        ListBox4.Items.Add(ListBox1.SelectedItem)
        MemoryListFavorites.Add(MemoryListPOTG(ContadorPOTG))
        NewChanges = NewChanges + 1
    End Sub

    '=================Inicio Menu Destacados=================
    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        ListBox2.Items.Clear()
        MemoryListHightlights.Clear()
        AbreArchivoParaListBoxDESTACADOs()
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Categorias Actualizada", "La Categoria 'Destacados' se Actualizo", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Categories Updated", "The Category 'Highlights' is updated", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        MemoryListHightlights.RemoveAt(ContadorHightlights)
        ListBox2.Items.Remove(ListBox2.SelectedItem)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        ListBox2.Items.Clear()
        MemoryListHightlights.Clear()
        NewChanges = NewChanges + 1
    End Sub
    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        ListBox4.Items.Add(ListBox2.SelectedItem)
        MemoryListFavorites.Add(MemoryListHightlights(ContadorHightlights))
        NewChanges = NewChanges + 1
    End Sub

    '=================Inicio Menu Gameplays=================
    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        ListBox3.Items.Clear()
        MemoryListGameplays.Clear()
        AbreArchivoParaListBoxGAMEPLAYs()
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Categorias Actualizada", "La Categoria 'Gameplays' se Actualizo", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Categories Updated", "The Category 'Gameplays' is updated", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        MemoryListGameplays.RemoveAt(ContadorGameplays)
        ListBox3.Items.Remove(ListBox3.SelectedItem)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        ListBox3.Items.Clear()
        MemoryListGameplays.Clear()
        NewChanges = NewChanges + 1
    End Sub
    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        ListBox4.Items.Add(ListBox3.SelectedItem)
        MemoryListFavorites.Add(MemoryListGameplays(ContadorGameplays))
        NewChanges = NewChanges + 1
    End Sub

    '=================Inicio Menu Favoritos=================
    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        ListBox4.Items.Clear()
        MemoryListFavorites.Clear()
        AbreArchivoParaListBoxFAVORITOs()
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Categorias Actualizada", "La Categoria 'Favoritos' se Actualizo", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Categories Updated", "The Category 'Favorites' is updated", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub ToolStripMenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem18.Click
        MemoryListFavorites.RemoveAt(ContadorFavorites)
        ListBox4.Items.Remove(ListBox4.SelectedItem)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub ToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem19.Click
        ListBox4.Items.Clear()
        MemoryListFavorites.Clear()
        NewChanges = NewChanges + 1
    End Sub

    '=================Mover Items entre Categorias=================
    Private Sub DestacadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DestacadosToolStripMenuItem.Click
        ContadorPOTG = ListBox1.SelectedIndex
        ListBox2.Items.Add(ListBox1.SelectedItem)
        MemoryListHightlights.Add(MemoryListPOTG(ContadorPOTG))
        MemoryListPOTG.RemoveAt(ContadorPOTG)
        ListBox1.Items.Remove(ListBox1.SelectedItem)
        NewChanges = NewChanges + 1
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Item Movido", "Se Movio el Item de 'Jugadas de la Partida' > a > 'Destacados'", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Moving Item", "Move the Item of 'Play of the Game'> to > 'Highlights'", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub GameplaysToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GameplaysToolStripMenuItem.Click
        ContadorPOTG = ListBox1.SelectedIndex
        ListBox3.Items.Add(ListBox1.SelectedItem)
        MemoryListGameplays.Add(MemoryListPOTG(ContadorPOTG))
        MemoryListPOTG.RemoveAt(ContadorPOTG)
        ListBox1.Items.Remove(ListBox1.SelectedItem)
        NewChanges = NewChanges + 1
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Item Movido", "Se Movio el Item de 'Jugadas de la Partida' > a > 'Gameplays'", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Moving Item", "Move the Item of 'Play of the Game'> to > 'Gameplays'", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub JugadasDeLaPartidaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JugadasDeLaPartidaToolStripMenuItem1.Click
        ContadorHightlights = ListBox2.SelectedIndex
        ListBox1.Items.Add(ListBox2.SelectedItem)
        MemoryListPOTG.Add(MemoryListHightlights(ContadorHightlights))
        MemoryListPOTG.RemoveAt(ContadorPOTG)
        ListBox2.Items.Remove(ListBox2.SelectedItem)
        NewChanges = NewChanges + 1
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Item Movido", "Se Movio el Item de 'Destacados' > a > 'Jugadas de la Partida'", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Moving Item", "Move the Item of 'Highlights'> to > 'Jugadas de la Partida'", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub GameplaysToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GameplaysToolStripMenuItem1.Click
        ContadorHightlights = ListBox2.SelectedIndex
        ListBox3.Items.Add(ListBox2.SelectedItem)
        MemoryListGameplays.Add(MemoryListHightlights(ContadorHightlights))
        MemoryListHightlights.RemoveAt(ContadorHightlights)
        ListBox2.Items.Remove(ListBox2.SelectedItem)
        NewChanges = NewChanges + 1
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Item Movido", "Se Movio el Item de 'Destacados' > a > 'Gameplays'", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Moving Item", "Move the Item of 'Highlights'> to > 'Gameplays'", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub JugadasDeLaPartidaToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JugadasDeLaPartidaToolStripMenuItem2.Click
        ContadorGameplays = ListBox3.SelectedIndex
        ListBox1.Items.Add(ListBox3.SelectedItem)
        MemoryListPOTG.Add(MemoryListGameplays(ContadorGameplays))
        MemoryListGameplays.RemoveAt(ContadorGameplays)
        ListBox3.Items.Remove(ListBox3.SelectedItem)
        NewChanges = NewChanges + 1
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Item Movido", "Se Movio el Item de 'Gameplays' > a > 'Jugadas de la Partida'", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Moving Item", "Move the Item of 'Gameplays'> to > 'Play of the Game'", ToolTipIcon.Info)
        End If
    End Sub
    Private Sub DestacadosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DestacadosToolStripMenuItem1.Click
        ContadorGameplays = ListBox3.SelectedIndex
        ListBox2.Items.Add(ListBox3.SelectedItem)
        MemoryListHightlights.Add(MemoryListGameplays(ContadorGameplays))
        MemoryListGameplays.RemoveAt(ContadorGameplays)
        ListBox3.Items.Remove(ListBox3.SelectedItem)
        NewChanges = NewChanges + 1
        If Debugger.Espanglish = "ESP" Then
            NotifyIcon1.ShowBalloonTip(1, "Item Movido", "Se Movio el Item de 'Gameplays' > a > 'Destacados'", ToolTipIcon.Info)
        ElseIf Debugger.Espanglish = "ENG" Then
            NotifyIcon1.ShowBalloonTip(1, "Moving Item", "Move the Item of 'Gameplays'> to > 'Highlights'", ToolTipIcon.Info)
        End If
    End Sub

    '=================Eliminar archivos=================
    Private Sub EliminarArchivoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarArchivoToolStripMenuItem.Click
        If MessageBox.Show("¿Realmente quieres eliminar este Archivo de tu Computadora?" & vbCrLf & "Do you really want to delete this file from your computer?", "Eliminar Archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            My.Computer.FileSystem.DeleteFile(MemoryListPOTG(ContadorPOTG), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            MemoryListPOTG.RemoveAt(ContadorPOTG)
            ListBox1.Items.Remove(ListBox1.SelectedItem)
        End If
        NewChanges = NewChanges + 1
    End Sub
    Private Sub EliminarArchivoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EliminarArchivoToolStripMenuItem1.Click
        If MessageBox.Show("¿Realmente quieres eliminar este Archivo de tu Computadora?" & vbCrLf & "Do you really want to delete this file from your computer?", "Eliminar Archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            My.Computer.FileSystem.DeleteFile(MemoryListHightlights(ContadorHightlights), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            MsgBox(MemoryListHightlights(ContadorHightlights))
            MemoryListHightlights.RemoveAt(ContadorHightlights)
            ListBox2.Items.Remove(ListBox2.SelectedItem)
        End If
        NewChanges = NewChanges + 1
    End Sub
    Private Sub EliminarArchivoToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles EliminarArchivoToolStripMenuItem2.Click
        If MessageBox.Show("¿Realmente quieres eliminar este Archivo de tu Computadora?" & vbCrLf & "Do you really want to delete this file from your computer?", "Eliminar Archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            My.Computer.FileSystem.DeleteFile(MemoryListGameplays(ContadorGameplays), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            MemoryListGameplays.RemoveAt(ContadorGameplays)
            ListBox3.Items.Remove(ListBox3.SelectedItem)
        End If
        NewChanges = NewChanges + 1
    End Sub

    '=================Marcar Archivos=================
    Private Sub AgregarMarcadorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarMarcadorToolStripMenuItem.Click
        If ListBox1.SelectedItem = Nothing Then
        Else
            Debugger.MarcaPOTG = ListBox1.SelectedItem
        End If
        Debugger.SaveConfig()
        NewChanges = NewChanges + 1
    End Sub
    Private Sub AgregarMarcadorToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AgregarMarcadorToolStripMenuItem1.Click
        If ListBox2.SelectedItem = Nothing Then
        Else
            Debugger.MarcaHighlights = ListBox2.SelectedItem
        End If
        Debugger.SaveConfig()
        NewChanges = NewChanges + 1
    End Sub
    Private Sub AgregarMarcadorToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles AgregarMarcadorToolStripMenuItem2.Click
        If ListBox3.SelectedItem = Nothing Then
        Else
            Debugger.MarcaGameplay = ListBox3.SelectedItem
        End If
        Debugger.SaveConfig()
        NewChanges = NewChanges + 1
    End Sub
    Private Sub AgregarMarcadorToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles AgregarMarcadorToolStripMenuItem3.Click
        If ListBox4.SelectedItem = Nothing Then
        Else
            Debugger.MarcaFavorites = ListBox4.SelectedItem
        End If
        Debugger.SaveConfig()
        NewChanges = NewChanges + 1
    End Sub

    '=================Mover de posicion=================
    Private Sub ArribaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ArribaToolStripMenuItem1.Click
        MoverItem_JugadasDeLaPartida(0)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub AbajoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AbajoToolStripMenuItem1.Click
        MoverItem_JugadasDeLaPartida(1)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub ArribaToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ArribaToolStripMenuItem2.Click
        MoverItem_Destacados(0)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub AbajoToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles AbajoToolStripMenuItem2.Click
        MoverItem_Destacados(1)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub ArribaToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ArribaToolStripMenuItem3.Click
        MoverItem_Gameplays(0)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub AbajoToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles AbajoToolStripMenuItem3.Click
        MoverItem_Gameplays(1)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub ArribaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArribaToolStripMenuItem.Click
        MoverItem_Favoritos(0)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub AbajoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbajoToolStripMenuItem.Click
        MoverItem_Favoritos(1)
        NewChanges = NewChanges + 1
    End Sub
    Private Sub MoverItem_JugadasDeLaPartida(Index As Integer)
        Try
            Dim j As Long
            Dim ar As Long
            Dim arTmp As String
            Dim sTmp As String
            j = ListBox1.Items.Count - 1
            ar = MemoryListPOTG.Count - 1
            If j < 0 Then Exit Sub
            If ar < 0 Then Exit Sub
            j = ListBox1.SelectedIndex
            ar = ListBox1.SelectedIndex
            If j < 0 Then j = 0
            If ar < 0 Then ar = 0
            Select Case Index
                Case 0 'Subir
                    If j = 0 Then Exit Sub
                    sTmp = ListBox1.Items(j)
                    arTmp = MemoryListPOTG.Item(ar)
                    ListBox1.Items(j) = ListBox1.Items(j - 1)
                    MemoryListPOTG.Item(ar) = MemoryListPOTG.Item(ar - 1)
                    ListBox1.Items(j - 1) = sTmp
                    MemoryListPOTG.Item(ar - 1) = arTmp
                    ListBox1.SelectedIndex = j - 1
                Case 1 'Bajar
                    If j = ListBox1.Items.Count - 1 Then Exit Sub
                    sTmp = ListBox1.Items(j)
                    arTmp = MemoryListPOTG.Item(ar)
                    ListBox1.Items(j) = ListBox1.Items(j + 1)
                    MemoryListPOTG.Item(ar) = MemoryListPOTG.Item(ar + 1)
                    ListBox1.Items(j + 1) = sTmp
                    MemoryListPOTG.Item(ar + 1) = arTmp
                    ListBox1.SelectedIndex = j + 1
            End Select
        Catch ex As Exception
            Console.WriteLine("[OW_Manager@MoverItem_JugadasDeLaPartida]Error: " & ex.Message)
        End Try
    End Sub
    Private Sub MoverItem_Destacados(Index As Integer)
        Try
            Dim j As Long
            Dim ar As Long
            Dim arTmp As String
            Dim sTmp As String
            j = ListBox2.Items.Count - 1
            ar = MemoryListHightlights.Count - 1
            If j < 0 Then Exit Sub
            If ar < 0 Then Exit Sub
            j = ListBox2.SelectedIndex
            ar = ListBox2.SelectedIndex
            If j < 0 Then j = 0
            If ar < 0 Then ar = 0
            Select Case Index
                Case 0 'Subir
                    If j = 0 Then Exit Sub
                    sTmp = ListBox2.Items(j)
                    arTmp = MemoryListHightlights.Item(ar)
                    ListBox2.Items(j) = ListBox2.Items(j - 1)
                    MemoryListHightlights.Item(ar) = MemoryListHightlights.Item(ar - 1)
                    ListBox2.Items(j - 1) = sTmp
                    MemoryListHightlights.Item(ar - 1) = arTmp
                    ListBox2.SelectedIndex = j - 1
                Case 1 'Bajar
                    If j = ListBox2.Items.Count - 1 Then Exit Sub
                    sTmp = ListBox2.Items(j)
                    arTmp = MemoryListHightlights.Item(ar)
                    ListBox2.Items(j) = ListBox2.Items(j + 1)
                    MemoryListHightlights.Item(ar) = MemoryListHightlights.Item(ar + 1)
                    ListBox2.Items(j + 1) = sTmp
                    MemoryListHightlights.Item(ar + 1) = arTmp
                    ListBox2.SelectedIndex = j + 1
            End Select
        Catch ex As Exception
            Console.WriteLine("[OW_Manager@MoverItem_Destacados]Error: " & ex.Message)
        End Try
    End Sub
    Private Sub MoverItem_Gameplays(Index As Integer)
        Try
            Dim j As Long
            Dim ar As Long
            Dim arTmp As String
            Dim sTmp As String
            j = ListBox3.Items.Count - 1
            ar = MemoryListGameplays.Count - 1
            If j < 0 Then Exit Sub
            If ar < 0 Then Exit Sub
            j = ListBox3.SelectedIndex
            ar = ListBox3.SelectedIndex
            If j < 0 Then j = 0
            If ar < 0 Then ar = 0
            Select Case Index
                Case 0 'Subir
                    If j = 0 Then Exit Sub
                    sTmp = ListBox3.Items(j)
                    arTmp = MemoryListGameplays.Item(ar)
                    ListBox3.Items(j) = ListBox3.Items(j - 1)
                    MemoryListGameplays.Item(ar) = MemoryListGameplays.Item(ar - 1)
                    ListBox3.Items(j - 1) = sTmp
                    MemoryListGameplays.Item(ar - 1) = arTmp
                    ListBox3.SelectedIndex = j - 1
                Case 1 'Bajar
                    If j = ListBox3.Items.Count - 1 Then Exit Sub
                    sTmp = ListBox3.Items(j)
                    arTmp = MemoryListGameplays.Item(ar)
                    ListBox3.Items(j) = ListBox3.Items(j + 1)
                    MemoryListGameplays.Item(ar) = MemoryListGameplays.Item(ar + 1)
                    ListBox3.Items(j + 1) = sTmp
                    MemoryListGameplays.Item(ar + 1) = arTmp
                    ListBox3.SelectedIndex = j + 1
            End Select
        Catch ex As Exception
            Console.WriteLine("[OW_Manager@MoverItem_Gameplays]Error: " & ex.Message)
        End Try
    End Sub
    Private Sub MoverItem_Favoritos(Index As Integer)
        Try
            Dim j As Long
            Dim ar As Long
            Dim arTmp As String
            Dim sTmp As String
            j = ListBox4.Items.Count - 1
            ar = MemoryListFavorites.Count - 1
            If j < 0 Then Exit Sub
            If ar < 0 Then Exit Sub
            j = ListBox4.SelectedIndex
            ar = ListBox4.SelectedIndex
            If j < 0 Then j = 0
            If ar < 0 Then ar = 0
            Select Case Index
                Case 0 'Subir
                    If j = 0 Then Exit Sub
                    sTmp = ListBox4.Items(j)
                    arTmp = MemoryListFavorites.Item(ar)
                    ListBox4.Items(j) = ListBox4.Items(j - 1)
                    MemoryListFavorites.Item(ar) = MemoryListFavorites.Item(ar - 1)
                    ListBox4.Items(j - 1) = sTmp
                    MemoryListFavorites.Item(ar - 1) = arTmp
                    ListBox4.SelectedIndex = j - 1
                Case 1 'Bajar
                    If j = ListBox4.Items.Count - 1 Then Exit Sub
                    sTmp = ListBox4.Items(j)
                    arTmp = MemoryListFavorites.Item(ar)
                    ListBox4.Items(j) = ListBox4.Items(j + 1)
                    MemoryListFavorites.Item(ar) = MemoryListFavorites.Item(ar + 1)
                    ListBox4.Items(j + 1) = sTmp
                    MemoryListFavorites.Item(ar + 1) = arTmp
                    ListBox4.SelectedIndex = j + 1
            End Select
        Catch ex As Exception
            Console.WriteLine("[OW_Manager@MoverItem_Favoritos]Error: " & ex.Message)
        End Try
    End Sub

    Dim aLatch As Boolean = False
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        If NewChanges > 0 Then
            If MessageBox.Show("Hay cambios sin guardar" & vbCrLf & "¿Quiere continuar?", "Cambios sin Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                If aLatch = False Then
                    AxWindowsMediaPlayer1.Dock = DockStyle.Fill
                    aLatch = True
                    Label17.Text = ">"
                    Me.Width = "862"
                    frmSizeSwitch = "862"
                    Me.CenterToScreen()
                Else
                    Debugger.Restart()
                End If
            End If
        Else
            If aLatch = False Then
                AxWindowsMediaPlayer1.Dock = DockStyle.Fill
                aLatch = True
                Label17.Text = ">"
                Me.Width = "862"
                frmSizeSwitch = "862"
                Me.CenterToScreen()
            Else
                Debugger.Restart()
            End If
        End If
    End Sub

#Region "CambiarNombres" 'En desarrollo, realmente esto consume la vida
    Private Sub CambiarJuegoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CambiarJuegoToolStripMenuItem.Click
        Dim TXB = InputBox("Ingrese el nuevo nombre para la Aplicacion", "Nuevo Nombre", Label1.Text)
        If TXB = Nothing Then
        Else
            Debugger.Juego = TXB
            Debugger.SaveConfig()
        End If
        NewChanges = NewChanges + 1
    End Sub
    Private Sub Label3_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Label3.MouseDoubleClick
        Dim TXB = InputBox("Ingrese el nuevo nombre para la Categoria", "Nuevo Nombre", Label3.Text)
        If TXB = Nothing Then
        Else
            If Debugger.Lista = "L1" Then
                Debugger.Categoria1Lista1 = TXB
                Debugger.SaveConfig()
            ElseIf Debugger.Lista = "L2" Then
                Debugger.Categoria1Lista2 = TXB
                Debugger.SaveConfig()
            ElseIf Debugger.Lista = "L3" Then
                Debugger.Categoria1Lista3 = TXB
                Debugger.SaveConfig()
            End If
        End If
    End Sub
    Private Sub Label4_MouseDoubleClick(sender As Object, e As EventArgs) Handles Label4.MouseDoubleClick
        Dim TXB = InputBox("Ingrese el nuevo nombre para la Categoria", "Nuevo Nombre", Label4.Text)
        If TXB = Nothing Then
        Else
            If Debugger.Lista = "L1" Then
                Debugger.Categoria2Lista1 = TXB
                Debugger.SaveConfig()
            ElseIf Debugger.Lista = "L2" Then
                Debugger.Categoria2Lista2 = TXB
                Debugger.SaveConfig()
            ElseIf Debugger.Lista = "L3" Then
                Debugger.Categoria2Lista3 = TXB
                Debugger.SaveConfig()
            End If
        End If
    End Sub
    Private Sub Label5_MouseDoubleClick(sender As Object, e As EventArgs) Handles Label5.MouseDoubleClick
        Dim TXB = InputBox("Ingrese el nuevo nombre para la Categoria", "Nuevo Nombre", Label5.Text)
        If TXB = Nothing Then
        Else
            If Debugger.Lista = "L1" Then
                Debugger.Categoria3Lista1 = TXB
                Debugger.SaveConfig()
            ElseIf Debugger.Lista = "L2" Then
                Debugger.Categoria3Lista2 = TXB
                Debugger.SaveConfig()
            ElseIf Debugger.Lista = "L3" Then
                Debugger.Categoria3Lista3 = TXB
                Debugger.SaveConfig()
            End If
        End If
    End Sub

    'String: RUTA>Nombre
    'Dim Cadena As String() = TEXTO.Split(">")
    'Dim RUTA As String
    'Dim NOMBRE As String
    'RUTA = Cadena(0)
    'NOMBRE = Cadena(1)
    Private Sub CambiarNombreToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles CambiarNombreToolStripMenuItem2.Click
        'obtener el nombre actual
        Dim NOMBRE As String = ListBox3.SelectedItem
        'cambiar el nombre el la lista
        ListBox3.Items(ListBox3.SelectedIndex) = InputBox("Ingrese un nombre", "Renombrar", NOMBRE)

        'agregar el nuevo nombre al MemoryList

    End Sub
#End Region
#End Region
End Class
'Como dijo un Sabio "Nunca hay Muchos 'Try Catch' en los Programas..."

'BUG: Cuando se cambia de lista se cierra si esta marcada la opcion en el SaveChangesDialog