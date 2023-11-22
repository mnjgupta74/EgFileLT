<%@ Page Title="Egras.Rajasthan.gov.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgRefundChallanList.aspx.cs" Inherits="WebPages_Reports_EgRefundChallanList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server">


</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

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
            $('[name*="ctl00$ContentPlaceHolder1$btnshow"]').click(function () {
                if ($(<%=txtFromDate.ClientID%>).val() != "" && $(<%=txtToDate.ClientID%>).val() != ""&& $(<%=ddlTreasury.ClientID%>).val() != "0") {
                    $('#cover-spin').show(0)

                }
            });
        });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>

    <%--<fieldset runat="server" id="lstrecord" style="width: 900px; margin-left: 50px">
        <legend style="color: #336699; font-weight: bold">Refund Challan Detail</legend>--%>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Bank Scroll Report (45-A)</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Bank Scroll Report (45-A)" />
    </div>

    <table style="width: 100%" border="1" align="center" id="MainTable">
        <tr>
            <td align="left">
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" onkeypress="Javascript:return NumberOnly(event)"
                        Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px"
                        onpaste="return false" onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                    Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                </ajaxToolkit:CalendarExtender>
            </td>
            <td align="left">
                <b><span style="color: #336699">To Date : </span></b>&nbsp;
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" onkeypress="Javascript:return NumberOnly(event)"
                        Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px"
                        onpaste="return false" onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                    Format="dd/MM/yyyy" TargetControlID="txtToDate">
                </ajaxToolkit:CalendarExtender>
            </td>
            </tr>
        <tr>
            <td align="left">
                <b><span style="color: #336699">Treasury : </span></b>&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlTreasury" runat="server" Width="250px" CssClass="form-control chzn-select">
                    <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
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
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTreasury"
                    SetFocusOnError="true" ErrorMessage="*" InitialValue="0" ValidationGroup="de"></asp:RequiredFieldValidator>
            </td>
            <td align="center">
                <asp:Button ID="btnshow" runat="server" Text="Show" Height="100%" style="margin-right:14px" CssClass="btn btn-default" ValidationGroup="de" OnClick="btnshow_Click" />
                <asp:Button ID="btnPrint" runat="server" Text="PDF" Height="100%" style="margin-right:14px" CssClass="btn btn-default" ValidationGroup="a" OnClick="btnPrint_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" Height="100%" CssClass="btn btn-default " ValidationGroup="de" OnClick="btnReset_Click" />
            </td>
        </tr>

        <tr runat="server" id="trrpt" visible="false">
            <td colspan="4" align="center">
                <center>
                    <rsweb:ReportViewer ID="EgRefundChallanList" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
    </table>
    <%--</fieldset>--%>
</asp:Content>
