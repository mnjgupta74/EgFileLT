using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EgBL;
using System.Security.Cryptography;
public partial class WebPages_IntegrationString : System.Web.UI.Page
{
    string path = "/WebPages/EgEchallanBank.aspx";
    string plainText = "";
    RemoteClass myremotepost = new RemoteClass();

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //// Test PlaintText for 0030  head

        //plainText = string.Format("AUIN={0}|Head_Name1={1}|Head_Amount1={2}|Head_Name2={3}|Head_Amount2={4}|Head_Name3={5}|Head_Amount3={6}|Head_Name4={7}|Head_Amount4={8}|Head_Name5={9}|Head_Amount5={10}|Head_Name6={11}|Head_Amount6={12}|Head_Name7={13}|Head_Amount7={14}|Head_Name8={15}|Head_Amount8={16}|Head_Name9={17}|Head_Amount9={18}|RemitterName={19}|Discount={20}|TotalAmount={21}|MerchantCode={22}|PaymentMode={23}| REGTINNO={24}| Location={25}| DistrictCode={26}|OfficeCode={27}| DepartmentCode={28}| FromDate={29}| ToDate={30}| Address={31}|PIN={32}| City={33}| Remarks={34}|Filler={35}",
        //           "14-05-2014-14-05-2014-310-8", "003002102020000000", "104.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "RajOnline RISL", "1.00", "221.00", "195", "N", "0", "2100", "21", "1887", "195", "2014/05/14", "2014/05/14", "RISL,Yojana bhawan Tilak Marg ,Jaipur", "302004", "Jaipur", "working", "A");

        // plainText = "AUIN=1230|Head_Name1=004100102010100082|Head_Amount1=30.0|Head_Name2=0|Head_Amount2=0.00|Head_Name3=0|Head_Amount3=0.00|Head_Name4=0|Head_Amount4=0.00|Head_Name5=0|Head_Amount5=0.00|Head_Name6=0|Head_Amount6=0.00|Head_Name7=0|Head_Amount7=0.00|Head_Name8=0|Head_Amount8=0.00|Head_Name9=0|Head_Amount9=0.00|RemitterName=RajOnline RISL|Discount = 0 |TotalAmount=30.0|MerchantCode=195|PaymentMode=N|REGTINNO=0|Location=2004|DistrictCode=12|OfficeCode=28357|DepartmentCode=104|FromDate=2014/08/14|ToDate=2014/08/31|Address=RISL,Yojana bhawan Tilak Marg ,Jaipur|PIN=302004| City=Jaipur| Remarks=Learner Licence| Filler=A|ChallanYear=1415";
        //plainText = "AUIN=100000000000000119|Head_Name1=085300102010100501|Head_Amount1=15.00|Head_Name2=085300800500100000|Head_Amount2=6.00|Head_Name3=085300800030100337|Head_Amount3=7.00|Head_Name4=0|Head_Amount4=0.00|Head_Name5=0|Head_Amount5=0.00|Head_Name6=0|Head_Amount6=0.00|Head_Name7=0|Head_Amount7=0.00|Head_Name8=0|Head_Amount8=0.00|Head_Name9=0|Head_Amount9=0.00|RemitterName=Elegant Premises Pvt. Ltd.|Discount=0|TotalAmount=28.00|MerchantCode=60|PaymentMode=N|REGTIONNO=08312853637|Location=0300|DistrictCode=28|OfficeCode= 3843|DepartmentCode=60|FromDate= 2014/09/23|ToDate=2014/09/23|Address=R.K. House Madanganj-Kishangarh (Raj)|PIN=305801|City=Kishangarh|Remarks=SampleRemarks|Filler=A|ChallanYear=1415";
        //plainText = "AUIN=100000000000000012|Head_Name1=801100105020100171|Head_Amount1=100.00|Head_Name2=0|Head_Amount2=0.00|Head_Name3=0|Head_Amount3=0.00|Head_Name4=0|Head_Amount4=0.00|Head_Name5=0|Head_Amount5=0.00|Head_Name6=0|Head_Amount6=0.00|Head_Name7=0|Head_Amount7=0.00|Head_Name8=0|Head_Amount8=0.00|Head_Name9=0|Head_Amount9=0.00|RemitterName=Mahi Cement Limited|Discount=0|TotalAmount=100.00|MerchantCode=101|PaymentMode=M|REGTIONNO=123456789|Location=0300|DistrictCode=28|OfficeCode= 1022|DepartmentCode=101|FromDate= 2014/09/27|ToDate=2014/09/27|Address=Mahi Cement, Parthavipura Near Village:- vajwana Teh:- Garhi Distt:- Banswara.|PIN=327001|City=Vajwana|Remarks=SampleRemarks|Filler=A|ChallanYear=1415";
        plainText = "AUIN=10021720141001|Head_Name1=801100105020100171|Head_Amount1=100.00|Head_Name2=0|Head_Amount2=0.00|Head_Name3=0|Head_Amount3=0.00|Head_Name4=0|Head_Amount4=0.00|Head_Name5=0|Head_Amount5=0.00|Head_Name6=0|Head_Amount6=0.00|Head_Name7=0|Head_Amount7=0.00|Head_Name8=0|Head_Amount8=0.00|Head_Name9=0|Head_Amount9=0.00|RemitterName=ADD SIPF JAIPUR DIV-II|Discount=0|TotalAmount=100.00||MerchantCode=101|PaymentMode=N|REGTINNO=0|Location=0300|DistrictCode=28|OfficeCode=1022|DepartmentCode=101|FromDate=1900/01/01|ToDate=1900/01/01|Address=k|PIN=302023| City=jaipur|Remarks=SIPF Sample Remark | Filler=A|ChallanYear=1415";
        EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
        string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        plainText = plainText + "|checkSum=" + CheckSum;
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        RemoteClass myremotepost = new RemoteClass();
        //string cipherText1 = "AukmoGxqL7pQJEcgH66odol7v7p2OF+wqRTq6M/Lf3uQXBdl60VbTUAG9gMik4o8by2fnj+9tpH/KcIPg3yMzO1VT6AWJbKDnvcy9Arl4pV5id5LuQHQ1F3inPrqFvB93i/7QH+BWiNTQzz+xboEK1BBwKD01Fqgqj/GGTzz3tguDOO1AB60LUN9o70El1RxYOOnxuiw0Bi5xvN53zTBtEL0SNn2BHKwmU+/azrVrbWQPuAtw09G2Pa7RcoEEVv3cX0QplT5w5aJOtAOBOocV4KdHKKVL8Qbj/x6Qi5aMQbkjJm6IXW9wbBtPLkHDcr9dTSYgbnY/f9l+FPv/9gc3oBMnKwXfwAI+KejVkkngR1N3B7gcJo4WZZiCMWNHwr2GVbyG7pTKQBRfQg3SXLxvi9VRdmywxT2jbm44HxDOG7JwIufTkwg7BJDA7w0enAok00oMi6GWVDoMzJ/wpo4o6oJGf2n9myhhxoOel+m2dsbyMzlSHFzZ/PNnRNktkfPNnc5nRY6iUeUHp0SDb9ANz73lh3zN/YwpjKLoRc6fQpXn0hgssP7GuQQrdOE0Pn+LK/slDvkRxMaaKJmtY9cr+GjTIvp8QaXsFjuynIAlJMLDwuwJm9LZPE/rsGLYOXUD4C49yBP2M9XveSAR54hCaqRAKJIn5Hvxg/TfQyJ5Cw1Sz16TkeRj2AW3x0rf6HWwLbQuck9eEWF3BRUTYeqnM3wo6J2h98WwjFGBv1MQIfNoWU3bKWNj5xhP/RjU1QBt8AKhrZvmb3T+dZ9MsMDN5ytOvC4Y/PzsOCI0DTmzWyDIIl3bx1IOT9dCAmhUdDF3H0vViFENXRYXauHoqjCZG2keVM+1GWi+M6Psd3b5NE5EpeLd3bdszbV8kSDtFA+m2aSoCQlyJbvlt8dYmt6PGHewqNXKtr6DcizjolFwNMuo1xeRS9KjrhnsSM+cBxmp4YCbl5XSyyoaP8EUY+SqA==";




        // string TestEncTest = "DehupUy52uW4Ebq7zouIyouwXpjpbDMy7vuADGmB4HyUrHT1qf8DkxaZNXgTmwBWtai3/9zbS4nPH/czO70mE705HkU7jwLljGlAxonJyuXw6hQTqRbzSE4wkrTL1OskoKe2UU3SG47LnOoc/HY5GaqpqbTP3CfwGBVTSrHtyQBJyyb07LJDkCab2QCw51EYhfFVe/WHtZysFem1sa+GPkMFuqF0AS2MOIGo60voCxbfE1Pg9uLTmo7WU+v0sMUJQS7XxJRFOtTch+ZJTjoqQhztvPO5LTLcDstkLZ7jeJR519gyFjCjuVQYTAYODtSdakQHKsLQgpnB5EZnf1blGuogi1elabwoLyWQYOhUhar+8fxFjQcbIJQ5cLOO9TsXSmQ2vKoQo9Ae+B6fpzhJ9nfOtEeHbH88Mtl/g0KDqHLpTl5vS/CQgiupazGU6SFvD4Do77KUkzaK53Hhwx6d6iyZ60dFBGj2cketzgDLV1daQeuY5jwu1macC17GMwVcaKy6RHLSvNDw3c6l+/Wtbv8NXrgdPKqyQNphmulKyt43vd7Y+H3A3rDUAoPzQEsIA4zrtk0Rtq+9hIFgcxazTA6YEriWmdWs4dPk2CtTSE4LNEgXk39/RkiqTaFZhBDX6BKv78sOK/1B7rnfKwKU6sFrii2rda2sxMkS9q+QAsCa1z5PPxrocdNGbgsPfcmV3N/Dy+TX+B4YcbiXBWBguK6CVunNs2lklca9beoxa52k64yWKyvoEaombJAyTd/BTeW/PWbSDph3YvIc3PbsOZSBW1SmTrWmx8PMIq0z6AiYUr0yGnDc9d3myyh9sf/Z";
        // string TestData = objEncry.Decrypt(TestEncTest, Server.MapPath("../webpages/Key/86.key"));

        // cipherText = "X1snOj3Ya3Yrh9jbE9EocdBYsRpK8udCb3ostHhtQuXgadJrXWLeefRV2y+5DGABfDIrvbqyvrPK  my/yALdE0Y1K+jz7b10EgJS2X7wmnCO53JQtZBX1a9p7B7MxpCCGns5AHqcSf8tjbbPcIqBtX6Ym  2o9qt/5W5pqqFjeaNMxm2mCnfs2Vei6cDkJuVjCu8+o5VTmpFiYfxFWEcnNU6U9/vn2q+2yP7rT2  Z9FkNa265geZTDjXoFWfIbmur69cJRMJz5ICPy9Z+Wnhrd+ABut0xpugzMq0ShVQxqDIhqguGBrL  K/od3sqDDLf+X1Pgv6Pjk/FPuTbhKBVfCA8Z1AN7+uP/TfmC93XWfLwSnF+B9jokutmquiWEvJHk  SEDna6tGJzEnurVdVV2mAkkEbTmVVEB2m04RyMxqhi9c/O6UwMVyJa0JcefNPzcpzBXQ22igOfOr  CJNMNFLc6lcJEaU5AYD/hvq3bOz/4m7ri39suRU/Ec17Mh6488/EmL3yrWCUc1a5dFoFXHDhpncz  tUuZTSBhaOmB5WajogO8lH56BxhEESBQc7zzfYtf+/FwsQMAb3jFWmv1h1+LK46OZSHAtPgUfMlV  UFXvFTEJsF1AWdt0CQVEmGAI1S2/oC3wabiRFRdVIeDn01JWvwhn0sH2rXHoSFiUx74yNFZBtj8j  5DmiukDhfTQi/UfstxguUVYi2H0qIfxIGkm8jdVMSghRbRrRWa1ckEEl5NOz5f0/cCyxRM3rK88Z  BRTzCVP84DF7+OHjCwJthu2JTb/TMhvwFIgs1owErjQ3LImL4vkHjxKGXw/HVfCI8JZfZH9TzD3/  gOA7oXIuGV7cxGxmT9UvGJbPiDipq4YeZaDsFWyVYcQfH2fcbNUeyhoFU9iuvQgWLg7L105y+Oh0  bB4Gqt17qe7DYN1qy7E2xTPuUQA7nnn/n03U7EBH+WW8UTPYZqevCO1q1uRNvJp4wRMZzsIjcIi+  F4vkVUwYs4G2DoU=";
        string RandomAUIN = Convert.ToString(RandomNumber(8, 10000000));
        string cipherText = objEncry.Encrypt("AUIN="+RandomAUIN+"|Head_Name1=085301102010100501|Head_Amount1=3500.00|Head_Name2=0|Head_Amount2=0|Head_Name3=0|Head_Amount3=0|Head_Name4=0|Head_Amount4=0.00|Head_Name5=0|Head_Amount5=0.00|Head_Name6=0|Head_Amount6=0.00|Head_Name7=0|Head_Amount7=0.00|Head_Name8=0|Head_Amount8=0.00|Head_Name9=0|Head_Amount9=0.00|RemitterName=ASSOCIATED SOAPSTONE DISTRIBUTING|Discount=0|TotalAmount=3500.00|MerchantCode=5001|PaymentMode=N|REGTIONNO=08261702517|Location=3200|DistrictCode=33|OfficeCode=20612|DepartmentCode=60|FromDate=2014/12/19|ToDate=2014/12/19|Address=24 Akashwani Marg Post Box No 3 MIA|PIN=313003|City=Udaipur|Remarks=SampleRemarks|Filler=A|ChallanYear=1415", Server.MapPath("../webpages/Key/31.key"));

        //cipherText = objEncry.Encrypt("AUIN=100003|Head_Name1=003900105010000512|Head_Amount1=1.00|Head_Name2=003900105010000251|Head_Amount2=1.00|Head_Name3=0|Head_Amount3=0.00|Head_Name4=0|Head_Amount4=0.00|Head_Name5=0|Head_Amount5=0.00|Head_Name6=0|Head_Amount6=0.00|Head_Name7=0|Head_Amount7=0.00|Head_Name8=0|Head_Amount8=0.00|Head_Name9=0|Head_Amount9=0.00|RemitterName=JAGATJIT|Discount=0|TotalAmount=2|MerchantCode=31|PaymentMode=N|REGTINNO=08100700789|Location=0400|DistrictCode=31|OfficeCode=4981|DepartmentCode=31|FromDate=2014/05/01|ToDate=2014/05/31|Address=SP 1-3, RIICO INDUSTRIAL AREA,|PIN=302017|City=Behror|Remark=Test|Filler=A|ChallanYear=1415|Checksum=c8be9cf9b31d4f566513e8dc12d83558", Server.MapPath("../webpages/Key/31.key"));

        string Address = "http://localhost:5484/Server%20-%20Server/SampleMerchantpreLogin.aspx";
        myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
        myremotepost.Add("merchant_code", "31");
        myremotepost.Add("AUIN", RandomAUIN); //Convert.ToString(RandomNumber(8, 10000000)));
        myremotepost.Add("URL", Address);
        myremotepost.Url = Address;
        myremotepost.Post();



    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //RemoteClass myremotepost = new RemoteClass();
        //EncryptionAndDecryptionBL objEncry = new EncryptionAndDecryptionBL();
        //Rijndael myRijndael = Rijndael.Create();
        //string plainText = BudgetHead1.Text + "@" + BudgetHead2.Text + "@" + BudgetHead3.Text + "@" + BudgetHead4.Text + "@" + 2;   // original plaintext
        //string passPhrase = "Pas5pr@se";        // can be any string
        //string saltValue = "s@1tValue";        // can be any string
        //string hashAlgorithm = "SHA1";             // can be "MD5"
        //int passwordIterations = 2;                  // can be any number
        //string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        //int keySize = 256;                // can be 192 or 128
        //plainText = objEncry.EncryptNew(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        //myremotepost.Add("String1", plainText.ToString());
        //myremotepost.Url = "http://localhost:51284/EgrasWebSite/WebPages/EgEchallanBank.aspx";
        //myremotepost.Post();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //RemoteClass myremotepost = new RemoteClass();
        //EncryptionAndDecryptionBL objEncry = new EncryptionAndDecryptionBL();
        //Rijndael myRijndael = Rijndael.Create();
        //string plainText = BudgetHead1.Text + "@" + BudgetHead2.Text + "@" + BudgetHead3.Text + "@" + BudgetHead4.Text + "@" + 0;   // original plaintext
        //string passPhrase = "Pas5pr@se";        // can be any string
        //string saltValue = "s@1tValue";        // can be any string
        //string hashAlgorithm = "SHA1";             // can be "MD5"
        //int passwordIterations = 2;                  // can be any number
        //string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        //int keySize = 256;                // can be 192 or 128
        //plainText = objEncry.EncryptNew(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        //myremotepost.Add("String1", plainText.ToString());
        //myremotepost.Url = "http://localhost:51284/EgrasWebSite/WebPages/EgEchallanBank.aspx";
        //myremotepost.Post();
    }
    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
}
