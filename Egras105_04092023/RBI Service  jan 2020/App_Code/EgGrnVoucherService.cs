using System.Web.Services;
using EgBL;
using System;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class EgGrnVoucherService : System.Web.Services.WebService
{
    EgCTD objEgCTD;

    public EgGrnVoucherService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod] //for Getting Grn data for Voucher Entry In Rajkosh 
    public string GetGrnVoucherString(System.Int64 grn , string Tcode) 
    {
        string result = string.Empty;
        try
        {
            objEgCTD = new EgCTD();
            objEgCTD.GRN = grn;
            objEgCTD.TreasuryCode = Tcode;
             result = objEgCTD.GetGrnVoucherData();
            if (result == "0")
            {
                result = "Data Does Not Exist !!";
            }


        }

        catch (Exception ex)
        {

            result = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());

        }
        return result;
    }

}

