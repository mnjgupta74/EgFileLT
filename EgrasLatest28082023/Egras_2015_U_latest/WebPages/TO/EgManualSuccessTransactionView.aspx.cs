using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using EgBL;
using System.Web.UI.WebControls;

public partial class WebPages_EgManualSuccessTransactionView : System.Web.UI.Page
{
    EgEChallanBL objEChallan;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            objEChallan = new EgEChallanBL();
            objEChallan.GetBanks(ddlbanks);
           
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        EgManualSuccessChallanBL objManaulSuccessCHallan=new EgManualSuccessChallanBL();
        objManaulSuccessCHallan.BankName = ddlbanks.SelectedValue.ToString();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objManaulSuccessCHallan.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        //objEgFailTransactionsBL.FromDate = Convert.ToDateTime(txtFromDate.Text);
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objManaulSuccessCHallan.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        //objEgFailTransactionsBL.ToDate = Convert.ToDateTime(txtToDate.Text);
        //if (((objManaulSuccessCHallan.ToDate - objManaulSuccessCHallan.FromDate).TotalDays) > 15)
        //{
        //    trrpt.Visible = false;
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Date difference cannot be more than 15 days.')", true);

        //    return;
        //}
        if (((objManaulSuccessCHallan.ToDate - objManaulSuccessCHallan.FromDate).TotalDays) < 0)
        {
            trrpt.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('From Date Must be less Then ToDate.')", true);

            return;
        }
        else
        {
            objManaulSuccessCHallan.fillrepeater(rptrManualSuccess);
            if (objManaulSuccessCHallan.flag == 1)
            {
                trrpt.Visible = true;
            }
            else
            {
                trrpt.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);

                return;
            }
        }
    }
    protected void rptrManualSuccess_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        string strURLWithData = "";
        if (e.CommandName.Equals("Status"))
        {
            string grn = Convert.ToString(e.CommandArgument);
                strURLWithData = "~/webpages/reports/EgEchallanViewRptAnywhere.aspx?" + ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}", grn.ToString().Trim()));
            Response.Redirect(strURLWithData);

        }
    }
    
}