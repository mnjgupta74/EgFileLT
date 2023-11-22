using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace EgBL
{
   public  class Egpurpose_Visible
    {
       public bool Visible(Int64 grn)
       
       {    
           GenralFunction gf= new GenralFunction ();
           SqlParameter[] PARM = new SqlParameter[1];
           PARM[0] = new SqlParameter("@grn", SqlDbType.Int) { Value = grn };
           string result = gf.ExecuteScaler(PARM, "EgVisiblePurpose");
           if (result == "1")
              return false;
           else
              return true;
          
      
       }
    }
}
