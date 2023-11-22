using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EgBL
{
    public class EgCheckChallanCondition
    {        
        public string BudgetHead { get; set; }
        public int DeptCode { get; set; }
        public string payMode { get; set; }
        public string pdAccountNo { get; set; }
        public int pdAccountNoCount { get; set; }
        public string treasuryCodeBank { get; set; }
        public string treasuryCodePd { get; set; }
        public string msg { get; set; }
        public bool pdVisible {get;set;}
        public bool isPD { get; set; }
        public int proc_id { get; set; }
        public string[] serviceBudgetHead { get; set; }
        public bool CheckHead0030()
        {
            bool value = false;
            if(BudgetHead.Substring(0,4) == "0030")
            {
                if (BudgetHead.Substring(0, 6) == "003003")
                    value = false;
                else
                    value = true;
            }
            return value;
        }

        public bool CheckHead004000102()
        {
            bool value = false;
            if (BudgetHead.Substring(0, 9) == "004000102")
            {
                value = true;
            }
            return value;
        }

        public bool CheckHeadGreater8000WithPD()
        {

            bool value = false;
            if (Convert.ToInt32(BudgetHead.Substring(0, 4)) > 7999 && pdAccountNo != "0")
            {
                value = true;
            }
            return value;
        }

        public bool CheckHeadGreater8000()
        {

            bool value = false;
            if (Convert.ToInt32(BudgetHead.Substring(0, 4)) > 7999)
            {
                value = true;
            }
            return value;
        }

        public bool CheckHeadGreater8000andDept()
        {
            bool value = false;
            if (Convert.ToInt32(BudgetHead.Substring(0, 4)) > 7999 || (DeptCode != 18 && DeptCode != 60 && DeptCode != 104))
            {
                value = true;
            }
            return value;
        }


        public bool CheckCTDCase()
        {
            bool value = false;
            if ((BudgetHead.Substring(0, 4) == "0040" || BudgetHead.Substring(0, 4) == "0045" || BudgetHead.Substring(0, 4) == "0042" || BudgetHead.Substring(0, 4) == "0043" || BudgetHead.Substring(0, 4) == "0044" || BudgetHead.Substring(0, 4) == "0028") && DeptCode == 18)
            {
                value = true;
            }
            return value;
        }
       
        public bool Check8782()
        {
            bool value = false;
            if (BudgetHead.Substring(0, 4) == "8782")
            {
                value = true;
            }
            return value;
        }

        public bool Check8338withmultiple()
        {
            bool value = false;
            if ( (BudgetHead.Substring(0, 4) == "8338" || BudgetHead.Substring(0, 4) == "8342" || BudgetHead.Substring(0, 4) == "8448" || BudgetHead == "8443001060000") &&
                BudgetHead != "8342001170100" &&
                BudgetHead != "8342001170200" &&
                BudgetHead != "8448001200705" && // Change on 01/09/2014 as per request by user.
                BudgetHead != "8448001200707" 
                )
            {
                value = true;
            }
            return value;
        }

       

        public bool Check8793()
        {
            bool value = false;
            if (BudgetHead.Substring(0, 4) != "8782" && BudgetHead.Substring(0, 4) != "8793")
            {
                value = true;
            }
            return value;
        }

        public bool CheckDept()
        {
            bool value = false;
            if (DeptCode == 18)
            {
                value = true;
            }
            return value;
        }
        // Mofify 30 Aug 2016 for visible division/pd drop  down  for all heads
        public bool CheckSubmitCondition()
        {
            bool retrunValue = false;
            string MaxBudgetHead = serviceBudgetHead.Max();
            // Service BudgetHead  Close Condition 28092022 
            //if ((MaxBudgetHead.Substring(0, 4)==("8009") || MaxBudgetHead.Substring(0, 4) == ("8011")))
                
            //{
            //    retrunValue = true;
            //    msg = " Please Proceed the Transaction Through SIPF Site  ";
            //}

            if (pdAccountNo != "0" && Convert.ToInt32(MaxBudgetHead.Substring(0, 4)) > 7999 && (payMode == "4" || payMode == "5"))
            {
                if (isPD)
                {
                    if (CheckPDRunMode())
                        return false;
                }

                if ((BudgetHead == "8443001030000" || BudgetHead == "8443001080000" || BudgetHead == "8443001090000") && !isPD) // ispd conditiion added on 13march2019 by sandeep
                    return false;

                //condition for service case
                else if ((proc_id == -1 || proc_id == -2) && (serviceBudgetHead.Contains("8443001030000") || serviceBudgetHead.Contains("8443001080000") || serviceBudgetHead.Contains("8443001090000")) && !isPD)
                {
                    return false;
                }

                else if (BudgetHead == "8658001021602")
                {
                    return false;
                }
                else
                {
                    retrunValue = true;
                    msg = "E-Banking is not allowed in this Case. Please select 0215 for depositing amount.";
                }
            }

            else if (payMode == "3" && pdAccountNo != "0" && treasuryCodeBank != treasuryCodePd && treasuryCodeBank.Substring(7, 2) != "-1")
            {
                if (treasuryCodeBank.Substring(7, 2) != treasuryCodePd.Trim().Substring(treasuryCodePd.Length - 4, 2))
                {
                    retrunValue = true;
                    msg = "Pd / Division does not match with selected treasury.";
                }

                else
                {

                }
            }
            //condition for service case 
            // 8443001080000 division code compulsory for Manual challan Non-Proc Tendor challan 8 Jan 2021
            else if ((proc_id == -1 || proc_id == -2) && (serviceBudgetHead.Contains("8443001080000") || serviceBudgetHead.Contains("8443001090000")) && (payMode == "4" || payMode == "5" || payMode == "3" ))
            {
                if (pdAccountNo != "0" && !isPD)       // Add condition in 8 jan 2021
                    retrunValue = false;
                else
                {
                    retrunValue = true;
                    // msg = "E-Banking is not allowed in this Case. Please select 0215 for depositing amount.";
                    msg = "Kindly check ,Selected office does not have any division code ";
                }
            }

            else if((BudgetHead== "8443001080000" || BudgetHead == "8443001090000" )&& (payMode == "4" || payMode == "5" || payMode == "3") && pdAccountNo=="0")
            {
                retrunValue = true;
                msg = "please Select Division Code.";
            }

            //Add Pd Condition 22 june 2023

            else if ((BudgetHead == "8443001060000" || BudgetHead== "8342001207200") && (payMode == "4" || payMode == "5" || payMode == "3") && pdAccountNo == "0")
            {
                retrunValue = true;
                msg = "PD AccountNo is Compulsory for this BudgetHead";
            }

            else if (BudgetHead != "8443001030000" && (BudgetHead.Substring(0, 4) == "8782" || BudgetHead.Substring(0, 4) == "8443" || BudgetHead == "8448001200708" || BudgetHead == "8448001200900") && (payMode == "4" || payMode == "5"))
            {
                retrunValue = true;
                msg = "E-Banking is not allowed in this Case. Please select 0215 for depositing amount.";
            }
            else if (pdVisible == true && pdAccountNo == "0" && (
                BudgetHead.Substring(0, 4) == "8338" ||
                BudgetHead.Substring(0, 4) == "8342" ||
                BudgetHead.Substring(0, 4) == "8448" ||
                BudgetHead == "8443001060000" &&      // Condition Change in 16 April 2021
                BudgetHead == "8342001207200") &&      // Add condition 6 june 2023 Pd Compulsory
                BudgetHead != "8342001170100" &&
                BudgetHead != "8342001170200" &&
                BudgetHead != "8448001200705" &&
                BudgetHead != "8448001200707" &&
                BudgetHead != "8448001200708" &&
                BudgetHead != "8448120070400" && // added by sandeep 06-12-2017
                BudgetHead != "8448001200900" &&
                BudgetHead != "8448001200704" &&   // Rajasthan Flud
                BudgetHead != "8448001201001" &&
                BudgetHead != "8448001200709" &&     // Add 25 Oct 2018 KARal Flood Help
                BudgetHead != "8448001200710" &&     // Add 4 April 2020 Covid-19  (Help to Corona Desease)
                BudgetHead != "8448001200711" &&    // Add 8 June 2021 Covid-19  (Help to Corona VacciNation)
                BudgetHead != "8342001206900"

               )
            {
                retrunValue = true;
                msg = "please select Pd / Division Code.";
            }
            else if (pdVisible == true && pdAccountNoCount > 1 && pdAccountNo == "0" &&
                    BudgetHead.Substring(0, 4) != "8782" && BudgetHead.Substring(0, 4) != "8793" && BudgetHead != "8448001200704")
            {
                retrunValue = true;
                msg = "please select Pd / Division Code.";
            }
            else if (BudgetHead == "8658001290000" && (pdAccountNo == null || pdAccountNo == "0"))// added by sandeep 06-12-2017
            {
                retrunValue = true;
                msg = "please select Pd / Division Code.";
            }
            else
            {
                retrunValue = false;
            }
            return retrunValue;
        }
        /// <summary>
        /// Allow Pd for Online Challan 24 nov 2017
        /// </summary>
        /// <returns></returns>
        /// 
        private bool CheckPDRunMode()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@PDAcc", SqlDbType.Int) { Value = pdAccountNo.Remove(pdAccountNo.Length - 4) };
            return Convert.ToBoolean(gf.ExecuteScaler(PM, "EgCheckPDRunMode"));
        }
        public bool CheckManualBanksForAnyWhereBranch()
        {
            //if (treasuryCodeBank == "2100" || treasuryCodeBank == "1900" || treasuryCodeBank == "2000" || treasuryCodeBank == "1800")
            //{
                if (pdAccountNoCount > 1)
                {
                    if (pdAccountNo == "--- Select Division Code ---")
                        return true;

                    return false;
                }
                else
                {
                    return true;
                }
            //}
            return false;
        }
                      
    }
}
