Imports MySql.Data.MySqlClient

Public Class salesreport
    Dim connectionString As String = "Server=localhost;Database=eyeandwatchmis;userid=root;password=''"
    Dim conn As New MySqlConnection(connectionString)
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim query As String = ""
        Dim parameters As New List(Of MySqlParameter)()
        DataGridView1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 14)
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Microsoft Sans Serif", 14)
        DataGridView1.RowHeadersDefaultCellStyle.Font = New Font("Microsoft Sans Serif", 14)
        query = "SELECT Products.Name AS ProductName, Products.Category, " &
            "SUM(SalesDetails.Quantity) AS TotalQuantitySold, " &
            "SUM(SalesDetails.Quantity * SalesDetails.PriceAtSale) AS TotalSalesAmount " &
            "FROM Sales JOIN SalesDetails ON Sales.SaleID = SalesDetails.SaleID " &
            "JOIN Products ON SalesDetails.ProductID = Products.ProductID "
        If RadioButton1.Checked Then
            query &= "WHERE DATE(Sales.Date) = CURDATE() "
        ElseIf RadioButton2.Checked Then
            query &= "WHERE YEARWEEK(Sales.Date, 1) = YEARWEEK(CURDATE(), 1) "
        ElseIf RadioButton3.Checked Then
            query &= "WHERE MONTH(Sales.Date) = MONTH(CURDATE()) AND YEAR(Sales.Date) = YEAR(CURDATE()) "
        ElseIf RadioButton4.Checked Then
            query &= "WHERE Sales.Date BETWEEN @StartDate AND @EndDate "
            parameters.Add(New MySqlParameter("@StartDate", DateTimePicker1.Value))
            parameters.Add(New MySqlParameter("@EndDate", DateTimePicker2.Value))
        End If
        If ComboBox1.SelectedItem IsNot Nothing Then
            query &= "AND Products.Category = @Category "
            parameters.Add(New MySqlParameter("@Category", ComboBox1.SelectedItem.ToString()))
        End If
        query &= "GROUP BY Products.ProductID;"
        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddRange(parameters.ToArray())
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table
            End Using
        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message)
        Finally
            conn.Close()
        End Try
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataGridView1.RowHeadersVisible = False
        DataGridView1.AllowUserToResizeColumns = False
        DataGridView1.AllowUserToResizeRows = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
        admin.Show()
    End Sub
    Private Sub salesreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.BackColor = Color.FromArgb(200, 0, 0, 0)
    End Sub
End Class