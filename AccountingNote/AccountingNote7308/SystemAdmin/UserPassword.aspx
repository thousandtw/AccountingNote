<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.UserPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>會員管理</h2>
    <table>
        <tr>
            <th>帳號:</th>
            <td>
                <asp:Literal ID="ltAccount" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>原密碼:</th>
            <td>
                <asp:TextBox ID="txbPWD" runat="server" TextMode="Password" MaxLength="16"></asp:TextBox>
                <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>新密碼:</th>
            <td>
                <asp:TextBox ID="txbNewPWD" runat="server" TextMode="Password" MaxLength="16"></asp:TextBox>
                <asp:Literal ID="ltlMsg2" runat="server"></asp:Literal>
            </td>
        </tr>
        <br />
        <tr>
            <th>確認新密碼:</th>
            <td>
                <asp:TextBox ID="txbNewPWD_Check" runat="server" TextMode="Password" MaxLength="16"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <asp:Literal ID="ltlCheckInput" runat="server"></asp:Literal>
    <br />
    <asp:Button ID="btnChange" runat="server" Text="變更密碼" OnClick="btnChange_Click" />
</asp:Content>
