﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgSNAReport.aspx.cs" Inherits="WebPages.Reports.WebPages_EgSNAReport" Title="Untitled Page" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js"></script>
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
    </style>

    <div id="cover-spin"></div>
    <script type="text/javascript">

        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnshow"]').click(function () {
                if ($(<%=txtfromdate.ClientID%>).val() != "") {
                    $('#cover-spin').show(0)

                }
            });
        });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">SNA Report Data </span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Ty34" />
    </div>
    <table align="center" style="width: 100%" border="1">
        <tr>
            <td  style="text-align: center; height: 35px; padding-left: 250px" valign="top">
                <asp:RadioButtonList runat="server" Style="color: #336699; text-align: center" ID="rbtnList" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" style="margin-right: 30px;" Text="SNA Department Data " Value="0" />
                    <asp:ListItem Text="SNA Bank Data " Value="1" style="margin-right: 30px;" />
                    <asp:ListItem Text="SNA Generated GRN " Value="2" style="margin-right: 30px;" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td  align="left">
                <b><span style="color: #336699">Date : </span></b>

                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" Height="100%" Width="120px" Style="margin-top: 0px; display: inherit; margin-left: 8px"
                    onpaste="return false" onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtfromdate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                    ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnshow" runat="server" Text="Show" Height="33" CssClass="btn btn-default" ValidationGroup="de" Style="margin-right: 20px" OnClick="btnshow_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" Height="33" CssClass="btn btn-default" ValidationGroup="de" OnClick="btnReset_Click" />
            </td>
        </tr>
        <tr runat="server" id="trrpt" visible="false">
            <td colspan="7">
                <center>
                    <rsweb:ReportViewer ID="rptEgSNADataSSRS" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                    <rsweb:ReportViewer ID="rptEgSNABankDataSSRS" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                    <rsweb:ReportViewer ID="rptEgSNAGeneratedGRNSSRS" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
            PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
        </ajaxToolkit:CalendarExtender>
    </table>

</asp:Content>

