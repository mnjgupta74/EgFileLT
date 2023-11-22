<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgBudgetHeadWiseReport.aspx.cs" Inherits="WebPages_Reports_EgBudgetHeadWiseReport" %>

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
                var Type = $('input[type=radio][name=group1]:checked').val();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var BudgetHead = $('#txtBudgetHead').val();
                //start Date Range condition
                var fromDateday = parseInt(FromDate.substring(0, 2), 10);
                var fromDateMonth = (parseInt(FromDate.substring(3, 5), 10) - 1);
                var fromDateYear = parseInt(FromDate.substring(6, 10), 10);
                var Date1 = new Date(fromDateYear, fromDateMonth, fromDateday); // From Date
                var toDateday = parseInt(ToDate.substring(0, 2), 10);
                var toDateMonth = (parseInt(ToDate.substring(3, 5), 10) - 1);
                var toDateYear = parseInt(ToDate.substring(6, 10), 10);
                var Date2 = new Date(toDateYear, toDateMonth, toDateday); // To Date
                if (Math.floor((Date.UTC(Date2.getFullYear(), Date2.getMonth(), Date2.getDate()) - Date.UTC(Date1.getFullYear(), Date1.getMonth(), Date1.getDate())) / (1000 * 60 * 60 * 24)) > 32) {
                    alert("Max 30 Days Date Difference is Allow  Between Fromdate And Todate");
                    $('#ajaxloader').hide();
                    return false;
                }
                //End Date Range condition
                if ($.fn.DataTable.isDataTable("#BudgetHeadWise")) {
                    $('#BudgetHeadWise').DataTable().clear().destroy();
                }
                if ($.fn.DataTable.isDataTable("#BudgetHeadWiseAllDep")) {
                    $('#BudgetHeadWiseAllDep').DataTable().clear().destroy();
                }

                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgBudgetHeadWiseReport.aspx/GetBudgetHeadWiseReport") %>',
                    data: '{"BudgetHead":"' + BudgetHead + '", "Type":"' + Type + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                    contentType: 'application/json; charset=utf-8',
                    "timeout": 5000000,
                    dataType: "json",
                    success: function (data) {
                        $("#divHeader").show();


                        if (Type == 2) {
                            $("#BudgetHeadWiseAllDep").hide();
                            $("#BudgetHeadWise").show();
                            var datatableVariable = $('#BudgetHeadWise').DataTable({
                                "data": JSON.parse(data.d),
                                "paging": true,
                                "ordering": true,
                                //"info": false,
                                "searching": true,
                                "destroy": true,
                                "deferRender": true,//lazy loading
                                dom: 'Bfrtip',
                                buttons: [
                                    {
                                        extend: 'pdf',
                                        filename: 'BudgetHeadWise',
                                        title: 'BudgetHeadWise',
                                        pageSize: 'A4', // Default
                                        //orientation: 'portrait', // Default
                                        //customize: function (doc) {
                                        //    doc.content[1].margin = [2000, 0, 0, 0] //left, top, right, bottom
                                        //}
                                        //customize: function (doc) {
                                        //    doc.content[1].table.widths =
                                        //        Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                        //}
                                    },
                                    {
                                        extend: 'excel',
                                        title: 'BudgetHeadWise',
                                        filename: 'BudgetHeadWise',
                                    },
                                    {
                                        extend: 'print',
                                        title: 'BudgetHeadWise',
                                        filename: 'BudgetHeadWise',
                                        pageSize: 'A4', // Default
                                        orientation: 'portrait', // Default

                                    },
                                    {
                                        extend: 'copy',
                                        title: 'BudgetHeadWise',
                                        filename: 'BudgetHeadWise',

                                    },
                                ],
                                columns: [
                                    {
                                        "data": "GRN", "title": "GRN", "class": "text-center", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<a class="abc" data-grn="' + data + '"  style="font-weight:600">' + (data) + '</a>';
                                            }
                                            return data;
                                        }
                                    },
                                    {
                                        "data": "RemitterName",
                                        "title": "remitter name",
                                        "class": "text-center"

                                    },
                                    {
                                        "data": "DepositDate",
                                        "title": "deposit date",
                                        "class": "text-center"//,
                                        //"render": function (d) {
                                        //    return moment(d).format("dd/mm/yyyy");
                                        //}
                                    },
                                    {
                                        "data": "BankName",
                                        "title": "bank name",
                                        "class": "text-center",

                                    },
                                    {
                                        "data": "officename",
                                        "title": "Office Name",
                                        "class": "text-center",

                                    },
                                    {
                                        "data": "Amount",
                                        "title": "Amount",
                                        "class": "text-right"//,
                                        //"render": function (data) {
                                        //    return data.tofixed(2);
                                        //}
                                    }
                                ],
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
                                    // Update footer
                                    $("#spntotalamt").html('PageTotal: ₹' + pageTotal + ' (Total Amount: ₹' + total + ')');
                                },

                            });
                        }
                        else {
                            $("#BudgetHeadWiseAllDep").show();
                            $("#BudgetHeadWise").hide();

                            var datatableVariable = $('#BudgetHeadWiseAllDep').DataTable({
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
                                        filename: 'BudgetHeadWiseAllDepartment',
                                        title: 'BudgetHeadWiseAllDepartment',
                                        pageSize: 'A4', // Default
                                        //orientation: 'portrait', // Default
                                        customize: function (doc) {
                                            doc.content[1].table.widths =
                                                Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                        }

                                    },
                                    {
                                        extend: 'excel',
                                        title: 'BudgetHeadWiseAllDepartment',
                                        filename: 'BudgetHeadWiseAllDepartment',
                                    },
                                    {
                                        extend: 'print',
                                        title: 'BudgetHeadWiseAllDepartment',
                                        filename: 'BudgetHeadWiseAllDepartment',
                                        pageSize: 'A4', // Default
                                        orientation: 'portrait', // Default

                                    },
                                    {
                                        extend: 'copy',
                                        title: 'BudgetHeadWiseAllDepartment',
                                        filename: 'BudgetHeadWiseAllDepartment',

                                    },
                                ],
                                //[{"Amount":6000.0000,"Head":"0030-01-101-01-00"},{"Amount":6000.0000,"Head":"0030-01-101-90-00"},{"Amount":4300.0000,"Head":"0030-01-102-01-00"}]
                                columns: [
                                    {
                                        "data": "Head",
                                        "title": "Head",
                                        "class": "text-center"
                                    }, {
                                        "data": "Amount",
                                        "title": "Amount",
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
                                        .column(1)
                                        .data()
                                        .reduce(function (a, b) {
                                            return intVal(a) + intVal(b);
                                        }, 0);
                                    // Total over this page
                                    pageTotal = api
                                        .column(1, { page: 'current' })
                                        .data()
                                        .reduce(function (a, b) {
                                            return intVal(a) + intVal(b);
                                        }, 0);
                                    // Update footer
                                    $("#spntotalamt").html('PageTotal: ₹' + pageTotal + ' (Total Amount: ₹' + total + ')');
                                },

                            });
                        }
                        $('#ajaxloader').hide();
                    },
                    error: function (error) {
                        $('#ajaxloader').hide();
                        alert(error.toString());
                    }
                })
            });
            $('#BudgetHeadWise').on('click', 'a.abc', function () {
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

    <style>
        /*body {
            font-family: 'PT Sans', sans-serif;
            font-size: 1.3em;
            font-weight: bold;
            color: #fff;
        }*/

        #first {
            background-color: #3C76b0;
        }

        #second {
            background-color: #3C76b0;
        }

        .section {
            /*padding: 100px;*/
            /*padding-left: 100px;*/
        }

            .section input[type="radio"] {
                display: none;
            }

        .container1 {
            margin-bottom: 10px;
        }

            /*.container label {
                position: relative;
            }*/

            /* Base styles for spans */
            .container1 span::before,
            .container1 span::after {
                content: '';
                position: absolute;
                top: 0;
                bottom: 0;
                margin: auto;
            }

            /* Radio buttons */
            .container1 span.radio:hover {
                cursor: pointer;
            }

            .container1 span.radio::before {
                left: 20px;
                width: 45px;
                height: 25px;
                background-color: #A8AAC1;
                border-radius: 50px;
            }

            .container1 span.radio::after {
                left: 20px;
                width: 17px;
                height: 17px;
                border-radius: 10px;
                background-color: #6C788A;
                transition: left .25s, background-color .25s;
            }

        input[type="radio"]:checked + label span.radio::after {
            left: 40px;
            background-color: #EBFF43;
        }

        .radio {
            padding-left: 55px;
            margin-top: 10px;
            margin-bottom: -10px;
            color: white;
        }
    </style>
    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="BudgetHead Wise Report">
                <span _ngcontent-c6="" style="color: #FFF">BudgetHead Wise Report</span></h1>
        </div>
        <div class="">
            <div class="row">
                <div class="col-md-12">
                    <label>Select One:</label>
                    <div class="col-md-12">
                        <section id="first" class="section">
                            <div class="row">
                            <div class="col-md-4">
                                <div class="container1">
                                  <input type="radio" name="group1" id="radio-1" value="1" checked/>
                                  <label for="radio-1"><span class="radio">All Department Summary Report</span></label>
                                </div></div>
                            <div class="col-md-4">

                                <div class="container1">
                                  <input type="radio" name="group1" id="radio-2"  value="2"/>
                                  <label for="radio-2"><span class="radio">All Department Detail Report</span></label>
                                </div></div>
                            <div class="col-md-4">

                                <div class="container1">
                                  <input type="radio" name="group1" id="radio-3"  value="3"/>
                                  <label for="radio-3"><span class="radio">All Department RajKosh Summary Report</span></label>
                                </div></div></div>
                        </section>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-md-8">
                    <div class="col-md-6">
                        <label>FromDate:</label>
                        <div class='input-group date' id='datetimepicker1'>
                            <input id="txtFromDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label>ToDate:</label>
                        <div class='input-group date' id='datetimepicker2'>
                            <input id="txtToDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>BudgetHead:</label>
                    <div id='divBudgetHead'>
                        <input id="txtBudgetHead" type="text" class="form-control" />
                    </div>
                </div>

            </div>
            <div class="row text-center" style="margin-top: 10px; margin-bottom: 15px;">
                <button id="btnSubmit" type="submit" class="btn btn-primary">Submit</button>
            </div>
        </div>
        <span class="row col-md-12" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: right; text-align: right;"></span>
        <div class="section2header col-md-12" id="divHeader" style="display: none;">
            <h1>Budget Head Details</h1>
        </div>

        <table id="BudgetHeadWise" class="table table-responsive table-striped table-bordered" style="display: none;">
        </table>
        <table id="BudgetHeadWiseAllDep" class="table table-responsive table-striped table-bordered" style="display: none;">
        </table>
        <div id="ajaxloader">
        </div>
    </div>
</asp:Content>
