using System;
using System.Data.SqlClient;
using System.Data;

namespace EgBL
{
// Created by Rachit Sharma
    public class EgZoneCtdBL
    {
        GenralFunction gf;
        #region Properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CtdMajorHead { get; set; }
        public string Zone { get; set; }

        #endregion
        #region Function

        public DataTable FillGrdCtdHeadWiseAmount()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            DataTable dt = new DataTable();
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@CtdMajorHead", SqlDbType.Char, 4) { Value = CtdMajorHead };
            PM[3] = new SqlParameter("@Zone", SqlDbType.Char, 4) { Value = Zone };
            gf.Filldatatablevalue(PM, "EgZoneWiseHeadAmount", dt, null);
            return dt;
        }

        #endregion
    }
}
