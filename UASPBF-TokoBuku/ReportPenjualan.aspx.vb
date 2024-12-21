Imports MySql.Web
Imports MySql.Data.MySqlClient
Public Class ReportPenjualan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub LoadSales()
        Try
            connect()

        Catch ex As Exception
            Response.Write($"<script>alert('Gagal dalam loading penjualan: {ex.Message}')</script>")
        Finally
            cn.Close()
        End Try
    End Sub

End Class