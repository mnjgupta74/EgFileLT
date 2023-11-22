<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgManualSuccessTransactionView.aspx.cs" Inherits="WebPages_EgManualSuccessTransactionView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">

        function NumberOnly(ctrl) {
            var ch;

            if (window.event) {
                ch = ctrl.keyCode;
            }
            else if (ctrl.which) {
                ch = ctrl.which;
            }
            if ((ch >= 48 && ch <= 57))
                return true;

            else
                return false;
        }
        //for txtFromDate text box
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
                alert("To date cannot be greater than from current date");
                dtObj.value = ""
                return false
            }
        }


        //txtTo Date textbox

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
            }
        }
    </script>

    <fieldset runat="server" id="lstrecord" style="width: 1000px;">
        <legend style="color: #336699; font-weight: bold">ManualSuccess Challan</legend>
        <table style="width: 100%" align="center" id="MainTable">
            <%-- <tr>
                <td align="center" colspan="4">
                    <asp:Label ID="lblSchema" runat="server" Text="Failed Transactions" ForeColor="Green"
                        Font-Bold="true"></asp:Label>
                </td>
            </tr>--%>
            
            <tr style="height: 45px">
                <td align="center">
                    <b><span style="color: #336699">Bank:-</span></b>&nbsp;
                    <asp:DropDownList ID="ddlbanks" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation()"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                    </ajaxToolKit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtToDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation1()"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                        Format="dd/MM/yyyy" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                    </ajaxToolKit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                </td>
                <td align="center">
                    <asp:Button ID="btnShow" runat="server" ValidationGroup="de" Text="Show" OnClick="btnShow_Click" />
                </td>
            </tr>
            <tr align="center" id="trrpt" runat="server">
                <td colspan="4">
                    <asp:Repeater ID="rptrManualSuccess"    OnItemCommand="rptrManualSuccess_ItemCommand" runat="server">
                        <HeaderTemplate>
                            <table border="1" width="80%" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #14c4ff; color: White; font-weight: bold; height: 20px">
                                    <th>
                                        <b>S.No</b>
                                    </th>
                                    <th>
                                        <b>GRN</b>
                                    </th>
                                    <th>
                                        <b>Challan No</b>
                                    </th>
                                    <th>
                                        <b>Amount</b>
                                    </th>
                                    <th>
                                        <b>View</b>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%# DataBinder.Eval(Container.DataItem,"GRN")%>
                                </td>
                                <td align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "Challanno")%>
                                </td>
                                <td align="right">
                                    <%# DataBinder.Eval(Container.DataItem, "Amount", "{0:0.00}")%>
                                </td>
                                <td align="center">
                                            <asp:LinkButton ID="LinkStatus" runat="server" CausesValidation="false" CommandName="Status"
                                            CommandArgument='<%# Eval("Grn") %>' Text="View"></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                               <tr>
                                <td align="center">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%# DataBinder.Eval(Container.DataItem,"GRN")%>
                                </td>
                                <td align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "Challanno")%>
                                </td>
                                <td align="right">
                                    <%# DataBinder.Eval(Container.DataItem, "Amount", "{0:0.00}")%>
                                </td>
                                   <td align="center">
                                            <asp:LinkButton ID="LinkStatus" runat="server" CausesValidation="false" CommandName="Status"
                                            CommandArgument='<%# Eval("Grn")  %>' Text="View"></asp:LinkButton>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

