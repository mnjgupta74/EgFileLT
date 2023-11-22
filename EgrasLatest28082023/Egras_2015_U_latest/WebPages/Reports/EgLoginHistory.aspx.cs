using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using EgBL;


public partial class WebPages_EgLoginHistory : System.Web.UI.Page
{
    EgLoginAuditBL EgLoginAudit = new EgLoginAuditBL();

    protected void Page_Load(object sender, EventArgs e)
    {


        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }


        //............*******...............
        //Datatable from XML
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("DeptCode", typeof(string));
        dt1.Columns.Add("DeptNameEnglish", typeof(string));
        DataSet ds = new DataSet();
        //ds.ReadXml(@"EgOfficeCodes.xml");
        ds.ReadXml(Server.MapPath ("~/EgOfficeCodes.xml"));
        //ds.ReadXml(@"EgOfficeCodes.xml");
        dt1 = ds.Tables[0];



        //............*******...............
        ////DataTable from Database
        DataTable dt = new DataTable();
        dt = EgLoginAudit.FillrptLoginDetail();
        object sumObject;
        sumObject = dt.Compute("Sum(usercount)", "");
        var query1 = from p in dt.AsEnumerable()
                     where p.Field<int>("a") < 1
                     select new
                      {
                          UserId = p == null ? "" : p.Field<string>("UserId"),
                          UserCount = p == null ? 0 : p.Field<int>("UserCount")

                      };
        DataListLoginDetails.DataSource = query1;
        DataListLoginDetails.DataBind();



        //............*******...............
        //Code for Filling DataListOffice
        var query = from p in dt.AsEnumerable()
                    join q in dt1.AsEnumerable() on p.Field<string>("UserId").Trim() equals q.Field<string>("DeptCode").Trim() into UP
                    from q in UP.DefaultIfEmpty()
                    where p.Field<int>("a") > 0
                    select new
               {
                   UserId = p == null ? "" : p.Field<string>("UserId"),
                   UserCount = p == null ? 0 : p.Field<int>("UserCount"),
                   DeptNameEnglish = q == null ? "" : q.Field<string>("DeptNameEnglish")
               };
        
        if ( query.Count() > 0)
        {
            DataListOffice.DataSource = query;
            DataListOffice.DataBind();
        }
        else
        {
            Fieldset1.Visible = false;
        }
        LabelUser.Text = sumObject.ToString();

    }

    protected void DataListOffice_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Label UserCount = (Label)e.Item.FindControl("LabelUserCount");
            if (UserCount.Text == "0")
            {
                Image ImageStatus = (Image)e.Item.FindControl("Image1");
                ImageStatus.ImageUrl = "~/Image/red.png";

            }
            else
            {
                Image ImageStatus = (Image)e.Item.FindControl("Image1");
                ImageStatus.ImageUrl = "~/Image/green.png";
            }
        }
    }

    protected void DataListLoginDetails_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label UserCount = (Label)e.Item.FindControl("LabelUserCount");
            if (UserCount.Text == "0")
            {
                Image ImageStatus = (Image)e.Item.FindControl("Image1");
                ImageStatus.ImageUrl = "~/Image/red.png";

            }
            else
            {
                Image ImageStatus = (Image)e.Item.FindControl("Image1");
                ImageStatus.ImageUrl = "~/Image/green.png";
           }

        }
    }


  
}
