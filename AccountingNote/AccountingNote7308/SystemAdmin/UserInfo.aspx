<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>使用者資訊</h2>
    <table>
        <tr>
            <th>帳號:</th>
            <td>
                <asp:Literal ID="ltAccount" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>姓名:</th>
            <td>
                <asp:Literal ID="ltName" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>Email:</th>
            <td>
                <asp:Literal ID="ltEmail" runat="server"></asp:Literal></td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnLogout" runat="server" Text="登出" OnClick="btnLogout_Click"/>
</asp:Content>
