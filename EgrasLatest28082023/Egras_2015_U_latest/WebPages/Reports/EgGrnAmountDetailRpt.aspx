<%@ Page Title="Egras.Rajasthan.gov.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgGrnAmountDetailRpt.aspx.cs" Inherits="WebPages_Reports_EgGrnAmountDetailRpt" %>

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
                minDate: '-2010/12/01',
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

            //$("#txtBudgetHead").mask("9999-99-999-99-99");
            $("#btnSubmit").click(function (e) {
                e.preventDefault();
                $("#spntotalamt").html("");
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var Amount1 = $('#txtAmount1').val();
                var Amount2 = $('#txtAmount2').val();

                if ($.fn.DataTable.isDataTable("#RemitterWiseAmountDetail")) {
                    $('#RemitterWiseAmountDetail').DataTable().clear().destroy();
                }
                //var Grn = $('#ctl00_ContentPlaceHolder1_ddldept').val();
                //var FromDate = $('#ctl00_ContentPlaceHolder1_ddlMhead').val();
                //var ToDate = $('#ctl00_ContentPlaceHolder1_ddlMhead').val();
                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgGrnAmountDetailRpt.aspx/GrnAmountDetails") %>',
                    data: '{"Amount1":"' + Amount1 + '", "Amount2":"' + Amount2 + '" ,"FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        //$.extend(true, $.fn.dataTable.defaults, {
                        //    columnDefs: [
                        //       {
                        //           targets: '_all',
                        //           defaultContent: '-'
                        //       }
                        //    ]
                        //});
                        $('.section2header').show();
                        $("#RemitterWiseAmountDetail").show();

                        var datatableVariable = $('#RemitterWiseAmountDetail').DataTable({
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
                                    filename: 'EgGrnAmountDetailRpt',
                                    title: 'Remitter Wise Amount Detail',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default

                                },
                                {
                                    extend: 'excel',
                                    title: 'Remitter Wise Amount Detail',
                                    filename: 'EgGrnAmountDetailRpt',
                                },
                                {
                                    extend: 'print',
                                    title: 'Remitter Wise Amount Detail',
                                    filename: 'EgGrnAmountDetailRpt',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default

                                },
                                {
                                    extend: 'copy',
                                    title: 'Remitter Wise Amount Detail',
                                    filename: 'EgGrnAmountDetailRpt',

                                },
                            ],

                            //"columnDefs": [{
                            //    "defaultContent": "-",
                            //    "targets": "_all"
                            //}],
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
                                    "data": "GRN", "title": "GRN", "render": function (data, type, row, meta) {
                                        if (type === 'display') {
                                            data = '<a class="abc" data-grn="' + data + '"  style="font-weight:600">' + (data) + '</a>';
                                            //return data;
                                            //data = '<span style="color:black">' + data + '</span>';
                                        }
                                        return data;
                                    }

                                },

                                //{
                                //    "data": "RemitterName",
                                //    "class": "text-center"

                                //},

                                {
                                    "data": "FullName",
                                    "class": "text-center"

                                },
                            {
                                "data": "DeptNameEnglish",
                                "class": "text-center",


                            },

                            {
                                "data": "MobilePhone",
                                "class": "text-center",

                            },
                            {
                                "data": "BankDate",
                                "class": "text-center",
                                "render": function (d) {
                                    return moment(d).format("DD/MM/YYYY");
                                }
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

            $('#RemitterWiseAmountDetail').on('click', 'a.abc', function () {
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



            //$(function () {
            //    $('#datetimepicker1').datetimepicker();
            //    $('#datetimepicker2').datetimepicker();
            //});
        });


    </script>
    <style>
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
    <style type="text/css">
        .section2header {
            background: #3276b1;
            display:none;
            margin-bottom: 1%;
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
            border: 1px solid #1b2a47;
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
                    background: #1b2a47;
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
            <h1 _ngcontent-c6="" title="BunchChallan">
                <span _ngcontent-c6="" style="color: #FFF">Remmiter Wise Amount Detail</span></h1>
             <img src="../../Image/help1.png" style="height: 44px; width: 34px;" title="Master-Report" />
        </div>
            
                <div class="row">
                  <table style="width: 97%" border="1" align="center">
                    <div class="col-md-5">
                        <tr>
                        <td>
                    <div class="col-md-12">
                        <label>FromDate:</label>
                        <div class='input-group date' id='datetimepicker1' style="width: 50%;margin-left:20px;; display: inline-table">
                            <input id="txtFromDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                        </td>
                        <td>
                    <div class="col-md-12">
                        <label>ToDate:</label>
                        <div class='input-group date' id='datetimepicker2' style="width: 50%;margin-left:20px; display: inline-table">
                            <input id="txtToDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                             </td>
                            </tr>
                        
                       </div>
                      <tr>
                          <td><div class="col-md-12">
                    <label>Amount1:</label>
                    <div id='divAmount1' style="width: 50%;margin-left:25px; display: inline-table">
                        <input id="txtAmount1" class="form-control" style="width:120px" type="text" value="1" required />

                    </div>
                </div></td>
                          <td><div class="col-md-12">
                    <label>Amount2:</label>
                    <div id='divAmount2' style="width: 50%;margin-left:8px; display: inline-table">

                        <input id="txtAmount2" class="form-control" style="width:120px" type="text" value="10000" required />
                    </div>
                </div></td>

                      </tr>
                      <tr>
                          <td colspan="3">
                               <div class="row  text-center" >
                <button id="btnSubmit" type="submit" class="btn btn-default">Submit</button>
            </div>
                          </td>
                      </tr>
                      </table>
                </div>                                             
        </div>
        <span class="row col-md-12" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: right; text-align: right;"></span>
        <div class="section2header col-md-12">
            <h1>Remitter Wise Amount Details</h1>
        </div>

        <table id="RemitterWiseAmountDetail" class="table table-responsive table-striped table-bordered" style="display:none;">
            <thead>
                <tr>
                    <th>S.No</th>
                    <th>Grn</th>
                    <th>RemitterName</th>
                    <th>Department</th>
                    <th>ContactNo</th>
                    <th>Challandate</th>
                    <th>Total Amount</th>

                </tr>
            </thead>
        </table>
        <div id="ajaxloader">
        </div>
    </div>
</asp:Content>
