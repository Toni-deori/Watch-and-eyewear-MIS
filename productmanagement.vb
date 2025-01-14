Imports MySql.Data.MySqlClient
Imports System.IO
Imports Mysqlx
Imports System.Data.SqlClient

Public Class productmanagement
    Dim connectionString As String = "Server=localhost;Database=eyeandwatchmis;userid=root;password=''"
    Dim conn As New MySqlConnection(connectionString)
    Dim categorySubCategoryMap As New Dictionary(Of String, List(Of String)) From {
    {"Watches", New List(Of String) From {"Wristwatches", "Smartwatches", "Pocket Watches"}},
    {"Eyewear", New List(Of String) From {"Sunglasses", "Reading Glasses", "Contact Lenses"}}
}
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Close()
    End Sub
    Private Sub LoadProducts()
        Dim query As String = "SELECT ProductID,Name, Category, SubCategory, Price, Discount, Description, ImagePath FROM products;"
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
            DataGridView1.ReadOnly = True
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
            If DataGridView1.Columns.Contains("ProductID") Then
                DataGridView1.Columns("ProductID").Visible = False
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

            DataGridView1.Columns("Description").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            DataGridView1.Columns("Description").Width = 220
            DataGridView1.Columns("Description").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox2.Items.Clear()

        Dim selectedCategory As String = ComboBox1.SelectedItem.ToString()

        If categorySubCategoryMap.ContainsKey(selectedCategory) Then
            ComboBox2.Items.AddRange(categorySubCategoryMap(selectedCategory).ToArray())
        End If
    End Sub

    Private Sub productmanagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Image = My.Resources.PlaceholderImage
        TextBox5.Visible = False
        Panel1.BackColor = Color.FromArgb(200, 0, 0, 0)
        LoadProducts()
        ComboBox1.Items.AddRange(categorySubCategoryMap.Keys.ToArray())
    End Sub
    Sub clearinput()
        TextBox4.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        RichTextBox1.Clear()
        TextBox5.Clear()
        PictureBox1.Image = My.Resources.PlaceholderImage
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim insertquery As String = "INSERT INTO Products (Name, Category, SubCategory, Price, Discount, Description, ImagePath) 
                           VALUES (@Name, @Category, @SubCategory, @Price, @Discount, @Description, @ImagePath);"
        Try
            conn.Open()
            Using cmd As New MySqlCommand(insertquery, conn)
                cmd.Parameters.AddWithValue("@Name", TextBox4.Text)
                cmd.Parameters.AddWithValue("@Category", ComboBox1.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@SubCategory", ComboBox2.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(TextBox2.Text))
                cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(TextBox3.Text))
                cmd.Parameters.AddWithValue("@Description", RichTextBox1.Text)
                cmd.Parameters.AddWithValue("@ImagePath", TextBox5.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Inserted Sucessfully")
            End Using
            conn.Close()
            LoadProducts()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        clearinput()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            TextBox5.Text = openFileDialog.FileName
            PictureBox1.Image = Image.FromFile(openFileDialog.FileName)
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
        admin.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If DataGridView1.SelectedRows.Count > 0 Then

            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)


            Dim productID As Integer = Convert.ToInt32(selectedRow.Cells("ProductID").Value)

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                DeleteProductFromDatabase(productID)

                DataGridView1.Rows.Remove(selectedRow)

                MessageBox.Show("Product deleted successfully.")
            End If
        Else
            MessageBox.Show("Please select a product to delete.")
        End If
        LoadProducts()
    End Sub
    Private Sub DeleteProductFromDatabase(productID As Integer)
        Dim query As String = "DELETE FROM products WHERE ProductID = @ID"

        Try
            conn.Open()

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ID", productID)

            cmd.ExecuteNonQuery()

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

            Dim productID As Integer = Convert.ToInt32(selectedRow.Cells("ProductID").Value)

            Dim currentProduct As DataTable = GetCurrentProductData(productID)

            Dim name As String = If(String.IsNullOrEmpty(TextBox4.Text), currentProduct.Rows(0)("Name").ToString(), TextBox4.Text)
            Dim price As Decimal = If(String.IsNullOrEmpty(TextBox2.Text), currentProduct.Rows(0)("Price"), Convert.ToDecimal(TextBox2.Text))
            Dim category As String = If(ComboBox1.SelectedIndex = -1, currentProduct.Rows(0)("Category").ToString(), ComboBox1.SelectedItem.ToString())
            Dim subCategory As String = If(ComboBox2.SelectedIndex = -1, currentProduct.Rows(0)("SubCategory").ToString(), ComboBox2.SelectedItem.ToString())
            Dim discount As Decimal = If(String.IsNullOrEmpty(TextBox3.Text), currentProduct.Rows(0)("Discount"), Convert.ToDecimal(TextBox3.Text))
            Dim imagePath As String = If(String.IsNullOrEmpty(TextBox5.Text), currentProduct.Rows(0)("ImagePath").ToString(), TextBox5.Text)
            Dim description As String = If(String.IsNullOrEmpty(RichTextBox1.Text), currentProduct.Rows(0)("Description").ToString(), RichTextBox1.Text)

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to modify this product?", "Modify Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ModifyProductInDatabase(productID, name, price, category, subCategory, discount, imagePath, description)
                LoadProducts()
                clearinput()
                MessageBox.Show("Product modified successfully.")
            End If
        Else
            MessageBox.Show("Please select a product to modify.")
        End If
    End Sub
    Private Function GetCurrentProductData(productID As Integer) As DataTable
        Dim query As String = "SELECT * FROM products WHERE ProductID = @ProductID"
        Dim currentProduct As New DataTable()

        Try
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ProductID", productID)
            Dim adapter As New MySqlDataAdapter(cmd)
            adapter.Fill(currentProduct)
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        Return currentProduct
    End Function

    Private Sub ModifyProductInDatabase(productID As Integer, name As String, price As Decimal, category As String, subCategory As String, discount As Decimal, imagePath As String, description As String)

        Dim query As String = "UPDATE products SET Name = @Name, Price = @Price, Category = @Category, SubCategory = @SubCategory, Discount = @Discount, ImagePath = @ImagePath, Description = @Description WHERE ProductID = @ProductID"

        Try
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Name", name)
            cmd.Parameters.AddWithValue("@Price", price)
            cmd.Parameters.AddWithValue("@Category", category)
            cmd.Parameters.AddWithValue("@SubCategory", subCategory)
            cmd.Parameters.AddWithValue("@Discount", discount)
            cmd.Parameters.AddWithValue("@ImagePath", imagePath)
            cmd.Parameters.AddWithValue("@Description", description)
            cmd.Parameters.AddWithValue("@ProductID", productID)
            cmd.ExecuteNonQuery()
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub showmatch()
        Dim searchText As String = TextBox7.Text.Trim()
        Dim query As String = "SELECT ProductID,Name, Category, SubCategory, Price, Discount, Description, ImagePath FROM products WHERE " &
                          "Category LIKE @searchText OR " &
                          "SubCategory LIKE @searchText OR " &
                          "Price LIKE @searchText OR " &
                          "Discount LIKE @searchText OR " &
                          "Description LIKE @searchText ;"
        DataGridView1.Columns.Clear()

        Try
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@searchText", "%" & searchText & "%")

                Dim adapter As New MySqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table
                DataGridView1.ReadOnly = True
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
            If DataGridView1.Columns.Contains("ProductID") Then
                DataGridView1.Columns("ProductID").Visible = False
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

            DataGridView1.Columns("Description").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                DataGridView1.Columns("Description").Width = 220
                DataGridView1.Columns("Description").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        showmatch()
        TextBox7.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WindowState = FormWindowState.Minimized
    End Sub
End Class