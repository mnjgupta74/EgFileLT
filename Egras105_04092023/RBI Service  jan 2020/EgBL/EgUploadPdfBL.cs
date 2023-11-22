using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
   public class EgUploadPdfBL
    {
       GenralFunction gf = new GenralFunction();
        #region Class Properties
      
        public string PdfPath { get; set; }
        public string PdfName { get; set; }
        #endregion

        #region Class Functions
        public int InsertData()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@PdfPath", SqlDbType.VarChar,100) { Value = PdfPath };
            PARM[1] = new SqlParameter("@PdfName", SqlDbType.VarChar, 100) { Value = PdfName };
            return gf.UpdateData(PARM, "EgUploadPDF");
        }
        #endregion
    }
}
