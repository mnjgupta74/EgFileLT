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
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        //if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        //{
        //    Response.Redirect("~\\logout.aspx");
        //}


    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~\\Default.aspx");
    }
}