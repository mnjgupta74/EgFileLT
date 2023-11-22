using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{

    public class EgChallanDetail
    {
       GenralFunction gf = new GenralFunction();

       #region Properties

       public string  TreasuryCode { get; set; }
      // public string DistrictName { get; set; }
       public long GRN { get; set; }
      // public int Pincode { get; set; }
       //public double TotalAmount { get; set; }
       public DateTime fromDate { get; set; }
       public DateTime Todate { get; set; }
       public int Usertype { set;get; }
       public int Userid { set; get; }
     //  public int challanno { set; get; }
      // public  int deptid { set; get; }
      // public int officeid { set; get; }
      // public int locationid { set; get; }
       //public string Bankcode { set; get; }
       public Int32 StartIdx {set;get;}
       public Int32 EndIdx { set; get; }
        public Int32 pageIndex { get; set; }
        #endregion


        #region functions

        //public void FillDropdown(DropDownList ddl)
        //{
        //    gf.FillListControl(ddl, "EgFillLocation", "DistrictName", "TreasuryCode", null);
        //    ddl.Items.Insert(0, new ListItem("--Select Location--", "0"));

        //}


        public DataTable FillSearchRecord()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[6];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PM[1] = new SqlParameter("@fromDate", SqlDbType.Date) { Value = fromDate };
            PM[2] = new SqlParameter("@toDate", SqlDbType.Date) { Value = Todate };
            PM[3] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Userid };
            PM[4] = new SqlParameter("@Usertype", SqlDbType.Int) { Value = Usertype };
            PM[5] = new SqlParameter("@PageIndex", SqlDbType.Int) { Value = pageIndex };
            
            return gf.Filldatatablevalue(PM, "EgGetChallanDetails", dt, null);
        }

        //public void FillDepartmentlist(DropDownList ddl)
        //{
        //    SqlParameter[] PM = new SqlParameter[1];
        //    PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Userid };
        //    gf.FillListControl(ddl, "EgGetSelectedDepartmentList", "deptnameEnglish", "DeptCode", PM);

        //}
        //public void GetOfficeIdList()
        //{
        //   SqlParameter[] PM = new SqlParameter[1];
        //   PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Userid };
        //   SqlDataReader dr = gf.FillDataReader(PM, "EgSetOfficeForSearch");
        //   if (dr.HasRows)
        //   {
        //       dr.Read();
        //       deptid  = Convert.ToInt32(dr[0].ToString().Trim());
        //       locationid  = Convert.ToInt32 (dr[1].ToString().Trim());
        //       officeid =   Convert.ToInt32(dr[2].ToString().Trim());
        //       dr.Close();
        //       dr.Dispose();
        //   }
        //}

        public string EchallanString()
        {
            string str = "";
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];

            PARM[0] = new SqlParameter("@GRN", SqlDbType.Int) { Value = GRN };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };

            dt = gf.Filldatatablevalue(PARM, "EGEChallanViewService", dt, null);

            for (int j = 0; j < dt.Rows.Count; j++)
            {

                #region Filling FormData
                if (dt.Rows[j]["rowno"].ToString() == (0).ToString())
                {

                    str = "GRN =" + dt.Rows[j]["GRN"].ToString() + " | ";
                    str = str + "ChallanDate =" + dt.Rows[j]["ChallanDate"].ToString() + " | ";
                    if (dt.Rows[j]["Profile"] != DBNull.Value)
                    {
                        str = str + "Department =" + dt.Rows[j]["Profile"].ToString() + " | ";
                    }
                    else
                    {
                        str = str + "Department =" + 0 + " | ";
                    }

                    str = str + "Identity =" + dt.Rows[j]["Identity"].ToString() + " | ";
                    str = str + "OfficeName =" + int.Parse(dt.Rows[j]["OfficeName"].ToString()) + " | ";
                    str = str + "Office =" + dt.Rows[j]["office"].ToString().Trim() + " | ";
                    str = str + "Challanno =" + dt.Rows[j]["Challanno"].ToString() + " | ";
                    str = str + "PanNumber =" + dt.Rows[j]["PANNumber"].ToString() + " | ";
                    str = str + "Location =" + dt.Rows[j]["TreasuryName"].ToString() + " | ";
                    str = str + "FullName =" + dt.Rows[j]["FullName"].ToString() + " | ";
                    str = str + "ChallanYear =" + dt.Rows[j]["ChallanYear"].ToString() + " | ";
                    str = str + "ChallanFromMonth =" + Convert.ToDateTime(dt.Rows[j]["ChallanFromMonth"]) + " | ";
                    str = str + "ChallanToMonth =" + Convert.ToDateTime(dt.Rows[j]["ChallanToMonth"]) + " | ";
                    str = str + "Address =" + dt.Rows[j]["Address"].ToString() + " | ";
                    str = str + "City =" + dt.Rows[j]["City"].ToString() + " | ";
                    str = str + "PINCode =" + dt.Rows[j]["PINCode"].ToString() + " | ";
                    str = str + "DeductCommission =" + Convert.ToDouble(dt.Rows[j]["DeductCommission"]) + " | ";
                    str = str + "TotalAmount =" + dt.Rows[j]["TotalAmount"].ToString() + " | ";
                    str = str + "ChequeDDNo =" + dt.Rows[j]["ChequeDDNo"].ToString() + " | ";
                    str = str + "BankName =" + dt.Rows[j]["BankName"].ToString() + " | ";
                    str = str + "BankCode =" + dt.Rows[j]["BankCode"].ToString() + " | ";
                    str = str + "TypeofPayment =" + dt.Rows[j]["Paymenttype"].ToString() + " | ";
                    str = str + "Remark =" + dt.Rows[j]["Remarks"].ToString() + " | ";
                    str = str + "PanNumber1 =" + dt.Rows[j]["PanNumber1"].ToString() + " | ";
                    str = str + "zone =" + dt.Rows[j]["zone"].ToString() + " | ";
                    str = str + "Circle =" + dt.Rows[j]["Circle"].ToString() + " | ";
                    str = str + "Ward =" + dt.Rows[j]["Ward"].ToString() + " | ";
                    str = str + "ProfileCode =" + Convert.ToInt32(dt.Rows[j]["ProfileCode"]) + " | ";

                    if (dt.Rows[j]["CIN"] != DBNull.Value)
                    {
                        str = str + "CIN =" + Convert.ToString(dt.Rows[j]["CIN"]) + " | ";
                    }
                    else
                    {
                        str = str + "CIN =" + 0 + " | ";
                    }

                    if (dt.Rows[j]["Ref"] != DBNull.Value)
                    {
                        str = str + "Ref =" + Convert.ToString(dt.Rows[j]["Ref"]) + " | ";
                    }
                    else
                    {
                        str = str + "Ref =" + 0 + " | ";
                    }

                    str = str + "TransDate =" + Convert.ToString(dt.Rows[j]["TransDate"]) + " | ";
                    str = str + "DeptNameEnglish =" + Convert.ToString(dt.Rows[j]["DeptNameEnglish"]) + " | ";
                    str = str + "TreasuryName =" + dt.Rows[j]["TreasuryName"].ToString() + " | ";
                    str = str + "DistrictName =" + dt.Rows[j]["District"].ToString() + " | ";

                    if (dt.Rows[j]["Status"] != DBNull.Value)
                    {
                        str = str + "Status =" + Convert.ToString(dt.Rows[j]["Status"]) + " | ";
                    }
                    else
                    {
                        str = str + "Status =" + " " + " | ";
                    }

                    str = str + "UserId =" + dt.Rows[j]["UserId"].ToString() + " | ";
                    str = str + "Details =" + dt.Rows[j]["Details"].ToString() + " | ";
                    str = str + "pdacc =" + dt.Rows[j]["pdacc"].ToString() + " | ";
                    str = str + "TreasuryCode =" + dt.Rows[j]["Tcode"].ToString() + " | ";
                    str = str + "ChallanType =" + dt.Rows[j]["ChallanType"].ToString() + " < ";

                }
                #endregion

                #region Filling SchemaAmount
                if (dt.Rows[j]["rowno"].ToString() == (1).ToString())
                {

                    str = str + "SchemaName =" + dt.Rows[j]["SCHEMANAME"].ToString() + " | ";
                    str = str + "Amount =" + dt.Rows[j]["Amount"].ToString() + " | ";

                }
                #endregion


            }
            return str;
        }
        #endregion

    }
}
