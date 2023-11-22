using System;
using System.Data;
using System.Web.UI;
using EgBL;

public partial class WebPages_Reports_EgDuplicateCINList : System.Web.UI.Page
{
    EgDuplicateCINListBL objEgDuplicateCINListBL = new EgDuplicateCINListBL();
    EgEchallanHistoryBL objEChallan = new EgEchallanHistoryBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            objEChallan.PopulateBankList(ddlbanks);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        objEgDuplicateCINListBL.BankName = ddlbanks.SelectedValue.ToString();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objEgDuplicateCINListBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objEgDuplicateCINListBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEgDuplicateCINListBL.PaymentType = rdBtnList.SelectedValue;
        DataTable DuplicateList = objEgDuplicateCINListBL.fillrepeater();
        if (DuplicateList.Rows.Count > 0)
        {
            trrpt.Visible = true; ;
            lblMessage.Visible = false;
            trmsg.Visible = false;
            rptrDuplicateCINlst.DataSource = DuplicateList;
            rptrDuplicateCINlst.DataBind();
            DuplicateList.Dispose();
        }
        else
        {
            trmsg.Visible = true;
            trrpt.Visible = false;
            lblMessage.Visible = true;
        }
    }
    protected void rdBtnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        trmsg.Visible = false;
        trrpt.Visible = false;
        lblMessage.Visible = false;
    }
}
