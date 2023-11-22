using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;


namespace EgBL
{
    
    public class VerifySignature
    {
        EgErrorHandller ObjErrj = new EgErrorHandller();
        public bool VerifyXmlFile(XmlDocument xml, X509Certificate2 cert)
        {
            bool flag;
            SignedXml signedXml = new SignedXml(xml);
           
            XmlNodeList nodeList = xml.GetElementsByTagName("Signature");
            signedXml.LoadXml((XmlElement)nodeList[0]);
            try
            {
                flag = signedXml.CheckSignature(cert,true);
            }
            catch (Exception ex)
            {
                ObjErrj.InsertError(ex.ToString());
                flag = false;
            }
            return flag;
        }

    }
    
}
