using System;
using System.Data;
using System.Text;
using EgBL;
public partial class WebPages_EgDeptwiseRevenueTotalChart : System.Web.UI.Page
{
    EgDeptwiseRevenueTotal objrev;
    EgToChallanReportBL objEgToChallanReportBL;
    StringBuilder sb = new StringBuilder();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    string[] from;
    string[] ToDate1;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblhead.Visible = false;

        dt.Dispose();
        dt.Clear();
        ltRev.Text = "";
        ltRevCol.Text = "";
        ltTran.Text = "";
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        fldPie.Visible = false;
        fldColumn.Visible = false;
        fldTran.Visible = false;
        if (!Page.IsPostBack)
        {

            objrev = new EgDeptwiseRevenueTotal();
            string thisYear = DateTime.Now.Year.ToString();
            DateTime fromDate = DateTime.Parse("04/01/"+(thisYear).ToString());
            txtFromDate.Text = fromDate.ToString("dd'/'MM'/'yyyy");
            DateTime toDate = DateTime.Parse(DateTime.Now.ToString());
            txtToDate.Text = toDate.ToString("dd'/'MM'/'yyyy");

            loadChart();

        }
    }
    public void ShowChartRev()
    {//BENext/Expenditure chart
        try
        {
            sb.Remove(0, sb.Length);
            ////////-------------------chart print-----------------
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("var chart;");
            sb.Append("var chartData = [];");

            //lbexp.Text = dt.Rows[0]["Expenditure"].ToString();
            //lblBEExp.Text = dt.Rows[0]["BENextFy"].ToString();
            //lblRev.Text = dt.Rows[0]["amount"].ToString();

            sb.Append("function parseData2() {");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                sb.Append("var deptCode='" + dt.Rows[i]["deptnameshort"].ToString() + "';");
                sb.Append("var deptCode=deptCode.split('-');");
                sb.Append("var FinYear =  deptCode[0];");
                sb.Append("var visits =  " + dt.Rows[i]["amount"].ToString() + ";");
                //sb.Append("var  color= colors[" + i + "];");
                //sb.Append(" var color =  colors[" + i + "];");
                //sb.Append(" var test  = '" + dt.Rows[i]["officename"].ToString() + "';");
                //sb.Append(" var Amount1  ='" + dt.Rows[i]["Amount1"].ToString() + "';");

                sb.Append("chartData.push({");
                sb.Append("FinYear: FinYear,");
                sb.Append("visits: visits");
                //sb.Append(", trans: trans,");
                //sb.Append("color: color,");
                //sb.Append(",test: test");
                //sb.Append("Amount1: Amount1");
                sb.Append("});");
            }
            sb.Append("}");


            sb.Append("AmCharts.ready(function() {");
            // PIE CHART
            sb.Append(" parseData2() ;");
            sb.Append(" chart = new AmCharts.AmPieChart();");
            // title of the chart
            //sb.Append("chart.addTitle('Dept and Revenue ', 16);");
            sb.Append("chart.dataProvider = chartData;");
            sb.Append("chart.titleField = 'FinYear';");
            sb.Append("chart.valueField = 'visits';");
            sb.Append("chart.sequencedAnimation = true;");
            sb.Append("chart.startEffect = 'elastic';");
            sb.Append("chart.innerRadius = '30%';");
            sb.Append("chart.startDuration = 2;");
            sb.Append(" chart.labelRadius = 15;");
            sb.Append(" chart.labelWidth = 5;");



            sb.Append("chart.balloonText = '[[title]]:[[value]]([[percents]])%';");
            //sb.Append("chart.labelText = '[[percents]]%';");
            // the following two lines makes the chart 3D
            sb.Append("chart.depth3D = 10;");
            sb.Append("chart.angle = 25;");
            //sb.Append(" chart.colors = ['#" + color1.ToString() + "','#" + color2.ToString() + "'];");
            //sb.Append(" chart.colors = ['#ff2255','#ff6699'];");
            sb.Append("chart.write('divDERev');");
            sb.Append("});");
            sb.Append(" </script>");
            ltRev.Text = sb.ToString();
            sb.Remove(0, sb.Length);
        }
        catch
        {

        }
    }
    public void ShowChartTrans()
    {
        try
        {
            sb.Remove(0, sb.Length);
            ////////-------------------chart print-----------------
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("var chart5;");
            sb.Append("var chartData5 = [];");

            //lbexp.Text = dt.Rows[0]["Expenditure"].ToString();
            //lblBEExp.Text = dt.Rows[0]["BENextFy"].ToString();
            //lblRev.Text = dt.Rows[0]["amount"].ToString();

            sb.Append("function parseData5() {");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                sb.Append("var deptCode='" + dt.Rows[i]["deptnameshort"].ToString() + "';");
                sb.Append("var deptCode=deptCode.split('-');");
                sb.Append("var FinYear =  deptCode[0];");
                sb.Append("var visits =  " + dt.Rows[i]["Trans"].ToString() + ";");
                //sb.Append("var  color= colors[" + i + "];");
                //sb.Append(" var color =  colors[" + i + "];");
                //sb.Append(" var test  = '" + dt.Rows[i]["officename"].ToString() + "';");
                //sb.Append(" var Amount1  ='" + dt.Rows[i]["Amount1"].ToString() + "';");

                sb.Append("chartData5.push({");
                sb.Append("FinYear: FinYear,");
                sb.Append("visits: visits");
                //sb.Append(", trans: trans,");
                //sb.Append("color: color,");
                //sb.Append(",test: test");
                //sb.Append("Amount1: Amount1");
                sb.Append("});");
            }
            sb.Append("}");


            sb.Append("AmCharts.ready(function() {");
            // PIE CHART
            sb.Append(" parseData5() ;");
            sb.Append(" chart5 = new AmCharts.AmPieChart();");
            // title of the chart
            //sb.Append("chart.addTitle('Dept and Revenue ', 16);");
            sb.Append("chart5.dataProvider = chartData5;");
            sb.Append("chart5.titleField = 'FinYear';");
            sb.Append("chart5.valueField = 'visits';");
            sb.Append("chart5.sequencedAnimation = true;");
            sb.Append("chart5.startEffect = 'elastic';");
            sb.Append("chart5.innerRadius = '30%';");
            sb.Append("chart5.startDuration = 2;");
            sb.Append(" chart5.labelRadius = 15;");
            sb.Append(" chart5.labelWidth = 5;");

            sb.Append("chart5.balloonText = '[[title]]:[[value]]([[percents]])%';");
            //sb.Append("chart.labelText = '[[percents]]%';");
            // the following two lines makes the chart 3D
            sb.Append("chart5.depth3D = 10;");
            sb.Append("chart5.angle = 25;");
            //sb.Append(" chart.colors = ['#" + color1.ToString() + "','#" + color2.ToString() + "'];");
            //sb.Append(" chart.colors = ['#ff2255','#ff6699'];");
            sb.Append("chart5.write('divTran');");
            sb.Append("});");
            sb.Append(" </script>");
            ltTran.Text = sb.ToString();
            sb.Remove(0, sb.Length);
        }
        catch
        {

        }
    }
    public void Column3dChartBENextExp()
    {

        ltRevCol.Text = "";

        sb.Remove(0, sb.Length);
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("var chart4;");
        sb.Append("var dataSet;");
        sb.Append("var colors=['#8A3324','#FA8072','#5C4033','#E47833','#A68064','#B6AFA9','#FFDAB9','#FFC125','#8B8B83','#4F4F2F','#D9D919','#808000','#556B2F'];");
        //handling popup window event
        sb.Append("function basicPopup1(event) {");
        sb.Append("var startDate = '01/12/2012';");
        sb.Append("var startDate = '" + from[0].ToString().Trim() + "/" + from[1].ToString().Trim() + "/" + from[2].ToString().Trim() + "';");
        //sb.Append(" var endDate = '12/03/2013';");
        sb.Append("var dept=event.item.category;");
        sb.Append("var dept=dept.split('-');");

        sb.Append(" var endDate = '" + ToDate1[0].ToString().Trim() + "/" + ToDate1[1].ToString().Trim() + "/" + ToDate1[2].ToString().Trim() + "';");
        sb.Append(" popupWindow = window.open('EgTreasuryWiseRev.aspx?fdate='+startDate+','+endDate+','+dept[1]+','+dept[0]+'', 'popUpWindow', 'location=no,height=460,width=920,left=252,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');");

       // sb.Append("alert(event.item.category + ': ' + event.item.values.value  + ': value='+ event.item.values.name+' INdex='+event.index);");
        sb.Append("}");
        //end of popup window event
        sb.Append("var chartData4 = [];");


        sb.Append("function parseData() {");
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            // we have to subtract 1 from month, as months in javascript are zero-based
            //string[] dateArray = dt.Rows[i]["monthN"].ToString().Trim().Split('/');
            //sb.Append("var Month = '" + dt.Rows[i]["monthN"].ToString() + "';");
            sb.Append("var deptCode='" + dt.Rows[i]["deptnameshort"].ToString() + "';");
            //sb.Append("var deptCode=deptCode.split('-');");
            sb.Append("var Month =  deptCode;");
            sb.Append("var visits =  " + dt.Rows[i]["amount"].ToString() + ";");
            //sb.Append("var  color= colors[" + i + "];");
            sb.Append(" var color =  colors[" + i + "];");
            sb.Append(" var test  = '" + dt.Rows[i]["officename"].ToString() + "';");
            sb.Append(" var Amount1  ='" + dt.Rows[i]["amount"].ToString() + "';");

            sb.Append("chartData4.push({");
            sb.Append("Month: Month,");
            sb.Append("visits: visits,");
            //sb.Append(" trans: trans,");
            sb.Append("color: color,");
            sb.Append("test: test,");
            sb.Append("Amount1: Amount1");
            sb.Append("});");
        }
        sb.Append("}");
        sb.Append("AmCharts.ready(function () {");
        // SERIAL CHART
        sb.Append("parseData();");
        sb.Append("chart4 = new AmCharts.AmSerialChart();");
        //sb.Append("chart4.addTitle('" + "hello" + "', 16);");
        sb.Append("chart4.dataProvider = chartData4;");
        sb.Append("chart4.categoryField = 'Month';");
        sb.Append("chart4.angle = 30;");
        sb.Append("chart4.depth3D = 20;");
         //sb.Append("dataSet = new AmCharts.DataSet();");
        //sb.Append("dataSet.dataProvider = chartData4;");
        //sb.Append("dataSet.categoryField = 'Month';");
        //sb.Append("chart.dataSets = [dataSet];");
        //
        sb.Append("chart4.addListener('clickGraphItem', basicPopup1);");
        // AXES
        // category
        sb.Append("var categoryAxis = chart4.categoryAxis;");
        sb.Append("categoryAxis.gridPosition = 'start';");
        sb.Append("categoryAxis.dashLength = 5;");
        sb.Append("categoryAxis.labelRotation = 40;");
        sb.Append("categoryAxis.axisColor = '#000000';");
        sb.Append("categoryAxis.gridColor = '#000000';");
        sb.Append("categoryAxis.axisAlpha = 0.5;");
        // value
        sb.Append(" var valueAxis = new AmCharts.ValueAxis();");
        sb.Append("valueAxis.dashLength = 5;");
        sb.Append("valueAxis.title = 'Revenue(in Crore) ';");
        sb.Append(" chart4.addValueAxis(valueAxis);");
        // GRAPHS         
        //// first graph
        sb.Append(" var graph4 = new AmCharts.AmGraph();");
        sb.Append(" graph4.valueField = 'visits';");
        sb.Append(" graph4.type = 'column';");
        sb.Append("graph4.lineAlpha = 0;");
        sb.Append("graph4.colorField = 'color';");
        //sb.Append("graph4.colors = ['#A34775'];");
        sb.Append("graph4.fillAlphas = 1;");
        sb.Append("graph4.balloonText = '[[test]]:([[Amount1]])  Rupees(IN CRORE)';");
        //"Income: [[value]]"
        sb.Append("chart4.addGraph(graph4);");
        sb.Append("chart4.write('divDERevCol');");
        sb.Append("});");
        sb.Append("</script>");
        ltRevCol.Text = sb.ToString();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        dt.Dispose();
        dt.Clear();
        ltRev.Text = "";
        ltRevCol.Text = "";
        ltTran.Text = "";
        fldPie.Visible = true;
        fldColumn.Visible = true;
        fldTran.Visible = true;
        loadChart();
    }

    public void loadChart()
    {
        objEgToChallanReportBL = new EgToChallanReportBL();
        objrev = new EgDeptwiseRevenueTotal();
        ///////Date passing
        from = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objrev.Fromdate = Convert.ToDateTime(from[1].ToString().Trim() + "/" + from[0].ToString().Trim() + "/" + from[2].ToString().Trim());
        ToDate1 = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objrev.Todate = Convert.ToDateTime(ToDate1[1].ToString().Trim() + "/" + ToDate1[0].ToString().Trim() + "/" + ToDate1[2].ToString().Trim());
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
            objrev.UserType = Convert.ToInt32(Session["UserType"].ToString());
            string dept = objrev.GetDeptCode();
            objrev.Deptcode = dept.Substring(0, (dept.Length - 1));
        }
        else
        {
            objrev.Deptcode = "0";
        }
        objrev.RevenuePie(dt);
        if (dt.Rows.Count > 0)
        {
            fldPie.Visible = true;
            fldColumn.Visible = true;
            fldTran.Visible = true;
            ShowChartRev();
            ShowChartTrans();
            Column3dChartBENextExp();
        }
        else
        {
            lblhead.Visible = true;
        }
    }
}
