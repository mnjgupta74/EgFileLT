<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgGuestProfile.aspx.cs" Inherits="WebPages_EgGuestProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/EgrasCss.css" rel="Stylesheet" type="text/css" />
    <link href="../js/SweetAlert/sweetalert.css" rel="stylesheet" />
     <script src="../Scripts/chosen.jquery.js"></script>
    <link href="../CSS/chosen.css" rel="stylesheet" />
    <script src="../js/SweetAlert/sweetalert.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
    </script>

    <style type="text/css">
        select {
            height: 25px;
        }

        .chzn-container {
            text-align: left;
            vertical-align: middle;
        }

        .chzn-container-single .chzn-single {
            border-radius: 0px;
            -webkit-border-radius: 0px;
            background-image: none;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function txtDepartmentColourChange() {
            var inputVal = document.getElementById("<%= txtDepartment.ClientID %>").value;
            if (inputVal == "") {
                document.getElementById("<%= txtDepartment.ClientID %>").style.backgroundColor = "#f4faff";
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
     <script type="text/javascript">
        function ShowPopup() {
            
            $("#exampleModal").modal('hide');
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoaded)

        });


        function PageLoaded(sender, args) {
            $("#ctl00_ContentPlaceHolder1_ddlDeptPopup").chosen();
            $("#ctl00_ContentPlaceHolder1_ddlDeptPopup").change(function () {
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/To/EgAddServiceDeptWise.aspx/getServiceList") %>',
                    data: '{"DeptCode":"' + this.value + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        DisplayServiceList(msg);
                    }
                });
            });
            $("#btnSubmitPopup").click(function () {
                var deptcode = $("#ctl00_ContentPlaceHolder1_ddlDeptPopup").val();
                var ServiceId = $("#ctl00_ContentPlaceHolder1_ddlService").val();
                if (deptcode == 0)
                    alert('Select Department');
                if (ServiceId == 0)
                    alert('Select Service');
                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/WebPages/EgHome.aspx/CreateServiceChallan") %>',
                    data: '{"DeptCode":"' + deptcode + '","ServiceId":"' + ServiceId + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        if (msg.d.length > 2) {
                            var returnString = msg.d.split('|', 2);
                            if ($.trim(returnString[0]) != "") {
                                alert(returnString[0]);
                            }
                            else {
                                window.location = returnString[1];
                            }
                        }
                    }
                });
            });

        }
    </script>
  <%--  <script type="text/javascript" src='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
    <link rel="stylesheet" href='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' media="screen" />--%>
    <link href="../js/bootstrap.min.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function DisplayServiceList(msg) {
            if (msg.d.length > 0 && msg.d != "[]") {
                var json = JSON.parse(msg.d);
                $("#ctl00_ContentPlaceHolder1_ddlService").empty();
                $("#ctl00_ContentPlaceHolder1_ddlService").append('<option value=' + '0' + '>' + '--- Select Service ---' + '</option>');
                $.each(json, function (index, obj) {
                    $("#ctl00_ContentPlaceHolder1_ddlService").append('<option value=' + this.ServiceId + '>' + this.ServiceName + '</option>');
                });
            }
            else {
                $("#ctl00_ContentPlaceHolder1_ddlService").empty();
                alert("No Record found");
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
            <table runat="server" style="width: 90%;" id="TABLE1" cellpadding="0" border="1"
                cellspacing="0" align="center">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblSchema" runat="server" Text="Guest Schema" ForeColor="Green" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <marquee bgcolor="yellow" behavior="Slide" direction="left" scrollamount="4"><font size = 4 color = "red">To get smooth services and transaction history create your own User ID on eGRAS instead of using Guest Account.
</font></marquee>
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
                <tr>
                    <%--<td colspan="4" style="background-color: aliceblue">
                        <img src="../Image/new.gif" /><a href="#" data-toggle="modal" data-target="#exampleModal" style="color: cadetblue; font-size: 16px; font-weight: bold; text-align: center;">Create quick Challan using Services</a>
                    </td>--%>
                </tr>
                <tr>
                    <td width="40%">Department:-
                        <asp:DropDownList ID="ddldepartment" runat="server" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"
                            Width="50%" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvdept" runat="server" ControlToValidate="ddldepartment"
                            ErrorMessage="*" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td width="50%">MajorHead:- &nbsp;
                        <asp:DropDownList ID="ddlMajorHeadList" runat="server" OnSelectedIndexChanged="ddlMajorHeadList_SelectedIndexChanged"
                            AutoPostBack="true" Width="50%">
                        </asp:DropDownList>
                        <asp:Button ID="btnMore" runat="server" Text="More Heads..." OnClick="btnMore_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMajorHeadList"
                            InitialValue="0" ErrorMessage="*" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                    </td>
                    <%--<td width="10%">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" Text="Back"></asp:HyperLink>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtDepartment" runat="server" AutoPostBack="true" ForeColor="#408080"
                            BackColor="#c0c0c0" onkeypress="return txtDepartmentColourChange();" ToolTip="You can enter Departments manually here!"
                            OnTextChanged="txtDepartment_TextChanged" onclick="cleardata();" onblur="if(this.value==''){this.value='Search Department By Name Or Code'}"
                            Width="49%" Height="20px">Search Department By Name Or Code</asp:TextBox><br />
                    </td>
                </tr>
            </table>
            <div id="BudgetSchema" runat="server" style="margin-left: 103px; width: 100%;" visible="false">
                <table id="budgetlist" border="1" cellpadding="0" cellspacing="1" style="width: 80%;">
                    <tr>
                        <td width="46%">
                            <asp:ListBox ID="lstbudgethead" runat="server" Width="100%" Height="97px"></asp:ListBox>
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
                            <asp:Label ID="addlabel" runat="server" Text="Add Your Budget Head/Purpose" ForeColor="#339933"></asp:Label>
                            <asp:ListBox ID="lstselectedbudget" runat="server" Width="100%" Height="96px" Style="margin-right: 0px"></asp:ListBox>
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
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" style="position: static; margin-top: 190px; width: 990px" role="document">
                    <div class="modal-content" style="position: initial">
                        <div class="modal-header" style="height: 50px">
                            <h5 class="modal-title col-xs-11" id="exampleModalLabel" style="text-align: center; font-weight: 600; color: #004F00; font-size: 17px;">Select Service</h5>
                            <button type="button" class="close col-xs-1" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body" style="margin-top: 5px;">
                            <div class="row" style="color: #2874f0">
                                <div class="col-md-6" style="padding-right: 0px;">
                                    <span style="font-size: 15px; vertical-align: center;">Department Name :-</span>
                                    <asp:DropDownList ID="ddlDeptPopup" runat="server" Width="200px" Style="height: 33px;" AutoPostBack="false" class="chzn-select">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Department"
                                        ControlToValidate="ddlDeptPopup" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                                        Style="text-align: center">*</asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4" style="padding-right: 0px;">
                                    <span style="font-size: 15px; vertical-align: center;">Service:-</span>
                                    <asp:DropDownList ID="ddlService" runat="server" Style="font-family: Verdana !important; font-size: 13px; height: 24px;"
                                        Width="70%">
                                        <asp:ListItem Value="0" Text="--Select Service--"></asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Service"
                                        ControlToValidate="ddlService" ValidationGroup="vldInsert" InitialValue="0"
                                        ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-2" style="padding-right: 0px; text-align: center;">
                                    <button type="button" class="btn btn-primary" id="btnSubmitPopup" style="height: auto">Submit</button>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer" style="margin-top: 0px; padding: 15px 20px 15px">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal" style="height: auto">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
