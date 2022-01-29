<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserAuth.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.UserAuth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <th>Account</th>
            <td>
                <asp:Literal runat="server" ID="ltAccount"></asp:Literal></td>
        </tr>

        <tr>
            <th>Add Roles</th>
            <td>
                <asp:CheckBoxList ID="ckbRoleList" runat="server" DataValueField="ID" DataTextField="RoleName"></asp:CheckBoxList>
                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
            </td>
        </tr>

        <tr>
            <th>Roles</th>
            <td>
                <asp:Repeater ID="rptRoleList" runat="server" OnItemCommand="rptRoleList_ItemCommand">
                    <ItemTemplate>
                        <div>
                            <%# Eval("RoleName") %>
                            <asp:Button runat="server" CommandName="DeleteRole" CommandArgument='<%# Eval("ID") %>' Text="Remove" />
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
</asp:Content>
