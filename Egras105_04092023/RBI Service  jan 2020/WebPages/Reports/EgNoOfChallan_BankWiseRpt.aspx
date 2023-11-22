<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgNoOfChallan_BankWiseRpt.aspx.cs" Inherits="WebPages_Reports_EgNoOfChallan_BankWiseRpt" %>

<%@ Register Src="~/UserControls/Months_FinyearControl.ascx" TagName="FinYear" TagPrefix="ucl" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../JS/bootstrap.min.js"></script>
    <style>

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
    <Triggers>
        <asp:PostBackTrigger ControlID="btnPDF" />
    </Triggers>
        <ContentTemplate>
         <%--   <fieldset runat="server" id="lstrecord" style="width: 100%;">
                <legend style="color: #336699; font-weight: bold">Number Of Challan Bank Wise</legend>--%>
                <table style="width: 100%"  id="MainTable">
                    <tr >
                        <td colspan="2">
                            <ucl:FinYear ID="ddlDate" runat="server" />
                        </td>
                        </tr><tr style="border:1px">
                        <td align="center" style="border:1px">
                            <asp:Button Text="Submit" style="" Height="33" Width="120px" ID="btnSubmit" CssClass="btn btn-default" OnClick="btnSubmit_Click" runat="server" />
                             
                        </td>
                        <td align="center" style="border:1px">
                            <asp:Button Text="PDFDownload" Height="33" Width="30%" CssClass="btn btn-default"  ID="btnPDF" Visible="false" OnClick="btnPDF_Click" runat="server" />
                        </td>
                    </tr>
                
                    <tr>
                        <td colspan="3">
                            <rsweb:ReportViewer ID="SSRSreport" runat="server" Font-Names="Times New Roman" Font-Size="8pt" ShowToolBar="false" AsyncRendering="false" Width="100%" Height="800PX">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
           <%-- </fieldset>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

