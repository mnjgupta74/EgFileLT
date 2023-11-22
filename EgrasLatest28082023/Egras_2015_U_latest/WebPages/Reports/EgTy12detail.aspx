<%@ Page Title="Egras.Rajasthan.gov.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgTy12detail.aspx.cs" Inherits="WebPages_Reports_EgTy12detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <style type="text/css">
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>

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
            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="TY-34" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Ty-34 Report</span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="right" title="Ty-34 Report" />
            </div>
            <%--<fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px;">--%>
            <%--<legend style="color: #336699; font-weight: bold">Ty-34 Report</legend>--%>
            <table style="width: 100%" align="center" border="1" id="MainTable">
                <tr>
                    <td >
                        <b><span style="color: #336699">From Date : </span></b>&nbsp;
                    
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"  Width="120px" Style="display:inherit !important;height: 35px; margin-left:4px"
                            onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                        </ajaxToolkit:MaskedEditExtender>
                    </td>
                    <td >
                        <b><span style="color: #336699">To Date : </span></b>&nbsp;
                    
                        <asp:TextBox ID="txtToDate" runat="server"  CssClass="form-control"  Width="120px" Style="display:inherit !important; height: 35px;margin-left:4px"
                            onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                            Format="dd/MM/yyyy" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                        </ajaxToolkit:MaskedEditExtender>
                    </td>
                    </tr><tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" Height="33px" CssClass="btn btn-default" OnClick="btnshow_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="grdTy12Rpt" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Names="Verdana" Font-Size="10pt" Width="100%" EmptyDataText="No Record Found"
                            EmptyDataRowStyle-HorizontalAlign="Center" ForeColor="#333333" GridLines="None"
                            EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#507CD1" AllowPaging="true"
                            PageSize="25" OnPageIndexChanging="grdTy12Rpt_PageIndexChanging" OnRowCommand="grdTy12Rpt_RowCommand">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="GRN">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkgrn" CommandName="grnbind" Text='<%#Eval("GRN")%>' CommandArgument='<%#Eval("GRN")%>'
                                            runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Challanno" HeaderText="ChallanNo">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BankChallanDate" HeaderText="ChallanDate">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalAmount" HeaderText="Cash Amount" DataFormatString="{0:n}">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DeductCommission" HeaderText="Commission" DataFormatString="{0:n}">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <%-- </fieldset>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
