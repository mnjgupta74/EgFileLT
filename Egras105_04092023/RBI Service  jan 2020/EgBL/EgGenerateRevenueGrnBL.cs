using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace EgBL
{
    public class EgGenerateRevenueGrnBL
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
        public int DivCode { get; set; }
        public int Id { get; set; }
        public int typeFlag { get; set; }
        public DataTable dtSchema { get; set; }
        string[][] ChallanValues;


        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();

        public string GenerateGRN()
        {
            string SCode = string.Empty;
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
                    if (SCode == "200T" && typeFlag == 2)
                        return "200T";
                }
                var PlainString = objDecrypt.DecryptSBIWithKey256(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key", null);

                try
                {

                    if (CheckIntegrationData(PlainString))
                    {
                        var ret = InsertChallan();
                        if (ret > 0)
                            return "GRN=" + ret;
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
            //if (MerchantCode == "5025")
            //{
            SplitToProperties(PlainText);

            CheckMerchantCode();
            //checkAUIN();
            if (Filler != "A" && !Filler.Contains("PD"))
                CheckDivisionCode();
            CheckOfcTreasDeptMapping();
            //CheckOfcTreasDdoMapping();
            CheckDiscountAmt();
            CheckBudgetHeadConditions();
            CheckAmount();
            checkFiller();
            CheckBsrcodeWithTreasury();
            getSchemadt();
            return true;
        }
        public void checkAUIN()
        {
            try
            {
                EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
                string msg = string.Empty;

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
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message);
                throw new Exception("Some Technical Error in AUIN NO !");
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

        /// <summary>
        /// Check Division Code Exist Or Not if filler is passed with office value
        /// </summary>
        public void CheckDivisionCode()
        {
            try { 
            EgIntegrationChallanBL objEgIntegrationChallanBL = new EgBL.EgIntegrationChallanBL();
            objEgIntegrationChallanBL.Location = Location;
            objEgIntegrationChallanBL.filler = Filler;
            int DivCode = objEgIntegrationChallanBL.GetDivisionCode();
            //Data.Add("DivCode", new IntegrationProp() { Value = DivCode, ParameterName = "@DivCode", DbType = SqlDbType.Int, Size = 4 });
            this.DivCode = DivCode;
            if (DivCode == 0)
            {
                throw new Exception("Division code Not exist For particular office");
            }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message);
                throw new Exception("Some Technical Error in Divison NO !");
            }
        }
        public void CheckOfcTreasDeptMapping()
        {
            try { 
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            objEgCheckBudgetHead.TreasuryCode = Location;
            objEgCheckBudgetHead.OfficeId = Convert.ToInt32(OfficeName);
            objEgCheckBudgetHead.DepartmentCode = Convert.ToInt32(DeptCode);
            if (objEgCheckBudgetHead.VarifyOfficeId() != 1)
            {
                throw new Exception("OfficeId Not Map With Treasury and Department ");
                }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message);
                throw new Exception("Some Technical Error in Treasury Department Mapping !");
            }
        }

        public void CheckOfcTreasDdoMapping()
        {
            try { 
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            objEgCheckBudgetHead.TreasuryCode = Location;
            objEgCheckBudgetHead.OfficeId = Convert.ToInt32(OfficeName);
            objEgCheckBudgetHead.DdoCode = ddo;
            if (objEgCheckBudgetHead.VarifyDDoCode() != 1)
            {
                throw new Exception("OfficeId Not Map With Treasury and DDO Code ! ");
            }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message);
                throw new Exception("Some Technical Error in Office Treasury DDO Mapping !");
            }
        }
        public void CheckDiscountAmt()
        {
            if (Convert.ToDouble(DeductCommission) > Convert.ToDouble(TotalAmount))
            {
                throw new Exception("Discount can not be greater than Total Amount");
            }
        }
        public void CheckBudgetHeadConditions()
        {
            try { 
            DataTable dt = new DataTable();
            GetBudgetHeadDt();
            HeadsCondition();
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            objEgCheckBudgetHead.DepartmentCode = Convert.ToInt16(DeptCode);
            objEgCheckBudgetHead.ChallanType = ChallanType;
            dt = objEgCheckBudgetHead.GetBudgetHeadDetail(dtCheckHeads);
            if (dt.Rows.Count <= 0)
                throw new Exception("DataTable not found");
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message);
                throw new Exception("Some Technical Error in Budgethead Conditions !");
            }
        }
        //private bool CheckBudgetHeadME()
        //{
        //    EgEMinusChallanBL objEgEMinusChallanBL = new EgEMinusChallanBL();
        //    objEgEMinusChallanBL.BudgetHead = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(0, 13));
        //    objEgEMinusChallanBL.ObjectHead = Convert.ToString(ChallanValues[3][1].ToString().Trim());
        //    objEgEMinusChallanBL.PNP = Convert.ToString(ChallanValues[4][1].ToString().Trim());
        //    objEgEMinusChallanBL.VNC = Convert.ToString(ChallanValues[5][1].ToString().Trim());
        //    return objEgEMinusChallanBL.CheckBudgetHead() == 1 ? true : false;
        //}
        public void CheckAmount()
        {
            if (HeadTotalAmount - Convert.ToDouble(DeductCommission) != Convert.ToDouble(TotalAmount))
            {
                throw new Exception("Amount mismatch.");
            }
        }
        /// <summary>
        /// Check Manual  Bank Treasury Mapping17 May 2022
        /// </summary>
        public void CheckBsrcodeWithTreasury()
        {
            try { 
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@bsrcode", SqlDbType.Char, 4) { Value = BankName };
            PARM[1] = new SqlParameter("@paymenttype", SqlDbType.Char, 1) { Value = Paymenttype };
            string result = gf.ExecuteScaler(PARM, "EgCheckBSRCodeExists");
            if (result.Trim() == "0")
            {
                throw new Exception("Bank Not Valid !");
            }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message);
                throw new Exception("Some Technical Error in BSR Code !");
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
            try
            {
                for (int i = 1; i <= 17; i += 2)
                {
                    if (ChallanValues[i][1].ToString().Trim() != "0" && Convert.ToDouble(ChallanValues[i + 1][1].ToString().Trim()) > 0 && Convert.ToDouble(ChallanValues[i + 1][1]) > 0)
                    {
                        DataRow dr = dtCheckHeads.NewRow();
                        dr["SNo"] = Convert.ToInt32(sno);
                        dr["BudgetHead"] = Convert.ToString(ChallanValues[i][1].ToString().Trim().Substring(0, 13));
                        dr["ScheCode"] = Convert.ToString(ChallanValues[i][1].ToString().Trim().Substring(13, 5));
                        dr["Amount"] = Convert.ToDouble(ChallanValues[i + 1][1].ToString());
                        amount = amount + Convert.ToDouble(ChallanValues[i + 1][1].ToString());
                        dtCheckHeads.Rows.Add(dr);
                        sno++;
                    }
                }
                Convert.ToInt32(ChallanValues[1][1].ToString().Trim().Substring(0, 4));
            }
            catch (FormatException ef)
            {
                throw new Exception("Format of Budget Head or Amount is invalid");
            }
            HeadTotalAmount = amount;

        }


        private void checkFiller()
        {
            if (Filler.ToString().Trim() == "A")
            {
                Filler = "0";
            }
        }
        public void SplitToProperties(string PlainText)
        {
            
            if (PlainText.Contains("PD:") || PlainText.Contains("ObjectHead="))
            {
                throw new Exception("Incomplete data in Filler.");
            }

            EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();

            ChallanValues = PlainText.Trim().Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(splitequal => splitequal.Split('=')).ToArray();

            RefNumber = objEgDeptIntegrationPropBL.refNo(ChallanValues[0][1].ToString());
            FullName = objEgDeptIntegrationPropBL.fullName(ChallanValues[19][1].ToString());
            DeductCommission = objEgDeptIntegrationPropBL.deductCommission(ChallanValues[20][1].ToString()).ToString();
            TotalAmount = objEgDeptIntegrationPropBL.amount(ChallanValues[21][1].ToString()).ToString();
            MerchantCode = objEgDeptIntegrationPropBL.merchantCode(ChallanValues[22][1].ToString()).ToString();
            Paymenttype = objEgDeptIntegrationPropBL.pType(ChallanValues[23][1].ToString());
            Identity = objEgDeptIntegrationPropBL.identity(ChallanValues[24][1].ToString());
            Location = objEgDeptIntegrationPropBL.location(ChallanValues[25][1].ToString());
            DistrictCode = objEgDeptIntegrationPropBL.districtCode(ChallanValues[26][1].ToString()).ToString();
            OfficeName = objEgDeptIntegrationPropBL.officeCode(ChallanValues[27][1].ToString(), false).ToString();
            DeptCode = objEgDeptIntegrationPropBL.departmentCode(ChallanValues[28][1].ToString()).ToString();
            ChallanFromMonth = Convert.ToDateTime(ChallanValues[29][1].ToString()).ToString();
            ChallanToMonth = Convert.ToDateTime((ChallanValues[30][1].ToString())).ToString();
            Address = objEgDeptIntegrationPropBL.address(ChallanValues[31][1].ToString());
            PINCode = objEgDeptIntegrationPropBL.pincode(ChallanValues[32][1].ToString());
            City = objEgDeptIntegrationPropBL.city(ChallanValues[33][1].ToString());
            Remarks = objEgDeptIntegrationPropBL.remarks(ChallanValues[34][1].ToString());
            Filler = objEgDeptIntegrationPropBL.filler(ChallanValues[35][1].ToString());
            ChallanYear = objEgDeptIntegrationPropBL.ChallanYear(ChallanValues[36][1].ToString());
            BankName = objEgDeptIntegrationPropBL.BankName(ChallanValues[37][1].ToString());
            Profile = "0";
            UserId = "73";
            IpAddress = HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
            
        }


        private void getSchemadt()
        {
            DataTable schemaAmtTable = new DataTable();
            schemaAmtTable.Columns.Add(new DataColumn("DeptCode", System.Type.GetType("System.Int32")));
            schemaAmtTable.Columns.Add(new DataColumn("ScheCode", System.Type.GetType("System.Int32")));
            schemaAmtTable.Columns.Add(new DataColumn("amount", System.Type.GetType("System.Double")));
            schemaAmtTable.Columns.Add(new DataColumn("UserId", System.Type.GetType("System.Int32")));
            schemaAmtTable.Columns.Add(new DataColumn("BudgetHead", System.Type.GetType("System.String")));

            for (int i = 0; i < dtCheckHeads.Rows.Count; i++)
            {
                DataRow dtRow;
                dtRow = schemaAmtTable.NewRow();
                dtRow[0] = DeptCode;
                dtRow[1] = Convert.ToInt32(dtCheckHeads.Rows[i][2]);
                dtRow[2] = Convert.ToDouble(dtCheckHeads.Rows[i][3]);
                dtRow[3] = Convert.ToInt32(UserId);
                dtRow[4] = dtCheckHeads.Rows[i][1].ToString();
                schemaAmtTable.Rows.Add(dtRow);
                schemaAmtTable.AcceptChanges();
            }

            dtSchema = schemaAmtTable;
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
                //    GenralFunction gf = new GenralFunction();
                //    SqlParameter[] PM = new SqlParameter[29];
                //    objEgDeptIntegrationPropBL.districtCode(ChallanValues[26][1].ToString());

                //    PM[0] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = MerchantCode };
                //    PM[1] = new SqlParameter("@Paymenttype", SqlDbType.Char, 1) { Value = Paymenttype };
                //    PM[2] = new SqlParameter("@Identity", SqlDbType.VarChar, 15) { Value = Identity };
                //    PM[3] = new SqlParameter("@Location", SqlDbType.Char, 4) { Value = Location };
                //    PM[4] = new SqlParameter("@FullName", SqlDbType.VarChar, 50) { Value = FullName };
                //    PM[5] = new SqlParameter("@OfficeName", SqlDbType.Int) { Value = OfficeName };
                //    PM[6] = new SqlParameter("@ChallanFromMonth", SqlDbType.SmallDateTime, 4) { Value = ChallanFromMonth };
                //    PM[7] = new SqlParameter("@ChallanToMonth", SqlDbType.SmallDateTime, 4) { Value = ChallanToMonth };
                //    PM[8] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = objEgDeptIntegrationPropBL.address(Address) };
                //    PM[9] = new SqlParameter("@PINCode", SqlDbType.Char, 6) { Value = objEgDeptIntegrationPropBL.pincode(PINCode) };
                //    PM[10] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = objEgDeptIntegrationPropBL.city(City) };
                //    PM[11] = new SqlParameter("@Remarks", SqlDbType.VarChar, 200) { Value = objEgDeptIntegrationPropBL.remarks(Remarks) };
                //    PM[12] = new SqlParameter("@RefNumber", SqlDbType.VarChar, 50) { Value = objEgDeptIntegrationPropBL.refNo(RefNumber) };
                //    PM[13] = new SqlParameter("@TotalAmount", SqlDbType.VarChar, 8) { Value = objEgDeptIntegrationPropBL.amount(TotalAmount) };
                //    PM[14] = new SqlParameter("@ChallanYear", SqlDbType.Char, 4) { Value = objEgDeptIntegrationPropBL.ChallanYear(ChallanYear) };
                //    PM[15] = new SqlParameter("@Profile", SqlDbType.Int) { Value = 0 };
                //    PM[16] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
                //    PM[17] = new SqlParameter("@BankName", SqlDbType.Char, 7) { Value = objEgDeptIntegrationPropBL.BankName(BankName) };
                //    //PM[18] = new SqlParameter("@deptcode", SqlDbType.Int) { Value = Convert.ToInt16(DeptCode) };
                //    //PM[19] = new SqlParameter("@BudgetHead", SqlDbType.Char, 20) { Value = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(0, 13).Trim()) };
                //    //PM[20] = new SqlParameter("@BudGetHeadAmount", SqlDbType.Money) { Value = Convert.ToDouble(ChallanValues[2][1].ToString()) };
                //    //PM[21] = new SqlParameter("@ScheCode", SqlDbType.Char, 5) { Value = ScheCode };
                //    PM[22] = new SqlParameter("@IpAddress", SqlDbType.NVarChar, 50) { Value = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString() };
                //    PM[23] = new SqlParameter("@DeductCommission", SqlDbType.NVarChar, 10) { Value = DeductCommission };
                //    PM[24] = new SqlParameter("@DivCodeC", SqlDbType.Int) { Value = DivCode };
                //    PM[25] = new SqlParameter("@mytable", SqlDbType.Structured) { Value = dtSchema };
                //    PM[26] = new SqlParameter("@filler", SqlDbType.Int) { Value = Convert.ToInt32(Filler) };
                //    PM[27] = new SqlParameter("@DistrictCode", SqlDbType.NVarChar, 50) { Value = DistrictCode };
                //    PM[28] = new SqlParameter("@Id", SqlDbType.Int) { Value = Id };
                //    PM[28].Direction = ParameterDirection.Output;

                //    Rv = gf.UpdateData(PM, "EgEChallan");

                //    if (Rv == 0)
                //    {
                //        Rv = -1;
                //    }
                //    else
                //    {
                //        Id = Convert.ToInt32(PM[28].Value);
                //        Rv = int.Parse(PM[28].Value.ToString());
                //    }
                //}
                //catch (Exception ex) { throw new Exception(ex.Message); }
                //return Rv;

                GenralFunction gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[24];
                PARM[0] = new SqlParameter("@Identity", SqlDbType.VarChar, 15) { Value = Identity };
                PARM[1] = new SqlParameter("@OfficeName", SqlDbType.Int) { Value = OfficeName };
                PARM[2] = new SqlParameter("@Location", SqlDbType.Char, 4) { Value = Location };
                PARM[3] = new SqlParameter("@FullName", SqlDbType.VarChar, 50) { Value = FullName };
                PARM[4] = new SqlParameter("@ChallanYear", SqlDbType.Char, 4) { Value = objEgDeptIntegrationPropBL.ChallanYear(ChallanYear) };
                PARM[5] = new SqlParameter("@ChallanFromMonth", SqlDbType.SmallDateTime) { Value = ChallanFromMonth };
                PARM[6] = new SqlParameter("@ChallanToMonth", SqlDbType.SmallDateTime) { Value = ChallanToMonth };
                PARM[7] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = objEgDeptIntegrationPropBL.city(City) };
                PARM[8] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = objEgDeptIntegrationPropBL.address(Address) };
                PARM[9] = new SqlParameter("@PINCode", SqlDbType.Char, 6) { Value = objEgDeptIntegrationPropBL.pincode(PINCode) };
                PARM[10] = new SqlParameter("@DeductCommission", SqlDbType.Money) { Value = DeductCommission };
                PARM[11] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = objEgDeptIntegrationPropBL.amount(TotalAmount) };
                PARM[12] = new SqlParameter("@BankName", SqlDbType.Char, 7) { Value = objEgDeptIntegrationPropBL.BankName(BankName) };
                PARM[13] = new SqlParameter("@Paymenttype", SqlDbType.Char, 1) { Value = Paymenttype };
                PARM[14] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = UserId };
                PARM[15] = new SqlParameter("@Id", SqlDbType.Int) { Value = Id };
                PARM[15].Direction = ParameterDirection.Output;
                PARM[16] = new SqlParameter("@Remarks", SqlDbType.VarChar, 200) { Value = objEgDeptIntegrationPropBL.remarks(Remarks) };
                PARM[17] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = MerchantCode };
                PARM[18] = new SqlParameter("@RefNumber", SqlDbType.VarChar, 50) { Value = RefNumber };
                PARM[19] = new SqlParameter("@Profile", SqlDbType.Int) { Value = Profile };
                PARM[20] = new SqlParameter("@mytable", SqlDbType.Structured) { Value = dtSchema };
                //PARM[21] = new SqlParameter("@PdAcc", SqlDbType.Int) { Value = PDacc };
                //PARM[22] = new SqlParameter("@Zone", SqlDbType.Char, 4) { Value = Zone };
                //PARM[23] = new SqlParameter("@Circle", SqlDbType.Char, 4) { Value = Circle };
                //PARM[24] = new SqlParameter("@Ward", SqlDbType.Char, 5) { Value = Ward };
                PARM[21] = new SqlParameter("@Filler", SqlDbType.NVarChar) { Value = Filler };
                PARM[22] = new SqlParameter("@DivCode", SqlDbType.Int) { Value = DivCode };
                PARM[23] = new SqlParameter("@IpAddress", SqlDbType.NVarChar, 50) { Value = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString() };
                //Add For Department Integration Mcode and Ref
                Rv = gf.UpdateData(PARM, "EgEChallan_Integration");

                if (Rv == 0)
                {
                    Rv = -1;
                }
                else
                {
                    Id = Convert.ToInt32(PARM[15].Value);
                    Rv = int.Parse(PARM[15].Value.ToString());
                }
                return Rv;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return Rv;
        }

        public void HeadsCondition()
        {
            if (dtCheckHeads.Rows.Count > 0)
            {
                if (dtCheckHeads.Rows[0][1].ToString().Substring(0, 4) != "0030" && Convert.ToDouble(DeductCommission) > 0)
                {
                    throw new Exception("Discount not allowed.");
                }
                if (dtCheckHeads.Rows[0][1].ToString().Substring(0, 6) == "003003" && Convert.ToDouble(DeductCommission) > 0)
                {
                    throw new Exception("Discount is not allowed with (0030-03) Head");
                }
                if ((dtCheckHeads.Rows[0][1].ToString().Substring(0, 4) == "0030" || Convert.ToInt32(dtCheckHeads.Rows[0][1].ToString().Substring(0, 4)) > 7999) && dtCheckHeads.Rows.Count > 1)
                {
                    throw new Exception("Multiple Heads are not allowed.");
                }
                var duplicateHead = dtCheckHeads.AsEnumerable().Select(row => new { BudgetHead = row.Field<string>("BudgetHead"), ScheCode = row.Field<string>("ScheCode") }).Distinct().LongCount();
                if (dtCheckHeads.Rows.Count != duplicateHead)
                {
                    throw new Exception("Duplicates BudgetHead Found.");
                }
                var MajorHeadCount = dtCheckHeads.AsEnumerable().Select(row => new { MajorHead = row.Field<string>("BudgetHead").Substring(0, 4) }).Distinct().LongCount();
                if (MajorHeadCount > 1)
                {
                    throw new Exception("BudgetHead with multiple MajorHead not allowed.");
                }
                if ((dtCheckHeads.Rows[0][1].ToString().Substring(0, 13) == "8443001080000" || dtCheckHeads.Rows[0][1].ToString().Substring(0, 13) == "8443001090000") && (Filler == "A" || Filler.Contains("PD")))  //  Division Code  Compulsory for  This Budgethead  17 dec 2019 Add 109 12 jan 2021
                {
                    throw new Exception("Division Code Compulsory With This Head");
                }
            }
        }
    }
}
