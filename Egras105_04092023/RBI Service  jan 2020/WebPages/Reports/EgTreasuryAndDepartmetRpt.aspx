<%@ Page Title="Egras.Raj.Nic.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgTreasuryAndDepartmetRpt.aspx.cs" Inherits="WebPages_Reports_EgTreasuryAndDepartmetRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../js/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/Control.js"></script>

    <script language="javascript" type="text/javascript">
        $('#<%= rblType.ClientID %>')
        $(document).ready(function () {
            $('#<%= rblType.ClientID %>').click(function () {
                var rblTypeOfBusiness = $('#<%=rblType.ClientID %> input:checked').val();
                ShowHide(rblTypeOfBusiness);
            });
        });
        function ShowHide(rblTypeOfBusiness) {
            if (rblTypeOfBusiness == 1) {
                document.getElementById("<%= DivBudgetText.ClientID %>").style.display = "none";

            }
            else {
                document.getElementById("<%= DivBudgetText.ClientID %>").style.display = "block";
            }
            document.getElementById("<%= txtBudgetHead.ClientID %>").value = "";
            document.getElementById("<%= tbldeptDetails.ClientID %>").style.display = "none";
        }

        function CheckMajorHeadlength() {
            var Mvalue = document.getElementById("<%= txtBudgetHead.ClientID %>").value
            if (Mvalue.length != 4) {
                alert('Please Enter MajorHead.!');
                document.getElementById("<%= txtBudgetHead.ClientID %>").value = "";
            }

        }

    </script>

    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>

    <div style="text-align: center">
       
        <div _ngcontent-c6="" class="tnHead minus2point5per">
            <h2 _ngcontent-c6="" title="Master-Report">
                <span _ngcontent-c6="" style="color: #FFF">Master-Report </span></h2>
            <img src="../../Image/help1.png" style="height: 44px; width: 34px;" title="Master-Report" />
        </div>

        <table id="Table1" border="1" width="100%" align="center">
            <tr>
                <td align="center">
                    <div id="div1" runat="server">
                        <asp:RadioButtonList runat="server" ID="rblType" RepeatDirection="Horizontal" ForeColor="#336699"
                            Font-Bold="true" Style="width: 70% !important; display: contents !important" CssClass="form-control">
                            <asp:ListItem Text="TreasuryWise Report" style="margin-right: 20px" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="BudgetHead-wise Report" Value="2" Selected="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </td>
            </tr>
            <tr style="height: 45px">
                <td align="center">
                    <div id="divMain" runat="server">
                        <table style="width: 100%" align="center" border="1">
                            <tr>
                                <td align="left">
                                    <b><span style="color: #336699">From Date : </span></b>&nbsp;
                                        <asp:TextBox ID="txtFromDate" runat="server" Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px"
                                            onkeypress="Javascript:return NumberOnly(event)"
                                            onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left">
                                    <b><span style="color: #336699">To Date : </span></b>&nbsp;
                                        <asp:TextBox ID="txtToDate" runat="server" Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px"
                                            onkeypress="Javascript:return NumberOnly(event)"
                                            onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <b><span style="color: #336699">Treasury : </span></b>&nbsp;
                                        <asp:DropDownList ID="ddlTreasury" runat="server" Width="180px" class="form-control chzn-select">
                                            <asp:ListItem Value="0" Text="--All Treasury--"></asp:ListItem>
                                            <asp:ListItem Value="0100" Text="Ajmer"></asp:ListItem>
                                            <asp:ListItem Value="0200" Text="ALWAR"></asp:ListItem>
                                            <asp:ListItem Value="0300" Text="BANSWARA"></asp:ListItem>
                                            <asp:ListItem Value="0400" Text="BARAN"></asp:ListItem>
                                            <asp:ListItem Value="0500" Text="BARMER"></asp:ListItem>
                                            <asp:ListItem Value="0600" Text="BEAWAR"></asp:ListItem>
                                            <asp:ListItem Value="0700" Text="BHARATPUR"></asp:ListItem>
                                            <asp:ListItem Value="0800" Text="BHILWARA"></asp:ListItem>
                                            <asp:ListItem Value="0900" Text="BIKANER"></asp:ListItem>
                                            <asp:ListItem Value="1000" Text="BUNDI"></asp:ListItem>
                                            <asp:ListItem Value="1100" Text="CHITTORGARH"></asp:ListItem>
                                            <asp:ListItem Value="1200" Text="CHURU"></asp:ListItem>
                                            <asp:ListItem Value="1300" Text="DAUSA"></asp:ListItem>
                                            <asp:ListItem Value="1400" Text="DHOLPUR"></asp:ListItem>
                                            <asp:ListItem Value="1500" Text="DUNGARPUR"></asp:ListItem>
                                            <asp:ListItem Value="1600" Text="GANGANAGAR"></asp:ListItem>
                                            <asp:ListItem Value="1700" Text="HANUMANGARH"></asp:ListItem>
                                            <asp:ListItem Value="1800" Text="JAIPUR (CITY)"></asp:ListItem>
                                            <asp:ListItem Value="2000" Text="JAIPUR (RURAL)"></asp:ListItem>
                                            <asp:ListItem Value="2100" Text="JAIPUR (SECTT.)"></asp:ListItem>
                                            <asp:ListItem Value="2200" Text="JAISALMER"></asp:ListItem>
                                            <asp:ListItem Value="2300" Text="JALORE"></asp:ListItem>
                                            <asp:ListItem Value="2400" Text="JHALAWAR"></asp:ListItem>
                                            <asp:ListItem Value="2500" Text="JHUNJHUNU"></asp:ListItem>
                                            <asp:ListItem Value="2600" Text="JODHPUR (CITY)"></asp:ListItem>
                                            <asp:ListItem Value="2700" Text="JODHPUR (RURAL)"></asp:ListItem>
                                            <asp:ListItem Value="2800" Text="KAROLI"></asp:ListItem>
                                            <asp:ListItem Value="2900" Text="KOTA"></asp:ListItem>
                                            <asp:ListItem Value="3000" Text="NAGAUR"></asp:ListItem>
                                            <asp:ListItem Value="3100" Text="PALI"></asp:ListItem>
                                            <asp:ListItem Value="3200" Text="PRATAPGARH"></asp:ListItem>
                                            <asp:ListItem Value="3300" Text="RAJSAMAND"></asp:ListItem>
                                            <asp:ListItem Value="3400" Text="SAWAI MADHOPUR"></asp:ListItem>
                                            <asp:ListItem Value="3500" Text="SIKAR"></asp:ListItem>
                                            <asp:ListItem Value="3600" Text="SIROHI"></asp:ListItem>
                                            <asp:ListItem Value="3700" Text="TONK"></asp:ListItem>
                                            <asp:ListItem Value="3800" Text="UDAIPUR"></asp:ListItem>
                                            <asp:ListItem Value="4100" Text="UDAIPUR RURAL"></asp:ListItem>
                                        </asp:DropDownList>

                                    <div id="DivBudgetText" style="display: none; margin: 9px" runat="server">
                                        <b><span style="color: #336699">Major-Head :</span></b>&nbsp;
                                            <asp:TextBox ID="txtBudgetHead" Height="100%" Style="display: initial !important" CssClass="form-control" runat="server" Width="60px" MaxLength="4" onblur="javascript:CheckMajorHeadlength()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBudgetHead" runat="server" ControlToValidate="txtBudgetHead"
                                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Vld"></asp:RequiredFieldValidator>
                                    </div>
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnshow" runat="server" Style="margin-left: 80px" Height="33" CssClass="btn btn-default" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="center">
                                    <asp:Label ID="lblNoRecord" runat="server" Visible="false" ForeColor="#336699" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div class="row col-md-12" width="100%" id="tbldeptDetails" runat="server" style="display: block">
            <div class="col-md-6" id="trTreasuryList" runat="server" visible="false">
                <div id="DivTreasuryList" style="max-height: 380px; vertical-align: top; overflow: -webkit-paged-y">
                   
                    <table border="1" style="font-size: 9pt; font-family: Arial; margin-top: 10px; width: 99%">

                        <asp:Repeater ID="RptTreasury" runat="server" OnItemCommand="RptTreasury_ItemCommand"
                            OnItemDataBound="RptTreasury_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td colspan="3" align="center" style="font-size: medium;">
                                        <b><span style="color: #008080">Treasury Amount List</span></b>&nbsp;
                                    </td>
                                </tr>
                                <tr style="background-color: #008080; color: White; font-weight: bold; height: 20px">
                                    <td style="color: White; width: 60px;">Sr.No
                                    </td>
                                    <td style="color: White; text-align: left;">Treasury Name
                                    </td>
                                    <td style="color: White; text-align: right">Total Amount &nbsp;
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: #e3eaeb; height: 20px;">
                                    <td align="center" style="font-size: 15;">
                                        <%# Container.ItemIndex+1 %>
                                    </td>
                                    <td style="font-size: 15; text-align: left;">
                                        <asp:LinkButton ID="lnkTName" ForeColor="#336699" runat="server" Text='<%# Eval("TreasuryName") %>'
                                            CommandArgument='<%# Eval("TreasuryCode") %>' CommandName="Tcode" Font-Bold="true"></asp:LinkButton>
                                    </td>
                                    <td align="center" style="font-size: 15; text-align: right;">
                                        <%# string.Format("{0:0.00}", Eval("TotalAmount"))%>&nbsp; &nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                    <td align="center" style="font-size: 15;">
                                        <%# Container.ItemIndex+1 %>
                                    </td>
                                    <td style="font-size: 15; text-align: left;">
                                        <asp:LinkButton ID="lnkTName" ForeColor="#336699" runat="server" Text='<%# Eval("TreasuryName") %>'
                                            CommandArgument='<%# Eval("TreasuryCode") %>' CommandName="Tcode" Font-Bold="true"></asp:LinkButton>
                                    </td>
                                    <td align="center" style="font-size: 15; text-align: right;">
                                        <%# string.Format("{0:0.00}", Eval("TotalAmount"))%>&nbsp; &nbsp;
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="left" colspan="2">
                                        <b><span style="width: 400px; color: #008080">Total Treasury Amount in (<img src="../../Image/rupees.jpg" />):-</span></b>
                                    </td>
                                    <td align="right" height="40px">
                                        <asp:LinkButton ID="lblamt" runat="server" CommandArgument='<%# Eval("TreasuryCode") %>' CommandName="PopUp" Font-Bold="true" ForeColor="#008080"></asp:LinkButton>&nbsp;
                                                        &nbsp;
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>

                    </table>
                </div>
            </div>

            <div class="col-md-6" id="DivDepartment" style="max-height: 380px; vertical-align: top; overflow: -webkit-paged-y"
                runat="server" visible="false">
                
                <table border="1" style="font-size: 9pt; font-family: Arial; margin-top: 10px; width: 99%">

                    <asp:Repeater ID="RptDepartment" runat="server" OnItemDataBound="RptDepartment_ItemDataBound"
                        OnItemCommand="RptDepartment_ItemCommand">
                        <HeaderTemplate>
                            <tr>
                                <td colspan="3" align="center" style="font-size: medium;">
                                    <b><span style="color: #008080">Department List</span></b>&nbsp;
                                </td>
                            </tr>
                            <tr style="background-color: #008080; color: White; font-weight: bold; text-align: center; height: 20px">
                                <td style="color: White;">Sr.No
                                </td>
                                <td style="color: White; text-align: left">Department Name
                                </td>
                                <td style="color: White; text-align: right">Total Amount&nbsp;
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #e3eaeb; height: 20px;">
                                <td align="center" style="font-size: 15;">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="left" style="font-size: 15;">
                                    <asp:LinkButton ID="lnkDeptName" ForeColor="#336699" runat="server" Text='<%# Eval("DepartmentName") %>'
                                        CommandArgument='<%# Eval("deptcode") %>' CommandName="DeptCode" Font-Bold="true"></asp:LinkButton>
                                </td>
                                <td align="center" style="font-size: 15; text-align: right;">
                                    <%# string.Format("{0:0.00}", Eval("Amount"))%>&nbsp;
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                <td align="center" style="font-size: 15;">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="left" style="font-size: 15;">
                                    <asp:LinkButton ID="lnkDeptName" ForeColor="#336699" runat="server" Text='<%# Eval("DepartmentName") %>'
                                        CommandArgument='<%# Eval("deptcode") %>' CommandName="DeptCode" Font-Bold="true"></asp:LinkButton>
                                </td>
                                <td align="center" style="font-size: 15; text-align: right;">
                                    <%# string.Format("{0:0.00}", Eval("Amount"))%>&nbsp;
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td align="left">
                                    <b><span style="color: #008080; font-weight: bold;">Total Amount in(<img src="../../Image/rupees.jpg" />):-</span></b>
                                </td>
                                <td colspan="2" align="right" height="40px">
                                    <asp:Label ID="lblDepatment" runat="server" Font-Bold="true" ForeColor="#008080"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>

                </table>
            </div>


            <div class="col-md-6" id="DivTreasuryMhead" style="margin-top: 1px; vertical-align: top;" runat="server"
                visible="false">
                
                <table border="1" style="font-size: 9pt; font-family: Arial; margin-top: 10px; width: 99%">

                    <asp:Repeater ID="RptMajorHead" runat="server" OnItemDataBound="RptMajorHead_ItemDataBound"
                        OnItemCommand="RptMajorHead_ItemCommand">
                        <HeaderTemplate>
                            <tr>
                                <td colspan="3" align="center" style="font-size: medium;">
                                    <b><span style="color: #008080">Major Head Amount</span></b>&nbsp;
                                </td>
                            </tr>
                            <tr style="background-color: #008080; color: White; font-weight: bold; text-align: center; height: 20px">
                                <td style="color: White;">Sr.No
                                </td>
                                <td style="color: White; text-align: left">MajorHead
                                </td>
                                <td style="color: White; text-align: right">Total Amount&nbsp;
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #e3eaeb; height: 20px;">
                                <td align="center" style="font-size: 15;">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="left" style="font-size: 15;">
                                    <asp:LinkButton ID="lnkMajorHead" ForeColor="#336699" runat="server" Text='<%# Eval("MajorHead") %>'
                                        CommandArgument='<%# Eval("MheadCode") %>' CommandName="MheadCode"></asp:LinkButton>
                                </td>
                                <td align="center" style="font-size: 15; text-align: right;">
                                    <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                                            &nbsp;
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                <td align="center" style="font-size: 15;">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="left" style="font-size: 15;">
                                    <asp:LinkButton ID="lnkMajorHead" ForeColor="#336699" runat="server" Text='<%# Eval("MajorHead") %>'
                                        CommandArgument='<%# Eval("MheadCode") %>' CommandName="MheadCode"></asp:LinkButton>
                                </td>
                                <td align="center" style="font-size: 15; text-align: right;">
                                    <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                                            &nbsp;
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td align="left">
                                    <b><span style="color: #008080">Total MajorHead Amount in (<img src="../../Image/rupees.jpg" />)
                                    </span></b>
                                </td>
                                <td colspan="2" align="right" height="40px">
                                    <asp:Label ID="lblSchemaTotal" runat="server" Font-Bold="true" ForeColor="#008080"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>

                </table>
            </div>


            <div class="col-md-6" id="DivBudgetHead" runat="server" visible="false" style="margin-top: 1px; vertical-align: top;">
                
                <table border="1" style="font-size: 9pt; font-family: Arial; margin-top: 10px; width: 99%">

                    <asp:Repeater ID="RptBudgetHead" runat="server" OnItemDataBound="RptBudgetHead_ItemDataBound">
                        <HeaderTemplate>
                            <tr>
                                <td colspan="3" align="center" style="font-size: medium;">
                                    <b><span style="color: #008080">Budget Head Amount</span></b>&nbsp;
                                </td>
                            </tr>
                            <tr style="background-color: #008080; color: White; font-weight: bold; text-align: center; height: 20px">
                                <td style="color: White;">Sr.No
                                </td>
                                <td style="color: White; text-align: left">BudgetHead
                                </td>
                                <td style="color: White; text-align: right">Total Amount &nbsp;
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #e3eaeb; height: 20px;">
                                <td align="center" style="font-size: 15;">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="left" style="font-size: 15;">
                                    <%# Eval("BudgetHead")%>
                                </td>
                                <td align="center" style="font-size: 15; text-align: right;">
                                    <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                                            &nbsp;
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                <td align="center" style="font-size: 15;">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="left" style="font-size: 15;">
                                    <%# Eval("BudgetHead")%>
                                </td>
                                <td align="center" style="font-size: 15; text-align: right;">
                                    <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                                            &nbsp;
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td align="left">
                                    <b><span style="color: #008080; font-weight: bold;">Total BudgetHead Amount in (<img src="../../Image/rupees.jpg" />):-</span></b>
                                </td>
                                <td colspan="2" align="right" height="40px">
                                    <asp:Label ID="lblBudgetHeadTotal" runat="server" Font-Bold="true" ForeColor="#008080"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>

                </table>
            </div>

            <div align="center">
                <asp:Label ID="lblNoRec" runat="server" Visible="false" ForeColor="Green" Font-Underline="true"
                    Text=" No Record Found"></asp:Label>
            </div>
        </div>
        <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
            Format="dd/MM/yyyy" TargetControlID="txtFromDate">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
            Format="dd/MM/yyyy" TargetControlID="txtToDate">
        </ajaxToolkit:CalendarExtender>
    </div>
</asp:Content>
