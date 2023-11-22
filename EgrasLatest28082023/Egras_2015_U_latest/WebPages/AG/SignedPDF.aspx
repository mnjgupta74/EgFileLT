<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="SignedPDF.aspx.cs" Inherits="WebPages_Reports_SignedPDF" %>

<script runat="server">

</script>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <script type="text/javascript">

        function dateValidation(FromDate, ToDate) {
            var dtObj = document.getElementById("<%=txtfromdate.ClientID %>")
             //var strAA = new Date();
             //var sA = strAA.format("dd/MM/yyyy");
             var type = $('#<%=rblFileType.ClientID %> input:checked').val()
            if (type == "3") {
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
        function OpenCard() {
            try {
                ////var certificate = new ActiveXObject("DSC.eKSignedPDF");

                ////if (certificate == "DSC.eKSignedPDF")

                ////{

                var certificate = new ActiveXObject("DSC.eKSignedPDF");
                if (certificate == "DSC.eKSignedPDF") {


                    var cert = certificate.sel_certificate().split("#");
                    if (cert.length == 4) {

                        var CardUserName = cert[0].split(",");
                        $("#ContentPlaceHolder1_hidDSCUserName").val(CardUserName[0].substring(3));
                        $("#ContentPlaceHolder1_hidDSCSerialKey").val(cert[1]);
                        $("#ContentPlaceHolder1_hidThumbprint").val(cert[2]);
                        $("#ContentPlaceHolder1_hidDSCValiddate").val(cert[3]);
                    }
                    else {
                        alert("Either your Digital Signature device is not installed or not ready for use.");
                        return false;
                    }
                }
                else {
                    alert("Please Install Digital Signature Setup (DSC MSI) file (If already done then Reinstall and chnage the IE browser settings (Refer Manual).");
                    return false;
                }
            }
            catch (err) {
                if (err.message == "Automation server can't create object")
                    alert("Please Install Digital Signature Setup (DSC MSI) file (If already done then Reinstall the Same).");
                return false;
            }
        }

        function RejectDSC(DSC_id) {
            if (confirm("Are you sure, you want to Revoke?")) {
                $("#ContentPlaceHolder1_hidDSCId").val(DSC_id);
                __doPostBack("ctl00$ContentPlaceHolder1$lnk_DeleteDSC", '');
                return false;
            }
        }

        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnShow"]').click(function () {
                if ($(<%=txtfromdate.ClientID%>).val() != "" && $(<%=txttodate.ClientID%>).val() != "") {
                    $('#cover-spin').show(0);

            }
        });
        });
       
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

        #ctl00_ContentPlaceHolder1_txtSignPdf {
            display: none;
        }

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
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

        #ctl00_ContentPlaceHolder1_btnFTP {
            color: #f4f4f4 !important;
            background-color: #274869 !important;
            margin-left: 5px;
        }

         .form-control{
            display: inherit;
        }
    </style>
    <div id="cover-spin"></div>
    <%--==============END CSS - JQUERY LOADER============--%>
    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">eSign Process</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Ty-33" />
    </div>

    <table align="center" style="width: 100%" border="1">
        <tr>
            <td align="center" colspan="5">
                 <asp:RadioButtonList ID="rblFileType" style="BORDER: 0px;"  class="form-control" runat="server" 
                     OnSelectedIndexChanged="rblFileType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" onChange="enableDisable()">
                    <asp:ListItem Text="AG File" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="DMS File" Value="2" style="margin-left:20px"></asp:ListItem>
                    <asp:ListItem Text="Daily Account" Value="3" style="margin-left:20px"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="trAgFile" runat="server">
            <td id="tdMonth" runat="server">
                <b><span style="color: #336699">Month : </span></b>&nbsp;
                 <asp:DropDownList ID="ddlMonth"  class="form-control" runat="server" TabIndex="2" Enabled="true" Width="170px" ToolTip="Select Month">
                </asp:DropDownList>
            </td>
            <td id="tdYear" runat="server">
                <b><span style="color: #336699">Year : </span></b>&nbsp;
                 <asp:DropDownList ID="ddlYear"  class="form-control" runat="server" TabIndex="2" Enabled="true" Width="170px" ToolTip="Select Financial Year">
                </asp:DropDownList>
            </td>

            <td id="tdAccount" runat="server">
                <b><span style="color: #336699">Account : </span></b>&nbsp;
                <asp:DropDownList ID="ddlPhase"  class="form-control" runat="server" TabIndex="2" Enabled="true" Width="170px" ToolTip="Select Month">
                </asp:DropDownList>
            </td>
            <td id="tdDate" runat="server" visible="false" align="center">
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                <%--<b><span style="color: #336699">From Date : </span></b>&nbsp;--%>
                <asp:TextBox ID="txtfromdate" Height="100%" Style=" margin-left: 6px;" CssClass="form-control" runat="server" Width="120px" TabIndex="1"
                    onpaste="return false" onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)" onblur="isDate(this);"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td id="tdToDate" runat="server" visible="false" align="center">
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
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td bgcolor="White" class="style21" align="center">
                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-default" Text="Show" Width="100px"
                    TabIndex="8" OnClick="btnShow_Click" Visible="true"     height="40px" />

            </td>
            <td bgcolor="White">
                <asp:Button ID="btnFTP" Visible="false" runat="server" OnClick="btnFTP_Click" Text="Send To FTP"></asp:Button>
            </td>
        </tr>

        <asp:TextBox ID="txtSignPdf" runat="server" Text=""></asp:TextBox>
    </table>
    <center>
        <div style="width: 100%; background-color: White; height: 650px">
            <center>
                <div style="width: 100%; background-color: White; height: 650px">
                    <asp:Panel ID="pnldgekSign" runat="server" Width="100%" Visible="false">
                        <asp:GridView runat="server" ID="dgekSignDtls" AutoGenerateColumns="False" Width="100%"
                            Visible="False"
                            BackColor="#5D7B9D" PageSize="18" BorderColor="#CCCCCC" CellPadding="0" CellSpacing="1"
                            Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="#333333" GridLines="None" EmptyDataText="No Record Found"
                            OnRowCommand="dgekSignDtls_RowCommand"
                            OnSelectedIndexChanged="dgekSignDtls_SelectedIndexChanged"
                            OnRowDataBound="dgekSignDtls_RowDataBound">
                            <FooterStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                Font-Names="Times New Roman" Font-Size="15px" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDWN" runat="server" CommandName="Select"
                                            CommandArgument='<%# Eval("Id") %>' Text="ViewPDF" Font-Bold="True" Font-Size="Small" ForeColor="#669900"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblId" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDocID" Text='<%# Eval("DocID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFName" Text='<%# Eval("FName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Month-Year">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMonthYear" Text='<%# Eval("MonthYear") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Phase">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPhase" Text='<%# Eval("Account") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Creation Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCreationDate" Text='<%# Eval("CreationDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="eSignDate">
                                    <ItemTemplate>
                                        <table>
                                            <tr runat="server" id="trID">
                                                <td height="30px">
                                                    <asp:Label runat="server" ID="lbleSignDate" Text='<%# Eval("eSignDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="eSign" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btneSign" runat="server" Text="Do eSign" CommandArgument='<%# Eval("Id") %>'
                                            CommandName="eSign"
                                            BackColor="#e1f3e3" Font-Bold="True" ForeColor="White"></asp:Button>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FTP Date">
                                    <ItemTemplate>
                                        <table>
                                            <tr runat="server" id="trFTPID">
                                                <td height="30px">
                                                    <asp:Label runat="server" ID="lblFtpdate" Text='<%# Eval("FTPDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </asp:Panel>
                    <asp:Panel ID="pnldgekSign_BankUpload" runat="server" Width="100%" Visible="false">
                        <asp:GridView runat="server" ID="dgekSignDtls_BankUpload" AutoGenerateColumns="False" Width="100%"
                            Visible="False"
                            BackColor="#5D7B9D" PageSize="18" BorderColor="#CCCCCC" CellPadding="0" CellSpacing="1"
                            Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="#333333" GridLines="None" EmptyDataText="No Record Found"
                            OnRowCommand="dgekSignDtls_BankUpload_RowCommand"
                            OnSelectedIndexChanged="dgekSignDtls_BankUpload_SelectedIndexChanged"
                            OnRowDataBound="dgekSignDtls_BankUpload_RowDataBound">
                            <FooterStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                Font-Names="Times New Roman" Font-Size="15px" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDWN_BankUpload" runat="server" CommandName="Select_BankUpload"
                                            CommandArgument='<%# Eval("Id") %>' Text="ViewPDF" Font-Bold="True" Font-Size="Small" ForeColor="#669900"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblId_BankUpload" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>                                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFName_BankUpload" Text='<%# Eval("FName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBankName_BankUpload" Text='<%# Eval("BankName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Month-Year">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMonthYear_BankUpload" Text='<%# Eval("MonthYear") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>                              
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Upload Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCreationDate_BankUpload" Text='<%# Eval("CreationDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="eSignDate">
                                    <ItemTemplate>
                                        <table>
                                            <tr runat="server" id="trID_BankUpload">
                                                <td height="30px">
                                                    <asp:Label runat="server" ID="lbleSignDate_BankUpload" Text='<%# Eval("eSignDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="eSign" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btneSign_BankUpload" runat="server" Text="Do eSign" CommandArgument='<%# Eval("Id") %>'
                                            CommandName="eSign_BankUpload"
                                            BackColor="#e1f3e3" Font-Bold="True" ForeColor="White"></asp:Button>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FTP Date">
                                    <ItemTemplate>
                                        <table>
                                            <tr runat="server" id="trFTPID_BankUpload">
                                                <td height="30px">
                                                    <asp:Label runat="server" ID="lblFtpdate_BankUpload" Text='<%# Eval("FTPDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </asp:Panel>
                    <asp:Panel ID="pnldailyFile" runat="server" Width="100%" Visible="false">
                        <asp:GridView runat="server" ID="grddailyFile" AutoGenerateColumns="False" Width="100%"
                            Visible="False"
                            BackColor="#5D7B9D" PageSize="18" BorderColor="#CCCCCC" CellPadding="0" CellSpacing="1"
                            Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="#333333" GridLines="None" EmptyDataText="No Record Found"
                            OnRowCommand="grddailyFile_RowCommand"
                            OnSelectedIndexChanged="grddailyFile_SelectedIndexChanged"
                            OnRowDataBound="grddailyFile_RowDataBound">
                            <FooterStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                Font-Names="Times New Roman" Font-Size="15px" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDailyView" runat="server" CommandName="Select"
                                            CommandArgument='<%# Eval("Id") %>' Text="ViewPDF" Font-Bold="True" Font-Size="Small" ForeColor="#669900"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblIdDaily" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDocIDDaily" Text='<%# Eval("DocID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFNameDaily" Text='<%# Eval("FName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDayDaily" Text='<%# Eval("FileDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Month-Year">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMonthYear" Text='<%# Eval("MonthYear") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Phase">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPhase" Text='<%# Eval("Account") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Creation Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCreationDateDaily" Text='<%# Eval("CreationDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="eSignDate">
                                    <ItemTemplate>
                                        <table>
                                            <tr runat="server" id="trIDDaily">
                                                <td height="30px">
                                                    <asp:Label runat="server" ID="lbleSignDateDaily" Text='<%# Eval("eSignDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="eSign" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btneSignDaily" runat="server" Text="Do eSign" CommandArgument='<%# Eval("Id") %>'
                                            CommandName="eSign_Daily"
                                            BackColor="#e1f3e3" Font-Bold="True" ForeColor="White"></asp:Button>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FTP Date">
                                    <ItemTemplate>
                                        <table>
                                            <tr runat="server" id="trFTPIDDaily">
                                                <td height="30px">
                                                    <asp:Label runat="server" ID="lblFtpdateDaily" Text='<%# Eval("FTPDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </asp:Panel>
                </div>
            </center>


        </div>
    </center>
    <asp:HiddenField ID="hidPDfBinaryData" runat="server" Value=""></asp:HiddenField>
    <asp:HiddenField ID="hidSignaturePosition" runat="server" Value=""></asp:HiddenField>
    <asp:HiddenField ID="hidDSCUserName" runat="server" Value=""></asp:HiddenField>
    <asp:HiddenField ID="hidDSCSerialKey" runat="server" Value=""></asp:HiddenField>
    <asp:HiddenField ID="hidThumbprint" runat="server" Value=""></asp:HiddenField>
    <asp:HiddenField ID="hidDSCValiddate" runat="server" Value=""></asp:HiddenField>
    <asp:HiddenField ID="hideSignDate" runat="server" Value=""></asp:HiddenField>
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidTreasuryCode" runat="server" />
    <asp:HiddenField ID="hidDSCId" runat="server" />
</asp:Content>

