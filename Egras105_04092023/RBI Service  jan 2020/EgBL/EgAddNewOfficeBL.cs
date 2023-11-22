using System;
using System.Data;
using System.Data.SqlClient;
using EgDAL;

namespace EgBL
{
    public class EgAddNewOfficeBL
    {
        GenralFunction gf;
       
        #region Properties
        public int OfficeId { get; set; }
        public int flag { get; set; }

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

        public int CheckOfficeId()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@officeid", SqlDbType.Int) { Value = OfficeId };
            SqlDataReader dr = gf.FillDataReader(PM, "EgCheckExistOfficeID");
            if (dr.HasRows)
            {
                dr.Read();
                result = int.Parse(dr[0].ToString());
               
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

        #endregion
    }
}
