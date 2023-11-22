using System;
using System.Text.RegularExpressions;
namespace EgBL
{
    // Created by Rachit Sharma to check properties before assigning them

    public class EgSnaIntegrationPropBL
    {
        // Checking properties before assigning them to datatable
        EgErrorHandller obj = new EgErrorHandller();
        #region Property Validation and Exception Handing


        // checking refrence number for blank or null values
        public string SNArefNo(string RefNo)
        {
            Regex regex = new Regex("^[0-9a-zA-Z]{1,10}$");  // check string contains only alphanumeric value from [0-9][a-z][A-Z]
            if (string.IsNullOrEmpty(RefNo))
            { throw new Exception("007"); }//007 Renference no can not be null or blank

            if (!regex.IsMatch(RefNo))
            { throw new Exception("007A"); }//Invalid Renference No and minimum length 1
            return RefNo;
        }

        // Check if amount is greater than 0
        public double amount(string Amount)
        {
            double amt;
            bool isNumericAmt = double.TryParse(Amount, out amt);
            if (!isNumericAmt)
                throw new Exception("008");//amount is invalid

            if ((Convert.ToDouble(Amount) - Math.Floor(Convert.ToDouble(Amount))) != 0)
            {
                throw new Exception("009");//Amount should be only in Rupees not in Paise !! 
            }
            if (amt <= 0)
            {
                { throw new Exception("010"); }//amount must be greater than 0
            }
            return amt;
        }

        // Function to check the length of BudgetHead as well as checking for null value 
        public string BudgetHead(string BudgetHead)
        {
            Regex regex = new Regex("^[0-9]{18}$");  // check string contains only digits from [0-9] BudgetHead+ScheCode
            if (string.IsNullOrEmpty(BudgetHead))
            { throw new Exception("011"); }// BudgetHead Can Not Be Blank !

            if (BudgetHead.Length != 18)
            { throw new Exception("012"); }//BudgetHead Must Contain BeugetHead(13) + Sche Code(5) !

            if (!regex.IsMatch(BudgetHead))
            { throw new Exception("013"); }//Invalid BudgetHead.
            return BudgetHead;
        }

        //Pd Account
        public string PdAcc(string PDAcc)
        {
            Regex regex = new Regex("^[0-9]{2,6}$");  // check string contains only digits from [0-9]
            if (string.IsNullOrEmpty(PDAcc))
            { throw new Exception("014"); }// Pd Can Not Be Blank !

            if (!regex.IsMatch(PDAcc))
            { throw new Exception("015"); }//Invalid Pd Account.
            return PDAcc;
        }

        // Full name length cannot be greater than 50 and it should not be null 
        public string fullName(string name)
        {
            Regex reg = new Regex("^([a-zA-Z0-9_.,:;*!#`$+\\[(.*?)\\]()'@%?={}&//\\ \\s\\-]*)$");
            if (string.IsNullOrEmpty(name.Trim()))
            {
                { throw new Exception("016"); }//Remitter name cannot be null
            }
            if (name.Length > 50)
            {
                { throw new Exception("017"); }//Remitter name Length must be less then or equal to 50
            }
            if (!reg.IsMatch(name.Trim()))
            {
                { throw new Exception("018"); }//Invalid Remitter Name
            }
            return name;
        }

        public string officeCode(string OfficeCode)
        {
            Regex regex = new Regex("^[0-9]{2,6}$");  // check string contains only digits from [0-9]
            //int office;
            //bool isNumericOffice = int.TryParse(OfficeCode, out office);
            //if (!isNumericOffice)
            //    throw new Exception("019");//Office Code is invalid
            if (string.IsNullOrEmpty(OfficeCode.Trim()))
            {
                { throw new Exception("019"); }//Office Code Can Not Be blank.
            }
            if (!regex.IsMatch(OfficeCode))
            { throw new Exception("020"); }//Invalid Office Code.
            return OfficeCode;
        }

        // Method to check the Location length
        public string location(string Location)
        {
            Regex regex = new Regex("^[0-9]{4}$");  // check string contains only digits from [0-9]
            if (string.IsNullOrEmpty(Location))
            { throw new Exception("021"); }//Location cannot be null and length should be 4

            if (!regex.IsMatch(Location))
            { throw new Exception("022"); }//Invalid Location and length should be 4

            return Location;
        }

        // checking refrence number for blank or null values
        public string AccountNo(string accNo)
        {
            Regex regex = new Regex("^[0-9a-zA-Z]{3,10}$");  // check string contains only alphanumeric value from [0-9][a-z][A-Z]
            if (string.IsNullOrEmpty(accNo))
            { throw new Exception("023"); }//Account No Can not be blank

            if (!regex.IsMatch(accNo))
            { throw new Exception("024"); }//Invalid Account No

            return accNo;
        }

        // Method to check Department code 
        public string departmentCode(string DepartmentCode)
        {
            Regex regex = new Regex("^[0-9]{1,4}$");  // check string contains only digits from [0-9]

            if (string.IsNullOrEmpty(DepartmentCode.Trim()))
            {
                { throw new Exception("025"); }//Department Code Can Not Be blank.
            }
            if (!regex.IsMatch(DepartmentCode))
            { throw new Exception("026"); }//Invalid Department Code.

            return DepartmentCode;
        }

        // Merchant Code Should not be null ,greater than 0 and not 0
        public string merchantCode(string value)
        {
            Regex regex = new Regex("^[0-9]{1,7}$");  // check string contains only digits from [0-9]

            if (string.IsNullOrEmpty(value.Trim()))
            {
                { throw new Exception("027"); }//Merchant Cod Can Not Be blank.
            }
            if (!regex.IsMatch(value))
            { throw new Exception("028"); }//Invalid Department Code and Merchant Code Should not be null ,greater than 0 and not 0.

            return value;
        }
        public string BSRCode(string bsrcode)
        {
            Regex regex = new Regex("^[0-9]{7}$");  // check string contains only digits from [0-9]
            if (string.IsNullOrEmpty(bsrcode))
            { throw new Exception("036"); }//BSRCode cannot be null and length should be 7

            if (!regex.IsMatch(bsrcode))
            { throw new Exception("037"); }//Invalid BSRCode and length should be 7

            return bsrcode;
        }
        public string BankRefNo(string RefNo)
        {
            Regex regex = new Regex("^[0-9a-zA-Z]{1,20}$");  // check string contains only alphanumeric value from [0-9][a-z][A-Z]
            if (string.IsNullOrEmpty(RefNo))
            { throw new Exception("038"); }//007 Bank Renference no can not be null or blank

            if (!regex.IsMatch(RefNo))
            { throw new Exception("039"); }//Invalid Bank Renference No and minimum length 1
            return RefNo;
        }

        public string PaymentDate(string date)
        {
            //Regex regex = new Regex("^[0-9a-zA-Z]{1,20}$");  // check string contains only alphanumeric value from [0-9][a-z][A-Z]
            if (string.IsNullOrEmpty(date))
            { throw new Exception("040"); }//007 PaymentDate can not be null or blank

            //if (!regex.IsMatch(RefNo))
            //{ throw new Exception("041"); }//PaymentDate and minimum length 1
            return date;
        }
     

        /// <summary>
        /// Allow  Pd  online 24 nov 2017
        /// </summary>
        /// <param name="PDAcc"></param>
        /// <returns></returns>

        #endregion
    }
}
