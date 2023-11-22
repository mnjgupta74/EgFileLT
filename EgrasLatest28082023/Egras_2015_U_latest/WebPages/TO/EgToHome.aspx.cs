using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_TO_EgToHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["Page"] != null && (Request.QueryString["Page"] == "" || Request.QueryString["Page"] != ""))
        {
            GeneralClass.ShowMessageBox("You are not authorized to access the page");

        }

        if (Session["UserId"] == null || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        EgHomeBL objEgHomeBL = new EgHomeBL();
        objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"]);
        int result = objEgHomeBL.GetUserDetail();
        if (result == 1)
        {
            //DateTime.ParseExact(objEgHomeBL.lastslogin.ToString(), "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            lblFirstNameBound.Text = "E-Treasury";
            lblLastsuccess.Text = objEgHomeBL.lastslogin.ToString("dd/MM/yyyy hh:mm:ss");

            if (objEgHomeBL.lastflogin.ToString() == "")
            {
                lbllastfail.Visible = false;
                lblfail.Visible = false;
            }
            else
            {
                lbllastfail.Text = Convert.ToDateTime(objEgHomeBL.lastflogin).ToString("dd/MM/yyyy  hh:mm:ss");
            }
            if (objEgHomeBL.lastchangepass.ToString() == "")
            {
                lblpass.Visible = false;
                lblLastchange.Visible = false;
            }
            else
            {
                lblLastchange.Text = Convert.ToDateTime(objEgHomeBL.lastchangepass).ToString("dd/MM/yyyy hh:mm:ss");
            }
        }

    }
}