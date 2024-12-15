Imports MySql.Web
Imports MySql.Data.MySqlClient
Public Class Login
    Inherits System.Web.UI.Page

    Dim cmd As New MySqlCommand
    Dim dr As MySqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MultiViewAuthorization.ActiveViewIndex = 0
        connect()
    End Sub

    Protected Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        If TbUsername.Text = "" Or TbPassword.Text = "" Then
            ErrorMes.Visible = True
            ErrorMes.Text = "Data yang anda masukkan kosong"
        Else
            ErrorMes.Visible = False
            cmd = New MySqlCommand("SELECT id_admin, nama_admin, tipe_admin, password from admin where nama_admin=@namaadmin and password=@pass", cn)
            cmd.Parameters.AddWithValue("@namaadmin", TbUsername.Text)
            cmd.Parameters.AddWithValue("@pass", TbPassword.Text)
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read
                    Dim tipeAdmin As String = dr("tipe_admin").ToString()
                    Dim idAdmin As String = dr("id_admin").ToString()

                    Session("id_admin") = idAdmin

                    Select Case tipeAdmin
                        Case "pengguna"
                            Response.Redirect("pengguna.aspx")
                        Case "admin"
                            Response.Redirect("admin.aspx")
                        Case "manajer"
                            Response.Redirect("laporan.aspx")
                        Case Else
                            ErrorMes.Text = "Tipe Admin tidak dikenali."
                            ErrorMes.Visible = True
                    End Select
                End While
            Else
                ErrorMes.Visible = True
                ErrorMes.Text = "Username atau Password tidak sesuai"
                TbUsername.Text = ""
                TbPassword.Text = ""
            End If
        End If
    End Sub

    Protected Sub LkRegister_Click(sender As Object, e As EventArgs) Handles LkRegister.Click
        MultiViewAuthorization.ActiveViewIndex = 1
    End Sub

    Protected Sub LkLogin_Click(sender As Object, e As EventArgs) Handles LkLogin.Click
        MultiViewAuthorization.ActiveViewIndex = 0
    End Sub

    Protected Sub BtnRegister_Click(sender As Object, e As EventArgs) Handles BtnRegister.Click
        Dim idadmin As String
        Dim tipe As String = "pengguna"
        Dim prefix As String = String.Empty

        Select Case tipe.ToLower()
            Case "pengguna"
                prefix = "P"
            Case "admin"
                prefix = "A"
            Case "laporan"
                prefix = "L"
            Case Else
                Throw New ArgumentException("Tipe tidak valid.")
        End Select

        Dim countCmd As New MySqlCommand("SELECT COUNT(*) FROM admin WHERE tipe_admin = @type", cn)
        countCmd.Parameters.AddWithValue("@type", tipe)
        Dim count As Integer = Convert.ToInt32(countCmd.ExecuteScalar()) + 1

        idadmin = prefix & count

        If TbNewUser.Text = "" Or TbNewPassword.Text = "" Then
            ErrorMes.Visible = True
            ErrorMes.Text = "Data yang anda masukkan kosong"
        Else
            cmd = New MySqlCommand("INSERT INTO admin (id_admin, tipe_admin, nama_admin, password) VALUES (@idAdmin, @type, @namaAdmin, @password)", cn)
            cmd.Parameters.AddWithValue("@idAdmin", idadmin)
            cmd.Parameters.AddWithValue("@type", tipe)
            cmd.Parameters.AddWithValue("@namaAdmin", TbNewUser.Text)
            cmd.Parameters.AddWithValue("@password", TbNewPassword.Text)

            cmd.ExecuteNonQuery()
            MultiViewAuthorization.ActiveViewIndex = 0
        End If
    End Sub
End Class