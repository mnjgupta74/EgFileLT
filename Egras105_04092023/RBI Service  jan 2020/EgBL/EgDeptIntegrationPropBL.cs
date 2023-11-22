using System;
using System.Text.RegularExpressions;
namespace EgBL
{
    // Created by Rachit Sharma to check properties before assigning them

    public class EgDeptIntegrationPropBL
    {
        // Checking properties before assigning them to datatable
        EgErrorHandller obj = new EgErrorHandller();
        #region Property Validation and Exception Handing
        // Merchant Code Should not be null ,greater than 0 and not 0
        public int merchantCode(string value)
        {
            int merCode;
            bool isNumericMCode = int.TryParse(value, out merCode);
            if (!isNumericMCode)
                throw new Exception("Merchant Code is invalid");
            if (merCode <= 0)
            {
                { throw new Exception("Merchant Code Should not be null ,greater than 0 and not 0"); }
            }
            return merCode;
        }
        // Full name length cannot be greater than 50 and it should not be null 
        public string fullName(string name)
        {
            Regex reg = new Regex("^([a-zA-Z0-9_.,:;*!#`$+\\[(.*?)\\]()'@%?={}&//\\ \\s\\-]*)$");
            if (!reg.IsMatch(name.Trim()) || name.Length > 50 || string.IsNullOrEmpty(name.Trim()))
            {
                { throw new Exception("Remitter name cannot be null and Length must be less then or equal to 50"); }
            }
            return name;
        }
        // Function to check the length of pincode as well as checking for null value 
        public string pincode(string pincode)
        {
            Regex regex = new Regex("^[0-9]{6}$");  // check string contains only digits from [0-9]
            if (!regex.IsMatch(pincode) || string.IsNullOrEmpty(pincode) || pincode.Length > 6)
            {
                { throw new Exception("Pin code must be less than 6 or equal to 6 and must not contain any character"); }
            }
            return pincode;
        }
        // Payment type length must be equal to 1 and should not be null
        public string pType(string ptype)
        {
            // Used Regex here to check if string contains only letters or not 
            if (string.IsNullOrEmpty(ptype) || ptype.Length > 1 || ptype.Length == 0 || !(Regex.IsMatch(ptype, @"^[MN]+$")))
            {
                { throw new Exception("Payment type length must be equal to 1 and should not be null and contains M/N only"); }
            }
            return ptype;
        }
        // checking refrence number for blank or null values
        public string RegistrationNo(string regisNo)
        {
            if (string.IsNullOrEmpty(regisNo) || regisNo.Length >= 50)
            {
                { throw new Exception("Identity cannot be null and must be less than and equal to 50"); }
            }
            return regisNo;
        }
        // Method to check the Location length
        public string location(string Location)
        {
            if (string.IsNullOrEmpty(Location) || Location.Length != 4)
            {
                { throw new Exception("Location cannot be null and length should be 4"); }

            }
            return Location;
        }
        // Method to check the Identity 
        public string identity(string Identity)
        {
            if (Identity.Length >= 50)
            {
                { throw new Exception("Identity cannot be null and must be less than and equal to 50"); }
            }
            return Identity;
        }
        // Method to check office Code should not be less than or equal to 0
        public int officeCode(string OfficeCode, bool isMultipleOffice)
        {
            int office;
            bool isNumericOffice = int.TryParse(OfficeCode, out office);
            if (!isNumericOffice)
                throw new Exception("Office Code is invalid");
            if (office <= 0 && !isMultipleOffice)
            {
                { throw new Exception("OfficeCode cannot be less than or equal 0"); }
            }
            return office;
        }
        public int officeCode(string OfficeCode)
        {
            int office;
            bool isNumericOffice = int.TryParse(OfficeCode, out office);
            if (!isNumericOffice)
                throw new Exception("Office Code is invalid");
            if (office <= 0)
            {
                { throw new Exception("OfficeCode cannot be less than or equal 0"); }
            }
            return office;
        }
        // Method to check Department code 
        public int departmentCode(string DepartmentCode)
        {
            int deptcode;
            bool isNumeric = int.TryParse(DepartmentCode, out deptcode);
            if (!isNumeric)
                throw new Exception("Department Code is invalid");
            if (deptcode < 0)
            {
                { throw new Exception("Department Code must be int"); }
            }
            return deptcode;
        }

        public string city(string City)
        {
            Regex reg = new Regex("^[a-zA-Z ]+$");
            if (string.IsNullOrEmpty(City) || City.Length > 20 || !reg.IsMatch(City))
            {
                { throw new Exception("City Cannot be null and length must be less than 20"); }
            }
            return City;
        }
        // Checking length of remarks
        public string remarks(string Remarks)
        {
            Regex reg = new Regex("^([a-zA-Z0-9_.+,//\\ \\s\\-]*)$");
            if (!reg.IsMatch(Remarks) || Remarks.Length > 200)
            {
                { throw new Exception("Remarks Cannot be null and length must be less than 200"); }
            }
            return Remarks;
       }
        // checking refrence number for blank or null values
        public string refNo(string RefNo)
        {
            if (string.IsNullOrEmpty(RefNo))
            {
                { throw new Exception("Refrence number cannot be null"); }
            }
            return RefNo;
        }
        // If if deduct commission is less than 0 or 0
        public double deductCommission(string DeductCommission)
        {
            double discount;
            bool isNumeric = double.TryParse(DeductCommission, out discount);
            if (!isNumeric)
                throw new Exception("Department Code is invalid");
            if (discount < 0)
            {
                { throw new Exception("deduct commission must be double "); }
            }
            return discount;
        }
        // Check if amount is greater than 0
        public double amount(string Amount)
        {
            double amt;
            bool isNumericAmt = double.TryParse(Amount, out amt);
            if (!isNumericAmt)
                throw new Exception("amount is invalid");

            if ((Convert.ToDouble(Amount) - Math.Floor(Convert.ToDouble(Amount))) != 0)
            {
                throw new Exception("Amount should be only in Rupees not in Paise !! ");
            }
            if (amt <= 0)
            {
                { throw new Exception("amount must be greater than 0"); }
            }
            return amt;
        }
        // Check if district code is 0 or less than it
        public int districtCode(string DistrictCode)
        {
            int distCode;
            bool isNumeric = int.TryParse(DistrictCode, out distCode);
            if (!isNumeric)
                throw new Exception("District Code is invalid");
            if (distCode <= 0)
            {
                { throw new Exception("District code cannot be 0 or less than 0"); }
            }
            return distCode;
        }
        // Method to check address length
        public string address(string Address)
        {
            Regex reg = new Regex("^([a-zA-Z0-9_.,:;*!#`$+\\[(.*?)\\]()'@%?={}&//\\ \\s\\-]*)$");
            if (!reg.IsMatch(Address) || string.IsNullOrEmpty(Address) || Address.Length > 100)
            {
                { throw new Exception("address must not be null and length must be smaller than 100"); }
            }
            return Address;
        }
        // check string contains only digits from [0-9]
        public string ChallanYear(string ChallanYear)
        {
            Regex regex = new Regex("^[0-9]{4}$");  // check string contains only digits from [0-9]
            if (!regex.IsMatch(ChallanYear) || string.IsNullOrEmpty(ChallanYear) || ChallanYear.Length > 4)
            {
                { throw new Exception("Invalid Challan Year"); }
            }
            return ChallanYear;
        }
        // Function to check the length of BudgetHead as well as checking for null value 
        public string BudgetHead(string BudgetHead)
        {
            Regex regex = new Regex("^[0-9]{18}$");  // check string contains only digits from [0-9]
            if (!regex.IsMatch(BudgetHead) || string.IsNullOrEmpty(BudgetHead) || BudgetHead.Length != 18)
            {
                { throw new Exception("Invalid BudgetHead."); }
            }
            return BudgetHead;
        }

        // Method to check office Code should not be less than or equal to 0
        public string filler(string filler)
        {
            //if ( filler.Length<0)
            //{
            //    { throw new Exception("filler can not be Blank"); }
            //}
            return filler;
        }
        /// <summary>
        /// Allow  Pd  online 24 nov 2017
        /// </summary>
        /// <param name="PDAcc"></param>
        /// <returns></returns>
        public int PdAcc(string PDAcc)
        {
            int pd;
            bool isNumeric = int.TryParse(PDAcc, out pd);
            if (!isNumeric)
                throw new Exception("PD Account No is invalid");
            if (pd <= 0)
            {
                { throw new Exception("PD Account No cannot be 0 or less than 0"); }
            }
            return pd;
        }
        public string BankName(string bankname)
        {
            Regex regex = new Regex("^[0-9]{7}$");  // check string contains only digits from [0-9]
            if (!regex.IsMatch(bankname) || string.IsNullOrEmpty(bankname) || bankname.Length > 7)
            {
                { throw new Exception("Bank Code Should be 7 Numeric Number"); }
            }
            return bankname;
        }
        #endregion

        #region Trgupdatestatus
        public string status(string ptype)
        {
            // Used Regex here to check if string contains only letters or not 
            if (string.IsNullOrEmpty(ptype) || ptype.Length > 1 || ptype.Length == 0 || !(Regex.IsMatch(ptype, @"^[SFP]+$")))
            {
                { throw new Exception("Status length must be equal to 1 and should not be null and contains S,P,F only"); }
            }
            return ptype;
        }
        public string CINNumber(string cin)
        {
            Regex reg = new Regex("^([a-zA-Z0-9]*)$");
            if (!reg.IsMatch(cin) || cin.Length > 200)
            {
                { throw new Exception("CIN Cannot be null !"); }
            }
            return cin;
        }
        public int GRN(string grn)
        {
            int grnnumber;
            bool isNumeric = int.TryParse(grn, out grnnumber);
            if (!isNumeric)
                throw new Exception("GRN is invalid");
            if (grnnumber <= 0)
            {
                { throw new Exception("GRN cannot be 0 or less than 0"); }
            }
            return grnnumber;
        }
        #endregion
    }
}
