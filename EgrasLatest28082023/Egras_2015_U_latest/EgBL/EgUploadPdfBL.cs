using System;
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
        //PDF Document Upload Properties
        public byte[] PdfByte { get; set; }
        public string FileName { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }
        public bool Flag { get; set; }
        public string InsertionType { get; set; }
        #endregion
        /// <summary>
        /// Circular Document Upload with Btyes 6 May 2022
        /// </summary>
        /// <returns></returns>
        #region Class Functions
        public int InsertData()
        {
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@PdfPath", SqlDbType.VarChar, 100) { Value = PdfPath };
            PARM[1] = new SqlParameter("@PdfByte", SqlDbType.VarBinary) { Value = PdfByte };
            PARM[2] = new SqlParameter("@PdfName", SqlDbType.VarChar, 100) { Value = PdfName };
            return gf.UpdateData(PARM, "EgUploadPDF");
        }
        /// <summary>
        /// Document  Share by Url to Requestes Client 6 May 2022
        /// </summary>
        /// <returns></returns>
        //PDF Document Upload Method
        public int InsertDocument()
        {
            SqlParameter[] PARM = new SqlParameter[6];
            PARM[0] = new SqlParameter("@FileName", SqlDbType.VarChar, 250) { Value = FileName };
            PARM[1] = new SqlParameter("@PdfByte", SqlDbType.VarBinary) { Value = PdfByte };
            PARM[2] = new SqlParameter("@Id", SqlDbType.Int) { Value = Id };
            PARM[3] = new SqlParameter("@Type", SqlDbType.Int) { Value = Type };
            PARM[4] = new SqlParameter("@Flag", SqlDbType.Bit) { Value = Flag };
            PARM[5] = new SqlParameter("@InsertionType", SqlDbType.Char, 2);
            PARM[5].Value = InsertionType;
            PARM[5].Direction = ParameterDirection.Output;
            int x = gf.UpdateData(PARM, "EgDocumentUpload");
            InsertionType = PARM[5].Value.ToString();
            return Convert.ToInt32(InsertionType.ToString());
        }
        /// <summary>
        /// Get Bytes Code
        /// </summary>
        /// <returns></returns>
        public DataSet GetFiles()
        {
            try
            {
                DataSet ds = new DataSet();
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[2];
                PARM[0] = new SqlParameter("@FileName", SqlDbType.VarChar, 250) { Value = FileName };
                PARM[1] = new SqlParameter("@Type", SqlDbType.Int) { Value = Type };
                DataSet DS = gf.Filldatasetvalue(PARM, "EgGetDocument", ds, null);
                return ds;
            }
            catch (Exception ex)
            {
                //result = "Request Unable To Process !";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                throw new Exception("Due to Some Technical Issue File Could not Be Upload");
            }
        }
        #endregion
    }
}
