Module Module1
    <System.Runtime.InteropServices.DllImport("shell32.dll")> _
    Public Sub SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer, _
        ByVal dwItem1 As Integer, ByVal dwItem2 As Integer)
    End Sub
End Module
