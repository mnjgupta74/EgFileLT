using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace EgBL
{
     public class EgETrafficBL
    {
        string PlainText;
        string[][] ChallanValues;
        public int DeptCode { get; set; }
        public string AUIN { get; set; }
        public int Mcode { get; set; }
        public string EncData { get; set; }
        public string ScheCode { get; set; }
        public string BudgetHead { get; set; }
        public string TreasuryCode { get; set; }
        public int OfficeId { get; set; }
        public int DepartmentCode { get; set; }
        public object Id { get; private set; }
        GenralFunction gf;

        /// <summary>
        /// Start calling
        /// </summary>
        /// <returns></returns>
        public Int64 IntegrationInterface()
        {
            try
            {
                SbiEncryptionDecryption objEncryptDecrypt = new SbiEncryptionDecryption();
                // PlainText = objEncryptDecrypt.Decrypt(EncData, HttpContext.Current.Server.MapPath("~/WebPages/Key/5008.key"));
                PlainText = objEncryptDecrypt.Decrypt(EncData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "5008.key");
               
                bool result = CheckIntegrationData(PlainText);
                Int64 grn = -1;
                if (result)
                {
                    grn = InsertEge_TrafficDynamicChallan();
                }
                return grn;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check Integration
        /// </summary>
        /// <param name="PlainText"></param>
        /// <returns></returns>
        public bool CheckIntegrationData(string PlainText)
        {
            SplitToProperties(PlainText);
            CheckMerchantCode();
            checkAUIN();
            CheckOfcTreasDeptMapping();
            CheckAmount();
            PaymentType();
            return true;
        }

        /// <summary>
        /// split all the properties
        /// </summary>
        /// <param name="PlainText"></param>
        public void SplitToProperties(string PlainText)
        {
            ChallanValues = PlainText.Trim().Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(splitequal => splitequal.Split('=')).ToArray();
        }
        public void CheckMerchantCode()
        {

            if (Mcode != Convert.ToInt32(ChallanValues[22][1].ToString()))
            {
                throw new Exception("Merchant Code in plaintext is not matching with post Merchant Code.");
            }
        }
        public void PaymentType()
        {

            if ("N" != ChallanValues[23][1].ToString())
            {
                throw new Exception("Payment Type Should be Online(N)");
            }
        }
        // Amount Checking Head Amount And Total Amount Should be  same
        public void CheckAmount()
        {

            if (Convert.ToDouble(ChallanValues[21][1].ToString()) != Convert.ToDouble(ChallanValues[2][1].ToString()))
            {
                throw new Exception("Total Amount And Head Amount MisMatch.");
            }
        }


        public void checkAUIN()
        {

            if (AUIN != ChallanValues[0][1].ToString())
            {
                throw new Exception("AUIN Number in plaintext is not matching with post AUIN number.");
            }
        }
        /// <summary>
        /// Check Office Treasury and Department mapping and budgethead and schecode
        /// </summary>
        public void CheckOfcTreasDeptMapping()
        {
            try
            {
                EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();
                TreasuryCode = objEgDeptIntegrationPropBL.location(ChallanValues[25][1].ToString());
                OfficeId = objEgDeptIntegrationPropBL.officeCode(ChallanValues[27][1].ToString(), false);
                BudgetHead = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(0, 13));
                ScheCode = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(13, 5));
                DeptCode = objEgDeptIntegrationPropBL.departmentCode(ChallanValues[28][1].ToString());
                int var = VarifyOfficeId();
                if (var == 3)
                {
                    throw new Exception("OfficeId not map with Treasury and Department Integration");
                }
                if (var == 2)
                {
                    throw new Exception("Mapping Issue With BudgetHead OR Sche Code!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
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
        /// create insert method
        /// </summary>
        /// <param name="PM"></param>
        /// <returns></returns>
        public int InsertEge_TrafficDynamicChallan()
        {
            int Rv = -1;
            try
            {
                EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[24];
                objEgDeptIntegrationPropBL.districtCode(ChallanValues[26][1].ToString());

                PM[0] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = objEgDeptIntegrationPropBL.merchantCode(ChallanValues[22][1].ToString()) };
                PM[1] = new SqlParameter("@Paymenttype", SqlDbType.Char, 1) { Value = objEgDeptIntegrationPropBL.pType(ChallanValues[23][1].ToString()) };
                PM[2] = new SqlParameter("@Identity", SqlDbType.VarChar, 15) { Value = objEgDeptIntegrationPropBL.RegistrationNo(ChallanValues[24][1].ToString()) };
                PM[3] = new SqlParameter("@Location", SqlDbType.Char, 4) { Value = TreasuryCode };
                PM[4] = new SqlParameter("@FullName", SqlDbType.VarChar, 50) { Value = objEgDeptIntegrationPropBL.fullName(ChallanValues[19][1].ToString()) };
                PM[5] = new SqlParameter("@OfficeName", SqlDbType.Int) { Value = OfficeId };
                PM[6] = new SqlParameter("@ChallanFromMonth", SqlDbType.SmallDateTime, 4) { Value = Convert.ToDateTime(ChallanValues[29][1].ToString()) };
                PM[7] = new SqlParameter("@ChallanToMonth", SqlDbType.SmallDateTime, 4) { Value = Convert.ToDateTime((ChallanValues[30][1].ToString())) };
                PM[8] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = objEgDeptIntegrationPropBL.address(ChallanValues[31][1].ToString()) };
                PM[9] = new SqlParameter("@PINCode", SqlDbType.Char, 6) { Value = objEgDeptIntegrationPropBL.pincode(ChallanValues[32][1].ToString()) };
                PM[10] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = objEgDeptIntegrationPropBL.city(ChallanValues[33][1].ToString()) };
                PM[11] = new SqlParameter("@Remarks", SqlDbType.VarChar, 200) { Value = objEgDeptIntegrationPropBL.remarks(ChallanValues[34][1].ToString()) };
                PM[12] = new SqlParameter("@RefNumber", SqlDbType.VarChar, 50) { Value = objEgDeptIntegrationPropBL.refNo(ChallanValues[0][1].ToString()) };
                PM[13] = new SqlParameter("@TotalAmount", SqlDbType.VarChar, 8) { Value = objEgDeptIntegrationPropBL.amount(ChallanValues[21][1].ToString()) };
                PM[14] = new SqlParameter("@ChallanYear", SqlDbType.Char, 4) { Value = objEgDeptIntegrationPropBL.ChallanYear(ChallanValues[36][1].ToString()) };
                PM[15] = new SqlParameter("@Profile", SqlDbType.Int) { Value = 0 };
                PM[16] = new SqlParameter("@UserId", SqlDbType.Int) { Value = System.Web.HttpContext.Current.Session["UserId"] };
                PM[17] = new SqlParameter("@BankName", SqlDbType.Char, 7) { Value = "9920001" };
                PM[18] = new SqlParameter("@deptcode", SqlDbType.Int) { Value = Convert.ToInt16(ChallanValues[28][1].ToString().Trim()) };
                PM[19] = new SqlParameter("@BudgetHead", SqlDbType.Char, 20) { Value = Convert.ToString(BudgetHead.Trim()) };
                PM[20] = new SqlParameter("@BudGetHeadAmount", SqlDbType.Money) { Value = Convert.ToDouble(ChallanValues[2][1].ToString()) };
                PM[21] = new SqlParameter("@ScheCode", SqlDbType.Char, 5) { Value = Convert.ToString(ScheCode.Trim()) };
                PM[22] = new SqlParameter("@IpAddress", SqlDbType.NVarChar, 50) { Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() };
                PM[23] = new SqlParameter("@Id", SqlDbType.Int) { Value = Id };
                PM[23].Direction = ParameterDirection.Output;

                Rv = gf.UpdateData(PM, "EgEChallan_Traffic");

                if (Rv == 0)
                {
                    Rv = -1;
                }
                else
                {
                    Id = Convert.ToInt32(PM[23].Value);
                    Rv = int.Parse(PM[23].Value.ToString());
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return Rv;
        }
        /// <summary>
        /// VArify Office Id and budgethead
        /// </summary>
        /// <returns>1,0</returns>
        public int VarifyOfficeId()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PM[1] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeId };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[3] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PM[4] = new SqlParameter("@ScheCode", SqlDbType.Char, 5) { Value = ScheCode };
            string result = gf.ExecuteScaler(PM, "[EgCheckBudgetheadAndOfficeMapping_eTraffic]");
            int returnVal;
            bool isNumeric = int.TryParse(result, out returnVal);
            if (isNumeric)
                return returnVal;
            else
                return 0;
        }
    }
}
