<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAddDDONo.aspx.cs" Inherits="WebPages_Admin_EgAddDDONo" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function NumberOnly(field) {
            var valid = "0123456789"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("  Only Numeric Value Allowed.!");
                field.focus();
                field.select();
                field.value = "";
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
                <legend style="color: #336699; font-weight: bold">DDO-Master</legend>
                <table style="width: 100%" align="center" id="MainTable">
                    <tr style="height: 40px">
                        <td align="center">
                            <b><span style="width: 250px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                DDOCode:-</b>&nbsp;
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtddocode" runat="server" OnChange="Javascript:return NumberOnly(this)"
                                MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtddocode"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            <span style="width: 250px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                <b>Treasury Code : -</b></span>
                            <asp:TextBox ID="txtptreascode" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtptreascode"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                           <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                            
                             <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />--%>
                            &nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <span style="width: 250px; color: Red; font-family: Arial CE; font-size: 13px;">
                                    <asp:Label ID="lblmsg" runat="server" Text="Label1" Visible="false"></asp:Label></span>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="height: 40px">
                            <div>
                                <fieldset runat="server" id="Fieldset1" style="width: 500px;" visible="false">
                                    <legend style="color: #336699; font-weight: bold">DDO-Detail</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    DDOName :- </span></b>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblddoName" runat="server" Text="DDO Name"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    TreasuryCode :-</span></b>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTreas" runat="server" Text="Treasury"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    Status :- </span></b>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFlag" runat="server" Text="Status"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                               <asp:Button ID="btnSubmit" runat="server" Text="ReVerify" OnClick="btnSubmit_Click" /> 
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
