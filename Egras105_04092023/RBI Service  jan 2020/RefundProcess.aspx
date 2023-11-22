<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefundProcess.aspx.cs" Inherits="RefundProcess" %>

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
    <style>
        #example1 {
            border-block-style: solid;
            border-block-width: 500px;
        }

        #example2 {
            border-block-style: solid;
            border-block-width: thin thick;
        }

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="srcManager" runat="server">
        </asp:ScriptManager>
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

                       <li class="col-md-1"><a href="ContactUs.aspx" style="margin-left: -34px;" class="fcolor">ContactUs</a></li>
                       <li class="col-md-2"><a class="fcolor" href="GetGRNStatus.aspx">GRN Status</a></li>

                  </ul>
                </div><!-- /.navbar-collapse -->
              </div><!-- /.container-fluid -->
            </nav>
            <div class="container">
                <div class="row">
                    <%--<div class="col-md-2"></div>--%>
                    <div class="col-md-12 shadow">
                        <h3 align="center" style="color: #009900;"><u>-ई-ग्रास के तहत ऑनलाइन जमा हुए राजकीय राजस्व के रिफ़ंड की प्रक्रिया-
                        
                        </u>
                        </h3>

                        <div id="example2 list-group-item list-group-item-action flex-column align-items-start">
                            <p>
                                <right>
    <br>
    <b>
        <ul>
            <li>
        <b>
        ऑनलाइन जमा हुए राजकीय राजस्व के रिफंड हेतु सामान्य वित्तीय एवं लेखा नियम भाग- III के बिंदु संख्या 38 के अनुसार संबंधित विभाग के समक्ष अधिकारी स्तर से स्वीकृति जारी जायेगी ।<br> स्वीकृति जारी कराए जाने से पूर्व सामान्य वित्तीय एवं लेखा नियम भाग-I के नियम 255 के प्रावधान की पालना भी सुनिश्चित की जायेगी ।<br>
    </b><br>
    
        <li>रिफंड हेतु जारी सक्षम अधिकारी के आधार पर संबंधित कोषाधिकारी / उप कोषाधिकारी द्वारा राशि का रिफंड किया जायेगा। </li><br>
    <li>
        <b>
        राशि रिफंड से पूर्व संबंधित कोषाधिकारी/ उप कोषाधिकारी द्वारा ई-ग्रास साइट पर जाकर बिल के साथ संलगन  चालान पर अंकित जी.आर.एन. के आधार पर राशि जमा होने की पुष्टि संतुष्टि की जायेगी।<br>
        <br>
    </b>
    </li>
    <li>
        <b>
        राशि जमा होने की पुष्टि के पश्चात  कोषाधिकारी /उप कोषाधिकारी द्वारा रिफंड बिल पारित किया जायेगा।<br>
        <br>
    </b>
    </li>
    <li>
        <b>
        ऑनलाइन जमा हुए राजकीय राजस्व के रिफंड का लेखा हेतु संबंधित कोषाधिकारी /उप कोषाधिकारी द्वारा एक रजिस्टर का संधारण किया जायेगा। <br> जिसमें राशि जमा होने का पूर्ण विवरण (जी.आर.एन. न. दिनांक, जमाकर्ता का नाम एवं पता, चालान न. कुल जमा हुई राशि आदि) अंकित किया जायेगा <br>तथा उसके आगे राशि रिफंड की जारी स्वीकृति क्रमांक एवं दिनांक तथा राशि का उल्लेख किया जायेगा तथा कोषाधिकारी /अतिरिक्त कोषाधिकारी/ सहायक कोषाधिकारी द्वारा हस्ताक्षर किए जायेगे।<br>
        <br>
    </b>
    </li>
    <li> <b>
         कोषाधिकारी /उप कोषाधिकारी द्वारा जमा राशि की पुष्टि/संतुष्टि किए जाने के पश्चात डिफेस विकल्प के माध्यम से रिफंड की जाने वाली राशि को डिफेस किया जाएगा <br>ताकि उक्त राशि के पुनः रिफंड की संभावना न हो। इस हेतु ई-ग्रास साइट पर कोषाधिकारी /उपकोषाधिकारी के लॉगिन पर यह सुविधा उपलब्ध करा दी गयी है।<br>
         <br></b>
        </li>
    <li><b>
        ऑनलाइन जमा हुए राजस्व रिफंड का लेखा संबंधित कोषालय\उपकोषालय द्वारा महालेखाकर कार्यालय को प्रस्तुत किया जायेगा।<br>
        <br>
    </b>
        
    </li>
        </ul>
    
    
    
    </right>
                                </b>

                            </p>
                        </div>
                    </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
