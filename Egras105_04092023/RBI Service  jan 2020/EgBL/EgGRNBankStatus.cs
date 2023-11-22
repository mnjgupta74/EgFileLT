using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgGRNBankStatus
    {
        GenralFunction gf;
        public string BankCode { get; set; }
        public Int64 GRN { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Int16 rblval { get; set; }
        public int NoofDaysDiff { get; set; }

        //public object sumofobject { get; set; }

        //public DateTime expdate;
        //public DateTime bankchalndate;

        public void PopulateBankList(DropDownList ddlbank) // fill department Droddownlist
        {
            var page = (Page)HttpContext.Current.CurrentHandler;
            string url = page.AppRelativeVirtualPath;
            gf = new GenralFunction();
            if (url.Contains("EgDMSScrollPDF.aspx"))
            {
                SqlParameter[] PARM = new SqlParameter[2];
                PARM[0] = new SqlParameter("@Userid", SqlDbType.BigInt) { Value = Convert.ToInt64(System.Web.HttpContext.Current.Session["UserId"]) };
                PARM[1] = new SqlParameter("@Usertype", SqlDbType.Int) { Value = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserType"]) };
                EgEChallanBL objE = new EgEChallanBL();
                DataTable dt = objE.GetChallanBank();
                var rows = dt.AsEnumerable().Where(t => t.Field<string>("access").Trim() == "Y");
                dt = rows.Any() ? rows.CopyToDataTable() : dt.Clone(); //dt.AsEnumerable().Where(t => t.Field<string>("access").Trim() == "Y").CopyToDataTable();


                ddlbank.DataSource = dt;
                ddlbank.DataTextField = "BankName";
                ddlbank.DataValueField = "BSRCode";
                ddlbank.DataBind();
                ddlbank.Items.Insert(0, new ListItem("--ALL Bank--", "1"));
                ddlbank.Items.Insert(1, new ListItem("RBI", "0000000"));
                //gf.FillListControl(ddlbank, "EgGetBanks_Reports", "BankName", "BSRCode", PARM);
                //ddlbank.Items.Insert(0, new ListItem("--Select Bank--", "0"));

            }
            else
            {
               
                SqlParameter[] PARM = new SqlParameter[1];
                gf.FillListControl(ddlbank, "EgFillBank", "BankName", "BSRCode", null);
                ddlbank.Items.Insert(0, new ListItem("--Select Bank--", "0"));
            }
        }

        public bool BankdetailRptBind(Repeater rpt)
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@BankCode", SqlDbType.VarChar) { Value = BankCode };
            PARM[1] = new SqlParameter("@Type", SqlDbType.TinyInt) { Value = rblval };
            PARM[2] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[3] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            dt = gf.Filldatatablevalue(PARM, "EgLateReportedGrnListBankWise", dt, null);
            //sumofobject = dt.Compute("Amount", "");
            //sumofobject = string.Format("{0:0.00}", sumofobject);
            if (dt.Rows.Count > 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
                dt.Dispose();
                return true;
            }
            else
            {
                rpt.DataSource = null;
                rpt.DataBind();
                return false;
                // Response.Write("<script>alert('No data Found');</script>");
                //System.Windows.Forms.MessageBox.Show("No data Found");
            }

        }

        public DataTable BankRptBind()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@BankCode", SqlDbType.VarChar) { Value = BankCode };
            PARM[1] = new SqlParameter("@NoofDaysDiff", SqlDbType.TinyInt) { Value = NoofDaysDiff };
            PARM[2] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[3] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = ToDate };
            return dt = gf.Filldatatablevalue(PARM, "EgLateReportedGrnListGroupBYDays", dt, null);
        
            //if (dt.Rows.Count > 0)
            //{
            //    rptgrnbankstatus.DataSource = dt;
            //    rptgrnbankstatus.DataBind();
            //    dt.Dispose();               
            //}
            //else
            //{
            //    rptgrnbankstatus.DataSource = null;
            //    rptgrnbankstatus.DataBind();
            //}

        }
    }
}
