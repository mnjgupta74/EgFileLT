<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgChallanWithORWithOutDivision.aspx.cs" Inherits="WebPages_Reports_EgChallanWithORWithOutDivision" %>

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

            $("#txtBudgetHead").mask("9999-99-999-99-99");
            $("#btnSubmit").click(function (e) {
                e.preventDefault();
                $("#spntotalamt").html("");
                $('#ajaxloader').show();
                var Type = $('input[type=radio][name=toggle]:checked').val();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var BudgetHead = $('#txtBudgetHead').val();

                if ($.fn.DataTable.isDataTable("#ChallanwithDivision")) {
                    $('#ChallanwithDivision').DataTable().clear().destroy();
                }

                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgChallanWithORWithOutDivision.aspx/GetChallanWithORWithOutDivision") %>',
                    data: '{"BudgetHead":"' + BudgetHead + '", "Type":"' + Type + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        var datatableVariable = $('#ChallanwithDivision').DataTable({
                            "data": JSON.parse(data.d),
                            "paging": true,
                            "ordering": true,
                            //"info": false,
                            "searching": true,
                            "destroy": true,
                            dom: 'Bfrtip',
                            buttons: [
                                {
                                    extend: 'pdf',
                                    filename: 'EgChallanWithORWithOutDivision',
                                    title: 'ChallanWithORWithOutDivision',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default

                                },
                                {
                                    extend: 'excel',
                                    title: 'ChallanWithORWithOutDivision',
                                    filename: 'EgChallanWithORWithOutDivision',
                                },
                                {
                                    extend: 'print',
                                    title: 'ChallanWithORWithOutDivision',
                                    filename: 'EgChallanWithORWithOutDivision',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default

                                },
                                {
                                    extend: 'copy',
                                    title: 'ChallanWithORWithOutDivision',
                                    filename: 'EgChallanWithORWithOutDivision',

                                },
                            ],

                            columnDefs: [
                        {

                            "targets": 3,
                            "className": "text-right",
                        }],
                            //"order": [[ 1, 'asc' ]],
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
                                    "data": "Grn",
                                    "class": "text-center"
                                },
                                //{
                                //    "data": "RemitterName",
                                //    "class": "text-center"

                                //},

                                {
                                    "data": "Challanno",
                                    "class": "text-center"

                                },
                            {
                                "data": "Challandate",
                                "class": "text-center",
                                "render": function (d) {
                                    return moment(d).format("DD/MM/YYYY");
                                }
                            },

                            {
                                "data": "OfficeName",
                                "class": "text-center",

                            },
                            {
                                "data": "DivCode",
                                "class": "text-center"
                            }, {
                                "data": "Amount",
                                "class": "text-right",
                                "render": function (data) {
                                    return data.toFixed(2);
                                }


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
                                    .column(6)
                                    .data()
                                    .reduce(function (a, b) {
                                        return intVal(a) + intVal(b);
                                    }, 0);
                                // Total over this page
                                pageTotal = api
                                    .column(6, { page: 'current' })
                                    .data()
                                    .reduce(function (a, b) {
                                        return intVal(a) + intVal(b);
                                    }, 0);
                                // Update footer
                                $("#spntotalamt").html('PageTotal: ₹' + pageTotal + ' (Total Amount: ₹' + total + ')');
                                //$(api.column(3).footer()).html(

                                //    'PageTotal: ₹' + pageTotal + ' (Total Amount: ₹' + total + ')'
                                //);
                            },

                        });
                        $('#ajaxloader').hide();
                    },
                    error: function (error) {
                        $('#ajaxloader').hide();
                        alert(error.toString());
                    }
                })
            });

            //$(function () {
            //    $('#datetimepicker1').datetimepicker();
            //    $('#datetimepicker2').datetimepicker();
            //});
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
    </style>
    <style type="text/css">
        .section2header {
            background: #3276b1;
        }

            .section2header h1 {
                font-size: 18px;
                color: #fff;
                padding: 1% 2%;
                font-weight: 500;
                margin-bottom: 0;
            }

        .side_nav {
            background: #f4f4f4;
            padding: 0;
        }

            .side_nav li.active {
                background: #fff;
                border-right: 3px solid #555;
                pointer-events: none;
            }


            .side_nav li {
                list-style: none;
                border-bottom: 1px solid #dedede;
            }

            .side_nav li {
                text-decoration: none;
                display: block;
                padding: 4% 6%;
            }

            .panel-title, .side_nav li, .txt {
                font-size: 17px;
                font-weight: 400;
                word-wrap: break-word;
            }

        ::-webkit-scrollbar {
            width: 5px;
        }

        ::-webkit-scrollbar-thumb {
            background: rgba(0, 0, 0, 0.56);
            border-radius: 10px;
        }

        ::-webkit-scrollbar-track {
            background: transparent;
        }

        [class*=" icon-"], [class^="icon-"] {
            font-size: 22px;
            vertical-align: -webkit-baseline-middle;
            padding-right: 5%;
        }

        h1 {
            margin: 0;
        }

        .sectionright {
            display: block;
            padding: 5% 6%;
            background-color: #f4f4f4;
        }
        /*.icon-internetbanking:before {
            content: "\63";
        }*/

        [class^="icon-"]:before, [class*=" icon-"]:before {
            font-family: "sbi-payment" !important;
            font-style: normal !important;
            font-weight: normal !important;
            font-variant: normal !important;
            text-transform: none !important;
            speak: none;
            line-height: 1;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        #BunchTable_wrapper {
            margin-bottom: 15px;
            border: 1px solid #dedede;
        }

        .btn {
            height: auto;
            border-radius: 0px;
        }

        #totalchallan, #totalamt {
            font-size: x-large;
        }

        label {
            font-weight: bold;
        }

        .panelcolor {
            padding: 15px;
            background-color: aliceblue;
        }
    </style>
    <style>
        .btnToggle {
            border: 1px solid #3276b1;;
            display: inline-block;
            padding: 6px;
            position: relative;
            text-align: center;
            transition: background 600ms ease, color 600ms ease;
        }

        input[type="radio"].toggle {
            display: none;
        }

            input[type="radio"].toggle + label {
                cursor: pointer;
                min-width: 150px;
            }

                input[type="radio"].toggle + label:hover {
                    background: none;
                    color: #1a1a1a;
                }

                input[type="radio"].toggle + label:after {
                    background: #3276b1; 
                    content: "";
                    height: 100%;
                    position: absolute;
                    top: 0;
                    transition: left 200ms cubic-bezier(0.77, 0, 0.175, 1);
                    width: 100%;
                    z-index: -1;
                }

        label {
            margin-bottom: 2px;
        }

        input[type="radio"].toggle.toggle-left + label {
            border-right: 0;
        }

            input[type="radio"].toggle.toggle-left + label:after {
                left: 100%;
            }

        input[type="radio"].toggle.toggle-right + label {
            margin-left: -5px;
        }

            input[type="radio"].toggle.toggle-right + label:after {
                left: -100%;
            }

        input[type="radio"].toggle:checked + label {
            cursor: default;
            color: #fff;
            transition: color 200ms;
        }

            input[type="radio"].toggle:checked + label:after {
                left: 0;
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
            .btn:hover {
            background-color: #eeeeee;
            color: black;
        }
    </style>
    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="BunchChallan">
                <span _ngcontent-c6="" style="color: #FFF">Challan With or Without Division</span></h1>
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
                    </div>
                </td>

                <td>
                    <b><span style="color: #336699">BudgetHead : </span></b>&nbsp;
                <input id="txtBudgetHead" type="text" class="form-control" style="width: 50%; display: inline-table" />
                </td>

            </tr>
            <tr>
                <td colspan="2" align="left">
                   
                        <b>
                            <label style="color: #336699">Select One: </label>
                        </b>&nbsp;
                        <input id="toggle-on" class="toggle toggle-left" name="toggle" value="0" type="radio" checked />
                        <label for="toggle-on" class="btnToggle">With Division</label>
                        <input id="toggle-off" class="toggle toggle-right" name="toggle" value="1" type="radio" />
                        <label for="toggle-off" class="btnToggle">With Out Division</label>
                   
                </td>
                <td>
                    
                        <button id="btnSubmit" type="submit" class="btn btn-primary">Submit</button>
                    
                </td>
            </tr>
        </table>

        <span class="row col-md-12" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: right; text-align: right;"></span>
        <div class="section2header col-md-12">
            <h1>Challan Details</h1>
        </div>

        <table id="ChallanwithDivision" class="table table-responsive table-striped table-bordered">
            <thead>
                <tr>
                    <th>S.No</th>
                    <th>Grn</th>
                    <th>Challanno</th>
                    <th>Challandate</th>
                    <th>OfficeName</th>
                    <th>DivisionCode</th>
                    <th>Amount</th>
                </tr>
            </thead>
        </table>
        <div id="ajaxloader">
        </div>
    </div>
</asp:Content>


