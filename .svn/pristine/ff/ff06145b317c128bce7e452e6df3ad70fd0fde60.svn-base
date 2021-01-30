<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menus.aspx.cs" Inherits="HR_SOP.Menus" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () {
                ListMenus('', '');
            }, 500);
           
            $('#fromMenu').dialog({
                dialogClass: 'my_dialog',
                autoOpen: false,
                modal: true,
                width: 500,
                buttons: {
                    Submit: function () {
                        $.ajax({
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            url: 'Models/WebServiceDB.asmx/InsertOrUpdateMenus',
                            data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "','Code': '" + $('#<%=hidCode.ClientID%>').val() + "','Name': '" + $('#<%=txtName.ClientID%>').val()
                                + "','Url': '" + $('#<%=txtUrl.ClientID%>').val() + "','Orders': '" + $('#<%=txtOrder.ClientID%>').val() + "','Groups': '" + $('#<%=ddlGroup.ClientID%>').val() + "'}",
                            async: true,
                            success: function (data) {
                                if ($.parseJSON(data.d) == 'ADD') {
                                    ResetRow();
                                    ListMenus('', '');
                                    $('#fromMenu').dialog('close');
                                    swal({
                                        title: "通報 / Thông báo",
                                        text: "新增成功 / Thêm mới thành công",
                                        confirmButtonColor: "#66BB6A",
                                        type: "success"
                                    });
                                }
                                else if ($.parseJSON(data.d) == 'EDIT') {
                                    ResetRow();
                                    ListMenus('', '');
                                    $('#fromMenu').dialog('close');
                                    swal({
                                        title: "通報 / Thông báo",
                                        text: "存儲成功 / Lưu dữ liệu thành công",
                                        confirmButtonColor: "#66BB6A",
                                        type: "success"
                                    });
                                }
                                else {
                                    bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                                }
                            },
                            error: function (er) {
                                bootbox.alert('Error system : -' +er);
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

        function EditRow(row) {
            $('#tr_' + $(row).attr('value') + ' td').each(function (i) {
                if (i == 0) {
                    $('#<%=hidID.ClientID%>').val($(row).attr('value'));
                    $('#<%=txtOrder.ClientID%>').val($(row).attr('Orders'));
                    $('#<%=ddlGroup.ClientID%>').val($(row).attr('Groups'));
                }
                else if (i == 1) {
                    $('#<%=hidCode.ClientID%>').val($(this).html());
                }
                else if (i == 2) {
                    $('#<%=txtName.ClientID%>').val($(this).html());
                }
                else if (i == 3) {
                    $('#<%=txtUrl.ClientID%>').val($(this).html());
                }
            });
            $('#fromMenu').dialog('open');
        }

        function DeleteRow(row) {
            swal({
                title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
                text: "Menu 會被刪除 / Menus sẽ bị xóa khỏi hệ thống",
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
                            url: 'Models/WebServiceDB.asmx/DeletedMenus',
                            data: "{ 'ID': '" + $(row).attr('value') + "'}",
                            async: true,
                            success: function (data) {
                                if ($.parseJSON(data.d) == 'SUCCESS') {
                                    ListMenus('', '');
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
            $('#<%=hidID.ClientID%>').val('');
            $('#<%=txtName.ClientID%>').val('');
            $('#<%=txtUrl.ClientID%>').val('');
            $('#<%=txtOrder.ClientID%>').val('');
            $('#<%=ddlGroup.ClientID%>').val('CT-00026');
        }

        function GetCode() {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/GetCodeAuto',
                data: "{ 'NameCol': 'Code', 'NameTable': 'Menus' }",
                async: true,
                success: function (data) {
                    $('#<%=hidCode.ClientID%>').val($.parseJSON(data.d));
                    ResetRow();
                    $('#fromMenu').dialog('open');
                },
                error: function (er) {
                    bootbox.alert('Error system : -' + er);
                }
            });
            }

            function ListMenus(Code, Name) {
                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    url: 'Models/WebServiceDB.asmx/ListMenus',
                    data: "{ 'Code': '" + Code + "', 'Name': '" + Name + "'}",
                    async: true,
                    success: function (data) {
                        $('#bodyApp').empty();
                        var html = '';
                        $.each($.parseJSON(data.d), function (i, v) {

                            html += '<tr id="tr_' + v.ID + '">';

                            html += '<td>' + (i + 1) + '</td>';
                            html += '<td>' + v.Code + '</td>';
                            html += '<td>' + v.Name + '</td>';
                            html += '<td>' + v.Url + '</td>';
                            html += '<td>' + v.Groups_Name + '</td>';
                            if (v.IsDeleted == true) {
                                html += '<td class="text-center"><span class="label label-danger">True</span></td>';
                            }
                            else {
                                html += '<td class="text-center"><span class="label label-success">False</span></td>';
                            }
                            html += '<td class="text-center">';
                            html += '    <ul class="icons-list">';
                            html += '        <li class="text-primary-600"><a href="javascript:void(0)" value="' + v.ID + '" Orders="' + v.Orders + '" Groups="' + v.Groups + '" onclick="EditRow(this);"  title="Edit"><i class="icon-pencil7"></i></a></li>';
                            html += '        <li class="text-danger-600"><a href="javascript:void(0)" value="' + v.ID + '" onclick="DeleteRow(this);" title="Delete"><i class="icon-trash"></i></a></li>';
                            html += '    </ul>';
                            html += '</td>';

                            html += '</tr>';

                        });
                        $('#bodyApp').append(html);
                    },
                    error: function (er) {
                        bootbox.alert('Error system : -' + er);
                    }
                });
            }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">權限清單 / DANH SÁCH QUYỀN HẠN</h5>
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
                        <th class="text-bold col-lg-1">Mã <br /> 編號</th>
                        <th class="text-bold col-lg-2">Tên <br /> 名稱</th>
                        <th class="text-bold col-lg-4">Dường dẫn <br /> 地址</th>
                        <th class="text-bold col-lg-2">Nhóm <br /> 小組</th>
                        <th class="text-bold col-lg-1">Xóa <br />刪除</th>
                        <th class="col-lg-1"></th>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple" onclick="GetCode();">
                                <i class="icon-add-to-list position-left"></i>新增 / Thêm mới
                            </a>
                        </td>

                    </tr>
                </thead>
                <tbody id="bodyApp"></tbody>
            </table>

        </div>

    </div>

    <div id="fromMenu" title="THÊM MỚI HOẶC CẬP NHẬT MENUS">
        <div class="row">
            <div class="form-group">
                <label class="control-label col-sm-4 text-bold">Tên/ 名稱</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtName" CssClass="form-control" runat="server" placeholder="Input name"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label class="control-label col-sm-4 text-bold">Dường dẫn/ 地址</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtUrl" CssClass="form-control" runat="server" placeholder="Input url"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label class="control-label col-sm-4 text-bold">STT / 序號</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtOrder" CssClass="form-control" runat="server" placeholder="Only input number"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label class="control-label col-sm-4 text-bold">Nhóm/ 小組</label>
                <div class="col-sm-8">
                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidCode" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
    </div>

</asp:Content>

