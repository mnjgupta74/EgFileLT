<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgDepartment.aspx.cs" Inherits="WebPages_EgDepartment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>
    <link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../js/checkNumber.js" type="text/javascript"></script>
    <script src="../md5.js" type="text/javascript"></script>

    
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            //  var getsessionvalue = ('<%= Session["ReturnVal"] %>');

            var Cookies = document.cookie;
            var CookieArr = Cookies.split(';')[0];
            var GetCookie = CookieArr.split('=')[1];

            if (GetCookie == -2) {
                $(function () {
                    $("#dialog").dialog({
                        title: "Warning",
                        position: [0, 600],
                        buttons: {

                        }
                    });

                });
                setTimeout(function () { $('#dialog').dialog('destroy'); }, 10000);
            }
            else {
                // nothing

            }
        });
        function show() {
            //alert(123);

            $('#myDiv').css('visibility', 'visible');
        }
    </script>

    <script language="javascript" type="text/javascript">
        function openPopUp(str) {

            popupWindow = window.open("EgDefaceDetailNew.aspx?" + str, 'popUpWindow', 'height=200px,width=200px,left=300px,top=500px,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>

    <script type="text/javascript">


        //for txtFromDate text box
        function dateValidation() {

            var dtObj = document.getElementById("<%=txtfromdate.ClientID %>")

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
            var dtObj = document.getElementById("<%=txttodate.ClientID %>")

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


            var fdate = document.getElementById("<%=txtfromdate.ClientID %>")
            var tdate = document.getElementById("<%=txttodate.ClientID %>")
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



        function CheckNull() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_txtAmount").value == null || document.getElementById("ctl00_ContentPlaceHolder1_txtAmount").value == "") {
                alert("please enter Defece Amount");
            }
        }

        window.onload = function () { Radio_Click(); }

        function Radio_Click() {

            var radio1 = document.getElementById("<%=RadioButton1.ClientID %>");
            var radio2 = document.getElementById("<%=RadioButton2.ClientID %>");
            var textBox = document.getElementById("<%=txtgrn.ClientID %>");
            var textBox1 = document.getElementById("<%=txtfromdate.ClientID %>");
            var textBox2 = document.getElementById("<%=txttodate.ClientID %>");
            textBox.disabled = !radio1.checked;
            textBox1.disabled = !radio2.checked;
            textBox2.disabled = !radio2.checked;
            if (radio1.checked == true) {

                textBox.disabled = !radio1.checked;
                textBox1.value = "";
                textBox2.value = "";
                textBox1.disabled = !radio2.checked;
                textBox2.disabled = !radio2.checked;
                textBox.focus();

            }
            else if (radio2.checked == true) {
                textBox.value = "";
                textBox.disabled = !radio1.checked;
                textBox1.disabled = !radio2.checked;
                textBox2.disabled = !radio2.checked;

                textBox1.focus();
                textBox2.focus();
            }


            else { }

        }
    </script>

    <script type="text/javascript">

        function ClickToPrint() {
            docPrint = window.open("", "mywindow", "location=1,status=1,scrollbars=1,width=1000,height=500");
            docPrint.document.open();
            docPrint.document.write('<html><head><title>ChallanPage</title>');
            docPrint.document.write('</head><body onLoad="self.print()"><left>');
            docPrint.document.write('</Center><br/><table width="1030px" height="50%" top="0"  border=0 font Size="8"><tr><td width="150%"><left><font face="Small Fonts">');
            docPrint.document.write(document.getElementById("divChallanView").innerHTML);
            docPrint.document.write('</td></tr></table></left></font></body></html>');
            docPrint.document.close();

        }
        function NumCheck(field) {
            var valid = "0123456789"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.

                substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }

        }
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

            else if (ch == 8)
                return true;

            else if (ch == 9)
                return true;
            else
                return false;
        }


    </script>

    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenTextAmt" runat="server" />
             <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Department-Deface">
            <span _ngcontent-c6="" style="color: #FFF">Department-Deface</span></h2>
        <img src="../../Image/help1.png" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="right" title="Department-Deface" />
    </div>
           <%-- <fieldset id="Fieldset1" runat="server" width="60%">--%>
                <%--<legend style="color: #005CB8; font-size: small">Department-Deface</legend>--%>
                <div id='myDiv' style="visibility: hidden">
                    <img id="dwnldgif" alt="" style='position: absolute; margin-top: 200px; width: 100px; left: 45%;' src="../Image/waiting_process.gif" />
                </div>
                <div id="dialog" style="display: none">
                    * please Change your password
                    <br>
                        <br></br>
                        * now it is mandatory to change password after 45 days </br>
                </div>
                <table id="Table1" border="1" cellpadding="1" cellspacing="1" align="center" style="width: 100%">
                    <tr>
                        <td colspan="2" align="Center" style="padding:1%;">
                            <asp:RadioButton ID="RadioButton1" runat="server" ForeColor="#336699" Text="GRN" GroupName="Radio" onclick="Radio_Click()" />
                            <asp:TextBox ID="txtgrn" runat="server" Height="30px" Width="20%" MaxLength="8" TabIndex="1" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>
                        </td>
                    </tr>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="padding:1%;">
                            <asp:RadioButton ID="RadioButton2" runat="server" Text="DateWise" GroupName="Radio" ForeColor="#336699" 
                                onclick="Radio_Click()" />
                            <asp:TextBox ID="txtfromdate" Height="30px" runat="server" Width="15%" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation()" Visible="true"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                                ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txttodate" Height="30px" runat="server" Width="15%" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation1()" Visible="true"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="txttodate" TargetControlID="txttodate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txttodate"
                                ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                        </td>
                        <tr style="padding:1%;">
                            <td colspan="2" align="center">
                                <asp:Button ID="btnsubmit" Height="35px" CssClass="btn btn-primary" runat="server" Text="Submit" TabIndex="2" OnClientClick="show();" OnClick="btnsubmit_Click"
                                    ValidationGroup="a" />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" align="center">
                                <br />
                                <asp:GridView ID="grdprofile" runat="server" AutoGenerateColumns="False" Width="100%"
                                    Font-Names="Verdana" Font-Size="10pt" OnRowDataBound="grdprofile_RowDataBound"
                                    DataKeyNames="Grn" EmptyDataText="No Record Found" EmptyDataRowStyle-Font-Bold="true"
                                    EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                                    OnRowCreated="grdprofile_RowCreated" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    OnRowCommand="grdprofile_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="GRN">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkgrn" CommandName="grnbind" Text='<%#Eval("grn")%>' CommandArgument='<%#Eval("grn")%>'
                                                    runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UserName" HeaderText="Remitter Name">
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="middle" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tin" HeaderText="Tin" DataFormatString="{0:n}">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BankDate" HeaderText="ChallanDate">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Partily Deface">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtn" ImageUrl="~/Image/searchicon.jpg" runat="server" Width="20"
                                                    Height="25" OnClick="imgbtn_Click" ToolTip="Partily" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40" HeaderText="Fully Deface">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtn1" ImageUrl="~/Image/desc.jpg" runat="server" Width="20"
                                                    Height="25" OnClick="imgbtn1_Click" ToolTip="Fully" OnClientClick="return confirm('Are you sure, You want to fully deface amount?');" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40" HeaderText="Details">
                                            <ItemTemplate>
                                                <asp:Image ID="Imagesearch" runat="server" ImageUrl="~/Image/search-icon.gif" Height="23px"
                                                    Width="32px" />
                                                <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="Panel1"
                                                    TargetControlID="Imagesearch" DynamicContextKey='<%# Eval("GRN") %>' DynamicControlID="Panel1"
                                                    DynamicServiceMethod="GetDynamicContent" Position="Right">
                                                </ajaxToolkit:PopupControlExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <asp:Panel ID="Panel1" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                </table>
       <%--     </fieldset>--%>
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="470px"
                Style="display: none">
                <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #D55500">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">Details
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 45%">Grn Number : &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblgrn" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 45%">Challan Amount(A): &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            <%-----%>
                            <asp:Label ID="lblamt" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 45%">Refunded Amount(B): &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblRefundedAmt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 45%">Defaced Amount(C): &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lbllastDeface" runat="server"></asp:Label>
                            <%-----%>
                            <asp:Label ID="lblrelAmount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 45%">Release from Defaced(D): &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblReleasedAmt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 45%">Remaining Amount(A-B-C+D): &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblRemaining" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 45%">Deface Amount: &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" onChange="javascript:return DecimalNumber(this,ctl00_ContentPlaceHolder1_HiddenTextAmt);" />
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAmount"
                                Type="double" ErrorMessage="You must be between 0 and 999999999" MaximumValue="999999999"
                                MinimumValue="0" ValidationGroup="a"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" ValidationGroup="a"
                                OnClientClick="javascript:CheckNull();" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                        </td>
                    </tr>
          
                </table>
            </asp:Panel>
     
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
