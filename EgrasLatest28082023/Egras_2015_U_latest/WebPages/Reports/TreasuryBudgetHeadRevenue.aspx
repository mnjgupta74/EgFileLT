<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="TreasuryBudgetHeadRevenue.aspx.cs" Inherits="WebPages_Reports_TreasuryBudgetHeadRevenue" %>

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
            width: 110px;
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
            text-align: left;
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

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
                  
.col-md-9{
    padding-left:0px;
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
            $("#txtBudgetHead").mask("9999-99-999-99-99");
            $("#btnSubmit").click(function (e) {
                e.preventDefault();
                $("#spntotalamt").html("");
               // $('#ajaxloader').show();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var DeptCode = $('#ddlDept').val();
                var TreasulyCode = $('#ddlTreasury').val();
                
                var BudgetHead = $('#txtBudgetHead').val();
                if(<%= Session["UserType"] %>=='3' && $('#ddlTreasury').val()=='0')
                {
                    alert('Please Select Treasury..');
                }
                if (BudgetHead.length != 17) {
                    alert('Please Fill Complete Budget Head')
                }
                else {

                    if(<%= Session["UserType"] %>=='3' && $('#ddlTreasury').val()=='0')
                    {
                        alert('Please Select Treasury..');
                    }
                    else
                    {     $('#ajaxloader').show();                
                        if ($.fn.DataTable.isDataTable("#tbDetails")) {
                            $('#tbDetails').DataTable().clear().destroy();
                        }

                        $.ajax({
                            type: "POST",
                            url: '<%= ResolveUrl("~/WebPages/Reports/TreasuryBudgetHeadRevenue.aspx/GetTreasuryBudgetHead") %>',
                            data: '{"FromDate":"' + FromDate + '","ToDate":"' + ToDate + '","Tcode":"' + TreasulyCode + '","Mcode":"' + BudgetHead + '","DeptCode":"' + 0 + '"}',
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
                                            extend: 'pdfHtml5',
                                            filename: 'Revenue Budget HeadWise',
                                            title: 'Revenue Budget HeadWise',                                        
                                            messageTop: 'FromDate: ' + FromDate + '     ToDate: ' + ToDate + '    Treasury Name: ' + $('#ddlTreasury').find(':selected').text() + '',
                                            //messageTop: '<p style="font-size:11px">FromDate :  ' + FromDate + '<br><br>ToDate : ' + ToDate + 'Treasury Name: ' + $('#ddlTreasury').find(':selected').text() + '</p>',
                                            pageSize: 'A4', // Default
                                            customize: function (doc) {
                                                doc.content[2].table.widths = 
                                                    Array(doc.content[2].table.body[0].length + 1).join('*').split('');
                                            }
                                        },
                                    ],

                                    columns: [
                                        //{
                                        //    "data": null, "sortable": false,
                                        //    render: function (data, type, row, meta) {
                                        //        return meta.row + 1;
                                        //    }
                                        //},
                                        {
                                            "data": null, "sortable": false,
                                            render: function (data, type, row, meta) {
                                                return meta.row + meta.settings._iDisplayStart + 1;
                                            }
                                        },
                                        {
                                            "data": "Treasury",

                                            "class": "text-center"
                                        },
                                             {
                                                 "data": "BudgetHead",

                                                 "class": "text-center"
                                             },
                                    {
                                        "data": "Amount",

                                        "class": "text-center"
                                    },

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
                                        $("#spntotalamt").html('Total Amount: ₹ ' + total + ')');
                                    },
                                    "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                                        $("td:first", nRow).html(iDisplayIndex + 1);
                                        return nRow;
                                    },

                                });

                                $('#ajaxloader').hide();
                            },
                            error: function (error) {
                                $("#tbDetails").hide();
                                $('#ajaxloader').hide();
                                alert(error.toString());
                            }
                        })
                    }
                }
            });
            <%--if(<%= Session["UserType"] %>=='2')
            {                            //$('#ddlTreasury').find(':selected').text() = ''                            
                $('#ddlOffice').append('<option value="0">--- Select Office ---</option>');
            }
            else
            {--%>  
            if(<%= Session["UserType"] %>!='2'){
                $('#ddlTreasury').prop("disabled", true);
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/Reports/TreasuryBudgetHeadRevenue.aspx/GetTreasury") %>',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        var data = $.parseJSON(msg.d);
                        $.each(data, function (i) { 
                        
                            $('#ddlTreasury').append('<option value="' + data[i].TreasuryCode + '">' + data[i].TreasuryName + '</option>');
                            //ddlTreasury.Enabled = false;
                        });
                    }
                })           
            }
            else{
                $('#ddlTreasury').append('<option value="0" selected="selected">--All Treasury--</option');
                $('#ddlTreasury').append('<option value="0100">Ajmer</option>');
                $('#ddlTreasury').append('<option value="0200">ALWAR</option>');
                $('#ddlTreasury').append('<option value="0300">BANSWARA</option>');
                $('#ddlTreasury').append('<option value="0400">BARAN</option>');
                $('#ddlTreasury').append('<option value="0500">BARMER</option>');
                $('#ddlTreasury').append('<option value="0600">BEAWAR</option>');
                $('#ddlTreasury').append('<option value="0700">BHARATPUR</option>');
                $('#ddlTreasury').append('<option value="0800">BHILWARA</option>');
                $('#ddlTreasury').append('<option value="0900">BIKANER</option>');
                $('#ddlTreasury').append('<option value="1000">BUNDI"</option>');
                $('#ddlTreasury').append('<option value="1100">CHITTORGARH</option>');
                $('#ddlTreasury').append('<option value="1200">CHURU</option>');
                $('#ddlTreasury').append('<option value="1300">DAUSA</option>');
                $('#ddlTreasury').append('<option value="1400">DHOLPUR</option>');
                $('#ddlTreasury').append('<option value="1500">DUNGARPUR</option>');
                $('#ddlTreasury').append('<option value="1600">GANGANAGAR</option>');
                $('#ddlTreasury').append('<option value="1700">HANUMANGARH</option>');
                $('#ddlTreasury').append('<option value="1800">JAIPUR (CITY)</option>');
                $('#ddlTreasury').append('<option value="2000">JAIPUR (RURAL)</option');
                $('#ddlTreasury').append('<option value="2100">JAIPUR (SECTT.)</optio');
                $('#ddlTreasury').append('<option value="2200">JAISALMER</option>');
                $('#ddlTreasury').append('<option value="2300">JALORE</option> ');
                $('#ddlTreasury').append('<option value="2400">JHALAWAR</option>');
                $('#ddlTreasury').append('<option value="2500">JHUNJHUNU</option>');
                $('#ddlTreasury').append('<option value="2600">JODHPUR (CITY)</option');
                $('#ddlTreasury').append('<option value="2700">JODHPUR (RURAL)</optio');
                $('#ddlTreasury').append('<option value="2800">KAROLI</option>');
                $('#ddlTreasury').append('<option value="2900">KOTA</option>');
                $('#ddlTreasury').append('<option value="3000">NAGAUR</option>');
                $('#ddlTreasury').append('<option value="3100">PALI</option>');
                $('#ddlTreasury').append('<option value="3200">PRATAPGARH</option>');
                $('#ddlTreasury').append('<option value="3300">RAJSAMAND</option> ');
                $('#ddlTreasury').append('<option value="3400">SAWAI MADHOPUR</option');
                $('#ddlTreasury').append('<option value="3500">SIKAR</option>');
                $('#ddlTreasury').append('<option value="3600">SIROHI</option>');
                $('#ddlTreasury').append('<option value="3700">TONK</option>');
                $('#ddlTreasury').append('<option value="3800">UDAIPUR</option>');
                $('#ddlTreasury').append('<option value="4100">UDAIPUR RURAL</option>');
            }
        });
    </script>

    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="BunchChallan">
                <span _ngcontent-c6="" style="color: #FFF">Budget Head Wise Revenue</span></h1>
        </div>

        <table width="100%" style="text-align: center" align="center" border="1">
            <tr>
                <td align="left">
                    <b><span style="color: #336699">From Date &nbsp;&nbsp;&nbsp;: </span></b>&nbsp;
                       
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
                <td align="left">
                    <b><span style="color: #336699">Treasury : </span></b>&nbsp;
                        <div class='input-group date' id="DivTreasury" class="col-md-3" style="width: 70%; display: inline-table">

                            <select id="ddlTreasury" class="form-control">
                                <%--<option value="0" selected="selected">--- Select Treasury ---</option>--%>
                                <%-- </select>--%>
                                <%--  <asp:DropDownList ID="ddlTreasury" runat="server" Width="150px" class="chzn-select">--%>

                                <%--</asp:DropDownList>--%>
                            </select>
                        </div>
                </td>
            </tr>
            <tr>

                <td align="left" colspan="2">
                    <div class='col-md-9 pull-left' id="DivbudgetHead">
                        <b><span style="color: #336699;text-align:left;">BudgetHead : </span></b>&nbsp;
                            <input id="txtBudgetHead" type="text" class="form-control" style="width: 50%; display: inline-table" />

                    </div>
                </td>
                <td align="center">
                    <button id="btnSubmit" type="submit" class="btn btn-default">Show</button>
                </td>

            </tr>
        </table>
    </div>
    <span class="row col-md-6" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: left; text-align: left;"></span>
    <div class="section2header col-md-12" id="divheader" style="display: none;">
        <h1>Treasury Wise Budget Head Revenue</h1>
    </div>
    <div class="row col-md-12" align="center" id="trrpt" runat="server">
        <table id="tbDetails" border="1" width="100%" cellpadding="0" cellspacing="0" style="display: none; text-align: left" class="table table-responsive table-striped table-bordered">
            <thead>
                <tr>
                    <th>S.N.</th>
                    <th>Treasury Name</th>
                    <th>BudgetHead</th>
                    <th>Amount(Treasury + Sub-Treasury)</th>
                </tr>
            </thead>

        </table>
        <div id="ajaxloader">
        </div>
    </div>
</asp:Content>

