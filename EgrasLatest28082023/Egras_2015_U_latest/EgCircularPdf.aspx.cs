using System;
using EgBL;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class EgCircularPdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EgLoginBL obj = new EgLoginBL();
            obj.CircularPdfNew(grdCircular);
        }

    }

    protected void grdCircular_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "Download")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdCircular.Rows[rowIndex];

                //Create & Download PDF File
                byte[] returnValue = (byte[])(grdCircular.DataKeys[rowIndex].Values[0]);
                string FileName = (string)(grdCircular.DataKeys[rowIndex].Values[1]);
                Response.Buffer = true;
                Response.Clear();
                Response.AddHeader("Content-disposition", "attachment; filename=" + FileName + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(returnValue);
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('File Not Found');", true);
        }
    }
}
