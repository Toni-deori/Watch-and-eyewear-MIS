Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim connectionString As String = "Server=localhost;Database=eyeandwatchmis;userid=root;password=''"
    Dim conn As New MySqlConnection(connectionString)
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Using borderPen As New Pen(Color.FromArgb(255, 191, 0), 3)
            e.Graphics.DrawRectangle(borderPen, 0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)
        End Using
    End Sub
    Private isDragging As Boolean = False
    Private dragStartPoint As Point
    Dim currentClerkID As Integer
    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Left Then
            isDragging = True
            dragStartPoint = e.Location
        End If
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If isDragging Then
            Me.Location = Me.PointToScreen(New Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y))
        End If
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        isDragging = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        Dim query As String = "SELECT UserID, role FROM users WHERE username=@username AND password=@password"
        Dim cmd As New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@username", TextBox1.Text)
        cmd.Parameters.AddWithValue("@password", TextBox2.Text)

        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        If reader.Read() Then
            currentClerkID = Convert.ToInt32(reader("UserID"))
            Dim role As String = reader("role").ToString()

            If role = "Admin" Then
                admin.Show()
                Me.Hide()
            ElseIf role = "Clerk" Then
                Dim clerkForm As New clerk(currentClerkID)
                clerkForm.Show()
                Me.Hide()
            End If
            TextBox1.Clear()
            TextBox2.Clear()
        Else
            MessageBox.Show("Invalid username or password.")
        End If

        conn.Close()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.BackColor = Color.FromArgb(120, 0, 0, 0)
        Label1.BackColor = Color.Transparent
        Label2.BackColor = Color.Transparent
        Label3.BackColor = Color.Transparent
        Button2.BackColor = Color.Transparent

    End Sub
    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.BackColor = Color.Gray
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.BackColor = Color.White
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
