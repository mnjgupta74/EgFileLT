<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgEditTreasury.aspx.cs" Inherits="WebPages_TO_EgEditTreasury" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../md5.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

            .btn-default.disabled, .btn-default[disabled], fieldset[disabled] .btn-default, .btn-default.disabled:hover, .btn-default[disabled]:hover, fieldset[disabled] .btn-default:hover, .btn-default.disabled:focus, .btn-default[disabled]:focus, fieldset[disabled] .btn-default:focus, .btn-default.disabled:active, .btn-default[disabled]:active, fieldset[disabled] .btn-default:active, .btn-default.disabled.active, .btn-default[disabled].active, fieldset[disabled] .btn-default.active {
                background-color: #abaaaa;
                border-color: #ccc;
            }

        tr.border_bottom td {
            border: 1pt solid black;
            /*border-bottom :1pt solid black;*/
            /*border-top :1pt solid black;*/
            border-spacing: 10px;
            border-collapse: separate;
        }

        tr.padding td {
            padding-top: 5px;
            padding-bottom: 5px;
            padding-left: 10px;
            padding-right: 10px;
        }

        tr.margin td {
            margin-left: 10px;
            margin-right: 10px;
        }
    </style>
    <style type="text/css">
        .sectiontopheader[_ngcontent-c6] {
            border-top: 5px solid #337ab7;
            margin-bottom: 5px;
            display: -webkit-box;
        }

        .tnHead, .sectiontopheader {
            display: flex;
            justify-content: space-between;
            align-items: center;
            position: relative;
        }

            .sectiontopheader[_ngcontent-c6] h1[_ngcontent-c6] {
                background: #337ab7;
            }

            .sectiontopheader h1, .tnHead h1 {
                padding: 8px 20px;
                position: relative;
                top: -5px;
                margin: 0;
                font-size: 18px;
            }

                .sectiontopheader h1:after, .tnHead h1:after {
                    position: absolute;
                    right: -34px;
                    top: 0;
                    content: '';
                    border-style: solid;
                    border-width: 34px 34px 0 0;
                }

            .sectiontopheader[_ngcontent-c6] h1[_ngcontent-c6]:after {
                border-color: #337ab7 transparent transparent;
            }
    </style>
    <script type="text/javascript">

        function UpdateData(val) {

            var loc = '';
            loc = $('#ddlTreasury').val();
            existingval = val;
            if (existingval != loc) {

                var grn = $('#idGRN').val();
                var val = loc + '|' + grn
                var Url = '<%= ResolveUrl("~/GRNHandler.ashx") %>';
                $.ajax({
                    type: "POST",
                    url: Url + "?HandlerVal=" + val,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //data: obj,
                    async: true,
                    complete: function () {

                        $.ajax({
                            type: 'POST',
                            url: '<%= ResolveUrl("~/WebPages/to/EgEditTreasury.aspx/UpdateTreasury") %>',
                            data: '{"GRN":"' + $('#idGRN').val() + '","treasurycode":"' + $('#ddlTreasury').val() + '"}',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (msg) {
                                $('#updtbtn').hide();
                                $('#tbDetails').hide();
                                $("#ddlTreasury").attr("disabled", "true");
                                alert(msg.d);
                            }
                        });

                    }
                });
            }
        }
        //function ResetData() {
        //    
        //    $("#idGRN").attr("disabled", "false");
        //    $("#tbDetails").hide();

        // }
        $(document).ready(function () {
            $("#tbDetails").hide(), $("#reset").hide();
            $("#form1").attr('autocomplete', 'NoAutocomplete');
        });
        function CallHandler() {


            var obj = {
                'HandlerVal': $('#idGRN').val(),
                //'HandlerVal': Val,
            }
            var Url = '<%= ResolveUrl("~/GRNHandler.ashx") %>';
            $.ajax({
                type: "POST",
                url: Url + "?HandlerVal=" + $('#idGRN').val(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //data: obj,
                async: true,
                complete: function () {
                    ShowData();
                }
            });
        }

        function PwdEnable() {

            var GRN = $('#idGRN').val();
            if (GRN == null || GRN == "") {
                alert("Please Enter GRN");
            }
            else {
                var GRN = document.getElementById("idGRN")
                var GRNAtt = GRN.getAttribute("disabled");
                if (GRNAtt != "disabled") {
                    $('#PanelPassword').show();
                }
            }
        }
        function NumberOnly(evt) {
            //if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            //var parts = evt.srcElement.value.split('.');
            //if (parts.length > 3) return false;

            //if (evt.keyCode == 46) return (parts.length == 1);

            //if (parts[0].length >= 14) return false;

            //if (parts.length == 3 && parts[1].length >= 3) return false;
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        //function NumberOnly(evt) {

        //    if (!((evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
        //    //var parts = evt.srcElement.value.split('.');
        //    //if (parts.length > 3) return false;
        //    //if (evt.keyCode == 46) return (parts.length == 1);
        //    //if (parts[0].length >= 14) return false;
        //    //if (parts.length == 3 && parts[1].length >= 3) return false;
        //}
        function isStrongPassword() {
            var newpassword = $('#txtSecurePass').val();
            $('#txtSecurePass').val(hex_md5(hex_md5(newpassword) + $(<%=hdnRnd.ClientID%>).val()));
            CallHandler();
            return true;
        }
        function ShowData() {

            //CallHandler($('#idGRN').val());

            var GRN = $('#idGRN').val();
            if (GRN == null || GRN == "") {
                alert("Please Enter GRN");
            }
            else {
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/to/EgEditTreasury.aspx/GetGRNDetail") %>',
                    data: '{"GRN":"' + GRN + '","securePwd":"' + $('#txtSecurePass').val() + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        if (msg.d.length > 0) {
                            if (msg.d == '-2') {
                                alert('Invalid Password');
                                return;
                            }
                            if (msg.d == '-1') {
                                alert('Invalid Request');
                                return;
                            }
                            $('#PanelPassword').hide();
                            $("#tbDetails").show();
                            $("#tbDetails").empty();
                            $("#ddlTreasury").empty();

                            var jsonarr = msg.d.split('|');
                            var json1 = JSON.parse(jsonarr[0]);
                            var json2 = JSON.parse(jsonarr[1]);
                            var myOptions = JSON.parse(jsonarr[2]);
                            var mySelect = $('#tbDetails tbody');
                            var val = 0;
                            $.each(json1, function (index, obj) {

                                val = obj.Location.toString();
                                var row = '<tr class="padding"><td colspan="2"> <b>Department Name :</b> ' + obj.DepartmentName + '</td></tr><tr class="padding"><td colspan="2"><b> Office Name :</b> ' + obj.OfficeName + '</td></tr><br>'
                                $("#tbDetails").append(row);
                            });
                            var row = '<tr class="border_bottom padding margin" style="background-color: #507CD1;color:white;"><td> <b>Head Name</b> </td><td> <b>Amount</b></td></tr><br><br>'
                            $("#tbDetails").append(row);
                            $.each(json2, function (index, obj) {
                                var row = '<tr class="border_bottom padding margin"><td>' + obj.SCHEMANAME + '</td><td>' + obj.Amount + '</td></tr>'
                                $("#tbDetails").append(row);
                            });
                            $("#tbDetails").append('<tr class="padding"><td> <b>Location :</b> <select id ="ddlTreasury"></select></td><td><input id="updtbtn" type="button" value="Update" onClick="UpdateData(' + "'" + val + "'" + '); return false;"/></td></tr>');
                            $.each(myOptions, function (data, value) {
                                $("#ddlTreasury").append($("<option></option>").val(value.TreasuryCode).html(value.TreasuryName));
                            });
                            $("#ddlTreasury").val(val);
                        }
                        else {
                            alert("No Record found");
                        }

                    }

                });
                $("#idGRN").attr("disabled", "true");
                $("#reset").show()
                $('#PanelPassword').hide();
            }
        }
    </script>




    <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
        <h1 _ngcontent-c6="" title="EditTreasury">
            <span _ngcontent-c6="" style="color: #FFF">Edit Treasury</span></h1>
    </div>
    <asp:HiddenField ID="hdnRnd" runat="server" />
    <div id="sh1" style="margin-top: 20px; border: 1px solid #808080; padding-top: 5px;">
        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-4">
                <div class="col-md-2" style="text-align: center; margin-top: 5px;">
                    <span style="color: #336699; margin-top: 10px; font-size: 15px">GRN:</span>
                </div>
                <div class="col-md-10" style="text-align: center; margin-top: 5px;">
                    <input id="idGRN" type="number" maxlength="12" min="1" onkeypress="Javascript:return NumberOnly(event)" class="form-control" />
                </div>
            </div>
            <div class="col-md-4">
                <input id="inpsubmit" type="submit" value="Submit" onclick="PwdEnable(); return false;" style="height: 33px; margin-top: 5px;" class="btn btn-default" />
                <input id="reset" type="submit" value="Reset" class="btn btn-default" style="height: 33px; margin-top: 5px;" />
            </div>

        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4" id="PanelPassword" style="text-align: center; display: none; padding-top: 10px">
                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px">Enter Your Transaction Password:-</span></b>&nbsp;
                            <input type="password" id="txtSecurePass" class="form-control" style="width: 50%; margin-left: 80px; margin-top: 10px" />
                <br />
                <br />
                <input type="button" value="Verify" id="btnVerify" class="btn btn-success" style="height: 33px; margin-top: -20px;" onclick="isStrongPassword(); return false" />
                <%--<input type="button" value="Cancel" id="btnPasswordCancel" />--%>
            </div>
        </div>
        <br />
        <br />
        <table id="tbDetails" cellspacing="0" style="margin-top: auto; margin-left: 150px; width: 70%; border: 1px solid gray;">
        </table>
    </div>
</asp:Content>
