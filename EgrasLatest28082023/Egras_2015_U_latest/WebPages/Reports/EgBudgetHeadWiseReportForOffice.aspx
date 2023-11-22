<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgBudgetHeadWiseReportForOffice.aspx.cs" Inherits="WebPages_Reports_EgBudgetHeadWiseReportForOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/moment.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../js/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap-datetimepicker.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/jquery.dataTables.min.js"></script>
    <link href="../../CSS/jquery.dataTables.min.css" rel="stylesheet" />

    <script src="../../js/CDNFiles/dataTables.buttons.min.js"></script>
    <script src="../../js/CDNFiles/pdfmake.min.js"></script>
    <script src="../../js/CDNFiles/vfs_fonts.js"></script>
    <script src="../../js/CDNFiles/jquery.mask.js"></script>
    <script src="../../js/CDNFiles/buttons.print.min.js"></script>
    <script src="../../js/CDNFiles/jszip.min.js"></script>
    <script src="../../js/CDNFiles/buttons.html5.min.js"></script>




    <script type="text/javascript">
        $(document).ready(function () {
            $("#TblBudgetHeadDetail").hide();
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
            $("#txtBudgetHead").mask("9999-99-999-99-99");
            $("#btnSubmit").click(function (e) {
                $("#TblBudgetHeadDetail").hide();
                e.preventDefault();
                $('#btnSubmit').prop('disabled', true);
                $('#ddlTreasury').prop('disabled', true);
                $('#txtBudgetHead').prop('disabled', true);
                $('#txtFromDate').prop('disabled', true);
                $('#txtToDate').prop('disabled', true);
                $("#spntotalamt").html("");
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var BudgetHead = $('#txtBudgetHead').val().split('-').join('');
                var TreasuryCode = $('#ddlTreasury').val();
                var startDate = moment(FromDate, "DD/MM/YYYY");
                var endDate = moment(ToDate, "DD/MM/YYYY");
                var result = endDate.diff(startDate, 'days');
                if ((result) > 90) {
                    $('#ajaxloader').hide();
                    alert('Date Difference can not be more than 90 days.');
                    // $('#btnSubmit').prop('disabled', false);
                }
                else {

                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("~/WebPages/Reports/EgBudgetHeadWiseReportForOffice.aspx/GetData") %>',
                        data: '{"BudgetHead":"' + BudgetHead + '", "TreasuryCode":"' + TreasuryCode + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: "json",
                        success: function (data) {
                            if (data.d != '[]') {
                                $("#TblBudgetHeadDetail").show();
                                var datatableVariable = $('#TblBudgetHeadDetail').DataTable({
                                    dom: 'Bfrtip',
                                    buttons: [
                                        {
                                            extend: 'pdfHtml5',
                                            filename: 'BudgetHeadDetail',
                                            title: 'Government of Rajasthan \n BudgetHead Detail for Office',
                                            pageSize: 'A4', // Default
                                            orientation: 'landscape',
                                            //orientation: 'portrait', // Default
                                            footer: true,
                                            customize: function (doc) {
                                                doc.styles.tableHeader.fontSize = 10;
                                                doc.defaultStyle.fontSize = 10;
                                                doc.defaultStyle.alignment = 'center'
                                            }
                                        }

                                    ],
                                    data: JSON.parse(data.d),
                                    columns: [
                                   {
                                       "data": "Rownum", "title": "S.No", "render": function (data, type, row, meta) {
                                           if (type === 'display') {
                                               data = '<span style="color:black">' + data + '</span>';
                                           }
                                           return data;
                                       }
                                   },
                                        {
                                            "data": "GRN", "title": "GRN", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<a class="abc" data-grn="' + data + '"  style="font-weight:600">' + (data) + '</a>';
                                                    //return data;
                                                    //data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }

                                        },
                                        {
                                            "data": "RemitterName", "title": "RemitterName", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }
                                        },
                                        {
                                            "data": "challanno", "title": "ChallanNo", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }

                                        },
                                        {
                                            "data": "BudgetHead", "title": "BudgetHead", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }

                                        },

                                    {
                                        "data": "DepositDate", "title": "DepositDate", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + moment(data).format("DD/MM/YYYY") + '</span>';
                                            }
                                            return data;
                                        }
                                    },

                                    {
                                        "data": "BankName", "title": "BankName", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + data + '</span>';
                                            }
                                            return data;
                                        }
                                    },
                                   {
                                       "data": "Amount", "title": "Amount", className: "text-right", "render": function (data, type, row, meta) {
                                           if (type === 'display') {
                                               data = '<span style="color:black">' + data.toFixed(2) + '</span>';
                                           }
                                           return data;
                                       }

                                   },
                                   {
                                       "data": "gROSSAMT", "title": "GrossAmount", className: "text-right", "render": function (data, type, row, meta) {
                                           if (type === 'display') {
                                               data = '<span style="color:black">' + data.toFixed(2) + '</span>';
                                           }
                                           return data;
                                       }

                                   }, ],
                                    "paging": true,
                                    "ordering": true,
                                    //"info": false,
                                    "searching": true,
                                    "destroy": true,
                                    columnDefs: [
                                {
                                    "targets": 3,
                                    "className": "text-right",
                                }],
                                    "footerCallback": function (row, data, start, end, display) {
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
                                        total = api
                                            .column(7)
                                            .data()
                                            .reduce(function (a, b) {
                                                return intVal(a) + intVal(b);
                                            }, 0);
                                        // Total over this page
                                        pageTotal = api
                                            .column(7, { page: 'current' })
                                            .data()
                                            .reduce(function (a, b) {
                                                return intVal(a) + intVal(b);
                                            }, 0);
                                        $(api.column(7).footer()).html(
                                            'TotalAmount: ₹' + total + ''
                                        );
                                    }
                                });
                            }
                            else {
                                $("#TblBudgetHeadDetail").hide();
                                alert('No record found !');
                            }
                            $('#ajaxloader').hide();
                        },
                        error: function (error) {
                            $('#ajaxloader').hide();
                            alert(error.toString());
                        }
                    })
                }
                $('#TblBudgetHeadDetail').on('click', 'a.abc', function () {
                    var argObj = window;
                    var id = $(this).data('grn');
                    $.ajax({
                        type: 'POST',
                        url: '<%= ResolveUrl("~/WebPages/Reports/EgBudgetHeadWiseReportForOffice.aspx/GetEncryptParam") %>',
                        data: '{"id":"' + id + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (msg) {
                            window.open('../EgDefaceDetailNew.aspx?' + escape(msg.d), "", "_blank");
                        }
                    });
                });
            });
        });
    </script>
    <style>
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

        .btn {
            height: auto !important;
            border-radius: 0px !important;
        }

        input[type=text], input[type=password] {
            height: auto !important;
        }

        input[type="radio"].toggle + label:after {
            height: 101%;
        }

        .dataTables_wrapper .dataTables_paginate {
            background-color: #1b2a47;
        }

        .btn-primary {
            color: #fff;
            background-color: #1b2a47;
            border-color: #1b2a47;
        }

            .btn-primary:hover, .btn-primary:after {
                color: #fff;
                background-color: #36548e;
                border-color: #1b2a47;
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
    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h2 _ngcontent-c6="" title="BudgetHeadDetail">
                <span _ngcontent-c6="" style="color: #FFF">Budget Head Detail</span></h2>
        </div>
        
        <div class="row">
            <div class="col-md-3">
                <label>FromDate:</label>
                <div class='input-group date' id='datetimepicker1'>
                    <input id="txtFromDate" type="text" class="form-control" readonly />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
            <div class="col-md-3">
                <label>ToDate:</label>
                <div class='input-group date' id='datetimepicker2'>
                    <input id="txtToDate" type="text" class="form-control" readonly />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
            <div class="col-md-3">
                <label>BudgetHead:</label>
                <div id='divBudgetHead'>
                    <input id="txtBudgetHead" type="text" class="form-control" />
                </div>
            </div>
            <div class="col-md-3">
                <label>Treasury:</label>
                <select id="ddlTreasury" class="form-control">
                    <option value="0">--All Treasury--</option>
                    <option value="0100">Ajmer</option>
                    <option value="0200">ALWAR</option>
                    <option value="0300">BANSWARA</option>
                    <option value="0400">BARAN</option>
                    <option value="0500">BARMER</option>
                    <option value="0600">BEAWAR</option>
                    <option value="0700">BHARATPUR</option>
                    <option value="0800">BHILWARA</option>
                    <option value="0900">BIKANER</option>
                    <option value="1000">BUNDI</option>
                    <option value="1100">CHITTORGARH</option>
                    <option value="1200">CHURU</option>
                    <option value="1300">DAUSA</option>
                    <option value="1400">DHOLPUR</option>
                    <option value="1500">DUNGARPUR</option>
                    <option value="1600">GANGANAGAR</option>
                    <option value="1700">HANUMANGARH    </option>
                    <option value="1800">JAIPUR (CITY) </option>
                    <option value="2000">JAIPUR (RURAL)</option>
                    <option value="2100">JAIPUR (SECTT.)</option>
                    <option value="2200">JAISALMER</option>
                    <option value="2300">JALORE    </option>
                    <option value="2400">JHALAWAR  </option>
                    <option value="2500">JHUNJHUNU </option>
                    <option value="2600">JODHPUR (CITY)</option>
                    <option value="2700">JODHPUR (RURAL)</option>
                    <option value="2800">KAROLI </option>
                    <option value="2900">KOTA  </option>
                    <option value="3000">NAGAUR</option>
                    <option value="3100">PALI  </option>
                    <option value="3200">PRATAPGARH</option>
                    <option value="3300">RAJSAMAND </option>
                    <option value="3400">SAWAI MADHOPUR</option>
                    <option value="3500">SIKAR </option>
                    <option value="3600">SIROHI</option>
                    <option value="3700">TONK  </option>
                    <option value="3800">UDAIPUR</option>
                    <option value="4100">UDAIPUR RURAL</option>
                </select>
            </div>
        </div>
        <div class="row text-center" style="margin-top: 10px; margin-bottom: 15px;">
            <button id="btnSubmit" type="submit" class="btn btn-primary">Submit</button>
            <button type="button" class="btn btn-default" onclick="document.location.reload()">Reset</button>
            <%--<button id="btnReset" type="button" class="btn btn-default">Reset</button>--%>
        </div>
    </div>
    <div class="row dtbudgethead">
        <table id="TblBudgetHeadDetail" cellspacing="0" style="background-color: #1b2a47; color: white; text-align: center" border="1">
            <tfoot>
                <tr>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: right"></th>
                    <th style="text-align: right"></th>
                </tr>
            </tfoot>
        </table>
    </div>
    <div id="ajaxloader">
    </div>
</asp:Content>

