using EgBL;
using System;
using System.IO;
using System.Web.Services;
using System.Web.UI;

public partial class WebPages_Admin_EgDTALetterData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string filename1 = fileDTA.FileName; // getting the file name of uploaded file  
        string ext = Path.GetExtension(filename1); // getting the file extension of uploaded file  
        string type = String.Empty;
        if (!fileDTA.HasFile)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Upload File.')", true);
        }
        else
        if (fileDTA.HasFile)
        {
            try
            {
                switch (ext) // this switch code validate the files which allow to upload only PDF file   
                {
                    case ".pdf":
                        type = "application/pdf";
                        break;
                }
                if (type != String.Empty)
                {
                    if (txtSno.Value != "" && txtsub.Value!="")
                    {
                        EgDTALetter objEgDTALetter = new EgDTALetter();
                        objEgDTALetter.LetterName = fileDTA.FileName;
                        objEgDTALetter.SerialNo = txtSno.Value;
                        objEgDTALetter.Status = ddlStatus.Value;
                        objEgDTALetter.Subject = txtsub.Value;
                        objEgDTALetter.Remarks = txtremarks.Value;
                        int res = objEgDTALetter.InsertLetterData();
                        string filepath = Path.GetFileName(fileDTA.PostedFile.FileName);
                        fileDTA.SaveAs(Server.MapPath("~/DTA/" + filepath));
                        if (res == 1)
                        {
                            txtremarks.Value = "";
                            txtsub.Value = "";
                            txtSno.Value = "";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Data Save SuccessFully.')", true);
                        }

                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Data  Could  Not be Save.')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Fill the mandatory fields.')", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('upload pdf file only.')", true);
                }

            }
            catch (Exception ex)
            {
                
            }
        }
        
    }
}