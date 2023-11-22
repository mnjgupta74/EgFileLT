<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgBankPage.aspx.cs" Inherits="WebPages_EgBankPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

    function NumberOnly(ctrl) {
        var ch;

        if (window.event) {
            ch = ctrl.keyCode;
        }
        else if (ctrl.which) {
            ch = ctrl.which;
        }
        if ((ch >= 48 && ch <= 57))
            return true;

        else
            return false;
    }

    
</script>

<html>
<head id="Head1" runat="server">
</head>
<body>
    <form id="Form1" runat="server">
    <table align="center">
        <tr>
            <td align="center">
                <p>
                    <ajaxToolKit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </ajaxToolKit:ToolkitScriptManager>
                    <em>GRN </em>:
                    <asp:TextBox ID="txtGRN" runat="server" Width="100px" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfcgrn" runat="server" ControlToValidate="txtGRN"
                        ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <em>Amount </em>:
                    <asp:TextBox ID="txtAmt" runat="server" Width="100px" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmt"
                        ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Button ID="btngenerate" runat="server" OnClick="btngenerate_Click" Text="Verify" ValidationGroup="vldInsert" />
                </p>
            </td>
        </tr>
        <tr   >
            <td align="center"> 
                <div id="divRecord" visible="false" runat ="server">
                    <table id="tblRecord" class="style1" visible="false" align="center">
                        <tr>
                            <td class="style9">
                                GRN
                            </td>
                            <td class="style8">
                                <asp:Label ID="grnlbl" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                FullName
                            </td>
                            <td class="style8">
                                <asp:Label ID="fullnamelbl" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name1
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead1" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount1
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount1" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name2
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead2" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount2
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount2" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name3
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead3" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount3
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount3" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name4
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead4" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount4
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount4" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name5
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead5" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount5
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount5" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name6
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead6" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount6
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount6" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name7
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead7" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount7
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount7" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name8
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead8" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount8
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount8" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                Head_Name9
                            </td>
                            <td class="style8">
                                <asp:Label ID="lblhead9" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10">
                                Head_Amount9
                            </td>
                            <td>
                                <asp:Label ID="lblheadamount9" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                &nbsp;
                            </td>
                            <td class="style8">
                                &nbsp;
                            </td>
                            <td class="style10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style9">
                                &nbsp;
                            </td>
                            <td class="style8">
                                &nbsp;
                            </td>
                            <td class="style10">
                                NetAmount
                            </td>
                            <td>
                                <asp:Label ID="netamountlbl" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
