<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccountingNote7308.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <th>初次記帳</th>
            <td>
                <asp:Literal ID="firstdatalb" runat="server"></asp:Literal>
            </td>
        </tr>

        <tr>
            <th>最後記帳</th>
            <td>
                <asp:Literal ID="enddatalb" runat="server"></asp:Literal>
            </td>
        </tr>

        <tr>
            <th>記帳數量</th>

            <td>
                <asp:Literal ID="datacountlb" runat="server"></asp:Literal>
            </td>
        </tr>

        <tr>
            <th>會員數</th>
            <td>
                <asp:Literal ID="userlb" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
