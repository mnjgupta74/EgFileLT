<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgMEChallanDetails.aspx.cs" Inherits="WebPages_Reports_EgMEChallanDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function NumberOnly(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;

            if (evt.keyCode == 46) return (parts.length == 1);

            if (parts[0].length >= 14) return false;

            if (parts.length == 3 && parts[1].length >= 3) return false;
        }
        function DisplayTable() {
            document.getElementById("trRevenue").style.display = "none";
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

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background ">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../App_Themes/images/progress.gif" />
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
            <fieldset style="width: 1000px; text-align: center">
                <legend style="color: #336699;">ME-Challan Report </legend>
                <table style="width: 100%" cellpadding="0" cellspacing="0" align="center">
                    <tr>
                        <td height="40px">
                            <b><span style="width: 300px; color: #336699">From Date:-</span></b>&nbsp;
                        </td>
                        <td height="40px">
                            <asp:TextBox ID="txtFromDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation()" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </td>
                        <td height="40px">
                            <b><span style="width: 300px; color: #336699">To Date:-</span></b>&nbsp;
                        </td>
                        <td height="40px">
                            <asp:TextBox ID="txtToDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation1()" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                        </td>
                    </tr>
                </table>
                <center>
                    <asp:GridView ID="grdME" runat="server" AutoGenerateColumns="false" BackColor="#EFF3FB"
                        AllowPaging="true" PageSize="50" OnPageIndexChanging="grdME_PageIndexChanging"
                        Width="85%" OnRowCommand="grdME_onRowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                        <HeaderStyle BackColor="#507CD1" Font-Italic="false" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="GRN">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGRN" runat="server" Text='<%# Eval("GRN") %>' CommandArgument='<%# Eval("GRN") %>'
                                        CommandName="lnkGetGRN"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField HeaderText="GRN" DataField="GRN" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:BoundField HeaderText="officename" DataField="Officename" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="DDOCode" DataField="DDOCode" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                    </asp:GridView>
                </center>
                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                    Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                    Format="dd/MM/yyyy" TargetControlID="txtToDate">
                </ajaxToolkit:CalendarExtender>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
