using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgEditTreasuryBL
    {

        public Int64 GRN { get; set; }
        public string TreasuryCode { get; set; }
        public string StatusValue { get; set; }
        public int UserId { get; set; }
        public int DeptCode { get; set; }
        public Int32 OfficeId { get; set; }
        public string DistrictId { get; set; }
        public Int32 GrnUserId { get; set; }
        public string GetGRNDetail()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            dt = gf.Filldatatablevalue(PM, "EgGRNEditDetails", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        /// <summary>
        /// Get grn Detail for Update Status
        /// </summary>
        /// <returns></returns>


        public string GetGRNDetailForChangeStatus()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PM[1] = new SqlParameter("@Status", SqlDbType.Char, 1) { Value = StatusValue };
            dt = gf.Filldatatablevalue(PM, "EgGRNStatusUpdate", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }


        public string GetGRNAmtDetail()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[1] = new SqlParameter("@GRN", SqlDbType.Int) { Value = GRN };
            dt = gf.Filldatatablevalue(PM, "EgGRNSchemas", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        public string GetTreasury()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[0];
            dt = gf.Filldatatablevalue(PM, "EgFillTreasury", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        public int UpdateTreasury()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            int Rv = gf.UpdateData(PARM, "EgGRNUpdateTreasury");
            return Rv;
        }


      
        /////
        ///UpdateStatus  26 march 2019
        //// 
        public int UpdateStatus()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PARM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[2] = new SqlParameter("@Status", SqlDbType.Char, 1) { Value = StatusValue };
            int Rv = gf.UpdateData(PARM, "EgGRNUpdateStatus");
            return Rv;
        }


        // Office Transfer 18 dec 2019 

        public string GetGRNFromOfficeTransfer()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            dt = gf.Filldatatablevalue(PM, "EgGetOfficeForTransfer", dt, null);
            if (dt.Rows.Count > 0)
            {
                GrnUserId = Convert.ToInt32(dt.Rows[0]["OfficeName"].ToString());
                DistrictId = dt.Rows[0]["DistrictCode"].ToString();
                DeptCode = Convert.ToInt32(dt.Rows[0]["DeptCode"].ToString());

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;
            }
            else
            {
                return "";
            }
        }

        public string GetOfficeList()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 50) { Value = DistrictId };
            dt = gf.Filldatatablevalue(PARM, "EgFillOfficeList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        public string FillLocation()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[0];
            dt = gf.Filldatatablevalue(PARM, "EgFillLocation", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        public int UpdateOffice()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PARM[1] = new SqlParameter("@OfficeName", SqlDbType.Int) { Value = OfficeId };
            PARM[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[3] = new SqlParameter("@TreasuryCode", SqlDbType.Char,4) { Value = TreasuryCode };
            int Rv = gf.UpdateData(PARM, "EgUpdateOfficeForTransfer");
            return Rv;
        }

    }
}
