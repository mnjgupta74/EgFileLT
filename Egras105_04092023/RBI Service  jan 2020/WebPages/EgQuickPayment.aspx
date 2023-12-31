<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgQuickPayment.aspx.cs" Inherits="WebPages_EgQuickPayment" Title="EgQuickPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/EgrasCss.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="../js/SweetAlert/sweetalert.css" rel="stylesheet" />
    <script src="../js/SweetAlert/sweetalert.min.js"></script>
      <link href="../CSS/PageHeader.css" rel="stylesheet" />
    <style>
        .sweet-alert {
            left: 45%;
        }
        input[type=text] {
        width:auto;
        height:23px;
        }
    </style>
    <script type="text/javascript">
      
        var msg = "Dear Remitter, " + "\n \n" + "\
                   " + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + "Guest login has been closed.  \
                     Please register yourself." + " \n\n" + "\
                   " + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + " Guest  Login   बंद हो गया है ।  । कृपया अपना रजिस्टर लॉगिन बनाये।";
        // msg
        var head = "";
        function GuestPopUp()
        {
            swal(head, msg);
        }
        function AlphNumericOnly(el) {
            var ex = /^[a-z\d\-_\s]+$/i;
            if (ex.test(el.value) == false) {
                alert('special characters not allowed');
                el.value = "";
            }
        }

   
    </script>

    <script type="text/javascript" language="javascript">

        // Addded by Rachit on 31 jan 2014 for clearing data in textbox and changing

        function txtDepartmentColourChange() {

            // The color of Dept's on keypress

            var inputVal = document.getElementById("<%= txtAutoCompleteDepartments.ClientID %>").value;
            if (inputVal == "") {
                document.getElementById("<%= txtAutoCompleteDepartments.ClientID %>").style.fontSize = "large";
            }
        }

        function cleardata() {
            document.getElementById("<%= txtAutoCompleteDepartments.ClientID %>").value = "";
        }
     
    </script>

    <script type="text/javascript" language="javascript">
      
        function ShowDivAllPurpose() {
            $('#<%=rblDepartments.ClientID %>').each(function() {
                var checked = $(this).find('input:radio:checked');
                if (checked.length > 0) {
                    document.getElementById("<%=DivAllPurpose.ClientID %>").style.display = 'block';

                    HidePaymentSection();
                }
            });
        }
       
        function HidePaymentSection() {
            //            document.getElementById("<%=trPayment.ClientID %>").style.display = '';
            document.getElementById("<%=trPurposeList.ClientID %>").style.display = '';
        }
        
        $(document).ready(function() {
            $("#BtnSearch").click(function(){
                $("#slidediv").show("fast");
          
            });
        });
    </script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../Image/waiting_process.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail">
            <span _ngcontent-c6="" style="color: #FFF">Quick Payment</span></h2>
        <img src="../Image/help1.png" style="height: 44px; width: 34px;"  title="Quick Payment" />
    </div>
            <table runat="server" width="100%" border="1" align="center">
                <tr>
                    <td colspan="3" style="height: 20px; background-color: #1C92D3; color: White" align="center"> 
                        <asp:LinkButton ID="commonHeads" runat="server" Style="color: Yellow; font-weight: bold; margin-left:45%">Common Heads</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rblSearchCriteria" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" Width="450px" TabIndex="1" OnSelectedIndexChanged="rblSearchCriteria_SelectedIndexChanged">
                            <asp:ListItem Value="D">Department Search</asp:ListItem>
                            <asp:ListItem Value="P">Purpose Search</asp:ListItem>
                            <asp:ListItem Value="B">BudgetHead Search</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="rblSearchCriteria"
                            ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                    </td>
                    <td align="center" id="tdTxt">
                        <asp:TextBox ID="txtSearch" runat="server" TabIndex="2" MaxLength="100"
                            onChange="AlphNumericOnly(this);">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                            ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtAutoCompleteDepartments" runat="server" ToolTip="You can enter Departments manually here!"
                            onkeypress="return txtDepartmentColourChange();" AutoPostBack="true" MaxLength="100"
                            ForeColor="#408080" onblur="if(this.value==''){this.value='Search Department By Name Or Code'}"
                            onclick="cleardata();" Height="22px" Width="290px"></asp:TextBox>
                        <div id="divwidth">
                        </div>
                        <ajaxToolkit:AutoCompleteExtender ID="txtAutoCompleteDepartments_AutoCompleteExtender"
                            EnableCaching="true" MinimumPrefixLength="1" CompletionInterval="1000" CompletionSetCount="5"
                            runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" ServiceMethod="GetDeptList"
                            CompletionListElementID="divwidth" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtAutoCompleteDepartments">
                        </ajaxToolkit:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAutoCompleteDepartments"
                            ValidationGroup="vldInsert">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="center" id="btntxtSearch">
                        <asp:Button ID="BtnSearch" runat="server" Text="Search" Width="80px" ValidationGroup="vldInsert"
                            TabIndex="3" OnClick="BtnSearch_Click" />
                    </td>
                </tr>
                <tr id="trDept" runat="server" visible="false">
                    <td id="tdrblDept" runat="server" width="50%" style="text-align:center" valign="top">
                        <asp:Label ID="lblDept" runat="server" Text="Department List" ForeColor="#1c92d3"
                            Font-Bold="true" Font-Size="Small"></asp:Label>
                        <div runat="server" style="overflow: auto; text-align: left;">
                            <asp:RadioButtonList ID="rblDepartments" runat="server" RepeatDirection="Vertical"
                                Width="100%" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="rblDepartments_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </div>
                    </td>
                    <td id="tdDept" runat="server" colspan="2" width="50%" align="center">
                        <div id="DivFilterSelect" runat="server" style="text-align: center;">
                        </div>
                        <div id="DivAllPurpose" runat="server" style="display: none" align="center">
                            <asp:Label ID="lblMajorHead" runat="server" Text="Major Head" ForeColor="#1c92d3"
                                Font-Bold="true" Font-Size="Small"></asp:Label>
                            <br />
                            <div runat="server" style="overflow: auto; text-align: left;">
                                <asp:RadioButtonList ID="rblMajorhead" runat="server" RepeatDirection="Vertical"
                                    AutoPostBack="true" Enabled="true" TabIndex="7" Width="100%" OnSelectedIndexChanged="rblMajorhead_SelectedIndexChanged">
                                </asp:RadioButtonList>
                            </div>
                            
                        </div>
                    </td>
                </tr>
                <%--<tr id="trselectedList"  style="display:none; width: 100%">
                <td colspan="3" runat="server" align="center" style="width: 100%; border: Solid 1px black;"
                        valign="top">
                
                <div id="divselectedList" runat="server" style=" overflow: auto; width: 100%;
                            text-align: left">
                           
                            <asp:ListBox ID="lstselectedList" Width="100%"  ForeColor="green" SelectionMode= "Multiple" runat="server"></asp:ListBox>
                        </div></td>
                </tr>--%>
                <tr id="trPurposeList" style="display: none; width: 100%">
                    <td colspan="3" runat="server" align="center" style="width: 100%; border: Solid 1px black;"
                        valign="top">
                        <asp:Label ID="lblPurpose" runat="server" Text="Purpose/BudgetHead List" ForeColor="#1c92d3"
                            Font-Bold="true" Font-Size="Small"></asp:Label>
                        <div id="DivPurpose" runat="server" style="height: 250px; overflow: auto; width: 100%; text-align: left">
                            <%--<asp:CheckBoxList ID="chklstPurposeHeads" runat="server" TabIndex="11" RepeatDirection="Vertical"
                                Width="100%" AppendDataBoundItems="True">
                            </asp:CheckBoxList>--%>
                            <asp:ListBox ID="lstPurposeHeads" Width="100%" Height="250px" SelectionMode="Multiple" runat="server"></asp:ListBox>
                        </div>
                    </td>
                </tr>
                <tr id="trPayment" style="display: none; width: 100%">
                    <td colspan="3" align="center" runat="server" style="width: 100%; border: Solid 1px black;">
                        <asp:Button ID="btnPayment" runat="server" Text="Payment" Width="80px" TabIndex="12"
                            Visible="false" />
                    </td>
                </tr>
            </table>
            <ajaxToolkit:ModalPopupExtender PopupControlID="divCommonHeads" TargetControlID="commonHeads" CancelControlID="close"
                runat="server" ID="popCommonHeads">
            </ajaxToolkit:ModalPopupExtender>

            <div id="divCommonHeads" style="display: none; z-index: 999; background-color: #A8E1B7;">
                <%--   <iframe id="ifmCommonHead">--%>
                <table style="border: solid 1px black" rules="rows" width="400px">
                    <thead align="center" style="background-color: #1C92D3">
                        <tr>
                            <td style="color: White">Common Heads
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>0049 -Receipt of intrest
                            </td>
                        </tr>
                        <tr>
                            <td>0070 -Other Administrative Services
                            </td>
                        </tr>
                        <tr>
                            <td>0075 -Mis. Gen. Services
                            </td>
                        </tr>

                        <tr>
                            <td>1475 -अन्‍य सामान्‍य आर्थिक सेवाएं
                            </td>
                        </tr>

                        <tr>
                            <td>8443 -civil deposit
                            </td>
                        </tr>
                        <%-- <tr>
                            <td>
                                2071 -पेंशन तथा अन्य सेवानिवृत्ति हितलाभ
                            </td>
                        </tr>--%>
                        <%--  <tr>
                            <td>
                                2235 -सामाजिक सुरक्षा तथा कल्याण
                            </td>
                        </tr>--%>
                        <%-- <tr>
                            <td>
                                0210 -चिकित्‍सा एवं लोक स्‍वास्‍थ्‍य
                            </td>
                        </tr>--%>

                        <%-- <tr>
                            <td>
                                8782 -Remittance
                            </td>
                        </tr>--%>
                        <%-- <tr>
                            <td>
                                0220 -सूचना तथा प्रचार
                            </td>
                        </tr>--%>
                        <%--  <tr>
                            <td>
                                8793 -अंतर्राज्यीय उचंत लेखा
                            </td>
                        </tr>--%>
                        <%-- <tr>
                            <td>
                                0070 -other administrative services
                            </td>
                        </tr>--%>
                        <%-- <tr>
                            <td>
                                0202 -शिक्षा, खेलकूद, कला तथा संस्कृति
                            </td>
                        </tr>--%>

                        <asp:ImageButton runat="server" ID="close" ImageUrl="~/Image/cancel-icone-9292-16.png" />
                    </tbody>
                </table>
                <%-- </iframe>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
