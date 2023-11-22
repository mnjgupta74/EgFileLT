<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgOpenBankScrollYear.aspx.cs" Inherits="WebPages_TO_EgOpenBankScrollYear" %>

<%@ Register Src="~/UserControls/CalenderYear.ascx" TagPrefix="uc1" TagName="CalenderYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <style type="text/css">
        .btn-primary {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
            margin-top: 20px;
            height: 35px;
        }

        .sectiontopheader[_ngcontent-c6] {
            border-top: 5px solid #337ab7;
            margin-bottom: 5px;
            display: -webkit-box;
        }

        .tnHead, .sectiontopheader {
            display: flex;
            justify-content: space-between;
            align-items: center;
            position: relative;
        }

            .sectiontopheader[_ngcontent-c6] h1[_ngcontent-c6] {
                background: #337ab7;
            }

            .sectiontopheader h1, .tnHead h1 {
                padding: 8px 20px;
                position: relative;
                top: -5px;
                margin: 0;
                font-size: 18px;
            }

                .sectiontopheader h1:after, .tnHead h1:after {
                    position: absolute;
                    right: -34px;
                    top: 0;
                    content: '';
                    border-style: solid;
                    border-width: 34px 34px 0 0;
                }

            .sectiontopheader[_ngcontent-c6] h1[_ngcontent-c6]:after {
                border-color: #337ab7 transparent transparent;
            }
    </style>
    <style type="text/css">
        .sectiontopheader[_ngcontent-c6] {
            border-top: 5px solid #337ab7;
            margin-bottom: 5px;
            display: -webkit-box;
        }

        .tnHead, .sectiontopheader {
            display: flex;
            justify-content: space-between;
            align-items: center;
            position: relative;
        }

            .sectiontopheader[_ngcontent-c6] h1[_ngcontent-c6] {
                background: #337ab7;
            }

            .sectiontopheader h1, .tnHead h1 {
                padding: 8px 20px;
                position: relative;
                top: -5px;
                margin: 0;
                font-size: 18px;
            }

                .sectiontopheader h1:after, .tnHead h1:after {
                    position: absolute;
                    right: -34px;
                    top: 0;
                    content: '';
                    border-style: solid;
                    border-width: 34px 34px 0 0;
                }

            .sectiontopheader[_ngcontent-c6] h1[_ngcontent-c6]:after {
                border-color: #337ab7 transparent transparent;
            }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_CalenderYear_ddlCalenderYear, #ctl00_ContentPlaceHolder1_CalenderYear_ddlCalenderYear {
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }

        input[type=submit] {
            width: 100px;
            height: 35px;
        }

        input[type=text], input[type=password] {
            height: 35px;
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
                    <img src="../../App_Themes/Images/progress.gif" />
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
            <div class="">
                <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
                    <h2 _ngcontent-c6="" title="OpenYearForBankScroll">
                        <span _ngcontent-c6="" style="color: #FFF">Open Year For Bank Scroll</span></h2>
                </div>
                <div class="row" style="border: 1px solid; width: 100%; margin-left: 0px; padding: 5px;">
                    <div class="col-md-3">
                        <label>Year</label>
                        <uc1:CalenderYear runat="server" ID="CalenderYear" />
                    </div>

                    <div class="col-md-4">
                        <div class="col-md-10">
                            <label>Bank</label>
                            <asp:DropDownList ID="ddlbankgrnstatus" class="selectpicker form-control" runat="server" ValidationGroup="de">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2" style="    margin-top: 15%;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Bank"
                                ControlToValidate="ddlbankgrnstatus" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                                Style="text-align: center">*</asp:RequiredFieldValidator>
                        </div>


                    </div>
                    <div class="col-md-2">
                        <label>Duration(In Days)</label>
                        <asp:TextBox ID="txtDays" class="form-control" MaxLength="2" ValidationGroup="de" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDays"
                            ErrorMessage="Please Enter Only Numbers" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="de">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Fill Durations"
                            ControlToValidate="txtDays" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                            Style="text-align: center">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Submit" ValidationGroup="de" OnClick="btnSubmit_Click" />
                        <asp:Button runat="server" ID="btnReset" Style="width: 45%" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click" />

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

