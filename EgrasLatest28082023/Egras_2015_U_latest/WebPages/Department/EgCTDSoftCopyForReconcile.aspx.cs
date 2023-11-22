using System;
using System.Web.UI;
using EgBL;

public partial class WebPages_Department_EgCTDSoftCopyForReconcile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void Click_Click(object sender, EventArgs e)
    {
        EgTimeSlab objTimeSlab = new EgTimeSlab();
      
        string[] date = txtDate.Text.Trim().Replace("-", "/").Split('/');
        objTimeSlab.RequestedTime = Convert.ToDateTime(date[2].ToString() + "/" + date[1].ToString() + "/" + date[0].ToString());
        objTimeSlab.Slab = Convert.ToInt32(ddlSlab.SelectedValue);

        objTimeSlab.flag = rblSearchType.SelectedValue;
        string result = objTimeSlab.CTDOnlineDataForReconcile();

        if (result != "0")
        {
            string fileName = Session["UserName"].ToString().ToUpper() + "_" + txtDate.Text.Replace("/", "").ToString() + "_" + ddlSlab.SelectedItem.Text;
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
