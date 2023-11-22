<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgUnDefacedGRNs.aspx.cs" Inherits="WebPages_Reports_EgUnDefacedGRNs"
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
                $('#datetimepicker2').data('DateTimePicker').minDate(incrementDay);
                $(this).data("DateTimePicker").hide();
            });

            $('#datetimepicker2').datetimepicker().on('dp.change', function (e) {
                var decrementDay = moment(new Date(e.date));
                $('#datetimepicker1').data('DateTimePicker').maxDate(decrementDay);
                $(this).data("DateTimePicker").hide();
            });

            $("#txtBudgetHead").mask("9999-99-999-99-99");

            $("#btnSubmit").click(function (e) {

                e.preventDefault();
                $("#spntotalamt").html("");
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();

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
                if ($.fn.DataTable.isDataTable("#UndefacedGrn")) {
                    $('#UndefacedGrn').DataTable().clear().destroy();
                }

                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgUnDefacedGRNs.aspx/GetUndefacedGrn") %>',
                    data: '{"FromDate":"' + FromDate + '","ToDate":"' + ToDate + '","userid":"' + <%= Session["UserID"] %> + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {

                        $("#UndefacedGrn").show();
                        $("#divheader").show();

                        var datatableVariable = $('#UndefacedGrn').DataTable({
                            "data": JSON.parse(data.d),
                            "paging": true,
                            "ordering": true,
                            "searching": true,
                            "destroy": true,
                            "deferRender": true,//lazy loading
                            dom: 'Bfrtip',
                            buttons: [
                                {
                                    extend: 'pdf',
                                    filename: 'PendingGRNForDeface',
                                    title: 'PendingGRNForDeface',
                                    pageSize: 'A4', // Default
                                    //orientation: 'portrait', // Default
                                    //customize: function (doc) {
                                    //    doc.content[1].table.widths =
                                    //        Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                    //},
                                    exportOptions: {
                                        columns: [0, 1, 2, 3]
                                    },
                                    customize: function (doc) {
                                        doc.styles.tableHeader.fontSize = 10;
                                        doc.defaultStyle.fontSize = 10;
                                        doc.content[1].table.widths = Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                        var rowCount = doc.content[1].table.body.length;
                                        for (i = 1; i < rowCount; i++) {
                                            doc.content[1].table.body[i][0].alignment = 'left';
                                            doc.content[1].table.body[i][1].alignment = 'center';
                                            doc.content[1].table.body[i][2].alignment = 'center';
                                            doc.content[1].table.body[i][3].alignment = 'center';
                                        };
                                    }
                                },
                                {
                                    extend: 'excel',
                                    title: 'PendingGRNForDeface',
                                    filename: 'PendingGRNForDeface',
                                },
                                {
                                    extend: 'print',
                                    title: 'PendingGRNForDeface',
                                    filename: 'PendingGRNForDeface',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default

                                },
                                {
                                    extend: 'copy',
                                    title: 'PendingGRNForDeface',
                                    filename: 'PendingGRNForDeface',

                                },
                            ],
                            //columnDefs: [
                            //{

                            //    "targets": 3,
                            //    "className": "text-right",
                            //}],
                            //{"Office":"EX.ENG.DIST.RURAL DIV II PHED, Jaipur","officeid":4087,"tottrans":1,"Deface":0,"Pending":1}
                            //[{"Office":"Multiple Offices","officeid":0,"tottrans":54943,"Deface":1,"Pending":54942}]
                            columns: [
                                {
                                    "data": "Office",
                                    "class": "text-center"

                                },
                                {
                                    "data": "tottrans",
                                    "class": "text-center",
                                },
                                {
                                    "data": "Deface",
                                    "class": "text-center"
                                }
                                ,
                                {
                                    "data": "Pending",
                                    "title": "Pending",
                                    "class": "text-center",
                                    "render": function (data, type, row, meta) {
                                        if (type === 'display') {
                                            //alert(row.officeid);
                                            //alert(data)
                                            data = '<a class="abc" data-officeid="' + row.officeid + '"  style="font-weight:600">' + (data) + '</a>';
                                        }
                                        return data;
                                    }

                                },
                            {
                                "data": "officeid",
                                "visible": false
                            }
                            ]
                        });
                        $('#btnSubmit').attr('disabled', 'disabled');
                        $('#ajaxloader').hide();
                    },
                    error: function (error) {

                        $("#UndefacedGrn").hide();
                        $('#ajaxloader').hide();
                        alert(error.toString());
                    }
                })
            });
            $('#UndefacedGrn').on('click', 'a.abc', function () {
                var argObj = window;
                var officeid = $(this).data('officeid');
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();

                //alert(id);
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgUnDefacedGRNs.aspx/GetEncryptParam") %>',
                    data: '{"officeid":"' + officeid + '","fromdate":"' + FromDate + '","todate":"' + ToDate + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        window.open('../reports/EgPendingDefaceGRN.aspx?' + escape(msg.d), "", "_blank");
                    }

                });
            });

            $("#btnReset").click(function (e) {
                $('#btnSubmit').attr('disabled', '');
            });
        });


    </script>

    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="Pending GRN for Deface">
                <span _ngcontent-c6="" style="color: #FFF">Pending GRN for Deface</span></h1>
        </div>
        <div style="border: 1px solid; width: 100%; margin-left: 0px; padding: 5px;">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-4">
                        <label style="color: #336699">FromDate:</label>
                        <div class='input-group date' id='datetimepicker1'>
                            <input id="txtFromDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <label style="color: #336699">ToDate:</label>
                        <div class='input-group date' id='datetimepicker2'>
                            <input id="txtToDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>

                    <div class="col-md-2" style="margin-top: 19px; margin-bottom: 15px; text-align: center;">
                        <button id="btnSubmit" type="submit" style="width: 70%" class="btn btn-primary">Submit</button>

                    </div>
                    <div class="col-md-2" style="margin-top: 19px; margin-bottom: 15px; text-align: center;">
                        <button id="btnReset" type="submit" style="width: 70%" class="btn btn-primary">Reset</button>
                    </div>
            </div>
        </div>
    </div>

    <span class="row col-md-12" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: right; text-align: right;"></span>
    <div class="section2header col-md-12" id="divheader" style="display: none; text-align: center;">
        <h1>Pending GRN for Deface</h1>
    </div>

    <table id="UndefacedGrn" class="table table-responsive table-striped table-bordered" style="display: none;">
        <thead>
            <tr>
                <th>Office</th>
                <th>Total Transaction</th>
                <th>Defaced</th>
                <th>Pending</th>
            </tr>
        </thead>
    </table>
    <div id="ajaxloader">
    </div>
    </div>
</asp:Content>
