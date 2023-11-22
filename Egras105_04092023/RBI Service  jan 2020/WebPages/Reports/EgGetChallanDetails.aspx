<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgGetChallanDetails.aspx.cs" Inherits="WebPages_EgGetChallanDetails"
    EnableEventValidation="false" Title="Challan Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font-size: 9pt;
            font-family: Verdana;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function NumberOnly(el) {
            var ex = /^\s*\d+\s*$/;
            if (ex.test(el.value) == false) {
                alert('Incorrect Number');
                el.value = "";
            }
        }
        function DecimalNumber(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (el.value != "") {
                if (ex.test(el.value) == false) {
                    alert('Incorrect Amount');
                    el.value = "";
                }
            }
        }
    </script>

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
    </style>

    <div id="cover-spin"></div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnSubmit"]').click(function () {
                if ($(<%=txtfromdate.ClientID%>).val() != "" && $(<%=txttodate.ClientID%>).val() != "") {
                    $('#cover-spin').show(0)

                }
            });
        });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail">
            <span _ngcontent-c6="" style="color: #FFF">Challan Detail</span></h2>
        <img src="../../Image/help1.png" style="height: 44px; width: 34px;" title="Challan Detail" />
    </div>
    <table id="tblheader" border="1" align="center" width="100%" cellpadding="1" cellspacing="1">
        <%--  <tr>
            <td colspan="4" style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="Challan Details" Font-Bold="True"
                    ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td style="text-align: left" colspan="1">GRN
            </td>
            <td style="text-align: left" colspan="3">
                <asp:TextBox ID="txtGRN" runat="server" onkeyup="NumberOnly(this);"
                    MaxLength="12"></asp:TextBox>
            </td>
        </tr>
        <tr id="trDate" runat="server">
            <td align="left">From Date : &nbsp;&nbsp;&nbsp;
            </td>
            <td align="left">
                <asp:TextBox ID="txtfromdate" runat="server"
                    onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
            </td>
            <td align="left">&nbsp;To Date : &nbsp;&nbsp;
            </td>
            <td align="left">
                <asp:TextBox ID="txttodate" runat="server"
                    onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txttodate" TargetControlID="txttodate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="a" Text="Search" OnClick="btnSubmit_Click" />
            </td>
        </tr>
        <tr id="rowLabel" runat="server" visible="false">
            <td align="center" colspan="4" style="height: 16px">
                <asp:Label ID="lblEmptyData" runat="server" Text="" ForeColor="green" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <%--<tr id="RowSearch" runat="server" visible="false">
            <td colspan="1" align="left">
                <asp:Label ID="LabelSearch" runat="server" Text="Search in Records : " ForeColor="#005CB8"
                    Font-Bold="true"></asp:Label>
            </td>
            <td colspan="3" align="left">
                <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged"
                    AutoPostBack="true" MaxLength="30" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelMatch" runat="server" Text="" Visible="false" ForeColor="green"
                    Font-Bold="true"></asp:Label>
            </td>
        </tr>--%>
        <%-- paging on Repeater--%>
        <tr id="paging" runat="server">
            <td colspan="4">
                <center>
                    <%--<asp:LinkButton ID="lnk_first" runat="server" Text="<< First " Visible="false" Font-Bold="true"
                        Font-Size="Small" OnClick="lnk_first_Click"></asp:LinkButton>
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkpre" runat="server" Text="<< Previous " Visible="false" Font-Bold="true"
                        Font-Size="Small" OnClick="lnkpre_Click"></asp:LinkButton>
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnknext" runat="server" Text=" Next >>" Font-Bold="true" Font-Size="Small"
                        Visible="false" OnClick="lnknext_Click"></asp:LinkButton>
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnk_last" runat="server" Text=" Last>>" Font-Bold="true" Font-Size="Small"
                        Visible="false" OnClick="lnk_last_Click"></asp:LinkButton>--%>
                    <asp:Label ID="lblCurrentPage" runat="server" Text="" ForeColor="green" Width="95%"></asp:Label>
                    <asp:Label ID="lblrecord" runat="server" ForeColor="green" Width="95%" Font-Bold="true"></asp:Label>
                </center>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table style="background-color: #333333; font-weight: normal;"
                    cellspacing="2" cellpadding="2" width="100%" align="center" border="1">
                    <asp:Repeater ID="rptChallanFill" runat="server" OnItemDataBound="rptChallanFill_ItemDataBound"
                        OnItemCommand="rptChallanFill_ItemCommand">
                        <HeaderTemplate>
                            <tr style="background-color: #336699; color: White;">
                                 <td align="center">S.No
                                </td>
                                <td align="center">GRN
                                </td>
                                <td align="center">Remitter Name
                                </td>
                                <td align="center">Bank Name
                                </td>
                                <td align="center" style="width: 100px;">Bank Date
                                </td>
                                <td align="center">Amount
                                </td>
                                <td align="center" style="width: 120px;">Payment Type
                                </td>
                                <td align="center">Status
                                </td>
                                <td id="hdrRepeat" runat="server" align="center">
                                    <asp:Label ID="lblrpt" runat="server" Text="Repeat"></asp:Label>
                                </td>
                                <td align="center">View
                                </td>
                                <td align="center">Verify
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: white; height: 20px; font-family: Verdana; font-size: 10pt;">
                                <td align="center">
                                    <asp:Label ID="Labelrow" runat="server" Text='<%#Eval("row")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="LabelGRN" runat="server" Text='<%#Eval("GRN")%>'></asp:Label>
                                </td>
                                <td align="left">
                                    <%#DataBinder.Eval(Container, "DataItem.RemitterName")%>
                                </td>
                                <td align="left">
                                    <%#DataBinder.Eval(Container, "DataItem.BankName")%>
                                </td>
                                <td align="center">
                                    <%#DataBinder.Eval(Container, "DataItem.Bankdate")%>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# string.Format("{0:0.00}", Eval("TotalAmount"))%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblPaymentType" runat="server" Text='<%#Eval("Paymenttype")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                </td>
                                <td id="tdrepeat" runat="server" align="center">
                                    <asp:LinkButton ID="lnkrepeat" runat="server" CausesValidation="false" CommandName="Repeat"
                                        CommandArgument='<%#Eval("Grn")%>' Text="Repeat"></asp:LinkButton>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="ImageViewbtn" runat="server" ImageUrl="~/Image/view.png" CommandName="View"
                                        Width="60px" Height="20" ToolTip="View Details" />
                                    <asp:ImageButton ID="imgPrintbtn" runat="server" ImageUrl="~/Image/printer.jpg" CommandName="print"
                                        Height="25" ToolTip="Print"></asp:ImageButton>
                                </td>

                                <td id="tdverify" runat="server" align="center">
                                    <asp:LinkButton ID="LinkVerify" runat="server" CausesValidation="false" CommandName="Verify" ForeColor="Green"
                                        ToolTip='<%#Eval("BankCode")%>' Text="Verify"></asp:LinkButton>
                                    <asp:HiddenField ID="hdnverify" runat="server" Value='<%#Eval("VerifyStatus")%>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr id="trPage" runat="server" visible="false">
                        <td colspan="11" style="font-size: 15px; background-color: #336699; color: White; text-align: center;">
                            <asp:Repeater ID="rptPager" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton Style="background-color: #336699; color: White;" ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled"))==true ? "page_enabled" : "page_disabled" %>'
                                        OnClick="Page_Changed"></asp:LinkButton>
                                </ItemTemplate>

                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
