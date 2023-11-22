<%@ Page Title="Egras.Raj.Nic.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgAddNewOffice.aspx.cs" Inherits="WebPages_Admin_EgAddNewOffice" %>

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
                <legend style="color: #336699; font-weight: bold">Office-Master</legend>
                <table style="width: 100%" align="center" id="MainTable">
                    <tr style="height: 40px">
                        <td align="center">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Office Id:-</span></b>&nbsp;
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtofficeid" runat="server" OnChange="Javascript:return NumberOnly(this)"
                                MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtofficeid"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnVerify" runat="server" Text="ReVerify" ValidationGroup="de" OnClick="btnVerify_Click"
                                Visible="false" />
                            <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />  --%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="height: 40px">
                            <asp:Label ID="lblmsg" runat="server" Text="Label1" Visible="false"></asp:Label>
                            <div>
                                <fieldset runat="server" id="Fieldset1" style="width: 450px;" visible="false">
                                    <legend style="color: #336699; font-weight: bold">Office-Detail</legend>
                                    <table>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    Office Name:- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblOffice" runat="server" Text="OfficeName"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    Treasury Code :-</span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblTreas" runat="server" Text="Treasury"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    DDO-Code :- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblDDO" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    CreateDate :- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblFromdate" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    ParentOfficeid :- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblpid" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                                    DeptCode :- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lbldept" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
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
