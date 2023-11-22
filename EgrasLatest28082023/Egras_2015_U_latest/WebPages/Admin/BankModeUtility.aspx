<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="BankModeUtility.aspx.cs" Inherits="WebPages_Admin_BankModeUtility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />

   
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/jquery.dataTables.min.js"></script>
    <link href="../../CSS/jquery.dataTables.min.css" rel="stylesheet" />
   
    <style>
        .btn {
            height: auto !important;
            border-radius: 0px !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            
            onloadcall();
            function onloadcall() {
              
                $('#InfoSection').hide();
                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/admin/BankModeUtility.aspx/GetData") %>',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        if (data.d != '[]') {
                            $("#TblBankList").show();
                            var datatableVariable = $('#TblBankList').DataTable({
                                data: JSON.parse(data.d),
                                "columnDefs": [{
                                    "defaultContent": "-",
                                    "targets": "_all"
                                }],
                                columns: [

                                    {
                                        "data": "BankName", "title": "BankName", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + data + '</span>';
                                            }
                                            return data;
                                        }

                                    },
                                    {
                                        "data": "BranchName", "title": "BranchName", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + data + '</span>';
                                            }
                                            return data;
                                        }
                                    },
                                    {
                                        "data": "BSRcode", "title": "BSRcode", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + data + '</span>';
                                            }
                                            return data;
                                        }

                                    },
                                    {
                                        "data": "TreasuryCode", "title": "Treasury", "render": function (data, type, row, meta) {
                                            if (type === 'display') {
                                                data = '<span style="color:black">' + data + '</span>';
                                            }
                                            return data;
                                        }

                                    },
                                     //{
                                     //    "data": "MICRCode", "title": "MICRCode", "render": function (data, type, row, meta) {
                                     //        if (type === 'display') {
                                     //            data = '<span style="color:black">' + data + '</span>';
                                     //        }
                                     //        return data;
                                     //    }
                                     //},
                                     {
                                         "data": "BSRcode", "title": "Edit", "render": function (data, type, row, meta) {
                                             if (type === 'display') {
                                                 if (data != null) {
                                                     data = '<i class="glyphicon glyphicon-edit" data-bsrcode="' + data + '" data-bankname="' + row.BankName + '" data-branchname="' + row.BranchName + '" data-ifsc="' + row.IFSC + '" data-micrcode="' + row.MICRCode + '" data-rbicode="' + row.RBICode + '" data-acno="' + row.acno + '" data-chequeprint="' + row.ChequePrint + '" data-bankbranchcode="' + row.BankBranchCode + '" data-address="' + row.Address + '" data-banktype="' + row.BankType + '" data-treasurycode="' + row.TreasuryCode + '"    style="font-size:20px;font-weight:600;color:black"></i>';
                                                 }
                                                 else {
                                                     data = '<span></span>';
                                                 }
                                             }
                                             return data;
                                         }

                                     },

                                ],
                                "paging": true,
                                "ordering": true,
                                //"info": false,
                                "searching": true,
                                "destroy": true,

                            });
                        }
                        else {
                            $("#TblBankList").hide();
                            alert('No record found !');
                        }
                        $('#ajaxloader').hide();
                    },
                    error: function (error) {
                        $('#ajaxloader').hide();
                        alert(error.toString());
                    }
                })
            }
            $("#btnSubmit").click(function (e) {
                e.preventDefault();
                
                var Mode = $('#ddlMode').val();
                var TreasuryCode = $('#hdnTreasuryCode').html();
                var BSRCode = $('#LblBSRCode').html();
                var BankName = $('#LblBankName').html();
                var BranchName = $('#LblBranchName').html();
                var IFSC = $('#LblIFSCCode').html();
                var MICRCode = $('#LblMICRCode').html();
                var Address = $('#LblAddress').html();
                var BankType = $('#LblBankType').html();
                var BankBranchCode = $('#hdnBankBranchCode').html();
                var acno = $('#hdnacno').html();
                var RBICode = $('#hdnRBICode').html();
                var ChequePrint = $('#hdnChequePrint').html();
                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/WebPages/admin/BankModeUtility.aspx/InsertData") %>',
                    data: '{"BSRCode":"' + BSRCode + '","Mode":"' + Mode + '","TreasuryCode":"' + TreasuryCode + '","BankName":"' + BankName + '","BranchName":"' + BranchName + '","IFSC":"' + IFSC + '","MICRCode":"' + MICRCode + '","Address":"' + Address + '","BankType":"' + BankType + '","BankBranchCode":"' + BankBranchCode + '","acno":"' + acno + '","RBICode":"' + RBICode + '","ChequePrint":"' + ChequePrint + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        alert('Bank added successfully !');
                        onloadcall();
                    },
                    error: function (error) {
                        alert(error.toString());
                    }
                })
            });

            $('#TblBankList').on('click', 'i.glyphicon-edit', function () {
                $('#InfoSection').show();
                //if (confirm("Are you sure you want to update this record ?")) {
                //alert("Are you sure to delete this record from bunch data ?");
                LblBankName.innerHTML = $(this).data('bankname');
                LblBranchName.innerHTML = $(this).data('branchname');
                LblBSRCode.innerHTML = $(this).data('bsrcode');
                LblIFSCCode.innerHTML = $(this).data('ifsc');
                LblMICRCode.innerHTML = $(this).data('micrcode');
                LblAddress.innerHTML = $(this).data('address');
                LblBankType.innerHTML = $(this).data('banktype');
                hdnBankBranchCode.innerHTML = $(this).data('bankbranchcode');
                hdnacno.innerHTML = $(this).data('acno');
                hdnRBICode.innerHTML = $(this).data('rbicode');
                hdnChequePrint.innerHTML = $(this).data('chequeprint');
                hdnTreasuryCode.innerHTML = $(this).data('treasurycode');
            });
        });
    </script>

    <div class="container col-md-12">
        <div id="InfoSection">
            <div class="row">
                <div class="col-md-4">
                    <label>Bank Name:</label>
                    <label id="LblBankName"></label>
                </div>
                <div class="col-md-4">
                    <label>BranchName:</label>
                    <label id="LblBranchName"></label>
                </div>
                <div class="col-md-4">
                    <label>BSRCode:</label>
                    <label id="LblBSRCode"></label>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <label>IFSCCode:</label>
                    <label id="LblIFSCCode"></label>
                </div>
                <div class="col-md-4">
                    <label>MICRCode:</label>
                    <label id="LblMICRCode"></label>
                </div>
                <div class="col-md-4">
                    <label>Address:</label>
                    <label id="LblAddress"></label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label>BankType:</label>
                    <label id="LblBankType">Bank:</label>
                </div>
                <div class="col-md-4">
                    <label>Mode:</label>
                    <select id="ddlMode">
                        <option value="1" selected="selected">All</option>
                        <option value="0">PD</option>
                        <option value="-1">Anywhere</option>
                    </select>
                </div>
                <div class="col-md-4">
                    <button id="btnSubmit" type="submit" class="btn btn-primary">Update</button>
                </div>
            </div>
            <label id="hdnBankBranchCode" style="visibility:hidden"></label>
            <label id="hdnacno" style="visibility:hidden"></label>
            <label id="hdnRBICode" style="visibility:hidden"></label>
            <label id="hdnChequePrint" style="visibility:hidden"></label>
            <label id="hdnTreasuryCode" style="visibility:hidden"></label>
        </div>
        <div id="DataSection" class="row dtBankList">
            <table id="TblBankList" cellspacing="0" style="background-color: #1b2a47; color: white; text-align: center" border="1">
                <tfoot>
                    <tr>
                        <th style="text-align: center"></th>
                        <th style="text-align: center"></th>
                        <th style="text-align: center"></th>
                        <th style="text-align: center"></th>
                        <th style="text-align: center"></th>
                        <%--<th style="text-align: center"></th>--%>
                        <%--<th style="text-align: center"></th>--%>
                        <%--<th style="text-align: center"></th>--%>
                    </tr>
                </tfoot>
            </table>
        </div>
        <div id="ajaxloader">
        </div>
    </div>
</asp:Content>

