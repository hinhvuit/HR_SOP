<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SettingPermitsion.aspx.cs" Inherits="HR_SOP.SettingPermitsion" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .fix {
            width: 23px !important;
            height: 23px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () {
                ListSetPermitsion($('#<%=cboRoles.ClientID%>').val());
            }, 500);

            $('#<%=cboRoles.ClientID%>').change(function () {
                ListSetPermitsion($(this).val());
            });
        });

        function CheckRowAll(row) {
            var order = '';
            $('#tr_' + $(row).attr('value') + ' td').each(function (i) {
                if (i == 0) {
                    order = $(this).html();
                }
            });
            if ($('#chk_All_' + order + '').is(':checked') == true) {
                $('#chk_Xem_' + order + '').prop('checked', true);
                $('#chk_ThemMoi_' + order + '').prop('checked', true);
                $('#chk_Sua_' + order + '').prop('checked', true);
                $('#chk_Xoa_' + order + '').prop('checked', true);
                $('#chk_BaoCao_' + order + '').prop('checked', true);
                $('#chk_TimKiem_' + order + '').prop('checked', true);
                $('#chk_ResetPass_' + order + '').prop('checked', true);
            }
            else {
                $('#chk_Xem_' + order + '').prop('checked', false);
                $('#chk_ThemMoi_' + order + '').prop('checked', false);
                $('#chk_Sua_' + order + '').prop('checked', false);
                $('#chk_Xoa_' + order + '').prop('checked', false);
                $('#chk_BaoCao_' + order + '').prop('checked', false);
                $('#chk_TimKiem_' + order + '').prop('checked', false);
                $('#chk_ResetPass_' + order + '').prop('checked', false);
            }
        }

        function SavePermitsion() {
            var order = '0';
            var CodeRole = $('#<%=cboRoles.ClientID%>').val();
            var html = '';
            $('#bodyApp_Permission tr').each(function (i) {
                html += '<tr>';
                $('#' + $(this).attr('id') + ' td').each(function (i) {
                    html += '<td>';
                    if (i == 0) {
                        order = $(this).text();
                        html += $(this).text();
                    } else if (i == 1) {
                        html += $(this).text();
                    } else if (i == 2) {
                        html += $(this).text();
                    }
                    else if (i == 3) {
                        html += $('#chk_Xem_' + order + '').is(':checked') ? 1 : 0;
                    } else if (i == 4) {
                        html += $('#chk_ThemMoi_' + order + '').is(':checked') ? 1 : 0;
                    } else if (i == 5) {
                        html += $('#chk_Sua_' + order + '').is(':checked') ? 1 : 0;
                    } else if (i == 6) {
                        html += $('#chk_Xoa_' + order + '').is(':checked') ? 1 : 0;
                    } else if (i == 7) {
                        html += $('#chk_BaoCao_' + order + '').is(':checked') ? 1 : 0;
                    } else if (i == 8) {
                        html += $('#chk_TimKiem_' + order + '').is(':checked') ? 1 : 0;
                    } else if (i == 9) {
                        html += $('#chk_ResetPass_' + order + '').is(':checked') ? 1 : 0;
                    } else {
                        html += $('#chk_All_' + order+'').attr('value');
                    }
                    html += '</td>';
                });
                html += '</tr>';
                if ($('#bodyApp_Permission tr').length == (i + 1)) {
                    $.ajax({
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        url: 'Models/WebServiceDB.asmx/InsertOrUpdateModule',
                        data: "{'CodeRole':'" + CodeRole + "','Content':'" + html + "'}",
                        async: true,
                        success: function (data) {
                            if ($.parseJSON(data.d) == 'SUCCESS') {
                                swal({
                                    title: "通知 / Thông báo",
                                    text: "存儲資料成功 / Lưu dữ liệu thành công!",
                                    confirmButtonColor: "#66BB6A",
                                    type: "success"
                                });
                            }
                            else {
                                swal({
                                    title: "通知 / Thông báo",
                                    text: "過程發現錯誤，請重新操作 / Lỗi trong quá trình lưu dữ liệu vui lòng thực hiện lại!",
                                    confirmButtonColor: "#EF5350",
                                    type: "error"
                                });
                            }
                        },
                        error: function (er) {
                            console.log('err');
                        }
                    });
                }
            });

        }

        function ListSetPermitsion(CodeRole) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListSetPermitsion',
                data: "{ 'CodeRole': '" + CodeRole + "'}",
                async: true,
                success: function (data) {
                    $('#bodyApp_Permission').empty();
                    $.each($.parseJSON(data.d), function (i, v) {
                        var order = (i + 1);
                        var html = '';
                        html = '<tr id="tr_' + v.ID + '">';
                        html += '<td>' + order + '</td>';
                        html += '<td>' + v.Code + '</td>';
                        html += '<td>' + v.Name + '</td>';

                        if (v.Xem == true) {
                            html += '<td class="text-center"><input id="chk_Xem_' + order + '" type="checkbox" class="form-control fix" checked="checked" /></td>';
                        }
                        else {
                            html += '<td class="text-center"><input id="chk_Xem_' + order + '" type="checkbox" class="form-control fix" /></td>';
                        }

                        if (v.ThemMoi == true) {
                            html += '<td class="text-center"><input id="chk_ThemMoi_' + order + '" type="checkbox" class="form-control fix" checked="checked" /></td>';
                        }
                        else {
                            html += '<td class="text-center"><input id="chk_ThemMoi_' + order + '" type="checkbox" class="form-control fix"  /></td>';
                        }

                        if (v.Sua == true) {
                            html += '<td class="text-center"><input id="chk_Sua_' + order + '" type="checkbox" class="form-control fix" checked="checked" /></td>';
                        }
                        else {
                            html += '<td class="text-center"><input id="chk_Sua_' + order + '" type="checkbox" class="form-control fix" /></td>';
                        }

                        if (v.Xoa == true) {
                            html += '<td class="text-center"><input id="chk_Xoa_' + order + '" type="checkbox" class="form-control fix" checked="checked" /></td>';
                        }
                        else {
                            html += '<td class="text-center"><input id="chk_Xoa_' + order + '" type="checkbox" class="form-control fix" /></td>';
                        }

                        if (v.BaoCao == true) {
                            html += '<td class="text-center"><input id="chk_BaoCao_' + order + '" type="checkbox" class="form-control fix" checked="checked" /></td>';
                        }
                        else {
                            html += '<td class="text-center"><input id="chk_BaoCao_' + order + '" type="checkbox" class="form-control fix" /></td>';
                        }

                        if (v.TimKiem == true) {
                            html += '<td class="text-center"><input id="chk_TimKiem_' + order + '" type="checkbox" class="form-control fix" checked="checked" /></td>';
                        }
                        else {
                            html += '<td class="text-center"><input id="chk_TimKiem_' + order + '" type="checkbox" class="form-control fix" /></td>';
                        }

                        if (v.ResetPass == true) {
                            html += '<td class="text-center"><input id="chk_ResetPass_' + order + '" type="checkbox" class="form-control fix" checked="checked" /></td>';
                        }
                        else {
                            html += '<td class="text-center"><input id="chk_ResetPass_' + order + '" type="checkbox" class="form-control fix"/></td>';
                        }
                        if (v.CK_ALL == true) {
                            html += '<td class="text-center"><input id="chk_All_' + order + '" type="checkbox" checked="checked" class="form-control fix" value="' + v.ID + '" onclick="CheckRowAll(this);"/></td>';

                        } else {
                            html += '<td class="text-center"><input id="chk_All_' + order + '" type="checkbox" class="form-control fix" value="' + v.ID + '" onclick="CheckRowAll(this);"/></td>';
                        }


                        html += '</tr>';
                        $('#bodyApp_Permission').append(html);
                    });
                },
                error: function (er) {
                    console.log('err');
                }
            });
        }

    </script>

    <div class="panel panel-flat border-top-info-300 border-right-info-300 border-bottom-info-300 border-left-info-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">權利設定 / THIẾT LẬP QUYỀN</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>

        <div class="panel-body">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td colspan="3">
                            <asp:DropDownList ID="cboRoles" runat="server" data-placeholder="Select a role..." class="select-search"></asp:DropDownList>
                        </td>
                        <td colspan="8">
                            <a href="#" class="btn btn-info btn-sm legitRipple" onclick="SavePermitsion();"> 存儲 / Lưu
                            </a>
                        </td>
                    </tr>

                    <tr class="bg-info">

                        <th class="text-bold text-center"> 序號 <br /> STT</th>

                        <th class="text-bold text-center col-lg-2"> 編號 <br /> Mã</th>
                        <th class="text-bold text-center col-lg-3"> 名稱 <br /> Tên</th>
                        <th class="text-bold text-center"> 查看 <br /> Xem</th>
                        <th class="text-bold text-center"> 新增 <br /> Thêm</th>
                        <th class="text-bold text-center"> 修改 <br /> Sửa</th>

                        <th class="text-bold text-center"> 刪除 <br /> Xóa</th>
                        <th class="text-bold text-center"> 報告 <br /> Báo cáo </th>
                        <th class="text-bold text-center"> 查詢 <br />Tìm kiếm</th>
                        <th class="text-bold text-center"> 重置 <br /> Reset</th>
                        <th class="text-bold text-center"> 全部 <br /> Tất cả</th>

                    </tr>
                </thead>
                <tbody id="bodyApp_Permission">
                    <tr>
                        <td colspan="11" class="text-center">
                            <label id="spinner-light-8">
                                <i class="icon-spinner11 spinner position-left"></i>Loading...
                            </label>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

    </div>

</asp:Content>

