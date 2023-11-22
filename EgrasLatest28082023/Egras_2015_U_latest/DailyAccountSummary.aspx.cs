using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DailyAccountSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenralFunction gf = new GenralFunction();
        SqlParameter[] PM = new SqlParameter[2];
        DataTable dt = new DataTable();
        PM[0] = new SqlParameter("@fromdate ", SqlDbType.Date) { Value = "2019-08-01" };
        PM[1] = new SqlParameter("@todate ", SqlDbType.Date) { Value = "2019-08-05" };
        // return gf.ExecuteScaler (PM, "IM_Server_Daily_Data");
         gf.Filldatatablevalue(PM, "IM_Server_Daily_Data", dt, null);
        string returndata = DataTableToJSONWithJSONNet(dt);
        DownloadFile(returndata);
        //    if (Request.QueryString.Count > 0)
        //    {
        //        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
        //        string strReqq = Request.Url.ToString();
        //        strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
        //        var PlainString = objDecrypt.DecryptAES256(strReqq, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "5028.key", null);
        //        EgDailyAccountBL objAccountService = new EgDailyAccountBL();
        //        string[] arrMsgs = PlainString.Split('|');
        //        objAccountService.FromDate = Convert.ToDateTime(arrMsgs[0]); ;
        //        objAccountService.ToDate = Convert.ToDateTime(arrMsgs[1]); ;
        //        GenralFunction gf = new GenralFunction();
        //        SqlParameter[] PM = new SqlParameter[2];
        //        DataTable dt = new DataTable();
        //        PM[0] = new SqlParameter("@fromdate ", SqlDbType.Date) { Value = FromDate };
        //        PM[1] = new SqlParameter("@todate ", SqlDbType.Date) { Value = ToDate };
        //        // return gf.ExecuteScaler (PM, "IM_Server_Daily_Data");
        //        return gf.Filldatatablevalue(PM, "IM_Server_Daily_Data", dt, null);

        //        string returndata = objAccountService.DailyAccountSummaryFile(strReqq); ;
        //        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        //        return System.Convert.ToBase64String(plainTextBytes);
        //        //DownloadFile(returndata);
        //        //objAccountService.GetFile();
        //        //string[] arrMsgs = PlainString.Split('|');
        //        //objAccountService.FromDate = Convert.ToDateTime(PlainString[0]);// SnaRefrence no
        //        //objAccountService.ToDate = Convert.ToDateTime(PlainString[1]);
        //        ////objAccountService.SnaDate = Convert.ToDateTime(PlainString);
        //        //string returndata = objAccountService.GetIMServerFile();
        //    }
        //}
        //Return Json String
    }
    public string DataTableToJSONWithJSONNet(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    public void DownloadFile(string data)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(data);
        sb.Append("\r\n");

        string text = sb.ToString();

        Response.Clear();
        Response.ClearHeaders();

        Response.AppendHeader("Content-Length", text.Length.ToString());
        Response.ContentType = "text/plain";
        Response.AppendHeader("Content-Disposition", "attachment;filename=\"AccountSummary.txt\"");

        Response.Write(text);
        Response.End();



    }
}