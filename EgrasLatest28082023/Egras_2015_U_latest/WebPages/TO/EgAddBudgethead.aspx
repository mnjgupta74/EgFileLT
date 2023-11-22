<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAddBudgethead.aspx.cs" Inherits="WebPages_TO_EgAddBudgethead" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
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
    <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Department-Revenue" class="pull-left">
            <span _ngcontent-c6="" class="pull-right" style="color: #FFF">Add Budget Head</span></h2>
    </div>
    <table id="Table1" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr align="center" style="height:30px">
            <td valign="middle" style="height:30px">
                <b><span>BudgetHead :-</span></b></td>
            <td style="height:30px">
                    <asp:TextBox ID="txtBudgetHead" runat="server" Height="100%" Width="80%" style="font-size:18px !important" MaxLength="13" onkeypress="Javascript:return NumberOnly(event)"
                        onchange="JavaScript:return CheckBudget()"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="9999-99-999-99-99"  
                    MaskType="None" CultureName="en-US" TargetControlID="txtBudgetHead" AcceptNegative="None"
                    runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfctxtbudgethead" runat="server" ErrorMessage="*"
                    ControlToValidate="txtBudgetHead" ValidationGroup="VldInsert">
                </asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    ValidationGroup="VldInsert" />
            </td>
        </tr>
    </table>
</asp:Content>
