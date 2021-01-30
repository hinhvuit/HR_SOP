<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="HR_SOP.Roles" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () {
                ListRoles('', '', '');
            }, 500);

           
            $('#fromRoles').dialog({
                dialogClass: 'my_dialog',
                autoOpen: false,
                modal: true,
                width: 500,
                buttons: {
                    Submit: function () {
                        if ($('#txtCodeRole').val() == '') {
                            $('#txtCodeRole').focus();
                            bootbox.alert("小組編碼不能為空 / Mã nhóm không được phép bỏ trống");
                            return;
                        }
                        else if ($('#txtNameRole').val() == '') {
                            $('#txtNameRole').focus();
                            bootbox.alert("小組名稱不能為空 / Tên không được bỏ trống.");
                            return;
                        }
                        else {

                            var del = '';
                            if ($('#uniform-chkIsDeleted span').attr('class') == 'checked') {

                                del = '1';
                            }
                            else {
                                del = '0';
                            }

                            $.ajax({
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                url: 'Models/WebServiceDB.asmx/InsertOrUpdateRole',
                                data: "{ 'ID': '" + $('#hidID').val() + "', 'Code': '" + $('#txtCodeRole').val() + "', 'Name': '"
                                    + $('#txtNameRole').val() + "', 'IsDeleted': '" + del + "' }",
                                async: true,
                                success: function (data) {
                                    if ($.parseJSON(data.d) == 'ADD') {
                                        ListRoles('', '', '');
                                        ResetRow();
                                        $('#fromRoles').dialog('close');
                                    }
                                    else if ($.parseJSON(data.d) == 'EDIT') {
                                        ListRoles('', '', '');
                                        ResetRow();
                                        $('#fromRoles').dialog('close');
                                    }
                                    else {
                                        bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                                    }
                                },
                                error: function (er) {
                                    bootbox.alert("Error system :-" + er);
                                }
                            });
                        }
                    },
                    Cancel: function () {
                        $(this).dialog('close');
                    }
                }
            });

            $('.my_dialog .ui-button-text:contains(Cancel)').text('拒絕 / Từ chối');
            $('.my_dialog .ui-button-text:contains(Submit)').text('存儲 / Lưu');
        });

        function EditRow(row) {

            $('#tr_' + $(row).attr('value') + ' td').each(function (i) {
                if (i == 0) {
                    $('#hidID').val($(row).attr('value'));
                }
                else if (i == 1) {
                    $('#txtCodeRole').val($(this).html());
                }
                else if (i == 2) {
                    $('#txtNameRole').val($(this).html());
                }
                else if (i == 3) {
                    var ck = '';
                    if ($(this).text() == 'True') {
                        ck = 'checked';
                    }
                    else {
                        ck = '';
                    }
                    $('#uniform-chkIsDeleted span').attr('class', '' + ck + '');
                }

            });
            $('#fromRoles').dialog('open');
        }

        function DeleteRow(row) {
            swal({
                title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
                text: "小組會被刪除 / Nhóm này sẽ bị xóa khỏi hệ thống.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#FF7043",
                confirmButtonText: "同意 / Đồng ý",
                cancelButtonText: '拒絕 / Từ chối',
                showLoaderOnConfirm: true
            },
               function (isConfirm) {
                   if (isConfirm) {
                       $.ajax({
                           type: 'POST',
                           contentType: 'application/json; charset=utf-8',
                           dataType: 'json',
                           url: 'Models/WebServiceDB.asmx/DeletedRole',
                           data: "{ 'ID': '" + $(row).attr('value') + "'}",
                           async: true,
                           success: function (data) {
                               if ($.parseJSON(data.d) == 'SUCCESS') {
                                   ListRoles('', '', '');
                               }
                               else {
                                   bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                               }
                           },
                           error: function (er) {
                               bootbox.alert("Error system :-" + er);
                           }
                       });
                   }
               });
        }

        function ResetRow() {
            $('#hidID').val('');
            $('#txtNameRole').val('');
            $('#uniform-chkIsDeleted span').attr('class', '');
        }

        function GetCode() {
            ResetRow();
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/GetCodeAuto',
                data: "{ 'NameCol': 'Code', 'NameTable': 'Roles' }",
                async: true,
                success: function (data) {
                    $('#txtCodeRole').val($.parseJSON(data.d));
                    $('#fromRoles').dialog('open');
                },
                error: function (er) {
                    bootbox.alert("Error system :-" + er);
                }
            });
        }

        function ListRoles(Code, Name, IsDeleted) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListRoles',
                data: "{ 'Code': '" + Code + "', 'Name': '" + Name + "', 'IsDeleted': '" + IsDeleted + "' }",
                async: true,
                success: function (data) {
                    $('#bodyApp').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {

                        html += '<tr id="tr_' + v.ID + '">';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td>' + v.Code + '</td>';
                        html += '<td>' + v.Name + '</td>';
                        if (v.IsDeleted == true) {
                            html += '<td class="text-center"><span class="label label-danger">True</span></td>';
                        }
                        else {
                            html += '<td class="text-center"><span class="label label-success">False</span></td>';
                        }
                        html += '<td class="text-center">';
                        html += '    <ul class="icons-list">';
                        html += '        <li class="text-primary-600"><a href="#" value="' + v.ID + '" onclick="EditRow(this);" ><i class="icon-pencil7"></i></a></li>';
                        html += '        <li class="text-danger-600"><a href="#" value="' + v.ID + '" onclick="DeleteRow(this);"><i class="icon-trash"></i></a></li>';
                        html += '    </ul>';
                        html += '</td>';
                        html += '</tr>';

                    });
                    $('#bodyApp').append(html);
                },
                error: function (er) {
                    bootbox.alert("Error system :-" + er);
                }
            });
        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">權限組 / DANH SÁCH NHÓM QUYỀN</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>

        <div class="panel-body">
            <table class="table table-bordered">
                <thead>
                    <tr class="bg-info">
                        <th class="text-bold  col-lg-1">STT <br /> 序號</th>
                        <th class="text-bold col-lg-2">Mã <br /> 編號</th>
                        <th class="text-bold col-lg-7">Tên <br /> 名稱</th>
                        <th class="text-bold col-lg-1">Xóa <br /> 刪除</th>
                        <th class="col-lg-1"></th>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <a href="#" class="btn btn-info btn-sm legitRipple" onclick="GetCode();">
                                <i class="icon-add-to-list position-left"></i>新增 / Thêm mới
                            </a>
                        </td>

                    </tr>
                </thead>
                <tbody id="bodyApp"></tbody>
            </table>

        </div>

    </div>

    <div id="fromRoles" title="THÊM MỚI HOẶC CẬP NHẬT NHÓM">

        <div class="form-group">
            <label class="control-label col-sm-3 text-bold">Mã / 編號 <span class="text-danger">(*)</span></label>
            <div class="col-sm-9">
                <input id="hidID" type="hidden" />
                <input id="txtCodeRole" type="text" class="form-control" readonly="readonly">
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-3 text-bold">Tên / 名稱 <span class="text-danger">(*)</span></label>
            <div class="col-sm-9">
                <input id="txtNameRole" type="text" class="form-control" placeholder="Input role name">
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-3 text-bold">Xóa/ 刪除</label>
            <div class="col-sm-9">
                <label class="checkbox-inline">
                    <input id="chkIsDeleted" type="checkbox" class="styled">
                    Xóa / 刪除
                </label>
            </div>
        </div>

    </div>
</asp:Content>
