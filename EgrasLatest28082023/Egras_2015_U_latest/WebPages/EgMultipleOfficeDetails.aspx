<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="EgMultipleOfficeDetails.aspx.cs" Inherits="WebPages_EgMultipleOfficeDetails" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <table id="tblExtradetails" width="80%" align="center">
        <tr align="center">
            <td style="height: 16px; background-color: white" valign="top">
                <div style="width: 100%; margin-left: 0px">
                    <b>GRN:</b>&nbsp <asp:Label style="font-size:large" Text="" ID="lblGRN" runat="server"  />
                    <asp:Repeater ID="RptOffices" runat="server" Visible="true">
                        <FooterTemplate>
                            <asp:Label ID="lblEmpty" runat="server" Font-Bold="true" ForeColor="#20872E"></asp:Label>
                        </FooterTemplate>
                        <HeaderTemplate>
                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                <tr style="background-color: #20b0ff; color:white">
                                    <td style="text-align: center" width="200px">Sno
                                    </td>
                                    <td style="text-align: center" width="200px">Location
                                    </td>
                                    <td style="text-align: center" width="200px">Office Name
                                    </td>
                                    <td style="text-align: center" width="200px">Amount
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table border="1" style="border-width:0.5px" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: center" width="200px">
                                        <%# Container.ItemIndex+1 %>
                                    </td>
                                    <td style="text-align: center" width="200px">
                                        <%# DataBinder.Eval(Container.DataItem, "TreasuryName")%>
                                    </td>
                                    <td style="text-align: center" width="200px">
                                        <%# DataBinder.Eval(Container.DataItem, "Office")%>
                                    </td>
                                    <td style="text-align: right" width="200px">
                                        <%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table border="1" style="border-width:0.5px" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: center" width="200px">
                                        <%# Container.ItemIndex+1 %>
                                    </td>
                                    <td style="text-align: center" width="200px">
                                        <%# DataBinder.Eval(Container.DataItem, "TreasuryName")%>
                                    </td>
                                    <td style="text-align: center" width="200px">
                                        <%# DataBinder.Eval(Container.DataItem, "Office")%>
                                    </td>
                                    <td style="text-align: right" width="200px">
                                        <%# string.Format("{0:0.00}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="2" style="text-align: right" width="100px">
                                       Total Amount
                                    </td>
                                    <td colspan="2" style="text-align: right" width="100px">
                                        <%# GetTotalAmount() %>
                                    </td>
                                </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
    </body>
    </html>

