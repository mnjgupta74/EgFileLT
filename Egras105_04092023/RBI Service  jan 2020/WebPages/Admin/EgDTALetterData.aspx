<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgDTALetterData.aspx.cs" Inherits="WebPages_Admin_EgDTALetterData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../js/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <style>
        /*CSS FOR TOP HEADER STARTS*/

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
        /*TOPHEADER CSS ENDS HERE*/

        .btn {
        height:auto;
        }
        label {
        float:left;
        }
          .mandatory {
            color: #ff0000;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('.Numeric').bind('keydown', function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            })
        });
        
    </script>
    <div class="">
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h1 _ngcontent-c6="" title="DtaLetter">
                <span _ngcontent-c6="" style="color: #FFF">DTA Letter Upload</span></h1>
            <%--<img src="../../Image/help1.png" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="left" title="" />--%>
        </div>
        <span style="float:right"><span class="mandatory">*</span> :- Mandatory Fields</span>
        <div id="InputSection">
            <div class="row text-center">
                <div id="DivFile">
                    
                    <div class="col-sm-4 col-md-3">
                        <label>Subject<span class="mandatory">*</span>:</label>
                        <input id="txtsub" runat="server" type="text" class="form-control" maxlength="50" required/>
                    </div>
                    <div class="col-sm-4 col-md-3">
                        <label>File Url<span class="mandatory">*</span>:</label>
                        <asp:FileUpload runat="server" ID="fileDTA" CssClass="form-control" />
                        <asp:RegularExpressionValidator ID="regpdf" Text="*" ErrorMessage="please upload only pdf files" ControlToValidate="fileDTA" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" />
                    </div>
                    <div class="col-sm-4 col-md-3">
                        <label>Serial No<span class="mandatory">*</span>:</label>
                        <input id="txtSno" runat="server" class="form-control Numeric"  type="text" maxlength="7" required/>
                    </div>
                </div>
            </div>
            <div class="row text-center">
                <div id="DivDesc">
                    <div class="col-sm-4 col-md-3">
                        <label>Status:</label>
                        <select id="ddlStatus" class="form-control" runat="server">
                            <option value="P">Pending</option>
                            <option value="S">Success</option>
                            <option value="F">Fail</option>
                            <option value="C">Closed</option>
                        </select>
                    </div>
                    <div class="col-sm-4 col-md-3">
                        <label>Remarks<span class="mandatory">*</span>:</label>
                        <input id="txtremarks" maxlength="150" type="text" class="form-control" runat="server" required/>
                    </div>
                    <div class="col-sm-3 text-center" style="margin-top: 25px;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSubmit_Click" />
                    </div>
                </div>


            </div>

        </div>


    </div>
</asp:Content>

