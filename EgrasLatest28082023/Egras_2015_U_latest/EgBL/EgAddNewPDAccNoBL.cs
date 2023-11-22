using System;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
namespace EgBL
{
    public class EgAddNewPDAccNoBL : EgCommonPropertyBL
    {
        GenralFunction gf;
        public int flag { get; set; }
        public string  chkflag { get; set; }
        public int Output { get; set; }
        //public int PdAcc { get; set; }
        //public string Budgethead { get; set; }
        //public string PdAccName { get; set; }
        //public string Treasurycode { get; set; }
        //public string flag { get; set; }
        public string errMSG
        {
            get
            {
                switch (flag)
                {
                    case 0: return " Retry !";
                    case 1: return "PDAccno Not Mapped In Treasury";
                    case 2: return "PDAccno Already Deleted from treasury";
                    case 3: return "Successfully Saved !";
                    case 4: return "Successfully Updated !";
                    default: return "Data Not Saved </br>";
                };
            }

        }

        public void EgGetPDAccNoDetails()
        {

            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@PDAccNo", SqlDbType.Int) { Value = PDAccNo };
            PM[1] = new SqlParameter("@treasCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PM[2] = new SqlParameter("@Output", SqlDbType.Int) { Value = 0 };
            PM[2].Direction = ParameterDirection.Output;
            //SqlDataReader dr = gf.FillDataReader(PM, "EgGetPDAccNoDetails");
            SqlDataReader dr = gf.FillDataReader(PM, "TrgGetPDAccNoDetailsLinkServer_temp");
            if (dr.HasRows)
            {
                dr.Read();
                PDAccNo = Convert.ToInt32(dr[0].ToString());
                PDAccName = dr[1].ToString();
                TreasuryCode = dr[2].ToString();
                BudgetHead = dr[3].ToString().Trim();
                chkflag = dr[4].ToString();
                dr.Close();
                dr.Dispose();
                Output = Convert.ToInt32(PM[2].Value.ToString());
            }
            else
            {
                chkflag = "1";
                PDAccName = "NA";
                TreasuryCode = "NA";
                BudgetHead = "0000000000000";
                flag = 1;
            }
            dr.Close();
            dr.Dispose();
        }

        public void InsertOfficeDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@PDAccNo", SqlDbType.Int) { Value = PDAccNo };
            PM[1] = new SqlParameter("@TreasCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            flag = Convert.ToInt16(gf.ExecuteScaler(PM, "EgInsertPDAccNoDetail"));
        }

    }
}
