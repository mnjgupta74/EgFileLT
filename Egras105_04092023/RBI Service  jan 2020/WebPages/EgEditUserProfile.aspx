<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgEditUserProfile.aspx.cs" Inherits="WebPages_EgEditUserProfile" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/UserControls/CustomTextBox.ascx" TagName="CustomTextBox" TagPrefix="ucl" %>
<%@ Register Assembly="WebControlCaptcha" Namespace="WebControlCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../js/jquery-ui.css" rel="stylesheet" type="text/css" />--%>

    <script src="../js/Control.js"></script>
    <%--<script src="../js/jquery-ui.min.js" type="text/javascript"></script>--%>
    <%--<script src="../../js/JScript.js" type="text/javascript"></script>--%>
    <link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageHeader.css" rel="stylesheet" />
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
    </script>

    <style type="text/css">
        .ui-datepicker {
            background: #1C92D3;
            border: 1px solid #555;
            color: #EEE;
        }
    </style>

    <script language="javascript" type="text/javascript">
        $(function () {
            $('#<%=txtBirthDate.ClientID %>').on('focus',
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
            if (YearDt < 1900) {
                alert('year cannot be less than 1900')
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
        function white_space(field) {
            field.value = field.value.replace(/^\s+/, "");
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
                    <img src="../App_Themes/Images/progress.gif" />
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
            <div id="dialog" style="display: none">
                * please Change your password 
    <br></br>
                * now it is mandatory to change password after 45 days
            </div>
            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Challan Detail">
                    <span _ngcontent-c6="" style="color: #FFF">Edit Profile</span></h2>
                <%--<img src="../../Image/help1.png" style="height: 44px; width: 34px;"  title="Edit Profile" />--%>
            </div>
            <table style="width: 100%; text-align: center; border-spacing: 0px;">
                <tr>
                    <td>
                        <center>
                            <table style="width: 100%; text-align: left;" border="1" id="tblBody">
                                <%-- <tr>
                                    <td colspan="2" style="height: 20px; background-color: #1C92D3; color: White">
                                        Employee Personal Details:
                                    </td>
                                </tr>--%>

                                <tr>
                                    <td>Name : <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfirst" runat="server" Style="color: gray;" MaxLength="30" OnChange="CharCheck(this);"
                                            TabIndex="1"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfirst"
                                            Text="*" ErrorMessage="Enter First Name" InitialValue="FirstName" ValidationGroup="de"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator><ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2"
                                            runat="server" TargetControlID="txtfirst" WatermarkText="FirstName">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        &nbsp;&nbsp;&nbsp; LastName : <font color="red">*</font> &nbsp;
                                        <asp:TextBox ID="txtlast" runat="server" Style="color: gray;" MaxLength="30" OnChange="CharCheck(this);"
                                            TabIndex="2">
                                        </asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtlast"
                                            Text="*" ErrorMessage="Enter Last Name" InitialValue="LastName" ValidationGroup="de"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator><ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1"
                                            runat="server" TargetControlID="txtlast" WatermarkText="LastName">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>UserType: <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlusertype" runat="server" Width="150px" TabIndex="3">
                                            <asp:ListItem Value="0">Select User Type</asp:ListItem>
                                            <asp:ListItem Value="10">General User</asp:ListItem>
                                            <asp:ListItem Value="2">E-TO</asp:ListItem>
                                            <asp:ListItem Value="3">TO</asp:ListItem>
                                            <asp:ListItem Value="4">Office</asp:ListItem>
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
                                    <td style="height: 28px">DOB : <font color="red">*</font>
                                    </td>
                                    <td style="height: 28px">
                                        <asp:TextBox ID="txtBirthDate" runat="server" CssClass="datepicker" onkeypress="Javascript:return NumberOnly(event)"
                                            onpaste="return false" OnChange="javascript:return dateValidation()" Width="150px"
                                            TabIndex="5"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                            CultureName="en-US" TargetControlID="txtBirthDate" AcceptNegative="None" runat="server">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter D.O.B."
                                            Text="*" ControlToValidate="txtBirthDate" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Email Id :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailId" runat="server" MaxLength="100" Style="width: 150px;"
                                            TabIndex="7"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtEmailId"
                                            ErrorMessage="Enter Email ID.!" Text="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpForemail" runat="server" ControlToValidate="txtEmailId"
                                            ErrorMessage="Fill Correct EmailId" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="de"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Address : 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtaddress" runat="server" MaxLength="40" Width="300px" TextMode="MultiLine"
                                            OnkeyPress="javascript:Count(this,100);" TabIndex="8"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtaddress"
                                            Text="Special character not allowed in address.!" CssClass="XMMessage" ErrorMessage="Special character not allowed in address.!"
                                            ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>
                                </tr>
                                <tr>
                                    <td>Country : <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcountry" runat="server" CssClass="DropDownlist" Width="150px"
                                            TabIndex="9">
                                            <asp:ListItem Text="India" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlcountry"
                                            Text="*" ErrorMessage="Select Country.!" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>State : <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlstate" runat="server" CssClass="DropDownlist" Width="150px"
                                            TabIndex="10">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlstate"
                                            ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>City : <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" runat="server" MaxLength="40" Style="width: 150px;" OnChange="CharCheck(this);"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCity"
                                            Text="*" ErrorMessage="Enter City Name.!" ValidationGroup="de"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 14px; font-weight: 600;">Mobile Phone : <font color="red">*</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        +91
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" Style="width: 150px; border: #009900 3px solid; font-size: 17px; font-weight: 600;" onblur="Javascript:return NumberOnly(event)"
                                            TabIndex="12"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtmobile"
                                            Text="*" ErrorMessage="Enter Mobile Number." ValidationGroup="de"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobile"
                                            ErrorMessage="Enter Mobile phone number" ValidationGroup="de"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtmobile"
                                            ErrorMessage="Enter Correct Mob No." ValidationGroup="de" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblerrormsg" runat="server" Style="color: red;"></asp:Label>
                                        <asp:LinkButton ID="LinkButtonOTP" runat="server"
                                            Text="Already have OTP ??"></asp:LinkButton>
                                    </td>

                                </tr>
                                <tr>
                                    <td>PinCode :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpincode" runat="server" MaxLength="6" onblur="Javascript:return NumberOnly(event)"
                                            onchange="Javascript:return CheckPinCode();" Style="width: 150px;" TabIndex="13"></asp:TextBox>
                                        &nbsp;
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtpincode"
                                            ErrorMessage="Enter PinCode!" ValidationGroup="de"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtpincode"
                                            ValidationExpression="\d{6}" ErrorMessage="Pin code must be 6 numeric digits"
                                            runat="server" ValidationGroup="de" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>TIN/Actt.No./VehicleNo/Taxid :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTIN" runat="server" AutoComplete="off" MaxLength="20" Width="150px" TabIndex="14"></asp:TextBox>
                                        &nbsp;
                                        <%-- <asp:RequiredFieldValidator ID="rfcTIN" runat="server" ControlToValidate="txtTIN"
                                            ErrorMessage="Enter TIN/AcctNo/VehicleNo/Taxid!" ValidationGroup="de"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Select Your Security Question : <font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsecQuestion" runat="server" TabIndex="15">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlsecQuestion" runat="server" ControlToValidate="ddlsecQuestion"
                                            Text="*" ErrorMessage="Select your Security Question " ValidationGroup="de" InitialValue="0"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                        &nbsp;&nbsp; &nbsp; &nbsp;Answer :-<font color="red">*</font>
                                        <asp:TextBox ID="txtsecAnswer" runat="server" TabIndex="16" onkeypress="white_space(this)"
                                            TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSecAnswer" runat="server" ControlToValidate="txtsecAnswer"
                                            Text="*" ErrorMessage="Enter Your Security Answer" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtsecAnswer"
                                            Text="Special character not allowed.!" CssClass="XMMessage" ErrorMessage="Special character not allowed.!"
                                            ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="de" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="lblpass" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        <asp:TextBox ID="txtpass" runat="server" TabIndex="17"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:ValidationSummary ID="summary1" ShowSummary="false" runat="server" ValidationGroup="de"
                                            ShowMessageBox="true" DisplayMode="BulletList" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="102px" OnClick="btnUpdate_Click"
                                            TabIndex="20" ValidationGroup="de" />
                                        &nbsp; &nbsp; 
                                        <asp:HyperLink ID="hpSignin" runat="server" NavigateUrl="~/Default.aspx" Text="Back"
                                            ForeColor="Blue" Visible="false"></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </td>
                </tr>
            </table>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="MpeOTP" runat="server" TargetControlID="LinkButtonOTP"
                    PopupControlID="PanelOTP" CancelControlID="btnOTPCancel" BackgroundCssClass="DivBackground" BehaviorID="popup1">
                </ajaxToolkit:ModalPopupExtender>

                <asp:Panel ID="PanelOTP" runat="server" BackColor="honeydew" Height="150px" Width="400px"
                    Style="display: none">
                    <div style="text-align: left; margin-top: 10px; padding: 20px;">

                        <b><span style="color: #336699; font-family: Arial CE; font-size: 13px">Mobile No : </span></b>&nbsp;
                            <asp:TextBox ID="txtmob" runat="server" MaxLength="10" Width="60%"></asp:TextBox>
                        <br />
                        <br />
                        <b><span style="color: #336699; font-family: Arial CE; font-size: 13px">Enter Your OTP : </span></b>&nbsp;
                            <asp:TextBox ID="txtOTP" runat="server" TextMode="Password" MaxLength="6"></asp:TextBox>
                        <br />
                        <br />
                        <div style="text-align: center;">
                            <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnVerify_Click" />
                            <asp:Button ID="btnOTPCancel" runat="server" Text="Cancel" />

                            <asp:LinkButton ID="lnkresendcode" runat="server" OnClick="lnkresendcode_Click"
                                Text="Resend OTP"></asp:LinkButton>
                            <%--<asp:LinkButton ID="LinkButton1" runat="server" 
                                        Text=""></asp:LinkButton>--%>
                            <asp:Label ID="lblermsg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
