using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgBL
{
  public  class EgPDAccountDetail
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string GetPdAccountDetail()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate};
            PM[1] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };
            dt = gf.Filldatatablevalue(PM, "EgPdAccoutDetailTreasuryWise", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

    }
}
