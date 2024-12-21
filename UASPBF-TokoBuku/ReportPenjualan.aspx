<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Manager.Master" CodeBehind="ReportPenjualan.aspx.vb" Inherits="UASPBF_TokoBuku.ReportPenjualan" %>
<asp:Content ID="Report" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Report History</h2>
    <div class ="report-form">
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="id_pendapatan" HeaderText="ID" />
                <asp:BoundField DataField="id_admin" HeaderText="Nama Sales" />
                <asp:BoundField DataField="id_pelanggan" HeaderText="Nama Pelanggan" />
                <asp:BoundField DataField="id_buku" HeaderText="Nama Buku" />
                <asp:BoundField DataField="id_transaksi" HeaderText ="Jenis Pembayaran" />
                <asp:BoundField DataField ="total_pendapatan" HeaderText="Total Pendapatan" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
