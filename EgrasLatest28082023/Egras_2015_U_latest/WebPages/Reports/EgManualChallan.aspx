<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgManualChallan.aspx.cs" Inherits="WebPages_EgManualChallan" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <link href="../../CSS/EgEchallan.css" rel="stylesheet" />
   <%-- <script src="../../js/jscript2.js"></script>
    <script src="../../js/Jscript1.js"></script>--%>
    <link href="../../js/CDNFiles/smoothness/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../js/SweetAlert/sweetalert.css" rel="stylesheet" />
    <script src="../../js/SweetAlert/sweetalert.min.js"></script>


    <script language="javascript" type="text/javascript">
        function myAlert(heading, mycontent) {
            
            swal({
                title: heading,
                text: mycontent,
                button: "Close",
            });
        }

    </script>
    <style>
     

        .sweet-alert button {
            width: 100%;
        }

        .sweet-alert {
            text-align: center;
            width:20%;
            margin-left: -10%;
            height:20%;
        }
            .sweet-alert h2 {
                font-size: 15px;
            }
            .sweet-alert p {
               margin: -30px;
            }
    </style>


    <script language="javascript" type="text/javascript">

         function openPopup() {
            var argObj = window;
            var id = '<%= Session["GrnNumber"] %>';
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/EgEChallanView.aspx/EncryptData") %>',
                data: '{"id":"' + id + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    window.open("../EgAddExtraDetail.aspx?id=" + escape(msg.d), argObj, "dialogWidth:800px; dialogHeight:500px; dialogLeft:252px; dialogTop:120px; center:yes");
                }
            });
         }
        function openOfficePopup() {
            //var xorKey = 13;
            var argObj = window;
            var id = '<%= Session["GrnNumber"] %>';

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/EgEChallanView.aspx/EncryptData") %>',
                data: '{"id":"' + id + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    window.open("../EgMultipleOfficeDetails.aspx?id=" + escape(msg.d), argObj, "dialogWidth:800px; dialogHeight:500px; dialogLeft:252px; dialogTop:120px; center:yes");
                }
            });
        }
        function checkamounttotal() {
            var junkVal = document.getElementById("<%= lblTotalAmount.ClientID %>").innerHTML;
            var x = new Array();
            x = junkVal.split(".");
            var value1 = x[0];
            var value2 = x[1];

            var numberReversed = value1.split("");
            var actnumber = numberReversed.reverse();
            if (Number(value1) >= 0) {
                //do nothing  
            } else {
                alert('Invalid Amount');
                return false;
            }
            if (Number(value1) == 0) {
                document.getElementById("<%= txtamountwords.ClientID %>").value = 'Rupees Zero Only';
                //                return false;
            }
            if (actnumber.length > 14) {
                alert('Oops!!!! the Number is too big to convert');
                return false;

            }

            var iWords = ["Zero", " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine"];
            var ePlace = [
                'Ten', ' Eleven', ' Twelve', ' Thirteen', ' Fourteen', ' Fifteen', ' Sixteen', ' Seventeen', ' Eighteen',
                ' Nineteen'
            ];
            var tensPlace = [
                'dummy', ' Ten', ' Twenty', ' Thirty', ' Forty', ' Fifty', ' Sixty', ' Seventy', ' Eighty', ' Ninety'
            ];

            var iWordsLength = numberReversed.length;
            var inWords = new Array();
            var finalWord = "";
            var j = 0;
            var i;
            for (i = 0; i < iWordsLength; i++) {
                switch (i) {
                    case 0:
                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {
                            inWords[j] = '';
                        } else {
                            inWords[j] = iWords[actnumber[i]];
                        }
                        inWords[j] = inWords[j] + ' Rupees  ';
                        break;
                    case 1:
                        tensComplication();
                        break;
                    case 2:
                        if (actnumber[i] == 0) {
                            inWords[j] = '';
                        } else if (actnumber[i - 1] != 0 && actnumber[i - 2] != 0) {
                            inWords[j] = iWords[actnumber[i]] + ' Hundred and';
                        } else {
                            inWords[j] = iWords[actnumber[i]] + ' Hundred';
                        }
                        break;
                    case 3:
                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {
                            inWords[j] = '';
                        } else {
                            inWords[j] = iWords[actnumber[i]];
                        }
                        if (actnumber[i + 1] != 0 || actnumber[i] > 0) {
                            inWords[j] = inWords[j] + " Thousand";
                        }
                        break;
                    case 4:
                        tensComplication();
                        break;
                    case 5:
                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {
                            inWords[j] = '';
                        } else {
                            inWords[j] = iWords[actnumber[i]];
                        }
                        if (actnumber[i + 1] != 0 || actnumber[i] > 0) {
                            inWords[j] = inWords[j] + " Lakh";
                        }
                        break;
                    case 6:
                        tensComplication();
                        break;
                    case 7:

                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {

                            inWords[j] = '';

                        } else {

                            inWords[j] = iWords[actnumber[i]];

                        }

                        if (actnumber[i + 1] != 0 || actnumber[i] > 0) {

                            inWords[j] = inWords[j] + " Crore";

                        }

                        break;

                    case 8:

                        tensComplication();

                        break;
                    case 9:

                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {

                            inWords[j] = '';

                        } else {

                            inWords[j] = iWords[actnumber[i]];

                        }

                        inWords[j] = inWords[j] + " Arab";

                        break;
                    case 10:

                        tensComplication();

                        break;
                    default:

                        break;

                }

                j++;


            };


            function tensComplication() {
                if (actnumber[i] == 0) {
                    inWords[j] = '';
                } else if (actnumber[i] == 1) {
                    inWords[j] = ePlace[actnumber[i - 1]];
                } else {
                    inWords[j] = tensPlace[actnumber[i]];
                }
            }

            inWords.reverse();
            for (i = 0; i < inWords.length; i++) {
                finalWord += inWords[i];
            }
            if (value2 == undefined) {
                document.getElementById("<%= txtamountwords.ClientID %>").value = finalWord;
            } else {

                numberReversed = value2.split("");
                actnumber = numberReversed.reverse();
                var finalWord1 = "";
                if (Number(value2) >= 0) {
                    //do nothing  
                } else {
                    alert('Invalid Amount');
                    return false;
                }

                if (actnumber.length > 14) {
                    alert('Oops!!!! the Number is too big to converte');
                    return false;
                }

                if (Number(value2) != 0) {
                    iWordsLength = numberReversed.length;
                    inWords = new Array();
                    finalWord1 = "";
                    j = 0;
                    for (i = 0; i < iWordsLength; i++) {
                        switch (i) {
                            case 0:
                                if (actnumber[i] == 0 || actnumber[i + 1] == 1) {
                                    inWords[j] = '';
                                } else {
                                    inWords[j] = iWords[actnumber[i]];
                                }
                                inWords[j] = inWords[j] + ' Paisa Only ';
                                break;
                            case 1:
                                tensComplication();
                                break;
                            default:
                                break;
                        }
                        j++;
                    }

                    inWords.reverse();
                    for (i = 0; i < inWords.length; i++) {
                        finalWord1 += inWords[i];
                    }
                } else {
                    finalWord1 = " Zero Paisa Only";
                }
                document.getElementById('<%= txtamountwords.ClientID %>').value = finalWord + 'and' + finalWord1;
            }
            return false;
        }

    </script>
    <table align="center" border="1" cellpadding="1" cellspacing="1" style="width: 520px;" frame="box" height="700px">
        <tr id="tr" runat="server">
            <td colspan="3">Please install Acrobat Reader from this link : <a id="anchor" href="http://get.adobe.com/reader/">
                <u>Click Here</u></a>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <center>
                    <asp:LinkButton ID="lnkExtraDetails" runat="server" Text="View Extra Details" Visible="false"
                        OnClientClick="openPopup();"></asp:LinkButton>
                    
                </center>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="border-bottom-style: inset; border-bottom-width: thin;">
                <b><span style="color: #a80808;">MANUAL-CHALLAN</span>
                    <br />
                    Government Of Rajasthan
            <br />
                    <asp:Label ID="lblDepartment" runat="server" />
                </b>
            </td>
        </tr>
        <tr style="border-bottom-style: solid; border-bottom-width: thin">
            <td style="width: 149px" colspan="3"><span style="font-size: 15px;">Valid Upto:-</span>
                <asp:Label ID="lblChallanDateValidUpto" runat="server" Style="font-size: 15px;"></asp:Label>
            </td>

        </tr>
        <tr style="border-bottom-style: solid; border-bottom-width: thin">
            <td style="width: 149px">
                <b>GRN :-</b>
                <b>
                    <asp:Label ID="lblGrn" runat="server"></asp:Label>
                </b>
            </td>
            <td style="width: 166px" colspan="2">Date:-
        <asp:Label ID="lblChallanDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="border-bottom-style: solid; border-bottom-width: thin">
            <td style="width: 149px">
                <%--BarCode :-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
        <%--<br />--%>
                Office Name:-&nbsp;&nbsp;&nbsp;<br />
                Location:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
                Year:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            </td>
            <td colspan="2">
                <%--<asp:Image ID="Image2" runat="server" Height="21px" Width="150px" />--%>
                <%--<br />--%>
                <asp:Label ID="lblOfficeName" runat="server"></asp:Label>
                <asp:LinkButton ID="lnkMultipleOfcs" runat="server" Text="View Office Details" Visible="false"
                            OnClientClick="openOfficePopup();"></asp:LinkButton>
                <br />
                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblFromYear" runat="server"></asp:Label>
                To
        <asp:Label ID="lblToYear" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="border-bottom-style: solid; border-bottom-width: thin">
            <td colspan="2" align="left">
                <b>Head(<asp:Label ID="lblMajorHead" runat="server"></asp:Label>)
                </b>
            </td>
            <td align="right" style="width: 166px"><b>Amount(<asp:Image runat="server" ImageUrl="../../Image/rupees.jpg" />)</b></td>
        </tr>
        <tr style="border-bottom-style: solid; border-bottom-width: thin">
            <td colspan="2">
                <asp:Label ID="lblSchema" runat="server"></asp:Label>&nbsp;
            </td>

            <td align="right" style="width: 166px">
                <asp:Label ID="lblSchemaAmount" runat="server"></asp:Label>
            </td>
        </tr>


        <tr style="border-top-style: solid; border-top-width: thin;">
            <td class="auto-style2" style="width: 149px" colspan="2">Discount :-
            </td>

            <td align="right">
                <asp:Label ID="lblDiscount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 149px" colspan="2">Total/Net Amount:-
            </td>

            <td align="right" style="font-weight: 700">
                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="border-bottom-style: solid; border-bottom-width: thin">
            <td colspan="3" style="font-size: large;">
                <asp:TextBox ID="txtamountwords" runat="server" BorderWidth="0" Width="500px" TextMode="MultiLine"
                    Font-Bold="true" ForeColor="Green" Height="30px" ReadOnly="True" Style="text-align: left; overflow: hidden; font-size: 15px;">
                </asp:TextBox>
            </td>

        </tr>
        <tr>
            <td colspan="3" runat="server" id="trDdo" visible="False">
                <b>
                    <asp:Label ID="lblDdo" Text="DDO:-" runat="server" Visible="False"></asp:Label>
                </b>
                <b>
                    <asp:Label ID="lblDdoname" runat="server" Visible="False"></asp:Label>
                </b>
            </td>
        </tr>
        <tr runat="server" id="trPdacc" visible="False">
            <td colspan="3">
                <b>
                    <asp:Label ID="lblPdacc" runat="server" Visible="False"></asp:Label>
                </b>
            </td>
        </tr>

        <tr style="border-top-style: solid; border-bottom-style: solid; border-top-width: thin; border-bottom-width: thin">
            <td colspan="3" align="center">
                <b>Payee Detail</b>
            </td>

        </tr>
        <tr style="border-top-style: solid; border-top-width: thin">
            <td style="width: 149px">TIN/Actt. no./VehicleNo/TaxId/Lease No:-
            </td>
            <td colspan="2">
                <asp:Label ID="lbltin" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 149px">PAN No.:-
            </td>
            <td colspan="2">
                <asp:Label ID="lblPan" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 149px">Remitter Name:-
            </td>
            <td colspan="2">
                <asp:Label ID="lblRemitter" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 149px">Address:-
            </td>
            <td colspan="2">
                <asp:Label ID="lblAddress" runat="server"></asp:Label>
            </td>

        </tr>
        <tr>
            <td style="width: 149px">Remarks:
            </td>
            <td colspan="2">
                <asp:Label ID="lblRemarks" runat="server"></asp:Label>
            </td>

        </tr>
        <tr runat="server" id="trFv" visible="False">
            <td style="width: 149px">
                <asp:Label ID="lblF1" runat="server" Visible="False"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblV3" runat="server" Visible="False"></asp:Label>
            </td>

        </tr>
        <tr style="border-top-style: solid; border-bottom-style: solid; border-top-width: thin; border-bottom-width: thin">
            <td colspan="3" align="center">
                <b>FOR USE IN RECEIVING BANK</b>
            </td>
        </tr>
        <tr>
            <td style="width: 149px">Cheque-DD-No.:-
            </td>
            <td colspan="2">
                <asp:Label ID="lblChequeNo" runat="server"></asp:Label>
            </td>

        </tr>
        <tr>
            <td style="width: 149px">Bank CIN No:-
            </td>
            <td colspan="2">
                <asp:Label ID="lblCin" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 149px">Bank:-
            </td>
            <td colspan="2">
                <asp:Label ID="lblBank" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" style="color: #d80606;">
                <asp:ImageButton runat="server" ImageAlign="Right" ImageUrl="../../Image/download.png" ID="imageButtonPdf" OnClick="imageButtonPdf_Click" />
                <asp:ImageButton runat="server" ImageAlign="Right" ImageUrl="../../Image/download.png" ID="btnshow" OnClick="btnshow_Click" />
                
                 <%--<asp:Button ID="btnshow" runat="server" Text="Download Receipt" OnClick="btnshow_Click" visible="false"/>--%>
                 </td>
        </tr>

    </table>
    <asp:HiddenField runat="server" ID="hdnBankCode" />
    <rsweb:ReportViewer ID="SSRSreport" runat="server" Width="100%" SizeToReportContent="true"
        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false" Visible="False">
    </rsweb:ReportViewer>
    <rsweb:ReportViewer ID="rptGoodsAndServices" runat="server" Width="100%" SizeToReportContent="true"
        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false" Visible="false">
    </rsweb:ReportViewer>
</asp:Content>
