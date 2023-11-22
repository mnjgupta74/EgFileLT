using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgDeptOfficewiseAmount
    {

        #region properties
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Type { get; set; }

        public string BudgetHead { get; set; }
        #endregion


        //public void EgDeptOfficeWiseAmountRpt(Repeater rpt)
        //{
        //    GenralFunction gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[4];
        //    PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
        //    PARM[1] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
        //    PARM[2] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
        //    PARM[3] = new SqlParameter("@BudgetHead", SqlDbType.Char,13) { Value = BudgetHead };
        //    //if (Type == 2)
        //    //    gf.FillRepeaterControl(rpt, PARM, "EgDeptOfficeWiseAmount");
        //    if (Type == 4)
        //        gf.FillRepeaterControl(rpt, PARM, "EgOfficeWiseChallanTotalToRajkosh");
        //    //else if (Type==1)
        //    //    gf.FillRepeaterControl(rpt, PARM, "EgDeptOfficeWiseAmount_CTD");
        //    else
        //        gf.FillRepeaterControl(rpt, PARM, "EgDeptOfficeWiseAmount_challandate");
        //}

        // Instantiate a SafeHandle instance.
        // Modify 31 may 2019 for data Bind with Json  And Html
        public string EgDeptOfficeWiseAmountRpt()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[3] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            if (Type == 4)
                gf.Filldatatablevalue(PARM, "EgOfficeWiseChallanTotalToRajkosh", dt, null);
            else
                gf.Filldatatablevalue(PARM, "EgDeptOfficeWiseAmount_challandate", dt, null);


            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }



    }
}
