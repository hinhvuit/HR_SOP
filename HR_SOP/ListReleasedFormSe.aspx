<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListReleasedFormSe.aspx.cs" Inherits="HR_SOP.ListReleasedFormSe" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var codeRole = '';
        $(document).ready(function () {
           ListPublishReff($('#<%=txtFormNo.ClientID%>').val(), $('#<%=txtFormName.ClientID%>').val(), $('#<%=ddlDepartment.ClientID%>').val(),
                $('#<%=txtFromDate.ClientID%>').val(), $('#<%=txtToDate.ClientID%>').val());
        });

        function ListPublishReff(FormNo, FormName, PreservingDepartment, sFromDate, sToDate) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListPublishReffSe',
                data: "{ 'PublishDocument': '', 'FormNo': '" + FormNo + "','FormName':'" + FormName + "','PreservingDepartment': '" + PreservingDepartment
                    + "','sFromDate': '" + sFromDate + "', 'sToDate': '" + sToDate + "' }",
                async: true,
                success: function (data) {
                    $('#bodyAppPublishReff').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {

                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td>' + v.FormNo + '</td>';
                        html += '<td>' + v.FormName + '</td>';
                        html += '<td>' + v.DocumentNo + '</td>';
                        html += '<td>' + v.DocumentName + '</td>';
                        html += '<td>' + v.DepartmentName + '</td>';
                        html += '<td>' + v.PreservingTime_Text + '</td>';
                        html += '<td><a href="javascript:void(0)" value="' + v.Attachment + '" onclick="ShowPublishReff(this);">' + v.Attachment + '</a></td>';
                        html += '</tr>';

                    });
                    $('#bodyAppPublishReff').append(html);
                },
                error: function (er) {
                    console.log('err');
                }
            });
        }

        function SearchReleasedForm() {
            ListPublishReff($('#<%=txtFormNo.ClientID%>').val(), $('#<%=txtFormName.ClientID%>').val(), $('#<%=ddlDepartment.ClientID%>').val(),
                $('#<%=txtFromDate.ClientID%>').val(), $('#<%=txtToDate.ClientID%>').val());
        }

        function ShowPublishReff(row) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckExistsFilePublishReffSe',
                data: "{ 'FileName': '" + $(row).attr('value').trim() + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'EXISTED') {
                        var url = $(location).attr('protocol') + '//' + $(location).attr('host') + '/Updatafile/PublishDoc/Refferent/' + $(row).attr('value').trim();
                        window.open(url);
                    }
                    else {
                        bootbox.alert("File không tồn tại");
                    }
                },
                error: function (er) {
                    bootbox.alert("Error");
                }
            });
        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">附件表單清單 / DANH SÁCH PHỤ LỤC ĐÍNH KÈM</h5>
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
                        <label class="control-label text-bold col-md-3">Mã biểu đơn
                            <br />
                            表單編號:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtFormNo" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Tên biểu đơn
                            <br />
                            表單名稱:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtFormName" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">BP lưu trữ
                            <br />
                            保管單位:</label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddlDepartment" runat="server" data-placeholder="Chọn bộ phận lưu trữ..." class="select-size-lg" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3"> Từ ngày <br /> 從日:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control pickadate-format border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Đến ngày <br /> 到日:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control pickadate-format border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple"
                                onclick="SearchReleasedForm();">尋找 / Tìm kiếm <i class="glyphicon glyphicon-search position-right"></i></a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-bordered">
                                <thead>
                                    <tr class="bg-info">
                                        <th class="text-bold  col-lg-1">STT <br /> 序號</th>
                                        <th class="text-bold col-lg-1">Mã biểu đơn <br /> 表單編號</th>
                                        <th class="text-bold col-lg-2">Tên biểu đơn <br /> 表單名稱</th>
                                        <th class="text-bold col-lg-1">Mã văn bản <br /> 文件編號</th>
                                        <th class="text-bold col-lg-3">Tên văn bản <br /> 文件名稱</th>
                                        <th class="text-bold col-lg-2">Bộ phận lưu trữ <br /> 保管單位</th>
                                        <th class="text-bold col-lg-1">Thời gian lưu trữ <br /> 保存期限</th>
                                        <th class="text-bold col-lg-1">File đính kèm <br /> 文件附檔</th>
                                    </tr>
                                </thead>
                                <tbody id="bodyAppPublishReff">
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
