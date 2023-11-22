using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace EgBL
{
   public class EgDeptIntegrationController
    {
       public DataTable GetIntegrationData(EgDeptInterfaceBL objEgDeptInterfaceBL)
       {
           return objEgDeptInterfaceBL.SetDeptIntegrationData();
       }
    }
}
