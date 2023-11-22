<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAgSignedPdf.aspx.cs" Inherits="WebPages_Reports_EgAgSignedPdf" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/chosen.jquery.min.js"></script>
    <link href="../../CSS/chosen.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function isDate(objField) {
            var datePat = /^(\d{1,2})(\/|-)(\d{1,2})\2(\d{2}|\d{4})$/;
            var strDate = objField.value;
            var matchArray = strDate.match(datePat);

            if (matchArray == null) {
                objField.value = "";

                return false;
            }
            // matchArray[0] will be the original entire string, for example, 4-12-02 or 4/12/2002
            var month = matchArray[3];     // (\d{1,2}) - 1st parenthesis set - 4
            var day = matchArray[1];         // (\d{1,2}) - 3rd parenthesis set - 12
            var year = matchArray[4];        // (\d{2}|\d{4}) - 5th parenthesis set - 02 or 2002   

            if (month < 1 || month > 12) {
                objField.value = "";
                alert("Not Valid month in date format");
                return false;
            }
            if (day < 1 || day > 31) {
                objField.value = "";
                alert("Not Valid days in date format, Because Should be days is not less than 1 and not Grater than 31");
                return false;
            }
            if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
                objField.value = "";
                alert("Not Valid Date format, Because this month is 30 days");
                return false;
            }
            if (month == 2) {
                var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));

                if (day > 29 || (day == 29 && !isleap)) {
                    objField.value = "";
                    alert("Not Valid Date format, Because this Year is Leep year");
                    return false;
                }
            }

            if ((month.length) < 2) {
                objField.value = "";
                alert("Not Valid Month format, Please Enter Month (Date Format DD/MM/YYYY, 01/09/2012)");
                return false;
            }
            if ((year.length) < 4) {
                objField.value = "";
                alert("Not Valid year format, Please Enter Valid Year (Date Format DD/MM/YYYY, 01/09/2012)");
                return false;
            }
            if (day.length < 2) {
                objField.value = "";
                alert("Not Valid day in date format : (Date Format DD/MM/YYYY, 01/09/2012)");
                return false;
            }

            return true;
        }

    </script>
    <%--==================CSS-JQUERY LOADER==================--%>
    <style type="text/css">
        #cover-spin {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            display: none;
        }

        #ctl00_ContentPlaceHolder1_RadioButtonList1_0 {
            margin-left: 150px;
        }

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

        #btnSavePDF .btn-default {
            color: #f4f4f4 !important;
            background-color: #274869 !important;
        }

        #cover-spin::after {
            content: '';
            display: block;
            position: absolute;
            left: 48%;
            top: 40%;
            width: 40px;
            height: 40px;
            border-style: solid;
            border-color: black;
            border-top-color: transparent;
            border-width: 4px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }
    </style>
    <div id="cover-spin"></div>
    <script type="text/javascript">

        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnshow"]').click(function () {
                if ($(<%=txtfromdate.ClientID%>).val() != "" && $(<%=txttodate.ClientID%>).val() != "") {
                    $('#cover-spin').show(0);

                }
            });

            
            
        });
        function enableDisable() {
            var dtObj = document.getElementById("<%=txtfromdate.ClientID %>")
             if (type == "2") {
                 document.getElementById("<%=txttodate.ClientID %>").value = dtObj.value;
                 //document.getElementById("<%=txttodate.ClientID %>").disabled = true;
             }
             else {
                 //document.getElementById("<%=txttodate.ClientID %>").disabled = false;
             }
            <%--var type = $('#<%=rblFileType.ClientID %> input:checked').val()
             if (type == "2") {
                 document.getElementById("<%=txttodate.ClientID %>").value = dtObj.value;
                 document.getElementById("<%=txttodate.ClientID %>").disabled = true;
             }
             else {
                 document.getElementById("<%=txttodate.ClientID %>").disabled = false;
             }--%>
         }
        function dateValidation(FromDate, ToDate) {
            var dtObj = document.getElementById("<%=txtfromdate.ClientID %>")
             //var strAA = new Date();
             //var sA = strAA.format("dd/MM/yyyy");
             var type = $('#<%=rblFileType.ClientID %> input:checked').val()
            if (type == "2") {
                //var txtVal = $(this).val();
                <%--$('<%=txttodate.ClientID %>').val(dtObj);--%>
                 document.getElementById("<%=txttodate.ClientID %>").value = dtObj.value;
                 document.getElementById("<%=txttodate.ClientID %>").disabled = true;
             }
             else {
                 document.getElementById("<%=txttodate.ClientID %>").disabled = false;
             }
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
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>
    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Ag Report</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Ty-33" />
    </div>
    <table align="center" style="width: 100%" border="1">

        <tr>
            <td id="tdddlReportName" runat="server">
                <b><span style="color: #336699">Report : </span></b>&nbsp;
                    <asp:DropDownList ID="ddlReportName" Style="display: initial !important; margin-left: 10px;" CssClass="form-control chzn-select"
                        runat="server" Width="200px" TabIndex="1" ToolTip="Select Report name" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlReportName_SelectedIndexChanged">
                    </asp:DropDownList>
            </td>
            <td align="center" colspan="2">
                <asp:RadioButtonList ID="rblFileType" Style="border: 0px;" class="form-control" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="rblFileType_SelectedIndexChanged" onChange="enableDisable()">
                    <asp:ListItem Text="Monthly Account" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Daily Account" Value="2" style="margin-left: 20px"></asp:ListItem>
                </asp:RadioButtonList>

            </td>

        </tr>

        <tr runat="server" id="trReport">
            <td>
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                <asp:TextBox ID="txtfromdate" Height="100%" Style="display: initial !important; margin-left: 6px;" CssClass="form-control" runat="server" Width="120px" TabIndex="1"
                    onpaste="return false" onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)"
                    onblur="isDate(this);"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td>
                <b><span style="color: #336699">To Date : </span></b>&nbsp;
                <asp:TextBox ID="txttodate" Height="100%" Style="display: initial !important" CssClass="form-control" runat="server" Width="120px" TabIndex="2"
                    onpaste="return false" onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)" onblur="isDate(this);"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txttodate" TargetControlID="txttodate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txttodate"
                    ErrorMessage="" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td id="tdMajorHead" runat="server" visible="false">
                <b><span style="color: #336699">Major Head : </span></b>&nbsp;
                <asp:TextBox CssClass="form-control" Height="100%" Style="display: initial !important" ID="txtFMajorHead" MaxLength="4"
                    runat="server"
                    Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        </tr>
        <tr id="trCoveringLetter" runat="server" visible="false">
            <td class="col-md-4">
                <b><span style="color: #336699">Select Month : </span></b>&nbsp;
                <asp:DropDownList ID="ddlstartmonth" runat="server" Width="150px"
                    Style="display: initial !important" CssClass="form-control chzn-select">
                    <asp:ListItem Value="0"> --Select--</asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2"> February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October </asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="col-md-4">
                <b><span style="color: #336699">Select Year</span></b>&nbsp;
                <asp:DropDownList ID="ddlyear" Width="150px" runat="server" Style="display: initial !important" CssClass="form-control chzn-select"></asp:DropDownList>
            </td>
            <td class="col-md-4">
                <b><span style="color: #336699">List Of Account</span></b>&nbsp;
                <asp:RadioButtonList ID="rbllisttype" RepeatDirection="Horizontal" runat="server" Style="width: 70% !important; display: contents !important" CssClass="form-control">
                    <asp:ListItem Value="1" style="margin-right: 35px" Selected="True">1st </asp:ListItem>
                    <asp:ListItem Value="2">2nd </asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Button ID="btnshow" runat="server" Height="33" CssClass="btn btn-default pull-right" Text="Show" OnClick="btnshow_Click" ValidationGroup="a" />

                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnReset" runat="server" Height="33" CssClass="btn btn-default pull-left" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />

                    </div>
                </div>
            </td>
            <td colspan="2">
                <div class="row">
                    <div class="col-md-12">
                        <asp:Button ID="btnSavePDF" Visible="false" runat="server" Style="color: #f4f4f4 !important; background-color: #274869 !important;"
                            Height="33" Width="160px" CssClass="btn btn-default pull-left" Text="Save PDF for eSign" ValidationGroup="de" OnClick="btnSavePDF_Click" />
                    </div>
                </div>
            </td>


        </tr>




        <tr runat="server" id="trrpt" visible="false">
            <td colspan="3">
                <center>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
    </table>
    <div id="Div2" visible="false" runat="server" style="border: thin solid #000000; width: 100%">
        <table align="center" style="width: 90%">
            <tr>
                <td align="right" style="font-size: smaller">
                    <b>ANNEXURE - C</b>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-size: large">
                    <b>Government of Rajasthan </b>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <b><span id="Treasuryname" style="font-size: large;" runat="server">E-Treasury Office,
                        Jaipur</span> </b>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 20px; font-size: larger;">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">Date :<b> <span id="datee" runat="server">date</span></b>
                </td>
            </tr>
            <tr>
                <td align="left">No. : eGRAS/A.G./Account/<span id="Finyear" runat="server"></span>/
                    <asp:Image ID="image1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left " style="height: 20px">
                    <br />
                    <br />
                    The Principal Accountant General,
                </td>
            </tr>
            <br />
            <tr>
                <td align="left ">(Accounts & Entitlement),<br />
                    Rajasthan, Jaipur.
                </td>
            </tr>
            <tr>
                <td align="left " style="width: 386px">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 32px;">
                    <b>Subject :- Submission of &nbsp;<span id="list2" runat="server"></span> list of Accounts
                        for the month of <span id="month" runat="server"></span></b>
                </td>
            </tr>
            <tr>
                <td align="left " style="width: 386px">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left " style="width: 386px">Sir/Madam,
                </td>
            </tr>
            <tr>
                <td align="left ">
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Kindly Find enclosed herewith the <b><span id="list3" runat="server">list </span>
                        </b>List of Accounts for the month of <b><span id="month1" runat="server"></span></b>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List is being carried by 1___________ 2___________
                    </p>
                </td>
            </tr>
            <tr>
                <td align="left " style="height: 18px">
                    <b>Encl : </b>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="list" runat="server">1. L.O.R.</span>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span1" runat="server">2. TY-33.</span><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span3" runat="server">3. Closing Abstract</span><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span4" runat="server">4. Bank Statement-D.M.S.</span><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span5" runat="server">5. L.O.P.</span><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span6" runat="server">6. TY-34</span><br />
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 20px"><b>Kamal Preet Kaur</b>
                    <b><span id="ToName" runat="server"></span></b>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 18px">
                    <b><span id="Treasury" runat="server"></span></b>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 19px">
                    <br />
                    <br />
                    No. : eGRAS/A.G./Account/<span id="Finyear1" runat="server"></span>/
                    <asp:Image ID="Barcode" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 19px">
                    <asp:Label ID="Label1" runat="server" Text="Date : "></asp:Label>
                    <b><span id="datee1" runat="server"></span></b>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 19px">
                    <b>Copy forwarded for information and necessary action to :-</b>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 19px">
                    <br />
                    1. Additional Director, (Treasury & Budget ), D.T.A. , Rajsthan , Jaipur.
                </td>
            </tr>
            <tr>
                <td align="right">
                    <br />
                    <b>Kamal Preet Kaur </b>
                    <b><span id="toname1" runat="server"></span></b>
                    <br />
                    <b><span id="Treasury1" runat="server"></span></b>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidSummary" runat="server" />
    <asp:HiddenField ID="hidPhase" runat="server" />
    <asp:HiddenField ID="hidFName" runat="server" />
    <asp:HiddenField ID="hidMM" runat="server" />
    <asp:HiddenField ID="hidYYYY" runat="server" />
    <asp:HiddenField ID="hidReqSign" runat="server" />
</asp:Content>

