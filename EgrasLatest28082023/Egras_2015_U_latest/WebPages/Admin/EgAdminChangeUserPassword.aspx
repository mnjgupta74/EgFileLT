<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAdminChangeUserPassword.aspx.cs" Inherits="WebPages_Admin_EgAdminChangeUserPassword"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../md5.js" type="text/javascript" language="javascript"></script>--%>
    <script src="../../js/SHA256.js"></script>

    <script language="javascript" type="text/javascript">
        function CheckPasswordPolicy() {
            Reset();
            window.showModalDialog("../Account/EgfrmPasswordPolicy.aspx", "Null", "dialogWidth:500px; dialogHeight:200px; scroll:no; center:yes");
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
    </script>

    <script language="javascript" type="text/javascript">
        function isStrongPassword() {


            var control = document.getElementById("<%=txtNewPassword.ClientID%>");


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


            var newpassword = document.getElementById("<%=txtNewPassword.ClientID %>").value;
            //document.getElementById("<%=txtNewPassword.ClientID %>").value = hex_md5(newpassword);
            document.getElementById("<%=txtNewPassword.ClientID %>").value = SHA256(newpassword);

            var cnfpassword = document.getElementById("<%=txtCnfPassword.ClientID %>").value;
            //document.getElementById("<%=txtCnfPassword.ClientID %>").value = hex_md5(cnfpassword);
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = SHA256(cnfpassword);

            return true;
        }
        function Reset() {
            
            document.getElementById("<%=txtNewPassword.ClientID %>").value = "";
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = "";

        }
        $(document).ready(function () {
            $("a[id$='ctl00_lnkLogout']").click(function () {
            document.getElementById("<%=txtNewPassword.ClientID %>").value = "";
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = "";
            });

        });
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
            <fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px">
                <center>
                    <asp:RadioButtonList ID="rblList" runat="server"
                        RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rblList_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">Reset Password</asp:ListItem>
                        <asp:ListItem Value="1">Reset Login Attempts</asp:ListItem>
                    </asp:RadioButtonList>
                </center>
                <legend style="color: #336699; font-weight: bold">
                    <asp:Label ID="lblLegend" runat="server" Text="Change-User Password"></asp:Label>
                </legend>
                <table style="width: 100%" align="center" id="ChangePasswordTable" runat="server">
                    <tr style="height: 45px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Enter LoginID:-</span></b>&nbsp;
                        </td>
                        <td>&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtlogin"
                            runat="server" MaxLength="20" Style="width: 180px;" TabIndex="1" onkeypress="white_space(this)"
                            OnTextChanged="txtlogin_TextChanged" AutoPostBack="true" ToolTip="LoginId have Atleast one Alphabet Character and Special character not allowed(Ex-Ram@0141)"></asp:TextBox>&nbsp;&nbsp; &nbsp;
                            <%--  <asp:RegularExpressionValidator ID="rgv" ResourceName="rgv" ValidationGroup="vldLoginId"
                                runat="server" ControlToValidate="txtlogin" ErrorMessage="Special character not allowed and LoginId have Atleast one Alphabet Character ."
                                Text="*" ValidationExpression="^.*[A-Za-z]([a-z]|[A-Z]|[0-9]|[.]|[_@])*$" Display="Dynamic"
                                ForeColor="Red"></asp:RegularExpressionValidator>--%>
                            &nbsp;<asp:Image ID="Image1" runat="server" Height="20px" Width="20px" Visible="false" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <fieldset runat="server" id="Fieldset1" style="width: 500px; margin-left: 150px; border-left-style: groove; border-right-style: groove; border-bottom-style: groove; border-top-style: groove; border-color: #336699"
                    visible="false">
                    <legend style="color: #336699; font-weight: bold">User-Personal Detail</legend>
                    <asp:Panel ID="Panel1" runat="server">
                        <table id="TableshowDetails" width="100%" align="center" runat="server">
                            <tr>
                                <td style="width: 200px">
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">DOB :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDOB" runat="server" Text="LabelDOB"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">E-Mail ID :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblEmail" runat="server" Text="LabelEmail"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Mobile:-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblMobile" runat="server" Text="LabelMobile"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Address :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" Text="LabelAddress"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Question :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LabelQuestion" runat="server" Text="Question"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Answer :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LabelAnswer" runat="server" Text="Answer"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lbluserid" runat="server" Text="LabelUserId" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" height="40px" colspan="2">
                                    <asp:LinkButton ID="lnkChangePassword" runat="server" Text=" Click For Change Password"
                                        OnClick="lnkChangePassword_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <table style="width: 100%" align="center" id="MainTable" runat="server" visible="false">
                    <tr style="height: 30px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">New Password:-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="20" TextMode="Password"
                                Style="width: 180px;" AutoComplete="Off" ToolTip="Password min Length 6 and max Length 20 and Check Password Formet. (e.g Nic@123)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtNewPassword"
                                Text="*" ErrorMessage="Enter Password" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                            &nbsp;&nbsp; &nbsp;&nbsp;
                            <ajaxToolkit:PasswordStrength ID="PS" runat="server" TargetControlID="txtNewPassword"
                                DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="0"
                                PrefixText="Strength:" MinimumNumericCharacters="1" MinimumSymbolCharacters="1"
                                MinimumUpperCaseCharacters="1" RequiresUpperAndLowerCaseCharacters="true" CalculationWeightings="50;15;15;20" />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:LinkButton ID="lnkPassword" runat="server" Text=" Password Policy (?)" ForeColor="Maroon"
                                OnClientClick="Javascript:CheckPasswordPolicy()"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 250px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Confirm New Password :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtCnfPassword" runat="server" MaxLength="15" TextMode="Password"
                                AutoComplete="Off" Style="width: 180px;"></asp:TextBox>
                            &nbsp;<asp:CompareValidator ID="CompareValidator" runat="server" ControlToCompare="txtNewPassword"
                                ControlToValidate="txtCnfPassword" ErrorMessage="Confirm password not match."
                                Text="Confirm password not match." ValidationGroup="de" ForeColor="Red"></asp:CompareValidator></td>
                    </tr>
                    <tr style="height: 30px">
                        <td colspan="2" align="center">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                OnClientClick="isStrongPassword();" Text="Create New Password" ValidationGroup="de" />
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" Text="Click For Cancel" runat="server" OnClick="btnCancel_Click"
                               OnClientClick="Reset();"  CssClass="btn" />&nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
