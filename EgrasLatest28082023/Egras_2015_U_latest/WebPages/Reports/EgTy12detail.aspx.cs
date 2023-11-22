using System;
using System.Web.UI;
using EgBL;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebPages_Reports_EgTy12detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
 }
    public void loadreport()
    {
        DataTable dt = new DataTable();
        EgReports ObjTy12 = new EgReports();
        string[] revdateFrom, revdateTo;
        revdateFrom = txtFromDate.Text.Trim().Split('/');
        ObjTy12.Fromdate = Convert.ToDateTime(revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]);
        revdateTo = txtToDate.Text.Trim().Split('/');
        ObjTy12.Todate = Convert.ToDateTime(revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]);
        dt = ObjTy12.BindTy12DetailGrid();
        if (dt.Rows.Count > 0)
        { 
        grdTy12Rpt.DataSource = dt;
        grdTy12Rpt.DataBind();
        }
        dt.Dispose();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        loadreport();
    }
    protected void grdTy12Rpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadreport();
        grdTy12Rpt.PageIndex = e.NewPageIndex;
        grdTy12Rpt.DataBind();
    }
    protected void grdTy12Rpt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName == "grnbind")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));

            string script = "window.open('../EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";



            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);

        }
    }
}
