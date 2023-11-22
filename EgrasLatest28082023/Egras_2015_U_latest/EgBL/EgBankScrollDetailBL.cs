using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
  public  class EgBankScrollDetailBL
    {
        GenralFunction gf;
        #region Properties
      
        public DateTime Date { get; set; }
        public Int32 UserId { get; set; }
        public int TotalTransaction { get; set; }
        public double TotalAmount { get; set; }
        #endregion
        #region Function

        public void GetTransactionDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
            PARM[1] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = UserId };
            //DataTable dt = new DataTable();
            //dt = gf.Filldatatablevalue(PARM, "Eg8782HeadDetails", dt, null);
            SqlDataReader sdr;
            sdr = gf.FillDataReader(PARM, "EgBankTransactionDetail");
            if (sdr.HasRows)
            {
                sdr.Read();
                if (Convert.ToInt32(sdr["TotalTransaction"].ToString()) > 0)
                {
                    TotalTransaction = Convert.ToInt32(sdr["TotalTransaction"].ToString());
                    TotalAmount = Convert.ToDouble(sdr["TotalAmount"].ToString());
                }
            }
            sdr.Close();
            sdr.Dispose();

           
        }
        #endregion
    }
}
