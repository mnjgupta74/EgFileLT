<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgOnlineChallanSoftCopy.aspx.cs" Inherits="WebPages_Reports_EgOnlineChallanSoftCopy"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script src="../../js/Control.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">

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
       
    </script>

    <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
        <ContentTemplate>--%>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Online-Challan" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Online-Challan SoftCopy</span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 35px;width: 34px;" data-toggle="tooltip" data-placement="right" Title="" />
            </div>
    <%--<fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px">
        <legend style="color: #336699; font-weight: bold">Online-Challan SoftCopy</legend>--%>
        <table style="width: 100%" align="center" id="MainTable">
            <tr>
                <td>
                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                        From Date :- </span></b>&nbsp;
                    <asp:TextBox ID="txtfromdate" runat="server" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                        onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                        ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                        To Date :- </span></b>&nbsp;
                    <asp:TextBox ID="txttodate" runat="server" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                        onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="txttodate" TargetControlID="txttodate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txttodate"
                        ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" ValidationGroup="vldInsert"
                        Text="Download" />
                </td>
            </tr>
        </table>
    <%--</fieldset>--%>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
