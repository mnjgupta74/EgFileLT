using System;
using System.Data;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_EgLoginInfo : System.Web.UI.Page
{

    DataTable dt;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt objEgEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (!IsPostBack)
        {
            CurrentPage = 1;

        }

    }
    protected void btnclick_Click(object sender, EventArgs e)
    {
        BindScrollData();
        lstrecord.Visible = true;
    }

    private void BindScrollData()
    {
        dt = new DataTable();
        EgLoginAuditBL objEgLoginAuditBL = new EgLoginAuditBL();
        string[] fromdate = txtFromdate.Text.Trim().Replace("-", "/").Split('/');
        objEgLoginAuditBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtTodate.Text.Trim().Replace("-", "/").Split('/');
        objEgLoginAuditBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        var TotalCount = objEgLoginAuditBL.TotalRecord();
        int ItemPerPage = 14;
        if (CurrentPage == 1)
        {
            objEgLoginAuditBL.StartIdx = 1;
        }
        else
            objEgLoginAuditBL.StartIdx = (((CurrentPage-1) * ItemPerPage)+1);
            objEgLoginAuditBL.EndIdx = (((objEgLoginAuditBL.StartIdx) + ItemPerPage)-1);
            dt = objEgLoginAuditBL.tablegrid();
            if (dt.Rows.Count == 0)
            {
                lblrecord.Visible = false;
                lnknext.Visible = false;
                lnkpre.Visible = false;
                lnk_first.Visible = false;
                lnk_last.Visible = false;
                lblCurrentPage.Visible = false;
                grdLogindetail.DataSource = dt;
                grdLogindetail.DataBind();
            }
            else
            {
                lblrecord.Visible = true;
                lnknext.Visible = true;
                lnkpre.Visible = true;
                lnk_first.Visible = true;
                lnk_last.Visible = true;
                lblCurrentPage.Visible = true;
                lblrecord.Text = "Total Record :" +  TotalCount;
                lblCurrentPage.Text = "Page Number : " + CurrentPage;
                grdLogindetail.DataSource = dt;
                grdLogindetail.DataBind();
            }

            if (CurrentPage == 1)
            {
                lnkpre.Enabled = false;
                lnk_first.Enabled = false;
            }
            else 
            {
                lnkpre.Enabled = true;
                lnknext.Enabled = true;
                lnk_first.Enabled = true;
                lnk_last.Enabled = true;
            }

    }

    protected void grdLogindetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Flag = grdLogindetail.DataKeys[e.Row.RowIndex][0].ToString();
            ImageButton img = (ImageButton)e.Row.FindControl("imgbtn");
            if (Flag == "N")
            {

                img.ImageUrl = "~/Image/red.png";
                img.ToolTip = "User is not Successful Login";
            }
            else
            {
                img.ImageUrl = "~/Image/green.png";
                img.ToolTip = "User is Successful Login";
            }
        }
    }

    public int CurrentPage   // maintain indexing with currentpage 
    {
        get
        {
            // look for current page in ViewState
            object o = this.ViewState["_CurrentPage"];
            if (o == null)
                return 0; // default page index of 0
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }
    protected void lnknext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        lblCurrentPage.Text = "Page Number : " + CurrentPage.ToString();
        BindScrollData();

    }
    protected void lnkpre_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        lblCurrentPage.Text = "Page Number : " + CurrentPage.ToString();
        BindScrollData();

    }
    protected void lnk_first_Click(object sender, EventArgs e)
    {
        lnkpre.Enabled = false;
        lnk_first.Visible = false;
        CurrentPage = 1;
        lblCurrentPage.Text = "Page Number : " + CurrentPage.ToString();
        BindScrollData();
        lnknext.Enabled = true;
        lnk_last.Enabled = true;
    }
    protected void lnk_last_Click(object sender, EventArgs e)
    {
        string[] Split = lblrecord.Text.Split(':');
        CurrentPage = (Convert.ToInt32(Split[1].ToString()) / 14) + 1;
        lblCurrentPage.Text = "Page Number : " + CurrentPage.ToString();
        BindScrollData();
        lnknext.Enabled = false;
        lnk_last.Enabled = false;
    }
}
