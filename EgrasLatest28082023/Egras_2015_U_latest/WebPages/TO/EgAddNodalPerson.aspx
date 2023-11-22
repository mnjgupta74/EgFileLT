<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAddNodalPerson.aspx.cs" Inherits="WebPages_TO_EgAddNodalPerson" Title="Egras.Rajasthan.gov.in" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/EgrasCss.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function txtDepartmentColourChange() {
            var inputVal = document.getElementById("<%= txtDepartment.ClientID %>").value;
            if (inputVal == "") {
                //                document.getElementById("<%= txtDepartment.ClientID %>").style.backgroundColor = "#c0c0c0";
                document.getElementById("<%= txtDepartment.ClientID %>").style.fontSize = "large";
            }

        }
        function cleardata() {
            document.getElementById("<%= txtDepartment.ClientID %>").value = "";

        }
    </script>

    <script type="text/javascript">
        function NumberOnly(evt) {

            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 3 && parts[1].length >= 3) return false;
        }
        function NumberCheck(field) {
            var valid = "1234567890"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("  Only Numeric value Allowed !");
                field.focus();
                field.select();
                field.value = "";
            }
        }
        function ChangeCase(elem) {

            elem.value = elem.value.toUpperCase();
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
            <fieldset runat="server" id="lstrecord" style="width: 1000px;">
                <legend style="color: #336699; font-weight: bold">Nodel Officer Detail</legend>
                <table style="width: 900px" align="center" id="MainTable">
                    <tr style="height: 35px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Department :- </span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDepartment" runat="server" AutoPostBack="true" ForeColor="#408080"
                                OnTextChanged="txtDepartment_TextChanged" onkeypress="return txtDepartmentColourChange();"
                                ToolTip="You can enter Departments manually here!" BackColor="#c0c0c0" onclick="cleardata();"
                                onblur="if(this.value==''){this.value='Search Department By Name Or Code'}" Width="49%"
                                Height="20px">Search Department By Name Or Code</asp:TextBox><br />
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="330px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ControlToValidate="ddlDepartment" ValidationGroup="vldInsert" InitialValue="0"
                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="divwidth">
                            </div>
                            <ajaxToolKit:AutoCompleteExtender ID="txtDepartment_AutoCompleteExtender" EnableCaching="true"
                                MinimumPrefixLength="1" CompletionInterval="1000" runat="server" DelimiterCharacters=""
                                Enabled="True" ServicePath="" CompletionSetCount="5" ServiceMethod="GetDepartmentList"
                                TargetControlID="txtDepartment" CompletionListElementID="divwidth" CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                            </ajaxToolKit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Officer Contact Name :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNodelName" runat="server" Width="250px" MaxLength="50" onblur="ChangeCase(this);"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ControlToValidate="txtNodelName" ValidationGroup="vldInsert" ForeColor="Red"
                                Style="text-align: center">*</asp:RequiredFieldValidator>
                            &nbsp;<asp:RegularExpressionValidator ID="RfvNodelName" runat="server" Text="Special character and Numbers not allowed in Name.!"
                                ErrorMessage="Special character and Numbers not allowed in Name.!" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtNodelName" ValidationExpression="^[a-zA-Z ]+$"
                                ValidationGroup="vldInsert"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Contact Number :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumber" runat="server" Width="150px" MaxLength="11" OnChange="NumberCheck(this);"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                ControlToValidate="txtNumber" ValidationGroup="vldInsert" ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                            <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNumber"
                                Text="Enter Correct Mob No." ErrorMessage="Enter Correct Mob No." ValidationGroup="vldInsert"
                                ForeColor="Red" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>--%>
                            <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                TargetControlID="txtNumber" WatermarkText="Number 1">
                            </ajaxToolKit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Alternate Contact Number :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumber2" runat="server" Width="150px" MaxLength="11" OnChange="NumberCheck(this);"></asp:TextBox>
                            <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                TargetControlID="txtNumber2" WatermarkText="Number 2">
                            </ajaxToolKit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                EmailID :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                ControlToValidate="txtEmail" ValidationGroup="vldInsert" ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpForemail" runat="server" ControlToValidate="txtEmail"
                                Text="Enter Correct EmailId" ErrorMessage="Enter Correct EmailId" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Address :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="250px" MaxLength="200"
                                onblur="ChangeCase(this);" Height="45px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                ControlToValidate="txtAddress" ValidationGroup="vldInsert" ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAddress"
                                Text="Special character not allowed in address.!" ErrorMessage="Special character not allowed in address.!"
                                ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td colspan="2" align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vldInsert"
                                OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
                <table style="width: 900px" align="center">
                    <tr>
                        <td>
                            <asp:GridView ID="gridNodal" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                EmptyDataText="No Record" ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="Nodalid">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo">
                                        <ItemTemplate>
                                            <%# ((GridViewRow)Container).RowIndex + 1%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="DeptName" HeaderText="DepartmentName" Visible="false" />--%>
                                    <asp:BoundField DataField="NodalName" HeaderText="Nodal Name">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Number" HeaderText="Contact Number">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmailID" HeaderText="EmailID">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Address" HeaderText="Address">
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
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGvDelete" runat="server" OnClick="btnGvDelete_Click" Text="Delete" />
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
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
