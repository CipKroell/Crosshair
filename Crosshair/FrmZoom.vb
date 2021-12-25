Imports System.Drawing.Drawing2D

Public Class FrmZoom
    Public OriSize As Integer = 2  ' size of the original rectangle
    Public MagnSize As Integer = 128 ' size of the magnification glass
    Dim bmpOriCopy As Bitmap        ' buffer of the original rectangle on screen
    Dim bmpgrOriCopy As Graphics
    Dim rctOri As Rectangle         ' original rectangle on screen
    Dim rctOriCopy As Rectangle     ' a copy of the original rectangle on screen
    Dim rctMagn As Rectangle        ' rectangle of the magnification glass
    Dim Desktop As Image
    Dim picgr As Graphics

    Dim gpath As GraphicsPath       ' used to make a round circle shaped glass
    Dim rgn As Region
    Dim pn As Pen = New Pen(Color.Silver, 4)

    Dim SC As ScreenShot.ScreenCapture

    Private InitialStyle As Integer

    Private Sub FrmZoom_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialStyle = GetWindowLong(Me.Handle, GWL.ExStyle)
        SetWindowLong(Me.Handle, GWL.ExStyle, CType(InitialStyle Or WS_EX.Layered Or WS_EX.Transparent, WS_EX))

        Timer1.Enabled = True
    End Sub

    Private Sub FrmZoom_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        MagnSize = 128
        OriSize = 128 \ 2

        bmpOriCopy = New Bitmap(OriSize, OriSize)
        bmpgrOriCopy = Graphics.FromImage(bmpOriCopy)
        rctOriCopy = New Rectangle(0, 0, OriSize, OriSize)
        rctOri = New Rectangle(0, 0, OriSize, OriSize) 'where on screen
        rctMagn = New Rectangle(0, 0, MagnSize, MagnSize)

        SC = New ScreenShot.ScreenCapture

        pic.SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        pic.Image = SC.CaptureScreen
        Desktop = CType(pic.Image.Clone, Image)
        picgr = pic.CreateGraphics
    End Sub

    Private Sub FrmZoom_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        rgn.Dispose()
        gpath.Dispose()
        bmpgrOriCopy.Dispose()
        bmpOriCopy.Dispose()
        Desktop.Dispose()
        picgr.Dispose()
    End Sub

    Private Sub Pic_MouseMove(sender As Object, e As MouseEventArgs) Handles pic.MouseMove

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        bmpOriCopy = New Bitmap(OriSize, OriSize)
        bmpgrOriCopy = Graphics.FromImage(bmpOriCopy)
        rctOriCopy = New Rectangle(0, 0, OriSize, OriSize)
        rctOri = New Rectangle(0, 0, OriSize, OriSize) 'where on screen
        rctMagn = New Rectangle(0, 0, MagnSize, MagnSize)

        SC = New ScreenShot.ScreenCapture

        pic.SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        pic.Image = SC.CaptureScreen
        Desktop = CType(pic.Image.Clone, Image)
        picgr = pic.CreateGraphics

        rctOri.X = CInt((Screen.PrimaryScreen.Bounds.Width / 2) - OriSize \ 2)
        rctOri.Y = CInt((Screen.PrimaryScreen.Bounds.Height / 2) - OriSize \ 2)
        bmpgrOriCopy.DrawImage(pic.Image, rctOriCopy, rctOri, GraphicsUnit.Pixel)

        'restore background first before putting new magn glass
        picgr.DrawImage(Desktop, rctMagn, rctMagn, GraphicsUnit.Pixel)

        ' putt new magn glass
        rctMagn.X = CInt((Screen.PrimaryScreen.Bounds.Width / 2) - MagnSize \ 2)
        rctMagn.Y = CInt((Screen.PrimaryScreen.Bounds.Height / 2) - MagnSize \ 2)
        gpath = New GraphicsPath
        gpath.AddEllipse(rctMagn)
        rgn = New Region(gpath)
        picgr.Clip = rgn

        picgr.DrawImage(bmpOriCopy, rctMagn, rctOriCopy, GraphicsUnit.Pixel)
        picgr.DrawEllipse(pn, rctMagn)
    End Sub
End Class