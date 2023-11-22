<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage3.master" AutoEventWireup="true" CodeFile="EPayBankForward.aspx.cs" Inherits="WebPages_EPayBankForward" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table width="70%" align="center">
        <tr align="center">
            <td>
                <asp:GridView ID="grdVerifyChallan" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                    Width="100%" ShowFooter="True" FooterStyle-BackColor="#336699" EditRowStyle-Font-Bold="true"
                    EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                    OnRowCommand="grdVerifyChallan_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GRN" HeaderText="GRN">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BankCode" HeaderText="BankCode">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonStatus" runat="server" CausesValidation="false" CommandName="Verify"
                                    CommandArgument='<%#Container.DataItemIndex+1 %>' Text="Forward To Bank"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#336699" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

