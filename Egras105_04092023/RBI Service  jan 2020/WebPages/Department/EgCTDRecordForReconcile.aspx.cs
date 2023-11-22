using System;
using System.Data;
using System.Web.UI;
using EgBL;

public partial class WebPages_Department_EgCTDRecordForReconcile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        EgTimeSlab objTimeSlab = new EgTimeSlab();
        string[] date = txtDate.Text.Trim().Replace("-", "/").Split('/');
        objTimeSlab.RequestedTime = Convert.ToDateTime(date[2].ToString() + "/" + date[1].ToString() + "/" + date[0].ToString());

        DataTable dtResult = objTimeSlab.CTDRecordForReconcile();
        if (dtResult.Rows.Count > 0)
        {
            lblRecords.Text = "Total Records:      " +  dtResult.Rows[0][0].ToString();
            double amount = Convert.ToDouble(dtResult.Rows[0][1]);
            lblAmount.Text = "Amount:      " + amount.ToString("0.00");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('No Record found.')", true);
        }
    }
}
