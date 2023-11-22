<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgEChallan.aspx.cs" Inherits="WebPages_EgEChallan" %>
<%@ Register Src="~/UserControls/FinancialYearDropDown.ascx" TagName="FinYear" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge;" />

    <%--<link href="../js/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
    
    <%--<script src="../js/jquery.min.js" type="text/javascript"></script>--%>

    <%--<script src="../js/jquery-ui.min.js" type="text/javascript"></script>--%>

    <link href="../CSS/EgEchallan.css" rel="stylesheet" type="text/css" />
    <style>
        .mandatory {
    color: #ff0000;
}
    </style>
    <script type="text/javascript">  
        
        // Choosen Class
        $(function () {
            $(".chzn-select").chosen({
                search_contains: true
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                //Binding Code Again
                $(".chzn-select").chosen({
                    search_contains: true
                });
            }
        });

        function TinWarning() {
            var vCode = document.getElementById("vCode");

            var Tel = document.getElementById("<%= txtTIN.ClientID %>").value

            if (Tel == "" && vCode.value == "18") {

                var retVal = confirm("You have not entered TIN.Are you sure to continue?");
                if (retVal) {
                    return true;
                } else {
                    return false;
                }
            }

        }
        
        $(document).ready(function () {
          <%--  document.getElementById("<%=txtPanNo.ClientID %>").val = "";--%>
           
            var inputs = [];
            var TotalAmount = parseFloat(0);
            inputs = document.getElementById("<%=tbl.ClientID %>").getElementsByTagName("input");
            for (i = 0; i < inputs.length; i++) {
                TotalAmount += parseFloat(inputs[i].value);
            }
            document.getElementById("<%= txttotalAmount.ClientID %>").value = (((TotalAmount - parseFloat(document.getElementById("<%= txtDeduct.ClientID %>").value)) * 100) / 100).toFixed(2);
        });

        function SetTreasury() {

            if (document.getElementById("<%= ddlbankname.ClientID %>").value != 0) {

                var Rtype = $('#<%=rblpaymenttype.ClientID %> input[type=radio]:checked').val()
                if (Rtype == 3) {
                    document.getElementById("DivTcode").style.display = "block";
                    var ddlLocation = document.getElementById("<%= ddllocation.ClientID %>");
                    ddlLocation.value = document.getElementById("<%= ddlbankname.ClientID %>").value.substring(7, 11);

                    var text = $('#ddlLocation option[value=2]').text('Two');
                    var selectedText = ddlLocation.options[ddlLocation.selectedIndex].text;
                    document.getElementById("<%= lblTcode.ClientID %>").innerHTML = 'Effective Treasury:              ' + selectedText;
                }

            }
            else {
                document.getElementById("DivTcode").style.display = "none";
            }

        }


        function CheckTIN() {
         
            var Tel = document.getElementById("<%= txtTIN.ClientID %>").value
            var Period = document.getElementById("<%= ddlPeriod.ClientID %>").value
            //            if (Tel == "") {
            //                alert("Enter Your Tin No");
            //                return false;
            //            }
            if (Period == "0") {
                alert("Please select Period first.");
                document.getElementById("<%=HiddenField2.ClientID %>").value = "1";
                document.getElementById("<%= txtTIN.ClientID %>").value = "";
                return false;
            }
            var TinPat = /^[a-zA-Z0-9\/]{10,11}$/;
            var TINid = Tel;
            var matchArray = TINid.match(TinPat);
            if (matchArray == null) {
                alert("Please Enter Complete Tin No.");
                document.getElementById("<%=HiddenField2.ClientID %>").value = "1";
                document.getElementById("<%= txtTIN.ClientID %>").value = "";
                return false;
            }
            else {
                document.getElementById("<%=HiddenField2.ClientID %>").value = "2";
            }
        }
    </script>

    <script language="javascript" type="text/javascript">

        function NumberOnly(evt) {

            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 3 && parts[1].length >= 3) return false;
        }

        function CallMe(a) {
            var result = a;
            document.getElementById("<%=HiddenField1.ClientID %>").value = result;

        }
     function openPopup() {
            if (document.getElementById("<%= txttotalAmount.ClientID %>").value != "") {
                var grn = document.getElementById("<%= hidGRN.ClientID %>").value;
                var totamt = document.getElementById("<%= txttotalAmount.ClientID %>").value;
                var argObj = window;
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/EgEchallan.aspx/EncryptData") %>',
                    data: '{"grn":"' + grn + '", "totamt":"' + totamt + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    var url1 = "EgAddExtraDetail.aspx";
                    window.open('' + url1 + '?id=' + escape(msg.d) + '', 'popup_window', 'width=1000,height=800,left=252,top=120,center=yes,resizable=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');
                }
                 });
                
            }
            else {
                alert('Please Enter Total Amount.!');
            }
        }
    
    
    </script>

    <script type='text/javascript'>
        $(document).ready(function() {

            $("#datepicker").blur(function(e) {

                var from = $("#datepicker");
                var d = new Date();
                var curr_date = d.getDate();
                var curr_month = d.getMonth();
                var curr_year = d.getFullYear();
                var currdate = curr_date + "/" + (curr_month + 1) + "/" + curr_year
                //alert(currdate);
                if (from.val() > currdate) {
                    alert("date cannot be greater then current date");
                    //alert('hello');
                    $("#datepicker").val('')
                    return false;
                }
            });
        });

        function DisableTextBox() {
            var Uid = document.getElementById("<%=txtChequeDDNo.ClientID %>");
            var Tid = document.getElementById("<%=ddlbankname.ClientID %>");

            var ControlRef = document.getElementById('<%= rblpaymenttype.ClientID %>');
            var CheckBoxListArray = ControlRef.getElementsByTagName('input');
            var spanArray = ControlRef.getElementsByTagName('span');
            var checkedValues = '';
            var nIndex = 0;
            var sValue = '';
            for (var i = 0; i < CheckBoxListArray.length; i++) {
                var checkBoxRef = CheckBoxListArray[i];

                if (checkBoxRef.checked == true) {
                    var labelArray = checkBoxRef.parentNode.getElementsByTagName('label');


                    if (labelArray.length > 0) {
                        if (checkedValues.length > 0) {
                            checkedValues += ', ';
                            nIndex += ', ';
                            sValue += ', ';

                        }
                        checkedValues += labelArray[0].innerHTML;
                        nIndex += i;
                        if (checkedValues == "E-Banking") {
                            Cid.disabled = true;
                            Tid.disabled = true;
                            Uid.disabled = true;
                            Cid.value = "";
                            Tid.value = "";
                            Uid.value = "";
                        }
                        else {
                            Cid.disabled = false;
                            Tid.disabled = false;
                            Uid.disabled = false;
                        }
                    }

                }
            }

        }



      
    </script>

    <script type="text/javascript" language="javascript">


        function ClearValue(el) {

            if (el.value == "0.00") {
                el.value = "";
            }

        }

        function SetValue(el) {

            if (el.value.length == 0) {
                el.value = "0.00";
            }

        }


        function updateValue(txtID) {



            var res = 0;
            var inputs = [];
            var totalbox = 0;

            var i;
            inputs = document.getElementById("<%=tbl.ClientID %>").getElementsByTagName("input");

            for (i = 0; i < inputs.length; i++) {
                if (i != "length") {
                    if (isNaN(inputs[i].value) == true || inputs[i].value.length < 1)
                        continue;

                    else
                        if (inputs[i].value != "0.00") {
                        res = res + parseFloat(inputs[i].value);


                    }
                }
            }
            if (document.getElementById("<%= txtDeduct.ClientID %>").value != "") {
                document.getElementById("<%= txttotalAmount.ClientID %>").value = res - document.getElementById("<%= txtDeduct.ClientID %>").value;
                // Rakesh Add for Convert Total amont decimal 2 Places
                var num = document.getElementById("<%= txttotalAmount.ClientID %>").value;

                var result = parseFloat(num).toFixed(2);
                document.getElementById("<%= txttotalAmount.ClientID %>").value = result;
            }
            else {
                document.getElementById("<%= txttotalAmount.ClientID %>").value = res;
                // Rakesh Add for Convert Total amont decimal 2 Places
                var num = document.getElementById("<%= txttotalAmount.ClientID %>").value;

                var result = parseFloat(num).toFixed(2);
                document.getElementById("<%= txttotalAmount.ClientID %>").value = result;
            }

            var getId = document.getElementById("ctl00_ContentPlaceHolder1_" + txtID).value;
            if (getId == "" || getId == ".") {
                document.getElementById("ctl00_ContentPlaceHolder1_" + txtID).value = "0.00";
            }
         
            
            AmountInWords(document.getElementById("<%= txttotalAmount.ClientID %>").value);

        }
         



        function checkamounttotal(e) {
           if (document.getElementById("<%= txtDeduct.ClientID %>").value < 0) {
                alert('Invalid Amount');
                document.getElementById("<%= txtDeduct.ClientID %>").value = "";
                return false;
            }
            else {
                var res = 0;
                var inputs = [];
                var totalbox = 0;

                var i;
                inputs = document.getElementById("<%=tbl.ClientID %>").getElementsByTagName("input");
                for (i = 0; i < inputs.length; i++) {
                    if (i != "length") {
                        if (isNaN(inputs[i].value) == true || inputs[i].value.length < 1)
                            continue;

                        else
                            if (inputs[i].value != "0.00") {
                                res = res + parseFloat(inputs[i].value);
                            }


                    }
                }
                if (document.getElementById("<%= txtDeduct.ClientID %>").value != "") {
                    document.getElementById("<%= txttotalAmount.ClientID %>").value = res - document.getElementById("<%= txtDeduct.ClientID %>").value;
                    // Rakesh Add for Convert Total amont decimal 2 Places
                    var num = document.getElementById("<%= txttotalAmount.ClientID %>").value;

                    var result = parseFloat(num).toFixed(2);
                    document.getElementById("<%= txttotalAmount.ClientID %>").value = result;

                }
                else {
                    document.getElementById("<%= txttotalAmount.ClientID %>").value = res
                    // Rakesh Add for Convert Total amont decimal 2 Places
                    var num = document.getElementById("<%= txttotalAmount.ClientID %>").value;

                    var result = parseFloat(num).toFixed(2);
                    document.getElementById("<%= txttotalAmount.ClientID %>").value = result;

                }
                AmountInWords(document.getElementById("<%= txttotalAmount.ClientID %>").value);
            }
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
                document.getElementById("<%= txtDeduct.ClientID %>").value = "";
                document.getElementById("<%= txtamountwords.ClientID %>").value = "";
                document.getElementById("<%= txttotalAmount.ClientID %>").value = "";
                return false;

            }

            if (Number(value1) == 0 && Number(value2) == 0) {


                document.getElementById("<%= txtamountwords.ClientID %>").value = 'Rupees Zero Only';

                return false;

            }

            if (actnumber.length > 14) {

                alert('Oops!!!! the Number is too big to covertes');
                document.getElementById("<%= txtamountwords.ClientID %>").value = "";
                document.getElementById("<%= txttotalAmount.ClientID %>").value = "";
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
                    document.getElementById("<%= txtamountwords.ClientID %>").value = finalWord;
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
                        document.getElementById("<%= txtamountwords.ClientID %>").value = "";
                        document.getElementById("<%= txttotalAmount.ClientID %>").value = "";
                        return false;

                    }

                    if (Number(value2) == 0) {


                        document.getElementById("<%= txtamountwords.ClientID %>").value2 = 'Rupees Zero Only';

                        return false;

                    }

                    if (actnumber.length > 14) {

                        alert('Oops!!!! the Number is too big to covertes');
                        document.getElementById("<%= txtamountwords.ClientID %>").value = "";
                        document.getElementById("<%= txttotalAmount.ClientID %>").value = "";
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


            document.getElementById("<%= txtamountwords.ClientID %>").value = finalWord + con + finalWord1;
            document.getElementById("<%= HiddenAmount.ClientID %>").value = finalWord + con + finalWord1;
        }


        function DecimalNumber(el) {
            // var ex = /^[0-9]+\.?[0-9]*$/;
            var ex = /^\d*\.?\d{0,2}$/; // for 2 digits after decimal
            if (el.value != "") {
                if (ex.test(el.value) == false) {
                    alert('Incorrect Number');
                    el.value = "";
                }
            }
        }
        function CharCheck(field) {
            var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ()0123456789 .://\\&"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("  Only character  is Allowed !");
                field.focus();
                field.select();
                field.value = "";
            }
        }
        function CharForCity(field) {
            var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 "
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("  Only character  is Allowed !");
                field.focus();
                field.select();
                field.value = "";
            }
        }
        function PanNumberValidation() {
            if ("<%= Session["UserType"].ToString() %>" != "4") {
                var a = document.getElementById("<%=txtPanNo.ClientID %>").value;
                var regex1 = /^[A-Z]{5}\d{4}[A-Z]{1}$/;  //this is the pattern of regular expersion
                if (regex1.test(a) == false) {
                    alert('Please enter valid pan number');
                    document.getElementById("<%=txtPanNo.ClientID %>").value = "";
                    return false;
                }
            }
            else {
                var a = document.getElementById("<%=txtPanNo.ClientID %>").value;
                var regex1 = /^[A-Z]{4}\d{5}[A-Z]{1}$/;  //this is the pattern of regular expersion
                if (regex1.test(a) == false) {
                    alert('Please enter valid tan number');
                    document.getElementById("<%=txtPanNo.ClientID %>").value = "";
                    return false;
                }
            }
        }

        function validate(field) {
            var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 "
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("Invalid entry!  Only characters and numbers are accepted!");
                field.focus();
                field.select();
            }
        }
        function ChangeCase(elem) {
            elem.value = elem.value.toUpperCase();
        }
        function DisplayDiv(val) {
            //in val u get dropdown list selected value
            var id = val;

            if (id == 1) {
                document.getElementById("divhalfyealy").style.display = "none";
                document.getElementById("divMothly").style.display = "none";
                document.getElementById("divQUARTERLY").style.display = "none";
                document.getElementById("divOneTime").style.display = "none";
            }
            if (id == 2) {
                document.getElementById("divOneTime").style.display = "none";
                document.getElementById("divMothly").style.display = "none";
                document.getElementById("divQUARTERLY").style.display = "none";
                document.getElementById("divhalfyealy").style.display = "block";
            }
            if (id == 3) {
                document.getElementById("divOneTime").style.display = "none";
                document.getElementById("divQUARTERLY").style.display = "none";
                document.getElementById("divhalfyealy").style.display = "none";
                document.getElementById("divMothly").style.display = "block";
            }
            if (id == 4) {
                document.getElementById("divOneTime").style.display = "none";
                document.getElementById("divQUARTERLY").style.display = "block";
                document.getElementById("divhalfyealy").style.display = "none";
                document.getElementById("divMothly").style.display = "none";
            }
            if (id == 5) {
                document.getElementById("divOneTime").style.display = "block";
                document.getElementById("divQUARTERLY").style.display = "none";
                document.getElementById("divhalfyealy").style.display = "none";
                document.getElementById("divMothly").style.display = "none";

                date = new Date();
                var month = date.getMonth() + 1;


                var day = date.getDate();
                var year = date.getFullYear();
                var Fdate = document.getElementById("<%=txtfromdate.ClientID %>").value;

                if (Fdate == "") {
                    document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
                }
                else {
                    document.getElementById("<%=txtfromdate.ClientID %>").value = Fdate;
                }
                var m = new Number(date.getMonth());
                var y = new Number(date.getYear());

                var tmpDate = new Date(y, m, 28);
                var checkMonth = tmpDate.getMonth();

                var lastDay = 27;

                while (lastDay <= 31) {
                    temp = tmpDate.setDate(lastDay + 1);
                    if (checkMonth != tmpDate.getMonth())
                        break;
                    lastDay++
                }
                var Tdate = document.getElementById("<%=txttodate.ClientID %>").value;

                if (Tdate == "") {
                    document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
                }
                else {
                    document.getElementById("<%=txttodate.ClientID %>").value = Tdate;
                }
            }

        }


        function ClearDate() {
            var fmdate = document.getElementById("<%=txtfromdate.ClientID %>")
            fmdate.value = "";
        }
        function ClearDate1() {
            var tdate = document.getElementById("<%=txttodate.ClientID %>")
            tdate.value = "";
        }

        function dateValidation() {

            var dtObj = document.getElementById("<%=txtfromdate.ClientID %>")
            var dtStr = dtObj.value;
            var dtTemp = dtStr;


            date = new Date();
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var year = date.getFullYear();

            if (dtTemp == '') {
                alert('Date cant be blank')
                //                dtObj.value = "";
                document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
                return false;
            }
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                //dtObj.value = ""
                document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
                return false;
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                // dtObj.value = "";
                document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
                return false;
            }
            //check for parts of date
            var DayDt1
            var MonDt1
            var YearDt1
            var dtTemp1 = dtStr
            DayDt1 = dtTemp1.substring(0, dtTemp1.indexOf('/'));
            dtTemp1 = dtTemp1.substring(dtTemp1.indexOf('/') + 1);
            MonDt1 = dtTemp1.substring(0, dtTemp1.indexOf('/'));
            YearDt1 = dtTemp1.substring(dtTemp1.indexOf('/') + 1);
            if (YearDt1 < 1956) {
                alert('Year cannot be less than 1956')
                //  dtObj.value = "";
                document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
                return false
            }
            if (YearDt1.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.');
                //  dtObj.value = "";
                document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
                return false;
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt1) || isNaN(MonDt1) || isNaN(YearDt1)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
                //  dtObj.value = "";
                document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
                return false;
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt1, parseInt(MonDt1 - 1), DayDt1)


            if (DateEntered.getMonth() != (parseInt(MonDt1 - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
                // dtObj.value = "";
                document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
                return false;
            }

            //Date with Current Date Comparision
            var tdate = document.getElementById("<%=txttodate.ClientID %>")
            var dttdate = tdate.value
            var dtTemp2 = dttdate
            if (dttdate == "") {
                return true;
            }
            else {
                var DayDt2 = dtTemp2.substring(0, dtTemp2.indexOf('/'));
                dtTemp2 = dtTemp2.substring(dtTemp2.indexOf('/') + 1);
                var MonDt2 = dtTemp2.substring(0, dtTemp2.indexOf('/'));
                var YearDt2 = dtTemp2.substring(dtTemp2.indexOf('/') + 1);
                var date1 = new Date(YearDt1, MonDt1 - 1, DayDt1)
                var date2 = new Date(YearDt2, MonDt2 - 1, DayDt2)
                if (date2 < date1) {
                    alert("from date cannot be greater than to date");


                    date = new Date();
                    var month = date.getMonth() + 1;
                    var day = date.getDate();
                    var year = date.getFullYear();
                    dtObj.value = day + '/' + month + '/' + year;


                }
            }

        }

        function dateValidation1() {

            var dtObj = document.getElementById("<%=txttodate.ClientID %>")
            var dtStr = dtObj.value;
            var dtTemp = dtStr;

            date = new Date();
            var month = date.getMonth() + 1;

            var year = date.getFullYear();
            var m = new Number(date.getMonth());
            var y = new Number(date.getYear());

            var tmpDate = new Date(y, m, 28);
            var checkMonth = tmpDate.getMonth();

            var lastDay = 27;

            while (lastDay <= 31) {
                temp = tmpDate.setDate(lastDay + 1);
                if (checkMonth != tmpDate.getMonth())
                    break;
                lastDay++
            }

            if (dtStr == '') {
                alert('Date cant be blank');
                // dtObj.value = "";
                document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
                return false;
            }
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.');
                //dtObj.value = "";
                document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
                return false;
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.');
                // dtObj.value = "";
                document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
                return false;
            }
            //check for parts of date
            var DayDt1
            var MonDt1
            var YearDt1
            var dtTemp1 = dtStr;
            DayDt1 = dtTemp1.substring(0, dtTemp1.indexOf('/'));
            dtTemp1 = dtTemp1.substring(dtTemp1.indexOf('/') + 1);
            MonDt1 = dtTemp1.substring(0, dtTemp1.indexOf('/'));
            YearDt1 = dtTemp1.substring(dtTemp1.indexOf('/') + 1);
            if (YearDt1 < 1956) {
                alert('Year cannot be less than 1956')
                // dtObj.value = ""
                document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
                return false
            }

            if (YearDt1.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.');
                //  dtObj.value = "";
                document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
                return false;
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt1) || isNaN(MonDt1) || isNaN(YearDt1)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
                // dtObj.value = "";
                document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
                return false;
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt1, parseInt(MonDt1 - 1), DayDt1)


            if (DateEntered.getMonth() != (parseInt(MonDt1 - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
                // dtObj.value = "";
                document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
                return false;
            }

            //Date with Current Date Comparision
            var fdate = document.getElementById("<%=txtfromdate.ClientID %>")
            var dtfdate = fdate.value
            var dtTemp2 = dtfdate
            if (dtfdate == "") {
                return true;
            }
            else {
                var DayDt2 = dtTemp2.substring(0, dtTemp2.indexOf('/'));
                dtTemp2 = dtTemp2.substring(dtTemp2.indexOf('/') + 1);
                var MonDt2 = dtTemp2.substring(0, dtTemp2.indexOf('/'));
                var YearDt2 = dtTemp2.substring(dtTemp2.indexOf('/') + 1);
                var date1 = new Date(YearDt1, MonDt1 - 1, DayDt1);
                var date2 = new Date(YearDt2, MonDt2 - 1, DayDt2);
                if (date2 > date1) {
                    alert("from date cannot be greater than to date");
                    //                    date = new Date();
                    //                    var month = date.getMonth() + 1;
                    //                    var day = date.getDate();
                    //                    var year = date.getFullYear();
                    //                    dtObj.value = day + '/' + month + '/' + year;
                    document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;

                }
            }
        }

        function NumericOnly(el) {
            var ex = /^\s*\d+\s*$/;
            if (ex.test(el.value) == false) {
                alert('Enter Number Only');
                el.value = "";
            }
        }

        function Count(text, long) {

            var maxlength = new Number(long); // Change number to your max length.

            if (document.getElementById('<%=txtRemark.ClientID%>').value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                return false;
            }

            if (document.getElementById('<%=txtaddress.ClientID%>').value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                return false;
            }
        }
        
        function textCounter(field, field2, maxlimit) {
            var countfield = document.getElementById(field2);
            if (field.value.length > maxlimit) {
                field.value = field.value.substring(0, maxlimit);
                return false;
            } else {
                countfield.value = maxlimit - field.value.length;
            }
        }
      
    </script>
   

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../App_Themes/Images/progress.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenAmount" runat="server" />
            <table width="100%" style="position: relative;">
                <tr align="center">
                    <td style="height: 16px; background-color: #B2D1F0" valign="top">
                        <div style="width: 100%; margin-left: 0px">
                            <asp:Label ID="lblEChallan" runat="server" Text="E-CHALLAN" Font-Bold="True" ForeColor="#009900"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <div id="MainDiv" style="margin-left: 0px; width: 100%;">
                <div id="divtop" class="boxshadow">
                    <table style="position: relative;" width="100%" height="330px" align="center" border="1"
                        id="Tabledata">
                        <tr class="gridalternaterow">
                            <td class="style3  fcolor">
                                District<span class="mandatory">*</span>
                            </td>
                            <td class="tdstyle style3 fcolor" style="text-align: left;" colspan="3">
                                <asp:DropDownList ID="ddlTreasury" class="chzn-select" runat="server" AutoPostBack="true" 
                                    Width="320px" OnSelectedIndexChanged="ddlTreasury_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="--Select Location--"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Location"
                                    ControlToValidate="ddlTreasury" ValidationGroup="vldInsert" InitialValue="0"
                                    ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                <%--</td>
                            <td class="style3 fcolor" id="trprofile" runat="server">--%>
                                Profile &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <%--</td>
                            <td class="tdstyle" style="text-align: left;">--%>
                                <asp:DropDownList ID="ddlProfile" runat="server" AutoPostBack="true" Width="190px"
                                    Style="height: auto" CssClass="borderRadius inputDesign">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="style3  fcolor">
                                Office Name<span class="mandatory">*</span>
                            </td>
                            <td class="tdstyle style3 fcolor" style="text-align: left;" colspan="3">
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="chzn-select" 
                                    AutoPostBack="true" Width="320px" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="--Select Office--"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select Office"
                                    ControlToValidate="ddlOfficeName" ValidationGroup="vldInsert" InitialValue="0"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                Department
                                <asp:TextBox ID="txtDept" runat="server" CssClass="borderRadius inputDesign" Height="19px"
                                    MaxLength="50" Width="190px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="style3  fcolor">
                                Treasury<span class="mandatory">*</span>
                            </td>
                            <td class="tdstyle style3 fcolor" style="text-align: left;" colspan="3">
                                <asp:DropDownListX ID="ddllocation" runat="server" AutoPostBack="true"  Enabled="False"  class="chzn-select"  
                                    OnSelectedIndexChanged="ddllocation_SelectedIndexChanged" Width="320px">
                                    <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                                </asp:DropDownListX>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Treasury"
                                    ControlToValidate="ddllocation" ValidationGroup="vldInsert" InitialValue="0"
                                    ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                PAN No/TAN No
                                <asp:TextBox ID="txtPanNo" runat="server" Width="120px" MaxLength="10"  CssClass="borderRadius inputDesign" OnChange="validate(this);ChangeCase(this);PanNumberValidation();" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="style3 fcolor" colspan="1" style="width: 173px;">
                                Year(Period)<span class="mandatory">*</span>
                            </td>
                            <td colspan="3" style="height: 10px; width: auto; text-align: left;">
                                <ucl:FinYear ID="ddlYear" runat="server" Width="100px" Height="22px" CssClass="borderRadius " />
                                <%--<asp:DropDownList ID="ddlYear" runat="server" Width="100px" Height="22px" CssClass="borderRadius ">
                                    <asp:ListItem Text="2009-10" Value="0910"></asp:ListItem>
                                    <asp:ListItem Text="2010-11" Value="1011"></asp:ListItem>
                                    <asp:ListItem Text="2011-12" Value="1112"></asp:ListItem>
                                    <asp:ListItem Text="2012-13" Value="1213"></asp:ListItem>
                                    <asp:ListItem Text="2013-14" Value="1314"></asp:ListItem>
                                    <asp:ListItem Text="2014-15" Value="1415"></asp:ListItem>
                                    <asp:ListItem Text="2015-16" Value="1516"></asp:ListItem>
                                    <asp:ListItem Text="2016-17" Value="1617"  Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                    ErrorMessage="Select Year!" ControlToValidate="ddlYear" ValidationGroup="vldInsert"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                &nbsp;
                                <asp:DropDownList ID="ddlPeriod" runat="server" AutoPostBack="false" onchange="DisplayDiv(this.options[this.selectedIndex].value);"
                                    Width="130px" Height="22px" Style="text-align: left;">
                                    <asp:ListItem Value="0" Text="-Select Period-"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="ANNUAL"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="HALF YEARLY"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="MONTHLY"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="QUARTERLY"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="ONE TIME"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPeriod" runat="server" ErrorMessage="Select Period!"
                                    InitialValue="0" ControlToValidate="ddlPeriod" ValidationGroup="vldInsert" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <div id="divhalfyealy" style="display: none; margin: -21px 0px 0px 270px;">
                                    <asp:DropDownList ID="ddlhalfyearly" runat="server" Height="22px">
                                        <asp:ListItem Value="1" Text="APRIL-SEPT"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="OCT-MARCH"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                &nbsp;
                                <div id="divQUARTERLY" style="display: none; margin: -21px 0px 0px 270px;">
                                    <asp:DropDownList ID="ddlQUARTERLY" runat="server" Height="22px">
                                        <asp:ListItem Value="1" Text="APRIL-JUNE"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="JULY-SEPT"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="OCT-DEC"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="JAN-MARCH"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                &nbsp;
                                <div id="divMothly" style="display: none; margin: -21px 0px 0px 270px;">
                                    <asp:DropDownList ID="ddlMothly" runat="server" Height="22px">
                                        <asp:ListItem Value="/04/01@/04/30">April</asp:ListItem>
                                        <asp:ListItem Value="/05/01@/05/31">May</asp:ListItem>
                                        <asp:ListItem Value="/06/01@/06/30">June</asp:ListItem>
                                        <asp:ListItem Value="/07/01@/07/31">July</asp:ListItem>
                                        <asp:ListItem Value="/08/01@/08/31">August</asp:ListItem>
                                        <asp:ListItem Value="/09/01@/09/30">September</asp:ListItem>
                                        <asp:ListItem Value="/10/01@/10/31">October</asp:ListItem>
                                        <asp:ListItem Value="/11/01@/11/30">November</asp:ListItem>
                                        <asp:ListItem Value="/12/01@/12/31">December</asp:ListItem>
                                        <asp:ListItem Value="/01/01@/01/31">January</asp:ListItem>
                                        <asp:ListItem Value="/02/01@/02/28">February</asp:ListItem>
                                        <asp:ListItem Value="/03/01@/03/31">March</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div id="divOneTime" style="display: none; margin: -21px 0px 0px 270px;">
                                    <asp:TextBox ID="txtfromdate" runat="server" Width="100px" Height="16px" onkeypress="Javascript:return NumberOnly(event)"
                                        onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)">
                                    </asp:TextBox>
                                    <ajaxToolKit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtfromdate">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                        CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <asp:TextBox ID="txttodate" runat="server" Width="100px" Height="16px" onkeypress="Javascript:return NumberOnly(event)"
                                        onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)"></asp:TextBox>
                                    <ajaxToolKit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txttodate">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                                        CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                                    </ajaxToolKit:MaskedEditExtender>
                                </div>
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="style3 fcolor" colspan="2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BudgetHead
                            </td>
                            <td class="style3 fcolor" colspan="2" style="text-align: center;">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Amount
                                    in Rs.</b>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td colspan="4" align="center">
                                <asp:Panel ID="pnlHead" runat="server" Visible="false" align="center" ScrollBars="auto"
                                    Width="100%" Height="106px" BorderWidth="0">
                                    <table align="right" valign="middle" id="tbl" runat="server" cellpadding="0" border="0"
                                        cellspacing="0" class="tablepannel tblfcolor">
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="style3 fcolor" style="text-align: left;">
                                Total/Net Amount(<asp:Image ID="Image1" runat="server" ImageUrl="~/Image/rupees.jpg" />)<span class="mandatory">*</span>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txttotalAmount" runat="server" Width="175px" Style="text-align: left;"
                                    AutoPostBack="True" OnTextChanged="txttotalAmount_TextChanged" Font-Bold="true"
                                    ForeColor="Green" BorderStyle="None" BorderWidth="0" CssClass="borderRadius inputDesign"
                                    MaxLength="9"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txttotalAmount"
                                    ErrorMessage="Invalid Total Amount." MaximumValue="999999999999" MinimumValue="1"
                                    Type="Double" ValidationGroup="vldInsert">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter Total Amount"
                                    ControlToValidate="txttotalAmount" ValidationGroup="vldInsert" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="style3 fcolor" style="text-align: left;">
                                Discount:
                            </td>
                            <td style="text-align: left;">
                                &nbsp;<asp:TextBox Enabled="False" ID="txtDeduct" runat="server" Width="180px" MaxLength="9" 
                                    Style="text-align: right;" Text="0.00" onfocus="javascript:ClearValue(this);" 
                                    onBlur="javascript:SetValue(this);" onChange="javascript:return checkamounttotal(event);"
                                    CssClass="borderRadius inputDesign"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                        runat="server" ErrorMessage="Enter Deduct Commission" ControlToValidate="txtDeduct"
                                        ValidationGroup="vldInsert" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtDeduct" runat="server" ErrorMessage="Number Only" ValidationGroup="vldInsert" ValidationExpression="^[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>--%>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="style3 fcolor" style="text-align: left; height: 38px;" colspan="1">
                                Amount in Words
                            </td>
                            <td colspan="3" style="text-align: Left; height: 38px;">
                                <asp:TextBox ID="txtamountwords" runat="server" BorderWidth="0" CssClass="borderRadius inputDesign"
                                    Font-Bold="true" ForeColor="Green" Style="text-align: left; position: relative;
                                    overflow: auto; width: 550px;" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td colspan="4">
                                <div id="divPD" runat="server" visible="true">
                                    PD Account/Division Code &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%-- <asp:TextBox ID="txtpd" runat="server" Width="80px" MaxLength="6" CssClass="borderRadius inputDesign">
                                    </asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlpdAccount" runat="server" class="chzn-select"  Width="300px">
                                       <%-- <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <%-- <ajaxToolKit:AutoCompleteExtender ServiceMethod="GetDynamicContent" MinimumPrefixLength="1"
                                        CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtpd"
                                        ID="autoCompleteExtender1" runat="server" FirstRowSelected="false">
                                    </ajaxToolKit:AutoCompleteExtender>
                                    <div style="display: none; margin: -15px 2px 2px 500px;" runat="server" id="lblError">
                                        Record exsits
                                    </div>
                                    <div style="display: none; margin: -15px 2px 2px 500px;" runat="server" id="lblError1">
                                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                        Record Not exsits
                                    </div>--%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divmiddle" class="boxshadow" style="margin-top: 8px;">
                    <table style="position: relative;" width="100%" height="82px" align="center" border="1"
                        id="TableMid">
                        <tr class="gridalternaterow">
                            <td colspan="4" valign="middle" align="left">
                                <asp:Label ID="lblpayment" runat="server" Text="Payment Details :" Font-Bold="True"
                                    Font-Size="10" ForeColor="#D87E3D"></asp:Label>
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="tdstyle fcolor" align="center" style="text-align: left;width:380px" colspan="2">
                                <%--    <asp:UpdatePanel ID="chidlup" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>--%>
                                <asp:RadioButtonList ID="rblpaymenttype" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rblpaymenttype_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="3">Manual/NEFT-RTGS</asp:ListItem>
                                    <asp:ListItem Value="4" Selected="True">E-Banking</asp:ListItem>
                                    <asp:ListItem Value="5">Payment gateway/Credit/Debit Card</asp:ListItem>
                                    <asp:ListItem Value="6">Upi</asp:ListItem>
                                   
                                </asp:RadioButtonList>
                                <%--    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </td>
                            <%--<td class="tdstyle fcolor" style="width:100px">
                                Name of Bank:
                            </td>--%>
                            <td class="tdstyle" style="text-align: center;" colspan="2">
                                <asp:DropDownList ID="ddlbankname" runat="server" Width="180px" CssClass="borderRadius inputDesign">
                                </asp:DropDownList>
                                <%-- <asp:TextBox ID="txtbankname" runat="server" Width="180px" MaxLength="35" CssClass="borderRadius inputDesign"></asp:TextBox>--%>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ddlbankname"
                                    Text="Special character not allowed.!" CssClass="XMMessage" Display="Dynamic"
                                    ErrorMessage="Special character not allowed in Bank Name." ValidationExpression="^([a-zA-Z0-9_., \s\-]*)$"
                                    ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="gridalternaterow" id="trpaydetail">
                            <td class="tdstyle fcolor" style="width: 124px;" colspan="2">
                               <div id="divbank" runat="server" visible="false">
                                    <table>
                                        <tr>
                                            <td style="width: 50%;">
                                                <asp:RadioButtonList ID="rblCashCheque" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rblCashCheque_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="1" Selected="True">Cash</asp:ListItem>
                                                    <asp:ListItem Value="2">Cheque</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </td>
                                            <td style="width: 50%;">
                                                <span id="spanCheque" runat="server" visible="false">
                                                Cheque/DD No.
                                    <asp:TextBox ID="txtChequeDDNo" runat="server" onkeyup="NumericOnly(this);" style="width: 50%;"
                                        MaxLength="6" CssClass="borderRadius inputDesign"></asp:TextBox>
                                                <br />
                                                <asp:RegularExpressionValidator ID="revChequeNo" runat="server" ErrorMessage="Minimum 6 digits required."
                                                    ControlToValidate="txtChequeDDNo" ValidationGroup="vldInsert" Display="Dynamic"
                                                    ValidationExpression="[0-9]{6,}" />
                                                    </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="PayuDialog" runat="server" visible="false" style="height: 80px;">
                                    <table style="position: relative;" width="100%" height="82px" align="center" border="0"
                                        cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" valign="middle" style="text-align: left; font-size: small; border-collapse: collapse;
                                                border-spacing: 0;">
                                                <asp:Label ID="Label4" runat="server" Text="PNB Gateway Charges Applicable" Font-Bold="True"
                                                    Style="font-weight: bold; text-align: center" ForeColor="#D87E3D"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Credit Card&nbsp;&nbsp;&nbsp; :&nbsp; 0.90%+ST of transaction amount<br />
                                                Debit Card&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;0.75%+ST of transaction amount<br />
                                                Net Banking&nbsp;&nbsp; :&nbsp;Nil
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td class="tdstyle fcolor" style="width: 124px;" colspan="2">
                                <div id="dialog" runat="server" visible="false" style="height: 80px;">
                                    <table style="position: relative;" width="100%" height="82px" align="center" border="0"
                                        cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" valign="middle" style="text-align: center; font-size: small; border-collapse: collapse;
                                                border-spacing: 0;">
                                                <asp:Label ID="Label1" runat="server" Text="EPAY Charges Applicable" Font-Bold="True"
                                                    Style="font-weight: bold; text-align: center" ForeColor="#D87E3D"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Credit Card&nbsp;&nbsp;&nbsp; :&nbsp; 0.90%+ST of transaction amount<br />
                                                Debit Card&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;No Charge<br />
                                                Net Banking&nbsp;&nbsp; :&nbsp;Rs 5 per transaction except SBI & associated banks
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="DivTcode" style="display: none">
                                    <asp:Label ID="lblTcode" runat="server" ForeColor="Red" Text="Label1"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divbottom" class="boxshadow" style="margin-top: 8px;">
                    <table style="position: relative;" width="100%" height="80px" align="center" border="1"
                        id="TableBottom">
                        <tr class="gridalternaterow">
                            <td colspan="4" valign="middle" align="left">
                                <asp:Label ID="lblinfo" runat="server" Text="Personal Detail :" Font-Bold="True"
                                    ForeColor="#D87E3D"></asp:Label>
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td style="margin-left: 10px; width: 124px;" class="tdstyle fcolor">Remitter's Name<span class="mandatory">*</span>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="180px" CssClass="borderRadius inputDesign"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvfullname" runat="server" ErrorMessage="Enter Full Name!"
                                    ControlToValidate="txtName" ValidationGroup="vldInsert" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtName"
                                    CssClass="XMMessage" Text="Special character not allowed.!" ErrorMessage="Special character not allowed in Full Name.!"
                                    ValidationExpression="^([a-zA-Z0-9_.,:;*!#`$+\[(.*?)\]()'@%?={}&//\\ \s\-]*)$"
                                    Display="Dynamic" ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                            <td class="tdstyle fcolor" style="width: 173px; text-align: left;">TIN/Lease No./Actt. No./Vehicle No.<br />
                                /Tax-Id(If Any)<%--<span class="mandatory">*</span>--%>
                            </td>
                            <td class="tdstyle" style="text-align: left">
                                <asp:TextBox ID="txtTIN" runat="server" Width="180px" CssClass="borderRadius inputDesign"
                                    AutoPostBack="True" OnTextChanged="txtTIN_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Your Tin Number !"
                                    ControlToValidate="txtTIN" ValidationGroup="vldnotInsert" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <asp:RegularExpressionValidator ID="rgvTinNo" runat="server" ControlToValidate="txtTIN"
                                    CssClass="XMMessage" Text="Special character not allowed.!" ErrorMessage="Special character not allowed in Tin No.!"
                                    ValidationExpression="^([a-zA-Z0-9_.,:;*!#`$+\[(.*?)\]()'@%?={}&//\\ \s\-]*)$"
                                    Display="Dynamic" ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="tdstyle fcolor" style="width: 124px">
                                PIN<span class="mandatory">*</span>
                            </td>
                            <td class="tdstyle" style="text-align: left;">
                                <asp:TextBox ID="txtPin" runat="server" Width="180px" MaxLength="6" CssClass="borderRadius inputDesign"
                                    onChange="NumericOnly(this);"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Your PIN Number !"
                                    ControlToValidate="txtPin" ValidationGroup="vldInsert" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Minimum 6 digits required."
                                    ControlToValidate="txtPin" ValidationGroup="vldInsert" Display="Dynamic" ValidationExpression="[0-9]{6,}" />
                            </td>
                            <td class="tdstyle fcolor" style="width: 173px">Town/City/District<span class="mandatory">*</span>
                            </td>
                            <td class="tdstyle" style="text-align: left">
                                <asp:TextBox ID="txtCity" runat="server" MaxLength="40" Style="width: 150px;" OnChange="CharForCity(this);"></asp:TextBox>
                                &nbsp;
                                <asp:RequiredFieldValidator ID="rfvcity" runat="server" ControlToValidate="txtCity"
                                    Text="*" ErrorMessage="Enter City Name.!" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td class="tdstyle fcolor" style="width: 124px">Mobile No<span class="mandatory">*</span>
                            </td>
                            <td class="tdstyle" style="text-align: left;">
                                <asp:TextBox ID="txtMobileNo" runat="server" Width="180px" MaxLength="10" CssClass="borderRadius inputDesign"
                                    onChange="NumericOnly(this);"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Your Mobile No Number !"
                                    ControlToValidate="txtMobileNo" ValidationGroup="vldInsert" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Minimum 10 digits required."
                                    ControlToValidate="txtMobileNo" ValidationGroup="vldInsert" Display="Dynamic" ValidationExpression="[0-9]{10,}" />
                            </td>
                            <td class="tdstyle fcolor" style="width: 124px">Address<span class="mandatory">*</span>
                            </td>
                            <td class="tdstyle" style="text-align: left;">
                                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="borderRadius inputDesign"
                                    OnkeyPress="javascript:Count(this,100);" Width="180px" MaxLength="100"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Your Address !"
                                    ControlToValidate="txtaddress" ValidationGroup="vldInsert" Display="Dynamic"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtaddress"
                                    CssClass="XMMessage" ErrorMessage="Special character not allowed in Address.!"
                                    ValidationExpression="^([a-zA-Z0-9_.,:;*!#`$+\[(.*?)\]()'@%?={}&//\\ \s\-]*)$"
                                    Display="Dynamic" ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <%--<td class="tdstyle fcolor" style="width: 124px">Address
                            </td>
                            <td class="tdstyle" style="text-align: left;">
                                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="borderRadius inputDesign"
                                    OnkeyPress="javascript:Count(this,100);" Width="180px" MaxLength="100"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvaddress" runat="server" ErrorMessage="Enter Your Address !"
                                    ControlToValidate="txtaddress" ValidationGroup="vldInsert" Display="Dynamic"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtaddress"
                                    CssClass="XMMessage" ErrorMessage="Special character not allowed in Address.!"
                                    ValidationExpression="^([a-zA-Z0-9_.,:;*!#`$+\[(.*?)\]()'@%?={}&//\\ \s\-]*)$"
                                    Display="Dynamic" ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>--%>
                            <td class="tdstyle fcolor" style="width: 173px">Remarks(If Any)
                            </td>
                            <td class="tdstyle fcolor" style="text-align: left" colspan="3">
                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="95%" OnkeyPress="javascript:textCounter(this,'counter',200);"
                                    CssClass="borderRadius inputDesign" MaxLength="100"></asp:TextBox>
                                <br />
                                Remaining Words:
                                <input maxlength="3" size="3" id="counter" disabled="disabled">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRemark"
                                    CssClass="XMMessage" ErrorMessage="Special character not allowed in Remarks.!"
                                    ValidationExpression="^([a-zA-Z0-9_.,//\\ \s\-]*)$" ValidationGroup="vldInsert"
                                    Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                             <%--<td class="tdstyle fcolor" style="width: 173px">
                            </td>
                             <td class="tdstyle fcolor" style="width: 173px">
                            </td>--%>
                        </tr>
                    </table>
                </div>
                <div id="divExtraDetails" class="boxshadow" style="margin-top: 8px;">
                    <table style="position: relative;" width="100%" align="center" id="TableAddExtra"
                        cellpadding="0" cellspacing="0" border="0">
                        <tr class="gridalternaterow">
                            <td valign="middle" align="left">
                                <asp:HiddenField ID="hidGRN" runat="server" />
                                <asp:Label ID="Label3" runat="server" Text="Add Extra Details on Challan :" Font-Bold="True"
                                    ForeColor="#D87E3D"></asp:Label>
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            </td>
                            <td align="right">
                                <%-- <asp:Button ID="btnaddExtra" runat="server" Text="Same as previous Details" OnClientClick="openPopupExtra();"
                                    Visible="false" />--%>
                                &nbsp&nbsp&nbsp;
                                <asp:Button ID="btnaddRemark" runat="server" Text="Add More Details" OnClientClick="openPopup();" />&nbsp&nbsp&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divCTD" class="boxshadow" style="margin-top: 8px;">
                    <table style="position: relative;" width="100%" height="80px" align="center" border="1"
                        id="Table1">
                        <tr id="trCTD" runat="server" visible="false" class="gridalternaterow">
                            <td colspan="4">
                                <table style="position: relative;" width="100%" height="80px" align="center" border="1">
                                    <tr class="gridalternaterow">
                                        <td colspan="4" valign="middle" align="left">
                                            <asp:Label ID="Label2" runat="server" Text="CTD Detail :" Font-Bold="True" ForeColor="#D87E3D"></asp:Label>
                                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                        </td>
                                    </tr>
                                    <tr class="gridalternaterow">
                                        <td class="tdstyle fcolor" style="width: 124px">
                                            <asp:Label ID="lblZone" runat="server" Text="Zone_CD :-"></asp:Label><span class="mandatory">*</span>
                                        </td>
                                        <td class="tdstyle">
                                            <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" CssClass="borderRadius inputDesign"
                                                Width="186px" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Zone"
                                                ControlToValidate="ddlZone" ValidationGroup="vldInsert" InitialValue="0" ForeColor="Red"
                                                Style="text-align: center">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td class="tdstyle fcolor" style="width: 173px">
                                            <asp:Label ID="lblCircle" runat="server" Text="Circle_CD :-"></asp:Label><span class="mandatory">*</span>
                                        </td>
                                        <td class="tdstyle">
                                            <asp:DropDownList ID="ddlCircle" runat="server" AutoPostBack="true" CssClass="borderRadius inputDesign"
                                                Width="186px" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Circle"
                                                ControlToValidate="ddlCircle" ValidationGroup="vldInsert" InitialValue="0" ForeColor="Red"
                                                Style="text-align: center">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="gridalternaterow">
                                        <td class="tdstyle fcolor" style="width: 173px">
                                            <asp:Label ID="lblWard" runat="server" Text="Ward_CD :-"></asp:Label><span class="mandatory">*</span>
                                        </td>
                                        <td class="tdstyle">
                                            <asp:DropDownList ID="ddlWard" runat="server" AutoPostBack="true" CssClass="borderRadius inputDesign"
                                                Width="186px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Select Ward"
                                                ControlToValidate="ddlWard" ValidationGroup="vldInsert" InitialValue="0" ForeColor="Red"
                                                Style="text-align: center">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="gridalternaterow">
                                        <td colspan="4">
                                            <asp:Label ID="CTDMSG" runat="server" Text="Label" Visible="false" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="gridalternaterow">
                            <td colspan="4" align="center" valign="middle">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vldInsert"
                                    ShowMessageBox="true" ShowSummary="false" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btninsert" runat="server"
                                    Text="Submit" ValidationGroup="vldInsert" OnClientClick="JavaScript:return TinWarning();"
                                    OnClick="btninsert_Click" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/WebPages/EgGuestProfile.aspx"
                                    Text="Reset" Visible="false"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="dialog" title="Basic dialog">
    <div id="dialog-text"></div>
</div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
