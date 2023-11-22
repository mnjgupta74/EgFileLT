using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CrystalDecisions;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;

namespace EgBL
{
  public class ReportFactory
  {
    protected static Queue reportQueue = new Queue();

    public  ReportDocument CreateReport(Type reportDoc)
    {
      object report = Activator.CreateInstance(reportDoc);
      reportQueue.Enqueue(report);
      return (ReportDocument)report;
    }

    public ReportDocument GetReport(Type reportDoc)
    {
      //40 is my print job limit.
      if (reportQueue.Count > 25) ((ReportDocument)reportQueue.Dequeue()).Dispose();
      return CreateReport(reportDoc);
    }
  }
}


