using System;
using EgBL;


public partial class WebPages_EgCtd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Click_Click(object sender, EventArgs e)
    {
        CTDSelfServ.CTDWebService objWebService = new CTDSelfServ.CTDWebService();
        //ctd.CTDWebService objWebService = new ctd.CTDWebService();
        string[] dat = txtReqTime.Text.Split('/');
        string date = dat[0].ToString().Trim() + "/" + dat[1].ToString().Trim() + "/" + dat[2].ToString().Trim();
        string getdate = "UserName=" + txtUserName.Text.Trim() + "|Password =" + txtPass.Text.Trim() + "|Slab =" + Convert.ToInt16(txtSlab.Text.Trim()) + "|RequestedTime =" + date +"";
        //string getdate = "UserName=ctd|Password =Ctd#18|Slab =7|RequestedTime =2013/06/28";
        SbiEncryptionDecryption objEnc = new SbiEncryptionDecryption(); 
        string Encgetdate = objEnc.Encrypt(getdate, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"aes.key");


        //EgTimeSlab objWebService = new EgTimeSlab();
        //objWebService.UserName = txtUserName.Text.Trim();
        //objWebService.Password = txtPass.Text.Trim();
        //objWebService.Slab = Convert.ToInt32(txtSlab.Text.Trim());
        //objWebService.RequestedTime = Convert.ToDateTime(date);
        String Return = objWebService.UpdateSlab(Encgetdate);      
        
        if (Return != "0")
        {
            string DecriptVal = objEnc.Decrypt(Return, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"aes.key");
            lblshow.Text = Return.ToString();
        }
        else
        {
            lblshow.Text = Return.ToString(); 
        }

    }
}
