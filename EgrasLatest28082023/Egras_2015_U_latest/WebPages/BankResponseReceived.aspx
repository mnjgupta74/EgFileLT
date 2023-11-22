<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankResponseReceived.aspx.cs"
    Inherits="WebPages_BankResponseReceived" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Egras.rajasthan.gov.in</title>
    <script type="text/javascript">
        var i = 0;
        
        function ShowCurrentTime() {
            var dt = new Date();
            document.getElementById("lblTime").innerHTML = 30 - i + " Seconds";
            i++;

            window.setTimeout("ShowCurrentTime()", 1000); // Here 1000(milliseconds) means 1 sec

        }
        function AmountInWords(value) {
            
            var junkVal = value;

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
                document.getElementById('txtamountwords').value = "";
                return false;

            }

            if (Number(value1) == 0 && Number(value2) == 0) {


                document.getElementById('txtamountwords').value = 'Rupees Zero Only';

                return false;

            }

            if (actnumber.length > 14) {

                alert('Oops!!!! the Number is too big to covertes');
                document.getElementById('txtamountwords').value = "";
                return false;

            }



            var iWords = ["Zero", " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine"];

            var ePlace = ['Ten', ' Eleven', ' Twelve', ' Thirteen', ' Fourteen', ' Fifteen', ' Sixteen', ' Seventeen', ' Eighteen', ' Nineteen'];

            var tensPlace = ['dummy', ' Ten', ' Twenty', ' Thirty', ' Forty', ' Fifty', ' Sixty', ' Seventy', ' Eighty', ' Ninety'];

            var finalWord = "";
            var finalWord1 = "";
            if (Number(value1) != 0) {
                var iWordsLength = numberReversed.length;

                var totalWords = "";

                var inWords = new Array();



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

                                inWords[j] = iWords[actnumber[i]] + ' Hundred and ';

                            }

                            else {

                                inWords[j] = iWords[actnumber[i]] + ' Hundred ';

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
                            if (((actnumber[i + 1] != 0 || actnumber[i] > 0)) || iWordsLength > 9) {

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
                            if (actnumber[i + 1] != 0 || actnumber[i] > 0) {

                                inWords[j] = inWords[j] + " Hundred ";

                            }

                            break;
                        case 10:

                            tens_complication();

                            break;
                        default:

                            break;

                    }

                    j++;


                }
                inWords.reverse();

                for (i = 0; i < inWords.length; i++) {

                    finalWord += inWords[i];

                }
            }
            if (Number(value2) != 0) {
                if (value2 == undefined) {
                    document.getElementById('txtamountwords').value = finalWord;
                }
                else {

                    var obStr = new String(value2);

                    var numberReversed = value2.split("");

                    actnumber = numberReversed.reverse();



                    if (Number(value2) >= 0) {

                        //do nothing  

                    }

                    else {

                        alert('Invalid Amount');
                        document.getElementById('txtamountwords').value = "";
                        return false;

                    }

                    if (Number(value2) == 0) {


                        document.getElementById('txtamountwords').value2 = 'Rupees Zero Only';

                        return false;

                    }

                    if (actnumber.length > 14) {

                        alert('Oops!!!! the Number is too big to covertes');
                        document.getElementById('txtamountwords').value = "";
                        return false;

                    }

                    var iWordsLength = numberReversed.length;

                    var totalWords = "";

                    var inWords = new Array();



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

            }
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

            var con = "";
            if (finalWord1 != "" && finalWord != "") {
                con = 'and';

            }


            document.getElementById('txtamountwords').innerHTML = '('+finalWord + con + finalWord1+')';
        }
    </script>
</head>
<body onload="ShowCurrentTime()" style="background-color: #f9f9f9">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div style="width: 100%">

            <div style="text-align: center; color: Blue; width: 100%">
                <img name="Grass" src="../App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="center"
                    width="1024px" />
            </div>
            <div style="margin-left: 23%; width: 54%; clear: both;">
                <div style="background-color: white; float: left;">
                    <img src="../Image/user_icon.png" />
                    <asp:Label ID="lblWelcome" runat="server" Text="User :" CssClass="HmenuText"
                        ForeColor="Black" />
                    <asp:Label ID="lblUser" runat="server" CssClass="HmenuText" ForeColor="Black" Style="padding-right: 40px;" />

                    <asp:Label ID="lblDate1" runat="server" Text="Date :" CssClass="HmenuText" Font-Bold="true"
                        ForeColor="Black" />
                    <asp:Label ID="lblDate" runat="server" CssClass="HmenuText" Font-Bold="true"
                        ForeColor="Black" />
                </div>
                <div style="background-color: white; float: right">
                    <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" OnClick="lnkLogout_Click"
                        BackColor="white" ForeColor="Black" Font-Bold="true"></asp:LinkButton>
                </div>
            </div>

            <div style="margin-left: 23%; width: 54%; clear: both;">
                <div id="main_menu" style="border-top: #cb3e00 2px solid; padding-left: 10px;">
                </div>
            </div>


            <div style="margin-left: 23%; width: 54%; clear: both;">
                <asp:LinkButton ID="lnkHome" runat="server" Text="Home" Visible="false" OnClick="lnkHome_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnkGuest" runat="server" Text="GuestSchema" Visible="false" OnClick="lnkGuest_Click"></asp:LinkButton>
            </div>

        </div>
        <br />
        <br />
        <div id="divReport" runat="server" visible="true">
             <div style="text-align: center;">
            <img src="../Image/PaymentSuccess1.png" align="middle" id="successpay" runat="server" visible="false" style="width: 55px;">
            <img src="../Image/UnsuccessPayment.png" align="middle" id="unsuccesspay" runat="server" visible="false" style="width: 55px;">
            <img src="../Image/PendingPayment.png" align="middle" id="pendingpay" runat="server" visible="false" style="width: 55px;" />

        </div>

             <div style="margin-left: 20%; width: 60%; text-align: center; padding-top: 1%; font-family: Verdana;">
            <asp:Label ID="lblstatus" runat="server" Text="Label" ForeColor="black" Font-Bold="true" Style="font-size: 20px; text-decoration: solid"
                Visible="false"></asp:Label>
            <asp:Label ID="Label7" runat="server" Text="Label" ForeColor="Red" Font-Bold="true"
                Visible="false"></asp:Label>
            <br />
            <br />
        </div>

             <div style="margin-left: 30%; width: 100%">

            <div runat="server" style="width: 40%; text-align: left;" id="TABLE1">
                <div style="box-shadow: 0px 0px 3px 3px gray; padding: 7px; margin-bottom: 10px; background-color: #FFFFFF">
                    <div align="center">
                        <div style="height: 30px; font-size: 20px;">
                            <div style="width: 100%; margin-left: 0px">
                                <asp:Label ID="lblheading" runat="server" Text="eGRAS Challan Receipt" Font-Bold="True" Style="font-size: inherit !important; font-family: arial;"
                                    ForeColor="#27ae60"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="font-size: 15px !important; font-family: Verdana; color: gray;">
                        <div style="width: 100%; clear: both; padding-bottom: 10px; margin-top: 10px;">
                            <div style="float: left; width: 50%;">
                                GRN
                            </div>
                            <div style="text-align: left; float: inherit;">
                                <asp:Label ID="grnlbl" runat="server" Text="GRN" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%; clear: both;">
                            <div style="float: left; width: 50%;">
                                For Amount
                            </div>
                            <div style="text-align: left; float: right; width:50%; padding-bottom: 10px;">
                                <asp:Label ID="netamountlbl" runat="server"  Style="font-weight: bold; font-size: 15px;"></asp:Label><br />
                                <asp:Label ID="txtamountwords" runat="server" Style="text-align: left; position: relative; overflow: auto;"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%; clear: both; padding-bottom: 10px;">
                            <div style="float: left; width: 50%;">
                                Bank Reference No.
                            </div>
                            <div style="text-align: left; float: inherit;">
                                <asp:Label ID="Label1" runat="server" Text="Label" Style="font-size: 15px;"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%; clear: both; padding-bottom: 10px;">
                            <div style="float: left; width: 50%;">
                                Bank CIN
                            </div>
                            <div style="text-align: left; float: inherit;">
                                <asp:Label ID="Label2" runat="server" Text="Label" Style="font-size: 15px;"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%; clear: both; padding-bottom: 10px;">
                            <div style="float: left; width: 50%;">
                                Payment Date
                            </div>
                            <div style="text-align: left; float: inherit;">
                                <asp:Label ID="Label3" runat="server" Text="Label" Style="font-size: 15px;"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%; clear: both; padding-bottom: 10px;">
                            <div style="float: left; width: 50%;">
                                Status
                            </div>
                            <div style="text-align: left; float: inherit;">
                                <asp:Label ID="Label4" runat="server" Text="Label" Style="font-size: 18px;"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%; clear: both; padding-bottom: 10px;">
                            <div style="text-align: left; float: inherit;">
                                <asp:Label ID="Label5" runat="server" Text="Label" Style="font-size: 15px; visibility: hidden;"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="font-size: 17px !important;">

                        <div style="font-family: arial; text-align: right;">
                            <asp:ImageButton ID="lnkprint" ImageUrl="~/Image/printer.jpg" runat="server" OnClick="lnkprint_Click" />
                            <%--<asp:LinkButton ID="lnkprint" runat="server" Text="Print Challan" OnClick="lnkprint_Click"></asp:LinkButton>--%>
                        </div>
                    </div>
                </div>
                </br>
                        <div runat="server" id="TABLE2" class="table1"
                            align="center" width="100%;" style="clear: both;">
                            <div class="style1" style="text-align: Left; float: left">

                                <span style="color: Blue; font-family: Verdana; font-size: 18px;">You Will Redirect
                                            Back In:</span>
                                <label id="lblTime" style="font-weight: bold; color: gray; font-size: x-large">
                                </label>
                                &nbsp;
                                       
                                        <asp:Timer ID="AjaxTimerControl" runat="server" Interval="30000" OnTick="AjaxTimerControl_Tick">
                                        </asp:Timer>
                            </div>
                            <div style="text-align: right; float: right; font-family: Verdana; font-size: 18px;border: 1px solid blue; border-radius: 3px;padding: 5px;">
                                <asp:LinkButton ID="LinkIntegration" runat="server" Text="Back to Department web site" style="margin-bottom: 5px; text-decoration: none;"
                                    OnClick="LinkIntegration_Click"></asp:LinkButton>
                            </div>
                        </div>
            </div>
            <br />
            <br />
        </div>
        </div>
    </form>
</body>
</html>
