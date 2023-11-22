using System;
using System.Data;
using System.Text;
using EgBL;
using System.Collections.Generic;
public partial class WebPages_EgExtraDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgEChallanBL objEgEChallan = new EgEChallanBL();
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            EgEncryptDecrypt ObjEncrytDecrypt = new EgEncryptDecrypt();
            if (Request.QueryString.Count > 0)
            {
                string strReqq = Request.Url.ToString();
                strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
                List<string> strList = ObjEncrytDecrypt.Decrypt(strReqq);
                objEgEChallan.GRNNumber = Convert.ToInt32(strList[1].ToString());
                dt = objEgEChallan.GetExtraDetails();
                if (dt.Rows.Count > 0)
                {
                    //if (dt.Rows[0]["Details"].ToString() != "")
                    //{
                    string[] list = dt.Rows[0]["Details"].ToString().Split('^');
                    if (dt.Rows.Count > 0)
                    {
                        //for (int i = 0; i < list.Length; i++)   ' " +Convert.ToString (ListValue.Length - 2 )+ " ' 
                        //{
                        sb.Append("<table width='400' id='module' border='1' cellpadding='1' cellspacing='0' style='background-color:transparent;border-color:green;color:#CA3B2B;'");
                        //sb.Append("<tr><td colspan='2' align='center'  style='font-size: 12pt;padding-left:3px;'>Extra Details</td></tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td style='font-size: 12pt;padding-left:3px;color:dark red;'>&nbsp;");
                        //sb.Append("S.No.");
                        //sb.Append("&nbsp;</td>");
                        //string[] ListValue = list[0].Split('#');
                        //for (int j = 0; j < ListValue.Length - 1; j++)
                        //{
                        //    sb.Append("<td style='font-size: 12pt;padding-left:3px;color:dark red;'>&nbsp;");
                        //    sb.Append("Details" + (j + 1).ToString());
                        //    sb.Append("&nbsp;</td>");
                        //}
                        //sb.Append("</tr>");

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < list.Length; i++)
                            {
                                //if (list[i].ToString() != "")
                                //{
                                sb.Append("<tr>");
                                //sb.Append("<td style='font-size: 12pt;padding-left:3px;'>&nbsp;");
                                //sb.Append(i + 1);
                                //sb.Append("&nbsp;</td>");

                                string[] ListValue2 = list[i].Split('#');
                                for (int j = 0; j < ListValue2.Length - 1; j++)
                                {
                                    sb.Append("<td style='font-size: 12pt;bold;padding-left:3px;'>&nbsp;");
                                    if (ListValue2[j].ToString() != "0")
                                    {
                                        sb.Append(ListValue2[j].ToString());
                                    }
                                    else
                                    {
                                        sb.Append("");
                                    }
                                    sb.Append("&nbsp;</td>");
                                }
                                sb.Append("</tr>");
                                // }

                            }
                        }
                        sb.Append("</table>");
                        literal1.Text = sb.ToString();
                        literal1.Visible = true;
                        ////////////////////////////////////////////////   
                        // }
                    }
                }
            }
        }
    }
}
