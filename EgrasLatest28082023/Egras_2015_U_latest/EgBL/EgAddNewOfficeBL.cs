using System;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using System.Web.UI.WebControls;
namespace EgBL
{
    public class EgAddNewOfficeBL
    {
        GenralFunction gf;
       
        #region Properties
        public int OfficeId { get; set; }
        public int flag { get; set; }
        public string TreasuryCode { get; set; }
        public string errMSG
        {
            get
            {
                switch (flag)
                {
                    case 0: return " Retry !";
                    case 1: return "Successfully Saved !";
                    case 2: return "Successfully Updated !";
                    case 3: return "OfficeID Already Deleted from treasury";
                    case 4: return "OfficeID Not Mapped In Treasury";
                    
                    default: return "Data Not Saved </br>";
                };
            }

        }
        #endregion
       
        #region Method
        /// <summary>
        /// Check Office ID Exist and not in database 
        /// </summary>
        /// <returns> 1,2,0</returns>

        public void InsertOfficeDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@officeid", SqlDbType.Int) { Value = OfficeId };
            flag =Convert.ToInt16( gf.ExecuteScaler(PM, "EgInsertOfficeDetail"));
        }

        public string CheckOfficeId()
        {
            string result = "0";
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@officeid", SqlDbType.Int) { Value = OfficeId };
            SqlDataReader dr = gf.FillDataReader(PM, "EgCheckExistOfficeID");
            if (dr.HasRows)
            {
                dr.Read();
                result = dr[0].ToString();
               
            }
            dr.Close();
            dr.Dispose();
            return result;

        }

        public string getOfficeIdDetails_L_Server(int officeid)
        {
            gf = new GenralFunction();
            string OffDetail = "";
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@officeid", SqlDbType.Int) { Value = officeid };
            OffDetail = gf.ExecuteScaler(PM, "TrgGetOfficeDetailsLinkServer").ToString();
            if(OffDetail.Trim()=="0")
            {
                flag = 4;
            }
            return OffDetail;
        }
        /// <summary>
        /// add new Office which is  not exist in Rajkosh
        /// 6 May 2022
        /// </summary>
        /// <returns></returns>
    public int InsertTreasuryDetail()
    {
        gf = new GenralFunction();
        SqlParameter[] PM = new SqlParameter[3];
        PM[0] = new SqlParameter("@officeid", SqlDbType.Int) { Value = OfficeId };
        PM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
        PM[2] = new SqlParameter("@Mode", SqlDbType.Int) { Value = flag };
            int a = Convert.ToInt16(gf.ExecuteScaler(PM, "EgInsertOfficewithTreasuryCode"));
        return a;
    }
    public DataTable FillTreasury()
    {
        GenralFunction gf = new GenralFunction();
        DataTable dt = new DataTable();
        SqlParameter[] PM = new SqlParameter[0];
        dt = gf.Filldatatablevalue(PM, "EgFillTreasury", dt, null);
        return dt;
    }
    #endregion
}
}
