
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgForgotPassword.aspx.cs"
    Inherits="WebPages_EgForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControls/WebCaptcha.ascx" TagName="WebCaptchal" TagPrefix="cap" %>
<%@ Register Src="~/UserControls/HorizontalMenu.ascx" TagName="HMenu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <style type="text/css">
        img {
            margin-top: 15px;
            margin-bottom: 12px;
        }
    </style>
    <link href="../../App_Themes/Theme1/ifms.css" rel="stylesheet" type="text/css" />

    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>--%>
    <script type="text/javascript" src="../../js/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../js/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/SHA256.js"></script>


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#pp").click(function () {
                //alert('hi');
                $("#MyPopup").modal("show");
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function Success() {

            alert('Password changed Successfully.');
            window.location = "../../Default.aspx";
        }
        function isStrongPassword() {


            var control = document.getElementById('txtNewPassword');


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



            var newpassword = document.getElementById('txtNewPassword').value;

            document.getElementById('txtNewPassword').value = SHA256(newpassword);

            var cnfpassword = document.getElementById('txtCnfPassword').value;
            document.getElementById('txtCnfPassword').value = SHA256(cnfpassword);

            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function generaterandomnumber() {
            var subject = Math.floor(Math.random() * 99999) + 1;
            return subject;
        }
        function RefreshCaptcha() {

            var img = document.getElementById("imgCaptcha");
            img.src = "../../Image/captcha.ashx?arg=" + generaterandomnumber();
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txt_Login').change(function () {
                //var a = $("[id*=rdblReset] input:checked").val();
                var flag = false;
                if (isNaN($('#txt_Login').val())) {
                    flag = true;
                }
                var len = $('#txt_Login').val().length;
                if (len >= 6 && len < 10 && !flag) {
                    alert("Enter Correct Mobile Number !!");
                    $('#txt_Login').val("");
                    $('#txt_Login').focus();
                }
            });
        });
    </script>
</head>
<body>
    <!-- Modal Popup -->
    <div id="MyPopup" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">Password Policy
                    </h4>
                </div>
                <div class="modal-body">
                    1. Password should contain atleast 6 characters.
           <br />
                    2. Password should contain atleast one numeric digit.
           <br />
                    3. Password should contain atleast one Capital Letter.
           <br />
                    4. Password should contain atleast one special character from !@#$*_-~=.
           <br />

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Popup -->
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        </ajaxToolkit:ToolkitScriptManager>

        <center>
            <%--<input id="inpHide" type="hidden" runat="server" />--%>
            <img id="imgHide" runat="server" height="1" width="1" alt="" />
            <div style="border: 1px solid green; width: 1024px;">
                <table align="center" id="MasterTable" cellspacing="0" cellpadding="0" width="1024px">
                    <tr>
                        <td align="right" valign="top">
                            <ucl:HMenu ID="hmenu2" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid green; width: 1024px;" id="divVerify" runat="server">
                <table style="text-align: center;">
                    <tr>
                        <td colspan="3" align="center" style="font-size: medium;">
                            <h2 style="text-decoration: underline; color: #FFF; background-color: #41AEBF; margin: 0;">Recover Your Account</h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>

                    <tr>
                        <td style="font-size: 18px;" colspan="3" align="center">
                            <asp:RadioButtonList runat="server" ID="rdblReset" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdblReset_click" AutoPostBack="true">
                                <asp:ListItem Text="Forgot Password" Value="01" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Forgot Login ID" Value="02"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="3">
                            <asp:Label ID="Label1" runat="server" Style="color: blue; font-size: small;" Text="Recover your account:"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="width: 291px" colspan="3">
                            <asp:Label ID="lblMsg" runat="server" Visible="false" Style="color: red; font-size: small; text-align: center"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblLogin" Text="Login ID/Mobile No" Style="font-size: 20px;"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txt_Login" runat="server" Width="100%" Height="30px" MaxLength="30" AutoComplete="Off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Login"
                                ErrorMessage="RequiredFieldValidator" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Enter Captcha </td>
                        <td align="center">
                            <asp:TextBox ID="inpHide" runat="server" AutoComplete="Off" Text="" Width="100%" Height="30px"></asp:TextBox>

                        </td>
                        <td align="left">
                            <asp:RequiredFieldValidator ID="reqinpHide" runat="server" ControlToValidate="inpHide"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="submit" Style="position: absolute"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="padding: 10px;">
                        <td></td>
                        <td>
                            <img src="../../Image/captcha.ashx" height="35px" id="imgCaptcha" width="200px" />
                            <a
                                href="#" onclick="javascript:RefreshCaptcha();" id="imge">
                                <img src="../../Image/refresh.png" style="margin-top: 15px;" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <br />
                    <tr>
                        <td colspan="3" style="text-align: center; padding-bottom: 20px;">
                            <asp:Button ID="btnSub" Style="font-size: Small; float: left; margin-right: 10%; width: 40%; height: 35px; font-size: 15px; font-weight: 600;" runat="server" Text="Submit" OnClick="btnSub_Click" ValidationGroup="submit" />
                            <asp:Button ID="btnBack" Style="font-size: Small; float: left; margin-left: 10%; width: 40%; height: 35px; font-size: 15px; font-weight: 600;" runat="server" Text="Back" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <fieldset runat="server" id="lstrecord" style="width: 700px; margin-left: 150px; margin-right: 150px; margin-top: 50px;"
                    visible="false">
                    <legend style="color: #336699; font-weight: bold">Please Verify Your Details</legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="right">
                                <span style="color: Red; font-family: Arial CE; font-size: 12px;">* Mobile Number Should
                                be Valid for Change Password</span>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">Full Name :-</span></b>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text="FullName" Enabled="false" ForeColor="#336699"
                                    Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">Mobile Number:-</span></b>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblMobile" runat="server" Text="Mobile" Enabled="false" ForeColor="#336699"
                                    Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <img src="../../Image/icon-mobile.gif" style="width: 19px; margin-top: 9px" />&nbsp;&nbsp;
                            <span style="font-family: Arial CE; font-size: 14px; font-family: Calibri; color: #336699">code for reset password.</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnverify" runat="server" Text="Continue" OnClick="btnverify_Click" />
                                <asp:Button ID="Cancelbtn" runat="server" Text="Cancel" OnClick="Cancelbtn_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div>
                <fieldset runat="server" id="FieldsetCode" style="width: 700px; margin-left: 150px; margin-right: 150px; margin-top: 50px;"
                    visible="false">
                    <legend style="color: #83c922; font-weight: bold; font-size: larger;">Check Your Phone</legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <a href="../../Default.aspx">Go Home</a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 35px">
                                <span style="color: #336699; font-family: Arial CE; font-size: 28px; font-family: Calibri;">We have sent your 
                                   <span id="lblloginmsg" runat="server" visible="false"><span style="color: #83c922;">Login ID</span> along with </span>
                                    <span style="color: #83c922;">OTP</span>. Enter the OTP below to continue to reset your password.</span>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="txtcode" Style="font-size: 30px;" runat="server" Width="50%" Height="35px" MaxLength="6" AutoComplete="Off"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Enter 6 digits Code."
                                    ControlToValidate="txtcode" ValidationGroup="vldInsert" Display="Dynamic" ValidationExpression="[0-9]{6,}"></asp:RegularExpressionValidator>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                    TargetControlID="txtcode" WatermarkText="######">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 35px">
                                <asp:LinkButton ID="lknResendCode" runat="server" Text="Resend me code Again." OnClick="lknResendCode_Click"></asp:LinkButton>

                                <asp:Button ID="btnmobilecode" runat="server" Text="Continue" OnClick="btnmobilecode_Click" Width="200px"
                                    ValidationGroup="vldInsert" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div id="divchangePass" runat="server" visible="false">
                <fieldset runat="server" id="Fieldset1" style="width: 700px; margin-left: 150px; margin-right: 150px; margin-top: 50px;">
                    <legend style="color: #336699; font-weight: bold">Change Password</legend>
                    <table id="tblPasswordDetails" runat="server" align="center" width="100%" visible="false">
                        <tr>
                            <td align="right" colspan="2">
                                <a href="../../Default.aspx">Go Home</a>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 35px;">
                                <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">New Password
                                :-</span></b>&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="20" TextMode="Password" AutoComplete="Off" />
                                <asp:RequiredFieldValidator ID="rfvNew" ControlToValidate="txtNewPassword" ErrorMessage="New Password is required"
                                    SetFocusOnError="true" ValidationGroup="vldg" ForeColor="Red" Display="None"
                                    runat="server" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                           <%-- <asp:LinkButton ID="lnkPassword" runat="server" Text=" Password Policy (?)" ForeColor="Maroon"
                                OnClientClick="Javascript:CheckPasswordPolicy()"></asp:LinkButton>--%>
                                <a id="pp" href="#" class="pull-right">Password Policy (?)</a>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 35px;">
                                <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">Confirm New
                                Password:-</span></b>&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtCnfPassword" runat="server" MaxLength="20" TextMode="Password" AutoComplete="Off" />
                                <asp:CompareValidator ID="rfvCnf" ControlToValidate="txtCnfPassword" CssClass="XMMessage"
                                    ErrorMessage="New password and confirm password Should be same." SetFocusOnError="true"
                                    ValidationGroup="vldg" ControlToCompare="txtNewPassword" runat="server"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnReSetPass" runat="server" Text="Reset Password" OnClick="btnReSetPass_Click"
                                    OnClientClick="isStrongPassword();" ValidationGroup="vldg" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div id="divResetAttempt" runat="server" visible="false">
                <fieldset runat="server" id="FieldsetResetAttempt" style="width: 700px; margin-left: 150px; margin-right: 150px; margin-top: 50px;">
                    <legend style="color: #336699; font-weight: bold">Reset Attempt</legend>
                    <div id="divBtnReset" runat="server" style="margin-top: 5px;">
                        <asp:Button ID="btnResetAttempt" runat="server" Text="Reset Attempt" OnClick="btnResetAttempt_Click" />
                    </div>
                </fieldset>
            </div>
        </center>
    </form>
</body>
</html>
