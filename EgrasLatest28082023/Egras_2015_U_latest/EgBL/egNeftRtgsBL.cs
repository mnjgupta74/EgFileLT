using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using System.Runtime.Serialization;

namespace EgBL
{
    public class egNeftRtgsBL : ResponsePayload
    {

        GenralFunction gf = new GenralFunction();
        ASCIIEncoding ByteConverter = new ASCIIEncoding();
        RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
        //ResponsePayload objResponsePayload = new ResponsePayload();
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Signature { get; set; }
        public string Status_cd { get; set; }
        public string CPIN { get; set; }
        public string UTR { get; set; }
        public string RTN_CODE { get; set; }
        public string RTN_DESC { get; set; }
        public string TotalAmt { get; set; }

        public string PaymentDtTm { get; set; }
        public string RequestDtTm { get; set; }
        public string BankRefNo { get; set; }
        public string ErrorCode { get; set; }
        public string certificatePath { get; set; }
        public string IPAddress { get; set; }
        public string JSONString { get; set; }

        private string _description;
        public string Description
        {
            get
            {
                switch (ErrorCode)
                {
                    case "CPNT0001":
                        _description = "Malformed Request(mandatory fields are missing)";
                        break;
                    case "CPNT0002":
                        _description = "Signature validation failed";
                        break;
                    case "CPNT0003":
                        _description = "Technical error";
                        break;
                    case "CPNB0001":
                        _description = "Mismatcha b.w total amount and breakup amount";
                        break;
                    case "CPNB0002":
                        _description = "Error in Admin Zone";
                        break;
                    case "CPNB0003":
                        _description = "Error in Account ID";
                        break;
                    case "CPNB0004":
                        _description = "Admin Zone and Account ID mismatch";
                        break;
                    case "CPNB0005":
                        _description = "Invalid CPIN";
                        break;
                    case "CPNB0006":
                        _description = "Expired CPIN";
                        break;
                    case "CPNB0007":
                        _description = "Challan already paid";
                        break;

                    case "PYMT0001":
                        _description = "Invalid amount";
                        break;
                    case "PYMT0002":
                        _description = "Invalid Bank Reference details";
                        break;
                    case "PYMT0003":
                        _description = "Invalid UTR";
                        break;
                    default:

                        break;
                }
                return _description;
            }
            set { _description = value; }
        }

        public string InsertRBIResponse()
        {
            string str = string.Empty;
            try
            {
                gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[2];
                PARM[0] = new SqlParameter("@JSONString", SqlDbType.NVarChar) { Value = JSONString };
                PARM[1] = new SqlParameter("@cpin", SqlDbType.BigInt) { Value = CPIN };
                return gf.UpdateData(PARM, "EgInsertNeftRgtsPushReq").ToString().Trim();
            }
            catch (Exception ex)
            {
                str = "CPNT0003";
            }
            return str;
        }
        public string InsertNEFTResponse()
        {
            string str = string.Empty;
            try
            {
                gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[7];
                PARM[0] = new SqlParameter("@System", SqlDbType.VarChar, 6) { Value = System };
                PARM[1] = new SqlParameter("@ServiceType", SqlDbType.VarChar, 11) { Value = ServiceType };
                PARM[2] = new SqlParameter("@Signature", SqlDbType.VarChar) { Value = Signature };
                PARM[3] = new SqlParameter("@Status_cd", SqlDbType.TinyInt) { Value = Status_cd };
                PARM[4] = new SqlParameter("@RESP_CODE", SqlDbType.VarChar, 20) { Value = RESP_CODE };
                PARM[5] = new SqlParameter("@RJCT_DESC", SqlDbType.VarChar, 100) { Value = RJCT_DESC };
                PARM[6] = new SqlParameter("@cpin", SqlDbType.VarChar, 20) { Value = CPIN };
                return gf.UpdateData(PARM, "EgInsertNeftRgtsPushResponse").ToString().Trim();
            }
            catch (Exception ex)
            {
                str = "CPNT0003";
            }
            return str;
        }
        public string CPINNotificationReq()
        {
            string str = string.Empty;
            try
            {
                gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[9];
                PARM[0] = new SqlParameter("@System", SqlDbType.VarChar, 6) { Value = System };
                PARM[1] = new SqlParameter("@ServiceType", SqlDbType.VarChar, 20) { Value = ServiceType };
                PARM[2] = new SqlParameter("@grn", SqlDbType.VarChar) { Value = CPIN };
                PARM[3] = new SqlParameter("@UTR", SqlDbType.VarChar) { Value = UTR };
                PARM[4] = new SqlParameter("@totalAmt", SqlDbType.VarChar, 20) { Value = TotalAmt };
                PARM[5] = new SqlParameter("@paymentDtTm", SqlDbType.VarChar, 100) { Value = PaymentDtTm };
                PARM[6] = new SqlParameter("@BankRefNo", SqlDbType.VarChar, 100) { Value = BankRefNo };
                PARM[7] = new SqlParameter("@Signature", SqlDbType.VarChar, 100) { Value = Signature };
                PARM[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 30) { Value = IPAddress };
                str = gf.UpdateData(PARM, "CPINServicetLog").ToString();
            }
            catch (Exception ex)
            {
                str = "CPNT0003";
                ErrorCode = str;
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("CPINNotificationReqCPIN For IPAddress= " + IPAddress + " and CPIN :- " + CPIN + " = " + ex.Message.ToString());
            }
            return str;
        }
        public string CheckReturnNotifyReq()
        {
            string str = string.Empty;
            try
            {
                gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[10];
                PARM[0] = new SqlParameter("@System", SqlDbType.VarChar, 6) { Value = System };
                PARM[1] = new SqlParameter("@ServiceType", SqlDbType.VarChar, 20) { Value = ServiceType };
                PARM[2] = new SqlParameter("@grn", SqlDbType.VarChar) { Value = CPIN };
                PARM[3] = new SqlParameter("@UTR", SqlDbType.VarChar) { Value = UTR };
                PARM[4] = new SqlParameter("@totalAmt", SqlDbType.VarChar, 20) { Value = TotalAmt };
                PARM[5] = new SqlParameter("@requestDtTm", SqlDbType.VarChar, 100) { Value = PaymentDtTm };
                PARM[6] = new SqlParameter("@BankRefNo", SqlDbType.VarChar, 100) { Value = BankRefNo };
                PARM[7] = new SqlParameter("@Signature", SqlDbType.VarChar, 100) { Value = Signature };
                PARM[8] = new SqlParameter("@RTN_CODE", SqlDbType.VarChar, 100) { Value = RTN_CODE };
                PARM[9] = new SqlParameter("@RTN_DESC", SqlDbType.VarChar, 100) { Value = RTN_DESC };
                str = gf.UpdateData(PARM, "CPINServicetLog").ToString();
            }
            catch (Exception ex)
            {
                str = "CPNT0003";
                ErrorCode = str;
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("CheckReturnNotifyReq For IPAddress= " + IPAddress + " and CPIN :- " + CPIN + " = " + ex.Message.ToString());
            }
            return str;
        }
        public DataTable GetPushPullJsonData(string GRN, string System, string ServiceType)
        {
            DataTable dt = new DataTable();
            try
            {
                gf = new GenralFunction();
                string signaturedata = string.Empty;
                SqlParameter[] PM = new SqlParameter[3];
                PM[0] = new SqlParameter("@grn", SqlDbType.BigInt) { Value = GRN };
                PM[1] = new SqlParameter("@System", SqlDbType.VarChar, 6) { Value = System };
                PM[2] = new SqlParameter("@ServiceType", SqlDbType.VarChar, 11) { Value = ServiceType };
                dt = gf.Filldatatablevalue(PM, "CPINHeader", dt, null);
                ErrorCode = dt.Rows.Count > 0 ? "0" : "CPNB0005";
            }
            catch (Exception ex)
            {
                ErrorCode = "CPNT0003";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("GetPushPullJsonData   For IPAddress= " + IPAddress + " and CPIN :- " + GRN + " = " + ex.Message.ToString());
            }
            return dt;
        }
        public string CheckNotifyReqData()
        {
            string str = string.Empty;
            try
            {
                //DataTable dt = new DataTable();
                gf = new GenralFunction();

                SqlParameter[] PM = new SqlParameter[5];
                PM[0] = new SqlParameter("@grn", SqlDbType.BigInt) { Value = CPIN };
                PM[1] = new SqlParameter("@totalAmt", SqlDbType.VarChar, 20) { Value = TotalAmt };
                PM[2] = new SqlParameter("@paymentDtTm", SqlDbType.VarChar, 30) { Value = PaymentDtTm };
                PM[3] = new SqlParameter("@BankRefNo", SqlDbType.VarChar, 50) { Value = BankRefNo };
                PM[4] = new SqlParameter("@UTR", SqlDbType.VarChar, 50) { Value = UTR };
                str = gf.ExecuteScaler(PM, "CPINNotificationReqData");
            }
            catch (Exception ex)
            {
                str = "CPNT0003";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("CheckNotifyReqData   For IPAddress= " + IPAddress + " and CPIN :- " + CPIN + " = " + ex.Message.ToString());
            }
            return str;
        }
        public DataTable CheckReturnNotifyReqData()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            string signaturedata = string.Empty;
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@grn", SqlDbType.BigInt) { Value = CPIN };
            PM[1] = new SqlParameter("@totalAmt", SqlDbType.VarChar, 20) { Value = TotalAmt };
            PM[2] = new SqlParameter("@requestDtTm", SqlDbType.VarChar, 30) { Value = PaymentDtTm };
            PM[3] = new SqlParameter("@BankRefNo", SqlDbType.VarChar, 50) { Value = BankRefNo };
            PM[4] = new SqlParameter("@RTN_CODE", SqlDbType.VarChar, 50) { Value = RTN_CODE };
            dt = gf.Filldatatablevalue(PM, "CPINReturnNotificationReqData", dt, null);
            return dt;
        }
        public string Verifysignature(byte[] Data, byte[] SignatureData)
        {

            string pubKeyString1 = "<?xml version=\"1.0\" encoding=\"utf-16\"?><RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Exponent>AQAB</Exponent><Modulus>8qT8svCYd6HSta+6kQFapNwD8A3nZwbdwQ6eSiX3fhvn9acfT7nu/xtmETw91F44NzcVwbwOlg+fleNXfYAeG6o49IZD/2MScKayoSFGxOckisXYx6ionTLy4F3rgdjcw8w8TIB6FU3Pc/q/K2ImM2ZVRAMWdPvHSyEsu77nCVTunRJBuza/1/7khWGMm/1VOG4++xbs/NSySY1wQ+SKheg1KcKR/F9G1DNCssLuwh58Vj7NvNOoFGVZOePrhmvb8HPkT71P83JYdJzGFrgyWrP5nuHnLfc1Kfcln1djuV6DcgzQbYdyFdJ71EMB9lYkNcMGBAKwXGP7PEnG9Gcy/Q==</Modulus></RSAParameters>";

            //byte[] SignatureData = ByteConverter.GetBytes(Signature);
            //Verify the data and display the result to the
            // console.
            //RSAParameters publickey = RSAalg.ExportParameters(false);
            //string CertPath = ConfigurationManager.AppSettings["NEFTCertPath"].ToString();// (System.Configuration.ConfigurationManager.AppSettings["NEFTCertPath"]);
            //X509Certificate2 cert = new X509Certificate2(CertPath, "123", X509KeyStorageFlags.Exportable);


            //byte[] encodedPublicKey = cert.PublicKey.EncodedKeyValue.RawData;
            //get a stream from the string
            var sr = new System.IO.StringReader(pubKeyString1);
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            var pubKey1 = (RSAParameters)xs.Deserialize(sr);
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubKey1);
            RSAParameters rsaParam = csp.ExportParameters(false);
            //RSAParameters rsaParam = rsa.ExportParameters(false);

            if (VerifySignedHash(Data, SignatureData, rsaParam))
            {
                return "0";
            }
            else
            {
                ErrorCode = "CPNT0002";
                return "1";
            }
        }
        public byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the 
                // key from RSAParameters.  
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(2048);

                RSAalg.ImportParameters(Key);

                // Hash and sign the data. Pass a new instance of SHA256CryptoServiceProvider
                // to specify the use of SHA256 for hashing.
                return RSAalg.SignData(DataToSign, new SHA256CryptoServiceProvider());
            }
            catch (CryptographicException e)
            {
                //Console.WriteLine(e.Message);
                return null;
            }
        }
        public bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, RSAParameters Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the 
                // key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(2048);

                RSAalg.ImportParameters(Key);

                // Verify the data using the signature.  Pass a new instance of SHA256CryptoServiceProvider
                // to specify the use of SHA256 for hashing.
                return RSAalg.VerifyData(DataToVerify, new SHA256CryptoServiceProvider(), SignedData);

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
        public string CPINPUSHREQ(Int64 GRN)
        {

            //CPINNotificationReq
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            //ResponsePayload objResponsePayload = new ResponsePayload();

            ////Push Data///////////
            //string GRN = "243";
            DataTable dt = GetPushPullJsonData(GRN.ToString(), "GORJ", "CPINPUSHREQ");
            string JSONString = string.Empty;


            int Rowcont = dt.Rows.Count;
            if (Rowcont > 0)
            {
                CPINHeader objCPINHeader = new CPINHeader();
                PayloadDetail objpay = new PayloadDetail();
                objCPINHeader.System = dt.Rows[0]["System"].ToString();
                objCPINHeader.ServiceType = dt.Rows[0]["ServiceType"].ToString();
                objpay.CPIN = dt.Rows[0]["GRN"].ToString();
                objpay.ExpDt = DateTime.Parse(dt.Rows[0]["ExpDt"].ToString()).ToString("yyyyMMdd");
                objpay.TotalAmt = string.Format("{0:0.0}", dt.Rows[0]["TotalAmount"]);
                //objpay.TotalAmt = "4.0";
                //objpay.PayerName = dt.Rows[0]["PayerName"].ToString();
                string signaturedata = objpay.CPIN.Trim() + "^" + objpay.ExpDt.Trim() + "^" + objpay.TotalAmt.Trim();// + "^" + objpay.PayerName.Trim();

                string challanDetailSignatureData = string.Empty;
                List<ChallanDetail> objChallanDetail = new List<ChallanDetail>();

                for (int row = 0; row < Rowcont; row++)
                {
                    ChallanDetail objchallan = new ChallanDetail();
                    objchallan.AcntID = dt.Rows[row]["AcntID"].ToString().Trim();//"114001001";//
                    objchallan.Amount = string.Format("{0:0.0}", dt.Rows[row]["Amount"]).Trim();
                    //objchallan.Amount= "4.0";
                    objchallan.AdminZone = dt.Rows[row]["AdminZone"].ToString().Trim();//"08";//
                    objChallanDetail.Add(objchallan);
                    challanDetailSignatureData = challanDetailSignatureData.Trim() + "^" + objchallan.AcntID.Trim() + "~" + objchallan.Amount.Trim() + "~" + objchallan.AdminZone.Trim();
                    //challanDetailSignatureData = challanDetailSignatureData + "^" + objchallan.AcntID + "~" + "4.0" + "~" + objchallan.AdminZone;
                }
                signaturedata = signaturedata.Trim() + challanDetailSignatureData.Trim();

                var str = signaturedata;
                /////////////Signing///////////////////////////
                // Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] originalData = ByteConverter.GetBytes(signaturedata);
                byte[] signedData;
                string CertPath = ConfigurationManager.AppSettings["NEFTCertPath"].ToString();
                X509Certificate2 cert = new X509Certificate2(CertPath, ConfigurationManager.AppSettings["CertificatePassword"].ToString(), X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;

                RSAParameters rsaParam = rsa.ExportParameters(true);//true=export only private key, false=public key export
                signedData = HashAndSignBytes(originalData, rsaParam);

                // Hash and sign the data.
                signaturedata = Convert.ToBase64String(signedData);
                objCPINHeader.Signature = signaturedata;
                objpay.ChallanDtls = objChallanDetail;
                objCPINHeader.Payload = objpay;
                JSONString = JsonConvert.SerializeObject(objCPINHeader);

                CPIN = objpay.CPIN;
                this.JSONString = JSONString;
                var responseInsert = InsertRBIResponse();
            }

            //string result = CPINPUSHREQDemo(GRN);
            return JSONString;
        }
        //public string CPINPUSHREQDemo(Int64 GRN)
        //{
        //    ASCIIEncoding ByteConverter = new ASCIIEncoding();
        //    string JSONString = string.Empty;
        //    CPINHeaderPullDemo objCPINHeader = new CPINHeaderPullDemo();
        //    PayloadDetailPullDemo objpay = new PayloadDetailPullDemo();
        //    objCPINHeader.System = "GOBH";
        //    objCPINHeader.ServiceType = "CPINPULLREQ";
        //    objpay.CPIN = GRN.ToString();
        //    string signaturedata = objpay.CPIN;// + "^" + objpay.ExpDt + "^" + objpay.TotalAmt;

        //    /////////////Signing///////////////////////////
        //    // Create byte arrays to hold original, encrypted, and decrypted data.
        //    byte[] originalData = ByteConverter.GetBytes(signaturedata);
        //    byte[] signedData;

        //    string CertPath = ConfigurationManager.AppSettings["NEFTPullCertPath"].ToString();// (System.Configuration.ConfigurationManager.AppSettings["NEFTCertPath"]);
        //    System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(CertPath);
        //    signedData = SignData(cert, originalData);
        //    // Hash and sign the data.
        //    signaturedata = Convert.ToBase64String(signedData);


        //    objCPINHeader.Signature = signaturedata;
        //    objCPINHeader.Payload = objpay;
        //    JSONString = JsonConvert.SerializeObject(objCPINHeader);
        //    //**************************************
        //    //byte[] s = SignData(cert, originalData);
        //    //signedData = SignData(cert, originalData);
        //    //VerifySignedData(originalData, signedData, cert);

        //    return JSONString;
        //}
        private byte[] SignData(X509Certificate2 certificate, byte[] dataToSign)
        {
            // get xml params from current private key
            var rsa = (RSA)certificate.PrivateKey;
            var xml = RSAHelper.ToXmlString(rsa, true);
            var parameters = RSAHelper.GetParametersFromXmlString(rsa, xml);

            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(2048);
            rsaCryptoServiceProvider.ImportParameters(parameters);

            // sign data
            var signedBytes = rsaCryptoServiceProvider.SignData(dataToSign, new SHA256CryptoServiceProvider());

            return signedBytes;
        }
        public bool VerifySignedData(byte[] originalData, byte[] signedData, X509Certificate2 certificate)
        {
            bool flag = false;
            using (var rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key)
            {
                if (rsa.VerifyData(originalData, new SHA256CryptoServiceProvider(), signedData))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }
        public string CPINPUSHRES(string json)
        {
            //List<ResponsePayload> Payload;
            //json = "{\"System\":\"GOBH\",\"ServiceType\":\"CPINPUSHRES\",\"Signature\":\"BXJDBCHDBHD\",\"Status_cd\":\"0\",\"Payload\":[{\"RESP_CODE\":\"CPNT0001\",\"RJCT_DESC\":\"Malfomed Request\"}]}";
            var result = JsonConvert.DeserializeObject<eChallanPushResParent>(json);

            EgErrorHandller obj = new EgErrorHandller();
            System = result.System;
            ServiceType = result.ServiceType;
            Signature = result.Signature;
            //verify signature
            Status_cd = result.Status_cd;
            RESP_CODE = result.payload.RESP_CODE;
            RJCT_DESC = result.payload.RJCT_DESC;
            var res = InsertNEFTResponse();
            return res.ToString();
        }


        public string CPINPULLREQ(CPINPullRequest result)
        {

            string JSONString = string.Empty;
            try
            {
                string challanDetailSignatureData = string.Empty;
                string signaturedata = string.Empty;
                byte[] originalData;
                byte[] signedData;
                byte[] sign;
                PayloadPullDetail objpay = new PayloadPullDetail();
                try
                {
                    System = result.System;
                    ServiceType = result.ServiceType;
                    Signature = result.Signature;
                    CPIN = result.Payload.CPIN;

                    DataTable dt = new DataTable();

                    var resp = CPINNotificationReq();

                    if (string.IsNullOrEmpty(Signature) ||
                        string.IsNullOrEmpty(CPIN) ||
                        string.IsNullOrEmpty(System) ||
                        string.IsNullOrEmpty(ServiceType))
                    {
                        ErrorCode = "CPNT0001";
                    }
                    else
                    {
                        bool verify = ValidatePULLReq(Signature, CPIN);
                        if (verify)
                        {
                            dt = GetPushPullJsonData(CPIN, "GOBH", "CPINPULLREQ");

                            if (dt.Rows.Count > 0)// CPIN PULL Acceceptance
                            {

                                objpay.CPIN = dt.Rows[0]["GRN"].ToString();
                                objpay.ExpDt = DateTime.Parse(dt.Rows[0]["ExpDt"].ToString()).ToString("yyyyMMdd");
                                objpay.TotalAmt = Convert.ToDecimal(dt.Rows[0]["TotalAmount"]).ToString("0.00");
                                objpay.PayerName = dt.Rows[0]["PayerName"].ToString();
                                signaturedata = objpay.CPIN + "^" + objpay.ExpDt + "^" + objpay.TotalAmt + "^" + objpay.PayerName;
                                List<ChallanDetail> objChallanDetail = new List<ChallanDetail>();
                                for (int row = 0; row < dt.Rows.Count; row++)
                                {
                                    ChallanDetail objchallan = new ChallanDetail();
                                    //objchallan.AcntID = "114001001";//dt.Rows[row]["AcntID"].ToString();
                                    objchallan.AcntID = dt.Rows[row]["AcntID"].ToString();
                                    objchallan.Amount = Convert.ToDecimal(dt.Rows[row]["Amount"]).ToString("0.00");
                                    objchallan.AdminZone = dt.Rows[row]["AdminZone"].ToString();
                                    //objchallan.AdminZone = "08"; //dt.Rows[row]["AdminZone"].ToString();
                                    objChallanDetail.Add(objchallan);
                                    challanDetailSignatureData = challanDetailSignatureData + "^" + objchallan.AcntID + "~" + objchallan.Amount + "~" + objchallan.AdminZone;
                                }
                                objpay.ChallanDtls = objChallanDetail;
                                signaturedata = signaturedata + challanDetailSignatureData;
                            }
                            else
                            {
                                ErrorCode = "CPNB0005";
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    ErrorCode = "CPNT0003";
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + result.Payload.CPIN + " :- " + ex.Message.ToString());
                }

                IPayload objResponsePayload;
                PULLACKCPINHeader objPULLNCKCPINHeader = new PULLACKCPINHeader();
                objPULLNCKCPINHeader.System = System;
                objPULLNCKCPINHeader.ServiceType = "CPINPULLRES";
                objPULLNCKCPINHeader.Status_cd = ErrorCode == "0" ? "1" : "0";
                if (ErrorCode == "0")
                {
                    objResponsePayload = new PayloadPullDetail();
                    objResponsePayload = objpay;

                    //objResponsePayload = new ResponseNotifyPayload();
                    //ResponseNotifyPayload obja = new ResponseNotifyPayload();
                    //obja.RESP_CODE = "Success";
                    //objResponsePayload = obja;
                    //signaturedata = obja.RESP_CODE.ToString();

                }
                else
                {
                    objResponsePayload = new ResponseNotifyFailPayload();
                    ResponseNotifyFailPayload obja = new ResponseNotifyFailPayload();
                    obja.RESP_CODE = ErrorCode;
                    obja.RJCT_DESC = Description;
                    objResponsePayload = obja;
                    signaturedata = obja.RESP_CODE + "^" + obja.RJCT_DESC;
                }

                sign = ByteConverter.GetBytes(signaturedata);
                /////////////Signing///////////////////////////
                // Create byte arrays to hold original, encrypted, and decrypted data.
                originalData = ByteConverter.GetBytes(signaturedata); 
                string CertPath = (ConfigurationManager.AppSettings["NEFTCertPath"]);

                X509Certificate2 cert = new X509Certificate2(CertPath, ConfigurationManager.AppSettings["CertificatePassword"], X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;
                RSAParameters rsaParam = rsa.ExportParameters(true);

                signedData = HashAndSignBytes(originalData, rsaParam);
                // Hash and sign the data.
                signaturedata = Convert.ToBase64String(signedData);
                objPULLNCKCPINHeader.Signature = signaturedata;
                objPULLNCKCPINHeader.Payload = objResponsePayload;
                JSONString = JsonConvert.SerializeObject(objPULLNCKCPINHeader);

                //CPIN = objpay.CPIN;
                this.JSONString = JSONString;
                var responseInsert = InsertRBIResponse();
            }
            catch (Exception ex)
            {
                JSONString = "Technical error !";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + result.Payload.CPIN + " :- " + ex.Message.ToString());
            }
            return JSONString;



        }
        //public string CPINPULLRESP(string json)
        //{
        //    //json = "{\"System\":\"GOBH\",\"ServiceType\":\"CPINPUSHRES\",\"Signature\":\"BXJDBCHDBHD\",\"Status_cd\":\"0\",\"Payload\":[{\"RESP_CODE\":\"CPNT0001\",\"RJCT_DESC\":\"Malfomed Request\"}]}";
        //    var result = JsonConvert.DeserializeObject<egNeftRtgsBL>(json);
        //    System = result.System;
        //    ServiceType = result.ServiceType;
        //    Signature = result.Signature;
        //    Status_cd = result.Status_cd;
        //    RESP_CODE = result.Payload.Select(p => p.RESP_CODE).ToList()[0];
        //    RJCT_DESC = result.Payload.Select(p => p.RJCT_DESC).ToList()[0];
        //    var res = InsertNEFTResponse();
        //    return res.ToString();
        //}
        public bool ValidatePULLReq(string signature, string verificationString)
        {
            bool flag = false;
            try
            {
                string CertPath = ConfigurationManager.AppSettings["NEFTPullCertPath"].ToString();
                //X509Certificate2 cert = new X509Certificate2(CertPath);
                System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(CertPath);
                byte[] signedData;
                byte[] sign;
                sign = ByteConverter.GetBytes(verificationString);
                signedData = Convert.FromBase64String(Signature);
                bool str = VerifySignedData(sign, signedData, cert);
                if (str)
                {
                    flag = true;
                }
                else
                {
                    ErrorCode = "CPNT0002";
                }
            }
            catch (Exception ex)
            {
                ErrorCode = "CPNT0003";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("ValidatePULLReq  For IPAddress= " + IPAddress + " and verificationString :- " + verificationString + " = " + ex.Message.ToString());
            }
            return flag;
        }
        public string CPINNotifyReq(CPINNotificationRequestr result)
        {
            string JSONString = string.Empty;
            //PULLNCKCPINHeader objPULLNCKCPINHeader = new PULLNCKCPINHeader();
            try
            {

                string signaturedata = string.Empty;
                byte[] originalData;
                byte[] signedData;
                byte[] sign;

                try
                {
                    //var result = JsonConvert.DeserializeObject<CPINNotificationRequest>(CPINString);

                    //string JSONString = string.Empty;
                    System = result.System;
                    ServiceType = result.ServiceType;
                    Signature = result.Signature;
                    CPIN = result.Payload.CPIN;
                    UTR = result.Payload.UTR;
                    TotalAmt = result.Payload.TotalAmt;
                    PaymentDtTm = result.Payload.PaymentDtTm;
                    BankRefNo = result.Payload.BankRefNo;
                    DataTable dt = new DataTable();

                    var resp = CPINNotificationReq();

                    if (string.IsNullOrEmpty(Signature) ||
                        string.IsNullOrEmpty(CPIN) ||
                        string.IsNullOrEmpty(UTR) ||
                        string.IsNullOrEmpty(TotalAmt) ||
                        string.IsNullOrEmpty(PaymentDtTm) ||
                        string.IsNullOrEmpty(BankRefNo) ||
                        string.IsNullOrEmpty(System) ||
                        string.IsNullOrEmpty(ServiceType))
                    {
                        ErrorCode = "CPNT0001";
                    }
                    else
                    {
                        bool verify = ValidateNotifyReq(Signature, CPIN, UTR, TotalAmt, PaymentDtTm, BankRefNo);
                        ErrorCode = verify ? CheckNotifyReqData() : ErrorCode;
                    }

                }
                catch (Exception ex)
                {
                    ErrorCode = "CPNT0003";
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + result.Payload.CPIN + " :- " + ex.Message.ToString());
                }

                IPayload objResponsePayload;
                PULLNCKCPINHeader objPULLNCKCPINHeader = new PULLNCKCPINHeader();
                objPULLNCKCPINHeader.System = System;
                objPULLNCKCPINHeader.ServiceType = "CPINNTFNRES";
                objPULLNCKCPINHeader.Status_cd = ErrorCode == "0" ? "1" : "0";
                if (ErrorCode == "0")
                {
                    objResponsePayload = new ResponseNotifyPayload();
                    ResponseNotifyPayload obja = new ResponseNotifyPayload();
                    obja.RESP_CODE = "Success";
                    objResponsePayload = obja;
                    signaturedata = obja.RESP_CODE.ToString();
                }
                else
                {
                    objResponsePayload = new ResponseNotifyFailPayload();
                    ResponseNotifyFailPayload obja = new ResponseNotifyFailPayload();
                    obja.RESP_CODE = ErrorCode;
                    obja.RJCT_DESC = Description;
                    objResponsePayload = obja;
                    signaturedata = obja.RESP_CODE + "^" + obja.RJCT_DESC;
                }

                sign = ByteConverter.GetBytes(signaturedata);
                /////////////Signing///////////////////////////
                // Create byte arrays to hold original, encrypted, and decrypted data.
                originalData = ByteConverter.GetBytes(signaturedata);
                string CertPath = (ConfigurationManager.AppSettings["NEFTCertPath"]);

                X509Certificate2 cert = new X509Certificate2(CertPath, ConfigurationManager.AppSettings["CertificatePassword"], X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;
                RSAParameters rsaParam = rsa.ExportParameters(true);

                signedData = HashAndSignBytes(originalData, rsaParam);
                // Hash and sign the data.
                signaturedata = Convert.ToBase64String(signedData);
                objPULLNCKCPINHeader.Signature = signaturedata;
                objPULLNCKCPINHeader.Payload = objResponsePayload;
                JSONString = JsonConvert.SerializeObject(objPULLNCKCPINHeader);

                //CPIN = result.Payload.CPIN;
                this.JSONString = JSONString;
                var responseInsert = InsertRBIResponse();
            }
            catch (Exception ex)
            {
                ErrorCode = "CPNT0003";
                JSONString = "Technical error !";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + result.Payload.CPIN + " :- " + ex.Message.ToString());
                //return objPULLNCKCPINHeader;
            }
            return JSONString;
        }
        public string CPINReturnNotificationReq(CPINReturnNotificationRequest result)
        {
            string JSONString = string.Empty;
            try
            {
                //string challanDetailSignatureData = string.Empty;
                string signaturedata = string.Empty;
                byte[] originalData;
                byte[] signedData;
                byte[] sign;



                //string JSONString = string.Empty;
                //var result = JsonConvert.DeserializeObject<CPINReturnNotificationRequest>(CPINString);
                try
                {
                    System = result.System;
                    ServiceType = result.ServiceType;
                    Signature = result.Signature;
                    CPIN = result.Payload.CPIN;
                    UTR = result.Payload.UTR;
                    TotalAmt = result.Payload.TotalAmt;
                    RequestDtTm = result.Payload.RequestDtTm;
                    BankRefNo = result.Payload.BankRefNo;
                    RTN_CODE = result.Payload.RTN_CODE;
                    RTN_DESC = result.Payload.RTN_DESC;

                    var resp = CPINNotificationReq();

                    if (string.IsNullOrEmpty(System) ||
                        string.IsNullOrEmpty(ServiceType) ||
                        string.IsNullOrEmpty(Signature) ||
                        string.IsNullOrEmpty(CPIN) ||
                        string.IsNullOrEmpty(UTR) ||
                        string.IsNullOrEmpty(TotalAmt) ||
                        string.IsNullOrEmpty(RequestDtTm) ||
                        string.IsNullOrEmpty(BankRefNo) ||
                        string.IsNullOrEmpty(RTN_CODE) ||
                        string.IsNullOrEmpty(RTN_DESC))
                    {
                        ErrorCode = "CPNT0001";
                    }
                    else
                    {
                        bool verify = ValidateReturnNotifyReq(Signature, CPIN, UTR, TotalAmt, RequestDtTm, BankRefNo, RTN_CODE, RTN_DESC);
                        ErrorCode = verify ? CheckReturnNotifyReq().ToString() : ErrorCode;
                    }
                }
                catch (Exception ex)
                {
                    ErrorCode = "CPNT0003";
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + result.Payload.CPIN + " :- " + ex.Message.ToString());
                }

                DataTable dt = CheckReturnNotifyReqData();
                //Dictionary<string, string> objResponsePayload = new Dictionary<string, string>();
                IPayload objResponsePayload;
                PULLNCKCPINHeader objPULLNCKCPINHeader = new PULLNCKCPINHeader();
                ErrorCode = dt.Rows[0]["errorcode"].ToString();
                objPULLNCKCPINHeader.System = System;
                objPULLNCKCPINHeader.ServiceType = "RTNNTFNRES";
                objPULLNCKCPINHeader.Status_cd = ErrorCode == "0" ? "1" : "0";

                if (ErrorCode == "0")
                {
                    objResponsePayload = new ResponseNotifyPayload();
                    ResponseNotifyPayload obja = new ResponseNotifyPayload();
                    obja.RESP_CODE = "Success";
                    objResponsePayload = obja;
                    signaturedata = obja.RESP_CODE.ToString();
                }
                else
                {
                    objResponsePayload = new ResponseNotifyFailPayload();
                    ResponseNotifyFailPayload obja = new ResponseNotifyFailPayload();
                    obja.RESP_CODE = ErrorCode;
                    obja.RJCT_DESC = Description;
                    objResponsePayload = obja;
                    signaturedata = obja.RESP_CODE + "^" + obja.RJCT_DESC;
                }

                sign = ByteConverter.GetBytes(signaturedata);
                /////////////Signing///////////////////////////
                // Create byte arrays to hold original, encrypted, and decrypted data.
                originalData = ByteConverter.GetBytes(signaturedata);

                string CertPath = ConfigurationManager.AppSettings["NEFTCertPath"].ToString();
                X509Certificate2 cert = new X509Certificate2(CertPath, ConfigurationManager.AppSettings["CertificatePassword"], X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;
                RSAParameters rsaParam = rsa.ExportParameters(true);


                signedData = HashAndSignBytes(originalData, rsaParam);
                // Hash and sign the data.
                signaturedata = Convert.ToBase64String(signedData);
                objPULLNCKCPINHeader.Signature = signaturedata;

                JSONString = JsonConvert.SerializeObject(objPULLNCKCPINHeader);

                CPIN = result.Payload.CPIN;
                this.JSONString = JSONString;
                var responseInsert = InsertRBIResponse();
            }
            catch (Exception ex)
            {
                ErrorCode = "CPNT0003";
                JSONString = "Technical error !";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + result.Payload.CPIN + " :- " + ex.Message.ToString());
                //return objPULLNCKCPINHeader;
            }
            return JSONString;
        }
        public bool ValidateNotifyReq(string Signature, string CPIN, string UTR, string TotalAmt, string paymentDtTm, string BankRefNo)
        {

            bool flag = false;
            try
            {
                string CertPath = ConfigurationManager.AppSettings["NEFTPullCertPath"].ToString();
                //X509Certificate2 cert = new X509Certificate2(CertPath);
                System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(CertPath);
                byte[] signedData;
                byte[] sign;
                sign = ByteConverter.GetBytes(CPIN.Trim() + "^" + UTR.Trim() + "^" + TotalAmt.Trim() + "^" + paymentDtTm.Trim() + "^" + BankRefNo.Trim());
                signedData = Convert.FromBase64String(Signature);
                bool str = VerifySignedData(sign, signedData, cert);
                if (str)
                {
                    flag = true;
                }
                else
                {
                    ErrorCode = "CPNT0002";
                }
            }
            catch (Exception ex)
            {
                ErrorCode = "CPNT0003";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("ValidateNotifyReq  For IPAddress= " + IPAddress + " and CPIN :- " + CPIN + " = " + ex.Message.ToString());
            }
            return flag;
        }
        public bool ValidateReturnNotifyReq(string Signature, string CPIN, string UTR, string TotalAmt, string RequestDtTm, string BankRefNo, string RTN_CODE, string RTN_DESC)
        {
            bool flag = false;
            try
            {
                string CertPath = ConfigurationManager.AppSettings["NEFTPullCertPath"].ToString();
                //X509Certificate2 cert = new X509Certificate2(CertPath);
                System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(CertPath);
                byte[] signedData;
                byte[] sign;
                sign = ByteConverter.GetBytes(CPIN.Trim() + "^" + UTR.Trim() + "^" + TotalAmt.Trim() + "^" + RequestDtTm.Trim() + "^" + BankRefNo.Trim() + "^" + RTN_CODE.Trim() + "^" + RTN_DESC.Trim());
                signedData = Convert.FromBase64String(Signature);
                bool str = VerifySignedData(sign, signedData, cert);
                if (str)
                {
                    flag = true;
                }
                else
                {
                    ErrorCode = "CPNT0002";
                }
            }
            catch (Exception ex)
            {
                ErrorCode = "CPNT0003";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("ValidateReturnNotifyReq  For IPAddress= " + IPAddress + " and CPIN :- " + CPIN + " = " + ex.Message.ToString());
            }
            return flag;
        }
    }

    public class eChallanPushResParent
    {
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Signature { get; set; }
        public string Status_cd { get; set; }
        public Payload payload { get; set; }

    }
    public class Payload
    {
        public string RESP_CODE { get; set; }
        public string RJCT_DESC { get; set; }
    }
    public class CPINHeaderPullDemo
    {
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Signature { get; set; }
        public PayloadDetailPullDemo Payload { get; set; }
    }
    public class PayloadDetailPullDemo
    {
        public string CPIN { get; set; }

    }
    public class CPINPullRequest
    {
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Signature { get; set; }
        public RequestPayload Payload { get; set; }
    }
    public class RequestPayload
    {
        public string CPIN { get; set; }
    }
    public class ResponseNotifyFailPayload : IPayload
    {
        public string RESP_CODE { get; set; }
        public string RJCT_DESC { get; set; }
    }
    public class ResponseNotifyPayload : IPayload
    {
        public string RESP_CODE { get; set; }
    }
    public class ResponsePayload
    {
        public string CPIN { get; set; }
        public string RESP_CODE { get; set; }
        public string RJCT_DESC { get; set; }
    }
    public class CPINHeader
    {
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Signature { get; set; }
        public PayloadDetail Payload { get; set; }
    }
    public class PULLACKCPINHeader
    {
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Status_cd { get; set; }
        public string Signature { get; set; }
        public IPayload Payload { get; set; }
    }
    public class PULLNCKCPINHeader
    {
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Status_cd { get; set; }
        public string Signature { get; set; }
        public IPayload Payload { get; set; }
    }
    public interface IPayload
    {

    }
    public class PayloadDetail
    {
        public string CPIN { get; set; }
        public string ExpDt { get; set; }
        public string TotalAmt { get; set; }
        public List<ChallanDetail> ChallanDtls { get; set; }
    }

    public class ChallanDetail
    {
        public string AcntID { get; set; }
        public string Amount { get; set; }
        public string AdminZone { get; set; }
    }
    public class PayloadPullDetail : IPayload
    {
        public string CPIN { get; set; }
        public string ExpDt { get; set; }
        public string TotalAmt { get; set; }
        public string PayerName { get; set; }
        public List<ChallanDetail> ChallanDtls { get; set; }
    }

    public class CPINNotificationRequest
    {
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Signature { get; set; }
        public PayloadNotifyRequest Payload { get; set; }
    }
    public class PayloadNotifyRequest
    {
        public string CPIN { get; set; }
        public string UTR { get; set; }
        public string TotalAmt { get; set; }
        public string PaymentDtTm { get; set; }
        public string BankRefNo { get; set; }
    }
    public class CPINReturnNotificationRequest
    {
        public string System { get; set; }
        public string ServiceType { get; set; }
        public string Signature { get; set; }
        public PayloadReturnNotifyRequest Payload { get; set; }
    }
    public class PayloadReturnNotifyRequest
    {
        public string CPIN { get; set; }
        public string UTR { get; set; }
        public string TotalAmt { get; set; }
        public string RequestDtTm { get; set; }
        public string BankRefNo { get; set; }
        public string RTN_CODE { get; set; }
        public string RTN_DESC { get; set; }
    }

    /// <summary>
    /// CPINNotificationRequest Main String
    /// </summary>

    public class CPINNotificationRequestr
    {
        public string System { get; set; }

        public string ServiceType { get; set; }

        public string Signature { get; set; }

        public RequestPayloadr Payload { get; set; }
    }

    /// <summary>
    /// CPINNotificationRequest Payload
    /// </summary>
    public class RequestPayloadr
    {
        public string CPIN { get; set; }

        public string UTR { get; set; }

        public string TotalAmt { get; set; }

        public string PaymentDtTm { get; set; }

        public string BankRefNo { get; set; }
    }
}
