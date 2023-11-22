using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgBankAccessBL
    {
        GenralFunction gf = new GenralFunction();
        public string BSRCode { get; set; }
        public string Access { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSC { get; set; }
        public string MICRCode { get; set; }
        public string Address { get; set; }
        public string BankType { get; set; }
        public string TreasuryCode { get; set; }
        public int BankBranchCode { get; set; }
        public string ChequePrint { get; set; }
        public Int64 acno { get; set; }
        public string RBICode { get; set; }
        #region Function
        public DataTable FillBankGrid()
        {
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(null, "EgGetAllBank", dt, null);
           
        }
        public int UpdateBankAccess()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BSRCode };
            PARM[1] = new SqlParameter("@Access", SqlDbType.Char, 1) { Value = Access };
            int result = gf.UpdateData(PARM, "EgBankUpdateAccess");
            return result;
        }

        public string GetBankListToUpdate()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[0];
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgBankListToUpdate", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        public string InsertBankData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[13];
            PARM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BSRCode };
            PARM[1] = new SqlParameter("@Access", SqlDbType.Char, 1) { Value = Access };
            PARM[2] = new SqlParameter("@treasuryCode", SqlDbType.VarChar, 10) { Value = TreasuryCode };
            PARM[3] = new SqlParameter("@BankBranchCode", SqlDbType.Int) { Value = BankBranchCode };
            PARM[4] = new SqlParameter("@BankName", SqlDbType.VarChar, 100) { Value = BankName };
            PARM[5] = new SqlParameter("@BranchName", SqlDbType.VarChar, 50) { Value = BranchName };
            PARM[6] = new SqlParameter("@IFSC", SqlDbType.Char, 12) { Value = IFSC };
            PARM[7] = new SqlParameter("@MICRCode", SqlDbType.Char, 12) { Value = MICRCode };
            PARM[8] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = Address };
            PARM[9] = new SqlParameter("@BankType", SqlDbType.Char, 1) { Value = BankType };
            PARM[10] = new SqlParameter("@ChequePrint", SqlDbType.Char, 1) { Value = ChequePrint };
            PARM[11] = new SqlParameter("@acno", SqlDbType.BigInt) { Value = acno };
            PARM[12] = new SqlParameter("@RBICode", SqlDbType.Char, 8) { Value = RBICode };

            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgInsertBankFromTreasury", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        #endregion
    }
}
