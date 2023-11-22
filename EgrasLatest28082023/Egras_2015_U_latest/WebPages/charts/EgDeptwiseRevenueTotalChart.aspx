<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgDeptwiseRevenueTotalChart.aspx.cs" Inherits="WebPages_EgDeptwiseRevenueTotalChart"
    Title="Untitled Page" %>

<%--<%@ Register Src="~/UserControls/chart.ascx" TagName="chart" TagPrefix="NIC" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script src="../../js/amcharts.js" type="text/javascript"></script>

    <fieldset id="fldRev" runat="server" style="color: Green; border-top-left-radius: 0.5em 0.5em;
        border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
        border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
        behavior: url(../PIE.htc);">
        <legend style="color: Green;">
            <h4>
                Department Wise Revenue Report</h4>
        </legend>
        <table style="width: 100%" cellpadding="2px" cellspacing="5px">
            <tr>
                <td colspan="2">
                    <center>
                        From Date :
                        <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqFromDate" runat="server" ControlToValidate="txtFromDate"
                            SetFocusOnError="true" Text="Required" ValidationGroup="de"></asp:RequiredFieldValidator>
                        <%-- </td>
            <td>--%>
                        To Date :
                        <asp:TextBox ID="txtToDate" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqToDate" runat="server" ControlToValidate="txtToDate"
                            SetFocusOnError="true" Text="Required" ValidationGroup="de"></asp:RequiredFieldValidator>
                        &nbsp;
                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" ValidationGroup="de" /></center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <asp:Label ID="lblhead" runat="server" Text="No Record Found !!" Visible="false"></asp:Label>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset id="fldPie" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em;
                        border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
                        padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
                        behavior: url(../PIE.htc);">
                        <legend style="color: Green;">Pie Chart For Revenue</legend>
                        <div id="divDERev" style="width: 500px; height: 300px; background-color: white;">
                            <asp:Literal ID="ltRev" runat="server"></asp:Literal>
                        </div>
                    </fieldset>
                </td>
                <td>
                    <fieldset id="fldColumn" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em;
                        border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
                        padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
                        behavior: url(../PIE.htc);">
                        <legend style="color: Green;">Drill Down Column Chart</legend>
                        <div id="divDERevCol" style="width: 500px; height: 300px; background-color: white;">
                            <asp:Literal ID="ltRevCol" runat="server"></asp:Literal>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td  colspan="2">
                   <fieldset id="fldTran" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em;
                        border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
                        padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
                        behavior: url(../PIE.htc);">
                        <legend style="color: Green;">Pie Chart For Transactions</legend>
                        <div id="divTran" style="width: 500px; height: 300px; background-color: white;">
                            <center>
                                <asp:Literal ID="ltTran" runat="server"></asp:Literal></center>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<NIC:chart ID="revchart" runat="server" />--%>
                </td>
                <td>
                    <asp:Label ID="lblRev" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
    <ajaxToolkit:CalendarExtender ID="calfromdate" runat="server" Format="dd/MM/yyyy"
        TargetControlID="txtFromDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="calTodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
