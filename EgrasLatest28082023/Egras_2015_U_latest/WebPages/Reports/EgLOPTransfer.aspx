<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgLOPTransfer.aspx.cs" Inherits="WebPages_Reports_EgLOPTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/Control.js" type="text/javascript"></script>

    <table width="90%" style="text-align: center" align="center" border="1" cellpadding="0"
        cellspacing="0">
        <tr>
            <td colspan="4" style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="LOP Data Shifting Process" Font-Bold="True" ForeColor="#009900"
                    Style="text-decoration: underline;"></asp:Label>
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
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    ValidationGroup="a" TabIndex="3" />
            </td>
        </tr>
    </table>
</asp:Content>
