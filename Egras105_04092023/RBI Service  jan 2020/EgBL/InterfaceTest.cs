using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using EgDAL;
namespace EgBL
{
    public abstract class InterFaceTest
    {
         void CheckSession()
        {
            if ((System.Web.HttpContext.Current.Session["UserID"] == null) || System.Web.HttpContext.Current.Session["UserID"].ToString() == "")
            {
                HttpContext.Current.Response.Write("<Script>alert('Session Expired')</Script>");
                 HttpContext.Current.Response.Redirect("~\\logout.aspx");
            }
        }

    }
}

