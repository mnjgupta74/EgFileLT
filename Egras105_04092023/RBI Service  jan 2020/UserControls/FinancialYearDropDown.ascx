<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FinancialYearDropDown.ascx.cs" Inherits="UserControls_FinancialYearDropDown" %>
<asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                ErrorMessage="Select Year!" ControlToValidate="ddlYear" ValidationGroup="vldInsert"
                ForeColor="Red">*</asp:RequiredFieldValidator>