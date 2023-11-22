using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using EgDAL;
namespace EgBL
{
    public class EgAddNewDDOCodeBL
    {
        GenralFunction gf;
        public int flag { get; set; }
        public int ddo_code { get; set; }
        public string treas_code { get; set; }
        public string chkflag { get; set; }
        public string officename { get; set; }
        public string Status { get; set; }
        public string StrString { get; set; }

        public string errMSG
        {
            get
            {
                switch (flag)
                {
                    case 0: return " Retry !";                   
                    case 1: return "DDO Not Mapped In Treasury";
                    case 2: return "DDO Code Already Deleted from treasury";
                    case 3: return "Successfully Saved !";
                    case 4: return "Successfully Updated !";
                    default: return "Data Not Saved </br>";
                };
            }
        }

        public void EgGetDDOCodeDetails()
        {

            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@ddo_code", SqlDbType.Int) { Value = ddo_code };
            PM[1] = new SqlParameter("@treasCode", SqlDbType.Char, 4) { Value = treas_code };
            SqlDataReader dr = gf.FillDataReader(PM, "EgCheckExistDDONo");
            if (dr.HasRows)
            {
                dr.Read();
                ddo_code = Convert.ToInt32(dr[0].ToString());
                officename = dr[1].ToString();
                treas_code = dr[2].ToString();
                Status = dr[3].ToString().Trim();
                chkflag = "6";
                //dr.Close();
                //dr.Dispose();

            }
            else
            {
                chkflag = "3";
            }
            dr.Close();
            dr.Dispose();
        }

        public string getddoCodeDetails_L_Server(int ddoCode, string treascode)
        {
            gf = new GenralFunction();
            string PdDetail = "";
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@ddo_code", SqlDbType.Int) { Value = ddoCode };
            PM[1] = new SqlParameter("@treascode", SqlDbType.Char, 4) { Value = treascode };
            PdDetail = gf.ExecuteScaler(PM, "TrgGetddocodeDetailsLinkServer").ToString();
            return PdDetail;
        }

        public void InsertProperty()
        {
            string[] str = StrString.ToString().Split('|');


            if (str.Length > 1)
            {
                treas_code = str[0].ToString().Split('=').Last().ToString();
                officename = str[1].ToString().Split('=').Last().ToString();
                Status = str[2].ToString().Split('=').Last().ToString() == "" ? "A" : str[2].ToString().Split('=').Last().ToString();
                chkflag = "5";
            }
            else
            {
                chkflag = "4";
            }
        }

        public void InsertDDODetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@treas_code", SqlDbType.Char, 4) { Value = treas_code };
            PM[1] = new SqlParameter("@ddo_code", SqlDbType.Int) { Value = ddo_code };
            flag=Convert.ToInt16(gf.ExecuteScaler(PM, "EgInsertDDOCodeDetail"));
        }

    }
}
