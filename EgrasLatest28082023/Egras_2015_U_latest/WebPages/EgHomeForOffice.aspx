<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgHomeForOffice.aspx.cs" Inherits="WebPages_EgHomeForOffice" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <%-- <script src="../js/chosen.jquery.js"></script>
       <link href="../CSS/chosen.css" rel="stylesheet" />--%>
       <link href="../js/SweetAlert/sweetalert.css" rel="stylesheet" />
       <script src="../js/SweetAlert/sweetalert.min.js"></script>
  <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });

        function myAlert(heading, mycontent) {
            //swal(heading, mycontent);
            swal({
                title: heading,
                text: mycontent,
                button: "Close",
            });
        }
    </script>
    <style>
        #ctl00_ContentPlaceHolder1_rbltype tbody tr td {
            border: 1px solid gray;
            background: cornflowerblue;
            height: 25px;
            padding: 1px;
            border-radius: 5px;
            color: black;
        }
    </style>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../App_Themes/Images/progress.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" colspan="4">
                            <asp:RadioButtonList ID="rbltype" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbltype_SelectedIndexChanged" Style="font-size: 14px;">
                                <asp:ListItem Text="Regular" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Minus Expenditure" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>

                    <tr>
                       
                        <td colspan="4">
                            <fieldset class="borderEgHome">
                                <legend style="color: Green; width: inherit">Profile </legend>
                                <br />
                                <div class="row" style="align-items: center;">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-4">
                                        <asp:RadioButtonList ID="rblService" runat="server" AutoPostBack="true" Font-Bold="true"
                                            Font-Size="8pt" ForeColor="Green" OnSelectedIndexChanged="rblService_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" Style="margin-left: 380px">
                                            <asp:ListItem Text="Profile Challan" Value="P" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Service Challan" Value="S"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-4"></div>

                                </div>
                                <div class="row" id="divProfile" runat="server">

                                    
                                    <table>
                                        <tr>
                                            <td>
                                                  <asp:Label ID="lblprofile" runat="server" Text="ProfileList:- " Font-Bold="true"></asp:Label>
                                    <asp:DropDownList ID="ddlprofile" runat="server" Width="280px" OnSelectedIndexChanged="ddlprofile_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="chzn-select">
                                    </asp:DropDownList>
                                 
                                    <asp:RequiredFieldValidator ID="rfvddldept" runat="server" ErrorMessage="Select Profile"
                                        InitialValue="0" ValidationGroup="VldSelect" ControlToValidate="ddlprofile">
                                    </asp:RequiredFieldValidator>
                                    <div id="SchemaDiv" runat="server" visible="false">
                                        <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" Height="106px">
                                            <table id="Table3">
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="RptProfile" runat="server" Visible="false">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblEmpty" runat="server" Font-Bold="true" ForeColor="#20872E"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr style="background-color: #5F8DC9;">
                                                                        <td style="text-align: left" width="200px">Schemaname
                                                                        </td>
                                                                        <td style="text-align: center" width="200px">&nbsp;&nbsp;&nbsp;BudgetHead
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td style="text-align: left" width="200px">
                                                                            <%# DataBinder.Eval(Container.DataItem, "Schemaname")%>
                                                                        </td>
                                                                        <td style="text-align: left" width="100px">
                                                                            <%# DataBinder.Eval(Container.DataItem, "BudgetHead")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr style="background-color: #E1E9F2;">
                                                                        <td style="text-align: left" width="200px">
                                                                            <%# DataBinder.Eval(Container.DataItem, "Schemaname")%>
                                                                        </td>
                                                                        <td style="text-align: left" width="100px">
                                                                            <%# DataBinder.Eval(Container.DataItem, "BudgetHead")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </AlternatingItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                            </td>
                                            <td align="right">
                                        <asp:HyperLink ID="HyperLink1" Class="pull-right" runat="server" NavigateUrl="~/WebPages/NewEgUserProfile.aspx">Create Profile</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                  
                                </div>
                                <div class="row" id="divService" runat="server" visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Department:-" Font-Bold="true"></asp:Label>
                                                <asp:DropDownList ID="ddlDeptPopup" runat="server" OnSelectedIndexChanged="ddlDeptPopup_SelectedIndexChanged" AutoPostBack="true" Width="180px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Department"
                                                    ControlToValidate="ddlDeptPopup" ValidationGroup="VldSelect" InitialValue="0" ForeColor="Red"
                                                    Style="text-align: center">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Service:-" Font-Bold="true"></asp:Label>
                                                <asp:DropDownList ID="ddlService" runat="server" Width="180px" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="--Select Service--"></asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Service"
                                                    ControlToValidate="ddlService" ValidationGroup="VldSelect" InitialValue="0"
                                                    ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" style="height: 26px">
                            <asp:Button ID="btnRedirect" runat="server" OnClick="btnRedirect_Click" Text="Continue"
                                ValidationGroup="VldSelect" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" height="35px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblShow" runat="server" Font-Bold="true" ForeColor="Green" Text="Show last 10 transactions"></asp:Label>
                            &nbsp;
                        </td>
                        <td align="left" height="35px">
                            <asp:RadioButtonList ID="rblTransaction" runat="server" AutoPostBack="true" Font-Bold="true"
                                Font-Size="8pt" ForeColor="Green" OnSelectedIndexChanged="rblTransaction_SelectedIndexChanged"
                                RepeatDirection="Horizontal" Style="margin-left: 0px">
                                <asp:ListItem Text="E-Banking" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Manual-Banking" Value="M"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="auto" width="100%">
                            <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <div id="stylebox" style="position: relative; height: auto;">
                                <asp:Panel ID="pnlTemplate" runat="server">
                                    <fieldset class="fieldset">
                                        <legend style="color: Green;">Transactions</legend>
                                        <table width="100%" id="Ttable">
                                            <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound" OnItemCommand="rpt_ItemCommand">
                                                <HeaderTemplate>
                                                    <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center; height: 20px"
                                                        id="header" runat="server">
                                                        <td bgcolor="#507CD1" style="color: White;">Sr.No
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White;">ChallanDate
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White;">GRN
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White;">Status
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White;">PaymentType
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White; text-align: right">Amount
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White;">Repeat
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White;">View
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White;"><%--ChallanType--%>
                                                        </td>
                                                        <td bgcolor="#507CD1" style="color: White;"></td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr style="background-color: #EFF3FB; height: 20px;">
                                                        <td align="center" style="font-size: 15; color: Black;">
                                                            <%# Container.ItemIndex+1 %>
                                                        </td>
                                                        <td align="center" style="font-size: 15; color: Black;">
                                                            <%#  Eval("ChallanDate")%>
                                                        </td>
                                                        <td align="center" style="font-size: 15; color: Black;">
                                                            <%#  Eval("Grn")%>
                                                        </td>
                                                        <td align="center" style="font-size: 15; color: Black;">
                                                            <%#  Eval("Status")%>
                                                        </td>
                                                        <td align="center" style="font-size: 15; color: Black;">
                                                            <%#  Eval("PaymentType")%>
                                                        </td>
                                                        <td align="center" style="font-size: 15; color: Black; text-align: right;">
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# string.Format("{0:0.00}", Eval("TotalAmount"))%>'></asp:Label>

                                                        </td>
                                                        <td align="center">
                                                            <asp:LinkButton ID="lnkrepeat" runat="server" CausesValidation="false" CommandName="Repeat"
                                                                CommandArgument='<%#Eval("Grn")%>' Text="Repeat"></asp:LinkButton>
                                                        </td>


                                                        <td align="center">
                                                            <asp:LinkButton ID="LinkStatus" runat="server" CausesValidation="false" CommandName="Status"
                                                                CommandArgument='<%# Eval("Grn") + "&" + Eval("PaymentType") %>' Text="View"></asp:LinkButton>
                                                            <div id="tooltip" style="position: absolute; display: none; width: 300px; padding-left: 100px">
                                                                <div style="padding: 400px 450px 0px; text-align: center">
                                                                    <div id="text" style="padding-left: 10px; padding-right: 10px; position: relative">
                                                                    </div>
                                                                </div>
                                                                <div style="padding: 10px 8px 0px; text-align: center;">
                                                                    &nbsp;
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="LinkVerify" runat="server" CausesValidation="false" CommandName="Verify" ForeColor="Green" Visible="false"
                                                                ToolTip='<%#Eval("BankName")%>' Text="Verify"></asp:LinkButton>
                                                        </td>
                                                        <td align="center">
                                                            <asp:UpdatePanel ID="UpdatePannelpdf" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="LinkPDF" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="LinkPDF" runat="server" ImageUrl="~/Image/pdf.jpg" CommandName="PDF"
                                                                        CommandArgument='<%# Eval("Grn")  %>' ToolTip="Click for PDF print" />
                                                                    <div id="tooltipPDF" style="position: absolute; display: none; width: 300px; padding-left: 100px">
                                                                        <div style="padding: 400px 450px 0px; text-align: center">
                                                                            <div id="text1" style="padding-left: 10px; padding-right: 10px; position: relative">
                                                                            </div>
                                                                        </div>
                                                                        <div style="padding: 10px 8px 0px; text-align: center;">
                                                                            &nbsp;
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <%-- <td>
                                                                      <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="DDOCode"
                                                                        CommandArgument='<%#Eval("DDOCode")%>' Text="ChallanType"></asp:LinkButton>

                                                                </td>--%>
                                                    </tr>
                                                </ItemTemplate>


                                            </asp:Repeater>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblErrorMsg" runat="server" Text="No Record Found" Visible="false"
                                                        Font-Bold="true" ForeColor="#507CD1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Click here to hide" Font-Bold="true"
                                                        ForeColor="Green" OnClientClick="return closeStyleBox('stylebox');"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </asp:Panel>
                            </div>
                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
