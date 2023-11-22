using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using EgBL;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebPages_Reports_EgDeletedBankScrollRpt : System.Web.UI.Page
{
    EgEChallanBL objEChallan = new EgEChallanBL();
    EgDeletedBankScrollRptBL objEgDeletedBankScrollRptBL = new EgDeletedBankScrollRptBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (!IsPostBack)
            objEChallan.GetBanks(ddlBank);
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objEgDeletedBankScrollRptBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());

        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objEgDeletedBankScrollRptBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEgDeletedBankScrollRptBL.BSRCode = Convert.ToInt32(ddlBank.SelectedValue);
        objEgDeletedBankScrollRptBL.fillRptrDate(rptrDeletedScrollDateWise);
        if (objEgDeletedBankScrollRptBL.flag == 1)
        {
            trrpt2.Visible = false;
            trrpt.Visible = true;
            lblStatus.Visible = false;
        }
        else
        {
            trrpt2.Visible = false;
            trrpt.Visible = false;
            lblStatus.Visible = true;
            return;
        }
    }

    protected void sample(object sender, EventArgs e)
    {
        string sss = ViewState["DeletionDate"].ToString();
    }

    protected void rptrDeletedScrollDateWise_View(object sender, CommandEventArgs e)
    {
            objEgDeletedBankScrollRptBL.BankDate = Convert.ToDateTime(e.CommandArgument);
            ViewState["BankDate"] = objEgDeletedBankScrollRptBL.BankDate.ToString();
            objEgDeletedBankScrollRptBL.TransDate = Convert.ToDateTime(e.CommandName);
            ViewState["DeletionDate"] = objEgDeletedBankScrollRptBL.TransDate.ToString();
            objEgDeletedBankScrollRptBL.BSRCode = Convert.ToInt32(ddlBank.SelectedValue);
            objEgDeletedBankScrollRptBL.fillRptrData(rptrScrollData);
            if (objEgDeletedBankScrollRptBL.flag == 1)
            {
                trrpt2.Visible = true;
                lblStatus.Visible = false;
            }
            else
            {
                trrpt2.Visible = false;
                lblStatus.Visible = true;
                return;
            }
    }
    protected void grdScroll_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objEgDeletedBankScrollRptBL.BankDate = Convert.ToDateTime(ViewState["BankDate"]);
        objEgDeletedBankScrollRptBL.TransDate = Convert.ToDateTime(ViewState["DeletionDate"]);
        objEgDeletedBankScrollRptBL.BSRCode = Convert.ToInt32(ddlBank.SelectedValue);
        objEgDeletedBankScrollRptBL.fillRptrData(rptrScrollData);
        if (objEgDeletedBankScrollRptBL.flag == 1)
        {
            trrpt2.Visible = true;
            lblStatus.Visible = false;
        }
        else
        {
            trrpt2.Visible = false;
            lblStatus.Visible = true;
            return;
        }
        rptrScrollData.PageIndex = e.NewPageIndex;
        rptrScrollData.DataBind();
    }
}
