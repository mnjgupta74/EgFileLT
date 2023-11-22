using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace EgBL
{
    public class EgReleaseAmountBL
    {
        public bool isVerify = false;
        private long _Grn;
        public long Grn
        {
            get
            {
                return _Grn;
            }
            set
            {
                //if (value > 0 || (isVerify && value == 0))
                if (value > 0 || isVerify)
                {
                    _Grn = value;
                }
                else
                {
                    msg = "Grn can not be blank !";
                    throw new Exception("Grn can not be blank !");
                }
            }
        }
        //private string _type { get; set; }
        //public string type
        //{
        //    get { return _type; }
        //    set
        //    {
        //        if (!string.IsNullOrEmpty(value) && (value.Trim() == "D" || value.Trim() == "R"))
        //        {
        //            _type = value;
        //        }
        //        else
        //        {
        //            msg = "Deface Or Refund type can be only D Or R Type !";
        //            throw new Exception("Deface Or Refund type can be only D Or R Type !");
        //        }
        //    }
        //}
        private string _login { get; set; }
        public string login
        {
            get { return _login; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _login = value;
                }
                else
                {
                    msg = "Login can not be blank !";
                    throw new Exception("Login can not be blank !");
                }
            }
        }
        private Int64 _ReferenceNo { get; set; }
        public Int64 ReferenceNo
        {
            get { return _ReferenceNo; }
            set
            {
                if (value > -1)
                {
                    _ReferenceNo = value;
                }
                else
                {
                    msg = "ReferenceNo can not be blank !";
                    throw new Exception("ReferenceNo can not be blank !");
                }
            }
        }
        private Int32 _MerchantCode { get; set; }
        public Int32 MerchantCode
        {
            get { return _MerchantCode; }
            set
            {
                if (value > 0)
                {
                    _MerchantCode = value;
                }
                else
                {
                    msg = "MerchantCode can not be blank !";
                    throw new Exception("MerchantCode can not be blank !");
                }
            }
        }
        private string _MapCode { get; set; }
        public string MapCode
        {
            get { return _MapCode; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _MapCode = value;
                }
                else
                {
                    msg = "MapCode can not be blank !";
                    throw new Exception("MapCode can not be blank !");
                }
            }
        }
        private string _EncData { get; set; }
        public string EncData
        {
            get { return _EncData; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _EncData = value;
                }
                else
                {
                    msg = "EncData can not be blank !";
                    throw new Exception("EncData can not be blank !");
                }
            }
        }
        private double _Amount { get; set; }
        public double Amount
        {
            get { return _Amount; }
            set
            {
                if (value > 0)
                {
                    _Amount = value;
                }
                else
                {
                    msg = "Amount can not be blank !";
                    throw new Exception("Amount can not be blank !");
                }
            }
        }
        public string msg { get; set; }
        public bool Flag { get; set; }
        public string statuscode { get; set; }
        GenralFunction gf = new GenralFunction();

        //public void InsertGrnServiceLog()
        public void ReleaseAuditStatus()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            //PARM[1] = new SqlParameter("@type", SqlDbType.Char, 1) { Value =  };
            PARM[1] = new SqlParameter("@login", SqlDbType.VarChar, 50) { Value = MapCode };
            PARM[2] = new SqlParameter("@Amount", SqlDbType.Money) { Value = Amount };
            PARM[3] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
         
            
            //dt = gf.Filldatatablevalue(PARM, "EgGrnReleaseServiceLog", dt, null);
            dt = gf.Filldatatablevalue(PARM, "EgGrnReleaseServiceStatus", dt, null);
            if (dt.Rows.Count > 0)
            {
                //msg = type == "D" ? "Deface" : "Refund";
                //msg = dt.Rows[0][0].ToString().Trim() == "N" ? msg + " Release Amount Can Not Be greather Then Total Amount !" : msg + " Release Amount Accepted !";
                statuscode = dt.Rows[0][0].ToString().Trim();
                //msg = dt.Rows[0][1].ToString().Trim();
            }
            else
            {
                statuscode = "014";
                //msg = "No Grn Found !";
            }
            //return msg;
        }
        public void ReleaseAuditStatusLog()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            PARM[1] = new SqlParameter("@StatusMessage", SqlDbType.VarChar,100) { Value = msg };
            //dt = gf.Filldatatablevalue(PARM, "EgGrnReleaseServiceLog", dt, null);
            dt = gf.Filldatatablevalue(PARM, "EgGrnReleaseServiceStatusLog", dt, null);
            if (dt.Rows.Count > 0)
            {
                //msg = type == "D" ? "Deface" : "Refund";
                //msg = dt.Rows[0][0].ToString().Trim() == "N" ? msg + " Release Amount Can Not Be greather Then Total Amount !" : msg + " Release Amount Accepted !";
                statuscode = dt.Rows[0][0].ToString().Trim();
                //msg = dt.Rows[0][1].ToString().Trim();
            }
            else
            {
                statuscode = "014";
                //msg = "No Grn Found !";
            }
            //return msg;
        }
        

        public DataTable GetReleaseStatus()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            PARM[1] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            return gf.Filldatatablevalue(PARM, "EgVerifyGrnReleaseStatus", dt, null);
        }
        /// <summary>
        /// Check Reference No exist or not
        /// </summary>
        /// <returns> 1 and 0</returns>
        public Int16 CheckReferenceNo()
        {
            SqlParameter[] PARM = new SqlParameter[3];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = MerchantCode };
            PARM[2] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 20) { Value = HttpContext.Current.Request.UserHostAddress };
            string result = gf.ExecuteScaler(PARM, "EgReleaseReferenceExist");
            return Convert.ToInt16(result);
        }

        /// <summary>
        /// Insert Deface Merchant data audit
        /// </summary>
        /// <returns> 1 and 0</returns>
        public int ReleaseAuditLogs()
        {
            SqlParameter[] PARM = new SqlParameter[5];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = MerchantCode };
            PARM[2] = new SqlParameter("@EncData", SqlDbType.VarChar, -1) { Value = EncData };
            PARM[3] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            PARM[4] = new SqlParameter("@IpAddress", SqlDbType.VarChar, 20) { Value = HttpContext.Current.Request.UserHostAddress };
            return gf.UpdateData(PARM, "EgInsertReleaseMerchantAudit");
        }

    }
}
