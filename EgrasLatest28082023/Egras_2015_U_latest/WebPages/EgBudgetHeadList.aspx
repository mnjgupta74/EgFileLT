<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgBudgetHeadList.aspx.cs" Inherits="WebPages_EgBudgetHeadList" Title="Egras.Rajasthan.gov.in" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/moment.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <link href="../CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../js/CDNFiles/buttons.print.min.js"></script>
    <script src="../js/CDNFiles/dataTables.buttons.min.js"></script>
    <script src="../js/CDNFiles/buttons.html5.min.js"></script>
    
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #337ab7;
            height: auto;
            border-radius: inherit;
        }

        .dt-buttons {
            display: none;
        }

        #ajaxloader {
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

        #ajaxloader::after {
            content: '';
            display: block;
            position: absolute;
            left: 48%;
            top: 40%;
            width: 70px;
            height: 70px;
            border-style: solid;
            border-color: #1b2a47;
            border-top-color: lightcyan;
            border-width: 10px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }

        .dataTables_wrapper .dataTables_paginate {
            background-color: #1b2a47;
        }
    </style>
    <style type="text/css">
        .sectiontopheader[_ngcontent-c6] {
            border-top: 5px solid #337ab7;
            margin-bottom: 5px;
            display: -webkit-box;
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
    <style type="text/css">
        .sectiontopheader[_ngcontent-c6] {
            border-top: 5px solid #337ab7;
            margin-bottom: 5px;
            display: -webkit-box;
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

        .dataTables_wrapper .dataTables_paginate {
            background-color: #428bca;
        }

        .dataTables_wrapper {
            padding: 1%;
        }
    </style>

    <script>

        $(document).ready(function () {
            
            var userid = '<%= Session["UserId"]%>';
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/WebPages/EgBudgetHeadList.aspx/GetDepartment") %>',
                data: '{"userid":"' + userid + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (data) {
                    var ddldepartment = $("[id*=ddldepartment]");
                    ddldepartment.empty();
                    $('#ddldepartment').append('<option value="0">--- Select Department ---</option>');
                    $.each(JSON.parse(data.d), function (data, value) {
                        ddldepartment.append($("<option></option>").val(value.DeptCode).html(value.deptnameEnglish));
                    })
                },
                error: function (error) {
                    alert(error.toString());
                }
            })
            $("#btnSubmit").click(function (e) {

                $("#TblChllanHistory").hide();
                $(".dtBudgetHeadList").hide();

                e.preventDefault();
                $('#ajaxloader').show();
                var deptcode = $('#ddldepartment').val();
                if (deptcode != "0") {
                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("~/WebPages/EgBudgetHeadList.aspx/FillBudgetHeadList") %>',
                        data: '{"deptcode":"' + deptcode + '"}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: "json",
                        success: function (data) {
                            if (data.d != '[]') {
                                $("#TblBudgetHeadList").show();
                                $(".dtBudgetHeadList").show();
                                var datatableVariable = $('#TblBudgetHeadList').DataTable({
                                    dom: 'Bfrtip',
                                    data: JSON.parse(data.d),
                                    columns: [
                                   {
                                       "data": null, "sortable": false, "title": "S.No",
                                       "render": function (data, type, row, meta) {
                                           {
                                               return meta.row + meta.settings._iDisplayStart + 1;
                                           }
                                       }
                                   },
                                        {
                                            "data": "schemaname", "title": "SchemaName", "render": function (data, type, row, meta) {
                                                if (type === 'display') {
                                                    data = '<span style="color:black">' + data + '</span>';
                                                }
                                                return data;
                                            }
                                        }],
                                    "paging": true,
                                    "ordering": true,
                                    "searching": true,
                                    "destroy": true
                                });
                            }
                            else {
                                $(".dtChllanHistory").hide();
                                $("#TblBudgetHeadList").hide();
                                alert('No record found !');
                                $('#ajaxloader').hide();
                            }
                            $('#ajaxloader').hide();
                        },
                        error: function (error) {
                            $('#ajaxloader').hide();
                            alert(error.toString());
                        }
                    })
                }
                else {
                    alert('Select Department');
                    $('#ajaxloader').hide();
                }
            });
        });
    </script>


    <div class="">
        <div _ngcontent-c6="" class="tnHead minus2point5per">
            <h2 _ngcontent-c6="" title="Track Budget Head Schema" class="pull-left">
                <span _ngcontent-c6="" style="color: #FFF">Track Budget Head Schema</span></h2>
            <img src="../Image/help1.png" class="pull-right" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="left" title="" />
        </div>
        <table width="100%" style="text-align: center" align="center" border="1">
            <tr>
                <td align="left">
                    <b><span style="color: #336699">Department </span></b>&nbsp;
                <select id="ddldepartment" class="selectpicker form-control" style="width: 80%; display: inline-table">
                    <option value="0">--- Select Department ---</option>
                </select>
                </td>
                <td align="center">
                    <button id="btnSubmit" type="submit" class="btn btn-default">Submit</button>
                </td>
            </tr>
        </table>
    </div>
    <div class="row dtBudgetHeadList">
        <table id="TblBudgetHeadList" cellspacing="0" style="display: none; background-color: #428bca; color: white; text-align: left" border="1">
            <tbody style="color: black">
            </tbody>
            <tfoot>
                <tr>
                    <th style="text-align: center"></th>
                    <th style="text-align: left"></th>
                </tr>
            </tfoot>
        </table>
    </div>
    <div id="ajaxloader">
    </div>
</asp:Content>
