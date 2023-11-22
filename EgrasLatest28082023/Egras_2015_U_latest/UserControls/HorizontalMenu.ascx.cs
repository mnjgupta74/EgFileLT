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
using NIC.Utilities;
public partial class UserControls_HorizontalMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        //{
        //    Response.Write("<Script>alert('Session Expired')</Script>");
        //     Server.Transfer("~\\logout.aspx");
        //}
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (Session["User"] != null)
        {

        }

    }
    private string GetUsername()
    {
        string uname = "";
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"].ToString());
        ds = GeneralClass.GetDataset(cmd, "User_getUserName");
        cmd.Dispose();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                uname = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                ds.Dispose();
            }
            else
            {

                uname = Session["User"].ToString().ToUpper();
            }

        }
        else
        {

            uname = Session["User"].ToString().ToUpper();
        }
        return uname;
    }


    // protected void lnksh_Click(object sender, EventArgs e)
    // {

    //     if (lblsh.Text.Equals("HideHeader"))
    //     {
    //         HMenuTable.Rows[0].Visible = false;
    //         lblsh.Text = "ShowHeader";
    //     }
    //     else
    //     {
    //         HMenuTable.Rows[0].Visible = true;
    //         lblsh.Text = "HideHeader";
    //     }

    // }

}
