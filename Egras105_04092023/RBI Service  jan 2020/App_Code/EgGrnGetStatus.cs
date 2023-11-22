using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgGrnGetStatus" in code, svc and config file together.
public class EgGrnGetStatus : IEgGrnGetStatus
{
    public string GetGrnStatus(string GRN)
    {
        try
        {
            EgGrnGetStatusBL egStatus = new EgGrnGetStatusBL();
            egStatus.GRN = Convert.ToInt64(GRN);
            DataTable dt = new DataTable();
            dt = egStatus.GetGRNStatus();
            if (dt.Rows.Count > 0)
            {
                string Status = dt.Rows[0]["Status"].ToString();
                string GRN1 = dt.Rows[0]["GRN"].ToString();
                string Amount = dt.Rows[0]["Amount"].ToString();
                string BankDate = dt.Rows[0]["BankDate"].ToString();
                return GRN1 + "|" + Amount + "|" + Status + "|" + BankDate;
                //if (GRNValue == null && GRNValue == "")
                //    return "No Data Found";
                //else
                //    return dt;
            }
            else
            {
                return "No Data Found";
            }
            //string GRNValue = egStatus.GetGRNStatus();
                
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            return "Request Unable To Process !";
        }
    }

}
