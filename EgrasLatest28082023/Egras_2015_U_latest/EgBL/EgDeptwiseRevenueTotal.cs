using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
   public class EgDeptwiseRevenueTotal
    {
        public  DateTime    _fromdate, _todate;
        private string      _flag;
        private int         _UserId;
        private string      _Location;
        private int         _Usertype;
        private string      _deptcode;

        public string Deptcode
        {
            get { return _deptcode; }
            set { _deptcode = value; }
        }
       
        public int UserType
        {
            get { return _Usertype; }
            set { _Usertype = value; }
        }
      
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public string Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
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

        public void RevenuePie(DataTable dt)
        {
            SqlParameter[] pm = new SqlParameter[4];
            GenralFunction gf = new GenralFunction();
            pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
            pm[0].Value = Fromdate;
            pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            pm[1].Value = Todate;
            pm[2] = new SqlParameter("@Location", SqlDbType.Char, 4);
            pm[2].Value = Location ;
            pm[3] = new SqlParameter("@Deptcode", SqlDbType.VarChar, 20);
            pm[3].Value = Deptcode;       
            gf.Filldatatablevalue(pm, "EgDeptRevenue", dt, null);
        }
        public void RevMonthOrDayWise(DataTable dt)
        {
            SqlParameter[] pm = new SqlParameter[5];
            GenralFunction gf = new GenralFunction();
            pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
            pm[0].Value = Fromdate;
            pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            pm[1].Value = Todate;
            pm[2] = new SqlParameter("@Flage", SqlDbType.Char, 1);
            pm[2].Value=Flag;
            pm[3] = new SqlParameter("@Deptcode", SqlDbType.VarChar, 20);
            pm[3].Value=Deptcode;
            pm[4] = new SqlParameter("@Location", SqlDbType.VarChar, 20);
            pm[4].Value=Location;
            gf.Filldatatablevalue(pm, "EgRevenueMonthORDayWise", dt, null);
        }
        public void RevUserWise(DataTable dt)
        {
            SqlParameter[] pm = new SqlParameter[4];
            GenralFunction gf = new GenralFunction();
            pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
            pm[0].Value = Fromdate;
            pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            pm[1].Value = Todate;
            pm[2] = new SqlParameter("@Location", SqlDbType.Char, 4);
            pm[2].Value = Location;
            pm[3] = new SqlParameter("@Deptcode", SqlDbType.VarChar, 20);
            pm[3].Value = Deptcode;
            gf.Filldatatablevalue(pm, "EgUserWiseRevOrTran", dt, null);
           
        }
        public void DeptWiseRevOrTrans(DataTable dt)
        { 
            SqlParameter[] pm = new SqlParameter[3];
            GenralFunction gf = new GenralFunction();
            pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
            pm[0].Value = Fromdate;
            pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            pm[1].Value = Todate;
            pm[2] = new SqlParameter("@Location", SqlDbType.Char, 4);
            pm[2].Value = Location;
            gf.Filldatatablevalue(pm, "EgUserWiseRevOrTran", dt, null);
        }
        public void DeptWiseRevOrTransNew(DataTable dt)
        { 
         SqlParameter[] pm = new SqlParameter[4];
            GenralFunction gf = new GenralFunction();
            pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
            pm[0].Value = Fromdate;
            pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            pm[1].Value = Todate;
            pm[2] = new SqlParameter("@Location", SqlDbType.Char, 4);
            pm[2].Value = Location;
            pm[3] = new SqlParameter("@Deptcode", SqlDbType.VarChar, 20);
            pm[3].Value = Deptcode;
            gf.Filldatatablevalue(pm, "EgDeptWiseRevOrTrens", dt, null);
        }
        public void UserWiseExpenduture(DataTable dt)
        {
            SqlParameter[] pm = new SqlParameter[4];
            GenralFunction gf = new GenralFunction();
            pm[0] = new SqlParameter("@FromDate", SqlDbType.DateTime);
            pm[0].Value = Fromdate;
            pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            pm[1].Value = Todate;
            pm[2] = new SqlParameter("@UserId", SqlDbType.Int);
            pm[2].Value=UserId;
            pm[3] = new SqlParameter("@Location", SqlDbType.Char, 4);
            pm[3].Value = Location;
            gf.Filldatatablevalue(pm, "EgUserWiseDeptOrExpenditure", dt, null);
        }
        public void RevMonthOrDayWiseNew(DataTable dt)
        {
            SqlParameter[] pm = new SqlParameter[2];
            GenralFunction gf = new GenralFunction();
            //pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
            //pm[0].Value = Fromdate;
            //pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            //pm[1].Value = Todate;
            pm[0] = new SqlParameter("@Deptcode", SqlDbType.VarChar, 20);
            pm[0].Value = Deptcode;
            pm[1] = new SqlParameter("@Location", SqlDbType.VarChar, 20);
            pm[1].Value = Location;
            gf.Filldatatablevalue(pm, "EgRevenueMonthORDayWiseNew", dt, null);
        }
        public void RevenuePieNew(DataTable dt)
        {
            SqlParameter[] pm = new SqlParameter[5];
            GenralFunction gf = new GenralFunction();
            pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
            pm[0].Value = Fromdate;
            pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            pm[1].Value = Todate;
            pm[2] = new SqlParameter("@Location", SqlDbType.Char, 4);
            pm[2].Value = Location;
            pm[3] = new SqlParameter("@Deptcode", SqlDbType.VarChar, 20);
            pm[3].Value = Deptcode;
            pm[4] = new SqlParameter("@flage", SqlDbType.Char, 1);
            pm[4].Value = Flag;
            gf.Filldatatablevalue(pm, "EgDeptRevenuenew", dt, null);
        }
       public string GetDeptCode()
       {
           string DeptCode = "0";
           GenralFunction gf = new GenralFunction();
           SqlParameter[] PARM = new SqlParameter[2];
           PARM[0] = new SqlParameter("@UserId", SqlDbType.Int);
           PARM[0].Value=UserId;
           PARM[1] = new SqlParameter("@UserType", SqlDbType.Int);
           PARM[1].Value = UserType;
           SqlDataReader dr = gf.FillDataReader(PARM, "EgGetDeptCode");
           if (dr.HasRows)
           {
               dr.Read();
               DeptCode = dr[0].ToString().Trim();
               
           }
            dr.Close();
            dr.Dispose();
            return DeptCode;
       }

       public void GetGrnDetail(DataTable dt)
       {
           SqlParameter[] pm = new SqlParameter[5];
           GenralFunction gf = new GenralFunction();
           pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
           pm[0].Value = Fromdate;
           pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
           pm[1].Value = Todate;
           pm[2] = new SqlParameter("@Location", SqlDbType.Char, 4);
           pm[2].Value = Location;
           pm[3] = new SqlParameter("@Deptcode", SqlDbType.VarChar, 20);
           pm[3].Value = Deptcode;
           pm[4] = new SqlParameter("@userId", SqlDbType.Int);
           pm[4].Value = UserId;
           gf.Filldatatablevalue(pm, "EgGrnview", dt, null);
       }
       public void GetGrnDetailNew(DataTable dt)
       {
           SqlParameter[] pm = new SqlParameter[5];
           GenralFunction gf = new GenralFunction();
           pm[0] = new SqlParameter("@fromDate", SqlDbType.DateTime);
           pm[0].Value = Fromdate;
           pm[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
           pm[1].Value = Todate;
           pm[2] = new SqlParameter("@usertype", SqlDbType.Int);
           pm[2].Value = UserType;
           pm[3] = new SqlParameter("@userId", SqlDbType.Int);
           pm[3].Value = UserId;
           gf.Filldatatablevalue(pm, "EgAllGrn", dt, null);
       }
    }
}
