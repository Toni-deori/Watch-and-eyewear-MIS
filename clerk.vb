Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class clerk
    Dim connectionString As String = "Server=localhost;Database=eyeandwatchmis;userid=root;password=''"
    Dim conn As New MySqlConnection(connectionString)

    Dim currentClerkID As Integer

    Public Sub New(clerkID As Integer)

        InitializeComponent()

        currentClerkID = clerkID
    End Sub
    'Private Sub LoadProducts()
    '    Try
    '        conn.Open()

    '        Dim query As String = "SELECT ProductID, Name, ImagePath, Price, Discount, Description FROM Products"
    '        Dim cmd As New MySqlCommand(query, conn)

    '        Dim reader As MySqlDataReader = cmd.ExecuteReader()

    '        Panel1.Controls.Clear()

    '        Dim xPos As Integer = 10
    '        Dim yPos As Integer = 10
    '        Dim width As Integer = 100
    '        Dim height As Integer = 120
    '        Dim buttonHeight As Integer = 30
    '        Dim spacing As Integer = 10

    '        While reader.Read()
    '            Dim productID As Integer = Convert.ToInt32(reader("ProductID"))
    '            Dim productName As String = reader("Name").ToString()
    '            Dim imagePath As String = reader("ImagePath").ToString()
    '            Dim productPrice As Decimal = Convert.ToDecimal(reader("Price"))
    '            Dim productDiscount As Decimal = Convert.ToDecimal(reader("Discount"))
    '            Dim productDescription As String = reader("Description").ToString()

    '            Dim pictureBox As New PictureBox()
    '            pictureBox.Image = If(imagePath <> "", Image.FromFile(imagePath), Nothing)
    '            pictureBox.Size = New Size(width, height)
    '            pictureBox.Location = New Point(xPos, yPos)
    '            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
    '            pictureBox.Tag = New Tuple(Of Integer, String, Decimal, Decimal, String)(productID, productName, productPrice, productDiscount, productDescription)

    '            Dim label As New Label()
    '            label.Text = productName
    '            label.Font = New Font("Palatino Linotype", 8)
    '            label.BackColor = Color.Transparent
    '            label.Location = New Point(xPos, yPos + height + 5)
    '            label.Size = New Size(width, 20)
    '            label.TextAlign = ContentAlignment.MiddleCenter

    '            Dim descriptionButton As New Button()
    '            descriptionButton.ForeColor = Color.Black
    '            descriptionButton.Font = New Font("Palatino Linotype", 12)
    '            descriptionButton.BackColor = Color.FromArgb(255, 255, 128, 128)
    '            descriptionButton.Text = "Description"
    '            descriptionButton.Size = New Size(width, buttonHeight)
    '            descriptionButton.Location = New Point(xPos, yPos + height + 25)
    '            descriptionButton.Tag = productDescription
    '            AddHandler descriptionButton.Click, AddressOf DescriptionButton_Click

    '            Dim lowStockAlertButton As New Button()
    '            lowStockAlertButton.ForeColor = Color.White
    '            lowStockAlertButton.Font = New Font("Palatino Linotype", 12)
    '            lowStockAlertButton.BackColor = Color.FromArgb(255, 255, 0, 0)
    '            lowStockAlertButton.Text = "Flag Low Stock"
    '            lowStockAlertButton.Size = New Size(width, buttonHeight)
    '            lowStockAlertButton.Location = New Point(xPos, yPos + height + 55)
    '            lowStockAlertButton.Tag = productID
    '            AddHandler lowStockAlertButton.Click, AddressOf LowStockAlertButton_Click

    '            Panel1.Controls.Add(pictureBox)
    '            Panel1.Controls.Add(label)
    '            Panel1.Controls.Add(descriptionButton)
    '            Panel1.Controls.Add(lowStockAlertButton)

    '            xPos += width + spacing
    '            If xPos + width > Panel1.Width Then
    '                xPos = 10
    '                yPos += height + buttonHeight + 70
    '            End If

    '            AddHandler pictureBox.Click, AddressOf PictureBox_Click
    '        End While

    '    Catch ex As MySqlException
    '        MessageBox.Show("Error loading products: " & ex.Message)
    '    Finally
    '        conn.Close()
    '    End Try
    'End Sub
    Private Sub LoadProducts()
        Try
            conn.Open()

            Dim query As String = "SELECT ProductID, Name, ImagePath, Price, Discount, Description FROM Products"
            Dim cmd As New MySqlCommand(query, conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Panel1.Controls.Clear()

            Dim width As Integer = 200
            Dim height As Integer = 240
            Dim buttonHeight As Integer = 40
            Dim spacing As Integer = 20

            Dim xPos As Integer = 10
            Dim yPos As Integer = 10

            While reader.Read()
                Dim productID As Integer = Convert.ToInt32(reader("ProductID"))
                Dim productName As String = reader("Name").ToString()
                Dim imagePath As String = reader("ImagePath").ToString()
                Dim productPrice As Decimal = Convert.ToDecimal(reader("Price"))
                Dim productDiscount As Decimal = Convert.ToDecimal(reader("Discount"))
                Dim productDescription As String = reader("Description").ToString()

                Dim pictureBox As New PictureBox()
                pictureBox.Image = If(imagePath <> "", Image.FromFile(imagePath), Nothing)
                pictureBox.Size = New Size(width, height)
                pictureBox.Location = New Point(xPos, yPos)
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                pictureBox.Tag = New Tuple(Of Integer, String, Decimal, Decimal, String)(productID, productName, productPrice, productDiscount, productDescription)


                Dim label As New Label()
                label.Text = productName
                label.Font = New Font("Palatino Linotype", 10, FontStyle.Bold)
                label.BackColor = Color.Transparent
                label.Location = New Point(xPos, yPos + height + 5)
                label.Size = New Size(width, 25)
                label.TextAlign = ContentAlignment.MiddleCenter


                Dim descriptionButton As New Button()
                descriptionButton.ForeColor = Color.Black
                descriptionButton.Font = New Font("Palatino Linotype", 10)
                descriptionButton.BackColor = Color.FromArgb(255, 255, 128, 128)
                descriptionButton.Text = "Description"
                descriptionButton.Size = New Size(width, buttonHeight)
                descriptionButton.Location = New Point(xPos, yPos + height + 35)
                descriptionButton.Tag = productDescription
                AddHandler descriptionButton.Click, AddressOf DescriptionButton_Click

                Dim lowStockAlertButton As New Button()
                lowStockAlertButton.ForeColor = Color.White
                lowStockAlertButton.Font = New Font("Palatino Linotype", 10)
                lowStockAlertButton.BackColor = Color.FromArgb(255, 255, 0, 0)
                lowStockAlertButton.Text = "Flag Low Stock"
                lowStockAlertButton.Size = New Size(width, buttonHeight)
                lowStockAlertButton.Location = New Point(xPos, yPos + height + 80)
                lowStockAlertButton.Tag = productID
                AddHandler lowStockAlertButton.Click, AddressOf LowStockAlertButton_Click

                Panel1.Controls.Add(pictureBox)
                Panel1.Controls.Add(label)
                Panel1.Controls.Add(descriptionButton)
                Panel1.Controls.Add(lowStockAlertButton)

                xPos += width + spacing
                If xPos + width > Panel1.Width Then
                    xPos = 10
                    yPos += height + buttonHeight + 110
                End If

                AddHandler pictureBox.Click, AddressOf PictureBox_Click
            End While

        Catch ex As MySqlException
            MessageBox.Show("Error loading products: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub LowStockAlertButton_Click(sender As Object, e As EventArgs)
        Try
            Dim lowStockProductID As Integer = Convert.ToInt32(DirectCast(sender, Button).Tag)

            Dim stockQuery As String = "SELECT StockQuantity FROM Products WHERE ProductID = @ProductID"

            conn.Open()

            Dim cmdStock As New MySqlCommand(stockQuery, conn)
            cmdStock.Parameters.AddWithValue("@ProductID", lowStockProductID)

            Dim stockQuantity As Integer = Convert.ToInt32(cmdStock.ExecuteScalar())

            If stockQuantity > 10 Then
                MessageBox.Show("No low stock alert generated. Stock is greater than 10 for Product ID: " & lowStockProductID)
                Return
            End If

            Dim insertQuery As String = "INSERT INTO lowstockalerts (ProductID, AlertDate, Status) VALUES (@ProductID, @AlertDate, @Status)"

            Using cmd As New MySqlCommand(insertQuery, conn)

                cmd.Parameters.AddWithValue("@ProductID", lowStockProductID)
                cmd.Parameters.AddWithValue("@AlertDate", DateTime.Now)
                cmd.Parameters.AddWithValue("@Status", "Pending")

                cmd.ExecuteNonQuery()
                MessageBox.Show("Low Stock Alert flagged successfully for Product ID: " & lowStockProductID)
            End Using

        Catch ex As MySqlException
            MessageBox.Show("Error flagging low stock alert: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    Private Sub PictureBox_Click(sender As Object, e As EventArgs)
        Dim pictureBox As PictureBox = CType(sender, PictureBox)
        Dim productInfo As Tuple(Of Integer, String, Decimal, Decimal, String) = CType(pictureBox.Tag, Tuple(Of Integer, String, Decimal, Decimal, String))
        Dim productID As Integer = productInfo.Item1
        Dim productName As String = productInfo.Item2
        Dim productPrice As Decimal = productInfo.Item3
        Dim productDiscount As Decimal = productInfo.Item4
        Dim quantity As Integer = 1
        Dim input As String = InputBox("Enter quantity:", "Quantity", quantity.ToString())
        If Integer.TryParse(input, quantity) AndAlso quantity > 0 Then
            Dim stockQuantity As Integer = GetProductStockQuantity(productID)
            Dim cartQuantity As Integer = If(cart.ContainsKey(productID), cart(productID), 0)
            Dim totalQuantity As Integer = cartQuantity + quantity
            If totalQuantity > stockQuantity Then
                MessageBox.Show("Not enough stock available. Only " & stockQuantity & " units are available.", "Stock Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If cart.ContainsKey(productID) Then
                    cart(productID) += quantity
                Else
                    cart.Add(productID, quantity)
                End If
                Dim finalPrice As Decimal = productPrice * quantity
                If productDiscount > 0 Then
                    finalPrice -= finalPrice * (productDiscount / 100)
                End If
                AddProductToDataGridView(productID, productName, productPrice, productDiscount, quantity, finalPrice)
                UpdateTotal()
            End If
        Else
            MessageBox.Show("Invalid quantity entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Function GetProductStockQuantity(productID As Integer) As Integer
        Dim stockQuantity As Integer = 0
        Dim query As String = "SELECT StockQuantity FROM Products WHERE ProductID = @ProductID"
        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ProductID", productID)
                stockQuantity = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
            conn.Close()
        Catch ex As MySqlException
            MessageBox.Show("Error fetching stock quantity: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return stockQuantity
    End Function
    Private Sub DescriptionButton_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim productDescription As String = CType(button.Tag, String)
        MessageBox.Show(productDescription, "Product Description")
    End Sub
    Private Sub AddProductToDataGridView(productID As Integer, productName As String, productPrice As Decimal, productDiscount As Decimal, quantity As Integer, finalPrice As Decimal)
        Dim existingRow As DataGridViewRow = Nothing
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("ProductID").Value = productID Then
                existingRow = row
                Exit For
            End If
        Next

        If existingRow IsNot Nothing Then
            existingRow.Cells("Quantity").Value = Convert.ToInt32(existingRow.Cells("Quantity").Value) + quantity
            existingRow.Cells("TotalPrice").Value = Convert.ToDecimal(existingRow.Cells("TotalPrice").Value) + finalPrice
        Else
            DataGridView1.Rows.Add(productID, productName, productPrice, productDiscount, quantity, finalPrice)
        End If
    End Sub

    Private cart As New Dictionary(Of Integer, Integer)()
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim amountPaid As Decimal = 0D
        If Decimal.TryParse(TextBox1.Text, amountPaid) Then

            Dim totalAmount As Decimal = 0D
            If Decimal.TryParse(Label5.Text.Replace("Total: ", "").Trim(), totalAmount) Then
                Dim change As Decimal = amountPaid - totalAmount
                If change >= 0 Then
                    Label4.Text = change.ToString("F2")
                Else
                    Label4.Text = "Insufficient Amount!"
                End If
            Else
                Label4.Text = "Invalid Total"
            End If
        Else
            Label4.Text = "Invalid Amount"
        End If
    End Sub
    Private Sub clerk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Columns.Add("ProductID", "Product ID")
        DataGridView1.Columns.Add("ProductName", "Product Name")
        DataGridView1.Columns.Add("ProductPrice", "Price")
        DataGridView1.Columns.Add("Discount", "Discount (%)")
        DataGridView1.Columns.Add("Quantity", "Quantity")
        DataGridView1.Columns.Add("TotalPrice", "Total Price")
        Panel1.BackColor = Color.FromArgb(200, 0, 0, 0)
        LoadProducts()
    End Sub
    Private WithEvents printDoc As New Printing.PrintDocument()
    Private WithEvents printPreviewDialog As New PrintPreviewDialog()
    Private Sub UpdateTotal()
        Dim subtotal As Decimal = 0D
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("TotalPrice").Value IsNot Nothing Then
                subtotal += Convert.ToDecimal(row.Cells("TotalPrice").Value)
            End If
        Next
        Dim tax As Decimal = subtotal * 0.16D
        Dim total As Decimal = subtotal + tax
        Label10.Text = subtotal.ToString("F2")
        Label9.Text = tax.ToString("F2")
        Label5.Text = total.ToString("F2")
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            TextBox1.Clear()
            Label4.Text = "0"
            TextBox1.Enabled = True
        Else
            TextBox1.Enabled = False
            TextBox1.Text = Label5.Text
            Label4.Text = "0"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("No items in the cart. Please add products to the cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim totalAmount As Decimal = Convert.ToDecimal(Label5.Text)

        Dim saleID As Integer = InsertSale(totalAmount)

        If saleID > 0 Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim productID As Integer = Convert.ToInt32(row.Cells("ProductID").Value)
                Dim productPrice As Decimal = Convert.ToDecimal(row.Cells("ProductPrice").Value)
                Dim productDiscount As Decimal = Convert.ToDecimal(row.Cells("Discount").Value)
                Dim quantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                Dim totalPrice As Decimal = Convert.ToDecimal(row.Cells("TotalPrice").Value)

                InsertSaleDetail(saleID, productID, quantity, productPrice, productDiscount)

                UpdateProductStock(productID, quantity)
            Next
            MessageBox.Show("Sale completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            print()
            DataGridView1.Rows.Clear()
            Label10.Text = "0.00"
            Label9.Text = "0.00"
            Label5.Text = "0.00"
            Label4.Text = "0.00"
        Else
            MessageBox.Show("Failed to complete the sale. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Function InsertSale(totalAmount As Decimal) As Integer
        Dim saleID As Integer = 0
        Dim query As String = "INSERT INTO Sales (Date, ClerkID,CustomerName, TotalAmount) VALUES (NOW(), @ClerkID,@cname, @TotalAmount); SELECT LAST_INSERT_ID();"

        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ClerkID", currentClerkID)
                cmd.Parameters.AddWithValue("@cname", TextBox2.Text)
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
                saleID = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        Catch ex As MySqlException
            MessageBox.Show("Error inserting sale: " & ex.Message)
        Finally
            conn.Close()
        End Try

        Return saleID
    End Function
    Private Sub InsertSaleDetail(saleID As Integer, productID As Integer, quantity As Integer, priceAtSale As Decimal, discountAtSale As Decimal)
        Dim query As String = "INSERT INTO SalesDetails (SaleID, ProductID, Quantity, PriceAtSale, DiscountAtSale) VALUES (@SaleID, @ProductID, @Quantity, @PriceAtSale, @DiscountAtSale)"

        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@SaleID", saleID)
                cmd.Parameters.AddWithValue("@ProductID", productID)
                cmd.Parameters.AddWithValue("@Quantity", quantity)
                cmd.Parameters.AddWithValue("@PriceAtSale", priceAtSale)
                cmd.Parameters.AddWithValue("@DiscountAtSale", discountAtSale)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As MySqlException
            MessageBox.Show("Error inserting sale details: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
    Private Sub UpdateProductStock(productID As Integer, quantity As Integer)
        Dim query As String = "UPDATE Products SET StockQuantity = StockQuantity - @Quantity WHERE ProductID = @ProductID"

        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ProductID", productID)
                cmd.Parameters.AddWithValue("@Quantity", quantity)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As MySqlException
            MessageBox.Show("Error updating product stock: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        cart.Clear()
        DataGridView1.Rows.Clear()
        Label10.Text = ""
        Label9.Text = ""
        Label5.Text = ""
        TextBox1.Text = ""
        Label4.Text = ""
        ComboBox1.SelectedIndex = -1
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            For i As Integer = DataGridView1.SelectedRows.Count - 1 To 0 Step -1
                Dim productID As Integer = Convert.ToInt32(DataGridView1.SelectedRows(i).Cells("ProductID").Value)
                If cart.ContainsKey(productID) Then
                    cart.Remove(productID)
                End If
                DataGridView1.Rows.Remove(DataGridView1.SelectedRows(i))
            Next
            UpdateTotal()
        Else
            MessageBox.Show("Please select rows to remove.", "No Rows Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Sub print()
        printPreviewDialog.Document = printDoc
        printPreviewDialog.ShowDialog()
    End Sub
    Private Sub printDoc_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        Dim g As Graphics = e.Graphics
        Dim font As New Font("Arial", 12)
        Dim boldFont As New Font("Arial", 12, FontStyle.Bold)
        Dim yPos As Integer = 10
        Dim xPos As Integer = 10
        Dim lineHeight As Integer = 20
        Dim columnWidth As Integer = 150
        Dim marginLeft As Integer = 10
        Dim marginTop As Integer = 10
        Dim marginRight As Integer = 10
        Dim marginBottom As Integer = 10
        g.DrawString("Receipt", boldFont, Brushes.Black, xPos, yPos)
        yPos += 30
        g.DrawLine(Pens.Black, marginLeft, yPos, e.PageBounds.Width - marginRight, yPos)
        yPos += 5
        g.DrawString("Customer: " & TextBox2.Text, font, Brushes.Black, xPos, yPos)
        yPos += lineHeight
        g.DrawLine(Pens.Black, marginLeft, yPos, e.PageBounds.Width - marginRight, yPos)
        yPos += 10
        g.DrawString("Product Name", boldFont, Brushes.Black, xPos, yPos)
        g.DrawString("Quantity", boldFont, Brushes.Black, xPos + columnWidth, yPos)
        g.DrawString("Price", boldFont, Brushes.Black, xPos + columnWidth * 2, yPos)
        g.DrawString("Total", boldFont, Brushes.Black, xPos + columnWidth * 3, yPos)
        yPos += lineHeight
        g.DrawLine(Pens.Black, marginLeft, yPos, e.PageBounds.Width - marginRight, yPos)
        yPos += 5
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("ProductName").Value IsNot Nothing Then
                g.DrawString(row.Cells("ProductName").Value.ToString(), font, Brushes.Black, xPos, yPos)
                g.DrawString(row.Cells("Quantity").Value.ToString(), font, Brushes.Black, xPos + columnWidth, yPos)
                g.DrawString(row.Cells("ProductPrice").Value.ToString(), font, Brushes.Black, xPos + columnWidth * 2, yPos)
                g.DrawString(row.Cells("TotalPrice").Value.ToString(), font, Brushes.Black, xPos + columnWidth * 3, yPos)
                yPos += lineHeight
            End If
        Next
        g.DrawLine(Pens.Black, marginLeft, yPos, e.PageBounds.Width - marginRight, yPos)
        yPos += 10
        g.DrawString("Subtotal: " & Label10.Text, font, Brushes.Black, xPos, yPos)
        yPos += lineHeight
        g.DrawString("Tax: " & Label9.Text, font, Brushes.Black, xPos, yPos)
        yPos += lineHeight
        g.DrawString("Total: " & Label5.Text, boldFont, Brushes.Black, xPos, yPos)
        yPos += lineHeight
        g.DrawLine(Pens.Black, marginLeft, yPos, e.PageBounds.Width - marginRight, yPos)
        yPos += 10
        g.DrawString("Thank you for your purchase!", font, Brushes.Black, xPos, yPos)
        yPos += 30
        g.DrawString("Eye Wear and Watch co.", font, Brushes.Black, xPos, yPos)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
        Form1.Show()
    End Sub
End Class