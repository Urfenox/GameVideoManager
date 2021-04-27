Imports System.Runtime.InteropServices
Imports System.Text
Public Class Debugger
    Dim DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\PS4VideoManager"
    Public CambioLista As Boolean = False
    Public VideoManagerForm As Form = OW_Manager

    Dim AlreadyRegistered As Boolean = False

    Public Lista As String = "L1"
    Public Espanglish As String = "0"

    Public CriticalUpdates As Boolean = False
    Public DarkMode As Boolean = False

    Public MarcaPOTG As String = "None"
    Public MarcaHighlights As String = "None"
    Public MarcaGameplay As String = "None"
    Public MarcaFavorites As String = "None"

    Public OfflineMode As Boolean = False

    Public Categoria1Lista1 As String = "Jugadas de la Partida"
    Public Categoria2Lista1 As String = "Destacados"
    Public Categoria3Lista1 As String = "Gameplays"
    Public Categoria3Lista3 As String = "Gameplays"
    Public Categoria2Lista3 As String = "Destacados"
    Public Categoria1Lista3 As String = "Jugadas de la Partida"
    Public Categoria3Lista2 As String = "Gameplays"
    Public Categoria2Lista2 As String = "Destacados"
    Public Categoria1Lista2 As String = "Jugadas de la Partida"
    Public Juego As String = "PS4: Overwatch"
    Public UndexList As String = "1"
    Public NoShowWithoutChanges As Boolean = False
    Dim ConfigFile As String = DIRCommons & "\Config.ini"

    Private Sub Debugger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim parametros As String
        parametros = Microsoft.VisualBasic.Command
        If My.Computer.FileSystem.FileExists(ConfigFile) = True Then
            AlreadyRegistered = True
            LoadConfig()
        Else
            My.Computer.FileSystem.WriteAllText(ConfigFile, "#CRZ Labs VideoManager Configuration File" &
                                                    vbCrLf & "[STR]" &
                                                    vbCrLf & "Lista=" & Lista &
                                                    vbCrLf & "Espanglish=" & Espanglish &
                                                    vbCrLf & "MarcaPOTG=" & MarcaPOTG &
                                                    vbCrLf & "MarcaHighlights=" & MarcaHighlights &
                                                    vbCrLf & "MarcaGameplay=" & MarcaGameplay &
                                                    vbCrLf & "MarcaFavorites=" & MarcaFavorites &
                                                    vbCrLf & "Categoria1Lista1=" & Categoria1Lista1 &
                                                    vbCrLf & "Categoria2Lista1=" & Categoria2Lista1 &
                                                    vbCrLf & "Categoria3Lista1=" & Categoria3Lista1 &
                                                    vbCrLf & "Categoria3Lista3=" & Categoria3Lista3 &
                                                    vbCrLf & "Categoria2Lista3=" & Categoria2Lista3 &
                                                    vbCrLf & "Categoria1Lista3=" & Categoria1Lista3 &
                                                    vbCrLf & "Categoria3Lista2=" & Categoria3Lista2 &
                                                    vbCrLf & "Categoria2Lista2=" & Categoria2Lista2 &
                                                    vbCrLf & "Categoria1Lista2=" & Categoria1Lista2 &
                                                    vbCrLf & "Juego=" & Juego &
                                                    vbCrLf & "UndexList=" & UndexList &
                                                    vbCrLf & "[BLN]" &
                                                    vbCrLf & "CriticalUpdates=" & CriticalUpdates &
                                                    vbCrLf & "DarkMode=" & DarkMode &
                                                    vbCrLf & "OfflineMode=" & OfflineMode &
                                                    vbCrLf & "NoShowWithoutChanges=" & NoShowWithoutChanges, False)
        End If
        If parametros = Nothing Then
            InicioComun()
        ElseIf parametros = "/FactoryReset" Then
            FactoryReset()
        End If
    End Sub

    Sub InicioComun()
        If My.Computer.Network.IsAvailable = False Then
            If CriticalUpdates = True Then
                If Espanglish = "ESP" Then
                    MsgBox("Debes conectarte a internet para actualizar el estado de esta aplicacion", MsgBoxStyle.Information, "Sistema de Seguridad")
                ElseIf Espanglish = "ENG" Then
                    MsgBox("You must connect to the internet to update the status of this application", MsgBoxStyle.Information, "Security System")
                End If
                End
            End If
        End If
        If DarkMode = True Then
            ActiveTheme(False)
        Else
            ActiveTheme(True)
        End If
        Dim myCurrentLanguage As InputLanguage = InputLanguage.CurrentInputLanguage
        If myCurrentLanguage.Culture.EnglishName.Contains("Spanish") Then
            Idioma.Español.LANG_Español()
            Espanglish = "ESP"
        ElseIf myCurrentLanguage.Culture.EnglishName.Contains("English") Then
            Idioma.Ingles.LANG_English()
            Espanglish = "ENG"
        Else
            LangSelector.ShowDialog()
        End If
        If Espanglish = "ESP" Then
            Idioma.Español.LANG_Español()
        ElseIf Espanglish = "ENG" Then
            Idioma.Ingles.LANG_English()
        Else
            LangSelector.ShowDialog()
            End If
        VideoManagerForm.Show()
    End Sub

    Sub LoadConfig()
        Try
            Console.WriteLine("CALLED LOAD")
            'Dim Lineas = IO.File.ReadLines(ConfigFile)
            'Lista = Lineas(1).Split(">"c)(1).Trim()
            'Espanglish = Lineas(2).Split(">"c)(1).Trim()
            'MarcaPOTG = Lineas(3).Split(">"c)(1).Trim()
            'MarcaHighlights = Lineas(4).Split(">"c)(1).Trim()
            'MarcaGameplay = Lineas(5).Split(">"c)(1).Trim()
            'MarcaFavorites = Lineas(6).Split(">"c)(1).Trim()
            'Categoria1Lista1 = Lineas(7).Split(">"c)(1).Trim()
            'Categoria2Lista1 = Lineas(8).Split(">"c)(1).Trim()
            'Categoria3Lista1 = Lineas(9).Split(">"c)(1).Trim()
            'Categoria3Lista3 = Lineas(10).Split(">"c)(1).Trim()
            'Categoria2Lista3 = Lineas(11).Split(">"c)(1).Trim()
            'Categoria1Lista3 = Lineas(12).Split(">"c)(1).Trim()
            'Categoria3Lista2 = Lineas(13).Split(">"c)(1).Trim()
            'Categoria2Lista2 = Lineas(14).Split(">"c)(1).Trim()
            'Categoria1Lista2 = Lineas(15).Split(">"c)(1).Trim()
            'Juego = Lineas(16).Split(">"c)(1).Trim()
            'UndexList = Lineas(17).Split(">"c)(1).Trim()
            'CriticalUpdates = Boolean.Parse(Lineas(18).Split(">"c)(1).Trim())
            'DarkMode = Boolean.Parse(Lineas(19).Split(">"c)(1).Trim())
            'OfflineMode = Boolean.Parse(Lineas(20).Split(">"c)(1).Trim())
            'NoShowWithoutChanges = Boolean.Parse(Lineas(21).Split(">"c)(1).Trim())

            Lista = GetIniValue("STR", "Lista", ConfigFile)
            Espanglish = GetIniValue("STR", "Espanglish", ConfigFile)
            MarcaPOTG = GetIniValue("STR", "MarcaPOTG", ConfigFile)
            MarcaHighlights = GetIniValue("STR", "MarcaHighlights", ConfigFile)
            MarcaGameplay = GetIniValue("STR", "MarcaGameplay", ConfigFile)
            MarcaFavorites = GetIniValue("STR", "MarcaFavorites", ConfigFile)
            Categoria1Lista1 = GetIniValue("STR", "Categoria1Lista1", ConfigFile)
            Categoria2Lista1 = GetIniValue("STR", "Categoria2Lista1", ConfigFile)
            Categoria3Lista1 = GetIniValue("STR", "Categoria3Lista1", ConfigFile)
            Categoria3Lista3 = GetIniValue("STR", "Categoria3Lista3", ConfigFile)
            Categoria2Lista3 = GetIniValue("STR", "Categoria2Lista3", ConfigFile)
            Categoria1Lista3 = GetIniValue("STR", "Categoria1Lista3", ConfigFile)
            Categoria3Lista2 = GetIniValue("STR", "Categoria3Lista2", ConfigFile)
            Categoria2Lista2 = GetIniValue("STR", "Categoria2Lista2", ConfigFile)
            Categoria1Lista2 = GetIniValue("STR", "Categoria1Lista2", ConfigFile)
            Juego = GetIniValue("STR", "Juego", ConfigFile)
            UndexList = GetIniValue("STR", "UndexList", ConfigFile)
            CriticalUpdates = Boolean.Parse(GetIniValue("BLN", "CriticalUpdates", ConfigFile))
            DarkMode = Boolean.Parse(GetIniValue("BLN", "DarkMode", ConfigFile))
            OfflineMode = Boolean.Parse(GetIniValue("BLN", "OfflineMode", ConfigFile))
            NoShowWithoutChanges = Boolean.Parse(GetIniValue("BLN", "NoShowWithoutChanges", ConfigFile))
        Catch ex As Exception
            Console.WriteLine("[LoadConfig@Debugger]Error: " & ex.Message)
        End Try
    End Sub

    Sub SaveConfig()
        Try
            Console.WriteLine("CALLED SAVE")
            If My.Computer.FileSystem.FileExists(ConfigFile) = True Then
                My.Computer.FileSystem.DeleteFile(ConfigFile)
            End If
            My.Computer.FileSystem.WriteAllText(ConfigFile, "#CRZ Labs VideoManager Configuration File" &
                                                    vbCrLf & "[STR]" &
                                                    vbCrLf & "Lista=" & Lista &
                                                    vbCrLf & "Espanglish=" & Espanglish &
                                                    vbCrLf & "MarcaPOTG=" & MarcaPOTG &
                                                    vbCrLf & "MarcaHighlights=" & MarcaHighlights &
                                                    vbCrLf & "MarcaGameplay=" & MarcaGameplay &
                                                    vbCrLf & "MarcaFavorites=" & MarcaFavorites &
                                                    vbCrLf & "Categoria1Lista1=" & Categoria1Lista1 &
                                                    vbCrLf & "Categoria2Lista1=" & Categoria2Lista1 &
                                                    vbCrLf & "Categoria3Lista1=" & Categoria3Lista1 &
                                                    vbCrLf & "Categoria3Lista3=" & Categoria3Lista3 &
                                                    vbCrLf & "Categoria2Lista3=" & Categoria2Lista3 &
                                                    vbCrLf & "Categoria1Lista3=" & Categoria1Lista3 &
                                                    vbCrLf & "Categoria3Lista2=" & Categoria3Lista2 &
                                                    vbCrLf & "Categoria2Lista2=" & Categoria2Lista2 &
                                                    vbCrLf & "Categoria1Lista2=" & Categoria1Lista2 &
                                                    vbCrLf & "Juego=" & Juego &
                                                    vbCrLf & "UndexList=" & UndexList &
                                                    vbCrLf & "[BLN]" &
                                                    vbCrLf & "CriticalUpdates=" & CriticalUpdates &
                                                    vbCrLf & "DarkMode=" & DarkMode &
                                                    vbCrLf & "OfflineMode=" & OfflineMode &
                                                    vbCrLf & "NoShowWithoutChanges=" & NoShowWithoutChanges, False)
            LoadConfig()
        Catch ex As Exception
            Console.WriteLine("[SaveConfig@Debugger]Error: " & ex.Message)
        End Try
    End Sub

    Sub ActiveTheme(ByVal Defecto As Boolean)
        Try
            If Defecto = False Then
                OW_Manager.BackColor = Color.DimGray
                OW_Manager.ListBox1.BackColor = Color.DimGray
                OW_Manager.ListBox2.BackColor = Color.DimGray
                OW_Manager.ListBox3.BackColor = Color.DimGray
                OW_Manager.ListBox4.BackColor = Color.DimGray
                OW_Manager.Panel1.BackColor = Color.Silver
                OW_Manager.Panel2.BackColor = Color.Silver
                OW_Manager.Panel3.BackColor = Color.Silver
                OW_Manager.Button1.BackColor = Color.DimGray
                SaveChangesDialog.BackColor = Color.DimGray
                SaveChangesDialog.Button1.BackColor = Color.DimGray
                SaveChangesDialog.Button2.BackColor = Color.DimGray
                SaveChangesDialog.Button3.BackColor = Color.DimGray
                SaveChangesDialog.Button4.BackColor = Color.DimGray
                ListCollector.BackColor = Color.DimGray
                ListCollector.Button1.BackColor = Color.DimGray
                ListCollector.Button2.BackColor = Color.DimGray
                ListCollector.Button3.BackColor = Color.DimGray
                OW_BackUp.BackColor = Color.DimGray
                OW_BackUp.TabPage1.BackColor = Color.DimGray
                OW_BackUp.TabPage2.BackColor = Color.DimGray
                OW_BackUp.Button1.BackColor = Color.DimGray
                About.BackColor = Color.DimGray
                About.Button1.BackColor = Color.DimGray
            Else
                OW_Manager.BackColor = DefaultBackColor
                OW_Manager.ListBox1.BackColor = DefaultBackColor
                OW_Manager.ListBox2.BackColor = DefaultBackColor
                OW_Manager.ListBox3.BackColor = DefaultBackColor
                OW_Manager.ListBox4.BackColor = DefaultBackColor
                OW_Manager.Panel1.BackColor = Color.Silver
                OW_Manager.Panel2.BackColor = Color.Silver
                OW_Manager.Panel3.BackColor = Color.Silver
                OW_Manager.Button1.BackColor = DefaultBackColor
                SaveChangesDialog.BackColor = DefaultBackColor
                SaveChangesDialog.Button1.BackColor = DefaultBackColor
                SaveChangesDialog.Button2.BackColor = DefaultBackColor
                SaveChangesDialog.Button3.BackColor = DefaultBackColor
                SaveChangesDialog.Button4.BackColor = DefaultBackColor
                ListCollector.BackColor = DefaultBackColor
                ListCollector.Button1.BackColor = DefaultBackColor
                ListCollector.Button2.BackColor = DefaultBackColor
                ListCollector.Button3.BackColor = DefaultBackColor
                OW_BackUp.BackColor = DefaultBackColor
                OW_BackUp.TabPage1.BackColor = DefaultBackColor
                OW_BackUp.TabPage2.BackColor = DefaultBackColor
                OW_BackUp.Button1.BackColor = DefaultBackColor
                About.BackColor = DefaultBackColor
                About.Button1.BackColor = DefaultBackColor
            End If
        Catch ex As Exception
            Console.WriteLine("[Debugger@DarkMode]Error: " & ex.Message)
        End Try
    End Sub

    Sub ResetTheme()
        OW_Manager.Close()
        OW_Manager.Show()
    End Sub

    Sub StartSecureForm(ByVal form As Form)
        form.Show()
    End Sub

    Sub Restart()
        Try
            OW_Manager.Dispose()
            ListCollector.Dispose()
            SaveChangesDialog.Dispose()
            OW_BackUp.Dispose()
            OW_Manager.Show()
            OW_Manager.Refresh()
        Catch ex As Exception
            MsgBox("Error: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Sistema Debugger")
        End Try
    End Sub

    Sub FactoryReset()
        Try
            If MessageBox.Show("¿Seguro que quiere hacer un Factory Reset?" & vbCrLf & "Esto eliminara la Base de Datos del Programa y Configuraciones" & vbCrLf & "¿Deseas Continuar?", "Ejecutar Factory Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                My.Computer.FileSystem.DeleteDirectory(DIRCommons, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.SendToRecycleBin)
                Lista = "L1"
                Espanglish = "0"
                SaveConfig()
                MsgBox("Aplicacion vuelta a la version de Fabrica", MsgBoxStyle.Information, "Factory Reset")
                End
            Else
                End
            End If
        Catch ex As Exception
            MsgBox("Error: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Sistema Debugger")
        End Try
    End Sub

    Sub CambioDeLista()
        Try
            ListCollector.Close()
            SaveChangesDialog.Close()
            OW_Manager.Close()
            OW_Manager.Show()
            OW_Manager.Refresh()
        Catch ex As Exception
            MsgBox("Error: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Sistema Debugger")
        End Try
    End Sub

    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function
    'aString = GetIniValue("Key", "SubKey" AppConfig)
    Public Function GetIniValue(section As String, key As String, filename As String, Optional defaultValue As String = "") As String
        Dim sb As New StringBuilder(500)
        If GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filename) > 0 Then
            Return sb.ToString
        Else
            Return defaultValue
        End If
    End Function
End Class
'Aunque cueste trabajo creerlo, este modulo es IMPORTANTISIMO!