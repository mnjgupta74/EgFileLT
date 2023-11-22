using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EgBL;

public partial class WebPages_EgBankPage : System.Web.UI.Page
{

    EgBankSoftCopyBL objBankSoftcopyBL = new EgBankSoftCopyBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void btngenerate_Click(object sender, EventArgs e)
    {
        int Grn = int.Parse(txtGRN.Text);
        string CIN = "1235432118092012876543";
        int reference = RandomNumber(1, 1000);
        double amount = Convert.ToDouble(txtAmt.Text);
        string status = "s";
        DateTime timestamp = DateTime.Now;

        //EService.Service objEService = new EService.Service();

        //int result = objEService.UpdateGRN(Grn, amount, CIN, reference, status, timestamp);

        divRecord.Visible = true;
        
        objBankSoftcopyBL.GRN = Grn;
        DataTable dt = objBankSoftcopyBL.GetBankManualData();


        grnlbl.Text = dt.Rows[0][0].ToString();


        lblhead1.Text = dt.Rows[0][1].ToString(); lblheadamount1.Text = dt.Rows[0][2].ToString();

        lblhead2.Text = dt.Rows[1][1].ToString(); lblheadamount2.Text = dt.Rows[1][1].ToString();

        lblhead3.Text = dt.Rows[2][1].ToString(); lblheadamount3.Text = dt.Rows[2][1].ToString();

        lblhead4.Text = dt.Rows[3][1].ToString(); lblheadamount4.Text = dt.Rows[3][1].ToString();

        lblhead5.Text = dt.Rows[4][1].ToString(); lblheadamount5.Text = dt.Rows[4][1].ToString();

        lblhead6.Text = dt.Rows[5][1].ToString(); lblheadamount6.Text = dt.Rows[5][1].ToString();

        lblhead7.Text = dt.Rows[6][1].ToString(); lblheadamount7.Text = dt.Rows[6][1].ToString();

        lblhead8.Text = dt.Rows[7][1].ToString(); lblheadamount8.Text = dt.Rows[7][1].ToString();

        lblhead9.Text = dt.Rows[8][1].ToString(); lblheadamount9.Text = dt.Rows[8][1].ToString();

        fullnamelbl.Text = dt.Rows[0][3].ToString();
        netamountlbl.Text = dt.Rows[0][4].ToString();

        //ToVerified.Service objTO = new ToVerified.Service();
        //string cinRef = objTO.ToVerfied();

        //Response.Redirect("EgBankVerifiedChallan.aspx?id=" + cinRef + "&grn=" + grn + "&amt=" + money);

        //string[] fromdate = txtdate.Text.Trim().Replace("-", "/").Split('/');
        //string date1 = fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString();
        //DataTable dt = Services.GenerateGrn(date1);

        //if (mySvc.Reconcile(dt).ToString() == "1")
        //{
        //    Label2.Text = "Successfully ";
        //}
        //else
        //{
        //    Label2.Text = "UnSuccessfully";
        //}

    }

    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
}
