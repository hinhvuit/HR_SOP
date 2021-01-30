﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListRegisterRenewalsDocument.aspx.cs" Inherits="HR_SOP.ListRegisterRenewalsDocument" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
       
        $(document).ready(function () {
            setTimeout(function () {
                ListRegisterRenewalDocument($('#<%=txtApplicationNO.ClientID%>').val(), $('#<%=txtDocumentNO.ClientID%>').val(), $('#<%=ddlType.ClientID%>').val());
            }, 500);
        });

        function ListRegisterRenewalDocument(Code, DCC, Type) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListRegisterRenewalDocument',
                data: "{ 'Code': '" + Code + "', 'DCC': '" + DCC + "','Type':'" + Type + "' }",
                async: true,
                success: function (data) {
                    $('#bodyApp').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {
                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td> <a href="RenewalsDocument.aspx?Code=' + v.Code + '&DCC=' + v.DCC + '&Type=' + v.Type + '">' + v.Code + '</a></td>';
                        html += '<td>' + v.DCC + '</td>';
                        html += '<td>' + v.Revised + '</td>';
                        html += '<td>' + v.Revisor + '</td>';
                        html += '<td>' + v.CloseDate + '</td>';
                        html += '</tr>';
                    });
                    $('#bodyApp').append(html);
                },
                error: function (er) {
                    console.log('err');
                }
            });
        }

        function SearchListRegisterRenewalDocument() {
            ListRegisterRenewalDocument($('#<%=txtApplicationNO.ClientID%>').val(), $('#<%=txtDocumentNO.ClientID%>').val(), $('#<%=ddlType.ClientID%>').val());
        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">文件延期登記 / ĐĂNG KÝ GIA HẠN VĂN KIỆN</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>
        <div class="panel-body">
            <div class="row">

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Mã đơn <br /> 表單編碼:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtApplicationNO" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Mã văn bản <br /> 文件編碼:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDocumentNO" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Loại văn kiện <br />文件類型:</label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="select-size-lg">
                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                <asp:ListItem Value="CODE">Đăng ký Mã văn bản / 程序規範編號申請單</asp:ListItem>
                                <asp:ListItem Value="EDIT">Đăng ký sửa văn kiện / 文件修改登記</asp:ListItem>
                                <asp:ListItem Value="CHECK">Đơn kiểm tra SOP / SOP 檢查單</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple"
                                onclick="SearchListRegisterRenewalDocument();">尋找 / Tìm kiếm <i class="glyphicon glyphicon-search position-right"></i></a>
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
                                            <th class="text-bold  col-lg-1">STT <br /> 序號</th>
                                            <th class="text-bold col-lg-2">Mã đơn <br /> 表單編碼</th>
                                            <th class="text-bold col-lg-2">Mã văn bản <br /> 文件編碼</th>
                                            <th class="text-bold col-lg-2">Người sửa <br /> 修改人</th>
                                            <th class="text-bold col-lg-3">Người duyệt lại <br />批准人</th>
                                            <th class="text-bold col-lg-2">Ngày hết hạn <br />到期日</th>
                                        </tr>
                                    </thead>
                                    <tbody id="bodyApp">
                                        <tr>
                                            <td colspan="6" class="text-center">
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




