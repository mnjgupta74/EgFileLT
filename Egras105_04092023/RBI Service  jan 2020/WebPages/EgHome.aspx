<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgHome.aspx.cs" Inherits="WebPages_EgHome" Title="Eg Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11" />

    <%--  <meta http-equiv="X-UA-Compatible" content="IE=edge;" />--%>

    <script type="text/javascript" src="../js/pop.js"></script>
    <%--<script src="//code.jquery.com/ui/1.11.0/jquery-ui.js"></script>--%>
    <%--<link href="../js/jquery-ui.css" rel="stylesheet" />--%>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <%-- <script src="../JS/chosen.jquery.js"></script>
    <link href="../CSS/chosen.css" rel=sssss"stylesheet" />--%>
    <link href="../js/SweetAlert/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/SweetAlert/sweetalert.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
        $(function () {
            $(".chzn-select").chosen({
                search_contains: true
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                //Binding Code Again
                $(".chzn-select").chosen({
                    search_contains: true
                });
            }
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
    <style type="text/css">
        .faux-button {
            cursor: pointer;
            
        }

        select {
            height: 25px;
        }

        legend {
            font-size: 12px;
            margin-bottom: 5px;
        }

        fieldset {
            padding: .75em;
        }

        #Ttable td {
            border: 2px solid #FFF;
        }

        .borderEgHome {
            border: 2px solid threedface;
            border-style: groove;
            border-image: initial;
        }

        .chzn-container {
            text-align: left;
            vertical-align: middle;
        }

        .chzn-container-single .chzn-single {
            border-radius: 0px;
            -webkit-border-radius: 0px;
            background-image: none;
        }

        /*.btn-primary {
            padding: 10px;
            width: 29%;
            font-weight: 900;
        }*/
    </style>


    <style>
        fieldset {
            padding-right: 20px !important;
            padding-left: 20px !important;
            padding-top: 0px !important;
        }

        .btn-primary {
            margin-top: 16px;
        }

        .chzn-container-single .chzn-search input {
            width: 210px;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="dialog" style="display: none">
                * please Change your password 
    <br></br>
                * now it is mandatory to change password after 45 days
            </div>
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="42%" valign="top" class="style1" style="padding: 0px; padding-right: 2px; margin-bottom: 5px;">
                            <fieldset class="borderEgHome" style="height: 174px; text-align: left">
                                <legend style="color: Green; width: inherit">Login Info</legend>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblfname" runat="server" Text="Name:-" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFirstNameBound" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblsucc" runat="server" Text="Last Successful Login:-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastsuccess" runat="server" Text="Label" ForeColor="#003300"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblfail" runat="server" Text="Last Failure Login:-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbllastfail" runat="server" Text="Label" ForeColor="#CC3300"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblpass" runat="server" Text="Last Change Password:-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastchange" runat="server" Text="Label" ForeColor="#000066"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td width="58%" valign="top" class="style1">
                            <fieldset class="borderEgHome p-10px" style="height: 174px;">
                                <legend style="color: Green; width: inherit">Profile </legend>
                                <br />
                                <div class="row" style="align-items: center;">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-8">
                                        <asp:RadioButtonList ID="rblService" runat="server" AutoPostBack="true" Font-Bold="true"
                                            Font-Size="8pt" ForeColor="Green" OnSelectedIndexChanged="rblService_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" Style="margin-left: 0px">
                                            <asp:ListItem Text="Profile Challan" Value="P" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Service Challan" Value="S"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row" id="divProfile" runat="server">
                                    <%--<table align="right">
                                        <tr>
                                            <td align="right">--%>
                                    <asp:HyperLink ID="HyperLink1" Class="pull-right" runat="server" NavigateUrl="~/WebPages/NewEgUserProfile.aspx">Create Profile</asp:HyperLink>
                                    <%--</td>
                                        </tr>
                                    </table>--%>

                                    <asp:Label ID="lblprofile" runat="server" Text="ProfileList:- " Font-Bold="true"></asp:Label>
                                    <asp:DropDownList ID="ddlprofile" runat="server" Width="280px" OnSelectedIndexChanged="ddlprofile_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="chzn-select">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddldept" runat="server" ErrorMessage="Select Profile"
                                        InitialValue="0" ValidationGroup="VldSelect" ControlToValidate="ddlprofile">
                                    </asp:RequiredFieldValidator>
                                    <div id="SchemaDiv" runat="server" visible="false">
                                        <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" Height="80px">
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
                                </div>
                                <div class="row" id="divService" runat="server" visible="false">
                                    <div class="pull-left">
                                        <asp:Label ID="Label2" runat="server" Text="Department:-" Font-Bold="true"></asp:Label>
                                        <asp:DropDownList ID="ddlDeptPopup" runat="server" OnSelectedIndexChanged="ddlDeptPopup_SelectedIndexChanged" AutoPostBack="true" Width="190px" CssClass="chzn-select">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Department"
                                            ControlToValidate="ddlDeptPopup" ValidationGroup="VldSelect" InitialValue="0" ForeColor="Red"
                                            Style="text-align: center">*</asp:RequiredFieldValidator>
                                    </div>

                                    <div class="pull-right">
                                        <asp:Label ID="Label1" runat="server" Text="Service:-" Font-Bold="true"></asp:Label>
                                        <asp:DropDownList ID="ddlService" runat="server" Width="180px" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select">
                                            <asp:ListItem Value="0" Text="--Select Service--"></asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Service"
                                            ControlToValidate="ddlService" ValidationGroup="VldSelect" InitialValue="0"
                                            ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pull-right">
                                        <asp:LinkButton ID="lnkbtnHelp" Style="font-size: 15px; margin-right: 19px;" runat="server" Text="Help" OnClick="lnkbtnHelp_Click"></asp:LinkButton>

                                    </div>

                                </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <%--<img src="../Image/icons8-traffic-light-48.png" />--%>
                        <td colspan="2">
                            <fieldset class="borderEgHome" style="height: 100px; text-align: left">
                                <legend style="color: Green; width: inherit">Quick Challan</legend>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:ImageButton ID="ImageButton1" style="margin-bottom: 10px;" ImageUrl="../Image/icons8-traffic-light-48.png" runat="server"
                                        OnClick="ImageButton1_Click" AlternateText="EChallan" />
                                    <asp:Label ID="lblEChallan" style="margin-left: -60px" runat="server" Text="EChallan" AssociatedControlID="ImageButton1" />
                                </div>
                                <div class="col-md-4">
                                    <asp:ImageButton ID="ImageButton2" style="margin-bottom: 10px;" ImageUrl="../Image/water-template-icons-app.png" runat="server"
                                        OnClick="ImageButton2_Click" AlternateText="WaterBill" />
                                    <asp:Label ID="Label3" style="margin-left: -60px" runat="server" Text="Water Bill" AssociatedControlID="ImageButton2" />
                                </div>
                                <div class="col-md-4">
                                    <asp:ImageButton ID="ImageButton3" style="margin-bottom: 10px;" ImageUrl="../Image/electricty.png" runat="server"
                                        OnClick="ImageButton3_Click" AlternateText="ElectricityBill" />
                                    <asp:Label ID="Label4" style="margin-left: -60px;" runat="server" Text="Electricity Bill" AssociatedControlID="ImageButton3" />
                                </div>

                            </div></fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" style="height: 26px">
                            <asp:Button ID="btnRedirect" runat="server" OnClick="btnRedirect_Click" Text="Continue"
                                ValidationGroup="VldSelect" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button Class="btn-primary" Style="padding: 10px; margin-top: 3%; width: 29%; font-weight: 900;" ID="btnShowRecords" Text=" Show Last 10 Transactions" runat="server" OnClick="btnShowRecords_Click" />
                        </td>
                    </tr>
                    <tr id="trOptions" runat="server" visible="false">
                        <td align="left" height="35px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblShow" runat="server" Font-Bold="true" ForeColor="Green" Text="Select For Show last 10 transactions"></asp:Label>
                            &nbsp;
                        </td>
                        <td align="left" height="35px">
                            <asp:RadioButtonList ID="rblTransaction" runat="server" AutoPostBack="true" Font-Bold="true"
                                Font-Size="8pt" ForeColor="Green" OnSelectedIndexChanged="rblTransaction_SelectedIndexChanged"
                                RepeatDirection="Horizontal" Style="margin-left: 0px">
                                <asp:ListItem Text="E-Banking" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Manual-Banking" Value="M"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="auto" width="100%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="stylebox" style="position: relative; height: auto;" runat="server" visible="false">
                                        <asp:Panel ID="pnlTemplate" runat="server">
                                            <fieldset class="fieldset borderEgHome">
                                                <legend style="color: Green; width: inherit;">Transactions</legend>
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
                                                                <td bgcolor="#507CD1" style="color: White;"></td>
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
                                                                ForeColor="Green" OnClientClick="return closeStyleBox('ctl00_ContentPlaceHolder1_stylebox');"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <%-- <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                        <div class="modal-dialog" style="position: static; margin-top: 190px; width: 990px" role="document">
                                            <div class="modal-content" style="position: initial">
                                                <div class="modal-header" style="height: 50px">
                                                    <h5 class="modal-title col-xs-11" id="exampleModalLabel" style="text-align: center; font-weight: 600; color: #004F00; font-size: 17px;">Select Service</h5>
                                                    <button type="button" class="close col-xs-1" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body" style="margin-top: 5px;">
                                                    <div class="row" style="color: #2874f0">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <span style="font-size: 15px; vertical-align: center;">Department Name :-</span>
                                                            <asp:DropDownList ID="ddlDeptPopup" runat="server" Width="200px" Style="height: 33px;" AutoPostBack="false" class="chzn-select">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Department"
                                                                ControlToValidate="ddlDeptPopup" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                                                                Style="text-align: center">*</asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="col-md-4" style="padding-right: 0px;">
                                                            <span style="font-size: 15px; vertical-align: center;">Service:-</span>
                                                            <asp:DropDownList ID="ddlService" runat="server" Style="font-family: Verdana !important; font-size: 13px; height: 24px;"
                                                                Width="70%">
                                                                <asp:ListItem Value="0" Text="--Select Service--"></asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Service"
                                                                ControlToValidate="ddlService" ValidationGroup="vldInsert" InitialValue="0"
                                                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="col-md-2" style="padding-right: 0px; text-align: center;">
                                                            <button type="button" class="btn btn-primary" id="btnSubmitPopup" style="height: auto">Submit</button>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="modal-footer" style="margin-top: 0px; padding: 15px 20px 15px">
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal" style="height: auto">Close</button>
                                                </div>
                                            </div>
                                        </div>--%>
                                    <%--</div>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnHelp" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>

