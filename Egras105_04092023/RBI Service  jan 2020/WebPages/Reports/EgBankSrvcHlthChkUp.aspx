<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgBankSrvcHlthChkUp.aspx.cs" Inherits="WebPages_Reports_EgBankSrvcHlthChkUp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/moment.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../js/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.dataTables.min.js"></script>
    <link href="../../CSS/jquery.dataTables.min.css" rel="stylesheet" />

    <style type="text/css">
        input[type=text] {
            height: 0%;
        }
    </style>
    <script type="text/javascript">
        function show1() {
            document.getElementById('tblBankDetail1').style.display = 'block';
            document.getElementById('tblBankDetail2').style.display = 'none';
        }
        function GetStatus(bsrcode) {
            $('#myDiv').css('visibility', 'visible');
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/Reports/EgBankSrvcHlthChkUp.aspx/GetBankStatus") %>',
                data: '{"BSRCode":"' + bsrcode + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {


                    if (msg.d == 1) {
                        $('#myDiv').css('visibility', 'hidden');
                        $("#sp_" + bsrcode).text('Active');
                    }
                    else {
                        $('#myDiv').css('visibility', 'hidden');
                        $("#sp_" + bsrcode).text('InActive');
                    }
                }
                      , failure: $('#myDiv').css('visibility', 'hidden')
            });
        }
        function show2() {
            document.getElementById('tblBankDetail1').style.display = 'none';
            document.getElementById('tblBankDetail2').style.display = 'block';

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/Reports/EgBankSrvcHlthChkUp.aspx/GetBankData") %>',
                data: '',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {

                    if (msg.d.length > 0) {
                        $("#<%=trrpt1.ClientID %>").show();
                        $("#tbDetails2 thead").empty();
                        $("#tbDetails2 tbody").empty();
                        var json = JSON.parse(msg.d);
                        var column = '<tr><td class="paddingtd">BankName</td> <td class="paddingtd"></td><td class="paddingtd">Status</td></tr>'
                        $("#tbDetails2 thead").append(column);
                        $.each(json, function (index, obj) {

                            //var a = obj.BSRCode
                            var row = '<tr><td>' + obj.BankName + '</td><td><input style="margin-left: 12px;    margin-top: 3px;margin-bottom: 3px;" type="button" value="Get Status" onClick="GetStatus(' + "'" + obj.BSRCode + "'" + ');"/></td><td><span id="sp_' + obj.BSRCode + '"></span></td></tr>'

                            $("#tbDetails2 tbody").append(row);
                        });
                    }
                    else {
                        alert("No Record found");
                    }
                }
            });
        }
        $(document).ready(function () { $("#<%=trrpt.ClientID %>").hide(); $("#<%=trrpt1.ClientID %>").hide(); });
        $(function () {
            //$("#Frmdatetimepicker").datepicker({

            //    onSelect: function (selected) {

            //        $('#Todatetimepicker').val('');
            //        $("#DateError").text('')
            //    }
            //});
            //$("#Todatetimepicker").datepicker({
            //    onSelect: function (selected) {

            //        var fromdate = $('#Frmdatetimepicker').val();
            //        var todate = selected;
            //        if (fromdate > todate) {
            //            $("#DateError").text('ToDate must be greater or equal FromDate');
            //            $('#Todatetimepicker').val('');

            //        }
            //        else {
            //            $("#DateError").text('');
            //        }
            //    }
            //});
            $('#Frmdatetimepicker,#Todatetimepicker').datetimepicker({
                format: 'DD/MM/YYYY',
                ignoreReadonly: true,
                minDate: '2010/12/01',
                maxDate: new Date(),
                defaultDate: Date()
            });
            $('#Frmdatetimepicker').datetimepicker().on('dp.change', function (e) {
                var incrementDay = moment(new Date(e.date));
                //incrementDay.add(1, 'days');
                $('#Todatetimepicker').data('DateTimePicker').minDate(incrementDay);
                $(this).data("DateTimePicker").hide();
            });
            $('#Todatetimepicker').datetimepicker().on('dp.change', function (e) {
                var decrementDay = moment(new Date(e.date));
                //decrementDay.subtract(1, 'days');
                $('#Frmdatetimepicker').data('DateTimePicker').maxDate(decrementDay);
                $(this).data("DateTimePicker").hide();
            });
        });

        function ShowData() {

            var fromdate = $('#txtFromDate').val();
            var todate = $('#txtToDate').val();
            var bsrcode = $("#<%=ddlbanks.ClientID %>").val();

            var ddf = fromdate.split('/')[0];
            var mmf = fromdate.split('/')[1]; //January is 0!
            var yyyyf = fromdate.split('/')[2];

            var ddt = todate.split('/')[0];
            var mmt = todate.split('/')[1]; //January is 0!
            var yyyyt = todate.split('/')[2];

            fromdate = mmf + '/' + ddf + '/' + yyyyf;
            todate = mmt + '/' + ddt + '/' + yyyyt;


            if (fromdate && todate == "" || fromdate == "" || todate == null) {
                alert("Please select Date");
            }
            else {

                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgBankSrvcHlthChkUp.aspx/GetErrorData") %>',
                    data: '{"FromDate":"' + fromdate.toString("MM/dd/YYYY") + '","ToDate":"' + todate.toString("MM/dd/YYYY") + '","BsrCode":"' + bsrcode + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {

                        if (msg.d.length > 2) {

                            $("#<%=trrpt.ClientID %>").show();
                            //$("#tbDetails").empty();
                            $("#tbDetails thead").empty();
                            $("#tbDetails tbody").empty();
                            var json = JSON.parse(msg.d);
                            var column = '<tr><td>S No</td><td>Bank Name</td> <td>Service Type</td> <td>Bank Url</td> <td>Error Name</td><td>Trans Date</td></tr>'
                            $("#tbDetails thead").append(column);
                            $.each(json, function (index, obj) {
                                var row = '<tr><td>' + obj.SNo + '</td><td>' + obj.BankName + '</td><td>' + obj.ServiceType + '</td> <td>' + obj.BankUrl + '</td> <td>' + obj.ErrorName + '</td><td>' + obj.TransDate + '</td></tr>'
                                $("#tbDetails tbody").append(row);
                                //alert($("#tbDetdynails tbody").i);
                            });

                        }
                        else {
                            alert("No Record found");
                        }

                    }
                });
            }
        }
    </script>

    <fieldset runat="server" id="lstrecord" style="width: 1000px;">
        <legend style="color: #336699; font-weight: bold">Health CheckUp </legend>

        <div id='myDiv' style="visibility: hidden;">
            <img id="dwnldgif" alt="" style='position: absolute; width: 100px; left: 45%;' src="../../Image/waiting_process.gif" />
            <input type="text" size="80px" style="left: 32%; position: absolute; margin-top: 100px;" value="Wait while redirect to bank Site, don't press back or refresh button" />
        </div>
        <table style="width: 100%" align="center">
            <tr id="trrbl">
                <td colspan="2" align="center">
                    <input type="radio" id="rd1" name="tab" value="1" checked="checked" onclick="show1();" />
                    Report
                </td>
                <td colspan="2">
                    <input type="radio" id="rd2" name="tab" value="2" onclick="show2();" />
                    Status
                </td>
            </tr>
        </table>
        <table style="width: 100%" align="center">

            <tr style="height: 45px" id="tblBankDetail1">
                <td align="center">
                    <b><span style="color: #336699">Bank:-</span></b>&nbsp;
                    <asp:DropDownList ID="ddlbanks" runat="server" Width="70%">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <b><span style="color: #336699">From Date : </span></b>&nbsp;
                        <div class='input-group date' id='Frmdatetimepicker' style="width: 50%; display: inline-table">
                            <input id="txtFromDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                            <%--<b><span style="color: #336699">From Date:-</span></b>&nbsp;
                    <input id="Frmdatetimepicker" />--%>
                </td>
                <td>
                    <b><span style="color: #336699">To Date : </span></b>&nbsp;
                        <div class='input-group date' id='Todatetimepicker' style="width: 50%; display: inline-table">
                            <input id="txtToDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                            <%--<b><span style="color: #336699">To Date:-</span></b>&nbsp;
                    <input id="Todatetimepicker" />--%>
                </td>
                <td align="center">
                    <asp:Button ID="btnShow" runat="server" ValidationGroup="de" Width="36%" Text="Show" OnClientClick="ShowData(); return false;" />
                    <asp:Button ID="btncbi" Text="CBiManual" runat="server" OnClick="btncbi_Click" Width="58%" />
                </td>

            </tr>
            <tr align="center" id="alert" runat="server">
                <td colspan="4">
                    <asp:Label ID="lblalert" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr align="center" id="trrpt" runat="server">
                <td colspan="4">
                    <table id="tbDetails" border="1" width="100%" cellpadding="0" cellspacing="0">
                        <thead style="background-color: #507CD1; color: White; font-weight: bold; height: 20px; text-align: center">
                        </thead>
                        <tbody>
                        </tbody>
                    </table>

                </td>
            </tr>
        </table>

        <table style="width: 44%; margin-top: 15px;" align="center" id="tblBankDetail2">

            <tr align="center" id="trrpt1" runat="server">
                <td colspan="4">
                    <table id="tbDetails2" border="1" width="100%" cellpadding="0" cellspacing="0">
                        <thead style="background-color: #507CD1; color: White; font-weight: bold; height: 20px; text-align: center">
                        </thead>
                        <tbody>
                        </tbody>
                    </table>

                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>


