<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgDeletedScrollRpt.aspx.cs" Inherits="WebPages_TO_EgDeletedScrollRpt"
    Title="Untitled Page" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/Control.js" type="text/javascript"></script>

    <table width="100%" style="text-align: center" align="center" border="1" cellpadding="0"
        cellspacing="0">
        <tr>
            <td colspan="3" style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="Deleted Scroll Report" Font-Bold="True"
                    ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
            </td>
        </tr>
        <tr style="height: 45px">
            <td>
                <b><span style="color: #336699">Bank:-</span></b>&nbsp;
                <asp:DropDownList ID="ddlbankname" runat="server" Width="180px" CssClass="borderRadius inputDesign">
                </asp:DropDownList>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ddlbankname"
                    Text="Special character not allowed.!" CssClass="XMMessage" Display="Dynamic"
                    ErrorMessage="Special character not allowed in Bank Name." ValidationExpression="^([a-zA-Z0-9_., \s\-]*)$"
                    ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
            <td align="center">
                <b><span style="color: #336699">Bank Date:-</span></b>&nbsp;
                <asp:TextBox ID="txtFromDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                    onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"
                    Width="80px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                    Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                </ajaxToolkit:CalendarExtender>
            </td>
            <td align="left">
                <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <rsweb:ReportViewer ID="rptDeletedScroll" runat="server" Width="100%" SizeToReportContent="true"
                    AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
