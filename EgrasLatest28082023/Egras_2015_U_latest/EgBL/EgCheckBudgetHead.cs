using System;
using System.Data;
using System.Data.SqlClient;
namespace EgBL
{
    public class EgCheckBudgetHead
    {
        GenralFunction gf;
        #region Properties
        public string BudgetHead { get; set; }
        //public int Count { get; set; }
        public string TreasuryCode { get; set; }
        public int OfficeId { get; set; }
        public int DepartmentCode { get; set; }
        public int Mcode { get; set; }
        public string AUIN { get; set; }
        public string EncData { get; set; }
        public string Address { get; set; }
        public int PDAcc { get; set; }
        public string PayMode { get; set; }
        public string ChallanType { get; set; }
        public int type { get; set; }
        public string DdoCode { get; set; }
        #endregion
        #region Method
        //public DataTable GetBudgetHeadList()
        //{

        //    SqlParameter[] PM = new SqlParameter[2];
        //    PM[0] = new SqlParameter("@BudgetHeadList", SqlDbType.VarChar, 200) { Value = BudgetHead };
        //    PM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DepartmentCode };
        //    gf = new GenralFunction();
        //    DataTable dt = new DataTable();
        //    dt = gf.Filldatatablevalue(PM, "EgCheckBudgetHeadExistTest", dt, null);
        //    return dt;

        //}
        public bool isDeptIntegrationEnabled()
        {
            SqlParameter[] PM = new SqlParameter[0];
            gf = new GenralFunction();
            string returnData = gf.ExecuteScaler(PM, "EgDeptIntegrationEnabled");
            return Convert.ToBoolean(returnData);
        }
        public DataTable GetBudgetHeadDetail(DataTable Checkdt)
        {
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@mytable", SqlDbType.Structured) { Value = Checkdt };
            PM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DepartmentCode };
            PM[2] = new SqlParameter("@ChallanType", SqlDbType.VarChar,5) { Value = ChallanType };
            gf = new GenralFunction();
            DataTable dt1 = new DataTable();
            dt1 = gf.Filldatatablevalue(PM, "EgCheckBudgetHeadExist", dt1, null);
            return dt1;

        }
        public int GetMerchantinfo()
        {
            //gf = new GenralFunction();
            //DataTable dt = new DataTable();
            //dt = gf.Filldatatablevalue(null, "EgGetMerchantinfo", dt, null);
            //return dt;
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@Mcode", SqlDbType.Int) { Value = Mcode };
            PM[1] = new SqlParameter("@Address", SqlDbType.NVarChar, 300) { Value = Address };
            PM[2] = new SqlParameter("@AUIN", SqlDbType.VarChar, 50) { Value = AUIN };
            PM[3] = new SqlParameter("@EncData", SqlDbType.NVarChar, 3000) { Value = EncData };
            PM[4] = new SqlParameter("@ChallanType", SqlDbType.NVarChar, 3000) { Value = ChallanType };
            return Convert.ToInt32(gf.ExecuteScaler(PM, "EgGetMerchantinfo"));
        }
        /// <summary>
        /// VArify Office Id 
        /// </summary>
        /// <returns>1,0</returns>
        public int VarifyOfficeId()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PM[1] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeId };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DepartmentCode };
            //return Convert.ToInt32(gf.ExecuteScaler(PM, "EgCheckOfficeIdExisting"));
            string result = gf.ExecuteScaler(PM, "EgCheckOfficeIdExisting");
            int returnVal;
            bool isNumeric = int.TryParse(result, out returnVal);
            if (isNumeric)
                return returnVal;
            else
                return 0;
        }
       
        public DataTable FillBank1()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            gf.Filldatatablevalue(null, "EgFillBank", dt, null);
            dt = dt.AsEnumerable().Where(t => t.Field<string>("access").Trim() != "D" && t.Field<string>("access").Trim() != "R").CopyToDataTable();
            return dt;

        }

        public DataTable FillManualBanks()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@treasurycode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@type", SqlDbType.Int) { Value = type };
            gf.Filldatatablevalue(PARM, "EgGetBanksList", dt, null);
            return dt;

        }
        /// <summary>
        /// Check Pd Account 24 nov 2017  Pd Online Allow
        /// </summary>dow
        /// <returns></returns>
        public bool VerifyPdAcc()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PM[2] = new SqlParameter("@PDAcc", SqlDbType.Int) { Value = PDAcc };
            PM[3] = new SqlParameter("@PayMode", SqlDbType.Char, 1) { Value = PayMode };
            return Convert.ToBoolean(gf.ExecuteScaler(PM, "EgCheckPDAccountMapping"));
        }

        public int VerifyMultipleOfficeId(string MultipleOfficesDt)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@xml", SqlDbType.Xml) { Value = MultipleOfficesDt };
            PM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DepartmentCode };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Char , 4) { Value = TreasuryCode };
            return Convert.ToInt32(gf.ExecuteScaler(PM, "EgCheckMultipleOfficeIdExisting"));
        }
        public int CheckMultiOfcHeads(string officeXml, DataTable Checkdt)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@mytable", SqlDbType.Structured) { Value = Checkdt };
            PM[1] = new SqlParameter("@xml", SqlDbType.Xml) { Value = officeXml };
            DataTable dt1 = new DataTable();
            dt1 = gf.Filldatatablevalue(PM, "EgCheckMultiOfcHead_Multi", dt1, null);
            return Convert.ToInt32(dt1.Rows[0][0]);
        }
        public int CheckOfficeAmountMismatch(string MultipleOfficesDt, double BudgetHeadAmt)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@xml", SqlDbType.Xml) { Value = MultipleOfficesDt };
            PM[1] = new SqlParameter("@TtlBudgetHeadAmt", SqlDbType.Money) { Value = BudgetHeadAmt };
            return Convert.ToInt32(gf.ExecuteScaler(PM, "EgCheckAmountMismatch_Multi"));
        }
        public bool CheckPDRunMode()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@PDAcc", SqlDbType.Int) { Value = PDAcc };
            return Convert.ToBoolean(gf.ExecuteScaler(PM, "EgCheckPDRunMode"));
        }

        /// <summary>
        ///  Check  Merchant Security  for Interation  PineLab to Egras
        ///  12 Feb 2020
        /// </summary>
        /// <returns></returns>
        public int GetTrafficMerchantinfo()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@Mcode", SqlDbType.Int) { Value = Mcode };
            PM[1] = new SqlParameter("@AUIN", SqlDbType.VarChar, 50) { Value = AUIN };
            PM[2] = new SqlParameter("@EncData", SqlDbType.NVarChar, 3000) { Value = EncData };
            return Convert.ToInt32(gf.ExecuteScaler(PM, "EgGetTrafficMerchantinfo"));
        }

        
        /// <summary>
        ///  Check  Merchant Security  for SSP Integration merchnat 5018
        /// 29072022
        /// </summary>
        /// <returns></returns>
        public int GetSSPMerchantinfo()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@Mcode", SqlDbType.Int) { Value = Mcode };
            PM[1] = new SqlParameter("@AUIN", SqlDbType.VarChar, 50) { Value = AUIN };
            PM[2] = new SqlParameter("@EncData", SqlDbType.NVarChar, 3000) { Value = EncData };
            return Convert.ToInt32(gf.ExecuteScaler(PM, "EgGetSSPMerchantinfo"));
        }



        /// <summary>
        /// checks multiple heads exists in service table  or mapped to respective deaprtment
        /// </summary>
        /// <param name="Checkdt"></param>
        /// <returns></returns>
        public DataTable GetMultipleBudgetHeadStatus(DataTable Checkdt)
        {
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@mytable", SqlDbType.Structured) { Value = Checkdt };
            PM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DepartmentCode };
            gf = new GenralFunction();
            DataTable dt1 = new DataTable();
            dt1 = gf.Filldatatablevalue(PM, "EgCheckMultipleBudgetHeadMapping", dt1, null);
            return dt1;

        }


        /// <summary>
        /// Get Data Table From XML String
        /// </summary>
        /// <returns></returns>
        public DataTable GetXmlDatatable(string MultipleOfficesDt)
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@XML", SqlDbType.Xml) { Value = MultipleOfficesDt };
            gf.Filldatatablevalue(PM, "EgGetXmlDatatable", dt, null);
            return dt;

        }
        /// <summary>
        /// VArify DDO Code 
        /// </summary>
        /// <returns>1,0</returns>
        public int VarifyDDoCode()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PM[1] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeId };
            PM[2] = new SqlParameter("@DdoCode", SqlDbType.Char, 6) { Value = DdoCode };
            //return Convert.ToInt32(gf.ExecuteScaler(PM, "EgCheckOfficeIdExisting"));
            string result = gf.ExecuteScaler(PM, "EgCheckDDOExisting");
            int returnVal;
            bool isNumeric = int.TryParse(result, out returnVal);
            if (isNumeric)
                return returnVal;
            else
                return 0;
        }
        #endregion
    }
}

