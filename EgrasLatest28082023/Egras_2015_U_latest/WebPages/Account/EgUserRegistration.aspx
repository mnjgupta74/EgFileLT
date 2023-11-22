<%@ Page Title="EgUserRegistration" Language="C#" MasterPageFile="~/MasterPage/MasterPage4.master"
    AutoEventWireup="true" CodeFile="EgUserRegistration.aspx.cs" Inherits="WebPages_EgUserRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/UserControls/CustomTextBox.ascx" TagName="CustomTextBox" TagPrefix="ucl" %>
<%@ Register Assembly="WebControlCaptcha" Namespace="WebControlCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../js/jquery-ui.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" src="../../js/jquery-3.6.0.min.js"></script>
    <%--<script src="../../js/jquery.min.js" type="text/javascript"></script>--%>

    <%--<script src="../../js/jquery-ui.min.js" type="text/javascript"></script>--%>

    <%--<script src="../../md5.js" type="text/javascript" language="javascript"></script>--%>
    <script type="text/javascript" src="../../js/SHA256.js"></script>

    <style type="text/css">
        .ui-datepicker {
            background: #1C92D3;
            border: 1px solid #555;
            color: #EEE;
        }
    </style>

    <%--<script type="text/javascript">
        $(function () {
            $('#<%=txtBirthDate.ClientID %>').focus(
          function () {
              $(this).datepicker({
                  changeMonth: true,
                  changeYear: true,
                  dateFormat: 'dd/mm/yy',
                  yearRange: "1940:+0",
                  onSelect: function () {
                      currenttab = $(this).attr("tabindex");
                      nexttab = currenttab * 1 + 1;
                      $("[tabindex='" + nexttab + "']").focus();
                  }
              });

          });
        });
    </script>--%>

    <script type='text/javascript'>
        $(document).ready(function () {

            RefreshCaptcha();
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


        // NEW vALIDATION 6 aPRIL 2022

       
          $(function () {
            //$('#<%=txtpassward.ClientID %>')
            $('#<%=txtpassward.ClientID %>').keypress(function (e) {
                var keyCode = e.keyCode || e.which;
                //$("#lblError").html("");
                //Regex for Valid Characters i.e. Alphabets and Numbers.
                var regex = /^[>,<'"?]+$/;
                //Validate TextBox value against the Regex.
                var isValid = !regex.test(String.fromCharCode(keyCode));
                if (!isValid) {
                    //$("#lblError").html("");
                }
                return isValid;
            });
        });
        function Reset() {
            document.getElementById("<%=txtpassward.ClientID %>").value = "";
            document.getElementById("<%=txtrepassward.ClientID %>").value = "";
        }
    </script>

    <script language="javascript" type="text/javascript">
        function LoginLength() {
            var LID = document.getElementById("<%=txtlogin.ClientID%>");
            var myString = LID.value;
            var Stringlen = myString.length;

            if (Stringlen < 4) {
                alert("LoginId must be at least 4 characters");
                LID.value = "";
                control.focus();
                return false;
            }
        }
        function encpass() {
            var newpassword = document.getElementById("<%=txtpassward.ClientID %>").value;
            //document.getElementById("<%=txtpassward.ClientID %>").value = hex_md5(newpassword);
            document.getElementById("<%=txtpassward.ClientID %>").value = SHA256(newpassword);

            var cnfpassword = document.getElementById("<%=txtrepassward.ClientID %>").value;
            //document.getElementById("<%=txtrepassward.ClientID %>").value = hex_md5(cnfpassword);
            document.getElementById("<%=txtrepassward.ClientID %>").value = SHA256(cnfpassword);
            return true;
        }
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




                return true;
            }
            else {

                return false;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function white_space(field) {
            field.value = field.value.replace(/^\s+/, "");
        }
        function Success() {
            alert('User Created Successfully');
            window.location = "../NewEgUserProfile.aspx";
        }
        function Count(text, long) {

            var maxlength = new Number(long); // Change number to your max length.

            if (document.getElementById('<%=txtaddress.ClientID%>').value.length > maxlength)

                text.value = text.value.substring(0, maxlength);
            return false;



        }
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

            var Stringlen = field.value.length;

            if (Stringlen < 2) {
                alert("LoginId must be have at least 2 characters");
                field.value = "";
                control.focus();
                return false;
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
        function dateValidation() {

            var dtObj = document.getElementById("<%=txtBirthDate.ClientID %>");

            var dtStr = dtObj.value;

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
            if (YearDt < 1900) {
                alert('Year cannot be less than 1900')
                dtObj.value = ""
                return false
            }
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }
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
                alert("Invalid Date.");
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

        function CheckPasswordPolicy() {
            Reset();
            window.open("EgfrmPasswordPolicy.aspx", "Null", "dialogWidth:500px; dialogHeight:200px; scroll:no; center:yes");
        }
        function CheckPinCode() {
            var pin = document.getElementById("<%=txtpincode.ClientID %>").value;

            if (pin == "000000") {
                document.getElementById("<%=txtpincode.ClientID %>").value = "";
                alert('PinCode is Invalid.!');
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function generaterandomnumber() {
            //var subject = Math.floor(Math.random() * 99999) + 1;
            var subject = Math.floor(Math.random() * 900000) + 100000;

            return subject;
        }
        function RefreshCaptcha() {

            var img = document.getElementById("imgCaptcha");
            img.src = "../../Image/captcha.ashx?arg=" + generaterandomnumber();
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
                    <img src="../../App_Themes/images/progress.gif" />
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
            <table style="width: 100%; text-align: center;">
                <tr>
                    <td class=" PageTitle " width="50%">Login&nbsp;Form
                    </td>
                    <td width="20%">
                        <asp:HyperLink ID="hplSignin" runat="server" NavigateUrl="~/Default.aspx" Text="Registered? Sign In"
                            ForeColor="Blue"></asp:HyperLink>
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
                            <tr>
                                <td rowspan="2">Login Id: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtlogin" runat="server" MaxLength="20" Style="width: 180px; text-align: left;" TabIndex="1" AutoComplete="Off"
                                        onkeypress="white_space(this)" OnChange="LoginIdValidation(this);" ToolTip="LoginId have Atleast one Alphabet Character and Special character not allowed(Ex-Ram@0141)"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rgv" ResourceName="rgv" ValidationGroup="de"
                                        runat="server" ControlToValidate="txtlogin" ErrorMessage="Special character not allowed and LoginId have Atleast one Alphabet Character ."
                                        Text="*" ValidationExpression="^.*[A-Za-z]([a-z]|[A-Z]|[0-9]|[.]|[_@])*$" Display="Dynamic"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    &nbsp;<asp:Image ID="Image1" runat="server" Height="20px" Width="20px" Visible="false" />
                                    &nbsp;<asp:LinkButton ID="BtnCheckUser" runat="server" OnClick="ChechExistingLogin"
                                        OnClientClick="Reset();" Text="Check Availability"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%-- <asp:LinkButton ID="LinkButtonOTP" runat="server"
                                        Text="Already have OTP ??" ></asp:LinkButton>--%>
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Password: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpassward" runat="server" MaxLength="20" TextMode="Password" Style="width: 180px;"
                                         onselectstart="return false" onpaste="return false;" onCopy="return false" onCut="return false" onDrag="return false" onDrop="return false"

                                        AutoComplete="Off" TabIndex="2" ToolTip="Password min Length 6 and max Length 20 and Check Password Formet. (e.g Nic@123)"></asp:TextBox>
                                    
                                    
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtpassward"
                                        Text="*" ErrorMessage="Enter Password" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                    &nbsp;<%--<asp:RegularExpressionValidator ID="REVNew" runat="server" ControlToValidate="txtpassward"
                                        CssClass="XMMessage" ErrorMessage="Check Password Formet. (e.g Nic@123)" ValidationExpression="^(?=.*\d)(?=.*[A-Z])(?=.*[@\$=!:.#*%?^/&amp;_-]).{6,20}$"
                                        Text="*" ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>--%>&nbsp;
                                    &nbsp;&nbsp;
                                    <ajaxToolkit:PasswordStrength ID="PS" runat="server" TargetControlID="txtpassward"
                                        DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="0"
                                        PrefixText="Strength:" MinimumNumericCharacters="1" MinimumSymbolCharacters="1"
                                        MinimumUpperCaseCharacters="1" RequiresUpperAndLowerCaseCharacters="true" CalculationWeightings="50;15;15;20" />
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    <asp:LinkButton ID="lnkPassword" runat="server" Text=" Password Policy (?)" ForeColor="Maroon"
                                        OnClientClick="Javascript:CheckPasswordPolicy()"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>Confirm Password: <font color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtrepassward" runat="server" MaxLength="15" TextMode="Password"
                                         onselectstart="return false" onpaste="return false;" onCopy="return false" onCut="return false" onDrag="return false" onDrop="return false"
                                        AutoComplete="Off" Style="width: 180px;" TabIndex="3"></asp:TextBox>
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
                            <table style="width: 85%; text-align: left; color: #0033CC;" border="1" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <td colspan="2" style="height: 20px; background-color: #1C92D3; color: White">Remitter's Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td>First Name/Company Name : <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfirst" runat="server" Style="color: gray;" MaxLength="30" OnChange="CharCheck(this);"
                                            AutoComplete="Off" onkeypress="white_space(this)" TabIndex="4"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfirst"
                                            Text="*" ErrorMessage="Enter First Name" InitialValue="FirstName" ValidationGroup="de"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator><ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2"
                                            runat="server" TargetControlID="txtfirst" WatermarkText="FirstName">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        &nbsp;<asp:RegularExpressionValidator ID="RfvFirstName" runat="server" Text="Special character and Space not allowed in First Name.!"
                                            ErrorMessage="Special character and Space not allowed in First Name.!" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtfirst" ValidationExpression="^[a-zA-Z0-9]+$"
                                            ValidationGroup="de"></asp:RegularExpressionValidator>&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>LastName :-<font color="red">*</font> &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlast" runat="server" Style="color: gray;" MaxLength="30" OnChange="CharCheck(this);"
                                            TabIndex="5" onkeypress="white_space(this)">
                                        </asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtlast"
                                            Text="*" ErrorMessage="Enter Last Name" InitialValue="LastName" ValidationGroup="de"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator><ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1"
                                            runat="server" TargetControlID="txtlast" WatermarkText="LastName">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <asp:RegularExpressionValidator ID="RfvLastName" runat="server" Text="Special character and Space not allowed in Last Name.!"
                                            ErrorMessage="Special character and Space not allowed in Last Name.!" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtlast" ValidationExpression="^[a-zA-Z0-9]+$"
                                            ValidationGroup="de"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 28px">DOB: <font color="red">*</font>
                                    </td>
                                    <td style="height: 28px">
                                        <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" onkeypress="Javascript:return NumberOnly(event)"
                                            AutoComplete="Off" onpaste="return false" OnChange="javascript:return dateValidation()" Width="150px"
                                            TabIndex="7"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                                            PopupButtonID="txtBirthDate" TargetControlID="txtBirthDate">
                                        </ajaxToolkit:CalendarExtender>
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
                                            AutoComplete="Off" onkeypress="white_space(this)" TabIndex="9"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvemailidvld" runat="server" ControlToValidate="txtEmailId"
                                            Text="*" ErrorMessage="Enter E-Mail.!" ValidationGroup="de"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpForemail" runat="server" ControlToValidate="txtEmailId"
                                            Text="Enter Correct EmailId" ErrorMessage="Enter Correct EmailId" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Address <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtaddress" runat="server" MaxLength="40" Width="300px" TextMode="MultiLine"
                                             onselectstart="return false" onpaste="return false;" onCopy="return false" onCut="return false" onDrag="return false" onDrop="return false"
                                            AutoComplete="Off" OnkeyPress="javascript:Count(this,100);" TabIndex="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvaddressvld" runat="server" ControlToValidate="txtaddress"
                                            Text="*" ErrorMessage="Enter Address" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtaddress"
                                            Text="Special character not allowed in address.!" CssClass="XMMessage" ErrorMessage="Special character not allowed in address.!"
                                            ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>Country: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcountry" runat="server" CssClass="DropDownlist" Width="150px"
                                            TabIndex="13">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="India" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlcountry"
                                            Text="*" ErrorMessage="Select your Country " ValidationGroup="de" InitialValue="0"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            Text="*" ErrorMessage="Select your State " ValidationGroup="de" InitialValue="0"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>City: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" runat="server" MaxLength="40" Style="width: 150px;" TabIndex="15"
                                            AutoComplete="Off" OnChange="CharCheck(this);"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCity"
                                            Text="*" ErrorMessage="Select your City " ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mobile Phone <font color="red">*</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <input type="text" name="mbno91" value="+91" style="width: 23px;" disabled="disabled" />
                                        <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" Style="width: 150px;" onblur="Javascript:return NumberOnly(event)"
                                            AutoComplete="Off" TabIndex="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtmobile"
                                            Text="*" ErrorMessage="Enter Mobile Number" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtmobile"
                                            Text="Enter Correct Mob No." ErrorMessage="Enter Correct Mob No." ValidationGroup="de"
                                            ForeColor="Red" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblerrormsg" runat="server" Style="color: red;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PinCode: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpincode" runat="server" MaxLength="6" onblur="Javascript:return NumberOnly(event);"
                                            AutoComplete="Off" onchange="Javascript:return CheckPinCode();" Style="width: 150px;" TabIndex="17"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvpincodevld" runat="server" ControlToValidate="txtpincode"
                                            Text="*" ErrorMessage="Enter PinCode." ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtpincode"
                                            Text="Pin code must be 6 numeric digits" ValidationExpression="\d{6}" ErrorMessage="Pin code must be 6 numeric digits"
                                            runat="server" ValidationGroup="de" ForeColor="Red" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>TIN/Actt.No./VehicleNo/Taxid:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTIN" runat="server" MaxLength="20" Width="150px" TabIndex="18" AutoComplete="Off"></asp:TextBox>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtTIN"
                                            Text="Special character not allowed in TIN!!" CssClass="XMMessage" ErrorMessage="Special character not allowed in TIN!"
                                            ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Select Your Security Question:-<font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsecQuestion" runat="server" TabIndex="19" Width="250px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlsecQuestion" runat="server" ControlToValidate="ddlsecQuestion"
                                            Text="*" ErrorMessage="Select your Security Question " ValidationGroup="de" InitialValue="0"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;&nbsp;&nbsp;Answer :-<font color="red">*</font>
                                        <asp:TextBox ID="txtsecAnswer" runat="server" TabIndex="20" AutoComplete="Off" TextMode="Password"
                                            onkeypress="white_space(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSecAnswer" runat="server" ControlToValidate="txtsecAnswer"
                                            Text="*" ErrorMessage="Enter Your Security Answer" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtsecAnswer"
                                            Text="Special character not allowed.!" CssClass="XMMessage" ErrorMessage="Special character not allowed.!"
                                            ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-12" style="padding-top: 5px;">
                                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                                Enter Captcha
                                            </div>
                                    </td>
                                    <td align="left">
                                        <div class="input-group col-sm-8" style="padding-left: 0px;">
                                            <asp:TextBox ID="inpHide" runat="server" AutoComplete="Off" Text="" TabIndex="3"
                                                Style=""
                                                CssClass="form-control col-sm-11"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqinpHide" runat="server" ControlToValidate="inpHide"
                                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de" Style="position: absolute"></asp:RequiredFieldValidator>
                                        </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                            Captcha Code
                                        </div>
                                    </td>
                                    <td align="left">
                                        <div class="col-sm-12" style="padding-top: 5px; padding-bottom: 15px; width: 35%;">
                                            <div class=" col-sm-8" style="padding-left: 0px;">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div style="text-align: left;">
                                                            <span style="float: left; width: 90%">
                                                                <img src="../../Image/captcha.ashx" height="35px" id="imgCaptcha" width="95%" /></span>
                                                            <span style="float: center;"><a
                                                                href="#" onclick="javascript:RefreshCaptcha();" id="imge">
                                                                <img src="../../Image/refresh.png" style="margin-top: 15px;" /></a> </span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:ValidationSummary ID="summary1" ShowSummary="false" runat="server" ValidationGroup="de"
                                            ShowMessageBox="true" DisplayMode="BulletList" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="102px" OnClick="btnSubmit_Click"
                                            TabIndex="19" ValidationGroup="de" OnClientClick="isStrongPassword(); " />
                                        <asp:LinkButton ID="LinkButton1" runat="server"
                                            Text=""></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </td>
                </tr>
            </table>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="MpeOTP" runat="server" TargetControlID="LinkButton1"
                    PopupControlID="PanelOTP" CancelControlID="btnOTPCancel" BackgroundCssClass="DivBackground" BehaviorID="popup1">
                </ajaxToolkit:ModalPopupExtender>

                <asp:Panel ID="PanelOTP" runat="server" BackColor="honeydew" Height="150px" Width="400px"
                    Style="display: none">
                    <div style="text-align: left; margin-top: 10px; padding: 20px;">

                        <b><span style="color: #336699; font-family: Arial CE; font-size: 13px">Mobile No:-</span></b>&nbsp;
                            <asp:TextBox ID="txtmob" runat="server" MaxLength="10" Width="60%"></asp:TextBox>
                        <br />
                        <br />
                        <b><span style="color: #336699; font-family: Arial CE; font-size: 13px">Enter Your OTP:-</span></b>&nbsp;
                            <asp:TextBox ID="txtOTP" runat="server" TextMode="Password" MaxLength="6"></asp:TextBox>
                        <br />
                        <br />
                        <div style="text-align: center;">
                            <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnVerify_Click" />
                            <asp:Button ID="btnOTPCancel" runat="server" Text="Cancel" />

                            <asp:LinkButton ID="lnkresendcode" runat="server" OnClick="lnkresendcode_Click"
                                Text="Resend OTP"></asp:LinkButton>
                            <asp:Label ID="lblermsg" runat="server" ForeColor="Red"></asp:Label>
                            <asp:HiddenField ID="hdnuserid" runat="server" />
                        </div>
                    </div>
                </asp:Panel>

            </div>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lblmsgdis"
                    PopupControlID="Panel1" CancelControlID="Button2" BackgroundCssClass="DivBackground" BehaviorID="popup2">
                </ajaxToolkit:ModalPopupExtender>

                <asp:Panel ID="Panel1" runat="server" BackColor="White" Height="150px" Width="400px"
                    Style="display: none">
                    <div style="text-align: left; margin-top: 10px; padding: 20px;">

                        <asp:Label ID="lblmsgdis" runat="server"></asp:Label>
                        <br />
                        <br />
                        <div style="text-align: center;">
                            <asp:Button ID="Btnconfirm" runat="server" Text="Confirm" OnClick="Btnconfirm_Click" OnClientClick="encpass();" />
                            <asp:Button ID="Button2" runat="server" Text="Cancel" />
                        </div>
                    </div>
                </asp:Panel>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
