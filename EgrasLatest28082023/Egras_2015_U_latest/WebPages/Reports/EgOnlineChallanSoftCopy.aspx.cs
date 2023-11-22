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
public partial class WebPages_Reports_EgOnlineChallanSoftCopy : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            EgManualChallanDetailBL objManualChallan = new EgManualChallanDetailBL();
            objManualChallan.UserId = Convert.ToInt32(Session["UserId"]);
            string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
            objManualChallan.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
            objManualChallan.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());


            string result = objManualChallan.GetOnlineChallanData();


            if (result != "0")
            {
                string fileName = Session["UserName"].ToString().ToUpper() + "_" + txtfromdate.Text.Replace("/", "").ToString() + "_" + txttodate.Text.Replace("/", "").ToString();
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
        catch(Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Sorry!!Please Try Again.')", true);
        }
    }

}
