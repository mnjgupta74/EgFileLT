﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EGGenerateChallan.aspx.cs" Inherits="WebPages_EGGenerateChallan" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/moment.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../js/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../js/bootstrap-datetimepicker.min.js"></script>
    <link href="../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <link href="../CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../js/CDNFiles/jquery.mask.js"></script>
    <script src="../js/CDNFiles/pdfmake.min.js"></script>
    <script src="../js/CDNFiles/vfs_fonts.js"></script>
    <script src="../js/CDNFiles/buttons.print.min.js"></script>
    <script src="../js/CDNFiles/dataTables.buttons.min.js"></script>
    <script src="../js/CDNFiles/jszip.min.js"></script>
    <script src="../js/CDNFiles/buttons.html5.min.js"></script>
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #337ab7;
        }

        .dataTables_wrapper {
            padding: 1%;
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

        .dataTables_wrapper .dataTables_paginate {
            background-color: #428bca;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#TblGenerateChallan").hide();
            $(".dtGenerateChallan").hide();

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

            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/WebPages/EGGenerateChallan.aspx/GetBanks") %>',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (data) {

                    var ddlbank = $("[id*=ddlbank]");
                    ddlbank.empty();
                    $('#ddlbank').append('<option value="0">--- Select Bank ---</option>');
                    $.each(JSON.parse(data.d), function (data, value) {
                        ddlbank.append($("<option></option>").val(value.BSRCode).html(value.BankName));
                    })
                },
                error: function (error) {
                    alert(error.toString());
                }
            })

            $("#btnShow").click(function (e) {
                $("#TblGenerateChallan").hide();
                $(".dtGenerateChallan").hide();

                e.preventDefault();
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var Paytype = $("input:radio[name='paytype']:checked").val();
                var BankCode = $('#ddlbank').val();
                var startDate = moment(FromDate, "DD/MM/YYYY");
                var endDate = moment(ToDate, "DD/MM/YYYY");
                var result = endDate.diff(startDate, 'days');
                if ((result) > 30) {
                    $('#ajaxloader').hide();
                    alert('Date Difference can not be more than 30 days.');
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("~/WebPages/EGGenerateChallan.aspx/GetData") %>',
                        data: '{"Paytype":"' + Paytype + '", "BankCode":"' + BankCode + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: "json",
                        success: function (data) {
                            if (data.d != '[]') {
                                $("#TblGenerateChallan").show();
                                $(".dtGenerateChallan").show();
                                var datatableVariable = $('#TblGenerateChallan').DataTable({
                                    dom: 'Bfrtip',
                                    buttons: [
                                        {
                                            extend: 'pdfHtml5',
                                            filename: 'Generate Challan',
                                            title: 'Government of Rajasthan \n Generate Challan',
                                            pageSize: 'A4', // Default
                                            //orientation: 'landscape',
                                            orientation: 'landscape', // Default
                                            footer: true,
                                            className: 'btn btn-default',
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
                                       "data": null, "sortable": false, "title": "S.No",
                                       "render": function (data, type, row, meta) {
                                           {
                                               return meta.row + meta.settings._iDisplayStart + 1;
                                           }
                                       }
                                   },
                                        {
                                            "data": "GRN", "title": "GRN", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }
                                            //[{"RemitterName":"HAZARIMAL & COMPANY","CIN":"000632604516401012015","TIN":"08853354830","DepositDate":"01/01/2015","Amount":384.0000,"Grn":4542533}
                                        },
                                        {
                                            "data": "FullName", "title": "RemitterName", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }
                                        },
                                        {
                                            "data": "CIN", "title": "CIN", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }

                                        },
                                        {
                                            "data": "Challanno", "title": "ChallanNo", "render": function (data, type, row, meta) {
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

                                   }, ],
                                    "paging": true,
                                    "ordering": true,
                                    //"info": false,
                                    "searching": true,
                                    "destroy": true,
                                    columnDefs: [
                                {
                                    "targets": 5,
                                    "className": "text-right",
                                }],
                                    "footerCallback": function (row, data, start, end, display) {
                                        var api = this.api(), data;

                                        // Remove the formatting to get integer data for summation
                                        var intVal = function (i) {
                                            return typeof i === 'string' ?
                                                i.replace(/[\$,]/g, '') * 1 :
                                                typeof i === 'number' ?
                                                i : 0.00;
                                        };

                                        // Total over all pages
                                        total = api
                                            .column(5)
                                            .data()
                                            .reduce(function (a, b) {
                                                return intVal(a) + intVal(b);
                                            }, 0);

                                        // Total over this page
                                        pageTotal = api
                                            .column(5, { page: 'current' })
                                            .data()
                                            .reduce(function (a, b) {
                                                return intVal(a) + intVal(b);
                                            }, 0);
                                        $(api.column(5).footer()).html(

                                            '  Total Amount: ₹ ' + total.toFixed(2) + ''
                                        );
                                    },
                                    "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                                        $("td:first", nRow).html(iDisplayIndex + 1);
                                        return nRow;
                                    }
                                });
                            }
                            else {
                                $(".dtGenerateChallan").hide();
                                $("#TblGenerateChallan").hide();
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

            });
            $("#btnSubmit").click(function (e) {
                $("#TblGenerateChallan").hide();
                $(".dtGenerateChallan").hide();

                e.preventDefault();
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var Paytype = $("input:radio[name='paytype']:checked").val();
                var BankCode = $('#ddlbank').val();
                var startDate = moment(FromDate, "DD/MM/YYYY");
                var endDate = moment(ToDate, "DD/MM/YYYY");
                var result = endDate.diff(startDate, 'days');
                if ((result) > 30) {
                    $('#ajaxloader').hide();
                    alert('Date Difference can not be more than 30 days.');
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("~/WebPages/EGGenerateChallan.aspx/GetSummeryData") %>',
                        data: '{"Paytype":"' + Paytype + '", "BankCode":"' + BankCode + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: "json",
                        success: function (data) {
                            $.each(JSON.parse(data.d), function (index, value) {
                                //$('#txtTransaction').val(value.TotalGRN);
                                $('#txtTransaction').html(value.TotalGRN);
                                $('#txtAmount').html(value.TotalAmount);
                            });
                            //alert($('#txtTransaction').html());
                            if ($('#txtTransaction').html() > 0 && $('#txtAmount').html() > 0) {
                                $('#trverify').show();
                            }
                            else {
                                $('#trverify').hide();
                                alert('No record found !');
                            }
                            $('#ajaxloader').hide();
                        },
                        error: function (error) {
                            $('#trverify').hide();
                            $('#ajaxloader').hide();
                            alert(error.toString());
                        }
                    })
                }

            });
            $("#btnVerify").click(function (e) {
                $("#TblGenerateChallan").hide();
                $(".dtGenerateChallan").hide();

                e.preventDefault();
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var Paytype = $("input:radio[name='paytype']:checked").val();
                var BankCode = $('#ddlbank').val();
                var startDate = moment(FromDate, "DD/MM/YYYY");
                var endDate = moment(ToDate, "DD/MM/YYYY");
                var result = endDate.diff(startDate, 'days');
                var transaction = $('#txtTransaction').html();
                var totalAmount = $('#txtAmount').html();

                if ((result) > 30) {
                    $('#ajaxloader').hide();
                    alert('Date Difference can not be more than 30 days.');
                }
                else if ($('#txtTransaction').html() <= 0 || $('#txtAmount').html() <= 0) {
                    $('#ajaxloader').hide();
                    alert('Total Transaction or Amount Should not be 0 !');
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("~/WebPages/EGGenerateChallan.aspx/GenerateGrnChallanNo") %>',
                        data: '{"Paytype":"' + Paytype + '", "BankCode":"' + BankCode + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '","transaction":"' + transaction + '","totalAmount":"' + totalAmount + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: "json",
                        success: function (data) {

                            if (JSON.parse(data.d) != '0') {
                                $('#ajaxloader').hide();

                                $('#trverify').hide();
                                alert('Record Updated Successfully!');
                            }
                            else {
                                $('#trverify').show();
                                alert('No record Updated !');
                            }
                            $('#ajaxloader').hide();
                        },
                        error: function (error) {
                            $('#trverify').hide();
                            $('#ajaxloader').hide();
                            alert(error.toString());
                        }
                    })
                }

            });
        });
    </script>
    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h2 _ngcontent-c6="" title="BudgetHeadDetail">
                <span _ngcontent-c6="" style="color: #FFF">Generate Challan</span></h2>
        </div>
        <table width="100%" style="text-align: center" align="center" border="1">
            <tr>
                <td align="left">
                    <b><span style="color: #336699">From Date : </span></b>&nbsp;
                        <div class='input-group date' id='datetimepicker1' style="width: 50%; display: inline-table">
                            <input id="txtFromDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                </td>
                <td align="left">
                    <b><span style="color: #336699">To Date : </span></b>&nbsp;
                        <div class='input-group date' id='datetimepicker2' style="width: 50%; display: inline-table">
                            <input id="txtToDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                </td>
                <td align="left">
                    <b><span style="color: #336699">Bank </span></b>&nbsp;
                <select id="ddlbank" class="selectpicker form-control" style="width: 80%; display: inline-table">
                    <option value="0">--- Select Bank ---</option>
                </select>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 35px" colspan="2">
                    <div class="form-control">
                        <input type='radio' name='paytype' value='N' title="Online" checked="checked" class="radio radio-inline" style="margin-top: 0px; margin-right: 10px;">Online
                <input type='radio' name='paytype' value='M' title="Manual" style="margin-left: 35px; margin-top: 0px; margin-right: 10px" class="radio radio-inline">Manual
                    </div>
                </td>
                <td align="center">
                    <button id="btnSubmit" type="submit" class="btn btn-default">Show</button>
                </td>
            </tr>
            <tr id="trverify" style="display: none;">
                <td align="center" style="height: 35px">
                    <div class="form-control">
                        <label>Total Transaction : </label>
                        <label id="txtTransaction"></label>
                        <%--<input type="text" name="txtTransaction" id="txtTransaction" disabled />--%>
                    </div>
                </td>
                <td align="center" style="height: 35px">
                    <div class="form-control" style="padding: 6px 0px !important;">
                        <label>Total Amount : </label>
                        <label id="txtAmount"></label>
                        <%--<input name="txtAmount" id="txtAmount" disabled />--%>
                    </div>
                </td>
                <td align="center">
                    <button id="btnVerify" type="submit" class="btn btn-default">Verify</button>
                    <button id="btnShow" type="submit" class="btn btn-default">View Records</button>
                </td>
            </tr>
        </table>
    </div>
    <div class="row dtGenerateChallan">
        <table id="TblGenerateChallan" cellspacing="0" style="display: none; background-color: #428bca; color: white; text-align: center" border="1">
            <tbody style="color: black">
            </tbody>
            <tfoot>
                <tr>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: right"></th>
                </tr>
            </tfoot>
        </table>
    </div>
    <div id="ajaxloader">
    </div>
    
</asp:Content>
