using System;

namespace EgBL
{
    public class EgCommonPropertyBL
    {
        #region Variables
        private DateTime _FromDate, _ToDate;
        private string _BudgetHead, _BSRCode, _PDAccName, _TreasuryCode, _MajorHead, _SubMajorHead, _MinorHead, _SubMinorHead,
                       _GroupSubHead, _Bankcode;
        private int _DeptCode, _OfficeId, _UserId,  _PDAccNo;
        private decimal _Amount;
        private Int64 _GRN;
        #endregion

        #region Properties and validations
        public DateTime FromDate
        {
            get { return this._FromDate; }
            set
            {
                try
                {
                    this._FromDate = value;
                    if (string.IsNullOrEmpty(this._FromDate.ToString())) { throw new Exception("FromDate Can Not Be Blank "); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public DateTime ToDate
        {
            get { return this._ToDate; }
            set
            {
                try
                {
                    this._ToDate = value;
                    if (string.IsNullOrEmpty(this._ToDate.ToString())) { throw new Exception("ToDate Can Not Be Blank "); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public decimal Amount
        {
            get { return this._Amount; }
            set
            {
                try
                {
                    this._Amount = value;
                    if (string.IsNullOrEmpty(this._Amount.ToString())) { throw new Exception("Amount Can Not Be Blank or Zero"); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public int PDAccNo
        {
            get { return this._PDAccNo; }
            set
            {

                try
                {
                    this._PDAccNo = value;
                    if (string.IsNullOrEmpty(this._PDAccNo.ToString())) { throw new Exception("PDAccount No Can Not be Blank"); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }

        }
        public int DeptCode
        {
            get { return this._DeptCode; }
            set
            {
                try
                {
                    this._DeptCode = value;
                    if (string.IsNullOrEmpty(this._DeptCode.ToString())) { throw new Exception("Dept Code Can Not Be Blank"); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public int OfficeId
        {
            get { return this._OfficeId; }
            set
            {
                try
                {
                    this._OfficeId = value;
                    if (string.IsNullOrEmpty(this._OfficeId.ToString())) { throw new Exception("OfficeId Can Not Be Blank"); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public int UserId
        {
            get { return this._UserId; }
            set
            {
                try
                {
                    this._UserId = value;
                    if (string.IsNullOrEmpty(this._UserId.ToString())) { throw new Exception("UserId Can Not Be Blank"); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public Int64 GRN
        {
            get { return this._GRN; }
            set
            {
                try
                {
                    this._GRN = value;
                    if (string.IsNullOrEmpty(this._GRN.ToString())) { throw new Exception("GRN Can Not Be Blank"); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }

            }
        }
        public string PDAccName
        {
            get { return this._PDAccName; }
            set
            {
                try
                {
                    this._PDAccName = value;
                    if (string.IsNullOrEmpty(this._PDAccName)) { throw new Exception("PDAcc Name Can Not Be Blank"); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }

            }
        }
        public string TreasuryCode
        {
            get { return this._TreasuryCode; }
            set
            {
                try
                {
                    this._TreasuryCode = value;
                    if (string.IsNullOrEmpty(this._TreasuryCode)) { throw new Exception("Treasury Code Can Not Be Blank "); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public string MajorHead
        {
            get { return this._MajorHead; }
            set
            {
                try
                {
                    this._MajorHead = value;
                    if (string.IsNullOrEmpty(this._MajorHead)) { throw new Exception("MajorHead Can Not Be Blank "); }
                    if (this._MajorHead.Length != 4) { throw new Exception("MajorHead Should Be In Four Charectors"); }
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message.ToString());
                }
            }
        }
        public string SubMajorHead
        {
            get { return this._SubMajorHead; }
            set
            {
                try
                {
                    this._SubMajorHead = value;
                    if (string.IsNullOrEmpty(this._SubMajorHead)) { throw new Exception("Sub Major Head Can Not Be Blank"); }
                    if (this._SubMajorHead.Length != 2) { throw new Exception("SubMajorHead Should Be In Two Charectors"); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }
        public string MinorHead
        {
            get { return this._MinorHead; }
            set
            {
                try
                {
                    this._MinorHead = value;
                    if (string.IsNullOrEmpty(this._MinorHead)) { throw new Exception("MinorHead Head Can Not Be Blank"); }
                    if (this._MinorHead.Length != 3) { throw new Exception("MinorHead Should Be In Three Charectors"); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }
        public string SubMinorHead
        {
            get { return this._SubMinorHead; }
            set
            {
                try
                {
                    this._SubMinorHead = value;
                    if (string.IsNullOrEmpty(this._SubMinorHead)) { throw new Exception("SubMinorHead Head Can Not Be Blank"); }
                    if (this._SubMinorHead.Length != 2) { throw new Exception("SubMinorHead Should Be In Two Charectors"); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }
        public string GroupSubHead
        {
            get { return this._GroupSubHead; }
            set
            {
                try
                {
                    this._GroupSubHead = value;
                    if (string.IsNullOrEmpty(this._GroupSubHead)) { throw new Exception("GroupSubHead Head Can Not Be Blank"); }
                    if (this._GroupSubHead.Length != 2) { throw new Exception("GroupSubHead Should Be In Tow Charector"); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }
        public string BudgetHead
        {
            get { return this._BudgetHead; }
            set
            {
                try
                {
                    this._BudgetHead = value;
                    if (string.IsNullOrEmpty(this._BudgetHead)) { throw new Exception("BudgetHead Can Not Be Blank"); }
                    if (this._BudgetHead.Length != 13) { throw new Exception("BudgetHead Should Be In 13 Charectors"); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public string BSRCode
        {
            get { return this._BSRCode; }
            set
            {
                try
                {
                    this._BSRCode = value;
                    if (string.IsNullOrEmpty(this._BSRCode)) { throw new Exception("BSRCode Can Not Be Blank"); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        public string Bankcode
        {
            get { return this._Bankcode; }
            set
            {
                try
                {
                    this._Bankcode = value;
                    if (string.IsNullOrEmpty(this._Bankcode)) { throw new Exception("Bank Code Can Not Be Blank"); }
                }
                catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            }
        }
        #endregion
    }
}
