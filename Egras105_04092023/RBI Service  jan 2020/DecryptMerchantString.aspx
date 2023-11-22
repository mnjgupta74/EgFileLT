<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage4.master" AutoEventWireup="true"
    CodeFile="DecryptMerchantString.aspx.cs" Inherits="DecryptMerchantString" Title="Untitled Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table runat="server" style="width: 100%; text-align: left" id="TABLE2" cellpadding="0"
        border="0" cellspacing="0" align="center">
        <tr style="text-align: center">
            <td colspan="2" align="center">
                <b>Merchant String Decryption</b><br /><br /><br />
            </td>
        </tr>
         <tr style="text-align: center">
            <td style="height: 35px" valign="top" colspan="2" align="center">
                <b>Merchant Code: &nbsp;&nbsp;&nbsp;&nbsp; </b>
                <asp:TextBox runat="server" ID="txtMerchantCode"></asp:TextBox>
                <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtMerchantCode"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="text-align: center">
            <td colspan="2" align="center">
               <asp:RadioButtonList ID="rblTransaction" runat="server" Font-Bold="true"
                            Font-Size="8pt" ForeColor="Green" AutoPostBack="true"
                            RepeatDirection="Horizontal" Style="margin-left: 0px">
                            <asp:ListItem Text="128" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="256" Value="1"></asp:ListItem>
                            <asp:ListItem Text="SBI ePay" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                <br />
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
                <asp:Button ID="btnShow_WithoutKey" runat="server" Text="DecryptWithoutKey" ValidationGroup="Show" OnClick="btnShow_WithoutKey_Click"/>
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
        <tr>
            <td>
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr style="text-align: center">
            <td style="height: 35px" valign="top">
                <b>Plain String :-</b>
            </td>
            <td style="height: 35px" valign="top">
                <asp:TextBox ID="txtPlainText" runat="server" TextMode="MultiLine" Width="800px"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtEncrypt"
                    ValidationGroup="Show"></asp:RequiredFieldValidator>
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
                <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" ValidationGroup="Show"
                    OnClick="btnEncrypt_Click" />
                <asp:Button ID="btnEncrypt_WithoutKey" runat="server" Text="Encrypt_WithoutKey" ValidationGroup="Show"
                    OnClick="btnEncrypt_WithoutKey_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr style="text-align: center">
            <td style="height: 35px" valign="top">
                <b>Encrypted String :-</b>
            </td>
            <td>
                <asp:TextBox ID="txtEncryptedText" runat="server" TextMode="MultiLine" Width="800px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
