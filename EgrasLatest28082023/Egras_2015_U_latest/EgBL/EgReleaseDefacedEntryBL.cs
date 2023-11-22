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
        public double RefundAvailableAmount { get; set; }
        public double releaseAmt { get; set; }
        public double TotalAmount { get; set; }

        public int flag { get; set; }
        public double RefrenceNo { get; set; }
        public string ReleaseType { get; set; }
        public object rptSchemaDi { get; set; }
        #endregion

        public object sumofobject { get; set; }
        public string Comment { get; set; }
        public string ObjectionList { get; set; }
        public int DeptCode { get; set; }
        public string ObjectionID { get; set; }
        public int Valuetype { get; set; }
        public DataTable GetDefacedGRN()
        {
            SqlParameter[] PARM = new SqlParameter[3];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@type", SqlDbType.Int) { Value = type };
            PARM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            gf.Filldatatablevalue(PARM, "EgGetDefacedGRNDetail", dt, null);
            return dt;
        }

       
        public DataTable GetReleaseGRNByServiceForOffice()
        {
            SqlParameter[] PARM = new SqlParameter[3];
            DataTable dt = new DataTable();
            //PARM[0] = new SqlParameter("@type", SqlDbType.Int) { Value = type };
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = System.Web.HttpContext.Current.Session["UserId"] };
            PARM[2] = new SqlParameter("@valueType", SqlDbType.Int) { Value = Valuetype };
            gf.Filldatatablevalue(PARM, "[EgGetDefaceReleaseGrnForOffice]", dt, null);
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

        /// <summary>
        /// Refund Avaiable Amount
        /// 13 june 2022
        /// </summary>
        /// <returns></returns>
        /// 
        public double GetRefundAvaiableAmount()
        {
            SqlParameter[] PARM = new SqlParameter[1];

            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "[EgFetchRefundAvailableDetail]", dt, null);
            if (dt.Rows[0]["Amount"].ToString() != "")
            {
                RefundAvailableAmount = Convert.ToDouble(dt.Rows[0]["Amount"]);
            }
            else
                RefundAvailableAmount = 0.0;

            return RefundAvailableAmount;
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
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Grn };
            PARM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
            PARM[2] = new SqlParameter("@Amount", SqlDbType.Money) { Value = releaseAmt };
            PARM[3] = new SqlParameter("@ReferenceNo", SqlDbType.Money) { Value = RefrenceNo };
            PARM[4] = new SqlParameter("@Valuetype", SqlDbType.Int) { Value = Valuetype };
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
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@ReferenceNo", SqlDbType.Int) { Value = RefrenceNo };
            PARM[2] = new SqlParameter("@type", SqlDbType.Char, 1) { Value = ReleaseType };
            PARM[3] = new SqlParameter("@Valuetype", SqlDbType.Int) { Value = Valuetype };
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
            SqlParameter[] PARM = new SqlParameter[2];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@type", SqlDbType.Int) { Value = type };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            gf.Filldatatablevalue(PARM, "EgGetReleaseGRNDetail", dt, null);
            return dt;
        }

        ///<summary>
        ///Get ObjectionList 5 April 2022
        /// </summary>
        public DataTable GetObjectionList()
        {
            SqlParameter[] PARM = new SqlParameter[0];
            DataTable dt = new DataTable();
            //PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            //PARM[1] = new SqlParameter("@type", SqlDbType.Int) { Value = type };
            gf.Filldatatablevalue(PARM, "EgGetObjectionList", dt, null);
            return dt;
        }
        /// <summary>
        /// Insert Onbjection
        /// </summary>
        /// <returns></returns>
        public int InsertObjection()
        {
            SqlParameter[] PARM = new SqlParameter[5];

            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = RefrenceNo };
            PARM[1] = new SqlParameter("@ObjectionList", SqlDbType.VarChar,500) { Value = ObjectionList };
            PARM[2] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Grn };
            PARM[3] = new SqlParameter("@Comment", SqlDbType.VarChar,200) { Value = Comment };
            PARM[4] = new SqlParameter("@UserID", SqlDbType.Int) { Value = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]) };
            return gf.UpdateData(PARM, "EgInsertObjectionReleaseAmount");
        }
        /// <summary>
        /// Get objection for Office 1 june 2022
        /// </summary>
        /// <returns></returns>

        public DataTable GetObjectionListForOffice()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            PARM[1] = new SqlParameter("@ReferenceNo", SqlDbType.Int) { Value = RefrenceNo };
            gf.Filldatatablevalue(PARM, "EgGetObjectionListForOffice", dt, null);
            return dt;
        }
        /// <summary>
        /// insert Objection 1 june 2022
        /// </summary>
        /// <returns></returns>


        public int InsertObjectionForOffice()
        {
            SqlParameter[] PARM = new SqlParameter[5];

            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = RefrenceNo };
            PARM[1] = new SqlParameter("@ObjectionList", SqlDbType.VarChar, 500) { Value = ObjectionList };
            PARM[2] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Grn };
            PARM[3] = new SqlParameter("@UserID", SqlDbType.Int) { Value = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]) };
            PARM[4] = new SqlParameter("@Comment", SqlDbType.VarChar, 200) { Value = Comment };
            return gf.UpdateData(PARM, "EgInsertObjectionReleaseAmountForOffice");
        }

        //Get PDF Bytes For Office
        public DataTable GetDefaceReleasePDfBytes()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = RefrenceNo };
            PARM[1] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };

            gf.Filldatatablevalue(PARM, "EgGetDefaceReleasePDfBytes", dt, null);
            return dt;
            //return gf.ExecuteScaler(PARM, "EgGetDefaceReleasePDfBytes");

        }
    }
}
