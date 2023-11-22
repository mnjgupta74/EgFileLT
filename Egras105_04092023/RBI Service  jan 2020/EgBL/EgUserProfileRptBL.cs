using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;


namespace EgBL
{
    public class EgUserProfileRptBL
    {
        GenralFunction gf = new GenralFunction();
        #region properties
  
        public int UserId { get; set; }
        public int UserPro { get; set; }
       
        #endregion
        #region Function
        /// <summary>
        /// show all profile ,user Wise 
        /// </summary>
        /// <param name="grd"></param>
        public DataTable FillProgileGrid()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
        
            return gf.Filldatatablevalue(PARM, "EgUserProfile", dt, null);

        }
        /// <summary>
        /// Profile Schema
        /// </summary>
        /// <returns> DataTable </returns>
        public DataTable fillProfileSchema()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            PARM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            dt = gf.Filldatatablevalue(PARM, "EgGetUserProfileSchema", dt, null);
            return dt;
        }
        public DataTable GetTransactionList()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            PARM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            return  gf.Filldatatablevalue(PARM, "EgGetUserTransactionList", dt,null);
          
        }
        #endregion
    }
}
