<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckingNotice.aspx.cs" Inherits="HR_SOP.CheckingNotice" %>

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
            $('#btnSuaVanKien').hide();
            $('.Opinion').hide();

            var checkNO = $('#<%=hidCodeCheck.ClientID%>').val();
            if (checkNO != '') {
                $('#approval').show();
                setTimeout(function () {
                    ListApprovalSection(checkNO);
                    setTimeout(function () {
                        ShowOrHide(checkNO);
                    }, 300);
                }, 300);
            }
            else {
                $('#btnLuu').show();
                $('#btnLamLai').show();
            }
            LoadApplicableSite();
            LoadApplicableBU();

            $('#<%=ddlCurrentDepartment.ClientID%>').change(function () {
                ListDetailUserByDepartment($(this).val());
            });
        });

        function ListDetailUserByDepartment(Department) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListDetailUserByDepartment',
                data: "{ 'Department': '" + Department + "','Position':'C-00004'}",
                async: true,
                success: function (data) {
                    $('#<%=txtCurrentDirector.ClientID%>').val('');
                    $('#<%=hidCurrentDirector.ClientID%>').val('');
                    $.each($.parseJSON(data.d), function (i, v) {
                        $('#<%=txtCurrentDirector.ClientID%>').val(v.HoTen);
                        $('#<%=hidCurrentDirector.ClientID%>').val(v.TenDangNhap);
                    });
                },
                error: function (er) {
                    bootbox.alert("Error system : -" + er);
                }
            });
        }



        function ListApprovalSection(checkNO) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListApprovalSection',
                data: "{ 'CodeDocument': '" + checkNO + "'}",
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
                    bootbox.alert("Error system : -" + er);
                }
            });
        }

        function ShowOrHide(checkNO) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckDisplaySubmitCheckingNotice',
                data: "{ 'CodeCheck': '" + checkNO + "'}",
                async: true,
                success: function (data) {
                    $.each($.parseJSON(data.d), function (i, v) {
                        var states = $('#<%=hidStates.ClientID%>').val();
                        if (states == 'F01') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnLuu').show();
                                $('#btnXacNhan').show();
                                $('#btnHuyBo').show();
                            }
                        }
                        else if (states == 'F05') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnXacNhan').show();
                                $('#btnTuChoi').show();
                                $('.Opinion').show();
                            }
                        }
                        else if (states == 'F10') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnSuaVanKien').show();
                            }
                        }
                        else if (states == 'F20') {
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
                            $('.Opinion').hide();
                        }
                    })
                },
                error: function (er) {
                    bootbox.alert("Error.");
                }
            });
}

function LoadApplicableSite() {
    $('#applicableSite').empty();
    var html = '';
    html += '<label class="control-label col-lg-2 text-bold">適用廠區 (Applicable Site): </label>';
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
}

function LoadApplicableBU() {
    $('#applicableBU').empty();
    var html = '';
    html += '<label class="control-label col-lg-2 text-bold">適用 BU (Applicable BU): </label>';
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
}



function Luu() {

    var ID = $('#<%=hidID.ClientID%>').val();
    var CodeCheck = $('#<%=txtApplicationNO.ClientID%>').val();
    var PublishDocument = $('#<%=hidPublishDocument.ClientID%>').val();
    var DocumentNo = $('#<%=txtDocNO.ClientID%>').val();

    var sApplicationDate = $('#<%=txtApplicationDate.ClientID%>').val();
    var Department = $('#<%=ddlCurrentDepartment.ClientID%>').val();
    var Director = $('#<%=hidCurrentDirector.ClientID%>').val();
    var ApplicationSite = $('#<%=ddlApplicationSite.ClientID%>').val();

    if ($('#<%=ddlCurrentDepartment.ClientID%>').val() == '' || $('#<%=ddlCurrentDepartment.ClientID%>').val() == 'ALL') {
        bootbox.alert('Vui lòng chọn bộ phận/ 請選擇部門');
        $('#<%=ddlCurrentDepartment.ClientID%>').focus();
        return;
    }
    else if ($('#<%=txtCurrentDirector.ClientID%>').val() == '') {
        bootbox.alert('Không có chủ quản ký/ 無主官簽核');
        $('#<%=txtCurrentDirector.ClientID%>').focus();
        return;
    }
    else {

        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            url: 'Models/WebServiceDB.asmx/InsertOrUpdateCheckingNotice',
            data: "{ 'ID': '" + ID + "', 'CodeCheck': '" + CodeCheck + "', 'PublishDocument': '" + PublishDocument + "', 'DocumentNo': '" + DocumentNo
                + "', 'sApplicationDate': '" + sApplicationDate + "', 'Department': '" + Department + "', 'Director': '" + Director + "', 'ApplicationSite': '" + ApplicationSite + "'}",
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
                        text: "存儲成功 / Lưu dữ liệu thành công!",
                        confirmButtonColor: "#66BB6A",
                        type: "success"
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            location.href = 'CheckingNotice.aspx?CodeCheck=' + $.parseJSON(data.d);
                        }
                    });
                }
            },
            error: function (er) {
                bootbox.alert("Error system : -" + er);
            }
        });
    }
}


function LamLai() {
    swal({
        title: "Bạn có chắc chắn muốn thực hiện?/ 你確認想操作？",
        text: "Phiếu đăng ký sẽ xóa bỏ tất cả thông tin đã nhập!/ 登記票被刪除消息!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#FF7043",
        confirmButtonText: "是 / Có",
        showLoaderOnConfirm: true
    },
    function (isConfirm) {
        if (isConfirm) {
            location.href = $(location).attr('href');
        }
    });
}


function XacNhan() {

    if ($('#<%=txtCloseDate.ClientID%>').val() == '' && $('#<%=hidStates.ClientID%>').val() == 'F05') {
        bootbox.alert('Nhập ngày gia hạn/ 延期日輸入');
        $('#<%=txtCloseDate.ClientID%>').focus();
        return;
    }
    else if ($('#<%=txtReason.ClientID%>').val() == '' && $('#<%=hidStates.ClientID%>').val() == 'F05') {
        bootbox.alert('Vui lòng nhập lý do/ 請理由輸入');
        $('#<%=txtReason.ClientID%>').focus();
        return;
    }
    else {

        swal({
            title: "您是否想操作？ / Bạn có chắc chắn muốn thực hiện?",
            text: "表單傳承下一步 / Phiếu đăng ký này sẽ chuyển tiếp.",
            type: "input",
            showCancelButton: true,
            confirmButtonColor: "#2196F3",
            closeOnConfirm: false,
            confirmButtonText: "是 / Có",
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

                var Opinion = 'N';
                if ($('#uniform-chkOpinion span').attr('class') == 'checked') {
                    Opinion = 'Y';
                }
                else {
                    Opinion = 'N';
                }

                var Person = $('#<%=ddlPerson.ClientID%>').val();
                var EstimateCloseDate = $('#<%=txtCloseDate.ClientID%>').val();
                var Reason = $('#<%=txtReason.ClientID%>').val();

                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    url: 'Models/WebServiceDB.asmx/AcceptCheckingNotice',
                    data: "{ 'CodeCheck': '" + $('#<%=hidCodeCheck.ClientID%>').val() + "','States': '" + $('#<%=hidStates.ClientID%>').val()
                    + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "','Opinion':'" + Opinion
                    + "','Person':'" + Person + "','sEstimateCloseDate':'" + EstimateCloseDate + "','Reason':'" + Reason + "'}",
                    async: true,
                    success: function (data) {
                        if ($.parseJSON(data.d) == 'SUCCESS') {
                            location.href = "ListCheckingNotice.aspx";
                        }
                        else {
                            bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                        }
                    },
                    error: function (er) {
                        bootbox.alert("Error system : -" + er);
                    }
                });
            }

        });

}
}


function HuyBo() {

    swal({
        title: "您是否想操作？ / Bạn có chắc chắn muốn thực hiện?",
        text: "表單會被刪除 / Phiếu đăng ký này sẽ bị xóa khỏi hệ thống.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#FF7043",
        confirmButtonText: "是 / Có",
        showLoaderOnConfirm: true
    },
    function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/DeletedCheckingNotice',
                data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListCheckingNotice.aspx";
                    }
                    else {
                        bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                    }
                },
                error: function (er) {
                    bootbox.alert("Error system : -" + er);
                }
            });
        }
    });
}


function TuChoi() {
    swal({
        title: "您是否想操作？ / Bạn có chắc chắn muốn thực hiện?",
        text: "登記表轉至承辦人 / Phiếu đăng ký này sẽ chuyển lại cho người làm đơn.",
        type: "input",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        closeOnConfirm: false,
        confirmButtonText: "是 / Có",
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
            url: 'Models/WebServiceDB.asmx/RejectCheckingNotice',
            data: "{ 'CodeCheck': '" + $('#<%=hidCodeCheck.ClientID%>').val() + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "'}",
            async: true,
            success: function (data) {
                if ($.parseJSON(data.d) == 'SUCCESS') {
                    location.href = "ListPublishDocument.aspx";
                }
                else {
                    bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                }
            },
            error: function (er) {
                bootbox.alert("Error system : -" + er);
            }
        });
    }
});
}

function SuaVanKien() {
    var url = $(location).attr('protocol') + '//' + $(location).attr('host') + '/RegisterEditDocument.aspx?Type=Check&PublishDocument=' + $('#<%=hidPublishDocument.ClientID%>').val().trim();
    window.open(url);
}
    </script>


    <div class="panel panel-body border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300 bg-grey-300">
        <table class="col-md-12">
            <tr>
                <td class="col-md-6">
                    <img src="Images/fc_logo.png" alt="" height="40" />
                </td>
                <td class="col-md-6">
                    <label class="control-label text-size-large text-bold text-white">SOP 檢查單 / ĐƠN KIỂM TRA SOP</label>
                </td>
            </tr>
        </table>
    </div>

    <div class="panel panel-body border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">

        <table class="table table-borderless">

            <tr>
                <td class="col-lg-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單位 <br /> (Application Dep) :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>

                <td class="col-lg-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單編號 <br /> (Application No) :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationNO" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請人 <br /> (Applicant) :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請日期 <br /> (Application Date) :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationDate" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">文件編號 <br /> (Doc No.) </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtDocNO" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="hidPublishDocument" runat="server" />
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">版本 <br /> (Rev)</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtREV" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-2 text-bold">
                            文件名稱 <br /> (Doc Name)</label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請廠區 <br /> (Application Site):</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlApplicationSite" runat="server" CssClass="select-border-color" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">文件類 <br /> (Doc Type)別</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlDocType" runat="server" CssClass="select-border-color" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group" id="applicableSite">
                        <label class="control-label col-lg-2 text-bold">適用廠區 (Applicable Site): </label>
                        <label class="checkbox-inline">
                            <input type="checkbox">
                        </label>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <div class="form-group" id="applicableBU">
                            <label class="control-label col-lg-2 text-bold">適用 BU (Applicable BU): </label>
                            <label class="checkbox-inline">
                                <input type="checkbox" class="styled">
                            </label>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-2 text-bold">
                            原修訂單位 <br /> (Original responsible dept.)
                        </label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtOriginalResponsible" runat="server" CssClass="form-control border-bottom-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">承接修訂單位 <br /> (Current responsible dept.)</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlCurrentDepartment" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">承接修訂部主管 <br /> (Dircetor of current responsible dept.)</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtCurrentDirector" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="hidCurrentDirector" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-sm-2 text-bold">
                            說明 <br /> (Remark)
                        </label>
                        <asp:Label ID="txtRemark" runat="server" Text="Label" class="col-sm-10 control-label"></asp:Label>
                    </div>
                </td>
            </tr>

            <tr class="Opinion">
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-sm-2 text-bold">
                            需修訂(Yes) 
                        </label>
                        <label class="display-block text-semibold col-sm-1">
                            <input id="chkOpinion" type="checkbox" class="styled">
                        </label>
                    </div>
                </td>
            </tr>

            <tr class="Opinion">
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">表單修改人/ Responsible person</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlPerson" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-5 text-bold">到期日/ Estimate closed date</label>
                        <div class="col-lg-7">
                            <asp:TextBox ID="txtCloseDate" runat="server" CssClass="form-control border-bottom-blue-800 daterange-single"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr class="Opinion">
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-sm-2 text-bold">
                            理由/ Reason
                        </label>
                        <label class="col-sm-10">
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"></asp:TextBox>
                        </label>
                    </div>
                </td>
            </tr>




            <tr id="approval">
                <td colspan="2">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <td colspan="8">
                                    <label class="control-label text-bold">Approval Section : </label>
                                </td>
                            </tr>
                            <tr>

                                <th class="text-bold">Station</th>
                                <th class="text-bold">Dept</th>
                                <th class="text-bold">Name</th>
                                <th class="text-bold">Ext</th>
                                <th class="text-bold">Date & Time</th>
                                <th class="text-bold">Status</th>
                                <th class="text-bold">Time(hrs)</th>
                                <th class="text-bold">Comment</th>

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
            <asp:HiddenField ID="hidCodeCheck" runat="server" />
            <asp:HiddenField ID="hidStates" runat="server" />
            <asp:HiddenField ID="hidID" runat="server" />

            <asp:HiddenField ID="hidApplicableSite" runat="server" />
            <asp:HiddenField ID="hidApplicableBU" runat="server" />

            <div class="form-group text-center">

                <a id="btnLuu" href="javascript:void(0)" class="btn bg-green btn-rounded width-200" onclick="Luu();"><i class="icon-add position-left"></i>存儲 / Lưu</a>
                <a id="btnLamLai" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="LamLai();"><i class="icon-cancel-circle2 position-left"></i>重置 / Làm lại</a>
                <a id="btnXacNhan" href="javascript:void(0)" class="btn bg-pink btn-rounded width-200" onclick="XacNhan();">確認 / Xác nhận</a>
                <a id="btnHuyBo" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="HuyBo();">取消 / Hủy bỏ</a>
                <a id="btnTuChoi" href="javascript:void(0)" class="btn bg-indigo btn-rounded width-200" onclick="TuChoi();">拒絕 / Từ chối</a>
                <a id="btnSuaVanKien" href="javascript:void(0)" class="btn bg-pink btn-rounded width-200" onclick="SuaVanKien();">Sửa văn kiện</a>

            </div>
        </div>

    </div>


</asp:Content>
