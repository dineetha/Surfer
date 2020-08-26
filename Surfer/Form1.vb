#Region "Surfer Developer 2015"
'Version 2.0.0.0 Source
'Global Square
'Copyright © Sudam Dineetha 2015
'Feedback sudam1st@gmail.com
#End Region

Imports Microsoft.VisualBasic.FileIO
Imports System.Collections.ObjectModel

Public Class Form1

#Region "Form Startup Events"

    Dim ar As New ArrayList
    Dim recent01 As String = 0
   
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim filename1 As String = Application.StartupPath & "\browsers.surf"
        Dim fields1 As String()
        Dim a1 As Integer
        Dim delimiter1 As String = ","
        Using parser As New TextFieldParser(filename1)
            parser.SetDelimiters(delimiter1)
            While Not parser.EndOfData
                ' Read in the fields for the current line
                fields1 = parser.ReadFields()
                ' Add code here to use data in fields variable.
                For a1 = 0 To fields1.Length - 2
                    Me.BrowserToolStripMenuItem.DropDownItems.Add(fields1(a1).Remove(0, fields1(a1).LastIndexOf("\") + 1))
                Next
            End While
        End Using

        Dim a As Integer
        Dim files As ReadOnlyCollection(Of String)
        files = My.Computer.FileSystem.GetFiles(Application.StartupPath & "\surferextensions", SearchOption.SearchAllSubDirectories, "*.exe")
        For a = 0 To files.Count - 1
            Me.PlugInsToolStripMenuItem.DropDownItems.Add(files(a))
        Next

        If My.Settings.welcome = "surf" Then
            My.Forms.surferwelcome.ShowDialog()
            My.Settings.welcome = "ok"
            SHChangeNotify(&H8000000, 0, 0, 0)
        End If


        'Catch Startup Command Line Args
        For Each param As String In My.Application.CommandLineArgs
            Try
                ' pass the file path if it exists
                OpenFromPath(param)
            Catch
                'just open the application with no file
            End Try
        Next param

    End Sub


#End Region

#Region "New"
    Private Sub surfnew()

        My.Forms.surfnew.ShowDialog()
        If My.Forms.surfnew.ok = 1 And My.Forms.surfnew.ListView1.SelectedItems.Count <> 0 Then
            Dim r As New RichTextBox
            Dim t As New TabPage
            Dim n As Integer
            t.Controls.Add(r)
            ar.Add(r)
            r.Dock = DockStyle.Fill
            n = TabControl1.TabPages.Count + 1
            If My.Forms.surfnew.ListView1.SelectedItems(0).Text.EndsWith(".htm") = True Then
                t.Text = "[" & n & "]" & " HTML Document"
                t.ToolTipText = "*.htm"
            ElseIf My.Forms.surfnew.ListView1.SelectedItems(0).Text.EndsWith(".css") = True Then
                t.Text = "[" & n & "]" & " CSS Document"
                t.ToolTipText = "*.css"
            ElseIf My.Forms.surfnew.ListView1.SelectedItems(0).Text.EndsWith(".js") = True Then
                t.Text = "[" & n & "]" & " JavaScript"
                t.ToolTipText = "*.js"
            End If
            TabControl1.Controls.Add(t)
            TabControl1.SelectedTab = t
            r.Font = New Font("Arial", 12, FontStyle.Bold)
            r.BorderStyle = BorderStyle.None
            r.AllowDrop = True
            r.EnableAutoDragDrop = True
            r.LoadFile(Application.StartupPath & "\Str\" & My.Forms.surfnew.ListView1.SelectedItems(0).Text, RichTextBoxStreamType.PlainText)
            syntaxcolor()
            r.ContextMenuStrip = Me.ContextMenuStrip1
        End If
    End Sub

    Private Sub NewToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        surfnew()
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        surfnew()
    End Sub

    '  Private Sub NewWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'Dim nw As New Form1
    '     nw.Show()
    ' End Sub

#End Region

#Region "recent files"
    Private Sub FileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem.Click
        If recent01 = 0 Then
            recent01 = 1
            Try
                Dim filename As String = Application.StartupPath & "\recent.surf"
                Dim fields As String()
                Dim recent As Integer
                Dim delimiter As String = ","
                Using parser As New TextFieldParser(filename)
                    parser.SetDelimiters(delimiter)
                    While Not parser.EndOfData
                        ' Read in the fields for the current line
                        fields = parser.ReadFields()
                        ' Add code here to use data in fields variable.

                        If fields.Length > 5 Then
                            For recent = fields.Length - 2 To fields.Length - 5 Step -1
                                Me.RecentFilesToolStripMenuItem.DropDownItems.Add(fields(recent))
                            Next
                        Else
                            For recent = fields.Length - 2 To 0 Step -1
                                Me.RecentFilesToolStripMenuItem.DropDownItems.Add(fields(recent))
                            Next
                        End If

                    End While
                End Using
            Catch ex As Exception
                MsgBox("Config file not found.", MsgBoxStyle.OkOnly, "Surfer")
            End Try
        End If
    End Sub
#End Region

#Region "open"
    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        open()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        open()
    End Sub

    Private Sub open()
        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim r As New RichTextBox
            Dim t As New TabPage
            Dim n As Integer
            t.Controls.Add(r)
            ar.Add(r)
            r.Dock = DockStyle.Fill
            n = TabControl1.TabPages.Count + 1
            If Me.OpenFileDialog1.FileName.EndsWith(".htm") = True Or Me.OpenFileDialog1.FileName.EndsWith(".html") = True Then
                t.Text = "[" & n & "]" & " HTML Document"
                t.ToolTipText = Me.OpenFileDialog1.FileName
                Me.Text = "Surfer " & Me.OpenFileDialog1.FileName
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\recent.surf", Me.OpenFileDialog1.FileName & ",", True)
            ElseIf Me.OpenFileDialog1.FileName.EndsWith(".css") = True Then
                t.Text = "[" & n & "]" & " CSS Document"
                t.ToolTipText = "*.htm" = Me.OpenFileDialog1.FileName
                Me.Text = "Surfer " & Me.OpenFileDialog1.FileName
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\recent.surf", Me.OpenFileDialog1.FileName & ",", True)
            ElseIf Me.OpenFileDialog1.FileName.EndsWith(".js") = True Then
                t.Text = "[" & n & "]" & " JavaScript"
                t.ToolTipText = Me.OpenFileDialog1.FileName
                Me.Text = "Surfer " & Me.OpenFileDialog1.FileName
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\recent.surf", Me.OpenFileDialog1.FileName & ",", True)
            ElseIf Me.OpenFileDialog1.FileName.EndsWith(".txt") = True Then
                t.Text = "[" & n & "]" & " Text Document"
                t.ToolTipText = Me.OpenFileDialog1.FileName
                Me.Text = "Surfer " & Me.OpenFileDialog1.FileName
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\recent.surf", Me.OpenFileDialog1.FileName & ",", True)
            Else
                t.Text = "[" & n & "]" & " Unidentified"
                t.ToolTipText = Me.OpenFileDialog1.FileName
                Me.Text = "Surfer " & Me.OpenFileDialog1.FileName
            End If
            TabControl1.Controls.Add(t)
            TabControl1.SelectedTab = t
            ' t.ContextMenuStrip = Me.ContextMenuStrip3
            r.Font = New Font("Arial", 12, FontStyle.Bold)
            r.BorderStyle = BorderStyle.None
            r.AllowDrop = True
            r.EnableAutoDragDrop = True
            r.LoadFile(Me.OpenFileDialog1.FileName, RichTextBoxStreamType.PlainText)
            syntaxcolor()
            r.ContextMenuStrip = Me.ContextMenuStrip1
        End If
    End Sub
#End Region

    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).LoadFile(Me.OpenFileDialog1.FileName, RichTextBoxStreamType.PlainText)
                syntaxcolor()
            End If
        End If
    End Sub

    Private Sub AppendToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppendToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).AppendText(My.Computer.FileSystem.ReadAllText(Me.OpenFileDialog1.FileName, System.Text.Encoding.UTF8))
                syntaxcolor()
            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            If Me.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SaveFile(Me.TabControl1.SelectedTab.ToolTipText, RichTextBoxStreamType.PlainText)
            Else
                save()
            End If
        Catch ex As Exception
            MsgBox("Nothing to Save Plese click New or Open on the File menu")
        End Try
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        save()
    End Sub

    Private Sub save()
        Try
            If Me.TabControl1.SelectedTab.ToolTipText.EndsWith(".htm") = True Then
                Me.SaveFileDialog1.Filter = "HTML(*.htm,*.html)|*.htm;*.html"
            ElseIf Me.TabControl1.SelectedTab.ToolTipText.EndsWith(".css") = True Then
                Me.SaveFileDialog1.Filter = "CSS(*.css)|*.css"
            ElseIf Me.TabControl1.SelectedTab.ToolTipText.EndsWith(".js") = True Then
                Me.SaveFileDialog1.Filter = "JS(*.js)|*.js"
            Else
                Me.SaveFileDialog1.Filter = "HTML(*.htm,*.html)|*.htm;*.html"
            End If
            If Me.SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SaveFile(Me.SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText)
                Me.TabControl1.SelectedTab.ToolTipText = Me.SaveFileDialog1.FileName
                Me.Text = "Surfer " & Me.TabControl1.SelectedTab.ToolTipText
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\recent.surf", Me.SaveFileDialog1.FileName & ",", True)
            End If
        Catch ex As Exception
            MsgBox("Nothing to Save Plese click New or Open on the File menu")
        End Try
    End Sub
    Private Sub SaveFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Me.Text = "Surfer " & Me.TabControl1.SelectedTab.ToolTipText
    End Sub

    Private Sub RichTextToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            Me.SaveFileDialog1.Filter = "Rich Text(*.rtf)|*.rtf"
            If Me.SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SaveFile(Me.SaveFileDialog1.FileName, RichTextBoxStreamType.RichText)
            End If
        End If
    End Sub

    Private Sub TextToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            Me.SaveFileDialog1.Filter = "Text(*.txt)|*.txt"
            If Me.SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SaveFile(Me.SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText)
            End If
        End If
    End Sub

    Private Sub HTMLScriptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HTMLScriptToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            Me.SaveFileDialog1.Filter = "HTML(*.htm)|*.htm"

            If Me.SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim str As String = CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Text.Replace("<", "&lt;")
                Dim str1 As String = str.Replace(">", "&gt;")
                My.Computer.FileSystem.WriteAllText(Me.SaveFileDialog1.FileName, "<!DOCTYPE HTML><!--Surfer Developer Auto Genarated Code--><html><head><title>Surfer</title></head><body><pre>" & str1 & "</pre></body></html>", False)
            End If
        End If
    End Sub


    Private Sub RecentFilesToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles RecentFilesToolStripMenuItem.DropDownItemClicked
        If My.Computer.FileSystem.FileExists(e.ClickedItem.Text) Then
            Dim r As New RichTextBox
            Dim t As New TabPage
            Dim n As Integer
            t.Controls.Add(r)
            ar.Add(r)
            r.Dock = DockStyle.Fill
            n = TabControl1.TabPages.Count + 1
            If e.ClickedItem.Text.EndsWith(".htm") = True Or e.ClickedItem.Text.EndsWith(".html") = True Then
                t.Text = "[" & n & "]" & " HTML Document"
                t.ToolTipText = e.ClickedItem.Text
                Me.Text = "Surfer " & e.ClickedItem.Text
            ElseIf e.ClickedItem.Text.EndsWith(".css") = True Then
                t.Text = "[" & n & "]" & " CSS Document"
                t.ToolTipText = e.ClickedItem.Text
                Me.Text = "Surfer " & e.ClickedItem.Text

            ElseIf e.ClickedItem.Text.EndsWith(".js") = True Then
                t.Text = "[" & n & "]" & " JavaScript"
                t.ToolTipText = e.ClickedItem.Text
                Me.Text = "Surfer " & e.ClickedItem.Text

            ElseIf e.ClickedItem.Text.EndsWith(".txt") = True Then
                t.Text = "[" & n & "]" & " Text Document"
                t.ToolTipText = e.ClickedItem.Text
                Me.Text = "Surfer " & e.ClickedItem.Text

            Else
                t.Text = "[" & n & "]" & " Unidentified"
                t.ToolTipText = e.ClickedItem.Text
                Me.Text = "Surfer " & e.ClickedItem.Text
            End If
            TabControl1.Controls.Add(t)
            TabControl1.SelectedTab = t
            't.ContextMenuStrip = Me.ContextMenuStrip3
            r.Font = New Font("Arial", 12, FontStyle.Bold)
            r.BorderStyle = BorderStyle.None
            r.AllowDrop = True
            r.EnableAutoDragDrop = True
            r.LoadFile(e.ClickedItem.Text, RichTextBoxStreamType.PlainText)
            syntaxcolor()
            r.ContextMenuStrip = Me.ContextMenuStrip1
        Else
            MsgBox("File is not exists", 4096 + 48)
        End If

    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        If Me.TabControl1.TabCount = 0 Then
        Else
            Dim str As String = CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Text.Replace("<", "&lt;")
            Dim str1 As String = str.Replace(">", "&gt;")
            Dim str2 As String = "<!DOCTYPE HTML><!--Surfer Developer Auto Genarated Code--><html><head><title>Surfer</title></head><body><pre>" & str1 & "</pre></body></html>"
            My.Forms.surfbrowser.WebBrowser1.DocumentText = str2
            My.Forms.surfbrowser.Text = "Surfer Print"
            My.Forms.surfbrowser.ShowDialog()
            My.Forms.surfbrowser.WebBrowser1.ShowPrintDialog()
        End If
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        If Me.TabControl1.TabCount = 0 Then
        Else
            Dim str As String = CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Text.Replace("<", "&lt;")
            Dim str1 As String = str.Replace(">", "&gt;")
            Dim str2 As String = "<!DOCTYPE HTML><!--Surfer Developer Auto Genarated Code--><html><head><title>Surfer</title></head><body><pre>" & str1 & "</pre></body></html>"
            My.Forms.surfbrowser.WebBrowser1.DocumentText = str2
            My.Forms.surfbrowser.Text = "Surfer Print Preview"
            My.Forms.surfbrowser.ShowDialog()
            My.Forms.surfbrowser.WebBrowser1.ShowPrintPreviewDialog()
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.Text = "Surfer" And My.Forms.surfnew.ok = 0 Then
            Me.NotifyIcon1.Dispose()
        Else
            e.Cancel = True
            If MsgBox("Are you sure ? Unsaved documents will be clear", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                e.Cancel = False
                Me.NotifyIcon1.Dispose()
            End If
        End If

    End Sub

    Private Sub syntaxcolor()
        If CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Text.StartsWith("<!DOCTYPE HTML") = True Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Select(0, CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Text.IndexOf(">") + 1)
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Gray
        End If
        If CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<html>") > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<html>")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Brown
        End If
        If CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("</html>") > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("</html>")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Brown
        End If
        If CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<head>") > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<head>")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Red
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("</head>")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Red
        End If
        If CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("body") > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("body")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Red
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("/body")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Red
        End If
        If CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<!--") > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<!--")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Green
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("-->")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Green
        End If
        If CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<link rel=") > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<link rel=")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Chocolate
        End If
        If CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<script") > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("<script")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Chocolate
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Find("</script>")
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Chocolate
        End If
        CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).DeselectAll()
    End Sub

    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Undo()
        End If
    End Sub

    Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Redo()
        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectAll()
        End If
    End Sub

    Private Sub SelectedTextBrowserViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedTextBrowserViewToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            My.Forms.surfbrowser.WebBrowser1.DocumentText = CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText
            My.Forms.surfbrowser.Text = "Surfer Selected Text Quick Browser View"
            My.Forms.surfbrowser.ShowDialog()
        End If
    End Sub

    Private Sub CutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem1.Click
        cut()
    End Sub

    Private Sub CopyToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem1.Click
        copy()
    End Sub

    Private Sub PasteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem1.Click
        paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem1.Click
        If Me.TabControl1.TabCount > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectAll()
        End If

    End Sub

    Private Sub cut()
        If Me.TabControl1.TabCount > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Cut()
        End If
    End Sub
    Private Sub copy()
        If Me.TabControl1.TabCount > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Copy()
        End If

    End Sub

    Private Sub paste()
        If Me.TabControl1.TabCount > 0 Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Paste()
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If Me.TabControl1.TabCount > 0 Then
            syntaxcolor()
        End If
    End Sub

    Private Sub QuickViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuickViewToolStripMenuItem.Click
        If Me.TabControl1.TabCount = 0 Then
        Else
            If Me.TabControl1.SelectedTab.ToolTipText.EndsWith("*.htm") Or Me.TabControl1.SelectedTab.ToolTipText.EndsWith(".htm") Then
                If Me.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                    CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SaveFile(Me.TabControl1.SelectedTab.ToolTipText, RichTextBoxStreamType.PlainText)
                    My.Forms.surfbrowser.WebBrowser1.Navigate(Me.TabControl1.SelectedTab.ToolTipText)
                    My.Forms.surfbrowser.ShowDialog()
                Else
                    My.Forms.surfbrowser.WebBrowser1.DocumentText = CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Text
                    My.Forms.surfbrowser.Text = "Surfer Quick Browser View"
                    My.Forms.surfbrowser.ShowDialog()
                End If
            End If
        End If
    End Sub

    Private Sub DefaultViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultViewToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            If Me.TabControl1.SelectedTab.ToolTipText.EndsWith("*.htm") Or Me.TabControl1.SelectedTab.ToolTipText.EndsWith(".htm") Then
                If Me.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                    CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SaveFile(Me.TabControl1.SelectedTab.ToolTipText, RichTextBoxStreamType.PlainText)
                    Process.Start(Me.TabControl1.SelectedTab.ToolTipText)
                    Me.NotifyIcon1.ShowBalloonTip(8, "Surfer Developer Default Browser View", Me.TabControl1.SelectedTab.ToolTipText, ToolTipIcon.Info)
                Else
                    If MsgBox("First you should save document Do you want to save now ?", 36) = MsgBoxResult.Yes Then
                        save()
                        Process.Start(Me.TabControl1.SelectedTab.ToolTipText)
                        Me.NotifyIcon1.ShowBalloonTip(8, "Surfer Developer Default Browser View", Me.TabControl1.SelectedTab.ToolTipText, ToolTipIcon.Info)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub OtherBrowsersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OtherBrowsersToolStripMenuItem.Click
        My.Forms.settings.TabControl1.SelectTab(1)
        My.Forms.settings.ShowDialog()
    End Sub

    Private Sub ApplicationsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplicationsToolStripMenuItem.Click

    End Sub

    Private Sub ApplicationsToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ApplicationsToolStripMenuItem.DropDownItemClicked
        Try
            If e.ClickedItem.Text = "Notepad" Or e.ClickedItem.Text = "Wordpad" Or e.ClickedItem.Text = "Calculator" Then
                Process.Start(e.ClickedItem.Text & ".exe")
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "a")
        End Try
    End Sub

    Private Sub NotepadexeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotepadexeToolStripMenuItem.Click
        Try
            Process.Start("notepad")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub WordpadexeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WordpadexeToolStripMenuItem.Click
        Try
            Process.Start("wordpad")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculatorToolStripMenuItem.Click
        Try
            Process.Start("calc")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ApplicationsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplicationsToolStripMenuItem1.Click
        My.Forms.settings.TabControl1.SelectTab(1)
        My.Forms.settings.ShowDialog()
    End Sub

    Private Sub BrowserToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles BrowserToolStripMenuItem.DropDownItemClicked
        If e.ClickedItem.Text = "&Quick View" Or e.ClickedItem.Text = "&Default Browser View" Or e.ClickedItem.Text = "&Other Browsers" Then
        Else
            If Me.TabControl1.TabCount > 0 Then
                If Me.TabControl1.SelectedTab.ToolTipText.EndsWith("*.htm") Or Me.TabControl1.SelectedTab.ToolTipText.EndsWith(".htm") Then
                    If Me.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                        Process.Start(e.ClickedItem.Text, "file:///" & Uri.HexEscape(Me.TabControl1.SelectedTab.ToolTipText))
                    Else
                        Me.BrowserToolStripMenuItem.HideDropDown()
                        If MsgBox("First you should save document Do you want to save now ?", 36) = MsgBoxResult.Yes Then
                            save()
                            Process.Start(e.ClickedItem.Text, "file:///" & Uri.HexEscape(Me.TabControl1.SelectedTab.ToolTipText))
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ColorPalletteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorPalletteToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            If Me.ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Hex(Me.ColorDialog1.Color.A) + Hex(Me.ColorDialog1.Color.B) + Hex(Me.ColorDialog1.Color.G)
            End If
        End If
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        My.Forms.settings.TabControl1.SelectTab(0)
        My.Forms.settings.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Select Case Me.ToolStripMenuItem1.Checked
            Case False
                Me.MenuStrip1.Hide()
                My.Forms.settings.CheckBox1.Checked = False
            Case True
                Me.MenuStrip1.Show()
                My.Forms.settings.CheckBox1.Checked = True
        End Select
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Select Case Me.ToolStripMenuItem2.Checked
            Case False
                Me.ToolStrip1.Hide()
                My.Forms.settings.CheckBox2.Checked = False
            Case True
                Me.ToolStrip1.Show()
                My.Forms.settings.CheckBox2.Checked = True
        End Select
    End Sub



    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        If Me.TabControl1.TabCount > 0 Then
            If Me.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Me.ColorDialog1.Color
            End If
        End If

    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        If Me.TabControl1.TabCount > 0 Then
            If Me.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectionBackColor = Me.ColorDialog1.Color
            End If
        End If
    End Sub

    Private Sub ContentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContentsToolStripMenuItem.Click
        Try
            Process.Start(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\surfer.surf"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        My.Forms.About.ShowDialog()
    End Sub

    Private Sub OpenFileLocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFileLocationToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 0 Then
            If Me.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                Process.Start(Me.TabControl1.SelectedTab.ToolTipText.Remove(Me.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\")))
            Else
                MsgBox("First you should save document")
            End If
        End If
    End Sub
    Dim lr As String
    Private Sub ListBox1_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Me.TabControl1.TabCount = 0 Then
        'Else
        'lr = Me.ListBox1.SelectedItem
        'Me.ListBox1.DoDragDrop(Me.ListBox1.SelectedItem, DragDropEffects.Copy)
        'CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).DoDragDrop(Me.ListBox1.SelectedItem, DragDropEffects.All)
        'CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.ListBox1.SelectedItem
        'End If
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        If Cursor = Cursors.Hand Then
            Me.ToolStrip1.Items.Remove(e.ClickedItem)
            Cursor = Cursors.Arrow
        Else
            Try
                If e.ClickedItem.Tag <> "" Then
                    Process.Start(e.ClickedItem.Tag)
                End If
            Catch ex As Exception
                MsgBox("There is an error Please check tool command agian", MsgBoxStyle.OkOnly)
            End Try
        End If
    End Sub

    Private Sub PlugInManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlugInManagerToolStripMenuItem.Click
        My.Forms.settings.TabControl1.SelectTab(4)
        My.Forms.settings.ShowDialog()
    End Sub

    Private Sub PlugInsToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles PlugInsToolStripMenuItem.DropDownItemClicked
        If e.ClickedItem.Text = "Extensions" Then
        Else
            Try
                Process.Start(e.ClickedItem.Text)
            Catch ex As Exception
                MsgBox("Surfer can't find extension", MsgBoxStyle.OkOnly)
            End Try
        End If
    End Sub

    Private Sub CloseTabToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseTabToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 1 Then
            If Me.TabControl1.SelectedTab.ToolTipText.Length <= 5 Then
                If MsgBox("Are you sure ? Unsaved text will be clear", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Me.TabControl1.SelectedTab.Dispose()
                End If
            Else
                Me.TabControl1.SelectedTab.Dispose()
            End If
        End If
    End Sub

    Private Sub CloseCurrentTabToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseCurrentTabToolStripMenuItem.Click
        If Me.TabControl1.TabCount > 1 Then
            If Me.TabControl1.SelectedTab.ToolTipText.Length <= 5 Then
                If MsgBox("Are you sure ? Unsaved text will be clear", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Me.TabControl1.SelectedTab.Dispose()
                End If
            Else
                Me.TabControl1.SelectedTab.Dispose()
            End If
        End If
    End Sub

    Private Sub SpecialTagExplorerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpecialTagExplorerToolStripMenuItem.Click
        tags()
    End Sub

    Private Sub InsertTagToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles InsertTagToolStripMenuItem.DropDownItemClicked
        If Me.TabControl1.TabCount > 0 And e.ClickedItem.Text <> "" Then
            CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = e.ClickedItem.Text
        End If
    End Sub

    Private Sub tags()
        If Me.TabControl1.TabCount > 0 Then
            If Me.TabControl1.SelectedTab.ToolTipText.EndsWith(".htm") Then
                My.Forms.tags.ShowDialog()
            Else
                MsgBox("This is not an HTML document")
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        tags()
    End Sub

    Private Sub OpenMyDocumentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenMyDocumentsToolStripMenuItem.Click
        Process.Start(My.Computer.FileSystem.SpecialDirectories.MyDocuments)
    End Sub

    Private Sub OpenFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFolderToolStripMenuItem.Click
        If Me.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Process.Start(Me.FolderBrowserDialog1.SelectedPath)
        End If
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem1.Click
        Try
            Process.Start(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\surfer.surf"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AboutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem1.Click
        My.Forms.About.ShowDialog()
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            If Me.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SaveFile(Me.TabControl1.SelectedTab.ToolTipText, RichTextBoxStreamType.PlainText)
            Else
                save()
            End If
        Catch ex As Exception
            MsgBox("Nothing to Save Plese click New or Open on the File menu")
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        If Me.TabControl1.TabCount = 0 Then
        Else
            Dim str As String = CType(TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Text.Replace("<", "&lt;")
            Dim str1 As String = str.Replace(">", "&gt;")
            Dim str2 As String = "<!DOCTYPE HTML><!--Surfer Developer Auto Genarated Code--><html><head><title>Surfer</title></head><body><pre>" & str1 & "</pre></body></html>"
            My.Forms.surfbrowser.WebBrowser1.DocumentText = str2
            My.Forms.surfbrowser.Text = "Surfer Print"
            My.Forms.surfbrowser.ShowDialog()
            My.Forms.surfbrowser.WebBrowser1.ShowPrintDialog()
        End If
    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click
        cut()
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click
        copy()
    End Sub

    Private Sub PasteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click
        paste()
    End Sub

    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click
        Try
            Process.Start(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\surfer.surf"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub StartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem.Click
        My.Forms.surferwelcome.ShowDialog()
    End Sub

    Public Sub OpenFromPath(ByVal sFilePath As String)
        Try
            If sFilePath.EndsWith(".htm") Then
                Dim r As New RichTextBox
                Dim t As New TabPage
                Dim n As Integer
                t.Controls.Add(r)
                ar.Add(r)
                r.Dock = DockStyle.Fill
                n = TabControl1.TabPages.Count + 1
                t.Text = "[" & n & "]" & " HTML Document"
                t.ToolTipText = sFilePath
                Me.Text = Me.Text & " " & sFilePath
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\recent.surf", sFilePath & ",", True)
                TabControl1.Controls.Add(t)
                TabControl1.SelectedTab = t
                r.Font = New Font("Arial", 12, FontStyle.Bold)
                r.BorderStyle = BorderStyle.None
                r.AllowDrop = True
                r.EnableAutoDragDrop = True
                r.LoadFile(sFilePath, RichTextBoxStreamType.PlainText)
                syntaxcolor()
                r.ContextMenuStrip = Me.ContextMenuStrip1
            ElseIf sFilePath.EndsWith(".surf") Then
                MsgBox("Surfer Developer is using this file Do not edit it")
            End If
        Catch ex As Exception

        End Try

    End Sub

End Class
