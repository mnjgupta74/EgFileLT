<%@ Page Title="Egras.Rajasthan.gov.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgAddNewOffice.aspx.cs" Inherits="WebPages_Admin_EgAddNewOffice" %>
<%@ Register Src="~/UserControls/FinancialYearDropDown.ascx" TagName="FinYear" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <style type="text/css">
        #ctl00_ContentPlaceHolder1_lblmsg {
            margin-left: 20px;
        }

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>
    <script type="text/javascript">
        function NumberOnly(field) {
            var valid = "0123456789"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("  Only Numeric Value Allowed.!");
                field.focus();
                field.select();
                field.value = "";
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
            <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Office-Master" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Office-Master</span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Office-Master" />
            </div>
            <div style="margin-top: 20px; border: 1px solid #808080; padding-top: 5px;">
            <fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px">
                <%--<legend style="color: #336699; font-weight: bold">Office-Master</legend>--%>
                <table style="width: 100%" align="center" id="MainTable">
                    <tr style="height: 40px">
                        <td align="center">
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 16px;">Office Id:-</span></b>&nbsp;
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtofficeid" runat="server" height="33px" OnChange="Javascript:return NumberOnly(this)"
                                MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtofficeid"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnshow" runat="server" CssClass="btn btn-default" height="40px" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnVerify" CssClass="btn btn-default" height="40px" runat="server" Text="ReVerify" ValidationGroup="de" OnClick="btnVerify_Click"
                                Visible="false" />
                            <asp:Label ID="lblmsg" runat="server" Text="Label1" Visible="false"></asp:Label>
                            <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />  --%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="height: 40px">
                            
                            <div>
                                <fieldset runat="server" id="Fieldset1" style="width: 450px;margin-right: 100px;" visible="false">
                                    <legend style="color: #336699; font-weight: bold;font-size: 20px;">Office-Detail</legend>
                                    <table>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">Office Name:- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblOffice" runat="server" Text="OfficeName"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">Treasury Code :-</span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblTreas" runat="server" Text="Treasury"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">DDO-Code :- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblDDO" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">CreateDate :- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblFromdate" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">ParentOfficeid :- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lblpid" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b><span style="width: 220px; color: #336699; font-family: Arial CE; font-size: 13px;">DeptCode :- </span></b>&nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:Label ID="lbldept" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>

                                <table align="center" style="width: 100%" runat="server" id="tblQuestion" visible="false">
                                    <tr>
                                        <td align="right">
                                            <%--<asp:Label ID="lblQuestion" runat="server" style="margin-right: 20px;">Do You Want To Add New Trassury?</asp:Label>--%>
                                            <asp:LinkButton ID="lnkClick" OnClick="lnkClick_Click" style="margin-right:62px" runat="server">Click Here To Add Treasury On Egras</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center" style="width: 100%" runat="server" id="tblTreasury" visible="false">
                                    <tr style="height: 40px">
                                        <td align="left">
                                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 16px; margin-left: 10px">Treasury:- </span></b>&nbsp;
                                        </td>
                                        <td align="left">
                                           
<asp:DropDownListX ID="ddllocation" runat="server" class="chzn-select" Style="margin-right: 10px; height: 33px">
                                                    <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                                                </asp:DropDownListX>

                                            <asp:Button ID="btnAddTreasury" runat="server" Text="Add Treasury" Style="width:150px;margin-left:20px" CssClass="btn btn-default" height="40px" OnClick="btnAddTreasury_Click" />
                                        </td>
                                    </tr>



                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
