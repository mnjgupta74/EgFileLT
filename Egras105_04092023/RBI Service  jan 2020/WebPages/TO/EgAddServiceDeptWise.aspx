<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgAddServiceDeptWise.aspx.cs" Inherits="WebPages_TO_EgAddServiceDeptWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" src="../../JS/chosen.jquery.js"></script>--%>
    <%-- <script type="text/javascript" src="../../js/chosen.jquery.min.js"></script>
    <link href="../../CSS/chosen.min.css" rel="stylesheet" />--%>
    <%--<link href="../../js/jquery-ui.css" rel="stylesheet" />--%>
    <%--<script type="text/javascript"  src="../../js/jquery.min.js"></script>--%>
    <%--<script type="text/javascript"  src="../../js/jquery-ui.js"></script>--%>
    <script type="text/javascript" src="../../js/jquery-3.6.0.min.js"></script>

    <style type="text/css">
        select {
            height: 25px;
        }

        .chzn-container-single .chzn-single {
            border-radius: 0px;
            -webkit-border-radius: 0px;
            background-image: none;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('#btnSubmit').hide();
            $('#divServiceName').hide();
            $('#tdService').hide();
            $('#divBudgetHeads').hide();
            $('#divServiceEdit').hide();

            $('#<%=rblType.ClientID %>').change(function () {
                var challanType = $('#<%=rblType.ClientID %> input:checked').val();
                if (challanType == 1) {
                    $("#tdMajorHead").hide();
                    $("#tdService").show();
                    $('#divServiceName').hide();
                    $(".chzn-select").chosen();
                    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                    //$('#ctl00_ContentPlaceHolder1_ddldepartment').val("0").trigger("liszt:updated");
                    //$("#ctl00_ContentPlaceHolder1_ddlService").empty();
                    $('#ctl00_ContentPlaceHolder1_ddlService').val("0").trigger("liszt:updated");
                    //$("#ctl00_ContentPlaceHolder1_ddlService").append('<option value=' + '0' + '>' + '--- Select Service ---' + '</option>');
                    $('#btnSubmit').hide();
                    $('#divServiceName').hide();
                    $('#divServiceEdit').hide();
                    $('#divBudgetHeads').hide();
                    $('#txtServiceName').val('');
                }
                else {
                    $("#tdMajorHead").show();
                    $("#tdService").hide();
                    $('#btnSubmit').hide();
                    $('#divServiceName').hide();
                    $('#divServiceEdit').hide();
                    $('#divBudgetHeads').hide();
                    $('#txtServiceName').val('');
                    $(".chzn-select").chosen();
                    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

                    //$('#ctl00_ContentPlaceHolder1_ddldepartment').val("0").trigger("liszt:updated");
                    //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").empty();
                    $('#ctl00_ContentPlaceHolder1_ddlMajorHead').val("0").trigger("liszt:updated");
                    //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + '0' + '>' + '--- Select MajorHead ---' + '</option>');
                }
            });

            $("#ctl00_ContentPlaceHolder1_ddldepartment").change(function () {
                if (this.value == "0") {
                    alert('Select Department');
                    $('#tblCustomers').html('');
                    $('#tblSelectedHeads').html('');
                    return;
                }
                var Type = $('#<%=rblType.ClientID %> input:checked').val();
                if (Type == 0) {
                    $.ajax({
                        type: 'POST',
                        url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/getMajorHeadList") %>',
                        data: '{"DeptCode":"' + this.value + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (msg) {
                            DisplayMajorHeadList(msg);
                        }
                    });
                }
                else {
                    $.ajax({
                        type: 'POST',
                        url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/getServiceList") %>',
                        data: '{"DeptCode":"' + this.value + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (msg) {
                            DisplayServiceList(msg);
                        }
                    });
                }
                $('#ctl00_ContentPlaceHolder1_ddlMajorHead').val("0").trigger("liszt:updated");
                $('#tblCustomers').html('');
                $('#tblSelectedHeads').html('');
                $('#divBudgetHeads').hide();
                $('#divServiceEdit').hide();
                $('#divServiceName').hide();
                $('#btnSubmit').hide();
            });
            $("#ctl00_ContentPlaceHolder1_ddlMajorHead").change(function () {
                alert('hi');
                var DeptCode = $('#ctl00_ContentPlaceHolder1_ddldepartment').val();
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/getBudgetHeadList") %>',
                    data: '{"DeptCode":"' + DeptCode + '","MajorHead":"' + this.value + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        DisplayBudgetHeadList(msg);
                    }
                });
            });
            $("#ctl00_ContentPlaceHolder1_ddlService").change(function () {
                var DeptCode = $('#ctl00_ContentPlaceHolder1_ddldepartment').val();
                if (this.value == 0) {
                    alert('Select Service');
                    $("#tblCustomers").html("");
                    $("#tblSelectedHeads").html("");
                }
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/getServiceHeadsList") %>',
                    data: '{"DeptCode":"' + DeptCode + '","Service":"' + this.value + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        DisplayBudgetHeadList_ServiceWise(msg);
                    }
                });
            });
            $('#tblCustomers').on('click', 'input', function () {
                var SelectedValue = $("#" + this.id).val().split('|', 2);
                var SchemaName = SelectedValue[1];
                var ScheCode = SelectedValue[0];
                AddRemoveItems("#" + this.id, SchemaName, ScheCode);
            });
            $('#btnSubmit').click(function () {

                var HeadCnt = 0;
                var ParamBudgetHead = "";
                var ParamScheCode = "";
                $('#tblCustomers > tr').each(function () {
                    if ($("#cb1" + HeadCnt).prop("checked") == true) {
                        var HeadValue = $("#cb1" + HeadCnt).val().split('|', 2);
                        var SchemaName = HeadValue[1];
                        var ScheGrp = HeadValue[0];
                        var ScheCode = ScheGrp.split('-', 1);
                        if (ScheCode > 100000) {
                            ScheCode = 0;
                        }
                        var PreScheCode = '00000' + ScheCode.toString()

                        var BudgetHeadName = SchemaName.replace(/-/g, '').substr(0, 13);
                        ParamBudgetHead = BudgetHeadName + PreScheCode.substr(PreScheCode.length - 5) + '|' + ParamBudgetHead;
                        ParamScheCode = ScheCode + '|' + ParamScheCode;
                    }
                    HeadCnt = HeadCnt + 1;
                });
                if (ParamBudgetHead.length < 2) {
                    alert('Select Budget Head')
                    return;
                }

                if ($.trim($('#txtServiceName').val()) == "") {
                    alert('Enter Service Name')
                    return;
                }
                else {
                    if ($('#ctl00_ContentPlaceHolder1_hdnServiceNames').val() != "") {
                        var arraynames = JSON.parse($('#ctl00_ContentPlaceHolder1_hdnServiceNames').val());
                        var resultCase = 0;
                        $.each(arraynames, function (i, v) {
                            if ($.trim(v.toLowerCase()) == $.trim($('#txtServiceName').val().toLowerCase())) {
                                alert('Service Name Already Exist');
                                resultCase = 1;
                                return;
                            }
                        });
                    }
                    if (resultCase == 1)
                        return;
                }
                //alert(ParamBudgetHead.substr(0, ParamBudgetHead.length - 1));
                //alert(ParamScheCode.substr(0, ParamScheCode.length - 1));
                var Url = '<%= ResolveUrl("~/GRNHandler.ashx") %>';
                $.ajax({
                    type: "POST",
                    url: Url + "?HandlerVal=" + ParamBudgetHead.substr(0, ParamBudgetHead.length - 1) + "^" + $('#txtServiceName').val() + "^" + $('#ctl00_ContentPlaceHolder1_ddldepartment').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //data: obj,
                    async: true,
                    complete: function () {
                        $.ajax({
                            type: 'POST',
                            url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/InsertServiceData") %>',
                            //data: '{"BudgetHead":"' + ParamBudgetHead.substr(0, ParamBudgetHead.length - 1) + '","ScheCode":"' + ParamScheCode.substr(0, ParamScheCode.length - 1) + '","ServiceName":"' + $('#txtServiceName').val() + '","DeptCode":"' + $('#ctl00_ContentPlaceHolder1_ddldepartment').val() + '"}',
                            data: '{"Parameter":"' + ParamBudgetHead.substr(0, ParamBudgetHead.length - 1) + "^" + $('#txtServiceName').val() + "^" + $('#ctl00_ContentPlaceHolder1_ddldepartment').val() + '"}',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (msg) {
                                alert(msg.d);
                                $(".chzn-select").chosen();
                                $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                                $.ajax({
                                    type: 'POST',
                                    url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/getServiceList") %>',
                                    data: '{"DeptCode":"' + $("#ctl00_ContentPlaceHolder1_ddldepartment").val() + '"}',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    success: function (msg) {
                                        DisplayServiceList(msg);
                                    }
                                         });
                                //$('#ctl00_ContentPlaceHolder1_ddldepartment').val("0").trigger("liszt:updated");
                                //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").empty();
                                $('#ctl00_ContentPlaceHolder1_ddlMajorHead').val("0").trigger("liszt:updated");
                                //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + '0' + '>' + '--- Select MajorHead ---' + '</option>');
                                $('#btnSubmit').hide();
                                $('#divServiceName').hide();
                                $('#divBudgetHeads').hide();
                                $('#txtServiceName').val('')
                            }
                        });
                    }
                });
            });
            $('#btnEdit').click(function () {
                var HeadCnt = 0;
                var ParamBudgetHead = "";
                var ParamScheCode = "";
                $('#tblCustomers > tr').each(function () {
                    if ($("#cb1" + HeadCnt).prop("checked") == true) {
                        var HeadValue = $("#cb1" + HeadCnt).val().split('|', 2);
                        var SchemaName = HeadValue[1];
                        var ScheGrp = HeadValue[0];
                        var ScheCode = ScheGrp.split('-', 1);
                        if (ScheCode > 100000) {
                            ScheCode = 0;
                        }
                        var PreScheCode = '00000' + ScheCode.toString()

                        var BudgetHeadName = SchemaName.replace(/-/g, '').substr(0, 13);
                        ParamBudgetHead = BudgetHeadName + PreScheCode.substr(PreScheCode.length - 5) + '|' + ParamBudgetHead;

                        //var BudgetHeadName = SchemaName.replace(/-/g, '').substr(0, 13);
                        //ParamBudgetHead = BudgetHeadName + '|' + ParamBudgetHead;
                        ParamScheCode = ScheCode + '|' + ParamScheCode;
                    }
                    HeadCnt = HeadCnt + 1;
                });
                if (ParamBudgetHead.length < 2) {
                    alert('Select Budget Head')
                    return;
                }
                //alert(ParamBudgetHead)
                //alert(ParamScheCode)
                //alert($('#ctl00_ContentPlaceHolder1_ddlService').val())
                //alert($('#ctl00_ContentPlaceHolder1_ddldepartment').val())
                var Url = '<%= ResolveUrl("~/GRNHandler.ashx") %>';
                $.ajax({
                    type: "POST",
                    url: Url + "?HandlerVal=" + ParamBudgetHead.substr(0, ParamBudgetHead.length - 1) + "^" + $('#ctl00_ContentPlaceHolder1_ddlService').val() + "^" + $('#ctl00_ContentPlaceHolder1_ddldepartment').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //data: obj,
                    async: true,
                    complete: function () {
                        $.ajax({
                            type: 'POST',
                            url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/editServiceData") %>',
                            data: '{"Parameter":"' + ParamBudgetHead.substr(0, ParamBudgetHead.length - 1) + "^" + $('#ctl00_ContentPlaceHolder1_ddlService').val() + "^" + $('#ctl00_ContentPlaceHolder1_ddldepartment').val() + '"}',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (msg) {
                                alert(msg.d);
                                $(".chzn-select").chosen();
                                $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                                //$('#ctl00_ContentPlaceHolder1_ddldepartment').val("0").trigger("liszt:updated");
                                //$("#ctl00_ContentPlaceHolder1_ddlService").empty();
                                $('#ctl00_ContentPlaceHolder1_ddlService').val("0").trigger("liszt:updated");
                                //$("#ctl00_ContentPlaceHolder1_ddlService").append('<option value=' + '0' + '>' + '--- Select Service ---' + '</option>');
                                $('#btnSubmit').hide();
                                $('#<%=rblType.ClientID %>').find("input[value='0']").prop("checked", true);
                                $('#divServiceName').hide();
                                $("#tdMajorHead").show();
                                $("#tdService").hide();
                                $('#divServiceEdit').hide();
                                $('#divBudgetHeads').hide();
                                $('#txtServiceName').val('')
                            }
                        });
                    }
                });
            });
            $('#btnActive_Deactive').click(function () {
                if ($('#btnActive_Deactive').val() == 'DeActivate')
                    var ActiveFlag = 'false';
                else
                    var ActiveFlag = 'true';
                var Url = '<%= ResolveUrl("~/GRNHandler.ashx") %>';
                $.ajax({
                    type: "POST",
                    url: Url + "?HandlerVal=" + $('#ctl00_ContentPlaceHolder1_ddlService').val() + "^" + $('#ctl00_ContentPlaceHolder1_ddldepartment').val() + "^" + ActiveFlag,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //data: obj,
                    async: true,
                    complete: function () {
                        $.ajax({
                            type: 'POST',
                            url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/ActiveDeactiveServiceData") %>',
                            data: '{"Parameter":"' + $('#ctl00_ContentPlaceHolder1_ddlService').val() + "^" + $('#ctl00_ContentPlaceHolder1_ddldepartment').val() + "^" + ActiveFlag + '"}',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (msg) {
                                alert(msg.d);
                                $(".chzn-select").chosen();
                                $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                                //$('#ctl00_ContentPlaceHolder1_ddldepartment').val("0").trigger("liszt:updated");
                                //$("#ctl00_ContentPlaceHolder1_ddlService").empty();
                                $('#ctl00_ContentPlaceHolder1_ddlService').val("0").trigger("liszt:updated");
                                $('#btnSubmit').hide();
                                $('#<%=rblType.ClientID %>').find("input[value='0']").prop("checked", true);
                                $('#divServiceName').hide();
                                $('#divServiceEdit').hide();
                                $('#divBudgetHeads').hide();
                                $('#txtServiceName').val('');
                            }
                        });
                    }
                });
            });
        });
        function DisplayMajorHeadList(msg) {
            if (msg.d.length > 0 && msg.d != "[]") {
                var json = JSON.parse(msg.d);
                $("#ctl00_ContentPlaceHolder1_ddlMajorHead").empty();
                $("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + '0' + '>' + '--- Select MajorHead ---' + '</option>');
                $.each(json, function (index, obj) {
                    if (this.Count == 0) {
                        $("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + $.trim(this.Majorheadcode) + '>' + $.trim(this.Majorheadname) + '</option>');
                    }
                });
            }
            else {
                alert("No Record found");
            }
        }
        function DisplayServiceList(msg) {
            if (msg.d.length > 0 && msg.d != "[]") {
                var json = JSON.parse(msg.d);
                $("#ctl00_ContentPlaceHolder1_ddlService").empty();
                $("#ctl00_ContentPlaceHolder1_ddlService").append('<option value=' + '0' + '>' + '--- Select Service ---' + '</option>');
                $.each(json, function (index, obj) {
                    $("#ctl00_ContentPlaceHolder1_ddlService").append('<option value=' + $.trim(this.ServiceId) + '>' + $.trim(this.ServiceName) + '</option>');
                });
            }
            else {
                alert("No Record found");
            }
        }
        function DisplayBudgetHeadList_ServiceWise(msg) {
            if (msg.d.length > 3) {
                $('#divBudgetHeads').show();
                $('#divServiceEdit').show();
                $("#tblCustomers").html("");
                $("#tblSelectedHeads").html("");
                var json = JSON.parse(msg.d);
                if (json[0].Active_Deactive == true)
                    $('#btnActive_Deactive').val('DeActivate');
                else
                    $('#btnActive_Deactive').val('Activate');
                var MajorHead = json[0].schemaname.substr(0, 4);
                AjaxCallgetBudgetHeadsList(MajorHead);
                $('#ctl00_ContentPlaceHolder1_hdnServiceNames').val(msg.d);
            }
            else {
                alert("No Record found");
            }
        }
        function AjaxCallgetBudgetHeadsList(MajorHead) {
            var DeptCode = $('#ctl00_ContentPlaceHolder1_ddldepartment').val();
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/getBudgetHeads") %>',
                data: '{"DeptCode":"' + DeptCode + '","MajorHead":"' + MajorHead + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    DisplayBudgetHeads_ServiceWise(msg);
                }
            });
        }
        function DisplayBudgetHeads_ServiceWise(msg) {
            if (msg.d.length > 3) {
                $('#btnSubmit').show();
                $('#divBudgetHeads').show();
                $("#tblCustomers").html("");
                var json = JSON.parse(msg.d);
                var HeadCount = 0;
                $.each(json, function (index, obj) {
                    var row = '<tr id="tr1' + HeadCount + '"><td>' + ' <input type="checkbox" value="' + $.trim(obj.schecode) + '|' + $.trim(obj.Schemaname) + '" id = "cb1' + HeadCount + '"/>' + '</td><td><label>' + $.trim(obj.Schemaname) + '</label></td> </tr>'
                    $("#tblCustomers").append(row);
                    HeadCount = HeadCount + 1;
                });
                CheckHeads_ServiceWise();
            }
            else {
                alert("No Record found");
            }
        }
        function CheckHeads_ServiceWise() {
            var msg = $('#ctl00_ContentPlaceHolder1_hdnServiceNames').val();
            json = JSON.parse(msg);
            $('#tblCustomers > tr').each(function () {
                var cb = $('#' + this.id + ' >td >input').attr('id');
                var tr = '#' + this.id;
                $.each(json, function (index, obj) {
                    if (($('#' + cb).val().split('|', 2)[1]) == $.trim(obj.schemaname)) {
                        $('#' + cb).prop('checked', true);
                        $(tr).children('td, th').css('background-color', '#458B00');
                        $(tr).children('td, th').css('color', '#fff');
                        var row = '<tr id="tr2' + cb.substr(3, cb.length) + '"><td>' + ' <input type="checkbox" style="visibility:hidden" value="' + $.trim(obj.ScheCode) + '|' + $.trim(obj.schemaname) + '" id = "cb2' + cb.substr(3, cb.length) + '" />' + '</td><td>' + $.trim(obj.schemaname) + '</td> </tr>'
                        $("#tblSelectedHeads").append(row);
                        HeadsConditions(true, obj.schemaname, obj.ScheCode)
                    }
                });
            });
        }
        function DisplayBudgetHeadList(msg) {
            if (msg.d.length > 3) {
                var data = msg.d.split('|', 2)
                data1 = $.parseJSON(data[1]);
                var ArrayName = [];
                $.each(data1, function (i, v) {
                    ArrayName.push(this.ServiceName);
                });
                if (ArrayName.length > 0) {
                    $('#ctl00_ContentPlaceHolder1_hdnServiceNames').val(JSON.stringify(ArrayName));
                    $("#txtServiceName").autocomplete({ source: ArrayName });
                }
                $('#btnSubmit').show();
                $('#divServiceName').show();
                $('#divBudgetHeads').show();
                $("#tblCustomers").html("");
                $("#tblSelectedHeads").html("");
                var json = JSON.parse(data[0]);
                var HeadCount = 0;
                $.each(json, function (index, obj) {
                    var row = '<tr id="tr1' + HeadCount + '"><td>' + ' <input type="checkbox" value="' + $.trim(obj.schecode) + '|' + $.trim(obj.Schemaname) + '" id = "cb1' + HeadCount + '"/>' + '</td><td><label>' + $.trim(obj.Schemaname) + '</label></td> </tr>'
                    $("#tblCustomers").append(row);
                    HeadCount = HeadCount + 1;
                });
            }
            else {
                alert("No Record found");
            }
        }

        function AddRemoveItems(ControlId, SchemaName, ScheCode) {
            if ($(ControlId).prop("checked") == true) {
                $("#tr1" + ControlId.substr(4, ControlId.length)).children('td, th').css('background-color', '#458B00');
                $("#tr1" + ControlId.substr(4, ControlId.length)).children('td, th').css('color', '#fff');
                var row = '<tr id="tr2' + ControlId.substr(4, ControlId.length) + '"><td>' + ' <input type="checkbox" style="visibility:hidden" value="' + ScheCode + '|' + SchemaName + '" id = "cb2' + ControlId.substr(4, ControlId.length) + '"  />' + '</td><td>' + SchemaName + '</td> </tr>'
                $("#tblSelectedHeads").append(row);
                HeadsConditions(true, SchemaName, ScheCode)
            }
            else {
                $("#tr1" + ControlId.substr(4, ControlId.length)).children('td, th').css('background-color', '#fff');
                $("#tr1" + ControlId.substr(4, ControlId.length)).children('td, th').css('color', '#000');
                $("#tr2" + ControlId.substr(4, ControlId.length)).remove();
                HeadsConditions(false, SchemaName, ScheCode)
            }
        }
        function HeadsConditions(isChecked, SchemaName, ScheCode) {
            if (isChecked == true) {
                if (SchemaName.split('-', 1) > 8000) {
                    DisableAllUnCheckedHeads();
                }
                var rows = $('#tblSelectedHeads tr').length;
                if (rows == 1) {
                    var ScheGrp = ScheCode.split('-', 2);
                    var GrpCode = ScheGrp[1]
                    DisableOtherGroupCodeHeads(GrpCode);
                }
                if (rows == 9) {
                    DisableAllUnCheckedHeads();
                }
            }
            else {
                var rows = $('#tblSelectedHeads tr').length;
                if (rows == 0) {
                    EnableAllUnCheckedHeads();
                }
                if (rows == 8) {
                    EnableAllUnCheckedHeads();
                    var ScheGrp = ScheCode.split('-', 2);
                    var GrpCode = ScheGrp[1]
                    DisableOtherGroupCodeHeads(GrpCode);
                }
            }
        }
        function DisableOtherGroupCodeHeads(GroupCode) {
            var HeadCnt = 0;
            $('#tblCustomers > tr').each(function () {
                var HeadValue = $("#cb1" + HeadCnt).val().split('|', 2);
                //var SchemaName = HeadValue[1];
                var ScheCode = HeadValue[0];
                var ScheGrp = ScheCode.split('-', 2);
                var GrpCode = ScheGrp[1]
                if (GrpCode != GroupCode) {
                    $("#cb1" + HeadCnt).attr("disabled", true);
                    $("#" + this.id).children('td, th').css('background-color', '#eee');
                }
                HeadCnt = HeadCnt + 1;
            });
        }
        function DisableAllUnCheckedHeads() {
            var HeadCnt = 0;
            $('#tblCustomers > tr').each(function () {
                if ($("#cb1" + HeadCnt).prop("checked") == false) {
                    $("#cb1" + HeadCnt).attr("disabled", true);
                    $("#" + this.id).children('td, th').css('background-color', '#eee');
                }
                HeadCnt = HeadCnt + 1;
            });
        }
        function EnableAllUnCheckedHeads() {
            var HeadCnt = 0;
            $('#tblCustomers > tr').each(function () {
                if ($("#cb1" + HeadCnt).prop("checked") == false) {
                    $("#cb1" + HeadCnt).attr("disabled", false);
                    $("#" + this.id).children('td, th').css('background-color', '#fff');
                }
                HeadCnt = HeadCnt + 1;
            });
        }
    </script>
    <div id="divService" style="width: 1127px">
        <asp:HiddenField ID="hdnServiceNames" runat="server" />
        <fieldset runat="server" style="width: 1000px; margin-left: 100px">
            <span id="spanPD" runat="server">
                <legend style="color: #336699; font-weight: bold">Create Service</legend>
            </span><span id="spanDiv" runat="server"></span>
            <div style="width: 100%; margin-top: 15px" align="center">
                <table style="width: 1015px">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Create New Service" Value="0" Selected="True" />
                                <asp:ListItem Text="Existing Service" Value="1" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td><span style="vertical-align: top">Department Name :-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <asp:DropDownList ID="ddldepartment" runat="server" Width="50%" AutoPostBack="false" class="chzn-select">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Department"
                                ControlToValidate="ddldepartment" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                                Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                        <td id="tdMajorHead"><span style="vertical-align: top">MajorHead:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <asp:DropDownList ID="ddlMajorHead" runat="server" Style="font-family: Verdana !important; font-size: 13px;"
                                Width="50%">
                                <asp:ListItem Value="0" Text="--Select MajorHead--"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select MajorHead"
                                ControlToValidate="ddlMajorHead" ValidationGroup="vldInsert" InitialValue="0"
                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                        <td id="tdService"><span style="vertical-align: top">Service:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <asp:DropDownList ID="ddlService" runat="server" Style="font-family: Verdana !important; font-size: 13px;"
                                Width="50%">
                                <asp:ListItem Value="0" Text="--Select Service--"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Service"
                                ControlToValidate="ddlService" ValidationGroup="vldInsert" InitialValue="0"
                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <br />
                        <td colspan="2">
                            <div id="divBudgetHeads">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <span style="vertical-align: top; color: royalblue; margin-left: 165px; font-size: 13px; font-weight: bold;">All Budget Heads</span>
                                            <div style="overflow: auto; height: 208px; width: 450px; border: 1px solid gray; margin-top: 15px; margin-bottom: 15px;">
                                                <table id="tblCustomers"></table>
                                            </div>
                                        </td>
                                        <td>
                                            <span style="vertical-align: top; color: royalblue; margin-left: 165px; font-size: 13px; font-weight: bold;">Selected Budget Heads</span>
                                            <div style="overflow: auto; height: 208px; width: 450px; border: 1px solid gray; margin-top: 15px; margin-bottom: 15px;">
                                                <table id="tblSelectedHeads"></table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divServiceName">
                                <label style="vertical-align: top" for="txtServiceName">Service Name:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                <input type="text" id="txtServiceName" />
                                <input type="button" id="btnSubmit" style="margin-left: 65px; width: 89px;" value="Submit" />
                            </div>
                            <div id="divServiceEdit">
                                <input type="button" id="btnEdit" style="margin-left: 65px; width: 90px;" value="Edit" />&nbsp&nbsp&nbsp&nbsp
                                <input type="button" id="btnActive_Deactive" style="margin-left: 65px; width: 90px;" value="DEACTIVE" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>

