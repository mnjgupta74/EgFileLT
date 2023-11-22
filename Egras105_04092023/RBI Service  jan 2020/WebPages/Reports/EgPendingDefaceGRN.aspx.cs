using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class WebPages_Reports_EgPendingDefaceGRN : System.Web.UI.Page
{
    EgEncryptDecrypt ObjEncrytDecrypt;
    int officeid;
    string UserId, Usertype, fromdate, todate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (Request.QueryString.Count > 0)
        {
            string strReqq = Request.Url.ToString();
            strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
            ObjEncrytDecrypt = new EgEncryptDecrypt();
            List<string> strList = ObjEncrytDecrypt.Decrypt(strReqq);
            if (strList != null)
            {
                if (strList.Count > 0)
                {
                    officeid = Convert.ToInt32(strList[1]);
                    UserId = strList[3].ToString();
                    Usertype = strList[5].ToString();
                    fromdate = strList[9].ToString();
                    todate = strList[11].ToString();
                    if (strList[7].ToString() == "1")
                    {
                        var menu = Page.Master.FindControl("vmenu1") as UserControl;
                        menu.Visible = false;
                        var lnk = Page.Master.FindControl("lnkLogout") as Control;
                        lnk.Visible = false;
                        UserControl uc = (UserControl)this.Page.Master.FindControl("hmenu1");
                        uc.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("~\\logout.aspx");
                }
            }
            else
            {
                Response.Redirect("~\\logout.aspx");
            }
        }

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        DataTable dt = new DataTable();
        EgReports objEgReports = new EgReports();
        objEgReports.userid = Convert.ToInt32(Session["UserId"]);
        objEgReports.officeid = officeid;
        string[] fromDate = fromdate.Split('/');
        objEgReports.Fromdate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
        string[] ToDate = todate.Split('/');
        objEgReports.Todate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
        dt= objEgReports.GetPendingDefaceGRNs();
        if (dt.Rows.Count > 0)
            { 

            grdDeface.DataSource = dt;
            grdDeface.DataBind();
            }
        dt.Dispose();
    }
    protected void grdDeface_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindData();
        grdDeface.PageIndex = e.NewPageIndex;
        grdDeface.DataBind();
    }
}
