Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic.FileIO
Public Class settings

    Private Sub settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim filename As String = Application.StartupPath & "\browsers.surf"
        Dim fields As String()
        Dim a1 As Integer
        Dim delimiter As String = ","
        Using parser As New TextFieldParser(filename)
            parser.SetDelimiters(delimiter)
            Me.ListBox1.Items.Clear()
            While Not parser.EndOfData
                ' Read in the fields for the current line
                fields = parser.ReadFields()
                ' Add code here to use data in fields variable.
                For a1 = 0 To fields.Length - 1
                    Me.ListBox1.Items.Add(fields(a1))
                Next
            End While
        End Using
        Dim a As Integer
        Dim files As ReadOnlyCollection(Of String)
        files = My.Computer.FileSystem.GetFiles(Application.StartupPath & "\surferextensions", SearchOption.SearchAllSubDirectories, "*.exe")
        Me.ListBox2.Items.Clear()
        For a = 0 To files.Count - 1
            Me.ListBox2.Items.Add(files(a))
        Next

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.OpenFileDialog1.Filter = "Applications|*.exe"
        Me.OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.ProgramFiles
        If Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.ListBox1.Items.Add(Me.OpenFileDialog1.FileName)
            My.Forms.Form1.BrowserToolStripMenuItem.DropDownItems.Add(Me.OpenFileDialog1.FileName.Remove(0, Me.OpenFileDialog1.FileName.LastIndexOf("\") + 1))
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\browsers.surf", Me.OpenFileDialog1.FileName & ",", True)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.ListBox1.Items.Clear()
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.OpenFileDialog1.Filter = "Applications|*.exe"
        Me.OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.ProgramFiles
        If Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\browsers.surf", Me.OpenFileDialog1.FileName, True)
        End If
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\browsers.surf", "", False)
        Me.ListBox1.Items.Clear()
    End Sub

    Private Sub Button3_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.OpenFileDialog1.Filter = "Applications|*.exe"
        Me.OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.ProgramFiles
        If Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.Close()
            Process.Start(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Select Case Me.CheckBox1.Checked
            Case False
                My.Forms.Form1.MenuStrip1.Hide()
                My.Forms.Form1.ToolStripMenuItem1.Checked = False
            Case True
                My.Forms.Form1.MenuStrip1.Show()
                My.Forms.Form1.ToolStripMenuItem1.Checked = True
        End Select
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Select Case Me.CheckBox2.Checked
            Case False
                My.Forms.Form1.ToolStrip1.Hide()
                My.Forms.Form1.ToolStripMenuItem2.Checked = False
            Case True
                My.Forms.Form1.ToolStrip1.Show()
                My.Forms.Form1.ToolStripMenuItem2.Checked = True
        End Select
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
        My.Forms.Form1.Cursor = Cursors.Hand
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim tool As New ToolStripButton
        tool.Tag = Me.TextBox2.Text
        tool.Image = Image.FromFile(Application.StartupPath & "\new.jpg")
        tool.ToolTipText = Me.TextBox3.Text
        tool.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        My.Forms.Form1.ToolStrip1.Items.Add(tool)
        Me.Close()
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            Select Case Me.CheckBox5.Checked
                Case True
                    CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).WordWrap = True
                Case False
                    CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).WordWrap = False
            End Select
        End If
    End Sub

    Private Sub FontDialog1_Apply(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontDialog1.Apply
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).Font = Me.FontDialog1.Font
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.FontDialog1.ShowDialog()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Me.ListBox2.SelectedItem <> "" Then
            My.Computer.FileSystem.DeleteDirectory(Me.ListBox2.SelectedItem.ToString.Remove(Me.ListBox2.SelectedItem.ToString.LastIndexOf("\")), DeleteDirectoryOption.DeleteAllContents)
            Me.ListBox2.ClearSelected()
            Me.Close()
        End If
    End Sub

End Class