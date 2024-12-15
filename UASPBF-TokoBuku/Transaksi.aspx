<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pengguna.Master" CodeBehind="Transaksi.aspx.vb" Inherits="UASPBF_TokoBuku.Transaksi" %>
<asp:Content ID="Transaksi" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Pelanggan</h2>
    <div class="pelanggan-form">
        <asp:TextBox ID="TbNamaPelanggan" runat="server" Placeholder="Nama Pelanggan"></asp:TextBox>
        <asp:DropDownList ID="DDLTipePelanggan" runat="server" CssClass="dropdown-pelanggan">
            <asp:ListItem>Individu</asp:ListItem>
            <asp:ListItem>Perusahaan</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TbAlamatPelanggan" runat="server" Placeholder="Alamat Pelanggan"></asp:TextBox>
    </div>

    <h3>Product List</h3>
    <asp:GridView ID="gvInventoryBuku" runat="server" AutoGenerateColumns="False" OnRowCommand="gvInventoryBuku_RowCommand">
        <Columns>
            <asp:BoundField DataField="id_buku" HeaderText="ID" />
            <asp:BoundField DataField="nama_buku" HeaderText="Nama Buku" />
            <asp:BoundField DataField="kategori_buku" HeaderText="Kategori" />
            <asp:BoundField DataField="penulis_buku" HeaderText="Penulis" />
            <asp:BoundField DataField="harga_buku" HeaderText="Harga" />
            <asp:BoundField DataField="stok" HeaderText="Stok" />
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkTambah" runat="server" CommandName="CustomAdd" CommandArgument='<%# Eval("id_buku") %>'>Tambah</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div class="transaksi-content">
        <div class="transaksi-form">
            <asp:HiddenField ID="hfIdTransaksi" runat="server" />
            <asp:DropDownList ID="DDLJenisPembayaran" runat="server" CssClass="dropdown-pelanggan">
                <asp:ListItem>Cash</asp:ListItem>
                <asp:ListItem>E-Wallet</asp:ListItem>
                <asp:ListItem>QRIS</asp:ListItem>
                <asp:ListItem>Kartu Kredit</asp:ListItem>
                <asp:ListItem>M-Banking</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="TbPembayaran" runat="server" Placeholder="Total yang dibayar"></asp:TextBox>
        </div>
        <div class="keranjang">
            <asp:GridView ID="gvKeranjang" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="id_buku" HeaderText="ID" />
                    <asp:BoundField DataField="nama_buku" HeaderText="Nama Buku" />
                    <asp:BoundField DataField="harga_buku" HeaderText="Harga" />
                    <asp:BoundField DataField="jumlah" HeaderText="Jumlah" />
                    <asp:BoundField DataField="total_harga" HeaderText="Total" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="footer">
        <div class="total-pembayaran">
            <div class="fixed-container">
                <h3>Total Transaksi</h3>
                <asp:TextBox ID="TbTotalPembayaran" runat="server"></asp:TextBox>
            </div>
                <div class="fixed-container">
                <p>Kembalian</p>
            <asp:TextBox ID="TbKembalian" runat="server"></asp:TextBox>
            </div>
        </div>
        <asp:Button ID="btnBayar" runat="server" Text="Bayar" />
    </div>
</asp:Content>
