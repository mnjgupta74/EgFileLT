using System;
using System.Data;
using System.Web;
using EgBL;


public partial class UserControls_VerticalMenu : System.Web.UI.UserControl
{

    // MstMenuBL objMstMenuBL = new MstMenuBL();
    //TMstMenuBL objTMstMenuBL = new TMstMenuBL();
    EgMstUserLogin objTMstMenuBL = new EgMstUserLogin();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        //{
        //    Response.Write("<Script>alert('Session Expired')</Script>");
        //     Server.Transfer("~\\logout.aspx");
        //}
        if (Session["UserId"] == null)
        {
            Server.Transfer("~/Default.aspx");
        }
        if (!IsPostBack)
        {
          Session["MenuDataSet"] = "";
            popMenu();
        }

    }
    private void popMenu()
    {
        //************************updation For Menu loading at a single time**************-------
        DataSet ds1 = new DataSet();
        int ind;
        if (Session["UserId"] != null && Session["UserId"] != "")
        {
            objTMstMenuBL.userId = Convert.ToInt32(Session["UserId"]);
            objTMstMenuBL.userType = Convert.ToInt32(Session["UserType"]);
            //Session["MenuDataSet"] = "";
            if (Session["MenuDataSet"].ToString() == "")
            {
                //ds1 = objMstMenuBL.GetMenuByUserId();
                ds1 = objTMstMenuBL.GetMenuByUserId();
                Session["MenuDataSet"] = ds1;
                // ds1 = MenuVisible(ds1);
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ds1.DataSetName = "Menus";
                        ds1.Tables[0].TableName = "Menu";
                        DataRelation relation = new DataRelation("ParentChild",
                        ds1.Tables[0].Columns["MenuId"],
                        ds1.Tables[0].Columns["MenuParentId"], false);
                        relation.Nested = true;
                        ds1.Relations.Add(relation);
                        xmlDataSource.Data = ds1.GetXml();
                    }
                }
            }
            else
            {
                ds1 = null;
                ds1 = (DataSet)Session["MenuDataSet"];
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        xmlDataSource.Data = ds1.GetXml();
                    }
                }
            }
            //code for the user which does not have permission on that page
            string url = HttpContext.Current.Request.Url.ToString().ToUpper();

            int indLast = url.LastIndexOf(".ASPX");
            //int ind = url.LastIndexOf("WEBPAGES");
            if (url.LastIndexOf("WEBPAGES") == -1)
            {
                ind = url.LastIndexOf("GUEST");
            }
            else
            {
                ind = url.LastIndexOf("WEBPAGES");
            }
            int lenght = indLast - ind;
            string pg = url.Substring(ind, lenght).ToString();
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if ((pg.Substring(0, 5) != "GUEST"))
                    {
                        DataRow[] filteredRows = ds1.Tables[0].Select("Navigateurl like '%" + pg + "%'");
                        DataRow[] filteredRows1 = ds1.Tables[1].Select("Navigateurl like '%" + pg + "%'");
                        if (filteredRows.Length == 0 && filteredRows1.Length == 0)
                        {
                            EgBL.GeneralClass.ShowMessageBox("Access Denied");
                            Session.Abandon();
                            Response.Redirect("~/Default.aspx");

                        }
                    }
                }
            }
            ds1.Dispose();
        }
    }

    private DataSet MenuVisible(DataSet ds)
    {

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    if (ds.Tables[0].Rows[i]["MenuVisible"].ToString() == "N")
                    {
                        ds.Tables[0].Rows[i].Delete();
                    }
                }
            }
        }
        return ds;
    }


}
