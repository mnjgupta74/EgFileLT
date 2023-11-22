<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgChangePassword.aspx.cs" Inherits="WebPages_Account_EgChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <%--<script src="../../md5.js" type="text/javascript" language="javascript"></script>--%>
    <script src="../../js/SHA256.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">

        <%--function clickme(seed) {
            var oldpassword = document.getElementById("<%=txtOldPassword.ClientID %>").value;
            var newpassword = document.getElementById("<%=txtNewPassword.ClientID %>").value;
            var cnfpassword = document.getElementById("<%=txtCnfPassword.ClientID %>").value;
           
            document.getElementById("<%=txtOldPassword.ClientID %>").value = SHA256(oldpassword);
            document.getElementById("<%=txtNewPassword.ClientID %>").value = SHA256(newpassword);
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = SHA256(cnfpassword);
            return true;
        }--%>

        function CheckPasswordPolicy() {
            window.open("EgfrmPasswordPolicy.aspx", "Null", "dialogWidth:400px; dialogHeight:200px; scroll:no; center:yes");
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

            var oldpassword = document.getElementById("<%=txtOldPassword.ClientID%>").value;
            //document.getElementById("<%=txtOldPassword.ClientID%>").value = hex_md5(oldpassword)
            document.getElementById("<%=txtOldPassword.ClientID%>").value = SHA256(oldpassword)

            var newpassword = document.getElementById("<%=txtNewPassword.ClientID %>").value;
            //document.getElementById("<%=txtNewPassword.ClientID %>").value = hex_md5(newpassword);
            document.getElementById("<%=txtNewPassword.ClientID %>").value = SHA256(newpassword);

            var cnfpassword = document.getElementById("<%=txtCnfPassword.ClientID %>").value;
            //document.getElementById("<%=txtCnfPassword.ClientID %>").value = hex_md5(cnfpassword);
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = SHA256(cnfpassword);

            return true;
        }
        <%--function PasswordEncrypt(i) {

            if (i == 1) {
                var oldpassword = document.getElementById("<%=txtOldPassword.ClientID%>").value;
                document.getElementById("<%=txtOldPassword.ClientID%>").value = SHA256(oldpassword)
            }
            else if (i == 2) {
                isStrongPassword();
                var newpassword1 = document.getElementById("<%=txtNewPassword.ClientID %>").value;
                newPassword = newpassword1;
                document.getElementById("<%=txtNewPassword.ClientID %>").value = SHA256(newpassword1);
            }
            else {
                var cnfpassword = document.getElementById("<%=txtCnfPassword.ClientID %>").value;
                document.getElementById("<%=txtCnfPassword.ClientID %>").value = SHA256(cnfpassword);
                if (newPassword === cnfpassword) {

                    $("#showMsg").hide();
                    $("#ctl00_ContentPlaceHolder1_btnSubmit").show();
                }
                else {
                    $("#showMsg").show();
                    $("#ctl00_ContentPlaceHolder1_btnSubmit").hide();

                }
            }--%>
        //}
        function Reset() {
            document.getElementById("<%=txtOldPassword.ClientID%>").value = "";
            document.getElementById("<%=txtNewPassword.ClientID %>").value = "";
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = "";
        }

        $(document).ready(function () {
            $("a[id$='ctl00_lnkLogout']").click(function () {
                document.getElementById("<%=txtOldPassword.ClientID%>").value = "";
            document.getElementById("<%=txtNewPassword.ClientID %>").value = "";
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = "";
            });

        });
    </script>
    <style>
        input[type=password] {
            height: 40px !important;
            width: -webkit-fill-available;
        }

        .btn {
            height: 40px !important;
            width: 125px !important;
        }

        .mandatory {
            color: #ff0000;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <div _ngcontent-c6="" class="tnHead minus2point5per">
                    <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
                        <span _ngcontent-c6="" style="color: #FFF">Change Password</span></h2>
                    <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Change Password" />
                </div>
                <div style="border: 1px solid green">
                    <table width="90%">
                        <br />

                        <tr>
                            <td>
                                <asp:Label ID="lblOldPassword" AssociatedControlID="txtOldPassword" Text="Old Password"
                                    runat="server" /><span class="mandatory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOldPassword" CssClass="form-control" runat="server" TextMode="Password" AutoComplete="Off" />
                            </td>
                            <td></td>
                        </tr>
                        <%--</asp:Panel>--%>
                        <tr>
                            <td>
                                <asp:Label ID="lblNewPassword" AssociatedControlID="txtNewPassword" Text="New Password"
                                    runat="server" /><span class="mandatory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewPassword" CssClass="form-control" runat="server" TextMode="Password" AutoComplete="Off" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNew" ControlToValidate="txtNewPassword" CssClass="XMMessage"
                                    ErrorMessage="New Password is required" SetFocusOnError="true" ValidationGroup="vldg"
                                    Display="None" runat="server" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:LinkButton ID="lnkPassword" runat="server" Text=" Password Policy (?)" ForeColor="#428bca"
                                    OnClientClick="Javascript:CheckPasswordPolicy()"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCnfPassword" AssociatedControlID="txtCnfPassword" Text="Confirm New Password"
                                    runat="server" /><span class="mandatory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCnfPassword" CssClass="form-control" runat="server" TextMode="Password" AutoComplete="Off" />
                            </td>
                            <td>
                                <asp:CompareValidator ID="rfvCnf" ControlToValidate="txtCnfPassword" CssClass="XMMessage"
                                    ErrorMessage="New password and confirm password Should be same." SetFocusOnError="true"
                                    ValidationGroup="vldg" ControlToCompare="txtNewPassword" runat="server"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="padding:15px;">
                                <asp:Button ID="btnSubmit"  runat="server"  CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="isStrongPassword();" ValidationGroup="vldg" />
                                &nbsp;
                                <asp:Button ID="btnCancel"  Text="Reset" runat="server" OnClick="btnCancel_Click" OnClientClick="Reset();"
                                    CssClass="btn  btn-primary" />&nbsp;
                                <%--<asp:Button ID ="btnBack" Text="Back" runat="server" CssClass="btn" PostBackUrl="~/WebPages/Home.aspx" /></td>--%>
                        </tr>
                        <br />

                    </table>
                    <br />
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
