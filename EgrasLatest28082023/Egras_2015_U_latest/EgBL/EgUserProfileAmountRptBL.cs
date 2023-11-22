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
    public class EgUserProfileAmountRptBL
    {
        GenralFunction gf = new GenralFunction();
        #region properties
        #endregion
   
        public string ProfileName { get; set; }

        #region Function
        public DataTable FillProfileGrid()
        {
            DataTable dt = new DataTable();

            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@type", SqlDbType.Int) { Value = 1 };
            PARM[1] = new SqlParameter("@ProfileName", SqlDbType.NVarChar,8) { Value = ProfileName };
            return gf.Filldatatablevalue(PARM, "EgUserProfileAmount", dt, null);
         
        }
        #endregion
    }
}
