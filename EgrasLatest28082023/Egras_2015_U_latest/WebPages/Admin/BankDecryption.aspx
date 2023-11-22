<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="BankDecryption.aspx.cs" Inherits="WebPages_Admin_BankDecryption" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <script type="text/javascript">
        function GetDropDownData() {
            // Get the DropDownList.
            var ddlTestDropDownListXML = $('#ddlTestDropDownListXML');
             
            // Provide Some Table name to pass to the WebMethod as a paramter.
            var BankMerchant = "someTableName";
         
            $.ajax({
                type: "POST",
                url: "BankDecryption.aspx/GetDropDownItems",
                data: '{BankMerchant: "' + BankMerchant + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Now find the Table from response and loop through each item (row).
                    $(response.d).find(tableName).each(function () {
                        // Get the OptionValue and OptionText Column values.
                        var OptionValue = $(this).find('OptionValue').text();
                        var OptionText = $(this).find('OptionText').text();
                         
                        // Create an Option for DropDownList.
                        var option = $("<option>" + OptionText + "</option>");
                        option.attr("value", OptionValue);
         
                        ddlbankname.append(option);
                    });
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        </script>
    <table runat="server" style="width: 100%; text-align: left" id="TABLE2" cellpadding="0"
        border="0" cellspacing="0" align="center">
        <tr style="text-align: center">
            <td colspan="2" align="center">
                <b>String Decryption</b>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 35px" valign="top" colspan="2" align="center">
                <asp:RadioButtonList runat="server" ID="rblBankMerchant" AutoPostBack="true"
                    RepeatDirection="Horizontal" 
                    onselectedindexchanged="rblBankMerchant_SelectedIndexChanged">
                    <asp:ListItem Text="Bank" Value="B" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Merchant" Value="V"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr style="text-align: center">
            <td style="height: 35px" valign="top" colspan="2" align="center">
                <asp:Label style="font-weight:bold;" ID="lblSelectDDL" runat="server" Text="Select Bank:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:DropDownList ID="ddlbankname" runat="server" Width="180px" CssClass="borderRadius inputDesign">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="text-align: center">
            <td style="height: 35px" valign="top">
                <b>Encrypted String :-</b>
            </td>
            <td style="height: 35px" valign="top">
                <asp:TextBox ID="txtEncrypt" runat="server" TextMode="MultiLine" Width="800px"></asp:TextBox><asp:RequiredFieldValidator
                    ID="rfcGRN" runat="server" ErrorMessage="*" ControlToValidate="txtEncrypt" ValidationGroup="Show"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr style="text-align: center">
            <td style="height: 35px" valign="top" align="center" colspan="2">
                <asp:Button ID="btnShow" runat="server" Text="Decrypt" ValidationGroup="Show" OnClick="btnShow_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr style="text-align: center">
            <td style="height: 35px" valign="top">
                <b>Decrypted String :-</b>
            </td>
            <td>
                <asp:TextBox ID="txtDecrypt" runat="server" TextMode="MultiLine" Width="800px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
