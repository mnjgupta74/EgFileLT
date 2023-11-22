<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgManualFailGRN.aspx.cs" Inherits="WebPages_Reports_EgManualFailGRN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/UserControls/CommonFromDateToDate.ascx" TagName="FromToDate" TagPrefix="ucl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../md5.js" type="text/javascript" language="javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            var rblType = $("[id$='rbllist']").find(":checked").val();
            ShowHide(rblType);
            $('#<%= rbllist.ClientID %>').click(function () {
                var rblType = $("[id$='rbllist']").find(":checked").val();
                //$('#<%=rbllist.ClientID %>').val;
                ShowHide(rblType);
            });
        });
        function
            ShowHide(rblType) {
            
            if (rblType == 1) {
                document.getElementById("<%=div1.ClientID %>").style.display = "block";
                document.getElementById("<%= div2.ClientID %>").style.display = "none";
            }
            else {
                document.getElementById("<%=div1.ClientID %>").style.display = "none";
                <%--document.getElementById("<%=divrpt.ClientID %>").style.display = "none";--%>
                document.getElementById("<%= div2.ClientID %>").style.display = "block";
            }
        }
        function isStrongPassword() {
            var newpassword = document.getElementById("<%=textsecure.ClientID %>").value;
            document.getElementById("<%=textsecure.ClientID %>").value = hex_md5(newpassword);
            return true;
        }
        function password() {
            var psword = document.getElementById("<%=securetext.ClientID %>").value;
            document.getElementById("<%=securetext.ClientID %>").value = hex_md5(psword);
            return true;
        }

    </script>


    <script src="../../js/Control.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Manual Fail GRN">
            <span _ngcontent-c6="" style="color: #FFF">Manual Fail GRN </span></h2>
        <img src="../../Image/help1.png" style="height: 44px; width: 34px;" title="Manual Fail GRN" />
    </div>
    <table style="width: 100%" align="center" id="MainTable" border="1">
        <tr style="height: 45px">
            <td align="center" >
                <asp:RadioButtonList runat="server" ID="rbllist" RepeatDirection="Horizontal" Width="50%" style="margin-left: 20%;">
                    <asp:ListItem Text="DateWise" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="GRNWise" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>

    <div id="div1" runat="server">
        <table id="table1" runat="server" border="1" width="100%">
            <tr style="height: 45px">
                <td>
                    <b>
                        <asp:Label ID="lblbank" runat="server" Text="Bank:-" Style="color: #336699"></asp:Label></b>&nbsp;
                   <asp:DropDownList ID="ddlbank" runat="server">
                   </asp:DropDownList>
                </td>
                <td>
                    <ucl:FromToDate ID="frmdatetodate" runat="server" Width="100px" Height="22px" CssClass="borderRadius " />
                </td>

                <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnfindresult" runat="server" Text="Show" /><%--OnClientClick="Javascript:return  Warning1()"--%>
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnfindresult"
                        PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="DivBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px"
                        Style="display: none">
                        <div style="text-align: center; margin-top: 50px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px">Enter Password:-</span></b>&nbsp;
                                    <asp:TextBox ID="textsecure" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnfindresult_Click"
                                OnClientClick="isStrongPassword();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                        </div>
                    </asp:Panel>
                </td>

            </tr>
        </table>
    </div>
    <div id="div2" runat="server">
        <table id="table2" runat="server" style="align-content: center" border="1" width="100%">
            <tr style="height: 45px">
                <td align="center">
                    <b><span style="color: #336699">GRN:-</span></b>&nbsp;
                            <asp:TextBox runat="server" ID="grntext" MaxLength="12" Width="140px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="grntext"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    <%--  </td>
                <td align="center">--%>
                    <asp:Button ID="grnsearch" runat="server" Text="Show" ValidationGroup="de" />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="grnsearch"
                        PopupControlID="Panel1" CancelControlID="cancel" BackgroundCssClass="DivBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="Panel1" runat="server" BackColor="White" Height="150px" Width="400px"
                        Style="display: none">
                        <div style="text-align: center; margin-top: 50px">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px">Enter Password:-</span></b>&nbsp;
                                    <asp:TextBox ID="securetext" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="Btnvrfy" runat="server" Text="Verify" OnClick="grnsearch_Click"
                                OnClientClick="password();" />
                            <asp:Button ID="cancel" runat="server" Text="Cancel" />
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>

    <div id="divrpt" runat="server">
        <table style="width: 100%" align="center">
            <asp:Repeater ID="rptmanualfail" runat="server" OnItemCommand="rptmanualfail_ItemCommand" OnItemDataBound="rptmanualfail_ItemDataBound" Visible="true">
                <HeaderTemplate>
                    <table border="1" width="100%" cellpadding="0" cellspacing="0">
                        <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px;">
                            <th align="center">
                                <b>S.No</b>
                            </th>
                            <th align="center">
                                <b>GRN</b>
                            </th>
                            <th align="center">
                                <b>Amount</b>
                            </th>
                            <th align="center">
                                <b>Status</b>
                            </th>
                            <th align="center">
                                <b></b>
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#  Eval("GRN")%>
                        </td>
                        <td align="right">
                            <%#  Eval("Amount")%>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblStatus" Text='<%#  Eval("Status")%>' runat="server" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnpending" CommandArgument='<%#  Eval("GRN") + ";" +Eval("Amount")%>' runat="server" Text="Set Pending" />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#  Eval("GRN")%>
                        </td>
                        <td align="right">
                            <%#  Eval("Amount")%>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblStatus" Text='<%#  Eval("Status")%>' runat="server" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnpending" CommandArgument='<%#  Eval("GRN") + ";" +Eval("Amount")%>' runat="server" Text="Set Pending" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td align="center" colspan="6">
                            <asp:Label ID="lblrecord" Text="No Record Found" runat="server" Visible="false" />
                        </td>
                    </tr>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>



</asp:Content>

