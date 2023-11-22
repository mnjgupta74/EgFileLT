<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgManualBankService.aspx.cs" Inherits="WebPages_Admin_EgManualBankService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="70%" align="center">
        <tr>
            <td align="center">
                <b>Select Bank: &nbsp;&nbsp;&nbsp;&nbsp; </b>
                <asp:DropDownList ID="ddlbankname" runat="server" Width="180px" CssClass="borderRadius inputDesign">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
            </td>
        </tr>
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
                                    CommandArgument='<%#Container.DataItemIndex+1 %>' Text="Verify with Bank"></asp:LinkButton>
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
