<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgYearwiseChart.aspx.cs" Inherits="WebPages_charts_EgYearwiseChart"
    Title="YearwiseChart" %>

<%@ Register Src="~/UserControls/FinYearDropdown.ascx" TagName="FinYear" TagPrefix="ucl" %>
<script runat="server">


</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="../../js/amcharts.js" type="text/javascript"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <%--==================CSS-JQUERY LOADER==================--%>
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

    <div id="cover-spin"></div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnShow"]').click(function () {
                $('#cover-spin').show(0)
            });
        });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>


    <%-- Radio button ServerSide --%>
    <style>
        input[name="ctl00$ContentPlaceHolder1$rdbYear"] {
            display: none;
        }

        label {
            border: 2px solid cornflowerblue;
            display: inline-block;
            padding: 5px;
            position: relative;
            text-align: center;
            transition: background 600ms ease, color 600ms ease;
        }

        input[name="ctl00$ContentPlaceHolder1$rdbYear"] + label {
            cursor: pointer;
            min-width: 205px;
        }

            input[name="ctl00$ContentPlaceHolder1$rdbYear"] + label:hover {
                background: none;
                color: #1a1a1a;
            }

            input[name="ctl00$ContentPlaceHolder1$rdbYear"] + label:after {
                background: cornflowerblue;
                content: "";
                height: 100%;
                position: absolute;
                top: 0;
                transition: left 200ms cubic-bezier(0.77, 0, 0.175, 1);
                width: 100%;
                z-index: -1;
                left: 100%;
            }

        label {
            margin-bottom: 2px;
        }

        input[id="ctl00_ContentPlaceHolder1_rdbYear_0"] + label {
            border-right: 0;
        }

        input[name="ctl00_ContentPlaceHolder1_rdbYear_0"] + label:after {
            left: 100%;
        }

        input[id="ctl00_ContentPlaceHolder1_rdbYear_1"] + label {
            margin-left: -100px;
        }

            input[id="ctl00_ContentPlaceHolder1_rdbYear_1"] + label:after {
                left: -100%;
            }

        input[name="ctl00$ContentPlaceHolder1$rdbYear"]:checked + label {
            cursor: default;
            color: #fff;
            transition: color 200ms;
        }

            input[name="ctl00$ContentPlaceHolder1$rdbYear"]:checked + label:after {
                left: 0;
            }

        /*label {
            border: 2px solid cornflowerblue;
            display: inline-block;
            padding: 5px;
            position: relative;
            text-align: center;
            transition: background 600ms ease, color 600ms ease;
        }*/

        /*input[name="ctl00$ContentPlaceHolder1$RadioButtonList1"] {
            display: none;
        }


            input[name="ctl00$ContentPlaceHolder1$RadioButtonList1"] + label {
                cursor: pointer;
                min-width: 45px;
            }

                input[name="ctl00$ContentPlaceHolder1$RadioButtonList1"] + label:hover {
                    background: none;
                    color: #1a1a1a;
                }

                input[name="ctl00$ContentPlaceHolder1$RadioButtonList1"] + label:after {
                    background: cornflowerblue;
                    content: "";
                    height: 100%;
                    position: absolute;
                    top: 0;
                    transition: left 200ms cubic-bezier(0.77, 0, 0.175, 1);
                    width: 100%;
                    z-index: -1;
                    left: 100%;
                }

        input[id="ctl00_ContentPlaceHolder1_RadioButtonList1_0"] + label {
            border-right: 0;
        }

        input[name="ctl00_ContentPlaceHolder1_RadioButtonList1_0"] + label:after {
            left: 100%;
        }

        input[id="ctl00_ContentPlaceHolder1_RadioButtonList1_1"] + label {
            margin-left: -5px;
        }

            input[id="ctl00_ContentPlaceHolder1_RadioButtonList1_1"] + label:after {
                left: -100%;
            }

        input[name="ctl00$ContentPlaceHolder1$RadioButtonList1"]:checked + label {
            cursor: default;
            color: #fff;
            transition: color 200ms;
        }

            input[name="ctl00$ContentPlaceHolder1$RadioButtonList1"]:checked + label:after {
                left: 0;
            }*/
    </style>
    <%-- Radio button ServerSide end --%>


    <%--<fieldset id="fldYear" runat="server" style="color: Green; border-top-left-radius: 0.5em 0.5em; border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888; behavior: url(../PIE.htc); text-align: center;">
        <legend style="color: Green;">
            <h4>Calender Year Wise OR Financial Year Wise Report</h4>
        </legend>--%>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Calender Year Wise OR Financial Year Wise Report</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Bank Scroll Report (45-A)" />
    </div>

    <table border="1" width="100%" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td colspan="2">
                <asp:RadioButtonList ID="rdbYear" runat="server" RepeatDirection="Horizontal" Width="459px"
                    Font-Bold="true">
                    <asp:ListItem Selected="True" Text="Calender Year Wise" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Financial Year Wise" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr align="center">
            <td>
                <b><span style="color: #336699">Year : </span></b>&nbsp;
                <ucl:FinYear ID="ddlYear" runat="server" class="form-control chzn-select" width="220px" />
                <%--<asp:DropDownList ID="ddlYear" runat="server" Width="200px">
                        <asp:ListItem Selected="True" Text="2012" Value="2012"></asp:ListItem>
                        <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                    </asp:DropDownList>--%>
            </td>
            <td>
                <asp:Button ID="btnShow" runat="server" Height="33" style="margin-right:15px" CssClass="btn btn-default" Text="Show" OnClick="btnShow_Click" />
                <asp:Button ID="btnReset" runat="server" Height="33" CssClass="btn btn-default" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />
            </td>
        </tr>
        <tr id="tr1" runat="server">
            <td colspan="2">
                <center>
                    <asp:Label ID="lblmsg" runat="server" Text="No Record Found !!" Visible="false"></asp:Label>
                </center>
            </td>
        </tr>
        <tr id="tr2" runat="server">
            <td>
                <fieldset id="fldPie" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em; border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em; padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888; behavior: url(../PIE.htc); text-align: center;">
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
                                            <%#Eval("rowno")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%#Eval("monthN") %>
                                        </td>
                                        <td style="text-align: right;">
                                            <%#Eval("totChallan")%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%#string.Format("{0:0.00}",Eval("Amount"))%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("rowno")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%#Eval("monthN") %>
                                        </td>
                                        <td style="text-align: right;">
                                            <%#Eval("totChallan")%>
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
                </fieldset>
            </td>
            <td>
                <fieldset id="fldColumn" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em; border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em; padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888; behavior: url(../PIE.htc);">
                    <legend style="color: Green;">Column Chart</legend>
                    <div id="divDERevCol" style="width: 500px; height: 350px; background-color: white;">
                        <asp:Literal ID="ltRevCol" runat="server"></asp:Literal>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
    <%--</fieldset>--%>
</asp:Content>
