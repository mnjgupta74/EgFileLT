using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public partial class WebPages_EgMultipleOfficeDetails : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        EgEChallanBL objEgEChallan = new EgEChallanBL();
        StringBuilder sb = new StringBuilder();
        EgEncryptDecrypt ObjEncrytDecrypt = new EgEncryptDecrypt();
        if (Request.QueryString.Count > 0)
        {
            string id;
            EncryptDecryptionBL objenc = new EncryptDecryptionBL();
            var idnew = objenc.Decrypt(Request.QueryString[0].ToString());
            string[] idvalue = idnew.ToString().Split('-');
            id = idvalue[0].ToString();
            objEgEChallan.GRNNumber = Convert.ToInt32(id);
            dt = objEgEChallan.GetOfficeDetails();
            if (dt.Rows.Count > 0)
            {
                lblGRN.Text = objEgEChallan.GRNNumber.ToString();
                RptOffices.DataSource = dt;
                RptOffices.DataBind();
            }
        }
    }
    public string GetTotalAmount()
    {
        double sum = Convert.ToInt64(dt.Compute("SUM(Amount)", string.Empty));
        return sum.ToString("F");
    }
}
