<%@ Page Title="Egras.Raj.Nic.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgDepartmentwiseGRNList.aspx.cs" Inherits="WebPages_Reports_EgDepartmentwiseGRNList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Department-Revenue" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Department-Revenue Details</span></h2>
               <img src="../../Image/help1.png" class="pull-right" style="height: 35px;width: 34px;" data-toggle="tooltip" data-placement="left" Title="Department-Revenue Details" />
            </div>
    <%--<fieldset runat="server" id="lstrecord" style="width: 1000px; margin-left: 50px">
        <legend style="color: #336699; font-weight: bold">Department-Revenue Details</legend>--%>
        <table style="width: 100%" align="center" id="MainTable">
            <tr style="height: 45px">
                <td align="center">
                    <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"
                        Width="80px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"
                        Width="80px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                        Format="dd/MM/yyyy" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <b><span style="color: #336699">Department:-</span></b>&nbsp;
                    <asp:DropDownList ID="ddldepartment" runat="server" Width="200px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqdepartment" runat="server" ValidationGroup="de"
                        ErrorMessage="*" SetFocusOnError="true" ControlToValidate="ddldepartment" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">Simple</asp:ListItem>
                        <asp:ListItem Value="2">PDF</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="left">
                    <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                </td>
            </tr>
            <tr runat="server" id="trrpt" visible="false">
                <td colspan="5">
                    <center>
                        <rsweb:ReportViewer ID="rptDepartmentWiseGRNList" runat="server" Width="100%" SizeToReportContent="true"
                            AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                        </rsweb:ReportViewer>
                    </center>
                </td>
            </tr>
        </table>
    <%--</fieldset>--%>
</asp:Content>
