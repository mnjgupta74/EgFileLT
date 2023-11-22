<%@ Page Title="Egras.Raj.Nic.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgOfficeChangePassword.aspx.cs" Inherits="WebPages_Account_EgOfficeChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../md5.js" type="text/javascript" language="javascript"></script>--%>
    <script src="../../js/SHA256.js"></script>

    <script language="javascript" type="text/javascript">
        function CheckPasswordPolicy() {
            Reset();
            window.showModalDialog("Account/EgfrmPasswordPolicy.aspx", "Null", "dialogWidth:500px; dialogHeight:200px; scroll:no; center:yes");
        }
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
            <%--document.getElementById("<%=txtNewPassword.ClientID %>").value = hex_md5(newpassword);--%>
            document.getElementById("<%=txtNewPassword.ClientID %>").value = SHA256(newpassword);

            var cnfpassword = document.getElementById("<%=txtCnfPassword.ClientID %>").value;
            <%--document.getElementById("<%=txtCnfPassword.ClientID %>").value = hex_md5(cnfpassword);--%>
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
                <legend style="color: #336699; font-weight: bold">Office-Change Password</legend>
                <table width="80%" align="center" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 35px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Trasury : </span></b>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTreasury" runat="server" class="chzn-select" AutoPostBack="true" Width="250px"
                                OnSelectedIndexChanged="ddlTreasury_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                ControlToValidate="ddlTreasury" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                                Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 35px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Office Name : </span></b>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOfficeName" runat="server" class="chzn-select" AutoPostBack="true" Width="250px"
                                OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="--Select Office--"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                ControlToValidate="ddlOfficeName" ValidationGroup="de" InitialValue="0" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 35px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Login Id:-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtlogin" runat="server" MaxLength="20" Style="width: 180px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlogin"
                                Text="*" ErrorMessage="*" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:Image ID="Image1" runat="server" Height="20px" Width="20px" Visible="false"
                                ToolTip="LoginID Not Exist.!" />
                        </td>
                    </tr>
                    <td>
                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">New Password:-</span></b>&nbsp;
                    </td>
                    <td style="height: 35px">
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
                    <tr>
                        <td style="height: 35px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Confirm New Password :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtCnfPassword" runat="server" MaxLength="15" TextMode="Password"
                                AutoComplete="Off" Style="width: 180px;"></asp:TextBox>
                            &nbsp;<asp:CompareValidator ID="CompareValidator" runat="server" ControlToCompare="txtNewPassword"
                                ControlToValidate="txtCnfPassword" ErrorMessage="Confirm password not match."
                                Text="Confirm password not match." ValidationGroup="de" ForeColor="Red"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                OnClientClick="isStrongPassword();" Text="Create New Password" ValidationGroup="de" />
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" Text="Click For Cancel" runat="server" OnClick="btnCancel_Click"
                                OnClientClick="Reset();" CssClass="btn" />&nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
