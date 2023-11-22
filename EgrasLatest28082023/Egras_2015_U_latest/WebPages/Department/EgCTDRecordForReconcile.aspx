<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgCTDRecordForReconcile.aspx.cs" Inherits="WebPages_Department_EgCTDRecordForReconcile"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" style="font-weight: 700; text-align: center; height: 49px">
                    <center>
                        <b>CTD Detail</b></center>
                </td>
            </tr>
            <tr>
                <td style="width: 400px" align="center">
                    <b>Date</b> :<asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txtDate"
                        ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 168px" align="left">
                    &nbsp;
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vldInsert"
                        OnClick="btnSubmit_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 168px" align="center" colspan="2">
                    &nbsp;
                    <asp:Label ID="lblRecords" runat="server" Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblAmount" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
