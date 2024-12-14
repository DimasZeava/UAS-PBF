Imports MySql.Web
Imports MySql.Data.MySqlClient

Module Koneksi

    Public cn As New MySqlConnection
    Public cmd As New MySqlCommand
    Public dr As MySqlDataReader

    Public Sub connect()
        cn.Close()
        cn = New MySqlConnection("Data Source=localhost;Database=tokobuku;User ID=root;Password=;")
        cn.Open()
    End Sub

    Public Sub dml(q As String)
        cmd = New MySqlCommand(q, cn)
        cmd.ExecuteNonQuery()
    End Sub
End Module
