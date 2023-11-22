<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="PDActiveDeactive.aspx.cs" Inherits="WebPages_TO_PDActiveDeactive" %>

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


            $("#ctl00_ContentPlaceHolder1_ddlTreasury").change(function () {
                $('#tblCustomers').html('');
                $('#tblSelectedHeads').html('');
                $('#divBudgetHeads').hide();
                $('#btnSubmit').hide();
                $('#btnReset').hide();
            });
            $('#btnGo').click(function () {
                var TreasuryCode = $('#ctl00_ContentPlaceHolder1_ddlTreasury').val();
                var BudgetHead = $('#<%= txtBudgetHead.ClientID %>').val();
                //alert(TreasuryCode);
                //alert(BudgetHead);

                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/To/PDActiveDeactive.aspx/getPdAccountList") %>',
                    data: '{"TreasuryCode":"' + TreasuryCode + '","BudgetHead":"' + BudgetHead + '"}',
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
            //-----SubmitButton_Click_START
            $('#btnSubmit').click(function () {
                var HeadCnt = 0;
                var ParamPD = "";
                $('#tblCustomers > tr').each(function () {
                    if ($("#cb1" + HeadCnt).prop("checked") == true) {

                        var HeadValue = $("#cb1" + HeadCnt).val().split('|', 2);
                        var PDID = HeadValue[0];
                        var PDName = PDID.toString().split("-", 1);
                        ParamPD = PDName + '|' + ParamPD;

                    }
                    HeadCnt = HeadCnt + 1;
                });
                var type = $('#<%=rblType.ClientID %> input:checked').val()
                //var BudgetHead = $('#<%= txtBudgetHead.ClientID %>').val();

                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/To/PDActiveDeactive.aspx/UpdateHeadData") %>',
                    data: '{"Parameter":"' + ParamPD.substr(0, ParamPD.length - 1) + "^" + type + "^" + $('#ctl00_ContentPlaceHolder1_ddlTreasury').val() + "^" + $('#<%= txtBudgetHead.ClientID %>').val() + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        alert(msg.d);
                        $(".chzn-select").chosen();
                        $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                        //$('#ctl00_ContentPlaceHolder1_ddldepartment').val("0").trigger("liszt:updated");
                        //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").empty();
                        //$('#ctl00_ContentPlaceHolder1_ddlMajorHead').val("0").trigger("liszt:updated");
                        $('#<%= txtBudgetHead.ClientID %>').empty().trigger("liszt:updated");
                                //$("#ctl00_ContentPlaceHolder1_ddlMajorHead").append('<option value=' + '0' + '>' + '--- Select MajorHead ---' + '</option>');
                                $('#btnSubmit').hide();
                                $('#btnReset').hide();
                                $('#<%=rblType.ClientID %>').find("input[value='Y']").prop("checked", true);
                                $("input[type=radio]").attr('disabled', false);
                                $('#divBudgetHeads').hide();
                            }
                });
            });
            //-------SubmitButtonEND--
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
                    if (obj.PDFlag == 'Y')
                        var col = 'yellowgreen';
                    else
                        var col = 'peachpuff';
                    if (obj.PDFlag == $('#<%=rblType.ClientID %> input:checked').val())
                        var dis = 'disabled="disabled" title="Head cannot be selected"';
                    else
                        var dis = ""
                    var row = '<tr id="tr1' + obj.PDAccName + '" style="background-color:' + col + '"><td>' + ' <input type="checkbox" ' + dis + '  value="' + obj.PDAccName + '|' + obj.PDFlag + ' " id = "cb1' + HeadCount + '"/>' + '</td><td><label>' + obj.PDAccName + '</label></td> </tr>'
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
            //188-Kusmiya Primery
            //alert(SchemaName);
            //var newPD = SchemaName.toString().split("-",1);
            if ($(ControlId).prop("checked") == true) {

                var row = '<tr id="tr2' + SchemaName.toString().split("-", 1) + '"><td>' + '<input type="checkbox" style="visibility:hidden" value="' + SchemaName + '" id = "cb2" />' + '</td><td>' + SchemaName + '</td> </tr>'
                $("#tblSelectedHeads").append(row);
                HeadsConditions(true)
            }
            else {
                $("#tr2" + SchemaName.toString().split("-", 1)).remove();
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
        function CheckMajorHeadlength() {
            var Mvalue = document.getElementById("<%= txtBudgetHead.ClientID %>").value
        if (Mvalue.length < 1) {
            alert('Please Enter BudgetHead/MajorHead.!');
            document.getElementById("<%= txtBudgetHead.ClientID %>").value = "";
            }

        }
    </script>
    <div id="divService" style="width: 1127px">
        <fieldset runat="server" style="width: 1000px; margin-left: 45px">
            <span id="spanPD" runat="server">
                <legend style="color: #336699; font-weight: bold">PD Active/Deactive</legend>
            </span><span id="spanDiv" runat="server"></span>
            <div style="width: 100%; margin-top: 15px" align="center">
                <table style="width: 1015px">
                    <tr>
                        <td style="padding-bottom: 20px;">
                            <asp:RadioButtonList runat="server" ID="rblType" RepeatDirection="Horizontal">
                                <%--<asp:ListItem Text="Regular Challan" Value="Y" Selected="True" />--%>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%"><span style="vertical-align: top">Treasury Name :-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <asp:DropDownList ID="ddlTreasury" runat="server" Width="50%" AutoPostBack="false" class="chzn-select">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Department"
                                ControlToValidate="ddlTreasury" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                                Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 50%"><span style="vertical-align: top">BudgetHead:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <asp:TextBox ID="txtBudgetHead" runat="server" MaxLength="13" onblur="javascript:CheckMajorHeadlength()"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="9999-99-999-99-99"
                                MaskType="None" CultureName="en-US" TargetControlID="txtBudgetHead" AcceptNegative="None"
                                runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                            <input type="button" value="Go" id="btnGo" style="width: 60px; margin-left: 20px" />

                        </td>
                    </tr>
                    <tr>
                        <br />
                        <td colspan="2">
                            <div id="divBudgetHeads">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <span style="vertical-align: top; color: royalblue; margin-left: 165px; font-size: 13px; font-weight: bold;">All PD</span>
                                            <div style="overflow: auto; height: 208px; width: 450px; border: 1px solid gray; margin-top: 15px; margin-bottom: 15px;">
                                                <table id="tblCustomers"></table>
                                            </div>
                                        </td>
                                        <td>
                                            <span style="vertical-align: top; color: royalblue; margin-left: 165px; font-size: 13px; font-weight: bold;">Selected PD</span>
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

