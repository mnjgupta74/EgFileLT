<%@ Page Title="Egras.Raj.Nic.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgDepartmentTotalAmountRpt.aspx.cs" Inherits="WebPages_Reports_EgDepartmentTotalAmountRpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server">


</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <%--==================CSS-JQUERY LOADER==================--%>
    <style type="text/css">
        #cover-spin {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            display: none;
        }

        @-webkit-keyframes spin {
            from {
                -webkit-transform: rotate(0deg);
            }

            to {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        #cover-spin::after {
            content: '';
            display: block;
            position: absolute;
            left: 48%;
            top: 40%;
            width: 40px;
            height: 40px;
            border-style: solid;
            border-color: black;
            border-top-color: transparent;
            border-width: 4px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

        .btn-default.disabled, .btn-default[disabled], fieldset[disabled] .btn-default, .btn-default.disabled:hover, .btn-default[disabled]:hover, fieldset[disabled] .btn-default:hover, .btn-default.disabled:focus, .btn-default[disabled]:focus, fieldset[disabled] .btn-default:focus, .btn-default.disabled:active, .btn-default[disabled]:active, fieldset[disabled] .btn-default:active, .btn-default.disabled.active, .btn-default[disabled].active, fieldset[disabled] .btn-default.active {
                background-color: #abaaaa;
                border-color: #ccc;
            }
    </style>

    <div id="cover-spin"></div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnshow"]').click(function () {
                if ($(<%=txtFromDate.ClientID%>).val() != "" && $(<%=txtFromDate.ClientID%>).val() != "") {
                    $('#cover-spin').show(0)
                }
            });
        });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>


    <%--<fieldset runat="server" id="lstrecord" style="width: 900px; margin-left: 100px">
        <legend style="color: #336699; font-weight: bold">Department Revenue</legend>--%>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Department Revenue</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Bank Scroll Report (45-A)" />
    </div>

    <table style="width: 100%" border="1" align="center" id="MainTable">
        <tr>
            <td align="left">
                <b><span style="color: #336699">From Date :</span></b>&nbsp;
                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                        Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px"
                        onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                    Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                </ajaxToolkit:CalendarExtender>
            </td>
            <td align="left">
                <b><span style="color: #336699">To Date : </span></b>&nbsp;
                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                        Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px"
                        onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                    Format="dd/MM/yyyy" TargetControlID="txtToDate">
                </ajaxToolkit:CalendarExtender>
            </td>
            <td align="center">
                <asp:RadioButtonList ID="rblOrderType" Style="width: 70% !important; display: contents !important" CssClass="form-control"
                    Width="130px" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Ascending" style="margin-right: 9px" Value="asc" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Descending" style="margin-left: 9px" Value="desc"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:RadioButtonList ID="RadioButtonList1" Style="width: 70% !important; display: contents !important" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" style="margin-right: 9px" Selected="True">Simple</asp:ListItem>
                    <asp:ListItem Value="2" style="margin-left: 9px">PDF</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnshow" runat="server" Height="33" CssClass="btn btn-default" Text="Show" Style="margin-right: 9px" ValidationGroup="de" OnClick="btnshow_Click" />
                <asp:Button ID="btnReset" runat="server" Height="33" CssClass="btn btn-default" Text="Reset" Style="margin-left: 9px" ValidationGroup="de" OnClick="btnReset_Click" />
            </td>
        </tr>

        <tr align="center">
            <td colspan="5" style="text-align: left" align="center">
                <center>
                    <rsweb:ReportViewer ID="rptDepartmentTotalAmount" runat="server" Width="100%" SizeToReportContent="true" Visible="false"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
    </table>
    <%--</fieldset>--%>
</asp:Content>
