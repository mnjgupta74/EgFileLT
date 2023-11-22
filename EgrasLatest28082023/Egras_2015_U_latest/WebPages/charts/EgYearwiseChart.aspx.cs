using System;
using System.Data;
using System.Text;
using EgBL;
using System.Web.UI.WebControls;
public partial class WebPages_charts_EgYearwiseChart : System.Web.UI.Page
{
    StringBuilder sb;
    EgYearWiseChart objYearWise;
    DataTable dt2;
    Int32 TotalTrans = 0;
    decimal TotalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        fldColumn.Visible = false;
        fldPie.Visible = false;
        lblmsg.Visible = false;
        tr1.Visible = false;
        tr2.Visible = false;
        if (!Page.IsPostBack)
        {
            ddlYear.count = 3;
            ddlYear.FinYearDropdown();
        }
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        objYearWise = new EgYearWiseChart();
        dt2 = new DataTable();
        objYearWise.Type = (rdbYear.SelectedValue == "0" ? "1" : "2");
        objYearWise.Year = ddlYear.SelectedValue;
        objYearWise.FillRepeter(dt2);
        if (dt2.Rows.Count > 0)
        {
            fldColumn.Visible = true;
            fldPie.Visible = true;
            lblmsg.Visible = false;
            tr1.Visible = false;
            tr2.Visible = true;
            FillRepeter(dt2);
            Column3dChartBENextExp1(dt2);
            btnShow.Enabled = false;
            rdbYear.Enabled = false;
            ddlYear.Enabled = false;
        }
        else
        {
            lblmsg.Visible = true;
            fldColumn.Visible = false;
            fldPie.Visible = false;
            tr1.Visible = true;
            tr2.Visible = false;
            btnEnable();
        }
    }
    public void FillRepeter(DataTable dt1)
    {
        rept.DataSource = dt1;
        rept.DataBind();
        dt1.Dispose();
    }
    public void Column3dChartBENextExp1(DataTable dt)
    {
        sb = new StringBuilder();
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
            //string[] dateArray = dt.Rows[i]["monthN"].ToString().Trim().Split('/');
            sb.Append("var Month = '" + dt.Rows[i]["monthN"].ToString() + "';");
            //sb.Append("var Month =  new Date(Number(" + dateArray[2].ToString() + "), Number(" + dateArray[1].ToString() + ") - 1, Number(" + dateArray[0].ToString() + ")).toDateString();");
            sb.Append("var visits =  " + Convert.ToDecimal(dt.Rows[i]["Amount"].ToString()) / 1000 + ";");
            sb.Append("var trans = " + Convert.ToInt64(dt.Rows[i]["totChallan"].ToString()) * 100 + ";");
            sb.Append(" var color =  '#FFA500';");
            sb.Append(" var color1 =  '#808000';");
            //sb.Append(" var color =  colors[" + 9 + "];");
            //            sb.Append(" var color1 =  colors[" + 4 + "];");
            sb.Append(" var Amount = '" + dt.Rows[i]["Amount"].ToString() + "';");

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
        sb.Append("valueAxis.title = 'Revenue(per /1000) ';");
        sb.Append(" chart.addValueAxis(valueAxis);");
        // GRAPHS         
        //// first graph
        sb.Append(" var graph2 = new AmCharts.AmGraph();");
        sb.Append(" graph2.valueField = 'trans';");
        sb.Append(" graph2.type = 'column';");
        sb.Append("graph2.lineAlpha = 0;");
        sb.Append("graph2.colorField = 'color';");
        //sb.Append("graph4.colors = ['#A34775'];");
        sb.Append("graph2.fillAlphas = 1;");
        sb.Append("graph2.balloonText = ' [[category]]  : (Transaction :[[value]]/100 Unit)';");
        //"Income: [[value]]"
        sb.Append("chart.addGraph(graph2);");

        //// Second graph
        sb.Append(" var graph = new AmCharts.AmGraph();");
        sb.Append(" graph.valueField = 'visits';");
        sb.Append(" graph.type = 'column';");
        sb.Append("graph.lineAlpha = 0;");
        sb.Append("graph.colorField = 'color1';");
        //sb.Append("graph4.colors = ['#A34775'];");
        sb.Append("graph.fillAlphas = 1;");
        sb.Append("graph.balloonText = ' [[category]] :(Revenue : [[Amount]]  Rupees)';");
        //"Income: [[value]]"
        sb.Append("chart.addGraph(graph);");
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
    protected void rept_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {

            TotalTrans = 0;
            TotalAmount = 0;
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TotalTrans += (Int32)((DataRowView)e.Item.DataItem)["totChallan"];
            TotalAmount += (decimal)((DataRowView)e.Item.DataItem)["Amount"];
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {

            Label lblTotal = e.Item.FindControl("lbltotalTrans") as Label;
            lblTotal.Text = string.Format("{0: #,#.00}", TotalTrans);

            Label lblTotalAmount = e.Item.FindControl("lblTotalAmt") as Label;
            lblTotalAmount.Text = string.Format("{0: #,#.00}", TotalAmount);

        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        btnEnable();
    }
    public void btnEnable()
    {
        btnShow.Enabled = true;
        rdbYear.Enabled = true;
        ddlYear.Enabled = true;
    }
}
