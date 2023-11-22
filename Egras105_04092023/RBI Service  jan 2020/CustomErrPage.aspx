<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterPage3.master" AutoEventWireup="true" CodeFile="CustomErrPage.aspx.cs" Inherits="CustomErrPage" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; text-align: center;">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="font-size: large">
            <b>Login Again </b>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="Button1" runat="server" PostBackUrl="~/Default.aspx" 
                Text="Continue" />
        </td>
    </tr>
</table>
</asp:Content>

