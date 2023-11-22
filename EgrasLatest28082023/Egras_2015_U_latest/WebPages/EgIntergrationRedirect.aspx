<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgIntergrationRedirect.aspx.cs" Inherits="WebPages_EgIntergrationRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <script language="javascript" type="text/javascript">

    function Submit1_onclick() {

        document.forms[0].action = form1.action;
        document.forms[0].submit();
    }
 
    </script>

    <style type="text/css">
         #Submit1
        {
            background: url(/Image/waiting_process.gif) no-repeat;
            cursor: pointer;
            border: none;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <asp:HiddenField ID="encdata" runat="server" />
        <asp:HiddenField ID="merchant_code" runat="server" />
       
        <table align="center" style="vertical-align: middle">
            <tr>
                <td align="center">
                    <input type="image" src="../Image/waiting_process.gif" name="image" width="120" height="120"
                        onclick="return Submit1_onclick()" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <input type="text" size="80px" value="Wait while redirect to bank Site, don't press back or refresh button" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
