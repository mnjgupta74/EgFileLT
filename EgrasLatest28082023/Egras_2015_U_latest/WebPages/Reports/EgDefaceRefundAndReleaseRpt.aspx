﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgDefaceRefundAndReleaseRpt.aspx.cs" Inherits="WebPages_Reports_EgDefaceRefundAndReleaseRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/bootstrap.min.js"></script>
    <script src="../../js/jquery.dataTables.min.js"></script>
    <link href="../../CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js"></script>--%>
    <script src="../../js/CDNFiles/moment.min.js"></script>
    <script src="../../js/moment.js"></script>
    <link href="../../js/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../js/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ajaxloader').hide();
            $('#datetimepicker1,#datetimepicker2').datetimepicker({
                format: 'DD/MM/YYYY HH:mm:ss',
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


            if ($('input[type=radio][name=toggle]:checked').val() == 0) {
                $("#DivGRN").show();
                $("#DivDate").hide();
            }
            else {
                $("#DivGRN").hide();
                $("#DivDate").show();
            }

            $('input[type=radio][name=toggle]').change(function () {
                if (this.value == 0) {
                    $("#DivGRN").show();
                    $("#DivDate").hide();
                }
                else {
                    $("#DivGRN").hide();
                    $("#DivDate").show();
                }
            });


            $("#btnSubmit").click(function () {
                $('#ajaxloader').show();
                var Type = $('input[type=radio][name=toggle]:checked').val();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var Grn = $('#grn').val();
                if ($.fn.DataTable.isDataTable("#DefaceRefundDetail")) {
                    $('#DefaceRefundDetail').DataTable().clear().destroy();
                }
                $.ajax({
                    type: "POST",    //4900485
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgDefaceRefundAndReleaseRpt.aspx/GetDefaceAndRefundDetail") %>',
                    data: '{"Grn":"' + Grn + '","Type":"' + Type + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        var datatableVariable = $('#DefaceRefundDetail').DataTable({
                            "data": JSON.parse(data.d),
                            "columnDefs": [{
                                "defaultContent": "-",
                                "targets": "_all"
                            }],
                            //"order": [[ 1, 'asc' ]],
                            "columns": [
                                {},
                                {
                                    "data": "Grn",
                                    "class": "text-center"
                                },
                                {
                                    "data": "TotalAmount",
                                    "class": "text-right",
                                    "render": function (data, type, row, meta) {
                                        if (isNaN(parseFloat(data).toFixed(2)))
                                            return 0.00.toFixed(2);
                                        else
                                            return parseFloat(data).toFixed(2);
                                    }
                                },
                                {
                                    "data": "DefaceAmount",
                                    "class": "text-right",
                                    "render": function (data, type, row, meta) {
                                        if (isNaN(parseFloat(data).toFixed(2)))
                                            return 0.00.toFixed(2);
                                        else
                                            return parseFloat(data).toFixed(2);
                                    }
                                },
                                {
                                    "data": "DefaceRelease",
                                    "class": "text-right",
                                    "render": function (data, type, row, meta) {
                                        if (isNaN(parseFloat(data).toFixed(2)))
                                            return 0.00.toFixed(2);
                                        else
                                            return parseFloat(data).toFixed(2);
                                    }
                                },
                            {
                                "data": "RefundRelease",
                                "class": "text-right",
                                "render": function (data, type, row, meta) {
                                    if (isNaN(parseFloat(data).toFixed(2)))
                                        return 0.00.toFixed(2);
                                    else
                                        return parseFloat(data).toFixed(2);
                                }
                            },

                               {
                                   "data": "Refund",
                                   "class": "text-right",
                                   "render": function (data, type, row, meta) {
                                       if (isNaN(parseFloat(data).toFixed(2)))
                                           return 0.00.toFixed(2);
                                       else
                                           return parseFloat(data).toFixed(2);
                                   }
                               },


                            ],
                            "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                                $("td:first", nRow).html(iDisplayIndex + 1);
                                return nRow;
                            },
                        });
                        $('#ajaxloader').hide();
                    },
                    error: function (error) {
                        $('#ajaxloader').hide();
                        alert(error.toString());
                    }
                })
                return false;
            });

            //$(function () {
            //    $('#datetimepicker1').datetimepicker();
            //    $('#datetimepicker2').datetimepicker();
            //});
        });
    </script>
    
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

        /*CSS FOR TOP HEADER STARTS*/

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
        /*TOPHEADER CSS ENDS HERE*/

        /*RADIO BUTTON CSS STARTS HERE*/
        .btnToggle {
            border: 2px solid #286090;
            display: inline-block;
            padding: 5px;
            position: relative;
            text-align: center;
            transition: background 600ms ease, color 600ms ease;
        }

        input[type="radio"].toggle {
            display: none;
        }

            input[type="radio"].toggle + label {
                cursor: pointer;
                min-width: 130px;
            }

                input[type="radio"].toggle + label:hover {
                    background: none;
                    color: #1a1a1a;
                }

                input[type="radio"].toggle + label:after {
                    background: #286090;
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
        /*RADIO BUTTON CSS ENDS HERE*/
        #InputSection {
            margin-bottom: 15px;
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
    <style>
            
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
        </style>
    
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="BunchChallan">
                <span _ngcontent-c6="" style="color: #FFF">Deface Refund</span></h1>
            <%--<img src="../../Image/help1.png" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="left" title="" />--%>
        </div>
        <div>
            <table width="100%" style="text-align: center;margin-bottom:10px" align="center" border="1">
            <div id="InputSection">
                <div class="row text-center">
                    

            <tr>
                <td align="left" colspan="3">
                    
                    <div class="col-sm-8">
                        
                        <div style="text-align: center;">
                            <b><span style="color: #336699">Select One: </span></b>&nbsp;
                            <input id="toggle-on" class="toggle toggle-left" name="toggle" value="0" type="radio" checked />
                            <label for="toggle-on" class="btnToggle">GrnWise</label>
                            <input id="toggle-off" class="toggle toggle-right" name="toggle" value="1" type="radio" />
                            <label for="toggle-off" class="btnToggle">DateWise</label>
                        </div>
                        <%--<label>Select One:</label>
                        <div class="custom-control custom-radio">
                            <input type="radio" name="division" id="grn" value="grn" onclick="toggleTextbox('calendar')" />GrnWise<br />
                            <input type="radio" name="division" id="calendar" value="calendar" onclick="toggleTextbox('grn')" />DateWise<br />
                        </div>--%>
                    </div></td>
                </tr>
                        <tr>
                            <td align="left">
                    <div id="DivGRN" class="col-sm-6" style="width: 50%; display: inline-flex">
                        <b><label style="color: #336699;margin-top: 10px;">GRN:</label></b>&nbsp;
                        <div id="txtGrn">
                            <input id="grn" type="text"  class="form-control" id="txtgrn" />
                        </div>
                    </div>
                </td>
                            <td align="left">
                                <div id="DivDate">
                        <div class="col-sm-6 pull-left">
                            <b><span style="color: #336699">FromDate:</span></b>
                            <div class='input-group date' id='datetimepicker1' style="width: 65%; display: inline-table">
                                <input id="txtFromDate" type="text" class="form-control" readonly />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-6 pull-left">
                            <b><span style="color: #336699">ToDate:</span></b>
                            <div class='input-group date' id='datetimepicker2' style="width: 65%; display: inline-table">
                                <input id="txtToDate" type="text" class="form-control" readonly />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                            </td> </tr>
                    <tr>
                        <td colspan="3">
                                <div class="col-sm-12 text-center">
                        <button id="btnSubmit" type="submit" class="btn btn-primary">Get Deface And Refund Detail</button>
                    </div>
                            </td></tr>
                </div>

            </div>
                </table>
            <div id="DataSection">
                <div class="section2header">
                    <h1>Deface And Refund Details</h1>
                </div>
                <table id="DefaceRefundDetail" class="table table-responsive table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>S.No</th>
                            <th>Grn</th>
                            <th>GRNAmount</th>
                            <th>DefaceAmount Date</th>
                            <th>DefaceRelease</th>
                            <th>RefundRelease</th>
                            <th>Refund</th>

                        </tr>
                    </thead>
                </table>
            </div>

        </div>
         <div id="ajaxloader">
    </div>
    
</asp:Content>


