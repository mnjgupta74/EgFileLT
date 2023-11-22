using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EgBL;



public partial class WebPages_ErrorLogNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void Button_Submit (object sender, EventArgs e)
    {
        
        BindRepeater();
    }

    private void BindRepeater()
    {
        DataTable dt = new DataTable();
        ErrorLogNewBL objErrorLog = new ErrorLogNewBL();
        string[] fromDate = txtFromDate.Text.Split('/');
        objErrorLog.Fromdate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
        string[] ToDate = txtToDate.Text.Split('/');
        objErrorLog.Todate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
        dt=objErrorLog.ErrorReport();
        rptErrorInfo.DataSource = dt;
        rptErrorInfo.DataBind();
        if (dt.Rows.Count == 0)
        {
            lblEmptyData.Visible = true;
            lblEmptyData.Text = "No Record Found.";
            Table2.Visible = false;
        }
        else
        {
            lblEmptyData.Visible = false;
            Table2.Visible = true;
            rptErrorInfo.DataSource = dt;
            rptErrorInfo.DataBind();

        }           

    }


    protected void rptErrorInfo_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}