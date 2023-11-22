<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YearlyChallanWiseChart.aspx.cs" Inherits="YearlyChallanWiseChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<%--<link href="CSS/style.css" rel="stylesheet" />--%>
<link href="CSS/style.css" rel="stylesheet" type="text/css" />
<script src="js/amcharts.js" type="text/javascript"></script>
<link href="CSS/bootstrap.min.css" rel="stylesheet" />
<script src="js/Control.js" type="text/javascript"></script>
<link href="CSS/PageHeader.css" rel="stylesheet" />

<style type="text/css">
    #cover-spin {
        position: fixed;
        width: 100%;
        left: 0;
        right: 0;
        top: 0;
        bottom: 0;
        background-color: rgba(255,255,255,0.7);
        z-index: 9999;
        display: none;
    }

    @-webkit-keyframes spin {
        from {
            -webkit-transform: rotate(0deg);
        }

        to {
            -webkit-transform: rotate(360deg);
        }
    }

    @keyframes spin {
        from {
            transform: rotate(0deg);
        }

        to {
            transform: rotate(360deg);
        }
    }

    #cover-spin::after {
        content: '';
        display: block;
        position: absolute;
        left: 48%;
        top: 40%;
        width: 40px;
        height: 40px;
        border-style: solid;
        border-color: black;
        border-top-color: transparent;
        border-width: 4px;
        border-radius: 50%;
        -webkit-animation: spin .8s linear infinite;
        animation: spin .8s linear infinite;
    }

    .btn-default {
        color: #f4f4f4;
        background-color: #428bca;
    }
</style>
<body>
    <form id="form1" runat="server">
        <div>
            <%--<div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Calender Year Wise OR Financial Year Wise Report</span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Bank Scroll Report (45-A)" />
            </div>--%>
            <table border="1" width="100%" cellpadding="0" cellspacing="0">
                <tr id="tr2" runat="server">
                    <td>
                        <%--<fieldset id="fldPie" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em; border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em; padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888; behavior: url(../PIE.htc); text-align: center;">
                            <legend style="color: Green;">Table</legend>
                            <div id="divDERev" style="width: 500px; height: 350px; background-color: white;">
                                <table style='font-weight: bold; margin-left: 70px;' border="1" cellpadding="0" cellspacing="0">
                                    <asp:Repeater ID="rept" runat="server" OnItemDataBound="rept_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr style='background-color: #3BB9FF;'>
                                                <td>Sno.
                                                </td>
                                                <td>Month
                                                </td>
                                                <td>Transactions
                                                </td>
                                                <td>Amount
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("SN")%>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%#Eval("Year") %>
                                                </td>
                                                <td style="text-align: right;">
                                                    <%#Eval("NoTransaction")%>
                                                </td>
                                                <td style="text-align: right;">
                                                    <%#string.Format("{0:0.00}",Eval("Amount"))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("SN")%>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%#Eval("Year") %>
                                                </td>
                                                <td style="text-align: right;">
                                                    <%#Eval("NoTransaction")%>
                                                </td>
                                                <td style="text-align: right;">
                                                    <%#string.Format("{0:0.00}", Eval("Amount"))%>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            <tr style='background-color: #3BB9FF;'>
                                                <td colspan="2" style="text-align: right;">
                                                    <b>Total:</b>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lbltotalTrans" runat="server"></asp:Label>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </fieldset>--%>
                    </td>
                    <td>
                        <fieldset id="fldColumn"  runat="server" visible="false" style="width: 700px;color: Green; border-top-left-radius: 0.5em 0.5em; border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em; padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888; behavior: url(../PIE.htc);">
                            <%--<legend style="color: Green;">Column Chart</legend>--%>
                            <div id="divDERevCol" style="width: 700px; height: 350px; background-color: white;">
                                <asp:Literal ID="ltRevCol" runat="server"></asp:Literal>
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
