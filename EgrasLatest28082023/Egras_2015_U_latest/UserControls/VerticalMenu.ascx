<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VerticalMenu.ascx.cs" Inherits="UserControls_VerticalMenu" %>
<asp:Menu ID="Menu1"   DataSourceID="xmlDataSource" runat="server" 
          BackColor="#F7F6F3" DynamicHorizontalOffset="2" 
    MaximumDynamicDisplayLevels ="50" Font-Names="Verdana"  Font-Size="1.0em" 
          ForeColor="#333" StaticSubMenuIndent="10px" Font-Overline="False" 
    Font-Strikeout="False" Orientation="Horizontal" Width="1024px"   >
          <DataBindings>
            <asp:MenuItemBinding DataMember="MenuItem" 
             NavigateUrlField="NavigateUrl" TextField="Text" ToolTipField="ToolTip"/>
          </DataBindings>
          <StaticSelectedStyle BackColor="#5D7B9D" />
          <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
          <DynamicMenuStyle BackColor="#F7F6F3" CssClass="DynamicMenuZIndex" />
          <DynamicSelectedStyle BackColor="#5D7B9D" />
          <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
          <DynamicHoverStyle BackColor="#F7F6F3" Font-Bold="false" ForeColor="#F89C01" 
              Font-Size="10pt"/>
          <StaticHoverStyle BackColor="#F7F6F3" Font-Bold="false" ForeColor="#F89C01" />
       </asp:Menu>
   <asp:XmlDataSource ID="xmlDataSource" TransformFile="../UserControls/TransformXSLT.xsl"  
          XPath="MenuItems/MenuItem" runat="server"   EnableCaching="false"/>

<%--<asp:Menu ID="Menu1"   DataSourceID="xmlDataSource" runat="server" Orientation ="Horizontal" 
          BackColor="SlateBlue" DynamicHorizontalOffset="0" MaximumDynamicDisplayLevels ="50" 
          Font-Names="Verdana"  Font-Size="13px"
          ForeColor="White" StaticSubMenuIndent="10px"  >
          <DataBindings>
            <asp:MenuItemBinding DataMember="MenuItem" 
             NavigateUrlField="NavigateUrl" TextField="Text" ToolTipField="ToolTip"/>
          </DataBindings>
          <StaticSelectedStyle BackColor="SlateBlue" />
          <StaticMenuItemStyle HorizontalPadding="10px" VerticalPadding="4px" />
          <DynamicMenuStyle BackColor="SlateBlue" CssClass="DynamicMenuZIndex" />
          <DynamicSelectedStyle BackColor="SlateBlue" />
          <DynamicMenuItemStyle HorizontalPadding="3px" VerticalPadding="4px" />
          <DynamicHoverStyle BackColor="SlateBlue" Font-Bold="True" ForeColor="White"/>
          <StaticHoverStyle BackColor="SlateBlue" Font-Bold="True" ForeColor="White" />
       </asp:Menu>
   <asp:XmlDataSource ID="xmlDataSource" TransformFile="../UserControls/TransformXSLT.xsl"  
          XPath="MenuItems/MenuItem" runat="server"   EnableCaching="false"/>
--%>