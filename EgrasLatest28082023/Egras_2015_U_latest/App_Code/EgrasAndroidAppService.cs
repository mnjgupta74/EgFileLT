using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using EgBL;
using Microsoft.Reporting.WebForms;
using System.Globalization;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgrasAndroidAppService" in code, svc and config file together.
[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]


public class EgrasAndroidAppService : IEgrasAndroidAppService
{
    System.Runtime.Caching.ObjectCache cache;
    AndroidAppBL androidAppBL;
    AppEncryptionDecryption objEncDec;
    private const string DeptCacheKey = "Dept_Egras_Key";
    private const string TreasuryCacheKey = "Treas_Egras_Key";
    private const string DistrictCacheKey = "Dist_Egras_Key";
    private const string OnlineBanksCacheKey = "Bank_Egras_Key";
    private string Key = "fd876698-89a7-46f5-bf5a-70a64f65d323";

    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }

    public DataTable GetDataTableFromJsonString(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {

            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }

    public string GetUserIDFromHeader()
    {
        //return "44";
        try
        {
            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if ((authHeader != null) && (authHeader != string.Empty))
            {
                AppEncryptionDecryption objAppEncDec = new AppEncryptionDecryption();
                var svcCredentials = objAppEncDec.DecryptText(System.Text.ASCIIEncoding.ASCII
                    .GetString(Convert.FromBase64String(authHeader.Substring(6))), Key)
                    .Split(':');
                SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
                return objDecrypt.DecryptString(svcCredentials[0], ConfigurationManager.AppSettings["AppKey"].ToString());
            }
            return "";
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetRNDFromHeader()
    {
        try
        {
            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if ((authHeader != null) && (authHeader != string.Empty))
            {
                AppEncryptionDecryption objAppEncDec = new AppEncryptionDecryption();
                var svcCredentials = objAppEncDec.DecryptText(System.Text.ASCIIEncoding.ASCII
                    .GetString(Convert.FromBase64String(authHeader.Substring(6))), Key)
                    .Split(':');
                SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
                return svcCredentials[1].Substring(0, 10);
            }
            return "";
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetLastModifiedFromHeader()
    {
        try
        {
            var authHeader = WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.LastModified];

            if ((authHeader != null) && (authHeader != string.Empty))
            {
                var svcCredentials = System.Text.ASCIIEncoding.ASCII
                            .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                            .Split('=');
                SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
                return svcCredentials[1];
            }
            return "";
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }


    //public string Login(string LoginID, string Pwd, string RND, string IPAddress)
    //{
    //    EgLoginBL objLogin = new EgLoginBL();
    //    SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
    //    string ReturnVal;
    //    objLogin.LoginID = LoginID;
    //    objLogin.Password = Pwd;
    //    objLogin.RND = RND;
    //    objLogin.IPAddress = IPAddress;
    //    ReturnVal = Convert.ToString(objLogin.GetAppLogin());
    //    return ReturnVal + "|" + objEncrypt.EncryptString(objLogin.UserId.ToString(), (ConfigurationManager.AppSettings["AppKey"].ToString())) + "|" + objLogin.UserType;
    //}



    #region GET Methods with No Parameters
    public string GetOnlineBanksList()
    {
        cache = System.Runtime.Caching.MemoryCache.Default;
        if (cache.Contains(OnlineBanksCacheKey))
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + cache.Get(OnlineBanksCacheKey).ToString(), Key);
        else
        {
            try
            {
                androidAppBL = new AndroidAppBL();
                objEncDec = new AppEncryptionDecryption();
                string returnval = androidAppBL.GetBanksList();
                System.Runtime.Caching.CacheItemPolicy cacheItemPolicy = new System.Runtime.Caching.CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(OnlineBanksCacheKey, returnval, cacheItemPolicy);
                return objEncDec.EncryptText(GetRNDFromHeader() + "|" + returnval, Key);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
    public string GetContactUsData()
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            EgAddContactUsBL objEgAddContactUsBL = new EgAddContactUsBL();
            objEncDec = new AppEncryptionDecryption();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + DataTableToJSONWithJavaScriptSerializer(objEgAddContactUsBL.GetData()), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public string GetCTDInformation()
    {
        try
        {
            EgEChallanBL objEgEchallanBL = new EgEChallanBL();
            objEncDec = new AppEncryptionDecryption();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + DataTableToJSONWithJavaScriptSerializer(objEgEchallanBL.GetCTDInformation()), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public string GetDistrictList()
    {
        cache = System.Runtime.Caching.MemoryCache.Default;
        if (cache.Contains(DistrictCacheKey))
            return cache.Get(DistrictCacheKey).ToString();
        else
        {
            try
            {
                androidAppBL = new AndroidAppBL();
                objEncDec = new AppEncryptionDecryption();
                string returnval = androidAppBL.GetDistrictsList();
                System.Runtime.Caching.CacheItemPolicy cacheItemPolicy = new System.Runtime.Caching.CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(DistrictCacheKey, returnval, cacheItemPolicy);
                return returnval;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
    public string GetTreasuryByOffice(string officeid)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.OfficeID = Convert.ToInt32(officeid);
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetTreasuryByOffice(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public string GetDepartmentList()
    {           
        cache = System.Runtime.Caching.MemoryCache.Default;
        if (cache.Contains(DeptCacheKey))
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + cache.Get(DeptCacheKey).ToString(), Key);
        else
        {
            try
            {
                androidAppBL = new AndroidAppBL();
                objEncDec = new AppEncryptionDecryption();
                string returnval = androidAppBL.GetDepartmentsList();
                System.Runtime.Caching.CacheItemPolicy cacheItemPolicy = new System.Runtime.Caching.CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(DeptCacheKey, returnval, cacheItemPolicy);
                return objEncDec.EncryptText(GetRNDFromHeader() + "|" + returnval, Key);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
    public string GetMajorHeadList(string DeptCode)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.DeptCode = DeptCode;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetMajorHeadList(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public string GetChallanTypebyGRN(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.GRN = GRN;
            androidAppBL.UserID = GetUserIDFromHeader();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetChallanTypeGRN(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public string GetChallanTypebyProfile(string ProfileID)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserProfile = ProfileID;
            androidAppBL.UserID = GetUserIDFromHeader();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetChallanTypeProfile(), Key); 
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public string GetBudgetHeadList(string Parameter)
    {
        try
        {
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('|').ToList();
            EgUserProfileBL objProfileBL = new EgUserProfileBL();
            objEncDec = new AppEncryptionDecryption();
            objProfileBL.DeptCode = Convert.ToInt32(returnValues[0]);
            objProfileBL.majorheadcode = returnValues[1];
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + DataTableToJSONWithJavaScriptSerializer(objProfileBL.GetSchemaBudgetName()), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public string InsertNewProfileData(string Parameter)
    {
        //Parameter = "[{BudgedHead:0070-01-800-04-05-अन्य विविध प्राप्तियां,ScheCode:600001-0},{BudgedHead:0070-01-900-00-00-घटाइये वापसियां,ScheCode:600002-0},{BudgedHead:0070-60-800-12-00-अन्य शुल्क,ScheCode:600003-0},{BudgedHead:0070-60-800-20-00-अन्य विविध प्राप्तियां,ScheCode:600005-0}]|fg|152";
        try
        {
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('|').ToList();
            DataTable dt = new DataTable();
            objEncDec = new AppEncryptionDecryption();
            dt = GetDataTableFromJsonString(returnValues[0].ToString());
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + BindInsertAndUpdateData(dt, returnValues[1].ToString(), Convert.ToInt32(returnValues[2])).ToString(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public int BindInsertAndUpdateData(DataTable dt, string Profilename, int DeptCode)
    {
        try
        {
            EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
            List<EgUserProfileBL> listReocord = new List<EgUserProfileBL>();

            foreach (DataRow li in dt.Rows)
            {
                objUserProfileBL = new EgUserProfileBL();
                objUserProfileBL.UserId = Convert.ToInt32(GetUserIDFromHeader());
                string[] SplitBudgethead = li[0].ToString().Split('-');
                objUserProfileBL.BudgetHead = SplitBudgethead[0].ToString() + SplitBudgethead[1].ToString() + SplitBudgethead[2].ToString() + SplitBudgethead[3].ToString() + SplitBudgethead[4].ToString();
                string[] Value = li[1].ToString().Split('-');
                if (Convert.ToInt32(Value[0]) > 100000)
                {
                    objUserProfileBL.ScheCode = 0;
                }
                else
                {
                    objUserProfileBL.ScheCode = Convert.ToInt32(Value[0].ToString());
                }
                objUserProfileBL.DeptCode = DeptCode;
                objUserProfileBL.ProfileName = Profilename;
                GenralFunction gf = new GenralFunction();
                //System.Data.SqlClient.SqlTransaction Trans = gf.Begintrans();
                int Maxpro = objUserProfileBL.GetMaxUserPro();
                objUserProfileBL.UserPro = Convert.ToInt32(Maxpro + 1);
                listReocord.Add(objUserProfileBL);
            }
            if (listReocord.Count > 0)
            {

                int i = objUserProfileBL.InsertUserProfile(listReocord);
                if (i == 1)
                {
                    return 1;
                    //BindValue();
                    //       ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Recored Save Successfully')", true);
                }
                else
                {
                    return 0;
                    //         ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Recored not Saved.')", true);
                }
            }
            else
            {
                return 0;
                //   ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please fill the records');", true);
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public string GetTreasuryList()
    {
        cache = System.Runtime.Caching.MemoryCache.Default;
        if (cache.Contains(TreasuryCacheKey))
            return cache.Get(TreasuryCacheKey).ToString();
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            string returnval = androidAppBL.GetTreasuryList();
            System.Runtime.Caching.CacheItemPolicy cacheItemPolicy = new System.Runtime.Caching.CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
            cache.Add(TreasuryCacheKey, returnval, cacheItemPolicy);
            return returnval;
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    #endregion



    public string GetLastTransactionsList()
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetTopTransactions(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetFullName()
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + DataTableToJSONWithJavaScriptSerializer(androidAppBL.GetUserFullName()), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetUserProfileList()
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            string LastModified = GetLastModifiedFromHeader();

            //string input = LastModified;
            //DateTime d;
            //String parsedDate = DateTime.ParseExact(LastModified,
            //                              "yyyy/MM/dd",
            //                              CultureInfo.InvariantCulture).ToString();
            //str = DateTime.TryParseExact(input, "MM-dd-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d).ToString();
            //if (DateTime.TryParseExact(input, "MM-dd-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
            //{
            //    str = d.ToString("yyyy/MM/dd");    
            //}
            if (LastModified != "")
                //06-29-2020 11:40:18 AM
                androidAppBL.LastModified = DateTime.Parse(LastModified);
            else
                androidAppBL.LastModified = DateTime.Parse("2000/01/01");
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetUserProfileList(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string InsertErrorLog(string ErrorName, string PageName)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.ErrorName = ErrorName;
            androidAppBL.Page_Name = PageName;
            androidAppBL.GetErrorLog();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "1", Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string InsertCatchErrorLog(string Parameter)
    {
        try
        {
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('*').ToList();
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.ErrorName = returnValues[0];
            androidAppBL.Page_Name = returnValues[1];
            androidAppBL.GetCatchErrorLog();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "1", Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetGRNPDF(string GRN)
    {
        try
        {
            ReportViewer objReport = new ReportViewer();
            objEncDec = new AppEncryptionDecryption();
            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("UserId", GetUserIDFromHeader());
            param[1] = new ReportParameter("Usertype", Convert.ToString(10));
            param[2] = new ReportParameter("GRN", Convert.ToString(GRN));
            param[3] = new ReportParameter("ChallanNo", Convert.ToString(0));
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(objReport, "EgDefaceDetailNew", param);
            ////create PDF
            byte[] returnValue = null;
            string format = "PDF";
            string deviceinfo = "";
            string mimeType = "";
            string encoding = "";
            string extension = "pdf";
            string[] streams = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            returnValue = objReport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + Convert.ToBase64String(returnValue), Key); 
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetGRNSearch(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.GRN = GRN;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetGRNSearchData(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetGRNSchema(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            //androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.GRN = GRN;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetGRNSchemas(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetChallanSchemas(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.GRN = GRN;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetChallanSchemas(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetGRNStatus(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.GRN = GRN;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetGRNStatus(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetCreateChallanDetailData(string Parameter)
    {
        try
        {
            //Parameter = "QYBrR32EFjpNVNX++YJwMg==";
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            List<string> requestData = objEncDec.DecryptText(Parameter, Key).Split('|').ToList<string>();
            androidAppBL.GRN = Convert.ToString(requestData[0]);
            androidAppBL.DeptCode = Convert.ToString(requestData[1]);

            if (requestData[2].ToString() == "True")
            {
                if (requestData[3].ToString() == "True")
                {
                    androidAppBL.ServiceId = Convert.ToString(requestData[0]);
                    
                    string ChallanType = androidAppBL.GetChallanTypeService().Trim();
                    string DeptNAme = androidAppBL.GetDeaprtmentList_Service().Trim();
                    return objEncDec.EncryptText(GetRNDFromHeader() + "|" + GetDistrictList().Trim() + "|" + GetTreasuryList().Trim() + "|" + ChallanType + "|" + DeptNAme + "|", Key);
                }
                else
                {
                    androidAppBL.UserProfile = Convert.ToString(requestData[0]);
                    string ChallanType = androidAppBL.GetChallanTypeProfile().Trim();
                    string DeptNAme = androidAppBL.GetDeaprtmentList().Trim();
                    return objEncDec.EncryptText(GetRNDFromHeader() + "|" + GetDistrictList().Trim() + "|" + GetTreasuryList().Trim() + "|" + ChallanType + "|" + DeptNAme + "|", Key);
                }
            }
            else
            {
                string ChallanType = androidAppBL.GetChallanTypeGRN().Trim();
                string DeptNAme = androidAppBL.GetDeaprtment(Convert.ToInt64(androidAppBL.GRN)).Trim();
                string TreasOffice = androidAppBL.GetTreasuryOfficeDetail().Trim();
                return objEncDec.EncryptText(GetRNDFromHeader() + "|" + GetDistrictList().Trim() + "|" + GetTreasuryList().Trim() + "|" + ChallanType + "|" + DeptNAme + "|" + TreasOffice, Key);
            }
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetUserProfileDetail(string Parameter)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            androidAppBL.UserId = Convert.ToInt32(GetUserIDFromHeader());
            objEncDec = new AppEncryptionDecryption();
            string result = androidAppBL.EditData();
            //result = result.Split('|').GetValue(0).ToString().Trim() + " " + result.Split('|').GetValue(1).ToString().Trim() + "|" + result.Split('|').GetValue(4).ToString().Trim() + "|" + result.Split('|').GetValue(5).ToString().Trim() + "|" + "000000" + "|" + result.Split('|').GetValue(6).ToString().Trim() + "|";
            //result = result.Split('|').GetValue(0).ToString().Trim() + "|" + result.Split('|').GetValue(3).ToString().Trim() + "|" + result.Split('|').GetValue(4).ToString().Trim() + "|" + result.Split('|').GetValue(6).ToString().Trim() + "|" + result.Split('|').GetValue(5).ToString().Trim() + "|";  //+ result.Split('|').GetValue(7).ToString().Trim() + "|";
            result = result.Split('|').GetValue(0).ToString().Trim() + "|" + result.Split('|').GetValue(4).ToString().Trim() + "|" + result.Split('|').GetValue(5).ToString().Trim() + "|" + result.Split('|').GetValue(6).ToString().Trim() + "|" + result.Split('|').GetValue(7).ToString().Trim() + "|";  //+ result.Split('|').GetValue(7).ToString().Trim() + "|";
           // FirstName + LastName + "|" + DateOfbirth + "|" + Email + "|" + Address + "|" + City + "|" + MobileNo + "|" + pinCode;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + result, Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetDepartmentName_FromGRN(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            //androidAppBL.UserID = GetUserIDFromHeader();
            //androidAppBL.GRN = GRN;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetDeaprtment(Convert.ToInt64(GRN)), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetGRNDetail(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.GRN = GRN;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetGRNDetail() + "|" + androidAppBL.GetGRNSchemas(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string CreateChallanSchemas(string UserProfile)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.UserProfile = UserProfile;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.CreateChallanSchemas(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string CreateServiceSchemas(string ServiceId)
    {
        try
        {
            //return ServiceId;
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.ServiceId = ServiceId.Split('|').GetValue(0).ToString();
            androidAppBL.DeptCode = ServiceId.Split('|').GetValue(1).ToString();
            //return objEncDec.EncryptText(androidAppBL.CreateServiceSchemas(), Key);
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.CreateServiceSchemas(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string FillUserSchema()
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            //androidAppBL.UserProfile = UserProfile;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.FillUserSchema(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetTreasuryOfficeDetail(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.GRN = GRN;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetTreasuryOfficeDetail(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetPayeeDetail(string GRN)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.GRN = GRN;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetPayeeDetail(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string GetDepartmentName_FromProfileID(string ProfileID)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.UserProfile = ProfileID;
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetDeaprtmentList(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string Repeatability(string GRN)
    {
        try
        {
            //GRN = "6468723";
            string Msg = "";
            EgVisibility ObjVisibility = new EgVisibility();
            objEncDec = new AppEncryptionDecryption();
            AppCheckChallanConditionBL ObjAppCheck = new AppCheckChallanConditionBL();
            EgCheckChallanCondition ObjCheck = new EgCheckChallanCondition();
            if (ObjVisibility.IsEligible(Convert.ToInt64(GRN)))
            {
                if (!ObjAppCheck.CheckCondition(Convert.ToInt64(GRN), Convert.ToInt32(GetUserIDFromHeader())))
                {
                    ObjCheck.BudgetHead = ObjAppCheck.BudgetHead;
                    ObjCheck.DeptCode = ObjAppCheck.DeptCode;
                    if (Convert.ToInt32(ObjAppCheck.BudgetHead.ToString().Substring(0, 4)) < 7999)
                    {
                        if (!ObjCheck.CheckHead004000102())
                        {
                            if (!ObjCheck.CheckHead0030())
                            {
                                if (!ObjCheck.CheckCTDCase())
                                {
                                    DataTable dt = ObjAppCheck.CheckBudgetHeadVisibilty();
                                    if (dt.Rows.Count == 0)
                                        return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "0" + "*" + Msg, Key);
                                    else
                                        return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "4" + '*', Key);// Division code Return
                                }
                                else
                                    return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "3" + '*' + Msg, Key);// Ctd

                            }
                            else
                                return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "2" + '*' + Msg, Key);//  Stamp  
                        }
                        else
                        {
                            Msg = "Warning: This Budget head is going out from Feb 1,2015. Hence select new budget head 0040-00-111 for all type of payments under VAT budget head.";
                            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "5" + '*' + Msg, Key);//  '004000102'  
                        }
                    }
                    else
                    {
                        Msg = "8000  Above BudgetHead Challan Make With Web Application";
                        return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "5" + '*' + Msg, Key);//  Pd Challan 
                    }
                }
                else
                {
                    return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "1" + '*' + ObjAppCheck.msg, Key);// Check Sumit Condition with Message
                }
            }
            else
                return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "-1" + '*' + "Profile/BudgetHead/Department/Office May Be Closed", Key);//   Stop  Repeat Transacrion 


        }
        catch (Exception ex)
        {
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "-2*", Key);// generate Any Typre exception
        }
    }

    public string Create_Ability(string UserProfile)
    {
        try
        {
            string Msg = "";
            AppCheckChallanConditionBL ObjAppCheck = new AppCheckChallanConditionBL();
            objEncDec = new AppEncryptionDecryption();
            EgCheckChallanCondition ObjCheck = new EgCheckChallanCondition();
            ObjAppCheck.GetProfileConditionalData(Convert.ToInt32(UserProfile), Convert.ToInt32(GetUserIDFromHeader()));

            ObjCheck.BudgetHead = ObjAppCheck.BudgetHead;
            ObjCheck.DeptCode = ObjAppCheck.DeptCode;
            if (Convert.ToInt32(ObjAppCheck.BudgetHead.ToString().Substring(0, 4)) < 7999)
            {
                if (!ObjCheck.CheckHead004000102())
                {
                    if (!ObjCheck.CheckHead0030())
                    {
                        if (!ObjCheck.CheckCTDCase())
                        {
                            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "0" + "*" + Msg, Key);
                        }
                        else
                        {
                            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "3" + '*' + Msg, Key);// Ctd
                        }
                    }
                    else
                    {
                        return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "2" + '*' + Msg, Key); //  Stamp
                    }
                }
                else
                {
                    Msg = "Warning: This Budget head is going out from Feb 1,2015. Hence select new budget head 0040-00-111 for all type of payments under VAT budget head.";
                    return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "5" + '*' + Msg, Key);//  '004000102'  
                }
            }
            else
            {
                Msg = "8000  Above BudgetHead Challan Make With Web Application";
                return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "5" + '*' + Msg, Key);//  Pd Challan 
            }

        }
        catch (Exception ex)
        {

            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "-2*", Key);// generate Any Typre exception
        }
    }

    public string Create_Ability_Service(string ServiceId)
    {
        try
        {
            string Msg = "";
            AppCheckChallanConditionBL ObjAppCheck = new AppCheckChallanConditionBL();
            objEncDec = new AppEncryptionDecryption();
            EgCheckChallanCondition ObjCheck = new EgCheckChallanCondition();
            ObjAppCheck.GetServiceConditionalData(Convert.ToInt32(ServiceId), Convert.ToInt32(GetUserIDFromHeader()));

            ObjCheck.BudgetHead = ObjAppCheck.BudgetHead;
            ObjCheck.DeptCode = ObjAppCheck.DeptCode;
            ObjCheck.proc_id = ObjAppCheck.proc_id;
            if (Convert.ToInt32(ObjAppCheck.BudgetHead.ToString().Substring(0, 4)) < 7999 )
            {
                if (!ObjCheck.CheckHead004000102())
                {
                    if (!ObjCheck.CheckHead0030())
                    {
                        if (!ObjCheck.CheckCTDCase())
                        {
                            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "0" + "*" + Msg, Key);
                        }
                        else
                        {
                            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "3" + '*' + Msg, Key);// Ctd
                        }
                    }
                    else
                    {
                        return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "2" + '*' + Msg, Key); //  Stamp
                    }
                }
                else
                {
                    Msg = "Warning: This Budget head is going out from Feb 1,2015. Hence select new budget head 0040-00-111 for all type of payments under VAT budget head.";
                    return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "5" + '*' + Msg, Key);//  '004000102'  
                }
            }
            else
            {
                Msg = "8000  Above BudgetHead Challan Make With Web Application";
                return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "5" + '*' + Msg, Key);//  Pd Challan 
            }

        }
        catch (Exception ex)
        {
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + "-2*", Key);// generate Any Typre exception
        }
    }

    public string GetOfficeList(string Parameter)
    {
        try
        {
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('|').ToList();
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.DeptCode = returnValues[0];
            androidAppBL.TreasuryCode = returnValues[1];
            //androidAppBL.UserID = GetUserIDFromHeader();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetOfficeList(), Key); 
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }

    }

    public string GetDivisionList(string Parameter)
    {
        try
        {
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('|').ToList();
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.OfficeID = Convert.ToInt32(returnValues[0]);
            androidAppBL.TreasuryCode = returnValues[1].ToString();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetDivisionList(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }

    }

    public string TinValidation(string Parameter)
    {
        try
        {
            string TinNo;
            int GroupCode;
            DateTime Fromdate;
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('|').ToList();
            TinNo = returnValues[0];
            GroupCode = Convert.ToInt32(returnValues[0]);
            Fromdate = DateTime.Parse(returnValues[0]);
            TinValidation ObjTinValidation = new TinValidation();
            objEncDec = new AppEncryptionDecryption();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + ObjTinValidation.TinNo(TinNo, GroupCode, Fromdate), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string CreateNewChallan(string objData)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            //objData = "zy5psQomcQfExNmcsSiX0c6Tcqgi15Zz7SrUvgXpS6wHz0TrDZJpniT/1WeaKloB5+OrCWx3///x1CwFWJ6wL3ZUPaPFD32YpA6l6VhDszElXiRnkH0bEu6XWGAoF5IaCxStBR2qaIWVeRlAr+WjsBRcxyQQfcDiVeGQA0is+X4LwQCgtzaYnVcKYZ2MbMuypIxd0YhhICoB5cXOhQ6rBa91AwOuR6/f9bylRrIGtTdKXh3dqpyEQr9l2tw9FKkSBWBvOTx0tutU8TeVrzfVLTr7Mvzs+rbb3b0Ofwmhu13zk4xyFB6l0ZREsNYa1EWWPqgYVE/G6LovXBNEynzXAiXXdcQVZGoAbSEm2aijM0vN0Mq5q4t//T5ogr8X1rTsb0QeKxOo7sikvmCOK1yLqPef11BAhD09SBsVRSUIF7tIHdGOMngNeiLCmfNAe9TNQwpCUQhJlwmmnM5qbEF0I5haIJnVyuBsXObAV90RL1jjwTNqXfSk6GaVhyelqbS005LkomPVSAVXlbbthnyZn0+l/RNfypVJLA5whcjAHQ6mu+djaWlCI07EP/IJVUBj4/gcoh3EgO7XhY1fc8kW+5lWjqbVhi6B80sehtb+U/GUBEx6xc395NzVMQAr5X3enAhuW+Z9WV03S+QoD6nCaoER4DpwJK4qUxkzGhe1D2uaGPYMr6Kk3OrzpAsfb8c3fTzSgSnT/pXM2fszGR4JPsuabUM7vRyiuFAWlX/90EYdBDOWdGd7zuDp6hG4jBpmhXATjP9yvcTatv7WLY6ijqEfDOmlOadQ5sI6rlkn8Tmt8Coz7Ji0wXROie0NqtW188bqLf1bxxqF+HBMIYf3ZKVo9l76WILhPUPF03gbpAdZTWkgeaOHS+nPUk1w6FrrETWk2/0gGtuXxt+skQPYTmEu9R6yDZuBEdGULZEcttJYU3sOINOkF+fM6iwmOQjwRMb3pyjdzrb3QCqcrMNtpzwEJ1rxdAbk7s9DWtBn4K0=";
            objEncDec = new AppEncryptionDecryption();
            objData = objEncDec.DecryptText(objData, Key);
            string GRN = androidAppBL.InsertChallan(objData, Convert.ToInt32(GetUserIDFromHeader()));
            //string GRN = androidAppBL.InsertChallan(objData, 146764);
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + GRN, Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string ChangePassword(string Parameter)
    {
        objEncDec = new AppEncryptionDecryption();
        SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
        Parameter = objEncDec.DecryptText(Parameter, Key);
        List<string> returnValues = new List<string>();
        returnValues = Parameter.Split('|').ToList();
        EgChangePasswordBL objChangePassword = new EgChangePasswordBL();
        objChangePassword.OldPassword = returnValues[0];
        objChangePassword.Password = returnValues[1];//GeneralClass.Md5AddSecret(txtNewPassword.Text.Trim());
        objChangePassword.UserId = Convert.ToInt32(objEncrypt.DecryptString(returnValues[2], (ConfigurationManager.AppSettings["AppKey"].ToString())));
        string output = objChangePassword.UpdateNewPassword();
        return objEncDec.EncryptText(GetRNDFromHeader() + "|" + objEncDec.EncryptText(output.Trim() + "|" + returnValues[3], Key), Key);
    }

    public string GetServiceList(string DeptCode)
    {
        androidAppBL = new AndroidAppBL();
        objEncDec = new AppEncryptionDecryption();
        androidAppBL.DeptCode = DeptCode;
        androidAppBL.Usertype = 10;
        return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.GetServiceNameList(), Key);
    }

    public string GetServiceDepartmentList()
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            string returnval = androidAppBL.GetServiceDepartmentsList();
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + returnval, Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    /// <summary>
    /// service challan - 31/12/19
    /// </summary>
    /// <param name="ServiceId"></param>
    /// <returns></returns>
    public string ServiceChallan(string ServiceId)
    {
        try
        {
            //return ServiceId;
            androidAppBL = new AndroidAppBL();
            objEncDec = new AppEncryptionDecryption();
            androidAppBL.UserID = GetUserIDFromHeader();
            androidAppBL.ServiceId = ServiceId.Split('|').GetValue(0).ToString();
            androidAppBL.DeptCode = ServiceId.Split('|').GetValue(1).ToString();
            //return objEncDec.EncryptText(androidAppBL.CreateServiceSchemas(), Key);
            return objEncDec.EncryptText(GetRNDFromHeader() + "|" + androidAppBL.CreateServiceChallan(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

}
