using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EgBL;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using System.Data.Sql;


public partial class masterpage_MasterPage7 : System.Web.UI.MasterPage
{
    DataTable dt;
    EgMstUserLogin objTMstMenuBL = new EgMstUserLogin();
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
        //string bankAddress = Request.UrlReferrer.AbsoluteUri.ToString();
        //string[] url1 = bankAddress.ToString().Split('/');
        //string address = url1[0] + url1[1] + url1[2];
        //dt = new DataTable();
        //dt = objTMstMenuBL.FetchBankUrls();
        //dt.Columns.Add("Address", Type.GetType("System.String"));
        //foreach (DataRow dr in dt.Rows)
        //{
        //    string[] bankurl = dr["bankUrl"].ToString().Split('/');
        //    dr["Address"] = bankurl[0] + bankurl[1] + bankurl[2];

        //}
        //dt.AcceptChanges();
        //var checkIP = dt.AsEnumerable().FirstOrDefault(r => r.Field<string>("Address") == address.ToString());
        //if (checkIP == null)
        //{
        //    EgBL.GeneralClass.ShowMessageBox("Access Denied");
        //    Session.Abandon();
        //    Response.Redirect("~/Default.aspx");
        //}
        //if (!IsPostBack)
        //{
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //}
    }
}
