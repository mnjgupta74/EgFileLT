<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage4.master" AutoEventWireup="true"
    CodeFile="EgDeptChangePassword.aspx.cs" Inherits="WebPages_EgDeptChangePassword"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../md5.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript">

        function clickme(seed) {
            var oldpassword = document.getElementById("<%=txtOldPassword.ClientID %>").value;
            var newpassword = document.getElementById("<%=txtNewPassword.ClientID %>").value;
            var cnfpassword = document.getElementById("<%=txtCnfPassword.ClientID %>").value;
            document.getElementById("<%=txtOldPassword.ClientID %>").value = hex_md5(oldpassword);
            document.getElementById("<%=txtNewPassword.ClientID %>").value = hex_md5(newpassword);
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = hex_md5(cnfpassword);
            return true;
        }

        function CheckPasswordPolicy() {
            window.showModalDialog("../Account/EgfrmPasswordPolicy.aspx", "Null", "dialogWidth:500px; dialogHeight:200px; scroll:no; center:yes");
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
            document.getElementById("<%=txtOldPassword.ClientID%>").value = hex_md5(oldpassword)

            var newpassword = document.getElementById("<%=txtNewPassword.ClientID %>").value;
            document.getElementById("<%=txtNewPassword.ClientID %>").value = hex_md5(newpassword);

            var cnfpassword = document.getElementById("<%=txtCnfPassword.ClientID %>").value;
            document.getElementById("<%=txtCnfPassword.ClientID %>").value = hex_md5(cnfpassword);

            return true;
        } 
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <div style="border: 1px solid green">
                    <table>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="TDblank" valign="middle" colspan="3">
                                <span style="font-size: 14pt"><strong>
                                    <asp:Literal ID="ltheadUsers" runat="server" Text="Change User Password" /><br />
                                </strong><span class="remark">(All the Fields are Mandatory)</span></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblOldPassword" AssociatedControlID="txtOldPassword" Text="Old Password"
                                    runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <%--</asp:Panel>--%>
                        <tr>
                            <td>
                                <asp:Label ID="lblNewPassword" AssociatedControlID="txtNewPassword" Text="New Password"
                                    runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNew" ControlToValidate="txtNewPassword" CssClass="XMMessage"
                                    ErrorMessage="New Password is required" SetFocusOnError="true" ValidationGroup="vldg"
                                    Display="None" runat="server" />
                                <%--                             <asp:RegularExpressionValidator ID="REVNew" runat="server" CssClass="XMMessage" ValidationExpression="^(?=.*\d)(?=.*[A-Z])(?=.*[@\$=!:.#%?^/&]).{6,10}$"
                                    ValidationGroup="vldg" ControlToValidate="txtNewPassword" ErrorMessage="Password Must Have Special Character,Capital Letter and Numeric Value"></asp:RegularExpressionValidator>--%>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:LinkButton ID="lnkPassword" runat="server" Text=" Password Policy (?)" ForeColor="Maroon"
                                    OnClientClick="Javascript:CheckPasswordPolicy()"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCnfPassword" AssociatedControlID="txtCnfPassword" Text="Confirm New Password"
                                    runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtCnfPassword" runat="server" TextMode="Password" />
                            </td>
                            <td>
                                <asp:CompareValidator ID="rfvCnf" ControlToValidate="txtCnfPassword" CssClass="XMMessage"
                                    ErrorMessage="New password and confirm password Should be same." SetFocusOnError="true"
                                    ValidationGroup="vldg" ControlToCompare="txtNewPassword" runat="server"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                    OnClientClick="isStrongPassword();" Text="Submit" ValidationGroup="vldg" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" Text="Reset" runat="server" OnClick="btnCancel_Click"
                                    CssClass="btn" />&nbsp;
                                <%--<asp:Button ID ="btnBack" Text="Back" runat="server" CssClass="btn" PostBackUrl="~/WebPages/Home.aspx" /></td>--%>
                        </tr>
                        <tr>
                            <td>
                                <td colspan="3" align="right">
                                    <asp:LinkButton ID="lnklogin" runat="server" OnClick="lnklogin_Click">Go To Login</asp:LinkButton>
                        </tr>
                    </table>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
