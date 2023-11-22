using System;
using System.Web;
using System.Web.UI;
using EgBL;
using System.Web.Caching;
public partial class _Default : System.Web.UI.Page
{

    EgLoginBL objLogin;//= new EgLoginBL();

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        iFrameDMSPDF.Attributes["src"] = "YearlyChallanWiseChart.aspx";
        inpHide.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('btnLogin').click();return false;}} else {return true}; ");
        txtUserName.Focus();
        if (!Page.IsPostBack)
        {

            objLogin = new EgLoginBL();
            Random randomclass = new Random();
            Session.Add("Rnd1", randomclass.Next().ToString());
            btnLogin.Attributes.Add("onclick", "javascript:return clickme(" + Session["Rnd1"] + ");");
            // Cache.Insert("Nodal", objLogin.GetNodalOfficerDetails(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);   //  Set Nodal Office Data
            // Cache.Insert("DepartmentList", objLogin.PopulateDepartmentList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration); // Get Department List 
        }
    }
    #endregion


    #region Common Function

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    SetFocus(txtUserName);
    //}
    /// <summary>
    /// check user authentication
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {


        if (Page.IsValid)
        {
            string imageValue = String.Empty;
            if (txtUserName.Text.Contains(" "))
            {
                lblloginInfo.Visible = true;
                inpHide.Text = "";
                txtUserName.Text = "";
                lblloginInfo.Text = "Invalid UserName/Password.";
                return;
            }
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string imageValue = inpHide.Value;
            imageValue = inpHide.Text.Trim();

            if (HttpContext.Current.Session["captcha"] == null || HttpContext.Current.Session["captcha"].ToString() == "")
            {
                Response.Redirect("~\\Default.aspx", true);
            }
            try
            {

                if (HttpContext.Current.Session["captcha"].ToString().ToLower().Trim() != "" && imageValue.Trim() != "" && imageValue.Trim() != "0")
                {
                    if (HttpContext.Current.Session["captcha"].ToString().Trim() == imageValue.ToString().Trim())
                    {
                        objLogin = new EgLoginBL();
                        sbyte ReturnVal;
                        objLogin.LoginID = txtUserName.Text.ToString().Trim();
                        objLogin.Password = txtPassword.Text.ToString();
                        objLogin.SHAPassword = hdnPassword.Value.ToString().Trim();
                        objLogin.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        if (Session["RND1"] != null)
                            objLogin.RND = Session["RND1"].ToString();
                        Session["RND"] = Session["RND1"].ToString();
                        ReturnVal = objLogin.GetLogin();
                        switch (ReturnVal)
                        {
                            case 0:
                            case -1:
                                HttpCookie appcookiee = new HttpCookie("Appcookie");
                                appcookiee.Value = Session["RND"].ToString();
                                appcookiee.Expires = DateTime.Now.AddDays(1);
                                appcookiee.Domain = HttpContext.Current.Request.Url.Host;
                                Response.Cookies.Add(appcookiee);
                                HttpCookie retValue = new HttpCookie("retVal");
                                retValue.HttpOnly = true;
                                retValue.Domain = HttpContext.Current.Request.Url.Host;
                                retValue.Value = ReturnVal.ToString();
                                retValue.Expires = DateTime.Now.AddDays(1);
                                Response.Cookies.Add(retValue);
                                Response.Redirect(objLogin.AddressUrl, false);
                                break;

                            case 2:
                                lblloginInfo.Visible = true;
                                inpHide.Text = "";
                                txtUserName.Text = "";
                                lblloginInfo.Text = "Your account is blocked.\n Please try after 30 minutes.";
                                break;
                            case 3:
                                lblloginInfo.Visible = true;
                                inpHide.Text = "";
                                txtUserName.Text = "";
                                lblloginInfo.Text = "Invalid UserName/Password.";
                                break;
                        }
                    }
                    else
                    {
                        lblloginInfo.Visible = true;
                        inpHide.Text = "";
                        txtUserName.Text = "";
                        lblloginInfo.Text = "Incorrect Captcha Code !!";
                    }
                }
            }
            catch (Exception ex)
            {
                //Browserinfo objbrowseringo = new Browserinfo();
                //string msg = ex.Message + objbrowseringo.Browserinformaion();
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message + "  2ndCatch");
            }
        }
    }
    #endregion
}
