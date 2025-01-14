
Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient
Imports Mysqlx
Public Class admin
    Dim connectionString As String = "Server=localhost;Database=eyeandwatchmis;userid=root;password=''"
    Dim conn As New MySqlConnection(connectionString)
    Private Sub admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Chart1.Series.Clear()
        Dim series As New Series("Monthly Sales")
        series.ChartType = SeriesChartType.Column
        Chart1.Series.Add(series)
        DisplayTotalStockQuantity()
        LoadMonthlySales()
        DisplayLast7DaysPurchases()
        DisplayLast7DaysItems()
        Panel2.BackColor = Color.FromArgb(120, 0, 0, 0)
    End Sub
    Private Sub DisplayLast7DaysPurchases()
        Dim query As String = "SELECT SUM(TotalAmount) AS Last7DaysTotal " &
                              "FROM Sales " &
                              "WHERE Date >= DATE_SUB(CURDATE(), INTERVAL 7 DAY);"
        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                Dim totalStock As Object = cmd.ExecuteScalar()
                If IsDBNull(totalStock) OrElse totalStock Is Nothing Then
                    Label10.Text = "0"
                Else
                    Label10.Text = totalStock.ToString()
                End If
            End Using
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub DisplayLast7DaysItems()
        Dim query As String = "SELECT SUM(SD.Quantity) AS TotalItemsSold
FROM SalesDetails SD
INNER JOIN Sales S ON SD.SaleID = S.SaleID
WHERE S.Date >= DATE_SUB(CURDATE(), INTERVAL 7 DAY);
"
        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                Dim totalStock As Object = cmd.ExecuteScalar()
                If IsDBNull(totalStock) OrElse totalStock Is Nothing Then
                    Label8.Text = "0"
                Else
                    Label8.Text = totalStock.ToString()
                End If
            End Using
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub DisplayTotalStockQuantity()
        Dim query As String = "SELECT SUM(StockQuantity) AS TotalStock FROM Products;"
        Try
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                Dim totalStock As Object = cmd.ExecuteScalar()
                If IsDBNull(totalStock) OrElse totalStock Is Nothing Then
                    Label5.Text = "0"
                Else
                    Label5.Text = totalStock.ToString()
                End If
            End Using
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub LoadMonthlySales()
        Try
            conn.Open()
            Dim query As String = "SELECT MONTH(Date) AS Month, SUM(TotalAmount) AS TotalSales " &
                                  "FROM Sales " &
                                  "WHERE YEAR(Date) = YEAR(CURDATE()) " &
                                  "GROUP BY MONTH(Date) " &
                                  "ORDER BY MONTH(Date);"

            Using cmd As New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim month As Integer = reader.GetInt32("Month")
                    Dim totalSales As Decimal = reader.GetDecimal("TotalSales")
                    Chart1.Series("Monthly Sales").Points.AddXY(GetMonthName(month), totalSales)
                End While
            End Using
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Function GetMonthName(monthNumber As Integer) As String
        Return New DateTime(1, monthNumber, 1).ToString("MMMM")
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WindowState = FormWindowState.Minimized
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
        Form1.Show()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
        salesreport.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
        inventory.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        usermanagement.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        productmanagement.Show()
    End Sub
End Class