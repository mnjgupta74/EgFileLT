using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using EgBL;


public partial class BanksoftCopy : System.Web.UI.Page
{
    string filename;
    string colvalue;
    DataTable dt = null;
    EgBankSoftCopyBL objBankSoftCopyBL = new EgBankSoftCopyBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            objBankSoftCopyBL.FillBank(ddlBank);
        }

        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }


  

    protected void btnReconcile_Click(object sender, EventArgs e)
    {
        DataTable dtMismatch = new DataTable();
        //objBankSoftCopyBL.BSRCode = ddlBank.SelectedValue;
        objBankSoftCopyBL.BSRCode = "00200603";
        string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
        // ObjChallan.fromdate = Convert.ToDateTime(txtfromdate.Text);
        objBankSoftCopyBL.Date = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        
        dtMismatch = objBankSoftCopyBL.GetSoftCopyDetail();
        grdVerify.DataSource = dtMismatch;
        grdVerify.DataBind();

        Message("Successfully Reconciled");
    }

    private void Message(string str)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + str + "');", true);
    }
    
}