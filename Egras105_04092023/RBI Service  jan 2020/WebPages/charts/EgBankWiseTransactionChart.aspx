<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" Culture="en-US"
    CodeFile="EgBankWiseTransactionChart.aspx.cs" Inherits="WebPages_charts_EgBankWiseTransactionChart"
    Title="Bank Wise Transaction Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../js/amcharts.js"></script>
    <script src="../../js/Control.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSubmit").click(function () {
                if (Page_ClientValidate("PD")) {
                    ShowMonthData(this)
                }
            });
        });
    </script>

    <script type="text/javascript">

        function ShowMonthData(e) {
            var FromDate = document.getElementById('<%=txtFromdate.ClientID  %>').value;
            var ToDate = document.getElementById('<%=txtTodate.ClientID  %>').value;
            $.ajax({
                type: 'POST',
                url: 'EgBankWiseTransactionChart.aspx/CallJSONMethod',
                data: '{FromDate: "' + FromDate + '"  ,ToDate: "' + ToDate + '",type:5}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    if (data.d.length == 0)  // case of no record
                    {
                        $('#lblmsg').css('display', 'none');
                        $('#tbDetails').css('display', 'none');
                        $('#trColumn').css('display', 'none');
                        $('#trmsg').css('display', 'none');
                    }
                    else {
                        document.getElementById("tbDetails").style.display = '';
                        document.getElementById('divColoumn').style.display = '';
                        document.getElementById('trColumn').style.display = '';
                        document.getElementById('trmsg').style.display = '';
                        ShowChart(data);
                        //  End of  Coloumn 3d Chart

                    } //end of else    
                },
                error: function (result) {
                    alert('Error');
                }
            });
        }
        function ShowChart(data) {

            document.getElementById("<%= lblmsg.ClientID %>").style.display = 'none';
            document.getElementById("tbDetails").style.display = '';
            document.getElementById('divColoumn').style.display = '';


            // for TableGrid
            var Parent = document.getElementById("tbDetails");   //  To avoid Reappending of table Rows
            while (Parent.hasChildNodes()) {
                Parent.removeChild(Parent.firstChild);
            }

            var headRow = "<thead style='background-color:#3BB9FF; color:white; font-weight: bold'><tr style='border: solid 1px #000000;'><td width='40%'>BankName</td><td width='30%'>Transaction</td><td width='30%'>Amount(in Crores)</td></thead>"
            $('#tbDetails').append(headRow);

            for (var i = 0; i < data.d.length; i++) {
                if (i == data.d.length - 1) {
                    $('#tbDetails').append("<tr style='background-color:#3BB9FF; color:white; font-weight: bold'><td><b>" + (data.d[i].BankName) + "</b></td><td align='right'><b>" + (data.d[i].ToChallan) + "</b></td><td  align='right'><b>" + (data.d[i].Amount) + "</b></td></tr>");

                }
                else {
                    $('#tbDetails').append("<tr><td>" + (data.d[i].BankName) + "</td><td  align='right'>" + (data.d[i].ToChallan) + "</td><td  align='right'>" + (data.d[i].Amount) + "</td></tr>");
                }
            }

            // end of TableGrid


            //  for Creating Coloumn 3d Chart
            columnChart(data);
        }
        function columnChart(data) {
            var chart;
            var colors = ['#52D017', '#04D215', '#0D8ECF', '#0D52D1', '#2A0CD0', '#8A0CCF', '#CD0D74', '#FFC125', '#8B8B83', '#4F4F2F', '#800000', '#808000', '#556B2F', '#CD0D74', '#FFC125', '#8B8B83', '#4F4F2F'];

            var chartData = [];

            //funciton to fil chart data[]
            //Zoom
            function zoomChart() {
                // different zoom methods can be used - zoomToIndexes, zoomToDates, zoomToCategoryValues
                chart.zoomToIndexes(chartData.length - 7, chartData.length - 1);
            }
            //End Zoom
            function parseData() {
                // split data string into array

                // loop through this array and create data items
                for (var i = 0; i <= (data.d.length - 2) ; i++) {
                    // we have to subtract 1 from month, as months in javascript are zero-based
                    //                    categoryAxis.color = colors[i];
                    var Month = data.d[i].BankShortName;
                    var visits = data.d[i].Amount;
                    var trans = data.d[i].ToChallan;
                    var color = '#52D017';
                    var color1 = '#3BB9FF';
                    var Amount = data.d[i].Amount;

                    chartData.push({
                        Month: Month,
                        visits: visits,
                        trans: trans,
                        color: color,
                        color1: color1,
                        Amount: Amount
                    });
                }
            }
            //end 

            document.getElementById('divColoumn').innerHTML = "";

            parseData();
            // SERIAL CHART
            chart = new AmCharts.AmSerialChart();
            chart.dataProvider = chartData;

            chart.categoryField = 'Month';
            chart.angle = 30;
            chart.depth3D = 20;

            // listen for dataUpdated event ad call "zoom" method then it happens
            chart.addListener('dataUpdated', zoomChart);

            chart.pathToImages = "../../Image/";

            // AXES<img src="../../Image/dragIcon.gif" />
            // category
            var categoryAxis = chart.categoryAxis;
            categoryAxis.gridPosition = 'start';
            categoryAxis.dashLength = 5;
            categoryAxis.labelRotation = 40;
            //            categoryAxis.color = ['#B0DE09'];    //// To Change label colors
            categoryAxis.axisColor = '#000000';
            categoryAxis.gridColor = '#000000';
            categoryAxis.axisAlpha = 0.5;
            categoryAxis.minPeriod = 'DD';
            // value
            var valueAxis = new AmCharts.ValueAxis();
            valueAxis.stackType = '3d';
            valueAxis.dashLength = 5;
            valueAxis.title = 'Amount And Total No Of Challan';
            chart.addValueAxis(valueAxis);
            // GRAPHS         
            //// first graph
            var graph2 = new AmCharts.AmGraph();
            graph2.valueField = 'visits';
            graph2.type = 'column';

            graph2.lineAlpha = 0;
            graph2.colorField = 'color1';
            graph2.fillAlphas = 1;

            graph2.balloonText = ' [[category]] :(Amount(in crores)  : [[Amount]] Rupees )';
            //"Income: [[value]]"
            chart.addGraph(graph2);

            //// Second graph
            var graph = new AmCharts.AmGraph();
            graph.valueField = 'trans';
            graph.type = 'column';
            graph.lineAlpha = 0;
            graph.colorField = 'color';
            graph.fillAlphas = 1;
            graph.balloonText = '  [[category]]  : (Total Challan :[[value]] Unit)';
            chart.addGraph(graph);
            // CURSOR
            var chartCursor = new AmCharts.ChartCursor();
            chart.addChartCursor(chartCursor);

            // SCROLLBAR
            var chartScrollbar = new AmCharts.ChartScrollbar();
            chartScrollbar.scrollbarHeight = 30;
            chartScrollbar.graph = graph;
            chartScrollbar.graphType = 'line';
            chartScrollbar.gridCount = 4;
            chartScrollbar.color = '#FFFFFF';
            chart.addChartScrollbar(chartScrollbar);
            chart.write('divColoumn');
        }
    </script>

    <style type="text/css">
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>

    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Bank Wise Transaction</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Bank Scroll Report (45-A)" />
    </div>
    <table style="width: 100%" align="center" border="1" cellpadding="0">
        <tr>
            <td align="left">
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                <asp:TextBox ID="txtFromdate" runat="server" MaxLength="10" AutoComplete="Off" 
                    Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px"
                    onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtTodate)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromdate"
                    ErrorMessage="Enter From Date" Text="*" ValidationGroup="PD"></asp:RequiredFieldValidator>
            </td>
            <td align="left">
                <b><span style="color: #336699">To Date : </span></b>&nbsp;
                <asp:TextBox ID="txtTodate" runat="server" MaxLength="10" AutoComplete="Off" 
                    Height="100%" Style="display: initial !important; margin:5px" CssClass="form-control" Width="120px"
                    onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromdate)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTodate"
                    ErrorMessage="Enter To Date" Text="*" ValidationGroup="PD"></asp:RequiredFieldValidator>
            </td>
            <td align="center">
                <input type="button" id="btnSubmit" style="height:33px" class="btn btn-default" name="btnSubmit" value="Show" validationgroup="PD" />
            </td>
        </tr>
        <tr>
            <td colspan="3">

            </td>
        </tr>
        <tr id="trmsg" style="display: none;">
            <td colspan="3" align="center">
                <asp:Label ID="lblmsg" runat="server" Text="No Record Found !!" ForeColor="Green"
                    Font-Bold="true" Font-Size="80" Style="display: none;"></asp:Label>
                <table id="tbDetails" cellpadding="4" cellspacing="0" border="1" width="100%">
                    <tbody>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr id="trColumn" style="display: none;">
            <td colspan="3" align="center">
                <div id="divColoumn" style="width: 100%; height: 300px; background-color: white;">
                </div>
            </td>
        </tr>
    </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
        PopupButtonID="txtFromdate" TargetControlID="txtFromdate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
        PopupButtonID="txtTodate" TargetControlID="txtTodate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
