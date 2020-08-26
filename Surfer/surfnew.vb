Imports System.Windows.Forms
Imports System.Collections.ObjectModel

Public Class surfnew
    Public ok As Integer = 0

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        '   Me.DialogResult = System.Windows.Forms.DialogResult.OK
        If Me.ListView1.SelectedItems.Count = 0 Then
            MsgBox("Please select a Structure and click OK", MsgBoxStyle.OkOnly)
        Else
            ok = 1
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub surfnew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim files As ReadOnlyCollection(Of String)
        Dim a As Integer
        files = My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Str", FileIO.SearchOption.SearchTopLevelOnly, "*.htm;*.css;*.js".Split(";"c))
        Me.ListView1.Clear()
        For a = 0 To files.Count - 1
            Me.ImageList1.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(files(a)))
            Me.ListView1.Items.Add(files(a).Remove(0, files(a).LastIndexOf("\") + 1), a)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Process.Start(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\Str\url.surf"))
    End Sub

    Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
        If Me.ListView1.SelectedItems.Count = 0 Then
            MsgBox("Please select a Structure and click OK", MsgBoxStyle.OkOnly)
        Else
            ok = 1
            Me.Close()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class
