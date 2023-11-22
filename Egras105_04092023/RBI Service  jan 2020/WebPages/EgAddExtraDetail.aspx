<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgAddExtraDetail.aspx.cs"
    Inherits="WebPages_EgAddExtraDetail" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add Extra Detail</title>
    <%--<script type="text/javascript" src="../js/jquery.js"></script>--%>
    <script type="text/javascript" src="../js/jquery-3.6.0.min.js"></script>
    <%--<script type="text/javascript" src="../js/jquery.innerfade.js"></script>--%>

    <style type="text/css">
        .header {
            text-align: center;
            background-color: #CCCCFF;
        }

        .column {
            vertical-align: top;
            width: 33%;
            background-color: #CCFFCC;
        }
    </style>



    <link rel="stylesheet" href="../CSS/reset.css" type="text/css" media="all" />
    <link rel="stylesheet" href="../CSS/fonts.css" type="text/css" media="all" />
    <%-- <style type="text/css" media="screen, projection">
        @import url(css/jq_fade.css);
    </style>--%>

    <script type="text/javascript" language="javascript">

        function callParentMethod(deatils) {
            var resultVariable = deatils;
            window.self.close();
            //  var objArg = window.dialogArguments;

            var objArg = null;

            if (window.dialogArguments) // Internet Explorer supports window.dialogArguments
            {
                objArg = window.dialogArguments;
            }
            else // Firefox, Safari, Google Chrome and Opera supports window.opener
            {
                if (window.opener) {
                    objArg = window.opener;
                }
            }

            objArg.CallMe(resultVariable);
        }

        function PrintDiv() {
            var divToPrint = document.getElementById('divtblbankinfo');
            var popupWin = window.open('', '_blank', 'width=400,height=400');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>

    <script type="text/javascript">

        function checknum(objEvent) {
            var iKeyCode;
            if (window.event || objEvent.keyCode) // IE
            {
                iKeyCode = objEvent.keyCode;
                if (objEvent.keyCode >= 48 && objEvent.keyCode <= 57 || objEvent.keyCode == 8 || objEvent.keyCode == 9 || objEvent.keyCode == 37 || objEvent.keyCode == 38 || objEvent.keyCode == 39 || objEvent.keyCode == 40 || objEvent.keyCode == 46) {
                    return true;
                }
                else {
                    alert("Plese Enter Numeric Characters");
                    return false;
                }
            }
            else if (objEvent.which) // Netscape/Firefox/Opera
            {
                iKeyCode = objEvent.which;
                if (objEvent.which >= 48 && objEvent.which <= 57 || objEvent.which == 8 || objEvent.which == 9 || objEvent.keyCode == 37 || objEvent.keyCode == 38 || objEvent.keyCode == 39 || objEvent.keyCode == 40 || objEvent.keyCode == 46) {
                    return true;
                }
                else {
                    alert("Plese Enter Numeric Characters");
                    return false;
                }
            }
        }

        function DecimalNumber(evt) {
            //// var ex = /^[0-9]+\.?[0-9]*$/;
            //var ex = /^[0-9]{0,2}$/; // for 2 digits after decimal
            //if (el.value != "") {
            //    if (ex.test(el.value) == false) {
            //        alert('only numeric value allow.!');
            //        el.value = "";
            //    }
            //}
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode > 47 && charCode < 58) || charCode === 191) {
                return true;
            }
            return false;
        }
        function updateValue(txtID) {

            var res = 0;
            var inputs = [];
            var totalbox = 0;
            var aRow = document.getElementById("txtrow").value;
            var bColumn = document.getElementById("txtColumn").value;
            for (var p = 0; p < aRow; p++) {
                for (var Q = 0; Q < bColumn; Q++) {
                    if (Q == bColumn - 1) {
                        var NewValue = "TextBox_" + p + Q;
                        var New1 = document.getElementById(NewValue).value
                        if (New1 != "") {
                            res = res + parseFloat(New1);
                        }
                    }
                }
            }
            res = res.toFixed(0);
           
            if (txtID != "") {
                var getId = document.getElementById(txtID).value;
            }
            
            if (getId == "" || getId == ".") {
                document.getElementById(txtID).value = "00";
            }

            document.getElementById("txttotalAmount").value = res;

            var num = document.getElementById("txttotalAmount").value;

            var result = parseFloat(num).toFixed(0);
            document.getElementById("txttotalAmount").value = result;

            var Value = document.getElementById("lbltotalAmount").innerHTML;
            var sbmt = document.getElementById("btnaddmore");

            if (Value == res) {
                sbmt.disabled = false;

                document.getElementById("divmsg").style.display = "none";
            }
            else {
                sbmt.disabled = true;
                document.getElementById("divmsg").style.display = "Block";
            }

        }
        function ClearValue(el) {
            if (el.value == "00") {
                el.value = "";
            }

        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <fieldset id="fieldExtra" runat="server" width="90%" align="center" style="border: Solid 2px #45A3DE;">
            <legend style="color: #005CB8; font-size: larger;">Add Extra Details</legend>
            <div id="divopenWindow1" runat="server">
                <table width="100%" style="border: ridge 1px #45A3DE; width: 100%;" id="tblExtraInfo"
                    cellpadding="0" cellspacing="0">

                    <tr>
                        <td>
                            <asp:Button ID="btnprevious" runat="server" Text="Same as previous Details" OnClick="btnprevious_Click" />
                        </td>
                        <td style="padding: 5px">
                            <asp:Button ID="btnnew" runat="server" Text="New Add Detail" OnClick="btnnew_Click" Style="margin-left: 30px; font-size: 15px;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divRestriction" runat="server" visible="false">
            </div>
            <div id="divopenWindow2" runat="server" visible="false">
                <asp:Panel ID="pnlpopup" runat="server" BackColor="White">
                    <table width="100%" style="width: 100%; font-size: 15px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>Row And Column
                            </td>
                            <td style="float: left">
                                <asp:TextBox ID="txtrow" runat="server" Width="50px" MaxLength="3" onkeypress="return checknum(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtrow" ValidationGroup="vld"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rangeNum" runat="server" ControlToValidate="txtrow" MinimumValue="1"
                                    ValidationGroup="vld" MaximumValue="100" Type="Integer" SetFocusOnError="true"
                                    Text="Enter number between 1 and 100" Display="Dynamic" />
                                <asp:TextBox ID="txtColumn" runat="server" Width="50px" MaxLength="1" onkeypress="return checknum(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtColumn"
                                    ValidationGroup="vld" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCreateTable" runat="server" Text="Create" OnClick="btnCreateTable_Click"
                                ValidationGroup="vld" Style="margin-left: 30px; font-size: 15px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnreset" runat="server" Text="Reset" Style="margin-left: 30px; font-size: 15px" OnClick="btnreset_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div id="divopenWindow3" runat="server" visible="false">
                <table width="100%" style="border: ridge 1px #45A3DE; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlHead2" runat="server" Visible="true" align="center" ScrollBars="Both"
                                Width="90%" Height="300px" BorderWidth="0">
                                <table align="center" valign="middle" id="tblDynamic" runat="server" cellpadding="0"
                                    width="100%" border="1" cellspacing="0">
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divamount" runat="server" visible="false">
                                <font color="green">Detail Amount:- </font>&nbsp;
                            <asp:TextBox ID="txttotalAmount" runat="server" Width="175px" Style="text-align: left; background: white;"
                                Text="00" Font-Bold="true" ForeColor="Green" BorderStyle="None" BorderWidth="0"
                                Enabled="false" MaxLength="12"></asp:TextBox>
                                <font color="green">Consolidate Amount:- </font>&nbsp;<asp:Label ID="lbltotalAmount"
                                    Enabled="false" runat="server" Text="Label1" ForeColor="Green" Font-Bold="true"></asp:Label>
                            </div>
                        </td>
                        <td align="center" height="35px">
                            <asp:Button ID="btnaddmore" runat="server" Text="Save Detail" OnClick="btnaddmore_Click"
                                ValidationGroup="a" Visible="false" />
                            <div id="divmsg" style="display: none">
                                <font color="#20C4FB">Detail Amount should be equal to Consolidate Amount. </font>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divShowExtra" runat="server" visible="false">
                <table id="tblExtradetails" width="100%" align="center">
                    <tr>
                        <td align="center">
                            <div id="divtblbankinfo" runat="server" visible="false">
                                <table>
                                    <tr align="center">
                                        <td colspan="4">
                                            <img src="../App_Themes/images/HeaderNewColor.jpg" width="800px" height="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px">
                                            <asp:Label ID="lblgrn" runat="server" Text="GRN" Font-Bold="true"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblform" runat="server" Text="BARCODE" Font-Bold="true"></asp:Label>
                                            &nbsp;:-&nbsp;&nbsp;
                                        <asp:Image ID="Image2" runat="server" Height="21px" Width="150px" />
                                        </td>
                                        <td height="35px">
                                            <asp:Label ID="LabelCIN" runat="server" Text="CIN" Visible="false"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td height="35px">
                                            <asp:Label ID="LabelRef" runat="server" Text="Reference" Visible="false"></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <div align="center" id="divExtra">
                                                <asp:Label ID="literal1" runat="server" Style="font-size: 11; border: 1px solid green; color: #660000; background-color: Transparent"
                                                    Visible="false"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:LinkButton ID="btnPrint" runat="server" Text="Click For Print" OnClientClick="PrintDiv();"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        &nbsp;
    </form>
</body>
</html>
