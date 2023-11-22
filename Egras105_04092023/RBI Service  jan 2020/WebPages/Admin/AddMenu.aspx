<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="AddMenu.aspx.cs"
    Inherits="WebPages_Admin_AddMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../js/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkUserType.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>
    <style type="text/css">
        #ctl00_ContentPlaceHolder1_gridMenuDetail td, th {
            border: 1px solid gray;
            text-align: center;
        }

        .sectiontopheader[_ngcontent-c6] {
            border-top: 5px solid #337ab7;
            margin-bottom: 5px;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
                <h1 _ngcontent-c6="" title="ViewBunchChallan">
                    <span _ngcontent-c6="" style="color: #FFF">Add Menu</span></h1>
            </div>
            <div class="row panel" style="margin: 0px; text-align: left">
                <div class="row" style="margin: 0px;">
                    <div class="col-sm-6 col-xs-12" style="padding: 0px; margin-bottom: 5px;">
                        <div class="col-xs-4 col-sm-4" style="padding: 0px;">
                            Menu Description
                        </div>
                        <div class="col-xs-6 col-sm-6" style="padding: 0px;">
                            <asp:TextBox runat="server" ID="txtMenuDesc" CssClass="" Style="float: left; width: 90%;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                runat="server" ControlToValidate="txtMenuDesc"
                                Text="*" ErrorMessage="Enter Menu Description" ValidationGroup="p"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xs-12" style="padding: 0px; margin-bottom: 5px;">
                        <div class="col-xs-4 col-sm-4" style="padding: 0px;">
                            NavigateUrl
                    
                        </div>
                        <div class="col-xs-6 col-sm-6" style="padding: 0px;">
                            <asp:TextBox ID="txtNavigateURL" CssClass="" runat="server" Style="float: left; width: 90%;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                runat="server" ControlToValidate="txtNavigateURL"
                                Text="*" ErrorMessage="Enter NavigateUrl" ValidationGroup="p"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin: 0px;">
                    <div class="col-sm-6 col-xs-12" style="padding: 0px; margin-bottom: 5px;">
                        <div class="col-xs-4 col-sm-4" style="padding: 0px;">
                            Parent Menu
                        </div>
                        <div class="col-xs-6 col-sm-6" style="float: left; padding: 0px;">
                            <asp:DropDownList ID="ddlMenuParentID" runat="server" OnSelectedIndexChanged="ddlMenuParentID_SelectedIndexChanged" AutoPostBack="True" CssClass="" Style="float: left; width: 90%;">
                            </asp:DropDownList>

                            <asp:DropDownList ID="ddlChildMenu" runat="server" Style="float: left; width: 90%;">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="col-sm-6 col-xs-12" style="padding: 0px; margin-bottom: 5px;">
                        <div class="col-xs-4 col-sm-4" style="padding: 0px;">
                            ModuleID
                        </div>
                        <div class="col-xs-6 col-sm-6" style="padding: 0px;">
                            <asp:TextBox ID="txtModeuleID" CssClass="" runat="server" Style="float: left; width: 90%;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server" ControlToValidate="txtModeuleID"
                                Text="*" ErrorMessage="Enter ModuleID" ValidationGroup="p"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtModeuleID"
                                ErrorMessage="Please Enter Only Numbers" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="p">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>

                <div class="row " style="margin: 0px;">
                    <div class="col-sm-6 col-xs-12" style="padding: 0px; margin-bottom: 5px;">
                        <div class="col-xs-4 col-sm-4" style="padding: 0px;">
                            OrderId
                        </div>
                        <div class="col-xs-6 col-sm-6" style="padding: 0px;">
                            <asp:TextBox ID="txtOrderID" runat="server" CssClass="" Style="float: left; width: 90%;">

                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ControlToValidate="txtOrderID"
                                Text="*" ErrorMessage="Enter OrderID" ValidationGroup="p"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOrderID"
                                ErrorMessage="Please Enter Only Numbers" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="p">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xs-12" style="padding: 0px; margin-bottom: 5px;">
                        <div class="col-xs-4 col-sm-4" style="padding: 0px;">
                            Menu Visible
                        </div>
                        <div class="col-xs-6 col-sm-6" style="padding: 0px;">
                            <asp:DropDownList ID="ddlMenuVisible" runat="server" AppendDataBoundItems="true" CssClass="" Style="float: left; width: 90%;">
                                <asp:ListItem Text="Select value" Value="0" />
                                <asp:ListItem Text="Y" Value="1" />
                                <asp:ListItem Text="N" Value="2" />
                            </asp:DropDownList>
                            <div class="col-xs-1  col-sm-1 col-md-1" style="float: right">
                                <asp:RequiredFieldValidator InitialValue="0" ID="Req_ID"
                                    runat="server" ControlToValidate="ddlMenuVisible"
                                    Text="*" ErrorMessage="Select Item" ValidationGroup="p"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row " style="margin: 0px;">
                    <div class="col-sm-6 col-xs-12" style="padding: 0px; margin-bottom: 5px;">
                        <div class="col-xs-4 col-sm-4" style="padding: 0px;">
                            Is Secure
                        </div>
                        <div class="col-xs-6 col-sm-6" style="padding: 0px;">
                            <asp:DropDownList ID="ddlMenuSecure" runat="server" AppendDataBoundItems="true" CssClass="" Style="float: left; width: 90%;">
                                <asp:ListItem Text="Select value" Value="0" />
                                <asp:ListItem Text="Y" Value="1" />
                                <asp:ListItem Text="N" Value="2" />
                            </asp:DropDownList>
                            <div class="col-xs-1  col-sm-1 col-md-1" style="float: right">
                                <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator6"
                                    runat="server" ControlToValidate="ddlMenuSecure"
                                    Text="*" ErrorMessage="Select Item" ValidationGroup="p"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <asp:Label ID="labdisplay" runat="server"></asp:Label>
                </div>

                <div class=" row" style="margin: 0px;">
                    <div class="col-sm-2 col-xs-4" style="padding: 0px;">
                        UserType
                    </div>
                    <div class="col-sm-10 col-xs-8" style="padding: 0px;">

                        <asp:CheckBoxList ID="chkUserType" runat="server" RepeatDirection="Horizontal" RepeatColumns="10" CssClass="col-xs-12" RepeatLayout="Flow" Style="padding: 0px">
                            <asp:ListItem Text="Admin" Value="1" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                            <asp:ListItem Text="To" Value="2" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                            <asp:ListItem Text="Treasury" Value="3" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                            <asp:ListItem Text="Office" Value="4" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                            <asp:ListItem Text="Department" Value="5" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                            <asp:ListItem Text="Bank" Value="6" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                            <asp:ListItem Text="AG" Value="7" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                            <asp:ListItem Text="Guest" Value="9" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                            <asp:ListItem Text="Register User" Value="10" style="margin-right: 10px; padding: 0px;" class="col-sm-2 col-xs-4"> </asp:ListItem>
                        </asp:CheckBoxList>

                        <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one item."
                            ForeColor="Red" runat="server" ClientValidationFunction="ValidateCheckBoxList" Text="*" ValidationGroup="p" />
                    </div>

                </div>
                <div class="row " style="margin: 0px; text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" Text="submit" OnClick="btnSubmit_Click" ValidationGroup="p" />
                </div>
            </div>

            <div class="row " style="margin: 0px; overflow: auto">
                <asp:GridView ID="gridMenuDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    EmptyDataText="No Record" ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="MenuId"
                    Style="border: 1px solid" CssClass="col-xs-12">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo">
                            <ItemTemplate>
                                <%# ((GridViewRow)Container).RowIndex + 1%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="MenuDesc" HeaderText="Menu Name">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NavigateUrl" HeaderText="URL">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MenuParentId" HeaderText="ParentId">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ModuleId" HeaderText="ModuleId">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OrderId" HeaderText="OrderId">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MenuVisible" HeaderText="Menu Visible">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserType" HeaderText="user Type">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MenuId" HeaderText="Menu Id">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnGvEdit" runat="server" OnClick="btnGvEdit_Click" Text="Edit" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>


                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

