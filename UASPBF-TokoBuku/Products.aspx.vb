Imports MySql.Web
Imports MySql.Data.MySqlClient
Public Class Products
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Protected Sub btnAddBuku_Click(sender As Object, e As EventArgs) Handles btnAddBuku.Click
        Dim prefix As String = TbPenulis.Text.Substring(0, 1).ToUpper() & TbKategori.Text.Substring(0, 1).ToUpper()
        Dim queryIncrement As String = $"SELECT MAX(CAST(SUBSTRING(id_buku, 3) AS UNSIGNED)) FROM buku WHERE id_buku LIKE '{prefix}%'"
        Dim newId As String = prefix & "1"

        Try
            connect()
            cmd = New MySqlCommand(queryIncrement, cn)
            Dim result = cmd.ExecuteScalar()

            If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                Dim maxNumber As Integer = Convert.ToInt32(result)
                newId = prefix & (maxNumber + 1).ToString()
            End If

            Dim query As String = $"INSERT INTO buku (id_buku,nama_buku,kategori_buku,penulis_buku,harga_buku,stok) VALUES ('{newId}','{TbNamaBuku.Text}','{TbKategori.Text}','{TbPenulis.Text}','{TbHarga.Text}','{TbStok.Text}')"
            dml(query)
            ClearFields()
            LoadInventory()
        Catch ex As Exception
            Response.Write($"<script>alert('Gagal menambahkan buku: {ex.Message}')</script>")
        Finally
            cn.Close()
        End Try
    End Sub

    Protected Sub gvInventoryBuku_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "CustomEdit" Then
            Dim id_buku As String = e.CommandArgument.ToString()
            Try
                connect()
                Dim query As String = $"SELECT * From buku WHERE id_buku = '{id_buku}'"
                cmd = New MySqlCommand(query, cn)
                dr = cmd.ExecuteReader()
                If dr.Read() Then
                    hfIdBuku.Value = dr("id_buku").ToString()
                    TbNamaBuku.Text = dr("nama_buku").ToString()
                    TbKategori.Text = dr("kategori_buku").ToString()
                    TbPenulis.Text = dr("penulis_buku").ToString()
                    TbHarga.Text = dr("harga_buku").ToString()
                    TbStok.Text = dr("stok").ToString()
                    btnAddBuku.Visible = False
                    btnUpdateBuku.Visible = True
                End If
            Catch ex As Exception
                Response.Write($"<script>alert('Gagal dalam mengambil buku: {ex.Message}')</script>")
            Finally
                dr.Close()
                cn.Close()
            End Try
        ElseIf e.CommandName = "CustomDelete" Then
            Dim id_buku As String = e.CommandArgument.ToString()
            Try
                connect()
                Dim query As String = $"DELETE From buku Where id_buku = '{id_buku}'"
                dml(query)
                LoadInventory()
            Catch ex As Exception
                Response.Write($"<script>alert('Gagal dalam menghapus buku: {ex.Message}')</script>")
            Finally
                cn.Close()
            End Try
        End If
    End Sub

    Protected Sub btnUpdateBuku_Click(sender As Object, e As EventArgs) Handles btnUpdateBuku.Click
        Try
            connect()
            Dim query As String = $"UPDATE buku SET nama_buku = '{TbNamaBuku.Text}', kategori_buku = '{TbKategori.Text}', penulis_buku = '{TbPenulis.Text}', harga_buku = '{TbHarga.Text}', stok = '{TbStok.Text}' WHERE id_buku = '{hfIdBuku.Value}'"
            dml(query)
            ClearFields()
            btnAddBuku.Visible = True
            btnUpdateBuku.Visible = False
            LoadInventory()
        Catch ex As Exception
            Response.Write($"<script>alert('Gagal dalam memperbarui buku: {ex.Message}')</script>")
        Finally
            cn.Close()
        End Try
    End Sub

    Private Sub ClearFields()
        TbNamaBuku.Text = ""
        TbKategori.Text = ""
        TbPenulis.Text = ""
        TbHarga.Text = ""
        TbStok.Text = ""
    End Sub

End Class