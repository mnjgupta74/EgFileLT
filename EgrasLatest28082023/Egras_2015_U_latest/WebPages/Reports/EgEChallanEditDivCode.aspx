<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgEChallanEditDivCode.aspx.cs" Inherits="WebPages_EgEChallanEditDivCode_temp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/UserControls/CommonFromDateToDate.ascx" TagName="FromToDate" TagPrefix="ucl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        function HideTRowGRN() {
            $('#<%=tRowGRN.ClientID%>').hide(); 
            var someval = $('input:radio:checked').val()
            if (someval == 3) {
                    $('#<%=tRowMajorHead.ClientID%>').hide();
                    $('#<%=tRowGRN.ClientID%>').show();
                    $('#<%=trrpt.ClientID%>').show();
                   
                }
                else {
                    $('#<%=tRowMajorHead.ClientID%>').show();
                    $('#<%=tRowGRN.ClientID%>').hide();
                    $('#<%=trrpt.ClientID%>').show();
                   
                }
        }
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
        function checkKeyCode(evt)// for F5 disable
        {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (event.keyCode == 116)//disable F5
            {
                evt.keyCode = 0;
                return false
            }
            if (event.keyCode == 123) {
                evt.keyCode = 0;
                return false
            }
            if (event.keyCode == 93) {
                evt.keyCode = 0;
                return false
            }
            if (event.altKey == true && event.keyCode == 115) //disable alt-F4
            {
                evt.keyCode = 0;
                return false
            }
        }
        document.onkeydown = checkKeyCode;
        function rightClickCheck(keyp) {
            var message = "Function Disable";
            if (navigator.appName == "Netscape" && keyp.which == 3) {
                alert(message); return false;
            }
            if (navigator.appVersion.indexOf("MSIE") != -1 && event.button == 2) {
                alert(message);
                return false;
            }
        }
        document.onmousedown = rightClickCheck;
        function setFrame() {
            contentFrame.location.href = "http://www.microsoft.com/downloads/en/details.aspx?familyid=9AE91EBE-3385-447C-8A30-081805B2F90B&amp;displaylang=en";
        }
      
        function onChangeRdbtn() {
            $('input:radio').change(function () {
                var someval = $('input:radio:checked').val()
                if (someval == 3) {
                    $('#<%=tRowMajorHead.ClientID%>').hide();
                    $('#<%=todates.ClientID%>').hide();
                    $('#<%=tRowGRN.ClientID%>').show();
                    $('#<%=trrpt.ClientID%>').hide();
                }
                else {
                    $('#<%=tRowMajorHead.ClientID%>').show();
                    $('#<%=todates.ClientID%>').show();
                    $('#<%=tRowGRN.ClientID%>').hide();
                    $('#<%=trrpt.ClientID%>').hide();
                }
            });
        };

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
            <fieldset runat="server" id="lstrecord" style="width: 1000px;">
                <legend style="color: #336699; font-weight: bold">EChallan Division Edit</legend>
                <table style="width: 101%" align="center" id="MainTable">
                    <tr style="height: 50px">
                        <td colspan="4" style="padding-left: 250px;">
                            <asp:RadioButtonList runat="server" ID="rbtnList" Width="550px" RepeatDirection="Horizontal" ForeColor="#336699">
                                <asp:ListItem Text="Paired " Value="1" Selected="True" style="margin-right: 15px" />
                                <asp:ListItem Text="UnPaired " Value="2" />
                                <asp:ListItem Text="0040 " Value="3" />
                            </asp:RadioButtonList>

                        </td>
                    </tr>
                    <tr align="center" style="height: 45px; " runat="server" id="tRowMajorHead">
                        
                        <td align="center" style="width:60%; padding-left:10%">
                            <b><span style="color: #336699">MajorHead:-</span></b>&nbsp;
                    <asp:TextBox runat="server" ID="txtmajorHead"   PlaceHolder="0000"  MaxLength="4" Width="80px"  />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtmajorHead"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>

                            &nbsp; &nbsp; &nbsp; 
                              <b><span style="color: #336699">Treasury :</span></b>&nbsp;
                                        <asp:DropDownList ID="ddlTreasury" runat="server" OnSelectedIndexChanged="ddlTreasury_SelectedIndexChanged" AutoPostBack="true" Width="150px">
                                       </asp:DropDownList>

                        </td>
                        
                        <td align="left" style="width:40%; padding-left:3%;">
                            <b><span style="color: #336699">DivCode:-</span></b>&nbsp;
                    <asp:DropDownList ID="divcode" runat="server"  Width="120px" ValidationGroup="de"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="divcode" InitialValue="0|0" ValidationGroup="de"  SetFocusOnError="true" ErrorMessage="Please select div code" />
                        </td>                    
                         </tr>
                      <tr align="center" runat="server" id="todates">
                       <%-- <td style="width:3%"></td>
                        <td align="center" colspan="1">
                            <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation()"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                                Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                                 
                            <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtToDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation1()"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                                Format="dd/MM/yyyy" TargetControlID="txtToDate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>--%>
                          <td style=" width:60%;padding-left:9%">
                               <ucl:FromToDate ID="frmdatetodate" runat="server" Width="100px" Height="22px" CssClass="borderRadius " />
                          </td>
                          <td align="left" style="width:40%; padding-left:3%;"> 
                            <asp:Button ID="btnShow" runat="server" ValidationGroup="de" size="25" Text="Show" OnClientClick="HideTRowGRN()" OnClick="btnSubmit_Click" Width="80px" />
                              &nbsp;&nbsp;
                               <asp:Button ID="btnPdfgenerate" runat="server" Text="ReportGenerate" OnClick="btnPdfgenerate_Click" />
                        </td>
                     
                                 
                     </tr>
                    <tr style="height: 45px" runat="server" id="tRowGRN" >
                        <td align="center" style="width:60%; padding-left:6%;">
                            <b><span style="color: #336699">GRN:-</span></b>&nbsp;
                    <asp:TextBox runat="server" ID="txtGRNSearch" MaxLength="12" Width="140px" onChange="NumericOnly(this);" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtGRNSearch"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ge"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnShowGRNSearch" runat="server" ValidationGroup="ge" size="25" Text="Show" OnClientClick="onChangeRdbtn()" OnClick="btnSubmit_Click" Width="57px" />
                        </td>
                       
                    </tr>
                    <tr align="center" id="trrpt" runat="server">
                        <td colspan="6">
                            <asp:Repeater ID="rptrManualSuccess" OnItemCommand="rptrManualSuccess_ItemCommand" OnItemDataBound="rptrManualSuccess_ItemDataBound" runat="server">
                                <HeaderTemplate>
                                    <table border="1" width="80%" cellpadding="0" cellspacing="0">
                                        <tr style="background-color: #14c4ff; color: White; font-weight: bold; height: 20px">
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
                                        <td align="center">
                                            <asp:LinkButton ID="lblEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                CommandArgument='<%# Eval("Grn") %>' Text="Edit"></asp:LinkButton>
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
                        <td colspan="6">
                            <center>
                                <asp:Label ID="lblDefaultMessage" runat="server" Text="Sorry, No Record Found." Style="color: firebrick; font-weight: bold" Visible="false">
                                </asp:Label>
                            </center>
                        </td>
                    </tr>
                    <tr runat="server" id="tr1" visible="false">
                        <td colspan="6">
                            <center>
                                <rsweb:ReportViewer ID="rptManualSuccessDivisionWiserpt" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false"></rsweb:ReportViewer>
                            </center>
                        </td>
                    </tr>
                    <tr runat="server" id="tr2" visible="false">


                        <td colspan="6">
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

