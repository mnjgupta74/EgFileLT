<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetGRNStatus.aspx.cs" Inherits="GetGRNStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="https://egras.rajasthan.gov.in/js/jquery-3.6.0.min.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width" />

    <script src="md5.js" type="text/javascript" language="javascript"></script>
    <script src="js/SHA256.js" type="text/javascript"></script>
    <link rel="shortcut icon" type="image/x-icon" href="App_Themes/images/HeaderNewColor.jpg" />
    <link href="CSS/css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="JS/bootstrap.min.js"></script>
    <link href="JS/bootstrap.min.css" rel="stylesheet" />

    <style type="text/css">
        .shadow /* used in content div's */ {
            -webkit-box-shadow: black 0px 2px 12px;
            -moz-box-shadow: black 0px 2px 12px;
            box-shadow: black 0px 2px 12px;
            behavior: url(../PIE.htc);
            border-collapse: separate;
            -moz-border-radius: 16px 16px 16px 16px;
            -webkit-border-radius: 16px 16px 16px 16px;
            border-radius: 16px 16px 16px 16px;
            padding: 20px 20px 0 20px;
            background-color: White;
        }

        body {
            background-color: #f9f9f9;
            line-height: 1.70;
            font-family: Verdana,Calibri;
            font-size: 13px;
        }

        .login {
            background-color: transparent;
        }

        .fcolor {
            color: #339933 !important;
        }

        .floatleft {
            float: left;
        }

        .floatright {
            float: right;
        }

        .form-control, .input-group-addon, .btn {
            border-radius: 0px;
        }

        ul {
            list-style-type: none;
        }

        .modal-dialog {
            width: 750px;
        }
    </style>

    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript" language="javascript">

        function Clear() {
            document.getElementById('txtUserName').value = "";
            document.getElementById('txtPassword').value = "";
            document.getElementById('inpHide').value = "";
        }

    </script>
    <script type="text/javascript">
        idleTime = 0;
        $(document).ready(function () {

            //Increment the idle time counter every minute.
            var idleInterval = setInterval("timerIncrement()", 20000); // 1 minute


            //Zero the idle timer on mouse movement.
            $(this).mousemove(function (e) {
                idleTime = 0;
            });
            $(this).keypress(function (e) {
                idleTime = 0;
            });
        })
        function timerIncrement() {
            idleTime = idleTime + 1;
            if (idleTime > 3) { // 20 minutes
                window.location.reload();
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        window.onload = function () { PopUpDetecter(), cmdButton1_Clicked(); }
        function cmdButton1_Clicked() {
            document.getElementById("txtGRN").focus();
            return false;
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#Popup').click(function () {
                var newwindow = window.open($(this).prop('href'), '', 'height=800,width=800');
                if (window.focus) {
                    newwindow.focus();
                }
                return false;
            });
        });
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            RefreshCaptcha();
            var x = (screen.width * 66) / 100;
            var x1 = (screen.width * 22) / 100;
            var x2 = (screen.width * 45) / 100;
            var x3 = (screen.width * 15) / 100;
            $('#lnkUserManual').hover(function (e) {
                $("#divUserManual").slideDown();
                $('#divUserManual').offset({ left: x3 });
                $('#divUserManual').width(300).height(110);
            }, function () {
                $('#divUserManual').fadeOut(10000);
            });
            $('#lblBestView').hover(function (e) {
                $("#DivOpenBestView").slideDown();
                $('#DivOpenBestView').offset({ left: x1 });
                $('#DivOpenBestView').width(300).height(110);
            }, function () {
                $('#DivOpenBestView').slideUp();
            });
            $('#lblcircular').hover(function () {
                $("#DivCircular").slideDown(
                function () {
                    $('#DivCircular').mouseenter(function () { $(this).stop(); }).mouseleave(function () { $("#DivCircular").slideUp(); });
                });
                $('#DivCircular').offset({ left: x2 });
                $('#DivCircular').width(300).height(110);
            }, function () { $("#DivCircular").slideUp(); });
        });

    </script>

    <script language="javascript" type="text/javascript">
        function generaterandomnumber() {
            var subject = Math.floor(Math.random() * 900000) + 100000;
            return subject;
        }
        function RefreshCaptcha() {

            var img = document.getElementById("imgCaptcha");
            img.src = "Image/captcha.ashx?arg=" + generaterandomnumber();
        }
    </script>

    <noscript>
        <div style="font-size: 25px; top: 0px; left: 0px; color: White; width: 100%; height: 100%; position: absolute; background-color: #000000; vertical-align: middle; padding-top: 300px; text-align: center; z-index: 1005;">
            Javascript is disabled in Your Browser.
            <br />
            Please Enable browser javascript first for Best Use of this Website.
            <br />
            To enable javascript plz refer this Link &nbsp; <a href="../EnableJavascript.htm"
                style="color: Red">Enable Javascript !</a>
        </div>
    </noscript>
</head>
<body>
    <form id="form1" runat="server" novalidate>
        <asp:ScriptManager ID="srcManager" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hdnPassword" />
        <div id="div1" class="container" onload="mainCheck()">
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
                        <li><a target="_blank" href="UserManual/eGRAS.pdf">English Version</a></li>
                           <li role="separator" class="divider"></li>
                        <li><a target="_blank" href="UserManual/e-GRAS Manual Hindi.pdf">Hindi Version</a></li>
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

                      
              
                       <li class="col-md-1"><a href="FAQ.htm" class="fcolor " >FAQ</a></li>
                       <li class="col-md-2"><a  href="EgCircularPdf.aspx" class="fcolor">Circular</a></li>
                        <li class="dropdown col-md-2">
                      <a href="#" class="dropdown-toggle fcolor" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Officer <span class="caret"></span></a>
                      <ul class="dropdown-menu">
                        <li><a href="NodalDetails.aspx">Nodal Officer</a></li>
                           <li role="separator" class="divider"></li>
                        <li><a href="EgBankOfficerDetails.aspx">Bank Officer</a></li>
                      </ul>
                    </li>

                       <li class="col-md-1"><a href="ContactUs.aspx" class="fcolor" style="margin-left: -34px;">ContactUs</a></li>
                       <li class="col-md-2"><a class="fcolor" href="GetGRNStatus.aspx">GRN Status</a></li>

                  </ul>
                </div><!-- /.navbar-collapse -->
              </div><!-- /.container-fluid -->
            </nav>
            <div class="col-md-12" style="padding-left: 0px; padding-right: 0px; line-height: 1.75">
                <div style="float: right; padding-left: 5px; padding-right: 0px; padding-top: 5px;" class="col-md-12 col-sm-12  col-xs-12">
                    <div class='shadow' id='div2' style="min-height: 375px; z-index: 10; overflow: auto;">
                        <center>
                            <h3 style="color: #009900;">
                                <u>GRN Status</u></h3>
                        </center>
                        <div class="col-sm-12" style="text-align: left">
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                GRN
                            </div>
                            <div class="input-group col-sm-4">
                                <%--<span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>--%>
                                <asp:TextBox ID="txtGRN" runat="server" MaxLength="20" TabIndex="1"
                                    CssClass="form-control"
                                    AutoComplete="Off"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGRN"
                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vldgrpLogin" Style="position: absolute"></asp:RequiredFieldValidator>
                            </div>
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                            </div>
                        </div>
                        <div class="col-sm-12" style="text-align: left">
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                Enter Captcha
                            </div>
                            <div class="input-group col-sm-4">
                                <asp:TextBox ID="inpHide" runat="server" AutoComplete="Off" Text=""
                                    TabIndex="1" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqinpHide" runat="server" ControlToValidate="inpHide"
                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vldgrpLogin" Style="position: absolute"></asp:RequiredFieldValidator>
                            </div>
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                            </div>
                        </div>
                        <div class="col-sm-12" style="padding-top: 5px; padding-right: 0px;">
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                Captcha Code
                            </div>
                            <div class="input-group col-sm-4" style="padding-left: 0px;">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="text-align: left; margin-left: -5px;">
                                            <span style="float: left; width: 90%">
                                                <img src="Image/captcha.ashx" height="35px" id="imgCaptcha" width="95%" /></span>
                                            <span style="float: center;"><a
                                                href="#" onclick="javascript:RefreshCaptcha();" id="imge">
                                                <img src="Image/refresh.png" style="margin-top: 15px;" /></a> </span>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                            </div>
                        </div>
                        <div class="col-sm-12" style="padding-top: 5px; padding-right: 0px;">
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                            </div>
                            <div class="input-group col-sm-4" style="padding: 5px; margin-left: -5px;">
                                <asp:Button ID="btnLogin" runat="server" ValidationGroup="vldgrpLogin" Text="Show"
                                    Font-Bold="true" ForeColor="#666666" TabIndex="4" OnClick="btnLogin_Click"
                                    CssClass="btn btn-default" Style="text-align: left; height: auto;"></asp:Button>
                                <%--<asp:Button ID="btneset" OnClientClick="Clear();" runat="server" Text="Reset"
                                    ForeColor="#666666" CssClass="btn btn-default"
                                    Style="height: auto; text-align: center; float: right;" Font-Bold="true"></asp:Button>--%>
                            </div>


                            <div class=" col-sm-4" style="">
                            </div>

                        </div>

                        <div class="col-sm-12" style="padding-top: 5px; padding-right: 0px;">
                            <%--<div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                            </div>--%>
                            <div class=" col-sm-12" style="padding-top: 10px;">
                                <asp:Label ID="lblloginInfo" runat="server" style="text-align:center" Text="label1" Visible="false" ForeColor="Red"></asp:Label>
                                <asp:PlaceHolder ID = "PlaceHolder1"  runat="server" Visible="false"/>
                                <%--<table id="tblInfo" class="table">
                                    <thead>
                                        <tr>
                                            <th>GRN</th>
                                            <th>Amount</th>
                                            <th>Status</th>
                                            <th>Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="success">
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>--%>
                            </div>
                            <%--<div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>

            <div>
            </div>
    </form>
</body>
</html>
