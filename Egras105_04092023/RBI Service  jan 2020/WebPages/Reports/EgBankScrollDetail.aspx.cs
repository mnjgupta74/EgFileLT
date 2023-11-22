using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;


public partial class WebPages_Reports_EgBankScrollDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        EgBankScrollDetailBL objEgBankScrollDetailBL = new EgBankScrollDetailBL();
        string[] fromdate = txtDate.Text.Trim().Replace("-", "/").Split('/');
        objEgBankScrollDetailBL.Date = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgBankScrollDetailBL.UserId = Convert.ToInt32(Session["UserID"].ToString());
        objEgBankScrollDetailBL.GetTransactionDetail();
        if (objEgBankScrollDetailBL.TotalTransaction > 0)
        {
            trTrans.Visible = true;
            trNoRec.Visible = false;
            lblTrans.Text = Convert.ToString(objEgBankScrollDetailBL.TotalTransaction);
            lblAmount.Text = objEgBankScrollDetailBL.TotalAmount.ToString("0.00");
        }
        else
        {
            trTrans.Visible = false;
            trNoRec.Visible = true;
        }

    }
}
