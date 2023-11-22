using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgBL
{
   public class EgVisibility
    { 
       private  EgDepartment_visible  objDept = new EgDepartment_visible();
     //  private Egpurpose_Visible objPurpose = new Egpurpose_Visible();
      // private EgHead_Visible objHead = new EgHead_Visible();

 

    public bool IsEligible(Int64 grn)
    {

      bool eligible = true;
      if (!objDept.Visible(grn))
        {
             eligible = false;
             return eligible;
        }
      //else if (!objPurpose.Visible (grn))
      //{
      //     eligible = false;
      //      return eligible;
      //}

      //else if (!objHead.Visible(grn))

      //{

      //  eligible = false;
      //  return eligible;

      // }

      return eligible;

    }

  }
    }

