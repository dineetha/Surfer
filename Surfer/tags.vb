Public Class tags

    Private Sub tags_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ComboBox1.SelectedIndex = 0
        Me.Label1.Text = "<a " & Me.ComboBox2.Text & " " & "title=" & Me.TextBox2.Text & ">" & Me.TextBox1.Text & "</a>"
        Me.WebBrowser1.DocumentText = Me.Label1.Text
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            If My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                Me.WebBrowser4.Navigate(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\")))
                Me.WebBrowser5.Navigate(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\")))
            Else
                Me.WebBrowser4.DocumentText = "Document is not saved"
                Me.WebBrowser5.DocumentText = "Document is not saved"
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case Me.ComboBox1.SelectedIndex
            Case 0
                Me.TabControl1.SelectTab(0)
                Me.Label1.Text = "<a " & Me.ComboBox2.Text & " " & "title=" & Me.TextBox2.Text & ">" & Me.TextBox1.Text & "</a>"
                Me.WebBrowser1.DocumentText = Me.Label1.Text
            Case 1
                Me.TabControl1.SelectTab(1)
            Case 2
                Me.TabControl1.SelectTab(2)
                Me.WebBrowser2.DocumentText = Me.TextBox4.Text
            Case 3
                Me.TabControl1.SelectTab(3)
            Case 4
                Me.TabControl1.SelectTab(4)
            Case 5
                Me.TabControl1.SelectTab(5)
                Me.WebBrowser6.DocumentText = Me.TextBox9.Text
            Case 6
                Me.TabControl1.SelectTab(6)
            Case 7
                Me.TabControl1.SelectTab(7)
                Me.WebBrowser7.DocumentText = Me.TextBox10.Text
        End Select
    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case Me.TabControl1.SelectedIndex
            Case 0
                Me.ComboBox1.SelectedIndex = 0
                Me.Label1.Text = "<a " & Me.ComboBox2.Text & " " & "title=" & Me.TextBox2.Text & ">" & Me.TextBox1.Text & "</a>"
                Me.WebBrowser1.DocumentText = Me.Label1.Text
            Case 1
                Me.ComboBox1.SelectedIndex = 1
            Case 2
                Me.ComboBox1.SelectedIndex = 2
                Me.WebBrowser2.DocumentText = Me.TextBox4.Text
            Case 3
                Me.ComboBox1.SelectedIndex = 3
            Case 4
                Me.ComboBox1.SelectedIndex = 4
            Case 5
                Me.ComboBox1.SelectedIndex = 5
                Me.WebBrowser6.DocumentText = Me.TextBox9.Text
            Case 6
                Me.ComboBox1.SelectedIndex = 6
            Case 7
                Me.ComboBox1.SelectedIndex = 7
                Me.WebBrowser7.DocumentText = Me.TextBox10.Text
        End Select
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Me.Label1.Text = "<a " & Me.ComboBox2.Text & " " & "title=" & Me.TextBox2.Text & ">" & Me.TextBox1.Text & "</a>"
        Me.WebBrowser1.DocumentText = Me.Label1.Text
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Me.Label1.Text = "<a " & Me.ComboBox2.Text & " " & "title=" & Me.TextBox2.Text & ">" & Me.TextBox1.Text & "</a>"
        Me.WebBrowser1.DocumentText = Me.Label1.Text
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Me.Label1.Text = "<a " & Me.ComboBox2.Text & " " & "title=" & Me.TextBox2.Text & ">" & Me.TextBox1.Text & "</a>"
        Me.WebBrowser1.DocumentText = Me.Label1.Text
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.OpenFileDialog1.Filter = "HTML Web Pages(*.htm,*.html)|*.htm;*.html"
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            If My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                Me.OpenFileDialog1.InitialDirectory = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\"))
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Me.OpenFileDialog1.FileName.Remove(Me.OpenFileDialog1.FileName.LastIndexOf("\")) = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\")) Then
                        Me.ComboBox2.Text = "file:///" & Me.OpenFileDialog1.FileName
                    End If
                End If
            Else
                MsgBox("Document is not saved, So initial directory is my document folder")
                Me.OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.ComboBox2.Text = "file:///" & Me.OpenFileDialog1.FileName
                End If
            End If
        End If
    End Sub

    Private Sub ComboBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.TextChanged
        Me.Label1.Text = "<a " & Me.ComboBox2.Text & " " & "title=" & Me.TextBox2.Text & ">" & Me.TextBox1.Text & "</a>"
        Me.WebBrowser1.DocumentText = Me.Label1.Text
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.Label1.Text
        End If
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.OpenFileDialog1.Filter = "Java Applets(*.jar)|*.jar"
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            If My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                Me.OpenFileDialog1.InitialDirectory = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\"))
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Me.OpenFileDialog1.FileName.Remove(Me.OpenFileDialog1.FileName.LastIndexOf("\")) = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\")) Then
                        Me.TextBox3.Select(Me.TextBox3.Text.LastIndexOf("archive=") + 9, Me.TextBox3.Text.LastIndexOf("jar") - 6 - Me.TextBox3.Text.LastIndexOf("archive="))
                        Me.TextBox3.SelectedText = "file:///" & Me.OpenFileDialog1.FileName
                    End If
                End If
            Else
                MsgBox("Document is not saved, So initial directory is my document folder")
                Me.OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.TextBox3.Select(Me.TextBox3.Text.LastIndexOf("archive=") + 9, Me.TextBox3.Text.LastIndexOf("jar") - 6 - Me.TextBox3.Text.LastIndexOf("archive="))
                    Me.TextBox3.SelectedText = "file:///" & Me.OpenFileDialog1.FileName
                End If
            End If
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Me.Label5.Text = Me.TextBox3.Text
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.Label5.Text
            Me.Close()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.TextBox4.Text
            Me.Close()
        End If
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Me.WebBrowser2.DocumentText = Me.TextBox4.Text
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Me.TextBox6.Text = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "img.txt")
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Me.TextBox6.Text = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "flash.txt")
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Me.TextBox6.Text = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "video.txt")
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        Me.WebBrowser3.DocumentText = Me.TextBox6.Text
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.OpenFileDialog1.Filter = "Web Images(*.jpg,*.png,*.gif)|*.jpg;*.jpeg;*.png;*.gif|Videos|*.mp3;*.wmv;*.mp4|Flash Objects|*.swf"
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            If My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.OpenFileDialog1.InitialDirectory = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText
                    Me.TextBox6.Select(Me.TextBox6.Text.IndexOf("src=") + 5, 0)
                    Me.TextBox6.SelectedText = "file:///" & Me.OpenFileDialog1.FileName
                    Me.TextBox5.Text = "file:///" & Me.OpenFileDialog1.FileName
                End If
            Else
                MsgBox("Document is not saved, So initial directory is my document folder")
                Me.OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.TextBox6.Select(Me.TextBox6.Text.IndexOf("src=") + 5, 0)
                    Me.TextBox6.SelectedText = "file:///" & Me.OpenFileDialog1.FileName
                    Me.TextBox5.Text = "file:///" & Me.OpenFileDialog1.FileName
                End If
            End If
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.TextBox6.Text
            Me.Close()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.OpenFileDialog1.Filter = "CSS(*.css)|*.css"
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            If My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                Me.OpenFileDialog1.InitialDirectory = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\"))
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Me.OpenFileDialog1.FileName.Remove(Me.OpenFileDialog1.FileName.LastIndexOf("\")) = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\")) Then
                        Me.TextBox7.Select(Me.TextBox7.Text.LastIndexOf("href=") + 6, 0)
                        Me.TextBox7.SelectedText = "file:///" & Me.OpenFileDialog1.FileName
                    End If
                End If
            Else
                MsgBox("Document is not saved, So initial directory is my document folder")
                Me.OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.TextBox7.Select(Me.TextBox7.Text.LastIndexOf("href=") + 6, 0)
                    Me.TextBox7.SelectedText = "file:///" & Me.OpenFileDialog1.FileName
                End If
            End If
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.TextBox7.Text
            Me.Close()
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Me.OpenFileDialog1.Filter = "JavaScript(*.js)|*.js"
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            If My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Length > 5 Then
                Me.OpenFileDialog1.InitialDirectory = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\"))
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Me.OpenFileDialog1.FileName.Remove(Me.OpenFileDialog1.FileName.LastIndexOf("\")) = My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.Remove(My.Forms.Form1.TabControl1.SelectedTab.ToolTipText.LastIndexOf("\")) Then
                        Me.TextBox8.Select(Me.TextBox8.Text.LastIndexOf("src=") + 5, 0)
                        Me.TextBox8.SelectedText = "file:///" & Me.OpenFileDialog1.FileName
                    End If
                End If
            Else
                MsgBox("Document is not saved, So initial directory is my document folder")
                Me.OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.TextBox8.Select(Me.TextBox8.Text.LastIndexOf("src=") + 5, 0)
                    Me.TextBox8.SelectedText = "file:///" & Me.OpenFileDialog1.FileName
                End If
            End If
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.TextBox8.Text
            Me.Close()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked = True Then
            Me.TextBox9.Select(0, 4)
            Me.TextBox9.SelectedText = "<ol>"
            Me.TextBox9.Select(Me.TextBox9.Text.LastIndexOf("<"), 5)
            Me.TextBox9.SelectedText = "</ol>"
        Else
            Me.TextBox9.Select(0, 4)
            Me.TextBox9.SelectedText = "<ul>"
            Me.TextBox9.Select(Me.TextBox9.Text.LastIndexOf("<"), 5)
            Me.TextBox9.SelectedText = "</ul>"
        End If
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        Me.WebBrowser6.DocumentText = Me.TextBox9.Text
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.TextBox9.Text
            Me.Close()
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        Me.WebBrowser7.DocumentText = Me.TextBox10.Text
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If My.Forms.Form1.TabControl1.TabCount > 0 Then
            CType(My.Forms.Form1.TabControl1.SelectedTab.Controls.Item(0), RichTextBox).SelectedText = Me.TextBox10.Text
            Me.Close()
        End If
    End Sub
End Class