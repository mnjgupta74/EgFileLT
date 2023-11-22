using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace EgBL
{
    public class EgAddGroupSubHeadHindiNameBL
    {
        GenralFunction gf;
        DataTable dt;
        #region Class Properties
        public string BudgetHead { get; set; }
        public string HeadName { get; set; }
        #endregion
        #region Function
        /// <summary>
        /// Get all heads which groupsubheadname is blank
        /// </summary>
        public void GetHeads(CheckBoxList chk)
        {
            gf = new GenralFunction();
            dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgGroupSubHeadHindiNamelist", dt, null);
           // if (dt.Rows.Count > 0)
           // {
                chk.DataSource = dt;
                chk.DataTextField = "BudgetHead";
                chk.DataValueField = "Head";
                chk.DataBind();
                dt.Dispose();
            //}
        }
        /// <summary>
        /// Get Major/BudgetHead wise List (Groupsubheadname hindi is blank)
        /// </summary>
        /// <param name="chk"></param>
        public void GetMajorheadAndBudgetHeadWise(CheckBoxList chk)
        {
            dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            dt = gf.Filldatatablevalue(PARM, "EgHeadWiseGroupSubHeadList", dt, null);
            //if (dt.Rows.Count > 0)
            //{
                chk.DataSource = dt;
                chk.DataTextField = "BudgetHead";
                chk.DataValueField = "Head";
                chk.DataBind();
                dt.Dispose();
            //}
        }
        /// <summary>
        /// Update groupsubheadhindi name
        /// </summary>
        public int UpdateHeadName()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[1] = new SqlParameter("@HeadName", SqlDbType.NVarChar,100) { Value = HeadName };
            return gf.UpdateData(PARM, "EgUpdateGroupSubHeadHindiName");
        }
        #endregion
    }
}
