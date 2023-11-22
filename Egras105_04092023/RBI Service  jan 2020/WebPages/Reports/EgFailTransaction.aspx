<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgFailTransaction.aspx.cs" Inherits="WebPages_Reports_EgFailTransaction"
    Title="Untitled Page" %>

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

    <script type="text/javascript">
        $(document).ready(function () {
            FillBank();
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
                $('#datetimepicker2').data('DateTimePicker').minDate(incrementDay);
                $(this).data("DateTimePicker").hide();
            });

            $('#datetimepicker2').datetimepicker().on('dp.change', function (e) {
                var decrementDay = moment(new Date(e.date));
                $('#datetimepicker1').data('DateTimePicker').maxDate(decrementDay);
                $(this).data("DateTimePicker").hide();
            });

            $("#btnSubmit").click(function (e) {
                e.preventDefault();
                $("#spntotalamt").html("");
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var bank = $('#ddlbank').val();

                //start Date Range condition
                var fromDateday = parseInt(FromDate.substring(0, 2), 10);
                var fromDateMonth = (parseInt(FromDate.substring(3, 5), 10) - 1);
                var fromDateYear = parseInt(FromDate.substring(6, 10), 10);
                var Date1 = new Date(fromDateYear, fromDateMonth, fromDateday); // From Date

                var toDateday = parseInt(ToDate.substring(0, 2), 10);
                var toDateMonth = (parseInt(ToDate.substring(3, 5), 10) - 1);
                var toDateYear = parseInt(ToDate.substring(6, 10), 10);
                var Date2 = new Date(toDateYear, toDateMonth, toDateday); // To Date


                if (Math.floor((Date.UTC(Date2.getFullYear(), Date2.getMonth(), Date2.getDate()) - Date.UTC(Date1.getFullYear(), Date1.getMonth(), Date1.getDate())) / (1000 * 60 * 60 * 24)) > 90) {
                    alert("Max 90 Days Date Difference is Allow  Between Fromdate And Todate");
                    $('#ajaxloader').hide();
                    return false;


                }
                //End Date Range condition


                if ($.fn.DataTable.isDataTable("#EgFailTransaction")) {
                    $('#EgFailTransaction').DataTable().clear().destroy();
                }

                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgFailTransaction.aspx/GetFailTransacrionsForBank") %>',
                    data: '{"bank":"' + bank + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        $("#EgFailTransaction").show();
                        $("#divheader").show();
                        var datatableVariable = $('#EgFailTransaction').DataTable({
                            "data": JSON.parse(data.d),
                            "paging": true,
                            "ordering": true,
                            "searching": true,
                            "destroy": true,
                            "deferRender": true,//lazy loading
                            dom: 'Bfrtip',
                            buttons: [
                                {
                                    extend: 'pdfHtml5',
                                    filename: 'FailTransaction',
                                    title: 'FailTransaction',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default
                                    customize: function (doc) {
                                        doc.content[1].table.widths = 
                                            Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                    }

                                },
                                {
                                    extend: 'excel',
                                    title: 'FailTransaction',
                                    filename: 'EgFailTransaction',
                                },
                                {
                                    extend: 'print',
                                    title: 'FailTransaction',
                                    filename: 'EgFailTransaction',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default

                                },
                                {
                                    extend: 'copy',
                                    title: 'FailTransaction',
                                    filename: 'EgFailTransaction',

                                },
                            ],

                            columns: [
                                {
                                    "data": "ROWNO",
                                    "class": "text-center"
                                },
                                {
                                    "data": "GRN",
                                    "class": "text-center"
                                },
                            {
                                "data": "BankTransdate",
                                "class": "text-center"
                            },
                            {
                                "data": "Amount",
                                "class": "text-right"
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
                                // Total Amount
                                total = api
                                    .column(3)
                                    .data()
                                    .reduce(function (a, b) {
                                        return intVal(a) + intVal(b);
                                    }, 0);
                                
                                // Total over this page
                                pageTotal = api
                                    .column(3, { page: 'current' })
                                    .data()
                                    .reduce(function (a, b) {
                                        return intVal(a) + intVal(b);
                                    }, 0);
                                // Update footer  spntotalComAmt
                                $("#spntotalamt").html('PageTotal: ₹' + pageTotal + ' (Total Amount: ₹ ' + total + ')');
                            },

                        });

                        $('#ajaxloader').hide();
                    },
                    error: function (error) {
                        $("#EgFailTransaction").hide();
                        $('#ajaxloader').hide();
                        alert(error.toString());
                    }
                })
            });
           
        });
        function FillBank() {
            var usertype= <%= Session["UserType"] %> ;
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/WebPages/Reports/EgFailTransaction.aspx/FillBank") %>',
                data: '{"userid":"' + <%= Session["UserID"] %> + '","usertype":"' + usertype + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length > 0 && msg.d != "[]") {
                        var json = JSON.parse(msg.d);
                        $("#ddlbank").empty();
                        if(usertype!="6")$("#ddlbank").append('<option value=' + '0' + '>' + '--- Select Bank ---' + '</option>');
                        $.each(json, function (index, obj) {
                            //{"BankName":"Bank Of Baroda","BSRCode":"0200113"}
                            $("#ddlbank").append('<option value=' + this.BSRCode + '>' + this.BankName + '</option>');
                        });
                    }
                    else {
                        $("#ddlbank").empty();
                        $("#ddlbank").append('<option value=' + '0' + '>' + '--- Select Bank ---' + '</option>');
                    }
                    $('#ajaxloader').hide();
                },
                error: function (error) {
                    $('#ajaxloader').hide();
                    alert(error.toString());
                }
            })
        }
    </script>
    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="Failed Transactions">
                <span _ngcontent-c6="" style="color: #FFF">Failed Transactions</span></h1>
        </div>
        <div class="">
            <div class="row">
                <div class="col-md-7">
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
                <div class="col-md-5">
                    <label>Select Bank:</label>
                    <div style="text-align: center; margin-bottom: 7px;">
                        <select id="ddlbank" name="ddlbank" class="form-control" style="width: 100%;">
                            <option value="0">--- Select Bank ---</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row text-center" style="margin-top: 10px; margin-bottom: 15px;">
                <button id="btnSubmit" type="submit" class="btn btn-primary">Submit</button>
            </div>
        </div>
        <span class="row col-md-6" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: right; text-align: right;"></span>

        <div class="section2header col-md-12" id="divheader" style="display: none;">
            <h1>Failed Transactions</h1>
        </div>

        <table id="EgFailTransaction" class="table table-responsive table-striped table-bordered" style="display: none;">
            <thead>
                <tr>
                    <th>SRNo</th>
                    <th>GRN</th>
                    <th>Bank Response Date</th>
                    <th>Amount</th>

                </tr>
            </thead>
        </table>
        <div id="ajaxloader">
        </div>
    </div>

</asp:Content>
