<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.UserList" %>

<%@ Register Src="~/UserControl/ucPager_UserList.ascx" TagPrefix="uc1" TagName="ucPager_UserList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>會員管理</h2>
    <table>
        <tr>
            <td>
                <asp:Button ID="addUserbtn" runat="server" Text="新增使用者" OnClick="addUserbtn_Click" /><br />
                <asp:GridView ID="GV_UserList" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="GV_UserList_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="12px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="帳號" DataField="Account" />
                        <asp:BoundField HeaderText="名稱" DataField="Name" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />
                        <asp:TemplateField HeaderText="等級">
                            <ItemTemplate>
                                <asp:Literal ID="ltlLevel" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="建立時間" DataField="CreateDate" />
                        <asp:TemplateField HeaderText="編輯">
                            <ItemTemplate>
                                <a href="UserDetail.aspx?UID=<%# Eval("ID") %>">編輯</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
                <asp:PlaceHolder ID="plc_noUser" runat="server" Visible="False">No User.</asp:PlaceHolder>
                <uc1:ucPager_UserList runat="server" ID="ucPager_UserList" Url="/SystemAdmin/UserList.aspx" PageSize="10"/>
            </td>
        </tr>
    </table>
</asp:Content>
