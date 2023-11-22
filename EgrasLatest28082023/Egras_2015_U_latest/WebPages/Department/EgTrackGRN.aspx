<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgTrackGRN.aspx.cs" Inherits="WebPages_Department_EgTrackGRN" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

        input[type=submit] {
            width: 25%;
            height: 41px;
        }
    </style>


    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Track GRNl" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Track GRN </span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Track GRN" />
    </div>


    <div class="row" style="border: 1px solid; width: 100%; margin-left: 0px; padding: 5px;">
        <div class="col-md-12">
            <asp:Button ID="btnAdd" runat="server" Text="Add GRN For Search" OnClick="btnAdd_Click"  CssClass="btn btn-default pull-right"/>
        </div>
    </div>
    <div class="row" style="border: 1px solid; width: 100%; margin-left: 0px; padding: 5px;" id="divPnl" runat="server" visible="false">
        <div class="col-md-12">
            <asp:Panel ID="pnlTextBoxes" runat="server">
            </asp:Panel>
        </div>
    </div>
    <div class="row" style="border: 1px solid; width: 100%; margin-left: 0px; padding: 5px;" id="divDownload" runat="server" visible="false">
        <div class="col-md-12">
            <asp:Button ID="btnPrint" Visible="false" Height="33" style="height:40px" CssClass="btn btn-default pull-right" runat="server" Text="Download" ValidationGroup="a" OnClick="btnPrint_Click" />
        </div>
    </div>
    <rsweb:ReportViewer ID="rptTrackGRN" runat="server" Width="100%" SizeToReportContent="true"
        AsyncRendering="false" ShowRefreshButton="false" Visible="false">
    </rsweb:ReportViewer>

</asp:Content>

