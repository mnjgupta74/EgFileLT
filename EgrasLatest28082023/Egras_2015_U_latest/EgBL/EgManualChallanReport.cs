using System;
using System.Data.SqlClient;
using System.Data;
namespace EgBL
{
   public class EgManualChallanReport:EgCommonPropertyBL
   {
       //#region Variables
       //private string _Tcode;
       //private DateTime _Fdate, _Tdate;
       //#endregion
       //#region properties
       //    public string Tcode
       //    {
       //        get { return _Tcode; }
       //        set { _Tcode = value; }
       //    }
       //    public DateTime Fdate
       //    {
       //        get { return _Fdate; }
       //        set { _Fdate = value; }
       //    }
       //    public DateTime Tdate
       //    {
       //        get { return _Tdate; }
       //        set { _Tdate = value; }
       //    }
       //#endregion
       public void FillManualChallanReport(DataTable dt)
       {
           SqlParameter[] pm = new SqlParameter[3];
           GenralFunction gf = new GenralFunction();
           pm[0] = new SqlParameter("@TCODE", SqlDbType.Char, 4) { Value = TreasuryCode };
           pm[1] = new SqlParameter("@FROMDATE", SqlDbType.DateTime) { Value = FromDate };
           pm[2] = new SqlParameter("@TODATE", SqlDbType.DateTime) { Value = ToDate };
           gf.Filldatatablevalue(pm, "EgManualChallanReport", dt, null);
       }
   }
}
