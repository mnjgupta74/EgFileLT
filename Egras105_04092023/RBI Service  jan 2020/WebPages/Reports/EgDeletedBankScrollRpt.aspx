<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgDeletedBankScrollRpt.aspx.cs" Inherits="WebPages_Reports_EgDeletedBankScrollRpt"
    Title="Untitled Page" %>

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
                tdate.value = ""
            }
        }
    </script>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../Image/waiting_process.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <fieldset runat="server" id="lstrecord" style="width: 100%;">
                <legend style="color: #336699; font-weight: bold">Deleted Bank Scroll</legend>
                <table style="width: 100%" align="center" id="MainTable">
                    <tr style="height: 45px">
                        <td align="center">
                            <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation()"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                                Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation1()"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                                Format="dd/MM/yyyy" TargetControlID="txtToDate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            Bank:<asp:DropDownList runat="server" ID="ddlBank">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnShow" runat="server" ValidationGroup="de" Text="Show" OnClick="btnShow_Click" />
                        </td>
                    </tr>
                    <tr align="center" id="trrpt2" runat="server">
                        <td colspan="3">
                        <asp:GridView ID="rptrScrollData" runat="server" AutoGenerateColumns="False" Width="100%"
                                Font-Names="Verdana" Font-Size="10pt" EmptyDataText="No Record Found" EmptyDataRowStyle-Font-Bold="true"
                                EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                                CellPadding="4" GridLines="None" OnPageIndexChanging="grdScroll_PageIndexChanging"
                                AllowPaging="True" PageSize="25" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="GRN" HeaderText="GRN">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount"  DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RemitterName" HeaderText="Remitter Name">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CIN" HeaderText="CIN">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ref" HeaderText="Ref">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:M-dd-yyyy}">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                         <%--   <asp:Repeater ID="rptrScrollData" runat="server">
                                <HeaderTemplate>
                                    <table border="1" width="100%" cellpadding="0" cellspacing="0">
                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px">
                                            <th>
                                                <b>Sr.No</b>
                                            </th>
                                            <th>
                                                <b>GRN</b>
                                            </th>
                                            <th>
                                                <b>Amount</b>
                                            </th>
                                            <th>
                                                <b>Remitter Name</b>
                                            </th>
                                            <th>
                                                <b>CIN</b>
                                            </th>
                                            <th>
                                                <b>Reference</b>
                                            </th>
                                            <th>
                                                <b>Transaction Date</b>
                                            </th>
                                            <%--<th>
                                                <b>Bank Date</b>
                                            </th>
                                            <th>
                                                <b>Complete Head</b>
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem,"ROWNO")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem,"GRN")%>
                                        </td>
                                        <td align="right">
                                            <%# DataBinder.Eval(Container.DataItem,"Amount")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "RemitterName")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "CIN")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "Ref")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "TransDate", "{0:d}")%>
                                        </td>
                                        <%--<td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "BankDate", "{0:d}")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "CompleteHead")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem,"ROWNO")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem,"GRN")%>
                                        </td>
                                        <td align="right">
                                            <%# DataBinder.Eval(Container.DataItem,"Amount")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "RemitterName")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "CIN")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "Ref")%>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "TransDate", "{0:d}")%>
                                        </td>
                                        
                                    </tr>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>--%>
                        </td>
                    </tr>
                    <tr align="center" id="trrpt" runat="server">
                        <td colspan="3">
                            <asp:Repeater ID="rptrDeletedScrollDateWise" runat="server">
                                <HeaderTemplate>
                                    <table border="1" width="80%" cellpadding="0" cellspacing="0">
                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px">
                                            <th>
                                                <b>Scroll Date</b>
                                            </th>
                                            <th>
                                                <b>Deleted On</b>
                                            </th>
                                            <th>
                                                <b>View</b>
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:Label runat="server" ID="lblScrollDate" Text='<%# DataBinder.Eval(Container.DataItem,"ScrollDate")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "DeletedOn")%>
                                        </td>
                                        <td align="center">
                                            <asp:LinkButton ID="linkbtnView" Text="View" runat="server" OnCommand="rptrDeletedScrollDateWise_View"
                                                CommandName='<%# DataBinder.Eval(Container.DataItem, "DeletedOn")%>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ScrollDate")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:Label runat="server" ID="lblScrollDate" Text='<%# DataBinder.Eval(Container.DataItem,"ScrollDate")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <%# DataBinder.Eval(Container.DataItem, "DeletedOn")%>
                                        </td>
                                        <td align="center">
                                            <asp:LinkButton ID="linkbtnView" Text="View" runat="server" OnCommand="rptrDeletedScrollDateWise_View"
                                                CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ScrollDate")%>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text="No Data Found" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
