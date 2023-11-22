<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgBankResponseAudit.aspx.cs" Inherits="WebPages_Admin_EgBankResponseAudit"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript" language="javascript">

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
        
         function ClearDate()
            {
             var fmdate = document.getElementById("<%=txtFromdate.ClientID %>")
             fmdate.value = "";
            }
            
            
            function ClearDate1()
            {
            var tdate = document.getElementById("<%=txtTodate.ClientID %>")
             tdate.value = "";
            }
     function dateValidation() {

            var dtObj = document.getElementById("<%=txtFromdate.ClientID %>")

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

        function dateValidation1() {
            var dtObj = document.getElementById("<%=txtTodate.ClientID %>")

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


            var fdate = document.getElementById("<%=txtFromdate.ClientID %>")
            var tdate = document.getElementById("<%=txtTodate.ClientID %>")
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

    <table id="tbl" cellpadding="0" cellspacing="0" border="1" width="80%" align="center">
        <tr>
            <td align="center" colspan="4">
                <asp:Label ID="LabelHeading" runat="server" Text="Bank String Decryption" ForeColor="Green"
                    Font-Size="Medium" Font-Underline="true"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                From Date :- &nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtFromdate" runat="server" Width="140px" onkeypress="Javascript:return NumberOnly(event)"
                    onChange="javascript:return dateValidation()" onmousedown="ClearDate();"></asp:TextBox>
                <ajaxToolKit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtFromdate">
                </ajaxToolKit:CalendarExtender>
                <ajaxToolKit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtFromdate" AcceptNegative="None" runat="server">
                </ajaxToolKit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtFromdate"
                    ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
            </td>
            <td>
                To Date :- &nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtTodate" runat="server" Width="140px" onkeypress="Javascript:return NumberOnly(event)"
                    onChange="javascript:return dateValidation1()" onmousedown="ClearDate1();"></asp:TextBox>
                <ajaxToolKit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtTodate">
                </ajaxToolKit:CalendarExtender>
                <ajaxToolKit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtTodate" AcceptNegative="None" runat="server">
                </ajaxToolKit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txtTodate"
                    ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Select Bank: &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlbankname" runat="server" Width="150px" CssClass="borderRadius inputDesign">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
            <td>
                Type :- &nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:RadioButtonList ID="rdtype" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="EgrasService" Value="1" Selected="True">Egras Service</asp:ListItem>
                    <asp:ListItem Text="BankResponse" Value="2">Bank Response</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnclick" Text="Decrypt" runat="server" ValidationGroup="vldInsert"
                    OnClick="btnclick_Click" />
            </td>
        </tr>
        <tr id="rowGrid" runat="server" visible="false">
            <td align="center" colspan="4">
                <asp:GridView ID="grdDecryptedString" runat="server" AutoGenerateColumns="False" 
                    Width="100%" BorderColor="#507cd1" Font-Names="Verdana" Font-Size="10pt" EmptyDataText="No Record Found"
                    EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                    CellPadding="4" BackColor="White" BorderStyle="Double" BorderWidth="3px" GridLines="Horizontal"
                    AllowPaging="true" PageSize="25" 
                    onpageindexchanging="grdDecryptedString_PageIndexChanging"  >
                    <Columns>
                        <asp:BoundField DataField="Response" HeaderText="Decypted Data">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <PagerStyle BackColor="#507cd1" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507cd1" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
