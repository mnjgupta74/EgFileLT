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

        //alert(document.getElementById('ctl00_ContentPlaceHolder1_txtfromdate').value);

        var day = date.getDate();
        var year = date.getFullYear();
        //var Fdate = document.getElementById("<%=txtfromdate.ClientID %>").value;
        var Fdate = document.getElementById('ctl00_ContentPlaceHolder1_txtfromdate').value;



        if (Fdate == "") {
            //document.getElementById("<%=txtfromdate.ClientID %>").value = day + '/' + month + '/' + year;
            document.getElementById('ctl00_ContentPlaceHolder1_txtfromdate').value = day + '/' + month + '/' + year;
        }
        else {
            document.getElementById('ctl00_ContentPlaceHolder1_txtfromdate').value = Fdate;
            //document.getElementById("<%=txtfromdate.ClientID %>").value = Fdate;
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
        //var Tdate = document.getElementById("<%=txttodate.ClientID %>").value;
        var Tdate = document.getElementById('ctl00_ContentPlaceHolder1_txttodate').value;

        if (Tdate == "") {
            //document.getElementById("<%=txttodate.ClientID %>").value = lastDay + '/' + month + '/' + year;
            document.getElementById('ctl00_ContentPlaceHolder1_txttodate').value = lastDay + '/' + month + '/' + year;
        }
        else {
            //document.getElementById("<%=txttodate.ClientID %>").value = Tdate;
            document.getElementById('ctl00_ContentPlaceHolder1_txttodate').value = Tdate;
        }
    }

}


function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode > 47 && charCode < 58) || charCode === 191) {
        return true;
    }
    return false;
}

function isNumberChar(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;

    if ((charCode > 47 && charCode < 58) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123)) {
        return true;
    }
    return false;
}
/* number only validation*/
function NumericOnly(el) {
    var ex = /^\s*\d+\s*$/;
    if (ex.test(el.value) == false) {
        alert('Enter Number Only');
        el.value = "";
    }
}
function AllowedChars(el) {
    var ex = /^[a-z0-9\s&_.,:;*!#`%$]*$/;
    if (ex.test(el.value) == false && el.value.length>0) {
        el.value = "";
        alert('Speial Charectors not allowed except (&_.,:;*!#`%$)');
        return false;
    }
}

function NumberOnly(evt) {
    if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
    var parts = evt.srcElement.value.split('.');
    if (parts.length > 3) return false;

    if (evt.keyCode == 46) return (parts.length == 1);

    if (parts[0].length >= 14) return false;

    if (parts.length == 3 && parts[1].length >= 3) return false;
}
function ClearValue(el) {
    if (el.value == "00") {
        el.value = "";
        el.focus();
    }
}

function SetValue(el) {
    if (el.value.length == 0) {
        el.value = "00";
    }
}

function TinWarning() {
    var vCode = document.getElementById("vCode");

    var Tel = document.getElementById('ctl00_ContentPlaceHolder1_txtTIN').value
    if (Tel == "" && vCode == "18") {
        var retVal = confirm("You have not entered TIN.Are you sure to continue?");
        if (retVal) {
            return true;
        } else {
            return false;
        }
    }
}

function CheckTIN() {
    var Tel = document.getElementById('ctl00_ContentPlaceHolder1_txtTIN').value


    var TinPat = /^[a-zA-Z0-9\/]{10,11}$/;
    var TINid = Tel;
    var matchArray = TINid.match(TinPat);
    if (matchArray == null) {
        alert("Please Enter Complete TIN.");
        document.getElementById('ctl00_ContentPlaceHolder1_HiddenField2').value = "1";
        document.getElementById('ctl00_ContentPlaceHolder1_txtTIN').value = "";
        return false;
    }
    else {
        document.getElementById('ctl00_ContentPlaceHolder1_HiddenField2').value = "2";
    }
}
function DecimalNumber(el) {
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


function ChangeCase(elem) {
    elem.value = elem.value.toUpperCase();
}

function ClearDate() {
    var fmdate = document.getElementById('ctl00_ContentPlaceHolder1_txtfromdate')
    fmdate.value = "";
}
function ClearDate1() {
    var tdate = document.getElementById('ctl00_ContentPlaceHolder1_txttodate')
    tdate.value = "";
}

function PanNumber(usertype) {
    if (usertype != "4") {
        //var amount = document.getElementById("<%=txttotalAmount.ClientID %>").value;
        var a = document.getElementById('ctl00_ContentPlaceHolder1_txtPanNo').value;
        var regex1 = /^[A-Z]{5}\d{4}[A-Z]{1}$/;  //this is the pattern of regular expersion
        if (regex1.test(a) == false && (a.length > 0)) {
            alert('Please enter valid PAN!( Ex: ABCDE1234F)');

            document.getElementById('ctl00_ContentPlaceHolder1_txtPanNo').value = "";
            //document.getElementById('ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').value = "Please enter valid PAN";
            //document.getElementById('ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').style.display = "block";
            return false;
        }
    }
    else {
        var a = document.getElementById('ctl00_ContentPlaceHolder1_txtPanNo').value;
        //var a = document.getElementById("<%=txtPanNo.ClientID %>").value;
        var regex1 = /^[A-Z]{4}\d{5}[A-Z]{1}$/;  //this is the pattern of regular expersion
        if (regex1.test(a) == false) {
            alert('Please enter valid TAN!( Ex: ABCD12345E)');
            document.getElementById('ctl00_ContentPlaceHolder1_txtPanNo').value = "";
            return false;
        }
    }
}


function dateValidation() {

    var dtObj = document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate")
    var dtStr = dtObj.value;
    var dtTemp = dtStr;


    date = new Date();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var year = date.getFullYear();

    if (dtTemp == '') {
        alert('Date cant be blank')
        //                dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = day + '/' + month + '/' + year;
        return false;
    }
    if (dtTemp.indexOf('/') == -1) {
        alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
        //dtObj.value = ""
        document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = day + '/' + month + '/' + year;
        return false;
    }
    dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
    if (dtTemp.indexOf('/') == -1) {
        alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
        // dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = day + '/' + month + '/' + year;
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
        document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = day + '/' + month + '/' + year;
        return false
    }
    if (YearDt1.length != 4) {
        alert('Invalid Date.Year should be in 4-digits.');
        //  dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = day + '/' + month + '/' + year;
        return false;
    }

    //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
    if (isNaN(DayDt1) || isNaN(MonDt1) || isNaN(YearDt1)) {
        alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
        //  dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = day + '/' + month + '/' + year;
        return false;
    }
    var DateEntered = new Date()
    DateEntered.setFullYear(YearDt1, parseInt(MonDt1 - 1), DayDt1)


    if (DateEntered.getMonth() != (parseInt(MonDt1 - 1))) {
        alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
        // dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = day + '/' + month + '/' + year;
        return false;
    }

    //Date with Current Date Comparision
    var tdate = document.getElementById("ctl00_ContentPlaceHolder1_txttodate")
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
           // dtObj.value = day + '/' + month + '/' + year;
            document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = '';
            document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = '';

        }
    }

}

function dateValidation1() {

    var dtObj = document.getElementById("ctl00_ContentPlaceHolder1_txttodate")
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
        document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = lastDay + '/' + month + '/' + year;
        return false;
    }
    if (dtTemp.indexOf('/') == -1) {
        alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.');
        //dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = lastDay + '/' + month + '/' + year;
        return false;
    }
    dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
    if (dtTemp.indexOf('/') == -1) {
        alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.');
        // dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = lastDay + '/' + month + '/' + year;
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
        document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = lastDay + '/' + month + '/' + year;
        return false
    }

    if (YearDt1.length != 4) {
        alert('Invalid Date.Year should be in 4-digits.');
        //  dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = lastDay + '/' + month + '/' + year;
        return false;
    }

    //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
    if (isNaN(DayDt1) || isNaN(MonDt1) || isNaN(YearDt1)) {
        alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
        // dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = lastDay + '/' + month + '/' + year;
        return false;
    }
    var DateEntered = new Date()
    DateEntered.setFullYear(YearDt1, parseInt(MonDt1 - 1), DayDt1)


    if (DateEntered.getMonth() != (parseInt(MonDt1 - 1))) {
        alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.");
        // dtObj.value = "";
        document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = lastDay + '/' + month + '/' + year;
        return false;
    }

    //Date with Current Date Comparision
    var fdate = document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate")
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
            //document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = lastDay + '/' + month + '/' + year;
            document.getElementById("ctl00_ContentPlaceHolder1_txtfromdate").value = '';
            document.getElementById("ctl00_ContentPlaceHolder1_txttodate").value = '';
        }
    }
}

function Count(text, long) {
    var maxlength = new Number(long); // Change number to your max length.
    if (document.getElementById('ctl00_ContentPlaceHolder1_txtRemark').value.length > maxlength) {
        text.value = text.value.substring(0, maxlength);
        return false;
    }

    if (document.getElementById('ctl00_ContentPlaceHolder1_txtaddress').value.length > maxlength) {
        text.value = text.value.substring(0, maxlength);
        return false;
    }
}

function textCounter(field, field2, maxlimit) {
    var countfield = document.getElementById(field2);
    if (field.value.length > maxlimit) {
        field.value = field.value.substring(0, maxlimit);
        alert('Maximum 200 Charectors allowed !');
        return false;
    } else {
        countfield.value = maxlimit - field.value.length;
    }
}


//function updateValue(txtID) {
//    // $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').hide();
//    var res = 0;
//    var inputs = [];
//    var totalbox = 0;
//    var i;
//    inputs = document.getElementById('ctl00_ContentPlaceHolder1_divBHead').getElementsByTagName("input");
//    for (i = 0; i < inputs.length; i++) {
//        if (i != "length") {
//            if (isNaN(inputs[i].value) == true || inputs[i].value.length < 1)
//                continue;
//            else
//                if (inputs[i].value != "00") {
//                    res = res + parseFloat(inputs[i].value);
//                    $('#spanbreakup').hide();
//                }
//        }
//    }
//    $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').hide();
//    if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value <= "0") {
//        document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "0"
//        document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = "0"
//    }

//    if (document.getElementById('ctl00_ContentPlaceHolder1_hdnDecuctAmount').value == "1" && document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value != "") {

//        var resPercent = (20 / 100) * res;
//        //alert(resPercent);
//        //alert(document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value);

//        if (document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value > resPercent) {
//            //alert('Commission Amount not allowed more than 20% of Total Amount!');
//            $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').show();
//            document.getElementById('ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').innerText = "Commission Amount not allowed more than 20% of Total Amount!"
//            //document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "00";
//            document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res
//            document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').focus();
//            return false;
//        }


//        document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res - document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value;
//        var num = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
//        var result = parseFloat(num).toFixed(2);
//        document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = result;
//    }
//    else {
//        document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res;
//        var num = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
//        var result = parseFloat(num).toFixed(2);
//        document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = result;
//    }

//    var getId = document.getElementById(txtID).value;

//    if (getId == "" || getId == ".") {
//        document.getElementById(txtID).value = "00";
//    }

//    //if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value >= 50000) //spanpantan
//    //{
//    //    document.getElementById("ctl00_ContentPlaceHolder1_spanpantan").style.display = "block";
//    //} else {
//    //    document.getElementById("ctl00_ContentPlaceHolder1_spanpantan").style.display = "none";
//    //}
//    //AmountInWords(document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value);
//}

//function checkamounttotal(e) {
//    if (document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value < 0) {
//        alert('Invalid Amount');
//        document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "";
//        return false;
//    }
//    else {
//        var res = 0;
//        var inputs = [];
//        var totalbox = 0;
//        var i;
//        inputs = document.getElementById('ctl00_ContentPlaceHolder1_divBHead').getElementsByTagName("input");
//        for (i = 0; i < inputs.length; i++) {
//            if (i != "length") {
//                if (isNaN(inputs[i].value) == true || inputs[i].value.length < 1)
//                    continue;
//                else
//                    if (inputs[i].value != "00") {
//                        res = res + parseFloat(inputs[i].value);
//                    }
//            }
//        }
//        $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').hide();
//        if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value <= "0") {
//            document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "0"
//            document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = "0"
//        }

//        if (document.getElementById('ctl00_ContentPlaceHolder1_hdnDecuctAmount').value == "1" && document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value != "") {
//            var resPercent = (20 / 100) * res;
//            //alert(resPercent);
//            //alert(res);
//            if (document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value > resPercent) {
//                //alert('Commission Amount not allowed more than 20% of Total Amount!');
//                $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').show();
//                document.getElementById('ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').innerText = "Commission Amount not allowed more than 20% of Total Amount!"
//                document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "00";
//                document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').focus();
//                return false;
//            }
//            //alert(res - document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value);
//            document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res - document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value;
//            var num = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
//            var result = parseFloat(num).toFixed(2);
//            document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = result;
//        }
//        else {
//            document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res
//            var num = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
//            var result = parseFloat(num).toFixed(2);
//            document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = result;
//        }
//        if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value >= 50000) //spanpantan
//        {
//            document.getElementById("ctl00_ContentPlaceHolder1_spanpantan").style.display = "block";
//            //document.getElementById("ctl00_ContentPlaceHolder1_txtPanNo").focus();
//        } else {
//            document.getElementById("ctl00_ContentPlaceHolder1_spanpantan").style.display = "none";
//        }
//        //AmountInWords(document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value);
//    }
//}
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
        document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "";
        document.getElementById('ctl00_ContentPlaceHolder1_txtamountwords').value = "";
        document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = "";
        return false;

    }

    if (Number(value1) == 0 && Number(value2) == 0) {


        document.getElementById('ctl00_ContentPlaceHolder1_txtamountwords').value = 'Rupees Zero Only';

        return false;

    }

    if (actnumber.length > 14) {

        alert('Oops!!!! the Number is too big to covertes');
        document.getElementById('ctl00_ContentPlaceHolder1_txtamountwords').value = "";
        document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = "";
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

                        inWords[j] = inWords[j] + " Thousand ";

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

                        inWords[j] = inWords[j] + " Lakh ";

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

                        inWords[j] = inWords[j] + " Crore ";

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
            document.getElementById('ctl00_ContentPlaceHolder1_txtamountwords').value = finalWord;
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
                document.getElementById('ctl00_ContentPlaceHolder1_txtamountwords').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = "";
                return false;

            }

            if (Number(value2) == 0) {


                document.getElementById('ctl00_ContentPlaceHolder1_txtamountwords').value2 = 'Rupees Zero Only';

                return false;

            }

            if (actnumber.length > 14) {

                alert('Oops!!!! the Number is too big to covertes');
                document.getElementById('ctl00_ContentPlaceHolder1_txtamountwords').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = "";
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


    document.getElementById('ctl00_ContentPlaceHolder1_txtamountwords').value = finalWord + con + finalWord1;
    document.getElementById('ctl00_ContentPlaceHolder1_HiddenAmount').value = finalWord + con + finalWord1;
}


function CallMe(a) {
    var result = a;
    document.getElementById('ctl00_ContentPlaceHolder1_HiddenField1').value = result;
}



function openPopup() {
    //debugger;
    //alert(document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value);
    if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value != "" && document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value != 00 && document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value != 0) {
        var grn = document.getElementById('ctl00_ContentPlaceHolder1_hidGRN').value;
        var totamt = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
        var argObj = window;
        $.ajax({
            type: 'POST',
            url: "EgEchallan.aspx/EncryptData",
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
        //alert('Please Enter BudgetHead Amount.!');
        //document.getElementById('spanbreakup').value = 'Please Enter BudgetHead Amount.!';
        //$('#spanbreakup').val('Please Enter BudgetHead Amount.!')
        $('#spanbreakup').show();
    }
}
function AssignBudgetheadwiseValue(list) {
    var jsonp = JSON.stringify(list);
    var obj = $.parseJSON(jsonp);
    //alert(JSON.stringify(obj));
    $.each(obj, function () {
        document.getElementById(this["id"]).value = this["value"];
    });


    //alert(amount)
    //document.getElementById("TextBox_"+i).value = amount;
}

//$(function () {
//    $(".chzn-select").chosen({
//        search_contains: true
//    });
//    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
//    function EndRequestHandler(sender, args) {
//        //Binding Code Again
//        $(".chzn-select").chosen({
//            search_contains: true
//        });
//    }
//});