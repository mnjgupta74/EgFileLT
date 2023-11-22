using System;
using System.Web.UI;
using EgBL;

public partial class WebPages_Reports_EgTotalTransaction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void btn_show_Click(object sender, EventArgs e)
    {
        EgTotalTransactionBL objEgTotal = new EgTotalTransactionBL();
        string[] fromDate = txtFromDate.Text.Split('/');
        objEgTotal.FromDate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
        string[] ToDate = txtToDate.Text.Split('/');
        objEgTotal.ToDate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());

        if ((objEgTotal.ToDate - objEgTotal.FromDate).TotalDays >= 15)
        {
            DataList1.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference can not be greater than 15 days.');", true);
        }
        else
        {
            DataList1.Visible = true;
            // objEgTotal.GetTotalTransaction(DataListBankTransaction);
            objEgTotal.GetTotalTransaction(DataList1);
        }
        
    }
}
