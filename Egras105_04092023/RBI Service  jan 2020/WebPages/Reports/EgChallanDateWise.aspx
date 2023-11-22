<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgChallanDateWise.aspx.cs" Inherits="WebPages_EgChallanDateWise"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../App_Themes/Images/progress.gif" />
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
            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="TY-33">
                    <span _ngcontent-c6="" style="color: #FFF">Challan List Date-Wise</span></h2>
                <img src="../../Image/help1.png" style="height: 35px;width: 34px;" data-toggle="tooltip" data-placement="right" Title="" />
            </div>
            <%--<fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px">
                <legend style="color: #336699; font-weight: bold">Challan List Date-Wise</legend>--%>
                <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" colspan="5">
                            <asp:RadioButtonList ID="rblTransactionType" runat="server" RepeatDirection="Horizontal"
                                ForeColor="Teal" BackColor="#66ccff" Height="39px" Width="800px" Font-Bold="true">
                                <asp:ListItem Value="1" Text="Payment Initiative & Bank Response Received Date" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Bank Response Date"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td height="40px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                FromDate : </span></b>&nbsp;
                        </td>
                        <td height="40px">
                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation()"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqFromd" runat="server" ControlToValidate="txtFromDate"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </td>
                        <td height="40px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Todate : </span></b>&nbsp;
                        </td>
                        <td height="40px">
                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation1()"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqTod" runat="server" ControlToValidate="txtToDate"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </td>
                        <td id="trmain" runat="server" visible="false">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="300px" class="chzn-select">
                            </asp:DropDownList>
                        </td>
                        <td height="40px">
                            <asp:Button ID="btnshow" Text="Show" runat="server" OnClick="btnshow_Click" ValidationGroup="de" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: center" height="40px">
                            <asp:Label ID="lblTotRecord" runat="server" ForeColor="#009933" Font-Bold="true"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label ID="lblTotAmt" runat="server" ForeColor="#009900"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5" height="40px">
                            <table style="width: 100%;" id="TABLE2" align="center">
                                <tr id="paging" runat="server" visible='<%#bool.Parse((DLMain.Items.Count!=0).ToString())%>'>
                                    <td>
                                        <center>
                                            <asp:LinkButton ID="lnk_first" runat="server" Text="<< First " OnClick="lnk_first_Click"
                                                Visible="false" Font-Bold="true" Font-Size="Small"></asp:LinkButton>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkpre" runat="server" Text="<< Previous " OnClick="lnkpre_Click"
                                                Visible="false" Font-Bold="true" Font-Size="Small"></asp:LinkButton>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnknext" runat="server" Text=" Next >>" OnClick="lnknext_Click"
                                                Font-Bold="true" Font-Size="Small" Visible="false"></asp:LinkButton>
                                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnk_last" runat="server" Text=" Last>>" OnClick="lnk_last_Click"
                                                Font-Bold="true" Font-Size="Small" Visible="false"></asp:LinkButton>
                                            <asp:Label ID="lblCurrentPage" runat="server" Text="" Width="95%"></asp:Label>
                                        </center>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="" colspan="5" align="center">
                            <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                            <asp:DataList ID="DLMain" runat="server" RepeatColumns="1" OnItemDataBound="DLMain_ItemDataBound">
                                <ItemTemplate>
                                    <fieldset style="width: 95%">
                                        <legend style="color: Green;">GRN :
                                            <%#Eval("GRN")%></legend>
                                        <table cellpadding="2" cellspacing="2" style="font-size: 9pt; font-family: Arial;
                                            padding-bottom: 10px; padding-top: 10px; padding-right: 10px;">
                                            <tr style="padding-bottom: 0px; padding-top: 0px;">
                                                <td valign="top" align="left" style="width: 800px; padding-right: 5px; z-index: 10px;
                                                    border: solid 1px black; height: 218px;">
                                                    <br />
                                                    <b>Bank Name :</b>
                                                    <%#Eval("BankName")%><br />
                                                    <br />
                                                    <b>Ofice Name :</b>
                                                    <%# Eval("office")%><br />
                                                    <br />
                                                    <b>Treasury Name :</b>
                                                    <%#Eval("TreasuryName")%><br />
                                                    <br />
                                                    <b>Challan Date :</b>
                                                    <%#Eval("ChallanDate")%><br />
                                                    <br />
                                                    <b>Year : From : </b>
                                                    <%#Eval("ChallanFromMonth")%>
                                                    <b>To : </b>
                                                    <%#Eval("ChallanToMonth")%>
                                                    <br />
                                                    <br />
                                                    <b>Remitter Name :</b>
                                                    <%#Eval("FullName")%><br />
                                                    <br />
                                                    <b>Address :</b>
                                                    <%#Eval("Address")%><br />
                                                </td>
                                                <td style="vertical-align: top; width: 200px">
                                                    <table width="100%" style="height: 100%; padding-left: 10px; margin-bottom: 0px;
                                                        margin-top: 0px; vertical-align: top; padding-right: 10px; z-index: 10px; border: solid 1px black">
                                                        <asp:Repeater ID="rptBudget" runat="server">
                                                            <HeaderTemplate>
                                                                <tr style="background-color: #336699; text-align: left; height: 20px;">
                                                                    <td>
                                                                        <b>Budget Head </b>
                                                                    </td>
                                                                    <td>
                                                                        <b>Amount </b>
                                                                    </td>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="background-color: #ffffff; text-align: left; height: 20px;">
                                                                    <td>
                                                                        <%#Eval("BudgetHead")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#string.Format("{0:0.00}", Eval("Amount"))%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <tr style="background-color: #336699; text-align: left; height: 20px;">
                                                                    <td>
                                                                        <b>Deduction </b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblded" runat="server" Font-Bold="true" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 5px">
                                                                    </td>
                                                                </tr>
                                                                <tr style="background-color: #336699; text-align: left; height: 20px;">
                                                                    <td>
                                                                        <b>Total Amount </b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbltot" runat="server" Font-Bold="true" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <center>
                                        -------------------------</center>
                                </SeparatorTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
                <ajaxToolkit:CalendarExtender ID="calFromd" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender ID="calToD" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                </ajaxToolkit:CalendarExtender>
            <%--</fieldset>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
