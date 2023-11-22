using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Reporting.WebForms;
using EgBL;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;
using System.Data;

namespace EgBL
{
    public class EgAGSignedReportBL
    {
        private string _TreasuryCode;

        public string TreasuryCode
        {
            get { return _TreasuryCode; }
            set { _TreasuryCode = value; }
        }

        string result = "";
        int resultop = 0;


        public string Mode { get; set; }
        public string FName { get; set; }
        public int YYYY { get; set; }
        public Int16 MM { get; set; }
        public Int16 Phase { get; set; }
        public Int16 SignType { get; set; }
        public string ReqSign { get; set; }
        public Int64 ID { get; set; }
        public byte[] eSignData { get; set; }
        public string DSCUserName { get; set; }
        public string DSCValidDate { get; set; }
        public string DSCSerialKey { get; set; }
        public string Thumbprint { get; set; }
        public string Flag { get; set; }
        public string InsertionType { get; set; }
        public int DocID { get; set; }
        public string IPAddress { get; set; }

        public string ResponseMsg { get; set; }

        public int UserId { get; set; }
        public byte[] UnSignData { get; set; }
        public string Error { get; set; }

        public int Type { get; set; }
        ////////////////////////////// eSign code statrt

        public int SavePDF()//Manoj Gupta
        {

            SqlParameter[] PM = new SqlParameter[11];
            PM[0] = new SqlParameter("@Mode", SqlDbType.Char, 3);
            PM[0].Value = Mode;
            PM[1] = new SqlParameter("@FName", SqlDbType.VarChar, 100);
            PM[1].Value = FName;

            PM[2] = new SqlParameter("@YYYY", SqlDbType.Int);
            PM[2].Value = YYYY;

            PM[3] = new SqlParameter("@MM", SqlDbType.TinyInt);
            PM[3].Value = MM;

            PM[4] = new SqlParameter("@Phase", SqlDbType.TinyInt);
            PM[4].Value = Phase;
            PM[5] = new SqlParameter("@RegSign", SqlDbType.Char, 3);
            PM[5].Value = ReqSign;
            PM[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 15);
            PM[6].Value = IPAddress;


            PM[7] = new SqlParameter("@UserId", SqlDbType.Int);
            PM[7].Value = UserId;
            PM[8] = new SqlParameter("@DocID", SqlDbType.Int);
            PM[8].Value = DocID;
            PM[9] = new SqlParameter("@InsertionType", SqlDbType.Char, 2);
            PM[9].Value = InsertionType;
            PM[9].Direction = ParameterDirection.Output;
            PM[10] = new SqlParameter("@UnSignData", SqlDbType.VarBinary);
            PM[10].Value = UnSignData;


            GenralFunction GF = new GenralFunction();
            int x = GF.UpdateData(PM, "EgSignedPDF");
            InsertionType = PM[9].Value.ToString();
            if (Convert.ToInt16(InsertionType) == -1)
            {
                return -1;
            }
            else if (Convert.ToInt16(InsertionType) == 1)
            {
                return 1;
            }
            else if (Convert.ToInt16(InsertionType) == 2)
            {
                return 2;
            }
            else
            {
                return 0;
            }

        }

        public DataSet GetFilesGrid() //// For  eSign Page Grid main Bind
        {
            try
            {
                DataSet ds = new DataSet();
                GenralFunction GF = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[6];
                PM[0] = new SqlParameter("@Mode", SqlDbType.Char, 3);
                PM[0].Value = Mode;
                PM[1] = new SqlParameter("@YYYY", SqlDbType.Int);
                PM[1].Value = YYYY;
                PM[2] = new SqlParameter("@MM", SqlDbType.TinyInt);
                PM[2].Value = MM;
                PM[3] = new SqlParameter("@Phase", SqlDbType.TinyInt);
                PM[3].Value = Phase;
                PM[4] = new SqlParameter("@RegSign", SqlDbType.Char, 3);
                PM[4].Value = ReqSign;

                DataSet DS = GF.Filldatasetvalue(PM, "EgGetSingedPdf", ds, null);

                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception("A DAL Exception occurred", ex);
            }
        }

        /// <summary>
        /// Create Date 28 Oct 2020 
        /// </summary>
        /// Sign file Send to Ftp
        /// <returns></returns>
        public int FTPTransfer()
        {
            SqlParameter[] PM = new SqlParameter[0];
            GenralFunction GF = new GenralFunction();
            int x = GF.UpdateData(PM, "EgSendToFTP");
            if (x == 1)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        public DataSet DownloadFile() //// For  eSign Page Grid main Bind
        {

            try
            {
                DataSet ds = new DataSet();
                GenralFunction GF = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[2];
                PM[0] = new SqlParameter("@Mode", SqlDbType.Char, 3);
                PM[0].Value = Mode;
                PM[1] = new SqlParameter("@ID", SqlDbType.Int);
                PM[1].Value = ID;
                DataSet DS = GF.Filldatasetvalue(PM, "EgViewSignedPdf", ds, null);


                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception("A DAL Exception occurred", ex);
            }




        }


        public string eSignFRZ()
        {
            try
            {

                try
                {

                    GenralFunction GF = new GenralFunction();
                    SqlParameter[] PM = new SqlParameter[11];
                    PM[0] = new SqlParameter("@Mode", SqlDbType.Char, 3);
                    PM[0].Value = Mode;
                    PM[1] = new SqlParameter("@ID", SqlDbType.BigInt);
                    PM[1].Value = ID;
                    PM[2] = new SqlParameter("@FName", SqlDbType.VarChar, 100);
                    PM[2].Value = FName;
                    PM[3] = new SqlParameter("@DSCUserName", SqlDbType.VarChar, 50);
                    PM[3].Value = DSCUserName;
                    PM[4] = new SqlParameter("@DSCValidDate", SqlDbType.VarChar, 50);
                    PM[4].Value = DSCValidDate;
                    PM[5] = new SqlParameter("@DSCSerialKey", SqlDbType.VarChar, 250);
                    PM[5].Value = DSCSerialKey;
                    PM[6] = new SqlParameter("@Thumbprint", SqlDbType.VarChar, 250);
                    PM[6].Value = Thumbprint;
                    PM[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
                    PM[7].Value = IPAddress;
                    PM[8] = new SqlParameter("@UserId", SqlDbType.Int);
                    PM[8].Value = UserId;
                    PM[9] = new SqlParameter("@eSignData", SqlDbType.VarBinary);
                    PM[9].Value = eSignData;
                    PM[10] = new SqlParameter("@Type", SqlDbType.Int);
                    PM[10].Value = Type;
                    string rst = GF.UpdateData(PM, "EgSignedPDFLog").ToString();


                    return rst;
                }
                catch (Exception ex)
                {
                    throw new Exception("A DAL Exception occurred", ex);
                }



            }
            catch (Exception ex)
            {
                throw new Exception("A DAL Exception occurred", ex);

            }

        }

        // 21 Jan 2021 SignedPdf Page for Download
        public DataSet DownloadFile_BankUpload() //// For  eSign Page Grid main Bind
        {

            try
            {
                DataSet ds = new DataSet();
                GenralFunction GF = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[2];
                PM[0] = new SqlParameter("@Mode", SqlDbType.Char, 3);
                PM[0].Value = Mode;
                PM[1] = new SqlParameter("@ID", SqlDbType.VarChar);
                PM[1].Value = ID;
                DataSet DS = GF.Filldatasetvalue(PM, "EgViewSignedPdf_DMSScroll", ds, null);


                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception("A DAL Exception occurred", ex);
            }




        }

        // Use On signPdfPage for Display
        public DataSet GetFilesGrid_BankUpload() //// For  eSign Page Grid main Bind
        {
            try
            {
                DataSet ds = new DataSet();
                GenralFunction GF = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[2];
                PM[0] = new SqlParameter("@FileYear", SqlDbType.Int);
                PM[0].Value = YYYY;
                PM[1] = new SqlParameter("@FileMonth", SqlDbType.TinyInt);
                PM[1].Value = MM;

                DataSet DS = GF.Filldatasetvalue(PM, "EgGetDMSScrollUpload", ds, null);

                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception("A DAL Exception occurred", ex);
            }
        }

    }
}
