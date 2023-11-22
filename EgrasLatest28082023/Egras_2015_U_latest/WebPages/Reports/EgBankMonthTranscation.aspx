<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="false"
    CodeFile="EgBankMonthTranscation.aspx.cs" Inherits="WebPages_Reports_EgBankMonthTranscation"
    Title="MonthWise Bank Transactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../js/amcharts.js"></script>

    <link rel="Stylesheet" type="text/css" href="../../CSS/css.css" />

    <script type="text/javascript">
        window.onload = isPostBack;
        function isPostBack() {
            var year = (new Date).getFullYear();
            $('#<%=ddlYear.ClientID %>').append('<option  selected="True" value=' + year + '>' + year + '</option >');
            $('#<%=ddlYear.ClientID %>').append('<option  value=' + (parseInt(year) - 1) + ' >' + (parseInt(year) - 1) + '</option >');
            $('#<%=ddlYear.ClientID %>').append('<option  value=' + (parseInt(year) - 2) + ' >' + (parseInt(year) - 2) + '</option >');
        }
        $(document).ready(function() {

            //ShowMonthData(this);
        });
    </script>

    <script type="text/javascript" language="javascript">
        function ShowMonthData(e) {
            var MonthCode = e.title;
            
            var chklist = '';
            var YearDropdown = document.getElementById('<%=ddlYear.ClientID %>');
            var Year = YearDropdown.options[YearDropdown.selectedIndex].value;
            //var Year = $('#ctl00_ContentPlaceHolder1_ddlYear option:selected').val();
            var Type = "";
            var rbList = document.getElementById('<%= rdbYearType.ClientID %>');
            var rbCount = rbList.getElementsByTagName("input");
            for (var i = 0; i < rbCount.length; i++) {
                if (rbCount[i].checked == true) {
                    Type = rbCount[i].value;

                }
            }

            if (MonthCode != 'MonthWise Bank Transactions' && MonthCode != '') {
                MonthCode = e.title;
            }
           
            if (MonthCode == '') {
                MonthCode = chkboxevent(Type);
            }
            if (MonthCode == 'MonthWise Bank Transactions') {
                MonthCode = chkboxevent(Type);
            }
          
                $.ajax({
                    type: 'POST',
                    url: 'EgBankMonthTranscation.aspx/CallJSONMethod',
                    data: '{ "Code": "' + MonthCode + '" ,Year: "' + Year + '" ,type: "' + Type + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        if (data.d.length == 0)  // case of no record
                        {
                            document.getElementById("<%= lblmsg.ClientID %>").style.display = ''; //browser will auto select the default property.
                            document.getElementById("tbDetails").style.display = 'none';
                            document.getElementById("<%= lblMonth.ClientID %>").style.display = 'none';
                            document.getElementById('divColoumn').innerHTML = "";
                            document.getElementById('divPie').style.display = 'none';
                            document.getElementById('divColoumn').style.display = 'none';
                            document.getElementById('divPie').innerHTML = "";
                        }
                        else {
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
            document.getElementById("<%= lblMonth.ClientID %>").style.display = '';
            //alert(data.d);
            //alert(data.d[0]);
            //alert(data.d[0].MonthName);

            document.getElementById("<%= lblMonth.ClientID %>").innerHTML = (data.d[0].MonthName) + " Month Transaction Details";
            document.getElementById('divPie').style.display = '';
            document.getElementById('divColoumn').style.display = '';

            if (document.getElementById('<%=chkQuarter.ClientID %>').checked == true) {
                document.getElementById("<%= tblmonth.ClientID %>").style.display = 'none';
                document.getElementById('<%= tblQuarter.ClientID %>').style.display = '';

            }
            else {
                document.getElementById("<%= tblmonth.ClientID %>").style.display = '';
                document.getElementById('<%= tblQuarter.ClientID %>').style.display = 'none';
                var checkList = document.getElementById('<%=chkQuarterList.ClientID %>');
                var checkBoxArray = checkList.getElementsByTagName('input');
                var checkedValues = '';
                if (checkBoxArray[0].checked == true || checkBoxArray[1].checked == true || checkBoxArray[2].checked == true || checkBoxArray[3].checked == true) {
                    for (var i = 0; i < checkBoxArray.length; i++) {
                        var checkBoxRef = checkBoxArray[i];

                        if (checkBoxRef.checked == true) {
                            checkBoxRef.checked = false;
                        }
                    }

                }
            }
            // for TableGrid
            var Parent = document.getElementById("tbDetails");   //  To avoid Reappending of table Rows
            while (Parent.hasChildNodes()) {
                Parent.removeChild(Parent.firstChild);
            }

            var headRow = "<thead style='background-color:#3BB9FF; color:white; font-weight: bold'><tr style='border: solid 1px #000000;'><td width='40%'>BankName</td><td width='30%'>Challan</td><td width='30%'>Amount(in Crores)</td></thead>"
            $('#tbDetails').append(headRow);

            for (var i = 0; i < data.d.length; i++) {
                $('#tbDetails').append("<tr><td>" + (data.d[i].BankName) + "</td><td>" + (data.d[i].ToChallan) + "</td><td>" + (data.d[i].Amount) + "</td></tr>");

            }

            // end of TableGrid


            //  for Creating Coloumn 3d Chart
            pieChart(data);
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
                for (var i = 0; i <= (data.d.length - 1); i++) {
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
            chart.pathToImages = 'http://www.amcharts.com/lib/images/';
            chart.dataProvider = chartData;

            chart.categoryField = 'Month';
            chart.angle = 30;
            chart.depth3D = 20;

            // listen for dataUpdated event ad call "zoom" method then it happens
            chart.addListener('dataUpdated', zoomChart);
            // AXES
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

        function pieChart(data) {

            var chart1;
            var chartData = '[';
            for (var i = 0; i < (data.d.length); i++) {

                if (i == (data.d.length - 1)) {
                    chartData = chartData + '{BankName:' + "'" + data.d[i].BankShortName + "'" + ',Amount:' + data.d[i].Amount + '}'
                }
                else {
                    chartData = chartData + '{BankName:' + "'" + data.d[i].BankShortName + "'" + ',Amount:' + data.d[i].Amount + '},'
                }

            }
            chartData = chartData + '];';
            chartData = eval(chartData);
            document.getElementById('divPie').innerHTML = "";
            // PIE CHART
            chart1 = new AmCharts.AmPieChart();
            // title of the chart
            chart1.addTitle('Amount In Percentage ( % )', 16);
            chart1.dataProvider = chartData;
            chart1.titleField = 'BankName';
            chart1.valueField = 'Amount';
            chart1.sequencedAnimation = true;
            chart1.startEffect = 'elastic';
            chart1.innerRadius = '30%';
            chart1.startDuration = 2;
            chart1.labelRadius = 15;
            // the following two lines makes the chart 3D
            chart1.depth3D = 10;
            chart1.angle = 25;
            chart1.colors = ["#B0DE09", "#04D215", "#0D8ECF", "#0D52D1", "#2A0CD0", "#8A0CCF", "#CD0D74"];
            chart1.write('divPie');

        }



        function chkboxevent(type) {
            var checkList = document.getElementById('<%=chkQuarterList.ClientID %>');
            var checkBoxArray = checkList.getElementsByTagName('input');
            var checkedValues = '';
            var str = '';
            if ((checkBoxArray[0].checked == true || checkBoxArray[1].checked == true || checkBoxArray[2].checked == true || checkBoxArray[3].checked == true) && (document.getElementById('<%=chkQuarter.ClientID %>').checked == true)) {


                if (checkBoxArray[0].checked == true) {
                    for (var i = 1; i <= 3; i++) {
                        if (i > 1) str += ',';
                        str = str + i;
                    }

                }
                if (checkBoxArray[1].checked == true) {
                    for (var i = 4; i <= 6; i++) {
                        if (i > 4 || str != '') str += ',';
                        str = str + i;
                    }
                }
                if (checkBoxArray[2].checked == true) {
                    for (var i = 7; i <= 9; i++) {
                        if (i > 7 || str != '') str += ',';
                        str = str + i;
                    }
                }
                if (checkBoxArray[3].checked == true) {
                    for (var i = 10; i <= 12; i++) {
                        if (i > 10 || str != '') str += ',';
                        str = str + i;
                    }
                }

            }
            else {
                if (document.getElementById('<%=chkQuarter.ClientID %>').checked == true) {
                    if (type == 1) {
                        for (var i = 1; i <= 3; i++) {
                            if (i > 1) str += ',';
                            str = str + i;
                        }
                    }
                    if (type == 2) {
                        for (var i = 4; i <= 6; i++) {
                            if (i > 4 || str != '') str += ',';
                            str = str + i;
                        }
                    }

                }
                else {
                    if (type == 1) str = 1;
                    if (type == 2) str = 4;
                }

            }
            return str;
        }
    </script>

    <table id="tblheader" width="80%" align="center" style="margin-top: -20px;">
        <tr>
            <td style="text-align: center; height: 35px">
                <asp:Label ID="Labelheader" runat="server" Text="Month wise Bank Transaction" Font-Bold="True"
                    ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="1" width="90%" align="center">
        <tr>
            <td>
                <b>Select Year</b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlYear" runat="server" Width="200px">
                  
                </asp:DropDownList>
                
            </td>
            <td align="center">
                <asp:RadioButtonList ID="rdbYearType" runat="server" RepeatDirection="Horizontal"
                     Width="350px" Font-Bold="true">
                    <asp:ListItem Selected="True" Text="Calender Year Wise" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Financial Year Wise" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="center">
                <asp:CheckBox Width="150px" ID="chkQuarter" runat="server" Text="Quarter Report"
                    Font-Bold="true" />
            </td>
        </tr>
        <tr id="trRecord" runat="server">
            <td width="30%">
                <table width="20%" id="tblmonth" runat="server">
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgJan" runat="server" ImageUrl="~/Image/MonthImages/JAN.gif" Width="170px" OnClientClick ="ShowMonthData(this); return false;"
                                Onmouseover="ShowMonthData(this);" ToolTip="1" Height="30px" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgFeb" runat="server" ImageUrl="~/Image/MonthImages/FebNew.gif" Width="170px" OnClientClick ="ShowMonthData(this); return false;"
                                Onmouseover="ShowMonthData(this);" ToolTip="2" Height="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgMarch" runat="server" ImageUrl="~/Image/MonthImages/March.gif" OnClientClick ="ShowMonthData(this); return false;"
                                Width="170px" Onmouseover="ShowMonthData(this);" ToolTip="3" Height="30px" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgApril" runat="server" ImageUrl="~/Image/MonthImages/April.gif" OnClientClick ="ShowMonthData(this); return false;"
                                Width="170px" Onmouseover="ShowMonthData(this);" ToolTip="4" Height="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgMay" runat="server" ImageUrl="~/Image/MonthImages/May.gif" Width="170px" OnClientClick ="ShowMonthData(this); return false;"
                                Onmouseover="ShowMonthData(this);" ToolTip="5" Height="30px" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgJune" runat="server" ImageUrl="~/Image/MonthImages/June.gif" Width="170px" OnClientClick ="ShowMonthData(this); return false;"
                                Onmouseover="ShowMonthData(this);" ToolTip="6" Height="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="imgJuly" runat="server" ImageUrl="~/Image/MonthImages/July.gif" Width="170px" OnClientClick ="ShowMonthData(this); return false;"
                                ImageAlign="Left" Onmouseover="ShowMonthData(this);" ToolTip="7" Height="30px" />
                        </td>
                        <td> 
                            <asp:ImageButton ID="imgAug" runat="server" ImageUrl="~/Image/MonthImages/Aug.gif" Width="170px" OnClientClick ="ShowMonthData(this); return false;"
                                Onmouseover="ShowMonthData(this);" ToolTip="8" Height="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgSept" runat="server" ImageUrl="~/Image/MonthImages/Sep.gif" Width="170px"
                                Onmouseover="ShowMonthData(this);" ToolTip="9" Height="30px" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgOct" runat="server" ImageUrl="~/Image/MonthImages/Oct.gif" Width="170px"
                                Onmouseover="ShowMonthData(this);" ToolTip="10" Height="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgNov" runat="server" ImageUrl="~/Image/MonthImages/NOV.gif" Width="170px"
                                Onmouseover="ShowMonthData(this);" ToolTip="11" Height="30px" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgDec" runat="server" ImageUrl="~/Image/MonthImages/dec.gif" Width="170px"
                                Onmouseover="ShowMonthData(this);" ToolTip="12" Height="30px" />
                        </td>
                    </tr>
                </table>
                <table id="tblQuarter" runat="server" style="display: none;" width="100%">
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="chkQuarterList" runat="server" onchange="ShowMonthData(this);">
                                <asp:ListItem Text="Jan To March" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Apr To Jun" Value="1"></asp:ListItem>
                                <asp:ListItem Text="July To Sept" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Oct To Dec" Value="3"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="70%" align="center" colspan="2">
                <asp:Label ID="lblmsg" runat="server" Text="No Record Found !!" ForeColor="Green"
                    Font-Bold="true" Font-Size="80" Style="display: none;"></asp:Label>
                <asp:Label ID="lblMonth" runat="server" Text="" Font-Bold="true" Font-Size="80" ForeColor="Green"
                    Style="display: none;"></asp:Label>
                <table id="tbDetails" cellpadding="4" cellspacing="0" border="1" width="100%">
                    <tbody>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td width="40%">
                <div id="divPie" style="width: 350px; height: 300px; background-color: white;">
                </div>
            </td>
            <td width="70%" colspan="2">
                <div id="divColoumn" style="width: 100%; height: 300px; background-color: white;">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
