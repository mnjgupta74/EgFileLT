<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="ProcessSNADeptORBankData.aspx.cs" Inherits="ProcessSNADeptORBankData" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/bootstrap4/bootstrap.min.js"></script>
    <link href="../CSS/bootstrap4/bootstrap.min.css" rel="stylesheet" />
    <link href="../CSS/PageHeader.css" rel="stylesheet" />
    <style>
        .row, .col-2 {
            padding: 10px;
        }

        table#ctl00_ContentPlaceHolder1_rbl tbody tr td {
            padding-right: 180px;
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
            <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="SNA Data" class="pull-left">
                    <span _ngcontent-c6="" class="pull-right" style="color: #FFF">Process SNA Dept OR Bank Data</span></h2>
                <%--<img src="../../Image/help1.png" style="height: 35px;width: 34px;" data-toggle="tooltip" data-placement="left" Title="" />--%>
            </div>
            <div class="container-fluid">
                <div class="content-center">
                    <!-- <h2 id="heading">Sign Up Your User Account</h2> -->
                    <!-- <p>Fill all form field to go to next step</p> -->
                    <div id="msform">
                        <div class="row">
                            <div class="col-sm-2"></div>
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rbl" runat="server" AutoPostBack="true" Font-Bold="true"
                                    Font-Size="8pt" ForeColor="Green" OnSelectedIndexChanged="rblS_SelectedIndexChanged"
                                    RepeatDirection="Horizontal" Style="margin-left: 0px">
                                    <asp:ListItem Text="SNA Dept" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="SNA Bank" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                UserName : 
                            </div>
                            <div class="col-sm">
                                <asp:TextBox ID="txtusername" runat="server">SNA</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ErrorMessage="Enter User Name" ControlToValidate="txtusername"
                                    ValidationGroup="vldtc" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                Password :
                            </div>
                            <div class="col-sm">
                                <asp:TextBox ID="txtpassword" runat="server">Sna@123</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ErrorMessage="Enter Password" ControlToValidate="txtpassword"
                                    ValidationGroup="vldtc" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                Merchant Code :
                            </div>
                            <div class="col-sm">
                                <asp:TextBox ID="txtmerchantcode" runat="server">6001</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ErrorMessage="Enter Merchant Code" ControlToValidate="txtmerchantcode"
                                    ValidationGroup="vldtc" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm">
                                <asp:Button ID="btnCreateToken" runat="server" ValidationGroup="vldtc" class="btn-success" Text="Create Token" OnClick="btnCreateToken_Click"   width="30%"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                Token :
                            </div>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txttoken" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server" ErrorMessage="Enter Token" ControlToValidate="txttoken"
                                    ValidationGroup="vldtk" ForeColor="Red" Display="Dynamic">* Enter Token</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row" id="divRequestexp" runat="server">
                            <div class="col-sm" style="display: none;">
                                Request String Example For SAN Dept : <span runat="server" id="spanRequestExampleForSNADept">12327|10.00|844300106000000000|6400|Egras|1128|3800|123456|105|BARB</span>
                                <br />
                                Request String Example For SAN Bank : <span runat="server" id="spanRequestExampleForSNABank">[{"ReferenceNo":"12310","Amount":200.00,"PaymentDate":"2022-03-14 16:24:28","BankRefNo":"1234567","BSRCode":"9910001","Accountno":"123456","Merchantcode":"2110109"}]</span>
                            </div>
                        </div>
                        <div class="row" id="divRequest" runat="server">
                            <div class="col-sm-3">
                                Request String :
                            </div>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtString" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server" ErrorMessage="Enter SNA Bank or SNA Dept String" ControlToValidate="txtString"
                                    ValidationGroup="vldtk" ForeColor="Red" Display="Dynamic">* Enter SNA Bank or SNA Dept String</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row" id="divResponse" runat="server">
                            <div class="col-sm-3"></div>
                            <div class="col-sm">
                                <asp:Button ID="btnGetResponse" runat="server" ValidationGroup="vldtk" class="btn-success" Text="Get SNA Dept/Bank Response" OnClick="btnGetResponse_Click"     width="30%"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                Response String : 
                            </div>
                            <div class="col-sm">
                                <span runat="server" id="spanResponse"></span>
                            </div>
                        </div>

                    </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
