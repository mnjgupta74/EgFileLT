<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgExpectedDateMisMatch.aspx.cs" Inherits="WebPages_Reports_EgExpectedDateMisMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        span {
            font-size: medium !important;
        }
    </style>
    <script src="../js/Control.js" language="javascript" type="text/javascript"></script>
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
                <td align="center"> <asp:Label ID="lblfromdate" runat="server" Text="From Date" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                </td>
            </tr>
            <tr style="height: 45px">
                <td align="center">  
        <asp:Label ID="lblselbank" runat="server" Text="Select Bank" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlbankgrnstatus" runat="server"></asp:DropDownList></td>
                <td align="center">
                      <asp:Button ID="btnFindResult" runat="server" Text="Show" ValidationGroup="de" OnClick="btnFindResult_Click" />
                </td>
            </tr>
    
         </table>
        </fieldset>
        <br />
        <br />
        <table width="80%" style="margin-left: 20%;" class="table-responsive" id="Ttable">
            <asp:Repeater ID="rptgrndetailbankstatus" runat="server">
                        <HeaderTemplate>
                            <table border="1" width="80%" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #008080; color: White; font-weight: bold; height: 20px;">
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
                                <td align="center">
                                    <%#  Eval("Amount")%>
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
                                <td align="center">
                                    <%#  Eval("Amount")%>
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
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>




            
        </table>
    </div>
</asp:Content>

