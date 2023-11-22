<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankResponseAbort.aspx.cs" Inherits="WebPages_BankResponseAbort" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" width="60%">
            <tbody>
                <tr valign="middle" style="color: Blue; font-size: small;">
                    <td colspan="2">
                        <img name="Grass" src="../App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left"
                            width="1024px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td valign="top" style="margin-left: 40px; background-color: white;" align="left">
                        <asp:Label ID="lblWelcome" runat="server" Text="Welcome :" CssClass="HmenuText" Font-Size="Small"
                            ForeColor="Black" />
                        <asp:Label ID="lblUser" runat="server" CssClass="HmenuText" Font-Size="Small" ForeColor="Black" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblDate1" runat="server" Text="Date :" CssClass="HmenuText" Font-Bold="true"
                            Font-Size="Small" ForeColor="Black" />
                        <asp:Label ID="lblDate" runat="server" CssClass="HmenuText" Font-Bold="true" Font-Size="Small"
                            ForeColor="Black" />
                    </td>
                    <td valign="top" style="margin-right: 40px; background-color: white;" align="right">
                        <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" OnClick="lnkLogout_Click"
                            BackColor="white" ForeColor= "Black" Font-Bold="true" ></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" style="margin-left: 40px;">
                        <div id="main_menu" style="border-top: #cb3e00 2px solid; 
                            padding-left: 10px;">
                        </div>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2" valign="top" style="margin-left: 40px;">
                       <asp:LinkButton ID="lnkHome" runat="server" Text="Home" Visible="false" OnClick="lnkHome_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lnkGuest" runat="server" Text="GuestSchema" Visible="false" OnClick="lnkGuest_Click"></asp:LinkButton> 
                    </td>
                </tr>
            </tbody>
        </table>
        <table align="center" runat="server" style="width: 60%; text-align: left" id="TABLE1"
            cellpadding="0" border="1" cellspacing="0">
            <tr align="center">
                <td colspan="2" style="height: 16px; background-color: #B2D1F0" valign="top">
                    <div style="width: 100%; margin-left: 0px">
                        <asp:Label ID="lblheading" runat="server" Text="Challan Status" Font-Bold="True"
                            ForeColor="#009900"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    GRN
                </td>
                <td>
                    <asp:Label ID="grnlbl" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Amount
                </td>
                <td>
                    <asp:Label ID="netamountlbl" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Ref
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Cin
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Date
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Status
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Bankcode
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:Label ID="lblstatus" runat="server" Text="Label" ForeColor="Red" Font-Bold="true"
                        Visible="false"></asp:Label>
                </td>
              
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
