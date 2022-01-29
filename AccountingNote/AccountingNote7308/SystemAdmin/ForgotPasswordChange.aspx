<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="ForgotPasswordChange.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.ForgotPasswordChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="font-size: 40px">忘記密碼</div>
    <br />
    <table>
        <tr>
            <td>
                帳號:
                <asp:Literal ID="ltlAct" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                認證碼:
                <asp:TextBox ID="txbAttest" runat="server"></asp:TextBox>
                <asp:Literal ID="ltlMsg1" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                新密碼:
                <asp:TextBox ID="txbNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                確認新密碼:
                <asp:TextBox ID="txbNewPasswordCmf" runat="server" TextMode="Password"></asp:TextBox>
                <asp:Literal ID="ltlMsg2" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
      <asp:Literal ID="ltlCheckInput" runat="server"></asp:Literal>
    <br />
    <div>
        <span>
            <asp:Button ID="btnSave" runat="server" Text="確定變更" OnClick="btnSave_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click"/>
        </span>
    </div>
</asp:Content>
