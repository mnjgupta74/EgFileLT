using System;
using System.Data;
using System.Web.UI;
using EgBL;

public partial class WebPages_charts_EgGrn : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    string[] fdate;
    int GRN;
    int UserType;
    int UserId;
    EgDeptwiseRevenueTotal objegdeptwise = new EgDeptwiseRevenueTotal();
    //AjaxControlToolkit.TabContainer ajxTab = new AjaxControlToolkit.TabContainer();
    EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();

    protected void Page_Load(object sender, EventArgs e)
      {
            if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
            {
            Response.Redirect("~\\LoginAgain.aspx");
        }
            listbox.Visible = false;
            iframe.Visible = false;
            if (!IsPostBack)
            {
                DateTime fromDate = DateTime.Parse("12/01/2012");
                txtfromDate.Text = fromDate.ToString("dd'/'MM'/'yyyy");
                DateTime toDate = DateTime.Parse(DateTime.Now.ToString());
                txttoDate.Text = toDate.ToString("dd'/'MM'/'yyyy");
                loadreport();
            }

         }
    public void loadreport()
      {
        if (Request.QueryString["ftrans"] != null)
        {
            listbox.Visible = true;
            iframe.Visible = true;
            fdate = Request.QueryString["ftrans"].ToString().Split(',');
            string[] from = fdate[0].ToString().Trim().Replace("-", "/").Split('/');
            objegdeptwise.Fromdate = Convert.ToDateTime(from[1].ToString().Trim() + "/" + from[0].ToString().Trim() + "/" + from[2].ToString().Trim());
            string[] ToDate1 = fdate[1].ToString().Trim().Replace("-", "/").Split('/');
            objegdeptwise.Todate = Convert.ToDateTime(ToDate1[1].ToString().Trim() + "/" + ToDate1[0].ToString().Trim() + "/" + ToDate1[2].ToString().Trim());
            objegdeptwise.Deptcode = fdate[2].ToString().Trim();
            objegdeptwise.Location = fdate[4].ToString().Trim();
            objegdeptwise.UserId = Convert.ToInt32(fdate[6].ToString().Trim());

            var menu = Page.Master.FindControl("vmenu1") as UserControl;
            menu.Visible = false;
            var lnk = Page.Master.FindControl("lnkLogout") as Control;
            lnk.Visible = false;
            UserControl uc = (UserControl)this.Page.Master.FindControl("hmenu1");
            uc.Visible = false;
        }
        else
        {
            string[] fromdate = txtfromDate.Text.Trim().Replace("-", "/").Split('/');
            objegdeptwise.Fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            string[] todate = txttoDate.Text.Trim().Replace("-", "/").Split('/');
            objegdeptwise.Todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
            objegdeptwise.UserId = Convert.ToInt32(Session["UserId"].ToString().Trim());
            objegdeptwise.UserType = Convert.ToInt32(Session["UserType"].ToString().Trim());
        }



        if (Request.QueryString["ftrans"] != null)
        {
            objegdeptwise.GetGrnDetail(dt);
        }
        else
        {
            objegdeptwise.GetGrnDetailNew(dt);
        }
        if (dt.Rows.Count > 0)
        {
            listbox.Visible = true;
            iframe.Visible = true;
            ViewState["dt"] = dt;
            lstGrn.DataSource = dt;
            lstGrn.DataTextField = "GRN";
            lstGrn.DataValueField = "GRN";
            lstGrn.DataBind();
        }
        else
        {
            listbox.Visible = false;
            iframe.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('NO Record Found !! ');", true);
        }



    }



    protected void lstGrn_SelectedIndexChanged(object sender, EventArgs e)
    {
        listbox.Visible = true;
        iframe.Visible = true;
        dt = (DataTable)ViewState["dt"];

        GRN = Convert.ToInt32(lstGrn.SelectedItem.Text);

            if (Request.QueryString["ftrans"] != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt64(GRN) == Convert.ToInt64(dt.Rows[i]["GRN"].ToString()))
                    {
                        UserId = Convert.ToInt32(dt.Rows[i]["Userid"].ToString());
                        UserType = Convert.ToInt16(dt.Rows[i]["User_type"].ToString());
                        break;
                    }
                }
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserId"].ToString().Trim());
                UserType = Convert.ToInt16(Session["UserType"].ToString().Trim());
            }


            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}", GRN.ToString(), UserId.ToString(), UserType.ToString()));

            lblgrn.Text = "<iframe src='../EgGrnView.aspx?" + strURLWithData + "' width='86%' height='100% scrollbar='no' '></iframe>";
        }


        protected void btnShow_Click(object sender, EventArgs e)
        {
            lblgrn.Text = "";
            lstGrn.DataSource = "";
            lstGrn.DataBind();
            loadreport();
        }
}
