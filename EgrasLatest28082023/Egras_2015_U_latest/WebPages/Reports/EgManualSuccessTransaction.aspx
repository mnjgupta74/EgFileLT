<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgManualSuccessTransaction.aspx.cs" Inherits="WebPages_Reports_EgManualSuccessTransaction" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <style type="text/css">
        span {
            font-size: medium !important;
        }
    </style>--%>
    <%--<script src="../js/Control.js" language="javascript" type="text/javascript"></script>--%>
    <script type="text/javascript">

        //function NumberOnly(ctrl) {
        //    
        //    var ch;

        //    if (window.event) {
        //        ch = ctrl.keyCode;
        //    }
        //    else if (ctrl.which) {
        //        ch = ctrl.which;
        //    }
        //    if ((ch >= 48 && ch <= 57))
        //        return true;

        //    else
        //        return false;
        //}
        //for txtFromDate text box
        function dateValidation() {
            

            var dtObj = document.getElementById("<%=txtfromdatebank.ClientID %>")

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
                alert("To date cannot be greater than from current date");
                dtObj.value = ""
                return false
            }
        }


        //txtTo Date textbox

        function dateValidation1() {
            
            var dtObj = document.getElementById("<%=txtTodatebnk.ClientID %>")

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


            var fdate = document.getElementById("<%=txtfromdatebank.ClientID %>")
            var tdate = document.getElementById("<%=txtTodatebnk.ClientID %>")
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
            }
        }
    </script>



    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>--%>
    <fieldset>
        <legend style="color: #336699; font-weight: bold">Mismatch Records</legend>
        <table style="width: 100%" align="center" id="MainTable">
            <tr style="height: 45px">


                <td align="center">
                    <b><span style="color: #336699">Bank:-</span></b>&nbsp;
                    <asp:DropDownList ID="ddlbankgrnstatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtfromdatebank" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation()"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                        Format="dd/MM/yyyy" TargetControlID="txtfromdatebank">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtfromdatebank" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdatebank"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtTodatebnk" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation1()"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtTodatebnk"
                        Format="dd/MM/yyyy" TargetControlID="txtTodatebnk">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtTodatebnk" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTodatebnk"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                </td>
                <td align="center">
                      <asp:Button ID="btnFindResult" runat="server" Text="Show" ValidationGroup="de" OnClick="btnFindResult_Click"  />
                      <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Print" />
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
                <%--<td align="center"> <asp:Label ID="lblfromdate" runat="server" Text="From Date" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtfromdatebank" runat="server"  onChange="javascript:return dateValidation()"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="calDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtfromdatebank"
            TargetControlID="txtfromdatebank">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
            CultureName="en-US" TargetControlID="txtfromdatebank" AcceptNegative="None" runat="server">
        </ajaxToolkit:MaskedEditExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdatebank"
            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator></td>
                <td align="center">
                     <asp:Label ID="lbltodate" runat="server" Text="To Date" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtTodatebnk" runat="server"  onChange="javascript:return dateValidation1()"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtTodatebnk"
            TargetControlID="txtTodatebnk">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
            CultureName="en-US" TargetControlID="txtTodatebnk" AcceptNegative="None" runat="server">
        </ajaxToolkit:MaskedEditExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTodatebnk"
            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                </td>--%>
            </tr>
           <%-- <tr style="height: 45px">
                <td align="center">  
        <asp:Label ID="lblselbank" runat="server" Text="Select Bank" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlbankgrnstatus" runat="server"></asp:DropDownList></td>
                <td align="center">
                      <asp:Button ID="btnFindResult" runat="server" Text="Show" ValidationGroup="de" OnClick="btnFindResult_Click" />
                </td>
            </tr>--%>
    
         </table>





        </fieldset>
        <br />
        <br /> 
      
</asp:Content>

