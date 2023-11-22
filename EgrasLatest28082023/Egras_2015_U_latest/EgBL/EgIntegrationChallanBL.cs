using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
   public class EgIntegrationChallanBL:EgEChallanBL
    {
         public int Mcode { get; set; }
         public string AUIN { get; set; }
         public string officeXml { get; set; }
        public SqlParameter[] PM { get; set; }
        public override  int InsertChallan()
         {
             GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[28];
            PARM[0] = new SqlParameter("@Identity", SqlDbType.VarChar, 15) { Value = Identity };
            PARM[1] = new SqlParameter("@OfficeName", SqlDbType.Int) { Value = OfficeName };
            PARM[2] = new SqlParameter("@Location", SqlDbType.Char, 4) { Value = Location };
            PARM[3] = new SqlParameter("@FullName", SqlDbType.VarChar, 50) { Value = FullName };
            PARM[4] = new SqlParameter("@ChallanYear", SqlDbType.Char, 4) { Value = ChallanYear };
            PARM[5] = new SqlParameter("@ChallanFromMonth", SqlDbType.SmallDateTime) { Value = ChallanFromMonth };
            PARM[6] = new SqlParameter("@ChallanToMonth", SqlDbType.SmallDateTime) { Value = ChallanToMonth };
            PARM[7] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = City };
            PARM[8] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = Address };
            PARM[9] = new SqlParameter("@PINCode", SqlDbType.Char, 6) { Value = PINCode };
            PARM[10] = new SqlParameter("@DeductCommission", SqlDbType.Money) { Value = DeductCommission };
            PARM[11] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = TotalAmount };
            PARM[12] = new SqlParameter("@BankName", SqlDbType.Char, 7) { Value = BankName };
            PARM[13] = new SqlParameter("@Paymenttype", SqlDbType.Char, 1) { Value = TypeofPayment };
            PARM[14] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[15] = new SqlParameter("@Id", SqlDbType.Int) { Value = Id };
            PARM[15].Direction = ParameterDirection.Output;
            PARM[16] = new SqlParameter("@Remarks", SqlDbType.VarChar, 200) { Value = Remark };
            PARM[17] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = Mcode };
            PARM[18] = new SqlParameter("@RefNumber", SqlDbType.VarChar, 50) { Value = AUIN };
            PARM[19] = new SqlParameter("@Profile", SqlDbType.Int) { Value = Profile };
            PARM[20] = new SqlParameter("@mytable", SqlDbType.Structured) { Value = dtSchema };
            PARM[21] = new SqlParameter("@PdAcc", SqlDbType.Int) { Value = PDacc };
            PARM[22] = new SqlParameter("@Zone", SqlDbType.Char, 4) { Value = Zone };
            PARM[23] = new SqlParameter("@Circle", SqlDbType.Char, 4) { Value = Circle };
            PARM[24] = new SqlParameter("@Ward", SqlDbType.Char, 5) { Value = Ward };
            PARM[25] = new SqlParameter("@Filler", SqlDbType.Int) { Value = filler };
            PARM[26] = new SqlParameter("@DivCode", SqlDbType.Int) { Value = DivCode };
            PARM[27] = new SqlParameter("@xmlOffices", SqlDbType.Xml) { Value = officeXml };
            //Add For Department Integration Mcode and Ref
            int Rv = gf.UpdateData(PARM, "EgEChallan");

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
        // Add New Method  for WAM department  add Division code according to Officeid
        public int InsertDynamicChallan()
        {
            GenralFunction gf = new GenralFunction();
            PM[0] = new SqlParameter("@Id", SqlDbType.BigInt) { Value = Id };
            PM[0].Direction = ParameterDirection.Output;
            int Rv = gf.UpdateData(PM, "EgEChallan");

            if (Rv == 0)
            {
                Rv = -1;
            }
            else
            {
                Id = Convert.ToInt32(PM[0].Value);
                Rv = int.Parse(PM[0].Value.ToString());
            }
            return Rv;
        }
        public int GetDivisionCode()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Location };
            PARM[1] = new SqlParameter("@OfficeId", SqlDbType.VarChar,7) { Value = filler };
            //return Convert.ToInt32(gf.ExecuteScaler(PARM, "EgGetDivisionCode"));
            string result = gf.ExecuteScaler(PARM, "EgGetDivisionCode");
            int returnVal;
            bool isNumeric = int.TryParse(result, out returnVal);
            if (isNumeric)
                return returnVal;
            else
                return 0;
        }
       


    }
}
