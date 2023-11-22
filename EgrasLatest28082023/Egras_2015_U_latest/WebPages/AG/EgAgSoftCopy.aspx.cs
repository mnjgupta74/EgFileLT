using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.IO;




public partial class WebPages_AG_EgAgSoftCopy : System.Web.UI.Page
{

    EgAgSoftcopyBL objTrgVoucherToTxt;
    string filename;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Write("<Script>alert('Session Expired')</Script>");
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {

            objTrgVoucherToTxt = new EgAgSoftcopyBL();
            //txtDateFrom.Attributes.Add("onchange", "checkdate('txtDateFrom')");
            //txtDateTo.Attributes.Add("onchange", "checkdate('txtDateTo')");
            objTrgVoucherToTxt.TreasuryCode = "5000";//Session["treasuryCode"].ToString();
            objTrgVoucherToTxt.fillTreasury(ddltreasury);
            ddltreasury.Items.Insert(0, new ListItem("--Select ALL--", "0000"));
            calendarfromdate.EndDate = DateTime.Now;
            calendartodate.EndDate = DateTime.Now;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objTrgVoucherToTxt = new EgAgSoftcopyBL();
        try
        {

            string filenamesave = ddlType.SelectedValue + DateTime.Now.Ticks.ToString() + ".txt";


            if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
            {


                string[] fromdate = txtDateFrom.Text.Trim().Replace("-", "/").Split('/');
                objTrgVoucherToTxt.DateFrom = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
                string[] todate = txtDateTo.Text.Trim().Replace("-", "/").Split('/');
                objTrgVoucherToTxt.DateTo = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
                if ((objTrgVoucherToTxt.DateTo - objTrgVoucherToTxt.DateFrom).TotalDays > 180)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
                    return;
                }


                objTrgVoucherToTxt.TreasuryCode = ddltreasury.SelectedValue; //Session["treasuryCode"].ToString();
                objTrgVoucherToTxt.Type = "1";
                // function calling to save data 
                objTrgVoucherToTxt.WriteToFile(Server.MapPath(filenamesave), ddlType.SelectedValue);
            }


            // code to download the text file
            //------------------------------------------------

            System.IO.FileStream fs = null;
            fs = File.Open(Server.MapPath(filenamesave), System.IO.FileMode.Open);
            if (fs.Length > 0)
            {
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + filenamesave);

                Response.ContentType = "application/octet-stream";

                if (btFile.Length > 0)
                {
                    Response.BinaryWrite(btFile);
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                    fs.Dispose();
                    fs = null;

                    File.Delete(filenamesave);  // delete file which has created

                    Response.End();


                }


            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopMsg", "alert('Sorry!! No Data Found ');", true);


        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopMsg", "alert('Sorry!!Please Try Again. ');", true);
        }
        //------------------------------------------------
    }

}
