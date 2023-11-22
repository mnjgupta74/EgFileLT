<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgMEGrnByServiceRpt.aspx.cs" Inherits="WebPages_Reports_EgMEGrnByServiceRpt" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
   <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
        <ContentTemplate>--%>



            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Released GRN" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">ME GRN </span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Released GRN" />
            </div>
            <table style="width: 100%" align="center" id="MainTable" border="1">
                <tr style="height: 45px">
                    <td align="left">
                        <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                            <asp:TextBox ID="txtFromDate" runat="server" 
                                Width="120px" Height="100%" Style="display: initial !important" CssClass="form-control" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                            <asp:TextBox ID="txtToDate" runat="server" Width="120px" Height="100%" Style="display: initial !important" CssClass="form-control" 
                                onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    </tr><tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-default" Height="100%" ValidationGroup="de" Text="Show" OnClick="btnShow_Click" />
                        &nbsp;
                            <asp:Button ID="BtnPdf" runat="server" style="margin-left:15px" Text="PDF" CssClass="btn btn-default" Height="100%" ValidationGroup="de" OnClick="btnpdf_Click" />
                    </td>
                </tr>
            </table>

            <table style="width: 100%" align="center" id="Ttable">
                <div id="rptSchemaDi" runat="server" visible="false">
                    <tr ">
                        <td>
                           <%-- <span style="color: #000000; font-weight: normal; left: inherit;">Total Amount :-</span>&nbsp;--%>
                             <asp:Label ID="lblAmount" TabIndex="5" runat="server" Font-Bold="true" ForeColor="Black" ></asp:Label>
                            <br />
                        </td>
                        <br />
                    </tr>
                    <asp:Repeater ID="rptrMEGRN" runat="server">
                        <HeaderTemplate>
                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px">

                                    <th><b>GRN</b> </th>
                                    <th><b>RemitterName</b> </th>
                                    <th><b>BudgetHead</b> </th>
                                    <th><b>Amount</b> </th>
                                    <th><b>BankName</b> </th>
                                    <th><b>TransDate</b> </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <%--<td align="center"><%# DataBinder.Eval(Container.DataItem,"GRN")%></td>--%>
                                <td align="center"><asp:LinkButton ID="lnkgrn" OnClick = "lnkgrn_Click" CommandName="grnbind" Text='<%#Eval("GRN")%>' CommandArgument='<%#Eval("GRN")%>'
                                                    runat="server"></asp:LinkButton></td>                               
                                <td align="left"><%# DataBinder.Eval(Container.DataItem,"RemitterName")%></td>
                                 <td align="left"><%# DataBinder.Eval(Container.DataItem,"BudgetHead")%></td>
                                <td align="right"><%# DataBinder.Eval(Container.DataItem, "Amount", "{0:0.00}")%></td>
                                  <td align="center"><%# DataBinder.Eval(Container.DataItem, "BankName")%>
                                <td align="center"><%# DataBinder.Eval(Container.DataItem, "TransDate")%></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td align="center"><asp:LinkButton ID="lnkgrn" OnClick = "lnkgrn_Click"  CommandName="grnbind" Text='<%#Eval("GRN")%>' CommandArgument='<%#Eval("GRN")%>'
                                                    runat="server"></asp:LinkButton></td>
                                <td align="left"><%# DataBinder.Eval(Container.DataItem,"RemitterName")%></td>
                                <td align="left"><%# DataBinder.Eval(Container.DataItem,"BudgetHead")%></td>
                                <td align="right"><%# DataBinder.Eval(Container.DataItem, "Amount", "{0:0.00}")%></td>
                                <td align="center"><%# DataBinder.Eval(Container.DataItem, "BankName")%>
                                <td align="center"><%# DataBinder.Eval(Container.DataItem, "TransDate")%></td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                           <tr>
                                 <asp:Label ID="Label1" runat="server"  Text="No Data Found!"  Visible='<%# bool.Parse((rptrMEGRN.Items.Count == 0).ToString()) %>'></asp:Label>
                           </tr>
                        </FooterTemplate>
                    </asp:Repeater>


                    <tr runat="server" id="trrpt" visible="false">
                        <td colspan="3" style="text-align: left">
                            <center>
                                <rsweb:ReportViewer ID="rptRG" runat="server" AsyncRendering="false" ShowExportControls="false" ShowRefreshButton="false" SizeToReportContent="true" Width="100%">
                                </rsweb:ReportViewer>
                            </center>
                        </td>
                    </tr>
                </div>
            </table>

        <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnPdf" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>

