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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EgBL;

public partial class WebPages_EgScrollInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgEChallanBL objEChallan = new EgEChallanBL();
            objEChallan.GetBanks(ddlbankname);
        }

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        EgBankSoftCopyBL objBankSoftCopyBL = new EgBankSoftCopyBL();
        string[] fromdate = txtDate.Text.Trim().Replace("-", "/").Split('/');
        objBankSoftCopyBL.Date = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
    //    objBankSoftCopyBL.Date = Convert.ToDateTime(txtDate.Text);      
        objBankSoftCopyBL.BSRCode = ddlbankname.SelectedValue;
        grdScroll.DataSource = objBankSoftCopyBL.GetScrollMismatchRecords();
        grdScroll.DataBind();
    }
}
