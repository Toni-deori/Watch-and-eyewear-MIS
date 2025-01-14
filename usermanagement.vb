Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient
Public Class usermanagement
    Dim connectionString As String = "Server=localhost;Database=eyeandwatchmis;userid=root;password=''"
    Dim conn As New MySqlConnection(connectionString)
    Private Sub usermanagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        Panel1.BackColor = Color.FromArgb(200, 0, 0, 0)
    End Sub
    Private Sub LoadData()
        Dim query As String = "SELECT * FROM users;"
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
            DataGridView1.ReadOnly = True
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        DataGridView1.Columns("Address").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataGridView1.Columns("Address").Width = 170
        DataGridView1.Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
    End Sub
    Private Sub showmatch()
        Dim searchText As String = TextBox7.Text.Trim()
        Dim query As String = "SELECT * FROM Users WHERE " &
                          "UserID LIKE @searchText OR " &
                          "name LIKE @searchText OR " &
                          "Username LIKE @searchText OR " &
                          "gender LIKE @searchText OR " &
                          "Role LIKE @searchText ;"

        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@searchText", "%" & searchText & "%")

                Dim adapter As New MySqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table
            End Using
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        showmatch()
        TextBox7.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim insertquery As String = "INSERT INTO Users (Name, Gender, Username, Password, Address, Contact_no, Role)
VALUES (@name,@gender,@usename,@password,@address,@contact,@role);"
        Try
            conn.Open()
            Using cmd As New MySqlCommand(insertquery, conn)
                cmd.Parameters.AddWithValue("@name", TextBox4.Text)
                cmd.Parameters.AddWithValue("@gender", TextBox5.Text)
                cmd.Parameters.AddWithValue("@usename", TextBox2.Text)
                cmd.Parameters.AddWithValue("@password", TextBox3.Text)
                cmd.Parameters.AddWithValue("@address", RichTextBox1.Text)
                cmd.Parameters.AddWithValue("@contact", TextBox6.Text)
                cmd.Parameters.AddWithValue("@role", ComboBox1.SelectedItem.ToString())
                cmd.ExecuteNonQuery()
            End Using
            conn.Close()
            LoadData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        clearall()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim UserID As Integer = Convert.ToInt32(selectedRow.Cells("UserID").Value)
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this user?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                DeleteProductFromDatabase(UserID)
                DataGridView1.Rows.Remove(selectedRow)

                MessageBox.Show("User deleted successfully.")
            End If
        Else
            MessageBox.Show("Please select a user to delete.")
        End If
        LoadData()
        clearall()
    End Sub
    Private Sub DeleteProductFromDatabase(UserID As Integer)
        Dim query As String = "DELETE FROM Users WHERE UserID=@ID;"

        Try
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ID", UserID)
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim userID As Integer = Convert.ToInt32(selectedRow.Cells("UserID").Value)
            Dim updateQuery As String = "UPDATE Users SET "
            Dim parameters As New List(Of MySqlParameter)()
            If Not String.IsNullOrEmpty(TextBox4.Text) Then
                updateQuery &= "Name = @name, "
                parameters.Add(New MySqlParameter("@name", TextBox4.Text))
            End If

            If Not String.IsNullOrEmpty(TextBox5.Text) Then
                updateQuery &= "Gender = @gender, "
                parameters.Add(New MySqlParameter("@gender", TextBox5.Text))
            End If

            If Not String.IsNullOrEmpty(TextBox2.Text) Then
                updateQuery &= "Username = @username, "
                parameters.Add(New MySqlParameter("@username", TextBox2.Text))
            End If

            If Not String.IsNullOrEmpty(TextBox3.Text) Then
                updateQuery &= "Password = @password, "
                parameters.Add(New MySqlParameter("@password", TextBox3.Text))
            End If

            If Not String.IsNullOrEmpty(RichTextBox1.Text) Then
                updateQuery &= "Address = @address, "
                parameters.Add(New MySqlParameter("@address", RichTextBox1.Text))
            End If

            If Not String.IsNullOrEmpty(TextBox6.Text) Then
                updateQuery &= "Contact_no = @contact, "
                parameters.Add(New MySqlParameter("@contact", TextBox6.Text))
            End If

            If ComboBox1.SelectedItem IsNot Nothing Then
                updateQuery &= "Role = @role, "
                parameters.Add(New MySqlParameter("@role", ComboBox1.SelectedItem.ToString()))
            End If

            If updateQuery.EndsWith(", ") Then
                updateQuery = updateQuery.Substring(0, updateQuery.Length - 2)
            End If
            updateQuery &= " WHERE UserID = @uid;"
            parameters.Add(New MySqlParameter("@uid", userID))
            Try
                conn.Open()
                Using cmd As New MySqlCommand(updateQuery, conn)
                    cmd.Parameters.AddRange(parameters.ToArray())
                    cmd.ExecuteNonQuery()
                End Using
                conn.Close()
                LoadData()
                MessageBox.Show("User updated successfully.")
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
            clearall()
        Else
            MessageBox.Show("Please select a row to update.")
        End If
    End Sub
    Private Sub clearall()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        ComboBox1.SelectedIndex = -1
        RichTextBox1.Clear()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
        admin.Show()
    End Sub

    Private Sub Button7_MouseEnter(sender As Object, e As EventArgs) Handles Button7.MouseEnter
        Button7.BackColor = Color.FromArgb(0, 0, 0, 0)
    End Sub
End Class