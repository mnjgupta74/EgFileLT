<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
        function ForgetPassword() {
            var xorKey = 13;
            var id = "ForgetPassword";
            var result = "";
            for (i = 0; i < id.length; ++i) {
                result += String.fromCharCode(xorKey ^ id.charCodeAt(i));
            }

            document.getElementById("ForgetPassword").href = "WebPages/Account/EgForgotPassword.aspx?id=" + result;
        }
        function ResetAttempt() {
            var xorKey = 13;
            var id = "ResetAttempt";
            var result = "";
            for (i = 0; i < id.length; ++i) {
                result += String.fromCharCode(xorKey ^ id.charCodeAt(i));
            }
            document.getElementById("ResetAttempt").href = "WebPages/Account/EgForgotPassword.aspx?id=" + result;
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
            document.getElementById("txtUserName").focus();
            return false;
        }


        function clickme(seed) {
            if (document.getElementById("txtPassword").value == "") {
                alert("Password can'nt be empty");
                document.getElementById("txtPassword").focus();
                return false;
            }
            else {
               
                var vhash;
                var rexp = /^\w+$/;
                var password = document.getElementById("txtPassword").value;
                var uid = document.getElementById("txtPassword").value;
                if (password.length = 0 || password.length > 20) {
                    alert("Please enter valid password");
                    document.getElementById("txtPassword").focus();
                    return false;
                }
                document.getElementById("txtPassword").value = hex_md5(seed + hex_md5(password))
                document.getElementById("hdnPassword").value = SHA256(seed + SHA256(password))
                return true;
            }
        }

        function PopUpDetecter() {
            if (document.URL == "http://localhost:49264/Server - Server/Default.aspx") {
                if (/Firefox[\/\s](\d+\.\d+)/.test(navigator.userAgent)) {
                    var popup = window.open('blank.htm', 'popup', "width=1,height=1,toolbar=no, top=3000px,left=3000,menubar=no,scrollbars=no,resizable=0");
                    if (!popup) {
                        alert('Popup blocker detected.' + "\n" + 'Please unblock it for better experience');
                        alert('To unblock PopUp follow steps :' + "\n" + 'Go to Tools' + "\n" + 'Click on Options' + "\n" + 'Click on Content' + "\n" + 'Now Uncheck "Block pop-up windows"');
                        return false;
                    }
                    else {
                        popup.close();
                        return true;
                    }
                }
                else if (/Chrome[\/\s](\d+\.\d+)/.test(navigator.userAgent)) {
                    var w = window.open('blank.htm', 'popup', "width=1,height=1,toolbar=no, top=3000px,left=3000,menubar=no,scrollbars=no,resizable=0");
                    var mybrsAgent = navigator.userAgent.toLowerCase();
                    var mybrsAgent = navigator.userAgent.toLowerCase();
                    if (w) {
                        if ((mybrsAgent.search(/chrome\//) != -1)) {
                            w.onload = function () {
                                setTimeout(function () {
                                    if (w.screenX === 0) {
                                        returnCromeValue(false);
                                    } else {
                                        w.close();
                                        returnCromeValue(true);
                                    }
                                }, 0);
                            };
                        }
                    }
                    var allowed = "Blocked";
                    function returnCromeValue(arg) {
                        if (arg == true) {
                            return true;
                        }
                        else {
                            allowed = "Popup blocker detected." + "\n" + "Please unblock it for better experience ";
                            alert(allowed);
                            alert('To unblock PopUp follow steps :' + "\n" + 'Go to Settings' + "\n" + 'Click on "Show Advanced settings" link' + "\n" + 'Click on Privacy' + "\n" + 'Click on content settings' + "\n" + 'In Pop-ups section check "Allow all sites to show pop-ups"');
                        }
                        return allowed;
                    }
                }
                else if (/IE[\/\s](\d+\.\d+)/.test(navigator.userAgent)) {
                    var popup = window.open('blank.htm', 'popup', "width=1,height=1,toolbar=no, top=3000px,left=3000,menubar=no,scrollbars=no,resizable=0");
                    if (!popup) {
                        alert('Popup blocker detected.' + "\n" + 'Please unblock it for better experience ');
                        alert('To unblock PopUp follow steps :' + "\n" + 'Go to Tools' + "\n" + 'Click on Internet Options' + "\n" + 'Click on Privacy' + "\n" + 'In Pop-Up Blocker section Uncheck "Turn on Pop-up Blocker"');
                        return false;
                    }
                    else {
                        popup.close();
                        return true;
                    }
                }

                else {
                    alert(navigator.appName);
                }
            }
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
                  <a class="navbar-brand fcolor" href="#">eGRAS</a>
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

                      
              
                       <li class="col-md-2"><a href="FAQ.htm" class="fcolor " >FAQ</a></li>
                       <li class="col-md-2"><a  href="EgCircularPdf.aspx" class="fcolor">Circular</a></li>
                        <li class="dropdown col-md-2">
                      <a href="#" class="dropdown-toggle fcolor" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Officer <span class="caret"></span></a>
                      <ul class="dropdown-menu">
                        <li><a href="NodalDetails.aspx">Nodal Officer</a></li>
                           <li role="separator" class="divider"></li>
                        <li><a href="EgBankOfficerDetails.aspx">Bank Officer</a></li>
                      </ul>
                    </li>

                       <li class="col-md-2"><a href="ContactUs.aspx" class="fcolor">Contact Us</a></li>
                  </ul>
                </div><!-- /.navbar-collapse -->
              </div><!-- /.container-fluid -->
            </nav>
            <div class="col-md-12" style="height: 40px;">
                <%--<marquee bgcolor=yellow behavior="alternate" direction="left" scrollamount="4" ><font size = 4 color = "red"> Today Site will not available from 5.30 PM to 6.00 PM </font></marquee>--%>
            </div>
            <div class="col-md-12" style="padding-left: 0px; padding-right: 0px; line-height: 1.75">
                <div style="float: left; padding-left: 5px; padding-right: 0px; padding-top: 5px;" class="col-md-8 col-sm-6 col-xs-12">
                    <div class='brd1 shadow login' id='div3' style="color: Black; padding-bottom: 5px; min-height: 375px; overflow: auto; top: 0px; z-index: 40;">
                        <center>
                            <h3 style="color: #009900;">
                                <u>About e-GRAS</u></h3>
                        </center>
                        <p style="padding-left: 5px; margin-right: 0px; text-align: left; /*background-image: url('Image/imagesCAKCNEEP.jpg'); */ text-decoration: blink; background-repeat: no-repeat;">
                            Online Government Receipts Accounting System (e-GRAS) is an e-Governance Initiative
                                                                                    of Government of Rajasthan under Mission Mode Project category and is part of Integrated
                                                                                    Financial Management System. e-GRAS facilitates collection of tax/non tax revenue
                                                                                    in both the mode online as well as manual.
                                                                                    <br />
                            <%-- </p>--%>
                            Limited access account with User Name "guest" and Password "guest" For Non-Registered User. 
                            <br />
                            <%--<asp:Image ID="Image2" runat="server" ImageUrl="~/Image/new1_e0.gif" />--%><img src="Image/new.gif" />
                            <u style="color: #3CB371; font-weight: bold; text-decoration: underline blink; /*background-color: #CCFF99; */">eGRAS facilitates payments through Cards and Netbanking with 35+ Banks: </u>
                            <br />
                            <div class="col-md-12">
                                <div class="panel col-md-12" style="text-align: left; border-radius: 0px; margin-bottom: 0px; background-color: azure; padding: 5px;">
                                    <ul>

                                        <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                            <li>
                                                <img src="Image/EpayBanksLogo/SBI.png" />&nbsp; State Bank of India
                                            </li>
                                            <li>
                                                <img src="Image/EpayBanksLogo/Canra.png" />&nbsp; Canara Bank
                                            </li>
                                            <li>
                                                <img src="Image/EpayBanksLogo/BOB.png" />&nbsp; Bank of Baroda
                                            </li>
                                        </div>
                                        <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                            <li>
                                                <img src="Image/EpayBanksLogo/IDBI.png" />&nbsp; IDBI Bank
                                            </li>
                                            <li>
                                                <img src="Image/EpayBanksLogo/PNB.png" />&nbsp; Punjab National Bank
                                            </li>
                                            <li>
                                                <img src="Image/EpayBanksLogo/CBI.png" />&nbsp; Central Bank of India
                                            </li>
                                        </div>
                                        <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                            <%--<li>
                                                <img src="Image/EpayBanksLogo/OBC.png" />&nbsp; Oriental Bank of Commerce
                                            </li>--%>
                                            <li>

                                                <img src="Image/EpayBanksLogo/Union1.png" />&nbsp; Union Bank
                                            </li>
                                            <li>
                                                <img src="Image/EpayBanksLogo/boi.png" />&nbsp; Bank Of India
                                            </li>
                                             <li>
                                                <img src="Image/EpayBanksLogo/icici.png" />&nbsp; ICICI
                                            </li>

                                        </div>
                                        <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                            <li>Payment Gateway and <a href="#" data-toggle="modal" data-target="#exampleModal" style="color: red">More....</a>
                                            </li>
                                        </div>

                                    </ul>
                                </div>
                                <div class="col-md-12" style="text-align: left; padding: 5px;">
                                    <u style="color: #3CB371; font-weight: bold; text-decoration: underline blink;">Any Branch Banking:</u>
                                </div>
                                <div class="panel col-md-12" style="text-align: left; border-radius: 0px; margin-bottom: 0px; background-color: azure; padding: 5px;">
                                    <ul>
                                        <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                            <li>
                                                <img src="Image/EpayBanksLogo/SBI.png" />&nbsp; State Bank of India
                                            </li>
                                        </div>
                                        <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                            <li>
                                                <img src="Image/EpayBanksLogo/PNB.png" />&nbsp; Punjab National Bank
                                            </li>
                                        </div>
                                        <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                            <li>
                                                <img src="Image/EpayBanksLogo/CBI.png" />&nbsp; Central Bank of India
                                            </li>
                                        </div>
                                        <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                        </div>
                                    </ul>
                                </div>
                            </div>
                            <br />

                        </p>
                    </div>
                </div>
                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header" style="height: 35px">
                                <h5 class="modal-title col-xs-11" id="exampleModalLabel" style="text-align: center; font-weight: 600; color: #004F00;">Associated Banks</h5>
                                <button type="button" class="close col-xs-1" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <h4 class="headingcls"><u>SBI - ePay</u></h4>
                                <div id="PAYuBanks" class="row" style="color: #2874f0">
                                    <div class="col-md-4 col-sm-6" style="padding-right: 0px;">
                                        <li>Bank of Maharashtra</li>
                                        <li>Catholic Syrian Bank</li>
                                        <li>City Union Bank</li>
                                        <li>DCB Bank Personal</li>
                                        <li>Dhanlaxmi Bank-Corporate</li>
                                        <li>Dhanlaxmi Bank-Retail</li>
                                        <li>Federal Bank</li>
                                        <li>HDFC Retail Bank</li>
                                    </div>

                                    <div class="col-md-4 col-sm-6" style="padding-right: 0px;">

                                        <li>Indian Bank</li>
                                        <li>IndusInd Bank</li>
                                        <li>Jammu and Kashmir Bank</li>
                                        <li>Janata Sahakari Bank Ltd. Pune</li>
                                        <li>Karnataka Bank Ltd</li>
                                        <li>Karur Vysya Bank</li>
                                        <li>Kotak Mahindra Bank</li>
                                        <li>Lakshmi Vilas Bank</li>
                                        <li>Mehsana Urban Co. Op. Bank Ltd</li>
                                        <li>PMC Bank Ltd</li>


                                    </div>
                                    <div class="col-md-4 col-sm-6" style="padding-right: 0px;">
                                        <li>Punjab and Sind Bank</li>
                                        <li>SVC - Retail</li>
                                        <li>Saraswat Bank</li>
                                        <li>South Indian Bank</li>
                                        <li>Tamilnad Mercantile Bank</li>
                                        <li>UCO Bank</li>
                                        <li>YES Bank</li>
                                    </div>

                                </div>
                                <%--<hr />--%>
                                <h4 class="headingcls"><u>PNB Gateway</u></h4>
                                <div id="EPayBanks" class="row" style="color: #2874f0">
                                    <div class="col-md-4 col-sm-6" style="padding-right: 0px;">
                                        <li>Airtel Payments Bank       </li>
                                        <li>AXIS Bank                  </li>
                                        <li>Bank of Maharashtra        </li>
                                        <li>Catholic Syrian Bank       </li>
                                        <li>Citibank Netbanking        </li>
                                        <li>City Union Bank            </li>
                                        <li>Cosmos Bank                </li>
                                        <li>DCB Bank                   </li>
                                        <li>Deutsche Bank              </li>
                                        <li>Dhanlaxmi Bank             </li>
                                        <li>Federal Bank                                </li>


                                    </div>
                                    <div class="col-md-4 col-sm-6" style="padding-right: 0px;">
                                        <li>HDFC Bank                                   </li>
                                        <li>IDFC Netbanking                             </li>
                                        <li>Indian Bank                                 </li>
                                        <li>Indian Overseas Bank                        </li>
                                        <li>IndusInd Bank                               </li>
                                        <li>Jammu and Kashmir Bank                      </li>
                                        <li>Janata Sahakari Bank Pune                   </li>
                                        <li>Karnataka Bank                              </li>
                                        <li>Karur Vysya - Corporate          </li>
                                        <li>Karur Vysya - Retail             </li>
                                        <li>Kotak Mahindra Bank                         </li>
                                        <li>Lakshmi Vilas Bank - Retail      </li>
                                        <li>Lakshmi Vilas Bank - Corporate   </li>
                                        <li>PMC Bank Limited   </li>
                                    </div>
                                    <div class="col-md-4 col-sm-6" style="padding-right: 0px;">

                                        <li>Punjab And Sind Bank                               </li>
                                        <li>Saraswat Bank                                      </li>
                                        <li>SVC Bank Ltd.              </li>
                                        <li>South Indian Bank                                  </li>
                                        <li>Tamilnad Mercantile Bank                           </li>
                                        <li>The Bharat Co-op. Bank Ltd                         </li>
                                        <li>The Nainital Bank                                  </li>
                                        <li>UCO Bank          
                                        <li>Yes Bank                                           </li>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer" style="margin-top: 0px; padding: 6px 20px 6px">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal" style="height: auto">Close</button>
                                <%--<button type="button" class="btn btn-primary" style="height:auto">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div style="float: right; padding-left: 5px; padding-right: 0px; padding-top: 5px;" class="col-md-4 col-sm-6  col-xs-12">
                    <div class='brd1 shadow login' id='div2' style="min-height: 375px; z-index: 10; overflow: auto;">
                        <center>
                            <h3 style="color: #009900;">
                                <u>Sign In</u></h3>
                        </center>
                        <div class="col-sm-12" style="text-align: left">
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                User Name
                            </div>
                            <div class="input-group col-sm-8">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" TabIndex="1" CssClass="form-control" Style=""
                                    AutoComplete="Off"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vldgrpLogin" Style="position: absolute"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-12" style="padding-top: 5px;">
                            <div id='td4' class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                Password
                            </div>
                            <div class="input-group col-sm-8">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                <asp:TextBox ID="txtPassword" runat="server" MaxLength="25" TabIndex="1" TextMode="Password" CssClass="form-control"
                                    AutoComplete="Off"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vldgrpLogin" Style="position: absolute"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--  </div>
                                <div id='td5' class='col-md-8'>
                                    <div class='usericon'>
                                        <img src="Image/password_icon.png" width='25px' height='25px' style="position: absolute; left: 0px;" />
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="160px" AutoComplete="Off"
                                            MaxLength="25" CssClass="loginInput" TabIndex="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>

                        <div class="col-sm-12" style="padding-top: 5px;">
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                Enter Captcha
                            </div>
                            <div class="input-group col-sm-8" style="padding-left: 0px; margin-left: -5px;">
                                <asp:TextBox ID="inpHide" runat="server" AutoComplete="Off" Text="" TabIndex="3" MaxLength="6"
                                    Style=""
                                    CssClass="form-control col-sm-11"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqinpHide" runat="server" ControlToValidate="inpHide"
                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vldgrpLogin" Style="position: absolute"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-12" style="padding-top: 5px; padding-right: 0px;">
                            <div class='col-sm-4' style="text-align: left; padding-left: 5px; padding-right: 0px;">
                                Captcha Code
                            </div>
                            <div class=" col-sm-8" style="padding-left: 0px;">
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

                        </div>
                        <div class="col-sm-12" style="padding-top: 25px;">
                            <div class="col-xs-4">
                            </div>
                            <div class="col-xs-4" style="padding: 5px; margin-left: -5px;">
                                <asp:Button ID="btnLogin" runat="server" ValidationGroup="vldgrpLogin" Text="Log In"
                                    Font-Bold="true" ForeColor="#666666" TabIndex="4" OnClick="btnLogin_Click" CssClass="btn btn-default" Style="height: auto; width: 103%; text-align: center;"></asp:Button>
                            </div>
                            <div class="col-xs-4" style="padding: 5px; padding-left: 15px;">
                                <asp:Button ID="btneset" OnClientClick="Clear();" runat="server" Text="Reset" ForeColor="#666666" CssClass="btn btn-default" Style="height: auto; width: 108%; text-align: center; float: left;" Font-Bold="true"></asp:Button>
                            </div>

                        </div>
                        <div class="col-xs-12" style="padding-top: 10px; padding-left: 5px; padding-right: 5px;">
                            <div class="col-xs-6" style="text-align: left; padding: 5px;">
                                <a href="#" onclick="ForgetPassword();" id="ForgetPassword" style="font-weight: 600;">Forgot password?</a>
                            </div>
                            <div class="col-xs-6" style="text-align: right; padding: 5px;">
                                <a href="WebPages/Account/EgUserRegistration.aspx" style="font-weight: 600;">New User?Sign up</a>
                            </div>
                        </div>
                        <div>
                            <div class="col-xs-12" style="padding-top: 10px;">
                                <asp:Label ID="lblloginInfo" runat="server" Text="label1" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div>
            <asp:Panel ID="NIC" runat="Server" HorizontalAlign="Center" Font-Bold="True"
                ForeColor="green" Style="padding-top: 3px;">
                || Application Designed,Developed &amp; hosted by
                                                            <asp:HyperLink ID="HlnkNic" runat="server" Text=" National Informatics Centre" NavigateUrl="Http://www.nic.in"
                                                                Target="_blank"></asp:HyperLink>. Contents provided by Finance Department Govt.
                                                            of Rajasthan |<asp:Label ID="lblServerName" runat="server" Text=""  ForeColor="green"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
