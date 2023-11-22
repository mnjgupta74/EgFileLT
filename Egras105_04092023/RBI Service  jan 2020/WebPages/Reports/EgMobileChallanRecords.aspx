<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgMobileChallanRecords.aspx.cs" Inherits="WebPages_Reports_EgMobileChallanRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
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
    
    <script type="text/javascript">
        $(document).ready(function () {

            $('#datetimepicker1,#datetimepicker2').datetimepicker({
                format: 'DD/MM/YYYY HH:mm:ss',
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


            $("#DataSection").hide();


            $("#btnSubmit").click(function () {
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();

                if ($.fn.DataTable.isDataTable("#MobileRecords")) {
                    $('#MobileRecords').DataTable().clear().destroy();
                }
                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgMobileChallanRecords.aspx/GetMobileData") %>',
                    data: '{"FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        $("#DataSection").show();
                        var datatableVariable = $('#MobileRecords').DataTable({
                            "data": JSON.parse(data.d),
                            "columnDefs": [{
                                "defaultContent": "-",
                                "targets": "_all"
                            }],
                            //"order": [[ 1, 'asc' ]],
                            "columns": [
                                { "title": "S.No" },
                                {
                                    "title": "Mobile Challan",
                                    "data": "MobileChallan",
                                    "class": "text-center"
                                },
                                {
                                    "title": "Integration Challan",
                                    "data": "IntegrationChallan",
                                    "class": "text-center",

                                },
                            ],
                            "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                                $("td:first", nRow).html(iDisplayIndex + 1);
                                return nRow;
                            },
                        });


                    },
                    error: function (error) {
                        $("#DataSection").hide();
                        alert(error.toString());
                    }
                })
                return false;
            });
        });
    </script>



    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Bunch Challan">
            <span _ngcontent-c6="" style="color: #FFF">Mobile and Integration Challan Report</span></h2>
        <img src="../../Image/help1.png" style="height: 44px; width: 34px;" title="Bunch Challan" />
    </div>

    <table width="100%" style="text-align: center" align="center" border="1">
        <tr>
            <td align="left">
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                       
                        <div class='input-group date' id='datetimepicker1' style="width: 50%; display: inline-table">
                            <input id="txtFromDate" type="text" class="form-control" style="height:33px" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
            </td>
            <td align="left">
                <b><span style="color: #336699">To Date : </span></b>&nbsp;

                        <div class='input-group date' id='datetimepicker2' style="width: 50%; display: inline-table">
                            <input id="txtToDate" type="text" class="form-control" style="height:33px" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
            </td>



            <td align="left">

                <div style="display: inline-table">
                    <button id="btnSubmit" type="submit" style="height:33px" class="btn btn-default">Submit</button>
                </div>

            </td>
        </tr>

    </table>



    <div id="DataSection" style="padding-top: 20px">
       
        <table id="MobileRecords" class="table table-responsive table-striped table-bordered">
            <thead>
                <tr>
                </tr>
            </thead>
        </table>
    </div>

</asp:Content>

