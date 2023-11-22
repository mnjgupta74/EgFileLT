using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace EgBL
{
    public class EgDailyAccountBL
    {

        public string token { get; set; }

        public string returndata { get; set; }
        public string enctype { get; set; }

        public string Auth { get; set; }
        public string type { get; set; }
        public string SCode { get; set; }
        public string key { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Mode { get; set; }

        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
        public string GetFileData()
        {
            return Validation(GetStatusCode(0));
        }

        // Imserver  daily Data
        public string GetIMServerData()
        {
            return Validation(GetStatusCode(1));
        }

        public string GetPay1Data()
        {
            return Validation(GetStatusCode(2));
        }
        public string GetPay2Data()
        {
            return Validation(GetStatusCode(3));
        }
        public string GetPay3Data()
        {
            return Validation(GetStatusCode(4));
        }
        public string GetPay4Data()
        {
            return Validation(GetStatusCode(5));
        }
        public string GetDMSData()
        {
            return Validation(GetStatusCode(6));
        }
        public string GetAGData()
        {
            return Validation(GetStatusCode(7));
        }

        private string GetStatusCode(int val)
        {
            try
            {

                //=======================================
                if (string.IsNullOrEmpty(Auth))
                    return SCode = "001";//Header Blank

                if (string.IsNullOrEmpty(enctype))
                    return SCode = "002";//  Parameter Empty

                token = Auth.Split('|')[0];
                if (string.IsNullOrEmpty(token))
                    return SCode = "003";// Get token 

                key = Auth.Split('|')[1];
                if (string.IsNullOrEmpty(key))
                    return SCode = "004";// Get Merchant Key

                SCode = VerifyToken();
                if (SCode != "200T")
                    return SCode;
                var PlainString = objDecrypt.DecryptAES256(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key", null);


                try
                {
                    //01/12/2018

                    if (val == 0)
                    {
                        FromDate = Convert.ToDateTime(PlainString);// SnaRefrence no
                        SCode = "200";
                        returndata = DataTableToJSONWithJSONNet(GetFile());
                        if (returndata == "{\"ResponseParam\":[]}")
                        {
                            SCode = "104";
                        }
                    }

                    else if(val == 1)
                    {
                        try
                        {
                            string[] arrMsgs = PlainString.Split('|');
                            FromDate = Convert.ToDateTime(arrMsgs[0]);// SnaRefrence no
                            ToDate = Convert.ToDateTime(arrMsgs[1]);// SnaRefrence no
                            // SnaRefrence no
                            SCode = "200";
                            returndata = DataTableToJSONWithJSONNet(GetIMServerFile());
                            if (returndata == "{\"ResponseParam\":[]}")
                            {
                                SCode = "104";
                            }
                        }
                        catch(Exception ex)
                        {
                            EgErrorHandller obj = new EgErrorHandller();
                            obj.InsertError(ex.Message.ToString());

                            SCode = "200F";
                            

                        }
                    }
                    else if(val == 2)
                    {
                        try
                        {
                            string[] arrMsgs = PlainString.Split('|');
                            Mode = 1;//Convert.ToInt32(arrMsgs[0]); // PayMode
                            FromDate = Convert.ToDateTime(arrMsgs[0]);// FromDate no
                            ToDate = Convert.ToDateTime(arrMsgs[1]);// ToDate no
                            // SnaRefrence no
                            SCode = "200";
                            returndata = DataTableToJSONWithJSONNet(GetPay1File());
                            if (returndata == "{\"ResponseParam\":[]}")
                            {
                                SCode = "104";
                            }
                        }
                        catch (Exception ex)
                        {
                            EgErrorHandller obj = new EgErrorHandller();
                            obj.InsertError(ex.Message.ToString());

                            SCode = "200F";
                        }
                    }
                    else if (val == 3)
                    {
                        try
                        {
                            string[] arrMsgs = PlainString.Split('|');
                            Mode = 2;//Convert.ToInt32(arrMsgs[0]); // PayMode
                            FromDate = Convert.ToDateTime(arrMsgs[0]);// FromDate no
                            ToDate = Convert.ToDateTime(arrMsgs[1]);// ToDate no
                            // SnaRefrence no
                            SCode = "200";
                            returndata = DataTableToJSONWithJSONNet(GetPay1File());
                            if (returndata == "{\"ResponseParam\":[]}")
                            {
                                SCode = "104";
                            }
                        }
                        catch (Exception ex)
                        {
                            EgErrorHandller obj = new EgErrorHandller();
                            obj.InsertError(ex.Message.ToString());

                            SCode = "200F";
                        }
                    }
                    else if (val == 4)
                    {
                        try
                        {
                            string[] arrMsgs = PlainString.Split('|');
                            Mode = 3;//Convert.ToInt32(arrMsgs[0]); // PayMode
                            FromDate = Convert.ToDateTime(arrMsgs[0]);// FromDate no
                            ToDate = Convert.ToDateTime(arrMsgs[1]);// ToDate no
                            // SnaRefrence no
                            SCode = "200";
                            returndata = DataTableToJSONWithJSONNet(GetPay1File());
                            if (returndata == "{\"ResponseParam\":[]}")
                            {
                                SCode = "104";
                            }
                        }
                        catch (Exception ex)
                        {
                            EgErrorHandller obj = new EgErrorHandller();
                            obj.InsertError(ex.Message.ToString());

                            SCode = "200F";
                        }
                    }
                    else if (val == 5)
                    {
                        try
                        {
                            string[] arrMsgs = PlainString.Split('|');
                            Mode = 4;//Convert.ToInt32(arrMsgs[0]); // PayMode
                            FromDate = Convert.ToDateTime(arrMsgs[0]);// FromDate no
                            ToDate = Convert.ToDateTime(arrMsgs[1]);// ToDate no
                            // SnaRefrence no
                            SCode = "200";
                            returndata = DataTableToJSONWithJSONNet(GetPay1File());
                            if (returndata == "{\"ResponseParam\":[]}")
                            {
                                SCode = "104";
                            }
                        }
                        catch (Exception ex)
                        {
                            EgErrorHandller obj = new EgErrorHandller();
                            obj.InsertError(ex.Message.ToString());

                            SCode = "200F";
                        }
                    }
                    else if (val == 6)
                    {
                        try
                        {
                            string[] arrMsgs = PlainString.Split('-');
                            //Mode = 4;//Convert.ToInt32(arrMsgs[0]); // PayMode
                            Month = Convert.ToInt32(arrMsgs[0]);// FromDate no
                            Year = Convert.ToInt32(arrMsgs[1]);// ToDate no
                            // SnaRefrence no
                            if (Month > 12)
                            {
                                return SCode = "105";
                            }
                            if (Year.ToString().Length > 4 || Year.ToString().Length < 4)
                            {
                                return SCode = "105";
                            }
                            SCode = "200";
                            returndata = DataTableToJSONWithJSONNet(GetDMSFile());
                            if (returndata == "{\"ResponseParam\":[]}")
                            {
                                SCode = "104";
                            }
                        }
                        catch (Exception ex)
                        {
                            EgErrorHandller obj = new EgErrorHandller();
                            obj.InsertError(ex.Message.ToString());

                            SCode = "200F";
                        }
                    }
                    else if (val == 7)
                    {
                        try
                        {
                            string[] arrMsgs = PlainString.Split('|');
                            string[] arrMsgs1 = arrMsgs[0].Split('-');
                            Mode = Convert.ToInt32(arrMsgs[1]); ;//Convert.ToInt32(arrMsgs[0]); // PayMode
                            Month = Convert.ToInt32(arrMsgs1[0]);// FromDate no
                            Year = Convert.ToInt32(arrMsgs1[1]);// ToDate no
                            if(Month > 12)
                            {
                                return SCode = "105";
                            }
                            if(Mode > 2)
                            {
                                return SCode = "104";
                            }
                            if (Year.ToString().Length > 4 || Year.ToString().Length < 4)
                            {
                                return SCode = "105";
                            }
                            // SnaRefrence no
                            SCode = "200";
                            returndata = DataTableToJSONWithJSONNet(GetAgFile());
                            if (returndata == "{\"ResponseParam\":[]}")
                            {
                                SCode = "104";
                            }
                        }
                        catch (Exception ex)
                        {
                            EgErrorHandller obj = new EgErrorHandller();
                            obj.InsertError(ex.Message.ToString());

                            SCode = "200F";
                        }
                    }


                }
                catch (Exception ex) { SCode = ex.Message; }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return SCode;
            //=======================================
        }
        //Return Json String
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            DailyAccountResponse objAccountResponse = new DailyAccountResponse();
            objAccountResponse.ResponseParam = table;
            JSONString = JsonConvert.SerializeObject(objAccountResponse);
            return JSONString;
        }
        private string Validation(string SCode)
        {
            string Status = string.Empty;
            string Message = string.Empty;
            string retMsg = string.Empty;

            switch (SCode)
            {
                case "001":
                    Status = "HF"; Message = "Header empty";
                    break;
                case "002":
                    Status = "PF"; Message = "Parameter empty";
                    break;
                case "003":
                    Status = "HFT"; Message = "Token in header missing";
                    break;
                case "004":
                    Status = "HFM"; Message = "Merchant key in header missing";
                    break;
                case "005":
                    Status = "HFTM"; Message = "No token match";
                    break;
                case "006":
                    Status = "HFTE"; Message = "Token expired";

                    break;

                case "200T":
                    Status = "HSTS"; Message = "Token verified";
                    break;

                case "200F":
                    Status = "SNAF"; Message = "Some technical issue";
                    break;
                case "200":
                    Status = "200"; Message = "Updated Successfully";
                    break;
                case "104":
                    Status = "104"; Message = "Data Not Found";
                    break;
                case "105":
                    Status = "105"; Message = "Date Format Is Not Valid";
                    break;
                default:
                    Status = "000"; Message = "No Status Code Matched";
                    break;
            }
            retMsg = SCode == "200" ? returndata :
               new JavaScriptSerializer().Serialize(new DailyAccountFileResponse()
                {
                   StatusCode = SCode,
                   Status = Status,
                   Message = Message
               });

            return retMsg;
        }

        public string VerifyToken()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.VarChar, 8) { Value = key };
            string result = gf.ExecuteScaler(PARM, "EgSNATokenVerify");
            return result;
        }
        public DataTable GetFile()
        {
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[1];
                DataTable dt = new DataTable();
                PM[0] = new SqlParameter("@AccountDate", SqlDbType.Date) { Value = FromDate };
                return gf.Filldatatablevalue(PM, "EgDailyAccountFile", dt, null);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable GetIMServerFile()
        {
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[2];
                DataTable dt = new DataTable();
                PM[0] = new SqlParameter("@fromdate ", SqlDbType.Date) { Value = FromDate };
                PM[1] = new SqlParameter("@todate ", SqlDbType.Date) { Value = ToDate };
                // return gf.ExecuteScaler (PM, "IM_Server_Daily_Data");
                return gf.Filldatatablevalue(PM, "IM_Server_Daily_Data", dt, null);
            }
            catch (Exception ex) {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                throw new Exception("Data Not Found");

            }
        }
        public DataTable GetPay1File()
        {
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[3];
                DataTable dt = new DataTable();
                PM[0] = new SqlParameter("@Mode ", SqlDbType.TinyInt) { Value = Mode };
                PM[1] = new SqlParameter("@fromdate ", SqlDbType.Date) { Value = FromDate };
                PM[2] = new SqlParameter("@todate ", SqlDbType.Date) { Value = ToDate };
                // return gf.ExecuteScaler (PM, "IM_Server_Daily_Data");
                return gf.Filldatatablevalue(PM, "DDSP", dt, null);
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                throw new Exception("Data Not Found");

            }
        }

        public DataTable GetDMSFile()
        {
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[2];
                DataTable dt = new DataTable();
                PM[0] = new SqlParameter("@FileMonth ", SqlDbType.Int) { Value = Month };
                PM[1] = new SqlParameter("@FileYear ", SqlDbType.VarChar,10) { Value = Year };
                // return gf.ExecuteScaler (PM, "IM_Server_Daily_Data");
                return gf.Filldatatablevalue(PM, "EgGetDMSFile", dt, null);
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                throw new Exception("Data Not Found");

            }
        }
        public DataTable GetAgFile()
        {
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[3];
                DataTable dt = new DataTable();
                PM[0] = new SqlParameter("@FileMonth ", SqlDbType.Int) { Value = Month };
                PM[1] = new SqlParameter("@FileYear ", SqlDbType.VarChar, 10) { Value = Year };
                PM[2] = new SqlParameter("@Type ", SqlDbType.VarChar, 10) { Value = Mode };
                // return gf.ExecuteScaler (PM, "IM_Server_Daily_Data");
                return gf.Filldatatablevalue(PM, "EgGetSignFile", dt, null);
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                throw new Exception("Data Not Found");

            }
        }
        //
        public string UpdateFTPDate()
        {
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[1];
                DataTable dt = new DataTable();
                PM[0] = new SqlParameter("@AccountDate", SqlDbType.Date) { Value = FromDate };
                return gf.ExecuteScaler(PM, "EgUpdateDailyFTPDate");
            }
            catch (Exception ex) {

                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());

                throw new Exception("Data Not Found");

            }
        }


        public void FileDownload(string URL)
        {
            //string FileDownloadPath = WebConfigurationManager.AppSettings["FileDownloadPath"];
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(
                        URL,
                        System.Web.Configuration.WebConfigurationManager.AppSettings["FileDownloadPath"] +"AccountSummary.txt"
                        );
                }
                catch (Exception ex)
                {
                    while (ex != null)
                    {
                        Console.WriteLine(ex.Message);
                        ex = ex.InnerException;
                    }
                }
            }
        }
    }

    public class DailyAccountRequest
    {
        public string RequestParam { get; set; }

    }
    public class DailyAccountResponse
    {
        public DataTable ResponseParam { get; set; }

    }
    
    public class DailyAccountFileResponse
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }


  
}
