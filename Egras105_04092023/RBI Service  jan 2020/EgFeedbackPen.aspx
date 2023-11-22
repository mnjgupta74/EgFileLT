<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgFeedbackPen.aspx.cs" Inherits="EgFeedbackPen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns:fb="http://www.facebook.com/2008/fbml" xmlns:og="http://ogp.me/ns#" xmlns="http://www.w3.org/1999/xhtml" xml:lang="EN">
<head runat="server">
    <title>Egras.Raj.Nic.in</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
<meta property="og:type" content="website"/>
<meta property="fb:admins" content="100000624233602"/>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/HeaderNewColor.jpg" 
        Width="1356px" Height="478px" />
    
    <div id="fb-root"></div>
<script>(function(d, s, id) {
  var js, fjs = d.getElementsByTagName(s)[0];
  if (d.getElementById(id)) return;
  js = d.createElement(s); js.id = id;
  js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
  fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
<div class="fb-comments" data-href="https://egras.raj.nic.in/EgFeedbackPen.aspx" data-numposts="10" data-colorscheme="light"></div>
    
    </form>
</body>
</html>
