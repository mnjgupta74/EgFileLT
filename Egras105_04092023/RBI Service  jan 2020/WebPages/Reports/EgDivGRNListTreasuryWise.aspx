<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgDivGRNListTreasuryWise.aspx.cs" Inherits="WebPages_Reports_EgDivGRNListTreasuryWise" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script language="javascript" type="text/javascript">
            
        function NumberOnly(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;

            if (evt.keyCode == 46) return (parts.length == 1);

            if (parts[0].length >= 14) return false;

            if (parts.length == 3 && parts[1].length >= 3) return false;
        }
       
        function dateValidation() {

            var dtObj = document.getElementById("<%=txtFromDate.ClientID %>")

            var dtStr = dtObj.value
            var dtTemp = dtStr

            if (dtTemp == '') {
                alert('Date cant be blank')
                dtObj.value = ""
                return false
            }
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            //check for parts of date
            var DayDt
            var MonDt
            var YearDt

            dtTemp = dtStr
            DayDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            MonDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            YearDt = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt) || isNaN(MonDt) || isNaN(YearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt, parseInt(MonDt - 1), DayDt)


            if (DateEntered.getMonth() != (parseInt(MonDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
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
                dtObj.value = ""
                return false
            }
        }

        function dateValidation1() {
            var dtObj = document.getElementById("<%=txtToDate.ClientID %>")

            var dtStr = dtObj.value
            var dtTemp = dtStr

            if (dtStr == '') {
                alert('Date cant be blank')
                dtObj.value = ""
                return false
            }
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            //check for parts of date
            var DayDt
            var MonDt
            var YearDt

            dtTemp = dtStr
            DayDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            MonDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            YearDt = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt) || isNaN(MonDt) || isNaN(YearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt, parseInt(MonDt - 1), DayDt)


            if (DateEntered.getMonth() != (parseInt(MonDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }


            var fdate = document.getElementById("<%=txtFromDate.ClientID %>")
            var tdate = document.getElementById("<%=txtToDate.ClientID %>")
            var dtfdate = fdate.value
            var dttdate = tdate.value


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
                dtObj.value = ""
                return false
            }
        }
    </script>
     <div style="text-align: center">
        <fieldset runat="server" id="fieldamount" style="width: 1000px; margin-left: 50px">
            <legend style="color: #008080; font-weight: bold">GRN List With Division Code</legend>
            <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                <tr style="height: 45px">
                    <td align="center">
                        <div id="divMain" runat="server">
                            <table style="width: 100%" cellpadding="0" cellspacing="0" align="center">
                                <tr>
                                    <td>
                                        <b><span style="color: #008080">From Date:-</span></b>&nbsp;
                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                                            onChange="javascript:return dateValidation()" Width="80px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                            CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                                            Format="dd/MM/yyyy" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <b><span style="color: #008080">To Date:-</span></b>&nbsp;
                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                                            onChange="javascript:return dateValidation1()" Width="80px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                            CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                                            Format="dd/MM/yyyy" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <b><span style="color: #008080; font-family: Arial CE; font-size: 13px;">Treasury :
                                        </span></b>&nbsp;
                                        <asp:DropDownList ID="ddlTreasury" runat="server" Width="175px" class="chzn-select">
                                            <asp:ListItem Value="0" Text="--All Treasury--"></asp:ListItem>
                                            <asp:ListItem Value="0100" Text="Ajmer"></asp:ListItem>
                                            <asp:ListItem Value="0200" Text="ALWAR"></asp:ListItem>
                                            <asp:ListItem Value="0300" Text="BANSWARA"></asp:ListItem>
                                            <asp:ListItem Value="0400" Text="BARAN"></asp:ListItem>
                                            <asp:ListItem Value="0500" Text="BARMER"></asp:ListItem>
                                            <asp:ListItem Value="0600" Text="BEAWAR"></asp:ListItem>
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
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                                    </td>
                                </tr>
                            </table>
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
        </fieldset>
    </div>
</asp:Content>

