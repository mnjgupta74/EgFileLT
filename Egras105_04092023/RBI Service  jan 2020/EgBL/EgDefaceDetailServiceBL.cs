using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace EgBL
{

    public class EgDefaceDetailServiceBL
    {
        GenralFunction BLobj = new GenralFunction();
        GenralFunction gf;

        private string _validationMessage;
        private bool _isValid = true;
        private bool _isValidRequest = true;
        private int _flage = 0;
        public string EncData { get; set; }
        public int MerchantCode { get; set; }
        public Int64 Grn { get; set; }
        public double DefaceAmount { get; set; }
        public string OfficeCode { get; set; }
        public Int64 ReferenceNo { get; set; }
        private int UserId { get; set; }
        private double RemainingAmount { get; set; }
        private string DefaceFlag { get; set; }
        private int OfficeMismatch { get; set; }
        public string MessageStatus { get; set; }
        //private string ValidationMessage(
        //{
        //    get
        //    {
        //        if (!_isValidRequest)
        //        {
        //            _validationMessage = "Details not matched (Office)(Department).";
        //        }
        //        else if (RemainingAmount == 0)
        //        {
        //            _validationMessage = "No amount left to deface.";
        //        }

        //        else if (Math.Round(DefaceAmount, 2) > Math.Round(RemainingAmount, 2))
        //        {

        //            _validationMessage = "Requested Amount is greater than left amount.";
        //        }

        //        else if (Math.Round(DefaceAmount, 2) <= 0)
        //        {
        //            _validationMessage = "Requested Deface Amount should be greater than zero.";
        //        }

        //        else if (_flage==1)
        //        {
                   
        //           _validationMessage = "Record Defaced Successfully.";
        //        }

             
        //        else
        //        {
        //            _validationMessage = "Record Not Defaced.";
        //        }

        //        return _validationMessage;

        //    }
        //}
        private bool IsValid
        {
            get
            {
                if (OfficeMismatch == 1)
                {
                    _isValid = false;
                    _validationMessage = "Details not matched (Office)(Department).";
                }
                else if (RemainingAmount == 0)
                {
                    _isValid = false;
                    _validationMessage = "No amount left to deface.";
                }

                else if (Math.Round(DefaceAmount, 2) > Math.Round(RemainingAmount, 2))
                {

                    _isValid = false;
                    _validationMessage = "Requested Amount is greater than left amount.";
                }

                else if (Math.Round(DefaceAmount, 2) == 0)
                {
                    _isValid = false;
                    _validationMessage = "Requested Deface Amount should be greater than zero.";
                }
                else if (Math.Round(DefaceAmount, 2) < 0)
                {
                    _isValid = false;
                    _validationMessage = "Requested Deface Amount should be greater than zero.";
                }

                

                else
                {
                   
                    _isValid = true;
                    _validationMessage = "Record Defaced Successfully.";
                }
                return _isValid;
            }
        }
        /// <summary>
        /// Update Deface Amount By Service
        /// </summary>
        /// <returns></returns>
        public string UpdateDefaceGrn()
        {
           
            DataTable dt = new DataTable();
            dt = GetAmountByGrn();
            double lastDefaceAmount = 0.00;
            if (dt.Rows.Count > 0)
            {
                lastDefaceAmount = Convert.ToDouble(dt.Rows[0]["totalAmount"].ToString());
                UserId = Convert.ToInt32(dt.Rows[0]["userid"]);
                DefaceFlag = dt.Rows[0]["DefaceFlage"].ToString();
                RemainingAmount = Math.Round(lastDefaceAmount, 2);
            }
            else
            {
                
                OfficeMismatch = 1;       //For  True
                 
            }


            
            
            try
            {
               

                  
                     _flage = IsValid ? InsertAutoDeface() : UpdateReferenceStatus(0);
                        if (_flage == 0 && IsValid == true)
                        {
                                     _validationMessage = "Record Not Defaced.";
                                     UpdateReferenceStatus(0);
                        }
                
                //UpdateReferenceStatus(_flage);

               
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString() + "- Deface Service-" + Grn);
                return "Record Not Defaced.";
                //UpdateReferenceStatus(_flage, Trans);
             }
            return _validationMessage;
        }
        /// <summary>
        /// Get Remaining Amount By calculating (LastDefaceAmount-ReleaseAmount+RefundAmount)
        /// </summary>
        /// <returns></returns>
        DataTable GetAmountByGrn()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@OfficeCode", SqlDbType.Int) { Value = OfficeCode };
            PARM[2] = new SqlParameter("@DefaceAmount", SqlDbType.Money) { Value = DefaceAmount };
            PARM[3] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = MerchantCode };
            dt = gf.Filldatatablevalue(PARM, "EgGetDefaceAmountByGRN", dt, null);
            return dt;
        }

        /// <summary>
        /// InsertFull GRN Deface Amount Detail 
        /// </summary>
        /// <returns> 1 and 0</returns>
        int InsertAutoDeface()
        {
            SqlParameter[] PARM = new SqlParameter[6];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            PARM[1] = new SqlParameter("@Deface", SqlDbType.Char, 1) { Value = DefaceFlag };
            PARM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
            PARM[3] = new SqlParameter("@amount", SqlDbType.Money) { Value = DefaceAmount };
            PARM[4] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            PARM[5] = new SqlParameter("@Flag", SqlDbType.TinyInt) { Value = _isValid };
            return gf.UpdateData(PARM, "EgInsertAutoDeface");
           
        }
        /// <summary>
        /// Check Reference No exist or not
        /// </summary>
        /// <returns> 1 and 0</returns>
        public Int16 CheckReferenceNo()
        {
            SqlParameter[] PARM = new SqlParameter[3];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = MerchantCode };
            PARM[2] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 20) { Value = HttpContext.Current.Request.UserHostAddress };
            string result = gf.ExecuteScaler(PARM, "EgDefaceReferenceExist");
            return Convert.ToInt16(result);
        }
        /// <summary>
        /// Check Reference No exist or not
        /// </summary>
        /// <returns> 1 and 0</returns>
        int UpdateReferenceStatus(int flag)
        {

            SqlParameter[] PARM = new SqlParameter[5];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            PARM[1] = new SqlParameter("@Message", SqlDbType.VarChar, 100) { Value = _validationMessage };
            PARM[2] = new SqlParameter("@flag", SqlDbType.Int) { Value = flag };
            PARM[3] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value =Grn };
            PARM[4] = new SqlParameter("@Amount", SqlDbType.BigInt) { Value = DefaceAmount };
            return gf.UpdateData(PARM, "UpdateDefaceLogStatus");
        }

        /// <summary>
        /// Check Reference No exist or not
        /// </summary>
        /// <returns> 1 and 0</returns>
        public string GetDefaceStatus()
        {
            SqlParameter[] PARM = new SqlParameter[1];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            return gf.ExecuteScaler(PARM, "GetDefaceStatus");
        }

        /// <summary>
        /// Insert Deface Merchant data audit
        /// </summary>
        /// <returns> 1 and 0</returns>
        public int DefaceAuditLogs()
        {
            SqlParameter[] PARM = new SqlParameter[6];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@ReferenceNo", SqlDbType.BigInt) { Value = ReferenceNo };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.Int) { Value = MerchantCode };
            PARM[2] = new SqlParameter("@EncData", SqlDbType.VarChar, -1) { Value = EncData };
            PARM[3] = new SqlParameter("@IpAddress", SqlDbType.VarChar, 20) { Value = HttpContext.Current.Request.UserHostAddress };
            PARM[4] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Grn };
            PARM[5] = new SqlParameter("@MessageStatus", SqlDbType.VarChar,100) { Value = MessageStatus };

            return gf.UpdateData(PARM, "EgInsertDefaceMerchantAudit");
        }
    }
}
