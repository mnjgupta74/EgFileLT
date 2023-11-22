using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using EgBL;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebPages_Department_EgDefaceReleaseRequest : System.Web.UI.Page
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
        string file_name = fileDTA.FileName.ToString();
        string filename = fileDTA.PostedFile.FileName;  //Get FileName
        if (fileDTA.HasFile && fileDTA.PostedFile != null && fileDTA.PostedFile.FileName != "")
        {
            if (fileDTA.PostedFile.ContentLength > 0 && Path.GetExtension(fileDTA.PostedFile.FileName).ToLower().Equals(".pdf"))
            {
                try
                {
                    EgReleaseAmountBL egReleaseBL = new EgReleaseAmountBL();
                    egReleaseBL.Grn = Convert.ToInt32(txtGRN.Text);
                    egReleaseBL.Amount = Convert.ToDouble(txtAmount.Text);
                    byte[] pdfByte = fileDTA.FileBytes;
                    egReleaseBL.PdfByte = pdfByte;
                    DataTable dt = new DataTable();
                    dt = egReleaseBL.InsertDefaceReleaseRequest();
                    if (dt.Rows.Count > 0)
                    {
                        string Scode = dt.Rows[0]["code"].ToString();
                        string RefNo = dt.Rows[0]["ReferenceID"].ToString();
                        if (Scode == "012")
                        {
                            trgrddefacerelease.Visible = false;
                            trRefNO.Visible = true;
                            lblRefNO.Text = "Request Forward to HOD By Ref.NO : " + RefNo ;
                             //ScriptManager.RegisterStartupScript(this, GetType(), "MSG", "myAlert('Request Accepted Reference No is :','" + RefNo + "');", true);
                            txtGRN.Text = "";
                            txtAmount.Text = "";
                        }
                        else if (Scode == "013")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Deface Release Amount Can Not Be greather Then Total Amount !');", true);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Your Reference No is '" + RefNO + "' Save This For Future Use')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('GRN Not Available for  Release Refund');", true);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Your Reference No is '" + RefNO + "' Save This For Future Use')", true);
                        }
                        


                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Reference No is Not Generated')", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Reference No is Not Generated')", true);
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.Message.ToString());
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Upload File.')", true);
        }


    }
    private void Message(string str)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + str + "');", true);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        EgReleaseAmountBL egReleaseBL = new EgReleaseAmountBL();
        DataTable dt = new DataTable();
        if (txtGRN.Text.ToString() == "" || txtGRN.Text.ToString() == null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Enter GRN.')", true);
            return;
        }
        egReleaseBL.Grn = Convert.ToInt64(txtGRN.Text.ToString());
        dt=  egReleaseBL.EgGetDefacerequestStatus();
        //if(dt.Rows.Count > 0)
        //{
        trgrddefacerelease.Visible = true;
        //}
        grddefacerelease.DataSource = dt;
        grddefacerelease.DataBind();
    }
    protected void grddefacerelease_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName == "grnbind")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));
            string script = "window.open('../EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
        }

        if (e.CommandName == "ReferenceNo")  // Add SectionLetter Url fro IGRS 28 April 2022
        {
            int GRN = Convert.ToInt32(e.CommandArgument);
            LinkButton lb = (LinkButton)e.CommandSource;
            int RefNo = Convert.ToInt32(lb.Text);
            SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();

                byte[] returnValue = getPDfBytes(RefNo, GRN);
                Response.Buffer = true;
                Response.Clear();
                Response.AddHeader("Content-disposition", "attachment; filename=" + RefNo + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(returnValue);
                Response.Flush();
                Response.End();
            
        }
    }
    public byte[] getPDfBytes(Int64 refNo, Int64 Grn)
    {
        byte[] bytes;
        EgReleaseDefacedEntryBL objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        objEgReleaseDefacedEntryBL.RefrenceNo = refNo;
        objEgReleaseDefacedEntryBL.Grn = Grn;
        DataTable dt = objEgReleaseDefacedEntryBL.GetDefaceReleasePDfBytes();
        bytes = (byte[])dt.Rows[0]["PDFFile"];
        return bytes;
        //GetDefaceReleasePDfBytes
    }
}
