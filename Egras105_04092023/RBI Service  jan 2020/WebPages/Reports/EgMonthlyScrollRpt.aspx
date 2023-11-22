<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgMonthlyScrollRpt.aspx.cs" Inherits="WebPages_Reports_EgMonthlyScrollRpt"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/moment.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../js/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap-datetimepicker.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/jquery.dataTables.min.js"></script>
    <link href="../../CSS/jquery.dataTables.min.css" rel="stylesheet" />

 

    <script src="../../js/CDNFiles/jquery.mask.js"></script>
    <script src="../../js/CDNFiles/pdfmake.min.js"></script>
    <script src="../../js/CDNFiles/vfs_fonts.js"></script>
    <script src="../../js/CDNFiles/buttons.print.min.js"></script>
    <script src="../../js/CDNFiles/dataTables.buttons.min.js"></script>
    <script src="../../js/CDNFiles/jszip.min.js"></script>
    <script src="../../js/CDNFiles/buttons.html5.min.js"></script>
    
    <script type="text/javascript">
        // Material Select Initialization
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/WebPages/Reports/EgMonthlyScrollRpt.aspx/GetBanks") %>',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (data) {

                    var ddlbank = $("[id*=ddlbankname]");
                    ddlbank.empty();
                    $('#ddlbankname').append('<option value="0">--- Select Bank ---</option>');
                    $.each(JSON.parse(data.d), function (data, value) {
                        ddlbank.append($("<option></option>").val(value.BSRCode).html(value.BankName));
                    })
                },
                error: function (error) {
                    alert(error.toString());
                }
            })
       });
        
        //function UpdateData() {
        //    e.preventDefault();
            
        //}
        $(document).ready(function () {
            $("#btnShow").click(function (e) {
                e.preventDefault();
                $('#ajaxloader').show();
            var SBank = $('#ddlbankname').val();
            var SMonth = $('#ddlMonth').val();
            var SMonthsChar = $('#ddlMonth option:selected').text();
            var SYear = $('#ddlyear').val();
            var Paytype = $("input:radio[name='paytype']:checked").val();
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/WebPages/Reports/EgMonthlyScrollRpt.aspx/GetDMSReport") %>',
                data: '{"SBank":"' + SBank + '","SMonth":"' + SMonth + '","SYear":"' + SYear + '","SrblType":"' + Paytype + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (data) {
                    if (data.d != '[]') {
                    $("#EgMonthlyScrollReport").show();
                    $("#divheader").show();
                    var datatableVariable = $('#EgMonthlyScrollReport').DataTable({
                        "data": JSON.parse(data.d),
                        
                        "paging": false,
                        "ordering": true,
                        "searching": true,
                        "destroy": true,
                        "deferRender": true,//lazy loading
                        dom: 'Bfrtip',
                        buttons: [
                            {
                                extend: 'pdfHtml5',
                                filename: 'EgMonthlyScrollReport',
                                messageTop: 'Month ' + SMonthsChar + ' ' + SYear,
                                footer: true,
                                title: 'Government of Rajasthan' + '\n' + 'EgMonthlyScrollReport' + '\n' + $('#ddlbankname option:selected').text(),
                                
                                messageBottom: '\n \n For More Detail : Egras.raj.nic.in',
                                pageSize: 'A4', // Default
                                customize: function (doc) {
                                    doc.content[2].table.widths = 
                                        Array(doc.content[2].table.body[0].length + 1).join('*').split('');              
                                    var rowCount = doc.content[2].table.body.length;
                                    for (i = 1; i < rowCount; i++) {
                                        doc.content[2].table.body[i][0].alignment = 'center';
                                        doc.content[2].table.body[i][1].alignment = 'center';
                                        doc.content[2].table.body[i][2].alignment = 'center';
                                        doc.content[2].table.body[i][3].alignment = 'right';

                                    };
                                }                    
                            },
                            {
                                extend: 'excel',
                                title: 'EgMonthlyScrollReport',
                                filename: 'EgMonthlyScrollReport',
                                footer: true,
                                messageTop: 'Month ' + SMonthsChar + ' ' + SYear,
                                title: 'Government of Rajasthan ' + '\n' + '  EgMonthlyScrollReport  ' + '\n' + $('#ddlbankname option:selected').text(),
                            },
                            {
                                extend: 'print',
                                title: 'EgMonthlyScrollReport',
                                filename: 'EgMonthlyScrollReport',
                                pageSize: 'A4', // Default
                                orientation: 'portrait', // Default

                            },
                            {
                                extend: 'copy',
                                title: 'EgMonthlyScrollReport',
                                filename: 'EgMonthlyScrollReport',

                            },
                        ],
                        columns: [
                               {
                                   "data": null, "sortable": false,
                                   render: function (data, type, row, meta) {
                                       return meta.row + meta.settings._iDisplayStart + 1;
                                   }
                               },
                                {
                                    "data": "Total",
                                    "class": "text-center"
                                },
                            {
                                "data": "Date",
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

                            // Total over all pages
                            total = api
                                .column(3)
                                .data()
                                .reduce(function (a, b) {
                                    return intVal(a) + intVal(b);
                                }, 0);
                            total1 = api
                                    .column(1)
                                    .data()
                                    .reduce(function (a, b) {
                                        return intVal(a) + intVal(b);
                                    }, 0);
                            // Total over this page
                            pageTotal = api
                                .column(3, { page: 'current' })
                                .column(1, { page: 'current' })
                                .data()
                                .reduce(function (a, b) {
                                    return intVal(a) + intVal(b);
                                }, 0);
                            $(api.column(3).footer()).html(

                                '  Total Amount: ₹ ' + total + ''
                            );
                            $(api.column(1).footer()).html(

                                ' Total Challan: ' + total1
                            );
                        },

                    });
                }
            else {
                        $("#divheader").hide();
                        $("#EgMonthlyScrollReport").hide();
            alert('No record found !');
                    } $('#ajaxloader').hide();
                },
            });
            });
        });
    </script>
    <style>
        #myInput {
  box-sizing: border-box;
  background-image: url('searchicon.png');
  background-position: 14px 12px;
  background-repeat: no-repeat;
  font-size: 16px;
  padding: 14px 20px 12px 45px;
  border: none;
  border-bottom: 1px solid #ddd;
}

#myInput:focus {outline: 3px solid #ddd;}
    </style>
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #337ab7;
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

        .btn {
            height: auto !important;
            border-radius: 0px !important;
        }

        input[type=text], input[type=password] {
            height: auto !important;
        }

        input[type="radio"].toggle + label:after {
            height: 101%;
        }

        .dataTables_wrapper .dataTables_paginate {
            background-color: #1b2a47;
        }

        .btn-primary {
            color: #fff;
            background-color: #1b2a47;
            border-color: #1b2a47;
        }

            .btn-primary:hover, .btn-primary:after {
                color: #fff;
                background-color: #36548e;
                border-color: #1b2a47;
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
    
<div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Monthly-Scroll Report</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Monthly-Scroll Report" />
    </div>
    <table style="width: 100%" border="1" align="center">
        <tr style="height: 40px">
            
        </tr>
        <tr>
            <td>
                <b><span style="color: #336699" class="chzn-single">BankName : </span></b>&nbsp;&nbsp;
                       
                <select id="ddlbankname"  style="display: initial !important;width:225px"  class="form-control">
                </select>
            </td>
            <td>
                <b><span style="color: #336699" >Select Month : </span></b>&nbsp;&nbsp;
                       
                <select id="ddlMonth" class="form-control" style="display: initial !important;width:150px">
                    <option value="0">--Month--</option>
                    <option value="1">January</option>
                    <option value="2">February</option>
                    <option value="3">March</option>
                    <option value="4">April</option>
                    <option value="5">May</option>
                    <option value="6">June</option>
                    <option value="7">July</option>
                    <option value="8">August</option>
                    <option value="9">September</option>
                    <option value="10">October</option>
                    <option value="11">November</option>
                    <option value="12">December</option>
                </select>

            </td>
            <td>
                <b><span style="color: #336699">Select Year : </span></b>&nbsp;&nbsp;
                       
                <select id="ddlyear" class="form-control" style="display: initial !important;width:150px">
                    <option value="0">--Year--</option>
                    <option value="2012">2012</option>
                    <option value="2013">2013</option>
                    <option value="2014">2014</option>
                    <option value="2015">2015</option>
                    <option value="2016">2016</option>
                    <option value="2017">2017</option>
                    <option value="2018">2018</option>
                    <option value="2019">2019</option>
                    <option value="2020">2020</option>
                    <option value="2021">2021</option>
                    <option value="2022">2022</option>
                    <option value="2023">2023</option>


                </select>

            </td>
            </tr>
        <tr>
            
                <td colspan="2" align="center" style="height: 35px">
                    <div class="form-control" style="width:35%">
                <input type='radio' name='paytype' value='N' title="Success" class="radio radio-inline" style="margin-top: 0px;margin-right: 10px;" checked="checked"><b>Online</b>
                <input type='radio' name='paytype' value='M' title="UnSuccess" style="margin-left: 35px;margin-top: 0px;margin-right: 10px" class="radio radio-inline"><b>Manual</b>
            </div></td>
           
            <td>
                 <div class="form-control" style="width:25%;margin-left:125px">
               <%--<button id="btnSubmit" type="submit" onclick="UpdateData()" class="btn btn-primary">Show</button>--%>
            <button id="btnShow" type="submit" style="margin-top: -7px;margin-right: -17px;width: 80px;" class="btn btn-default pull-right">Show</button>
            </div></td>
        </tr>
    </table>
    <%--<span class="row col-md-12" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: left; text-align: left;"></span>--%>
    <div class="section2header col-md-12" id="divheader" style="display: none;">
    </div>
    <%--table id="tbDetails" border="1" width="100%" cellpadding="0" cellspacing="0" style="display: none; text-align: left" class="table table-responsive table-striped table-bordered"background-color: #337ab7; color: white;--%>
    <table id="EgMonthlyScrollReport" width="100%" cellpadding="0" cellspacing="0" class="table table-responsive table-striped table-bordered" style="display: none; text-align: left" border="1">
        <thead>
            <tr>
                <th>SRNo</th>
                <th>Total Challan</th>
                <th>Bank Date</th>
                <th>Total Amount</th>
            </tr>
        </thead>
        <%--<tbody style="color: black">

        </tbody>--%>
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
</asp:Content>
