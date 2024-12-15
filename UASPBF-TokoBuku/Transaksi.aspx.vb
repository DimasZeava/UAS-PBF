Imports MySql.Web
Imports MySql.Data.MySqlClient
Public Class Transaksi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_admin") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            LoadInventory()
        End If
    End Sub

    Private Sub LoadInventory()
        Try
            connect()
            Dim query As String = "SELECT * from buku"
            Dim da As New MySqlDataAdapter(query, cn)
            Dim dt As New DataTable()
            da.Fill(dt)
            gvInventoryBuku.DataSource = dt
            gvInventoryBuku.DataBind()
        Catch ex As Exception
            Response.Write($"<script>alert('Gagal dalam loading inventory: {ex.Message}')</script>")
        Finally
            cn.Close()
        End Try
    End Sub

    Protected Sub gvInventoryBuku_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        cn.Open()
        If e.CommandName = "CustomAdd" Then
            Dim idBuku As String = e.CommandArgument.ToString()

            Dim query As String = $"SELECT id_buku, nama_buku, harga_buku FROM buku WHERE id_buku = '{idBuku}'"
            dml(query)

            dr = cmd.ExecuteReader()
            If dr.Read() Then
                Dim namaBuku As String = dr("nama_buku").ToString()
                Dim hargaBuku As Integer = Convert.ToInt32(dr("harga_buku"))
                Dim keranjang As DataTable = TryCast(ViewState("Keranjang"), DataTable)
                If keranjang Is Nothing Then
                    keranjang = New DataTable()
                    keranjang.Columns.Add("id_buku")
                    keranjang.Columns.Add("nama_buku")
                    keranjang.Columns.Add("harga_buku", GetType(Integer))
                    keranjang.Columns.Add("jumlah", GetType(Integer))
                    keranjang.Columns.Add("total_harga", GetType(Integer))
                End If

                Dim existingRow = keranjang.AsEnumerable().FirstOrDefault(Function(row) row("id_buku").ToString() = idBuku)
                If existingRow IsNot Nothing Then
                    existingRow("jumlah") = Convert.ToInt32(existingRow("jumlah")) + 1
                    existingRow("total_harga") = Convert.ToInt32(existingRow("jumlah")) * Convert.ToInt32(existingRow("harga_buku"))
                Else
                    keranjang.Rows.Add(idBuku, namaBuku, hargaBuku, 1, hargaBuku)
                End If

                ViewState("Keranjang") = keranjang
                gvKeranjang.DataSource = keranjang
                gvKeranjang.DataBind()

                UpdateTotalPembayaran()
                LoadInventory()
            End If

            dr.Close()
            cn.Close()
        End If
    End Sub

    Private Sub UpdateTotalPembayaran()
        Dim keranjang As DataTable = TryCast(ViewState("Keranjang"), DataTable)
        If keranjang IsNot Nothing Then
            Dim totalPembayaran As Integer = keranjang.AsEnumerable().Sum(Function(row) Convert.ToInt32(row("total_harga")))
            TbTotalPembayaran.Text = totalPembayaran.ToString()
        End If
        UpdateKembalian()
    End Sub
    Private Sub UpdateKembalian()
        If Not String.IsNullOrEmpty(TbPembayaran.Text) Then
            Dim totalBayar As Integer = Convert.ToInt32(TbPembayaran.Text)
            Dim totalPembayaran As Integer = If(String.IsNullOrEmpty(TbTotalPembayaran.Text), 0, Convert.ToInt32(TbTotalPembayaran.Text))
            Dim kembalian As Integer = totalBayar - totalPembayaran
            TbKembalian.Text = kembalian.ToString()
        End If
    End Sub

    Protected Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnBayar.Click
        cn.Open()

        'Generate ID Pelanggan
        Dim prefixPelanggan As String = TbNamaPelanggan.Text.Substring(0, 1).ToUpper() & TbAlamatPelanggan.Text.Substring(0, 1).ToUpper()
        Dim queryIncrementPelanggan As String = $"SELECT MAX(CAST(SUBSTRING(id_pelanggan, 3) AS UNSIGNED)) FROM pelanggan WHERE id_pelanggan LIKE '{prefixPelanggan}%'"
        Dim newIdPelanggan As String = prefixPelanggan & "1"

        Try
            connect()
            cmd = New MySqlCommand(queryIncrementPelanggan, cn)
            Dim result = cmd.ExecuteScalar()

            If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                Dim maxNumber As Integer = Convert.ToInt32(result)
                newIdPelanggan = prefixPelanggan & (maxNumber + 1).ToString()
            End If

            Dim queryPelanggan As String = $"INSERT INTO pelanggan (id_pelanggan,nama_pelanggan,tipe_pelanggan,alamat_pelanggan) VALUES ('{newIdPelanggan}','{TbNamaPelanggan.Text}','{DDLTipePelanggan.SelectedValue}','{TbAlamatPelanggan.Text}')"
            dml(queryPelanggan)
        Catch ex As Exception
            Response.Write($"<script>alert('Gagal menambahkan pelanggan: {ex.Message}')</script>")
        Finally
            cn.Close()
        End Try

        cn.Open()
        Dim idTransaksi As String = Guid.NewGuid().ToString()
        Dim tahun As Integer = DateTime.Now.Year
        Dim bulan As Integer = DateTime.Now.Month
        Dim tanggal As Integer = DateTime.Now.Day
        Dim jenisPembayaran As String = DDLJenisPembayaran.SelectedValue
        Dim totalPembayaran As Integer = Convert.ToInt32(TbTotalPembayaran.Text)

        'Proses Pemasukan ke tabel transaksi
        Dim queryTransaksi As String = "INSERT INTO transaksi (id_transaksi, tahun, bulan, tanggal, jenis_pembayaran, total_pembayaran) VALUES (@id_transaksi, @tahun, @bulan, @tanggal, @jenis_pembayaran, @total_pembayaran)"
        Dim cmdTransaksi As New MySqlCommand(queryTransaksi, cn)
        cmdTransaksi.Parameters.AddWithValue("@id_transaksi", idTransaksi)
        cmdTransaksi.Parameters.AddWithValue("@tahun", tahun)
        cmdTransaksi.Parameters.AddWithValue("@bulan", bulan)
        cmdTransaksi.Parameters.AddWithValue("@tanggal", tanggal)
        cmdTransaksi.Parameters.AddWithValue("@jenis_pembayaran", jenisPembayaran)
        cmdTransaksi.Parameters.AddWithValue("@total_pembayaran", totalPembayaran)
        cmdTransaksi.ExecuteNonQuery()

        Dim idAdmin As String = Session("id_admin").ToString()
        'Proses pemasukan ke tabel tabel_fakta
        Dim keranjang As DataTable = TryCast(ViewState("Keranjang"), DataTable)
        If keranjang IsNot Nothing Then
            For Each row As DataRow In keranjang.Rows
                Dim queryFakta As String = "INSERT INTO table_fakta (id_admin, id_pelanggan, id_buku, id_transaksi, total_pendapatan) VALUES (@id_admin, @id_pelanggan, @id_buku, @id_transaksi, @total_pendapatan)"
                Dim cmdFakta As New MySqlCommand(queryFakta, cn)
                cmdFakta.Parameters.AddWithValue("@id_admin", idAdmin)
                cmdFakta.Parameters.AddWithValue("@id_pelanggan", newIdPelanggan)
                cmdFakta.Parameters.AddWithValue("@id_buku", row("id_buku"))
                cmdFakta.Parameters.AddWithValue("@id_transaksi", idTransaksi)
                cmdFakta.Parameters.AddWithValue("@total_pendapatan", Convert.ToInt32(row("harga_buku")) * Convert.ToInt32(row("jumlah")))
                cmdFakta.ExecuteNonQuery()

                Dim queryUpdateStok As String = "UPDATE buku SET stok = stok - @jumlah WHERE id_buku = @id_buku"
                Dim cmdUpdateStok As New MySqlCommand(queryUpdateStok, cn)
                cmdUpdateStok.Parameters.AddWithValue("@jumlah", Convert.ToInt32(row("jumlah")))
                cmdUpdateStok.Parameters.AddWithValue("@id_buku", row("id_buku"))
                cmdUpdateStok.ExecuteNonQuery()
            Next
        End If

        TbNamaPelanggan.Text = ""
        TbAlamatPelanggan.Text = ""
        ViewState("Keranjang") = Nothing
        gvKeranjang.DataSource = Nothing
        gvKeranjang.DataBind()
        TbTotalPembayaran.Text = ""
        TbKembalian.Text = ""
        cn.Close()
    End Sub
End Class