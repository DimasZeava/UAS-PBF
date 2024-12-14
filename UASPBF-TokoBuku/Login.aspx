<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="UASPBF_TokoBuku.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles.css" rel="stylesheet" />
    <title>Toko Buku</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="content">
                <asp:MultiView ID="MultiViewAuthorization" runat="server">
                    <asp:View ID="LoginView" runat="server">
                        <div class="content-page">
                            <div class="card">
                                <div class="header-container">
                                    <asp:Image ID="Image" runat="server" />
                                    <asp:Label ID="Label1" runat="server" Text="Label">Toko Buku Sejahtera</asp:Label>
                                    <label for="loginform">Login</label>
                                </div>
                                <div class="input-container">
                                    <asp:TextBox ID="TbUsername" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                    <label for="TbUsername">Username</label>
                                </div>
                                <div class="input-container">
                                    <asp:TextBox ID="TbPassword" runat="server" CssClass="floating-input" placeholder=" " TextMode="Password"></asp:TextBox>
                                    <label for="TbPassword">Password</label>
                                </div>
                                <asp:Button ID="BtnLogin" runat="server" Text="Masuk" CssClass="button" />
                            </div>
                            <p>Belum memiliki akun? <span><asp:LinkButton ID="LkRegister" runat="server" CssClass="link">Daftar Sekarang</asp:LinkButton></span></p>
                        </div>
                    </asp:View>
                    <asp:View ID="RegisterView" runat="server">
                        <div class="content-page">
                            <div class="card">
                                    <div class="header-container">
                                        <asp:Image ID="Image1" runat="server" />
                                        <asp:Label ID="Label2" runat="server" Text="Label">Toko Buku Sejahtera</asp:Label>
                                        <label for="loginform">Register</label>
                                    </div>
                                    <div class="input-container">
                                        <asp:TextBox ID="TbNewUser" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label for="TbUsername">Username</label>
                                    </div>
                                    <div class="input-container">
                                        <asp:TextBox ID="TbNewPassword" runat="server" CssClass="floating-input" placeholder=" " TextMode="Password"></asp:TextBox>
                                        <label for="TbPassword">Password</label>
                                    </div>
                                    <asp:Button ID="BtnRegister" runat="server" Text="Daftar" CssClass="button" />
                                </div>
                            <p>Sudah memiliki akun? <span><asp:LinkButton ID="LkLogin" runat="server" CssClass="link">Login Disini</asp:LinkButton></span></p>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </form>
</body>
</html>
