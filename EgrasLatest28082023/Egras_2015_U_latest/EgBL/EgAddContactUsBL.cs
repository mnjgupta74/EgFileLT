using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
namespace EgBL
{

    public class EgAddContactUsBL
    {
        GenralFunction gf;
        //EgInsertContactUsDetail
        #region Class Properties
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public int Priority { get; set; }
        public bool  IsDisabled { get; set; }
        #endregion


        #region Class Functions
        /// <summary>
        /// Get All Contacts List
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            GenralFunction gf = new GenralFunction();
            return gf.Filldatatablevalue(null, "EgShowContactDetails", dt, null);
        }
        /// <summary>
        /// Insert ContactUs Details 
        /// </summary>
        /// <returns></returns>
        public int InsertContactDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@Name", SqlDbType.VarChar, 512) { Value = Name };
            PARM[1] = new SqlParameter("@ContactNo", SqlDbType.Char, 25) { Value = ContactNo };
            PARM[2] = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 512) { Value = EmailAddress };
            PARM[3] = new SqlParameter("@Priority", SqlDbType.Int) { Value = Priority };
            PARM[4] = new SqlParameter("@IsDisabled", SqlDbType.Bit) { Value = IsDisabled };
            return gf.UpdateData(PARM, "EgInsertContactDetail");
        }
        /// <summary>
        /// Show Contact Detail in grid
        /// </summary>
        /// <param name="grd"></param>
        public DataTable  gridContactUs()
        {
            DataTable dt = new DataTable();

            gf = new GenralFunction();
            return gf.Filldatatablevalue(null, "EgGetContactDetails", dt, null);
         
        }
        /// <summary>
        /// Update Contact details 
        /// </summary>
        /// <returns></returns>
        public int UpdateContactDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[6];
            PARM[0] = new SqlParameter("@ContactId", SqlDbType.Int) { Value = ContactId };
            PARM[1] = new SqlParameter("@Name", SqlDbType.VarChar, 512) { Value = Name };
            PARM[2] = new SqlParameter("@ContactNo", SqlDbType.Char, 25) { Value = ContactNo };
            PARM[3] = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 512) { Value = EmailAddress };
            PARM[4] = new SqlParameter("@priority", SqlDbType.Int) { Value = Priority };
            PARM[5] = new SqlParameter("@IsDisabled", SqlDbType.Bit) { Value = IsDisabled };
            return gf.UpdateData(PARM, "EgUpdateContactDetail");
        }
        /// <summary>
        /// Delete Contact Details
        /// </summary>
        /// <returns></returns>
        public int DeleteContactDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@ContactId", SqlDbType.Int) { Value = ContactId };
            return gf.UpdateData(PARM, "EgDeleteContactDetails");
        }

        #endregion
    }
}