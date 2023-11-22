<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgBankRegister.aspx.cs" Inherits="WebPages_EgBankRegister" Title="Bank Register" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

        .textBox-custom {
            
            font-size: 14px;
            line-height: 1.428571429;
            color: #555555;
            background-color: #ffffff;
            border: 1px solid #cccccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            -webkit-transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
            transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
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
        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnSubmit"]').click(function () {
                if ($(<%=txtfromdate.ClientID%>).val() != "" && $(<%=txttodate.ClientID%>).val() != "") {
                    $('#cover-spin').show(0)

                }
            });
        });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>


    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Bank Register" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Bank Register Report</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="right" title="Bank Register Report" />
    </div>
    <table width="100%" style="text-align: center" align="center" border="1">
        <%--<tr>
            <td colspan="3" style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="Bank Register Report" Font-Bold="True"
                    ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td align="center" style="width: 300px">
                <b><span style="color: #336699">From Date :</span></b>&nbsp;
                <asp:TextBox ID="txtfromdate" runat="server" TabIndex="1" CssClass="textBox-custom" Style="width: 120px; height: 28px"
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
            <td align="center">
                <b><span style="color: #336699">To Date :</span></b>&nbsp;
                <asp:TextBox ID="txttodate" runat="server" TabIndex="2" CssClass="textBox-custom" Style="width: 120px; height: 28px"
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
            <td align="center" width="400px">
                <asp:Button ID="btnSubmit" runat="server" Text="Show" Height="33px" Style="margin-right: 20px" OnClick="btnSubmit_Click" CssClass="btn btn-default"
                    ValidationGroup="a" TabIndex="3" />
                <asp:Button ID="btnPrint" runat="server" Text="PDF" ValidationGroup="a" Style="margin-right: 20px" Height="33px" OnClick="btnPrint_Click" CssClass="btn btn-default" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="de" Height="33px" OnClick="btnReset_Click" CssClass="btn btn-default" />
            </td>
        </tr>

        <tr runat="server" id="trrpt" visible="false">
            <td colspan="3" style="text-align: left">
                <center>
                    <rsweb:ReportViewer ID="rptBankRegister" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
    </table>
</asp:Content>
