<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgCircularPdf.aspx.cs" Inherits="EgCircularPdf" %>

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
        table #grdCircular
        {
            border:0 !important;
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



            <div class='brd1 shadow login' id='div3' style="color: Black; padding-bottom: 5px; min-height: 375px; overflow: auto; top: 0px; z-index: 40; text-align: left;">
                <table align="center" width="70%">
                    <tbody>
                        <tr>
                            <td>
                                <div id="divHeader">
                                    <h3 align="center" style="color: #009900;"><u>Circular/Order</u>
                                    </h3>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table align="center" id="MainTable">
                                    <tr>
                                        <td>
                                            <%--<asp:Repeater ID="RptCircular" runat="server" Visible="true" OnItemCommand="RptCircular_ItemCommand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpdfByte" Text='<%#Eval("PdfByte")%>' Visible ="false" runat="server"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("PDFid") %>'
                                                        CommandName="download"><%#Eval("pdfname")%></asp:LinkButton>--%>
                                            <%--<li><a target="_blank" href="Upload/<%#Eval("PdfPath")%>"><%#Eval("pdfname")%></a> </li>--%>
                                            <%--<li><a target="_blank" href="Upload/<%#Eval("PdfPath")%>"><%#Eval("pdfname")%></a> </li>--%>
                                            <%-- <br>
                                                </ItemTemplate>
                                            </asp:Repeater>--%>
                                            <asp:GridView runat="server" ID="grdCircular"  AutoGenerateColumns="False" CssClass="table table-striped table-bordered margin-top-zero" Width="100%" DataKeyNames="PdfByte,pdfname"
                                                
                                                OnRowCommand="grdCircular_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDownload" runat="server" Text='<%#Eval("pdfname")%>' CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="Download" Font-Bold="true" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>


        </div>
    </form>
</body>
</html>
