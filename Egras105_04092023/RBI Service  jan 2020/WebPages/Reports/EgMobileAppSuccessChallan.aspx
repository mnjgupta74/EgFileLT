<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgMobileAppSuccessChallan.aspx.cs" Inherits="WebPages_Reports_EgMobileAppSuccessChallan" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
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

            var dtObj = document.getElementById("<%=txtfromDate.ClientID %>")

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
             var dtObj = document.getElementById("<%=txttoDate.ClientID %>")

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


            var fdate = document.getElementById("<%=txtfromDate.ClientID %>")
            var tdate = document.getElementById("<%=txttoDate.ClientID %>")
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanal1"
        DisplayAfter="0">
          <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../App_Themes/Images/progress.gif" />
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
            <fieldset style="">
                <legend style="color: #336699; font-weight: bold">Successfull Challan Report </legend><br /><br /><br />
                <table cellspacing="0" cellpadding="0" style="width:100%" >
                    <tr>
                        
                         <td align="center">
           <b>  &nbsp;<span style="width: 300px; color: #336699; font-family:Arial CE; font-size: 13px"> From Date:- </span></b> 
                        </td>
                          <td align="center">
                            &nbsp;
                            <asp:TextBox ID="txtfromDate" runat="server" MaxLength="10" 
                                Width="100px" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation()"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromDate" ErrorMessage="Required"
                                SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator> </td>
                        <td align="center">
                            <b><span style="width: 300px; color: #336699; font-family:Arial CE; font-size: 13px"> To Date:- </span></b> &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                           
                            <asp:TextBox ID="txttoDate" runat="server" MaxLength="10" 
                                Width="100px" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation1()"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txttoDate" ErrorMessage="Required"
                                SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                           
                            &nbsp; &nbsp; &nbsp; &nbsp; 
                           
                            
                            
                        </td>
                        <td style="text-align:right;"> <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="de" /></td>
                        <td style="text-align:center;"><asp:Button ID="btnGeneratePDF" runat="server" Text="Generate PDF" OnClick="btnGeneratePDF_Click" ValidationGroup="de" /></td>
                             
                    </tr>
                </table>
                <ajaxToolkit:CalendarExtender ID="calFromdate" runat="server" Format="dd/MM/yyyy"
            TargetControlID="txtfromDate">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:CalendarExtender ID="calTodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txttoDate">
        </ajaxToolkit:CalendarExtender>
                <table style="width:100%;">
                    <tr>
                        <td style="text-align: center; height: 30px;">
                            <div id="rptSchemaDiv" runat="server" visible="false">
                                <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial;
                                   width:100%; padding:20px;">
                                    <tr>
                                        <td colspan="2" style="font-size: 15px; height: 40px; text-align:left;">
                                            <b><span style="color: #336699;font-weight:normal">Total Number Of GRN :-</span></b>&nbsp;
                                            <asp:Label ID="lblGrn" runat="server" ForeColor="#336699" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td colspan="4" style="font-size: 15px; height: 40px;text-align:right"><b><span style="width: 300px; color: #336699;font-weight:normal">Total Amount :-</span></b>&nbsp;
                                                <asp:Label ID="lblAmount" runat="server" Font-Bold="true" ForeColor="#336699"></asp:Label>
                                            </td>
                                    </tr>
                                    <caption>
                                        
                                        <tr>
                                            
                                        </tr>
                                        <asp:Repeater ID="rptschema" runat="server">
                                            <HeaderTemplate>
                                                <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center;
                                                height: 20px">
                                                    <td style="color: White; width: 100px">Sr.No </td>
                                                    <td style="color: White;">GRN </td>
                                                    <td style="color: White;">BankName </td>
                                                    <td style="color: White;">BankDate </td>
                                                    <td style="color: White;">Amount </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="background-color: #EFF3FB; height: 20px;">
                                                    <td align="center" style="font-size: 15px;"><%# Container.ItemIndex+1 %></td>
                                                    <td align="left" style="font-size: 15px; text-align: left; padding-left: 30px;"><%# Eval("Grn")%></td>
                                                    <td align="left" style="font-size: 15px; text-align: left; padding-left: 30px;"><%# Eval("BankName")%></td>
                                                    <td align="left" style="font-size: 15px; text-align: left; padding-left: 30px;"><%# DataBinder.Eval(Container.DataItem, "BankDate")%></td>
                                                    <td align="center" style="font-size: 15px; text-align: left; padding-left: 30px;"><%# Eval("AMOUNT")%></td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                                    <td align="center" style="font-size: 15px;"><%# Container.ItemIndex+1 %></td>
                                                    <td align="left" style="font-size: 15px; text-align: left; padding-left: 30px;"><%# Eval("Grn")%></td>
                                                    <td align="center" style="font-size: 15px; text-align: left; padding-left: 30px;"><%# Eval("BankName")%></td>
                                                    <td align="left" style="font-size: 15px; text-align: left; padding-left: 30px;"><%# DataBinder.Eval(Container.DataItem, "BankDate")%></td>
                                                    <td align="left" style="font-size: 15px; text-align: left; padding-left: 30px;"><%# Eval("Amount")%></td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                    </caption>
                                </table>
            
            </div> </td> </tr> </table>
                <rsweb:ReportViewer ID="rptManualSuccessDivisionWiserpt" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false"></rsweb:ReportViewer>
            <%--   </fieldset>--%>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

