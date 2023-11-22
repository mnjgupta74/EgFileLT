using System;
using System.Data;
using System.Web.UI;
using EgBL;
using System.Linq;
using System.Web.Services;
using System.Collections.Generic;

public partial class WebPages_Reports_EgBankMonthTranscation : System.Web.UI.Page
{
    public int SNo = 0;
    //EgBankMonthTransactionsBL objEGBANKBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        //if (!IsPostBack)
        //{
        //    ddlYear.FinyearSelectType = 3;
        //    ddlYear.count = 3;
        //    ddlYear.FinYearDropdown();
        //}

    }


    [WebMethod]
    public static GridData[] CallJSONMethod(string Code, string Year, string type)
    {
        EgBankMonthTransactionsBL objEGBANKBL = new EgBankMonthTransactionsBL();
        DataSet ds = new DataSet("Dset");
        string monthname = "";
        DataTable dt = new DataTable("Dtable");
        if (Code == "undefined")
        {
            objEGBANKBL.Month = "0";
        }
        else
        {
            objEGBANKBL.Month = Code;
        }
        objEGBANKBL.Year = Year;
        objEGBANKBL.Type = type;

        dt = objEGBANKBL.GetBankMonthTransaction();
        List<GridData> GridDetail = new List<GridData>();
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["name"].ToString() == dt.Rows[dt.Rows.Count - 1]["name"].ToString())
            {
                monthname = dt.Rows[0]["name"].ToString();
            }
            else
            {
                monthname = dt.Rows[0]["name"].ToString() + " To " + dt.Rows[dt.Rows.Count - 1]["name"].ToString();
            }

            var queryResult = (from item in dt.AsEnumerable()
                               group item by new { bankname = item["bankname"], bankshortname = item["BankShortName"] } into grp
                               select new
                               {
                                   BankName = grp.Key.bankname,
                                   BankShortName = grp.Key.bankshortname,
                                   TotalAmount = grp.Sum(i => i.Field<Decimal>("Amount")),
                                   TotalChallan = grp.Sum(i => i.Field<Int32>("totChallan"))
                               }).ToArray();

            foreach (var dt_row in queryResult)
            {
                GridData grd = new GridData();
                grd.BankName = dt_row.BankName.ToString();
                grd.BankShortName = dt_row.BankShortName.ToString();
                grd.Amount = Convert.ToDecimal(dt_row.TotalAmount);
                grd.ToChallan = Convert.ToInt32(dt_row.TotalChallan);
                grd.MonthName = monthname.ToString();
                GridDetail.Add(grd);
            }
        }
        return GridDetail.ToArray();
    }


    public class GridData
    {
        public string BankName { get; set; }
        public string BankShortName { get; set; }
        public decimal Amount { get; set; }
        public int ToChallan { get; set; }
        public string MonthName { get; set; }
    }



}
