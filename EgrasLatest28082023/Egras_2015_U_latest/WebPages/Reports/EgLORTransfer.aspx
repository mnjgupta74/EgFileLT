<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgLORTransfer.aspx.cs" Inherits="WebPages_Reports_EgLORTransfer" %>

<%@ Register Src="~/UserControls/CalenderYear.ascx" TagPrefix="uc1" TagName="CalenderYear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

        .form-control {
            width: 50%;
        }
    </style>

    <script type="text/javascript">
    </script>
    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Office-Master" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">LOR Data Shifting Process</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="" />
    </div>
    <table width="100%" style="text-align: center" border="1" cellpadding="0" cellspacing="0">

        <tr style="height: 40px;">
            <td style="padding-right: 2%;">
                <b><span style="color: #336699; font-family: Arial CE; font-size: 15px;">Month</span></b>
           <%-- </td>
            <td>--%>
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control pull-right" Width="50%">
                    <asp:ListItem Text="January" Value="01"></asp:ListItem>
                    <asp:ListItem Text="February" Value="02"></asp:ListItem>
                    <asp:ListItem Text="March" Value="03"></asp:ListItem>
                    <asp:ListItem Text="April" Value="04"></asp:ListItem>
                    <asp:ListItem Text="May" Value="05"></asp:ListItem>
                    <asp:ListItem Text="June" Value="06"></asp:ListItem>
                    <asp:ListItem Text="July" Value="07"></asp:ListItem>
                    <asp:ListItem Text="August" Value="08"></asp:ListItem>
                    <asp:ListItem Text="September" Value="09"></asp:ListItem>
                    <asp:ListItem Text="Octomber" Value="10"></asp:ListItem>
                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="ddlMonth"
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>

            <td style="padding-right: 2%;">
                <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">Year</span></b>
            <%--</td>
            <td>--%>
                <uc1:CalenderYear runat="server" ID="CalenderYear" class="pull-right" />
                <%--   <asp:RequiredFieldValidator ID="rfcCalenderYear" runat="server" ControlToValidate="CalenderYear"
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="35px" OnClick="btnSubmit_Click" CssClass="btn btn-primary"
                    ValidationGroup="a" TabIndex="3" />
            </td>
        </tr>
    </table>
</asp:Content>
