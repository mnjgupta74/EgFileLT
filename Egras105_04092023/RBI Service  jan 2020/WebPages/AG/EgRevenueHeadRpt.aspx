<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgRevenueHeadRpt.aspx.cs" Inherits="WebPages_EgRevenueHeadRpt" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <style>
        #ctl00_ContentPlaceHolder1_rblType_1
        {
            margin-left:30px;
        }
        
         for ctl00_ContentPlaceHolder1_rblType_1{
             margin:5px;
         }
    </style>
   
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background ">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../App_Themes/images/progress.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Department-Revenue" class="pull-left">
                    <span _ngcontent-c6=""  class="pull-right" style="color: #FFF">Department-Revenue Report</span></h2>
               <%--<img src="../../Image/help1.png" style="height: 35px;width: 34px;" data-toggle="tooltip" data-placement="left" Title="" />--%>
            </div>
             
                <table style="width: 100%" border="1" align="center">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Accounting Date" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Payment Date" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                         <td align="center">
                            <span >Department Code : </span>&nbsp;
                        </td>
                        <td  style="text-align:left;">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="250px" CssClass="borderRadius inputDesign chzn-select" 
                                onchange="DisplayTable();">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlDepartment"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td align="center"><span >MajorHead/BudgetHead : </span>&nbsp;</td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtBudgetHead" runat="server" MaxLength="13" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="9999-99-999-99-99"
                                MaskType="None" CultureName="en-US" TargetControlID="txtBudgetHead" AcceptNegative="None"
                                runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                             
                        </td>
                    </tr>
                    <tr>
                       <td align="center">
                            <span >From Date : </span>&nbsp;
                        </td>
                        <td  style="text-align:left;">
                            <asp:TextBox ID="txtFromDate" runat="server"
                    onpaste="return false" onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                             <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                        </ajaxToolkit:MaskedEditExtender>
                        </td>
                        <td align="center">
                            <span >To Date : </span>&nbsp;
                        </td>
                        <td  style="text-align:left;">
                            <asp:TextBox ID="txtToDate" runat="server"
                    onpaste="return false" onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                             <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                        </ajaxToolkit:MaskedEditExtender>
                        </td>
                    </tr>
                   <tr style="">
                       
                       <td colspan="4" style="text-align:center;"><asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />&nbsp;</td>
                           
                   </tr>
                </table>
                <table width="100%" >
                    <tr id="trTreasuryList" runat="server" visible="false">
                        <td style="width: 450px; vertical-align: top;">
                            <div id="DivTreasuryList" style="margin-right: 10px; height: 380px; overflow: scroll">
                                <fieldset style="width: 450px;" id="FieldTreasury">
                                    <legend style="color: #336699;">Treasury Amount List</legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial; width: 450px">
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="RptTreasury" runat="server" OnItemCommand="RptTreasury_ItemCommand"
                                                    OnItemDataBound="RptTreasury_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center; height: 20px">
                                                            <td style="color: White; width: 60px;">Sr.No
                                                            </td>
                                                            <td style="color: White; text-align: left;">Treasury Name
                                                            </td>
                                                            <td style="color: White; text-align: right">Total Amount &nbsp;
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr style="background-color: #EFF3FB; height: 20px;">
                                                            <td align="center" style="font-size: 15;">
                                                                <%# Container.ItemIndex+1 %>
                                                            </td>
                                                            <td style="font-size: 15; text-align: left;">
                                                                <asp:LinkButton ID="lnkTName" ForeColor="#336699" runat="server" Text='<%# Eval("TreasuryName") %>'
                                                                    CommandArgument='<%# Eval("TreasuryCode") %>' CommandName="Tcode"></asp:LinkButton>
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
                                                                    CommandArgument='<%# Eval("TreasuryCode") %>' CommandName="Tcode"></asp:LinkButton>
                                                            </td>
                                                            <td align="center" style="font-size: 15; text-align: right;">
                                                                <%# string.Format("{0:0.00}", Eval("TotalAmount"))%>&nbsp; &nbsp;
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <b><span style="width: 400px; color: #336699">Total Treasury Amount in (<img src="../../Image/rupees.jpg" />):-</span></b>
                                                            </td>
                                                            <td align="right" height="40px">
                                                                <asp:Label ID="lblamt" runat="server" Font-Bold="true" ForeColor="#336699"></asp:Label>&nbsp;
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                        <td style="width: 450px; vertical-align: top;">
                            <div id="DivDepartment" style="margin-right: 10px; height: 380px; overflow: scroll"
                                runat="server" visible="false">
                                <fieldset style="width: 450px;" id="Fieldset1">
                                    <legend style="color: #336699;">Revenue Head Report :
                                        <asp:Label ID="lblTreasury" runat="server" Text="Treasury"></asp:Label></legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial; width: 450px">
                                        <asp:Repeater ID="rpt" runat="server" OnItemCommand="rpt_ItemCommand" OnItemDataBound="rpt_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center; height: 20px">
                                                    <td style="color: White;">Sr.No
                                                    </td>
                                                    <td style="color: White;">Budget Head
                                                    </td>
                                                    <td style="color: White; text-align: right">Total Amount
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="background-color: #EFF3FB; height: 20px;">
                                                    <td align="center" style="font-size: 15;">
                                                        <%# Container.ItemIndex+1 %>
                                                    </td>
                                               <%--     <td align="center" style="font-size: 15;">
                                                        <asp:LinkButton ID="lnkBudgethead" ForeColor="#336699" runat="server" Text='<%# Eval("BudgetHead") %>'
                                                            CommandArgument='<%# Eval("budgetheadcode") %>' CommandName="Show"></asp:LinkButton>

                                                    </td>--%>
                                                    <td  align="center" style="font-size: 15;">
                                                         <%# Eval("BudgetHead")%>

                                                    </td>
                                                    <td align="center" style="font-size: 15; text-align: right;">
                                                        <%# string.Format("{0:0.00}", Eval("TotalAmt"))%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                                    <td align="center" style="font-size: 15;">
                                                        <%# Container.ItemIndex+1 %>
                                                    </td>
                                                    <td  align="center" style="font-size: 15;">
                                                         <%# Eval("BudgetHead")%>

                                                    </td>
                                                    <td align="center" style="font-size: 15; text-align: right;">
                                                        <%# string.Format("{0:0.00}",Eval("TotalAmt")) %>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <b><span style="width: 400px; color: #336699">Total Head Amount:-</span></b>
                                                    </td>
                                                    <td align="right" height="40px">
                                                        <asp:Label ID="lblamt" runat="server" Font-Bold="true" ForeColor="#336699"></asp:Label>
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 450px; vertical-align: top;" colspan="2">
                            <div id="rptSchemaDiv" style="margin-left: 10px; margin-top: 1px;" runat="server"
                                visible="false">
                                <fieldset style="width: 500px;">
                                    <legend style="color: #336699;">Purpose Report On:
                                        <asp:Label ID="lblHead" runat="server" Text="Head"></asp:Label></legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial; width: 450px">
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="rptschema" runat="server" OnItemDataBound="rptschema_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center; height: 20px">
                                                            <td style="color: White;">Sr.No
                                                            </td>
                                                            <td style="color: White; text-align: left">Purpose/BudgetHead
                                                            </td>
                                                            <td style="color: White; text-align: right">Total Amount
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr style="background-color: #EFF3FB; height: 20px;">
                                                            <td align="center" style="font-size: 15;">
                                                                <%# Container.ItemIndex+1 %>
                                                            </td>
                                                            <td align="left" style="font-size: 15;">
                                                                <%# Eval("ScheCode") %>
                                                            </td>
                                                            <td align="center" style="font-size: 15; text-align: right;">
                                                                <%# string.Format("{0:0.00}",Eval("TotAmt"))%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                                            <td align="center" style="font-size: 15;">
                                                                <%# Container.ItemIndex+1 %>
                                                            </td>
                                                            <td align="left" style="font-size: 15;">
                                                                <%# Eval("ScheCode") %>
                                                            </td>
                                                            <td align="center" style="font-size: 15; text-align: right;">
                                                                <%# string.Format("{0:0.00}", Eval("TotAmt"))%>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td align="left">
                                                                <b><span style="width: 300px; color: #336699">Total Schema Amount:-</span></b>
                                                            </td>
                                                            <td colspan="2" align="right" height="40px">
                                                                <asp:Label ID="lblSchemaTotal" runat="server" Font-Bold="true" ForeColor="#336699"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr style="display: block" id="trRevenue">
                        <td style="vertical-align: top" colspan="2"></td>
                    </tr>
                </table>
                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                    Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                    Format="dd/MM/yyyy" TargetControlID="txtToDate">
                </ajaxToolkit:CalendarExtender>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
