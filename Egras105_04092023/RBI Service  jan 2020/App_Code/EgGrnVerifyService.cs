using System.ServiceModel;
using System.ServiceModel.Activation;
using EgBL;
using System.Data;
using System;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgGrnVerifyService" in code, svc and config file together.
public class EgGrnVerifyService : IEgGrnVerifyService
{
    string Message = string.Empty;
    public void GrnVerify()
    {
        try
        {
            //EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
            EgFrmTOVerified objEgFrmTOVerified = new EgFrmTOVerified();
            VerifiedBankClass objVerifiedBankClass = new VerifiedBankClass();
            DataTable dt = new DataTable();
            dt = objEgFrmTOVerified.GetGrnForVerification();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    objVerifiedBankClass.GRN = Convert.ToInt64(dt.Rows[i]["GRN"].ToString());
                    objVerifiedBankClass.TotalAmount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                    objVerifiedBankClass.BSRCode = dt.Rows[i]["BankName"].ToString();
                    objVerifiedBankClass.PaymentMode = dt.Rows[i]["PaymentType"].ToString();
                    Message = objVerifiedBankClass.Verify();

                    //string Flag = "P";
                    string Flag = Message.ToString().Trim() == "Status updated as successfull ".Trim() ? "S" :
                                  Message.ToString().Trim() == "Status updated as Unsuccessfull ".Trim() ? "F" : "P";
                  
                    objEgFrmTOVerified.GRN = Convert.ToInt64(dt.Rows[i]["GRN"].ToString());
                    objEgFrmTOVerified.flag = Flag;
                    objEgFrmTOVerified.GrnPending();
                }
                catch (Exception ex) {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.Message.ToString() + "-Grn Auto Verify Service-" + objVerifiedBankClass.BSRCode + '-' + objVerifiedBankClass.GRN);

                }
            }
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "-Grn Auto Verify Service-");
        }
    }
}
