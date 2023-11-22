using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
 using EgDAL;
namespace EgBL
{
   public  class EgAgSoftcopyBL
    {
         GenralFunction gf = new GenralFunction();
             #region Class Variables
             private DateTime _DateFrom, _DateTo;
             private string _TreasuryCode,_Type;
             SqlDataReader dtr = null;
             #endregion
       #region Class Properties

        public DateTime  DateFrom
        {
           get { return _DateFrom; }
           set { _DateFrom = value; }
        }
        public DateTime  DateTo
        {
           get { return _DateTo; }
           set { _DateTo = value; }
         }

        public string TreasuryCode
        {
           get { return _TreasuryCode; }
           set { _TreasuryCode = value; }
        }
 
        public string  Type
        {
           get { return _Type; }
           set { _Type = value; }
        }
      

     #endregion

     #region Class Methods

      private  SqlDataReader GetData(string spname)
       {
           SqlParameter[] PM = new SqlParameter[2];
           PM[0] = new SqlParameter("@DateFrom",SqlDbType.DateTime );
           PM[0].Value = _DateFrom;
           PM[1]= new SqlParameter("@DateTo",SqlDbType.DateTime );
           PM[1].Value=_DateTo;
           //PM[2] = new SqlParameter("@treasurycode", SqlDbType.Char,4);
           //PM[2].Value = _TreasuryCode;
           //PM[3] = new SqlParameter("@type", SqlDbType.Char, 4);
           //PM[3].Value = _Type ;
          
           SqlDataReader  dtr = gf.FillDataReader(PM, spname );
           return dtr;
 
       }
     
      public void fillTreasury(DropDownList lst)
      {
          GenralFunction gf = new GenralFunction();
          SqlParameter[] PM = new SqlParameter[2];
          PM[0] = new SqlParameter("@treascode", SqlDbType.Char, 4);
          PM[0].Value = TreasuryCode;
          PM[1] = new SqlParameter("@tbname", SqlDbType.Char, 20);
          PM[1].Value = "TreasuryMst";
          gf.FillListControl(lst, "trggetlist", "Treasuryname", "Treasurycode", PM);

      }

     // write data from database to text file 
       public  void WriteToFile(string filename,string spname)
       {
         
               //StreamWriter SW;
               //SW = File.CreateText(filename);
               //dtr = GetData(spname );

               //if (dtr.HasRows)
               //{
               //    while (dtr.Read()) // getting rows of datareader
               //    {
               //        if(dtr[0].ToString()!="")
               //        SW.WriteLine(dtr[0].ToString());
               //    }

               //}

               //dtr.Close();
               //SW.Close();

//----------------------------------------------------------------------------------------

           FileStream fs = new FileStream(filename, FileMode.CreateNew);

           StreamWriter SW = new StreamWriter(fs, Encoding.ASCII);

           dtr = GetData(spname);

           if (dtr.HasRows)
           {
               while (dtr.Read()) // getting rows of datareader
               {
                   if (dtr[0].ToString() != "")
                       SW.WriteLine(dtr[0].ToString());
               }

           }

           dtr.Close();
          
           SW.Close();
       }

       private string _budgethead;
       public string budgethead
       {
           get { return _budgethead; }
           set { _budgethead = value; }
       }
      

       //BudgetHead SoftCopy

       private SqlDataReader GetDataSoftcopy(string spname)
       {
           SqlParameter[] PM = new SqlParameter[4];
           PM[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
           PM[0].Value = DateFrom;
           PM[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
           PM[1].Value = DateTo;
           //PM[2] = new SqlParameter("@budgethead", SqlDbType.Char, 13);
           //PM[2].Value = budgethead;
           PM[2] = new SqlParameter("@budgethead", SqlDbType.Char, 13);
           PM[2].Value = budgethead;
           PM[3] = new SqlParameter("@treasurycode", SqlDbType.Char, 4);
           PM[3].Value = TreasuryCode;
           //PM[4] = new SqlParameter("@type", SqlDbType.Char, 4);
           //PM[4].Value = _Type;

           SqlDataReader dtr = gf.FillDataReader(PM, spname);
           return dtr;

       }


       public void WriteToFileSoftCopy(string filename, string spname)
       {



           FileStream fs = new FileStream(filename, FileMode.CreateNew);

           StreamWriter SW = new StreamWriter(fs, Encoding.ASCII);

           dtr = GetDataSoftcopy(spname);

           if (dtr.HasRows)
           {
               while (dtr.Read()) // getting rows of datareader
               {
                   if (dtr[0].ToString() != "")
                       SW.WriteLine(dtr[0].ToString());
               }

           }

           dtr.Close();
           SW.Close();
       }

     #endregion



 }
}
