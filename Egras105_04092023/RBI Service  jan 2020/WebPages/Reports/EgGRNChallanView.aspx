<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgGRNChallanView.aspx.cs" Inherits="WebPages_Reports_EgGRNChallanView"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/EgEchallan.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">

        function validate(field) {
            var valid = "0123456789"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("Invalid entry! Only numbers are accepted!");
                field.value = "";
                field.focus();
                field.select();
            }
        }
    </script>

    <link href="../../CSS/PageHeader.css" rel="stylesheet" />


     <%-- Radio button ServerSide --%>
    <style>
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

        .btn-default{
                color: #f4f4f4;
                background-color: #428bca;
        }  

        input[name="ctl00$ContentPlaceHolder1$rblSearchType"] + label {
            cursor: pointer;
            min-width: 150px;
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

        /*label {
            border: 2px solid cornflowerblue;
            display: inline-block;
            padding: 5px;
            position: relative;
            text-align: center;
            transition: background 600ms ease, color 600ms ease;
        }*/

        /*input[name="ctl00$ContentPlaceHolder1$rblTransaction"] {
            display: none;
        }


            input[name="ctl00$ContentPlaceHolder1$rblTransaction"] + label {
                cursor: pointer;
                min-width: 150px;
            }

                input[name="ctl00$ContentPlaceHolder1$rblTransaction"] + label:hover {
                    background: none;
                    color: #1a1a1a;
                }

                input[name="ctl00$ContentPlaceHolder1$rblTransaction"] + label:after {
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



        input[id="ctl00_ContentPlaceHolder1_rblTransaction_0"] + label {
            border-right: 0;
        }

        input[name="ctl00_ContentPlaceHolder1_rblTransaction_0"] + label:after {
            left: 100%;
        }

        input[id="ctl00_ContentPlaceHolder1_rblTransaction_1"] + label {
            margin-left: -5px;
        }

            input[id="ctl00_ContentPlaceHolder1_rblTransaction_1"] + label:after {
                left: -100%;
            }

        input[name="ctl00$ContentPlaceHolder1$rblTransaction"]:checked + label {
            cursor: default;
            color: #fff;
            transition: color 200ms;
        }

            input[name="ctl00$ContentPlaceHolder1$rblTransaction"]:checked + label:after {
                left: 0;
            }*/
    </style>
    <%-- Radio button ServerSide end --%>


    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../App_Themes/Images/progress.gif" />
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
            <div id="divChallanView">
                <div _ngcontent-c6="" class="tnHead minus2point5per">
                    <h2 _ngcontent-c6="" title="E-CHALLAN VIEW" class="pull-left">
                        <span _ngcontent-c6="" style="color: #FFF">E-CHALLAN VIEW</span></h2>
                    <img src="../../Image/help1.png" class="pull-right" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="right" title="E-CHALLAN VIEW" />
                </div>
                <table runat="server" style="width: 100%; text-align: left" id="TABLE2"
                    border="1" align="center">

                    <tr>
                        <td align="center">
                            <asp:RadioButtonList ID="rblSearchType" runat="server" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rblSearchType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="1" Selected="True">GRN</asp:ListItem>
                                <asp:ListItem Value="2">Challan No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>

                    <tr style="text-align: center">
                        <td style="height: 35px" valign="top">
                            <asp:Label ID="lblGRN" runat="server" Height="100%" style="color: #336699" Width="120px" Text="GRN :"></asp:Label>
                            <asp:TextBox ID="txtGRN" runat="server" style="font-size:Small;height:30px;width:120px;"
                                     OnChange="validate(this);" MaxLength="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcGRN" runat="server" ErrorMessage="*" ControlToValidate="txtGRN" ValidationGroup="VldEnter"></asp:RequiredFieldValidator>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="txtbtn" runat="server" Height="33" Width="12%" CssClass="btn btn-default" Text="Challan View" OnClick="txtbtn_Click"
                                ValidationGroup="VldEnter" />
                        </td>
                    </tr>
                    <tr style="text-align: center">
                        <td style="height: 35px" valign="top">
                            <asp:Label ID="lblMsg" runat="server" style="color: #336699" Text="GRN :"></asp:Label>
                            <table style="background-color: #333333; font-weight: normal;"
                                cellspacing="2" cellpadding="2" width="100%" align="center" border="1">
                                <asp:Repeater ID="rptChallanFill" runat="server" OnItemCommand="rptChallanFill_ItemCommand">
                                    <HeaderTemplate>
                                        <tr style="background-color: #336699; color: White;">
                                            <td align="center">S.No
                                            </td>
                                            <td align="center">GRN
                                            </td>
                                            <td align="center">Amount
                                            </td>
                                            <td align="center" style="width: 100px;">Expected Date
                                            </td>
                                            <td align="center" style="width: 100px;">Scroll Date
                                            </td>
                                            <td align="center">View
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: white; height: 20px; font-family: Verdana; font-size: 10pt;">
                                            <td align="center">
                                                <asp:Label ID="Labelrow" runat="server" Text='<%#Eval("row")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="LabelGRN" runat="server" Text='<%#Eval("GRN")%>'></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# string.Format("{0:0.00}", Eval("Amount"))%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <%#DataBinder.Eval(Container, "DataItem.Expected_Date", "{0:d/M/yyyy}")%>
                                            </td>
                                            <td align="center">
                                                <%#DataBinder.Eval(Container, "DataItem.ScrollDate", "{0:d/M/yyyy}")%>
                                            </td>

                                            <td align="center">
                                                <asp:ImageButton ID="ImageViewbtn" runat="server" ImageUrl="~/Image/view.png" CommandName="View"
                                                    Width="60px" Height="20" ToolTip="View Details" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
