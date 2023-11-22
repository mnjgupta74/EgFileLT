using System;
using System.Linq;
using System.Data;

namespace EgBL
{
    public class EgSetIntegrationDataBL
    {
        DataTable dtCheckHead;
        public DataTable SetIntegrationData(string PlainText, string AUIN)
        {
            try
            {
                DataTable dtHeadDetails = new DataTable();
                EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();
                EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
                string[][] ChallanValues = PlainText.Trim().Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(splitequal => splitequal.Split('=')).ToArray();
                #region Check AUIN is  matching and not
                if (ChallanValues[0][1].ToString().Trim() != AUIN.ToString().Trim())
                {
                    throw new Exception("AUIN Number in plaintext is not matching with post AUIN number.");
                }
                #endregion
                objEgCheckBudgetHead.TreasuryCode = ChallanValues[25][1].ToString();
                objEgCheckBudgetHead.OfficeId = Convert.ToInt32(ChallanValues[27][1].ToString());
                objEgCheckBudgetHead.DepartmentCode = Convert.ToInt32(ChallanValues[28][1].ToString());
                #region Varify mapping with Officecode,Treasury and Department
                if (!ChallanValues[35][1].ToString().Trim().Contains("PD") &&  ChallanValues[35][1].ToString().Trim() != "A" && ChallanValues[35][1].ToString().Trim() != "0" && ChallanValues[35][1].ToString().Trim() != "" && ChallanValues[35][1].ToString().Trim() != null)
                {

                    EgIntegrationChallanBL objEgIntegrationChallanBL = new EgBL.EgIntegrationChallanBL();
                    objEgIntegrationChallanBL.Location = objEgCheckBudgetHead.TreasuryCode;
                    objEgIntegrationChallanBL.filler = ChallanValues[35][1].ToString();
                    if (objEgIntegrationChallanBL.GetDivisionCode() == 0)
                    {
                        throw new Exception("Division code Not exist For particular office");
                    }
                }
                if (objEgCheckBudgetHead.VarifyOfficeId() != 1)
                {
                    throw new Exception("OfficeId not map with Treasury and Department Integration");
                }
                #endregion

                #region Discount can not be greater than Total Amount
                if (Convert.ToDouble(ChallanValues[20][1].ToString()) > Convert.ToDouble(ChallanValues[21][1].ToString()))
                {
                    throw new Exception("Discount can not be greater than Total Amount");
                }
                #endregion

                createTable();  // Create Table for send TVP in database for Check BudgetHead Condition

                #region Assign Budgethead with schecode and Serial number in datatable column
                int sno = 1;
                double amount = 0;
                for (int i = 1; i <= 17; i += 2)
                {
                    if (ChallanValues[i][1].ToString().Trim() != "0" && Convert.ToDouble(ChallanValues[i + 1][1].ToString().Trim()) > 0 && Convert.ToDouble(ChallanValues[i+1][1]) > 0)
                    {
                        DataRow dr = dtCheckHead.NewRow();
                        dr["SNo"] = Convert.ToInt32(sno);
                        dr["BudgetHead"] = Convert.ToString(ChallanValues[i][1].ToString().Trim().Substring(0, 13));
                        dr["ScheCode"] = Convert.ToString(ChallanValues[i][1].ToString().Trim().Substring(13, 5));
                        dr["Amount"] = Convert.ToDouble(ChallanValues[i + 1][1].ToString());
                        amount = amount + Convert.ToDouble(ChallanValues[i + 1][1].ToString());
                        dtCheckHead.Rows.Add(dr);
                        sno++;
                    }
                }
                #endregion
                // check Pd mapping 24 nov 017
                if (ChallanValues[35][1].ToString().Trim().Contains("PD"))
                {
                    objEgCheckBudgetHead.BudgetHead = dtCheckHead.Rows[0][1].ToString();
                    objEgCheckBudgetHead.PDAcc = Convert.ToInt32(ChallanValues[35][1].ToString().Trim().Split(':').GetValue(1));
                    objEgCheckBudgetHead.PayMode = ChallanValues[23][1].ToString();
                    bool res = objEgCheckBudgetHead.VerifyPdAcc();
                    if (!res)
                        throw new Exception("PD Account not mapped with BudgetHead and treasury, Or Online Payment Not Allowed.");
                }
                #region Check "0030" Condition and duplicateHead

                if (dtCheckHead.Rows.Count > 0)
                {
                    if (dtCheckHead.Rows[0][1].ToString().Substring(0, 4) != "0030" && Convert.ToDouble(ChallanValues[20][1].ToString()) > 0)
                    {
                        throw new Exception("Discount not allowed.");
                    }
                    if ((dtCheckHead.Rows[0][1].ToString().Substring(0, 4) == "0030" || Convert.ToInt32(dtCheckHead.Rows[0][1].ToString().Substring(0, 4)) > 7999) && dtCheckHead.Rows.Count > 1)
                    {
                        throw new Exception("Multiple Heads are not allowed.");
                    }
                    var duplicateHead = dtCheckHead.AsEnumerable().Select(row => new { BudgetHead = row.Field<string>("BudgetHead"), ScheCode = row.Field<string>("ScheCode") }).Distinct().LongCount();
                    if (dtCheckHead.Rows.Count != duplicateHead)
                    {
                        throw new Exception("Duplicates BudgetHead Found.");
                    }
                }
                #endregion

                dtHeadDetails = objEgCheckBudgetHead.GetBudgetHeadDetail(dtCheckHead); // Check Heads with departmentcode Exists and Not and return DataTable 

                #region  create new Amount column and assign value in DataTable(dtHeadDetails) and Check amount Condition <>0 and TotalAmount=HeadsOfAmount
                if (dtHeadDetails.Rows.Count > 0)
                {
                    if ((amount - Convert.ToDouble(ChallanValues[20][1].ToString())) != Convert.ToDouble(ChallanValues[21][1].ToString()))
                    {
                        throw new Exception("Amount mismatch.");
                    }
                #endregion

                    #region Create Data table Column
                    dtHeadDetails.Columns.AddRange(new DataColumn[]
                    {
                    new DataColumn("fullname",typeof(string)),
                    new DataColumn("deductCommission",typeof(double)),
                    new DataColumn("merchantCode",typeof(int)),
                    new DataColumn("pType",typeof(string)),
                    new DataColumn("identity",typeof(string)),
                    new DataColumn("location",typeof(string)),
                    new DataColumn("districtCode",typeof(int)),
                    new DataColumn("officeCode",typeof(int)),
                    new DataColumn("fromDate",typeof(DateTime)),
                    new DataColumn("toDate",typeof(DateTime)),
                    new DataColumn("address",typeof(string)),
                    new DataColumn("pincode",typeof(string)),
                    new DataColumn("city",typeof(string)),
                    new DataColumn("remarks",typeof(string)),
                    new DataColumn("refNo",typeof(string)),
                    new DataColumn("TotalAmount",typeof(double)),
                     new DataColumn("ChallanYear",typeof(string)),
                      new DataColumn("filler",typeof(string)),
                       new DataColumn("PdAcc",typeof(int)),
                       new DataColumn("Zone",typeof(string)),
                     new DataColumn("Circle",typeof(string)),
                     new DataColumn("Ward",typeof(string))
                    
                    });
                    #endregion


                    #region Check Department Integration String values Valid and Not , Assign in datatable column

                    dtHeadDetails.Rows[0]["merchantCode"] = objEgDeptIntegrationPropBL.merchantCode(Convert.ToInt32(ChallanValues[22][1].ToString()));
                    dtHeadDetails.Rows[0]["pType"] = objEgDeptIntegrationPropBL.pType(ChallanValues[23][1].ToString());
                    dtHeadDetails.Rows[0]["identity"] = objEgDeptIntegrationPropBL.identity(ChallanValues[24][1].ToString());
                    dtHeadDetails.Rows[0]["location"] = objEgDeptIntegrationPropBL.location(ChallanValues[25][1].ToString());
                    dtHeadDetails.Rows[0]["fullName"] = objEgDeptIntegrationPropBL.fullName(ChallanValues[19][1].ToString());
                    dtHeadDetails.Rows[0]["deductCommission"] = objEgDeptIntegrationPropBL.deductCommission(Convert.ToDouble(ChallanValues[20][1].ToString()));
                    dtHeadDetails.Rows[0]["districtCode"] = objEgDeptIntegrationPropBL.districtCode(Convert.ToInt32(ChallanValues[26][1].ToString()));
                    dtHeadDetails.Rows[0]["officeCode"] = objEgDeptIntegrationPropBL.officeCode(Convert.ToInt32(ChallanValues[27][1].ToString()));
                    dtHeadDetails.Rows[0]["fromDate"] = Convert.ToDateTime(ChallanValues[29][1].ToString());
                    dtHeadDetails.Rows[0]["toDate"] = Convert.ToDateTime((ChallanValues[30][1].ToString()));
                    dtHeadDetails.Rows[0]["address"] = objEgDeptIntegrationPropBL.address(ChallanValues[31][1].ToString());
                    dtHeadDetails.Rows[0]["pincode"] = objEgDeptIntegrationPropBL.pincode(ChallanValues[32][1].ToString());
                    dtHeadDetails.Rows[0]["city"] = objEgDeptIntegrationPropBL.city(ChallanValues[33][1].ToString());
                    dtHeadDetails.Rows[0]["remarks"] = objEgDeptIntegrationPropBL.remarks(ChallanValues[34][1].ToString());
                    dtHeadDetails.Rows[0]["refNo"] = objEgDeptIntegrationPropBL.refNo(ChallanValues[0][1].ToString());
                    dtHeadDetails.Rows[0]["TotalAmount"] = objEgDeptIntegrationPropBL.amount(Convert.ToDouble(ChallanValues[21][1].ToString()));
                    dtHeadDetails.Rows[0]["ChallanYear"] = objEgDeptIntegrationPropBL.ChallanYear(ChallanValues[36][1].ToString());
                    dtHeadDetails.Rows[0]["filler"] = objEgDeptIntegrationPropBL.filler(ChallanValues[35][1].ToString());
                    dtHeadDetails.Rows[0]["PdAcc"] = Convert.ToInt32(ChallanValues[35][1].ToString().Trim().Contains("PD") ? objEgDeptIntegrationPropBL.PdAcc(ChallanValues[35][1].ToString().Trim().Split(':').GetValue(1).ToString()) : 0.ToString());
                    if (ChallanValues[28][1].ToString() == "18")
                    {
                        dtHeadDetails.Rows[0]["Zone"] = "0501";
                        dtHeadDetails.Rows[0]["Circle"] = "1018";
                        dtHeadDetails.Rows[0]["Ward"] = "3067";
                    }

                    #endregion
                }
                else // added on 10/04/2017 by sandeep
                {
                    throw new Exception("DataTable not found");
                }
                return dtHeadDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Create table for check Budget head Exist and not in data base
        /// </summary>
        private void createTable()
        {
            dtCheckHead = new DataTable();
            dtCheckHead.Columns.Add(new DataColumn("SNo", Type.GetType("System.Int64")));
            dtCheckHead.Columns.Add(new DataColumn("BudgetHead", Type.GetType("System.String")));
            dtCheckHead.Columns.Add(new DataColumn("ScheCode", Type.GetType("System.String")));
            dtCheckHead.Columns.Add(new DataColumn("Amount", System.Type.GetType("System.Double")));
        }

    }
}
