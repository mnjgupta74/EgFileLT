<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgFrmTOVerified.aspx.cs" Inherits="WebPages_EgFrmTOVerified" %>

<%@ Register Src="~/UserControls/Online_ManualRadioButton.ascx" TagPrefix="uc1" TagName="Online_ManualRadioButton" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script src="../js/Control.js" type="text/javascript"></script>
    <link href="../CSS/PageHeader.css" rel="stylesheet" />
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
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
    <script type="text/javascript">
        function NumericOnly(el) {
            var ex = /^\s*\d+\s*$/;
            if (ex.test(el.value) == false) {
                alert('Enter Number Only');
                el.value = "";
            }
        }
    </script>
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
                <h2 _ngcontent-c6="" title="Integration Mode">
                    <span _ngcontent-c6="" style="color: #FFF">To Verify</span></h2>
            </div>

            <table width="100%" border="1" align="center" style="padding-top: 1%;">
                <tr>
                    <td align="center">
                        <b><span style="color: #336699">Select Bank: : </span></b>&nbsp;
                        <div style="display: inline-table">
                            <asp:DropDownList ID="ddlbankname" runat="server" Width="180px" Style="margin-top: 10px" CssClass="borderRadius inputDesign form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td align="center">
                        <asp:RadioButtonList runat="server" ID="Online_ManualRadioButton" Width="150px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdBtnList_SelectedIndexChanged" ForeColor="#336699">
                            <asp:ListItem Text="Online" Value="N" Selected="True" style="margin-right: 15px" />
                            <asp:ListItem Text="Manual" Value="M" />
                        </asp:RadioButtonList>
                        <%--<uc1:Online_ManualRadioButton runat="server" ID="Online_ManualRadioButton" />--%>
                    </td>
                    <td id="RowFilter" runat="server" align="center">

                        <asp:RadioButtonList ID="RblFilter" runat="server" OnSelectedIndexChanged="RblFilter_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" Font-Bold="true">
                            <asp:ListItem Text="Filter GRNwise" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Filter Datewise" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <%-- <tr>
                    <td align="center">
                       <uc1:Online_ManualRadioButton runat="server" ID="Online_ManualRadioButton" />
                    </td>
                </tr>--%>

                <tr>
                    <td id="RowFromDate" runat="server" align="center" visible="false">
                        <b><span style="color: #336699">From Date : </span></b>&nbsp;
                        <div style="display: inline-table">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Height="30px" Style="margin-top: 10px"
                                onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                                Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                        </div>
                    </td>
                    <td id="RowToDate" runat="server" align="center" visible="false" colspan="2">
                        <b><span style="color: #336699">To Date : </span></b>&nbsp;
                        <div style="display: inline-table">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Height="30px" Style="margin-top: 10px"
                                onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" PopupButtonID="txtToDate"
                                Format="dd/MM/yyyy" TargetControlID="txtToDate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                                CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                        </div>
                    </td>
                    <td id="RowSearch" runat="server" align="center" visible="false" colspan="3">
                        <b><span style="color: #336699">Search By GRN : </span></b>&nbsp;
                        <div style="display: inline-table">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Height="30px" Style="margin-top: 10px"
                                MaxLength="20" onChange="NumericOnly(this);" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblMatch" runat="server" Text="" Visible="false" ForeColor="green"
                                    Font-Bold="true"></asp:Label>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <asp:HiddenField ID="queryParameters" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="3">
                        <div style="display: inline-table">
                            <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-default" Height="33px" ValidationGroup="de" OnClick="btnshow_Click" Style="margin-left: 0px" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-default" Height="33px" OnClick="btnReset_Click" Style="margin-left: 0px" />
                        </div>
                    </td>

                </tr>
                <tr>
                    <td colspan="3">

                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%" align="center">


                            <tr align="center">
                                <td>
                                    <asp:GridView ID="grdVerifyChallan" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                        Width="100%" ShowFooter="True" FooterStyle-BackColor="#336699" EditRowStyle-Font-Bold="true"
                                        EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                                        OnRowCommand="grdVerifyChallan_RowCommand">
                                        <%--AllowPaging="true" PageSize="25" OnPageIndexChanging="grdVerifyChallan_PageIndexChanging"--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="GRN" HeaderText="GRN">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BankCode" HeaderText="BankCode">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CIN" HeaderText="CIN">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButtonStatus" runat="server" CausesValidation="false" CommandName="Verify"
                                                        CommandArgument='<%#Container.DataItemIndex+1 %>' Text="Verify with Bank"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#336699" ForeColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
