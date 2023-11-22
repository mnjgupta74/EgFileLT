<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgDeptMonthOrDayChartNew.aspx.cs" Inherits="WebPages_Charts_EgDeptMonthOrDayChartNew"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script src="../../js/amcharts.js" type="text/javascript"></script>

    <table style="width: 100%">
    
        <tr>
            <td>
                <center>
                    <asp:Label ID="lblhead" runat="server" Text="No Record Found !!" Visible="false"></asp:Label>
                </center>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset id="fldColumn" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em;
                    border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
                    padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
                    behavior: url(../PIE.htc); margin-top: 50px;">
                    <legend style="color: Green;">Month OR Day Wise Column Chart</legend>
                    <div id="divDERevCol" style="width: 100%; height: 400px; background-color: white;">
                        <asp:Literal ID="ltRevCol" runat="server"></asp:Literal>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
    <%--</fieldset>--%>
</asp:Content>
