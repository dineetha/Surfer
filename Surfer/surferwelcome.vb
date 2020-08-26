Imports System.Collections.ObjectModel
Public Class surferwelcome

    Private Sub surferwelcome_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Computer.FileSystem.CopyFile(Application.StartupPath & "\start1.htm", Application.StartupPath & "\start.htm", True)
        Try
            Dim files As ReadOnlyCollection(Of String)
            Dim a As Integer
            files = My.Computer.FileSystem.GetDirectories(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Global Square", FileIO.SearchOption.SearchTopLevelOnly)
            For a = 0 To files.Count - 1
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\start.htm", files(a) & "</br>", True)
            Next
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\start.htm", "</p></body></html>", True)
        Catch ex As Exception

        End Try
        Me.WebBrowser1.Navigate(Application.StartupPath & "\start.htm")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class