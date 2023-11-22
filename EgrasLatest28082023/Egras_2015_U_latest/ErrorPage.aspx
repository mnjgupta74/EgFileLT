<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Egras.rajasthan.gov.in</title>
</head>
<body>
    <form id="form2" runat="server">
        <div style="width: 60%; margin-left: 20%;">
            <img name="Grass" src="App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left" width="100%" <="" div="" style="margin-bottom: 5%;" />
        </div>
        <div align="center" style="width: 100%; text-align: center; clear: both; margin-top: 4%;">
            <img src="Image/SadIcon.png" alt="Some Error Occurred" width="auto" />
        </div>

        <div style="margin-left: 25%; width: 50%; text-align: center; font-size: 25px; /*box-shadow: gray 0px 0px 3px 3px; */padding: 5px; margin-top: 1%;">
            <div style="color: red; font-size: 30px; font-family: Verdana;"><b>Oops!</b></div>
            <br />
            We're Sorry, an error has occured while proccessing your request! 
    <br />
            <br />
            <asp:Button ID="Button1" runat="server" PostBackUrl="~/Default.aspx"
                Text="Login Again" OnClick="btngo_Click" Style="font-size: small; font-weight: 600; padding: 7px; border-radius: 2px; margin-bottom: 10px;" />
        </div>
    </form>
</body>
</html>

