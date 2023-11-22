using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using EgBL;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class ContactUs : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindDummyItem();
        }
    }

    private void BindDummyItem()
    {
        DataTable dummy = new DataTable();
        dummy.Columns.Add("Name");
        dummy.Columns.Add("ContactNo");
        int count = dlCustomers.RepeatColumns == 0 ? 1 : dlCustomers.RepeatColumns;
        for (int i = 0; i < count; i++)
        {
            dummy.Rows.Add();
        }
        dlCustomers.DataSource = dummy;
        dlCustomers.DataBind();
    }

    [WebMethod]
    public static EgShowContactUs[] GetContactDetail()
    {
        EgShowContactUsBL ObjShowdata = new EgShowContactUsBL();
        DataTable dt = new DataTable();
        dt = ObjShowdata.GetData();
        List<EgShowContactUs> Details = new List<EgShowContactUs>();



        foreach (DataRow row in dt.Rows)
        {
            EgShowContactUs objShw = new EgShowContactUs();

            objShw.Name = row.Field<string>("Name");
            objShw.ContactNo = row.Field<string>("ContactNo");
            Details.Add(objShw);

        }
        return Details.ToArray();

    }

    //private static DataSet GetData(SqlCommand cmd)
    //{
    //    GenralFunction gf = new GenralFunction();
    //    SqlParameter PARM = new SqlParameter();
    //    DataSet ds = new DataSet();
    //    return gf.Filldatasetvalue(null, "EgShowContactDetails", ds, null);
    //}
    public class EgShowContactUs
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }

    }
    //protected string setClass(int ContactId)
    //{
    //    string classToApply = string.Empty;
    //    if (ContactId == 16)
    //    {
    //        classToApply = "blueRow"; //== Here blue row is the name of the CSS class I have created.
    //    }
    //    return setClass();
    //}
}
