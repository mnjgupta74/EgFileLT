<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HorizontalMenu.ascx.cs"
    Inherits="UserControls_HorizontalMenu" %>
<table id="HMenuTable" width="1024px" border="0" cellpadding="0" cellspacing="0"
    runat="server">
    <tr valign="middle" style="color:Blue; font-size: small;">
        <td>
            <%--  <img name="Grass" src= "../App_Themes/images/Grassheader.jpg"alt="Grass"
                align="left" width="1024" />--%>
            <img name="Grass" src='<%= ResolveUrl("../App_Themes/images/HeaderNewColor.jpg")%>'
                alt="Grass" align="left" width="1024px" />
        </td>
       <%-- <td align="left" valign="middle" class="style1">
            <asp:HyperLink ID="hbtnhome" Text="Home" CssClass="HmenuText" NavigateUrl="~/WebPages/Home.aspx"
                runat="server" ToolTip="Home">
            </asp:HyperLink>
        </td>

        <td align="middle">
            <asp:HyperLink ID="hbtnLogout" CssClass="HmenuText" Text="Logout" NavigateUrl="~/WebPages/Logout.aspx"
                runat="server" ToolTip="Logout" />
        </td>--%>
    </tr>
</table>
