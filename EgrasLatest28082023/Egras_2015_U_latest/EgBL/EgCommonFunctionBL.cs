using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace EgBL
{
    public class EgCommonFunctionBL
    {

        #region Class Variables
        private DateTime _fromdate, _todate;
        #endregion

        #region Class Properties
        public DateTime Todate
        {
            get { return _todate; }
            set { _todate = value; }
        }

        public DateTime Fromdate
        {
            get { return _fromdate; }
            set { _fromdate = value; }
        }

        public string majorHead { get; set; }
        public string tcode { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Class Common Function

        #endregion

    }
}
