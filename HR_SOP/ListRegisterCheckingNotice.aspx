﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListRegisterCheckingNotice.aspx.cs" Inherits="HR_SOP.ListRegisterCheckingNotice" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var codeRole = '';
        $(document).ready(function () {
            setTimeout(function () {
                ListRegisterCheckingNotice($('#<%=txtDocumentNo.ClientID%>').val(), $('#<%=txtCreateBy.ClientID%>').val(), $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
            }, 500);
        });

        function ListRegisterCheckingNotice(DocumentNo, CreatedBy, fApplicationDate, tApplicationDate) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListRegisterCheckingNotice',
                data: "{ 'PublishDocument': '', 'CreatedBy': '" + CreatedBy + "','DocumentNo':'" + DocumentNo + "','fApplicationDate': '" + fApplicationDate + "', 'tApplicationDate': '" + tApplicationDate + "' }",
                async: true,
                success: function (data) {
                    $('#bodyApp').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {

                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td> <a href="CheckingNotice.aspx?DocumentNo=' + v.DocumentNo + '">' + v.DocumentNo + '</a></td>';
                        html += '<td>' + v.HoTen + '</td>';
                        html += '<td>' + v.Department_Text + '</td>';
                        html += '<td>' + v.States_Text + '</td>';
                        html += '<td>' + v.ApplicationDate_Text + '</td>';
                        html += '<td>' + v.EffectiveDate_Text + '</td>';
                        html += '</tr>';

                    });
                    $('#bodyApp').append(html);
                },
                error: function (er) {
                    console.log('err');
                }
            });
        }

        function SearchListRegisterCheckingNotice() {
            ListRegisterCheckingNotice($('#<%=txtDocumentNo.ClientID%>').val(), $('#<%=txtCreateBy.ClientID%>').val(),
                $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">SOP 檢查單 / ĐƠN KIỂM TRA SOP</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>
        <div class="panel-body">
            <div class="row">

                

                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Từ ngày <br /> 從日:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtFromApplicationDate" runat="server" CssClass="form-control pickadate-format"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Đến ngày <br /> 到日:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtToApplicationDate" runat="server" CssClass="form-control pickadate-format"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Mã văn kiện <br /> 文件編碼:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Người tạo <br /> 申請人:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple"
                                onclick="SearchListRegisterCheckingNotice();">尋找 / Tìm kiếm <i class="glyphicon glyphicon-search position-right"></i></a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="table-responsive pre-scrollable">
                                <table class="table table-bordered">
                                    <thead>
                                        <tr class="bg-info">
                                            <th class="text-bold  col-lg-1">Order</th>
                                            <th class="text-bold col-lg-2">Mã văn kiện <br /> 文件編碼 </th>
                                            <th class="text-bold col-lg-2">Người làm đơn <br />申請人</th>
                                            <th class="text-bold col-lg-2">Bộ phận <br /> 部門 </th>
                                            <th class="text-bold col-lg-1">Tình trạng <br /> 狀況</th>
                                            <th class="text-bold col-lg-2">Ngày tạo <br /> 申請日</th>
                                            <th class="text-bold col-lg-2">Phát hành <br /> 發行 </th>
                                        </tr>
                                    </thead>
                                    <tbody id="bodyApp">
                                        <tr>
                                            <td colspan="8" class="text-center">
                                                <label id="spinner-light-8">
                                                    <i class="icon-spinner11 spinner position-left"></i>Loading...
                                                </label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>




