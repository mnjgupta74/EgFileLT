<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgTestBankSite.aspx.cs" Inherits="WebPages_EgTestBankSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center " >
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancel" Text="Cancel"  BackColor="Red"  ForeColor="Aqua" Font-Bold="true"
            onclick="btnCancel_Click" Height="49px" Width="83px" />
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnsuccess" Text="success"  BackColor="Green"  ForeColor="Aqua" Font-Bold="true"
            onclick="btnsuccess_Click"  Height="49px" Width="83px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnpending" Text="Pending"  BackColor="Blue"  ForeColor="Aqua" Font-Bold="true"
            onclick="btnpending_Click"  Height="49px" Width="83px" />
    </div>
    </form>
</body>
</html>
