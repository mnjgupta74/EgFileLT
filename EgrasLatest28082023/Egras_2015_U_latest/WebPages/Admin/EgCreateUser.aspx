<%@ Page Title="EgCreateUser" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgCreateUser.aspx.cs" Inherits="WebPages_Admin_EgCreateUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/UserControls/CustomTextBox.ascx" TagName="CustomTextBox" TagPrefix="ucl" %>
<%@ Register Assembly="WebControlCaptcha" Namespace="WebControlCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../js/jquery-ui.css" rel="stylesheet" type="text/css" />


    <script src="../../js/jquery-ui.min.js" type="text/javascript"></script>

    <%--<script src="../../md5.js" type="text/javascript" language="javascript"></script>--%>
    <script src="../../js/SHA256.js"></script>

    <style type="text/css">
        .ui-datepicker {
            background: #1C92D3;
            border: 1px solid #555;
            color: #EEE;
        }
    </style>



    <script language="javascript" type="text/javascript">

        $(function () {
            $('#<%=txtBirthDate.ClientID %>').on('click',
          function () {
              $(this).datepicker({
                  changeMonth: true,
                  changeYear: true,
                  dateFormat: 'dd/mm/yy',
                  yearRange: "-164:+0",
                  onSelect: function () {

                      currenttab = $(this).attr("tabindex");

                      nexttab = currenttab * 1 + 1;

                      $("[tabindex='" + nexttab + "']").focus();

                  }

              });

          });

        });


    </script>

    <script type='text/javascript'>
        $(document).ready(function () {
            jQuery.browser = {};
            (function () {
                jQuery.browser.msie = false;
                jQuery.browser.version = 0;
                if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
                    jQuery.browser.msie = true;
                    jQuery.browser.version = RegExp.$1;
                }
            })();
            $("#datepicker").blur(function (e) {
                var from = $("#datepicker");
                var d = new Date();
                var curr_date = d.getDate();
                var curr_month = d.getMonth();
                var curr_year = d.getFullYear();
                var currdate = curr_date + "/" + (curr_month + 1) + "/" + curr_year

                if (from.val() > currdate) {
                    alert("date cannot be greater then current date");

                    $("#datepicker").val('')
                    return false;
                }


            });
        });
    </script>

    <script language="javascript" type="text/javascript">
        function isStrongPassword() {

            var Check = Page_ClientValidate("de");

            if (Check == true) {
                var control = document.getElementById("<%=txtpassward.ClientID%>");


                var myString = control.value;
                var Stringlen = myString.length;

                var ValidateDigits = /[^0-9]/g;
                var ValidateSpChar = /[a-zA-Z0-9]/g;
                var ValidateChar = /[^A-Z]/g;


                var digitString = myString.replace(ValidateDigits, "");
                var specialString = myString.replace(ValidateSpChar, "");
                var charString = myString.replace(ValidateChar, "");




                if (Stringlen < 6) {

                    alert("Passwords must be at least 6 characters");

                    control.value = "";

                    control.focus();
                    return false;
                }
                if (Stringlen > 20) {
                    alert("Passwords must be  20 characters");
                    control.value = "";
                    control.focus();
                    return false;
                }
                if (specialString < 1) {

                    alert("Passwords must include at least 1 special (#,@,&,$ etc) characters");

                    control.value = "";

                    control.focus();
                    return false;
                }
                if (digitString < 1) {
                    alert("Passwords must include at least 1 numeric characters");
                    control.value = "";

                    control.focus();
                    return false;
                }
                if (charString < 1) {

                    alert("Passwords must include at least 1 Capital characters");
                    control.value = "";

                    control.focus();
                    return false;
                }



                var newpassword = document.getElementById("<%=txtpassward.ClientID %>").value;
                //document.getElementById("<%=txtpassward.ClientID %>").value = hex_md5(newpassword);
                document.getElementById("<%=txtpassward.ClientID %>").value = SHA256(newpassword);

                var cnfpassword = document.getElementById("<%=txtrepassward.ClientID %>").value;
                //document.getElementById("<%=txtrepassward.ClientID %>").value = hex_md5(cnfpassword);
                document.getElementById("<%=txtrepassward.ClientID %>").value = SHA256(cnfpassword);

                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_lblNoRecords').hide();
            //$('#<%=lblNoRecords.ClientID %>').css('display', 'none');
            $('input[type=checkbox]').removeAttr("disabled", "disabled");
            $('input[type=checkbox]').prop('checked', false);
            
              
        $(document).on('click', '#btnsearch', function () {
                $('#<%=txtlogin.ClientID%>').val("");
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                $("#<%=gdRows.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                var iCounter = 0;
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=gdRows.ClientID%> tr:has(td)").show();
                    return false;
                }
                //Iterate through all the td.
                $("#<%=gdRows.ClientID%> tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
                if (iCounter == 0) {
                    $('#<%=lblNoRecords.ClientID%>').css('display', '');
                }
        })
            $(document).on('click', '#lnkPassword', function () {
                // code here
                window.open("../Account/EgfrmPasswordPolicy.aspx", "Null", "dialogWidth:500px; dialogHeight:200px; scroll:no; center:yes");
            });
        })
        function CheckOne(obj) {
           
            var grid = obj.parentNode.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    //alert(inputs[i].val());
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                        
                    }
                    $(inputs).removeAttr("disabled", "disabled");
                    $(obj).attr("disabled", "disabled");
                    var gridResults = obj.parentNode.getAttribute("name");
                    $('#<%=txtlogin.ClientID%>').val(gridResults);
                }
            }
        }

        <%--function SelectAllCheckboxes1(chk) {
            $('#<%=gdRows.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) { this.checked = chk.checked; }
            });
        }--%>
    </script>

    <script language="javascript" type="text/javascript">
        function LoginIdValidation(field) {
            var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.@"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("  LoginId is not valid (Special character and space Not Allowed) !");
                field.focus();
                field.select();
                field.value = "";
            }
        }
        function CheckLoginId(el) {
            var ex = /[A-Za-z]([a-z]|[A-Z]|[0-9]|[.]|[_@])/;
            if (ex.test(el.value) == false) {
                alert('Special character not allowed except(. _ @) and Login ID Have Atleast One Alphabet Character.!');
                el.value = "";
            }
        }
        function CharCheck(field) {
            var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 "
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("character and numeric values allowed.!");
                field.focus();
                field.select();
                field.value = "";
            }
        }
        function Count(text, long) {

            var maxlength = new Number(long); // Change number to your max length.
            if (document.getElementById('<%=txtaddress.ClientID%>').value.length > maxlength)
                text.value = text.value.substring(0, maxlength);
            return false;
        }


        function Success() {

            alert('User Created Successfully');
            // your page name
            //window.location = "../EgUserProfile.aspx";
        }


        function dateValidation() {

            var dtObj = document.getElementById("<%=txtBirthDate.ClientID %>")

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
                alert("Invalid Date..!");
                dtObj.value = ""
                return false

            }
        }
        function NumberOnly(evt) {

            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 3 && parts[1].length >= 3) return false;
        }
        

        function CheckPinCode() {
            var pin = document.getElementById("<%=txtpincode.ClientID %>").value;

            if (pin == "000000") {
                document.getElementById("<%=txtpincode.ClientID %>").value = "";
                alert('PinCode is Invalid.!');
            }
        }
        function DisplayDiv(val) {
            var id = val;
            $("#<%=txtlogin.ClientID %>").val("");
            $("#<%=txtlogin.ClientID %>").removeAttr("disabled", "disabled");
            document.getElementById("<%=txtTIN.ClientID %>").value = "";
            document.getElementById("<%=txtTIN.ClientID %>").disabled = false;
            document.getElementById("<%=txtTIN.ClientID %>").style.backgroundColor = "White";
            if (id == 6) {
                document.getElementById("divDept").style.display = "none";
                document.getElementById("divbank").style.display = "block";
                document.getElementById("divd").style.display = "none";
                $('#ctl00_ContentPlaceHolder1_BtnCheckUser').show();
                //document.getElementById('ctl00_ContentPlaceHolder1_BtnCheckUser').style.display = "block";
            }
            if (id == 5) {
                document.getElementById("divDept").style.display = "block";
                document.getElementById("divd").style.display = "block";
                document.getElementById("divbank").style.display = "none";
                $("#<%=txtlogin.ClientID %>").val("");
                $("#<%=txtlogin.ClientID %>").attr("disabled", "disabled");
                $('input[type=checkbox]').removeAttr("disabled", "disabled");
                $('input[type=checkbox]').prop('checked', false);
                $('#ctl00_ContentPlaceHolder1_BtnCheckUser').hide();
                //document.getElementById('ctl00_ContentPlaceHolder1_BtnCheckUser').style.display = "none";

            }
            if (id == 2) {
                document.getElementById("divDept").style.display = "none";
                document.getElementById("divd").style.display = "none";
                document.getElementById("divbank").style.display = "none";
                $('#ctl00_ContentPlaceHolder1_BtnCheckUser').show();
                //document.getElementById('ctl00_ContentPlaceHolder1_BtnCheckUser').style.display = "block";
            }
            if (id == 7) {
                document.getElementById("divDept").style.display = "none";
                document.getElementById("divd").style.display = "none";
                document.getElementById("divbank").style.display = "none";
                $('#ctl00_ContentPlaceHolder1_BtnCheckUser').show();
                //document.getElementById('ctl00_ContentPlaceHolder1_BtnCheckUser').style.display = "block";
            }
            if (id == 8) {
                document.getElementById("divDept").style.display = "none";
                document.getElementById("divd").style.display = "none";
                document.getElementById("divbank").style.display = "none";
                $('#ctl00_ContentPlaceHolder1_BtnCheckUser').show();
            }
            if (id == 0) {
                document.getElementById("divDept").style.display = "none";
                document.getElementById("divd").style.display = "none";
                document.getElementById("divbank").style.display = "none";
                $('#ctl00_ContentPlaceHolder1_BtnCheckUser').show();
                //document.getElementById('ctl00_ContentPlaceHolder1_BtnCheckUser').style.display = "block";
            }
        }
        function BankBSRCode(val) {

            var id = val;
            if (id == 0) {

                alert('select Bank Name ');
                document.getElementById("<%=txtTIN.ClientID %>").value = "";
                document.getElementById("<%=txtTIN.ClientID %>").disabled = false;
                document.getElementById("<%=txtTIN.ClientID %>").style.backgroundColor = "White";

            }
            else {
                document.getElementById("<%=txtTIN.ClientID %>").value = id;
                document.getElementById("<%=txtTIN.ClientID %>").disabled = true;
                document.getElementById("<%=txtTIN.ClientID %>").style.backgroundColor = "Khaki";
            }

        }


    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%; text-align: center;">
                <tr>
                    <td class=" PageTitle " width="50%">Login&nbsp;Form
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table style="width: 85%; text-align: left; color: #0033CC;" border="1" cellpadding="0"
                            cellspacing="0">
                            <tr>
                                <td colspan="2" style="height: 20px; background-color: #1C92D3; color: White">Select ID and Passward:
                                </td>
                            </tr>
                            <tr id="DeptRow" runat="server">
                                <td colspan="2" align="center">
                                    <asp:RadioButtonList ID="rbltype" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbltype_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="5">Self</asp:ListItem>
                                        <asp:ListItem Value="4" Selected="True">Office</asp:ListItem>
                                        <asp:ListItem Value="11">FA</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table id="tblOffice" runat="server" style="width: 100%; text-align: left; color: #0033CC;"
                                        border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="style1" width="220px">Department Name :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldepartment" runat="server" Width="250px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Department"
                                                    ControlToValidate="ddldepartment" ValidationGroup="cu" InitialValue="0" ForeColor="Red"
                                                    Style="text-align: center">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">Treasury :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTreasury" runat="server" AutoPostBack="true" Width="250px"
                                                    OnSelectedIndexChanged="ddlTreasury_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Treasury"
                                                    ControlToValidate="ddlTreasury" ValidationGroup="cu" InitialValue="0" ForeColor="Red"
                                                    Style="text-align: center">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">Office Name :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOfficeName" runat="server" AutoPostBack="true" Width="250px"
                                                    OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="--Select Office--"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select Office"
                                                    ControlToValidate="ddlOfficeName" ValidationGroup="cu" InitialValue="0" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table id="tblSubTreasury" runat="server" visible="false" style="width: 100%; text-align: left; color: #0033CC;"
                                        border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="style1">Sub-Treasury :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSubTreasury" runat="server" AutoPostBack="true" Width="250px"
                                                    OnSelectedIndexChanged="ddlSubTreasury_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="--Select Sub-Treasury--"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select Sub-Treasury"
                                                    ControlToValidate="ddlSubTreasury" ValidationGroup="cu" InitialValue="0" ForeColor="Red"
                                                    Style="text-align: center">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr runat="server" id="UserType">
                                <td>UserType: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlusertype" runat="server" Width="150px" TabIndex="6"  OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select User Type</asp:ListItem>
                                        <asp:ListItem Value="2">TO</asp:ListItem>
                                        <asp:ListItem Value="6">Bank</asp:ListItem>
                                        <asp:ListItem Value="5">Department</asp:ListItem>
                                        <asp:ListItem Value="7">AG</asp:ListItem>
                                        <asp:ListItem Value="8">Report Viewer</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlusertype"
                                        Text="*" ErrorMessage="Select UserType." InitialValue="0" ValidationGroup="de"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    
                                    <div id="divd"  style="display: none;text-align: left">
                                        Department: <font color="red">*</font> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div style="display: none;height: 100px; overflow: auto;" id="divDept">
                                        Search Text :
                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                        &nbsp;
                                            <%--<asp:Button ID="btnsearch" runat="server" Text="Search" />--%>
                                        <input type="button" id="btnsearch" value="Search" />
                                        <br />
                                        <%-- <input type="checkbox" id="chkAll" value="1" onclick="javascript: SelectAllCheckboxes1(this);"
                                            tabindex="7">
                                        <font color="green">Select All</font>--%>
                                        <asp:GridView ID="gdRows" runat="server" AutoGenerateColumns="False" DataKeyNames="DeptCode"
                                            Font-Names="Arial" Font-Size="Small" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="DepartmentList" InsertVisible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkdept" runat="server" Text='<%# Eval("DeptNameEnglish") %>' name='<%# Eval("deptcode") %>' onclick="CheckOne(this)" />
                                                        <asp:HiddenField ID="idnum" runat="server" Value='<%# Eval("deptcode") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="red"></asp:Label>
                                    </div>
                                    <div style="display: none;" id="divbank">
                                        Select Bank: <font color="red">*</font> &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlbank" runat="server" onchange="BankBSRCode(this.options[this.selectedIndex].value);">
                                            </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2" class="style1" width="220px">Login Id: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtlogin" runat="server" MaxLength="20" Style="width: 180px;" TabIndex="1"
                                        OnChange="CheckLoginId(this);"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rgv" ResourceName="rgv" ValidationGroup="de"
                                        runat="server" ControlToValidate="txtlogin" ErrorMessage="Special character not allowed."
                                        Text="*" ValidationExpression="([a-z]|[A-Z]|[0-9]|[.]|[_@])*" Display="Dynamic"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    &nbsp;<asp:Image ID="Image1" runat="server" Height="20px" Width="20px" Visible="false" />
                                    &nbsp;<asp:LinkButton ID="BtnCheckUser" CssClass="checkavail" runat="server" OnClick="ChechExistingLogin"
                                        Text="Check Availability"></asp:LinkButton>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Password: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpassward" runat="server" MaxLength="20" TextMode="Password" Style="width: 180px;"
                                        AutoComplete="Off" TabIndex="2" ToolTip="Password min Length 6 and max Length 20 and Check Password Formet. (e.g Nic@123)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtpassward"
                                        Text="*" ErrorMessage="Enter Password" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp; &nbsp;&nbsp;
                                    <ajaxToolkit:PasswordStrength ID="PS" runat="server" TargetControlID="txtpassward"
                                        DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="0"
                                        PrefixText="Strength:" MinimumNumericCharacters="1" MinimumSymbolCharacters="1"
                                        MinimumUpperCaseCharacters="1" RequiresUpperAndLowerCaseCharacters="true" CalculationWeightings="50;15;15;20" />
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
<%--                                    <asp:LinkButton ID="lnkPassword" runat="server" Text=" Password Policy (?)" ForeColor="Maroon"
                                        OnClientClick="Javascript:CheckPasswordPolicy()"></asp:LinkButton>--%>

                                    
                                        <span id="lnkPassword" style="color:maroon; text-decoration:underline;">Password Policy (?)</span>
                                </td>
                            </tr>
                            <tr>
                                <td>Confirm Password: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtrepassward" runat="server" MaxLength="15" TextMode="Password" AutoComplete="Off"
                                        Style="width: 180px;" TabIndex="3"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtrepassward"
                                        Text="*" ErrorMessage="Enter Confirm Password" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                    &nbsp;<asp:CompareValidator ID="CompareValidator" runat="server" ControlToCompare="txtpassward"
                                        ControlToValidate="txtrepassward" ErrorMessage="Confirm password not match."
                                        Text="Confirm password not match." ValidationGroup="de" ForeColor="Red"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <center>
                            <table style="width: 85%; text-align: left; color: #0033CC;" border="1">
                                <tr>
                                    <td colspan="2" style="height: 20px; background-color: #1C92D3; color: White">Personal Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td>Name/Office : <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfirst" runat="server" Style="color: gray;" MaxLength="30" OnChange="CharCheck(this);"
                                            TabIndex="4"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfirst"
                                            Text="*" ErrorMessage="Enter First Name" InitialValue="FirstName" ValidationGroup="de"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator><ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2"
                                            runat="server" TargetControlID="txtfirst" WatermarkText="FirstName">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <asp:RegularExpressionValidator ID="RfvFirstName" runat="server" Text="*" ErrorMessage="Special character and Space not allowed in First Name.!"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtfirst" ValidationExpression="^[a-zA-Z0-9]+$"
                                            ValidationGroup="de"></asp:RegularExpressionValidator>
                                        &nbsp;&nbsp;&nbsp; LastName :-<font color="red">*</font> &nbsp;
                                        <asp:TextBox ID="txtlast" runat="server" Style="color: gray;" MaxLength="30" OnChange="CharCheck(this);"
                                            TabIndex="5">
                                        </asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtlast"
                                            Text="*" ErrorMessage="Enter Last Name" InitialValue="LastName" ValidationGroup="de"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator><ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1"
                                            runat="server" TargetControlID="txtlast" WatermarkText="LastName">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <asp:RegularExpressionValidator ID="RfvLastName" runat="server" Text="*" ErrorMessage="Special character and Space not allowed in Last Name.!"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtlast" ValidationExpression="^[a-zA-Z0-9]+$"
                                            ValidationGroup="de"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 28px">DOB: <font color="red">*</font>
                                    </td>
                                    <td style="height: 28px">
                                        <asp:TextBox ID="txtBirthDate" runat="server" CssClass="datepicker" onkeypress="Javascript:return NumberOnly(event)"
                                            onpaste="return false" OnChange="javascript:return dateValidation()" Width="150px"
                                            TabIndex="9"></asp:TextBox>
                                        <%--<ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                                    PopupButtonID="txtBirthDate" TargetControlID="txtBirthDate">
                                </ajaxToolkit:CalendarExtender>--%>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                            CultureName="en-US" TargetControlID="txtBirthDate" AcceptNegative="None" runat="server">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter D.O.B."
                                            Text="*" ControlToValidate="txtBirthDate" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Email Id: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailId" runat="server" MaxLength="100" Style="width: 150px;"
                                            TabIndex="11"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvemailidvld" runat="server" ControlToValidate="txtEmailId"
                                            Text="*" ErrorMessage="Enter E-Mail.!" ValidationGroup="de"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpForemail" runat="server" ControlToValidate="txtEmailId"
                                            ErrorMessage="Fill Correct EmailId" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="de"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Address <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtaddress" runat="server" MaxLength="40" Width="300px" TextMode="MultiLine"
                                            OnkeyPress="javascript:Count(this,100);" TabIndex="12"></asp:TextBox>&nbsp;
                                        <asp:RequiredFieldValidator ID="rfvaddressvld" runat="server" ControlToValidate="txtaddress"
                                            Text="*" ErrorMessage="Enter Address" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtaddress"
                                            Text="Special character not allowed in address.!" CssClass="XMMessage" ErrorMessage="Special character not allowed."
                                            ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>
                                </tr>
                                <tr>
                                    <td>Country: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcountry" runat="server" CssClass="DropDownlist" Width="150px"
                                            TabIndex="13">
                                            <asp:ListItem Text="India" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlcountry"
                                            Text="*" ErrorMessage="Select Country.!" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>State: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlstate" runat="server" CssClass="DropDownlist" Width="150px"
                                            TabIndex="14">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlstate"
                                            ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>City: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" runat="server" MaxLength="40" Style="width: 150px;" TabIndex="15"
                                            OnChange="CharCheck(this);"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCity"
                                            Text="*" ErrorMessage="Enter City Name.!" ValidationGroup="de"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mobile Phone <font color="red">*</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        +91
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" Style="width: 150px;" onblur="Javascript:return NumberOnly(event)"
                                            TabIndex="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtmobile"
                                            Text="*" ErrorMessage="Enter Mobile Number." ValidationGroup="de"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobile"
                                            ErrorMessage="Enter Mobile phone number" ValidationGroup="de"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtmobile"
                                            ErrorMessage="Enter Correct Mob No." ValidationGroup="de" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PinCode: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpincode" runat="server" MaxLength="6" onblur="Javascript:return NumberOnly(event)"
                                            onchange="Javascript:return CheckPinCode();" Style="width: 150px;" TabIndex="17"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvpincodevld" runat="server" ControlToValidate="txtpincode"
                                            Text="*" ErrorMessage="Enter PinCode." ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtpincode"
                                            ValidationExpression="\d{6}" ErrorMessage="Pin code must be 6 numeric digits"
                                            runat="server" ValidationGroup="de" />
                                    </td>
                                </tr>
                                <tr id="trTIN" runat="server">
                                    <td>TIN/Actt.No./VehicleNo/Taxid:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTIN" runat="server" MaxLength="20" Width="150px" TabIndex="18"></asp:TextBox>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtTIN"
                                            Text="Special character not allowed." CssClass="XMMessage" ErrorMessage="Special character not allowed."
                                            ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="lblpass" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        <asp:TextBox ID="txtpass" runat="server" TabIndex="19"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:ValidationSummary ID="summary1" ShowSummary="false" runat="server" ValidationGroup="de"
                                            ShowMessageBox="true" DisplayMode="BulletList" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="102px" OnClick="btnSubmit_Click"
                                            TabIndex="20" ValidationGroup="de" OnClientClick="isStrongPassword();" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
