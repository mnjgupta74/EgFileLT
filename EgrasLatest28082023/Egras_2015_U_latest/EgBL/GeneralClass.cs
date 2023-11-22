using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using EgDAL;

namespace EgBL
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
            DataSet ds = new DataSet();
            return ds;//CommonFunction.FillDropDown(cmd, sp);
        }


        public enum Modes
        {

            AddMode = 1, EditMode = 2, DeleteMode = 3, ViewMode = 4

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


    }
}
