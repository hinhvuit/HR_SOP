<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationObsoletedDocument.aspx.cs" Inherits="HR_SOP.ApplicationObsoletedDocument" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .fix {
            width: 20px !important;
            height: 20px !important;
            margin: 0px !important;
        }
    </style>

    <script type="text/javascript">
        var type = '';
        var listOld = '';
        var listRef = '';

        $(document).ready(function () {
            $('#approval').hide();
            $('#btnLuu').hide();
            $('#btnHuyBo').hide();
            $('#btnLamLai').hide();
            $('#btnXacNhan').hide();
            $('#btnTuChoi').hide();

            var pubNO = $('#<%=hidObsoletedDocument.ClientID%>').val();
            if (pubNO != '') {
                $('#approval').show();
                setTimeout(function () {
                    ListApprovalSection(pubNO);
                    setTimeout(function () {
                        ShowOrHide(pubNO);
                    }, 300);
                }, 300);
            }
            else {
                $('#btnLuu').show();
                $('#btnLamLai').show();
            }
            LoadDepartment();
            //LoadApplicableSite();
            //LoadApplicableBU();

        });


        function ListApprovalSection(ObsoletedDocument) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListApprovalSection',
                data: "{ 'CodeDocument': '" + ObsoletedDocument + "'}",
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
                    bootbox.alert('Error system : -' + er);
                }
            });
        }

        function ShowOrHide(ObsoletedDocument) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckDisplaySubmitObsoletedDocument',
                data: "{ 'ObsoletedDocument': '" + ObsoletedDocument + "'}",
                async: true,
                success: function (data) {
                    $.each($.parseJSON(data.d), function (i, v) {
                        var states = $('#<%=hidStates.ClientID%>').val();
                        if (states == 'D01') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnLuu').show();
                                $('#btnXacNhan').show();
                                $('#btnHuyBo').show();
                            }
                        }
                        else if (states == 'D03' || states == 'D05' || states == 'D10' || states == 'D15' || states == 'D20' || states == 'D25') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnXacNhan').show();
                                $('#btnTuChoi').show();

                            }
                        }
                        else if (states == 'D30') {
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
                    bootbox.alert('Error system : -' + er);
                }
            });
}

<%--function LoadDepartment() {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListCategorys',
        data: "{ 'CatCode': '', 'CatName': '', 'CatTypeCode': 'CT-00002' }",
        async: true,
        success: function (data) {
            $('#department').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                html += '<div class="col-md-4">';
                html += '   <div class="form-group">';
                html += '     <label class="checkbox-inline">';
                html += '       <input id="chk_' + v.CatCode + '" type="checkbox" value="' + v.CatCode + '" class="control-label fix" disabled="disabled">' + v.CatName + '';
                html += '     </label>';
                html += '   </div>';
                html += '</div>';
            });
            $('#department').append(html);

            var depart = $('#<%=hidDepartmentCheck.ClientID%>').val().split(',');
            if (depart.length > 1) {
                for (var i = 0; i < depart.length; i++) {
                    if (depart[i] != '') {
                        $('#chk_' + depart[i] + '').prop('checked', true);
                    }
                }
            }
        },
        error: function (er) {
            bootbox.alert('Error system : -' + er);
        }
    });

}--%>


<%--function LoadApplicableSite() {
    $('#applicableSite').empty();
    var html = '';
    html += '<label class="control-label col-lg-2 text-bold">適用廠區 / Nhà xưởng sử dụng: </label>';
    $('#<%=ddlApplicationSite.ClientID%> option ').each(function (ind) {
        html += '<label class="checkbox-inline">';
        html += '<input id="chk_' + $(this).val() + '" value="' + $(this).val() + '" type="checkbox" class="control-label fix" disabled="disabled">' + $(this).text() + '';
        html += '</label>';
    });
    $('#applicableSite').append(html);

    var site = $('#<%=hidApplicableSite.ClientID%>').val().split(',');
    if (site.length > 1) {
        for (var i = 0; i < site.length; i++) {
            if (site[i] != '') {
                $('#chk_' + site[i] + '').prop('checked', true);
            }
        }
    }
    else {
        $('#chk_C-00005').prop('checked', true);
    }
}--%>


<%--function LoadApplicableBU() {
    $('#applicableBU').empty();
    var html = '';
    html += '<label class="control-label col-lg-2 text-bold">適用BU / BU sửa dụng: </label>';
    $('#<%=ddlDocType.ClientID%> option ').each(function (ind) {
        html += '<label class="checkbox-inline">';
        html += '<input id="chk_' + $(this).val() + '" value="' + $(this).val() + '" type="checkbox" class="control-label fix" disabled="disabled"> ' + $(this).text() + '';
        html += '</label>';
    });
    $('#applicableBU').append(html);

    var bu = $('#<%=hidApplicableBU.ClientID%>').val().split(',');
    if (bu.length > 1) {
        for (var i = 0; i < bu.length; i++) {
            if (bu[i] != '') {
                $('#chk_' + bu[i] + '').prop('checked', true);
            }
        }
    }
    else {
        $('#chk_C-00006').prop('checked', true);
    }
}--%>

function Luu() {
    if ($('#<%=txtReasonObsoleted.ClientID%>').val() == '') {
        bootbox.alert("Nhập lý do phế bỏ văn kiện/ 文件報廢理由輸入");
        $('#<%=txtReasonObsoleted.ClientID%>').focus();
        return;
    }
    else {

        var ID = $('#<%=hidID.ClientID%>').val();
        var ObsoletedDocument = $('#<%=txtApplicationNO.ClientID%>').val();
        var PublishDocument = $('#<%=hidPublishDocument.ClientID%>').val();
        var ApplicationSite = $('#<%=ddlApplicationSite.ClientID%>').val();
        var sEffectiveDate = $('#<%=txtEffectiveDate.ClientID%>').val();

        var ReasonObsoleted = $('#<%=txtReasonObsoleted.ClientID%>').val();
        var sApplicationDate = $('#<%=txtApplicationDate.ClientID%>').val();

        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            url: 'Models/WebServiceDB.asmx/InsertOrUpdateApplicationObsoletedDocument',
            data: "{ 'ID': '" + ID + "', 'ObsoletedDocument': '" + ObsoletedDocument + "', 'PublishDocument': '" + PublishDocument + "', 'ApplicationSite': '" + ApplicationSite
            + "', 'sEffectiveDate': '" + sEffectiveDate + "', 'ReasonObsoleted': '" + ReasonObsoleted + "', 'sApplicationDate': '" + sApplicationDate + "'}",
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
                        title: "通報 / Thông báo",
                        text: "存儲成功 / Lưu dữ liệu thành công",
                        confirmButtonColor: "#66BB6A",
                        type: "success"
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            location.href = 'ApplicationObsoletedDocument.aspx?ObsoletedDocument=' + $.parseJSON(data.d);
                        }
                    });
                }
            },
            error: function (er) {
                bootbox.alert('Error system : -' + er);
            }
        });

    }
}
function LamLai() {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "表單會被刪除 / Phiếu đăng ký sẽ bị xóa bỏ khỏi hệ thống",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#FF7043",
        confirmButtonText: "同意 / Đồng ý",
        cancelButtonText: '拒絕 / Từ chối',
        showLoaderOnConfirm: true
    },
    function (isConfirm) {
        if (isConfirm) {
            location.href = $(location).attr('href');
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
                url: 'Models/WebServiceDB.asmx/AcceptAcceptApplicationObsoletedDocument',
                data: "{ 'ObsoletedDocument': '" + $('#<%=hidObsoletedDocument.ClientID%>').val() + "','States': '" + $('#<%=hidStates.ClientID%>').val()
                    + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListObsoletedDocument.aspx";
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

function HuyBo() {

    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "表單會被刪除 / Phiếu đăng ký sẽ bị xóa bỏ khỏi hệ thống",
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
                url: 'Models/WebServiceDB.asmx/DeletedApplicationObsoletedDocument',
                data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "'}",
                async: true,
                success: function (data) {
                    $(light).unblock();
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListObsoletedDocument.aspx";
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

function TuChoi() {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "登記表轉至承辦人 / Phiếu đăng ký này sẽ chuyển lại cho người làm đơn.",
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
            url: 'Models/WebServiceDB.asmx/RejectApplicationObsoletedDocument',
            data: "{ 'ObsoletedDocument': '" + $('#<%=hidObsoletedDocument.ClientID%>').val() + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "'}",
            async: true,
            success: function (data) {
                if ($.parseJSON(data.d) == 'SUCCESS') {
                    location.href = "ListObsoletedDocument.aspx";
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


    </script>

    <div class="panel panel-body border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300 bg-grey-300">
        <table class="col-md-12">
            <tr>
                <td class="col-md-6">
                    <img src="Images/fc_logo.png" alt="" height="49" />
                </td>
                <td class="col-md-6">
                    <label class="control-label text-size-large text-bold text-white"> 文件作廢申請單 / ĐƠN XIN PHẾ BỎ VĂN BẢN </label>
                </td>
            </tr>
        </table>
    </div>

    <div class="panel panel-body border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">

        <table class="table table-borderless">
            <tr>
                <td class="col-lg-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單位 <br /> Đơn vị xin đơn :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationDep" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>

                <td class="col-lg-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單編號 <br /> Mã đơn :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationNO" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請人 <br /> Người xin đơn:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請日期 <br /> Ngày xin đơn:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationDate" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請廠區 <br /> Nhà xưởng xin đơn:</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlApplicationSite" runat="server" CssClass="select-border-color" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">生效日期 <br /> Ngày có hiệu lực:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control border-bottom-blue-800 text-info" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">文件編號 <br /> Mã văn bản: </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtDocNO" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">版本 <br /> Phiên bản:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtREV" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Text="A" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">
                            文件名稱 <br /> Tên văn bản:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">發行日期 <br /> Ngày phát hành: </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtReleaseDate" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>
           <%-- <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-2 text-bold">
                            文件類別 <br /> Loại văn bản
                        </label>
                        <div class="col-lg-10">
                            <asp:DropDownList ID="ddlDocType" runat="server" CssClass="select-border-color" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                </td>
            </tr>--%>

          <%--  <tr>
                <td colspan="2">
                    <div class="form-group" id="applicableSite">
                        <label class="control-label col-lg-2 text-bold">適用廠區 <br /> Nhà xưởng sử dụng: </label>
                        <label class="checkbox-inline">
                            <input type="checkbox">
                        </label>
                    </div>
                </td>
            </tr>--%>

            <%--<tr>
                <td colspan="2">
                    <div class="form-group">
                        <div class="form-group" id="applicableBU">
                            <label class="control-label col-lg-2 text-bold">適用BU <br /> BU sửa dụng: </label>
                            <label class="checkbox-inline">
                                <input type="checkbox" class="styled">
                            </label>
                        </div>
                    </div>
                </td>
            </tr>--%>


            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">
                            部門主管 <br /> Chủ quản bộ phận:
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">
                            核准人 <br /> Người phê duyệt:
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtManager" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                </td>
            </tr>

     <%--       <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-12 text-bold"> 请勾選會簽單位 / Vui lòng chọn đơn vị trình ký</label>
                    </div>
                </td>
            </tr>

            <tr>
                <td id="department" colspan="2">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="checkbox-inline">
                                <input id="chk_" type="checkbox" class="control-label fix">供應鏈管理 / Quản lý chuỗi cung ứng
                            </label>
                        </div>
                    </div>
                </td>
            </tr>--%>

            <tr>
                <td colspan="2">

                    <div class="form-group">
                        <label class="control-label col-lg-12 text-bold">
                            文件作廢原因說明 / Lý do phế bỏ văn bản
                        </label>
                        <div class="col-lg-12">
                            <asp:TextBox ID="txtReasonObsoleted" runat="server" class="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>

                </td>
            </tr>

           <tr id="approval">
                <td colspan="2">
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
            <asp:HiddenField ID="hidUserName" runat="server" />
            <asp:HiddenField ID="hidObsoletedDocument" runat="server" />
            <asp:HiddenField ID="hidStates" runat="server" />
            <asp:HiddenField ID="hidID" runat="server" />
            <%--<asp:HiddenField ID="hidApplicableSite" runat="server" />--%>
            <%--<asp:HiddenField ID="hidApplicableBU" runat="server" />--%>
            <%--<asp:HiddenField ID="hidDepartmentCheck" runat="server" />--%>
            <asp:HiddenField ID="hidPublishDocument" runat="server" />

            <div class="form-group text-center">
                <a id="btnLuu" href="javascript:void(0)" class="btn bg-green btn-rounded width-200" onclick="Luu();"><i class="icon-add position-left"></i>存儲 / Lưu</a>
                <a id="btnLamLai" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="LamLai();"><i class="icon-cancel-circle2 position-left"></i>重置 / Làm lại</a>
                <a id="btnXacNhan" href="javascript:void(0)" class="btn bg-pink btn-rounded width-200" onclick="XacNhan();">確認 / Xác nhận</a>
                <a id="btnHuyBo" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="HuyBo();">取消 / Hủy bỏ</a>
                <a id="btnTuChoi" href="javascript:void(0)" class="btn bg-indigo btn-rounded width-200" onclick="TuChoi();">拒絕 /Từ chối</a>
            </div>
        </div>


    </div>

</asp:Content>

