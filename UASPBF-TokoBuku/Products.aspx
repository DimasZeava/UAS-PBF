<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Admin.Master" CodeBehind="Products.aspx.vb" Inherits="UASPBF_TokoBuku.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Products</h2>
    <div class="product-form">
        <asp:TextBox ID="TbNamaBuku" runat="server" Placeholder="Nama Buku"></asp:TextBox>
        <asp:TextBox ID="TbKategori" runat="server" Placeholder="Nama Kategori"></asp:TextBox>
        <asp:TextBox ID="TbPenulis" runat="server" Placeholder="Nama Penulis"></asp:TextBox>
        <asp:TextBox ID="TbHarga" runat="server" Placeholder="Harga Buku"></asp:TextBox>
        <asp:TextBox ID="TbStok" runat="server" Placeholder="0"></asp:TextBox>
        <asp:Button ID="btnAddBuku" runat="server" Text="Tambah Buku" />
        <asp:Button ID="btnUpdateBuku" runat="server" Text="Update" Visible="false" />
        <asp:HiddenField ID="hfIdBuku" runat="server" />
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
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="CustomEdit" CommandArgument='<%# Eval("id_buku") %>'>Edit</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="CustomDelete" CommandArgument='<%# Eval("id_buku") %>'>Hapus</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
