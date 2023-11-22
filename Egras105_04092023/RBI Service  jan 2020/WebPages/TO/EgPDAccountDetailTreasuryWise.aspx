<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgPDAccountDetailTreasuryWise.aspx.cs" Inherits="WebPages_TO_EgPDAccountDetailTreasuryWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/moment.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../js/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap-datetimepicker.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/jquery.dataTables.min.js"></script>
    <link href="../../CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />

    <script src="../../js/moment.js"></script>

    <script src="../../js/CDNFiles/pdfmake.min.js"></script>
    <script src="../../js/CDNFiles/vfs_fonts.js"></script>
    <script src="../../js/CDNFiles/buttons.print.min.js"></script>
    <script src="../../js/CDNFiles/dataTables.buttons.min.js"></script>
    <script src="../../js/CDNFiles/jszip.min.js"></script>
    <script src="../../js/CDNFiles/buttons.html5.min.js"></script>
    <%-- <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>--%>
    <script src="../../js/CDNFiles/pdfmake.min.js"></script>
    <script src="../../js/CDNFiles/vfs_fonts.js"></script>

    <style>
        .Header {
            background-color: blue;
        }

        #tbPDAccountList th {
            text-align: center;
        }


        .form-control {
            height: 33px;
        }
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>

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
            width: 120px;
            height: 120px;
            border-style: solid;
            border-color: #1b2a47;
            border-top-color: lightcyan;
            border-width: 15px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }

        input[type="text"] {
            height: auto !important;
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
            $("#tbPDAccountList").hide();

            $('#datetimepicker1,#datetimepicker2').datetimepicker({
                format: 'DD/MM/YYYY',
                ignoreReadonly: true,
                minDate: '2010/04/01',
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
            $("#btnShow").click(function () {
                $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                if ($('#txtFromDate').val() == "" || $('#txtToDate').val() == "") {

                    alert("Please enter date.");
                }
                else {
                    $.ajax({
                        type: 'POST',
                        url: '<%= ResolveUrl("~/WebPages/to/EgPDAccountDetailTreasuryWise.aspx/GetPDAccountDetail") %>',
                        data: '{"FromDate":"' + $('#txtFromDate').val() + '","ToDate":"' + $('#txtToDate').val() + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (data) {


                            $("#tbPDAccountList").show();

                            var datatableVariable = $('#tbPDAccountList').DataTable({

                                "data": JSON.parse(data.d),
                                "paging": true,
                                "ordering": true,
                                "searching": true,
                                "destroy": true,
                                dom: 'Bfrtip',
                                buttons: [
                                {
                                    extend: 'pdfHtml5',
                                    filename: 'EgGrnAmountDetailRpt',
                                    title: 'Treasury Wise Amount Detail',
                                    messageTop: 'FromDate: ' + FromDate + '     ToDate: ' + ToDate + '',
                                    pageSize: 'A4', // Default
                                    orientation: 'portrait',
                                    footer: true,
                                    customize: function (doc) {
                                        //doc.content[1].table.widths =
                                        //    Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                        //doc.content[2].table.widths =
                                        //            Array(doc.content[2].table.body[0].length + 1).join('*').split('');
                                        doc.styles.tableHeader.fontSize = 9;
                                        doc.defaultStyle.fontSize = 9;
                                        var rowCount = doc.content[2].table.body.length;
                                        for (i = 1; i < rowCount; i++) {
                                            doc.content[2].table.body[i][0].alignment = 'center';
                                            doc.content[2].table.body[i][1].alignment = 'center';
                                            doc.content[2].table.body[i][2].alignment = 'center';
                                            doc.content[2].table.body[i][3].alignment = 'right';
                                            doc.content[2].table.body[i][4].alignment = 'right';

                                        };

                                    }

                                },
                                {
                                    extend: 'excel'
                                },
                                {
                                    extend: 'print',

                                    pageSize: 'A4', // Default
                                    orientation: 'portrait', // Default

                                },
                                {
                                    extend: 'copy',


                                },
                                ],
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

                            "data": "TreasuryName", "title": "Treasury Name", "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<span style="color:black">' + data + '</span>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "GRNCount", "title": "No. of Challan", "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<span style="color:black">' + data + '</span>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "TotalAmount", "title": "Amount", "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<span style="color:black">' + data.toFixed(2) + '</span>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "pdacc", "title": "P.D. A/c", "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<span style="color:black">' + data + '</span>';
                                }
                                return data;
                            }
                        },
                                ],
                                "footerCallback": function (row, data, start, end, display) {
                                    debugger;
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
                                    totalGRN = api
                                        .column(2)
                                        .data()
                                        .reduce(function (a, b) {
                                            var c = intVal(a) + intVal(b);
                                            return c.toFixed(0);
                                        }, 0);
                                    // Total GRN
                                    total = api
                                        .column(3)
                                        .data()
                                        .reduce(function (a, b) {
                                            var c = intVal(a) + intVal(b);
                                            return c.toFixed(2);
                                        }, 0);
                                    // Total over this page
                                    //pageTotal = api
                                    //    .column(3, { page: 'current' })
                                    //    .data()
                                    //    .reduce(function (a, b) {
                                    //        var c = intVal(a) + intVal(b);
                                    //        return c.toFixed(2);
                                    //    }, 0);
                                    // Update footer
                                    $(api.column(2).footer()).html(
                                        'Total No Of GRN : (' + totalGRN + ')'
                                    );
                                    $(api.column(3).footer()).html(
                                        //'Total No Of GRN : ' + totalGRN + '|  PageTotal: ₹' + pageTotal + ' (Total Amount: ₹' + total + ')'
                                        ' Total Amount: ₹' + total
                                    );

                                }
                            });

                            $('#ajaxloader').hide();
                        },
                        error: function (error) {
                            $("#tbPDAccountList").hide();
                            $('#ajaxloader').hide();
                            alert(error.toString());
                        }
                    });
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="TY-33">
            <span _ngcontent-c6="" style="color: #FFF">PD Account List</span></h2>
        <%--<img src="../../Image/help1.png" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="left" title="PD Account List" />--%>
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
                            <td>
                                <div class="col-sm-4" style="text-align: center">
                <button id="btnShow" type="button" class="btn btn-default" style="height:33px">Show</button>
            </div>
                            </td>
                            </tr></div></table>
            

        </div>
        <br />
        <br />
        <table id="tbPDAccountList" cellspacing="0" style="background-color: #336699; color: white; text-align: center" border="1">
            <%-- <tfoot>
                <tr>
                    <th colspan="4" style="text-align: right">Total:</th>
                    <th></th>
                </tr>
            </tfoot>--%>
            <tfoot>
                <tr>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: center"></th>
                    <th style="text-align: right"></th>
                </tr>
            </tfoot>
        </table>
    
    <div id="ajaxloader">
    </div>
    <%--</fieldset>--%>
</asp:Content>










