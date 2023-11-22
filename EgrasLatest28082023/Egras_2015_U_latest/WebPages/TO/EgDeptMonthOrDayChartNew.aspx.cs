using System;
using System.Web.UI;
using System.Data;
using System.Text;
using EgBL;

public partial class WebPages_Charts_EgDeptMonthOrDayChartNew : System.Web.UI.Page
{
    EgDeptwiseRevenueTotal objrev;
    EgToChallanReportBL objEgToChallanReportBL;
    StringBuilder sb = new StringBuilder();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblhead.Visible = false;
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            loadChart();
        }
    }

    public void loadChart()
    {
        objEgToChallanReportBL = new EgToChallanReportBL();
        objrev = new EgDeptwiseRevenueTotal();
        ///////Date passing   "01/12/2012"
        //DateTime fromDate = DateTime.Parse("01/01/2015");
        //string frmDate = fromDate.ToString("dd'/'MM'/'yyyy");
        //DateTime toDate = DateTime.Parse(DateTime.Now.ToString());
        //string Todate = toDate.ToString("dd'/'MM'/'yyyy");

        //string[] from = frmDate.ToString().Trim().Replace("-", "/").Split('/');
        //objrev.Fromdate = Convert.ToDateTime(from[0].ToString().Trim() + "/" + from[1].ToString().Trim() + "/" + from[2].ToString().Trim());
        //string[] ToDate1 = toDate.ToString().Trim().Replace("-", "/").Split('/');
        //objrev.Todate = Convert.ToDateTime(ToDate1[0].ToString().Trim() + "/" + ToDate1[1].ToString().Trim() + "/" + ToDate1[2].ToString().Trim());
    
        //////////Location passing
        if (Session["UserType"].ToString() == "3")
        {
            objEgToChallanReportBL.UserId = Convert.ToInt32(Session["UserId"]);
            objrev.Location = objEgToChallanReportBL.GetTreasCode().Trim();
        }
        else
        {
            objrev.Location = "0";
        }
        /////////////Department passing
        if (Session["UserType"].ToString() == "5")
        {
            objrev.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objrev.UserType = Convert.ToInt16(Session["UserType"].ToString());
            string dept = objrev.GetDeptCode();
            objrev.Deptcode = dept.Substring(0, (dept.Length - 1));
        }
        else
        {
            objrev.Deptcode = "0";
        }
        objrev.RevMonthOrDayWiseNew(dt);
        if (dt.Rows.Count > 0)
        {
            fldColumn.Visible = true;

            Column3dChartBENextExp1();
        }

    }
    public void Column3dChartBENextExp1()
    {

        ltRevCol.Text = "";
        sb.Remove(0, sb.Length);
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("var chart;");
        sb.Append("var colors=['#8A3324','#FA8072','#5C4033','#E47833','#A68064','#B6AFA9','#FFDAB9','#FFC125','#8B8B83','#4F4F2F','#D9D919','#808000','#556B2F'];");
        sb.Append("var chartData = [];");

        //funciton to fil chart data[]
        //Zoom
        sb.Append("function zoomChart() {");
        // different zoom methods can be used - zoomToIndexes, zoomToDates, zoomToCategoryValues
        sb.Append("chart.zoomToIndexes(chartData.length - 7, chartData.length - 1);");
        sb.Append("}");
        //End Zoom
        sb.Append("function parseData() {");
        // split data string into array

        // loop through this array and create data items
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            // we have to subtract 1 from month, as months in javascript are zero-based
            string[] dateArray = dt.Rows[i]["monthN"].ToString().Trim().Split('/');
            //sb.Append("var Month = '" + dt.Rows[i]["monthN"].ToString() + "';");
            sb.Append("var Month =  new Date(Number(" + dateArray[2].ToString() + "), Number(" + dateArray[1].ToString() + ") - 1, Number(" + dateArray[0].ToString() + ")).toDateString();");
            sb.Append("var visits =  " + dt.Rows[i]["Amount"].ToString() + ";");
            sb.Append("var trans = " + dt.Rows[i]["TransNo"].ToString() + ";");
            sb.Append(" var color =  colors[" + 9 + "];");
            sb.Append(" var color1 =  colors[" + 4 + "];");
            sb.Append(" var Amount = '" + dt.Rows[i]["Amount1"].ToString() + "';");

            sb.Append("chartData.push({");
            sb.Append("Month: Month,");
            sb.Append("visits: visits,");
            sb.Append(" trans: trans,");
            sb.Append("color: color,");
            sb.Append("color1: color1,");
            sb.Append("Amount: Amount");
            sb.Append("});");
        }
        sb.Append("}");
        //end 
        //handdle Zoom

        // this methid is called each time the selected period of the chart is changed
        //sb.Append("function handleZoom(event) {");
        //sb.Append("var startDate = event.startDate;");
        //sb.Append("var endDate = event.endDate;");
        //sb.Append(" document.getElementById('startDate').value = AmCharts.formatDate(startDate, 'DD/MM/YYYY');");
        //sb.Append("document.getElementById('endDate').value = AmCharts.formatDate(endDate, 'DD/MM/YYYY');");
        //sb.Append("}");

        //end Zoom
        sb.Append("AmCharts.ready(function () {");
        sb.Append("parseData();");
        // SERIAL CHART
        sb.Append("chart = new AmCharts.AmSerialChart();");
        //sb.Append("chart4.addTitle('" + "hello" + "', 16);");
        sb.Append("chart.pathToImages = 'http://www.amcharts.com/lib/images/';");
        sb.Append("chart.dataProvider = chartData;");
        sb.Append("chart.categoryField = 'Month';");
        sb.Append(" chart.angle = 30;");
        sb.Append("chart.depth3D = 20;");

        // listen for dataUpdated event ad call "zoom" method then it happens
        sb.Append("chart.addListener('dataUpdated', zoomChart);");
        // listen for zoomed event andcall "handleZoom" method then it happens
        //sb.Append(" chart.addListener('zoomed', handleZoom);");
        // AXES
        // category
        sb.Append("var categoryAxis = chart.categoryAxis;");
        sb.Append("categoryAxis.gridPosition = 'start';");
        sb.Append("categoryAxis.dashLength = 5;");
        sb.Append("categoryAxis.labelRotation = 40;");
        sb.Append("categoryAxis.axisColor = '#000000';");
        sb.Append("categoryAxis.gridColor = '#000000';");
        sb.Append("categoryAxis.axisAlpha = 0.5;");
        //sb.Append("categoryAxis.parseDates = true;");
        sb.Append("categoryAxis.minPeriod = 'DD';");
        //sb.Append("categoryAxis.tickLenght = 0;");
        //sb.Append("categoryAxis.inside = true;");
        // value
        sb.Append(" var valueAxis = new AmCharts.ValueAxis();");
        sb.Append("valueAxis.stackType = '3d';");
        sb.Append("valueAxis.dashLength = 5;");
        sb.Append("valueAxis.title = 'Revenue(in Crore) ';");
        sb.Append(" chart.addValueAxis(valueAxis);");
        // GRAPHS         
        //// first graph
        sb.Append(" var graph = new AmCharts.AmGraph();");
        sb.Append(" graph.valueField = 'visits';");
        sb.Append(" graph.type = 'column';");
        sb.Append("graph.lineAlpha = 0;");
        sb.Append("graph.colorField = 'color';");
        //sb.Append("graph4.colors = ['#A34775'];");
        sb.Append("graph.fillAlphas = 1;");
        sb.Append("graph.balloonText = '[[category]] :(Revenue : [[Amount]]  Rupees) ';");
        //"Income: [[value]]"
        sb.Append("chart.addGraph(graph);");

        //// Second graph
        sb.Append(" var graph2 = new AmCharts.AmGraph();");
        sb.Append(" graph2.valueField = 'trans';");
        sb.Append(" graph2.type = 'column';");
        sb.Append("graph2.lineAlpha = 0;");
        sb.Append("graph2.colorField = 'color1';");
        //sb.Append("graph4.colors = ['#A34775'];");
        sb.Append("graph2.fillAlphas = 1;");
        sb.Append("graph2.balloonText = '[[category]]  : (Transaction :[[value]] Unit)';");
        //"Income: [[value]]"
        sb.Append("chart.addGraph(graph2);");
        // CURSOR
        sb.Append("var chartCursor = new AmCharts.ChartCursor();");
        sb.Append("chart.addChartCursor(chartCursor);");

        // SCROLLBAR
        sb.Append("var chartScrollbar = new AmCharts.ChartScrollbar();");
        sb.Append("chartScrollbar.scrollbarHeight = 30;");
        sb.Append("chartScrollbar.graph = graph;");
        sb.Append("chartScrollbar.graphType = 'line';");
        sb.Append("chartScrollbar.gridCount = 4;");
        sb.Append("chartScrollbar.color = '#FFFFFF';");
        sb.Append("chart.addChartScrollbar(chartScrollbar);");
        sb.Append("chart.write('divDERevCol');");
        sb.Append("});");
        sb.Append("</script>");
        ltRevCol.Text = sb.ToString();
    }
}
