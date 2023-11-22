using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgManualFailBL
    {
        GenralFunction gf;

        public DateTime FromDate { get; set; }
        public DateTime ToDate   { get; set; }

        public Int64 GRN { get; set; }
        public double Amount { get; set; }

        public int UserId { get; set; }

        public string BankCode { get; set; }

        public void PopulateBankList(DropDownList ddlbank) // fill department Droddownlist
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            gf.FillListControl(ddlbank, "EgGetBanks", "BankName", "BSRCode", null);
            ddlbank.Items.Insert(0, new ListItem("--Select Bank--", "0"));
        }
        public DataTable manualfail()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@BankCode", SqlDbType.VarChar) { Value = BankCode };
            return dt= gf.Filldatatablevalue(PARM, "EgManualFail",dt,null);
        } 
        
        public DataTable manualfailgrn()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@Grn",SqlDbType.BigInt) {Value=GRN };
            return dt= gf.Filldatatablevalue(par,"EgManualFailGrn",dt,null);
        }
        public int updatedatapending()
        {
            gf = new GenralFunction();
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value=GRN};
            para[1] = new SqlParameter("@Amount",SqlDbType.Money) {Value=Amount };
            para[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            para[3] = new SqlParameter("@BankCode", SqlDbType.VarChar) { Value = BankCode };
            return gf.UpdateData(para, "EgManualUpdate");
           
       }
    }
}
