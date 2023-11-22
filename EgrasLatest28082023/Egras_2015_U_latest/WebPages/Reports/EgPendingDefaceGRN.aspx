<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgPendingDefaceGRN.aspx.cs" Inherits="WebPages_Reports_EgPendingDefaceGRN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <fieldset runat="server" id="fieldamount" style="width: 950px; margin-left: 100px">
        <legend style="color: #336699; font-weight: bold">Pending GRN for Deface</legend>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">         
            <tr>
                <td style="height: 45px" align="center">
                    <asp:GridView ID="grdDeface" runat="server" AutoGenerateColumns="False" BorderColor="#336699"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Verdana" Font-Size="10pt"
                        ShowFooter="true" FooterStyle-BackColor="#336699" Width="80%" EmptyDataText="No Record Found"
                        AllowPaging="true" PageSize="30" EditRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699"
                        EmptyDataRowStyle-VerticalAlign="Middle" OnPageIndexChanging="grdDeface_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="GRN" DataField="GRN">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Amount" DataField="Amount">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:BoundField> 
                        </Columns>
                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#336699" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

