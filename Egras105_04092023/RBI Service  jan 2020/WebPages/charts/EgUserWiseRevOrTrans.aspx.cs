using System;
using System.Data;
using System.Text;
using System.Web.UI;
using EgBL;
public partial class WebPages_Charts_EgUserWiseRevOrTrans : System.Web.UI.Page
{
    StringBuilder sb = new StringBuilder();
    DataTable dt = new DataTable();
    EgDeptwiseRevenueTotal objrev = new EgDeptwiseRevenueTotal();
    string[] fdate;

    protected void Page_Load(object sender, EventArgs e)
    {
        dt.Dispose();

        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        lblhead.Visible = false;

        if (!Page.IsPostBack)
        {
            EgToChallanReportBL objEgToChallanReportBL = new EgToChallanReportBL();
            if (Request.QueryString["ftrans"] != null)
            {
                fdate = Request.QueryString["ftrans"].ToString().Split(',');
                txtFromDate.Text = fdate[0].ToString().Trim();
                txtToDate.Text = fdate[1].ToString().Trim();
                objrev.Deptcode = fdate[2].ToString().Trim();
                objrev.Location = fdate[4].ToString().Trim();
            }
            else
            {
                DateTime fromDate = DateTime.Parse("12/01/2012");
                txtFromDate.Text = fromDate.ToString("dd'/'MM'/'yyyy");
                DateTime toDate = DateTime.Parse(DateTime.Now.ToString());
                txtToDate.Text = toDate.ToString("dd'/'MM'/'yyyy");


                if (Session["UserType"].ToString() == "3")
                {
                    objEgToChallanReportBL.UserId = Convert.ToInt32(Session["UserId"]);
                    objrev.Location = objEgToChallanReportBL.GetTreasCode().Trim();
                }
                else
                {
                    objrev.Location = "0";

                }
            }
            lblhead.Visible = false;
            fldColumn.Visible = false;

            string[] from = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
            objrev.Fromdate = Convert.ToDateTime(from[1].ToString().Trim() + "/" + from[0].ToString().Trim() + "/" + from[2].ToString().Trim());
            string[] ToDate1 = txtToDate.Text.Trim().Replace("-", "/").Split('/');
            objrev.Todate = Convert.ToDateTime(ToDate1[1].ToString().Trim() + "/" + ToDate1[0].ToString().Trim() + "/" + ToDate1[2].ToString().Trim());

            objrev.RevUserWise(dt);
            if (dt.Rows.Count > 0)
            {
                fldColumn.Visible = true;
                Column3dChartBENextExp();
            }
            else
            {
                fldColumn.Visible = false;
                lblhead.Visible = true;
            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {



        string[] from = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objrev.Fromdate = Convert.ToDateTime(from[1].ToString().Trim() + "/" + from[0].ToString().Trim() + "/" + from[2].ToString().Trim());
        string[] ToDate1 = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objrev.Todate = Convert.ToDateTime(ToDate1[1].ToString().Trim() + "/" + ToDate1[0].ToString().Trim() + "/" + ToDate1[2].ToString().Trim());
        objrev.RevUserWise(dt);
        if (dt.Rows.Count > 0)
        {
            fldColumn.Visible = true;
            Column3dChartBENextExp();
        }
        else
        {
            fldColumn.Visible = false;
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
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (i == dt.Rows.Count - 1)
            {
                sb.Append(" {Month : '" + dt.Rows[i]["LoginID"].ToString() + "', visits: " + dt.Rows[i]["Amount"].ToString() + ",trans: " + dt.Rows[i]["TransNo"].ToString() + ", color: colors[" + 9 + "], color1: colors[" + 4 + "],Amount :'" + dt.Rows[i]["Amount1"].ToString() + "'}");
            }
            else
            {
                sb.Append(" {Month : '" + dt.Rows[i]["LoginID"].ToString() + "', visits: " + dt.Rows[i]["Amount"].ToString() + ",trans: " + dt.Rows[i]["TransNo"].ToString() + ", color: colors[" + 9 + "], color1: colors[" + 4 + "],Amount :'" + dt.Rows[i]["Amount1"].ToString() + "'},");
            }
        }
        //sb.Append("{country: 'USA',visits: 4025,color: '#FF0F00'},");
        //sb.Append("{country: 'China',visits: 1882,color: '#FF6600'}");
        //sb.Append("{Month:  'Revenue',visits: '" + dt.Rows[0]["Amount"].ToString() + "',color: '#FF0F00'},");
        //sb.Append("{Month:  'Transaction ',visits: '" + dt.Rows[0]["TransNo"].ToString() + "',color: '#FF6600'}");
        sb.Append("];");
        sb.Append("AmCharts.ready(function () {");
        // SERIAL CHART
        sb.Append("chart4 = new AmCharts.AmSerialChart();");
        //sb.Append("chart4.addTitle('" + "hello" + "', 16);");
        sb.Append("chart4.dataProvider = chartData4;");
        sb.Append("chart4.categoryField = 'Month';");
        sb.Append(" chart4.angle = 30;");
        sb.Append("chart4.depth3D = 20;");
        if (Request.QueryString["ftrans"] != null)
        {
            if (fdate.Length > 6)
            {
                sb.Append("chart4.addTitle('Treasury : " + fdate[5].ToString() + "-" + fdate[6].ToString() + "', 16);");

            }
            else
            {
                sb.Append("chart4.addTitle('Treasury : " + fdate[5].ToString() + "', 16);");

            }
        }
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
        sb.Append("valueAxis.stackType = '3d';");
        sb.Append("valueAxis.dashLength = 5;");
        sb.Append("valueAxis.title = 'Revenue(in Crore) ';");
        sb.Append(" chart4.addValueAxis(valueAxis);");
        // GRAPHS         
        //// first graph
        sb.Append(" var graph1 = new AmCharts.AmGraph();");
        sb.Append(" graph1.valueField = 'visits';");
        sb.Append(" graph1.type = 'column';");
        sb.Append("graph1.lineAlpha = 0;");
        sb.Append("graph1.colorField = 'color';");
        //sb.Append("graph4.colors = ['#A34775'];");
        sb.Append("graph1.fillAlphas = 1;");
        sb.Append("graph1.balloonText = '[[category]] :(Revenue : [[Amount]]  Rupees) ';");
        //"Income: [[value]]"
        sb.Append("chart4.addGraph(graph1);");

        //// Second graph
        sb.Append(" var graph4 = new AmCharts.AmGraph();");
        sb.Append(" graph4.valueField = 'trans';");
        sb.Append(" graph4.type = 'column';");
        sb.Append("graph4.lineAlpha = 0;");
        sb.Append("graph4.colorField = 'color1';");
        //sb.Append("graph4.colors = ['#A34775'];");
        sb.Append("graph4.fillAlphas = 1;");
        sb.Append("graph4.balloonText = '[[category]]  : (Transaction :[[value]] Unit)';");
        //"Income: [[value]]"
        sb.Append("chart4.addGraph(graph4);");

        sb.Append("chart4.write('divDERevCol');");
        sb.Append("});");
        sb.Append("</script>");
        ltRevCol.Text = sb.ToString();
    }
}
