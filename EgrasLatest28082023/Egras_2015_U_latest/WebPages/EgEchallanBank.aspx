<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage4.master" AutoEventWireup="true"
    CodeFile="EgEchallanBank.aspx.cs" Inherits="WebPages_EgEchallanBank" %>

<%@ Register Src="~/UserControls/VerticalMenu.ascx" TagName="VMenu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControls/HorizontalMenu.ascx" TagName="HMenu" TagPrefix="ucl1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" id="MasterTable" cellspacing="0" cellpadding="0" width="1024">
        <tr valign="middle" style="color: Blue; font-size: small;">
            <td>
               
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <ucl:VMenu ID="VMenu" runat="server" />
            </td>
        </tr>
    </table>
    <table align="center" runat="server" style="width: 80%; text-align: left" id="TABLE1"
        cellpadding="0" border="1" cellspacing="0">
        <tr align="center">
            <td colspan="2" style="height: 16px; background-color: #B2D1F0" valign="top">
                <div style="width: 100%; margin-left: 0px">
                    <asp:Label ID="lblheading" runat="server" Text="Challan Status" Font-Bold="True"
                        ForeColor="#009900"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                GRNNUMBER
            </td>
            <td>
                <asp:Label ID="grnlbl" runat="server" Text="Label"></asp:Label>
            </td>
           
        </tr>
     <%--   <tr>
            <td>
                Head_Name1
            </td>
            <td>
                <asp:Label ID="lblhead1" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount1
            </td>
            <td>
                <asp:Label ID="lblheadamount1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Head_Name2
            </td>
            <td>
                <asp:Label ID="lblhead2" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount2
            </td>
            <td>
                <asp:Label ID="lblheadamount2" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Head_Name3
            </td>
            <td>
                <asp:Label ID="lblhead3" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount3
            </td>
            <td>
                <asp:Label ID="lblheadamount3" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Head_Name4
            </td>
            <td>
                <asp:Label ID="lblhead4" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount4
            </td>
            <td>
                <asp:Label ID="lblheadamount4" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Head_Name5
            </td>
            <td>
                <asp:Label ID="lblhead5" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount5
            </td>
            <td>
                <asp:Label ID="lblheadamount5" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Head_Name6
            </td>
            <td>
                <asp:Label ID="lblhead6" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount6
            </td>
            <td>
                <asp:Label ID="lblheadamount6" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Head_Name7
            </td>
            <td>
                <asp:Label ID="lblhead7" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount7
            </td>
            <td>
                <asp:Label ID="lblheadamount7" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Head_Name8
            </td>
            <td>
                <asp:Label ID="lblhead8" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount8
            </td>
            <td>
                <asp:Label ID="lblheadamount8" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
         <tr>
            <td>
                Head_Name9
            </td>
            <td>
                <asp:Label ID="lblhead9" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                Head_Amount9
            </td>
            <td>
                <asp:Label ID="lblheadamount9" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                FullName
            </td>
            <td>
                <asp:Label ID="fullnamelbl" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>--%>
        <tr>
            <td>
                Amount
            </td>
            <td>
                <asp:Label ID="netamountlbl" runat="server" Text="Label"></asp:Label>
            </td>
            
        </tr>
        <tr>
            <td>
                &nbsp;
                <asp:Label ID="lblstatus" runat="server" Text="Label" ForeColor="Red" Font-Bold="true"
                    Visible="false"></asp:Label>
            </td>
            <td>
                <asp:LinkButton ID="lnkprint" runat="server" Text="Go For Print" OnClick="lnkprint_Click"></asp:LinkButton>
            </td>
           
        </tr>
    </table>
</asp:Content>
