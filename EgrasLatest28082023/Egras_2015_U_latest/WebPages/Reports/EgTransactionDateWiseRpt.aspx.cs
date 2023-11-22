using System;
using System.Data;
using System.Web.UI;
using EgBL;
public partial class WebPages_EgTransactionDateWiseRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        EgRevenueHeadRpt objRHR = new EgRevenueHeadRpt();
        DataTable dt = new DataTable();
        objRHR.Date = txtDate.Text;
        objRHR.BankChallanList(dt);
        if (dt.Rows.Count > 0)
        {
            rptSchemaDiv.Visible = true;
            rptschema.DataSource = dt;
            rptschema.DataBind();
            lblChallan.Text = Convert.ToString(objRHR.totalcount);
            lblAmount.Text = Convert.ToString(objRHR.TotalAmount.ToString("0.00"));
        }
        else
        {
            rptSchemaDiv.Visible = false;
            ScriptManager.RegisterClientScriptBlock(updatepanal1, this.GetType(), "PopupScript", "alert('There is no Record Found.')", true);
            return;
        }
        dt.Clear();
        dt.Dispose();
    }
}
