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
        <img src="../../Image/help1.png" class="pull-right" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="right" title="" />
    </div>

    <table style="width: 100%" border="1" align="center" id="MainTable">
        <tr>
            <td style="text-align: center; padding: 1%;">
                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">From Date :- </span></b>&nbsp;
                    <asp:TextBox ID="txtfromdate" Height="35px"  Class="form-control pull-right" runat="server" Width="50%" onkeypress="Javascript:return NumberOnly(event)"
                        onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                    ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
            </td>
            <td style="text-align: center; padding: 1%;">
                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">To Date :- </span></b>&nbsp;
                    <asp:TextBox ID="txttodate" Class="form-control pull-right" Height="35px" runat="server" Width="50%" onkeypress="Javascript:return NumberOnly(event)"
                        onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txttodate" TargetControlID="txttodate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txttodate"
                    ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
            </td>
            <td style="text-align: center;">
                <asp:Button ID="btnsearch" CssClass="btn btn-primary" runat="server"  Height="35px" 
                    OnClick="btnsearch_Click" ValidationGroup="vldInsert"
                    Text="Download" />
            </td>
        </tr>
    </table>
    <%--</fieldset>--%>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
