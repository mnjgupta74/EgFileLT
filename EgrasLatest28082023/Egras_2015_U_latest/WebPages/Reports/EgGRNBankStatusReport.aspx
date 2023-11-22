<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgGRNBankStatusReport.aspx.cs" Inherits="WebPages_EgGRNBankStatusReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/UserControls/CommonFromDateToDate.ascx" TagName="FromToDate" TagPrefix="ucl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            var rblTypeOfBusiness = $('#<%=rblbankdatewise.ClientID %> input:checked').val();
            if (rblTypeOfBusiness == 1) {
                $('#<%=divrpt1.ClientID %>').show();
                  $('#<%=divrpt2.ClientID %>').hide();
              }
              else if (rblTypeOfBusiness == 2) {
                  $('#<%=divrpt2.ClientID %>').show();
                      $('#<%=divrpt1.ClientID %>').hide();
                  }
                  else {
                      $('#<%=divrpt1.ClientID %>').show();
                      $('#<%=divrpt2.ClientID %>').hide();
                  }

            $('#<%=rblbankdatewise.ClientID %>').change(function () {
                $('#<%=divrpt1.ClientID %>').hide();
                  $('#<%=divrpt2.ClientID %>').hide();

              });
        });
    </script>
    <style>
        #ctl00_ContentPlaceHolder1_frmdatetodate_txtfromdatebank {
            width: 100px !important;
        }

        #ctl00_ContentPlaceHolder1_frmdatetodate_txtTodatebnk {
            width: 100px !important;
        }
    </style>

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
    </style>

    <div id="cover-spin"></div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnFindResult"]').click(function () {
                if ($('[id*="ctl00_ContentPlaceHolder1_frmdatetodate_txtfromdatebank"]').val() != "" && $('[id*="ctl00_ContentPlaceHolder1_frmdatetodate_txtTodatebnk"]').val() != "") {
                    $('#cover-spin').show(0)
                }
            });
        });
    </script>

    <%--==============END CSS - JQUERY LOADER============--%>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Mismatch Records" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Late Reporting</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Mismatch Records" />
    </div>

    <table style="width: 100%" align="center" id="MainTable" style="width: 100%" border="1">
        <tr style="height: 45px" align="center">
            <td colspan="3" align="center">
                <asp:RadioButtonList runat="server" ID="rblbankdatewise" RepeatDirection="Horizontal">
                    <asp:ListItem Text="BankWise" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="DayWise" Value="2"></asp:ListItem>
                    <asp:ListItem Text="ScrollWise" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>

        </tr>
        <tr style="height: 45px">
            <td align="center">
                <b>
                    <asp:Label ID="lblbank" runat="server" Text="Bank:-" Style="color: #336699" Visible="false"></asp:Label></b>&nbsp;
                    <asp:DropDownList ID="ddlbankgrnstatus" runat="server" class="chzn-select">
                    </asp:DropDownList>
            </td>
            <td>
                <ucl:FromToDate ID="frmdatetodate" runat="server" Width="120px" Height="22px" />
            </td>
            <td align="center">
                <asp:Button ID="btnFindResult" runat="server" Text="Show" ValidationGroup="de" OnClick="btnFindResult_Click" />
                <asp:Button ID="pdfsearch" runat="server" Text="PDF" ValidationGroup="de" OnClick="btnpdf_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />
            </td>
        </tr>
    </table>

    <div id="divrpt1" runat="server">
        <table border="1" width="100%" cellpadding="0" cellspacing="0">
            <%-- <table style="width: 100%" align="center" id="Ttable">--%>
            <asp:Repeater ID="rptgrndetailbankstatus" runat="server">
                <HeaderTemplate>

                    <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px;">
                        <th align="center">
                            <b>S.No</b>
                        </th>
                        <th align="center">
                            <b>GRN</b>
                        </th>
                        <th align="center">
                            <b>Amount</b>
                        </th>
                        <th align="center">
                            <b>BankName</b>
                        </th>
                        <th align="center">
                            <b>Bank Response Date</b>
                        </th>
                        <th align="center">
                            <b>Expected Date</b>
                        </th>
                        <th align="center">
                            <b>No of Days Difference</b>
                        </th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#  Eval("GRN")%>
                        </td>
                        <td align="right">
                            <%#  Eval("Amount","{0:0.00}").ToString()%>
                        </td>
                        <td align="right">
                            <%#  Eval("BankName")%>
                        </td>
                        <td align="center">
                            <%#  Eval("BankChallanDate")%>
                        </td>
                        <td align="center">
                            <%#  Eval("Expected_Date")%>
                        </td>
                        <td align="center">
                            <%#  Eval("DiffDate")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#  Eval("GRN")%>
                        </td>
                        <td align="right">
                            <%#  Eval("Amount","{0:0.00}").ToString()%>
                        </td>
                        <td align="right">
                            <%#  Eval("BankName")%>
                        </td>
                        <td align="center">
                            <%#  Eval("BankChallanDate")%>
                        </td>
                        <td align="center">
                            <%#  Eval("Expected_Date")%>
                        </td>
                        <td align="center">
                            <%#  Eval("DiffDate")%>
                        </td>
                    </tr>

                </AlternatingItemTemplate>
                <FooterTemplate></FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <table>
        <div id="divrpt2" runat="server">
            <asp:Repeater ID="rptgrnbankstatus" runat="server" OnItemCommand="rptgrnbankstatus_ItemCommand">
                <HeaderTemplate>
                    <table border="1" width="100%" cellpadding="0" cellspacing="0">
                        <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px;">
                            <th align="center">
                                <b>S.No</b>
                            </th>
                            <th align="center">
                                <b>Total Amount</b>
                            </th>
                            <th align="center">
                                <b>No of Days Difference</b>
                            </th>
                            <th align="center">
                                <b>BankName</b>
                            </th>

                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="LinkStatus" runat="server" CausesValidation="false" CommandName="Tamt"
                                CommandArgument='<%# Eval("NoofDaysDifference")%>' Text='<%# Eval("TotalAmount","{0:0.00}").ToString()%>'></asp:LinkButton>
                        </td>
                        <td align="center">
                            <%#  Eval("NoofDaysDifference")%>
                        </td>
                        <td align="center">
                            <%#  Eval("BankName")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="LinkStatus" runat="server" CausesValidation="false" CommandName="Tamt"
                                CommandArgument='<%# Eval("NoofDaysDifference")%>' Text='<%# Eval("TotalAmount","{0:0.00}").ToString()%>'></asp:LinkButton>
                        </td>
                        <td align="center">
                            <%#  Eval("NoofDaysDifference")%>
                        </td>
                        <td align="center">
                            <%#  Eval("BankName")%>
                        </td>
                    </tr>

                </AlternatingItemTemplate>

            </asp:Repeater>
        </div>
    </table>
    <div runat="server" id="trrpt1" visible="false">
        <%--<td colspan="3" style="text-align: left">
                <center>--%>
        <rsweb:ReportViewer ID="rptBSRBW" runat="server" Width="100%" SizeToReportContent="true"
            AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
        </rsweb:ReportViewer>
        <%-- </center>
            </td>--%>
    </div>

    <div runat="server" id="trrpt2" visible="false">
        <%-- <td colspan="3" style="text-align: left">
                <center>--%>
        <rsweb:ReportViewer ID="rptBSRDW" runat="server" Width="100%" SizeToReportContent="true"
            AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
        </rsweb:ReportViewer>
        <%--</center>
            </td>--%>
    </div>
    <div runat="server" id="trrpt3" visible="false">
        <%--<td colspan="3" style="text-align: left">
                <center>--%>
        <rsweb:ReportViewer ID="rptBSRSW" runat="server" Width="100%" SizeToReportContent="true"
            AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
        </rsweb:ReportViewer>
        <%-- </center>
            </td>--%>
    </div>
    <%-- </table>--%>
</asp:Content>
