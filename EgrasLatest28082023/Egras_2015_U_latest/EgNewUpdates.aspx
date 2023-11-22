<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgNewUpdates.aspx.cs" Inherits="EgNewUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" width="70%">
            <tbody>
                <tr valign="middle" style="color: Blue; font-size: small;">
                    <td class="style2">
                        <img name="Grass" src="App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left"
                            width="1024px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="Default.aspx">Home</a>
                    </td>
                    <td>
                </tr>
                <tr>
                    <td align="center">
                        <hr style="color: #c00; background-color: #c00;" />
                        <span style="width: 300px; color: Green; font-family: Verdana; font-size: 13px;">New
                            Updates</span>
                        <hr style="color: #c00; background-color: #c00;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <center>
                                <asp:Repeater ID="rptNewUpdate" runat="server">
                                    <ItemTemplate>
                                        <a href="Upload/<%#Eval("PdfPath")%>" target="_blank">
                                            <fieldset id="fldNewUpdate" runat="server" style="text-align: left; border-top-left-radius: 0.5em 0.5em;
                                                border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
                                                border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 2px 2px 5px #888888;
                                                behavior: url(PIE.htc); width: 600px; max-width: 100%;">
                                                <span>
                                                    <%#Eval("pdfname")%>
                                                </span>
                                                <br />
                                                <span style="margin-left: 550px;">
                                                    <img src="Image/new.gif" />
                                                </span>
                                                <br />
                                            </fieldset>
                                        </a>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </center>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
