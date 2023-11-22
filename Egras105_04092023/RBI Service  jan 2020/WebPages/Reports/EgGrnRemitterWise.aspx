<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgGrnRemitterWise.aspx.cs" Inherits="WebPages_Reports_EgGrnRemitterWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/moment.js"></script>
    <link href="../../CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../js/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap-datetimepicker.min.js"></script>
    <script src="../../js/jquery.dataTables.min.js"></script>
    <script src="../../js/CDNFiles/jquery.mask.js"></script>
    <script src="../../js/CDNFiles/pdfmake.min.js"></script>
    <script src="../../js/CDNFiles/vfs_fonts.js"></script>
    <script src="../../js/CDNFiles/buttons.print.min.js"></script>
    <script src="../../js/CDNFiles/dataTables.buttons.min.js"></script>
    <script src="../../js/CDNFiles/jszip.min.js"></script>
    <script src="../../js/CDNFiles/buttons.html5.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('#ajaxloader').hide();
            $('#datetimepicker1,#datetimepicker2').datetimepicker({
                format: 'DD/MM/YYYY',
                ignoreReadonly: true,
                minDate: '2010/12/01',
                maxDate: new Date(),
                defaultDate: Date()
            });
            $('#datetimepicker1').datetimepicker().on('dp.change', function (e) {
                var incrementDay = moment(new Date(e.date));
                //incrementDay.add(1, 'days');
                $('#datetimepicker2').data('DateTimePicker').minDate(incrementDay);
                $(this).data("DateTimePicker").hide();
            });

            $('#datetimepicker2').datetimepicker().on('dp.change', function (e) {
                var decrementDay = moment(new Date(e.date));
                //decrementDay.subtract(1, 'days');
                $('#datetimepicker1').data('DateTimePicker').maxDate(decrementDay);
                $(this).data("DateTimePicker").hide();
            });


            $("#btnSubmit").click(function (e) {
                e.preventDefault();
                $("#spntotalamt").html("");
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var RemitterName = $('#txtRemitter').val();
                //End Date Range condition
                if ($.fn.DataTable.isDataTable("#tblDetails")) {
                    $('#tblDetails').DataTable().clear().destroy();
                }
                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgGrnRemitterWise.aspx/GetRemitterWiseReport") %>',
                    data: '{"FromDate":"' + FromDate + '","ToDate":"' + ToDate + '","RemitterName":"' + RemitterName + '"}',
                    contentType: 'application/json; charset=utf-8',
                    "timeout": 5000000,
                    dataType: "json",
                    success: function (data) {
                        $("#tblDetails").show();
                        var datatableVariable = $('#tblDetails').DataTable({
                            "data": JSON.parse(data.d),
                            "paging": true,
                            "ordering": true,
                            "searching": true,
                            "destroy": true,
                            dom: 'Bfrtip',
                            buttons: [
                                {
                                    extend: 'pdfHtml5',
                                    filename: 'RemitterWiseGrn',
                                    title: 'RemitterWiseGrn',
                                    messageTop: 'FromDate: ' + FromDate + '     ToDate: ' + ToDate + '',
                                    pageSize: 'A4', // Default
                                },
                                {
                                    extend: 'excel',
                                    title: 'RemitterWiseGrn',
                                    filename: 'RemitterWiseGrn',
                                },
                                {
                                    extend: 'print',
                                    title: 'RemitterWiseGrn',
                                    filename: 'RemitterWiseGrn',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default

                                },
                                {
                                    extend: 'copy',
                                    title: 'RemitterWiseGrn',
                                    filename: 'RemitterWiseGrn',

                                },
                            ],
                            columns: [
                                {
                                    //"sortable": false,
                                    "data": null,  "class": "text-center", "title": "S.N.",
                                    render: function (data, type, row, meta) {
                                        return meta.row + meta.settings._iDisplayStart + 1;
                                    }
                                },
                                {
                                    "data": "GRN", "title": "GRN", "class": "text-center", "render": function (data, type, row, meta) {
                                        if (type === 'display') {
                                            data = '<a class="abc" data-grn="' + data + '"  style="font-weight:600">' + (data) + '</a>';
                                        }
                                        return data;
                                    }
                                },
                                {
                                    "data": "FullName",
                                    "title": "Full Name",
                                    "class": "text-center"

                                },
                                {
                                    "data": "MobileNo",
                                    "title": "Contact Number",
                                    "class": "text-center"//,
                                   
                                },
                                {
                                    "data": "ChallanDate",
                                    "title": "Challan Date",
                                    "class": "text-center",

                                },
                                {
                                    "data": "Amount",
                                    "title": "Total Amount",
                                    "class": "text-right"//,
                                    
                                }
                            ],
                            
                            "footerCallback": function (row, data, start, end, display) {
                                debugger;
                                var api = this.api(), data;

                                // Remove the formatting to get integer data for summation
                                var intVal = function (i) {
                                    return typeof i === 'string' ?
                                        i.replace(/[\$,]/g, '') * 1 :
                                        typeof i === 'number' ?
                                        i : 0;
                                };
                                //var total, pageTotal = 0;
                                // Total over all pages
                                totalGRN = api
                                    .column(5)
                                    .data()
                                    .reduce(function (a, b) {
                                        var c = intVal(a) + intVal(b);
                                        return c.toFixed(0);
                                    }, 0);
                                // Total GRN
                                total = api
                                    .column(5)
                                    .data()
                                    .reduce(function (a, b) {
                                        var c = intVal(a) + intVal(b);
                                        return c.toFixed(2);
                                    }, 0);    
                                $(api.column(5).footer()).html(                                   
                                    ' Total Amount: ₹' + total
                                );
                            }

                        });
                        $('#ajaxloader').hide();
                    },

                    error: function (error) {
                        $('#ajaxloader').hide();
                        alert(error.toString());
                    }
                })
            });
            $('#tblDetails').on('click', 'a.abc', function () {

                var argObj = window;
                var id = $(this).data('grn');

                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgBudgetHeadWiseReport.aspx/GetEncryptParam") %>',
                    data: '{"id":"' + id + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        window.open('../EgDefaceDetailNew.aspx?' + escape(msg.d), "", "_blank");
                    }
                });
            });
        });
    </script>
    <style type="text/css">
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

        #ajaxloader {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            display: none;
        }

        @-webkit-keyframes spin {
            from {
                -webkit-transform: rotate(0deg);
            }

            to {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        #ajaxloader::after {
            content: '';
            display: block;
            position: absolute;
            left: 48%;
            top: 40%;
            width: 70px;
            height: 70px;
            border-style: solid;
            border-color: #1b2a47;
            border-top-color: lightcyan;
            border-width: 10px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }
    </style>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Bunch Challan">
            <span _ngcontent-c6="" style="color: #FFF">Remitter Wise GRN</span></h2>
        <img src="../../Image/help1.png" style="height: 44px; width: 34px;" title="Bunch Challan" />
    </div>





    <table width="100%" style="text-align: center" align="center" border="1">
        <tr>
            <td align="left">
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                       
                        <div class='input-group date' id='datetimepicker1' style="width: 50%; display: inline-table">
                            <input id="txtFromDate" type="text" class="form-control" style="height: 33px" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
            </td>
            <td align="left">
                <b><span style="color: #336699">To Date : </span></b>&nbsp;

                        <div class='input-group date' id='datetimepicker2' style="width: 50%; display: inline-table">
                            <input id="txtToDate" type="text" class="form-control" style="height: 33px" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
            </td>

            <td align="left">
                <b><span style="color: #336699">Remitter Name: </span></b>&nbsp;
                 <div class='input-group date' style="width: 50%; display: inline-table">
                     <input id="txtRemitter" type="text" class="form-control" style="height: 33px" />
                 </div>
            </td>

            <td align="left">

                <div style="display: inline-table">
                    <button id="btnSubmit" type="submit" style="height: 33px" class="btn btn-default">Submit</button>
            </td>
        </tr>

    </table>
    <br />
    <span class="row col-md-12" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: right; text-align: right;"></span>
    <div class="row col-md-12" align="left" id="trrpt" runat="server">
        <table id="tblDetails" class="table table-responsive table-striped table-bordered" style="display:none">
            <tbody><tr></tr></tbody>
            <tfoot>
                <tr>
                    <th style="text-align: right" colspan="6"></th>
                </tr>
            </tfoot>
        </table>
    </div>
    <div id="ajaxloader">
    </div>
</asp:Content>

