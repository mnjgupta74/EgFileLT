using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace EgBL
{
   public class EgDigitalSignPdf
    {
        GenralFunction gf;

        public string Duration { get; set; }
        public Byte[] SignData { get; set; } 
        public string PageName { get; set; }
        public void InsertSignData()
        {
            gf = new GenralFunction();

            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@PageName", SqlDbType.VarChar, 50) { Value = PageName };
            PM[1] = new SqlParameter("@Duartion", SqlDbType.VarChar, 30) { Value = Duration };
            PM[2] = new SqlParameter("@SignData", SqlDbType.VarBinary) { Value = SignData };

            gf.UpdateData(PM, "EgInsertDigitalSignData");
        }

    }
}
