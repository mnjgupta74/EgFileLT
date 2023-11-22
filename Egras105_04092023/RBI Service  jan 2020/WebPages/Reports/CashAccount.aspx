<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="CashAccount.aspx.cs" Inherits="CashAccount" Title="LOR" %>
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
        .btn-default{
            color: #f4f4f4;
    background-color: #428bca;
        }
         .btn-default.disabled, .btn-default[disabled], fieldset[disabled] .btn-default, .btn-default.disabled:hover, .btn-default[disabled]:hover, fieldset[disabled] .btn-default:hover, .btn-default.disabled:focus, .btn-default[disabled]:focus, fieldset[disabled] .btn-default:focus, .btn-default.disabled:active, .btn-default[disabled]:active, fieldset[disabled] .btn-default:active, .btn-default.disabled.active, .btn-default[disabled].active, fieldset[disabled] .btn-default.active {
                background-color: #abaaaa;
                border-color: #ccc;
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
    </style>

    <div id="cover-spin"></div>
<script type="text/javascript">

    $(document).ready(function () 
    {
        $('[name*="ctl00$ContentPlaceHolder1$btnSubmit"]').click(function ()
        {
            if ($(<%=txtfromdate.ClientID%>).val()!= "" && $(<%=txttodate.ClientID%>).val()!= "")
            {
                $('#cover-spin').show(0)

            }
        });
    });
</script>
    <%--==============END CSS - JQUERY LOADER============--%>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail"class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Cash Account</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Cash Account" />
    </div>
    <table width="100%" style="text-align: center" align="center" border="1">
        <tr>
            <td>
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                <asp:TextBox ID="txtfromdate" CssClass="form-control"  Style="display:inline-block !important;height: 100% !important" runat="server" Width="120" TabIndex="1"
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
                <asp:TextBox ID="txttodate" Style="display:inline-block !important;height: 100% !important" CssClass="form-control" runat="server" Width="120" TabIndex="2"
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
            </tr><tr>
                <td style="height: 35px" valign="top" align="center">
                <asp:RadioButtonList runat="server" ID="rbtnList" CssClass="form-control" style="margin-left:25%;display:contents !important" Width="50%" RepeatDirection="Horizontal"  ForeColor="#336699">
                    <asp:ListItem Text="LOR" Value="LOR" Selected="True" style="margin-right: 35px;"/>
                    <asp:ListItem Text="LOR Detail" Value="LORDetail" />
                </asp:RadioButtonList>
            </td>
            <td>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="btnSubmit" Height="33" runat="server" CssClass="btn btn-default pull-right" Text="Show" OnClick="btnSubmit_Click"
                    ValidationGroup="a" TabIndex="3" />
                    </div>
                    <div class="col-md-3">
                         <asp:Button ID="btnPrint" Height="33"  CssClass="btn btn-default pull-right" runat="server" Text="PDF" ValidationGroup="a" OnClick="btnPrint_Click" />
                          
                    </div>
                    <div class="col-md-3" >
                     <asp:Button ID="btnSignPdf" Height="33"  CssClass="btn btn-default pull-right" runat="server" Text="SignPDF" ValidationGroup="a" OnClick="btnSignPdf_Click" />
                    </div>
                    <div class="col-md-3">
                <asp:Button ID="btnReset" Height="33" runat="server" CssClass="btn btn-default pull-left" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />
                    </div>
                </div>
                
            </td>
        </tr>
        <tr runat="server" id="trrpt" visible="false">
            <td colspan="3">
                <center>
                    <rsweb:ReportViewer ID="rptLORSSRS" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
    </table>
</asp:Content>
