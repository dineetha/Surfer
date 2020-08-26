Imports System.Collections.ObjectModel

Public Class surfbrowser

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.Close()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Me.WebBrowser1.Refresh()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            If Me.ToolStripButton2.Text = "Report" Then
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "", False)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<!DOCTYPE HTML><!--Surfer Developer Auto Genetated Report--><html><head><title>Surfer Report</title></head><body>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Absolute Path</font> " & Me.WebBrowser1.Document.Url.AbsolutePath & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Absolute Uri (Url)</font> " & Me.WebBrowser1.Document.Url.AbsoluteUri & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Title</font> " & Me.WebBrowser1.Document.Title & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Document Type</font> " & Me.WebBrowser1.DocumentType & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>All Elements Count</font> " & Me.WebBrowser1.Document.All.Count & " Elements</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Document Length</font> " & Me.WebBrowser1.DocumentText.Length & " Characters</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Links Count</font> " & Me.WebBrowser1.Document.Links.Count & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Images Count</font> " & Me.WebBrowser1.Document.Images.Count & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Link Color</font> " & Me.WebBrowser1.Document.LinkColor.ToString & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Visited Link Color</font> " & Me.WebBrowser1.Document.VisitedLinkColor.ToString & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Encoding</font> " & Me.WebBrowser1.Document.Encoding & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Cookies</font> " & Me.WebBrowser1.Document.Cookie & "</br>", True)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\report.htm", "<font color=red>Encryption Level</font> " & Me.WebBrowser1.EncryptionLevel.ToString & "</br></body></html>", True)
                Me.WebBrowser1.Navigate(Application.StartupPath & "\report.htm")
                Me.ToolStripButton2.Text = "Save Report"
            Else
                Me.WebBrowser1.ShowSaveAsDialog()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        Me.ToolStripLabel1.Text = "Title " & Me.WebBrowser1.DocumentTitle
    End Sub

    Private Sub surfbrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ToolStripButton2.Text = "Report"
    End Sub

    Private Sub ToolStripButton2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton2.TextChanged
        Me.ToolStripButton2.ToolTipText = Me.ToolStripButton2.Text
    End Sub
End Class