<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgIntegratedTransactionRpt.aspx.cs" Inherits="WebPages_Reports_EgIntegratedTransactionRpt"
    Title="IntegratedTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/moment.js"></script>

    <link href="../../CSS/EgEchallan.css" rel="stylesheet" type="text/css" />
    <link href="../../js/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap-datetimepicker.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/jquery.dataTables.min.js"></script>
    <link href="../../CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />

    <script src="../../js/CDNFiles/buttons.print.min.js"></script>
    <script src="../../js/CDNFiles/dataTables.buttons.min.js"></script>
    <script src="../../js/CDNFiles/jszip.min.js"></script>
    <script src="../../js/CDNFiles/buttons.html5.min.js"></script>
    <script src="../../js/CDNFiles/pdfmake.min.js"></script>
    <script src="../../js/CDNFiles/vfs_fonts.js"></script>
    <%--==================CSS-JQUERY LOADER==================--%>
    <style type="text/css">
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

        .section2header {
            background: #3276b1;
            margin-top: -20px;
        }

        #ctl00_ContentPlaceHolder1_trrpt {
            margin-left: 10%;
        }

        .section2header h1 {
            font-size: 18px;
            color: #fff;
            padding: 1% 2%;
            font-weight: 500;
            margin-bottom: 0;
        }

        .btn {
            height: 35px !important;
            border-radius: 0px;
            margin-bottom: 10px;
            color: #fff !important;
            background-color: #337ab7 !important;
            border-color: #2e6da4 !important;
            width: 200px;
            font-size: medium !important;
            /*margin-top: 18%;*/
        }

        input[type=text], input[type=password] {
            height: 10%;
            /*margin-top: 20px;*/
        }

        #ctl00_ContentPlaceHolder1_ddlMerchant_chosen {
            width: 40%;
        }

        .chosen-container-single .chosen-single {
            border-radius: 0px;
            padding: 5px;
            height: 30px;
        }

        .chosen-container {
            display: inline-flex;
            margin-left: -45px;
        }

        #ctl00_ContentPlaceHolder1_rbl {
            margin-left: 20%;
            width: 50%;
        }

        .chosen-container .chosen-results {
            text-align: left;
        }

        label {
            padding: 10px;
        }

        table.dataTable tbody th, table.dataTable tbody td {
            color: black;
        }

        .input-group[class*="col-"] {
            width: 100%;
            padding-top: 5%;
        }

        .cartdbody-section {
            padding-left: 15px;
            padding-right: 15px;
        }

        #lblfdate, #lbltdate {
            width: 2px !important;
            padding: 0px;
            padding-top: 5%;
        }

        .col-lg-3, .col-sm-3 {
            width: 20%;
        }
    </style>
    <div id="cover-spin"></div>
    <script type="text/javascript">
        function fillDepartment() {
            //start department filling
            var userid = '<%= Session["UserId"] %>'
            var usertype = '<%= Session["UserType"] %>'
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/reports/EgIntegratedTransactionRpt.aspx/GetDepartment") %>',
                data: '{"userid":"' + userid + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    var objdata = $.parseJSON(data.d.split("|")[0]);
                    var ddldepartment = $("[id*=ddldepartment]");
                    ddldepartment.empty();
                    if (usertype != "5")
                        $('#ddldepartment').append('<option value="0">--- Select Department ---</option>');

                    $.each(objdata, function (i) {
                        ddldepartment.append($("<option></option>").val(objdata[i].DeptCode).html(objdata[i].deptnameEnglish));
                    })
                }
            });
            //End Department fill
        }
        function FillMerchant() {
            var usertype = '<%= Session["UserType"] %>'
            var deptcode = usertype == "5" ? '<%= Session["UserName"] %>' : ($("[id*=ddldepartment]").val() == null || $("[id*=ddldepartment]").val() == '' ? "0" : $("[id*=ddldepartment]").val());

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/reports/EgIntegratedTransactionRpt.aspx/GetMerchant") %>',
                data: '{"deptcode":"' + deptcode + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    var objdata = $.parseJSON(data.d.split("|")[0]);
                    var ddlmerchant = $("[id*=ddlmerchant]");
                    ddlmerchant.empty();
                    $('#ddlmerchant').append('<option value="0">--- ALL Merchant ---</option>');

                    $.each(objdata, function (i) {
                        ddlmerchant.append($("<option></option>").val(objdata[i].MerchantCode).html(objdata[i].MerchantName));
                    })
                }
            });
        }
        $(document).ready(function () {
            $("#divMerchant").hide();
            $("#divdept").hide();
            $("#tbSummary").hide();
            $("#tbDetail").hide();
            $("#divAuin").hide();


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

            var rptType;
            //select Summary Data OR Detail Data
            $("#ctl00_ContentPlaceHolder1_rblSearchType").change(function () {
                var val = $('[id*=ctl00_ContentPlaceHolder1_rblSearchType]').find('input:checked').val();
                //$('#tbSummary').empty();
                //$('#tbDetail').empty();
                $("#tbSummary_wrapper").hide()
                $("#tbDetail_wrapper").hide()
                //alert(val);
                if (val == "D") {
                    $("#divMerchant").show();
                    $("#divdept").show();
                    $("#divAuin").show();
                    fillDepartment();
                    FillMerchant();
                } else {
                    $("#ddldepartment").empty();
                    $("#divMerchant").hide();
                    $("#divdept").hide();
                    $("#divAuin").hide();
                }
            });
            //start Merchant
            $("#ddldepartment").change(function () {
                FillMerchant();
            });
            //end merchant

            $("#btnShow").click(function () {
                $('#ajaxloader').show();

                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                if ($('#txtFromDate').val() == "" || $('#txtToDate').val() == "") {

                    alert("Please enter date.");
                }
                else {
                    var rptType = $('[id*=ctl00_ContentPlaceHolder1_rblSearchType]').find('input:checked').val();
                    if (rptType == 'S') {
                        var usertype = '<%= Session["UserType"] %>'
                        var deptcode = usertype == "5" ? '<%= Session["UserName"] %>' : "0";

                        $.ajax({
                            type: 'POST',
                            url: '<%= ResolveUrl("~/WebPages/reports/EgIntegratedTransactionRpt.aspx/GetSummaryReport") %>',
                            data: '{"FromDate":"' + $('#txtFromDate').val() + '","ToDate":"' + $('#txtToDate').val() + '","deptcode":"' + deptcode + '"}',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (data) {

                                if (data.d.length < 3) {
                                    alert('No Record Found !');
                                    $("#tbSummary_wrapper").hide()
                                    $("#tbDetail_wrapper").hide()
                                } else {
                                    $("#tbSummary").show();
                                    var datatableVariable = $('#tbSummary').DataTable({

                                        "data": JSON.parse(data.d),
                                        "paging": true,
                                        "ordering": true,
                                        "searching": true,
                                        "destroy": true,
                                        dom: 'Bfrtip',
                                        buttons: [
                                            {
                                                extend: 'pdfHtml5',
                                                filename: 'Integration Report Summary',
                                                title: 'Department Integration Summary',
                                                messageTop: 'FromDate: ' + FromDate + '     ToDate: ' + ToDate + '',
                                                pageSize: 'A4', // Default
                                                orientation: 'portrait',
                                                footer: true
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
                                            }],
                                        columns: [
                                        {
                                            "data": null, "title": "Sr.No", "sortable": false,
                                            render: function (data, type, row, meta) {
                                                return meta.row + meta.settings._iDisplayStart + 1;
                                            }
                                        },
                                        {
                                            "data": "DeptName", "title": "Department Name", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black;">' + data + '</span>';
                                                }
                                                return data;
                                            }
                                        },
                                        {
                                            "data": "MerchantName", "title": "Merchant Name", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }
                                        },
                                        {
                                            "data": "TotalOnlineChallan", "title": "No Of Success Challan", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black;">' + data.toFixed(0) + '</span>';
                                                }
                                                return data;
                                            }
                                        },
                                        {
                                            "data": "Amount", "title": "Revenue Of Success Challan", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black;">' + data.toFixed(2) + '</span>';
                                                }
                                                return data;
                                            }
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
                                            // Total over this page
                                            pageTotal = api
                                                .column(3, { page: 'current' })
                                                .data()
                                                .reduce(function (a, b) {
                                                    return intVal(a) + intVal(b);
                                                }, 0);

                                            // Total over this page
                                            pageTotalAmount = api
                                                .column(4, { page: 'current' })
                                                .data()
                                                .reduce(function (a, b) {
                                                    var d = intVal(a) + intVal(b);
                                                    return d.toFixed(2);
                                                }, 0);

                                            // Total GRN
                                            total = api
                                                .column(3)
                                                .data()
                                                .reduce(function (a, b) {
                                                    var c = intVal(a) + intVal(b);
                                                    return c.toFixed(0);
                                                }, 0);
                                            // Total GRN
                                            totalamount = api
                                                .column(4)
                                                .data()
                                                .reduce(function (a, b) {
                                                    var c = intVal(a) + intVal(b);
                                                    return c.toFixed(2);
                                                }, 0);

                                            // Update footer
                                            $(api.column(3).footer()).html(
                                             '<div class="row" style="border:1px solid;margin-top: -7px;"><div class="row" style="    border-bottom: 1px solid;width: 100%;margin-left: -1px;"><div class="col-sm-6">PageTotal: </div><div class="col-sm-6" style="text-align: right;height:25px;">' + pageTotal + ' </div></div><div class="row"><div class="col-sm-6" style="margin-left: -1px;height: 25px;">Total: </div><div class="col-sm-6">' + total + '  </div> </div> </div>'
                                            );
                                            $(api.column(4).footer()).html(
                                            '<div class="row" style="border:1px solid;margin-top: -7px;"><div class="row" style="border-bottom: 1px solid;width: 100%;margin-left: -1px;padding-right: 15%;"><div class="col-sm-6">PageTotal:</div><div class="col-sm-6" style="text-align: right;height:25px;">₹' + pageTotalAmount + ' </div></div><div class="row" style="padding-right: 15%;"><div class="col-sm-6" style="margin-left: -1px;height: 25px;margin-left: 13px;">TotalAmount: </div><div class="col-sm-6" style="position: relative;margin-left: -7%;">₹' + totalamount + '  </div> </div> </div>'
                                            );

                                        }
                                    });
                                }
                                $('#ajaxloader').hide();
                            },
                            error: function (error) {
                                $("#tbSummary_wrapper").hide()
                                $("#tbDetail_wrapper").hide()
                                $("#tbSummary").hide();
                                $('#ajaxloader').hide();
                                alert(error.toString());

                            }

                        });
                    } else {
                        if ($("#ddldepartment").val() == "" || $("#ddldepartment").val() == "0") {
                            $("#tbSummary_wrapper").hide()
                            $("#tbDetail_wrapper").hide()
                            alert("Please select Department !");
                            $('#ajaxloader').hide();

                        } else {

                            $.ajax({
                                type: 'POST',
                                url: '<%= ResolveUrl("~/WebPages/reports/EgIntegratedTransactionRpt.aspx/GetDetailReport") %>',
                                data: '{"FromDate":"' + $('#txtFromDate').val() + '","ToDate":"' + $('#txtToDate').val() + '","deptcode":"' + $("#ddldepartment").val() + '","merchantcode":"' + $("#ddlmerchant").val() + '","auin":"' + $('#txtAuin').val() + '"}',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                success: function (data) {

                                    if (data.d.length < 3) {
                                        $("#tbSummary_wrapper").hide()
                                        $("#tbDetail_wrapper").hide()
                                        alert('No Record Found !');
                                    } else {
                                        $("#tbDetail").show();
                                        var datatableVariable1 = $('#tbDetail').DataTable({

                                            "data": JSON.parse(data.d),
                                            "paging": true,
                                            "ordering": true,
                                            "searching": true,
                                            "destroy": true,
                                            "footer": true,
                                            dom: 'Bfrtip',
                                            buttons: [
                                            {
                                                extend: 'pdfHtml5',
                                                filename: 'Integration Report Detail',
                                                title: 'Department Integration Detail',
                                                messageTop: 'FromDate: ' + FromDate + '     ToDate: ' + ToDate + '',
                                                pageSize: 'A4', // Default
                                                orientation: 'portrait',
                                                footer: true
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
                                            }],
                                            columns: [
                                        {
                                            "data": null, "title": "Sr.No", "sortable": false,
                                            render: function (data, type, row, meta) {
                                                return meta.row + meta.settings._iDisplayStart + 1;
                                            }
                                        },
                                        {
                                            "data": "GRN", "title": "GRN", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }
                                        },
                                    {
                                        "data": "AUIN", "title": "AUIN", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + data + '</span>';
                                            }
                                            return data;
                                        }
                                    },
                                    {
                                        "data": "RemitterName", "title": "Remitter Name", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + data + '</span>';
                                            }
                                            return data;
                                        }
                                    },
                                    {
                                        "data": "BankName", "title": "Bank Name", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + data + '</span>';
                                            }
                                            return data;
                                        }
                                    },
                                        {
                                            "data": "Status", "title": "Status", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }
                                        },
                                    {
                                        "data": "Amount", "title": "Amount", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black;display: flex; flex-flow: row nowrap; justify-content: right;"">' + data.toFixed(2) + '</span>';
                                            }
                                            return data;
                                        }
                                    },
                                            ],
                                            "footerCallback": function (tfoot, data, start, end, display) {
                                                var api = this.api(), data;
                                                // Remove the formatting to get integer data for summation
                                                var intVal = function (i) {
                                                    return typeof i === 'string' ?
                                                        i.replace(/[\$,]/g, '') * 1 :
                                                        typeof i === 'number' ?
                                                        i : 0;
                                                };
                                                // Total over this page
                                                pageTotalAmount = api
                                                    .column(6, { page: 'current' })
                                                    .data()
                                                    .reduce(function (a, b) {
                                                        var d = intVal(a) + intVal(b);
                                                        return d.toFixed(2);
                                                    }, 0);

                                                // Total GRN
                                                totalamount = api
                                                    .column(6)
                                                    .data()
                                                    .reduce(function (a, b) {
                                                        var c = intVal(a) + intVal(b);
                                                        return c.toFixed(2);
                                                    }, 0);

                                                // Update footer

                                                $(api.column(6).footer()).html(
                                                '<div class="row" style="border:1px solid;margin-top: -7px;"><div class="row" style="border-bottom: 1px solid;width: 100%;margin-left: -1px;padding-right: 15%;"><div class="col-sm-6">PageTotal:</div><div class="col-sm-6" style="text-align: right;height:25px;">₹' + pageTotalAmount + ' </div></div><div class="row" style="padding-right: 15%;"><div class="col-sm-6" style="margin-left: -1px;height: 25px;margin-left: 13px;">TotalAmount: </div><div class="col-sm-6">₹' + totalamount + '  </div> </div> </div>'
                                                );
                                            }
                                        });
                                    }
                                    $('#ajaxloader').hide();
                                },
                                error: function (error) {
                                    $("#tbSummary_wrapper").hide()
                                    $("#tbDetail_wrapper").hide()
                                    $("#tbDetail").hide();
                                    $('#ajaxloader').hide();
                                    alert(error.toString());
                                }
                            });
                        }
                    }
                }
            });
        });
    </script>

    <%--==============END CSS - JQUERY LOADER============--%>

    <div class="row">
        <div class="col-md-12">
            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Integrated Transaction">
                    <span _ngcontent-c6="" style="color: #FFF">Integrated Department</span></h2>
            </div>
        </div>
    </div>
    <div class="section2" style="border: 1px solid #ddd;">
        <div class="section2header">
            <h1>Department Wise Transaction</h1>
        </div>
        <div class="cardbody cartdbody-section">
            <div class="row" style="border: 1px solid #ddd;">
                <div class="col-lg-4 col-md-4 col-lg-offset-4 col-md-offset-4">
                    <asp:RadioButtonList ID="rblSearchType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="S" Selected="True">Summary</asp:ListItem>
                        <asp:ListItem Value="D">Detail</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group row">
                <div class="col-lg-3" style="width: 20%;">
                    <label class="col-sm-6 text-right" id="lblfdate">FromDate:</label>
                    <div class='col-sm-6 input-group date' id='datetimepicker1'>
                        <input id="txtFromDate" type="text" class="form-control" readonly />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </div>
                <div class="col-lg-3" style="width: 20%;">
                    <label class="col-sm-6 text-right" id="lbltdate">ToDate:</label>
                    <div class='col-sm-6 input-group date' id='datetimepicker2'>
                        <input id="txtToDate" type="text" class="form-control" readonly />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </div>
                <div class="col-lg-3" id="divdept" visible="false">
                    <div class="panel-group">
                        <label id="lbldepartment">Department:-</label>
                        <select id="ddldepartment" class="selectpicker form-control">
                            <option value="0">--- Select Department ---</option>
                        </select>
                    </div>
                </div>
                <div class="col-lg-3" id="divMerchant">
                    <div class="panel-group">
                        <label id="lblmerchant">Merchant:-</label>
                        <select id="ddlmerchant" class="selectpicker form-control">
                            <option value="0">--- ALL Merchant ---</option>
                        </select>
                    </div>
                </div>
                <div class="col-sm-3" id="divAuin">
                    <label class="col-sm-6 text-left" id="lblAuin" style="padding-left: 0px;">AUIN:</label>
                    <input id="txtAuin" type="text" class="form-control" style="margin-bottom: 10%;" maxlength="35" placeholder="AUIN Number" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <button id="btnShow" type="button" class="btn btn-primary">Show</button>
                </div>

            </div>

            <table id="tbSummary" cellspacing="0" style="background-color: #336699; color: white; text-align: left" border="1">
                <tr>
                    <th style="text-align: center;"></th>
                    <th style="text-align: center;"></th>
                    <th style="text-align: center;"></th>
                    <th style="text-align: right;"></th>
                </tr>
                <tfoot>
                    <tr>
                        <th colspan="3" style="text-align: right"></th>
                        <th style="text-align: center;"></th>
                        <th style="text-align: center;"></th>
                    </tr>
                </tfoot>
            </table>
            <table id="tbDetail" cellspacing="0" style="background-color: #336699; color: white; text-align: left" border="1">
                <tr>
                    <th style="text-align: center;"></th>
                    <th style="text-align: center;"></th>
                    <th style="text-align: center;"></th>
                    <th style="text-align: center;"></th>
                    <th style="text-align: right;"></th>
                </tr>
                <tfoot>
                    <tr>
                        <th colspan="6" style="text-align: right;"></th>
                        <th class="de" style="text-align: center;"></th>
                    </tr>
                </tfoot>
            </table>
        </div>
        <div id="ajaxloader">
        </div>
    </div>
</asp:Content>
