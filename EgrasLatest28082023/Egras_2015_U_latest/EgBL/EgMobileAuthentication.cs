using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgMobileAuthentication
    {
        GenralFunction gf;
        public string mobileno { get; set; }
        public string OTP { get; set; }
        public string usreid { get; set; }
        public string path { get; set; }
        public string MobileNumberVerify()
        {
            gf = new GenralFunction();
            string sr = string.Empty;
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@mobileno", SqlDbType.VarChar, 10) { Value = mobileno };
            sr = gf.ExecuteScaler(PARM, "EgMobileNoAuthentication");
            return sr;
        }



        
    }
}
