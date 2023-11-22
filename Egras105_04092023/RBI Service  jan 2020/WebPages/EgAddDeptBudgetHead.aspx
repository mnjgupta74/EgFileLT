<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAddDeptBudgetHead.aspx.cs" Inherits="WebPages_Admin_EgAddDeptBudgetHead"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <link href="../CSS/PageHeader.css" rel="stylesheet" />
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">

        function NumberOnly(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 3 && parts[1].length >= 3) return false;
        }

        function CheckBudget() {
            var Blength = document.getElementById("<%=txtBudgetHead.ClientID %>");

            if (Blength.value.length < 4) {
                alert("Please fill correct MajorHead");
                Blength.value = "";
            }
        }
    </script>
     <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
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
             <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="Office-Master" class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">Map Budget-Head</span></h2>
                <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="" />
            </div>
            <table style="width: 100%" border="1" align="center" id="MainTable">
                <tr>
                    <td align="center">
                        <b><span style="color: #336699">MajorHead : </span></b>&nbsp;
                       
                        <div style="display: inline-table">
                            <asp:TextBox ID="txtBudgetHead" runat="server" CssClass="form-control" Style="margin-top:10px" Height="30px" Width="90px" MaxLength="4" 
                            onchange="JavaScript:return CheckBudget()" TabIndex="1"></asp:TextBox>
                       
                        <asp:RequiredFieldValidator ID="rfctxtbudgethead" runat="server" ErrorMessage="*"
                            ControlToValidate="txtBudgetHead" ValidationGroup="VldInsert">
                        </asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                    </td>
                    <td align="center">
                        <b><span style="color: #336699">MajorHeadName : </span></b>&nbsp;
                        <div style="display: inline-table">
                            <asp:TextBox ID="txtbudgetheadName" runat="server" CssClass="form-control" Style="margin-top:10px" Height="30px" MaxLength="200" TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="txtbudgetheadName" ValidationGroup="VldInsert">
                        </asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnsearch" runat="server" Text="Add" CssClass="btn btn-default" Height="33px" ValidationGroup="VldInsert"
                            OnClick="btnsearch_Click" TabIndex="3" />
                        </td>
                </tr>
            </table>
                 <table runat="server" style="width: 100%;" id="TABLE1" cellpadding="0" border="1"
                cellspacing="0" align="center">
                <tr>
                    <td colspan="2">
                        <asp:Repeater ID="rptMapbudgethead" runat="server">
                            <HeaderTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr style="background-color: #5F8DC9;">
                                        <td align="center" width="50px" height="25px">
                                            <b>Sr.No </b>
                                        </td>
                                        <td align="left" width="122px" height="25px">
                                            <b>BudgetHead</b>
                                        </td>
                                        <td align="left" width="122px" height="25px">
                                            <b>BudgetHeadName</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblEmpty" Style="text-align: center" runat="server" Text="No Record Found"
                                    runat="server" Font-Bold="true" ForeColor="Teal" Visible='<%#bool.Parse((rptMapbudgethead.Items.Count==0).ToString())%>'></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" width="50px" height="25px">
                                            <%# Container.ItemIndex+1 %>
                                        </td>
                                        <td align="left" width="122px" height="25px">
                                            <%# Eval("BudgetHead")%>
                                        </td>
                                        <td align="left" width="122px" height="25px">
                                            <%# Eval("BudgetHeadName")%>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr style="background-color: #E1E9F2;">
                                        <td align="center" width="50px" height="25px">
                                            <%# Container.ItemIndex+1 %>
                                        </td>
                                        <td align="left" width="122px" height="25px">
                                            <%# Eval("BudgetHead")%>
                                        </td>
                                        <td align="left" width="122px" height="25px">
                                            <%# Eval("BudgetHeadName")%>
                                        </td>
                                    </tr>
                                </table>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
