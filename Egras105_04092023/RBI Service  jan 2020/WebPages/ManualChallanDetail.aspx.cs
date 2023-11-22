using System;
using System.Web.UI;
using EgBL;
using System.Web;
public partial class WebPages_ManualChallanDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void Click_Click(object sender, EventArgs e)
    {
        EgBankSoftCopyBL objBankSoftCopyBL = new EgBankSoftCopyBL();

        objBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserId"]);
        string BankBSRcode = objBankSoftCopyBL.GetBSRCode();

        EgManualChallanDetailBL objManualChallan = new EgManualChallanDetailBL();
        string[] dat = txtDate.Text.Split('/');
        string date = dat[1].ToString().Trim() + "/" + dat[0].ToString().Trim() + "/" + dat[2].ToString().Trim();
        objManualChallan.BSRCode = BankBSRcode;
        objManualChallan.Date = DateTime.Parse(date);
        objManualChallan.fromtime = ddlTime.SelectedValue;
        objManualChallan.toTime = ddlTimeSlab.SelectedValue;
        string result = objManualChallan.GetBankManualData();


        if (result != "0")
        {
            string fileName = Session["UserName"].ToString().ToUpper() + "_" + txtDate.Text.Replace("/", "").ToString() + "_" + ddlTime.SelectedItem.Text.Substring(0, 2) + ddlTime.SelectedItem.Text.Substring(3, 2) + "_" + ddlTimeSlab.SelectedItem.Text.Substring(0, 2) + ddlTimeSlab.SelectedItem.Text.Substring(3, 2);
            System.IO.StreamWriter file = new System.IO.StreamWriter(Server.MapPath("~/ManualChallan/" + fileName + ".txt"));
            file.WriteLine(result.Replace("^", "\r\n"));
            file.Close();

            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(Server.MapPath("~/ManualChallan/" +
                      fileName + ".txt"), System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" +
                               fileName + ".txt");
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('No Record found.')", true);
        }

    }
}
