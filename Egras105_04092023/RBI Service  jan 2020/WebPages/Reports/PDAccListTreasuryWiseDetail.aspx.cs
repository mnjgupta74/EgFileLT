using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class WebPages_Reports_PDAccListTreasuryWiseDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PDAccListTreasuryBL objpd = new PDAccListTreasuryBL();
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgEncryptDecrypt objEncrypt = new EgEncryptDecrypt();
            string strReqq = Request.Url.ToString();
            strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
            List<string> queryItems = objEncrypt.Decrypt(strReqq);
            objpd.FromDate = Convert.ToDateTime(queryItems[1]);
            objpd.ToDate = Convert.ToDateTime(queryItems[3]);
            
            if (queryItems[4].ToString() == "BudgetHead")
            {
                objpd.BudgetHead = (queryItems[5]).ToString();
                objpd.TreasuryCode = (queryItems[9]).ToString();
                //objpd.TreasuryCode = Convert.ToInt32(queryItems[7]);
                DataTable dt = new DataTable();
                dt = objpd.BindHeadWiseExtraDetail();
                
                if (dt.Rows.Count > 0)
                {
                    RepeaterPop.DataSource = dt;
                    RepeaterPop.DataBind();
                    RepeaterPop.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Data Found.')", true);
                }
            }
            else if (queryItems[4].ToString() == "PDAcc")
            {
               
                objpd.PDAcc = Convert.ToInt32(queryItems[5]);
                objpd.TreasuryCode = queryItems[7].ToString();
                DataTable dt = new DataTable();
                dt = objpd.BindPdExtraDetail();
                if (dt.Rows.Count > 0)
                {
                    RepeaterPop.DataSource = dt;
                    RepeaterPop.DataBind();
                    RepeaterPop.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Data Found.')", true);
                }
            }
        }


    }
}