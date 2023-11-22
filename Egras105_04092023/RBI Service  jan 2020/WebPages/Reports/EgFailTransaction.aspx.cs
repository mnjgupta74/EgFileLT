using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EgBL;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;

public partial class WebPages_Reports_EgFailTransaction : System.Web.UI.Page
{
    //EgFailTransactionsBL objEgFailTransactionsBL = new EgFailTransactionsBL();
    //EgEchallanHistoryBL objEChallan = new EgEchallanHistoryBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        //if (!IsPostBack)
        //{
        //    objEChallan.PopulateBankList(ddlbanks);
        //    if (Session["UserType"].Equals(6))
        //    {
        //        objEgFailTransactionsBL.Userid = Convert.ToInt32(Session["UserId"].ToString());
        //        ddlbanks.Items.FindByValue(objEgFailTransactionsBL.getIdentity()).Selected = true;
        //        ddlbanks.Enabled = false;
        //    }
        //}
    }
    [WebMethod]
    public static string FillBank(string userid, string usertype)
    {
        string str = string.Empty;
        var objEChallan = new EgEchallanHistoryBL();
        objEChallan.UserId = Convert.ToInt64(userid);
        objEChallan.UserType = Convert.ToInt16(usertype);
        return objEChallan.GetBanks();
    }

    [WebMethod]
    public static string GetFailTransacrionsForBank(string bank, string FromDate, string ToDate)
    {
        EgFailTransactionsBL objEgFailTransactionsBL = new EgFailTransactionsBL();
        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        objEgFailTransactionsBL.BankName = bank;
        objEgFailTransactionsBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgFailTransactionsBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        string str = objEgFailTransactionsBL.GetFailTransaction();
        return str;
    }
    //protected void btnShow_Click(object sender, EventArgs e)
    //{


    //    objEgFailTransactionsBL.BankName = ddlbanks.SelectedValue.ToString();
    //    string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
    //    objEgFailTransactionsBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
    //    //objEgFailTransactionsBL.FromDate = Convert.ToDateTime(txtFromDate.Text);
    //    string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
    //    objEgFailTransactionsBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
    //    //objEgFailTransactionsBL.ToDate = Convert.ToDateTime(txtToDate.Text);
    //    if (((objEgFailTransactionsBL.ToDate - objEgFailTransactionsBL.FromDate).TotalDays) > 15)
    //    {
    //        trrpt.Visible = false;
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Date difference cannot be more than 15 days.')", true);

    //        return;
    //    }
    //    else if (((objEgFailTransactionsBL.ToDate - objEgFailTransactionsBL.FromDate).TotalDays)<0)
    //    {
    //        trrpt.Visible = false;
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('From Date Must be less Then ToDate.')", true);

    //        return;
    //    }
    //    else
    //    {
    //        objEgFailTransactionsBL.fillrepeater(rptrFailTrans);
    //        if (objEgFailTransactionsBL.flag == 1)
    //        {
    //            trrpt.Visible = true;
    //        }
    //        else
    //        {
    //            trrpt.Visible = false;
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);

    //            return;
    //        }
    //    }
    //}
}
