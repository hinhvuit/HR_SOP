<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="HR_SOP.Users" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .fix {
            width: 19px !important;
            height: 19px !important;
            margin: 0px !important;
        }
    </style>

    <script type="text/javascript">

        var codeRole = '';
        var ListDepartment = '';
        var ListPosition = '';
        $(document).ready(function () {
            setTimeout(function () {
                ListUsers('', '', '', '', '', '', '0');
            }, 500);


            $('#modal_theme_save_user').dialog({
                dialogClass: 'my_dialog',
                autoOpen: false,
                modal: true,
                width: 960,
                buttons: {
                    Submit: function () {
                        if ($('#<%=txtUserName.ClientID%>').val() == '') {
                            $('#<%=txtUserName.ClientID%>').focus();
                            bootbox.alert("賬款不能為空 / Tên đăng nhập không được phép bỏ trống");
                            return;
                        }
                        else if ($('#<%=txtPassword_User.ClientID%>').val() == '') {
                            $('#<%=txtPassword_User.ClientID%>').focus();
                            bootbox.alert("密碼不能為空 / Mật khẩu không được phép bỏ trống");
                            return;
                        }
                        else if ($('#txtFullName').val() == '') {
                            $('#txtFullName').focus();
                            bootbox.alert("姓名不能為空 / Họ tên không được phép bỏ trống");
                            return;
                        }
                        else if ($('#<%=txtEmail.ClientID%>').val() == '') {
                            $('#<%=txtEmail.ClientID%>').focus();
                            bootbox.alert("郵箱不能為空 / Email không được phép bỏ trống");
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

                            var dcc = '';
                            if ($('#uniform-chkIsDCC span').attr('class') == 'checked') {

                                dcc = '1';
                            }
                            else {
                                dcc = '0';
                            }

                            $.ajax({
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                url: 'Models/WebServiceDB.asmx/InsertOrUpdateUser',
                                data: "{ 'ID': '" + $('#hidID').val() + "', 'TenDangNhap': '" + $('#<%=txtUserName.ClientID%>').val() + "', 'MatKhau': '"
                                    + $('#<%=txtPassword_User.ClientID%>').val() + "','HoTen':'" + $('#txtFullName').val() + "','ChucVu':'"
                                    + ListPosition + "','PhongBan':'" + ListDepartment + "', 'IsDeleted': '" + del + "', 'Email': '"
                                    + $('#<%=txtEmail.ClientID%>').val() + "', 'SoDienThoai': '" + $('#<%=txtPhonenumber.ClientID%>').val()
                                    + "','DCC':'" + dcc + "','Manager_Room':'" + $('#cboManager_room').val() + "'}",
                                async: true,
                                success: function (data) {
                                    if ($.parseJSON(data.d) == 'ADD') {
                                        ListUsers('', '', '', '', '', '', '0');
                                        ResetRow();
                                        $('#modal_theme_save_user').dialog('close');
                                    }
                                    else if ($.parseJSON(data.d) == 'EDIT') {
                                        ListUsers('', '', '', '', '', '', '0');
                                        ResetRow();
                                        $('#modal_theme_save_user').dialog('close');
                                    }
                                    else if ($.parseJSON(data.d) == 'EXIST') {
                                        $('#<%=txtUserName.ClientID%>').focus();
                                        swal({
                                            title: "通報 / Thông báo",
                                            text: "賬款已存在請登入別的賬款 / Tài khoản đã tồn tại vui lòng nhập tài khoản khác",
                                            confirmButtonColor: "#66BB6A",
                                            type: "success"
                                        });
                                    }
                                    else {
                                        bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                                    }
                                },
                                error: function (er) {
                                    bootbox.alert('Error system : -' + er);
                                }
                            });
                        }
                    },
                    Cancel: function () {
                       $(this).dialog('close');
                    }
                }
            });


            $('#modal_theme_users_roles').dialog({
                dialogClass: 'my_dialog',
                autoOpen: false,
                modal: true,
                width: 550,
                buttons: {
                    Submit: function () {
                        $.ajax({
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            url: 'Models/WebServiceDB.asmx/InsertUsersInRoles',
                            data: "{ 'UserName': '" + $('#txtUser').val() + "', 'CodeRole': '" + codeRole + "' }",
                            async: true,
                            success: function (data) {
                                if ($.parseJSON(data.d) == 'SUCCESS') {
                                    $('#modal_theme_users_roles').dialog('close');
                                }
                                else {
                                    bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                                }
                            },
                            error: function (er) {
                                bootbox.alert('Error system : -' + er);
                            }
                        });
                    },
                    Cancel: function () {
                        $(this).dialog('close');
                    }
                }
            });

            $('.my_dialog .ui-button-text:contains(Cancel)').text('拒絕 / Từ chối');
            $('.my_dialog .ui-button-text:contains(Submit)').text('存儲 / Lưu');

        });

        function GetManagerRoom(Username) {
            $('.select2-selection__choice').remove();
         
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListUserManagerRoom',
        data: "{ 'Username': '" + Username + "'}",
        async: true,
        success: function (data) {
            $('#cboManager_room').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                if (v.Options == '1') {
                    html += '<option value="' + v.TenDangNhap + '" selected="selected">' + v.HoTen + '</option>';
                    var user = '<li class="select2-selection__choice" title="' + v.HoTen + '"><span class="select2-selection__choice__remove" role="presentation">×</span>' + v.HoTen + '</li>';
                    $('.select2-selection__rendered').append(user);
                }
                else {
                    html += '<option value="' + v.TenDangNhap + '">' + v.HoTen + '</option>';
                }
            });
            $('#cboManager_room').append(html);
            

        },
        error: function (er) {
            bootbox.alert('Error system : -' + er);
        }
    });
}


function AddRow() {
    $('#modal_theme_save_user').dialog('open');
    $('#<%=txtUserName.ClientID%>').removeAttr('disabled');
    $('#<%=txtPassword_User.ClientID%>').removeAttr('disabled');
    ResetRow();
    GetCheckPosition('');
    setTimeout(function () {
        GetCheckDepartment('');
        setTimeout(function () {
            GetManagerRoom('');
        }, 300);
    }, 300);
}

function EditRow(row) {
    
    $('#<%=txtUserName.ClientID%>').attr('disabled', 'disabled');
    $('#<%=txtPassword_User.ClientID%>').val('12345678');
    $('#<%=txtPassword_User.ClientID%>').attr('disabled', 'disabled');
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListUsers',
        data: "{ 'TenDangNhap': '" + $(row).attr('value') + "', 'HoTen': '','SoDienThoai':'','Email':'','ChucVu':'','PhongBan' :'', 'IsDeleted': '' }",
        async: true,
        success: function (data) {
            $.each($.parseJSON(data.d), function (i, v) {
                $('#hidID').val(v.ID);
                $('#<%=txtUserName.ClientID%>').val(v.TenDangNhap);
                $('#txtFullName').val(v.HoTen);
                $('#<%=txtEmail.ClientID%>').val(v.Email);
                $('#<%=txtPhonenumber.ClientID%>').val(v.SoDienThoai);
                GetCheckPosition(v.TenDangNhap);
                setTimeout(function () {
                    GetCheckDepartment(v.TenDangNhap);
                    setTimeout(function () {
                        GetManagerRoom(v.TenDangNhap);
                    }, 300);
                }, 300);

                var ck = '';
                if (v.IsDeleted == true) {
                    ck = 'checked';
                }
                else {
                    ck = '';
                }
                $('#uniform-chkIsDeleted span').attr('class', '' + ck + '');


                if (v.DCC == true) {
                    ck = 'checked';
                }
                else {
                    ck = '';
                }
                $('#uniform-chkIsDCC span').attr('class', '' + ck + '');

            });
            $('#modal_theme_save_user').dialog('open');
        },
        error: function (er) {
            bootbox.alert('Error system : -' + er);
        }
    });

}



function GetRole(row) {
    var ck = $(row).is(':checked');
    var code = $(row).attr('value') + ',';
    if (ck == true) {
        codeRole += code;
    }
    else {
        codeRole = codeRole.replace(code, '');
    }
}

function GetPosition(row) {
    var ck = $(row).is(':checked');
    var code = $(row).attr('value') + ',';
    if (ck == true) {
        ListPosition += code;
    }
    else {
        ListPosition = ListPosition.replace(code, '');
    }
}

function GetDepartment(row) {
    var ck = $(row).is(':checked');
    var code = $(row).attr('value') + ',';
    if (ck == true) {
        ListDepartment += code;
    }
    else {
        ListDepartment = ListDepartment.replace(code, '');
    }
}

function EditUserOrRole(row) {
    $('#txtUser').val($(row).attr('value'));
    codeRole = '';
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListRolesCheck',
        data: "{ 'UserName': '" + $(row).attr('value') + "'}",
        async: true,
        success: function (data) {
            codeRole = '';
            $('#chkRoles').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                html += '<div class="col-sm-6">';
                if (v.Checked == '1') {
                    codeRole += v.Code + ',';
                    html += '<input id="chk_' + v.Code + '" type="checkbox" class="control-label fix" onclick="GetRole(this);" checked="checked" value="' + v.Code + '" />' + v.Name + '';
                }
                else {
                    html += '<input id="chk_' + v.Code + '" type="checkbox" class="control-label fix" onclick="GetRole(this);" value="' + v.Code + '" />' + v.Name + '';
                }

                html += '</div>';
            });
            $('#chkRoles').append(html);

            $('#modal_theme_users_roles').dialog('open');
        },
        error: function (er) {
            bootbox.alert('Error system : -' + er);
        }
    });
}

function GetCheckPosition(userName) {
    ListPosition = '';
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListPositionInUser',
        data: "{ 'UserName': '" + userName + "'}",
        async: true,
        success: function (data) {
            ListPosition = '';
            $('#chkPosition').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                html += '<div class="col-sm-4">';
                html += '   <div class="form-group">';
                html += '     <label class="checkbox-inline">';
                if (v.Checked == '1') {
                    ListPosition += v.CatCode + ',';
                    html += '<input id="chk_' + v.CatCode + '" type="checkbox" class="control-label fix" onclick="GetPosition(this);" checked="checked" value="' + v.CatCode + '" />' + v.CatName + '';
                }
                else {
                    html += '<input id="chk_' + v.CatCode + '" type="checkbox" class="control-label fix" onclick="GetPosition(this);" value="' + v.CatCode + '" />' + v.CatName + '';
                }
                html += '       </div>';
                html += '   </div>';
                html += '</div>';
            });
            $('#chkPosition').append(html);
        },
        error: function (er) {
            bootbox.alert('Error system : -' + er);
        }
    });
}

function GetCheckDepartment(userName) {
    ListDepartment = '';
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListDepartmentInUser',
        data: "{ 'UserName': '" + userName + "'}",
        async: true,
        success: function (data) {
            ListDepartment = '';
            $('#chkDepartment').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                html += '<div class="col-sm-4">';
                html += '   <div class="form-group">';
                html += '     <label class="checkbox-inline">';
                if (v.Checked == '1') {
                    ListDepartment += v.CatCode + ',';
                    html += '<input id="chk_' + v.CatCode + '" type="checkbox" class="control-label fix" onclick="GetDepartment(this);" checked="checked" value="' + v.CatCode + '" />' + v.CatName + '';
                }
                else {
                    html += '<input id="chk_' + v.CatCode + '" type="checkbox" class="control-label fix" onclick="GetDepartment(this);" value="' + v.CatCode + '" />' + v.CatName + '';
                }
                html += '       </div>';
                html += '   </div>';
                html += '</div>';
            });
            $('#chkDepartment').append(html);
        },
        error: function (er) {
            bootbox.alert('Error system : -' + er);
        }
    });
}

function ResetPassword(row) {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "新密碼 / Mật khẩu mới là: foxconn168!!",
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
                    url: 'Models/WebServiceDB.asmx/ResetPassword',
                    data: "{ 'ID': '" + $(row).attr('value') + "', 'MatKhau': 'foxconn168!!' }",
                    async: true,
                    success: function (data) {
                        if ($.parseJSON(data.d) == 'ERROR') {
                            bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                        }
                    },
                    error: function (er) {
                        bootbox.alert('Error system : -' + er);
                    }
                });
            }
        });
}

function DeleteRow(row) {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "帳號會被刪除 / Tài khoản sẽ bị xóa khỏi hệ thống",
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
                    url: 'Models/WebServiceDB.asmx/DeletedUser',
                    data: "{ 'ID': '" + $(row).attr('value') + "'}",
                    async: true,
                    success: function (data) {
                        if ($.parseJSON(data.d) == 'SUCCESS') {
                            ListUsers('', '', '', '', '', '', '0');
                            swal({
                                title: "通報 / Thông báo",
                                text: "Xóa tài khoản thành công",
                                confirmButtonColor: "#66BB6A",
                                type: "success"
                            });
                        }
                        else {
                            bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                        }
                    },
                    error: function (er) {
                        bootbox.alert('Error system : -' + er);
                    }
                });
            }
        });
}

function ResetRow() {
    $('#hidID').val('');
    $('#<%=txtUserName.ClientID%>').val('');
    $('#<%=txtPassword_User.ClientID%>').val('');
    $('#txtFullName').val('');
    $('#hidPosition').val('');
    $('#hidDepartment').val('');
    $('#uniform-chkIsDeleted span').attr('class', '');
    $('#uniform-chkIsDCC span').attr('class', '');
    $('#<%=txtEmail.ClientID%>').val('');
    $('#<%=txtPhonenumber.ClientID%>').val('');
}

function ListUsers(TenDangNhap, HoTen, SoDienThoai, Email, ChucVu, PhongBan, IsDeleted) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListUsers',
        data: "{ 'TenDangNhap': '" + TenDangNhap + "', 'HoTen': '" + HoTen + "', 'SoDienThoai': '" + SoDienThoai + "', 'Email': '" + Email
            + "','ChucVu':'" + ChucVu + "','PhongBan' :'" + PhongBan + "', 'IsDeleted': '" + IsDeleted + "' }",
        async: true,
        success: function (data) {
            $('#bodyApp').empty();
            $.each($.parseJSON(data.d), function (i, v) {
                var html = '';
                html = '<tr id="tr_' + v.ID + '">';
                html += '<td>' + (i + 1) + '</td>';
                html += '<td>' + v.TenDangNhap + '</td>';
                html += '<td>' + v.HoTen + '</td>';
                html += '<td>' + v.SoDienThoai + '</td>';
                html += '<td>' + v.Email + '</td>';
                if (v.IsDeleted == true) {
                    html += '<td class="text-center"><span class="label label-danger">True</span></td>';
                }
                else {
                    html += '<td class="text-center"><span class="label label-success">False</span></td>';
                }
                html += '<td class="text-center">';
                html += '    <ul class="icons-list">';
                html += '        <li class="text-danger-600"><a href="#" value="'
                    + v.ID + '" onclick="ResetPassword(this);" title="Reset password"><i class="icon-reset"></i></a></li>';
                html += '        <li class="text-danger-600"><a href="#" value="'
                    + v.TenDangNhap + '" onclick="EditUserOrRole(this);" title="Setting"><i class="icon-menu7"></i></a></li>';
                html += '        <li class="text-primary-600"><a href="#" value="'
                    + v.TenDangNhap + '" onclick="EditRow(this);" title="Edit"><i class="icon-pencil7"></i></a></li>';
                html += '        <li class="text-danger-600"><a href="#" value="' + v.ID + '" onclick="DeleteRow(this);" title="Delete"><i class="icon-trash"></i></a></li>';
                html += '    </ul>';
                html += '</td>';
                html += '</tr>';
                $('#bodyApp').append(html);
            });
        },
        error: function (er) {
            bootbox.alert('Error system : -' + er);
        }
    });
}

function SearchUsers() {
    var del = '';
    if ($('#uniform-chkIsDeletedF span').attr('class') == 'checked') {
        del = '1';
    }
    else {
        del = '0';
    }
    ListUsers($('#txtUserNameF').val(), $('#txtFullNameF').val(), $('#txtPhonenumberF').val(), $('#txtEmailF').val(), '', '', del);
}

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">帳號清單 / DANH SÁCH TÀI KHOẢN </h5>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>
        <div class="panel-body">
            <div class="table-responsive">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th class="text-bold  col-lg-1"></th>
                            <th class="text-bold col-lg-2">
                                <input id="txtUserNameF" type="text" class="form-control" placeholder="Input user name">
                            </th>
                            <th class="text-bold col-lg-3">
                                <input id="txtFullNameF" type="text" class="form-control" placeholder="Input full name">
                            </th>
                            <th class="text-bold col-lg-2">
                                <input id="txtPhonenumberF" type="text" class="form-control" placeholder="Input phonenumber">
                            </th>
                            <th class="text-bold col-lg-2">
                                <input id="txtEmailF" type="text" class="form-control" placeholder="Input email">
                            </th>
                            <th class="text-bold col-lg-2">
                                <label class="checkbox-inline">
                                    <input id="chkIsDeletedF" type="checkbox" class="styled">
                                     刪除 / Xóa
                                </label>
                            </th>
                            <th class="col-lg-1">
                                <a href="#" class="btn btn-info btn-sm legitRipple" onclick="SearchUsers();"> 尋找 / Tìm kiếm<i class="glyphicon glyphicon-search position-right"></i>
                                </a>
                            </th>
                        </tr>

                        <tr class="bg-info">
                            <th class="text-bold  col-lg-1"> 序號 <br /> STT</th>
                            <th class="text-bold col-lg-2">帳號 <br /> Tên đăng nhập</th>
                            <th class="text-bold col-lg-3">名稱 <br /> Họ tên</th>
                            <th class="text-bold col-lg-2">電話 <br /> Số điện thoại</th>
                            <th class="text-bold col-lg-3">郵箱 <br /> Email </th>
                            <th class="text-bold col-lg-1">刪除 <br /> Xóa </th>
                            <th class="col-lg-1"></th>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <a href="#" class="btn btn-info btn-sm legitRipple" onclick="AddRow();">
                                    <i class="icon-add-to-list position-left"></i>新增 / Thêm mới
                                </a>
                            </td>

                        </tr>
                    </thead>
                    <tbody id="bodyApp">
                        <tr>
                            <td colspan="7" class="text-center">
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

    <div id="modal_theme_save_user" title="THÊM MỚI HOẶC CẬP NHẬT TÀI KHOẢN">
        <table class="table table-framed">
            <tr>
                <td class="col-md-2"> 帳號 <br /> Tên đăng nhập <span class="text-danger">(*)</span> : </td>
                <td class="col-md-4">
                    <input id="hidID" type="hidden" />
                    <%--<input id="txtUserName" type="text" class="form-control" placeholder="Input user name"  disabled="disabled" />--%>

                    <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Input username"></asp:TextBox>

                </td>
                <td class="col-md-2">密碼 <br /> Mật khẩu <span class="text-danger">(*)</span> :</td>
                <td class="col-md-4">
                    <%--<input id="txtPassword_User" type="password" class="form-control" placeholder="Input password" disabled="disabled" />--%>
                    <asp:TextBox ID="txtPassword_User" runat="server" class="form-control" placeholder="Input password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="col-md-2">名稱 <br /> Họ tên <span class="text-danger">(*)</span> :</td>
                <td class="col-md-4">
                    <input id="txtFullName" type="text" class="form-control" placeholder="Input FullName"></td>
                <td class="col-md-2">郵箱 <br /> Email <span class="text-danger">(*)</span> :</td>
                <td class="col-md-4">
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Input Email"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="col-md-2">電話 <br /> Số điện thoại :</td>
                <td class="col-md-4">
                    <asp:TextBox ID="txtPhonenumber" runat="server" class="form-control" placeholder="Input Phone number"></asp:TextBox></td>
                <td class="col-md-2">
                    <label class="checkbox-inline">
                        IS DCC
                    <input id="chkIsDCC" type="checkbox" class="styled">
                    </label>
                </td>
                <td class="col-md-4">
                    <label class="checkbox-inline">
                        刪除 / Xóa
                    <input id="chkIsDeleted" type="checkbox" class="styled">
                    </label>
                </td>
            </tr>
            <tr>
                <td class="col-md-2">職位 <br /> Chức vụ :</td>
                <td class="col-md-10" colspan="3">
                    <div id="chkPosition"></div>
                </td>
            </tr>
            <tr>
                <td class="col-md-2">課級主管 <br /> Chủ quản phòng :</td>
                <td class="col-md-10" colspan="3">
                    <select id="cboManager_room" multiple="multiple" class="select-border-color border-warning"></select>
                </td>
            </tr>
            <tr>
                <td class="col-md-2">部門 <br /> Bộ phận :</td>
                <td class="col-md-10" colspan="3">
                    <div id="chkDepartment"></div>
                </td>
            </tr>
        </table>
    </div>


    <div id="modal_theme_users_roles" title="設定小組 / THIẾT LẬP NHÓM">
        <div class="form-group">
            <label class="control-label col-sm-4 text-bold">帳號 / Tên đăng nhập:</label>
            <div class="col-sm-8">
                <input id="txtUser" type="text" class="form-control" readonly="readonly">
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-4 text-bold">小組 / Nhóm:</label>
            <div id="chkRoles" class="col-sm-8 text-blue"></div>
        </div>
    </div>

</asp:Content>

