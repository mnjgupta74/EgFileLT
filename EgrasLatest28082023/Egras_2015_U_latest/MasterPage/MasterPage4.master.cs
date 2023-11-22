using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class masterpage_MasterPage4 : System.Web.UI.MasterPage
{
    protected void Page_Init(object Sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        //Response.CacheControl = "no-cache";
        //Response.AddHeader("Pragma", "no-cache");
        //Response.AddHeader("cache-control", "private, no-cache, must-revalidate");
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.CacheControl = "no-cache";

        //Response.AddHeader("Pragma", "no-cache");

        //Response.AddHeader("cache-control", "private, no-cache, must-revalidate");

        //if (Request.Cookies["appcookie"] != null)
        //{
        //    if (Request.Cookies["EgrasCookie"].Value != Session["RND"].ToString())
        //    {
        //        Session.Abandon();
        //      //  Server.Transfer("../default.aspx");
        //        Response.Redirect("./Default.aspx");
        //    }
        //}
        //else
        //{
        //    Session.Abandon();
        //    Server.Transfer("/default.aspx");
        //}

        //if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        //{
        //    Response.Write("<Script>alert('Session Expired')</Script>");
        //    Server.Transfer("~\\logout.aspx");
        //}


        if (!IsPostBack)
        {
           // Response.Cache.SetCacheability(HttpCacheability.NoCache);

        }
    }
}
