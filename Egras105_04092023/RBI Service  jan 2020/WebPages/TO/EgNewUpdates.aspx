<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgNewUpdates.aspx.cs" Inherits="WebPages_TO_EgNewUpdates" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function CheckedChange(objCheckBox) {
            if (confirm('Are you sure you want to display on/off?')) {
                __doPostBack("'" + objCheckBox.id + "'", '');
                return true;
            }
            else {
                objCheckBox.checked = false;
                return false;
            }
        }
    </script>

    <fieldset runat="server" id="lstrecord">
        <legend style="color: #336699; font-weight: bold">Upload-File</legend>
        <center>
            <table style="width: 100%" align="center" id="MainTable">
                <tr>
                    <td align="center">
                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                            &nbsp; File Name&nbsp;&nbsp; :-</span></b>&nbsp;
                        <textarea id="txtfilename" runat="server" style="width: 540px"></textarea>
                        <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtfilename" ValidationGroup="Vld"
                            runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <b><span style="width: 500px; color: #336699; font-family: Arial CE; font-size: 13px;">
                            &nbsp;Upload File :- </span></b>&nbsp;
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="430px" Height="25px" />
                        <asp:Button ID="btnUpload" runat="server" Text="Save" Width="100px" OnClick="btnUpload_Click"
                            ValidationGroup="Vld" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <center>
                            <hr />
                            <asp:Repeater ID="reptNewUpdates" runat="server" OnItemDataBound="reptNewUpdates_ItemDataBound">
                                <HeaderTemplate>
                                    <table border="1" cellspacing="0" width="50%">
                                        <tr style="font-weight: bold;">
                                            <td align="left">
                                                SNo
                                            </td>
                                            <td align="left">
                                                Heading
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="left">
                                            <%#Eval("pdfID")%>
                                            <asp:HiddenField ID="hdnflag" runat="server" Value='<%#Eval("flag")%>' />
                                        </td>
                                        <td>
                                            <%#Eval("pdfName")%>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Display (on/off)" AutoPostBack="true"
                                                OnCheckedChanged="CheckBox2_CheckedChanged" onclick="javascript:return CheckedChange(this)" />
                                            <asp:HiddenField ID="hdnid" runat="server" Value='<%#Eval("pdfID")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </center>
                    </td>
                </tr>
            </table>
        </center>
    </fieldset>
</asp:Content>
