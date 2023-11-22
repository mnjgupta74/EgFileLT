<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgMapBudgethead.aspx.cs" Inherits="WebPages_TO_EgMapBudgethead" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function NumberOnly(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 3 && parts[1].length >= 3) return false;
        }

        function CheckBudget() {
            var Blength = document.getElementById("<%=txtBudgetHead.ClientID %>");

            if (Blength.value.length < 13) {
                alert("Please fill correct BudgetHead");
                Blength.value = "";
            }
        }
    </script>

    <fieldset id="fieldamount" class="fieldset" runat="server" width="70%">
        <legend style="color: #005CB8; font-size: small">Map Budget Head</legend>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr align="center">
                <td style="height: 30px" valign="middle">
                    <b>BudgetHead :-</b>&nbsp;
                    <asp:TextBox ID="txtBudgetHead" runat="server" Width="120px" MaxLength="13" onkeypress="Javascript:return NumberOnly(event)"
                        onchange="JavaScript:return CheckBudget()"></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="9999-99-999-99-99"
                        MaskType="None" CultureName="en-US" TargetControlID="txtBudgetHead" AcceptNegative="None"
                        runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfctxtbudgethead" runat="server" ErrorMessage="*"
                        ControlToValidate="txtBudgetHead" ValidationGroup="VldInsert">
                    </asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td style="width: 50%">
                    <b>Departments :-</b> &nbsp;
                    <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="True" Width="60%" class="chzn-select"
                        Height="20px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="ddldepartment" ValidationGroup="VldInsert"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" valign="bottom">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        ValidationGroup="VldInsert" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
