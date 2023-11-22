using EgBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
/// <summary>
/// Summary description for Emitra
/// </summary>
public class Emitra:Banks
{
    private string BankCode;
    public Emitra()
    {
        KeyName = "Emitra";
        BankCode = "1000200";
        Version = "2.0";
    }

    public override string CallManualDataPushService(string cipherText)
    {
        throw new NotImplementedException();
    }

    public override string CallVerifyManualService(string cipherText)
    {
        throw new NotImplementedException();
    }

    public override string CallVerifyService(string cipherText)
    {
        throw new NotImplementedException();
    }
}