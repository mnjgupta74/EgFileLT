<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="EgSchemaAmountRpt.aspx.cs" Inherits="WebPages_Reports_EgSchemaAmountRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <link href="../../js/jquery-ui.css" rel="stylesheet" />
    <script src="../../js/jquery-ui.js"></script>--%>
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


    <%--==================CSS-JQUERY LOADER==================--%>
    <style type="text/css">
        #cover-spin {
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

        .btn-default.disabled, .btn-default[disabled], fieldset[disabled] .btn-default, .btn-default.disabled:hover, .btn-default[disabled]:hover, fieldset[disabled] .btn-default:hover, .btn-default.disabled:focus, .btn-default[disabled]:focus, fieldset[disabled] .btn-default:focus, .btn-default.disabled:active, .btn-default[disabled]:active, fieldset[disabled] .btn-default:active, .btn-default.disabled.active, .btn-default[disabled].active, fieldset[disabled] .btn-default.active {
            background-color: #abaaaa;
            border-color: #ccc;
        }

        #cover-spin::after {
            content: '';
            display: block;
            position: absolute;
            left: 48%;
            top: 40%;
            width: 40px;
            height: 40px;
            border-style: solid;
            border-color: black;
            border-top-color: transparent;
            border-width: 4px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }
    </style>

    <div id="cover-spin"></div>
    <%--<script type="text/javascript">

        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnshow"]').click(function () {
                if ($('#txtFromDate').val() != "" && $('#txtToDate').val() != "") {
                $('#cover-spin').show(0)

            }
        });
    });
    </script>--%>
    <%--==============END CSS - JQUERY LOADER============--%>

    <style>
        .dt-buttons {
            text-align: left;
        }

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>


    <style type="text/css">
        input[type=text], input[type=password] {
            height: auto;
        }

        .input-group-addon, .input-group-btn {
            width: 20% !important;
        }
    </style>

    <script type="text/javascript">
        var favorite = [];
        var selectedData = [];
        var DeptCode;
        var FromDate;
        var ToDate;
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

            //Bind Department Using Ajax
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/WebPages/Reports/EgSchemaAmountRpt.aspx/Departmentlist") %>',
                data: '{"Session":"' + <%= Session["UserId"] %> + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (msg) {
                    BindDeptListDropDown(msg);
                }
            });



            function BindDeptListDropDown(msg) {
                
                if (msg.d.length > 0 && msg.d != "[]") {
                   
                    var json = JSON.parse(msg.d);
                    //$("#ctl00_ContentPlaceHolder1_ddlDepartment").empty();
                    //$("#ctl00_ContentPlaceHolder1_ddlDepartment").append('<option value=' + '0' + '>' + '--- Select Department ---' + '</option>');
                    $.each(json, function (index, obj) {
                        //if (this.Count > 0) {
                        //alert(this.deptnameEnglish);
                        //$('#<%=ddlDepartment.ClientID%>').chosen();
                        $(".chosen-select").chosen();
                        $("#<%=ddlDepartment.ClientID%>").append('<option value=' + this.DeptCode + '>' + this.deptnameEnglish + '</option>');
                        $('#<%=ddlDepartment.ClientID%>').trigger("chosen:updated");
                        //}
                    });
                }
                else {
                    alert("No Record found");
                }
            }











            //$("#btnReset").click(function () {
            //    $("#ctl00_ContentPlaceHolder1_ddlDepartment").attr('disabled', 'false');
            //    $("#txtFromDate").attr('disabled', 'false');
            //    $("#txtToDate").attr('disabled', 'false');
            //});
            $('#btnReset').click(function () {
                //$("#ctl00_ContentPlaceHolder1_ddlDepartment").prop("disabled", false);
                $('#<%=ddlDepartment.ClientID%>').prop('disabled', false).trigger("chosen:updated");
                $("#txtFromDate").prop("disabled", false);
                $("#txtToDate").prop("disabled", false);
                $("#btnSubmit").prop("disabled", false);
                $("#btnSubmitTreasury").prop("disabled", false);
                $("#tbDetails tbody tr").remove();
                $("#tblBudgetHead tr").empty();
                $("#tblSelectedHead tr").empty();
                $("#tbDetailsTreasuryWise tbody tr").remove();
                $("#tbDetails tbody tr").hide();
                $("#tbDetailsTreasuryWise tbody tr").hide();
                $("#spntotalamt").hide();
                $('#<%= trrptTreasuryWise.ClientID %>').hide();
                $('#<%= trrpt.ClientID %>').hide();
                $('input[type=checkbox]').prop('checked', false);
                $("#ctl00_ContentPlaceHolder1_ddlMajorHead").val('0');
                favorite = [];
                selectedData = [];
                DeptCode = "";
                FromDate = "";
                ToDate = "";

            });
            $("#btnSubmit").click(function () {
                $('#cover-spin').show(0);
                //$('#ajaxloader').show();
                //var Myobj = { Data: favorite };
                var DeptCode = $('#ctl00_ContentPlaceHolder1_ddlDepartment').val();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var Myobj = { Data: favorite, DeptCode: DeptCode, FromDate: FromDate, ToDate: ToDate };
                var listdata = JSON.stringify(Myobj);
                var startDate = moment(FromDate, "DD/MM/YYYY");
                var endDate = moment(ToDate, "DD/MM/YYYY");
                var result = endDate.diff(startDate, 'days');
                if ((result) > 30) {
                    //$('#ajaxloader').hide();
                    $('#cover-spin').hide(0);
                    alert('Date Difference can not be more than 30 days.');
                }
                else {
                    if (favorite == "") {
                        alert('Please Select purpose or MajorHead');
                        $('#cover-spin').hide(0);
                    }
                    else {
                        $.ajax({
                            type: 'POST',
                            url: '<%= ResolveUrl("~/WebPages/Reports/EgSchemaAmountRpt.aspx/ShowData") %>',
                            data: "{listdata:" + listdata + "}",
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (msg) {
                                if (msg.d.length > 0 && msg.d != "[]") {
                                    //$("#ctl00_ContentPlaceHolder1_ddlDepartment").attr('disabled', 'true');
                                    $('#<%=ddlDepartment.ClientID%>').prop('disabled', true).trigger("chosen:updated");
                                    $("#txtFromDate").attr('disabled', 'true');
                                    $("#txtToDate").attr('disabled', 'true');
                                    $('#cover-spin').hide(0);
                                    $("#tbDetails").show();
                                    $("#spntotalamt").show();
                                    $("#btnSubmit").attr('disabled', 'true');
                                    $("#btnSubmitTreasury").attr('disabled', 'true');
                                    $('#tbDetailsTreasuryWise').hide();
                                    $('#<%= trrptTreasuryWise.ClientID %>').hide();
                                    $('#<%= trrpt.ClientID %>').show();

                                    $('input[type=checkbox]').prop('disabled', true);

                                    var datatableVariable = $('#tbDetails').DataTable({
                                        "data": JSON.parse(msg.d),
                                        "paging": true,
                                        "ordering": true,
                                        "searching": true,
                                        "destroy": true,
                                        dom: 'Bfrtip',
                                        buttons: [
                                                    {
                                                        extend: 'pdfHtml5',
                                                        filename: 'Schema Amount Details',
                                                        pageSize: 'A4', // Default
                                                        title: 'Government of Rajasthan' + '\n' + 'Schema Amount Details' + '\n' + 'From ' + FromDate + ' To ' + ToDate,
                                                        messageBottom: '\n \n For More Detail : Egras.raj.nic.in',
                                                        orientation: 'portrait', // Default
                                                        customize: function (doc) {
                                                            doc.content[1].table.widths =
                                                                Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                                        }
                                                    },
                                                    {
                                                        extend: 'excel',
                                                        title: 'Schema Amount Details',
                                                        filename: 'SchemaAmountDetails',
                                                    },
                                            {
                                                extend: 'print',
                                                title: 'Schema Amount Details',
                                                filename: 'SchemaAmountDetails',
                                                title: 'Government of Rajasthan' + '\n' + 'Schema Amount Details' + '\n' + 'From ' + FromDate + ' To ' + ToDate,
                                                messageBottom: '\n \n For More Detail : Egras.raj.nic.in',
                                                pageSize: 'A4', // Default
                                                orientation: 'portrait', // Default

                                            },
                                            {
                                                extend: 'copy',
                                                filename: 'SchemaAmountDetails',
                                                title: 'Government of Rajasthan' + '\n' + 'Schema Amount Details' + '\n' + 'From ' + FromDate + ' To ' + ToDate,
                                                messageBottom: '\n \n For More Detail : Egras.raj.nic.in',
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
                                                        "data": null, "sortable": false, "class": "text-center", "title": "S.N.",
                                                        render: function (data, type, row, meta) {
                                                            return meta.row + meta.settings._iDisplayStart + 1;
                                                        }
                                                    },
                                                    {
                                                        "data": "SchemaName",
                                                        "title": "Schema Name",
                                                        "class": "text-center"
                                                    },
                                                         {
                                                             "data": "Budgethead",
                                                             "title": "BudgetHead",
                                                             "class": "text-center"
                                                         },
                                                {
                                                    "data": "Amount",
                                                    "title": "Amount",
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
                                            $("#spntotalamt").html('PageTotal: ₹' + pageTotal + ' (Total Amount: ₹ ' + total + ')');
                                        },
                                    });
                                    //$('#ajaxloader').hide();
                                    $('#cover-spin').hide(0);

                                }
                                else {
                                    alert("No Record found");
                                    //$('#ajaxloader').hide();
                                    $('#cover-spin').hide(0);
                                    $("#spntotalamt").hide();
                                }


                            },
                            error: function (error) {
                                //$('#ajaxloader').hide();
                                $('#cover-spin').hide(0);
                                $("#spntotalamt").hide();
                                $("#tbDetailsTreasuryWise").hide();
                                $("#tbDetails").hide();
                                $('#<%= trrptTreasuryWise.ClientID %>').hide();
                                $('#<%= trrpt.ClientID %>').hide();
                                alert(error.toString());
                            }
                        });
                    }
                }
                return false;
                //$("#tblSelectedHead input[type=checkbox]").each(function () {
                //    favorite.push($(this).val());
                //});
                //favorite.join(", ",1);
                //alert(favorite);
            });
            $("#btnSubmitTreasury").click(function () {
                $('#cover-spin').show(0)
                //$('#ajaxloader').show();
                //var Myobj = { Data: favorite };
                var DeptCode = $('#ctl00_ContentPlaceHolder1_ddlDepartment').val();
                var FromDate = $('#txtFromDate').val();
                var ToDate = $('#txtToDate').val();
                var Myobj = { Data: favorite, DeptCode: DeptCode, FromDate: FromDate, ToDate: ToDate };
                var listdata = JSON.stringify(Myobj);
                var startDate = moment(FromDate, "DD/MM/YYYY");
                var endDate = moment(ToDate, "DD/MM/YYYY");
                var result = endDate.diff(startDate, 'days');
                if ((result) > 30) {
                    $('#cover-spin').hide(0);
                    $("#spntotalamt").hide();
                    //$('#ajaxloader').hide();
                    alert('Date Difference can not be more than 30 days.');
                }
                else {
                    if (favorite == "") {
                        alert('Please Select purpose or MajorHead');
                        $('#cover-spin').hide(0);
                    }
                    else {
                        $.ajax({
                            type: 'POST',
                            url: '<%= ResolveUrl("~/WebPages/Reports/EgSchemaAmountRpt.aspx/ShowDataTreasuryWise") %>',
                            data: "{listdata:" + listdata + "}",
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (msg) {
                                if (msg.d.length > 0 && msg.d != "[]") {
                                    $("#spntotalamt").show();
                                    $('#<%=ddlDepartment.ClientID%>').prop('disabled', true).trigger("chosen:updated");
                                    //$("#ctl00_ContentPlaceHolder1_ddlDepartment").attr('disabled', 'true');
                                    $("#txtFromDate").attr('disabled', 'true');
                                    $("#txtToDate").attr('disabled', 'true');
                                    $("#btnSubmit").attr('disabled', 'true');
                                    $("#btnSubmitTreasury").attr('disabled', 'true');
                                    $('#cover-spin').hide(0);
                                    $("#tbDetailsTreasuryWise").show();
                                    $('#<%= trrpt.ClientID %>').hide();
                                    $('#<%= trrptTreasuryWise.ClientID %>').show();
                                    $('input[type=checkbox]').prop('disabled', true);
                                    $("#tbDetails").hide();
                                    var datatableVariable = $('#tbDetailsTreasuryWise').DataTable({
                                        "data": JSON.parse(msg.d),
                                        "paging": true,
                                        "ordering": true,
                                        "searching": true,
                                        "destroy": true,
                                        dom: 'Bfrtip',
                                        buttons: [
                                                    {
                                                        extend: 'pdfHtml5',
                                                        filename: 'Schema Amount Details',
                                                        pageSize: 'A4', // Default
                                                        title: 'Government of Rajasthan' + '\n' + 'Schema Amount Details' + '\n' + 'Date From ' + FromDate + ' To ' + ToDate,
                                                        //messageTop: 'Date From ' + FromDate + ' To ' + ToDate,
                                                        messageBottom: '\n \n For More Detail : Egras.raj.nic.in',
                                                        customize: function (doc) {
                                                            doc.content[1].table.widths =
                                                                Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                                        }

                                                    },
                                                    {
                                                        extend: 'excel',
                                                        title: 'Schema Amount Details',
                                                        filename: 'SchemaAmountDetails',
                                                    },
                                            {
                                                extend: 'print',
                                                filename: 'SchemaAmountDetails',
                                                pageSize: 'A4', // Default
                                                orientation: 'portrait', // Default
                                                title: 'Government of Rajasthan' + '\n' + 'Schema Amount Details' + '\n' + 'From ' + FromDate + ' To ' + ToDate,
                                                messageBottom: '\n \n For More Detail : Egras.raj.nic.in',

                                            },
                                            {
                                                extend: 'copy',
                                                filename: 'SchemaAmountDetails',
                                                title: 'Government of Rajasthan' + '\n' + 'Schema Amount Details' + '\n' + 'From ' + FromDate + ' To ' + ToDate,
                                                messageBottom: '\n \n For More Detail : Egras.raj.nic.in',

                                            },
                                        ],
                                        columns: [
                                                    {
                                                        "data": null, "sortable": false, "class": "text-center", "title": "S.N.",
                                                        render: function (data, type, row, meta) {
                                                            return meta.row + meta.settings._iDisplayStart + 1;
                                                        }
                                                    },
                                                    {
                                                        "data": "SchemaName",
                                                        "title": "Schema Name",
                                                        "class": "text-center"
                                                    },
                                                         {
                                                             "data": "Budgethead",
                                                             "title": "BudgetHead",
                                                             "class": "text-center"
                                                         },
                                                         {
                                                             "data": "Location",
                                                             "title": "Location",
                                                             "class": "text-center"
                                                         },
                                                {
                                                    "data": "Amount",
                                                    "title": "Amount",
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
                                                .column(4)
                                                .data()
                                                .reduce(function (a, b) {
                                                    return intVal(a) + intVal(b);
                                                }, 0);

                                            // Total over this page
                                            pageTotal = api
                                                .column(4, { page: 'current' })
                                                .data()
                                                .reduce(function (a, b) {
                                                    return intVal(a) + intVal(b);
                                                }, 0);
                                            // Update footer  spntotalComAmt
                                            $("#spntotalamt").html('PageTotal: ₹' + pageTotal + ' (Total Amount: ₹ ' + total + ')');
                                        },
                                    });
                                    //$('#ajaxloader').hide();
                                    $('#cover-spin').hide(0);

                                }
                                else {
                                    alert("No Record found");
                                    //$('#ajaxloader').hide();
                                    $('#cover-spin').hide(0);
                                    $("#spntotalamt").hide();
                                }
                            },

                            error: function (error) {
                                //$('#ajaxloader').hide();
                                $('#cover-spin').hide(0);
                                $("#spntotalamt").hide();
                                $("#tbDetailsTreasuryWise").hide();
                                $("#tbDetails").hide();
                                $('#<%= trrptTreasuryWise.ClientID %>').hide();
                                $('#<%= trrpt.ClientID %>').hide();
                                alert(error.toString());
                            }
                        });
                    }
                }
                return false;
                //$("#tblSelectedHead input[type=checkbox]").each(function () {
                //    favorite.push($(this).val());
                //});
                //favorite.join(", ",1);
                //alert(favorite);
            });



            // Select Department
            $("#ctl00_ContentPlaceHolder1_ddlDepartment").change(function () {
                var DeptCode = $('#ctl00_ContentPlaceHolder1_ddlDepartment').val();
                //alert(DeptCode);
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgSchemaAmountRpt.aspx/getMajorHeadList") %>',
                    data: '{"DeptCode":"' + DeptCode + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        DisplayMajorHeadListDropDown(msg);
                    }
                });
            });

            //Select MajorHead
            $("#ctl00_ContentPlaceHolder1_ddlMajorHead").change(function () {
                var DeptCode = $('#ctl00_ContentPlaceHolder1_ddlDepartment').val();
                //$("#ctl00_ContentPlaceHolder1_ddlDepartment").disabled = true;
                //alert(DeptCode + "-"+ this.value)
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/Reports/EgSchemaAmountRpt.aspx/getBudgetHeadList") %>',
                    data: '{"DeptCode":"' + DeptCode + '","MajorHead":"' + this.value + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        DisplayBudgetHeadList(msg);
                    }
                });
            })

            //Major Head List Bind
            function DisplayMajorHeadListDropDown(msg) {
                if (msg.d.length > 0 && msg.d != "[]") {
                    //$("#<%=ddlDepartment.ClientID%>").attr('disabled', 'true');
                    $('#<%=ddlDepartment.ClientID%>').prop('disabled', true).trigger("chosen:updated");
                    var json = JSON.parse(msg.d);
                    $("#ctl00_ContentPlaceHolder1_ddlMajorHead").empty();
                    $("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + '0' + '>' + '--- Select MajorHead ---' + '</option>');
                    $.each(json, function (index, obj) {
                        if (this.Count == 0) {
                            $("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + this.Majorheadcode + '>' + this.Majorheadname + '</option>');
                        }
                    });
                }
                else {
                    alert("No Record found");
                }
            }

            //BudgetHead List Bind
            function DisplayBudgetHeadList(msg) {
                $('#divBudgetHeads').show();
                $("#tblBudgetHead").html("");
                var json = JSON.parse(msg.d);
                var HeadCount = 0;
                var dis = "";
                $.each(json, function (index, obj) {
                    var row;
                    if (selectedData.includes(obj.ScheName)) {
                        row = '<tr id="tr1' + obj.ScheName + '"><td>' +
                       '<input type="checkbox" ' + dis + '  value="' + obj.ScheName + "|" + obj.ScheCode + ' " id = "cb1' + HeadCount + '" checked/>' + '</td><td><label style="margin-left: 10px;margin-top: 10px;">' + obj.ScheName + '</label></td> </tr>'
                    }
                    else {
                        row = '<tr id="tr1' + obj.ScheName + '"><td>' +
                       '<input type="checkbox" ' + dis + '  value="' + obj.ScheName + "|" + obj.ScheCode + ' " id = "cb1' + HeadCount + '"/>' + '</td><td><label style="margin-left: 10px;margin-top: 10px;">' + obj.ScheName + '</label></td> </tr>'
                    }
                    $("#tblBudgetHead").append(row);
                    HeadCount = HeadCount + 1;
                });
            }

            //  BudgetHead Selection Final List
            $('#tblBudgetHead').on('click', 'input', function () {
                //var DeptCode = $('#ctl00_ContentPlaceHolder1_ddlDepartment').val();

                var SchemaName = $("#" + this.id).val().split('|');
                AddRemoveItems("#" + this.id, SchemaName[0], SchemaName[1]);
            });

        });

        function AddRemoveItems(ControlId, SchemaName, SchemaCode) {
            if ($(ControlId).prop("checked") == true) {
                var row = '<tr id="tr2' + SchemaName.toString().replace(/-/g, '').substr(0, 13) + '"><td>' +
                '<input type="checkbox" style="visibility:hidden" value="' + SchemaCode + '" id = "cb2 + ' + SchemaName + '" />' + '</td><td>' + SchemaName + '</td> </tr>'
                $("#tblSelectedHead").append(row);

                favorite.push(SchemaName + "|" + SchemaCode);
                selectedData.push(SchemaName);

            }
            else {
                favorite.pop(SchemaName + "|" + SchemaCode);
                selectedData.pop(SchemaName);
                $("#tr2" + SchemaName.toString().replace(/-/g, '').substr(0, 13)).remove();
            }
        }



    </script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../App_Themes/images/progress.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Search Report" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Schemawise Report</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="right" title="Search Report By Identity" />
    </div>
    <table width="100%" style="text-align: center" align="center" border="1">
        <tr>
            <td align="left">
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                       
                        <div class='input-group date' id='datetimepicker1' style="width: 70%; display: inline-flex">
                            <input id="txtFromDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
            </td>
            <td align="left">
                <b><span style="color: #336699">To Date : </span></b>&nbsp;
                        <div class='input-group date' id='datetimepicker2' style="width: 50%; display: inline-flex">
                            <input id="txtToDate" type="text" class="form-control" readonly />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 400px">
                <b><span style="color: #336699">Department : </span></b>&nbsp;
                       
                        <div style="width: 70%; display: inline-flex">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Style="font-family: Verdana !important; font-size: 13px;"
                                CssClass="form-control chosen-select">
                                <asp:ListItem Value="0" Text="--Select Department--"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Department"
                                ControlToValidate="ddlDepartment" ValidationGroup="vldInsert" InitialValue="0"
                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                        </div>
            </td>
            <td align="left">
                <b><span style="color: #336699">MajorHead : </span></b>&nbsp;
                       
                        <div style="display: inline-flex; width: 40%;">
                            <asp:DropDownList ID="ddlMajorHead" runat="server" Style="font-family: Verdana !important; font-size: 13px;"
                                CssClass="form-control">
                                <asp:ListItem Value="0" Text="--Select MajorHead--"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select MajorHead"
                                ControlToValidate="ddlMajorHead" ValidationGroup="vldInsert" InitialValue="0"
                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                        </div>
            </td>
        </tr>
    </table>
    <table style="width: 100%" align="center" border="1" id="divBudgetHeads">
        <tr>
            <td>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <span style="vertical-align: top; color: royalblue; margin-left: 165px; font-size: 13px; font-weight: bold;">All Budget Heads</span>
                                <div style="overflow: auto; height: 208px; width: 450px; border: 1px solid gray; margin-top: 15px; margin-bottom: 15px;">
                                    <table id="tblBudgetHead"></table>
                                </div>
                            </td>
                            <td>
                                <span style="vertical-align: top; color: royalblue; margin-left: 165px; font-size: 13px; font-weight: bold;">Selected Budget Heads</span>
                                <div style="overflow: auto; height: 208px; width: 450px; border: 1px solid gray; margin-top: 15px; margin-bottom: 15px;">
                                    <table id="tblSelectedHead"></table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnSubmit" class="btn btn-default" style="margin-left: 65px; height: 33px" value="Show Schewise Report" />
                <input type="button" id="btnSubmitTreasury" class="btn btn-default" style="margin-left: 65px; height: 33px" value="Show Treasury Wise Report" />
                <input type="button" id="btnReset" class="btn btn-default" style="margin-left: 65px; height: 33px" value="Reset" />
            </td>
        </tr>
    </table>

    <span class="row col-md-6" id="spntotalamt" style="font-size: 14px; font-weight: 600; float: left; text-align: left;"></span>
    <div class="row col-md-12" align="center" id="trrpt" runat="server" style="margin-top: 10px">
        <table id="tbDetails" border="1" width="100%" cellpadding="0" cellspacing="0" style="display: none; text-align: center" class="table table-responsive table-striped table-bordered">
        </table>

    </div>
    <div class="row col-md-12" align="center" id="trrptTreasuryWise" runat="server" style="margin-top: 10px">
        <table id="tbDetailsTreasuryWise" border="1" width="100%" cellpadding="0" cellspacing="0" style="display: none; text-align: center" class="table table-responsive table-striped table-bordered">
        </table>
        <div id="ajaxloader">
        </div>
    </div>
   <%-- <script>
        $('#<%=ddlDepartment.ClientID%>').chosen();
    </script>--%>
</asp:Content>




