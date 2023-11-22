<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgTestBank.aspx.cs" Inherits="EgTestBank" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>--%>
    <script type="text/javascript" src="../js/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <link href="../js/bootstrap.min.css" rel="stylesheet" />
    <style>
        tr {
            padding: 10px;
        }

        input {
            margin-bottom: 10px;
        }
    </style>
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
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="container" style="text-align: center">
            <div class="row col-xs-12" style="text-align: center; color: Blue; width: 100%; min-height: 10%;">
                <img name="Grass" src="../App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left" width="100%" />
            </div>
            <div class="row col-xs-12" runat="server" id="divVerify">
                <div class="col-sm-3"></div>
                <div class="col-sm-6" style="padding: 10px; margin-top: 20px;">
                    <div class="col-sm-12">
                        <h3 style="text-decoration: underline; color: #FFF; background-color: #41AEBF; margin: 0;">Mobile Authourization</h3>
                    </div>
                    <hr />
                    <div class="col-sm-6 text-left">
                        <label class="LabelText" style="color: #336699">Mobile Number</label>
                        <asp:TextBox ID="txtMobile" onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false" autocomplete="off" CssClass="form-control" runat="server" MaxLength="10" AutoPostBack="true" onkeypress="Javascript:return NumberOnly(event)" OnTextChanged="MobileVerification_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblMsgMobileVerify" Style="color: white" CssClass="label-danger" runat="server" Visible="false"></asp:Label>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMobile" ID="RegularExpressionValidator3" ValidationExpression="^[\d]{10,10}$" runat="server" ErrorMessage="Minimum 10 and Maximum 10 degits required."></asp:RegularExpressionValidator>

                    </div>
                    <div class="col-sm-6 text-left">
                        <label class="LabelText" style="color: #336699">OTP </label>
                        <asp:TextBox ID="txtOtp" onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false" autocomplete="off" CssClass="form-control" runat="server" MaxLength="10" AutoPostBack="true" onkeypress="Javascript:return NumberOnly(event)" OnTextChanged="OTPVerification_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblMsgOtp" Style="color: white" CssClass="label-danger " runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-3"></div>
            </div>

            <div runat="server" id="tblTestBank" style="margin-top: 10%;">
                <table style="width: 100%; margin-top: 30px">
                    <tr style="text-align: center">
                        <td style="width: 40%; text-align: right">GRN No. :
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtGRN" runat="server" Style="color: gray;"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 40%; text-align: right">Bank Code :
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtBankCode" runat="server" Style="color: gray;"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 40%; text-align: right">Bank Reference No :
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtBankReferenceNo" runat="server" Style="color: gray;"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 40%; text-align: right">CIN :
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtCIN" runat="server" Style="color: gray;"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 40%; text-align: right">Paid Date :
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtPaidDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)" onChange="javascript:return dateValidation1()"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtPaidDate"
                                Format="MM/dd/yyyy" TargetControlID="txtPaidDate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtPaidDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPaidDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>


                        </td>

                    </tr>

                    <tr>
                        <td style="width: 40%; text-align: right">Paid Amount :
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtPaidAmt" runat="server" Style="color: gray;"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 40%; text-align: right">Transaction Status :
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtTransStatus" runat="server" Style="color: gray;"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td style="width: 20%"></td>
                        <td style="width: 80%; text-align: left;">
                            <asp:Button runat="server" ID="btnsubmit" Text="Submit" BackColor="Red" ForeColor="Aqua" Font-Bold="true"
                                OnClick="btnSubmit_Click" Height="30px" Width="85px" />

                        </td>

                    </tr>


                </table>
            </div>
        </div>
    </form>
</body>
</html>
