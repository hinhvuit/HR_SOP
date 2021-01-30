<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterCancelDocument.aspx.cs" Inherits="HR_SOP.RegisterCancelDocument" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .fix {
            width: 20px !important;
            height: 20px !important;
            margin: 0px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#approval').hide();
            $('#btnLuu').hide();
            $('#btnHuyBo').hide();
            $('#btnLamLai').hide();
            $('#btnXacNhan').hide();
            $('#btnTuChoi').hide();

            var docNO = $('#<%=hidCancelDocument.ClientID%>').val();
            if (docNO != '') {
                $('#approval').show();
                setTimeout(function () {
                    ListApprovalSection(docNO);
                    setTimeout(function () {
                        ShowOrHide(docNO);
                    }, 300);
                }, 300);
            }
            else {
                $('#btnLuu').show();
                $('#btnLamLai').show();
            }

        });

        function ShowOrHide(CancelDocument) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckDisplaySubmitCancelDocument',
                data: "{ 'CancelDocument': '" + CancelDocument + "'}",
                async: true,
                success: function (data) {
                    $.each($.parseJSON(data.d), function (i, v) {
                        var states = $('#<%=hidStates.ClientID%>').val();
                        if (states == 'G01') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnLuu').show();
                                $('#btnXacNhan').show();
                                $('#btnHuyBo').show();
                            }
                        }
                        else if (states == 'G03' || states == 'G05' || states == 'G10') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnXacNhan').show();
                                $('#btnTuChoi').show();
                            }
                        }
                        else if (states == 'G20') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnLuu').show();
                                $('#btnHuyBo').show();
                            }
                        }
                        else {
                            $('#btnLuu').hide();
                            $('#btnHuyBo').hide();
                            $('#btnLamLai').hide();
                            $('#btnXacNhan').hide();
                            $('#btnTuChoi').hide();
                        }
                    })
                },
                error: function (er) {
                    bootbox.alert("Error system : -" + er);
                }
            });
}

function LamLai() {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "表單會被刪除 / Phiếu đăng ký sẽ bị xóa bỏ khỏi hệ thống",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#FF7043",
        confirmButtonText: "同意 / Đồng ý",
        showLoaderOnConfirm: true
    },
    function (isConfirm) {
        if (isConfirm) {
            location.href = $(location).attr('href');
        }
    });
}

function Luu() {
    if ($('#<%=ddlDepartment.ClientID%>').val() == null) {
        bootbox.alert("選擇簽核主管 / Chọn chủ quản ký");
        $('#<%=ddlDepartment.ClientID%>').focus();
        return;
    }
    else {
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            url: 'Models/WebServiceDB.asmx/InsertOrUpdateRegisterCancelDocument',
            data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "', 'CancelDocument': '" + $('#<%=txtApplicationNO.ClientID%>').val() + "', 'sApplicationDate': '" + $('#<%=txtApplicationDate.ClientID%>').val()
               + "', 'sEffectiveDate': '" + $('#<%=txtEffectiveDate.ClientID%>').val() + "', 'ApplicationSite': '" + $('#<%=ddlApplicationSite.ClientID%>').val()
               + "', 'CloseDocument': '', 'ApplicationNo_Code': '" + $('#<%=txtApplicationNo_DCC.ClientID%>').val() + "', 'DocNo_DCC': '" + $('#<%=txtDocNo_DCC.ClientID%>').val() + "', 'ReasonOfApplication': '"
               + $('#<%=txtReasonApplication.ClientID%>').val() + "','Department':'" + $('#<%=ddlDepartment.ClientID%>').val() + "'}",
            async: true,
            success: function (data) {
                if ($.parseJSON(data.d) == 'ERROR') {
                    swal({
                        title: "通報 / Thông báo",
                        text: "操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình lưu dữ liệu vui lòng thực hiện lại!",
                        confirmButtonColor: "#EF5350",
                        type: "error"
                    });
                }
                else if ($.parseJSON(data.d) != '') {
                    swal({
                        title: "通報 / Thông báo!",
                        text: "存儲成功 / Lưu dữ liệu thành công!",
                        confirmButtonColor: "#66BB6A",
                        type: "success"
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            location.href = 'RegisterCancelDocument.aspx?CancelDocument=' + $.parseJSON(data.d);
                        }
                    });
                }
            },
            error: function (er) {
                bootbox.alert('Error system : - ' + er);
            }
        });
    }
}


function ListApprovalSection(CancelDocument) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListApprovalSection',
        data: "{ 'CodeDocument': '" + CancelDocument + "'}",
        async: true,
        success: function (data) {
            $('#listApproval').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                html += '<tr>';
                html += ' <td>' + v.Station + '</td>';
                html += ' <td>' + v.Dept + '</td>';
                html += ' <td>' + v.Name + '</td>';
                html += ' <td>' + v.Ext + '</td>';

                html += ' <td>' + v.DateAndTime + '</td>';
                html += ' <td>' + v.Status + '</td>';
                html += ' <td>' + v.Times + '</td>';
                html += ' <td>' + v.Comment + '</td>';
                html += '</tr>';
            });
            $('#listApproval').append(html);
        },
        error: function (er) {
            bootbox.alert('Error system : - ' + er);
        }
    });
}

function XacNhan() {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "表單呈簽下一步Đơn sẽ trình ký đến chủ quản tiếp theo",
        type: "input",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        closeOnConfirm: false,
        confirmButtonText: "同意 / Đồng ý",
        cancelButtonText: '拒絕 / Từ chối',
        animation: "slide-from-top",
        inputPlaceholder: "意見輸入 / Nhập ý kiến",
        showLoaderOnConfirm: true
    },
    function (isConfirm) {
        if (isConfirm === false) {
            return false;
        }
        else if (isConfirm === "") {
            swal.showInputError("意見輸入 / Nhập ý kiến");
            return false
        }
        else {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/AcceptRegisterCancelDocument',
                data: "{ 'CancelDocument': '" + $('#<%=hidCancelDocument.ClientID%>').val() + "','States': '" + $('#<%=hidStates.ClientID%>').val()
                        + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListCancelDocument.aspx";
                    }
                    else {
                        swal({
                            title: "通報 / Thông báo",
                            text: "操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình lưu dữ liệu vui lòng thực hiện lại!",
                            confirmButtonColor: "#EF5350",
                            type: "error"
                        });
                    }
                },
                error: function (er) {
                    bootbox.alert('Error system : - ' + er);
                }
            });
        }
    });
}

function HuyBo() {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "表單會被刪除 / Phiếu đăng ký này sẽ bị xóa khỏi hệ thống.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#FF7043",
        confirmButtonText: "同意 / Đồng ý",
        cancelButtonText: '拒絕 / Từ chối',
        showLoaderOnConfirm: true
    },
    function (isConfirm) {
        if (isConfirm) {

            var light = $('#loader').parent();
            $(light).block({
                message: '<i class="icon-spinner2 spinner"></i>',
                overlayCSS: {
                    backgroundColor: '#fff',
                    opacity: 0.8,
                    cursor: 'wait'
                },
                css: {
                    border: 0,
                    padding: 0,
                    backgroundColor: 'none'
                }
            });


            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/DeletedRegisterCancelDocument',
                data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "'}",
                async: true,
                success: function (data) {
                    $(light).unblock();
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListCancelDocument.aspx";
                    }
                    else {
                        bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                    }
                },
                error: function (er) {
                    bootbox.alert('Error system : - ' + er);
                }
            });
        }
    });


}

function TuChoi() {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "登記表轉至承辦人 / Phiếu đăng ký này sẽ chuyển lại về người làm đơn.",
        type: "input",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        closeOnConfirm: false,
        confirmButtonText: "同意 / Đồng ý",
        cancelButtonText: '拒絕 / Từ chối',
        animation: "slide-from-top",
        inputPlaceholder: "意見輸入 / Nhập ý kiến",
        showLoaderOnConfirm: true
    },
    function (isConfirm) {
        if (isConfirm === false) {
            return false;
        }
        else if (isConfirm === "") {
            swal.showInputError("意見輸入 / Nhập ý kiến");
            return false
        }
        else {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/RejectRegisterCancelDocument',
                data: "{ 'CancelDocument': '" + $('#<%=hidCancelDocument.ClientID%>').val() + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListCancelDocument.aspx";
                    }
                    else {
                        bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                    }
                },
                error: function (er) {
                    bootbox.alert('Error system : - ' + er);
                }
            });
        }
    });

}



    </script>


    <div class="panel panel-body border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300 bg-grey-300">
        <table class="col-md-12">
            <tr>
                <td class="col-md-6">
                    <img src="Images/fc_logo.png" alt="" height="40" />
                </td>
                <td class="col-md-6">
                    <label class="control-label text-size-large text-bold text-white">文件編號取消申請 / ĐƠN XIN HỦY MÃ VĂN BẢN</label>
                </td>
            </tr>
        </table>
    </div>

    <div class="panel panel-body border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">

        <table class="table table-borderless">
            <tr>
                <td colspan="2" class="col-lg-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單位 <br /> Đơn vị xin đơn:</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>

                <td colspan="2" class="col-lg-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單編號 <br /> Mã đơn:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationNO" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請人 <br /> Người xin đơn :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請日期 <br /> Ngày xin đơn :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationDate" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請廠區 <br /> Nhà xưởng xin đơn:</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlApplicationSite" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">生效日期 <br /> Ngày có hiệu lực:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單編號 <br /> Mã đơn:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationNo_DCC" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">文件編號 <br /> Mã văn bản:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtDocNo_DCC" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    

                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">預計結案日 <br /> Ngày kết án dự tính:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtEstimatedCloseData" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                </td>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">原應修訂文件 <br /> Tài liệu sửa đổi ban đầu: </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtNameFile" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>

            </tr>

          <%--  <tr>
                <td colspan="2">

                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">原指定修訂人 <br /> (Assigned Revisor): </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtAssignedRevisor" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                </td>
                <td colspan="2">

                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">關結單據別 <br /> (Type of closing application):</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlCloseDocument" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>

                </td>
            </tr>--%>


            <tr>
                <td colspan="4">
                    <div class="form-group">
                        <label class="control-label col-lg-3 text-bold">申請原因 <br /> Nguyên nhân xin đơn: </label>
                        <div class="col-lg-9">
                            <asp:TextBox ID="txtReasonApplication" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr id="approval">
                <td colspan="4">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <td colspan="8">
                                    <label class="control-label text-bold">批准 / Phê duyệt : </label>
                                </td>
                            </tr>
                            <tr>

                                <th class="text-bold">位置 <br /> Vị trí</th>
                                <th class="text-bold">部門<br /> Bộ phận</th>
                                <th class="text-bold">姓名 <br /> Họ tên</th>
                                <th class="text-bold">電話號碼 <br /> Số điện thoại</th>
                                <th class="text-bold">日期 <br /> Ngày giờ</th>
                                <th class="text-bold">簽核狀態<br /> Tình trạng ký</th>
                                <th class="text-bold">時間 <br /> Thời gian</th>
                                <th class="text-bold">備註 <br /> Ghi chú</th>

                            </tr>
                        </thead>
                        <tbody id="listApproval"></tbody>
                    </table>
                </td>
            </tr>


        </table>

        <br />

        <div class="col-lg-12">
            <div class="form-group text-center">
                <asp:HiddenField ID="hidUserName" runat="server" />
                <asp:HiddenField ID="hidCancelDocument" runat="server" />
                <asp:HiddenField ID="hidStates" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />

                <a id="btnLuu" href="javascript:void(0)" class="btn bg-green btn-rounded width-200" onclick="Luu();"><i class="icon-add position-left"></i>存儲 / Lưu</a>
                <a id="btnLamLai" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="LamLai();"><i class="icon-cancel-circle2 position-left"></i>重置 / Làm lại</a>

                <a id="btnXacNhan" href="javascript:void(0)" class="btn bg-pink btn-rounded width-200" onclick="XacNhan();">確認 / Xác nhận</a>
                <a id="btnHuyBo" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="HuyBo();">取消 / Hủy bỏ</a>
                <a id="btnTuChoi" href="javascript:void(0)" class="btn bg-indigo btn-rounded width-200" onclick="TuChoi();">拒絕 / Từ chối</a>

            </div>
        </div>
    </div>


</asp:Content>

