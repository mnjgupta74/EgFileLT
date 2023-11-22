<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgEChallanEditDivCode.aspx.cs" Inherits="WebPages_EgEChallanEditDivCode_temp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />    
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
     <link href="../../js/SweetAlert/sweetalert.css" rel="stylesheet" />
    <script src="../../js/SweetAlert/sweetalert.min.js"></script>
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
    </style>
    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () { HideTRowGRN(); });

        function NumericOnly(el) {
            var ex = /^\s*\d+\s*$/;
            if (ex.test(el.value) == false) {
                alert('Enter Number Only');
                el.value = "";
            }
        }
        function NumberOnly(ctrl) {
            var ch;

            if (window.event) {
                ch = ctrl.keyCode;
            }
            else if (ctrl.which) {
                ch = ctrl.which;
            }
            if ((ch >= 48 && ch <= 57))
                return true;

            else
                return false;
        }
        function myAlert(heading, mycontent) {
            //swal(heading, mycontent);
            swal({
                title: heading,
                text: mycontent,
                button: "Close",
            });
        }
        function HideTRowGRN() {
            $('#<%=tRowGRN.ClientID%>').hide();
            var someval = $('input:radio:checked').val()
            if (someval == 3) {
                $('#<%=tRowMajorHead.ClientID%>').hide();
                $('#<%=tRowMajorHead1.ClientID%>').hide();
                $('#<%=tRowGRN.ClientID%>').show();
                $('#<%=trrpt.ClientID%>').show();
                    <%--$('#<%=txtmajorHead.ClientID%>').val("");
                    $('#<%=txtFromDate.ClientID%>').val("");
                    $('#<%=txtToDate.ClientID%>').val("");
                    $('#<%=divcode.ClientID%>').val("0");--%>
            }
            else {
                $('#<%=tRowMajorHead.ClientID%>').show();
                $('#<%=tRowMajorHead1.ClientID%>').show();
                $('#<%=tRowGRN.ClientID%>').hide();
                $('#<%=trrpt.ClientID%>').show();
                    <%--$('#<%=txtmajorHead.ClientID%>').val("");
                    $('#<%=txtFromDate.ClientID%>').val("");
                    $('#<%=txtToDate.ClientID%>').val("");
                    $('#<%=divcode.ClientID%>').val("0");--%>
            }
        }

        //$(function () {
        function onChangeRdbtn() {
            $('input:radio').change(function () {
                var someval = $('input:radio:checked').val()
                if (someval == 3) {
                    $('#<%=tRowMajorHead.ClientID%>').hide();
                    $('#<%=tRowMajorHead1.ClientID%>').hide();
                    $('#<%=tRowGRN.ClientID%>').show();
                    $('#<%=trrpt.ClientID%>').hide();
                    <%--$('#<%=txtmajorHead.ClientID%>').val("");
                    $('#<%=txtFromDate.ClientID%>').val("");
                    $('#<%=txtToDate.ClientID%>').val("");
                    $('#<%=divcode.ClientID%>').val("0");--%>
                }
                else {
                    $('#<%=tRowMajorHead.ClientID%>').show();
                    $('#<%=tRowMajorHead1.ClientID%>').show();
                    $('#<%=tRowGRN.ClientID%>').hide();
                    $('#<%=trrpt.ClientID%>').hide();
                    <%--$('#<%=txtmajorHead.ClientID%>').val("");
                    $('#<%=txtFromDate.ClientID%>').val("");
                    $('#<%=txtToDate.ClientID%>').val("");
                    $('#<%=divcode.ClientID%>').val("0");--%>
                }
            });
        };

        //for txtFromDate text box
        function dateValidation() {

            var dtObj = document.getElementById("<%=txtFromDate.ClientID %>")

            var dtStr = dtObj.value
            var dtTemp = dtStr

            if (dtTemp == '') {
                alert('Date cant be blank')
                dtObj.value = ""
                return false
            }
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            //check for parts of date
            var DayDt
            var MonDt
            var YearDt

            dtTemp = dtStr
            DayDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            MonDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            YearDt = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt) || isNaN(MonDt) || isNaN(YearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt, parseInt(MonDt - 1), DayDt)


            if (DateEntered.getMonth() != (parseInt(MonDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }

            var str2 = new Date();
            var s = str2.format("dd/MM/yyyy");
            var dt1 = parseInt(dtStr.substring(0, 2), 10);
            var mon1 = parseInt(dtStr.substring(3, 5), 10);
            var yr1 = parseInt(dtStr.substring(6, 10), 10);
            var dt2 = parseInt(s.substring(0, 2), 10);
            var mon2 = parseInt(s.substring(3, 5), 10);
            var yr2 = parseInt(s.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1);
            var date2 = new Date(yr2, mon2, dt2);
            if (date2 < date1) {
                alert("To date cannot be greater than from current date");
                dtObj.value = ""
                return false
            }
        }


        //txtTo Date textbox

        function dateValidation1() {
            var dtObj = document.getElementById("<%=txtToDate.ClientID %>")

            var dtStr = dtObj.value
            var dtTemp = dtStr

            if (dtStr == '') {
                alert('Date cant be blank')
                dtObj.value = ""
                return false
            }
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            //check for parts of date
            var DayDt
            var MonDt
            var YearDt

            dtTemp = dtStr
            DayDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            MonDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            YearDt = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt) || isNaN(MonDt) || isNaN(YearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt, parseInt(MonDt - 1), DayDt)


            if (DateEntered.getMonth() != (parseInt(MonDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }


            var fdate = document.getElementById("<%=txtFromDate.ClientID %>")
            var tdate = document.getElementById("<%=txtToDate.ClientID %>")
            var dtfdate = fdate.value
            var dttdate = tdate.value


            var dt1 = parseInt(dtfdate.substring(0, 2), 10);
            var mon1 = parseInt(dtfdate.substring(3, 5), 10);
            var yr1 = parseInt(dtfdate.substring(6, 10), 10);

            var dt2 = parseInt(dttdate.substring(0, 2), 10);
            var mon2 = parseInt(dttdate.substring(3, 5), 10);
            var yr2 = parseInt(dttdate.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1);
            var date2 = new Date(yr2, mon2, dt2);
            if (date2 < date1) {
                alert("from date cannot be greater than to date");
            }
        }

        <%-- function onFocus() {
            var txtBox = document.getElementById('<%=txtmajorHead.ClientID %>');
            if (txtBox.value == "0000") {
                txtBox.value = "";
                txtBox.style.backgroundColor = "white";
                txtBox.style.color = "Black"
            }
        }
        function onFoucsOut() {
            var txtBox = document.getElementById('<%=txtmajorHead.ClientID %>');
            if (txtBox.value == "") {
                txtBox.value = "0000";
                txtBox.style.backgroundColor = "Azure";
                txtBox.style.color = "Silver";
            }--%>
        
    </script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../Image/waiting_process.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(onChangeRdbtn);
                Sys.Application.add_load(HideTRowGRN);
            </script>
            <div class="">
                <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
                    <h1 _ngcontent-c6="" title="BunchChallan">
                        <span _ngcontent-c6="" style="color: #FFF">Budget Head Wise Revenue</span></h1>
                </div>
                <fieldset runat="server" id="lstrecord" style="width: 1000px;">
                    <table align="center" id="MainTable" style="width: 100%;" cellpadding="10" cellspacing="10">
                        <tr>
                            <td colspan="4" align="center">
                                <asp:RadioButtonList runat="server" ID="rbtnList" Width="550px" RepeatDirection="Horizontal" ForeColor="#336699"
                                    Style="display: contents !important" CssClass="form-control">
                                    <asp:ListItem Text="Paired " Value="1" Selected="True" style="margin-right: 25px" />
                                    <asp:ListItem Text="UnPaired " Value="2" style="margin-right: 25px" />
                                    <asp:ListItem Text="0040 " Value="3" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr runat="server" id="tRowMajorHead">
                            <td align="left">
                                <b><span style="color: #336699">MajorHead:-</span></b>&nbsp;
                    <asp:TextBox runat="server" ID="txtmajorHead" PlaceHolder="0000" MaxLength="4" Width="120px" Height="30px" CssClass="form-control" Style="display:inline-flex"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtmajorHead"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>
                            <td  align="left">
                                <b><span style="color: #336699">DivCode:-</span></b>&nbsp;
                    <asp:DropDownList ID="divcode" runat="server" Width="100%" ValidationGroup="de" Height="30px" CssClass="form-control" Style="display:inline-flex;margin-top:10px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="divcode" InitialValue="0|0" ValidationGroup="de" SetFocusOnError="true" ErrorMessage="Please select div code" />
                            </td>
                            <td  align="left">
                                <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtFromDate" runat="server" Width="120px"  CssClass="form-control"  Height="30px" Style="display:inline-flex" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation()"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                                    Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                    CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>
                            <td  align="left">
                                <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtToDate" runat="server" Width="120px"  CssClass="form-control"  Height="30px"  Style="display:inline-flex" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation1()"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                                    Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>
                            
                        </tr>
                        <tr id="tRowMajorHead1" runat="server">
                            <td align="right" colspan="3">
                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-default"  Height="33px" ValidationGroup="de" size="25" Text="Show" OnClientClick="HideTRowGRN()" OnClick="btnSubmit_Click" />
                            </td>
                            <td align="right" colspan="1" style="width: 100px;" >
                                <asp:Button ID="btnPdfgenerate" runat="server" CssClass="btn btn-default"  Height="33px" Width="130px" Text="ReportGenerate" OnClick="btnPdfgenerate_Click" />
                            </td>
                        </tr>
                        <tr style="height: 45px" runat="server" id="tRowGRN">
                            <td align="center" colspan="2">
                                <b><span style="color: #336699">GRN:-</span></b>&nbsp;
                    <asp:TextBox runat="server" ID="txtGRNSearch" CssClass="form-control" Height="30px" style="display:inline-flex" MaxLength="12" Width="140px" onChange="NumericOnly(this);" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtGRNSearch"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ge"></asp:RequiredFieldValidator>
                            </td>
                           
                           
                            <td align="center" colspan="2">
                                <asp:Button ID="btnShowGRNSearch" runat="server" CssClass="btn btn-default" Height="33px" ValidationGroup="ge" size="25" Text="Show" OnClientClick="onChangeRdbtn()" OnClick="btnSubmit_Click" Width="97px" /></td>
                        </tr>
                        <tr align="center" id="trrpt" runat="server">
                            <td colspan="4">
                                <asp:Repeater ID="rptrManualSuccess" OnItemCommand="rptrManualSuccess_ItemCommand" OnItemDataBound="rptrManualSuccess_ItemDataBound" runat="server">
                                    <HeaderTemplate>
                                        <table border="1" width="100%" cellpadding="0" cellspacing="0">
                                            <tr style="background-color: #337ab7; color: White; font-weight: bold; height: 20px">
                                                <th>
                                                    <b>S.No</b>
                                                </th>
                                                <th>
                                                    <b>GRN</b>
                                                </th>
                                                <th>
                                                    <b>Office Name</b>
                                                </th>
                                                <th>
                                                    <b>Challan No</b>
                                                </th>
                                                <th>
                                                    <b>Challan Date</b>
                                                </th>
                                                <th>
                                                    <b>Amount</b>
                                                </th>
                                                <th>
                                                    <b>Div Code</b>
                                                </th>
                                                <th>
                                                    <b>Edit</b>
                                                </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center">
                                                <%# Container.ItemIndex+1 %>
                                            </td>
                                            <td align="center">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Status"
                                                    CommandArgument='<%# Eval("Grn") %>' Text='<%# Eval("Grn") %>'></asp:LinkButton>
                                            </td>
                                            <td align="center">
                                                <%# DataBinder.Eval(Container.DataItem, "officename")%>
                                            </td>
                                            <td align="center">
                                                <%# DataBinder.Eval(Container.DataItem, "Challanno")%>
                                            </td>
                                            <td align="center">
                                                <%# DataBinder.Eval(Container.DataItem, "BankChallanDate")%>
                                            </td>
                                            <td align="right">
                                                <%# DataBinder.Eval(Container.DataItem, "Amount", "{0:0.00}")%>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text='<%# Eval("DivCode") %>' ID="lblDivCode" runat="server" />
                                                <asp:DropDownList runat="server" Enabled="false" Visible="false" ID="ddlDivCode">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="display: none">
                                                <asp:Label Text='<%# Eval("MerchantGrn") %>' ID="lblMerchantGrn" runat="server" Visible="false" />
                                            </td>
                                            <td align="center">
                                                <asp:LinkButton ID="lbledit" runat="server"   CommandName="Edit" CommandArgument='<%# Eval("Grn") %>'
                                                    Text="Edit"></asp:LinkButton>

                                               <%-- <asp:LinkButton ID="lblEdit" runat="server" CommandName="Edit" CausesValidation="true" CommandArgument='<%# Eval("Grn") %>'
                                                    Text='<%# bool.Parse(Eval("Status").ToString()) == true ? "" : "Edit"%>'>
                                                </asp:LinkButton>--%>
                                                <%--<%# bool.Parse(Eval("Status").ToString()) == true ? "<img src='img/active.png' title='Make Hide' border='0'/>" : "<img src='img/deactive.png' title='Make Show' border='0'/>"%>--%>

                                                <%--<asp:LinkButton ID="lblEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument='<%# Eval("Grn") %>'
                                       Text= '<%# bool.Parse(Eval("Status").ToString()) == true ? "" : "Edit"%>'>
                                    </asp:LinkButton>--%>

                                                <asp:LinkButton ID="lnkCancel" Visible="false" runat="server" CausesValidation="false" CommandName="Cancel"
                                                    Text="Cancel"></asp:LinkButton>


                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%--<asp:Label ID="lblDefaultMessage" runat="server" Text="Sorry, No Record Found." Visible="false">
                            </asp:Label>--%>
                            </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>

                        </tr>
                        <tr runat="server" id="tr3">
                            <td colspan="4">
                                <center>
                                    <asp:Label ID="lblDefaultMessage" runat="server" Text="Sorry, No Record Found." Style="color: firebrick; font-weight: bold" Visible="false">
                                    </asp:Label>
                                </center>
                            </td>
                        </tr>
                        <tr runat="server" id="tr1" visible="false">
                            <td colspan="4">
                                <center>
                                    <rsweb:ReportViewer ID="rptManualSuccessDivisionWiserpt" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false"></rsweb:ReportViewer>
                                </center>
                            </td>
                        </tr>
                        <tr runat="server" id="tr2" visible="false">


                            <td colspan="4">
                                <center>
                                    <rsweb:ReportViewer ID="SSRSreport" runat="server" AsyncRendering="false" Width="70%" Height="800PX">
                                    </rsweb:ReportViewer>
                                </center>
                            </td>

                        </tr>
                    </table>
                </fieldset>
        </ContentTemplate>

        <Triggers>
            <%--            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnShowGRNSearch" />--%>
            <%--<asp:PostBackTrigger ControlID="btnShowGRNSearch" />--%>
            <asp:PostBackTrigger ControlID="btnPdfgenerate" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

