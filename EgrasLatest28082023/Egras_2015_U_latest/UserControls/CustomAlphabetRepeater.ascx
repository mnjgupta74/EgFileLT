<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomAlphabetRepeater.ascx.cs" Inherits="UserControls_CustomAlphabetRepeater" %>
<asp:Repeater ID="repAlphabet" runat="server" OnItemCommand="repAlphabet_ItemCommand">
<ItemTemplate>
<asp:LinkButton ID="lbtnSelectAlphabet" runat="server" Text='<%#Eval("Char") %>' CssClass="LinkButtonTab" >
</asp:LinkButton>
</ItemTemplate>
<SeparatorTemplate>
|
</SeparatorTemplate>
</asp:Repeater>