using System;
using EgBL;
public partial class WebPages_EgChangeUserLoginID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
        }
        if (!IsPostBack)
        {
            //var menu = Page.Master.FindControl("vmenu1") as UserControl;
            //menu.Visible = false;
         
                EgLoginBL objLoginBL = new EgLoginBL();
                objLoginBL.UserId = Convert.ToInt32(Session["UserID"]);
               
            
                var x = objLoginBL.UpdateUserLoginID().ToString().Trim();
                spntxt.InnerHtml =  x    ;
           


        }

    }
    //protected void lnklogin_Click(object sender, EventArgs e)
    //{
    //    Response.Write("<Script>alert('Session Expired')</Script>");
    //    Response.Redirect("~\\Default.aspx");

    //}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~\\Default.aspx");
    }
}
