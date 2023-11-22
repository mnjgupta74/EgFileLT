using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for DetermineInitial
/// </summary>
namespace NIC.Ifms
{
    public sealed class DetermineInitial
    {
        public static bool DetermineInitialString(string MatchString, string SelectedAlphabet)
        {
            if (SelectedAlphabet == "Other")
            {
                // Code for non-alpha character
                Regex objAlphaPattern = new Regex("[a-zA-Z]");
                if (MatchString.Length > 0)
                {
                    string s1 = MatchString.Substring(0, 1);
                    if (objAlphaPattern.IsMatch(s1))
                        return false;
                    else
                        return true;
                }
                return false;
            }
            // Code for alpha character
            else
                return MatchString.StartsWith(SelectedAlphabet, true, CultureInfo.CurrentCulture);
        }

        public static DataView FilterDataView(DataView dv, string SelectedAlphabet, string FilterColumn)
        {
            if (SelectedAlphabet == "Other")
            {
                dv.RowFilter = FilterColumn + " NOT LIKE  'A%'" +
                "AND " +FilterColumn +" NOT LIKE  'B%'" +
                "AND " +FilterColumn +" NOT LIKE  'C%'" +
                "AND " +FilterColumn +" NOT LIKE  'D%'" +
                "AND " +FilterColumn +" NOT LIKE  'E%'" +
                "AND " +FilterColumn +" NOT LIKE  'F%'" +
                "AND " +FilterColumn +" NOT LIKE  'G%'" +
                "AND " +FilterColumn +" NOT LIKE  'H%'" +
                "AND " +FilterColumn +" NOT LIKE  'I%'" +
                "AND " +FilterColumn +" NOT LIKE  'J%'" +
                "AND " +FilterColumn +" NOT LIKE  'K%'" +
                "AND " +FilterColumn +" NOT LIKE  'L%'" +
                "AND " +FilterColumn +" NOT LIKE  'M%'" +
                "AND " +FilterColumn +" NOT LIKE  'N%'" +
                "AND " +FilterColumn +" NOT LIKE  'O%'" +
                "AND " +FilterColumn +" NOT LIKE  'P%'" +
                "AND " +FilterColumn +" NOT LIKE  'Q%'" +
                "AND " +FilterColumn +" NOT LIKE  'R%'" +
                "AND " +FilterColumn +" NOT LIKE  'S%'" +
                "AND " +FilterColumn +" NOT LIKE  'T%'" +
                "AND " +FilterColumn +" NOT LIKE  'U%'" +
                "AND " +FilterColumn +" NOT LIKE  'V%'" +
                "AND " +FilterColumn +" NOT LIKE  'W%'" +
                "AND " +FilterColumn +" NOT LIKE  'X%'" +
                "AND " +FilterColumn +" NOT LIKE  'Y%'" +
                "AND " +FilterColumn +" NOT LIKE  'Z%'";
            }
            else
                dv.RowFilter = FilterColumn + " LIKE  '" + SelectedAlphabet + "%'";
            return dv;
        }
    }
}
