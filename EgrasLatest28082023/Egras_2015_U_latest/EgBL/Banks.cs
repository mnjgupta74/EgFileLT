using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EgBL
{
    public class Banks : iBanks 
    {
        public string MerchantCode;
        public string Url;
        private  string  bsrcode;
        public Banks (string bsrcode)
        {
          this.bsrcode=bsrcode; 
        }

        public iBankForward getBanks()
        {
           
            
            EgEChallanBL ObjEchallan = new EgEChallanBL();
            ObjEchallan.BankName = bsrcode;
            DataTable bankData=ObjEchallan.GetBankDetail();
            MerchantCode = bankData.Rows[0][1].ToString();
            Url = bankData.Rows[0][0].ToString();
            switch (bsrcode)
            {
                case "0350316":
                    return new CB(Url, MerchantCode);
                case "0240539":
                    return new Canara(Url, MerchantCode);
                //case "0171051":
                //    return new SBBJ(Url, MerchantCode);
                //case "0292861":
                //    return new UnionBank(Url, MerchantCode);
                //case "0200113":
                //    return new BOB(Url, MerchantCode);
                //case "0304017":
                //    return new PNB(Url, MerchantCode);
                //case "6910213":
                //    return new IDBI(Url, MerchantCode);
                //case "0280429":
                //    return new CBI(Url, MerchantCode);
                //case "0361193":
                //    return new OBC(Url, MerchantCode);
                //case "0230001":
                //    return new BOM(Url, MerchantCode);
                //case "0350316":
                //    return new CB(Url, MerchantCode);
                default:
                    break;
            }
            return null;
        }

    }
}
