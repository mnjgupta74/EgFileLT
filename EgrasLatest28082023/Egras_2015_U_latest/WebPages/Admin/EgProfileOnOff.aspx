<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgProfileOnOff.aspx.cs" Inherits="WebPages_Admin_EgProfileOnOff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .PnlDesign {
            border: solid 1px #000000;
            height: 100px;
            width: 450px;
            overflow-y: scroll;
            background-color: aliceblue;
            font-size: 15px;
            font-family: Arial;
            overflow-x: hidden;
        }
        .txtbox {
            background: url("../../Image/dropdown.png");
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
        }
    </style>
    <div id="divProfileOnOff" style="width: 1127px">
        <fieldset runat="server" id="Division" style="width: 1000px; margin-left: 100px">
            <span id="spanPD" runat="server">
                <legend style="color: #336699; font-weight: bold"><%--Profile or --%> User Activate/Deactivate</legend>
            </span><span id="spanDiv" runat="server"></span>
            <div style="width: 100%; max-height: 350px; margin-top: 15px" align="center">
                <table style="width: 995px">
                    <tr>
                        <td valign="top" colspan="2" align="left">
                            <asp:RadioButtonList runat="server" ID="rbltype" AutoPostBack="true"
                                RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rbltype_SelectedIndexChanged">
                                <%--<asp:ListItem Text="Profile" Value="1" Selected="True"></asp:ListItem>--%>
                                <asp:ListItem Text="User" Value="2" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>LoginId:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtLoginId" runat="server" Width="200px" AutoPostBack="true" OnTextChanged="txtLoginId_TextChanged"></asp:TextBox>
                        </td>
                        <td id="chkuserAct1" runat="server" visible="false">
                            IsActive:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chkuserAct" runat="server" />
                        </td>
                    </tr>
                    <tr id="trprofilelabel" runat="server">
                        <td>
                            <b>
                                <asp:Label ID="lblsel" runat="server" Visible="false">Selected Profiles:</asp:Label></b>
                        </td>
                        <td>
                            <b>
                                <asp:Label ID="lblrem" runat="server" Visible="false">Removed Profiles:</asp:Label></b>
                        </td>
                    </tr>
                    <tr id="trprofilepanel" runat="server">
                        <td align="left">
                            <asp:Panel ID="PanelSelected" runat="server" CssClass="PnlDesign" Visible="false">
                                <asp:CheckBoxList ID="chksel" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="chksel_SelectedIndexChanged"></asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                        <td colspan="4">
                            <asp:Panel ID="PanelRemove" runat="server" CssClass="PnlDesign" Visible="false" Style="background-color: darkseagreen">
                                <asp:ListBox ID="lstRemove" runat="server" Style="width: 450px; background-color: darkseagreen; border: none; height: inherit" Visible="false"></asp:ListBox>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>

                        <td align="right" style="width:55%;">
                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" Width="100px" Visible="false" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
