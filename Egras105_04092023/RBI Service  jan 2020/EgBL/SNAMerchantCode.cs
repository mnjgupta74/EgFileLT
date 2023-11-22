using System.Collections.Generic;

/// <summary>
/// Summary description for SNAMerchantCode
/// </summary>
public class SNAMerchantCode
{
    public SNAMerchantCode()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    static Dictionary<string, string> keydictionary = new Dictionary<string, string>
    {
       {"9930001","PayTMKey"},
       {"9910001","9910001"},
       {"6001","6001"},
       {"0200113","BOB1"},
       {"2110109","AU"},
       {"6003","6003"},
       {"6390013","ICICI"},
       {"0322170","UCO"},
       {"0240539","CANARA256"},
       {"0292861","Union_Bank256"},
       {"0304017","PNB256"},

       {"0006326","RAJASTHAN_EGRASS"},
       {"5018","5018"},// For SSp Integration
       { "9970001","Emitra"},
       { "6360010","Axis"},//Axis
       { "0510055","HDFC"},//HDFC
         {"5028","5028"}, // For SSp Integration
         {"9950001","Billdesk"}, // Billdesk

     };

    // 0 Mean   7 Parameter    (Bank)
    // 1 Mean  11 Parameter     (Payment Gateway)
    static Dictionary<string, string> parameterListDictionary = new Dictionary<string, string>
    {
            { "9910001","1" },//PAYU
            { "0304017", "0"},//new PNB() 
            { "0006326", "0"},//SBI() 
            { "0200113", "0"},//BOB("N")
            { "0292861", "0"},//UnionBank()
            { "6910213", "0"},//IDBI()
            { "0280429", "0"},//CBI() 
            { "0361193", "0"},//OBC() 
            { "0240539", "0"},// Canara()
            { "1000132", "1"},//SBIePay() 
            { "0171051", "0"},//TestBank()
            { "0220123", "0"},//BOI()
            { "0281065", "0"},//CBI() 
            { "9920001", "0"},//PineLab()
            { "9930001", "1"},//PaytmBank() 
            { "6390013", "0"},//ICIC() 
            { "1000200","0"},
            { "6360010","0"},//Axis
            { "0510055","0"},//HDFC
            { "9950001","1"},//BillDesk

     };
    public string GetKey(string code)
    {
        return keydictionary[code];
    }
    public string GetParameterListCountFlag(string code)
    {
        return parameterListDictionary[code];
    }


    static Dictionary<string, string> bsrdictionary = new Dictionary<string, string>
    {

       {"9930001","PayTMKey"},
       {"9910001","9910001"},
       {"6001","6001"},
       {"0200113","BOB1"},
       {"6002","6002"},
       {"6003","6003"},
       {"5018","5018"},// For SSp Integration
       { "9970001","Emitra"},
       { "0006326","RAJASTHAN_EGRASS"},
       { "6360010","Axis"},
       { "6390013","ICICI"},
       { "0510055","HDFC"},
       {"5028","5028"},// For SSp Integration
       {"9950001","Billdesk"}
     };
    public string GetBankName(string code)
    {

        return bsrdictionary[code];

    }
}