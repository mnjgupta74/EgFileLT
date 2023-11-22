using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace EgBL
{
     public class AppCheckChallanConditionBL
    {


        public string BudgetHead { get; set; }
        public int DeptCode { get; set; }
        private string payMode { get; set; }
        private string pdAccountNo { get; set; }
        private int pdAccountNoCount { get; set; }
        public string treasuryCodeBank { get; set; }
        private string treasuryCodePd { get; set; }
        public  string msg { get; set; }
        private bool pdVisible { get; set; }
        public string Office { get; set; }
        public int proc_id { get; set; }
        EgCheckChallanCondition ObjCheck = new EgCheckChallanCondition();
        GenralFunction gf;

        public bool CheckCondition(Int64 Grn, int Userid)
        {
            GetConditionalData(Grn, Userid);
            return false;
        }



        //public bool CheckCondition(string budgetHead, int deptCode, string payMode, string pdAccountNo, int pdAccountNoCount, string treasury,int pdvisible,string officeId)
        //{

        //    ObjCheck.BudgetHead = budgetHead;
        //    ObjCheck.DeptCode = deptCode;
        //    ObjCheck.payMode = payMode;
        //    ObjCheck.pdAccountNo = pdAccountNo;
        //    ObjCheck.pdAccountNoCount = pdAccountNoCount;
        //    ObjCheck.treasuryCodeBank = treasury;
        //    ObjCheck.treasuryCodePd = treasury;
        //    if (Convert.ToInt16(pdvisible) == 0)
        //        ObjCheck.pdVisible = false;
        //    else
        //        ObjCheck.pdVisible = true;
        //    Office = officeId;
        //    if (!ObjCheck.CheckSubmitCondition())
        //    {
        //        if (!ObjCheck.CheckHead004000102())
        //        {
        //            if (!ObjCheck.CheckHeadGreater8000WithPD())
        //            {
        //                return false;

        //            }
        //            else
        //                return true;

        //        }
        //        else
        //            return true;
        //    }
        //    else
        //    {
        //        msg = ObjCheck.msg;
        //        return true;
        //    }


        //}



        public void  GetConditionalData(Int64 Grn ,int UserId)
            {

            gf = new GenralFunction();
            SqlDataReader dr;
            string Result = "";
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            PM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            dr = gf.FillDataReader(PM, "EgCheckChallanCondition_App");
            if (dr.HasRows != false)
            {
                if (dr.Read())
                {
                    ObjCheck.BudgetHead = dr[0].ToString();
                    BudgetHead = dr[0].ToString();
                    ObjCheck.DeptCode = Convert.ToInt32(dr[1].ToString());
                    DeptCode = Convert.ToInt32(dr[1].ToString());
                    ObjCheck.payMode = dr[2].ToString();
                    ObjCheck.pdAccountNo = dr[3].ToString();
                    ObjCheck.pdAccountNoCount = Convert.ToInt16(dr[4].ToString());
                    ObjCheck.treasuryCodeBank = dr[5].ToString();
                    treasuryCodeBank = dr[5].ToString(); 
                    ObjCheck.treasuryCodePd =dr[6].ToString();
                   
                 
                    if (Convert.ToInt16(dr[7]) == 0)
                        ObjCheck.pdVisible =false;
                      
                    else
                        ObjCheck.pdVisible = true;

                    Office = dr[8].ToString();
                }

            }
            else
            {
                Result = "0";
              
            }
            dr.Close();
            dr.Dispose();
           

           }


         //data Get By Profile Based
        public void GetProfileConditionalData(int profileId, int UserId)
        {

            gf = new GenralFunction();
            SqlDataReader dr;
            string Result = "";
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@ProfileId", SqlDbType.BigInt) { Value = profileId };
            PM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            dr = gf.FillDataReader(PM, "EgCheckprofileCondition_App");
            if (dr.HasRows != false)
            {
                if (dr.Read())
                {
                    ObjCheck.BudgetHead = dr[0].ToString();
                    BudgetHead = dr[0].ToString();
                    ObjCheck.DeptCode = Convert.ToInt32(dr[1].ToString());
                    DeptCode = Convert.ToInt32(dr[1].ToString());
                }
            }
            else
            {
                Result = "0";

            }
            dr.Close();
            dr.Dispose();


        }

        //data Get By Service Based
        public void GetServiceConditionalData(int ServiceId, int UserId)
        {

            gf = new GenralFunction();
            SqlDataReader dr;
            string Result = "";
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@ServiceId", SqlDbType.BigInt) { Value = ServiceId };
            dr = gf.FillDataReader(PM, "EgCheckServiceCondition_App");
            if (dr.HasRows != false)
            {
                if (dr.Read())
                {
                    ObjCheck.BudgetHead = dr[0].ToString();
                    BudgetHead = dr[0].ToString();
                    ObjCheck.DeptCode = Convert.ToInt32(dr[1].ToString());
                    DeptCode = Convert.ToInt32(dr[1].ToString());
                    proc_id = Convert.ToInt32(dr[2].ToString());
                }
            }
            else
            {
                Result = "0";

            }
            dr.Close();
            dr.Dispose();


        }





        public DataTable CheckBudgetHeadVisibilty()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];

            PM[0] = new SqlParameter("@BudgetHead", SqlDbType.VarChar,13) { Value = BudgetHead };
            PM[1] = new SqlParameter("@Officeid", SqlDbType.Int) { Value = Convert.ToInt32 (Office) };
            PM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char,4) { Value = treasuryCodeBank };
            return (gf.Filldatatablevalue(PM, "EgGetDivisionCodeList_App", dt, null));

        }

         /// <summary>
         /// Message List For Flash Screen
         /// </summary>
         /// <returns></returns>
        public string Message()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[1];

            PM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            return gf.ExecuteScaler(PM, "EgMassage_App");
           
        
        }

    }
}
