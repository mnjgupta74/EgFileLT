using System;
using System.Data;
using System.Linq;
using EgBL;
using System.Web.UI;
using System.Web.Services;

public partial class WebPages_Admin_EgAddNewPDAccNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            
        }
    }
    [WebMethod]
    public static String[] Show(int PDDiv, string TreasCode, string OfficeCode, string RBLValue)
    {
        string BudgetHeadOfficeCode = OfficeCode;
        try
        {
            if (RBLValue == "pd")
            {
                EgAddNewPDAccNoBL objEgAddNewPDAccNOBL = new EgAddNewPDAccNoBL();
                objEgAddNewPDAccNOBL.PDAccNo = Convert.ToInt32(PDDiv);
                objEgAddNewPDAccNOBL.TreasuryCode = TreasCode.ToString().Trim();
                //-- calling GetDetail--> sp - "EgGetPDAccNoDetails"
                objEgAddNewPDAccNOBL.EgGetPDAccNoDetails();
                return AssignValues(RBLValue, objEgAddNewPDAccNOBL.chkflag.ToString(), objEgAddNewPDAccNOBL.PDAccNo.ToString(),objEgAddNewPDAccNOBL.PDAccName.ToString(),objEgAddNewPDAccNOBL.TreasuryCode.ToString(),objEgAddNewPDAccNOBL.BudgetHead.ToString(), objEgAddNewPDAccNOBL.Output);
            }
            else
            {
                EgAddNewDivCodeBL objEgAddNewDivCodeBL = new EgAddNewDivCodeBL();
                objEgAddNewDivCodeBL.DivCode = Convert.ToInt32(PDDiv);
                objEgAddNewDivCodeBL.treasuryCode = TreasCode.ToString().Trim();
                objEgAddNewDivCodeBL.OfficeCode = Convert.ToInt32(OfficeCode);
                objEgAddNewDivCodeBL.EgGetDivCodeDetails();
                return AssignValues(RBLValue, objEgAddNewDivCodeBL.flag.ToString(), objEgAddNewDivCodeBL.DivCode.ToString(), objEgAddNewDivCodeBL.DivName.ToString(), objEgAddNewDivCodeBL.treasuryCode.ToString(), objEgAddNewDivCodeBL.OfficeCode.ToString(), objEgAddNewDivCodeBL.Output);
            }
        }
        catch (Exception ex)
        {
            throw ex; 
        }
    }
    public static String[] AssignValues(string Rbvalue, string flag, string PDDivNo, string PDDivName, string TreasuryCode, string BudgetHeadOfficeCode, int Output)
    {
        String[] strValues;
        strValues = new String[] { "", "", "", "", flag, "true", "Submit", "false" };
        if (Output == 0)
        {
            if (flag != "1")
            {
                strValues[0] = PDDivNo;
                strValues[1] = PDDivName;
                strValues[2] = BudgetHeadOfficeCode;
                strValues[3] = TreasuryCode;
                strValues[4] = flag;
                if (strValues[4] == "D")
                    strValues[5] = "false";//Button Enable property Value
                strValues[7] = "true";//Detail div visible property
            }
            else
            {
                strValues[5] = "false";
                strValues[6] = "false";//PDAccNo is Not mapped In Treasury
                strValues[7] = "false";//Detail div visible property
            }
        }
        else
        {
            strValues[0] = PDDivNo;
            strValues[1] = PDDivName;
            strValues[2] = BudgetHeadOfficeCode;
            strValues[3] = TreasuryCode;
            strValues[4] = flag;
            strValues[6] = "ReVerify";
            strValues[7] = "true";//Detail div visible property
        }
        return strValues;
    }
    [WebMethod]
    public static String UpdateData(int PDDivNo, string TreasCode, string OfficeCode, string RBLValue)
    {
        try
        {
            if (RBLValue == "pd")
            {
                EgAddNewPDAccNoBL objEgAddNewPDAccNOBL = new EgAddNewPDAccNoBL();
                objEgAddNewPDAccNOBL.PDAccNo = Convert.ToInt32(PDDivNo);
                objEgAddNewPDAccNOBL.TreasuryCode = TreasCode.ToString().Trim();
                objEgAddNewPDAccNOBL.InsertOfficeDetail();
                return objEgAddNewPDAccNOBL.errMSG;
                //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objEgAddNewPDAccNOBL.errMSG + "');", true);
                //btnSubmitUpdate.Visible = false;
            }
            else
            {
                EgAddNewDivCodeBL objEgAddNewDivCodeBL = new EgAddNewDivCodeBL();
                objEgAddNewDivCodeBL.DivCode = Convert.ToInt32(PDDivNo);
                objEgAddNewDivCodeBL.treasuryCode = TreasCode.ToString().Trim();
                objEgAddNewDivCodeBL.OfficeCode = Convert.ToInt32(OfficeCode);
                objEgAddNewDivCodeBL.InsertDivisionDetail();
                return objEgAddNewDivCodeBL.errMSG;
                //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objEgAddNewDivCodeBL.errMSG + "');", true);
                //btnSubmitUpdate.Visible = false;
            }
        }
        catch (Exception ex)
        {
            return "<B>Data Not Saved </br>" + ex.Message.ToString() + "</B>";
            //lblmsg.Visible = true;
        }
    }
}
