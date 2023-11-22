<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="Frm45a.aspx.cs" Inherits="WebPages_Reports_Frm45a" Title="Untitled Page" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>
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
    </style>

    <div id="cover-spin"></div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnprint"]').click(function () {
                if ($(<%=txtfromdate.ClientID%>).val() != "" && $(<%=txttodate.ClientID%>).val() != "") {
                    $('#cover-spin').show(0)
                    setTimeout(function () { $('#cover-spin').hide() }, 2500);
                }
            });
        });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>

    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">45-A Report (HeadWise)</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="45-A Report (HeadWise)" />
    </div>
    <table width="100%" style="text-align: center" align="center" border="1">
        <tr>
            <td>
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                <asp:TextBox ID="txtfromdate" runat="server" Height="100%" Width="120px" Style="display: initial !important" CssClass="form-control"
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
                <asp:TextBox ID="txttodate" runat="server" Height="100%" Width="120px" Style="display: initial !important" CssClass="form-control"
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
                <b><span style="color: #336699">Enter Budget Head :</span></b>&nbsp;
                <asp:TextBox ID="txtdetaiedheadsearch" TabIndex="2" Height="100%" Style="display: initial !important" CssClass="form-control" runat="server" Width="200px"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" Mask="9999-99-999-99-99"
                    MaskType="None" CultureName="en-US" TargetControlID="txtdetaiedheadsearch" AcceptNegative="None"
                    runat="server">
                </ajaxToolkit:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:RadioButtonList Style="color: #336699; font-size: 15px" ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" style="margin-right: 35px" Selected="True">&nbsp;Simple</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;PDF</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="center">
                <asp:Button ID="btnprint" runat="server" Text="Show" Height="33" CssClass="btn btn-default" OnClick="btnprint_Click"
                    ValidationGroup="a" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" Height="33" CssClass="btn btn-default" Style="margin-left: 20px"
                    ValidationGroup="a" OnClick="btnReset_Click" />
            </td>
        </tr>
        <tr runat="server" id="trrpt" visible="false">
            <td colspan="3">
                <center>
                    <rsweb:ReportViewer ID="rptHeadWise45SSRS" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
    </table>
</asp:Content>
