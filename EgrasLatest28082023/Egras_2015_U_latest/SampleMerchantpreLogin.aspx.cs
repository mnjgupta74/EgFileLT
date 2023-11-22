﻿using System;
using System.Data;
using System.Web;
using System.Web.UI;
using EgBL;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public partial class SampleMerchantpreLogin : System.Web.UI.Page
{
    DataTable schemaAmtTable;
    public int Mcode;
    public string EncData;
    public string AUIN,ChallanType;
    protected void Page_Load(object sender, EventArgs e)
    {
        string Site1address = "";
        if (!Page.IsPostBack)
        {
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            //if (objEgCheckBudgetHead.isDeptIntegrationEnabled())
            //{
            //    imgBackSoon.Visible = false;
            //    txtGo.Visible = true;
            //}
            //else
            //{
            //    imgBackSoon.Visible = true;
            //    txtGo.Visible = false;
            //    return;
            //}
            #region check Address
            try
            {
                Site1address = Request.UrlReferrer.AbsoluteUri.Split('?').GetValue(0).ToString();
            }
            catch (Exception ex)
            {
                if (ex is HttpException && ex.InnerException is ViewStateException)
                {
                    Site1address = "Address has Error :-" + ex.ToString();
                }
            }
            #endregion

            #region check Encdata
            try
            {
                EncData = Request.Form["Encdata"].ToString();
                hdnEncData.Value = EncData;
                //EncData = "yhwNPTZcXmDU0yn80JETRTQ01GGGGy1YmD/h4FszyKMIrMsvyP2jG7iYh7TO6onzYPXkIZOYO80fjyKDv3CsQvtTV+JLANbhhVsiE2gZDd0tmJi1p7NlMNtRCvd24RNmH2mPAB3lbcbv/16puL+kqnB3ND1kL5sEtTubfXtR/1sNFPZYff9LQB65vA2/ttYSZYxC+oX6rIYfJV7Xkbesl7ivUSFxLli61PsUvsqcvDYwYH4YQgBwRWsGmxEJr0ABnkbv0tLmGgxF7esbYW8CqXStDQ+0I17YgYKFgNJfL1LwMpPkGS7kYjT59DTrFgW9AKrFY6BEl/uPt+ZLPuly+34JTkFb60ZLoyeRsZv088o1Gnvkc/yBGMSyxoTOjVqJxwpc7k5G14bY4u/7mWtadcJTQVWFOK0nlPugzQj608kLU6uX29+izJtahbnkabGDmeOm5w+c2Zf/rvw2xJOxXRgogx2XPHLriJK2e4STlfJKt6+urAwi47b7SbffaLndzvXlA7NWLuZq0m0hi4XLleLy3pyM0+EWl6to9zE+gKhztoGMhWRDSoyXNM1abH4xT71FAlC5ERw8CyAaiKWxlAyT0ajc7nwNRE9gEyjhbXdx4bpGsY4HwkTMUZDSfuOy2VOqKXkpqd6Ix6NpE6L7zaYmYrxn6IestgABPJRhRqUxCRiW+VQtQ8yj7B/y4VM2O9KktWjPAFWu/FFLuWBi7Jnq2qOOP52c2t4cuKXiL14VS/kHVZIsss+ohMRFAjIK/6SkKkqFcCd46vJUi3BQAU78v6nA0nXWDp6fp0co4ynnTGbwc7FHnLWjefw8dL36wErrW6jsO6rMQbURhuJSlg==";

            }
            catch (NullReferenceException ex)
            {
                EncData = "EncData Blank or NULL";
            }
            #endregion
            #region check MerchantCode
            try
            {
                Mcode = Convert.ToInt32(Request.Form["Merchant_code"]);
                hdnMCode.Value = Mcode.ToString();
                //Mcode = 5006;
            }
            catch (NullReferenceException ex)
            {
                Mcode = 0;
                hdnMCode.Value = Mcode.ToString();
            }
            catch (FormatException ef)
            {
                Mcode = 0;
                hdnMCode.Value = Mcode.ToString();
            }
            #endregion
            #region check AUIN

            try
            {
                AUIN = Request.Form["AUIN"].ToString().Trim();
                hdnAUIN.Value = AUIN;
                //AUIN = "106764";
            }
            catch (NullReferenceException ex)
            {
                AUIN = null;
            }
            #endregion
            #region check ChallanType
            try
            {
                if (Request.Form.Count > 3)
                {
                    ChallanType = Request.Form["ChallanType"].ToString().Trim();
                    hdnCType.Value = ChallanType;
                }
            }
            catch (NullReferenceException ex)
            {
                ChallanType = null;
            }
            #endregion

            //EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            objEgCheckBudgetHead.Mcode = Mcode;
            objEgCheckBudgetHead.Address = Site1address.ToString().Trim();
            objEgCheckBudgetHead.AUIN = AUIN;
            objEgCheckBudgetHead.EncData = EncData;
            objEgCheckBudgetHead.ChallanType = ChallanType;
            if (objEgCheckBudgetHead.GetMerchantinfo() == 1)
                //if (objEgCheckBudgetHead.GetMerchantinfo() != 1)
            {
                //  Due to lost property
                //ViewState["Mcode"] = Mcode;
                //ViewState["EncData"] = EncData;
                //ViewState["AUIN"] = AUIN;
                field1.Visible = true;
                SetSessionValues();
            }
            else
            {
                Response.Redirect("IntegrationErrorPage.htm");
            }

        }
    }
    public void SetSessionValues()
    {
        Session["UserId"] = "73";
        Session["UserType"] = "9";
        Session["userName"] = "Guest";
        HttpContext.Current.Session["MenuDataSet"] = "";
        Random randomclass = new Random();
        HttpContext.Current.Session.Add("Rnd1", randomclass.Next().ToString());
        HttpContext.Current.Session["RND"] = HttpContext.Current.Session["RND1"].ToString();
        HttpCookie appcookie = new HttpCookie("Appcookie");
        appcookie.HttpOnly = true;
        appcookie.Domain = HttpContext.Current.Request.Url.Host;
        appcookie.Value = HttpContext.Current.Session["RND"].ToString();
        appcookie.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(appcookie);

    }
    [System.Web.Services.WebMethod]
    public static string GetBanks(string EncData, string AUIN, string Mcode, string ChallanType)
    {
        try
        {
            DeptIntegrationController objDeptIntegrationController = new DeptIntegrationController();
            bool result = objDeptIntegrationController.CheckData(new IntegrationClass(EncData.Trim(), AUIN.Trim(), Convert.ToInt32(Mcode), ChallanType.Trim()));
            byte[] bytesDataObject;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, objDeptIntegrationController);
                bytesDataObject = stream.ToArray();
            }
            HttpContext.Current.Session["EgEchallanData"] = bytesDataObject;
            bytesDataObject = null;
            formatter = null;
            if (result)
            {
                DataTable dt = new DataTable();
                dt = GetBanks(objDeptIntegrationController.Data["Location"].Value.ToString(), objDeptIntegrationController.Data["Paymenttype"].Value.ToString(), objDeptIntegrationController.ManualBanksType);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                dt.Dispose();
                return JSONString + "|" + objDeptIntegrationController.Data["Paymenttype"].Value.ToString().Trim();
            }
            return "0";
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "=" + Mcode);
            DisposeSessionValue();
            return "-1";
        }
    }
    private static void DisposeSessionValue()
    {
        HttpContext.Current.Session["UserId"] = "";
        HttpContext.Current.Session["UserType"] = "";
        HttpContext.Current.Session["userName"] = "";
    }
    private static DataTable GetBanks(string Location, string PaymentType, int type = -1)
    {
        DataTable dt = new DataTable();
        if (PaymentType == "N")
        {
            EgCheckBudgetHead objEgFillBank = new EgCheckBudgetHead();
            dt = objEgFillBank.FillBank1();
            //dt = dt.AsEnumerable().Where(t => t.Field<string>("access").Trim() != "R").CopyToDataTable();

        }
        else if (PaymentType == "M")
        {
            EgCheckBudgetHead objEgFillBank = new EgCheckBudgetHead();
            objEgFillBank.type = type;
            objEgFillBank.TreasuryCode = Location;
            dt = objEgFillBank.FillManualBanks();
        }
        return dt;
    }
    [System.Web.Services.WebMethod]
    public static string SubmitChallan(string BSRCode)
    {
        if (BSRCode != "0" && BSRCode != null && BSRCode != string.Empty)
        {

            byte[] bytesDataObj = (byte[])HttpContext.Current.Session["EgEchallanData"];
            IFormatter formatter = new BinaryFormatter();
            DeptIntegrationController obj;
            using (MemoryStream stream = new MemoryStream(bytesDataObj))
            {
                obj = (DeptIntegrationController)formatter.Deserialize(stream);
            }
            obj.Data.Add("BankName", new IntegrationProp() { Value = BSRCode, ParameterName = "@BankName", DbType = SqlDbType.Char, Size = 7 });
            Int64 GRN = obj.InsertChallan(new IntegrationClass());
            bool result = GRN > 0 ? true : false;
            string ReturnUrl = "";
            if (result)
            {
                HttpContext.Current.Session["GrnNumber"] = GRN;
                if (obj.Data["Paymenttype"].Value.ToString() == "N")
                {
                    ReturnUrl = "webpages/EgEChallanView.aspx";
                }
                else if (obj.Data["Paymenttype"].Value.ToString() == "M")
                {
                    ReturnUrl = "webpages/reports/EgManualChallan.aspx";
                }
            }
            HttpContext.Current.Session["EgEchallanData"] = null;
            return result ? "Record Saved Successfully." + "|" + ReturnUrl : "Record Not Saved." + "|" + ReturnUrl;
        }
        else
        {
            return "Please Select Bank !" + "|";
        }
    }
}
