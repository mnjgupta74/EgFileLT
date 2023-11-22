using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using EgBL;

public partial class masterpage_MasterPage5 : System.Web.UI.MasterPage
{
    EgEncryptDecrypt egEncrypt;

    protected void Page_Init(object Sender, EventArgs e)
    {
        Page.ViewStateUserKey = Session.SessionID;

        Response.ClearHeaders();
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
        Response.Cache.SetNoStore();
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(-1, 0, 0, 0));
        Response.AppendHeader("Pragma", "no-cache");
        Response.AppendHeader("", "");
        Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1
        Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1 
        Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1 
        Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1 
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.1 
        Response.AppendHeader("Keep-Alive", "timeout=3, max=993"); // HTTP 1.1   
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = ConfigurationManager.AppSettings["PageTitle"].ToString();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));

        if (Session["UserId"].ToString() == "73")
        {
            lblUser.Text = "Guest";
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy H:mm:ss");
        }
        else if (Session["userName"] != null)
        {
            lblUser.Text = Session["userName"].ToString();
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy H:mm:ss");
        }
        else
        {

        }
        //=================For Office Or Department Name ======================
        if (Session["UserType"].ToString() == "4" || Session["UserType"].ToString() == "5")
        {
            EgUserRegistrationBL obj = new EgUserRegistrationBL();
            obj.UserId = Convert.ToInt32(Session["UserId"]);
            obj.UserType = Session["UserType"].ToString();
            if (Session["UserType"].ToString() == "4")
                lblWelcome.Text = "Welcome Office:";
            else
                lblWelcome.Text = "Welcome Dept:";
            lblDeptOrOfficeName.Text = "-" + obj.GetOfficeOrDeptName();
            lblDeptOrOfficeName.Visible = true;
            //lblDeptOrOfficeName
        }
        //=================For Session Fixation ===============================
        if (Request.Cookies["appcookie"] != null && Session["RND"] != null)
        {
            if (Request.Cookies["appCookie"].Value != Session["RND"].ToString())//&& Request.UrlReferrer.AbsolutePath != "/EgrasWebSite/WebPages/Account/EgUserRegistration.aspx")
            {
                Session.Abandon();
                Response.Redirect("~\\default.aspx");
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("~\\default.aspx");
        }
        //=====================================================================


        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Write("<Script>alert('Session Expired')</Script>");
            Server.Transfer("~\\logout.aspx");
        }
        //if (Request.UrlReferrer == null)
        //{
        //    Response.Redirect("~\\logout.aspx", true);
        //}


        if (!IsPostBack)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

        }

    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
      
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();

        if (Request.Cookies["ASP.NET_SessionId"] != null)
        {
            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Request.Cookies["appCookie"] != null)
        {
            Response.Cookies["appCookie"].Value = string.Empty;
            Response.Cookies["appCookie"].Expires = DateTime.Now.AddMonths(-20);
        }
        Response.Redirect("~\\Logout.aspx");
    }
}
