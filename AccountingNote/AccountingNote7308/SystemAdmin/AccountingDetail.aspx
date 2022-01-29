<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AccountingDetail.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.AccountingDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>類別:
                    <asp:DropDownList ID="ddIActType" runat="server">
                        <asp:ListItem Value="0">支出</asp:ListItem>
                        <asp:ListItem Value="1">收入</asp:ListItem>
                    </asp:DropDownList>
                <br />
                金額:
                    <asp:TextBox ID="txtAmount" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                標題:
                    <asp:TextBox ID="txtCaption" runat="server"></asp:TextBox><br />
                說明:
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                上傳:
                    <asp:FileUpload ID="fileCover" runat="server" /><br />
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
                <asp:Literal ID="ltMsg" runat="server"></asp:Literal><br />
            </td>
        </tr>
    </table>
</asp:Content>
