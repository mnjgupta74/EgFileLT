<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgDefacedRefundAmountDetailsRemitterWiseRpt.aspx.cs" Inherits="WebPages_Reports_DefacedRefundAmountDetailsRemitterWiseRpt" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>
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
        }.btn-default {
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
    $(document).ready(function () 
    {
        $('[name*="ctl00$ContentPlaceHolder1$btnShow"]').click(function ()
        {
            if ($(<%=txtFromDate.ClientID%>).val()!= "" && $(<%=txtToDate.ClientID%>).val()!= "")
            {
                $('#cover-spin').show(0)

            }
        });
    });
</script>
        <%--==============END CSS - JQUERY LOADER============--%>

     <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Mismatch Records" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Defaced Challan</span></h2>
        <img src="../../Image/help1.png"  class="pull-right" style="height: 44px; width: 34px;" title="Mismatch Records" />
    </div>
                <table style="width: 100%" align="center" id="MainTable" border="1">
                    <tr align="center">
                        <td align="center">
                            <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                            <asp:TextBox ID="txtFromDate" runat="server" Style="display: initial !important" Height="100%" CssClass="form-control" Width="120px" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                                Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                            </ajaxToolkit:CalendarExtender>
                           <%-- <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                       </td>
                        <td align="left">

                            <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                            <asp:TextBox ID="txtToDate" runat="server" Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                                Format="dd/MM/yyyy" TargetControlID="txtToDate">
                            </ajaxToolkit:CalendarExtender>
                           <%-- <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <b><span style="color: #336699">Treasury :
                                        </span></b>&nbsp;
                                        <asp:DropDownList ID="ddlTreasury" runat="server" Width="150px" Style="display: initial !important"  CssClass="form-control chzn-select">
                                            <asp:ListItem Value="0" Text="--All Treasury--"></asp:ListItem>
                                            <asp:ListItem Value="0100" Text="Ajmer"></asp:ListItem>
                                            <asp:ListItem Value="0200" Text="ALWAR"></asp:ListItem>
                                            <asp:ListItem Value="0300" Text="BANSWARA"></asp:ListItem>
                                            <asp:ListItem Value="0400" Text="BARAN"></asp:ListItem>
                                            <asp:ListItem Value="0500" Text="BARMER"></asp:ListItem>
                                            <asp:ListItem Value="0600" Text="BARMER"></asp:ListItem>
                                            <asp:ListItem Value="0700" Text="BHARATPUR"></asp:ListItem>
                                            <asp:ListItem Value="0800" Text="BHILWARA"></asp:ListItem>
                                            <asp:ListItem Value="0900" Text="BIKANER"></asp:ListItem>
                                            <asp:ListItem Value="1000" Text="BUNDI"></asp:ListItem>
                                            <asp:ListItem Value="1100" Text="CHITTORGARH"></asp:ListItem>
                                            <asp:ListItem Value="1200" Text="CHURU"></asp:ListItem>
                                            <asp:ListItem Value="1300" Text="DAUSA"></asp:ListItem>
                                            <asp:ListItem Value="1400" Text="DHOLPUR"></asp:ListItem>
                                            <asp:ListItem Value="1500" Text="DUNGARPUR"></asp:ListItem>
                                            <asp:ListItem Value="1600" Text="GANGANAGAR"></asp:ListItem>
                                            <asp:ListItem Value="1700" Text="HANUMANGARH"></asp:ListItem>
                                            <asp:ListItem Value="1800" Text="JAIPUR (CITY)"></asp:ListItem>
                                            <asp:ListItem Value="2000" Text="JAIPUR (RURAL)"></asp:ListItem>
                                            <asp:ListItem Value="2100" Text="JAIPUR (SECTT.)"></asp:ListItem>
                                            <asp:ListItem Value="2200" Text="JAISALMER"></asp:ListItem>
                                            <asp:ListItem Value="2300" Text="JALORE"></asp:ListItem>
                                            <asp:ListItem Value="2400" Text="JHALAWAR"></asp:ListItem>
                                            <asp:ListItem Value="2500" Text="JHUNJHUNU"></asp:ListItem>
                                            <asp:ListItem Value="2600" Text="JODHPUR (CITY)"></asp:ListItem>
                                            <asp:ListItem Value="2700" Text="JODHPUR (RURAL)"></asp:ListItem>
                                            <asp:ListItem Value="2800" Text="KAROLI"></asp:ListItem>
                                            <asp:ListItem Value="2900" Text="KOTA"></asp:ListItem>
                                            <asp:ListItem Value="3000" Text="NAGAUR"></asp:ListItem>
                                            <asp:ListItem Value="3100" Text="PALI"></asp:ListItem>
                                            <asp:ListItem Value="3200" Text="PRATAPGARH"></asp:ListItem>
                                            <asp:ListItem Value="3300" Text="RAJSAMAND"></asp:ListItem>
                                            <asp:ListItem Value="3400" Text="SAWAI MADHOPUR"></asp:ListItem>
                                            <asp:ListItem Value="3500" Text="SIKAR"></asp:ListItem>
                                            <asp:ListItem Value="3600" Text="SIROHI"></asp:ListItem>
                                            <asp:ListItem Value="3700" Text="TONK"></asp:ListItem>
                                            <asp:ListItem Value="3800" Text="UDAIPUR"></asp:ListItem>
                                            <asp:ListItem Value="4100" Text="UDAIPUR RURAL"></asp:ListItem>
                                            <asp:ListItem Value="3900" Text="NEW DELHI"></asp:ListItem>
                                        </asp:DropDownList>
                            </td>
                            
                          
                        </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnShow" runat="server" CssClass="btn btn-default" Height="100%" ValidationGroup="de" Text="Show" OnClick="btnShow_Click"/>
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Height="100%" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />
                        </td>
                    </tr>
                    </table><table>
                    <tr align="center">
                        <td colspan="2">
                            <asp:Button Text="PDFDownload" ID="btnPDF" CssClass="btn btn-default" Height="100%" Visible="false" OnClick="btnPDF_Click" runat="server" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td colspan="2">
                            <rsweb:ReportViewer ID="SSRSreport" ShowPrintButton="true" ShowExportControls="false" ShowPageNavigationControls="true" ShowRefreshButton="false" runat="server" AsyncRendering="false" Width="100%" Height="100%">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                    
                </table>
            
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

