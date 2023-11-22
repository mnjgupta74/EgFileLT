using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;

namespace EgBL
{
    public  class EgEMinusChallanBL:EgEChallanBL
   {
       public string ObjectHead { get; set; }
       public string VNC { get; set; }
       public string PNP { get; set; }
       public int ddo { get; set; }
  
       GenralFunction gf = new GenralFunction();
       public DataTable GetOfficeForMinus()
       {
           DataTable dt = new DataTable();
           SqlParameter[] PARM = new SqlParameter[1];
           PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
           return gf.Filldatatablevalue(PARM, "EgGetOfficeTreasuryForMinus", dt, null);
       }
       public void GetOfficeWiseDDO(DropDownList ddl)
       {
           SqlParameter[] PARM = new SqlParameter[2];
           PARM[0] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeName };
           PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Int) { Value = TreasuryCode };
            gf.FillListControl(ddl, "EgGetOfficeWiseDDO", "ddo_name", "ddo_code", PARM);
           ddl.Items.Insert(0, new ListItem("--Select DDO Name --", "0"));
       }

       public void GetObjectHeads(DropDownList ddl)
       {
           gf.FillListControl(ddl, "EgGetObjectHeads", "objectheadnamehindi", "objectheadcode", null);
           ddl.Items.Insert(0, new ListItem("--Select ObjectHead--", "0"));
       }

       public DataTable fillDeptWiseMajorHeadListForMinus()
       {
           SqlParameter[] PM = new SqlParameter[1];
           PM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
           DataTable dt = new DataTable();
           return gf.Filldatatablevalue(PM, "EgDeptwiseMajorHeadListForMinus", dt, null);
       }
       public int CheckBudgetHead()
       {
           SqlParameter[] PM = new SqlParameter[4];
           PM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
           PM[1] = new SqlParameter("@ObjectHead", SqlDbType.Char, 2) { Value = ObjectHead };
           PM[2] = new SqlParameter("@VNC", SqlDbType.Char, 1) { Value = VNC };
           PM[3] = new SqlParameter("@PNP", SqlDbType.Char, 1) { Value = PNP };
           return int.Parse(gf.ExecuteScaler(PM, "EgCheckBudgetHead"));
       }

       public DataTable GetBudgetHeadPNPVNC()
       {
           DataTable dt = new DataTable();
           SqlParameter[] PARM = new SqlParameter[1];
           PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
           return gf.Filldatatablevalue(PARM, "EgGetBudgetHeadPNPVNC", dt,null);
         
       }

       public override  int InsertChallan()
       {
           GenralFunction gf = new GenralFunction();
           SqlParameter[] PARM = new SqlParameter[28];

           PARM[0] = new SqlParameter("@Profile", SqlDbType.Int) { Value = Profile };
           PARM[1] = new SqlParameter("@Identity", SqlDbType.VarChar, 50) { Value = Identity };
           PARM[2] = new SqlParameter("@OfficeName", SqlDbType.Int) { Value = OfficeName };
           PARM[3] = new SqlParameter("@PANNumber", SqlDbType.Char, 10) { Value = PanNumber };
           PARM[4] = new SqlParameter("@Location", SqlDbType.Char, 4) { Value = Location };
           PARM[5] = new SqlParameter("@FullName", SqlDbType.VarChar, 30) { Value = FullName };
           PARM[6] = new SqlParameter("@ChallanYear", SqlDbType.Char, 4) { Value = ChallanYear };
           PARM[7] = new SqlParameter("@ChallanFromMonth", SqlDbType.SmallDateTime) { Value = ChallanFromMonth };
           PARM[8] = new SqlParameter("@ChallanToMonth", SqlDbType.SmallDateTime) { Value = ChallanToMonth };
           PARM[9] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = City };
           PARM[10] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = Address };
           PARM[11] = new SqlParameter("@PINCode", SqlDbType.Char, 6) { Value = PINCode };
           PARM[12] = new SqlParameter("@DeductCommission", SqlDbType.Money) { Value = DeductCommission };
           PARM[13] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = TotalAmount };
           if (TypeofPayment == "M")
           {
               PARM[14] = new SqlParameter("@ChequeDDNo", SqlDbType.Char, 6) { Value = ChequeDDNo };
           }
           else
           {
               PARM[14] = new SqlParameter("@ChequeDDNo", SqlDbType.Char, 6) { Value = "" };
           }
           PARM[15] = new SqlParameter("@BankName", SqlDbType.Char, 7) { Value = BankName };
           PARM[16] = new SqlParameter("@Paymenttype", SqlDbType.Char, 1) { Value = TypeofPayment };
           PARM[17] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
           PARM[18] = new SqlParameter("@Id", SqlDbType.Int) { Value = Id };
           PARM[18].Direction = ParameterDirection.Output;
           PARM[19] = new SqlParameter("@Remarks", SqlDbType.VarChar, 200) { Value = Remark };  
           PARM[20] = new SqlParameter("@Details", SqlDbType.VarChar, 8000) { Value = Details };
           PARM[21] = new SqlParameter("@PdAcc", SqlDbType.Int) { Value = PDacc };
           PARM[22] = new SqlParameter("@ObjectHead", SqlDbType.Char, 2) { Value = ObjectHead };
           PARM[23] = new SqlParameter("@VNC", SqlDbType.Char, 1) { Value = VNC };
           PARM[24] = new SqlParameter("@PNP", SqlDbType.Char, 1) { Value = PNP };
           PARM[25] = new SqlParameter("@ddo", SqlDbType.Int) { Value = ddo };
           PARM[26] = new SqlParameter("@mytable", SqlDbType.Structured) { Value = dtSchema };
            PARM[27] = new SqlParameter("@IpAddress", SqlDbType.VarChar, 20) { Value = HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString() }; //HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() }; // add Ip Nov12 2019
            int Rv = gf.UpdateData(PARM, "EgEChallan");

           if (Rv == 0)
           {
               Rv = -1;
           }
           else
           {
               Id = Convert.ToInt32(PARM[18].Value);
               Rv = int.Parse(PARM[18].Value.ToString());
           }
           return Rv;
       }

       /// <summary>
       /// Fill Bank DropDown in case of Manual Challan Entry for ME
       /// </summary>
       /// <param name="ddl">BankName,BSRCode</param>
       public void FillBanksME(DropDownList ddl)
       {
           SqlParameter[] PARM = new SqlParameter[1];
           PARM[0] = new SqlParameter("@treasurycode", SqlDbType.Char, 4) { Value = TreasuryCode };
           gf.FillListControl(ddl, "EgGetBankListForME", "BankName", "BSRCode", PARM);

           ddl.Items.Insert(0, new ListItem("--Select Bank--", "0"));
       }

        public DataTable FillDeptwiseMajorHead(int deptcode, int userid)
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@deptcode", SqlDbType.Int) { Value = deptcode };
            PM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userid };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PM, "EgFillMajorHeadByOfficeId", dt, null);
            return dt;
        }
    }
}
