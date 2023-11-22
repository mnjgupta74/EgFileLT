<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A.aspx.cs" Inherits="A" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="https://egras.rajasthan.gov.in/js/jquery-3.6.0.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="CSS/bootstrap.css" rel="stylesheet" />

    <title>Egras.rajasthan.gov.in</title>
    <style type="text/css">
        .style2 {
            height: 84px;
            width: 839px;
        }

        .style3 {
            width: 839px;
        }

        body {
            margin: 0;
        }

        .login {
            border: 2px solid #999999 !important;
        }

        table #grdCircular {
            border: 0 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div1" class="container">
            <div>
                <img name="Grass" src="App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left"
                    width="100%" style="height: fit-content" />
            </div>
            <nav class="navbar navbar-default " style="margin-top: 80px; border-radius: 0px; background-color: #FFF">
              <div class=" container-fluid">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                  <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                  </button>
                  <a class="navbar-brand fcolor" href="Default.aspx">eGRAS</a>
                </div>

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                  <ul class="nav navbar-nav col-md-11">
                      <li class="dropdown col-md-2">
                      <a href="#" class="dropdown-toggle fcolor" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">UserManual <span class="caret"></span></a>
                      <ul class="dropdown-menu">
                        <li><a href="UserManual/eGRAS.pdf">English Version</a></li>
                           <li role="separator" class="divider"></li>
                        <li><a href="UserManual/e-GRAS Manual Hindi.pdf">Hindi Version</a></li>
                      </ul>
                    </li>
                           <li class="dropdown col-md-2">
                      <a href="#" class="dropdown-toggle fcolor" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Best View <span class="caret"></span></a>
                      <ul class="dropdown-menu">
                        <li><a href="#">Site is Best Viewed in :</a></li>
                           <li role="separator" class="divider"></li>
                        <li><a href="#">Internet Explorer Versions &nbsp; IE8 , IE9</a></li>
                        <li><a href="#">Firefox Version &nbsp;19.0.2</a></li>
           
                        <li><a href="#">Chrome Version &nbsp;&nbsp; 25.0.1364.160 m</a></li>
                      </ul>
                    </li>
              
                       <li class="col-md-2"><a href="FAQ.htm" class="fcolor " >FAQ</a></li>
                       <li class="col-md-2"><a  href="EgCircularPdf.aspx" class="fcolor">Circular</a></li>
                       <li class="col-md-2"><a href="NodalDetails.aspx" class="fcolor">Nodal Officer</a></li>
                       <li class="col-md-2"><a href="ContactUs.aspx" class="fcolor">Contact Us</a></li>
                  </ul>
                </div><!-- /.navbar-collapse -->
              </div><!-- /.container-fluid -->
            </nav>
            <div class="col-md-12 ">
                <asp:Button class="btn btn-primary btn-lg" Style="padding-bottom:20px" OnClick="btnSend_Click" runat="server" ID="btnSend" Text="Post" />
            </div>
        </div>
    </form>
</body>
</html>
