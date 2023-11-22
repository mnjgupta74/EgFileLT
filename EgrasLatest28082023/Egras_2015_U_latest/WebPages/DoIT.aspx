<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoIT.aspx.cs" Inherits="DoIT" %>

<!DOCTYPE html>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void btnCorrectCase_Click(object sender, EventArgs e)
    {

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Egras.raj.ni.in</title>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>--%>
    <%--<script src="../js/jquery-3.6.0.min.js"></script>--%>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../js/bootstrap.min.css" rel="stylesheet" />
    <%--    <script src="js/Js_Cal/bootstrap.min.js"></script>
    <link href="js/Js_Cal/bootstrap.min.css" rel="stylesheet" />--%>
    <script type="text/javascript">

        function NumberOnly(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 3 && parts[1].length >= 3) return false;
        }

    </script>
</head>
<body style="background-color: #f9f9f9;">
    <form id="form1" runat="server" method="post">

        <asp:HiddenField ID="Encdata" runat="server" Value="" />
        <asp:HiddenField ID="merchant_code" runat="server" Value="" />
        <asp:HiddenField ID="AUIN" runat="server" Value="" />
        <div class="container" style="text-align: center">
            <div class="row col-xs-12" style="text-align: center; color: Blue; width: 100%; min-height: 10%;">
                <img name="Grass" src="../App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left" width="100%" />
            </div>
            <div>
            </div>
            <div class="row col-xs-12" runat="server" id="divVerify">
                <div class="col-sm-3"></div>
                <div class="col-sm-6" style="padding: 10px; margin-top: 20px;">
                    <div class="col-sm-12">
                        <h3 style="text-decoration: underline; color: #FFF; background-color: #41AEBF; margin: 0;">Mobile Authorization</h3>
                    </div>
                    <hr />
                    <%--onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false" autocomplete="off"--%>
                    <div class="col-sm-6 text-left">
                        <label class="LabelText" style="color: #336699">Mobile Number</label>
                        <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" MaxLength="10" onkeypress="Javascript:return NumberOnly(event)"
                            AutoPostBack="true" onDrag="return false" onDrop="return false" onCopy="return false" onPaste="return false" autocomplete="off" OnTextChanged="MobileVerification_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblMsgMobileVerify" style="color:white"  CssClass="label-danger" runat="server" Visible="false"></asp:Label>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMobile" ID="RegularExpressionValidator3" ValidationExpression="^[\d]{10,10}$" runat="server" ErrorMessage="Minimum 10 and Maximum 10 degits required."></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-6 text-left">
                        <label class="LabelText" style="color: #336699">OTP </label>
                        <asp:TextBox ID="txtOtp" onDrag="return false" onDrop="return false" onCopy="return false" onPaste="return false" autocomplete="off"  CssClass="form-control" runat="server" MaxLength="10" AutoPostBack="true" onkeypress="Javascript:return NumberOnly(event)" OnTextChanged="OTPVerification_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblMsgOtp" CssClass="label-danger " style="color:white" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-3"></div>
            </div>

            <div class="col-md-12" style="background-color: azure; margin-top: 10px; clear: both;">
                <div runat="server" visible="false" id="DivDoIT">
                    <div style="clear: both;">
                        <div style="width: 20%; float: left;">
                            <b><span style="color: #336699">Merchant Code-</span></b>
                        </div>
                        <div style="width: 80%; float: right; text-align: left;">
                            <asp:TextBox runat="server" ID="txtMerchantCode" Style="height: 30px;" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMerchantCode"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 2%;">
                        <div style="width: 20%; float: left;">
                            <b><span style="color: #336699">Encrypted String-</span></b>
                        </div>
                        <div style="width: 80%; float: right; text-align: left;">
                            <asp:TextBox runat="server" ID="txtEncData" TextMode="MultiLine" Style="width: 60%; max-width: 90%; min-height: 30px; max-height: 200px;" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEncData"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 2%;">
                        <div style="width: 20%; float: left;">
                            <b><span style="color: #336699">Office Xml String-</span></b>
                        </div>
                        <div style="width: 80%; float: right; text-align: left;">
                            <asp:TextBox runat="server" ID="txtOfficeXml" TextMode="MultiLine" Style="width: 60%; max-width: 90%; min-height: 30px; max-height: 200px;" />
                        </div>
                    </div>

                    <br />
                    <div style="width: 70%; clear: both; margin-top: 2%; margin-left: 20%; text-align: left;">
                        <div style="float: left; width: 50%;">
                            <asp:Button Text="Submit" runat="server" ValidationGroup="de" ID="btnSubmit" OnClick="btnSubmit_Click" Style="height: 30px; background-color: #87CEFA" />
                        </div>
                        <asp:Button Text="MultiOfc_Submit" runat="server" ValidationGroup="de" ID="btnMultiOfcSubmit" OnClick="btnMultiOfcSubmit_Click" Style="height: 30px; background-color: #87CEFA" />
                    </div>
                    <br />
                    <div style="width: 60%; text-align: center; margin: auto; border: 1px solid lightgray; background-color: #FFF">
                        <div style="width: 100%; clear: both; margin-top: 2%">
                            <div style="float: left; width: 50%;">
                                <asp:Button Text="Office Mismatch" runat="server" ID="btnOfficeMismatch" OnClick="btnOfficeMismatch_Click" Style="height: 30px; background-color: #FF6347" />
                            </div>
                            <asp:Button Text="Amount Mismatch" runat="server" ID="btnAmountMismatch" OnClick="btnAmountMismatch_Click" Style="height: 30px; background-color: #CD5C5C" />
                        </div>
                        <div style="width: 100%; clear: both; margin-top: 2%">
                            <div style="float: left; width: 50%;">
                                <asp:Button Text="Budgethead Mismatch" runat="server" ID="btnBudgetHeadMismatch" OnClick="btnBudgetHeadMismatch_Click" Style="height: 30px; background-color: #DC143C" />
                            </div>
                            <asp:Button Text="Office Amount Mismatch" runat="server" ID="btnOfcAmtMismatch" OnClick="btnOfcAmtMismatch_Click" Style="height: 30px; background-color: #F08080" />
                        </div>
                        <div style="text-align: center; margin: 2%">
                            <asp:Button Text="Correct Details" runat="server" ID="btnCorrectCase" OnClick="btnCorrectCase_Click1" Style="height: 30px; background-color: #32CD32; color: white;" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
