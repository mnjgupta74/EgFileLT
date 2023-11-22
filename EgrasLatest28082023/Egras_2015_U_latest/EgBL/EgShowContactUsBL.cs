using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgShowContactUsBL
    {
        public string ContactId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }

        #region Class Functions



        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            GenralFunction gf = new GenralFunction();
            SqlParameter PARM = new SqlParameter();
            return gf.Filldatatablevalue(null, "EgShowContactDetails", dt, null);
        }

        #endregion




    }
}
