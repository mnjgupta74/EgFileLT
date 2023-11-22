using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using EgBL;

public partial class WebPages_Reports_EgGRNBankStatusDetail : System.Web.UI.Page
{
    EgGRNBankStatus objEgGRNBankStatus = new EgGRNBankStatus();
    protected void Page_Load(object sender, EventArgs e)
    {
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
            objEgGRNBankStatus.FromDate = Convert.ToDateTime(queryItems[1]);
            objEgGRNBankStatus.ToDate = Convert.ToDateTime(queryItems[3]);
            objEgGRNBankStatus.BankCode = (queryItems[5]);
            objEgGRNBankStatus.NoofDaysDiff = Convert.ToInt16(queryItems[7]);
            DataTable dt = new DataTable();


            dt = objEgGRNBankStatus.BankRptBind();
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