using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgAddMenu
    {
        GenralFunction gf;
        #region Class Properties
        public int menuid { get; set; }
        public string MenuDesc { get; set; }
        public string NavigateUrl { get; set; }
        public string ActualNavigateUrl { get; set; }
        public int MenuParentId { get; set; }
        public char MenuSecured { get; set; }
        public int ModuleId { get; set; }
        public char ObjectType { get; set; }
        public int OrderId { get; set; }
        public char MenuEnabled { get; set; }
        public char MenuVisible { get; set; }
        public DateTime TransDate { get; set; }
        public int UserId { get; set; }
        public string UserType { get; set; }
        #endregion

        #region Class Functions
        /// <summary>
        /// Insert ContactUs Details 
        /// </summary>
        /// <returns></returns>
        public int InsertMenuDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[9];
            PARM[0] = new SqlParameter("@UserType", SqlDbType.VarChar, 20) { Value = UserType };
            PARM[1] = new SqlParameter("@MenuDesc", SqlDbType.VarChar, 50) { Value = MenuDesc };
            PARM[2] = new SqlParameter("@NavigateUrl", SqlDbType.VarChar, 100) { Value = ActualNavigateUrl };
            PARM[3] = new SqlParameter("@MenuParentId", SqlDbType.Int) { Value = MenuParentId };
            
            PARM[4] = new SqlParameter("@ModuleId", SqlDbType.Int) { Value = ModuleId };
            //PARM[6] = new SqlParameter("@ObjectType", SqlDbType.Bit) { Value = ObjectType };
            PARM[5] = new SqlParameter("@OrderId", SqlDbType.Int) { Value = OrderId };
            //PARM[8] = new SqlParameter("@MenuEnabled", SqlDbType.Bit) { Value = MenuEnabled };
            PARM[6] = new SqlParameter("@MenuVisible", SqlDbType.Char,1) { Value = MenuVisible };
            PARM[7] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[8] = new SqlParameter("@MenuSecured", SqlDbType.Char, 1) { Value = MenuSecured };
            return gf.UpdateData(PARM, "EgInsertMenu");
        }

        public void  GetRootMenuItems(DropDownList ddl)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserType", SqlDbType.VarChar, 20) { Value = UserType };
            PARM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            gf.FillListControl(ddl, "EgGetMenuRootItems","MenuDesc","MenuId", PARM);
            ddl.Items.Insert(0, new ListItem("--Select menu item--", "0"));
        }

        public DataTable GetChildMenuItems()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];
            //PARM[0] = new SqlParameter("@UserType", SqlDbType.VarChar, 20) { Value = UserType };
            //PARM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[0] = new SqlParameter("@MenuParentId", SqlDbType.Int) { Value = MenuParentId };
            return dt = gf.Filldatatablevalue(PARM, "EgGetChildMenu", dt, null);
        }
        /// <summary>
        /// Show Contact Detail in grid
        /// </summary>
        /// <param name="grd"></param>
        public void gridMenuItems(GridView grd)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[0];
            gf.FillGridViewControl(grd, PARM, "EgGetMenuItemsGrid");

        }

        public int UpdateMenuDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[10];

            PARM[0] = new SqlParameter("@UserType", SqlDbType.VarChar, 20) { Value = UserType };
            PARM[1] = new SqlParameter("@MenuDesc", SqlDbType.VarChar, 50) { Value = MenuDesc };
            PARM[2] = new SqlParameter("@NavigateUrl", SqlDbType.VarChar, 100) { Value = ActualNavigateUrl };
            PARM[3] = new SqlParameter("@MenuParentId", SqlDbType.Int) { Value = MenuParentId };
            //PARM[4] = new SqlParameter("@MenuSecured", SqlDbType.Bit) { Value = MenuSecured };
            PARM[4] = new SqlParameter("@ModuleId", SqlDbType.Int) { Value = ModuleId };
            //PARM[6] = new SqlParameter("@ObjectType", SqlDbType.Bit) { Value = ObjectType };
            PARM[5] = new SqlParameter("@OrderId", SqlDbType.Int) { Value = OrderId };
            //PARM[8] = new SqlParameter("@MenuEnabled", SqlDbType.Bit) { Value = MenuEnabled };
            PARM[6] = new SqlParameter("@MenuVisible", SqlDbType.Char, 1) { Value = MenuVisible };
            PARM[7] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[8] = new SqlParameter("@MenuId", SqlDbType.Int) { Value = menuid };
            PARM[9] = new SqlParameter("@MenuSecured", SqlDbType.Char, 1) { Value = MenuSecured };
            return gf.UpdateData(PARM, "EgUpdateMenuItems");
        }

        public string GetMenuVal()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@MenuId", SqlDbType.Int) { Value = menuid };
            return gf.ExecuteScaler(PARM, "EgGetChildMenuItemsUpdate");
        }


            #endregion
        }


}
