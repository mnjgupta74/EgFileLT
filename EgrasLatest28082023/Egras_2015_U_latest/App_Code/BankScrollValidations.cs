using System;
using System.Web;
using System.Web.UI;
using EgBL;
using System.Xml.Linq;
using System.Collections;
using System.Linq;

/// <summary>
/// Class for Bank Scroll Field Check
/// </summary>


public class BankScrollValidations
{
    public BankScrollValidations()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int CheckScrollValidation(IEnumerable dataNodes, string BSRcode, int SubNodeCount, string Mode)
    {
        int Index = 0;
        int x = 0;
        bool chk;
        EgBankSoftCopyBL objBankSoftCopyBL = new EgBankSoftCopyBL();

        int NodesCount = dataNodes.OfType<XElement>().Count();
        if (NodesCount == 0)
        {
            x = 1;
            Message("File does not contains Records");
            goto loc;
        }

        foreach (XElement e in dataNodes)
        {


            Index = Index + 1;

            #region Node Check for Online
            if (BSRcode != "9910001" && BSRcode != "9920001" && BSRcode != "6390013")
            {
                // BudgetHead Check 
                if (e.Element("HeadAcc").Value.ToString().Trim().Length != 17)
                {
                    x = 1;
                    Message("Invalid Head Length on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                    goto loc;
                }
                else
                {
                    string[] Headlist = e.Element("HeadAcc").Value.ToString().Trim().Split('-');
                    string Bhead = Convert.ToString(Headlist[0] + Headlist[1] + Headlist[2] + Headlist[3] + Headlist[4]);
                    chk = objBankSoftCopyBL.IsNumeric(Bhead, 13, 'M');
                    if (chk == false)
                    {
                        x = 1;
                        Message("Invalid HeadAccount on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                        goto loc;
                    }
                }
            }

            // GRN Check
            chk = objBankSoftCopyBL.IsNumeric(e.Element("GRN").Value.ToString().Trim(), 18, 'M');
            if (chk == false)
            {
                x = 1;
                Message("Invalid GRN on node  " + Index);
                goto loc;
            }


            // RemitterName Check
            chk = objBankSoftCopyBL.IsChar(e.Element("RemitterName").Value.ToString().Trim(), 100, 'M');
            if (chk == false)
            {
                x = 1;
                Message("Invalid RemitterName on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                goto loc;
            }


            // Amount Check
            decimal decBankAmount = 0;
            string strAmount = e.Element("Amount").Value.ToString().Trim();
            chk = objBankSoftCopyBL.IsNumeric(strAmount, 15, 'M');
            chk = chk ? decimal.TryParse(strAmount, out decBankAmount) : chk;
            if (chk == false)
            {
                x = 1;
                Message("Invalid Amount on node " + Index + " , on Item [GRN] : " + e.Element("GRN").Value.ToString().Trim());
                goto loc;
            }


            // BankBSRcode Check
            if (BSRcode.Substring(0, 3) != e.Element("Bankcode").Value.ToString().Trim().Substring(0, 3))
            {
                x = 1;
                Message("Invalid BankCode on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                goto loc;
            }


            // BankRefNo Check
            chk = objBankSoftCopyBL.IsChar(e.Element("BankRefNo").Value.ToString().Trim(), 30, 'M');
            if (chk == false)
            {
                x = 1;
                Message("Invalid BankRefNo on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                goto loc;
            }


            // CIN Check

            if (e.Element("CIN").Value.ToString().Trim().Length == 20 && BSRcode == "1000132")
            {
                chk = objBankSoftCopyBL.IsChar(e.Element("CIN").Value.ToString().Trim(), 20, 'M');
                if (chk == false)
                {
                    x = 1;
                    Message("Invalid CIN on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                    goto loc;
                }
            }
            else if ( BSRcode != "1000132")
            {
                chk = true;
                //chk = objBankSoftCopyBL.IsChar(e.Element("CIN").Value.ToString().Trim(), 20, 'M');
                //if (chk == false)
                //{
                //    x = 1;
                //    Message("Invalid CIN on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                //    goto loc;
                //}

            }
            else
            {
                x = 1;
                Message("Invalid CIN Length on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                goto loc;
            }


            // Paid Date
            chk = objBankSoftCopyBL.IsDate(e.Element("paiddate").Value.ToString().Trim(), 'M');
            if (chk == false)
            {
                x = 1;
                Message("Invalid DateFormat on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                goto loc;
            }

            #endregion

            #region Payment Field Check for Manual and E-Pay

            if (SubNodeCount == 10)
            {
                if (Mode == "M") // for Manual
                {
                    //Payment Type
                    if (e.Element("PaymentType").Value.ToString().Trim() != "" && e.Element("PaymentType").Value != null)
                    {
                        chk = objBankSoftCopyBL.IsChar(e.Element("PaymentType").Value.ToString().Trim(), 1, 'M');
                        if (chk == false)
                        {
                            x = 1;
                            Message("Invalid PaymentType on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                            goto loc;
                        }
                    }

                    //BranchCode
                    if (e.Element("BranchCode").Value.ToString().Trim() != "" && e.Element("BranchCode").Value != null)
                    {


                        chk = objBankSoftCopyBL.IsChar(e.Element("BranchCode").Value.ToString().Trim(), 7, 'M');
                        if (chk == false)
                        {
                            x = 1;
                            Message("Invalid BranchCode on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                            goto loc;
                        }
                    }
                }
                else //for E-pay
                {
                    //debitbank
                    if (e.Element("debitbank").Value.ToString().Trim() != "" && e.Element("debitbank").Value != null)
                    {
                        chk = objBankSoftCopyBL.IsChar(e.Element("debitbank").Value.ToString().Trim(),15, 'M');
                        if (chk == false)
                        {
                            x = 1;
                            Message("Invalid DebitBank on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                            goto loc;
                        }
                    }

                    // debitbankref
                    if (e.Element("debitbankref").Value.ToString().Trim() != "" && e.Element("debitbankref").Value != null)
                    {
                        chk = objBankSoftCopyBL.IsChar(e.Element("debitbankref").Value.ToString().Trim(), 40, 'M');
                        if (chk == false)
                        {
                            x = 1;
                            Message("Invalid debitbankref on node " + Index + " , on Item [GRN] :" + e.Element("GRN").Value.ToString().Trim());
                            goto loc;
                        }
                    }
                }
            }

            #endregion

        }

    loc:
        return x; // x=0 for all Valid entries and x=1 for Wrong entries
    }

    private void Message(string str)
    {
        Page page = HttpContext.Current.Handler as Page;
        ClientScriptManager cs = page.ClientScript;
        cs.RegisterStartupScript(cs.GetType(), "PopupScript", "alert('" + str + "');", true);
    }

}
