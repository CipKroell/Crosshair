Imports System.Runtime.InteropServices

Module WindowControl
    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As Integer) As Boolean : End Function

    <DllImport("user32.dll", EntryPoint:="GetWindowLong")>
    Public Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As GWL) As Integer : End Function

    <DllImport("user32.dll", EntryPoint:="SetWindowLong")>
    Public Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As GWL, ByVal dwNewLong As WS_EX) As Integer : End Function

    Public Enum WS_EX As Integer
        Transparent = &H20
        Layered = &H80000
    End Enum

    Public Enum GWL As Integer
        ExStyle = -20
    End Enum

    Public Const SWP_NOSIZE As Integer = &H1
    Public Const SWP_NOMOVE As Integer = &H2

    Public ReadOnly HWND_TOPMOST As New IntPtr(-1)
    Public ReadOnly HWND_NOTOPMOST As New IntPtr(-2)

    Public Color1 As Color = Color.PowderBlue
    Public Color2 As Color = Color.YellowGreen

    Public Function MakeTopMost(Frm As Form) As Object
        SetWindowPos(Frm.Handle(), HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
        Return Nothing
    End Function
End Module
