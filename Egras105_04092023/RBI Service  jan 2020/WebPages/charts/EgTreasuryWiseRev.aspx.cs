using System;
using System.Data;
using System.Text;
using EgBL;
public partial class WebPages_Charts_EgTreasuryWiseRev : System.Web.UI.Page
{
    EgDeptwiseRevenueTotal objrev;
    EgToChallanReportBL objEgToChallanReportBL;
    StringBuilder sb = new StringBuilder();
    DataTable dt = new DataTable();
    string fdate = "";
    string[] from;
    string[] ToDate1;
    string[] data;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["fdate"] != null)
        {

            loadChart();
        }

    }

    public void loadChart()
    {
        objEgToChallanReportBL = new EgToChallanReportBL();
        objrev = new EgDeptwiseRevenueTotal();
        ///////Date passing       
        fdate = Request.QueryString["fdate"].ToString();
        data = fdate.Trim().Split(',');
        from = data[0].ToString().Trim().Replace("-", "/").Split('/');
        objrev.Fromdate = Convert.ToDateTime(from[1].ToString().Trim() + "/" + from[0].ToString().Trim() + "/" + from[2].ToString().Trim());
        ToDate1 = data[1].ToString().Trim().Replace("-", "/").Split('/');
        objrev.Todate = Convert.ToDateTime(ToDate1[1].ToString().Trim() + "/" + ToDate1[0].ToString().Trim() + "/" + ToDate1[2].ToString().Trim());

        objrev.Location = "0";

        objrev.Deptcode = data[2].ToString();
        objrev.Flag = "1";
        objrev.RevenuePieNew(dt);
        if (dt.Rows.Count > 0)
        {
            fldColumn.Visible = true;
            Column3dChartBENextExp();
        }
        else
        {
            lblhead.Visible = true;
        }
    }
    public void Column3dChartBENextExp()
    {
        ltRevCol.Text = "";
        sb.Remove(0, sb.Length);
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("var chart4;");
        sb.Append("var colors=['#8A3324','#FA8072','#5C4033','#E47833','#A68064','#B6AFA9','#FFDAB9','#FFC125','#8B8B83','#4F4F2F','#D9D919','#808000','#556B2F'];");
        sb.Append("var chartData4 = [");
        //for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //{
        //    if (i == dt.Rows.Count - 1)
        //    {
        //        sb.Append(" {Month : '" + dt.Rows[i]["TreasuryName"].ToString() + "', visits: " + dt.Rows[i]["Amount1"].ToString() + ", color: colors[" + i + "]}");
        //    }
        //    else
        //    {
        //        sb.Append(" {Month : '" + dt.Rows[i]["TreasuryName"].ToString() + "', visits: " + dt.Rows[i]["Amount1"].ToString() + ", color: colors[" + i + "]},");
        //    }
        //}
        //sb.Append("{country: 'USA',visits: 4025,color: '#FF0F00'},");
        //sb.Append("{country: 'China',visits: 1882,color: '#FF6600'}");
        sb.Append("];");
        sb.Append("function parseData() {");
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            // we have to subtract 1 from month, as months in javascript are zero-based
            //string[] dateArray = dt.Rows[i]["monthN"].ToString().Trim().Split('/');
            //sb.Append("var Month = '" + dt.Rows[i]["monthN"].ToString() + "';");
            sb.Append("var deptCode='" + dt.Rows[i]["TreasuryName"].ToString() + "';");
            //sb.Append("var deptCode=deptCode.split('-');");
            sb.Append("var Month =  deptCode;");
            sb.Append("var visits =  " + dt.Rows[i]["Amount1"].ToString() + ";");
            //sb.Append("var  color= colors[" + i + "];");
            sb.Append(" var color =  colors[" + i + "];");
            //sb.Append(" var test  = '" + dt.Rows[i]["officename"].ToString() + "';");
            //sb.Append(" var Amount1  ='" + dt.Rows[i]["Amount1"].ToString() + "';");

            sb.Append("chartData4.push({");
            sb.Append("Month: Month,");
            sb.Append("visits: visits,");
            //sb.Append(" trans: trans,");
            sb.Append("color: color");
            //sb.Append(",test: test,");
            //sb.Append("Amount1: Amount1");
            sb.Append("});");
        }
        sb.Append("}");

        sb.Append("function basicPopup1(event) {");
        //sb.Append("var startDate = '01/12/2012';");
        //sb.Append("var startDate = '" + from[1].ToString().Trim() + "/" + from[0].ToString().Trim() + "/" + from[2].ToString().Trim() + "';");
        //sb.Append(" var endDate = '12/03/2013';");
        sb.Append("var loc=event.item.category;");
        sb.Append("var loc=loc.split('-');");
        sb.Append("var fdat='" + fdate.ToString().Trim() + "';");
        //sb.Append(" var endDate = '" + ToDate1[1].ToString().Trim() + "/" + ToDate1[0].ToString().Trim() + "/" + ToDate1[2].ToString().Trim() + "';");
        sb.Append(" popupWindow12 = window.open('EgUserWiseRevOrTrans.aspx?ftrans='+fdat+','+loc[1]+','+loc[0]+'', 'popUpWindow12', 'location=no,height=460,width=920,left=252,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');");
        //sb.Append("alert('loc[0]='+loc[0]+'loc[1]='+loc[1]);");
        //sb.Append("alert(event.item.category + ': ' + event.item.values.value +'" + fdate.ToString().Trim() + ",'+loc[1]);");
        sb.Append("}");

        sb.Append("AmCharts.ready(function () {");
        // SERIAL CHART
        sb.Append("parseData();");
        sb.Append("chart4 = new AmCharts.AmSerialChart();");
        //sb.Append("chart4.addTitle('" + "hello" + "', 16);");
        sb.Append("chart4.dataProvider = chartData4;");
        sb.Append("chart4.categoryField = 'Month';");
        sb.Append(" chart4.angle = 30;");
        sb.Append("chart4.depth3D = 20;");
        sb.Append("chart4.addTitle('Department : " + data[3].ToString() + "', 16);");
        // AXES
        //
        sb.Append("chart4.addListener('clickGraphItem', basicPopup1);");
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
        sb.Append("valueAxis.title = 'Revenue(in Rupees) ';");
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
        sb.Append("graph4.balloonText = '[[category]]:([[value]]  Rupees)';");
        //"Income: [[value]]"
        sb.Append("chart4.addGraph(graph4);");
        sb.Append("chart4.write('divDERevCol');");
        sb.Append("});");
        sb.Append("</script>");
        ltRevCol.Text = sb.ToString();
    }
}
