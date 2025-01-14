Imports MySql.Data.MySqlClient

Public Class inventory
    Dim connectionString As String = "Server=localhost;Database=eyeandwatchmis;userid=root;password=''"
    Dim conn As New MySqlConnection(connectionString)
    Private Sub LoadStockLevels()
        GenerateLowStockAlerts()
        Dim query As String = "SELECT ProductID, Name, Category, SubCategory, StockQuantity,ImagePath FROM Products ORDER BY StockQuantity"

        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim table As New DataTable()
            adapter.Fill(table)

            DataGridView1.DataSource = table

            DataGridView1.Columns("ProductID").Visible = False
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill


            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim stockQuantity As Integer = Convert.ToInt32(row.Cells("StockQuantity").Value)

                If stockQuantity <= 5 Then
                    row.DefaultCellStyle.BackColor = Color.Red
                ElseIf stockQuantity <= 15 Then
                    row.DefaultCellStyle.BackColor = Color.Orange
                Else
                    row.DefaultCellStyle.BackColor = Color.Green
                End If
            Next

            conn.Close()
            If Not DataGridView1.Columns.Contains("ProductImage") Then
                Dim imgColumn As New DataGridViewImageColumn()
                imgColumn.Name = "ProductImage"
                imgColumn.HeaderText = "Image"
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom
                DataGridView1.Columns.Add(imgColumn)
            End If

            DataGridView1.Columns("ProductImage").Width = 100
            DataGridView1.Columns("ProductImage").DefaultCellStyle.NullValue = Nothing

            DataGridView1.Columns("ProductImage").DisplayIndex = 1

            If DataGridView1.Columns.Contains("ImagePath") Then
                DataGridView1.Columns("ImagePath").Visible = False
            End If

            Dim placeholderImg As Image = My.Resources.PlaceholderImage
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim imagePath As String = row.Cells("ImagePath").Value.ToString()
                If Not String.IsNullOrEmpty(imagePath) AndAlso System.IO.File.Exists(imagePath) Then
                    Try

                        Dim img As Image = Image.FromFile(imagePath)
                        Dim squareImg As New Bitmap(100, 100)
                        Using g As Graphics = Graphics.FromImage(squareImg)
                            g.DrawImage(img, 0, 0, 100, 100)
                        End Using

                        row.Cells("ProductImage").Value = squareImg
                    Catch ex As Exception

                        row.Cells("ProductImage").Value = placeholderImg
                    End Try
                Else
                    row.Cells("ProductImage").Value = placeholderImg
                End If
            Next
            DataGridView1.Columns("ImagePath").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStockLevels()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim productID As Integer = Convert.ToInt32(selectedRow.Cells("ProductID").Value)

            Dim restockAmountString As String = InputBox("Enter the amount to restock:", "Restock Product")

            If String.IsNullOrWhiteSpace(restockAmountString) Then
                MessageBox.Show("Restock operation cancelled.")
                Exit Sub
            End If

            Dim restockAmount As Integer
            If Integer.TryParse(restockAmountString, restockAmount) AndAlso restockAmount > 0 Then
                UpdateStockQuantity(productID, restockAmount)
                MessageBox.Show("Stock restocked successfully.")
                LoadStockLevels()
            Else
                MessageBox.Show("Invalid restock amount. Please enter a valid number greater than 0.")
            End If
        Else
            MessageBox.Show("Please select a product to restock.")
        End If
    End Sub


    Private Sub UpdateStockQuantity(productID As Integer, restockAmount As Integer)
        Dim query As String = "UPDATE Products SET StockQuantity = StockQuantity + @RestockAmount WHERE ProductID = @ProductID"

        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@RestockAmount", restockAmount)
                cmd.Parameters.AddWithValue("@ProductID", productID)
                cmd.ExecuteNonQuery()
            End Using
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        Dim query1 As String = "UPDATE lowstockalerts SET Status='Resolved' where ProductID = @ProductID"

        Try
            conn.Open()
            Using cmd As New MySqlCommand(query1, conn)
                cmd.Parameters.AddWithValue("@ProductID", productID)
                cmd.ExecuteNonQuery()
            End Using
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub GenerateLowStockAlerts()
        Dim query As String = "SELECT p.Name " &
                          "FROM Products p " &
                          "JOIN LowStockAlerts l ON p.ProductID = l.ProductID " &
                          "WHERE l.Status = 'Pending';"


        Using cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        Dim productList As String = "The following products are low in stock:" & vbCrLf

                        While reader.Read()
                            productList &= "- " & reader("Name").ToString() & vbCrLf
                        End While

                        MessageBox.Show(productList, "Low Stock Alerts", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No products are currently low in stock.", "Low Stock Alerts", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
                conn.Close()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
        admin.Show()
    End Sub
End Class