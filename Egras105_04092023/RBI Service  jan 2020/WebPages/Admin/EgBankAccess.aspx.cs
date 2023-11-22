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

public partial class WebPages_Admin_EgBankAccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            //EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            ////string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            //Response.Redirect("~\\logout.aspx");
            Response.Redirect("~\\LoginAgain.aspx");

        }
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    public void BindGrid()
    {
        DataTable dt = new DataTable();
        EgBankAccessBL objEgBankAccessBL = new EgBankAccessBL();
        dt= objEgBankAccessBL.FillBankGrid();
        grdbank.DataSource = dt;
        grdbank.DataBind();

        dt.Dispose();
    }

    protected void grdbank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Flag = grdbank.DataKeys[e.Row.RowIndex][0].ToString();

            CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("chkbox"); //(CheckBox)Row.FindControl("chkbox");
            if (Flag == "Y")
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }
        }

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        EgBankAccessBL objEgBankAccessBL = new EgBankAccessBL();

        foreach (GridViewRow row in grdbank.Rows)
        {
            CheckBox CheckBox1 = (CheckBox)row.FindControl("chkBox");
            if (CheckBox1.Checked == true)
            {
                objEgBankAccessBL.BSRCode = grdbank.DataKeys[row.RowIndex][1].ToString();
                objEgBankAccessBL.Access = "Y";
                objEgBankAccessBL.UpdateBankAccess();
            }
            else
            {
                objEgBankAccessBL.BSRCode = grdbank.DataKeys[row.RowIndex][1].ToString();
                objEgBankAccessBL.Access = "N";
                objEgBankAccessBL.UpdateBankAccess();
            }

        }
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Update Successfully')", true);
        BindGrid();
    }
}
