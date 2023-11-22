using System;
using System.Web;
using System.Web.Security;
using EgBL;
using System.Collections.Generic;

public partial class _Logout : System.Web.UI.Page
{

    //EgEncryptDecrypt ObjEncryptDecrypt ;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("~\\default.aspx");
        //if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        //{
        //    if (Session["RND"] != null && Session["RND"].ToString() != "")
        //    {
        //        ObjEncryptDecrypt = new EgEncryptDecrypt();
        //        string strReq = Request.Url.ToString();
        //        strReq = strReq.Substring(strReq.IndexOf('?') + 1);
        //        List<string> strList = ObjEncryptDecrypt.Decrypt(strReq);
        //        if (strList != null)
        //        {
        //            if (strList.Count > 0)
        //            {
        //                if (Session["RND"].ToString() != strList[1].ToString().Trim())
        //                {
        //                    Response.Redirect("~\\default.aspx");
        //                }
        //                else
        //                {
        //                    EgLogout objEgLogout = new EgLogout();
        //                    var LoginID = Session["UserID"].ToString();
        //                    var Logoutdate = System.DateTime.Now;
        //                    objEgLogout.LoginID = Convert.ToInt32(LoginID);
        //                    objEgLogout.Logoutdate = Logoutdate;
        //                    objEgLogout.FillTable();

        //                    FormsAuthentication.SignOut();
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Session.Clear();
        //                    Session.RemoveAll();
        //                    Session.Abandon();//Close the window.
        //                    Response.Redirect("~\\Default.aspx");
        //                }

        //            }
        //            else
        //            {
        //                Response.Redirect("~\\default.aspx");

        //            }
        //        }
        //        else
        //        {
        //            Session.Abandon();
        //            Response.Redirect("~\\default.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Session.Abandon();
        //        Response.Redirect("~\\default.aspx");
        //    }

        //}

        //else
        //{
        //    Session.Abandon();
        //    Response.Redirect("~\\default.aspx");

        //}
    }
        

        #endregion

    
    //protected void btngo_Click(object sender, EventArgs e)
    //{
    //    Session.Clear();
    //    Session.RemoveAll();
    //    Session.Abandon();
    //    Response.Redirect("~\\default.aspx");
    //}
}

