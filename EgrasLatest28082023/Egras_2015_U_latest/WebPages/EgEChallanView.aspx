<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgEChallanView.aspx.cs" Inherits="WebPages_EgEChallanView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/EgEchallan.css" rel="stylesheet" type="text/css" />


    <%--  <script type="text/javascript" src="../js/Jscript1.js"></script>
    <script type="text/javascript" src="../js/jscript2.js"></script>--%>
    <%--<link rel="Stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />--%>
    <link href="../js/CDNFiles/smoothness/jquery-ui.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function openPopup() {
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
                    window.open("EgAddExtraDetail.aspx?id=" + escape(msg.d), argObj, "dialogWidth:800px; dialogHeight:500px; dialogLeft:252px; dialogTop:120px; center:yes");
                }
            });
        }





        function StampPopup() {

            $("#DivPopupSche").html();
            $("#DivPopupSche").dialog({
                minWidth: 650,
                minHeight: 350,
                title: "Stamp Commission Case",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
        }


        // Proc Challan display Function 

        function ProcPopup() {

            $("#DivProcChallan").html();
            $("#DivProcChallan").dialog({
                minWidth: 150,
                minHeight: 150,
                title: "Procurement Challan",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
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
                    window.open("EgMultipleOfficeDetails.aspx?id=" + escape(msg.d), argObj, "dialogWidth:800px; dialogHeight:500px; dialogLeft:252px; dialogTop:120px; center:yes");
                }
            });
        }

        function ShowGrn() {

            if (document.getElementById("<%= ddlBank.ClientID %>").selectedIndex == "0") {
                alert('Please Select Bank from list.');
            }
            else {
                var fromDate = document.getElementById("<%= frommonthlbl.ClientID %>").innerHTML;
                var toDate = document.getElementById("<%= tomonthlbl.ClientID %>").innerHTML;

                var retVal = confirm("Selected Period  " + fromDate + "  To  " + toDate + "   Please verify the details you have entered. Do you want to continue?");

                if (retVal) {

                    var grn = document.getElementById("<%= Gennolbl.ClientID %>").innerHTML;

                    alert('Your GRN Number is: ' + grn);
                    return true;
                } else {
                    return false;
                }
            }
        }


        function checkamounttotal(value) {


            var junkVal = document.getElementById("<%= NetAmountlbl.ClientID %>").innerHTML;

            var x = new Array();
            x = junkVal.split(".");
            var value1 = x[0];
            var value2 = x[1];


            var obStr = new String(value1);

            var numberReversed = value1.split("");

            actnumber = numberReversed.reverse();



            if (Number(value1) >= 0) {

                //do nothing  

            }

            else {

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

            var ePlace = ['Ten', ' Eleven', ' Twelve', ' Thirteen', ' Fourteen', ' Fifteen', ' Sixteen', ' Seventeen', ' Eighteen', ' Nineteen'];

            var tensPlace = ['dummy', ' Ten', ' Twenty', ' Thirty', ' Forty', ' Fifty', ' Sixty', ' Seventy', ' Eighty', ' Ninety'];



            var iWordsLength = numberReversed.length;

            var totalWords = "";

            var inWords = new Array();

            var finalWord = "";

            j = 0;

            for (i = 0; i < iWordsLength; i++) {

                switch (i) {

                    case 0:

                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {

                            inWords[j] = '';

                        }

                        else {

                            inWords[j] = iWords[actnumber[i]];

                        }

                        inWords[j] = inWords[j] + ' Rupees  ';

                        break;

                    case 1:

                        tens_complication();

                        break;

                    case 2:

                        if (actnumber[i] == 0) {

                            inWords[j] = '';

                        }

                        else if (actnumber[i - 1] != 0 && actnumber[i - 2] != 0) {

                            inWords[j] = iWords[actnumber[i]] + ' Hundred and';

                        }

                        else {

                            inWords[j] = iWords[actnumber[i]] + ' Hundred';

                        }

                        break;

                    case 3:

                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {

                            inWords[j] = '';

                        }

                        else {

                            inWords[j] = iWords[actnumber[i]];

                        }

                        if (actnumber[i + 1] != 0 || actnumber[i] > 0) {

                            inWords[j] = inWords[j] + " Thousand";

                        }

                        break;

                    case 4:

                        tens_complication();

                        break;

                    case 5:

                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {

                            inWords[j] = '';

                        }

                        else {

                            inWords[j] = iWords[actnumber[i]];

                        }

                        if (actnumber[i + 1] != 0 || actnumber[i] > 0) {

                            inWords[j] = inWords[j] + " Lakh";

                        }

                        break;

                    case 6:

                        tens_complication();

                        break;

                    case 7:

                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {

                            inWords[j] = '';

                        }

                        else {

                            inWords[j] = iWords[actnumber[i]];

                        }

                        if (actnumber[i + 1] != 0 || actnumber[i] > 0) {

                            inWords[j] = inWords[j] + " Crore";

                        }

                        break;

                    case 8:

                        tens_complication();

                        break;
                    case 9:

                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {

                            inWords[j] = '';

                        }

                        else {

                            inWords[j] = iWords[actnumber[i]];

                        }

                        inWords[j] = inWords[j] + " Arab";

                        break;
                    case 10:

                        tens_complication();

                        break;
                    default:

                        break;

                }

                j++;


            };



            function tens_complication() {

                if (actnumber[i] == 0) {

                    inWords[j] = '';

                }

                else if (actnumber[i] == 1) {

                    inWords[j] = ePlace[actnumber[i - 1]];

                }

                else {

                    inWords[j] = tensPlace[actnumber[i]];

                }

            }

            inWords.reverse();

            for (i = 0; i < inWords.length; i++) {

                finalWord += inWords[i];

            }

            if (value2 == undefined) {
                document.getElementById("<%= txtamountwords.ClientID %>").value = finalWord;
            }
            else {



                var obStr = new String(value2);

                var numberReversed = value2.split("");

                actnumber = numberReversed.reverse();
                var finalWord1 = "";


                if (Number(value2) >= 0) {

                    //do nothing  

                }

                else {

                    alert('Invalid Amount');

                    return false;

                }



                if (actnumber.length > 14) {

                    alert('Oops!!!! the Number is too big to converte');

                    return false;

                }

                if (Number(value2) != 0) {

                    var iWordsLength = numberReversed.length;

                    var totalWords = "";

                    var inWords = new Array();

                    var finalWord1 = "";

                    j = 0;

                    for (i = 0; i < iWordsLength; i++) {

                        switch (i) {

                            case 0:

                                if (actnumber[i] == 0 || actnumber[i + 1] == 1) {

                                    inWords[j] = '';

                                }

                                else {

                                    inWords[j] = iWords[actnumber[i]];

                                }

                                inWords[j] = inWords[j] + ' Paisa Only ';

                                break;

                            case 1:

                                tens_complication();

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
                }
                else {
                    finalWord1 = " Zero Paisa Only"
                }

                document.getElementById("<%= txtamountwords.ClientID %>").value = finalWord + 'and' + finalWord1;



            }

        }
        function ClickToPrint() {
            docPrint = window.open("", "mywindow", "location=1,status=1,scrollbars=1,width=1000,height=500");
            docPrint.document.open();
            docPrint.document.write('<html><head><title>ChallanPage</title>');
            docPrint.document.write('</head><body onLoad="self.print()"><left>');
            docPrint.document.write('</Center><br/><table width="1030px" height="50%" top="0"  border=0 font Size="8"><tr><td width="150%"><left><font face="Small Fonts">');
            docPrint.document.write(document.getElementById("divChallanView").innerHTML);
            docPrint.document.write('</td></tr></table></left></font></body></html>');
            docPrint.document.close();

        }

    </script>
    <style>
        .ui-dialog .ui-dialog-title {
            text-align: center;
            width: 100%;
        }

        .ui-widget-content {
            background: #70B8E5;
            /*color: #FFF;*/
        }

        .ui-widget-header {
            background: #FFF;
            color: #000;
        }

            .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default {
                background: #FFF;
                color: #000;
            }

        @media (max-width:1000px) and (min-width:600px) {
            #ctl00_ContentPlaceHolder1_TABLE1 {
                margin-left: 25px !important;
                margin-right: 25px !important;
            }
        }


        @media (max-width:599px) and (min-width:250px) {
            #ctl00_ContentPlaceHolder1_TABLE1 {
                margin-left: 1px !important;
                margin-right: 1px !important;
            }
        }


        .equal {
            display: flex;
            flex-wrap: wrap;
        }

            .equal > div[class*='col-'] {
                display: flex;
                flex-direction: column;
            }
    </style>
    <div id="divChallanView">
        <table runat="server" style="width: 100%; text-align: left" id="TABLE2" cellpadding="0"
            border="0" cellspacing="0" align="center">
            <tr style="text-align: center">
                <td style="height: 16px" valign="top">
                    <asp:Label ID="Label1" runat="server" Text="E-CHALLAN" Font-Bold="True" ForeColor="#009900"></asp:Label>
                </td>
            </tr>
            <tr style="text-align: center">
                <td style="height: 16px" valign="top">
                    <asp:Label ID="Label2" runat="server" Text="Government of Rajasthan" Font-Bold="True"
                        ForeColor="#009900"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="DivPopupSche" style="display: none">
            वर्तमान में निम्न नॉन ज्युडिशियल बजट मदों के साथ दो पृथक-पृथक अधिभार वसूल किए जा रहे है :-
                    <br />
            1.	00300-02-102-(01) - विनिमय पत्र एवं हुण्डियां
                    <br />
            2.	00300-02-102-(02) – अन्य गैर अदालती स्टाम्प
                    <br />
            3.	00300-02-102-(03) – अन्य स्टाम्प 
                    <br />
            4.	00300-02-102-(05) – न्यायिकेतर स्टाम्पो के फ्रैकिंग हेतु जमा की गई आय
                    <br />
            5.	00300-02-102-(06) – ई-स्टापिंग हेतु जमा की गई आय
                    <br />
            00300-02-103-(01) – दस्तावेजों पर स्टाम्प शुल्क लगाना
                    <br />
            ये दोनों अधिभार स्वत: ही मुद्रित होकर तथा इनकी मुद्रांक कर देय 10-10 प्रत्तिशत राशि भी स्वत: ही मुद्रित हो जिससे उक्त तीनो बजट मद एवं उनकी सम्पूर्ण राशि जमा कराये जाने पर ही बैंक द्वारा मैनुअल अथवा इलेक्ट्रोनिक चालान स्वीकार किए जावें – "
                    <br />
            1.	00300-02-800-(03) – स्टाम्प शुल्क पर अधिभार
                    <br />
            2.	00300-02-800-(03) – स्टाम्प शुल्क गौ-संवर्धन / संरक्षण हेतु अधिभार
                    <br />
        </div>


        <div id="divshadow">
            <table runat="server" id="TABLE1" cellpadding="0" class="tablestyle" border="1" cellspacing="0"
                align="center">
                <tr>
                    <td style="height: 36px;" colspan="3">
                        <asp:Label ID="lblGenNo" runat="server" Text="GRN" Font-Bold="true"></asp:Label>
                        &nbsp;:-&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Gennolbl" runat="server" Text="Label"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;
                        <%--<asp:Label ID="lblform" runat="server" Text="BARCODE" Font-Bold="true"></asp:Label>--%>
                        <%-- &nbsp;:-&nbsp;&nbsp;--%>
                        <%--<asp:Image ID="Image2" runat="server" Height="21px" Width="150px" />--%>
                    </td>
                    <%--<td style="width: 160px; height: 36px;">
                        <asp:Label ID="lbldformdate" runat="server" Text="Date"></asp:Label>
                        :&nbsp;
                        <asp:Label ID="formdatelbl" runat="server" Text="Label"></asp:Label>
                    </td>--%>
                </tr>
                <tr>
                    <td style="height: 37px; width: 134px;">
                        <asp:Label ID="lblDepartment" runat="server" Text="Profile Name"></asp:Label>
                    </td>
                    <td style="height: 37px; width: 345px; margin-left: 40px;">&nbsp;
                        <asp:Label ID="Departmentlbl" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td style="height: 37px; text-align: center" colspan="2">
                        <asp:Label ID="lblpaydetail" runat="server" Text="Payee Details" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 37px; width: 134px;">Type Of Payment
                    </td>
                    <td style="height: 37px; width: 345px;">&nbsp;<asp:Label ID="TypeOfPaymentlbl" runat="server" Text="Label"></asp:Label>&nbsp;
                    </td>
                    <td style="height: 37px; width: 134px;">
                        <asp:Label ID="lblTin" runat="server" Text="TIN/Actt.No./VehicleNo./Taxid(If Any)"></asp:Label>
                    </td>
                    <td style="height: 37px" colspan="2">&nbsp;<asp:Label ID="Tinlbl" runat="server" Text="Label"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 36px; width: 134px;">Office Name
                    </td>
                    <td style="height: 36px; width: 345px;">&nbsp;
                        <asp:Label ID="OfficeNamelbl" runat="server" Text="Label"></asp:Label>
                        <asp:LinkButton ID="lnkMultipleOfcs" runat="server" Text="View Office Details" Visible="false"
                            OnClientClick="openOfficePopup();"></asp:LinkButton>
                    </td>
                    <td style="height: 36px; width: 134px;">PAN No.(If Applicable)
                    </td>
                    <td style="height: 36px" colspan="2">&nbsp;<asp:Label ID="PanNolbl" runat="server" Text="Label"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px; width: 134px;">Location
                    </td>
                    <td style="height: 25px; width: 345px;">&nbsp;<asp:Label ID="Locationlbl" runat="server" Text="Label"></asp:Label>&nbsp;
                    </td>
                    <td style="height: 25px; width: 134px;">Full Name
                    </td>
                    <td style="height: 25px" colspan="2">&nbsp;<asp:Label ID="fullnamelbl" runat="server" Text="Label"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px; width: 134px;">Year
                        &nbsp;&nbsp;&nbsp;(Period)&nbsp;
                    </td>
                    <td style="height: 25px; width: 345px;">&nbsp;<asp:Label ID="frommonthlbl" runat="server" Text="Label"></asp:Label>&nbsp;-To-
                        <asp:Label ID="tomonthlbl" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td style="height: 25px">Address
                    </td>
                    <td style="height: 25px" colspan="2">&nbsp;<asp:Label ID="addresslbl" runat="server" Text="Label" wrap="true"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="3">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="480px"
                            Style="margin-top: 8px">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Schemaname" HeaderText="Budget Head/Purpose">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="45%" Wrap="true" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="45%" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Amount" HeaderText="Amount in Rs." DataFormatString="{0:n}">
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70%" Wrap="true" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="70%" Wrap="true" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td style="height: 25px">Town/City/District
                    </td>
                    <td style="height: 25px" colspan="2">&nbsp;<asp:Label ID="Townlbl" runat="server" Text="Label"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px">PIN
                    </td>
                    <td style="height: 25px" colspan="3">&nbsp;<asp:Label ID="Pinlbl" runat="server" Text="Label"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px" rowspan="2">Remarks(If Any)
                    </td>
                    <td style="height: 25px" colspan="3" rowspan="2">
                        <asp:Label ID="Remarklbl" runat="server" Text="Label" wrap="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 37px; width: 134px;">Deduct:Commission
                    </td>
                    <td style="height: 37px; width: 345px;" align="right">
                        <asp:Label ID="deductcommisiionlbl" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 35px; width: 140px;">Total/NetAmount
                    </td>
                    <td style="height: 35px; width: 345px;" align="right">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Image/rupees.jpg" />
                        <asp:Label ID="NetAmountlbl" runat="server" Style="font-weight: bold" Text="Label"></asp:Label>
                    </td>
                    <td style="height: 35px" colspan="2">
                        <asp:Label ID="Amountwordslbl" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="txtamountwords" runat="server" BorderWidth="0" Width="500px" TextMode="MultiLine"
                            Font-Bold="true" ForeColor="Green" Height="30px" ReadOnly="True" Style="text-align: left; overflow: hidden;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div id="divBankTransfer" runat="server">
                            <table id="Table4" runat="server" style="width: 100%; text-align: left" cellpadding="0"
                                cddellspacing="0" cellspacing="0">
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lblmsg" runat="server" Style="font-size: 15px; color: #20b0ff; font-weight: bold;"></asp:Label>
                                    </td>
                                    <td align="right" colspan="3">
                                        <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Bank"
                                            ControlToValidate="ddlBank" ForeColor="Red" InitialValue="0" ValidationGroup="a"
                                            Display="Static"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnGo" runat="server" Text="Continue" Visible="true" OnClick="btnGo_Click"
                                            ForeColor="Blue" Style="background-color: #20b0ff" Height="34px" Width="86px" ValidationGroup="a" OnClientClick="JavaScript:return ShowGrn();" />
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/WebPages/EgGuestProfile.aspx"
                                            Text="Reset" Visible="false"></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:LinkButton ID="lnkExtraDetails" runat="server" Text="View Extra Details" Visible="false"
                            OnClientClick="openPopup();"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div>
        <ajaxToolkit:ModalPopupExtender ID="MpeOTP" runat="server" TargetControlID="btnGo" 
            PopupControlID="PanelOTP" CancelControlID="btnOTPCancel" BackgroundCssClass="DivBackground" BehaviorID="popup1">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelOTP" runat="server" BackColor="White" Height="500px" Width="400px"
            Style="display: none">
            <div id="divUPI" class="row" runat="server" style="padding-top: 20px">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                    <div class="row" style="padding-bottom: 10%;">
                        <div class="col-3 col-sm-2 col-md-1 col-lg-1"></div>
                        <div class="col-9 col-sm-9 col-md-9 col-lg-9" style="margin-left: 20px">
                            <asp:RadioButtonList ID="rblUpi" runat="server" RepeatDirection="Horizontal"
                                BorderStyle="Groove" RepeatColumns="4">
                                <asp:ListItem Value="99300011" Selected="True">&lt;img width=30px src=../Image/EpayBanksLogo/logo_paytm.png&gt; Paytm</asp:ListItem>
                                <asp:ListItem Value="99300012">&lt;img width=30px src=../Image/ic_googlepay-01.png&gt; GPay</asp:ListItem>
                                <asp:ListItem Value="99300013">&lt;img width=30px src=../Image/ic_phonepe-01.png&gt; PhonePe</asp:ListItem>
                                <asp:ListItem Value="99300014">&lt;img width=30px src=../Image/ic_upi-01.png&gt; BHIM</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 20px;">
                        <div class="col-2 col-sm-2 col-md-2 col-lg-2" style="margin-left: 20px; margin-top: 20px">UPI ID:</div>
                        <div class="col-5 col-sm-9 col-md-5 col-lg-5" style="text-align: right; padding-right: 3%; float: right">
                            <asp:TextBox AutoComplete="Off" ID="txtUpi" runat="server" CssClass="txtCheque"
                                Style="height: 25px; margin-right: 30px" MaxLength="20" onkeypress="return (event.charCode > 47 && event.charCode < 58) ||event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123)"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ErrorMessage="Last digit cannot be 0 and minimum 6 digits required."
                                ControlToValidate="txtUpi" ValidationGroup="vldBank" Display="Dynamic"
                                ValidationExpression="[a-zA-Z0-9]{7,20}" />
                        </div>

                    </div>
                    <div class="row" style="padding-bottom: 10%; margin-top: 20px">
                        <div class="col-2 col-sm-2 col-md-2 col-lg-2"></div>
                        <div class="col-6 col-sm-6 col-md-6 col-lg-6" style="margin-left: 200px; margin-top: 70px">
                            <asp:Button ID="btnVerify" runat="server" Text="Verify" />
                            <asp:Button ID="btnOTPCancel" runat="server" Text="Cancel" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-lg-2"></div>
                        <div class="col-md-10 col-lg-10 mandatory" id="divBankMsg" runat="server" visible="false">Please Select Payment Mode !!</div>
                    </div>
                    <%-- <div class="row">
                                                                                <div class="col-6 col-sm-8 col-md-7 col-lg-7">
                                                                                    <asp:Button ID="btnReVerify" class="next action-button btnHeight" ValidationGroup="vldBank" Text="ReVerify" runat="server" OnClick="btnReVerify_Click" Visible="false" />
                                                                                </div>
                                                                            </div>--%>
                </div>
            </div>
            <%--<div style="text-align: left; margin-top: 10px; padding: 20px;">

                        <b><span style="color: #336699; font-family: Arial CE; font-size: 13px">Mobile No:-</span></b>&nbsp;
                            <asp:TextBox ID="txtmob" runat="server" MaxLength="10" Width="60%"></asp:TextBox>
                        <br />
                        <br />
                        <b><span style="color: #336699; font-family: Arial CE; font-size: 13px">Enter Your OTP:-</span></b>&nbsp;
                            <asp:TextBox ID="txtOTP" runat="server" TextMode="Password" MaxLength="6"></asp:TextBox>
                        <br />
                        <br />
                        <div style="text-align: center;">
                            <asp:Button ID="btnVerify" runat="server" Text="Verify" />
                            <asp:Button ID="btnOTPCancel" runat="server" Text="Cancel" />

                            <asp:LinkButton ID="lnkresendcode" runat="server"
                                Text="Resend OTP"></asp:LinkButton>
                            <asp:Label ID="lblermsg" runat="server" ForeColor="Red"></asp:Label>
                            <asp:HiddenField ID="hdnuserid" runat="server" />
                        </div>
                    </div>--%>
        </asp:Panel>
    </div>

</asp:Content>
