using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using EgDAL;
using DL;
using System.Data;

namespace EgBL
{
   public class EgLogout
    {
       GenralFunction gf = new GenralFunction();

    #region class Variables

       public int LoginID { get; set; }
       public DateTime Logoutdate { get; set; }

    #endregion


       #region Function

       public DataTable FillTable()
       {
           DataTable dt = new DataTable();
           gf = new GenralFunction();
           SqlParameter[] PM = new SqlParameter[2];
           PM[0] = new SqlParameter("@LoginId", SqlDbType.Int) { Value = LoginID};
           PM[1] = new SqlParameter("@LogOutDate", SqlDbType.DateTime) { Value = Logoutdate};
           return gf.Filldatatablevalue(PM, "EgLogoutUpdate", dt, null);

       }

       #endregion
    }
}
