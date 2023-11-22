using EgBL;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Admin_EgDMSScrollPDF : System.Web.UI.Page
{
    EgBankSoftCopyUploadBL objBankSoftCopyBL;
    EgDmsCheckBL ObjDMSCheck;
    EgDTALetter objEgDTALetter = new EgDTALetter();
    protected void Page_PreInit(Object sender, EventArgs e)
    {
        if ((Session["UserID"].ToString() == "46"))
            this.MasterPageFile = "~/MasterPage/MasterPage5.master";
    }



    protected void Page_Load(object sender, EventArgs e)
    {


        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (!IsPostBack)
        {
            FillBank();
            ddlMonth.Items.Insert(0, new ListItem("Select Month", "-1"));
            ddlYear.Items.Insert(0, new ListItem("Select Year", "-1"));
            FillMonth(Convert.ToInt16(rbtnList.SelectedValue));
            FillYear(Convert.ToInt16(rbtnList.SelectedValue));

        }
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
           ObjDMSCheck = new EgDmsCheckBL();


            if (rbtnList.SelectedValue == "1")
            {
                
                if (!fileDTA.HasFile)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Upload File.')", true);
                }
                else
                if (fileDTA.HasFile)
                {

                string filename1 = fileDTA.FileName; // getting the file name of uploaded file  
                string ext = Path.GetExtension(filename1); // getting the file extension of uploaded file  
                string[] Cdate = fileDTA.FileName.Split('_');

                if (Cdate.Length < 2)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File Format is Not Valid')", true);
                    return;
                }

                if (Cdate[0] != BankIFSCName.GetBankName(ddlbank.SelectedValue))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File  is not Proper FileName_MMYYYY Format')", true);
                    return;
                }

                try
                {
                    string[] Cdate1 = Cdate[1].ToString().Split('.');
                    string Challandate = (Cdate1[0].ToString().Substring(2, 4) + "/" + Cdate1[0].ToString().Substring(0, 2) + "/" + "01");
                }
                catch(Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File Format is Not Valid')", true);
                    return;
                }

                


                try
                {
                   
                    string filepath = Path.GetFileName(fileDTA.PostedFile.FileName);
                    string path = Server.MapPath("~/DMSScroll/" + filepath);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not  
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File is already Exist.')", true);
                        return;
                    }
                    
                    else
                    {

                        fileDTA.SaveAs(Server.MapPath("~/DMSScroll/" + filepath));
                       
                        int res = ObjDMSCheck.SignAndUnSignPdf(path, filename1, Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue),ddlbank.SelectedValue);
                        
                        if (res == 1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Data Save SuccessFully.')", true);
                        }
                        else if (res == -1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File Already Saved.')", true);

                            existFile(filepath);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Data  Could  Not be Save.')", true);
                        }
                    }
                    
                    }

                    catch (InvalidOperationException ex)
                  {
                   
                    string filepath = Path.GetFileName(fileDTA.PostedFile.FileName);
                    existFile(filepath);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('" + ex.Message.ToString() + "')", true);
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.Message + "EgDMSScrollPDF");
                    }
                    catch (Exception ex)
                    {
                   
                    string filepath = Path.GetFileName(fileDTA.PostedFile.FileName);
                    existFile(filepath);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Something is Wrong!!')", true);
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.Message + "EgDMSScrollPDF");
                    }
                }
            }
            else
            {
                DataSet ds = new DataSet();
                objEgDTALetter.UserId = Convert.ToInt32(Session["UserID"]);
                objEgDTALetter.FileYear = Convert.ToInt16(ddlYear.SelectedValue);
                objEgDTALetter.FileMonth = Convert.ToInt16(ddlMonth.SelectedValue);
                objEgDTALetter.BsrCode = ddlbank.SelectedValue;
                ds = objEgDTALetter.GetFiles();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlGrid.Visible = true;
                    grdDMSScroll.Visible = true;
                    grdDMSScroll.DataSource = ds;
                    grdDMSScroll.DataBind();
                    ds.Dispose();
                }
                else
                {
                    ds.Dispose();
                    pnlGrid.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found..');", true);
                }
            }
        //}
    }
   


    protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (rbtnList.SelectedValue == "1")
        {
            ddlbank.Items.Clear();
            FillBank();
            FillMonth(Convert.ToInt16(rbtnList.SelectedValue));
            FillYear(Convert.ToInt16(rbtnList.SelectedValue));
            tdFileUpload.Visible = true;
            grdDMSScroll.Visible = false;
            pnlGrid.Visible = false;

        }
        else
        {
            FillMonth(Convert.ToInt16(rbtnList.SelectedValue));
            FillYear(Convert.ToInt16(rbtnList.SelectedValue));
            FillBank();
            tdFileUpload.Visible = false;
        }
    }

    private void FillMonth(Int16 dropdownval)
    {

        ddlMonth.Items.Clear();
        if (dropdownval == 1)   // Upload Pdf
        {

            var rightNow = DateTime.Now;
            DateTimeFormatInfo monthinfo = DateTimeFormatInfo.GetInstance(null);

            if (DateTime.Now.Month - 1 == 0)
            {
                for (int i = DateTime.Now.Month; i <= DateTime.Now.Month; i++)
                {
                    ddlMonth.Items.Add(new ListItem(monthinfo.GetMonthName(i), i.ToString()));
                }
            }
            else
            {
                for (int i = DateTime.Now.Month-1; i <= DateTime.Now.Month; i++)
                {
                    ddlMonth.Items.Add(new ListItem(monthinfo.GetMonthName(i), i.ToString()));
                }

            }
            monthinfo = null;
        }
        else
        {

            DateTimeFormatInfo monthinfo = DateTimeFormatInfo.GetInstance(null);
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(new ListItem(monthinfo.GetMonthName(i), i.ToString()));
            }
            monthinfo = null;


        }
    }
    private void FillYear(Int16 dropdownval)
    {
        ddlYear.Items.Clear();

        if (dropdownval == 1)   // Upload Pdf
        {
            int currentYear = DateTime.Today.Year;

            {
                ddlYear.Items.Add(new ListItem((currentYear).ToString(), currentYear.ToString()));
            }
        }

        else
        {
            for (int i = 2020; i <= DateTime.Today.Year; i++)
                ddlYear.Items.Add(new ListItem((i).ToString(), i.ToString()));

        }
    }
    private void FillBank()
    {
        EgGRNBankStatus objEgGRNBankStatus = new EgGRNBankStatus();
        objEgGRNBankStatus.PopulateBankList(ddlbank);
        objBankSoftCopyBL = new EgBankSoftCopyUploadBL();
        objBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserId"]);

        if (objBankSoftCopyBL.UserId == 46)
        {
            ddlbank.Enabled = true;
        }
        else
        {
            ddlbank.SelectedValue = objBankSoftCopyBL.GetBSRCode();
            ddlbank.Enabled = false;
        }




    }

    protected void grdDMSScroll_RowCommand(object sender, GridViewCommandEventArgs e)

    
        {
        DataSet ds = new DataSet();
        EgDTALetter objEgDTALetter = new EgDTALetter();

        byte[] bytes;
        string fileName = "", contentType;

        if (e.CommandName == "Download")
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdDMSScroll.Rows[rowIndex];
            Label lblFileYear = (row.FindControl("lblYear") as Label);
            Label lblFileMonth = (row.FindControl("lblMonth") as Label);
            Label lblFileName = (row.FindControl("lblFName") as Label);
            Label lblID = (row.FindControl("lblID") as Label);
            Label lblBsrCode = (row.FindControl("lblBsrcode") as Label);
            objEgDTALetter.UserId = Convert.ToInt32(Session["UserID"]);
            objEgDTALetter.FileYear = Convert.ToInt16(lblFileYear.Text);
            objEgDTALetter.FileMonth = Convert.ToInt16(lblFileMonth.Text);
            objEgDTALetter.BsrCode = lblBsrCode.Text;
            ds = objEgDTALetter.GetFiles();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    bytes = (byte[])ds.Tables[0].Rows[0]["eSignedBytes"];
                    contentType = "application/pdf";
                    fileName = "E" + ds.Tables[0].Rows[0]["FileName"].ToString();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }

        }
    }

    private void existFile(string filepath)
    {

        string UploadFile = HttpContext.Current.Server.MapPath("~/DMSScroll/" + filepath);
        FileInfo file1 = new FileInfo(UploadFile);
        if (file1.Exists)//check file exsit or not  
        {
            file1.Delete();

        }
    }
}