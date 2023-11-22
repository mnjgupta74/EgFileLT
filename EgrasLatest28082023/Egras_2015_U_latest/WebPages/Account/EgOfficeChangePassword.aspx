<%@ Page Title="Egras.Rajasthan.gov.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgOfficeChangePassword.aspx.cs" Inherits="WebPages_Account_EgOfficeChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../md5.js" type="text/javascript" language="javascript"></script>--%>
    <script src="../../js/SHA256.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
        $(function () {
            $(".chzn-select").chosen({
                search_contains: true
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                //Binding Code Again
                $(".chzn-select").chosen({
                    search_contains: true
                });
            }
        });
        function CheckPasswordPolicy() {
            Reset();
            //alert('hi');
            window.open("EgfrmPasswordPolicy.aspx", "Null", "dialogWidth:500px; dialogHeight:200px; scroll:no; center:yes");
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
    <style type="text/css">
        input[type=submit] {
            width: 20%;
        }

        .form-control {
            display: inline;
        }
    </style>
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
            <%--       <fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px">
                <legend style="color: #336699; font-weight: bold">Office-Change Password</legend>--%>
            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Office-Change Passwordt" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Office-Change Password</span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="left" title="Office-Change Password" />
            </div>
            <table width="100%" align="center" border="1" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="height: 35px">
                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">Trasury : </span></b>&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        ControlToValidate="ddlTreasury" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                        Style="text-align: center">*</asp:RequiredFieldValidator>

                    </td>
                    <td style="padding: 1%;">
                        <asp:DropDownList ID="ddlTreasury" runat="server" class="chzn-select" AutoPostBack="true" Width="40%"
                            OnSelectedIndexChanged="ddlTreasury_SelectedIndexChanged" Height="35px">
                            <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td style="height: 35px">
                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">Office Name : </span></b>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                            ControlToValidate="ddlOfficeName" ValidationGroup="de" InitialValue="0" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="padding: 1%;">
                        <asp:DropDownList ID="ddlOfficeName" runat="server" class="chzn-select" AutoPostBack="true" Width="40%"
                            OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged" Height="35px">
                            <asp:ListItem Value="0" Text="--Select Office--"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td style="height: 35px">
                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">Login Id:-</span></b>&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlogin"
                        Text="*" ErrorMessage="*" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>

                    </td>
                    <td style="padding: 1%;">
                        <asp:TextBox ID="txtlogin" runat="server" Height="35px" class="form-control" MaxLength="20" Style="width: 40%;"></asp:TextBox>

                        <asp:Image ID="Image1" runat="server" Height="20px" Width="20px" Visible="false"
                            ToolTip="LoginID Not Exist.!" />
                    </td>
                </tr>
                <td>
                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">New Password:-</span></b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtNewPassword"
                        Text="*" ErrorMessage="Enter Password" ValidationGroup="de" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td style="height: 35px; padding: 1%;">
                    <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" MaxLength="20" TextMode="Password" Height="35px"
                        Style="width: 40%;" AutoComplete="Off" ToolTip="Password min Length 6 and max Length 20 and Check Password Formet. (e.g Nic@123)"></asp:TextBox>


                    <ajaxToolkit:PasswordStrength ID="PS" runat="server" TargetControlID="txtNewPassword"
                        DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="0"
                        PrefixText="Strength:" MinimumNumericCharacters="1" MinimumSymbolCharacters="1"
                        MinimumUpperCaseCharacters="1" RequiresUpperAndLowerCaseCharacters="true" CalculationWeightings="50;15;15;20" />

                    <asp:LinkButton ID="lnkPassword" Class="pull-right" runat="server" Text=" Password Policy (?)" ForeColor="Maroon"
                        OnClientClick="return CheckPasswordPolicy();"></asp:LinkButton>
                </td>
                </tr>
                    <tr>
                        <td style="height: 35px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">Confirm New Password :-</span></b>&nbsp;
                        </td>
                        <td style="padding: 1%;">
                            <asp:TextBox ID="txtCnfPassword" runat="server" class="form-control" MaxLength="15" TextMode="Password" Height="35px"
                                AutoComplete="Off" Style="width: 40%;"></asp:TextBox>
                            &nbsp;<asp:CompareValidator ID="CompareValidator" runat="server" ControlToCompare="txtNewPassword"
                                ControlToValidate="txtCnfPassword" ErrorMessage="Confirm password not match."
                                Text="Confirm password not match." ValidationGroup="de" ForeColor="Red"></asp:CompareValidator>
                        </td>
                    </tr>
                <tr>
                    <td colspan="2" align="center" style="padding: 1%;">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Height="35px"
                            OnClientClick="isStrongPassword();" Text="Create New Password" ValidationGroup="de" Width="20%" />
                        <asp:Button ID="btnCancel" Text="Click For Cancel" runat="server" OnClick="btnCancel_Click" Height="35px"
                            OnClientClick="Reset();" CssClass="btn btn-primary" Width="20%" />&nbsp;
                    </td>
                </tr>
            </table>
            <%--</fieldset>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
