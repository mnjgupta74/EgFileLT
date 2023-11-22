using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EgBL;
public partial class WebPages_TO_EgUploadPDF : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            //   FileUpload1.Attributes.Add("onchange", "return checkFileExtension(this);");
            EgLoginBL obj = new EgLoginBL();
            obj.CircularPdf(RptCircular);
            if (RptCircular.Items.Count == 0)
            {
                FieldCircular.Visible = false;
            }
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
                    FileUpload1.SaveAs(Server.MapPath("~/Upload/" + filename));
                    EgUploadPdfBL objPdf = new EgUploadPdfBL();
                    objPdf.PdfName = txtfilename.Text.Trim();
                    objPdf.PdfPath = filename.Trim();
                    if (objPdf.InsertData() == 1)
                    {
                        txtfilename.Text = "";
                        Message("File Saved Successfully");
                        EgLoginBL obj = new EgLoginBL();
                        obj.CircularPdf(RptCircular);
                        FieldCircular.Visible = true;
                    }
                    else
                        Message("File not Save.!");
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
    // Added by Rachit Sharma on 1 sept 2014 (To edit name of PDF uploaded or to delete the Pdf)
    // AS told by JP sir
    protected void RptCircular_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {

            ((TextBox)e.Item.FindControl("txtpdfname")).Visible = true;
            ((Label)e.Item.FindControl("lblpdfname")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkEdit")).CommandName = "Update";
            ((LinkButton)e.Item.FindControl("lnkEdit")).Text = "Update";
            ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = true;
            ((TextBox)e.Item.FindControl("txtpdfname")).Style.Add("background-color", "buttonface");


        }
        if (e.CommandName == "delete")
        {
            EgLoginBL obj = new EgLoginBL();
            obj.pdfId = Convert.ToInt32(((Label)e.Item.FindControl("lblPdfId")).Text);
            obj.DeleteCircularPdf();
            obj.CircularPdf(RptCircular);
            if (RptCircular.Items.Count == 0)
            {
                FieldCircular.Visible = false;
            }

        }

        if (e.CommandName == "Update")
        {
            EgLoginBL obj = new EgLoginBL();
            obj.pdfname = ((TextBox)e.Item.FindControl("txtpdfname")).Text;
            obj.pdfId = Convert.ToInt32(((Label)e.Item.FindControl("lblPdfId")).Text);
            obj.EditCircularPdf();
            obj.CircularPdf(RptCircular);
        }

        if (e.CommandName == "Cancel")
        {
            ((TextBox)e.Item.FindControl("txtpdfname")).Visible = false;
            ((Label)e.Item.FindControl("lblpdfname")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkEdit")).CommandName = "Edit";
            ((LinkButton)e.Item.FindControl("lnkEdit")).Text = "Edit";
            ((TextBox)e.Item.FindControl("txtpdfname")).Text = ((Label)e.Item.FindControl("lblpdfname")).Text;

        }


    }



}
