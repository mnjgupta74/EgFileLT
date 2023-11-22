<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgDMFTGRNSummary.aspx.cs" Inherits="WebPages_Reports_EgDMFTGRNSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../../js/Control.js" type="text/javascript"></script>
    <fieldset style="width: 100%" id="FieldTreasury">
        <legend style="color: #336699;">DMFTGrn Summary</legend>
        <table width="100%" style="text-align: center" align="center" border="1" cellpadding="0"
            cellspacing="0">
            <tr>
                <td colspan="3" style="text-align: center; height: 35px" valign="top">
                    <asp:Label ID="Labelheader" runat="server" Text="DMFT Grn Summary Report" Font-Bold="True"
                        ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">From Date:-</span></b>&nbsp;
                <asp:TextBox ID="txtfromdate" runat="server" Width="100px" TabIndex="1" onkeypress="Javascript:return NumberOnly(event)"
                    onpaste="return false" onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                        ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">To Date:-</span></b>&nbsp;
                <asp:TextBox ID="txttodate" runat="server" Width="100px" TabIndex="2" onkeypress="Javascript:return NumberOnly(event)"
                    onpaste="return false" onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="txttodate" TargetControlID="txttodate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txttodate"
                        ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        ValidationGroup="a" TabIndex="3" />
                </td>
            </tr>
            <tr>
                <td colspan="3" height="20px;"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial; width: 100%">
                        <tr id="trDmft" runat="server" visible="false">
                            <td>
                                <asp:Repeater ID="RptDMFTGrnSummary" runat="server">
                                    <HeaderTemplate>
                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center; height: 20px">
                                            <td style="color: White; width: 60px;">Sr.No
                                            </td>
                                            <td style="color: White; text-align: left;">Challan Date
                                            </td>
                                            <td style="color: White; text-align: left;">Treasury
                                            </td>
                                            <td style="color: White; text-align: left;">GRN Count
                                            </td>
                                            <td style="color: White; text-align: left;">Amount
                                            </td>

                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: #EFF3FB; height: 20px;">
                                            <td align="center" style="font-size: 15;">
                                                <%# Container.ItemIndex+1 %>
                                            </td>
                                            <td style="font-size: 15; text-align: left;">
                                                <asp:Label ID="lblChallanDate" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("ChallanDate"))%>'></asp:Label>
                                            </td>
                                            <td style="font-size: 15; text-align: left;">
                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("TreasuryName") %>'></asp:Label>
                                            </td>
                                            <td style="font-size: 15; text-align: left;">
                                                <asp:Label ID="lblGRNCount" runat="server" Text='<%# Eval("GRNCount")%>'></asp:Label>
                                            </td>
                                                   <td align="center" style="font-size: 15; text-align: right;">
                                                <%# string.Format("{0:0.00}", Eval("Amount"))%>&nbsp; &nbsp;
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                       <tr style="background-color: #EFF3FB; height: 20px;">
                                            <td align="center" style="font-size: 15;">
                                                <%# Container.ItemIndex+1 %>
                                            </td>
                                            <td style="font-size: 15; text-align: left;">
                                                <asp:Label ID="lblChallanDate" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("ChallanDate"))%>'></asp:Label>
                                            </td>
                                            <td style="font-size: 15; text-align: left;">
                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("TreasuryName") %>'></asp:Label>
                                            </td>
                                            <td style="font-size: 15; text-align: left;">
                                                <asp:Label ID="lblGRNCount" runat="server" Text='<%# Eval("GRNCount")%>'></asp:Label>
                                            </td>
                                                   <td align="center" style="font-size: 15; text-align: right;">
                                                <%# string.Format("{0:0.00}", Eval("Amount"))%>&nbsp; &nbsp;
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>