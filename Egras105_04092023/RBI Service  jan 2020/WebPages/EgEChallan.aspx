<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgEChallan.aspx.cs" Inherits="WebPages_EgEChallan" %>

<%@ Register Src="~/UserControls/FinancialYearDropDown.ascx" TagName="FinYear" TagPrefix="ucl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.css"></link>
    <link href="../CSS/bootstrap4/bootstrap.min.css" rel="stylesheet" />
    <script src="../js/bootstrap4/bootstrap.min.js"></script>
    <link href="../CSS/EgEchallan.css" rel="stylesheet" type="text/css" />
     <script src="../js/EgeChallan.js"></script>
     <script type="text/javascript">

         function updateValue(txtID) {
             // $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').hide();
             var res = 0;
             var inputs = [];
             var totalbox = 0;
             var i;
             inputs = document.getElementById('ctl00_ContentPlaceHolder1_divBHead').getElementsByTagName("input");
             for (i = 0; i < inputs.length; i++) {
                 if (i != "length") {
                     if (isNaN(inputs[i].value) == true || inputs[i].value.length < 1)
                         continue;
                     else
                         if (inputs[i].value != "00") {
                             res = res + parseFloat(inputs[i].value);
                             $('#spanbreakup').hide();
                         }
                 }
             }
             $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').hide();
             if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value <= "0") {
                 document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "0"
                 document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = "0"
             }

             if (document.getElementById('ctl00_ContentPlaceHolder1_hdnDecuctAmount').value == "1" && document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value != "") {

                 var resPercent = (20 / 100) * res;
                 //alert(resPercent);
                 //alert(document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value);

                 if (document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value > resPercent) {
                     //alert('Commission Amount not allowed more than 20% of Total Amount!');
                     $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').show();
                     document.getElementById('ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').innerText = "Commission Amount not allowed more than 20% of Total Amount!"
                     //document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "00";
                     document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res
                     document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').focus();
                     //return false;
                 }
                 

                     document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res - document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value;
                     var num = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
                     var result = parseFloat(num).toFixed(2);
                     document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = result;
                 
             }
             else {
                 document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res;
                 var num = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
                 var result = parseFloat(num).toFixed(2);
                 document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = result;
             }

             var getId = document.getElementById(txtID).value;

             if (getId == "" || getId == ".") {
                 document.getElementById(txtID).value = "00";
             }

             //if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value >= 50000) //spanpantan
             //{
             //    document.getElementById("ctl00_ContentPlaceHolder1_spanpantan").style.display = "block";
             //} else {
             //    document.getElementById("ctl00_ContentPlaceHolder1_spanpantan").style.display = "none";
             //}
             //AmountInWords(document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value);
         }

         function checkamounttotal(e) {
             if (document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value < 0) {
                 alert('Invalid Amount');
                 document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "";
                 return false;
             }
             else {
                 var res = 0;
                 var inputs = [];
                 var totalbox = 0;
                 var i;
                 inputs = document.getElementById('ctl00_ContentPlaceHolder1_divBHead').getElementsByTagName("input");
                 for (i = 0; i < inputs.length; i++) {
                     if (i != "length") {
                         if (isNaN(inputs[i].value) == true || inputs[i].value.length < 1)
                             continue;
                         else
                             if (inputs[i].value != "00") {
                                 res = res + parseFloat(inputs[i].value);
                             }
                     }
                 }
                 $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').hide();
                 if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value <= "0") {
                     document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "0"
                     document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = "0"
                 }

                 if (document.getElementById('ctl00_ContentPlaceHolder1_hdnDecuctAmount').value == "1" && document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value != "") {
                     var resPercent = (20 / 100) * res;
                     //alert(resPercent);
                     //alert(res);
                     if (document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value > resPercent) {
                         //alert('Commission Amount not allowed more than 20% of Total Amount!');
                         $('#ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').show();
                         document.getElementById('ctl00_ContentPlaceHolder1_spanBudgetHeadMsg').innerText = "Commission Amount not allowed more than 20% of Total Amount!"
                         document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value = "00";
                         document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').focus();
                        // return false;
                     }
                     //alert(res - document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value);
                     document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res - document.getElementById('ctl00_ContentPlaceHolder1_txtDeduct').value;
                     var num = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
                     var result = parseFloat(num).toFixed(2);
                     document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = result;
                 }
                 else {
                     document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = res
                     var num = document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value;
                     var result = parseFloat(num).toFixed(2);
                     document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value = result;
                 }
                 if (document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value >= 50000) //spanpantan
                 {
                     document.getElementById("ctl00_ContentPlaceHolder1_spanpantan").style.display = "block";
                     //document.getElementById("ctl00_ContentPlaceHolder1_txtPanNo").focus();
                 } else {
                     document.getElementById("ctl00_ContentPlaceHolder1_spanpantan").style.display = "none";
                 }
                 //AmountInWords(document.getElementById('ctl00_ContentPlaceHolder1_txttotalAmount').value);
             }
         }

     </script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_rblpayMode tbody tr td {
            padding-right: 84px !important;
        }

        #ctl00_ContentPlaceHolder1_ddlpdAccount_chosen {
            font-size: 15px;
        }

        /*#msform .action-button:hover, #msform .action-button:focus {
            background-color: #0e5f85 !important;
        }*/

        #ctl00_ContentPlaceHolder1_inputBreakup:hover, #ctl00_ContentPlaceHolder1_inputBreakup:focus {
            color: #0e5f85 !important;
            margin-right: 10px;
        }

        #msform .action-button:hover, #msform .action-button:focus {
            background-color: #0e5f85 !important;
        }

        #msform .action-button-previous:hover, #msform .action-button-previous:focus {
            background-color: #0e5f85 !important;
        }

        .CalendarCSS {
            width: 90%;
            border: 1px solid;
        }

        .ajax__calendar_container {
            width: 100% !important;
            font-size: 10px !important;
        }

        #ctl00_ContentPlaceHolder1_ddlbankname_chosen {
            font-size: 15px;
        }

        .side_nav {
            font-size: 15px;
            font-weight: 400;
            word-wrap: break-word;
        }

        #msform input:focus, #msform textarea:focus {
            border: 1px solid #20b3f9;
        }

        .side_nav_active {
            background: #ffffff;
            border-right: 4px solid #39A0D9;
            pointer-events: none;
            color: #76BDE4;
        }

        .side_nav span.active label {
            background: #ffffff;
            border-right: 4px solid #39A0D9;
            pointer-events: none;
            color: #76BDE4;
        }

        .side_nav label {
            list-style: none;
            border: 1px solid #F1F1F1;
            border-image: url("image/arrow.png") 100% round;
        }

        .side_nav span label {
            list-style: none;
            border: 1px solid #F1F1F1;
            border-image: url("image/arrow.png") 100% round;
        }

        .side_nav label {
            text-decoration: none;
            display: block;
            padding: 6% 6%;
        }

        .side_nav span label {
            text-decoration: none;
            display: block;
            padding: 6% 6%;
        }
        /*@media screen and (max-width: 920px) {
            body {
                background-color: red;
            }
        }*/

        /*@media screen and (max-width: 720px) {
            #ddlTreasury #ddlOfficeName #ddllocation{
                width:70%;
            }
        }*/
        /*input[name="ctl00$ContentPlaceHolder1$rblBank"] {
            display: none;
        }*/
        /*#ctl00_body{
            font-size:15px;
        }*/
        #ctl00_ContentPlaceHolder1_rblBank tbody tr td label img, #ctl00_ContentPlaceHolder1_rblPG tbody tr td label img, #ctl00_ContentPlaceHolder1_rblUpi tbody tr td img {
            padding-right: 4px !important;
        }

        #msform {
            font-size: 15px;
        }

        input[name="ctl00$ContentPlaceHolder1$rblBank"], input[name="ctl00$ContentPlaceHolder1$rblPG"], input[name="ctl00$ContentPlaceHolder1$rblUpi"] {
            width: auto !important;
        }

        #ctl00_ContentPlaceHolder1_rblBank tbody tr td input[type=radio] {
            width: auto !important;
        }
        /*[for=ctl00_ContentPlaceHolder1_rblUpi_0]*/
        #ctl00_ContentPlaceHolder1_rblBank tbody tr td label, #ctl00_ContentPlaceHolder1_rblPG tbody tr td label, #ctl00_ContentPlaceHolder1_rblUpi tbody tr td label {
            margin-top: -20px !important;
            margin-left: 22px !important;
        }

        #ctl00_ContentPlaceHolder1_rblBank tbody tr td, #ctl00_ContentPlaceHolder1_rblPG tbody tr td {
            padding: 20px !important;
        }


        #ctl00_ContentPlaceHolder1_rblBank, #ctl00_ContentPlaceHolder1_rblPG, #ctl00_ContentPlaceHolder1_rblUpi {
            float: left;
            width: 100% !important;
        }
        /*end*/
        .chosen-container chosen-container-single {
            width: 100% !important;
        }


        /*#ctl00_ContentPlaceHolder1_rblUpi {
            float: right;
        }*/

        .btnHeight {
            height: 35px !important;
            padding-top: 6px !important;
        }

        textarea {
            padding-top: 0px !important;
        }

        .form-card {
            margin-top: -20px !important;
        }

        .col_color {
            background-color: #F8FBFD;
            font-weight: bold;
            font-size: 16px !important;
        }

        .chosen-single {
            height: 30px !important;
        }

        #ctl00_ContentPlaceHolder1_ddldistrict_chosen {
            font-size: 16px !important;
        }

        #ctl00_ContentPlaceHolder1_ddlOfficeName_chosen {
            font-size: 16px !important;
        }

        #ctl00_ContentPlaceHolder1_ddllocation_chosen {
            font-size: 16px !important;
        }

        #ctl00_ContentPlaceHolder1_ddlPeriod_chosen {
            font-size: 16px !important;
            width: 160px !important;
        }

        #ctl00_ContentPlaceHolder1_ddlpdAccount_chosen {
            float: right;
        }

        .formBox {
            border: 3px solid #ecf1f5 !important;
            box-shadow: none !important;
            width: 100%;
            margin-top: 10px;
        }

        .bgHeading {
            background-color: #20b3f9;
        }

        #divBankSection {
            margin-top: -1.5rem !important;
            margin-left: 0.3rem !important;
        }

        #ctl00_ContentPlaceHolder1_ddlbankname_chosen {
            width: 100% !important;
        }

        .content-center {
            margin-left: 0;
        }

        .container-fluid {
            padding-right: 13px;
        }

        @media screen and (max-width: 1200px) {
            #progressbar li:before {
                width: 30px;
                height: 30px;
                line-height: 25px;
                display: block;
                font-size: 20px;
                color: #ffffff;
                background: lightgray;
                border-radius: 50%;
                margin: 0 auto 10px auto;
                /*padding: 2px;*/
            }

            .formBox {
                border: 1px solid #ecf1f5 !important;
                box-shadow: none !important;
                width: 100%;
            }

            #progressbar li:after {
                top: 16px;
            }

            #ctl00_ContentPlaceHolder1_ddlTreasury_chosen {
                width: 90% !important;
            }

            #ctl00_ContentPlaceHolder1_ddlOfficeName_chosen {
                width: 90% !important;
            }

            #ctl00_ContentPlaceHolder1_ddllocation_chosen {
                width: 90% !important;
            }

            #ctl00_ContentPlaceHolder1_ddlPeriod_chosen {
                width: 160px !important;
                margin-bottom: 5px !important;
            }

            #ctl00_ContentPlaceHolder1_ddlbankname_chosen {
                width: 100% !important;
            }
        }

        @media screen and (max-width: 768px) {
            #progressbar li:before {
                width: 30px;
                height: 30px;
                line-height: 25px;
                display: block;
                font-size: 20px;
                color: #ffffff;
                background: lightgray;
                border-radius: 50%;
                margin: 0 auto 10px auto;
                /*padding: 2px;*/
            }

            .marginBox {
                margin-top: -32px;
            }

            .formBox {
                border: 1px solid #ecf1f5 !important;
                box-shadow: none !important;
                width: 100%;
            }

            .form-card {
                margin-top: -20px;
            }

            #progressbar li:after {
                top: 16px;
            }



            #ctl00_ContentPlaceHolder1_ddlTreasury_chosen {
                width: 90% !important;
            }

            #ctl00_ContentPlaceHolder1_ddlOfficeName_chosen {
                width: 90% !important;
            }

            #ctl00_ContentPlaceHolder1_ddllocation_chosen {
                width: 90% !important;
            }

            /*#ctl00_ContentPlaceHolder1_ddlPeriod_chosen {
                width: 160px !important;
                margin-bottom: 5px !important;
            }*/

            #ctl00_ContentPlaceHolder1_ddlbankname_chosen {
                width: 100% !important;
            }
            /*#ctl00_ContentPlaceHolder1_ddlTreasury #ctl00_ContentPlaceHolder1_ddlOfficeName #ctl00_ContentPlaceHolder1_ddllocation {
                width:70%;
            }*/
        }

        @media screen and (max-width: 540px) {
            #progressbar li:before {
                width: 30px;
                height: 30px;
                line-height: 25px;
                display: block;
                font-size: 20px;
                color: #ffffff;
                background: lightgray;
                border-radius: 50%;
                margin: 0 auto 10px auto;
                /*padding: 2px;*/
            }

            .marginBox {
                margin-top: -32px;
            }

            .formBox {
                border: 1px solid #ecf1f5 !important;
                box-shadow: none !important;
                width: 100%;
            }

            #progressbar li:after {
                top: 16px;
            }

            .mobhide {
                display: none !important;
            }

            #ctl00_ContentPlaceHolder1_ddlTreasury_chosen {
                width: 90% !important;
            }

            #ctl00_ContentPlaceHolder1_ddlOfficeName_chosen {
                width: 90% !important;
            }

            #ctl00_ContentPlaceHolder1_ddllocation_chosen {
                width: 90% !important;
            }

            /*#ctl00_ContentPlaceHolder1_ddlPeriod_chosen {
                width: 160px !important;
                margin-bottom: 5px !important;
            }*/

            #ctl00_ContentPlaceHolder1_ddlbankname_chosen {
                width: 100% !important;
            }
            /*#ctl00_ContentPlaceHolder1_ddlTreasury #ctl00_ContentPlaceHolder1_ddlOfficeName #ctl00_ContentPlaceHolder1_ddllocation {
                width:70%;
            }*/
        }

        @media screen and (max-width: 360px) {
            #progressbar li:before {
                width: 30px;
                height: 30px;
                line-height: 25px;
                display: block;
                font-size: 20px;
                color: #ffffff;
                background: lightgray;
                border-radius: 50%;
                margin: 0 auto 10px auto;
                padding: 2px;
            }

            #divBankSection {
                margin-top: 0px !important;
                margin-left: 0px !important;
            }

            #ctl00_ContentPlaceHolder1_ddlbankname_chosen {
                width: 100% !important;
            }
            /*#Neft,#UPI,#paymentgateway,#manual,#online{
                height:40px !important;
            }*/

            .form-card {
                margin-top: -20px !important;
            }

            .btnHeight {
                height: 35px;
            }

            .fontSet {
                font-size: 12px;
            }

            .fontSetHead {
                font-size: 16px;
            }

            .form-control {
                width: 90% !important;
                font-size: 11px !important;
            }

            .breakLine {
                width: 20% !important;
            }

            .bgHeading {
                display: block !important;
            }

            .marginBox {
                margin-top: -32px;
            }

            .formBox {
                border: 1px solid #ecf1f5 !important;
                box-shadow: none !important;
                width: 100%;
            }
            /*#ctl00_ContentPlaceHolder1_ddlTreasury #ctl00_ContentPlaceHolder1_ddlOfficeName #ctl00_ContentPlaceHolder1_ddllocation{
                width:60%;
            }*/
            .mobhide {
                display: none !important;
            }

            #ctl00_ContentPlaceHolder1_fldBudget {
                margin-top: -20px;
            }

            .spanpurpose {
                /*display: none !important;*/
                position: absolute;
                top: 22px;
                font-size: 10px !important;
            }

            .widthSet {
                font-size: 14px !important;
                width: 94% !important;
                height: 24px !important;
            }

            .widthSetPI {
                font-size: 14px !important;
                width: 94% !important;
                height: 24px !important;
            }

            #ctl00_ContentPlaceHolder1_ddlZone_chosen, #ctl00_ContentPlaceHolder1_ddlCircle_chosen, #ctl00_ContentPlaceHolder1_ddlWard_chosen {
                width: 98% !important;
                margin-left: 15px !important;
            }

            #ctl00_ContentPlaceHolder1_ddlTreasury_chosen {
                width: 90% !important;
                font-size: 16px !important;
            }

            #ctl00_ContentPlaceHolder1_ddlOfficeName_chosen {
                width: 90% !important;
                font-size: 16px !important;
            }

            #ctl00_ContentPlaceHolder1_ddllocation_chosen {
                width: 90% !important;
                font-size: 16px !important;
            }

            /*#ctl00_ContentPlaceHolder1_ddlPeriod_chosen {
                width: 160px !important;
                margin-bottom: 5px !important;
                font-size: 16px !important;
            }*/
            /*#ctl00_ContentPlaceHolder1_txtChequeDDNo
            {
                margin-right:0px !important;
                width:100% !important;
            }*/
            .txtCheque {
                margin-right: 0px !important;
                width: 100% !important;
            }

            #progressbar li:after {
                top: 16px;
            }

            .txtPanNo {
                width: 100% !important;
            }
        }
    </style>

    <script type="text/javascript" language="javascript">
        function PanNumberValidation() {
            PanNumber("<%= Session["UserType"].ToString() %>");
        }
    </script>

    <script type="text/javascript">

        function CallBankDropdown() {
            var ddlvalue = $("#<%=ddlbankname.ClientID %> option:selected").val();
            document.getElementById("ctl00_ContentPlaceHolder1_spanBankName").innerText = $("#<%=ddlbankname.ClientID %> option:selected").text();
            var paymenttypevalue = $('#ctl00_ContentPlaceHolder1_rblpaymenttype input[type=radio]:checked').val();
            var pgalue = $('#ctl00_ContentPlaceHolder1_rblPG input[type=radio]:checked').val();
            document.getElementById("divNetBanking").innerText = (paymenttypevalue == 5 && (pgalue == "1000132")) ? "Rs 5 per transaction except SBI & associated banks" : "0";

        }

        function displayBankNameFromNetBanking() {
            var SelectedValue = $('#ctl00_ContentPlaceHolder1_rblBank input[type=radio]:checked').val();
            var ddlvalue = $("#<%=ddlbankname.ClientID %> option:selected").val();
            document.getElementById("ctl00_ContentPlaceHolder1_spanBankName").innerText = ddlvalue > 0 ? $("#<%=ddlbankname.ClientID %> option:selected").text() : $('#ctl00_ContentPlaceHolder1_rblBank input[type=radio]:checked').next('label').text();
            <%--var totalamount = $("#<%=txttotalAmount.ClientID %>").val();
            $("#<%=btnBank.ClientID %>").val(ddlvalue > 0 ? "PAY- ₹" + totalamount + " " + $("#<%=ddlbankname.ClientID %> option:selected").text() : "PAY- ₹" + totalamount + " " + $('#ctl00_ContentPlaceHolder1_rblBank input[type=radio]:checked').next('label').text());--%>
        }

        function displayBankNameFromPG() {
            var SelectedValue = $('#ctl00_ContentPlaceHolder1_rblPG input[type=radio]:checked').val();
            var paymenttypevalue = $('#ctl00_ContentPlaceHolder1_rblpaymenttype input[type=radio]:checked').val();
            document.getElementById("divNetBanking").innerText = (paymenttypevalue == 5 && SelectedValue == "1000132") ? "Rs 5 per transaction except SBI & associated banks" : "0";
            document.getElementById("ctl00_ContentPlaceHolder1_spanBankName").innerText = $('#ctl00_ContentPlaceHolder1_rblPG input[type=radio]:checked').next('label').text();
            <%--var totalamount = $("#<%=txttotalAmount.ClientID %>").val();
            $("#<%=btnBank.ClientID %>").val(ddlvalue > 0 ? "PAY- ₹" + totalamount + " " + $("#<%=ddlbankname.ClientID %> option:selected").text() : "PAY- ₹" + totalamount + " " + $('#ctl00_ContentPlaceHolder1_rblBank input[type=radio]:checked').next('label').text());--%>
        }

        function StampPopup() {
            $('#myModal').modal('show');
        }
        <%--function CallRbl() {
            $('#<%=drpUpiID.ClientID %>  option').remove();
            var SelectedValue = $('#ctl00_ContentPlaceHolder1_rblUpi input[type=radio]:checked').val();
            var PhonePay = [
                { GooglePayId: 1, Name: "@okaxis" },
                { GooglePayId: 2, Name: "@ibl" },
                { GooglePayId: 3, Name: "@axl" },
            ];
            var GPay = [
                { GooglePayId: 4, Name: "@ybl" },
                { GooglePayId: 5, Name: "@okhdfcbank" },
                { GooglePayId: 6, Name: "@okicici" },
                { GooglePayId: 7, Name: "@oksbi" }
            ];
            var Paytm = [
                { GooglePayId: 8, Name: "@paytm" }
            ];
            var bhim = [
                            { GooglePayId: 9, Name: "@paytm" }
            ];
            var GooglePay = SelectedValue == 99300011 ? Paytm : SelectedValue == 99300012 ? GPay : SelectedValue == 99300013 ? PhonePay : bhim;
            $('#divShowHide').show();
            var ddlUpi = $('#<%=drpUpiID.ClientID %>');
            $(GooglePay).each(function () {
                var option = $("<option />");
                //Set Customer Name in Text part.
                option.html(this.Name);
                //Set Customer CustomerId in Value part.
                option.val(this.GooglePayId);
                //Add the Option element to DropDownList.
                ddlUpi.append(option);
            });
        }--%>
    </script>

    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
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
            <asp:HiddenField ID="HdnProfile" runat="server" />
            <asp:HiddenField ID="HiddenAmount" runat="server" />
            <asp:HiddenField ID="hdnServiceId" runat="server" />
            <asp:HiddenField ID="hdnProcUserId" runat="server" />
             <asp:HiddenField ID="hdnDecuctAmount" runat="server" value="2"  />
            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">Stamp Judicial And Non Judicial</h4>
                        </div>
                        <div class="modal-body">
                            वर्तमान में निम्न नॉन ज्युडिशियल बजट मदों के साथ तीन पृथक-पृथक अधिभार वसूल किए जा रहे है :-
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
                            ये तीनों अधिभार स्वत: ही मुद्रित होकर तथा इनकी मुद्रांक कर देय 10-10 प्रत्तिशत राशि भी स्वत: ही मुद्रित हो जिससे उक्त तीनो बजट मद एवं उनकी सम्पूर्ण राशि जमा कराये जाने पर ही बैंक द्वारा मैनुअल अथवा इलेक्ट्रोनिक चालान स्वीकार किए जावें – "
                    <br />
                            1.	00300-02-800-(02) – स्टाम्प शुल्क पर अधिभार
                    <br />
                            2.	00300-02-800-(03) – स्टाम्प शुल्क गौ-संवर्धन / संरक्षण हेतु अधिभार
                     <br />
                            3.	00300-02-800-(04) – प्राकृतिक एवं मानव निर्मित आपदाओं से राहत हेतु अधिभार
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" style="padding: 0px; width: 15%; height: 15%;" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>
            <!-- Modal Popup -->

            <div class="container-fluid">
                <div class="content-center">
                    <div>
                        <div>
                            <!-- <h2 id="heading">Sign Up Your User Account</h2> -->
                            <!-- <p>Fill all form field to go to next step</p> -->
                            <div id="msform">
                                <div class="row">
                                    <div class="col-12 col-md-12">
                                        <div style="background: #20b3f9; margin-bottom: 4px; color: white; font-size: 20px;">E-CHALLAN</div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-12">
                                        <!-- progressbar -->
                                        <ul id="progressbar">
                                            <li runat="server" class="active" id="account"><strong class="mobhide">Service For</strong></li>
                                            <li runat="server" id="budgethead1"><strong class="mobhide">Purpose For</strong></li>
                                            <li runat="server" id="personal"><strong class="mobhide">Remitter Info</strong></li>
                                            <li runat="server" id="bank"><strong class="mobhide">Payment</strong></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-12 formBox">

                                        <div class="row">
                                            <!-- fieldsets -->
                                            <fieldset runat="server" id="fldBasic">
                                                <div class="form-card">
                                                    <div class="row">
                                                        <div class="col-12 col-lg-12 col-sm-12 col-md-12  col_color">
                                                            <h5><span style="color: white; background: #20b3f9; padding-left: 10px; padding-right: 10px;" class="fontSetHead">For:</span><span id="txtDept" style="padding-left: 5px;" class="fontSetHead" runat="server"></span></h5>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12">
                                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 " style="font-size: 16px;">District</div>
                                                            <%--<asp:DropDownListX ID="ddldistrict" class="chzn-select drpWidth" runat="server" AutoPostBack="true"
                                                                Width="60%" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0" Text="--Select District--"></asp:ListItem>
                                                            </asp:DropDownListX>--%>
                                                            <asp:DropDownList ID="ddldistrict" class="chzn-select drpWidth" runat="server"
                                                                Width="60%" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="--Select Service Obtain From Office--"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddldistrict" runat="server" ErrorMessage="Select District"
                                                                ControlToValidate="ddldistrict" ValidationGroup="vldBI" InitialValue="0"
                                                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                    <div class="row">

                                                        <div class="col-12 col-sm-12 col-md-12">
                                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12  " style="font-size: 16px;">Service Obtain From Office:</div>
                                                            <asp:DropDownList ID="ddlOfficeName" class="chzn-select drpWidth" runat="server"
                                                                Width="60%" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="--Select Service Obtain From Office--"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvOfficeName" runat="server" ErrorMessage="Select Service Obtain From Office"
                                                                ControlToValidate="ddlOfficeName" ValidationGroup="vldBI" InitialValue="0"
                                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-12 col-sm-12 col-md-12">
                                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 " style="font-size: 16px;">Treasury</div>
                                                            <asp:DropDownListX ID="ddllocation" class="chzn-select drpWidth"  runat="server" Width="60%">
                                                                <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                                                            </asp:DropDownListX>
                                                            <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Select Treasury"
                                                                ControlToValidate="ddllocation" ValidationGroup="vldBI" InitialValue="0"
                                                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-7 col-md-8 col-lg-8">
                                                            <div class="row" style="margin-left: 0px !important;">
                                                                <div class="col-12 col-sm-12 col-md-12 col-lg-12">Duration</div>
                                                                <div class="col-3 col-sm-12 col-md-2 col-lg-2">From Date:</div>
                                                                <div class="col-3 col-sm-12 col-md-4 col-lg-4" style="font-size: 16px;">

                                                                    <asp:TextBox ID="txtfromdate" class="form-control widthSet" runat="server" Width="65%" Height="30px" onkeypress="Javascript:return isNumber(event)"
                                                                        onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)" Style="font-size: 16px;">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                                        runat="server" ErrorMessage="Please Enter From Date" ControlToValidate="txtfromdate"
                                                                        ValidationGroup="vldBI" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                    <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtfromdate" CssClass="CalendarCSS">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                                                        CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                </div>
                                                                <div class="col-3 col-sm-12 col-md-2 col-lg-2">To Date :</div>
                                                                <div class="col-3 col-sm-12 col-md-4 col-lg-4" style="font-size: 16px;">

                                                                    <%--<div class="col-8 col-sm-3 col-md-3 col-lg-3">--%>
                                                                    <asp:TextBox ID="txttodate" class="form-control widthSet" runat="server" Width="65%" Height="30px" onkeypress="Javascript:return isNumber(event)"
                                                                        onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)" Style="font-size: 16px;"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                                        runat="server" ErrorMessage="Please Enter To Date" ControlToValidate="txttodate"
                                                                        ValidationGroup="vldBI" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                    <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txttodate" CssClass="CalendarCSS">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                                                                        CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                </div>
                                                            </div>


                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-4 col-sm-12 col-md-4">
                                                            <asp:ValidationSummary ID="ValidationSummary_SignupForm"
                                                                ShowMessageBox="false" runat="server"
                                                                DisplayMode="BulletList" ShowSummary="true" Width="450"
                                                                ForeColor="Red" Font-Size="14px" Style="padding-left: 20px" ValidationGroup="vldBI" />
                                                        </div>
                                                        <div class="col-lg-3 col-sm-12 col-md-3">
                                                        </div>
                                                        <div class="col-12 col-lg-4 col-sm-12 col-md-4">
                                                            <asp:Button ID="btnBasicInformation" class="next action-button btnHeight" ValidationGroup="vldBI" Text="Next" runat="server" OnClick="btnBasicInformation_Click" />
                                                        </div>
                                                        <div class="col-lg-1 col-sm-12 col-md-1">
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>

                                            <fieldset runat="server" id="fldBudget" visible="false">
                                                <div class="form-card">
                                                    <div class="row">
                                                        <div class="col-12 col-lg-12 col-sm-12 col-md-12">
                                                            <h5><span style="color: white; background: #20b3f9; padding-left: 10px; padding-right: 10px;" class="fontSetHead">For:</span><span id="spanbhdepartmentname" style="padding-left: 5px;" class="fontSetHead" runat="server"></span></h5>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12">
                                                            <div id="divBHead" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 ">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px" runat="server" id="divdeduct" visible="false">
                                                            <div class="row">
                                                                <div class="col-6 col-sm-6 col-md-6 col-lg-6" style="font-size: 15px; font-weight: 600; margin-top: -6px;">
                                                                    <span style="color: white; background: #20b3f9; font-size: 17px; font-weight: 600; padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px" class="fontSetHead">Discount:</span>
                                                                </div>
                                                                <div class="col-5 col-sm-5 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:TextBox Enabled="False" ID="txtDeduct" runat="server" MaxLength="9" AutoComplete="Off" CssClass="txtPanNo widthSet"
                                                                        Style="text-align: right; float: right; width: 51%; height: 30px; font-size: 15px; font-weight: 600;" Text="00"
                                                                        onfocus="javascript:ClearValue(this);"
                                                                        onBlur="javascript:SetValue(this);" onChange="javascript:return checkamounttotal(event);" onkeypress="return isNumber(event)"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDeduct"
                                                                        CssClass="XMMessage" Text="Only numeric value allowed !" ErrorMessage="Only numeric value allowed !"
                                                                        ValidationExpression="^([0-9]*)$"
                                                                        Display="Dynamic" ValidationGroup="vldBudget" ForeColor="Red"></asp:RegularExpressionValidator>

                                                                </div>
                                                                <div class="col-1 col-sm-1 col-md-1 col-lg-1" style="padding-right: 23px">
                                                                    <asp:RequiredFieldValidator ID="rfvDedict"
                                                                        runat="server" ErrorMessage="Enter Deduct Commission" ControlToValidate="txtDeduct"
                                                                        ValidationGroup="vldBudget" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                                            <div class="row">
                                                                <div class="col-6 col-sm-6 col-md-6 col-lg-6" style="font-size: 15px; font-weight: 600; padding-left: 20px; margin-top: -6px;">
                                                                    <span style="color: white; background: #20b3f9; padding-left: 10px; padding-right: 10px; font-size: 17px; font-weight: 600; padding-top: 10px; padding-bottom: 10px" class="fontSetHead">Net Amount</span>
                                                                </div>
                                                                <div class="col-5 col-sm-5 col-md-5 col-lg-5" style="text-align: right; padding-right: 23px">
                                                                    <asp:TextBox ID="txttotalAmount" runat="server" Width="50%" Height="30px" Enabled="false"
                                                                        AutoPostBack="True" Font-Bold="true" AutoComplete="Off"
                                                                        Style="font-size: 15px; font-weight: 600; text-align: right; float: right" CssClass="borderRadius inputDesign widthSet" onkeypress="return isNumberChar(event)" MaxLength="13"></asp:TextBox>
                                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txttotalAmount"
                                                                        ErrorMessage="Please Enter Amount !" MaximumValue="9999999999999" MinimumValue="1"
                                                                        Type="Double" ValidationGroup="vldBudget">*</asp:RangeValidator>
                                                                </div>
                                                                <div class="col-1 col-sm-1 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Enter Amount !"
                                                                        ControlToValidate="txttotalAmount" ValidationGroup="vldBudget" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div runat="server" id="divpantan" class="col-6 col-sm-6 col-md-6 col-lg-6" style="margin-top: -6px; font-size: 15px; font-weight: 600;"></div>
                                                                <div class="col-5 col-sm-5 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:TextBox ID="txtPanNo" runat="server" Width="51%" Height="30px" MaxLength="10" AutoComplete="Off"
                                                                        Style="text-align: right; float: right; font-size: 15px; font-weight: 600;" CssClass="borderRadius inputDesign txtPanNo widthSet" onkeypress="return isNumberChar(event)"
                                                                        OnChange="ChangeCase(this);PanNumberValidation();"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtPanNo"
                                                                        CssClass="XMMessage" Text="Only alpha numeric value allowed !" ErrorMessage="Only alpha numeric value allowed !"
                                                                        ValidationExpression="^([a-zA-Z0-9]*)$"
                                                                        Display="Dynamic" ValidationGroup="vldBudget" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                </div>
                                                                <div class="col-1 col-sm-1 col-md-1 col-lg-1">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-6 col-sm-6 col-md-6 col-lg-8" style="margin-top: -6px; font-size: 15px; font-weight: 600;">Payment Mode</div>
                                                                <div class="col-3 col-sm-3 col-md-3 col-lg-3" style="padding-left: 3%">
                                                                    <asp:RadioButtonList ID="rblpayMode" runat="server"
                                                                        RepeatDirection="Horizontal" BorderStyle="Groove">
                                                                        <asp:ListItem Value="N" Selected="True">Online</asp:ListItem>
                                                                        <asp:ListItem Value="M">Manual</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="col-1 col-sm-1 col-md-1 col-lg-1">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row" id="divPD" runat="server" visible="false">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-6 col-sm-6 col-md-3 col-lg-3" runat="server" id="divPdacc" style="margin-top: -6px; font-size: 15px;"></div>
                                                                <div class="col-5 col-sm-5 col-md-8 col-lg-8" style="padding-right: 23px">
                                                                    <asp:DropDownList ID="ddlpdAccount" class="chzn-select" runat="server" Width="51%" Style="font-size: 15px;">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-lg-12 col-sm-1 col-md-1">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-6 col-md-6 ">
                                                            <asp:HiddenField ID="hidGRN" runat="server" />
                                                            <span id="spanbreakup" class="mandatory" style="display: none;">Please Enter BudgetHead Amount.!</span>
                                                        </div>
                                                        <div class="col-12 col-sm-5 col-md-5" style="text-align: right;">
                                                            <input type="button" id="inputBreakup" class="mobhide" style="padding: 5px; font-weight: 100; font-size: 12px; width: 160px; background: none; border: none; color: blue" runat="server" value="Break Up Details"
                                                                onclick="openPopup();" />
                                                        </div>
                                                        <div class="col-lg-12 col-sm-1 col-md-1">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4 col-sm-12 col-md-4">
                                                            <asp:ValidationSummary ID="ValidationSummary1"
                                                                ShowMessageBox="false" runat="server"
                                                                DisplayMode="BulletList" ShowSummary="true" Width="450"
                                                                ForeColor="Red" Font-Size="14px" Style="padding-left: 20px" ValidationGroup="vldBudget" />
                                                        </div>
                                                        <div class="col-lg-4 col-sm-12 col-md-4" style="padding-top: 2%;">
                                                            <span class="mandatory" id="spanBudgetHeadMsg" runat="server" style="display: none;"></span>
                                                        </div>
                                                        <div class="col-lg-3 col-sm-12 col-md-3" style="padding-right: 13px;">
                                                            <asp:Button ID="btnBudgetHead" class="next action-button btnHeight" ValidationGroup="vldBudget" Text="Next" runat="server" OnClick="btnBudgetHead_Click" />
                                                            <asp:Button ID="btnPrevBudgetHead" class="previous action-button-previous btnHeight" Text="Previous" runat="server" OnClick="btnPrevBudgetHead_Click" />
                                                        </div>
                                                        <div class="col-lg-1 col-sm-12 col-md-1">
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>

                                            <fieldset runat="server" id="fldPersonal" visible="false">
                                                <div class="form-card">
                                                    <div class="row">
                                                        <div class="col-12 col-lg-12 col-sm-12 col-md-12" style="margin-top: -10px;">
                                                            <h2 class="fs-title col-12 col-sm-12 col-md-12 col-lg-12 col_color p-2 mb-2 bgHeading text-white" style="display: contents">Personal Information:</h2>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; font-size: 15px; margin-top: -6px;">Remitter's Name</div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:TextBox ID="txtName" onPaste="return false" runat="server" MaxLength="40" CssClass="widthSet"
                                                                        Style="text-align: right; float: right; width: 100%; height: 30px; font-size: 15px"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtName"
                                                                        ErrorMessage="Special character not allowed in Remitter Name.!"
                                                                        ValidationExpression="^((([A-za-z\s]+)([A-za-z&_.,:;*!#`$\s]+))|([A-Za-z]+))$"
                                                                        Display="Dynamic" ValidationGroup="vldPersonal" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="rfvfullname" runat="server" ErrorMessage="Enter Remitter Name!"
                                                                        ControlToValidate="txtName" ValidationGroup="vldPersonal" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; margin-top: -6px; font-size: 15px;">Mobile No</div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:TextBox ID="txtMobileNo" onPaste="return false" runat="server" MaxLength="10" CssClass="widthSet"
                                                                        Style="text-align: right; float: right; width: 100%; height: 30px; font-size: 15px" onkeypress="return isNumber(event)"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="First digit cannot be 0 and minimum 10 digits required."
                                                                        ControlToValidate="txtMobileNo" ValidationGroup="vldPersonal" Display="Dynamic" ValidationExpression="[1-9]{1}[0-9]{9}" />
                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Your Mobile Number !"
                                                                        ControlToValidate="txtMobileNo" ValidationGroup="vldPersonal" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; margin-top: -6px; font-size: 15px;">City</div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="40" onkeypress="return (event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123)||event.charCode==32"
                                                                        Style="text-align: right; float: right; width: 100%; height: 30px; font-size: 15px" CssClass="widthSet"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Atleast 3 Character Mandatory."
                                                                        ControlToValidate="txtCity" ValidationGroup="vldPersonal" Display="Dynamic" ValidationExpression="[a-zA-Z][\s\S]{3,40}" />
                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="rfvcity" runat="server" ControlToValidate="txtCity"
                                                                        Text="*" ErrorMessage="Enter City Name.!" ValidationGroup="vldPersonal" Display="Dynamic"></asp:RequiredFieldValidator>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; margin-top: -6px; font-size: 15px;">TIN/Lease/Actt./ Vehicle/Tax-Id No</div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:TextBox ID="txtTIN" onPaste="return false" runat="server" MaxLength="40" AutoPostBack="True"
                                                                        Style="text-align: right; float: right; width: 100%; height: 30px; font-size: 15px;" CssClass="widthSet" OnTextChanged="txtTIN_TextChanged"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="rgvTinNo" runat="server" ControlToValidate="txtTIN"
                                                                        CssClass="XMMessage" ErrorMessage="Special character not allowed in Tin No.!"
                                                                        ValidationExpression="^([a-zA-Z0-9_.,:;*!#`$+\[(.*?)\]()'@%?={}&//\\ \s\-]*)$"
                                                                        Display="Dynamic" ValidationGroup="vldPersonal" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                    <span class="mandatory" runat="server" id="spanTin" visible="false"></span>
                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Your Tin Number !"
                                                                        ControlToValidate="txtTIN" ValidationGroup="vldnotPersonal" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                    <asp:HiddenField ID="HiddenField2" runat="server" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; margin-top: -6px; font-size: 15px;">Address</div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" OnkeyPress="javascript:Count(this,100);" MaxLength="200"
                                                                        Style="text-align: right; float: right; width: 100%; height: 30px; font-size: 15px" CssClass="widthSet"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtaddress"
                                                                        CssClass="XMMessage" ErrorMessage="Special character not allowed in Address.!"
                                                                        ValidationExpression="^([a-zA-Z0-9])+([a-zA-Z0-9.,:!\[()\]()'\s\-]*){0,200}$"
                                                                        Display="Dynamic" ValidationGroup="vldPersonal" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Your Address !"
                                                                        ControlToValidate="txtaddress" ValidationGroup="vldPersonal" Display="Dynamic"
                                                                        ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; margin-top: -6px; font-size: 15px;">Remarks</div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:TextBox onpaste="return false" ID="txtRemark" runat="server" TextMode="MultiLine" OnkeyUp="javascript:AllowedChars(this);"
                                                                        onkeypress="javascript:textCounter(this,'counter',200);"
                                                                        Style="text-align: right; float: right; width: 100%; height: 30px; font-size: 15px" CssClass="widthSet"
                                                                        MaxLength="200"></asp:TextBox>
                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RegularExpressionValidator runat="server" ID="validate" Display="Dynamic" ValidationGroup="vldPersonal"
                                                                        ControlToValidate="txtRemark" ValidationExpression="^((([A-za-z\s]+)([A-za-z0-9&_.,:;*!#`%$\s]+))|([A-Za-z0-9]+)){0,200}$" 
                                                                        ErrorMessage="Maximum 200 characters Allowed in Remark" ForeColor="red">*</asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" runat="server" id="divZone" visible="false">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; margin-top: -6px; font-size: 15px;">Zone_CD</div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:DropDownList ID="ddlZone" class="chzn-select drpWidth borderRadius inputDesign" runat="server"
                                                                        AutoPostBack="true" Style="font-size: 15px;"
                                                                        Width="100%" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="rfvZone" runat="server" ErrorMessage="Select Zone"
                                                                        ControlToValidate="ddlZone" ValidationGroup="vldPersonal" InitialValue="0" ForeColor="Red"
                                                                        Style="text-align: center" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; margin-top: -6px; font-size: 15px;">Circle_CD </div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:DropDownList ID="ddlCircle" class="chzn-select drpWidth borderRadius inputDesign" runat="server"
                                                                        AutoPostBack="true" Style="font-size: 15px;"
                                                                        Width="100%" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged">
                                                                    </asp:DropDownList>

                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="rfvCircle" runat="server" ErrorMessage="Select Circle"
                                                                        ControlToValidate="ddlCircle" ValidationGroup="vldPersonal" InitialValue="0" ForeColor="Red"
                                                                        Style="text-align: center" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" runat="server" id="divWard" visible="false">
                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                            <div class="row">
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="font-weight: 600; margin-top: -6px; font-size: 15px;">Ward_CD</div>
                                                                <div class="col-12 col-sm-12 col-md-5 col-lg-5" style="padding-right: 23px">
                                                                    <asp:DropDownList ID="ddlWard" class="chzn-select borderRadius inputDesign" runat="server" Style="font-size: 15px;"
                                                                        Width="100%">
                                                                    </asp:DropDownList>

                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-1 col-lg-1">
                                                                    <asp:RequiredFieldValidator ID="rfvWard" runat="server" ErrorMessage="Select Ward"
                                                                        ControlToValidate="ddlWard" ValidationGroup="vldPersonal" InitialValue="0" ForeColor="Red"
                                                                        Style="text-align: center" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-12 col-sm-12 col-md-6 col-lg-6" style="padding-right: 23px">
                                                                    <asp:Label ID="CTDMSG" runat="server" Text="Label" Visible="false" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4 col-sm-12 col-md-4">
                                                            <asp:ValidationSummary ID="ValidationSummary2"
                                                                ShowMessageBox="false" runat="server"
                                                                DisplayMode="BulletList" ShowSummary="true" Width="450"
                                                                ForeColor="Red" Font-Size="14px" Style="padding-left: 20px" ValidationGroup="vldPersonal" />
                                                        </div>
                                                        <div class="col-lg-4 col-sm-12 col-md-4">
                                                            <span class="mandatory" id="spanPersonalMsg" runat="server" style="display: none;"></span>
                                                        </div>
                                                        <div class="col-lg-3 col-sm-12 col-md-3" style="padding-right: 13px;">
                                                            <asp:Button ID="btnPersonal" class="next action-button btnHeight" ValidationGroup="vldPersonal" Text="Next" runat="server" OnClick="btnPersonal_Click" OnClientClick="JavaScript:return TinWarning();" />
                                                            <asp:Button ID="btnPrevPersonal" class="previous action-button-previous btnHeight" Text="Previous" runat="server" OnClick="btnPrevPersonal_Click" />
                                                        </div>
                                                        <div class="col-lg-21 col-sm-12 col-md-1">
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>

                                            <fieldset runat="server" id="fldBank" visible="false">
                                                <div class="form-card">
                                                    <div class="row">
                                                        <div class="col-12 col-lg-12 col-sm-12 col-md-12" style="margin-top: -10px;">
                                                            <h2 class="fs-title col-12 col-sm-12 col-md-12 col-lg-12 col_color p-2 mb-2 bgHeading text-white" style="display: contents">Payment Details:</h2>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-sm-12 col-md-12" id="divBankSection">
                                                            <div class="row">
                                                                <div class="col-4 col-sm-4 col-md-3 col-lg-3 border col_color" style="padding: 0;">
                                                                    <asp:RadioButtonList ID="rblpaymenttype" runat="server" OnSelectedIndexChanged="rblpaymenttype_SelectedIndexChanged" AutoPostBack="true"
                                                                        RepeatDirection="Horizontal" BorderStyle="Groove" RepeatColumns="1" RepeatLayout="Table" Class="col-12 side_nav">
                                                                        <asp:ListItem Value="4">&lt;img width=30px src=../Image/bank.png&gt;   Net Banking</asp:ListItem>
                                                                        <asp:ListItem Value="5">&lt;img width=30px src=../Image/payment_gateway.png&gt;   PG/Credit/Debit</asp:ListItem>
                                                                        <asp:ListItem Value="6">&lt;img width=30px src=../Image/UPI.png&gt;  UPI Payment</asp:ListItem>
                                                                        <asp:ListItem Value="3">&lt;img width=30px src=../Image/mannual.png&gt;   Offline(OTC)</asp:ListItem>
                                                                        <asp:ListItem Value="7" Enabled="false">&lt;img width=30px src=../Image/mannual.png&gt;   NEFT/RTGS</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                                                    <div class="row" id="divManualBankSection" runat="server" visible="false">
                                                                        <div class="col-md-2 col-lg-2"></div>
                                                                        <div class="col-12 col-sm-12 col-md-8 col-lg-8" style="padding: 5px;">
                                                                            <asp:RadioButtonList ID="rblCashCheque" runat="server" RepeatDirection="Horizontal"
                                                                                OnSelectedIndexChanged="rblCashCheque_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="1" style="font-weight: bold;" Selected="True">Cash</asp:ListItem>
                                                                                <asp:ListItem Value="2" style="font-weight: bold;">Cheque</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="divPopularBanks" runat="server" visible="false">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-4 col-sm-4 col-md-10 col-lg-10">
                                                                            <asp:RadioButtonList ID="rblBank" onchange="displayBankNameFromNetBanking()" runat="server" RepeatDirection="Horizontal"
                                                                                BorderStyle="Groove" RepeatColumns="3" RepeatLayout="Table">
                                                                                <asp:ListItem Value="0006326">&lt;img width=30px src=../Image/EpayBanksLogo/SBI.png&gt;   SBI</asp:ListItem>
                                                                                <asp:ListItem Value="0304017">&lt;img width=30px src=../Image/EpayBanksLogo/PNB.png&gt;   PNB</asp:ListItem>
                                                                                <asp:ListItem Value="6910213">&lt;img width=30px src=../Image/EpayBanksLogo/IDBI.png&gt;  IDBI</asp:ListItem>
                                                                                <asp:ListItem Value="0200113">&lt;img width=30px src=../Image/EpayBanksLogo/BOB.png&gt;   BOB</asp:ListItem>
                                                                                <asp:ListItem Value="0240539">&lt;img width=30px src=../Image/EpayBanksLogo/Canra.png&gt; CANRA</asp:ListItem>
                                                                                <asp:ListItem Value="0280429">&lt;img width=30px src=../Image/EpayBanksLogo/CBI.png&gt;   CBI</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="divPopularPG" runat="server" visible="false">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-4 col-sm-4 col-md-10 col-lg-10">
                                                                            <asp:RadioButtonList ID="rblPG" onchange="displayBankNameFromPG()" runat="server" RepeatDirection="Horizontal"
                                                                                BorderStyle="Groove" RepeatColumns="3" RepeatLayout="Table">
                                                                                <asp:ListItem Value="1000132">&lt;img width=30px src=../Image/EpayBanksLogo/SBI.png&gt;   SBI</asp:ListItem>
                                                                                <asp:ListItem Value="9910001">&lt;img width=30px src=../Image/EpayBanksLogo/PNB.png&gt;   PNB</asp:ListItem>
                                                                                <asp:ListItem Value="9930001">&lt;img width=30px src=../Image/EpayBanksLogo/logo_paytm.png&gt;   PAYTM</asp:ListItem>
                                                                                <asp:ListItem Value="9940001">&lt;img width=30px src=../Image/EpayBanksLogo/HDFC.jpg&gt;   HDFC</asp:ListItem>
                                                                                <asp:ListItem Value="9950001">&lt;img width=30px src=../Image/EpayBanksLogo/HDFC.jpg&gt;   Billdesk</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin-top: 20px" runat="server" id="divBank">
                                                                        <div class="col-md-2 col-lg-2"></div>
                                                                        <div class="col-10 col-sm-8 col-md-8 col-lg-8" style="padding-right: 23px; float: right">
                                                                            <asp:DropDownList ID="ddlbankname" Width="100%" runat="server" CssClass="chzn-select" onchange="CallBankDropdown();">
                                                                            </asp:DropDownList>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ddlbankname"
                                                                                Text="Special character not allowed.!" CssClass="XMMessage" Display="Dynamic"
                                                                                ErrorMessage="Special character not allowed in Bank Name." ValidationExpression="^([a-zA-Z0-9_., \s\-]*)$"
                                                                                ValidationGroup="vldBank" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                        </div>

                                                                    </div>
                                                                    <div class="row" runat="server" id="divCheque" visible="false">
                                                                        <div class="col-md-12 col-lg-12">
                                                                            <div class="row">
                                                                                <div class="col-md-2 col-lg-2"></div>
                                                                                <div class="col-10 col-sm-2 col-md-2 col-lg-10" id="divWriteChequeNoLable" runat="server" visible="false">Cheque No:</div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2 col-lg-2"></div>
                                                                                <div class="col-10 col-sm-8 col-md-8 col-lg-10" style="padding-right: 23px; float: right;">
                                                                                    <asp:TextBox onpaste="return false" AutoComplete="Off" ID="txtChequeDDNo" runat="server"
                                                                                        onkeypress="return (event.charCode > 47 && event.charCode < 58)"
                                                                                        Width="79%" CssClass="form-control txtCheque"
                                                                                        Style="height: 25px;" MaxLength="6"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="revChequeNo" runat="server" ErrorMessage="Last digit cannot be 0 and minimum 6 digits required."
                                                                                        ControlToValidate="txtChequeDDNo" ValidationGroup="vldBank" Display="Dynamic"
                                                                                        ValidationExpression="[0-9]{5}[1-9]{1}" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2 col-lg-2"></div>
                                                                        <div class="col-md-9 col-lg-9 mandatory" id="divBankMessage" runat="server" visible="false"></div>
                                                                        <div class="col-md-1 col-lg-1"></div>

                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1"></div>
                                                                        <div id="dialog" runat="server" class="col-9 col-sm-9 col-md-9 col-lg-9 epay" visible="false">
                                                                            <div class="row" style="padding-left: 20%; font-family: verdana; font-size: 13px; font-weight: bold; color: #7EC7EA;">Charges Applicable</div>
                                                                            <div class="row">
                                                                                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                                                                    <div class="row">
                                                                                        <div class="col-3">Credit Card : </div>
                                                                                        <div class="col-9">0.90%+GST of transaction amount</div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-3">Debit Card  : </div>
                                                                                        <div class="col-9">0.75%+GST of transaction amount</div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-4">Net Banking : </div>
                                                                                        <div class="col-8" id="divNetBanking">0</div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-1"></div>
                                                                    </div>
                                                                    <%--UPI Section--%>
                                                                    <div id="divUPI" class="row" runat="server" visible="false">
                                                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 20px">
                                                                            <div class="row" style="padding-bottom: 10%;">
                                                                                <div class="col-3 col-sm-2 col-md-1 col-lg-1"></div>
                                                                                <div class="col-9 col-sm-9 col-md-9 col-lg-9" style="margin-left: 20px">
                                                                                    <asp:RadioButtonList ID="rblUpi" runat="server" RepeatDirection="Horizontal"
                                                                                        BorderStyle="Groove" RepeatColumns="4" AutoPostBack="true" OnSelectedIndexChanged="rblUpi_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="99300011" Selected="True">&lt;img width=30px src=../Image/EpayBanksLogo/logo_paytm.png&gt; Paytm</asp:ListItem>
                                                                                        <asp:ListItem Value="99300012">&lt;img width=30px src=../Image/ic_googlepay-01.png&gt; GPay</asp:ListItem>
                                                                                        <asp:ListItem Value="99300013">&lt;img width=30px src=../Image/ic_phonepe-01.png&gt; PhonePe</asp:ListItem>
                                                                                        <asp:ListItem Value="99300014">&lt;img width=30px src=../Image/ic_upi-01.png&gt; BHIM</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-2 col-sm-2 col-md-2 col-lg-2" style="margin-left: 20px">UPI ID:</div>
                                                                                <div class="col-5 col-sm-9 col-md-5 col-lg-5" style="text-align: right; padding-right: 3%; float: right">
                                                                                    <asp:TextBox AutoComplete="Off" ID="txtUpi" runat="server" CssClass="txtCheque"
                                                                                        Style="height: 25px; margin-right: 30px" MaxLength="20" onkeypress="return (event.charCode > 47 && event.charCode < 58) ||event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123)"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                                        ErrorMessage="Last digit cannot be 0 and minimum 6 digits required."
                                                                                        ControlToValidate="txtUpi" ValidationGroup="vldBank" Display="Dynamic"
                                                                                        ValidationExpression="[a-zA-Z0-9]{7,20}" />
                                                                                </div>
                                                                                <div class="col-3 col-sm-3 col-md-3 col-lg-3">
                                                                                    <asp:DropDownList ID="drpUpiID" runat="server" Style="width: 100%; margin-left: -20%; border: 1px solid #ccc; background-color: #ECEFF1;">
                                                                                        <asp:ListItem Value="0" Text="@paytm"></asp:ListItem>
                                                                                    </asp:DropDownList>
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
                                                                </div>
                                                                <div class="col-4 col-sm-4 col-md-3 col-lg-3 border" style="margin-left: -0.5rem; top: 0.1rem;">
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="color: white; background: #20b3f9; font-weight: 600; margin-top: -6px;">Summary: </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="font-weight: 600; margin-top: -6px; padding-top: 5%;">GRN</div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="padding-right: 23px">
                                                                            <span id="spanGRN" runat="server"></span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="font-weight: 600; margin-top: -6px; padding-top: 5%;">Department</div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="padding-right: 23px">
                                                                            <span id="spanDepartment" runat="server"></span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="font-weight: 600; margin-top: -6px; padding-top: 5%;">Office</div>

                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="padding-right: 23px">
                                                                            <span id="spanOffice" runat="server"></span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="font-weight: 600; margin-top: -6px; padding-top: 5%;">Treasury</div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="padding-right: 23px">
                                                                            <span id="spanTreasury" runat="server"></span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="font-weight: 600; margin-top: -6px; padding-top: 5%;">Bank Name</div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                                        <div class="col-10 col-sm-10 col-md-10 col-lg-10" style="padding-right: 23px">
                                                                            <span id="spanBankName" runat="server" style="color: cornflowerblue; font-size: 20px;"></span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12 col-lg-3 col-sm-3 col-md-3"></div>
                                                        <div class="col-12 col-lg-5 col-sm-5 col-md-5" style="text-align: center;">
                                                            <span id="spanpay" runat="server" visible="false" style="font-size: 15px; font-weight: 600;"></span>
                                                        </div>
                                                        <div class="col-12 col-lg-4 col-sm-4 col-md-4">
                                                            <asp:Button ID="btnBank" Width="70%" class="next action-button btnHeight" ValidationGroup="vldBank" Text="Next" runat="server" OnClick="btnBank_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>

                                        </div>
                                        <%--<div class="row" style="margin-top: -4%; padding-bottom: 3%;">
                                            <div class="col-lg-7 col-sm-12 col-md-7"></div>
                                            <div class="col-lg-4 col-sm-12 col-md-4">
                                                <span class="mandatory" id="spanPersonalMsg" runat="server"></span>
                                            </div>
                                            <div class="col-lg-1 col-sm-12 col-md-1"></div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <span id="pdvisible" style="display: none;" runat="server"></span>
           
            <asp:HiddenField ID="HiddenField1" runat="server" />
             
        </ContentTemplate>

       
    </asp:UpdatePanel>
   
</asp:Content>
