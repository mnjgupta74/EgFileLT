using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{

    public class EgNewUpdates
    {
        GenralFunction gf = new GenralFunction();
        public string PdfName { get; set; }
        public string PdfPath { get; set; }
        public int pdfid { get; set; }
        public int Flag { get; set; }

        public int InsertData()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@PdfPath", SqlDbType.VarChar, 100) { Value = PdfPath };
            PARM[1] = new SqlParameter("@PdfName", SqlDbType.VarChar, 200) { Value = PdfName };
            return gf.UpdateData(PARM, "EgNewUpdates_Sp");
        }

        public void NewUpdatesPdf(Repeater rpt)
        {
            gf = new GenralFunction();
           // PagedDataSource objpds = new PagedDataSource();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@Flag", SqlDbType.Int) { Value = Flag };
            dt = gf.Filldatatablevalue(PARM, "EgGetNewUpdatePdf", dt, null);
            //DataView objdv = new DataView(dt);
            //objpds.DataSource = objdv;
            if (dt.Rows.Count > 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
                dt.Dispose();
            }
            else
            {
                rpt.DataSource = null;
                rpt.DataBind();
            }

        }
        public int UpdateCheck()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@pdfid", SqlDbType.Int) { Value = pdfid };
            PARM[1] = new SqlParameter("@Flag", SqlDbType.Int) { Value = Flag };
            return gf.UpdateData(PARM, "EgNewUpdates_Check");
        }

    }

}
