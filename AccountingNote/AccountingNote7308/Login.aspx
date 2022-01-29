<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AccountingNote7308.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>登入</h2>
    <asp:PlaceHolder ID="plcLogin" runat="server">帳號:
        <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
        <br />
        密碼:
        <asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox>
        <div style="margin-left: 150px">
            <a href="ForgotPassword.aspx">忘記密碼?</a>
        </div>
        <br />
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" />
    </asp:PlaceHolder>
</asp:Content>
