using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_Reports_EgMEChallanDetails : System.Web.UI.Page
{
    EgMEchallanDetailsBL objEgMEchallanDetailsBL;
    EgEncryptDecrypt ObjEncryptDecrypt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            //Do nothing
        }

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void BindData()
    {
        DataTable dt = new DataTable();
        objEgMEchallanDetailsBL = new EgMEchallanDetailsBL();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objEgMEchallanDetailsBL.fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objEgMEchallanDetailsBL.todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        TimeSpan datediff = objEgMEchallanDetailsBL.todate - objEgMEchallanDetailsBL.fromdate;

        if (datediff.Days > 30)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('FromDate and ToDate Difference can not be greater than 30 days.');", true);
            return;
        }
        dt = objEgMEchallanDetailsBL.MEChallanDetail();
        if (dt.Rows.Count > 0)
        {
            grdME.DataSource = dt;
            grdME.DataBind();
        }
    }

    protected void grdME_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdME.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void grdME_onRowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lnkGetGRN")
        {
            ObjEncryptDecrypt = new EgEncryptDecrypt();
            //string GRN = e.CommandArgument.ToString();
            Session["GrnNumber"] = e.CommandArgument.ToString();
            Response.Redirect("~/webpages/reports/EgManualChallan.aspx");
            //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}", GRN.ToString()));
            //strURLWithData = "~/Webpages/Reports/EgEChallanViewRptNew.aspx?" + strURLWithData;
            //Response.Redirect(strURLWithData);
        }
    }
}
