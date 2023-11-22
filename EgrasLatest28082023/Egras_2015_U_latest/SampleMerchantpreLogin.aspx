﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleMerchantpreLogin.aspx.cs"
    Inherits="SampleMerchantpreLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EgrasRaj.Nic.in</title>
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11" />
    <%--  <meta http-equiv="X-UA-Compatible" content="IE=edge;" />--%>
    <script type="text/javascript" src="https://egras.rajasthan.gov.in/js/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/PageHeader.css" rel="stylesheet" />

    <style type="text/css">
        .style2 {
            height: 34px;
        }

        .style6 {
            width: 402px;
            margin-top: 20%;
        }

        .verticalline {
            border-left: solid 1px black;
            height: 240px;
            width: 2px;
        }

        .demo {
            background-color: blue;
        }
    </style>
    <style type="text/css">
        .section2header {
            background: #3276b1;
            margin-top: -20px;
        }

            .section2header h1 {
                font-size: 18px;
                color: #fff;
                padding: 1% 2%;
                font-weight: 500;
                margin-bottom: 0;
            }

        .side_nav {
            /*background: #f4f4f4;*/
            padding: 0;
        }

            .side_nav li.active {
                /*background: #fff;*/
                background: #f4f4f4;
                border-right: 3px solid #555;
                pointer-events: none;
            }


            .side_nav li {
                list-style: none;
                /*border-bottom: 1px solid #dedede;*/
                border: 1px solid #dedede;
            }

            .side_nav li {
                text-decoration: none;
                display: block;
                padding: 6% 6%;
            }

            .panel-title, .side_nav li, .txt {
                font-size: 17px;
                font-weight: 400;
                word-wrap: break-word;
            }

        .btn {
            height: 45px !important;
            border-radius: 0px;
            margin-top: 5%;
            margin-bottom: 10px;
            color: #fff !important;
            background-color: #337ab7 !important;
            border-color: #2e6da4 !important;
            width: 200px;
            font-size: medium !important;
        }

        .panel-group .panel {
            margin-bottom: 0;
            overflow: hidden;
            border-radius: 4px;
            width: 90%;
            margin: 20px;
            /*background-color: steelblue;*/
            /*color: white;*/
            padding: 15px;
        }

        .img {
            width: 30%;
        }

        input[type=submit] {
            width: 40%;
            height: 28px;
        }

        input[type=text], input[type=password] {
            height: 10%;
            margin-top: 20px;
        }

        .col-md-3 {
            width: 25%;
            padding: 20px;
        }
    </style>

    <script type="text/javascript">

        var Banks = [];
        var ePayBanks = [];
        $(document).ready(function () {
            //START PAYMENT SECTION
            $("ul.side_nav li").click(function () {
                $("#divPayment").hide();
                $("#divMSGdd").hide();
                $("#divMSGManual").hide();
                //var URL;
                $(".side_nav li").removeClass('active');
                $(this).addClass('active');
                if ($(".side_nav li.active span").html() == 'Manual') {
                    var ddlbank = $("[id*=ddlbank]");
                    ddlbank.empty();
                    $('#ddlbank').append('<option value="0">--- Select Bank ---</option>');
                    $.each(Banks, function (i) {
                        ddlbank.append($("<option></option>").val(Banks[i].BSRCode).html(Banks[i].BankName));
                    })
                    $("#divMSGManual").show();
                }
                else if ($(".side_nav li.active span").html() == 'E-Banking') {
                    var ddlbank = $("[id*=ddlbank]");
                    ddlbank.empty();
                    $('#ddlbank').append('<option value="0">--- Select Bank ---</option>');
                    $.each(Banks, function (i) {
                        ddlbank.append($("<option></option>").val(Banks[i].BSRCode).html(Banks[i].BankName));
                    })
                }
                else {
                    $("#divMSGdd").show();
                    $("#divPayment").show();
                    var ddlbank = $("[id*=ddlbank]");
                    ddlbank.empty();
                    $('#ddlbank').append('<option value="0">--- Select Bank ---</option>');
                    $.each(ePayBanks, function (i) {
                        ddlbank.append($("<option></option>").val(ePayBanks[i].BSRCode).html(ePayBanks[i].BankName));
                    })
                }
            })

            $("#ddlbank").change(function () {
                //alert($('#ddlbank').val());
                var value = $('#ddlbank').val();
                //if (value == '1000132')
                    //if (value == '1000132')
                    //    $('#divpayu').val($(this).attr('value')).effect('highlight', 3000);
                    switch (value) {
                        case '1000132':
                            $('#divepay').val($(this).attr('value')).effect('highlight', 5000);
                            break;
                        case '9910001':
                            $('#divpayu').val($(this).attr('value')).effect('highlight', 5000);
                            break;
                        default:
                            // code block
                    }
            });


        });



        function btnGuestClick() {
            $("#divPayment").hide();
            $("#divMSGdd").hide();
            $("#divMSGManual").hide();

            $('#pleaseWaitDialog').modal();
            $.ajax({
                type: 'POST',
                url: 'SampleMerchantpreLogin.aspx/GetBanks',
                data: '{"EncData":"' + $("#hdnEncData").val() + '","AUIN":"' + $("#hdnAUIN").val() + '","Mcode":"' + $("#hdnMCode").val() + '","ChallanType":"' + $("#hdnCType").val() + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    $('#pleaseWaitDialog').modal('hide');
                    if (data.d == "-1") {
                        window.location = 'IntegrationErrorPage.aspx';
                    }
                    else if (data.d == "0") {
                        window.location = 'IntegrationErrorPage.aspx';
                    }
                    else {
                        $("#field1").hide();
                        $("#divBanks").show();

                        var pType = data.d.split("|")[1];
                        var objdata = $.parseJSON(data.d.split("|")[0]);
                        var lenresult = objdata.length;
                        if (lenresult > 0) {
                            var rows = "";
                            if (pType == "M") {
                                $("#manual").show();
                                $("#online").hide();
                                $("#paymentgateway").hide();
                                $("#divMSGManual").show();
                                var ddlbank = $("[id*=ddlbank]");
                                ddlbank.empty();
                                $('#ddlbank').append('<option value="0">--- Select Bank ---</option>');

                                $.each(objdata, function (i) {
                                    Banks.push({ BSRCode: objdata[i].BSRCode, BankName: objdata[i].BANKNAME });
                                    ddlbank.append($("<option></option>").val(objdata[i].BSRCode).html(objdata[i].BANKNAME));
                                })
                            }
                            else
                                if (pType == "N") {
                                    $("#manual").hide();
                                    $("#online").show();
                                    $("#paymentgateway").show();
                                    $("#divMSGManual").hide();

                                    var ddlbank = $("[id*=ddlbank]");
                                    ddlbank.empty();
                                    $('#ddlbank').append('<option value="0">--- Select Bank ---</option>');
                                    $.each(objdata, function (i) {
                                        if (objdata[i].access != "Z") {
                                            Banks.push({ BSRCode: objdata[i].BSRCode, BankName: objdata[i].BankName });
                                            ddlbank.append($("<option></option>").val(objdata[i].BSRCode).html(" <img  id=" + objdata[i].BSRCode + "  src='Image/BankLogo/" + objdata[i].BSRCode + ".png' Width='10%' Height='10%' style='padding:10px;'  />" + objdata[i].BankName));
                                        }
                                        else {
                                            ePayBanks.push({ BSRCode: objdata[i].BSRCode, BankName: objdata[i].BankName });
                                        }
                                    })
                                }
                        }
                    }
                }
            });
        }
    </script>
    <script type="text/javascript">
        function btnPaySubmit() {
            $('#pleaseWaitDialog').modal();
            if ($("#ddlbank").val() == "" || $("#ddlbank").val() == null || $("#ddlbank").val() == "0") {
                alert('Please Select Bank!');
                $('#pleaseWaitDialog').modal('hide');
                return;
            }
            $.ajax({
                type: 'POST',
                url: 'SampleMerchantpreLogin.aspx/SubmitChallan',
                data: '{"BSRCode":"' + $("#ddlbank").val() + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    alert(data.d.split("|")[0]);
                    if (data.d.split("|")[1] != "") {
                        window.location = data.d.split("|")[1];
                    } else {
                        $('#pleaseWaitDialog').modal('hide');
                    }
                }
            });

        }
        function GetBanks() {
            $("#divBanks").show();
        }
        $(document).ready(function () {
            if ($("#field1").is(":visible")) {
                $("#divBanks").hide();
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnEncData" runat="server" />
        <asp:HiddenField ID="hdnAUIN" runat="server" />
        <asp:HiddenField ID="hdnMCode" runat="server" />
        <asp:HiddenField ID="hdnCType" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <!-- column 2 //-->
                    <div class="row">
                        <div class="col-md-12">
                            <img name="Grass" src="App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="center" width="100%" />
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-12">
                            <div _ngcontent-c6="" class="tnHead minus2point5per">
                                <h2 _ngcontent-c6="" title="Integration Mode">
                                    <span _ngcontent-c6="" style="color: #FFF">Department Integration</span></h2>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row" runat="server" id="field1">
                        <div class="col-md-12">
                            <div>
                                <%--<div class="style6" align="center" width="48%">--%>
                                <div align="center">
                                    <asp:ImageButton ID="txtGo" runat="server" Height="100%"  style="padding:15%; width: 45%;" OnClientClick="btnGuestClick(); return false;" ImageUrl="~/Image/cont_Button.png" />
                                </div>
                            </div>

                        </div>
                        <%--<div class="col-md-6" style="width: 10px;">
                            <div class="verticalline">
                            </div>
                        </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="divBanks">
                                <%--START--%>
                                <div id="ChallanBanks" class="section2" style="border: 1px solid #ddd;">
                                    <div class="section2header">
                                        <h1>Payment Details</h1>
                                    </div>
                                    <div class="row flexrow">
                                        <div class="col-md-3 hidden-sm hidden-xs">
                                            <ul class="side_nav">
                                                <li id="manual" class=""><i class="icon-internetbanking"></i><span>Manual</span>
                                                </li>
                                                <li id="online" class="active"><i class="icon-cards"></i><span>E-Banking</span>
                                                </li>
                                                <li id="paymentgateway" class=""><i class="icon-cards"></i><span>Payment Gateway/ Credit/Debit Card</span>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-md-4" style="padding: 0;">
                                            <div class="panel-group" id="divManual" style="padding-top: 20px;">
                                                <select id="ddlbank" class="selectpicker form-control">
                                                    <option value="0">--- Select Bank ---</option>
                                                </select>
                                            </div>
                                            <div class="row text-center">
                                                <asp:Button ID="btnSubmit" type="submit" Text="Proceed" class="btn btn-primary" runat="server" OnClientClick="btnPaySubmit(); return false;"></asp:Button>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="panel-group col-md-12" id="divMSGdd">
                                                <div class="row row1" style="padding: 0;">
                                                    <div id="divpayu" class="col-sm-6 col1 panel" style="border: 1px solid gray; border-radius: 0px;">
                                                        <div class="" style="">
                                                            <%--<img src="Image/BankLogo/9910001.png" class="img" />--%>
                                                            <strong><span class="sub"><u>PNB Gateway Charges Applicable:</u></span></strong><br />
                                                            <br />
                                                            <strong>Net Banking:</strong>&nbsp;Nil<br />
                                                            <strong>Credit Card:</strong>&nbsp;0.90% +ST of transaction amount<br />
                                                            <strong>Debit Card :</strong>&nbsp;0.75% +ST of transaction amount
                                                        </div>
                                                    </div>
                                                    <div id="divepay" class="col-sm-6 col1 panel" style="border: 1px solid gray; border-radius: 0px;">
                                                        <div class="" style="">
                                                            <%--<img src="Image/BankLogo/1000132.png" class="img" />--%>
                                                            <strong><span class="sub"><u>EPAY Charges Applicable:</u></span></strong><br />
                                                            <br />
                                                            <strong>Net Banking:</strong>&nbsp;Rs 5 per transaction except SBI & associated banks<br />
                                                            <strong>Credit Card:</strong>&nbsp;0.90% +ST of transaction amount<br />
                                                            <strong>Debit Card :</strong>&nbsp;0.75% +ST of transaction amount
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <%--END--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" style="background-color: transparent">
            <div class="modal-body">
                <div id="myDiv">
                    <img id="dwnldgif" alt="" style='margin-top: 330px; position: absolute; background-color: transparent; width: 50px; left: 48%;' src="Image/waiting_process.gif" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
