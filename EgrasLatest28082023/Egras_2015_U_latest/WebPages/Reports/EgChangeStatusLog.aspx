<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgChangeStatusLog.aspx.cs" Inherits="WebPages_Reports_EgChangeStatusLog" %>

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

        *, *:before, *:after {
            /* -webkit-box-sizing: border-box; */
            -moz-box-sizing: border-box;
            box-sizing: border-box;
            text-align: left;
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

                if ($.fn.DataTable.isDataTable("#tbDetails")) {
                    $('#tbDetails').DataTable().clear().destroy();
                }

                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgChangeStatusLog.aspx/GetChangeStatuslog") %>',
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
                                    extend: 'pdf',
                                    filename: 'Edit Status Log',
                                    title: 'EditStausLog',
                                    pageSize: 'A4', // Default
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
                                    title: 'Edit Status Log',
                                    filename: 'EditStausLog',
                                },
                                {
                                    extend: 'print',
                                    title: 'Edit Status Log',
                                    filename: 'EditStausLog',
                                    pageSize: 'A4', // Default

                                },
                                {
                                    extend: 'copy',
                                    title: 'Edit Status Log',
                                    filename: 'EditStausLog',

                                },
                            ],

                            columns: [
                                {
                                    "data": "SNo",
                                    "class": "text-center"
                                },
                                {
                                    "data": "GRN",
                                    "class": "text-center"
                                },
                            {
                                "data": "OldStatus",

                                "class": "text-center"
                            },
                            {
                                "data": "NewStatus",
                                "class": "text-center"
                            },
                            {
                                "data": "ChangeBy",
                                "class": "text-center"
                            },
                            {
                                "data": "TransDate",
                                render: function (d) {
                                    return moment(d).format("DD/MM/YYYY");
                                },
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

        });



    </script>
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>
    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="BunchChallan">
                <span _ngcontent-c6="" style="color: #FFF">Edit Status Log</span></h1>
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

            </tr>
            <tr>
                <td colspan="2" align="center">
                    <div class="col-md-6" align="center"></div>
                    <div class="col-md-6" align="center">
                        <button id="btnSubmit" type="submit" class="btn btn-default">Submit</button>
                    </div>

                </td>
            </tr>

        </table>

        <div class="row col-md-12" align="center" id="trrpt" runat="server">
            <table id="tbDetails" border="1" width="100%" cellpadding="0" cellspacing="0" style="display: none; background-color: #428bca; color: white; text-align: center" class="table table-responsive table-striped table-bordered">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Grn</th>
                        <th>Old Status</th>
                        <th>New Status</th>
                        <th>ChangeBy</th>
                        <th>Trans Date</th>
                    </tr>
                </thead>
                <tbody style="color: black">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
