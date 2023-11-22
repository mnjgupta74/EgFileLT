using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class UserControls_DefaultHorizontalMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (Session["User"] != null)
        {
            //lblFinYear.Text  = Session["FinYear"].ToString();
            //lblUser.Text = GetUsername();
           
            //if (lblUser.Text.Length > 15)
            //{
            //    lblUser.Font.Bold = false;
            //    lblUser.Font.Size = FontUnit.XXSmall;
            //}

            //lblDate.Text = DateTime.Now.Day + "/" +DateTime.Now.Month + "/" + DateTime.Now.Year;
            //TName.Text = Session["TreasuryName"].ToString (); //Application["Tnm"].ToString ();
        }

    }
    //private string GetUsername()
    //{
    //    string uname = "";
    //    SqlCommand cmd = new SqlCommand();
    //    DataSet ds = new DataSet();
    //    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"].ToString());
    //    ds = GeneralClass.GetDataset(cmd, "User_getUserName");
    //    cmd.Dispose();
    //    if (ds.Tables.Count > 0)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            uname = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
    //            ds.Dispose();
    //        }
    //        else
    //        {

    //            uname = Session["User"].ToString().ToUpper();
    //        }
           
    //    }
    //    else
    //    {

    //        uname = Session["User"].ToString().ToUpper();
    //    }
    //    return uname;
    //}


    protected void lnksh_Click(object sender, EventArgs e)
    {

    //    if (lblsh.Text.Equals("HideHeader"))
    //    {
    //        HMenuTable.Rows[0].Visible = false;
    //        lblsh.Text = "ShowHeader";
    //    }
    //    else
    //    {
    //        HMenuTable.Rows[0].Visible = true;
    //        lblsh.Text = "HideHeader";
    //    }

    }
    
   }
