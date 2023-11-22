using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EgBL;

public partial class WebPages_TO_EgFAQUploadPDF : System.Web.UI.Page
{
    string filename;
    byte[] imgfile;
    private static object lockobject = new object();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string file_name = FileUpload1.FileName.ToString();
        filename = FileUpload1.PostedFile.FileName;  //Get FileName
        if (FileUpload1.HasFile && FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
        {
            if (FileUpload1.PostedFile.ContentLength > 0 && Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower().Equals(".pdf"))
            {
                if (checkRealFile(FileUpload1) == true)
                {
                    File.Delete(Server.MapPath("~/Upload/FAQ.PDF"));
                    //FileUpload1.SaveAs(Server.MapPath("~/FAQ/" + filename));
                    FileUpload1.SaveAs(Server.MapPath("~/Upload/FAQ.PDF"));

                    Message("File Saved Successfully");
                }
                else
                    Message("Uploaded File is not in PDF Formate.");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Only Pdf File Can be Upload');", true);
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Please select any file');", true);
    }
    private void Message(string str)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + str + "');", true);
    }
    Boolean checkRealFile(FileUpload passfile)
    {
        lock (lockobject)
        {
            Stream fs = default(Stream);
            fs = passfile.PostedFile.InputStream;
            BinaryReader br1 = new BinaryReader(fs);
            imgfile = br1.ReadBytes(FileUpload1.PostedFile.ContentLength);
            //Image file Starting Bytes      
            //byte[] chkByte = { 255, 216, 255, 224 };
            //if you want check doc format Content the Use below suitable one for your requirement              
            //doc files start like this value       
            //byte[] chkByte = { 208, 207, 17, 224 };  
            //2003 MS word starting bytes (.doc format)       
            // byte[] chkByte = {80,75,3,4,20};        
            //2007 MS word starting bytes (.docx format)   
            byte[] chkByte = { 37, 80, 68, 70 };
            int j = 0;        //Check bytes are equal to real file bytes        
            for (Int32 i = 0; i <= 2; i++)
            {
                if (imgfile[i] == chkByte[i])
                {
                    j = j + 1;
                    if (j == 3)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
