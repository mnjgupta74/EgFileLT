<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAdvanceScrollRpt.aspx.cs" Inherits="WebPages_Reports_EgAdvanceScrollRpt"
    Title="Untitled Page" %>

<%@ Register Src="~/UserControls/wucScrollControl.ascx" TagName="wucScrollControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:wucScrollControl ID="wucScrollControl1" runat="server" />
</asp:Content>
