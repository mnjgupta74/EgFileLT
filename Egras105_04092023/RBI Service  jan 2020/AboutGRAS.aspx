<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutGRAS.aspx.cs" Inherits="AboutGRAS" %>

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

                       <li class="col-md-1"><a href="ContactUs.aspx" class="fcolor" style="margin-left: -34px;">ContactUs</a></li>
                       <li class="col-md-2"><a class="fcolor" href="GetGRNStatus.aspx">GRN Status</a></li>

                  </ul>
                </div>
              </div><!-- /.container-fluid -->
            </nav>
            <div class="container">
                <div class="row">

                    <div class="col-md-12 shadow ">
                        <%--<h3>

                        <center>
                            About eGRAS
                        </center>
                    </h3>--%>
                        <div id="divHeader">
                            <h3 align="center" style="color: #009900;"><u>eGRAS ई-ग्रास </u></h3>
                            <p align="center" style="color: #009900;">(Electronic Government Receipt Accounting System) </p>


                        </div>

                        <div id="example2 list-group-item list-group-item-action flex-column align-items-start">

                            <ul>
                                <li><b>ई-ग्रास (Electronic Government Receipt Accounting System- eGRAS) इलेक्ट्रॉनिक राजकीय प्राप्ति लेखांकन प्रणाली, ई-जीआरएएस) राजस्थान सरकार की मिशन मोड प्रोजेक्ट श्रेणी के तहत प्रारंभ की गई ई-गवर्नेंस की पहल है, जो वर्ष 2011 से सफलतापूर्वक संचालित है। 
                                    </b>
                                </li></br>
                                <li><b>एक सेवा व सुविधा के रूप में ई-ग्रास वित्त विभाग, राजस्थान सरकार की ऑनलाइन राजकीय प्राप्ति लेखांकन प्रणाली है, जिसके माध्यम से राजकोष में निर्धारित बजट मद में धनराशि जमा कराने की सुविधा है। यह सेवा कर तथा गैर-कर राजस्व के संग्रह की सुविधा प्रदान करती है। 
                                    </b>
                                </li></br>
                                <li><b>यह सेवा वित्त विभाग, राजस्थान सरकार की एकीकृत वित्तीय प्रबंधन प्रणाली (Integrated Financial Management System-IFMS) से जुड़ी हुई है और इसकी समस्त प्राप्तियाँ 
एकीकृत राजस्व प्रबंधन प्रणाली (Integrated Revenue Management System-IRMS) पर प्रदर्शित होती हैं। 
                                </b>
                                </li></br>
                                <li><b>ई-ग्रास विभिन्न बैंकिंग सेवाओं से भी जुड़ा हुआ है और यह 
वर्तमान में 35 से अधिक बैंकों के साथ कार्ड और नेटबैंकिंग के माध्यम से भुगतान की सुविधा देता है। 
                                </b>
                                </li></br>
                                <li><b>ई-ग्रास ऑनलाइन के साथ-साथ मैनुअल दोनों मोड में कार्य करता है। 
</b>
                                </li></br>
                                <li><b>ई-ग्रास पर जमा राशि की रसीद दो रूपों में ऑनलाइन जारी की जाती है- 
1. ई-चालान (e-Challan) 
2. बैंक चालान (Bank Challan) </b>
                                </li></br>
                                <li><b>ई-ग्रास पर जमा राशि की रसीद या चालान समस्त राजकीय सेवाओं की देय राशि व हस्तांतरणों (Transactions) के लिए मान्य है। 
</b>
                                </li></br>
                                <li><b>पंजीयन व मुद्रांक शुल्क अर्थात् स्टाम्प ड्यूटी के सरल भुगतान की सुविधा के लिए राज-स्टाम्प नामक स्वतंत्र सुविधा भी विकसित की गई है, जिससे बिना किसी अतिरिक्त सुविधा शुल्क या कमीशन के भुगतान के कोई आवेदक राशि जमा कर सकता है। 
                                    </b>
                                </li></br>
                                <li><b>ई-ग्रास पर जमा राशि की रसीद या चालान को आवेदक अपनी सुविधा अनुसार निरस्त करने की कार्यवाही भी कर सकता है। ऐसी स्थिति में उसे यथानुसार अपने मूल बैंक एकाउंट में राशि प्राप्त हो सकेगी। 
                                    </b>
                                </li></br>
                                <li><b>ई-ग्रास पर जमा राशि की रसीद के रूप में जारी चालान के यूनिक नंबर के रूप में GRN (Government Receipt Number) जारी होता है। चूंकि प्रत्येक चालान का राजकीय प्राप्ति क्रमांक (GRN) एक यूनिक नंबर के रूप में होता है, अत: इसकी स्थिति को ई-ग्रास के पोर्टल पर वैलिडेट या वेरिफाई किया जा सकता है। जैसे ही कोई चालान किसी सेवा या हस्तांतरण में प्रयुक्त हो जाता है, वह डीफेस (Deface) हो जाता है और उसके नंबर के साथ उसके प्रयुक्त (Used) होने का स्टैटस (Status) प्रदर्शित हो जाता है। इसकी सरल सूचना ई-ग्रास के पोर्टल (https://egras.rajasthan.gov.in/)
पर होम पेज पर उपलब्ध है। प्रत्येक राजकीय विभाग, उपक्रम, संस्थान व स्वायत्त संस्थान का दायित्व है कि वह अपने यहां ई-ग्रास पर जमा राशि के प्रयोग के पूर्व उसका स्टैटस (Status) आवश्यक रूप से चेक कर ले और अपनी सेवा में ई-ग्रास के ऑटो चेक के लिए एपीआई से इंटीग्रेट करने की कार्यवाही सुनिश्चित कर ले। 
</b>
                                </li><br/>
                                <li><b>ई-ग्रास संबंधी समस्त विस्तृत सुविधाओं व सूचनाओं को विभागों के लिए निर्धारित डैशबोर्ड पर आधिकारिक डोमेन में SSO Login पर उपलब्ध कराया गया है। जनसामान्य के लिए भी गैर-पंजीकृत उपयोगकर्ताओं हेतु उपयोगकर्ता नाम "Guest" और पासवर्ड "Guest" के साथ सीमित एक्सेस दिया गया है।
                                </b>
                                </li><br/>
                            </ul>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
