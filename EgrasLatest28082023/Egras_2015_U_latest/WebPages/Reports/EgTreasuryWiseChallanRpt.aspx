<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgTreasuryWiseChallanRpt.aspx.cs" Inherits="WebPages_Reports_EgTreasuryWiseChallanRpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/FinancialYearDropDown.ascx" TagName="FinYear" TagPrefix="ucl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function NumericOnly(el) {
            var ex = /^\s*\d+\s*$/;
            if (ex.test(el.value) === false) {
                alert('Enter Number Only');
                el.value = "";
            }
        }
        function dateValidation() {

            var dtObj = document.getElementById("<%=txtFromDate.ClientID %>");

            var dtStr = dtObj.value;
            var dtTemp = dtStr;

            if (dtTemp === '') {
                alert('Date cant be blank');
                dtObj.value = "";
                return false;
            }
            if (dtTemp.indexOf('/') === -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.');
                dtObj.value = "";
                return false;
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1);
            if (dtTemp.indexOf('/') === -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.');
                dtObj.value = "";
                return false;
            }
            //check for parts of date
            var dayDt;
            var monDt;
            var yearDt;

            dtTemp = dtStr;
            dayDt = dtTemp.substring(0, dtTemp.indexOf('/'));
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1);
            monDt = dtTemp.substring(0, dtTemp.indexOf('/'));
            yearDt = dtTemp.substring(dtTemp.indexOf('/') + 1);
            if (yearDt.length !== 4) {
                alert('Invalid Date.Year should be in 4-digits.');
                dtObj.value = "";
                return false;
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(dayDt) || isNaN(monDt) || isNaN(yearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
                dtObj.value = "";
                return false;
            }
            var dateEntered = new Date();
            dateEntered.setFullYear(yearDt, parseInt(monDt - 1), dayDt);


            if (dateEntered.getMonth() !== (parseInt(monDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
                dtObj.value = "";
                return false;
            }

            var str2 = new Date();
            var s = str2.format("dd/MM/yyyy");
            var dt1 = parseInt(dtStr.substring(0, 2), 10);
            var mon1 = parseInt(dtStr.substring(3, 5), 10);
            var yr1 = parseInt(dtStr.substring(6, 10), 10);
            var dt2 = parseInt(s.substring(0, 2), 10);
            var mon2 = parseInt(s.substring(3, 5), 10);
            var yr2 = parseInt(s.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1);
            var date2 = new Date(yr2, mon2, dt2);
            if (date2 < date1) {
                alert("From date cannot be greater than from current date");
                dtObj.value = "";
                return false;
            }
            return false;
        }

        function dateValidation1() {
            var dtObj = document.getElementById("<%=txtToDate.ClientID %>");

            var dtStr = dtObj.value;
            var dtTemp = dtStr;

            if (dtStr === '') {
                alert('Date cant be blank');
                dtObj.value = "";
                return false;
            }
            if (dtTemp.indexOf('/') === -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.');
                dtObj.value = "";
                return false;
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1);
            if (dtTemp.indexOf('/') === -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.');
                dtObj.value = "";
                return false;
            }
            //check for parts of date
            var dayDt;
            var monDt;
            var yearDt;

            dtTemp = dtStr;
            dayDt = dtTemp.substring(0, dtTemp.indexOf('/'));
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1);
            monDt = dtTemp.substring(0, dtTemp.indexOf('/'));
            yearDt = dtTemp.substring(dtTemp.indexOf('/') + 1);
            if (yearDt.length !== 4) {
                alert('Invalid Date.Year should be in 4-digits.');
                dtObj.value = "";
                return false;
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(dayDt) || isNaN(monDt) || isNaN(yearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
                dtObj.value = "";
                return false;
            }
            var dateEntered = new Date();
            dateEntered.setFullYear(yearDt, parseInt(monDt - 1), dayDt);


            if (dateEntered.getMonth() !== (parseInt(monDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
                dtObj.value = "";
                return false;
            }


            var fdate = document.getElementById("<%=txtFromDate.ClientID %>");
            var tdate = document.getElementById("<%=txtToDate.ClientID %>");
            var dtfdate = fdate.value;
            var dttdate = tdate.value;


            var dt1 = parseInt(dtfdate.substring(0, 2), 10);
            var mon1 = parseInt(dtfdate.substring(3, 5), 10);
            var yr1 = parseInt(dtfdate.substring(6, 10), 10);

            var dt2 = parseInt(dttdate.substring(0, 2), 10);
            var mon2 = parseInt(dttdate.substring(3, 5), 10);
            var yr2 = parseInt(dttdate.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1);
            var date2 = new Date(yr2, mon2, dt2);
            if (date2 < date1) {
                alert("from date cannot be greater than to date");
                dtObj.value = "";
                return false;
            }
            return false;
        }
    </script>

    <style type="text/css">
        .form-control {
            display: inherit;
        }

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

        input[type=submit] {
            width: 135px;
            height: 35px;
        }

        table#MainTable tbody tr td {
            padding: 3px;
        }
    </style>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../App_Themes/images/progress.gif" alt="" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- <fieldset runat="server" id="lstrecord" style="width: 1000px;">
                <legend style="color: #336699; font-weight: bold">Treasury Wise Challan Report</legend>--%>
            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Treasury Wise Challan Report" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Treasury Wise Challan Report</span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Treasury Wise Challan Report" />
            </div>
            <table id="MainTable" style="width: 100%" border="1" align="center" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="center">
                        <b><span style="color: #336699">From Date:-</span></b>
                        <asp:TextBox ID="txtFromDate" CssClass="form-control" runat="server" Height="100%" onkeypress="Javascript:return NumberOnly(event)"
                            onChange="javascript:return dateValidation()" Width="50%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td align="center">
                        <b><span style="color: #336699">To Date:-</span></b>
                        <asp:TextBox ID="txtToDate" CssClass="form-control" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                            onChange="javascript:return dateValidation1()" Width="50%" Height="100%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td align="center" style="width:28%">
                        <b><span style="color: #336699">BudgetHead:-</span></b>
                        <asp:TextBox ID="txtMajorHead" CssClass="form-control" Height="100%" Width="65%" MaxLength="4" runat="server" onChange="NumericOnly(this);"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="9999-99-999-99-99"
                            MaskType="None" CultureName="en-US" TargetControlID="txtMajorHead" AcceptNegative="None"
                            runat="server">
                        </ajaxToolkit:MaskedEditExtender>
                    </td>
                    <td align="center"><b><span style="color: #336699">Treasury:-</span></b>
                     <asp:DropDownListX ID="ddllocation" runat="server" class="chzn-select"
                            Height="100%" CssClass="form-control" Width="65%">
                            <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                        </asp:DropDownListX>

                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div class="col-md-4">
                            <asp:Button ID="btnshow" CssClass="btn btn-default pull-right" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnPDFDownload" CssClass="btn btn-default pull-right" runat="server" Text="PDF Download" ValidationGroup="de" OnClick="btnPDFDownload_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnExcelDownload" CssClass="btn btn-default pull-right" runat="server" Text="Excel Download" ValidationGroup="de" OnClick="btnExcelDownload_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnReset" Height="33" CssClass="btn btn-default pull-right" runat="server" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="grdTy11Rpt" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Names="Verdana" Font-Size="10pt" Width="100%" EmptyDataText="No Record Found"
                            EmptyDataRowStyle-HorizontalAlign="Center" ForeColor="#333333" GridLines="None"
                            EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#507CD1" OnRowCommand="grdTy11Rpt_RowCommand"
                            AllowPaging="true" OnPageIndexChanging="grdTy11Rpt_PageIndexChanging1" PageSize="25">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRN">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkgrn" CommandName="grnbind" Text='<%#Eval("GRN")%>' CommandArgument='<%#Eval("GRN")%>'
                                            runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Challanno" HeaderText="ChallanNo">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ChallanDate" HeaderText="ChallanDate" DataFormatString="{0:dd-MMM-yyyy}"
                                    HtmlEncode="False">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OfficeId" HeaderText="Office Id">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FullName" HeaderText="FirstName/CompanyName">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CashAmt" HeaderText="Cash Amount" DataFormatString="{0:n}">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <%-- </fieldset>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPDFDownload" />
            <asp:PostBackTrigger ControlID="btnExcelDownload" />
        </Triggers>
    </asp:UpdatePanel>
    <rsweb:ReportViewer ID="SSRSreport" runat="server" Width="100%" SizeToReportContent="true"
        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false" Visible="False">
    </rsweb:ReportViewer>
</asp:Content>
