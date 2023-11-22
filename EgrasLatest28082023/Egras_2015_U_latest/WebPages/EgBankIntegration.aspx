<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgBankIntegration.aspx.cs" Inherits="WebPages_EgBankIntegration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <center>
            <fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 50px; margin-right: 50px;
                margin-top: 25px; height: 300px;">
                <legend style="color: #336699; font-weight: bold">Bank-List for pay Transaction</legend>
                <table style="margin-top: 25px">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblBanks" runat="server" Font-Size="Small" 
                                CellSpacing="10" CellPadding="5" RepeatDirection="Horizontal" RepeatColumns="3"
                                ForeColor="#336699" Font-Bold="true">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 45px; vertical-align: bottom;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Pay Now" OnClick="btnSubmit_Click"
                                CssClass="button_example" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
</asp:Content>
