<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgBankScroll45A.aspx.cs" Inherits="WebPages_Reports_EgBankScroll45A"
    Title="Untitled Page" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<script runat="server">


</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>
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
                if ($(<%=txtfromdate.ClientID%>).val() != "" && $(<%=txttodate.ClientID%>).val() != ""&& $(<%=ddlbankname.ClientID%>).val() != "0") {
                $('#cover-spin').show(0)

            }
        });
    });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Bank Scroll Report (45-A)</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Bank Scroll Report (45-A)" />
    </div>
    <table style="width: 100%" border="1" align="center">
        <tr>
            <td>
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                <asp:TextBox ID="txtfromdate" Height="100%" Style="display: initial !important" CssClass="form-control" runat="server" Width="120px" TabIndex="1"
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
                <b><span style="color: #336699">To Date : </span></b>&nbsp;
                <asp:TextBox ID="txttodate" Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px" runat="server" 
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
                <b><span style="color: #336699">Select Bank : </span></b>&nbsp;
                <asp:DropDownList ID="ddlbankname" runat="server" Width="100%" CssClass="form-control chzn-select ">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </td>


        </tr>
        <tr align="right">
            <td>
                <asp:RadioButtonList runat="server" Style="width: 70% !important; display: contents !important" CssClass="form-control" ID="Online_ManualRadioButton" Width="130px" RepeatDirection="Horizontal" ForeColor="#336699">
                    <asp:ListItem Text="Online" Value="N" Selected="True" style="margin-right: 35px" />
                    <asp:ListItem Text="Manual" Value="M" style="margin-right: 35px" />
                </asp:RadioButtonList>
                <%--<uc1:Online_ManualRadioButton runat="server" ID="Online_ManualRadioButton" />--%>
            </td>
            <td colspan="3" align="right">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Button ID="btnshow" runat="server" Height="33" CssClass="btn btn-default pull-right" Text="Show" ValidationGroup="a" OnClick="btnshow_Click" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnPrint" runat="server" Height="33" CssClass="btn btn-default pull-right" Text="PDF" ValidationGroup="a" OnClick="btnPrint_Click" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnReset" runat="server" Height="33" CssClass="btn btn-default pull-left" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />
                    </div>
                </div>
            </td>

        </tr>
        <tr runat="server" id="trrpt" visible="false">
            <td colspan="5">
                <center>
                    <rsweb:ReportViewer ID="rpt45ASSRS" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
    </table>
</asp:Content>
