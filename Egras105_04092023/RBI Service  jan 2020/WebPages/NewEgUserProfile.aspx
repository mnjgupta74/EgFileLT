<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="NewEgUserProfile.aspx.cs" Inherits="WebPages_NewEgUserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/EgrasCss.css" rel="Stylesheet" type="text/css" />
    <link href="../js/SweetAlert/sweetalert.css" rel="stylesheet" />
    <script  type="text/javascript" src="../js/SweetAlert/sweetalert.min.js"></script>
    <script type="text/javascript"  src="../js/CDNFiles/moment.min.js"></script>
    <script type="text/javascript"  src="../js/CDNFiles/jquery.mask.js"></script>
    <style>
        .searchTextBox {
            background-position: 10px 12px;
            background-repeat: no-repeat;
            font-size: 16px;
            padding: 3px 20px 3px 40px;
            border: 1px solid #ddd;
            margin-bottom: 2px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtBudgetHead").mask("9999-99-999-99-99");
            $('#<%= rblSearchType.ClientID %>')
            $(document).ready(function () {
                $('#<%= rblSearchType.ClientID %>').click(function () {
                    var rblTypeOfBusiness = $('#<%=rblSearchType.ClientID %> input:checked').val();
                    ShowHide(rblTypeOfBusiness);
                });
            });
            function ShowHide(rblTypeOfBusiness) {

                if (rblTypeOfBusiness == 1) {
                    document.getElementById("<%= DivBudgetText.ClientID %>").style.display = "none";
                    document.getElementById("<%= DivText.ClientID %>").style.display = "block";
                    document.getElementById("#txtBudgetHead").value = "9999-99-999-99-99";
                }
                else {
                    
                    document.getElementById("<%= DivBudgetText.ClientID %>").style.display = "block";
                    document.getElementById("<%= DivText.ClientID %>").style.display = "none";
                    document.getElementById("<%= txtSearchWithText.ClientID %>").value = "";
                }
            }
            var keys = [];
            var values = [];
            var options = $("#<% = lstbudgethead.ClientID %> option");
            $.each(options, function (index, item) {
                keys.push(item.value);
                values.push(item.innerHTML);
            });
            $("#<% = txtSearchWithText.ClientID %>").keyup(function () {
                var filter = $(this).val();
                DoCompanyNameSearch($("#<% = lstbudgethead.ClientID %>"), filter, keys, values);
            });
            $("#txtBudgetHead").keyup(function () {
                var filter = $(this).val();
                DoCompanyNameSearch($("#<% = lstbudgethead.ClientID %>"), filter, keys, values);
            });

            function DoCompanyNameSearch(listBoxSelector, filter, keys, values) {
                var list = $(listBoxSelector);
                var selectBase = '<option value="{0}">{1}</option>';
                list.empty();
                for (i = 0; i < values.length; ++i) {
                    var value = values[i];
                    if (value == "" || value.toLowerCase().indexOf(filter.toLowerCase()) >= 0) {

                        var temp = '<option value="' + keys[i] + '">' + value + '</option>';
                        list.append(temp);
                    }
                }
            }
        });

    </script>


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

        @-webkit-keyframes spin {
            from {
                -webkit-transform: rotate(0deg);
            }

            to {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
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

        input[name="ctl00$ContentPlaceHolder1$rblSearchType"] {
            display: none;
        }

        label {
            border: 2px solid cornflowerblue;
            display: inline-block;
            padding: 5px;
            position: relative;
            text-align: center;
            transition: background 600ms ease, color 600ms ease;
        }

        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }

        input[name="ctl00$ContentPlaceHolder1$rblSearchType"] + label {
            cursor: pointer;
            min-width: 60px;
        }

            input[name="ctl00$ContentPlaceHolder1$rblSearchType"] + label:hover {
                background: none;
                color: #1a1a1a;
            }

            input[name="ctl00$ContentPlaceHolder1$rblSearchType"] + label:after {
                background: cornflowerblue;
                content: "";
                height: 100%;
                position: absolute;
                top: 0;
                transition: left 200ms cubic-bezier(0.77, 0, 0.175, 1);
                width: 100%;
                z-index: -1;
                left: 100%;
            }

        label {
            margin-bottom: 2px;
        }

        input[id="ctl00_ContentPlaceHolder1_rblSearchType_0"] + label {
            border-right: 0;
            padding-left: 26px;
        }

        input[name="ctl00_ContentPlaceHolder1_rblSearchType_0"] + label:after {
            left: 100%;
        }

        input[id="ctl00_ContentPlaceHolder1_rblSearchType_1"] + label {
            margin-left: -5px;
        }

            input[id="ctl00_ContentPlaceHolder1_rblSearchType_1"] + label:after {
                left: -100%;
            }

        input[name="ctl00$ContentPlaceHolder1$rblSearchType"]:checked + label {
            cursor: default;
            color: #fff;
            transition: color 200ms;
        }

            input[name="ctl00$ContentPlaceHolder1$rblSearchType"]:checked + label:after {
                left: 0;
            }
    </style>

    <div id="cover-spin"></div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnSubmit"]').click(function () {
            <%--if ($(<%=txtfromdate.ClientID%>).val()!= "" && $(<%=txttodate.ClientID%>).val()!= "")
            {--%>
                $('#cover-spin').show(0)

                //}
            });
        });
    

    <%--==================CSS-JQUERY LOADER  END==================--%>
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

        function myAlert(heading, mycontent) {
            swal(heading, mycontent);
        }
    </script>

    <script type="text/javascript" language="javascript">
        function Warning() {
            var re = confirm("Do you want to Delete This Record?");
            return re;
        }
        function validate(field) {
            var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 "
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("Invalid Profile Name!  Only characters and numbers are accepted!");
                field.focus();
                field.select();
                field.value = "";
            }
        }

    </script>

<%--    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
        <ContentTemplate>--%>
            <%--Header Table--%>
            <table runat="server" style="width: 90%;" id="TABLE1" cellpadding="0" border="1"
                cellspacing="0" align="center">
                <tr>
                    <td align="center" style="width: 890px" colspan="2">
                        <asp:Label ID="lblSchema" runat="server" Text="Payee Profile" ForeColor="Green" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div id="divwidth">
                        </div>
                        <ajaxToolkit:AutoCompleteExtender ID="txtDepartment_AutoCompleteExtender" EnableCaching="true"
                            MinimumPrefixLength="1" CompletionInterval="1000" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" CompletionSetCount="5" ServiceMethod="GetCountries"
                            TargetControlID="txtDepartment" CompletionListElementID="divwidth" CompletionListCssClass="AutoExtender"
                            CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                </tr>
                <tr style="width: 890px">
                    <td style="width: 50%">
                        <b>Departments :-</b> &nbsp;
                        <asp:DropDownList ID="ddldepartment" runat="server" CssClass="chzn-select" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"
                            AutoPostBack="True" Width="60%" Height="20px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddldepartment"
                            InitialValue="0" ErrorMessage="Select Department" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 50%">
                        <b>MajorHead :-</b> &nbsp;
                        <asp:DropDownList ID="ddlMajorHeadList" runat="server" OnSelectedIndexChanged="ddlMajorHeadList_SelectedIndexChanged"
                            AutoPostBack="true" Width="50%" Height="20px">
                        </asp:DropDownList>
                        <asp:Button ID="btnMore" runat="server" Text="More Heads" OnClick="btnMore_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMajorHeadList"
                            InitialValue="0" ErrorMessage="Select MajorHead" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtDepartment" runat="server" AutoPostBack="true" ForeColor="#408080"
                            onkeypress="return txtDepartmentColourChange();" ToolTip="You can enter Departments manually here!"
                            BackColor="#c0c0c0" OnTextChanged="txtDepartment_TextChanged" onclick="cleardata();"
                            onblur="if(this.value==''){this.value='Search Department By Name Or Code'}" Width="49%"
                            Height="20px">Search Department By Name Or Code</asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 890px" colspan="2">
                        <b>Profile Name :-</b> &nbsp;<%--OnChange="validate(this);"--%>
                        <asp:TextBox ID="txtProfileName" runat="server" MaxLength="30" Height="18px" Width="29%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvProfilename" runat="server" ControlToValidate="txtProfileName"
                            ErrorMessage="Enter ProfileName" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtProfileName"
                            Text="Special character not allowed.!" CssClass="XMMessage" ErrorMessage="Special character not allowed.!"
                            ValidationExpression="^([a-zA-Z0-9_.\s\-, ]*)$" ValidationGroup="a" ForeColor="Red"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
            <div id="BudgetSchema" runat="server" style="margin-left: 50px; width: 100%;" visible="false">
                <table id="budgetlist" border="1" cellpadding="0" cellspacing="1" style="width: 90%;">
                    <tr>
                        <td width="46%">                   
                    	<table id="tblSearch" runat="server" visible="true">
                        <tr>
                            <td align="center">
                                <asp:RadioButtonList runat="server" ID="rblSearchType" RepeatDirection="Horizontal" ForeColor="#336699"
                                    Font-Bold="true">
                                    <asp:ListItem Text="Text" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="BudgetHead" Value="2" Selected="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <div id="DivText"  runat="server">
                                    <asp:TextBox ID="txtSearchWithText" runat="server" placeholder="Search Text" Width="70%" CssClass="searchTextBox"></asp:TextBox>
                                </div>
                                <div id="DivBudgetText" style="display: none" runat="server">
                                    <input class="txtBudgetHead" id="txtBudgetHead" type="text" onfocus="this.value=''" value="9999-99-999-99-99"/>
                                    <%--<asp:TextBox ID="txtBudgetHead" runat="server" MaxLength="13" onblur="javascript:CheckMajorHeadlength()" Width="70%" 
                                        CssClass="searchTextBox"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="9999-99-999-99-99" 
                                        MaskType="None" CultureName="en-US" TargetControlID="txtBudgetHead" AcceptNegative="None"
                                        runat="server">
                                    </ajaxToolkit:MaskedEditExtender>--%>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:ListBox ID="lstbudgethead" runat="server" Width="100%" Height="97px" class="chosen-select"></asp:ListBox>
                </td>
                        <td width="8%" style="text-align: center;">
                            <asp:Button ID="btnnext" runat="server" Text="&gt;&gt;" OnClick="btnnext_Click" />
                            &nbsp;&nbsp;
                            <br />
                            <br />
                            <asp:Button ID="btnprev" runat="server" OnClick="btnprev_Click" Text="&lt;&lt;" />
                            &nbsp;
                        </td>
                        <td width="46%">
                            <asp:Label ID="addlabel" runat="server" Text="Add Your Budget head" ForeColor="#339933"></asp:Label>
                            <asp:ListBox ID="lstselectedbudget" runat="server" Width="98%" Height="96px" Style="margin-right: 0px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                ValidationGroup="a" />
                            <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" />
                            <input type="hidden" id="listBox1Values" name="listBox1Values" runat="server" />
                            <input type="hidden" id="listBox2Values" name="listBox2Values" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                </table>
            </div>
            <table runat="server" style="width: 90%;" id="TABLE2" cellpadding="0" border="1"
                cellspacing="0" align="center">
                <tr>
                    <td align="center">
                        <asp:Repeater ID="rptuserprofile" runat="server" OnItemCommand="rptuserprofile_ItemCommand"
                            OnItemDataBound="rptuserprofile_ItemDataBound">
                            <HeaderTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr style="background-color: #5F8DC9;">
                                        <td align="left" width="122px" height="25px">
                                            <b>Profile</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblEmpty" Style="text-align: center" runat="server" Text="No Record Found"
                                    runat="server" Font-Bold="true" ForeColor="Teal" Visible='<%#bool.Parse((rptuserprofile.Items.Count==0).ToString())%>'></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left" width="122px" height="25px">
                                            <%-- <asp:HyperLink ID="hp1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Profile") %>'>
                                            </asp:HyperLink>--%>
                                            <%# DataBinder.Eval(Container.DataItem,"Profile")%>
                                        </td>
                                        <td align="center" width="100px" height="25px">
                                            <asp:LinkButton ID="lnkedit" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UserPro") + "/" + DataBinder.Eval(Container.DataItem,"Profile")  %> '>Edit</asp:LinkButton>
                                        </td>
                                        <td align="center" width="100px" height="25px">
                                            <asp:LinkButton ID="lnkDetail" runat="server" CommandName="Ac" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"UserPro") %>'> 
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Flag")%>' Visible="false" Display="Dynamic" ></asp:Label>
                                                 <%# Eval("Flag").ToString() == "Y" ? "<img src='../Image/active.png' title='Make Hide' border='0' style='width: 8%'/>" : "<img src='../Image/deactive.png' title='Make Show' border='0' style='width: 8%'/>"%>                                           
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr style="background-color: #E1E9F2;">
                                        <td align="left" width="122px" height="25px">
                                            <%-- <asp:HyperLink ID="hp1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Profile") %>'>
                                            </asp:HyperLink>--%>
                                            <%# DataBinder.Eval(Container.DataItem,"Profile")%>
                                        </td>
                                        <td align="center" width="100px" height="25px">
                                            <asp:LinkButton ID="lnkedit" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UserPro")  + "/" + DataBinder.Eval(Container.DataItem,"Profile")  %> '>Edit</asp:LinkButton>
                                        </td>
                                        <td align="center" width="100px" height="25px">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Flag")%>' Visible="false" Display="Dynamic" ></asp:Label>
                                            <asp:LinkButton ID="lnkDetail" runat="server" CommandName="Ac" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"UserPro") %>'>
                                        <%# Eval("Flag").ToString() == "Y" ? "<img src='../Image/active.png' title='Make Hide' border='0' style='width: 8%'/>" : "<img src='../Image/deactive.png' title='Make Show' border='0' style='width: 8%'/>"%>
                                                 <%--Text='<%# Eval("Flag").ToString() == "Y" ? "Active" : "De Activate"  %>'--%>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
