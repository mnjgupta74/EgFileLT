using EgBL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;

public partial class WebPages_Reports_EgBankSrvcHlthChkUp : System.Web.UI.Page
{
    EgEchallanHistoryBL objEChallan = new EgEchallanHistoryBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            objEChallan.PopulateBankList(ddlbanks);
        }
    }

    [WebMethod]
    public static string GetErrorData(string FromDate, string ToDate, string BsrCode)
    {
        
        EgBankHealthchkupBL objEgBankHealthchkupBL = new EgBankHealthchkupBL();
        DataTable dt = new DataTable();
        objEgBankHealthchkupBL.FromDate = Convert.ToDateTime(FromDate);
        objEgBankHealthchkupBL.ToDate = Convert.ToDateTime(ToDate);
        objEgBankHealthchkupBL.BsrCode = BsrCode.Trim();
        dt = objEgBankHealthchkupBL.GetEgErrorData();
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(dt);
        return JSONString;

        //Fields objfield = new Fields();
        //string json = objfield.GstData(date);
        //return json;
    }

    [WebMethod]
    public static string GetBankData()
    {

        EgBankHealthchkupBL objEgBankHealthchkupBL = new EgBankHealthchkupBL();
        DataTable dt = new DataTable();
        dt = objEgBankHealthchkupBL.GetEgBankData();
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(dt);
        return JSONString;

        //Fields objfield = new Fields();
        //string json = objfield.GstData(date);
        //return json;
    }
    [WebMethod]
    public static string GetBankStatus(string BSRCode)
    {
        BankServiceCheck objBankServiceCheck = new BankServiceCheck();
        objBankServiceCheck.GRN = 123;
        objBankServiceCheck.TotalAmount = 100;
        objBankServiceCheck.BSRCode = BSRCode;
        objBankServiceCheck.PaymentMode = "N";
        return objBankServiceCheck.Verify().ToString();




    }


    protected void btncbi_Click(object sender, EventArgs e)
    {
        BankServiceCheck objBankServiceCheck = new BankServiceCheck();
        objBankServiceCheck.GRN = 123;
        objBankServiceCheck.TotalAmount = 100;
        objBankServiceCheck.BSRCode = "0280429";
        objBankServiceCheck.PaymentMode = "M";
        lblalert.Text= objBankServiceCheck.Verify().ToString();
    }
}