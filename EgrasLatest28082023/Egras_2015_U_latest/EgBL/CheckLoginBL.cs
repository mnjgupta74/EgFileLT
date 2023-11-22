using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgBL
{
    public class CheckLoginBL
    {
        public string loginID { get; set; }
        public string checkLoginExistance()
        {
            string Message = string.Empty;
            string stringAfterReserved;
            int n;
            bool isNumeric;
            isNumeric = int.TryParse(loginID, out n);
            if (isNumeric == true)
            {
                return Message = "LoginID is Reserved!!";
            }
            else
            {
                if (loginID.Length > 3)
                {
                    string CheckReserved = loginID.Substring(0, 3);
                    if (CheckReserved.ToUpper() == "OFF" || CheckReserved.ToUpper() == "FA.")
                    {
                        stringAfterReserved = loginID.Replace(CheckReserved, "");
                        isNumeric = int.TryParse(stringAfterReserved, out n);
                        if (isNumeric == true)
                        {
                            return Message = "LoginID is Reserved!!";
                        }
                        else
                        {
                            return Message = "Valid";
                        }
                    }
                }
                else
                {
                    return Message = "LoginId must be at least 4 characters";
                }
                return Message = "Valid";
            }
        }
    }
}
