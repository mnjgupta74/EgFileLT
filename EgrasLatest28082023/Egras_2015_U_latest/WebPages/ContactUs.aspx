<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs"
    Title="Contact Us" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Contact Us</title>

    <script type="text/javascript" src="../js/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <link href="../js/bootstrap.min.css" rel="stylesheet" />

    <style type="text/css">
        body {
            background-color: #f9f9f9;
        }

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

        #DivToOpen {
            background-color: White;
        }
    </style>

    <script type="text/javascript">
        $(function getContacts() {
            $.ajax({
                type: "POST",
                url: "ContactUs.aspx/GetContactDetail",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    for (var i = 0; i < data.d.length; i++) {
                        $("#dlCustomers").append("<tr><td>" + data.d[i].Name + "</td><td>" + data.d[i].ContactNo + "</td></tr>");
                        //$("#dlCustomers tr:first").css("background-color", "red");
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(data));
                    alert("Error");

                }
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        //Function to disable Cntrl key/right click
        function DisableControlKey(e) {
            var message = "Cntrl key/ Right Click Option disabled";
            if (e.which == 1 || e.button == 2 || e.ctrlKey == 17) {
                alert(message);
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="div1" class="container">
            <div>
                <img name="Grass" src="../App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left"
                    width="100%" style="height: fit-content" />
            </div>
            <nav class="navbar navbar-default " style="margin-top: 80px; border-radius: 0px; background-color: #FFF">
                <div class=" container-fluid"=container-fluid">
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

                            <li class="col-md-2"><a href="FAQ.htm" class="fcolor ">FAQ</a></li>
                            <li class="col-md-2"><a href="EgCircularPdf.aspx" class="fcolor">Circular</a></li>
                            <li class="col-md-2"><a href="NodalDetails.aspx" class="fcolor">Nodal Officer</a></li>
                            <li class="col-md-2"><a href="ContactUs.aspx" class="fcolor">Contact Us</a></li>
                        </ul>
                    </div><!-- /.navbar-collapse -->
                </div><!-- /.container-fluid -->
            </nav>
            <%-- <div>
                <div valign="top" align="center" class="auto-style2"></div>
            </div>
            <div>
                <div align="left" class="auto-style1" colspan="2">
                    <a href="Default.aspx">Home</a>
                </div>
            </div>--%>
        </div>

        <div style="width: 54%; margin: auto;">
            <%--<fieldset>
               <legend>--%>
            <div class='shadow' id='div3' style="border: 2px solid #999999; background-color: white; color: Black; padding-bottom: 5px; min-height: 375px; overflow: auto; top: 0px; z-index: 40; text-align: left;">
                <%--<div id="divHeader">--%>
                <h3 align="center" style="color: #009900; font-family: Verdana, Arial, Helvetica, sans-serif;"><u>Contact Us</u>
                </h3>
                <%-- </div>
          </legend>--%>
                <div id="DivContent" style="padding-left: 20px;">
                    <asp:DataList ID="dlCustomers" runat="server" onKeyPress="return DisableControlKey(event)" onKeyDown="if(event.ctrlKey && event.keyCode==86){return false;}" onMouseDown="return DisableControlKey(event)" RepeatLayout="Table" RepeatColumns="2"
                        Width="100%" align="center" CellPadding="2" CellSpacing="0" Border="0" Font-Size="Large">
                    </asp:DataList>
                </div>
                <%--</fieldset>--%>
            </div>
        </div>
    </form>
</body>
</html>

