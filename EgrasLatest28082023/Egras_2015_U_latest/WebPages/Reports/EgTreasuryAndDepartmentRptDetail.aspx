<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgTreasuryAndDepartmentRptDetail.aspx.cs" Inherits="WebPages_Reports_EgTreasuryAndDepartmentRptDetail" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <fieldset runat="server" id="fieldamount" style="width: 1000px; margin-left: 0px">
            <legend style="color: #008080; font-weight: bold">Master-Report</legend>
            <table id="Table1" border="0" cellpadding="0" cellspacing="0" align="center">
                <tr align="center">
                    <td>&nbsp;</td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button Text="Download PDF" ID="btnDownloadPDF" OnClick="btnDownloadPDF_Click"
                             runat="server" /></td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblTotal" Font-Bold="true" ForeColor="#006600" runat="server" Text=""></asp:Label></td>

                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdBudgetHead" runat="server" AutoGenerateColumns="False" BorderColor="#336699"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Verdana" Font-Size="10pt"
                            ShowFooter="true" FooterStyle-BackColor="#336699" Width="100%" EmptyDataText="No Record Found"
                            EditRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                            OnPageIndexChanging="grdBudgetHead_PageIndexChanging" AllowPaging="true" PageSize="20"
                            OnRowDataBound="grdBudgetHead_RowDataBound" OnRowCommand="grdBudgetHead_RowCommand"
                            AllowSorting="true" OnSorting="grdBudgetHead_Sorting" EnableSortingAndPagingCallback="True">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRN" SortExpression="GRN" ControlStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkgrn" CommandName="grnbind" Text='<%#Eval("GRN")%>' CommandArgument='<%#Eval("GRN")%>'
                                            runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="RemitterName" DataField="RemitterName" SortExpression="RemitterName"
                                    ControlStyle-Font-Bold="true">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Deposit Date" DataField="DepositDate">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Bank Name" DataField="BankName">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataFormatString="{0:n}" HeaderText="Amount" DataField="Amount">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
    </div>
    <rsweb:ReportViewer ID="rptManualSuccessDivisionWiserpt" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false"></rsweb:ReportViewer>
</asp:Content>

