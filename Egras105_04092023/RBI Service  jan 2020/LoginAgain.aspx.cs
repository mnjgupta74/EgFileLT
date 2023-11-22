using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_404Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        //{
        //    Response.Redirect("~\\logout.aspx");
        //}

     
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
    }
    //protected void btngo_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~\\default.aspx");
    //}
}