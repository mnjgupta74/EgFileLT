<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgEditOffice.aspx.cs" Inherits="WebPages_Department_EgEditOffice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../md5.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>

    <style type="text/css">
        tr.border_bottom td {
            border: 1pt solid black;
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

        function UpdateData() {
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/Department/EgEditOffice.aspx/UpdateTreasury") %>',
                        data: '{"GRN":"' + $('#idGRN').val() + '","OfficeId":"' + $('#ddlOffice').val() + '", "TreasuryCode":"' + $('#ddlDistrict').val() + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (msg) {
                            var result = msg.d;
                            alert(result);
                            $('#updtbtn').hide();
                            $('#tbDetails').hide();
                            $("#ddlOffice").attr("disabled", "true");

                        }
                   });

                }
                $(document).ready(function () {
                    $("#tbDetails").hide(), $("#reset").hide();
                    $("#form1").attr('autocomplete', 'NoAutocomplete');

                });


                function GetSelectedTextValue(ddlDistrict) {

                    var DistrictCode = ddlDistrict.value;
                    $.ajax({
                        type: 'POST',
                        url: '<%= ResolveUrl("~/WebPages/Department/EgEditOffice.aspx/GetOfficeDetails") %>',
                    data: '{"DeptCode":"' + $('#deptcode').val() + '","TreasuryCode":"' + DistrictCode + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {

                        var jsonarr = msg.d.split('|');
                        var OfficeList = JSON.parse(jsonarr[0]);
                        $("#ddlOffice").empty();
                        $.each(OfficeList, function (data, value) {
                            $("#ddlOffice").append($("<option></option>").val(value.officeid).html(value.officename));
                        });
                    }
              });

            }
            function NumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }

            function ShowData() {
                var GRN = $('#idGRN').val();
                if (GRN == null || GRN == "") {
                    alert("Please Enter GRN");
                }
                else {

                    $.ajax({
                        type: 'POST',
                        url: '<%= ResolveUrl("~/WebPages/Department/EgEditOffice.aspx/GetGRNDetail") %>',
                    data: '{"GRN":"' + GRN + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {

                        if (msg.d.length > 0) {
                            if (msg.d == '-2') {
                                alert('Invalid Password');
                                return;
                            }
                            if (msg.d == '-1') {
                                alert('Grn Does not Belongs to Particular Dept.');
                                return;
                            }

                            $("#tbDetails").show();
                            $("#tbDetails").empty();
                            var jsonarr = msg.d.split('|');
                            var OfficeList = JSON.parse(jsonarr[0]);
                            var District = JSON.parse(jsonarr[1]);
                            var GetDistCode = JSON.parse(jsonarr[2]);
                            var mySelect = $('#tbDetails tbody');
                            var val = 0;

                            var row = $("#tbDetails").append('<tr class="padding"><td colspan="2"><b>District :</b><select id="ddlDistrict" onchange="GetSelectedTextValue(this)" style="margin-left: 38px"></select></td></tr><tr class="padding"><td colspan="2"><b> Office Name :</b> <select id ="ddlOffice"></select></td></tr><br>');
                            $.each(GetDistCode, function (index, obj) {
                                DistVal = obj.DistrictCode.toString();

                                OfficeValue = obj.OfficeName.toString();
                                deptcode = obj.Deptcode.toString();
                                $("#tbDetails").append('<tr class="padding" style="display:none"><td><input id="deptcode" type="input" value=' + deptcode + ' /></td></tr>');
                            });

                            $.each(OfficeList, function (data, value) {

                                $("#ddlOffice").append($("<option></option>").val(value.officeid).html(value.officename));
                            });
                            $("#ddlOffice").val(OfficeValue);
                            $.each(District, function (data, value) {

                                $("#ddlDistrict").val(DistVal);
                                $("#ddlDistrict").append($("<option></option>").val(value.TreasuryCode).html(value.TreasuryName));
                            });

                            $("#tbDetails").append('<tr class="padding"><td><input id="updtbtn" type="button" value="Update" onClick="UpdateData(); return false;"/></td></tr>');
                        }
                        else {
                            alert("No Record found");
                        }
                    }

                });
                $("#idGRN").attr("disabled", "true");
                $("#reset").show()

            }
        }


    </script>


    <fieldset runat="server" id="lstrecord" style="width: 1000px;">
        <asp:HiddenField ID="hdnRnd" runat="server" />
        <legend style="color: #336699; font-weight: bold">Transfer Office</legend>
        <div id="sh1">
            <div class="row">
                <div class="col-md-4" style="text-align: center">
                    <b>GRN:</b>
                    <input id="idGRN" type="number" maxlength="12" style="width: 20%; height: 20px; font-size: 18px;" min="1" onkeypress="Javascript:return NumberOnly(event)" />
                    <input id="inpsubmit" style="margin-left: 26px; height: 25px; width: 10%" type="submit" onclick="ShowData(); return false" value="Submit" />
                    <input id="reset" type="submit" value="Reset" style="margin-left: 26px; height: 25px; width: 10%" />
                </div>

            </div>
            <br />
            <br />

        </div>
        <br />
        <br />
        <table id="tbDetails" cellspacing="0" style="margin-top: auto; margin-left: 150px; width: 70%; border: 1px solid gray;">
        </table>

    </fieldset>
</asp:Content>

