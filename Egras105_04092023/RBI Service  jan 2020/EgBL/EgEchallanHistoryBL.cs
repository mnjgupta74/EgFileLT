using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgEchallanHistoryBL
    {
        GenralFunction gf ;
        #region Properties
        // public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int type { get; set; }
        public string BankCode { get; set; }
        public int StartIdx { get; set; }
        public int EndIdx { get; set; }
        public int totalcount { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public double TotalAmount { get; set; }
        public long UserId { get; set; }
        public int UserType { get; set; }
        #endregion
        #region Function

        /// <summary>
        /// Show SuccessFul Challan List
        /// </summary>
        /// <param name="grd"></param>
      //public void FillSuccessfulChallanGrid(GridView grd)
      //{
      //    gf = new GenralFunction();
      //    SqlParameter[] PARM = new SqlParameter[9];
      //    PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
      //    PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
      //    PARM[2] = new SqlParameter("@BankCode", SqlDbType.Char, 7) { Value = BankCode };
      //    PARM[3] = new SqlParameter("@startIdx", SqlDbType.Int) { Value = StartIdx };
      //    PARM[4] = new SqlParameter("@endIdx", SqlDbType.Int) { Value = EndIdx };
      //    PARM[5] = new SqlParameter("@status", SqlDbType.Char, 1) { Value = Status };
      //    PARM[6] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType.Trim() };//Added by Sandeep for Manual
      //    PARM[7] = new SqlParameter("@RowCount", SqlDbType.Int) { Value = 0 };
      //    PARM[7].Direction = ParameterDirection.Output;
      //    PARM[8] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = 0 };
      //    PARM[8].Direction = ParameterDirection.Output;
      //    gf.FillGridViewControl(grd, PARM, "EgSuccessfulChallan");
      //    totalcount = Int32.Parse(PARM[7].Value.ToString());
      //    if (PARM[8].Value.ToString() != "")
      //    { TotalAmount = Convert.ToDouble(PARM[8].Value.ToString()); }

      //}
        /// <summary>
        /// Binding of DropDown ddlbank
        /// </summary>
        /// <param name="grd"></param>
        public void PopulateBankList(DropDownList ddlbank) // fill department Droddownlist
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            gf.FillListControl(ddlbank, "EgGetBanks_Reports", "BankName", "BSRCode", null);
            ddlbank.Items.Insert(0, new ListItem("--Select Bank--", "0"));
        }

        public string GetSuccessfulChallanData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@BankCode", SqlDbType.Char, 7) { Value = BankCode };
            PARM[3] = new SqlParameter("@status", SqlDbType.Char, 1) { Value = Status };
            PARM[4] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType.Trim() };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgSuccessfulChallan", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        public string GetBanks()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Userid", SqlDbType.BigInt) { Value = UserId };
            PARM[1] = new SqlParameter("@Usertype", SqlDbType.Int) { Value = UserType };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetBanks_Reports", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        #endregion
    }
}
