<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404Error.aspx.cs" Inherits="WebPages_404Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 300px;">
           
            <img src="../Image/404-error.png" />
             <br />
             <br />
             <br />
             <br />
            <asp:Button ID="Button1" runat="server" PostBackUrl="~/Default.aspx" 
                Text="Login Again" OnClick="btngo_Click" Style="font-size: large;font-weight:600;background-color:dodgerblue;color:#AEE32B;  padding: 7px; border-radius: 2px; margin-bottom: 10px;width:500px;" />
        </div>
    </form>
</body>
</html>
