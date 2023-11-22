﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgGrnServiceRpt.aspx.cs" Inherits="WebPages_Reports_EgGrnServiceRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    
    <style type="text/css">
        .sectiontopheader[_ngcontent-c6] {
            border-top: 5px solid #337ab7;
            margin-bottom: 5px;
        }
        .dt-buttons {
            text-align:left;
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
            $("#btnSubmit").click(function (e) {
                e.preventDefault();
                $("#spntotalamt").html("");
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();

                if ($.fn.DataTable.isDataTable("#tbDetails")) {
                    $('#tbDetails').DataTable().clear().destroy();
                }

                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgGrnServiceRpt.aspx/GetServices") %>',
                    data: '{"FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        $("#tbDetails").show();
                        $("#divheader").show();
                        var datatableVariable = $('#tbDetails').DataTable({
                            "data": JSON.parse(data.d),
                            "paging": true,
                            "ordering": true,
                            "searching": true,
                            "destroy": true,
                            dom: 'Bfrtip',
                            buttons: [
                                {
                                    styles:'text-align:left',
                                    extend: 'pdfHtml5',
                                    filename: 'Services Challan',
                                    title: 'Services Challan',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default                                   
                                        customize: function (doc) {
                                            doc.styles.tableHeader.fontSize = 10;
                                            doc.defaultStyle.fontSize = 10;
                                            doc.content[1].table.widths =Array(doc.content[1].table.body[0].length + 1).join('*').split('');
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
                                    title: 'Services Challan',
                                    filename: 'Services Challan',
                                },
                                {
                                    extend: 'print',
                                    title: 'Services Challan',
                                    filename: 'Services Challan',
                                    pageSize: 'A4', // Default

                                },
                                {
                                    extend: 'copy',
                                    title: 'Services Challan',
                                    filename: 'Services Challan',

                                },
                            ],

                            columns: [
                                {
                                    "data": "SNo",
                                    "class": "text-center"
                                },
                                 {
                                     "data": "Grn", "title": "GRN", "render": function (data, type, row, meta) {
                                         if (type === 'display') {
                                             data = '<a class="abc" data-grn="' + data + '"  style="font-weight:600">' + (data) + '</a>';
                                             //return data;
                                             //data = '<span style="color:black">' + data + '</span>';
                                         }
                                         return data;
                                     }

                                 },
                            {
                                "data": "Amount",
                                
                                "class": "text-center"
                            },
                            {
                                "data": "todate",
                                render: function(d) {
                                    return moment(d).format("DD/MM/YYYY");},
                                "class": "text-center"
                            }

                            ]

                        });

                        $('#ajaxloader').hide();
                    },
                    error: function (error) {
                        $("#tbDetails").hide();
                        $('#ajaxloader').hide();
                        alert(error.toString());
                    }
                })
            });
            $('#tbDetails').on('click', 'a.abc', function () {
                var argObj = window;
                var id = $(this).data('grn');
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgGrnServiceRpt.aspx/GetEncryptParam") %>',
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

    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="BunchChallan">
                <span _ngcontent-c6="" style="color: #FFF">Services Log</span></h1>
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
                <div class="col-md-5"  style="margin-top:23px; margin-bottom: 15px;text-align: center;">
                    <button id="btnSubmit" type="submit" class="btn btn-primary">Submit</button>
                </div>
            </div>

        </div>
        <div class="section2header col-md-12" id="divheader" style="display: none;">
            <h1>Services Log</h1>
        </div>
        <div class="row col-md-12" align="center" id="trrpt" runat="server">
            <table id="tbDetails"  border="1" width="100%" cellpadding="0" cellspacing="0" style="display: none;text-align:left" class="table table-responsive table-striped table-bordered">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Grn</th>
                        <th>Amount</th>
                        <th>Date</th>
                    </tr>
                </thead>

            </table>
        </div>
    </div>
</asp:Content>

