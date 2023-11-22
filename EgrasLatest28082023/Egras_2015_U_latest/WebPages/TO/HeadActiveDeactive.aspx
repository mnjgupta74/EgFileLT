<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="HeadActiveDeactive.aspx.cs" Inherits="WebPages_TO_HeadActiveDeactive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <script type="text/javascript" src="../../Scripts/chosen.jquery.js"></script>
    <link href="../../CSS/chosen.css" rel="stylesheet" />
    <link href="../../js/jquery-ui.css" rel="stylesheet" />
    <script src="../../js/jquery-ui.js"></script>--%>
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
            $("input[type=radio]").attr('disabled', false);
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('#btnSubmit').hide();
            $('#btnReset').hide();
            
            $('#divBudgetHeads').hide();


            $("#ctl00_ContentPlaceHolder1_ddldepartment").change(function () {

                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/To/HeadActiveDeactive.aspx/getMajorHeadList") %>',
                    data: '{"DeptCode":"' + this.value + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        DisplayMajorHeadList(msg);
                    }
                });
                $('#ctl00_ContentPlaceHolder1_ddlMajorHead').val("0").trigger("liszt:updated");
                $('#tblCustomers').html('');
                $('#tblSelectedHeads').html('');
                $('#divBudgetHeads').hide();
                $('#btnSubmit').hide();
                $('#btnReset').hide();
            });
            $("#ctl00_ContentPlaceHolder1_ddlMajorHead").change(function () {
                var DeptCode = $('#ctl00_ContentPlaceHolder1_ddldepartment').val();
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/To/HeadActiveDeactive.aspx/getBudgetHeadList") %>',
                    data: '{"DeptCode":"' + DeptCode + '","MajorHead":"' + this.value + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        DisplayBudgetHeadList(msg);
                    }
                });
            });
            $('#tblCustomers').on('click', 'input', function () {
                //var SelectedValue = $("#" + this.id).val().split('|', 1);
                //var SchemaName = SelectedValue[0];
                var SchemaName = $("#" + this.id).val().split('|', 1);
                
                AddRemoveItems("#" + this.id, SchemaName);
            });
            $('#btnSubmit').click(function () {
                var HeadCnt = 0;
                var ParamBudgetHead = "";
                $('#tblCustomers > tr').each(function () {
                    if ($("#cb1" + HeadCnt).prop("checked") == true) {
                        debugger;
                        var HeadValue = $("#cb1" + HeadCnt).val().split('|', 2);
                        var SchemaName = HeadValue[0];
                        var BudgetHeadName = SchemaName.replace(/-/g, '').substr(0, 13);
                        ParamBudgetHead = BudgetHeadName + '|' + ParamBudgetHead;
                        alert(ParamBudgetHead);
                    }
                    HeadCnt = HeadCnt + 1;
                });
                if (ParamBudgetHead.length < 2) {
                    alert('Select Budget Head')
                    return;
                }
                var type = $('#<%=rblType.ClientID %> input:checked').val()
                var Url = '<%= ResolveUrl("~/GRNHandler.ashx") %>';
                $.ajax({
                    type: "POST",
                    url: Url + "?HandlerVal=" + ParamBudgetHead.substr(0, ParamBudgetHead.length - 1) + "^" + type + "^" + $('#ctl00_ContentPlaceHolder1_ddldepartment').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //data: obj,
                    async: true,
                    complete: function () {
                        $.ajax({
                            type: 'POST',
                            url: '<%= ResolveUrl("~/WebPages/To/HeadActiveDeactive.aspx/UpdateHeadData") %>',
                            data: '{"Parameter":"' + ParamBudgetHead.substr(0, ParamBudgetHead.length - 1) + "^" + type + "^" + $('#ctl00_ContentPlaceHolder1_ddldepartment').val() + '"}',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (msg) {
                                alert(msg.d);
                                $(".chzn-select").chosen();
                                $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                                //$('#ctl00_ContentPlaceHolder1_ddldepartment').val("0").trigger("liszt:updated");
                                //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").empty();
                                $('#ctl00_ContentPlaceHolder1_ddlMajorHead').val("0").trigger("liszt:updated");
                                //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + '0' + '>' + '--- Select MajorHead ---' + '</option>');
                                $('#btnSubmit').hide();
                                $('#btnReset').hide();
                                $('#<%=rblType.ClientID %>').find("input[value='Y']").prop("checked", true);
                                $("input[type=radio]").attr('disabled', false);
                                $('#divBudgetHeads').hide();
                            }
                        });
                    }
                });
            });
            $('#btnReset').click(function () {
                $(".chzn-select").chosen();
                $("input[type=radio]").attr('disabled', false);
                $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                //$('#ctl00_ContentPlaceHolder1_ddldepartment').val("0").trigger("liszt:updated");
                //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").empty();
                //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + '0' + '>' + '--- Select MajorHead ---' + '</option>');
                $('#ctl00_ContentPlaceHolder1_ddlMajorHead').val("0").trigger("liszt:updated");
                $('#btnSubmit').hide();
                $('#btnReset').hide();
                $('#divBudgetHeads').hide();
                $('#<%=rblType.ClientID %>').find("input[value='Y']").prop("checked", true);
            });
        });
        function DisplayMajorHeadList(msg) {
            if (msg.d.length > 0 && msg.d != "[]") {
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
        function DisplayBudgetHeadList(msg) {
            if (msg.d.length > 3) {
                $('#btnReset').show();
                $("input[type=radio]").attr('disabled', true);
                $('#divBudgetHeads').show();
                $("#tblCustomers").html("");
                $("#tblSelectedHeads").html("");
                var json = JSON.parse(msg.d);
                var HeadCount = 0;
                $.each(json, function (index, obj) {
                    if (obj.Flag == 'Y')
                        var col = 'yellowgreen';
                    else
                        var col = 'peachpuff';
                    if (obj.Flag == $('#<%=rblType.ClientID %> input:checked').val())
                    var dis = 'disabled="disabled" title="Head cannot be selected"';
                else
                    var dis = ""
                var row = '<tr id="tr1' + obj.SchemaName + '" style="background-color:' + col + '"><td>' + ' <input type="checkbox" ' + dis + '  value="' + obj.SchemaName + '|' + obj.Flag + ' " id = "cb1' + HeadCount + '"/>' + '</td><td><label>' + obj.SchemaName + '</label></td> </tr>'
                $("#tblCustomers").append(row);
                HeadCount = HeadCount + 1;
            });
        }
        else {
            $("input[type=radio]").attr('disabled', false);
            $("#tblCustomers").html("");
            $("#tblSelectedHeads").html("");
            alert("No Record found");
        }
    }
        function AddRemoveItems(ControlId, SchemaName) {
            

        if ($(ControlId).prop("checked") == true) {
            var row = '<tr id="tr2' + SchemaName.toString().replace(/-/g, '').substr(0, 13) + '"><td>' + '<input type="checkbox" style="visibility:hidden" value="' + SchemaName + '" id = "cb2" />' + '</td><td>' + SchemaName + '</td> </tr>'
            $("#tblSelectedHeads").append(row);
            HeadsConditions(true)
        }
        else {
           
            $("#tr2" + SchemaName.toString().replace(/-/g, '').substr(0, 13)).remove();
            HeadsConditions(false)
        }
    }
    function HeadsConditions(ischecked) {
        if (ischecked == true) {
            var rows = $('#tblSelectedHeads tr').length;
            $('#btnSubmit').show();
            
            if (rows == 9) {
                DisableAllUnCheckedHeads();
            }
        }
        else {
            var rows = $('#tblSelectedHeads tr').length;
            if (rows == 0) {
                $('#btnSubmit').hide();
                
                EnableAllUnCheckedHeads();
            }
            if (rows == 8) {
                EnableAllUnCheckedHeads();
            }
        }
    }
    function DisableAllUnCheckedHeads() {
        var HeadCnt = 0;
        $('#tblCustomers > tr').each(function () {
            if ($("#cb1" + HeadCnt).prop("checked") == false) {
                $("#cb1" + HeadCnt).attr("disabled", true);
            }
            HeadCnt = HeadCnt + 1;
        });
    }
    function EnableAllUnCheckedHeads() {
        var HeadCnt = 0;
        $('#tblCustomers > tr').each(function () {
            if ($("#cb1" + HeadCnt).prop("checked") == false) {

                var CBVal = $("#cb1" + HeadCnt).val().split('|', 3)
                if ($.trim(CBVal[2]) == $.trim($('#<%=rblType.ClientID %> input:checked').val())) {
                    $("#cb1" + HeadCnt).attr("disabled", true);
                }                
            }
            else {
                $("#cb1" + HeadCnt).attr("disabled", false);
            }
            HeadCnt = HeadCnt + 1;
        });
    }
    </script>
    <div id="divService" style="width: 1127px">
        <fieldset runat="server" style="width: 1000px; margin-left: 100px">
            <span id="spanPD" runat="server">
                <legend style="color: #336699; font-weight: bold">Head Active/Deactive</legend>
            </span><span id="spanDiv" runat="server"></span>
            <div style="width: 100%; margin-top: 15px" align="center">
                <table style="width: 1015px">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblType" RepeatDirection="Horizontal">
                                <%--<asp:ListItem Text="Regular Challan" Value="Y" Selected="True" />--%>
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
                        <td><span style="vertical-align: top">MajorHead:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <asp:DropDownList ID="ddlMajorHead" runat="server" Style="font-family: Verdana !important; font-size: 13px;"
                                Width="50%">
                                <asp:ListItem Value="0" Text="--Select MajorHead--"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select MajorHead"
                                ControlToValidate="ddlMajorHead" ValidationGroup="vldInsert" InitialValue="0"
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
                            <input type="button" id="btnSubmit" style="margin-left: 65px" value="Submit" />
                            <input type="button" id="btnReset" style="margin-left: 65px" value="Reset" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
