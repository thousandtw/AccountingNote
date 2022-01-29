<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.AccountingList" %>

<%@ Register Src="~/UserControl/ucPager_AccountingList.ascx" TagPrefix="uc1" TagName="ucPager_AccountingList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>流水帳管理</h2>
    <table>
        <tr>
            <td>
                <tb>
                    <asp:Button ID="btnCreate" runat="server" Text="新增項目" OnClick="btnCreate_Click" />  <a href="Permissions.aspx" id="admin">權限管理</a>
                </tb>
                <span style="margin-left: 170px">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </span>
                <tb>
                    <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAccountingList_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="標題" DataField="Caption" />
                            <asp:BoundField HeaderText="金額" DataField="Amount" />
                            <asp:TemplateField HeaderText="收入/支出">
                                <ItemTemplate>
                                    <asp:Label ID="lblActType" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:TemplateField HeaderText="編輯">
                                <ItemTemplate>
                                   <%-- <asp:Image runat="server" ID="imgCover" Width="80" Height="50" Visible='<%#  Eval("CoverInage")!=null %>'
                                                                                                   ImageUrl='<%# "/FileDownload/Accounting/"+ Eval("CoverInage") %>' />                --%>
                                    <asp:Image runat="server" ID="imgCover" Width="80" Height="50" Visible="false" />
                                    <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">編輯</a>
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

                    <asp:Literal runat="server" ID="ltPager">
                    </asp:Literal>

                    <uc1:ucPager_AccountingList runat="server" id="ucPager_AccountingList" CurrentPage="1" TotalSize="10" Url="AccountingList.aspx" PageSize="1"  />

                    <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                        <p style="color: red">
                            此帳號沒有任何流水帳紀錄
                        </p>
                    </asp:PlaceHolder>
            </td>
        </tr>
    </table>
    <!--使用者登入時,隱藏管理者頁面-->
     <script>
        var admin = document.getElementById('admin');
        if (1 ==<%=currentUser.UserLevel%>) {
            admin.style.display = 'none';
        }
     </script>
</asp:Content>
