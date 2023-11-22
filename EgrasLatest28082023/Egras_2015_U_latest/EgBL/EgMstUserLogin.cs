using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using EgDAL;

namespace EgBL
{
  public  class EgMstUserLogin
    {       
        GenralFunction gf = new GenralFunction();

        #region Class Variables
        public int userId { get; set; }
        public int userType { get; set; }
        #endregion     

        #region Function
        public DataSet GetMenuByUserId()
        {
            DataSet ds = new DataSet();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };
            PARM[1] = new SqlParameter("@UserType", SqlDbType.TinyInt) { Value = userType };

            ds = gf.Filldatasetvalue(PARM, "Menu_GetMenuByUserId", ds, null);
            return ds;
        }
        public DataTable FetchBankUrls()
        {
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgFetchBankUrl", dt, null);
            return dt;
        }
        #endregion
    }
}
