<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="AccountingNote7308.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div style="font-size: 40px">忘記密碼</div>
    <br />
    <table>
        <tr>
            <td>
                帳號:
                <asp:TextBox ID="txbAccount" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                信箱:
                <asp:TextBox ID="txbEmail" runat="server" TextMode="Email"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <br />
    <div>
        <span>
            <asp:Button ID="btnSave" runat="server" Text="送出" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" />
        </span>
    </div>
</asp:Content>
