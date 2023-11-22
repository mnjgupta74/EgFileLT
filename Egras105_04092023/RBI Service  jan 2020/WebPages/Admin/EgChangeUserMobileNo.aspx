<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgChangeUserMobileNo.aspx.cs" Inherits="WebPages_Admin_EgChangeUserMobileNo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../md5.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript">
        function NumberOnly(evt) {

            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 3 && parts[1].length >= 3) return false;
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
           
                    </center>
                <legend  style="color: #336699; font-weight: bold"   >
                    <asp:Label ID="lblLegend" runat="server" Text="Change-Mobile Number"></asp:Label>
                </legend>
                <table style="width: 100%" align="center" id="ChangePasswordTable" runat="server" >
                    <tr style="height: 45px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                 LoginID:-</span></b>&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtlogin"
                                runat="server" MaxLength="20" Style="width: 180px;" TabIndex="1" onkeypress="white_space(this)"></asp:TextBox>&nbsp;&nbsp; &nbsp;
                            <%--  <asp:RegularExpressionValidator ID="rgv" ResourceName="rgv" ValidationGroup="vldLoginId"
                                runat="server" ControlToValidate="txtlogin" ErrorMessage="Special character not allowed and LoginId have Atleast one Alphabet Character ."
                                Text="*" ValidationExpression="^.*[A-Za-z]([a-z]|[A-Z]|[0-9]|[.]|[_@])*$" Display="Dynamic"
                                ForeColor="Red"></asp:RegularExpressionValidator>--%>
                           
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Width="100px" />
                           
                        </td>
                        
                       
                    </tr>
                    <tr>

                        <td colspan="4" align="right">
                            <asp:Button ID="btnreset"  Text="Reset"  runat="server"  Visible="false" Width="100px" OnClick="btnreset_Click" />

                        </td>
                    </tr>
                 
                </table>
                <fieldset runat="server" id="Fieldset1" style="width: 500px; margin-left: 150px;
                    border-left-style: groove; border-right-style: groove; border-bottom-style: groove;
                    border-top-style: groove; border-color: #336699" visible="false">
                    <legend style="color: #336699; font-weight: bold">User-Personal Detail</legend>
                    <asp:Panel ID="Panel1" runat="server">
                        <table id="TableshowDetails" width="100%" align="center" runat="server">
                            <tr>
                                <td style="width: 200px">
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                        DOB :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDOB" runat="server" Text="LabelDOB"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                        E-Mail ID :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblEmail" runat="server" Text="LabelEmail"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                        Mobile:-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblMobile" runat="server" Text="LabelMobile"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                        Address :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" Text="LabelAddress"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                        Question :-</span></b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LabelQuestion" runat="server" Text="Question"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                        Answer :-</span></b>&nbsp;
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
                                 <td>
                                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                              MobileNo:-</span></b>&nbsp;
                                 </td>
                                <td>
                                     <asp:TextBox ID="txtMobileNumber" onkeypress="Javascript:return NumberOnly(event)"
                                         runat="server" MaxLength="10" Style="width: 180px;"></asp:TextBox>
                             
                                </td>
                                 
                             </tr>
                            <tr>
                                <td>
                                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                           Confirm MobileNo:-</span></b>&nbsp;
                                 </td>
                                <td>
                                     <asp:TextBox ID="txtConfirmMobileNumber" onkeypress="Javascript:return NumberOnly(event)"
                                         runat="server" MaxLength="10" Style="width: 180px;"></asp:TextBox>
                             
                                </td>
                            </tr>
                            <tr>
                                <td align="right" height="40px" colspan="2">
                                    <asp:LinkButton ID="lnkChangeMobileNo" runat="server" Text=" Click For Change Mobile Number" OnClick="lnkChangeMobileNo_Click" 
                                       ></asp:LinkButton>
                                </td>
                            </tr>
                        </table>

                      
                    </asp:Panel>
                </fieldset>
               
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

