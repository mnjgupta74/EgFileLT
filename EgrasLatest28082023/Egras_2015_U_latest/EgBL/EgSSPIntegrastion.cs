using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace EgBL
{
    public class EgSSPIntegrastion
    {
        public string MerchantCode { get; set; }
        public string Paymenttype { get; set; }
        public string Identity { get; set; }
        public string Location { get; set; }
        public string FullName { get; set; }
        public string ObjectHead { get; set; }
        public string VNC { get; set; }
        public string PNP { get; set; }
        public string DistrictCode { get; set; }
        public string OfficeName { get; set; }
        public string ddo { get; set; }
        public string DeductCommission { get; set; }
        public string ChallanFromMonth { get; set; }
        public string ChallanToMonth { get; set; }
        public string Address { get; set; }
        public string PINCode { get; set; }
        public string City { get; set; }
        public string Remarks { get; set; }
        public string RefNumber { get; set; }
        public string TotalAmount { get; set; }
        public string ChallanYear { get; set; }
        public string Filler { get; set; }
        public string DeptCode { get; set; }
        public string Profile { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public string Auth { get; set; }
        public string type { get; set; }
        public string key { get; set; }
        public string token { get; set; }
        public string enctype { get; set; }
        public double HeadTotalAmount { get; set; }
        public DataTable dtCheckHeads { get; set; }
        public string ChallanType { get; set; }
        public string BankName { get; set; }
        public string ScheCode { get; set; }
        public int Id { get; set; }
        public string statusCode { get; set; }
        string[][] ChallanValues;


        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();

        public string GenerateGRN()
        {
            string SCode = string.Empty;
            statusCode = "101";
            try
            {
                //=======================================
                if (string.IsNullOrEmpty(Auth))
                    return "Header Can Not be Blank !";//Header Blank

                if (string.IsNullOrEmpty(enctype))
                    return "Parameter Can Not be Blank !";//  Parameter Empty

                token = Auth.Split('|')[0];
                if (string.IsNullOrEmpty(token))
                    return "Token Not Found !";// Get token 

                key = Auth.Split('|')[1];
                if (string.IsNullOrEmpty(key))
                    return "Merchant code Not Found !";// Get Merchant Key
                if (key.Trim() != "5025")
                {
                    SCode = VerifyToken();
                    if (SCode != "200T")
                        return "Token is Invalid !";
                }

               
                var PlainString = objDecrypt.DecryptAES256(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key", null);
               
                try
                {

                    if (CheckIntegrationData(PlainString))
                    {
                        var ret = InsertChallan();
                        if (ret > 0)
                        {
                            statusCode = "200";
                            return "GRN=" + ret;
                        }
                    }

                }
                catch (Exception ex)
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.Message);
                    return ex.Message; ;
                }
                return "GRN Not Generated !";
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message);
                return "Some Technical Error !";
            }
        }
        public string VerifyToken()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.VarChar, 8) { Value = key };
            string result = gf.ExecuteScaler(PARM, "EgSNATokenVerify");
            return result;
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
            CheckOfcTreasDdoMapping();

            CheckBudgetHeadConditions("ME");
            CheckAmount();
            checkFiller();
            CheckBsrcodeWithTreasury();
            return true;
        }
        public void checkAUIN()
        {
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            string msg = string.Empty;
            //if (AUIN != Data["RefNumber"].Value.ToString())
            //{
            //    throw new Exception("AUIN Number in plaintext is not matching with post AUIN number.");
            //}
            if (!IsAlphaNumeric(RefNumber))
            {
                throw new Exception("AUIN Number Should Be Alphanumeric.");
            }

            objEgCheckBudgetHead.Mcode = Convert.ToInt32(MerchantCode);
            objEgCheckBudgetHead.AUIN = RefNumber;
            objEgCheckBudgetHead.EncData = enctype;

            var val = objEgCheckBudgetHead.GetSSPMerchantinfo();
            if (val == 0)
            {
                throw new Exception("Duplicate AUIN number OR Merchant Code Not Mapped !");
            }

        }
        public bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        public void CheckMerchantCode()
        {
            if (MerchantCode != key)
            {
                throw new Exception("Merchant Code in PlainText  Does  Not Match with Request  Merchant Code ");
            }
        }
        public virtual void CheckOfcTreasDeptMapping()
        {
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            objEgCheckBudgetHead.TreasuryCode = Location;
            objEgCheckBudgetHead.OfficeId = Convert.ToInt32(OfficeName);
            objEgCheckBudgetHead.DepartmentCode = Convert.ToInt32(DeptCode);
            if (objEgCheckBudgetHead.VarifyOfficeId() != 1)
            {
                throw new Exception("OfficeId Not Map With Treasury and Department ");
            }

        }

        public virtual void CheckOfcTreasDdoMapping()
        {
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            objEgCheckBudgetHead.TreasuryCode = Location;
            objEgCheckBudgetHead.OfficeId = Convert.ToInt32(OfficeName);
            objEgCheckBudgetHead.DdoCode = ddo;
            if (objEgCheckBudgetHead.VarifyDDoCode() != 1)
            {
                throw new Exception("OfficeId Not Map With Treasury and DDO Code ! ");
            }

        }
        public void CheckDiscountAmt()
        {
            if (Convert.ToDouble(TotalAmount) <= 0)
            {
                throw new Exception("Total Amount Cannot be Zero or less than Zero.");
            }
        }
        public void CheckBudgetHeadConditions(string ChallanType)
        {
            DataTable dt = new DataTable();
            GetBudgetHeadDt();
            if (!CheckBudgetHeadME())
                throw new Exception("Check BudgetHead");
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            objEgCheckBudgetHead.DepartmentCode = Convert.ToInt32(DeptCode);
            objEgCheckBudgetHead.ChallanType = ChallanType;
            dt = objEgCheckBudgetHead.GetBudgetHeadDetail(dtCheckHeads);
            //ScheCode = dt.Rows[0][0].ToString().Split('-')[1].ToString();
            ScheCode = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString().Split('-')[1].ToString() : "0";
            if (dt.Rows.Count <= 0)
                throw new Exception("DataTable not found");
        }
        private bool CheckBudgetHeadME()
        {
            EgEMinusChallanBL objEgEMinusChallanBL = new EgEMinusChallanBL();
            objEgEMinusChallanBL.BudgetHead = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(0, 13));
            objEgEMinusChallanBL.ObjectHead = Convert.ToString(ChallanValues[3][1].ToString().Trim());
            objEgEMinusChallanBL.PNP = Convert.ToString(ChallanValues[4][1].ToString().Trim());
            objEgEMinusChallanBL.VNC = Convert.ToString(ChallanValues[5][1].ToString().Trim());
            return objEgEMinusChallanBL.CheckBudgetHead() == 1 ? true : false;
        }
        public void CheckAmount()
        {
            if (HeadTotalAmount != Convert.ToDouble(TotalAmount))
            {
                throw new Exception("Amount mismatch.");
            }
        }
        /// <summary>
        /// Check Manual  Bank Treasury Mapping17 May 2022
        /// </summary>
        public void CheckBsrcodeWithTreasury()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@treasurycode", SqlDbType.Char, 4) { Value = Location };
            PARM[1] = new SqlParameter("@bsrcode", SqlDbType.Char, 4) { Value = BankName };
            string result = gf.ExecuteScaler(PARM, "EgMapTeasuryWithBSRCode");
            if (result.Trim() == "0")
            {
                throw new Exception("Treasury Does Not Map With  Bank !");
            }
        }
        protected void createTable()
        {
            dtCheckHeads = new DataTable();
            dtCheckHeads.Columns.Add(new DataColumn("SNo", Type.GetType("System.Int64")));
            dtCheckHeads.Columns.Add(new DataColumn("BudgetHead", Type.GetType("System.String")));
            dtCheckHeads.Columns.Add(new DataColumn("ScheCode", Type.GetType("System.String")));
            dtCheckHeads.Columns.Add(new DataColumn("Amount", System.Type.GetType("System.Double")));
        }
        public void GetBudgetHeadDt()
        {
            createTable();
            int sno = 1;
            double amount = 0;
            if (ChallanValues[1][1].ToString().Trim() != "0" && Convert.ToDouble(ChallanValues[2][1].ToString().Trim()) > 0)
            {
                DataRow dr = dtCheckHeads.NewRow();
                dr["SNo"] = Convert.ToInt32(sno);
                dr["BudgetHead"] = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(0, 13));
                dr["ScheCode"] = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(13, 5));
                dr["Amount"] = Convert.ToDouble(ChallanValues[2][1].ToString());
                amount = amount + Convert.ToDouble(ChallanValues[2][1].ToString());
                dtCheckHeads.Rows.Add(dr);
            }
            HeadTotalAmount = amount;
        }
        private void checkFiller()
        {
            if (Filler.ToString().Trim() == "A")
            {
                Filler = "0";
            }
            else
            {
                throw new Exception("Filler is not Valid.");
            }
        }
        public void SplitToProperties(string PlainText)
        {
            EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();

            ChallanValues = PlainText.Trim().Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(splitequal => splitequal.Split('=')).ToArray();
            string head = objEgDeptIntegrationPropBL.BudgetHead(ChallanValues[1][1].ToString()).ToString();
            MerchantCode = objEgDeptIntegrationPropBL.merchantCode(ChallanValues[8][1].ToString()).ToString();
            Paymenttype = objEgDeptIntegrationPropBL.pType(ChallanValues[9][1].ToString());
            Identity = objEgDeptIntegrationPropBL.identity(ChallanValues[10][1].ToString());
            Location = objEgDeptIntegrationPropBL.location(ChallanValues[11][1].ToString());
            FullName = objEgDeptIntegrationPropBL.fullName(ChallanValues[6][1].ToString());
            ObjectHead = ChallanValues[3][1];
            VNC = ChallanValues[5][1];
            PNP = ChallanValues[4][1];
            DistrictCode = objEgDeptIntegrationPropBL.districtCode(ChallanValues[12][1].ToString()).ToString();
            OfficeName = objEgDeptIntegrationPropBL.officeCode(ChallanValues[13][1].ToString(), false).ToString();
            ddo = objEgDeptIntegrationPropBL.officeCode(ChallanValues[24][1].ToString(), false).ToString();
            DeductCommission = "0.00";
            ChallanFromMonth = Convert.ToDateTime(ChallanValues[15][1].ToString()).ToString();
            ChallanToMonth = Convert.ToDateTime((ChallanValues[16][1].ToString())).ToString();
            Address = objEgDeptIntegrationPropBL.address(ChallanValues[17][1].ToString());
            PINCode = objEgDeptIntegrationPropBL.pincode(ChallanValues[18][1].ToString());
            City = objEgDeptIntegrationPropBL.city(ChallanValues[19][1].ToString());
            Remarks = objEgDeptIntegrationPropBL.remarks(ChallanValues[20][1].ToString());
            RefNumber = objEgDeptIntegrationPropBL.refNo(ChallanValues[0][1].ToString());
            TotalAmount = objEgDeptIntegrationPropBL.amount(ChallanValues[7][1].ToString()).ToString();
            ChallanYear = objEgDeptIntegrationPropBL.ChallanYear(ChallanValues[22][1].ToString());
            Filler = objEgDeptIntegrationPropBL.filler(ChallanValues[21][1].ToString());
            BankName = objEgDeptIntegrationPropBL.BankName(ChallanValues[23][1].ToString());
            DeptCode = objEgDeptIntegrationPropBL.departmentCode(ChallanValues[14][1].ToString()).ToString();
            Profile = "0";
            UserId = "73";
            IpAddress = HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
            //System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();


            if (Paymenttype.ToString().Trim() != "M")
                throw new Exception("Online Payment Mode Not Allowed.");
        }
        /// <summary>
        /// create insert method
        /// </summary>
        /// <param name="PM"></param>
        /// <returns></returns>
        public int InsertChallan()
        {
            int Rv = -1;
            try
            {
                EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[30];
                objEgDeptIntegrationPropBL.districtCode(ChallanValues[12][1].ToString());

                PM[0] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = MerchantCode };
                PM[1] = new SqlParameter("@Paymenttype", SqlDbType.Char, 1) { Value = Paymenttype };
                PM[2] = new SqlParameter("@Identity", SqlDbType.VarChar, 15) { Value = Identity };
                PM[3] = new SqlParameter("@Location", SqlDbType.Char, 4) { Value = Location };
                PM[4] = new SqlParameter("@FullName", SqlDbType.VarChar, 50) { Value = FullName };
                PM[5] = new SqlParameter("@OfficeName", SqlDbType.Int) { Value = OfficeName };
                PM[6] = new SqlParameter("@ChallanFromMonth", SqlDbType.SmallDateTime, 4) { Value = ChallanFromMonth };
                PM[7] = new SqlParameter("@ChallanToMonth", SqlDbType.SmallDateTime, 4) { Value = ChallanToMonth };
                PM[8] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = objEgDeptIntegrationPropBL.address(Address) };
                PM[9] = new SqlParameter("@PINCode", SqlDbType.Char, 6) { Value = objEgDeptIntegrationPropBL.pincode(PINCode) };
                PM[10] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = objEgDeptIntegrationPropBL.city(City) };
                PM[11] = new SqlParameter("@Remarks", SqlDbType.VarChar, 200) { Value = objEgDeptIntegrationPropBL.remarks(Remarks) };
                PM[12] = new SqlParameter("@RefNumber", SqlDbType.VarChar, 50) { Value = objEgDeptIntegrationPropBL.refNo(RefNumber) };
                PM[13] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = objEgDeptIntegrationPropBL.amount(TotalAmount) };
                PM[14] = new SqlParameter("@ChallanYear", SqlDbType.Char, 4) { Value = objEgDeptIntegrationPropBL.ChallanYear(ChallanYear) };
                PM[15] = new SqlParameter("@Profile", SqlDbType.Int) { Value = 0 };
                PM[16] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
                PM[17] = new SqlParameter("@BankName", SqlDbType.Char, 7) { Value = objEgDeptIntegrationPropBL.BankName(BankName) };
                PM[18] = new SqlParameter("@deptcode", SqlDbType.Int) { Value = Convert.ToInt16(DeptCode) };
                PM[19] = new SqlParameter("@BudgetHead", SqlDbType.Char, 20) { Value = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(0, 13).Trim()) };
                PM[20] = new SqlParameter("@BudGetHeadAmount", SqlDbType.Money) { Value = Convert.ToDouble(ChallanValues[2][1].ToString()) };
                //PM[21] = new SqlParameter("@ScheCode", SqlDbType.Char, 5) { Value = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(13, 5).Trim()) };
                PM[21] = new SqlParameter("@ScheCode", SqlDbType.Char, 5) { Value = ScheCode };
                PM[22] = new SqlParameter("@IpAddress", SqlDbType.VarChar, 20) { Value = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString() };
                PM[23] = new SqlParameter("@DeductCommission", SqlDbType.Money) { Value = DeductCommission };
                PM[24] = new SqlParameter("@VNC", SqlDbType.Char, 1) { Value = VNC };
                PM[25] = new SqlParameter("@PNP", SqlDbType.Char, 1) { Value = PNP };
                PM[26] = new SqlParameter("@ddo", SqlDbType.Int) { Value = Convert.ToInt32(ddo) };
                PM[27] = new SqlParameter("@filler", SqlDbType.Int) { Value = Convert.ToInt32(Filler) };
                PM[28] = new SqlParameter("@ObjectHead", SqlDbType.Char, 2) { Value = ObjectHead };
                //PM[29] = new SqlParameter("@DistrictCode", SqlDbType.NVarChar, 50) { Value = DistrictCode };
                PM[29] = new SqlParameter("@Id", SqlDbType.BigInt) { Value = Id };
                PM[29].Direction = ParameterDirection.Output;

                Rv = gf.UpdateData(PM, "EgEChallan_SSP");

                if (Rv == 0)
                {
                    Rv = -1;
                }
                else
                {
                    Id = Convert.ToInt32(PM[29].Value);
                    Rv = int.Parse(PM[29].Value.ToString());
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return Rv;
        }
    }
}
