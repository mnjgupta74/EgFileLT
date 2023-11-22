<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgTy11.aspx.cs" Inherits="WebPages_Reports_EgTy11" Title="Ty11" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js" type="text/javascript"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <%--==================CSS-JQUERY LOADER==================--%>
 
     <style type="text/css">
        #cover-spin {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            display: none;
        }
        #ctl00_ContentPlaceHolder1_RadioButtonList1_0 
        {
            margin-left:150px;
        }

        .btn-default{
                color: #f4f4f4;
                background-color: #428bca;
        }       

        #cover-spin::after {
            content: '';
            display: block;
            position: absolute;
            left: 48%;
            top: 40%;
            width: 40px;
            height: 40px;
            border-style: solid;
            border-color: black;
            border-top-color: transparent;
            border-width: 4px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }
    </style>

    <div id="cover-spin"></div>
   
    
    <%--==============END CSS - JQUERY LOADER============--%>


    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Challan Detail</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Ty-33" />
    </div>

    <table align="center" style="width: 100%" border="1">
        <%-- <tr>
            <td colspan="6" style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="Ty-33" Font-Bold="True" ForeColor="#009900"
                    Style="text-decoration: underline;"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td colspan="6" style="text-align: center; height: 35px; padding-left: 250px;" valign="top">
                <asp:RadioButtonList runat="server" style="display:contents !important" ID="rbtnList" Width="550px" CssClass="form-control" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtnList_SelectedIndexChanged">
                    <asp:ListItem Text="TY33 " Value="EgTy11" Selected="True" style="margin-right: 35px" />
                    <asp:ListItem Text="TY33 Summary " Value="EgTy11Summary" style="margin-right: 35px" />
                    <asp:ListItem Text="Ty33 Division Wise " Value="EgTy11DivisionWise" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <b><span style="color: #336699">From Date : </span></b>&nbsp;
                <asp:TextBox ID="txtfromdate" Height="100%" Style="display: initial !important" CssClass="form-control" runat="server" Width="120px" TabIndex="1"
                    onpaste="return false" onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td>
                <b><span style="color: #336699">To Date : </span></b>&nbsp;
                <asp:TextBox ID="txttodate" Height="100%" Style="display: initial !important" CssClass="form-control" runat="server" Width="120px" TabIndex="2"
                    onpaste="return false" onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="txttodate" TargetControlID="txttodate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txttodate"
                    ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td>
                <b><span style="color: #336699">Major Head : </span></b>&nbsp;
                <asp:TextBox CssClass="form-control" Height="100%" Style="display: initial !important" ID="txtMajorHead" MaxLength="4" runat="server"
                    Width="120px"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td id="tddivcode" runat="server" visible="false">
                <b><span style="color: #336699">DivCode : </span></b>&nbsp;
                    <asp:DropDownList ID="divcode" Style="display: initial !important"  CssClass="form-control chzn-select" runat="server" Width="300px"></asp:DropDownList>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" Style="width: 70% !important; display:contents !important" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True" style="margin-right: 35px;">Simple</asp:ListItem>
                    <asp:ListItem Value="2" >PDF</asp:ListItem>
                </asp:RadioButtonList>

            </td>
            <td colspan="4">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Button ID="btnshow"  runat="server" Height="33" CssClass="btn btn-default pull-right" Text="Show" OnClick="btnshow_Click" ValidationGroup="a" />

                    </div>
                    <div class="col-md-4" >
                     <asp:Button ID="btnSignPdf" Height="33" style="margin-right:22%;"  CssClass="btn btn-default pull-right" runat="server" Text="SignPDF" Enabled="false" ValidationGroup="a" OnClick="btnSignPdf_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnReset" runat="server"  Height="33" CssClass="btn btn-default pull-left" Text="Reset" ValidationGroup="de" OnClick="btnReset_Click" />

                    </div>

                </div>
            </td>
        </tr>


        <tr runat="server" id="trrpt" visible="false">
            <td colspan="6">
                <center>
                    <rsweb:ReportViewer ID="rptTy11" runat="server" Width="100%" SizeToReportContent="true"
                        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
                    </rsweb:ReportViewer>

                </center>
            </td>
        </tr>
    </table>
</asp:Content>
