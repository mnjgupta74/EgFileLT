<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgTreasuryWiseRev.aspx.cs"
    Inherits="WebPages_Charts_EgTreasuryWiseRev" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EgTreasuryWiseRev</title>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script src="../../js/amcharts.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Label ID="lblhead" runat="server" Text="No Record Found !!" Visible="false"></asp:Label>
        </center>
        <fieldset id="fldColumn" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em;
            border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
            padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
            behavior: url(../PIE.htc);">
            <legend style="color: Green;">Column Chart</legend>
            <div id="divDERevCol" style="width: 100%; height: 400px; background-color: white;">
                <asp:Literal ID="ltRevCol" runat="server"></asp:Literal>
            </div>
        </fieldset>
    </div>
    </form>
</body>
</html>
