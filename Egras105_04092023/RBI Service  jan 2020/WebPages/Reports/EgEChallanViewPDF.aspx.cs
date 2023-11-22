using System;
using System.Data;
using EgBL;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;


public partial class WebPages_EgEChallanViewPDF : System.Web.UI.Page
{
    EgEncryptDecrypt ObjEncrcryptDecrypt;
    DataTable dt;
    //DataTable dt1;
    string GRN;
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjEncrcryptDecrypt = new EgEncryptDecrypt();
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (Request.QueryString.Count == 0)
        {
            Response.Redirect("~\\logout.aspx");
        }
        else
        {
            string strReq = Request.Url.ToString();
            strReq = strReq.Substring(strReq.IndexOf('?') + 1);
            List<string> strList = ObjEncrcryptDecrypt.Decrypt(strReq);
            if (strList.Count > 0)
            {
                GRN = strList[1].ToString().Trim();
            }
            else
            {
                Response.Redirect("~\\logout.aspx");
            }
        }
      
            if (!IsPostBack)
            {
                loadreport();
            }
            else if (ViewState["dt"] != null && ViewState["dt1"] != null)
            {
                loadreport();
            }
      
    }
    //public void loadreport()
    //{

    //    DataTable dt2 = new DataTable();


    //    EgEChallanBL objEgEChallan = new EgEChallanBL();
    //    ObjEncrcryptDecrypt = new EgEncryptDecrypt();
    //    ReportDocument rpt = new ReportDocument();
    //    ReportFactory rf = new ReportFactory();
    //    ReportDocument rpt1 = new ReportDocument();
    //    ReportFactory rf1 = new ReportFactory();
    //    dt = new DataTable();
    //    objEgEChallan.GRNNumber = Convert.ToInt32(GRN.ToString());
    //    dt = objEgEChallan.EChallanViewPDF();
    //    if (dt.Rows.Count > 0)
    //    {
    //        dt2 = objEgEChallan.EChallanViewSubRptPDF();
    //        dt2.Merge(dt);
    //        rf.GetReport(rpt.GetType());
    //        rpt.Load(Server.MapPath("EgChallanViewRpt.rpt"));
    //        rpt.SetDataSource(dt2);

    //        CrystalReportViewer1.ReportSource = rpt;
    //        CrystalReportViewer1.DataBind();
    //        #region CreatePDF
    //        ExportOptions CrExportOptions;
    //        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
    //        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
    //        CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("EgChallanViewRpt.pdf");
    //        CrExportOptions = rpt.ExportOptions;
    //        {
    //            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
    //            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
    //            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
    //            CrExportOptions.FormatOptions = CrFormatTypeOptions;
    //        }
    //        rpt.Export();
    //        FileStream fs = null;
    //        fs = File.Open(Server.MapPath("EgChallanViewRpt.pdf"), System.IO.FileMode.Open);
    //        if (fs.Length > 0)
    //        {
    //            byte[] btFile = new byte[fs.Length];
    //            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
    //            fs.Close();
    //            Response.ClearContent();
    //            Response.ClearHeaders();
    //            Response.AddHeader("cache-control", "max-age=1");
    //            Response.ContentType = "application/pdf";
    //            Response.AddHeader("Content-disposition", "attachment; filename=" + ("EgChallanViewRpt.pdf"));
    //            Response.AppendHeader("Accept-Ranges", "none");
    //            if (btFile.Length > 0)
    //            {
    //                Response.BinaryWrite(btFile);
    //                Response.Flush();
    //                HttpContext.Current.ApplicationInstance.CompleteRequest();
    //                fs.Dispose();
    //                fs = null;
    //                File.Delete(Server.MapPath("EgChallanViewRpt.pdf"));  // delete file which has created
    //                Response.End();
    //                //Response.Redirect("Eghome.aspx");
    //                //HttpContext.Current.ApplicationInstance.CompleteRequest();
    //                //return;
    //            }
    //        }
    //        #endregion

    //    }



    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Record not found.');", true);
    //    }

    //}


    //Added by Rachit Sharma
    public void loadreport()
    {

        ReportParameter[] param = new ReportParameter[4];
        param[0] = new ReportParameter("UserId", Convert.ToString(Session["UserId"]).Trim());
        param[1] = new ReportParameter("Usertype", Convert.ToString(Session["UserType"]).Trim());
        param[2] = new ReportParameter("GRN", Convert.ToString(GRN));
        param[3] = new ReportParameter("ChallanNo", Convert.ToString(0));
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(DefaceDetailSSRS, "EgDefaceDetailNew", param);
        ////create PDF
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;
        returnValue = DefaceDetailSSRS.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        if (Response.IsClientConnected)//An error occurred while communicating with the remote host. The error code is 0x80070057.
        {
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=EgDefaceDetailNew.pdf");
            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }

    }


}