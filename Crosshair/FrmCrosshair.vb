Imports System.Drawing.Drawing2D

Public Class FrmCrosshair

    Private ShowCH As Boolean = False
    Private AnimateCH As Boolean = True
    Private ShowSpray As Boolean = False

    Private InitialStyle As Integer

    Dim R As Integer = 20

    Private WithEvents KbHook As New KeyboardHook

    Private Sub CenterMe()
        Left = CInt((Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2))
        Top = CInt((Screen.PrimaryScreen.Bounds.Height / 2) - (Height / 2))
    End Sub

    Private Sub SetSize(Sz As Integer)
        Width = Sz
        Height = Sz

        CenterMe()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MakeTopMost(Me)

        SetSize(75)

        InitialStyle = GetWindowLong(Me.Handle, GWL.ExStyle)
        SetWindowLong(Me.Handle, GWL.ExStyle, CType(InitialStyle Or WS_EX.Layered Or WS_EX.Transparent, WS_EX))

        FrmSpray.Show()
        FrmSpray.Visible = False
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        If ShowCH Then
            With e.Graphics
                .CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                .SmoothingMode = Drawing2D.SmoothingMode.HighQuality

                If AnimateCH Then
                    .DrawArc(New Pen(Color2, 2), New Rectangle(2, 2, Width - 4, Height - 4), R, 50)
                    .DrawArc(New Pen(Color2, 2), New Rectangle(2, 2, Width - 4, Height - 4), R + 180, 50)
                End If

                .DrawArc(New Pen(Color1, 1), New Rectangle(10, 10, Width - 20, Height - 20), 0, 360)

                .DrawArc(New Pen(Color2, 1), New Rectangle(CInt(Width / 2 - 1), CInt(Height / 2 - 1), 4, 4), 0, 360)
                .DrawLine(New Pen(Color1, 2), New Point(CInt(Width / 2 - 10), CInt(Height / 2)), New Point(CInt(Width / 2 - 20), CInt(Height / 2)))
                .DrawLine(New Pen(Color1, 2), New Point(CInt(Width / 2 + 10), CInt(Height / 2)), New Point(CInt((Width / 2) + 20), CInt(Height / 2)))

                .DrawLine(New Pen(Color1, 2), New Point(CInt(Width / 2), CInt(Height / 2 - 10)), New Point(CInt(Width / 2), CInt((Height / 2) - 20)))
                .DrawLine(New Pen(Color1, 2), New Point(CInt(Width / 2), CInt(Height / 2 + 10)), New Point(CInt(Width / 2), CInt(Height / 2 + 20)))
            End With
        End If
    End Sub

    Private Sub KbHook_KeyDown(ByVal Key As Keys) Handles KbHook.KeyDown
        Select Case Key
            Case Keys.F12
                ShowCH = Not ShowCH
                Invalidate()
            Case Keys.F11
                AnimateCH = Not AnimateCH
                Invalidate()
            Case Keys.F10
                ShowSpray = Not ShowSpray

                FrmSpray.Visible = ShowSpray
            Case Keys.F9
                If Width = 75 Then
                    SetSize(127)
                Else
                    SetSize(75)
                End If
        End Select
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        R += 5
        If R > 360 Then R = 0
        Invalidate()
    End Sub

    Private Sub SairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SairToolStripMenuItem.Click
        Application.Exit()
    End Sub
End Class
