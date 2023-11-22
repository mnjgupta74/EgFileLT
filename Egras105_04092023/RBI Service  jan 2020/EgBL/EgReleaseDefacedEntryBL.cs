using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace EgBL
{
    public class EgReleaseDefacedEntryBL
    {
        GenralFunction gf = new GenralFunction();

        

        #region Class Properties
        public Int64 Grn { get; set; }
        public String Sno { get; set; }
        public int UserId { get; set; }
        public int type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double DefacedAmount { get; set; }
        public double RefundAmount { get; set; }
        public double releaseAmt { get; set; }
        public double TotalAmount { get; set; }

        public int flag { get; set; }
        public double RefrenceNo { get; set; }
        public string ReleaseType { get; set; }
        public object rptSchemaDi {get;set;}
        #endregion

        public object sumofobject { get; set; }
        public DataTable GetDefacedGRN()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@type", SqlDbType.Int) { Value = type };
            gf.Filldatatablevalue(PARM, "EgGetDefacedGRNDetail", dt, null);
            return dt;
        }
        public void GetPartialAmount()
        {
            SqlParameter[] PARM = new SqlParameter[1];

            PARM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetPartialAmount", dt, null);
            if (dt.Rows[0]["Amt"].ToString() != "")
            {
                DefacedAmount = Convert.ToDouble(dt.Rows[0]["Amt"]);
            }
            else
                DefacedAmount = 0.0;
        }
        public void GetRefundAmount()
        {
            SqlParameter[] PARM = new SqlParameter[1];

            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgFetchRefundDetail", dt, null);
            if (dt.Rows[0]["Amount"].ToString() != "")
            {
                RefundAmount = Convert.ToDouble(dt.Rows[0]["Amount"]);
            }
            else
                RefundAmount = 0.0;
        }
        public Double GetReleaseAmount()
        {
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            double releasedAmt = Convert.ToDouble(gf.ExecuteScaler(PARM, "EgCheckReleaseAmount"));
            return releasedAmt;
        }
        public int InsertReleaseAmount()
        {
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Grn };
            PARM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
            PARM[2] = new SqlParameter("@Amount", SqlDbType.Money) { Value = releaseAmt };
            PARM[3] = new SqlParameter("@ReferenceNo", SqlDbType.Money) { Value = RefrenceNo };
            if (type == 0)
                return gf.UpdateData(PARM, "EgInsertReleaseAmount");
            else
                return gf.UpdateData(PARM, "EgInsertRefundReleaseAmount");
        }
        /// <summary>
        /// Refund Release Amount 13 june 2022
        /// </summary>
        /// <returns></returns>
        public int InsertRefundReleaseAmount()
        {
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Grn };
            PARM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
            PARM[2] = new SqlParameter("@Amount", SqlDbType.Money) { Value = releaseAmt };
          
           return gf.UpdateData(PARM, "EgInsertRefundReleaseAmount");
        }


        public void fillrepeater(Repeater rpt)
        {
            DataTable tblReport = new DataTable();
            
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgReleasedGRN", dt, null);
            sumofobject =dt.Compute("Sum(Amount)", "");
            sumofobject = string.Format("{0:0.00}",sumofobject);
            if (dt.Rows.Count > 0)
            {

                rptSchemaDi = true;
                flag = 1;
                rpt.DataSource = dt;
                rpt.DataBind();
                dt.Dispose();
              
            }
            else
            {
                flag = 0;
            }
        }

        /// <summary>
        /// Release Amount Auto Service 3 August 2021
        /// </summary>
        /// <returns></returns>
        public int CheckGrnStatus()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            PARM[1] = new SqlParameter("@type", SqlDbType.Char, 1) { Value = ReleaseType };

            int result = Convert.ToInt16(gf.ExecuteScaler(PARM, "EgCheckGrnStatus"));
            return Convert.ToInt16(result);
        }

        // RElasee Amount 2 june 2021
        /// <summary>
        /// Release Amount Auto Service 3 August 2021
        /// </summary>
        /// <returns></returns>
        public Double GetReleasableServiceAmount()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@ReferenceNo", SqlDbType.Int) { Value = RefrenceNo };
            PARM[2] = new SqlParameter("@type", SqlDbType.Char, 1) { Value = ReleaseType };
            //double releasedAmt = Convert.ToDouble(gf.ExecuteScaler(PARM, "EgGetReleasableServiceAmount"));
            dt = gf.Filldatatablevalue(PARM, "EgGetReleasableServiceAmount", dt, null);
            if (dt.Rows[0]["Amount"].ToString() != "")
            {
                RefrenceNo = RefrenceNo;
                return Convert.ToDouble(dt.Rows[0]["Amount"]);
            }
            else
                return 0.0;
        }
        /// <summary>
        /// Eg release Amount Auto Service   3 August 2021
        /// </summary>
        /// <returns></returns>
        public DataTable GetReleaseGRNByService()
        {
            SqlParameter[] PARM = new SqlParameter[1];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@type", SqlDbType.Int) { Value = type };
            gf.Filldatatablevalue(PARM, "EgGetReleaseGRNDetail", dt, null);
            return dt;
        }
    }
}
