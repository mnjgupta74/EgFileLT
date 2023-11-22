<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Online_ManualRadioButton.ascx.cs" Inherits="UserControls_Online_ManualRadioButton" %>
<div style="width:150px; float:left;">
    <asp:RadioButtonList runat="server" ID="rdBtnList" Width="150px" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdBtnList_SelectedIndexChanged" >
        <asp:ListItem Text="Online" Value="N" Selected="True" style="margin-right:15px"/>
        <asp:ListItem Text="Manual" Value="M"/>
    </asp:RadioButtonList>
</div>
