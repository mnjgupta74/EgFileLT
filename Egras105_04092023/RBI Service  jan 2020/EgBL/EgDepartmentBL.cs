using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;

namespace EgBL
{
    public class EgDepartmentBL
    {
        GenralFunction gf = new GenralFunction();
        #region Class Properties
        /// <summary>
        /// EgDepartmentBL Class Properties
        /// </summary>
        public int DeptCode { get; set; }
        public Int64 Grn { get; set; }
        public int UserId { get; set; }
    
        public double amount { get; set; }
        public string deface { get; set; }
        public int ScheCode { get; set; }
        public string LoginId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Result { get; set; }
        #endregion
        #region Function
        /// <summary>
        /// Return Record which GRN STATUS Successful and bank flag is y 
        /// </summary>
        /// <param name="grd"> dataset for fill gridview</param>
        public DataTable  FillGrid()
        {
            DataTable dt = new DataTable();
                
            if (Grn != 0)
            {
                SqlParameter[] PARM = new SqlParameter[2];
                PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
                PARM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
               return  gf.Filldatatablevalue(PARM, "EgDefaceGRN", dt, null);
               
            }
            else
            {
                SqlParameter[] PARM = new SqlParameter[3];
                PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime ) { Value = FromDate};
                PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
                PARM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
                return gf.Filldatatablevalue(PARM, "EgDefaceGrnDatewise", dt, null);
               
            }
        }
        /// <summary>
        /// Insert GRN Deface Amount Detail 
        /// </summary>
        /// <returns> 1 and 0</returns>
        public int InsertFullDeface()
        {
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@Deface", SqlDbType.Char, 1) { Value = deface };
            PARM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
            PARM[3] = new SqlParameter("@amount", SqlDbType.Money) { Value = amount };
            PARM[4] = new SqlParameter("@Res", SqlDbType.Int) { Value = 0 };
            PARM[4].Direction = ParameterDirection.Output;
            int i = gf.UpdateData(PARM, "EgInsertPartialDeface");
            if (PARM[4].Value != DBNull.Value)
            {
                Result = Convert.ToInt32(PARM[4].Value);
            }
            return i;
        }
        /// <summary>
        /// InsertFull GRN Deface Amount Detail 
        /// </summary>
        /// <returns> 1 and 0</returns>
        public int InsertPartialDeface()
        {
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@Deface", SqlDbType.Char, 1) { Value = deface };
            PARM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
            PARM[3] = new SqlParameter("@amount", SqlDbType.Money) { Value = amount };
            return gf.UpdateData(PARM, "EgInsertPartialDeface");
        }
        /// <summary>
        /// Get Total deface Amount  on GRN
        /// </summary>
        public void GetPartialAmount()
        {
            SqlParameter[] PARM = new SqlParameter[1];

            PARM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetPartialAmount", dt, null);
            if (dt.Rows[0]["Amt"].ToString() != "")
            {
                amount = Convert.ToDouble(dt.Rows[0]["Amt"]);
            }
            else
                amount = 0.0;
        }
        /// <summary>
        /// Check deface Full /partially
        /// </summary>
        public void CheckDeface()
        {
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgCheckGRNDeface", dt, null);
            if (dt.Rows.Count != 0)
            {
                deface = dt.Rows[0]["Deface"].ToString();
            }
            else
                deface = "N";
        }
        
        //// This Function Not in Use
        //public void FillLoginIdWiseGrid(GridView grd)
        //{
        //    SqlParameter[] PARM = new SqlParameter[2];
        //    PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
        //    PARM[1] = new SqlParameter("@LoginId", SqlDbType.VarChar, 20) { Value = LoginId };
        //    gf.FillGridViewControl(grd, PARM, "EgGetLoginIdWiseRecord");
        //}
        public DataTable FillDefaceDetail()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];

            PARM[0] = new SqlParameter("@GRN", SqlDbType.Int) { Value = Grn };
            //PARM[1] = new SqlParameter("@ScheCode", SqlDbType.Int) { Value = ScheCode };
            //PARM[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            dt = gf.Filldatatablevalue(PARM, "EgDefaceAmountDetail", dt, null);
            return dt;
        }

        public string GetDeptExistance()

        {
            SqlParameter[] PARM = new SqlParameter[2];
          
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@GRN", SqlDbType.Int) { Value = Grn };
            int value = Convert.ToInt32(gf.ExecuteScaler(PARM, "EgDeptExistance"));
            return value.ToString();
         
        
        }

        public DataTable FetDefaceDetails()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];

            PARM[0] = new SqlParameter("@GRN", SqlDbType.Int) { Value = Grn };
            //PARM[1] = new SqlParameter("@ScheCode", SqlDbType.Int) { Value = ScheCode };
            //PARM[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            dt = gf.Filldatatablevalue(PARM, "EgFetchDefaceDetail", dt, null);
            return dt;
        }
        //public int CheckUserIdDept(SqlTransaction Trans)
        //{
        //    SqlParameter[] PARM = new SqlParameter[1];
        //    PARM[0] = new SqlParameter("@GRN", SqlDbType.Int) { Value = Grn };
        //    return Convert.ToInt32(gf.ExecuteScaler(PARM, "EgCheckUserIdDept", Trans));

        //}
        /// <summary>
        /// Return Record which GRN STATUS Successful and bank flag is y 
        /// </summary>
        /// <param name="grd"> dataset for fill gridview</param>
        public DataTable   BindGrid()
        {
            DataTable dt = new DataTable();
            if (Grn != 0)
            {
                SqlParameter[] PARM = new SqlParameter[2];
                PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
                PARM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
               
                return gf.Filldatatablevalue(PARM, "EgDefaceGrnByTO", dt, null);
            }
            else
            {
                SqlParameter[] PARM = new SqlParameter[3];
                PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
                PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
                PARM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
                return gf.Filldatatablevalue(PARM, "EgDefaceGrnDateWiseByTO", dt, null);
                // gf.FillGridViewControl(grd, PARM, "EgDefaceGrnDateWiseByTO");
            }
        }
        #endregion
    }
}
