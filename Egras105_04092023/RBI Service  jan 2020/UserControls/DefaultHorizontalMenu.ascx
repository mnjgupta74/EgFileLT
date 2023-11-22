<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DefaultHorizontalMenu.ascx.cs"
    Inherits="UserControls_DefaultHorizontalMenu" %>
<style type="text/css">
    .style1
    {
        width: 5px;
        
    }
    .style3
    {
        width: 161px;
    }
    .style4
    {
        width: 618px;
    }
    .style6
    {
        width: 120px;
    }
    .style7
    {
        width: 645px;
    }
</style>
<table id="HMenuTable" width="1024" border="0" cellpadding="0" cellspacing="0" runat="server">
    <tr>
        <td colspan="6">
     <%--  <img name="Grass" src= "../App_Themes/images/Grassheader.jpg"alt="Grass"
                align="left" width="1024" />--%>
            <img name="Grass" src= '<%= ResolveUrl("../App_Themes/images/HeaderNewColor.jpg")%>' alt="Grass"
                align="left" width="1024" />
        </td>
    </tr>

</table>
