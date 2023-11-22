<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage3.master" AutoEventWireup="true"
    CodeFile="EgCtd.aspx.cs" Inherits="WebPages_EgCtd" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <table class="style1" width="700px" border ="1" >
            <tr>
                <td colspan="4" style="font-weight: 700; text-align: center; height: 49px">
                    <centre><b>Demo of Calling Egras Web Service From CTD</b></centre>
                </td>
            </tr>
            <tr>
                <td>
                    <b>UserName :</b>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <b>Password :</b>
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td style="width: 168px">
                    <b>slab</b> :
                    <asp:TextBox ID="txtSlab" runat="server"></asp:TextBox>
                </td>
                <td>
                    <b>Requested Time</b> :<asp:TextBox ID="txtReqTime" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="width: 168px">
                    &nbsp;
                    <asp:Button ID="Click" runat="server" Text="Submit" OnClick="Click_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
        <asp:Label ID="lblshow" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReqTime"
            Format="dd/MM/yyyy">
        </ajaxToolkit:CalendarExtender>
    </div>
</asp:Content>
