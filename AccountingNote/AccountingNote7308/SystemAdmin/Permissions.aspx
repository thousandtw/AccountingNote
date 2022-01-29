<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="Permissions.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.Permissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Account" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="UserAuth.aspx?ID=<%# Eval("ID") %>">修改權限</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
