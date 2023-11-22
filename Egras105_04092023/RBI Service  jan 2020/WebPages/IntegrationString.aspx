<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntegrationString.aspx.cs"
    Inherits="WebPages_IntegrationString" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Menu ID="Menu1" runat="server" BackColor="#E3EAEB" DynamicHorizontalOffset="2"
            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#666666" Orientation="Horizontal"
            StaticSubMenuIndent="10px">
            <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicMenuStyle BackColor="#E3EAEB" />
            <DynamicSelectedStyle BackColor="#1C5E55" />
            <Items>
                <asp:MenuItem NavigateUrl="~/ChallanVerificationRpt.aspx" Text="Challan Report &gt;&gt;"
                    Value="Challan Report "></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/FrmGenerateCIN.aspx" Text="Generate CIN&gt;&gt;" Value="Generate CIN">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/BankManualChallan.aspx" Text="BankManual Challan " Value="BankManual Challan">
                </asp:MenuItem>
            </Items>
            <StaticHoverStyle BackColor="#666666" ForeColor="White" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticSelectedStyle BackColor="#1C5E55" />
        </asp:Menu>
    </div>
    <div>
        <table class="style1" align="center" align="center">
            <tr>
                <td class="style4">
                    BudgetHead1
                </td>
                <td>
                    <asp:Label ID="BudgetHead1" runat="server" Text="1475000120200000000"></asp:Label>
                    <asp:Label ID="BudgetHead1AMT" runat="server" Text="1.00"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    BudgetHead2
                </td>
                <td>
                    <asp:Label ID="BudgetHead2" runat="server" Text="0"></asp:Label>
                    <asp:Label ID="BudgetHead2AMT" runat="server" Text="0"></asp:Label>
                    <asp:HiddenField ID="hdnvalue" runat="server"  Value="1234"  />
                </td>
            </tr>
            <%--    <tr>
                <td class="style4">
                    BudgetHead2
                </td>
                <td>
                    <asp:Label ID="BudgetHead2" runat="server" Text="003900101020000107"></asp:Label>
                    <asp:Label ID="BudgetHead2AMT" runat="server" Text="200.00"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    BudgetHead3
                </td>
                <td>
                    <asp:Label ID="BudgetHead3" runat="server" Text="003900101900000108"></asp:Label>
                    <asp:Label ID="BudgetHead3AMT" runat="server" Text="300.00"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    BudgetHead4
                </td>
                <td>
                    <asp:Label ID="BudgetHead4" runat="server" Text="003900103010000109"></asp:Label>
                    <asp:Label ID="BudgetHead4AMT" runat="server" Text="400.00"></asp:Label>
                </td>
            </tr>
             <tr>
                <td class="style4">
                    BudgetHead5</td>
                <td>
                    <asp:Label ID="BudgetHead5" runat="server" Text="003900103900000110"></asp:Label>
                    <asp:Label ID="BudgetHead5AMT" runat="server" Text="500.00"></asp:Label>
                </td>
             
            </tr>
             <tr>
                <td class="style4">
                    BudgetHead6</td>
                <td>
                    <asp:Label ID="BudgetHead6" runat="server" Text="003900105010000111"></asp:Label>
                    <asp:Label ID="BudgetHead6AMT" runat="server" Text="600.00"></asp:Label>
                </td>
             
            </tr>
             <tr>
                <td class="style4">
                    BudgetHead7</td>
                <td>
                    <asp:Label ID="BudgetHead7" runat="server" Text="003900105900000112"></asp:Label>
                    <asp:Label ID="BudgetHead7AMT" runat="server" Text="700.00"></asp:Label>
                </td>
             
            </tr>
             <tr>
                <td class="style4">
                    BudgetHead8</td>
                <td>
                    <asp:Label ID="BudgetHead8" runat="server" Text="003900106010000113"></asp:Label>
                    <asp:Label ID="BudgetHead8AMT" runat="server" Text="800.00"></asp:Label>
                </td>
             
            </tr>
             <tr>
                <td class="style4">
                    BudgetHead9</td>
                <td>
                    <asp:Label ID="BudgetHead9" runat="server" Text="003900106900000114"></asp:Label>
                    <asp:Label ID="BudgetHead9AMT" runat="server" Text="900.00"></asp:Label>
                </td>
             
            </tr>--%>
            <tr>
                <td class="style4" align="center" colspan="4">
                    <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Cancel" />
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Pending" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
