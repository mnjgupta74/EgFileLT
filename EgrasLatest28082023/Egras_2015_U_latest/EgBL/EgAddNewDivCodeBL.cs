using System;
using System.Data;
using System.Data.SqlClient;
using EgDAL;

namespace EgBL
{
    public class EgAddNewDivCodeBL 
    {
        GenralFunction gf;
        public int flag { get; set; }
        public string chkflag { get; set; }
        public int DivCode { get; set; }
        public String DivName { get; set; }
        public String treasuryCode { get; set; }
        public int OfficeCode { get; set; }
        public int Output { get; set; }
        public string errMSG
        {
            get
            {
                switch (flag)
                {
                    case 0: return " Retry !";
                    case 1: return "Division Code Not Mapped In Treasury";
                    case 2: return "Successfully Saved !";
                    case 3: return "Successfully Updated !";
                    default: return "Data Not Saved </br>";
                };
            }

        }

        public void EgGetDivCodeDetails()
        {

            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@divCode", SqlDbType.Int) { Value = DivCode };
            PM[1] = new SqlParameter("@treasCode", SqlDbType.Char, 4) { Value = treasuryCode };
            PM[2] = new SqlParameter("@officeCode", SqlDbType.Int) { Value = OfficeCode };
            PM[3] = new SqlParameter("@Output", SqlDbType.Int) { Value = 0 };
            PM[3].Direction = ParameterDirection.Output;
            //SqlDataReader dr = gf.FillDataReader(PM, "EgGetDivCodeDetails");
            SqlDataReader dr = gf.FillDataReader(PM, "EgGetDivCodeDetailLinkServer");
            if (dr.HasRows)
            {
                dr.Read();
                DivCode = Convert.ToInt16(dr[0].ToString());
                DivName = dr[1].ToString();
                treasuryCode = dr[2].ToString();
                OfficeCode = Convert.ToInt32(dr[3].ToString().Trim());
                dr.Close();
                dr.Dispose();
                Output = Convert.ToInt32(PM[3].Value.ToString());
            }
            else
            {
                flag = 1;
                DivName = "NA";
                treasuryCode = "NA";
            }
            dr.Close();
            dr.Dispose();
        }

        public void InsertDivisionDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@DivCode", SqlDbType.Int) { Value = DivCode };
            PM[1] = new SqlParameter("@TreasCode", SqlDbType.Char, 4) { Value = treasuryCode };
            PM[2] = new SqlParameter("@OfficeCode", SqlDbType.Int) { Value = OfficeCode};
            flag = Convert.ToInt16(gf.ExecuteScaler(PM, "EgInsertDivisionDetail"));
        }
    }
}
