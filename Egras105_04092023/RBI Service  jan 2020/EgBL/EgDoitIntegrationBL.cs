using System.Linq;
using System.Data;
using System.Web;
using System;
namespace EgBL
{
    public class EgDoitIntegrationBL : EgDeptInterfaceBL
    {
        #region Properties
        public string enc { get; set; }
        public string Mcode { get; set; }
        public string AUIN { get; set; }
        public static string errorName { get; set; }//sandeep for error show in IntegrationErrorPage
        #endregion
        public EgDoitIntegrationBL(string encData, string MerchantCode,string AUIN)
        {
            this.enc = encData;
            this.Mcode = MerchantCode;
            this.AUIN = AUIN;
        }

        DataTable dt;
        #region Method
        public DataTable SetDeptIntegrationData()
        {
            try
            {
                SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
                dt = new DataTable();
                EgSetIntegrationDataBL objEgSetIntegrationDataBL = new EgSetIntegrationDataBL();
                dt = objEgSetIntegrationDataBL.SetIntegrationData(objEncry.Decrypt(this.enc, HttpContext.Current.Server.MapPath("~/WebPages/Key/" + Mcode + ".key")), AUIN);
             
            }
            catch (Exception ex)
            {
                errorName = ex.Message; ;
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message);
                throw ex;
            }
            return dt;
        }
        #endregion
    }
}
