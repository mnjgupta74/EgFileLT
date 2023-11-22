using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
//using TrgOapBL;
using Treasury;
/// <summary>
/// Summary description for GeneralClass
/// </summary>
/// 
namespace NIC.Utilities
{
    public static class GeneralClass
    {
        
        public static bool IsNumeric(string strToCheck)
        {
            return Regex.IsMatch(strToCheck, "^\\d+(\\.\\d+)?$");

        }
        public static string Md5AddSecret(string strChange)
        {
            string strPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strChange, "MD5");
            return strPassword;
        }

        public static string convertDateIntoString(string dat)
        {
            string d = "";
            if (dat != "")
            {
                string yy = dat.Substring(6, 4).ToString();
                string mm = dat.Substring(3, 2).ToString();
                string dd = dat.Substring(0, 2).ToString();

                d = yy + mm + dd;
            }
            return d;
        }
        public static DataSet GetDataset(SqlCommand cmd, string sp)
        {
            DataSet ds= new DataSet();
            return ds;//CommonFunction.FillDropDown(cmd, sp);
        }
        
        //public static void ShowMessageBox(string msg, UpdatePanel pnID)
        //{
        //    //  Get a ClientScriptManager reference from the Page class.
        //    msg = msg.Replace("'", " ");
        //    msg = msg.Replace("\r", "");
        //    msg = msg.Replace("\n", "");
        //    if (HttpContext.Current == null)
        //    {
        //        throw new Exception("Method must be called from a page context");
        //    }

        //    Page page = HttpContext.Current.Handler as Page;

        //    if (page == null)
        //    {
        //        throw new Exception("Method must be called from a page context");
        //    }
        //    ClientScriptManager cs = page.ClientScript;
        //    // Check to see if the startup script is already registered.

        //    if (!cs.IsStartupScriptRegistered(cs.GetType(), "PopupScript"))
        //    {
        //        //cs.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + msg + "');", true);
        //        ScriptManager.RegisterClientScriptBlock(pnID, cs.GetType(), "PopupScript", "alert('" + msg + "');", true);
        //    }

        //}
        public enum Modes
        {

            AddMode = 1, EditMode = 2, DeleteMode = 3, ViewMode = 4

        }


        //public static int GetAccesslevel(int usrid)
        //{
        //    IfmsBL.MstUserBL usr = new IfmsBL.MstUserBL(); // get user detials
        //    string url = HttpContext.Current.Request.Url.ToString();
        //    int ind = url.LastIndexOf("/");
        //    string pg = url.Substring(ind + 1);
        //    string results;
        //    usr.NavigateUrl = pg.Trim();
        //    usr.UserId = usrid;
        //    results = usr.GetAccessLevel();
        //    if (IsNumeric(results))
        //    {
        //        results = results;
        //    }
        //    else
        //    {
        //        //InsertErr(results, "Front End");
        //    }
        //    return Convert.ToInt32(results);
        //}
        public static void InsertErr(string mgs, string spName)
        {
            CommonFunction.InsertErr(mgs, spName);
        }
        public static int GetMaxId(string sp, string fieldname, string tbname)
        {
            return Convert.ToInt32(CommonFunction.GetMaxId(sp, fieldname, tbname));
        }
        public static int CheckRecordExistence(string fieldname, string tbname, string para)
        {
                   //TrgBL.Commonfunction.CheckRecordExistence

            return Convert.ToInt32(CommonFunction.CheckRecordExistence(fieldname, tbname, para));
           // return 0;
        }
        public static Boolean AccessPage(char Role)
        {
            if (Convert.ToString(HttpContext.Current.Session["Role"]).IndexOf(Role) == -1)
            { return false; }
            else
            { return true; }
        }
        public static void ShowMessageBox(string msg)
        {
            //  Get a ClientScriptManager reference from the Page class.
            msg = msg.Replace("'", " ");
            msg = msg.Replace("\r", "");
            msg = msg.Replace("\n", "");
            if (HttpContext.Current == null)
            {
                throw new Exception("Method must be called from a page context");
            }

            Page page = HttpContext.Current.Handler as Page;

            if (page == null)
            {
                throw new Exception("Method must be called from a page context");
            }
            ClientScriptManager cs = page.ClientScript;
            // Check to see if the startup script is already registered.

            if (!cs.IsStartupScriptRegistered(cs.GetType(), "PopupScript"))
            {
                //cs.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + msg + "');", true);
                cs.RegisterStartupScript(cs.GetType(), "PopupScript", "alert('" + msg + "');", true);
            }

        }
        public static void PopulateDropDown(SqlCommand cmd, string sp, DropDownList dl, string textFieldIndexNumber, string valueFieldIndexNumber)
        {

            try
            {

                dl.DataSource = CommonFunction.FillDropDown(cmd, sp);
                dl.DataTextField = textFieldIndexNumber;
                dl.DataValueField = valueFieldIndexNumber;
                dl.DataBind();
            }
            catch (Exception ex)
            {
                //

            }
            finally
            {

                //db.InsertErr("", sp);


            }

        }
        public static void PopulateDropDownSelectAny(SqlCommand cmd, string sp, DropDownList dl, string textFieldIndexNumber, string valueFieldIndexNumber, bool bSelect)
        {

            try
            {

                dl.DataSource = CommonFunction.FillDropDown(cmd, sp);
                dl.DataTextField = textFieldIndexNumber;
                dl.DataValueField = valueFieldIndexNumber;
                dl.DataBind();
                if (bSelect == true)
                    dl.Items.Insert(0, "Select Any");
            }
            catch (Exception ex)
            {
                //

            }
            finally
            {

                //db.InsertErr("", sp);


            }

        }
        public static void PopulateDropDown(SqlCommand cmd, string sp, ListBox dl, string textFieldIndexNumber, string valueFieldIndexNumber)
        {

            try
            {
                dl.DataSource = CommonFunction.FillDropDown(cmd, sp);
                dl.DataTextField = textFieldIndexNumber;
                dl.DataValueField = valueFieldIndexNumber;
                dl.DataBind();

            }
            catch (Exception ex)
            {
                InsertErr(ex.Message, sp);

            }
            finally
            {




            }

        }
      
        public static void PopupControlListBox(SqlCommand cmd, string sp, ListBox dl, string txtLikeParam, string textFieldIndexNumber, string valueFieldIndexNumber)
        {

            try
            {
                //DataTable dt = new DataTable();
                //DataSet ds = new DataSet(); 
                dl.DataSource = CommonFunction.FillPopupControlListBox(cmd, sp, txtLikeParam);
                //dt.DefaultView.RowFilter = txtLikeParam;
                //dl.DataSource=dt.DefaultView(); 
                dl.DataTextField = textFieldIndexNumber;
                dl.DataValueField = valueFieldIndexNumber;
                dl.DataBind();
            }
            catch (Exception ex)
            {
                InsertErr(ex.Message, sp);

            }
            finally
            {




            }

        }
    }

}
