Public Class FrmSpray

    Private InitialStyle As Integer

    Private Sub FrmSpray_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MakeTopMost(Me)

        InitialStyle = GetWindowLong(Me.Handle, GWL.ExStyle)
        SetWindowLong(Me.Handle, GWL.ExStyle, CType(InitialStyle Or WS_EX.Layered Or WS_EX.Transparent, WS_EX))

        Left = CInt(Screen.PrimaryScreen.Bounds.Width / 2 - Width / 2)
        Top = FrmCrosshair.Top - Height
    End Sub

    Private Sub FrmSpray_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        With e.Graphics
            .CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            .SmoothingMode = Drawing2D.SmoothingMode.HighQuality

            Dim X As Integer = 2

            For Y As Integer = Height To 0 Step -20
                .DrawLine(New Pen(Color2, 1), New Point(CInt(Width / 2 - X), Y), New Point(CInt(Width / 2 + X), Y))
                X += 2
            Next
        End With
    End Sub
End Class