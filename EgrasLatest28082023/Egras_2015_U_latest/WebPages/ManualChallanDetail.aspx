<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="ManualChallanDetail.aspx.cs" Inherits="WebPages_ManualChallanDetail"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table class="style1" align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" style="font-weight: 700; text-align: center; height: 49px">
                    <center>
                        <b>Manual Challan Detail</b></center>
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
                <td style="width: 400px">
                    <b>Time Slab:</b> :
                    <asp:DropDownList ID="ddlTime" runat="server" CssClass="borderRadius inputDesign"
                        Width="200px">
                        <asp:ListItem Value="0" Text="--Select From Time--"></asp:ListItem>
                        <asp:ListItem Value="00:00:00" Text="00:00 AM"></asp:ListItem>
                        <asp:ListItem Value="00:30:00" Text="00:30 AM"></asp:ListItem>
                        <asp:ListItem Value="01:00:00" Text="01:00 AM"></asp:ListItem>
                        <asp:ListItem Value="01:30:00" Text="01:30 AM"></asp:ListItem>
                        <asp:ListItem Value="02:00:00" Text="02:00 AM"></asp:ListItem>
                        <asp:ListItem Value="02:30:00" Text="02:30 AM"></asp:ListItem>
                        <asp:ListItem Value="03:00:00" Text="03:00 AM"></asp:ListItem>
                        <asp:ListItem Value="03:30:00" Text="03:30 AM"></asp:ListItem>
                        <asp:ListItem Value="04:00:00" Text="04:00 AM"></asp:ListItem>
                        <asp:ListItem Value="04:30:00" Text="04:30 AM"></asp:ListItem>
                        <asp:ListItem Value="05:00:00" Text="05:00 AM"></asp:ListItem>
                        <asp:ListItem Value="05:30:00" Text="05:30 AM"></asp:ListItem>
                        <asp:ListItem Value="06:00:00" Text="06:00 AM"></asp:ListItem>
                        <asp:ListItem Value="06:30:00" Text="06:30 AM"></asp:ListItem>
                        <asp:ListItem Value="07:00:00" Text="07:00 AM"></asp:ListItem>
                        <asp:ListItem Value="07:30:00" Text="07:30 AM"></asp:ListItem>
                        <asp:ListItem Value="08:00:00" Text="08:00 AM"></asp:ListItem>
                        <asp:ListItem Value="08:30:00" Text="08:30 AM"></asp:ListItem>
                        <asp:ListItem Value="09:00:00" Text="09:00 AM"></asp:ListItem>
                        <asp:ListItem Value="09:30:00" Text="09:30 AM"></asp:ListItem>
                        <asp:ListItem Value="10:00:00" Text="10:00 AM"></asp:ListItem>
                        <asp:ListItem Value="10:30:00" Text="10:30 AM"></asp:ListItem>
                        <asp:ListItem Value="11:00:00" Text="11:00 AM"></asp:ListItem>
                        <asp:ListItem Value="11:30:00" Text="11:30 AM"></asp:ListItem>
                        <asp:ListItem Value="12:00:00" Text="12:00 PM"></asp:ListItem>
                        <asp:ListItem Value="12:30:00" Text="12:30 PM"></asp:ListItem>
                        <asp:ListItem Value="13:00:00" Text="13:00 PM"></asp:ListItem>
                        <asp:ListItem Value="13:30:00" Text="13:30 PM"></asp:ListItem>
                        <asp:ListItem Value="14:00:00" Text="14:00 PM"></asp:ListItem>
                        <asp:ListItem Value="14:30:00" Text="14:30 PM"></asp:ListItem>
                        <asp:ListItem Value="15:00:00" Text="15:00 PM"></asp:ListItem>
                        <asp:ListItem Value="15:30:00" Text="15:30 PM"></asp:ListItem>
                        <asp:ListItem Value="16:00:00" Text="16:00 PM"></asp:ListItem>
                        <asp:ListItem Value="16:30:00" Text="16:30 PM"></asp:ListItem>
                        <asp:ListItem Value="17:00:00" Text="17:00 PM"></asp:ListItem>
                        <asp:ListItem Value="17:30:00" Text="17:30 PM"></asp:ListItem>
                        <asp:ListItem Value="18:00:00" Text="18:00 PM"></asp:ListItem>
                        <asp:ListItem Value="18:30:00" Text="18:30 PM"></asp:ListItem>
                        <asp:ListItem Value="19:00:00" Text="19:00 PM"></asp:ListItem>
                        <asp:ListItem Value="19:30:00" Text="19:30 PM"></asp:ListItem>
                        <asp:ListItem Value="20:00:00" Text="20:00 PM"></asp:ListItem>
                        <asp:ListItem Value="20:30:00" Text="20:30 PM"></asp:ListItem>
                        <asp:ListItem Value="21:00:00" Text="21:00 PM"></asp:ListItem>
                        <asp:ListItem Value="21:30:00" Text="21:30 PM"></asp:ListItem>
                        <asp:ListItem Value="22:00:00" Text="22:00 PM"></asp:ListItem>
                        <asp:ListItem Value="22:30:00" Text="22:30 PM"></asp:ListItem>
                        <asp:ListItem Value="23:00:00" Text="23:00 PM"></asp:ListItem>
                        <asp:ListItem Value="23:30:00" Text="23:30 PM"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                        ErrorMessage="Select Time!" InitialValue="0" ControlToValidate="ddlTime" ValidationGroup="vldInsert"
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 400px">
                    <asp:DropDownList ID="ddlTimeSlab" runat="server"  CssClass="borderRadius inputDesign"
                        Width="200px">
                        <asp:ListItem Value="0" Text="--Select To Time--"></asp:ListItem>
                        <asp:ListItem Value="00:30:00" Text="00:30 AM"></asp:ListItem>
                        <asp:ListItem Value="01:00:00" Text="01:00 AM"></asp:ListItem>
                        <asp:ListItem Value="01:30:00" Text="01:30 AM"></asp:ListItem>
                        <asp:ListItem Value="02:00:00" Text="02:00 AM"></asp:ListItem>
                        <asp:ListItem Value="02:30:00" Text="02:30 AM"></asp:ListItem>
                        <asp:ListItem Value="03:00:00" Text="03:00 AM"></asp:ListItem>
                        <asp:ListItem Value="03:30:00" Text="03:30 AM"></asp:ListItem>
                        <asp:ListItem Value="04:00:00" Text="04:00 AM"></asp:ListItem>
                        <asp:ListItem Value="04:30:00" Text="04:30 AM"></asp:ListItem>
                        <asp:ListItem Value="05:00:00" Text="05:00 AM"></asp:ListItem>
                        <asp:ListItem Value="05:30:00" Text="05:30 AM"></asp:ListItem>
                        <asp:ListItem Value="06:00:00" Text="06:00 AM"></asp:ListItem>
                        <asp:ListItem Value="06:30:00" Text="06:30 AM"></asp:ListItem>
                        <asp:ListItem Value="07:00:00" Text="07:00 AM"></asp:ListItem>
                        <asp:ListItem Value="07:30:00" Text="07:30 AM"></asp:ListItem>
                        <asp:ListItem Value="08:00:00" Text="08:00 AM"></asp:ListItem>
                        <asp:ListItem Value="08:30:00" Text="08:30 AM"></asp:ListItem>
                        <asp:ListItem Value="09:00:00" Text="09:00 AM"></asp:ListItem>
                        <asp:ListItem Value="09:30:00" Text="09:30 AM"></asp:ListItem>
                        <asp:ListItem Value="10:00:00" Text="10:00 AM"></asp:ListItem>
                        <asp:ListItem Value="10:30:00" Text="10:30 AM"></asp:ListItem>
                        <asp:ListItem Value="11:00:00" Text="11:00 AM"></asp:ListItem>
                        <asp:ListItem Value="11:30:00" Text="11:30 AM"></asp:ListItem>
                        <asp:ListItem Value="12:00:00" Text="12:00 PM"></asp:ListItem>
                        <asp:ListItem Value="12:30:00" Text="12:30 PM"></asp:ListItem>
                        <asp:ListItem Value="13:00:00" Text="13:00 PM"></asp:ListItem>
                        <asp:ListItem Value="13:30:00" Text="13:30 PM"></asp:ListItem>
                        <asp:ListItem Value="14:00:00" Text="14:00 PM"></asp:ListItem>
                        <asp:ListItem Value="14:30:00" Text="14:30 PM"></asp:ListItem>
                        <asp:ListItem Value="15:00:00" Text="15:00 PM"></asp:ListItem>
                        <asp:ListItem Value="15:30:00" Text="15:30 PM"></asp:ListItem>
                        <asp:ListItem Value="16:00:00" Text="16:00 PM"></asp:ListItem>
                        <asp:ListItem Value="16:30:00" Text="16:30 PM"></asp:ListItem>
                        <asp:ListItem Value="17:00:00" Text="17:00 PM"></asp:ListItem>
                        <asp:ListItem Value="17:30:00" Text="17:30 PM"></asp:ListItem>
                        <asp:ListItem Value="18:00:00" Text="18:00 PM"></asp:ListItem>
                        <asp:ListItem Value="18:30:00" Text="18:30 PM"></asp:ListItem>
                        <asp:ListItem Value="19:00:00" Text="19:00 PM"></asp:ListItem>
                        <asp:ListItem Value="19:30:00" Text="19:30 PM"></asp:ListItem>
                        <asp:ListItem Value="20:00:00" Text="20:00 PM"></asp:ListItem>
                        <asp:ListItem Value="20:30:00" Text="20:30 PM"></asp:ListItem>
                        <asp:ListItem Value="21:00:00" Text="21:00 PM"></asp:ListItem>
                        <asp:ListItem Value="21:30:00" Text="21:30 PM"></asp:ListItem>
                        <asp:ListItem Value="22:00:00" Text="22:00 PM"></asp:ListItem>
                        <asp:ListItem Value="22:30:00" Text="22:30 PM"></asp:ListItem>
                        <asp:ListItem Value="23:00:00" Text="23:00 PM"></asp:ListItem>
                        <asp:ListItem Value="23:30:00" Text="23:30 PM"></asp:ListItem>
                        <asp:ListItem Value="23:59:59" Text="24:00 PM"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                        ErrorMessage="Select Time!" InitialValue="0" ControlToValidate="ddlTimeSlab"
                        ValidationGroup="vldInsert" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 168px" colspan="3" align="center">
                    &nbsp;
                    <asp:Button ID="Click" runat="server" Text="Submit" ValidationGroup="vldInsert" OnClick="Click_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
