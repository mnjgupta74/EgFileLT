using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DL;
using EgDAL;

namespace EgBL
{
    public class EgBankUploadFilesBL
    {
        GenralFunction gf;

        #region Class variables
        public int UserID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        #endregion    


        #region Function

        public void Fillrpt(Repeater rpt)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID };
            gf.FillRepeaterControl(rpt, PARM, "EgBankUploadFiles");
           
        }

        public DataTable LabelRecord()
        {
            
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID };
            return gf.Filldatatablevalue(PARM, "EgBankUploadFilesCount", dt, null);
        }

        #endregion

        
    }
}
