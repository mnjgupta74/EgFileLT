using EgBL;
using System;
using System.Web.Services;

public partial class WebPages_Admin_BankModeUtility : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    [WebMethod]
    public static string GetData()
    {
        EgBankAccessBL objEgBankAccessBL = new EgBankAccessBL();
        return objEgBankAccessBL.GetBankListToUpdate();
    }
    [WebMethod]
    public static string InsertData(string BSRCode, string BankName, string BranchName, string IFSC, string MICRCode, string Address, string BankType, string Mode, string TreasuryCode, string acno, string BankBranchCode, string RBICode, bool ChequePrint)
    {
        EgBankAccessBL objEgBankAccessBL = new EgBankAccessBL();
        objEgBankAccessBL.BSRCode = BSRCode;
        objEgBankAccessBL.Access = Mode;
        objEgBankAccessBL.TreasuryCode = TreasuryCode;
        objEgBankAccessBL.BankName = BankName;
        objEgBankAccessBL.BranchName = BranchName;
        objEgBankAccessBL.IFSC = IFSC;
        objEgBankAccessBL.MICRCode = MICRCode;
        objEgBankAccessBL.Address = Address;
        objEgBankAccessBL.BankType = BankType;
        objEgBankAccessBL.acno =Convert.ToInt64(acno);
        objEgBankAccessBL.BankBranchCode = Convert.ToInt32(BankBranchCode);
        objEgBankAccessBL.RBICode = RBICode;
        if (ChequePrint ==true)
            objEgBankAccessBL.ChequePrint = "1";
        else
            objEgBankAccessBL.ChequePrint = "0";
        return objEgBankAccessBL.InsertBankData();
    }
}