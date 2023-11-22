using System;
using System.Web.UI;
using EgBL;
using System.Data;
using System.Text.RegularExpressions;

public partial class WebPages_Reports_EgSearchByIdentity : System.Web.UI.Page
{
    EgSearchByIdentity objSearchByIdentity;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            rptShow.Visible = false;
            msgLable.Visible = false;
            Departmentlist();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblSearchByIdentity.Text = "Search Report By Identity";
                objSearchByIdentity = new EgSearchByIdentity();
                string identity = txtSearchByIdentity.Text;
                objSearchByIdentity.Identity = Regex.Replace(identity, @"[^\w\d]", "");
                objSearchByIdentity.DeptCode = ddlDepartment.SelectedValue;
                objSearchByIdentity.UserId = Convert.ToInt32(Session["UserId"]);
                objSearchByIdentity.UserType = Convert.ToInt16(Session["UserType"]);

                string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
                objSearchByIdentity.FromDate = Convert.ToDateTime(fromdate[1].ToString() + "/" + fromdate[0].ToString() + "/" + fromdate[2].ToString());
                string[] todate = txttodate.Text.Trim().ToString().Trim().Replace("-", "/").Split('/');
                objSearchByIdentity.Todate = Convert.ToDateTime(todate[1].ToString() + "/" + todate[0].ToString() + "/" + todate[2].ToString());
                //if ((objSearchByIdentity.Todate - objSearchByIdentity.FromDate).TotalDays > 30)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 30 days');", true);
                //    return;
                //}
                DataTable dt = new DataTable();
                objSearchByIdentity.SearchByIdentity(dt);
                if (dt.Rows.Count > 0)
                {
                    RptSearch.DataSource = dt;
                    RptSearch.DataBind();
                    rptShow.Visible = true;
                    msgLable.Visible = true;
                    txtSearchByIdentity.Enabled = false;
                }
                else
                {
                    rptShow.Visible = false;
                    msgLable.Visible = true;
                    txtSearchByIdentity.Enabled = true;
                    lblSearchByIdentity.Text = "No Record Found !";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
    }
    public void Departmentlist()
    {
        objSearchByIdentity = new EgSearchByIdentity();
        objSearchByIdentity.UserId = Convert.ToInt32(Session["UserId"]);
        objSearchByIdentity.PopulateDepartmentList(ddlDepartment);
        if (Session["UserType"].ToString().Trim() == "5")
        {
            ddlDepartment.SelectedIndex = 1;
            ddlDepartment.Enabled = false;
        }
        //if (Session["UserType"].ToString().Trim() == "5".Trim()) { ddlDepartment.SelectedValue = Session["UserName"].ToString(); }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptShow.Visible = false;
        msgLable.Visible = false;
        txtSearchByIdentity.Enabled = true;
    }
}
