<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="ChallanCheckList.aspx.cs" Inherits="WebPages_Reports_ChallanCheckList"
    Title="Untitled Page" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />

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

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>

    <div id="cover-spin"></div>
    <script type="text/javascript">

        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnSubmit"]').click(function () {
                if ($(<%=txtfromdate.ClientID%>).val() != "" && $(<%=txttodate.ClientID%>).val() != "" && $(<%=ddlbankbranch.ClientID%>).val() != "") {
                    $('#cover-spin').show(0)
                }
            });
        });
    </script>
    <%--==============END CSS - JQUERY LOADER============--%>


    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="TY-33">
            <span _ngcontent-c6="" style="color: #FFF">Challan CheckList Report</span></h2>
    </div>

    <table width="100%" style="text-align: center" align="center" border="1">
        <%--<tr>
            <td colspan="6" style="text-align: center; color: Green;">
                <asp:Label ID="Label1" runat="server" Text="Challan CheckList Report" Width="300px"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td align="left">
                <b><span style="color: #336699">From Date : </span></b>

                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" Width="120px" Height="100%" Style="display: initial !important"
                    onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>

            <td align="left">
                <b><span style="color: #336699">To Date : </span></b>

                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" Width="120px" Height="100%" Style="display: initial !important"
                    onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txttodate" TargetControlID="txttodate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txttodate"
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td align="left"><b><span style="color: #336699">
                <asp:Label ID="Label2" runat="server" Text="Bank Name : "></asp:Label></span></b>

                <asp:DropDownList ID="ddlbankbranch" runat="server" class="chzn-select" AutoPostBack="true">
                    <asp:ListItem>Select Bank</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="reqddlbankbranch" runat="server" ErrorMessage="*"
                    SetFocusOnError="true" InitialValue="0" ControlToValidate="ddlbankbranch" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>

            <td align="center" colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="33" Style="margin-top: 3px; margin-bottom: 3px; margin-right: 20px" CssClass="btn btn-default" TabIndex="4" OnClick="btnSubmit_Click" ValidationGroup="a" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" Height="33" Style="margin-top: 3px; margin-bottom: 3px; margin-right: 5px" CssClass="btn btn-default" ValidationGroup="a" OnClick="btnReset_Click" />
                <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="Export" OnCheckedChanged="CheckBox1_CheckedChanged" />--%>
            </td>
        </tr>

        <tr>
            <td colspan="5">
                <rsweb:ReportViewer ID="rptChallanCheckList" runat="server" Width="100%" SizeToReportContent="true"
                    AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false" ShowReportBody="true">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
        PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
        PopupButtonID="txttodate" TargetControlID="txttodate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
