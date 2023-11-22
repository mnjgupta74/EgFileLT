<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonFromDateToDate.ascx.cs" Inherits="UserControls_CommonFromDateToDate" %>
<script type="text/javascript">

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
        if (YearDt < 2009) {
            alert('year cannot be less than 2009')
            dtObj.value = ""
            return false
        }
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
           if (YearDt < 2009) {
               alert('year cannot less than 2011.!')
               dtObj.value = ""
               return false
           }
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
<table style="width: 100%" align="center" id="MainTable">
    <tr style="height: 45px">
        <td align="center">
            <b><span style="color: #336699">From Date:-</span></b>&nbsp;
            <asp:TextBox ID="txtfromdatebank" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation()"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                Format="dd/MM/yyyy" TargetControlID="txtfromdatebank" >
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
       
    </tr>
</table>
