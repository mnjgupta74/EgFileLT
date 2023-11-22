<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgAppPaymentRedirect.aspx.cs" Inherits="WebPages_EgAppPaymentRedirect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        GRN:&nbsp<asp:Label Text="GRN" ID="lblGRN" runat="server" /><br />
        Total Amount:&nbsp<asp:Label Text="Amount" ID="lblTotalAmount" runat="server" /><br />
        <asp:Button Text="Proceed To PAY" ID="btnProceedToPay" runat="server" />
    </div>
    </form>
</body>
</html>
