<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomTextBox.ascx.cs" Inherits="CustomTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:TextBox ID="txtCustom" runat="server" CssClass="SubValue" OnTextChanged="txtCustom_TextChanged" ></asp:TextBox>
<ajaxToolkit:FilteredTextBoxExtender ID="ftbrtxtCustom" TargetControlID="txtCustom" runat="server" ></ajaxToolkit:FilteredTextBoxExtender>
<asp:RequiredFieldValidator ID="reqvldtxtCustom" runat="server" Display="dynamic" CssClass="XMMessage" ControlToValidate="txtCustom"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="regvldtxtCustom" runat="server" Display="dynamic" CssClass="XMMessage" ControlToValidate="txtCustom" ></asp:RegularExpressionValidator>
<asp:RangeValidator ID="rngvldtxtCustom" runat="server" Display="dynamic" CssClass="XMMessage" ControlToValidate="txtCustom"></asp:RangeValidator>
<asp:CompareValidator ID="cmpvldtxtCustom" runat="server" Display="dynamic" CssClass="XMMessage" ControlToValidate="txtCustom" Enabled="false"></asp:CompareValidator>
&nbsp;&nbsp;

