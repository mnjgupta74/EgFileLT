using EgBL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Department_EgTrackGRN : System.Web.UI.Page
{


    protected override void OnLoad(EventArgs e)
    {
        //base.OnLoad(e);
        List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("txtDynamic")).ToList();
        int i = 1;
        foreach (string key in keys)
        {
            this.CreateTextBox("txtDynamic" + i);
            i++;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        

        int index = pnlTextBoxes.Controls.OfType<TextBox>().ToList().Count + 1;
        this.CreateTextBox("txtDynamic" + index);
    }

    private void CreateTextBox(string id)
    {
        TextBox txt = new TextBox();
        txt.ID = id;
        pnlTextBoxes.Controls.Add(txt);
        txt.Attributes.Add("Class", "form-control");
        txt.Attributes.Add("MaxLength", "15");
        txt.Style.Add("height", "30px");
        txt.Style.Add("width", "20%");

        txt.Attributes.Add("onkeypress", " return (event.charCode > 47 && event.charCode < 58)");



        Literal lt = new Literal();
        lt.Text = "<br />";
        pnlTextBoxes.Controls.Add(lt);
        btnPrint.Visible = true;
        divPnl.Visible = true;
        divDownload.Visible = true;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string message = "";
        foreach (TextBox textBox in pnlTextBoxes.Controls.OfType<TextBox>())
        {
            if(textBox.Text !="" && textBox.Text != null)
             message += textBox.Text + "|";
        }
        Regex regex = new Regex("^[0-9|]*$");
        if (regex.IsMatch(message))
        {
            if (message.Length > 0)
            {
                PrintReport("pdf", "PDF", message);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter GRN');", true);
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter Valid GRN');", true);
        }

    }

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    PrintReport("pdf", "PDF");
    //}
    private void PrintReport(string extention, string fileformate,string GRN)
    {
        try
        {
            ReportParameter[] param = new ReportParameter[2];


            param[0] = new ReportParameter("GRN", GRN);
            param[1] = new ReportParameter("UserName", Session["userName"].ToString());


            string rptname = string.Empty;

            rptname = "EgRptTrackGRN";

            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptTrackGRN, rptname, param);
            // create PDF
            // if (Response.IsClientConnected) { Response.Flush(); }
            byte[] returnValue = null;
            string format = fileformate;
            string deviceinfo = "";
            string mimeType = "";
            string encoding = "";
            string extension = extention;
            string[] streams = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;

            returnValue = rptTrackGRN.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=EgGRNLogReport." + extension);
            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Some Technical Error');", true);
        }
    }
}