<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="PdAccListTreasuryWise.aspx.cs" Inherits="WebPages_Reports_PdAccListTreasuryWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/UserControls/CommonFromDateToDate.ascx" TagName="FromToDate" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        #ctl00_ContentPlaceHolder1_BtnProcessPd {
            font-size: Small;
            height: 30px;
            background-color: antiquewhite;
            font-weight: 600;
        }

        #ctl00_ContentPlaceHolder1_BtnProcessPdHeadWise {
            font-size: Small;
            height: 30px;
            background-color: antiquewhite;
            font-weight: 600;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            var treasury = document.getElementById("<%=txtfromdatebank.ClientID %>").value
            if (treasury != "") {
                GetTreasuryList();
            }
            FillToDate();
            var rblTypeOfBusiness = $("[id$='rblheadpd']").find(":checked").val();
            ShowHide(rblTypeOfBusiness);
            <%--$('#<%= rblheadpd.ClientID %>').click(function () {
                
                
                var rblTypeOfBusiness = $("[id$='rblheadpd']").find(":checked").val();

                ShowHide(rblTypeOfBusiness);
                $("[id$='rblheadpd']").find(":checked").attr('css', 'background-color: blue') //.css({ "background-color": "blue" });
            });--%>
            $('#<%= rblheadpd.ClientID %>').click(function () {
                $(this)
                .prev().css('color', 'red')
                .siblings().css('color', 'black');
            });

        });

        function FillToDate() {
            document.getElementById("<%=txtTodatebnk.ClientID %>").value = document.getElementById("<%=txtfromdatebank.ClientID %>").value
        }



        function
            ShowHide(rblTypeOfBusiness) {
            if (rblTypeOfBusiness == 1) {
                document.getElementById("<%=divrpt1.ClientID %>").style.display = "block";
                 document.getElementById("<%=divrpt2.ClientID %>").style.display = "none";
             }
             else if (rblTypeOfBusiness == 2) {
                 document.getElementById("<%=divrpt1.ClientID %>").style.display = "none";
                document.getElementById("<%=divrpt2.ClientID %>").style.display = "block";
            }
            else {
                document.getElementById("<%=divrpt1.ClientID %>").style.display = "block";
                document.getElementById("<%=divrpt2.ClientID %>").style.display = "none";
            }
    }


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
        var date1 = new Date(yr1, mon1-1, dt1);
        var date2 = new Date(yr2, mon2-1, dt2);
        if (date2 < date1) {
            alert("To date cannot be greater than from current date");
            dtObj.value = ""
            return false
        }
        FillToDate();
        GetTreasuryList();
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
    function GetDDLValue() {
        var value = $('select#<%=ddlTreasury.ClientID %> option:selected').val();
            $('#<%=hdnVal.ClientID%>').val(value);
        }
        function GetTreasuryList() {
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/Reports/PdAccListTreasuryWise.aspx/GetTreasuryList") %>',
                data: '{"FromDate":"' + document.getElementById("<%=txtfromdatebank.ClientID %>").value + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    var myOptions = JSON.parse(msg.d);
                    $("#ctl00_ContentPlaceHolder1_ddlTreasury").empty();
                    $("#ctl00_ContentPlaceHolder1_ddlTreasury").append($("<option></option>").val('0').html('-- Select Treasury --'));
                    $.each(myOptions, function (data, value) {
                        $("#ctl00_ContentPlaceHolder1_ddlTreasury").append($("<option></option>").val(value.TreasuryCode).html(value.TreasuryName));
                    });
                    var hdnTreasury = document.getElementById("<%=hdnVal.ClientID %>").value
                    if (hdnTreasury != "") {
                        $("#<%=ddlTreasury.ClientID %>").val(hdnTreasury);
                    }
                }
            });

    }
    </script>

    <fieldset>
        <asp:HiddenField ID="hdnVal" runat="server" />
        <legend style="color: #336699; font-weight: bold">PD Account List Treasury Wise</legend>
        <table style="width: 100%" align="center" id="MainTable">
            <tr style="height: 45px;">
                <td></td>
                <td></td>
                <td align="left">
                    <asp:RadioButtonList runat="server" ID="rblheadpd" RepeatDirection="Horizontal">
                        <asp:ListItem Text="HeadWise" Value="2" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="PdAccWise" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>

            <tr style="height: 45px">
                <td>
                    <b><span style="color: #336699; font-size: 14px">From Date:-</span></b>
                    <asp:TextBox ID="txtfromdatebank" runat="server" Width="120px" Height="17px" onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation()"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                        Format="dd/MM/yyyy" TargetControlID="txtfromdatebank">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtfromdatebank" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdatebank"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <b><span style="color: #336699; font-size: 14px">To Date:-</span></b>
                    <asp:TextBox ID="txtTodatebnk" runat="server" Width="120px" Height="17px" onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation1()"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtTodatebnk"
                        Format="dd/MM/yyyy" TargetControlID="txtTodatebnk">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtTodatebnk" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTodatebnk"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                </td>
                <td align="center">
                    <b>
                        <asp:Label ID="lblpdacc" runat="server" Text="Treasury:-" Style="color: #336699"></asp:Label></b>
                    <asp:DropDownList ID="ddlTreasury" runat="server" Width="150px" Height="22px" AutoPostBack="false" >
                        <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                        <asp:ListItem Value="0100" Text="Ajmer"></asp:ListItem>
                        <asp:ListItem Value="0200" Text="ALWAR"></asp:ListItem>
                        <asp:ListItem Value="0300" Text="BANSWARA"></asp:ListItem>
                        <asp:ListItem Value="0400" Text="BARAN"></asp:ListItem>
                        <asp:ListItem Value="0500" Text="BARMER"></asp:ListItem>
                        <asp:ListItem Value="0600" Text="BARMER"></asp:ListItem>
                        <asp:ListItem Value="0700" Text="BHARATPUR"></asp:ListItem>
                        <asp:ListItem Value="0800" Text="BHILWARA"></asp:ListItem>
                        <asp:ListItem Value="0900" Text="BIKANER"></asp:ListItem>
                        <asp:ListItem Value="1000" Text="BUNDI"></asp:ListItem>
                        <asp:ListItem Value="1100" Text="CHITTORGARH"></asp:ListItem>
                        <asp:ListItem Value="1200" Text="CHURU"></asp:ListItem>
                        <asp:ListItem Value="1300" Text="DAUSA"></asp:ListItem>
                        <asp:ListItem Value="1400" Text="DHOLPUR"></asp:ListItem>
                        <asp:ListItem Value="1500" Text="DUNGARPUR"></asp:ListItem>
                        <asp:ListItem Value="1600" Text="GANGANAGAR"></asp:ListItem>
                        <asp:ListItem Value="1700" Text="HANUMANGARH"></asp:ListItem>
                        <asp:ListItem Value="1800" Text="JAIPUR (CITY)"></asp:ListItem>
                        <asp:ListItem Value="2000" Text="JAIPUR (RURAL)"></asp:ListItem>
                        <asp:ListItem Value="2100" Text="JAIPUR (SECTT.)"></asp:ListItem>
                        <asp:ListItem Value="2200" Text="JAISALMER"></asp:ListItem>
                        <asp:ListItem Value="2300" Text="JALORE"></asp:ListItem>
                        <asp:ListItem Value="2400" Text="JHALAWAR"></asp:ListItem>
                        <asp:ListItem Value="2500" Text="JHUNJHUNU"></asp:ListItem>
                        <asp:ListItem Value="2600" Text="JODHPUR (CITY)"></asp:ListItem>
                        <asp:ListItem Value="2700" Text="JODHPUR (RURAL)"></asp:ListItem>
                        <asp:ListItem Value="2800" Text="KAROLI"></asp:ListItem>
                        <asp:ListItem Value="2900" Text="KOTA"></asp:ListItem>
                        <asp:ListItem Value="3000" Text="NAGAUR"></asp:ListItem>
                        <asp:ListItem Value="3100" Text="PALI"></asp:ListItem>
                        <asp:ListItem Value="3200" Text="PRATAPGARH"></asp:ListItem>
                        <asp:ListItem Value="3300" Text="RAJSAMAND"></asp:ListItem>
                        <asp:ListItem Value="3400" Text="SAWAI MADHOPUR"></asp:ListItem>
                        <asp:ListItem Value="3500" Text="SIKAR"></asp:ListItem>
                        <asp:ListItem Value="3600" Text="SIROHI"></asp:ListItem>
                        <asp:ListItem Value="3700" Text="TONK"></asp:ListItem>
                        <asp:ListItem Value="3800" Text="UDAIPUR"></asp:ListItem>
                        <asp:ListItem Value="4100" Text="UDAIPUR RURAL"></asp:ListItem>
                        <asp:ListItem Value="3900" Text="NEW DELHI"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div id="divmjrhead" runat="server" visible="false">
                        <b>
                            <asp:Label ID="lblMjorHead" runat="server" Text="Major Head:-" Style="color: #336699"></asp:Label></b>
                        <asp:DropDownList ID="ddlMajorHead" runat="server" Width="125px" Height="22px">
                            <asp:ListItem Value="0" Text="-- Select Head --"></asp:ListItem>
                            <asp:ListItem Value="8443" Text="8443" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td align="center">
                    <asp:Button ID="btnFindResult" runat="server" Text="Show" ValidationGroup="de" OnClientClick="GetDDLValue()" OnClick="btnFindResult_Click" />

                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <div id="divrpt1" runat="server">
        <table width="100%">
            <tr runat="server" id="trlbltotamt" visible="false">
                <td></td>
                <td style="text-align: right">
                    <asp:Button ID="BtnProcessPd" runat="server" Text="Process to PD" OnClick="BtnProcessPd_Click" />
                </td>
                <td height="40px" style="text-align: right">
                    <b><span style="width: 300px; color: #336699">Total Amount(in Rs):-</span></b>&nbsp;
                </td>
                <td height="40px">
                    <asp:Label ID="LblTotAmt" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table border="1" width="100%" cellpadding="0" cellspacing="0">
            <asp:Repeater ID="rptpdacctdetail" runat="server" OnItemCommand="rptpdacctdetail_ItemCommand">
                <HeaderTemplate>
                    <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px;">
                        <th align="center">
                            <b>S.No</b>
                        </th>
                        <th align="center">
                            <b>BudgetHead</b>

                        </th>
                        <th align="center">
                            <b>PdAcc</b>

                        </th>

                        <th align="center">
                            <b>Amount</b>
                        </th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#  Eval("BudgetHead")%>
                        </td>
                        <td align="center">
                            <%#  Eval("pdacc")%>
                        </td>

                        <td align="right">
                            <asp:LinkButton ID="Linkamt" runat="server" CausesValidation="false" CommandName="PDTotlamt"
                                CommandArgument='<%# Eval("pdacc")%>' Text='<%# Eval("Amount", "{0:0.00}")%>'></asp:LinkButton>
                        </td>

                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#  Eval("BudgetHead")%>
                        </td>
                        <td align="center">
                            <%#  Eval("pdacc")%>
                        </td>

                        <td align="right">
                            <asp:LinkButton ID="Linkamt" runat="server" CausesValidation="false" CommandName="PDTotlamt"
                                CommandArgument='<%# Eval("pdacc")%>' Text='<%# Eval("Amount", "{0:0.00}")%>'></asp:LinkButton>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
    </div>

    <div id="divrpt2" runat="server">
        <table width="100%">
            <tr runat="server" id="trlbltotamtHeadWise" visible="false">
                <td></td>
                <td style="text-align: right">
                    <asp:Button ID="BtnProcessPdHeadWise" runat="server" Text="Process to PD" OnClick="BtnProcessPd_Click" />
                </td>
                <td height="40px" style="text-align: right">
                    <b><span style="width: 300px; color: #336699">Total Amount(in Rs):-</span></b>&nbsp;
                </td>
                <td height="40px">
                    <asp:Label ID="LblTotAmtHeadWise" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table border="1" width="100%" cellpadding="0" cellspacing="0">
            <asp:Repeater ID="rptheadwisedetail" runat="server" OnItemCommand="rptheadwisedetail_ItemCommand">
                <HeaderTemplate>

                    <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px;">
                        <th align="center">
                            <b>S.No</b>
                        </th>
                        <th align="center">
                            <b>Budget Head</b>
                        </th>
                        <th align="center">
                            <b>Amount</b>
                        </th>

                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#  Eval("budgethead")%>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="Linkamt" runat="server" CausesValidation="false" CommandName="HeadTotlamt"
                                CommandArgument='<%# Eval("budgethead")%>' Text='<%# Eval("Amount", "{0:0.00}")%>'></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#  Eval("budgethead")%>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="Linkamt" runat="server" CausesValidation="false" CommandName="HeadTotlamt"
                                CommandArgument='<%# Eval("budgethead")%>' Text='<%# Eval("Amount", "{0:0.00}")%>'></asp:LinkButton>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
    </div>




</asp:Content>
