<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgGRNScrollRpt.aspx.cs" Inherits="WebPages_Reports_EgGRNScrollRpt"
    Title="Untitled Page" %>

<%@ Register Src="~/UserControls/Online_ManualRadioButton.ascx" TagPrefix="uc1" TagName="Online_ManualRadioButton" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

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

        #ctl00_ContentPlaceHolder1_ddlbankname_chosen {
            margin-left: 20px;
        }

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
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

        function dateValidation1(txtDate) {
            var dtObj = txtDate;
            var dtStr = dtObj.value
            var dtTemp = dtStr

            if (dtStr == '') {
                alert('Date cant be blank')
                dtObj.value = ""
                return false
            }

            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            //check for parts of date
            var DayDt
            var MonDt
            var YearDt

            dtTemp = dtStr
            DayDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            MonDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            YearDt = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (YearDt < 2009) {
                alert('year cannot be less than 2009')
                dtObj.value = ""
                return false
            }
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt) || isNaN(MonDt) || isNaN(YearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt, parseInt(MonDt - 1), DayDt)


            if (DateEntered.getMonth() != (parseInt(MonDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }


            var dt1 = parseInt(dtStr.substring(0, 2), 10);
            var mon1 = (parseInt(dtStr.substring(3, 5), 10) - 1);
            var yr1 = parseInt(dtStr.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1); // To Date


            var str2 = new Date();
            var s = str2.format("dd/MM/yyyy");
            var dt2 = parseInt(s.substring(0, 2), 10);
            var mon2 = (parseInt(s.substring(3, 5), 10) - 1);
            var yr2 = parseInt(s.substring(6, 10), 10); // Current Date


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
            <div _ngcontent-c6="" class=" sectiontopheader tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Bank-Scroll" class="pull-left">
                    <span _ngcontent-c6="" class="pull-right" style="color: #FFF">Bank-Scroll Report Status</span></h2>
            </div>

            <%--<fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px">
                <legend style="color: #336699; font-weight: bold">Bank-Scroll Report Status</legend>--%>

            <table style="width: 100%" align="center" border="1" id="MainTable">
                <tr>
                    <td colspan="4">
                    <div id="divRecord" runat="server" visible="false">
                        <div id="example14" runat="server">
                            <img alt="Error" id="ImageS" src="../../Image/success.png" />
                            <asp:Label ID="LabelS" runat="server" ForeColor="#3ab51c" Text="Match"></asp:Label>
                            &nbsp;&nbsp;
                                    <img alt="Error" id="ImageF" src="../../Image/delete.png" />
                            <asp:Label ID="LabelF" runat="server" Text="Mismatch    " ForeColor="#bd190a"></asp:Label>&nbsp;&nbsp;
                                    <img alt="Error" src="../../Image/doubt_icon.png" id="ImageD" />
                            <asp:Label ID="LabelD" runat="server" Text="Doubted" ForeColor="#3479af"></asp:Label>
                        </div>
                    </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:RadioButtonList ID="rblstatus" runat="server" Style="width: 70% !important; display: contents !important" CssClass="form-control"
                            RepeatDirection="Horizontal" ForeColor="#336699" AutoPostBack="true" Font-Bold="true"
                            OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                            <asp:ListItem Value="1" style="margin-right:15px" Text="Match" Selected="True" ></asp:ListItem>
                            <asp:ListItem Value="2" style="margin-right:15px" Text="Mismatch" ></asp:ListItem>
                            <asp:ListItem Value="3" Text="Doubted"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr >
                    <td align="left">
                        <b><span style="color: #336699;">Date : </span></b>&nbsp;
                        <asp:TextBox ID="txtDate" runat="server" Height="100%" Style="display: initial !important" CssClass="form-control" Width="120px"
                            onkeypress="Javascript:return NumberOnly(event)" onchange="dateValidation1(this)"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtDate"
                            Format="dd/MM/yyyy" TargetControlID="txtDate">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" colspan="4">
                        <b><span style="color: #336699;">Select Bank : </span></b>&nbsp;
                        <asp:DropDownList ID="ddlbankname" runat="server" Width="50%" style="display: initial !important;" CssClass="form-control chzn-select">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    
                </tr>

                <tr align="center">
                    <td >
                        <%--<uc1:Online_ManualRadioButton runat="server" Style="width: 70% !important; display: contents !important" CssClass="form-control"
                                                      ID="Online_ManualRadioButton" />--%>
                        <asp:RadioButtonList runat="server" Style="width: 70% !important; display: contents !important" CssClass="form-control" 
                                             ID="Online_ManualRadioButton" Width="130px" RepeatDirection="Horizontal" ForeColor="#336699">
                            <asp:ListItem Text="Online" Value="N" style="margin-right:20px" Selected="True"  />
                            <asp:ListItem Text="Manual" Value="M"  />
                        </asp:RadioButtonList>
                    </td>

                    <td colspan="4" align="center">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Button ID="btnshow" Height="33" CssClass="btn btn-default pull-right" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnReset" Height="33" CssClass="btn btn-default pull-left" runat="server" Text="reset" ValidationGroup="de" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </td>
                </tr>

                <%--<tr visible="true" runat="server" id="Online_ManualRow">
                        <td align="center" colspan="4">
                            <uc1:Online_ManualRadioButton runat="server" ID="Online_ManualRadioButton" />
                        </td>
                    </tr>--%>
                <tr id="TotalTransRow" runat="server" visible="false">
                    <%--<td align="right" colspan="4" style="background-color: #FFFFFF">--%>
                    <td align="left" colspan="2" style="background-color: #FFFFFF">
                        <b>E-Treasury Total Transactions:</b>&nbsp;&nbsp;
                            <asp:Label ID="lblETotalTransactions" runat="server" Text="" Font-Bold="True" ForeColor="#009900"></asp:Label>
                    </td>
                    <td align="right" style="background-color: #FFFFFF">
                        <b style="text-align: left">E-Treasury Total Amount:</b>&nbsp;&nbsp;
                    </td>
                    <td align="right" style="background-color: #FFFFFF">
                        <asp:Label ID="lblEAmount" runat="server" Text="" Style="text-align: right;" Font-Bold="True" ForeColor="#009900"></asp:Label>
                    </td>
                    <%--</td>--%>
                </tr>
                <tr id="GrandTotalRow" runat="server" visible="false">
                    <%--<td align="right" colspan="4" style="background-color: #FFFFFF">--%>
                    <%--<b>Grand Total: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                    <td align="left" colspan="2" style="background-color: #FFFFFF">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Bank Total Transactions:</b>&nbsp;&nbsp;
                            <asp:Label ID="lblBTotalTransactions" runat="server" Text="" Font-Bold="True" ForeColor="#009900"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td align="right" style="background-color: #FFFFFF">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b style="text-align: left">Bank Total Amount:</b>&nbsp;&nbsp;
                    </td>
                    <td align="right" style="background-color: #FFFFFF">
                        <asp:Label ID="lblBAmount" runat="server" Text="" Style="text-align: right;" Font-Bold="True" ForeColor="#009900"></asp:Label>
                    </td>
                    <%--</td>--%>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:GridView ID="grdScroll" runat="server" AutoGenerateColumns="False" Font-Names="Verdana"
                            Font-Size="10pt" EmptyDataText="No Record Found" EmptyDataRowStyle-Font-Bold="true"
                            EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                            CellPadding="4" DataKeyNames="Flag" OnRowDataBound="grdScroll_RowDataBound" GridLines="None"
                            OnPageIndexChanging="grdScroll_PageIndexChanging" AllowPaging="True" PageSize="25"
                            ForeColor="#333333" OnRowCommand="grdScroll_RowCommand" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRN">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkGRN" ForeColor="#336699" runat="server" Text='<%# Eval("GRN") %>'
                                            CommandArgument='<%# Eval("GRN") %>' CommandName="View" ToolTip="Click For Challan View"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CIN" HeaderText="CIN">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Scroll Date">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankDate" styl="margin-left:10px;" runat="server" Text='<%# Eval("BankDate","{0:MM/dd/yyyy HH:mm:ss}") %>' />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="BankDate" HeaderText="Transaction Date">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>--%>
                                <asp:BoundField DataField="GbankAmount" HeaderText="E-Treasury Amount" DataFormatString="{0:n}">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UploadAmount" HeaderText="Scroll Amount" DataFormatString="{0:n}">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtn" runat="server" CssClass="target" Width="16px" Height="16px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="lblemsg" runat="server" visible="false">
                    <td colspan="4">
                        <asp:Label ID="lble" runat="server" Style="color: red">No Record Found.</asp:Label>
                    </td>
                </tr>
            </table>

            <%--</fieldset>--%>
            <div id="divTooltip" runat="server">
                <div id="divMatch">
                    Record Matched both at Egras and Bank Side.
                </div>
                <div id="DivMisMatch">
                    Amount Do not match with data.
                </div>
                <div id="divTooltipData">
                    Record do not exists either at Egras side or Bank side.
                </div>
            </div>

            <script type="text/javascript" language="javascript" type="text/javascript">

                function jquery() {
                    $('#divMatch').hide();
                    $('#DivMisMatch').hide();
                    $('#divTooltipData').hide();
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
