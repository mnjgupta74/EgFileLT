using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgEditChallanDetailsBL
    {
        #region Properties
       
        public Int64 GRN { get; set; }
        public string Location { get; set; }
        public string TreasuryCode { get; set; }            
        public double Amount { get; set; }
        public string District { get; set; }
        public int Office { get; set; }
        public int DeptCode { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public DateTime ChallanFromMonth { get; set; }
        public DateTime ChallanToMonth { get; set; }
        public int month { get; set; }
        public string tin { get; set; }

        #endregion

        #region functions

        public int GetGRNDetails()
        {
            GenralFunction gf = new GenralFunction();
            DataTable objsdr = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];           
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            objsdr = gf.Filldatatablevalue(PARM, "EgGetEditChallanDetails", objsdr, null);

            int output = 0;
            if (objsdr.Rows.Count != 0)
            {
                District = objsdr.Rows[0]["District"].ToString();
                TreasuryCode = objsdr.Rows[0]["Location"].ToString(); 
                Office = Convert.ToInt32(objsdr.Rows[0]["OfficeName"]);
                DeptCode = Convert.ToInt32(objsdr.Rows[0]["deptcode"]);
                Amount = Convert.ToInt32(objsdr.Rows[0]["amount"]);
                FullName = objsdr.Rows[0]["FullName"].ToString();
                Address = objsdr.Rows[0]["Address"].ToString();
                Remarks = objsdr.Rows[0]["Remarks"].ToString();
                month = Convert.ToInt32(objsdr.Rows[0]["Month"]);
                ChallanFromMonth = Convert.ToDateTime(objsdr.Rows[0]["ChallanFromMonth"].ToString());
                ChallanToMonth = Convert.ToDateTime(objsdr.Rows[0]["ChallanToMonth"].ToString());
                tin = objsdr.Rows[0]["Identity"].ToString();
                output = 1;
            }
            else
            {
                output = 0;
            }
            return output;
        }

        public int EditChallanDetails()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[9];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Char,4) { Value = TreasuryCode };
            PARM[2] = new SqlParameter("@Office", SqlDbType.Int) { Value = Office };
            PARM[3] = new SqlParameter("@FullName", SqlDbType.VarChar, 50) { Value = FullName };
            PARM[4] = new SqlParameter("@Address", SqlDbType.VarChar,100) { Value = Address };
            PARM[5] = new SqlParameter("@Remarks", SqlDbType.VarChar,200) { Value = Remarks };
            PARM[6] = new SqlParameter("@ChallanFromMonth", SqlDbType.SmallDateTime) { Value = ChallanFromMonth };
            PARM[7] = new SqlParameter("@ChallanToMonth", SqlDbType.SmallDateTime) { Value = ChallanToMonth };
            PARM[8] = new SqlParameter("@identity", SqlDbType.VarChar, 15) { Value = tin };
            return gf.UpdateData(PARM, "EgUpdateEChallanByTO");
           
        }

        #endregion
    }
}
