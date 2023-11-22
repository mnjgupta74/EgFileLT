<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginAgain.aspx.cs" Inherits="WebPages_404Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-3.6.0.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="js/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container" style="text-align: center">
    <div class="row col-xs-12" style="text-align: center; color: Blue; width: 100%; min-height: 10%;">
                <img name="Grass" src="../App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left" width="100%" />
            </div></div>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 300px;">
           
           <%-- <img src="../Image/404-error.png" />--%>
             <br />
             <br />
             <br />
             <br />
            <asp:Label ID="Label1"  Font-Size="Larger"  runat="server"   style="font-weight:300;    font-size: 30px;" Text="  Session Expired    <a href='Default.aspx'>Click here To Login Again</a>"></asp:Label>
            <%--<asp:Button ID="Button1" runat="server" PostBackUrl="~/Default.aspx" 
                Text="Session Expired Please Login Again "  OnClick="btngo_Click" Style="font-size: large;font-weight:600;background-color:dodgerblue;color:#AEE32B;  padding: 7px; border-radius: 2px; margin-bottom: 10px;width:500px;" />--%>
        </div>
    </form>
</body>
</html>
