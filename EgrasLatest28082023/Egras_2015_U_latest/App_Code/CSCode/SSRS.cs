using EgBL;
using Microsoft.Reporting.WebForms;

/// <summary>
/// Summary description for SSRS
/// </summary>
public class SSRS
{
    public SSRS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void LoadSSRS(ReportViewer SSRSreport, string RptName, ReportParameter[] Param)
    {
       
        string UName = System.Configuration.ConfigurationManager.AppSettings["UName"];
        string PWD = System.Configuration.ConfigurationManager.AppSettings["PWD"];
        string DOM = System.Configuration.ConfigurationManager.AppSettings["DOM"];

        string ReportServerUrl = System.Configuration.ConfigurationManager.AppSettings["ReportServerUrl"];
        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"];
        string ReportName = RptName;

        SSRSreport.ShowCredentialPrompts = false;
        SSRSreport.ServerReport.ReportServerCredentials = new ReportCredentials(UName, PWD, DOM);
        SSRSreport.ProcessingMode = ProcessingMode.Remote;
        SSRSreport.ServerReport.ReportServerUrl = new System.Uri(ReportServerUrl);
        SSRSreport.ServerReport.ReportPath = ReportPath + ReportName;


        SSRSreport.ShowParameterPrompts = false;
        SSRSreport.ServerReport.SetParameters(Param);
        SSRSreport.ServerReport.Refresh();
    }


}
