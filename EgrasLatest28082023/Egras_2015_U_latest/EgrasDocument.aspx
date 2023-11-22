<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgrasDocument.aspx.cs" Inherits="EgrasDocument" %>

<!DOCTYPE html>

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
        th{
            display: contents;
        }
        td{
            display:contents;
        }

        table #grdPDFUpload {
            border: 0 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div>
                <img name="Grass" src="App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left"
                    width="100%"/>
            </div>

            <div class='brd1 shadow login' id='div3' style="color: Black; padding-bottom: 5px; min-height: 375px; overflow: auto; top: 0px; z-index: 40; text-align: left;">
                <table align="center" width="70%">
                    <tbody>
                        <tr>
                            <td>
                                <div id="divHeader">
                                    <h3 align="center" style="color: #009900;"><u>Egras Document</u>
                                    </h3>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table align="center" id="MainTable">
                                    <tr>
                                        <td>
                                            <asp:GridView runat="server" ID="grdPDFUpload" AutoGenerateColumns="False" CssClass="table table-striped table-bordered margin-top-zero" Width="100%" DataKeyNames="PdfByte,FileName"
                                                OnRowCommand="grdPDFUpload_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDownload" runat="server" Text='<%#Eval("FileName")%>' CommandArgument="<%# Container.DataItemIndex %>"
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




