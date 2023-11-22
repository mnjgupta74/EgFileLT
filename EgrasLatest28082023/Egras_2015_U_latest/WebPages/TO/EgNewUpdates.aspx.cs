using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EgBL;

public partial class WebPages_TO_EgNewUpdates : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            EgNewUpdates objPdf = new EgNewUpdates();
            objPdf.Flag = 0;
            objPdf.NewUpdatesPdf(reptNewUpdates);
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
                    EgNewUpdates objPdf = new EgNewUpdates();
                    objPdf.PdfName = txtfilename.InnerText.Trim();
                    objPdf.PdfPath = filename.Trim();
                    if (objPdf.InsertData() == 1)
                    {
                        txtfilename.InnerText = "";
                        objPdf.Flag = 0;
                        Message("File Saved Successfully");

                        objPdf.NewUpdatesPdf(reptNewUpdates);
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
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        EgNewUpdates objpdf = new EgNewUpdates();
        CheckBox cb = (CheckBox)sender;
        HiddenField lbl = (HiddenField)cb.Parent.FindControl("hdnid");
        if (cb.Checked)
        {
            objpdf.Flag = 1;
            objpdf.pdfid = Convert.ToInt32(lbl.Value.ToString());
            int x = objpdf.UpdateCheck();
            if (x == 1)
            {
                Message("Display Off");
                objpdf.Flag = 0;
                objpdf.NewUpdatesPdf(reptNewUpdates);
            }
            //else
            //{
            //    Message("File not Deleted.!");
            //}
            //write ur code to insert into database
        }
        else
        {
            objpdf.Flag = 0;
            objpdf.pdfid = Convert.ToInt16(lbl.Value.ToString());
            int x = objpdf.UpdateCheck();
            if (x == 1)
            {
                Message("Display ON");
                objpdf.Flag = 0;
                objpdf.NewUpdatesPdf(reptNewUpdates);
            }
        }
    }
    protected void reptNewUpdates_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdnflag = (HiddenField)e.Item.FindControl("hdnflag");
            int Flag = Convert.ToInt16(hdnflag.Value);
            CheckBox CheckBox1 = (CheckBox)e.Item.FindControl("CheckBox1"); //(CheckBox)Row.FindControl("chkbox");
            if (Flag == 1)
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }
        }
    }
}
