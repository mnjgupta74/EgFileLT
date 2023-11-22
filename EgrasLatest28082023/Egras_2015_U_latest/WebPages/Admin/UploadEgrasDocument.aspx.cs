using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;
using System;

public partial class WebPages_Admin_UploadEgrasDocument : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            bindGrid();
        }
    }
 protected void btnSubmit_Click(object sender, EventArgs e)
    {
        EgUploadPdfBL egFileUpload = new EgUploadPdfBL();
        if (!FilePDF.HasFile)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Upload File.')", true);
        }
        else if (FilePDF.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(FilePDF.FileName);
            if (fileExtension.ToLower() != ".pdf")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Only pdf Format is allowed !')", true);
                return;
            }
            try
            {

                string filepath = Path.GetFileName(FilePDF.PostedFile.FileName);
                var path = Path.GetTempPath() + filepath;    // Temp Path
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File is already Exist.')", true);
                    return;
                }
                else
                {
                    byte[] bytes = FilePDF.FileBytes;       // Direct Path Bytes Generation
                    egFileUpload.FileName = FilePDF.PostedFile.FileName;
                    egFileUpload.PdfByte = bytes;
                    egFileUpload.Type = 1;
                    egFileUpload.Id = 0;
                    egFileUpload.Flag = Convert.ToBoolean("False");
                    int status = egFileUpload.InsertDocument();
                    if (status == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File Save SuccessFully !')", true);
                        bindGrid();
                        return;
                    }
                    else if (status == -1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File is already Exist.')", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File is Not Save')", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
            }
        }
    }

    protected void grdPDFUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdPDFUpload.Rows[rowIndex];

            //Create & Download PDF File
            byte[] returnValue = (byte[])(grdPDFUpload.DataKeys[rowIndex].Values[0]);
            string FileName = (string)(grdPDFUpload.DataKeys[rowIndex].Values[1]);
            Response.Buffer = true;
            Response.Clear();
            Response.AddHeader("Content-disposition", "attachment; filename=" + FileName);
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }
        //if (e.CommandName == "myCheckbox")
        //{
        //    int rowIndex = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow row = grdPDFUpload.Rows[rowIndex];
        //    bool ischecked = (row.FindControl("chkRow") as CheckBox).Checked;
        //}
    }
    private void bindGrid()
    {
        EgUploadPdfBL egFileUpload = new EgUploadPdfBL();
        DataSet ds = new DataSet();
        egFileUpload.FileName = null;
        egFileUpload.Type = 2;
        ds = egFileUpload.GetFiles();
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlGrid.Visible = true;
            grdPDFUpload.Visible = true;
            grdPDFUpload.DataSource = ds;
            grdPDFUpload.DataBind();
            ds.Dispose();
        }
        else
        {
            ds.Dispose();
            grdPDFUpload.Visible = false;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found..');", true);
        }
    }

    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        EgUploadPdfBL egFileUpload = new EgUploadPdfBL();
        DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Name"), new DataColumn("Country") });
        foreach (GridViewRow row in grdPDFUpload.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                Label lblId = (row.Cells[0].FindControl("lblID") as Label);
                egFileUpload.Type = 2;
                egFileUpload.Id = Convert.ToInt32(lblId.Text);
                egFileUpload.Flag = Convert.ToBoolean(chkRow.Checked);
                int status = egFileUpload.InsertDocument();
                bindGrid();
                //if (chkRow.Checked)
                //{
                //    string name = row.Cells[1].Text;
                //    string country = (row.Cells[2].FindControl("lblCountry") as Label).Text;
                //    dt.Rows.Add(name, country);
                //}
            }
        }
    }
}