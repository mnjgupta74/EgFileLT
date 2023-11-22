﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodalDetails.aspx.cs" Inherits="NodalDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="https://egras.rajasthan.gov.in/js/jquery-3.6.0.min.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Egras.Rajasthan.gov.in</title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            width: 1023px;
        }

        .auto-style2 {
            width: 443px;
        }

        body {
            margin: 0;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            RefreshCaptcha();
        });
        function generaterandomnumber() {
            var subject = Math.floor(Math.random() * 99999) + 1;
            return subject;
        }

        function RefreshCaptcha() {
            var img = document.getElementById("imgCaptcha");
            img.src = "Image/captcha.ashx?arg=" + generaterandomnumber();
        }
        //window.onload = RefreshCaptcha;
    </script>

</head>
<body text="Submit">
    <form id="form2" runat="server">
        <div id="div1" class="container">
            <div>
                <img name="Grass" src="App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left"
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
            <div class='shadow' id='div3' style="border: 2px solid #999999; background-color: white; color: Black; padding-bottom: 5px; overflow: auto; top: 0px; z-index: 40; text-align: left;">
                <h3 align="center" style="color: #009900;"><u>Nodel Officer Details</u>
                </h3>
                <div style="width: 80%; margin: auto;" class="col-md-12">
                    <div class="row" style="margin-bottom: 10px">
                        <div class="col-md-12">
                            <asp:DropDownList ID="DropDownList1" Width="70%" Height="30px" runat="server"></asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vldgrpLogin" InitialValue="0" Style="position: absolute"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 10px">
                        <div class="col-md-2">Enter Captcha </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="inpHide" runat="server" AutoComplete="Off" Text="" Width="350px" Height="30px" TabIndex="3"></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                            <asp:RequiredFieldValidator ID="reqinpHide" runat="server" ControlToValidate="inpHide"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vldgrpLogin" Style="position: absolute"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 10px">
                        <div class="col-md-12">
                            <div class="col-md-2" style="margin-left: -15px">Captcha Code</div>
                            <div class="col-md-6">
                                <img src="Image/captcha.ashx" height="35px" style="margin-left: 5px" id="imgCaptcha" width="340px" />
                                <a
                                    href="#" onclick="javascript:RefreshCaptcha();" id="imge">
                                    <img src="Image/refresh.png" style="margin-top: 15px;" /></a>
                            </div>
                            <div class="col-md-4" style="margin-left: -35px">
                                <asp:Button ID="btnsubmit" Text="Submit" runat="server" ValidationGroup="vldgrpLogin" OnClick="Button1_Click" Font-Bold="True" Style="text-align: left" Width="25%" Height="28px"></asp:Button>
                            </div>
                        </div>
                    </div>

                </div>
                <tr style="height: 45px">
                    <td align="center" class="auto-style1" colspan="2">
                        <fieldset id="field1" runat="server" visible="false" style="width: 800px">
                            <b><span style="width: 200px; color: Green; font-family: Arial CE; font-size: 13px;">
                                <asp:Label ID="lbldeptName" runat="server" Text="Label"></asp:Label>
                            </span></b>
                        </fieldset>
                        <asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333" RepeatDirection="Horizontal"
                            RepeatColumns="2" AlternatingItemStyle-BorderColor="White" AlternatingItemStyle-BorderWidth="2"
                            AlternatingItemStyle-VerticalAlign="Top" AlternatingItemStyle-Width="400px" ItemStyle-Width="400px"
                            ItemStyle-BorderWidth="2" ItemStyle-VerticalAlign="Top" ItemStyle-BorderColor="White">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingItemStyle BorderColor="White" BorderWidth="2px" Width="400px" VerticalAlign="Top"></AlternatingItemStyle>
                            <ItemStyle BackColor="#EFF3FB" VerticalAlign="Top" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <ItemStyle />
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 150px; vertical-align: top;">
                                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Nodal Officer :</span></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("NodalName") %>' Font-Names="Verdana"
                                                Font-Size="15pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; vertical-align: top;">
                                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Contact Number:</span></b>&nbsp;
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Number") %>' Font-Size="20pt"
                                                Font-Names="Verdana" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; vertical-align: top;">
                                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Email-ID:</span></b>&nbsp;
                                        </td>
                                        <td style="vertical-align: top;">
                                            <img alt="" src='Image/DrawImage.ashx?arg= <%#Eval("EmailID") %>' width="300px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; vertical-align: top;">
                                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">Address :</span></b>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Address") %>' Font-Names="Verdana"
                                                Font-Size="15pt" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="auto-style1" colspan="2">
                        <asp:Label ID="lblMsg" runat="server" Text=" Data Not Found" Visible="false" ForeColor="#336699"
                            Font-Names="Verdana" Font-Size="20pt" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
