<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgTransactionDateWiseRpt.aspx.cs" Inherits="WebPages_EgTransactionDateWiseRpt"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>

    <script type="text/javascript">
        function dateValidation() {
            var dtObj = document.getElementById("<%=txtDate.ClientID %>")

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

            var str2 = new Date();
            var s = str2.format("dd/MM/yyyy");
            var dt1 = parseInt(dtStr.substring(0, 2), 10);
            var mon1 = parseInt(dtStr.substring(3, 5), 10);
            var yr1 = parseInt(dtStr.substring(6, 10), 10);
            var dt2 = parseInt(s.substring(0, 2), 10);
            var mon2 = parseInt(s.substring(3, 5), 10);
            var yr2 = parseInt(s.substring(6, 10), 10);
            //Change
            var date1 = new Date(yr1 + '/' + mon1 + '/' + dt1);
            var date2 = new Date(yr2 + '/' + mon2 + '/' + dt2);
            //        var date1 = (dt1 + '/' + mon1 + '/' + yr1);
            //        var date2 = (dt2 + '/' + mon2 + '/' + yr2);
            //        var date1 = new Date(yr1, mon1, dt1);
            //        var date2 = new Date(yr2, mon2, dt2);
            if (date2 < date1) {
                alert("To date cannot be greater than from current date");
                dtObj.value = ""
                return false
            }
        }
    </script>

    <style type="text/css">
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanal1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
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
    <asp:UpdatePanel ID="updatepanal1" runat="server">
        <ContentTemplate>
            <%--<fieldset style="width: 800px; margin-left: 100px">
                <legend style="color: #336699; font-weight: bold">Date-wise Transaction Report </legend>--%>

            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Date-wise Transaction Report</span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Bank Scroll Report (45-A)" />
            </div>

            <table style="width: 100%" border="1" align="center" cellspacing="0" cellpadding="0">
                <%-- <tr>
                        <td colspan="2" style="text-align: center; height: 40px">
                            <asp:Label ID="Labelheader" runat="server" Text="Date wise Transaction Report" Font-Bold="True"
                                ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
                        </td>
                    </tr>--%>
                <tr>
                    <td align="left">
                        <%-- <asp:Label ID="lbllable" runat="server" Text="Date:-" Font-Bold="true"></asp:Label>--%>
                        <b><span style="color: #336699">Transaction Date :</span></b>&nbsp;
                        <%--</td>
                        <td align="center">&nbsp;--%>
                        <asp:TextBox ID="txtDate" runat="server" MaxLength="10" Height="100%" Style="display: initial !important" CssClass="form-control" 
                                     Width ="120px" onchange="dateValidation();"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="cal" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txtDate" ErrorMessage="Required"
                                                    SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                        <td align="center">
                            <asp:Button ID="btnShow" runat="server" Text="Show" Height="33px" CssClass="btn btn-default" OnClick="btnShow_Click" ValidationGroup="de" />
                        </td>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td style="text-align: center; height: 40px; width: 750px">
                        <div id="rptSchemaDiv" runat="server" visible="false">
                            <%--   <fieldset style="width: auto;">
                                    <legend style="color: #336699;">Transaction-List </legend>--%>
                            <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial; margin-left: 10px; padding-bottom: 10px; padding-top: 10px; width: 700px;">
                                <tr>
                                    <td colspan="2" style="font-size: 15; height: 40px">
                                        <b><span style="width: 300px; color: #336699; font-weight: normal">Total Number Of Challan :-</span></b>&nbsp;
                                            <asp:Label ID="lblChallan" runat="server" ForeColor="#336699" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td colspan="2" style="font-size: 15; height: 40px">
                                        <b><span style="width: 300px; color: #336699; font-weight: normal">Total Amount :-</span></b>&nbsp;
                                            <asp:Label ID="lblAmount" runat="server" ForeColor="#336699" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptschema" runat="server">
                                    <HeaderTemplate>
                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center; height: 20px">
                                            <td style="color: White; width: 100px">Sr.No
                                            </td>
                                            <td style="color: White;">Bank Name
                                            </td>
                                            <td style="color: White;">Number Of Challan
                                            </td>
                                            <td style="color: White;">Total Amount
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: #EFF3FB; height: 20px;">
                                            <td align="center" style="font-size: 15;">
                                                <%# Container.ItemIndex+1 %>
                                            </td>
                                            <td align="left" style="font-size: 15; text-align: left; padding-left: 30px;">
                                                <%# Eval("BankName")%>
                                            </td>
                                            <td align="center" style="font-size: 15; text-align: left; padding-left: 30px;">
                                                <%# Eval("NoOfChallan")%>
                                            </td>
                                            <td align="center" style="font-size: 15; text-align: right; padding-right: 30px;">
                                                <%# string.Format("{0:0.00}",Eval("Amount"))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                            <td align="center" style="font-size: 15;">
                                                <%# Container.ItemIndex+1 %>
                                            </td>
                                            <td align="left" style="font-size: 15; text-align: left; padding-left: 30px;">
                                                <%# Eval("BankName")%>
                                            </td>
                                            <td align="center" style="font-size: 15; text-align: left; padding-left: 30px;">
                                                <%# Eval("NoOfChallan")%>
                                            </td>
                                            <td align="center" style="font-size: 15; text-align: right; padding-right: 30px;">
                                                <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
            <%--   </fieldset>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
