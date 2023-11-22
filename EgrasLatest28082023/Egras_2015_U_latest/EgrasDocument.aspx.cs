using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EgrasDocument : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindGrid();
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
    }
    private void bindGrid()
    {
        EgUploadPdfBL egFileUpload = new EgUploadPdfBL();
        DataSet ds = new DataSet();
        ds = egFileUpload.GetFiles();
        ds.Tables[0].DefaultView.RowFilter = "Flag = 'True'";
        if (ds.Tables[0].Rows.Count > 0)
        {
            //pnlGrid.Visible = true;
            grdPDFUpload.Visible = true;
            grdPDFUpload.DataSource = ds.Tables[0].DefaultView;
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
}