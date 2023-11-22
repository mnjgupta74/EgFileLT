<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgDefaceDetailNew.aspx.cs" Inherits="WebPages_EgDefaceDetailNew" Title="Untitled Page" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function openPopup() {
            var argObj = window;
            var id = document.getElementById("<%= Gennolbl.ClientID %>").innerHTML;

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("EgEChallanView.aspx/EncryptData") %>',
                data: '{"id":"' + id + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    window.open("EgAddExtraDetail.aspx?id=" + escape(msg.d), argObj, "dialogWidth:800px; dialogHeight:500px; dialogLeft:252px; dialogTop:120px; center:yes");
                }
            });
        }



        function checkamounttotal() {
            var junkVal = document.getElementById("<%= NetAmountlbl.ClientID %>").innerHTML;
            var x = new Array();
            x = junkVal.split(".");
            var value1 = x[0];
            var value2 = x[1];

            var obStr = new String(value1);
            var numberReversed = value1.split("");
            actnumber = numberReversed.reverse();

            if (Number(value1) >= 0) {
            }

            else {
                alert('Invalid Amount');
                return false;
            }

            if (Number(value1) == 0) {
                document.getElementById("<%= txtamountwords.ClientID %>").value = 'Rupees Zero Only';
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

                    alert('Oops!!!! the Number is too big to convert');

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
    </script>

    <link href="../CSS/DefaceDetailNew.css" rel="stylesheet" type="text/css" />
    <center>
        <div id="divChallanView1">
            <center>
                <div id="Div" class="Div" runat="server">
                    <asp:Label ID="literal1" runat="server" Visible="false"></asp:Label>
                </div>
                <div id="divChallanHeading" align="center">
                    <asp:Label ID="lblEChallan" runat="server" Text="e-CHALLAN" Style="font-size: x-large; font-family: Arial;"
                        ForeColor="Black"></asp:Label>
                    <br />
                    <div id="divDept" style="position: relative;">
                        <img src="../Image/th.jpg" class="imageGreyShadeBehindDept"> <span class="cssSpan">
                            <asp:Label ID="lblDept1" runat="server" CssClass="lblDepartment" Style="margin-right: 65px;"
                                Font-Bold="true"></asp:Label>
                        </span></img>
                    </div>
                    <div id="divMinusChallan">
                        <asp:Label ID="lblGovtRaj" runat="server" Text="Government of Rajasthan" Font-Bold="true"
                            CssClass="arial"></asp:Label>
                    </div>
                </div>
                <table align="left">
                    <tr>
                        <td colspan="2">
                            <font class="fontGRN">GRN:</font>
                            <asp:Label ID="Gennolbl" runat="server" Font-Bold="true"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <font class="fontPaymentDate">Payment Date:</font>
                            <asp:Label ID="formdatelbl" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="divChallanDetails">
                    <div id="divOfficeName" align="left">
                        <b style="width: 50%;">_________________________________________________________________________________________</b>
                        <div align="left">
                            <font class="font">Office Name:</font>&nbsp;&nbsp;&nbsp; <b>
                                <asp:Label ID="OfficeNamelbl" runat="server" CssClass="arial"></asp:Label></b>
                        </div>
                        <div align="left">
                            <font class="font">Location:</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Locationlbl" runat="server"></asp:Label>
                        </div>
                        <div align="left">
                            <font class="font">Period:</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="frommonthlbl" runat="server" CssClass="arial"></asp:Label>
                            <font class="arial">-To-</font>
                            <asp:Label ID="tomonthlbl" runat="server" CssClass="arial"></asp:Label>
                        </div>
                        <b>_________________________________________________________________________________________</b>
                    </div>
                    <div id="divGridPurpose">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="CssGrid">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Purpose/Budget Head Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Schemaname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>
                                        Amount (<asp:Image ID="Image1" runat="server" ImageUrl="~/Image/rupees.jpg" />)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# String.Format("{0:n}", Eval("Amount") )  %>'
                                            align="right"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div align="right">
                            <div align="right">
                                <font class="font">Commision(-):</font>
                                <asp:Label ID="deductcommisiionlbl" Width="300px" runat="server" Style="font-family: Arial;"></asp:Label>
                            </div>
                            <div align="right">
                                <font class="fontMargin">Total/NetAmount</font>
                                <asp:Label ID="NetAmountlbl" runat="server" Width="300px" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div align="right">
                            <asp:Label ID="Amountwordslbl" runat="server" CssClass="arial"></asp:Label>
                            <asp:TextBox ID="txtamountwords" runat="server" TextMode="MultiLine" Height="30px"
                                Width="100%" ReadOnly="True" Columns="20" Rows="2" Font-Bold="true"></asp:TextBox>
                            <div>
                                <asp:Label ID="lbltxtamountinwords" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="divUpperPayeeDetails">
                        <div id="divPayeeDetails" align="left" class="position">
                            <b><font class="arial">Payee Details:</font> </b>
                            <table runat="server" style="width: 107%; text-align: left" id="TABLE6" cellpadding="0"
                                border="1" cellspacing="0">
                                <tr>
                                    <td colspan="2" height="25px">
                                        <font class="font">Full Name:</font> <b>
                                            <asp:Label ID="fullnamelbl" runat="server" CssClass="arial"></asp:Label></b>
                                    </td>
                                    <td colspan="2">
                                        <font class="font">Tin/Actt.No./VehicleNo./Taxid:</font><br></br> <b>
                                            <asp:Label ID="Tinlbl" runat="server" Font-Bold="true"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25px">
                                        <font class="font">Pan No.(If Applicable):</font>
                                    </td>
                                    <td>&nbsp;
                                        <asp:Label ID="PanNolbl" runat="server" CssClass="arial"></asp:Label>
                                    </td>
                                    <td>
                                        <font class="font">City(Pincode):</font>
                                    </td>
                                    <td>&nbsp;
                                        <asp:Label ID="Townlbl" runat="server" Text="Label" CssClass="arial"></asp:Label>
                                        <asp:Label ID="Pinlbl" runat="server" CssClass="arial"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div>
                                            <font class="font">Address:</font>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="TextBox1" runat="server" Wrap="true" ReadOnly="true" TextMode="MultiLine"
                                                Columns="20" Rows="2" Width="359px" CssClass="font" Height="99px"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        <div>
                                            <font class="font">Remarks:</font>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="TextBox2" runat="server" Wrap="true" ReadOnly="true" TextMode="MultiLine"
                                                Width="368px" Height="99px" Columns="20" CssClass="font" Rows="2"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <img src="../Image/HeaderNewColor.gif" class="image" />
                    <div id="divBankDetails">
                        <table runat="server" id="TABLE5" cellpadding="0" border="1" width="100%" cellspacing="0">
                            <%--start DMFT--%>
                            <tr id="trPdaccNo" visible="false" runat="server">
                                <td colspan="4" align="left"><b>PdAccNo:-
                                        <asp:Label ID="lblPdAccNo" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                            <%--End DMFT--%>
                            <tr>
                                <td colspan="4">
                                    <b><font class="arial">Payment Details:</font> </b>
                                    <asp:Label ID="lblChallan" runat="server" Text="Challan No." Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td1">
                                    <font class="font">Bank:</font>
                                </td>
                                <td class="td">&nbsp;<asp:Label ID="nameofbanklbl" runat="server"></asp:Label>&nbsp;
                                </td>
                                <td class="td1">
                                    <font class="font">Bank CIN No:</font>
                                </td>
                                <td class="td">&nbsp;&nbsp;<b>
                                    <asp:Label ID="lblCIN" runat="server" Visible="false"></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <font class="font">Date:</font>
                                </td>
                                <td>&nbsp;&nbsp; <b>
                                    <asp:Label ID="lbltransdate" runat="server"></asp:Label></b>
                                </td>
                                <td>
                                    <font class="font">Reference No:</font>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:Label ID="lblRef" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--<div style="background-color: White;" id="rpt-container">
                        <rsweb:ReportViewer ID="DefaceDetailSSRS" runat="server" Font-Names="Times New Roman"
                            Font-Size="8pt" Height="700px" Width="100%" ShowToolBar="false" AsyncRendering="true">
                        </rsweb:ReportViewer>
                    </div>--%>
                    <%--  <div style="margin-top: 160px">
                        <asp:Label ID="lblReceiptMsg" runat="server"> </asp:Label>
                    </div>--%>
                </div>
            </center>
        </div>
        <div id="divPrintImg">
            <asp:LinkButton ID="lnkExtraDetails" runat="server" Text="View Extra Details" Visible="false"
                Style="font-family: Arial;" OnClientClick="openPopup();"></asp:LinkButton>
            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Image/printer.jpg" runat="server"
                OnClick="ImageButton1_Click" OnClientClick="javascript:ClickToPrint();" />
        </div>
    </center>
</asp:Content>
