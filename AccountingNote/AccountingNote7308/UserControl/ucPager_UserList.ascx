<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager_UserList.ascx.cs" Inherits="AccountingNote7308.UserControl.ucPager" %>

<div>
    <a runat="server" id="aFirst" href="#">First</a>

    <a runat="server" id="a1" href="#">1</a>
    <a runat="server" id="a2" href="#">2</a>
    <a runat="server" id="a3" href="#">3</a>
    <a runat="server" id="a4" href="#">4</a>
    <a runat="server" id="a5" href="#">5</a>

    <a runat="server" id="aLast" href="#">Last</a>
    <br />
    <asp:Literal ID="Pager_ltl" runat="server"></asp:Literal>
</div>
