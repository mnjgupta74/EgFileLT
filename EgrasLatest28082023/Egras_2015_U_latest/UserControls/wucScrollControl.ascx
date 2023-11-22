<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucScrollControl.ascx.cs"
    Inherits="UserControls_wucScrollControl" %>
<script>
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

    function dateValidation(FromDate, ToDate) {
        var dtObj = FromDate;
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
            alert('Year cannot be less than 2009')
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

        var dt1 = parseInt(dtStr.substring(0, 2), 10);
        var mon1 = (parseInt(dtStr.substring(3, 5), 10) - 1);
        var yr1 = parseInt(dtStr.substring(6, 10), 10);
        var date1 = new Date(yr1, mon1, dt1); // from Date

        var str2 = new Date();
        var s = str2.format("dd/MM/yyyy");
        var dt2 = parseInt(s.substring(0, 2), 10);
        var mon2 = (parseInt(s.substring(3, 5), 10) - 1);
        var yr2 = parseInt(s.substring(6, 10), 10);
        var date2 = new Date(yr2, mon2, dt2); // Current Date

        //compairing from date with current date
        if (date2 < date1) {
            alert("Fromdate cannot be greater than current date");
            dtObj.value = ""
            return false
        }
        //end

        //compairing from date with To date
        var toDate = ToDate;
        var toDateStr = toDate.value;

        if (toDateStr != null && toDateStr != "") {
            var toDateday = parseInt(toDateStr.substring(0, 2), 10);
            var toDateMonth = (parseInt(toDateStr.substring(3, 5), 10) - 1);
            var toDateYear = parseInt(toDateStr.substring(6, 10), 10);
            var date3 = new Date(toDateYear, toDateMonth, toDateday); // To Date

            if (date3 < date1) {
                alert("Fromdate cannot be greater than to Todate");
                dtObj.value = ""
                return false
            }
        }
        //end
    }


    function dateValidation1(ToDate, FromDate) {
        var dtObj = ToDate;
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


        var dt1 = parseInt(dtStr.substring(0, 2), 10);
        var mon1 = (parseInt(dtStr.substring(3, 5), 10) - 1);
        var yr1 = parseInt(dtStr.substring(6, 10), 10);
        var date1 = new Date(yr1, mon1, dt1); // To Date


        var str2 = new Date();
        var s = str2.format("dd/MM/yyyy");
        var dt2 = parseInt(s.substring(0, 2), 10);
        var mon2 = (parseInt(s.substring(3, 5), 10) - 1);
        var yr2 = parseInt(s.substring(6, 10), 10); // Current Date

        // Compairing To Date with Current Date
        var date2 = new Date(yr2, mon2, dt2);
        if (date2 < date1) {
            alert("Todate cannot be greater than current date");
            dtObj.value = ""
            return false
        }
        //End

        //compairing from date with To date
        var fromDate = FromDate;
        var fromDateStr = fromDate.value;

        if (fromDateStr != null && fromDateStr != "") {
            var fromDateday = parseInt(fromDateStr.substring(0, 2), 10);
            var fromDateMonth = (parseInt(fromDateStr.substring(3, 5), 10) - 1);
            var fromDateYear = parseInt(fromDateStr.substring(6, 10), 10);
            var date3 = new Date(fromDateYear, fromDateMonth, fromDateday); // From Date

            if (date3 > date1) {
                alert("ToDate cannot be lesser than  FromDate");
                dtObj.value = ""
                return false
            }
        }
        //end 
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
                <img src="../../App_Themes/images/progress.gif" alt="Loading..." />
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
        <fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px">
            <legend style="color: #336699; font-weight: bold">Bank-Scroll Report Status</legend>
            <table style="width: 100%" align="center" id="MainTable">
                <tr>
                    <td colspan="4">
                        <div id="divRecord" runat="server" visible="false">
                            <div id="example14" runat="server">
                                <img alt="Error" id="ImageS" src="../../Image/success.png" />
                                <asp:Label ID="LabelS" runat="server" ForeColor="#3ab51c" Text="Match"></asp:Label>
                                &nbsp;&nbsp;
                                <img alt="Error" id="ImageF" src="../../Image/delete.png" />
                                <asp:Label ID="LabelF" runat="server" Text="Mismatch" ForeColor="#bd190a"></asp:Label>&nbsp;&nbsp;
                                <img alt="Error" src="../../Image/doubt_icon.png" id="ImageD" />
                                <asp:Label ID="LabelD" runat="server" Text="Doubted" ForeColor="#3479af"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="white-space: nowrap; font-weight: bold; color: #336699; font-family: Arial CE;
                        font-size: 13px;">
                        From Date:-
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)" 
                           
                            CssClass="borderRadius inputDesign" onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_wucScrollControl1_txtToDate);"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtFromDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    <td style="white-space: nowrap; font-weight: bold; color: #336699; font-family: Arial CE;
                        font-size: 13px;">
                        To Date:-
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)"
                            CssClass="borderRadius inputDesign" onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_wucScrollControl1_txtFromDate);"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    <td style="white-space: nowrap; font-weight: bold; color: #336699; font-family: Arial CE;
                        font-size: 13px;">
                        Select Bank:-
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlbankname" runat="server" Width="239px" CssClass="borderRadius inputDesign">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="right" style="height: 40px">
                        <asp:RadioButtonList ID="rblstatus" runat="server" RepeatDirection="Horizontal" ForeColor="#336699"
                            Font-Bold="true" Width="100%">
                            <%--<asp:ListItem Value="1" Text="Match" Selected="True"></asp:ListItem>--%>
                            <asp:ListItem Value="1" Text="Mismatch Amount" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Not Present In Egras"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not Present In Bank Scroll"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Present But Date Not Match"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClientClick="return ValidateBankScrollDateDiff();"
                            OnClick="btnshow_Click" />
                    </td>
                </tr>
                <tr id="GrandTotalRow" runat="server" visible="false">
                    <td align="left" colspan="6" style="background-color: #FFFFFF">
                        <b>Grand Total: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <b>E-Treasury Amount:</b>&nbsp;&nbsp;
                        <asp:Label ID="lblEAmount" runat="server" Text="" Font-Bold="True" ForeColor="#009900"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <b>Bank Amount:</b>&nbsp;&nbsp;
                        <asp:Label ID="lblBAmount" runat="server" Text="" Font-Bold="True" ForeColor="#009900"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:GridView ID="grdScroll" runat="server" AutoGenerateColumns="False" Font-Names="Verdana"
                            Font-Size="10pt" EmptyDataText="No Record Found" EmptyDataRowStyle-Font-Bold="true"
                            EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                            CellPadding="4" DataKeyNames="Flag" OnRowDataBound="grdScroll_RowDataBound" GridLines="None"
                            OnPageIndexChanging="grdScroll_PageIndexChanging" AllowPaging="True" PageSize="25"
                            ForeColor="#333333" OnRowCommand="grdScroll_RowCommand" ShowFooter="true" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRN">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkGRN" ForeColor="#336699" runat="server" Text='<%# Eval("GRN") %>'
                                            CommandArgument='<%# Eval("GRN") %>' CommandName="View" ToolTip="Click For Challan View"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="E-Treasury Amount">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblETreasuryAmount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scroll Amount">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankAmount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expected E-Treasury Date">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblETreasuryDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scroll Date">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Challan Date">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankChallanDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="CIN" HeaderText="CIN">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="TreasuryAmount" HeaderText="E-Treasury Amount" DataFormatString="{0:n}">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BankAmount" HeaderText="Bank Amount" DataFormatString="{0:n}">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TreasuryDate" HeaderText="Expected E-Treasury Date">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BankDate" HeaderText="Bank Date">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BankChallanDate" HeaderText="Bank Challan Date">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtn" runat="server" CssClass="target" Width="16px" Height="16px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
                                </asp:TemplateField>
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
                    </td>
                </tr>
            </table>
        </fieldset>
        <%--<div id="divTooltip" runat="server">
            <div id="divMatch">
                Record Matched both at Egras and Bank Side.
            </div>
            <div id="DivMisMatch">
                Amount Do not match with data.
            </div>
            <div id="divTooltipData">
                Record do not exists either at Egras side or Bank side.
            </div>
        </div>--%>

        <script type="text/javascript" language="javascript">
        function ValidateBankScrollDateDiff()
        {
            var txtFromDate=document.getElementById('<%=txtFromDate.ClientID %>');
            var txtToDate=document.getElementById('<%= txtToDate.ClientID %>');
            
//            var dtFromDate = txtFromDate != null && txtFromDate.value.trim()!="" ? new Date(txtFromDate.value.trim().replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3")) : "";
//            var dtToDate = txtToDate != null && txtToDate.value.trim()!="" ? new Date(txtToDate.value.replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3")) : "";
            var strFromDate=txtFromDate != null && txtFromDate.value.trim()!="" ?txtFromDate.value.trim():"";
            var strToDate=txtToDate != null && txtToDate.value.trim()!="" ?txtToDate.value.trim():""
            var s=strFromDate;
            
            var dtFromDate=null,dtToDate=null;
            var dt = parseInt(s.substring(0, 2), 10);
            var mon = (parseInt(s.substring(3, 5), 10)-1);
            var yr = parseInt(s.substring(6, 10), 10);
            dtFromDate= new Date(yr, mon, dt);
            
            s=strToDate;
            dt = parseInt(s.substring(0, 2), 10);
            mon = (parseInt(s.substring(3, 5), 10)-1);
            yr = parseInt(s.substring(6, 10), 10);
            dtToDate= new Date(yr, mon, dt);
            
            if(dtFromDate!=null && dtToDate!=null &&  Math.round((dtToDate-dtFromDate)/(1000*60*60*24))>4)
            {
                alert("Day's difference cannot be more then 5 day's");
                return false;
            }
            return true;
        }
                function NumberOnly(evt) {
                    if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
                    var parts = evt.srcElement.value.split('.');
                    if (parts.length > 3) return false;

                    if (evt.keyCode == 46) return (parts.length == 1);

                    if (parts[0].length >= 14) return false;

                    if (parts.length == 3 && parts[1].length >= 3) return false;
                }
                         
        </script>

    </ContentTemplate>
</asp:UpdatePanel>
