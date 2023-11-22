<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="ErrorLogApp.aspx.cs" Inherits="WebPages_ErrorLogNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="tblheader" border="0" cellpadding="0" cellspacing="0" align="center" width="60%">
    <tr>
            <td style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="Error Information" Font-Bold="True"
                    ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
            </td>
        
        </tr>
        </table>
    <fieldset id="fieldErrorInfo" runat="server">
        <legend style="color: #005CB8; font-size: small">DateWise Error Information</legend>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" align="center" width="60%">
            <%--Date/Search--%>
            <tr align="center">
                <td align="center" style="height: 30px; text-align: left;" valign="middle">
                    From Date :
                    <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" TabIndex="1" onkeypress="Javascript:return NumberOnly(event)" 
                        onChange="javascript:return dateValidation()"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="reqFromd" runat="server" ErrorMessage="Required Field"
                        SetFocusOnError="true" ControlToValidate="txtFromDate" ValidationGroup="de"></asp:RequiredFieldValidator>
                    &nbsp; &nbsp; &nbsp;
                </td>
                <td align="center" style="height: 30px; text-align: center;" valign="middle">
                    To Date :
                    <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" TabIndex="2" 
                        onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation1()"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field"
                        SetFocusOnError="true" ControlToValidate="txtToDate" ValidationGroup="de"></asp:RequiredFieldValidator>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  
                    <td style="padding-bottom:13px">
                        <asp:Button ID="Button2"  runat="server" Text="Submit" ValidationGroup="de" TabIndex="3" 
                            OnClick="Button_Submit" />
                    </td>
               </td>
            </tr>
             <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblEmptyData" runat="server" Text="" ForeColor="green" Visible="false"
                        Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
    <br />
       
    <table id="Table2" runat="server" cellpadding="0" cellspacing="0" align="center"
            width="60%">
            <%--  Repeater--%>
            
            <tr>
                <td>
                    <asp:Repeater ID="rptErrorInfo" runat="server" OnItemCommand="rptErrorInfo_ItemCommand">
                        <HeaderTemplate>
                            <table style="background-color: #336699; font-size: 11pt; font-family: Arial; font-weight: normal;"
                                cellspacing="1" cellpadding="1" width="100%" align="center">
                                <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center;
                                    height: 20px">
                                    <td align="center" width="20%">
                                        ErrorName
                                    </td>
                                    <td align="center" width="40%">
                                        BLMethodName    
                                    </td>
                                 
                                    <td align="center" width="40%">
                                        ErrorDate
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB; height: 20px; text-align: justify;">
                                <td width="40%">
                                   <asp:Label ID="lblError" runat="server" Text= '<%# Eval("ErrorName")%>' style="overflow:scroll;" width="800px"></asp:Label>
                                </td>
                                <td width="40%">
                                    <%#DataBinder.Eval(Container, "DataItem.BL_MethodName")%>
                                </td>
                   
                                <td width="40%">
                                    <%#DataBinder.Eval(Container, "DataItem.Transdate")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </td>
                
            </tr>
       
        </table>
     <ajaxToolkit:CalendarExtender ID="calFromdate" runat="server" Format="dd/MM/yyyy"
            TargetControlID="txtFromDate">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:CalendarExtender ID="calTodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
        </ajaxToolkit:CalendarExtender>
    </fieldset>

    

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


</asp:Content>


    