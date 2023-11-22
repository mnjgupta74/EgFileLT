using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
public partial class WebPages_Reports_EgGRNSearch : System.Web.UI.Page
{
    static DataTable DtSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {

            EgGrnSearch objEgGrnSearch = new EgGrnSearch();
            objEgGrnSearch.PopulateBankList(ddlbankname);
            objEgGrnSearch.PopulateDepartmentList(ddldepartment);
            DtSearch = objEgGrnSearch.GetDeptList();
            objEgGrnSearch.FillTreasury(ddltreasury);

            if (Session["UserType"].ToString().Trim() == "9")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "GuestPopUp();", true);
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        EnableControl();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Page.IsValid)
        {
            try
            {
                EgGrnSearch objEgGrnSearch = new EgGrnSearch();
                objEgGrnSearch.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
                objEgGrnSearch.Bankcode = ddlbankname.SelectedValue;
                objEgGrnSearch.GRN = Convert.ToInt32(txtGRN.Text.ToString().Trim());
                objEgGrnSearch.Amount = Convert.ToDecimal(txtAmount.Text.ToString().Trim());
                objEgGrnSearch.OfficeId = Convert.ToInt32(ddloffice.SelectedValue);
                dt = objEgGrnSearch.FillGrid();
                if (dt.Rows.Count > 0)
                {
                    DisableControl();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
        dt.Dispose();
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfStatus = (HiddenField)e.Row.FindControl("hfStatus");
            ImageButton Imgview = (ImageButton)e.Row.FindControl("ImageViewbtn");
            if (hfStatus.Value == "S")
            {
                Imgview.Enabled = true;
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();

        if (e.CommandName == "View")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string grn = index.ToString();
            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&UserId={1}&UserType={2}&Dept={3}", grn.ToString(), Session["UserId"].ToString(), Session["UserType"],"1"));

            strURLWithData = "../EgDefaceDetailNew.aspx?" + strURLWithData;
            string script = "window.open('" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
        }
    }
    protected void ddltreasury_SelectedIndexChanged(object sender, EventArgs e)
    {
        EgGrnSearch objEChallan = new EgGrnSearch();
        objEChallan.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        objEChallan.Tcode = ddltreasury.SelectedValue;
        objEChallan.FillOfficeList(ddloffice);
    }
    private void DisableControl()
    {
        ddldepartment.Enabled = false;
        ddlbankname.Enabled = false;
        ddloffice.Enabled = false;
        ddltreasury.Enabled = false;
        txtAmount.Enabled = false;
        txtGRN.Enabled = false;
        btnShow.Enabled = false;
        GridView1.Visible = true;
    }
    private void EnableControl()
    {
        ddldepartment.Enabled = true;
        ddlbankname.Enabled = true;
        ddloffice.Enabled = true;
        ddltreasury.Enabled = true;
        txtAmount.Enabled = true;
        txtGRN.Enabled = true;
        ddlbankname.SelectedValue = "0";
        ddldepartment.SelectedValue = "0";
        ddloffice.SelectedValue = "0";
        ddltreasury.SelectedValue = "0";
        txtAmount.Text = string.Empty;
        txtGRN.Text = string.Empty;
        btnShow.Enabled = true;
        GridView1.Visible = false;
    }
}
