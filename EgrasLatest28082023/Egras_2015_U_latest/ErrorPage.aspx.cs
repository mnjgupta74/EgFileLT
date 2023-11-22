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

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
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
            //EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            //Response.Redirect("~\\logout.aspx");
        }

      
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
        Response.Redirect("~\\Default.aspx");
    }
}
