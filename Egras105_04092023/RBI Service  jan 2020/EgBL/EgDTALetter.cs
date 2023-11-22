using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;


namespace EgBL
{
    public class EgDTALetter
    {
        GenralFunction gf = new GenralFunction();
        /// </summary>
        public string SerialNo { get; set; }
        public string LetterName { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public string Remarks { get; set; }
        public int FileYear { get; set; }
        public Int16 FileMonth { get; set; }
        public string BsrCode { get; set; }
        public int UserId { get; set; } // add userid for DMS Scroll PDF Table

        public byte[] eSignedBytes { get; set; }
        public string InsertionType { get; set; }
        //public Int16 Type { get; set; }

        // Get Deface And Refund  Detail Date WiSE or gRN wISE
        public int InsertLetterData()
        {
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@SerialNo", SqlDbType.Int) { Value = SerialNo };
            PARM[1] = new SqlParameter("@LetterName", SqlDbType.VarChar) { Value = LetterName };
            PARM[2] = new SqlParameter("@Status", SqlDbType.VarChar) { Value = Status };
            PARM[3] = new SqlParameter("@Subject", SqlDbType.VarChar) { Value = Subject };
            PARM[4] = new SqlParameter("@Remarks", SqlDbType.VarChar) { Value = Remarks };
            return gf.UpdateData(PARM, "EgInsertDTALetterData");
            //DataTable dt = new DataTable();
            //dt = gf.Filldatatablevalue(PARM, "EgInsertDTALetterData", dt, null);
            //string JSONString = string.Empty;
            //JSONString = JsonConvert.SerializeObject(dt);
            //return JSONString;
        }
        public int InsertDMSPDF()
        {
            SqlParameter[] PARM = new SqlParameter[7];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@FileYear", SqlDbType.Int) { Value = FileYear };
            PARM[2] = new SqlParameter("@FileMonth", SqlDbType.TinyInt) { Value = FileMonth };
            PARM[3] = new SqlParameter("@BsrCode", SqlDbType.VarChar, 20) { Value = BsrCode };
            PARM[4] = new SqlParameter("@FileName", SqlDbType.VarChar, 250) { Value = LetterName };            
            PARM[5] = new SqlParameter("@eSignedBytes", SqlDbType.VarBinary) { Value = eSignedBytes };
            PARM[6] = new SqlParameter("@InsertionType", SqlDbType.Char, 2);
            PARM[6].Value = InsertionType;
            PARM[6].Direction = ParameterDirection.Output;
            int x = gf.UpdateData(PARM, "EgDMSScrollPDFUpload");
            InsertionType = PARM[6].Value.ToString();
            if (Convert.ToInt16(InsertionType) == -1)
            {
                return -1;
            }
            else
            {
                return 1;
            }

        }
        public DataSet GetFiles()
        {

            try
            {
                DataSet ds = new DataSet();
                GenralFunction GF = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[4];
                PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
                PARM[1] = new SqlParameter("@FileYear", SqlDbType.Int) { Value = FileYear };
                PARM[2] = new SqlParameter("@FileMonth", SqlDbType.TinyInt) { Value = FileMonth };
                PARM[3] = new SqlParameter("@BsrCode", SqlDbType.VarChar, 20) { Value = BsrCode };
                DataSet DS = GF.Filldatasetvalue(PARM, "EgDMSScrollPDFDownload", ds, null);
                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception("A DAL Exception occurred", ex);
            }
        }
    }
}
