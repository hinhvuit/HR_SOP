<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RenewalsDocument.aspx.cs" Inherits="HR_SOP.RenewalsDocument" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            $('#approval').hide();
            $('#btnLuu').hide();
            $('#btnHuyBo').hide();
            $('#btnLamLai').hide();
            $('#btnXacNhan').hide();
            $('#btnTuChoi').hide();
            $('#Directer').hide();
            var RenewalCode = $('#<%=hidRenewalCode.ClientID%>').val();
            if (RenewalCode != '') {
                $('#approval').show();
                setTimeout(function () {
                    ListApprovalSection(RenewalCode);
                    setTimeout(function () {
                        ShowOrHide(RenewalCode);
                    }, 300);
                }, 300);
            }
            else {
                $('#btnLuu').show();
                $('#btnLamLai').show();
            }

        });

        function ListApprovalSection(RenewalCode) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListApprovalSection',
                data: "{ 'CodeDocument': '" + RenewalCode + "'}",
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

        function ShowOrHide(RenewalCode) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckDisplaySubmitRenewalsDocument',
                data: "{ 'RenewalCode': '" + RenewalCode + "'}",
                async: true,
                success: function (data) {
                    $.each($.parseJSON(data.d), function (i, v) {
                        var states = $('#<%=hidStates.ClientID%>').val();
                        if (states == 'H01') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnLuu').show();
                                $('#btnXacNhan').show();
                                $('#btnHuyBo').show();
                            }
                        }
                        else if (states == 'H05' || states == 'H10' || states == 'H15') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnXacNhan').show();
                                $('#btnTuChoi').show();
                            }
                            if (states == 'H05') {
                                $('#Directer').show();
                            }
                        }
                        else if (states == 'F25') {
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
                            $('#Directer').hide();
                        }
                    })
                },
                error: function (er) {
                    bootbox.alert("Error.");
                }
            });
}


function Luu() {
    var ID = $('#<%=hidID.ClientID%>').val();
    var RenewalCode = $('#<%=txtApplicationNO.ClientID%>').val();
    var sApplicationDate = $('#<%=txtApplicationDate.ClientID%>').val();
    var ApplicationSite = $('#<%=ddlApplicationSite.ClientID%>').val();
    var TypeRenewal = $('#<%=ddlType.ClientID%>').val();

    var DocumentNo = $('#<%=txtApplicationCode.ClientID%>').val();
    var DCC_NO = $('#<%=txtDocNO.ClientID%>').val();
    var Reason = $('#<%=txtReason.ClientID%>').val();
    var Revisor = $('#<%=txtAfterRevisor.ClientID%>').val();
    var sCloseDate = $('#<%=txtAfterCloseDate.ClientID%>').val();

    var Department = $('#<%=ddlDepartment.ClientID%>').val();
    var BeforRevised = $('#<%=txtRevised.ClientID%>').val();
    var BeforRevisor = $('#<%=txtRevisor.ClientID%>').val();
    var BeforCloseDate = $('#<%=txtCloseDate.ClientID%>').val();
    var Type = $('#<%=hidType.ClientID%>').val();

    if ($('#<%=txtReason.ClientID%>').val() == '') {
        bootbox.alert('Nhập lý do / 理由輸入');
        $('#<%=txtReason.ClientID%>').focus();
        return;
    }
    else if ($('#<%=txtAfterRevisor.ClientID%>').val() == '') {
        bootbox.alert('Nhập người duyệt lại / 批准人輸入');
        $('#<%=txtAfterRevisor.ClientID%>').focus();
        return;
    }
    else if ($('#<%=txtAfterCloseDate.ClientID%>').val() == '') {
        bootbox.alert('Nhập ngày hết hạn / 到期日輸入');
        $('#<%=txtAfterCloseDate.ClientID%>').focus();
        return;
    }
    else {
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            url: 'Models/WebServiceDB.asmx/InsertOrUpdateRenewalsDocument',
            data: "{ 'ID': '" + ID + "', 'RenewalCode': '" + RenewalCode + "', 'sApplicationDate': '" + sApplicationDate + "', 'ApplicationSite': '" + ApplicationSite
                + "', 'TypeRenewal': '" + TypeRenewal + "', 'DocumentNo': '" + DocumentNo + "', 'DCC_NO': '" + DCC_NO + "', 'Reason': '" + Reason
                + "', 'Revisor': '" + Revisor + "', 'sCloseDate': '" + sCloseDate + "', 'Department': '" + Department
                + "', 'BeforRevised': '" + BeforRevised + "', 'BeforRevisor': '" + BeforRevisor + "', 'BeforCloseDate': '" + BeforCloseDate + "', 'Type': '" + Type + "'}",
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
                            location.href = 'RenewalsDocument.aspx?RenewalCode=' + $.parseJSON(data.d);
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
        title: "Bạn có chắc chắn muốn thực hiện? / 你確定想操作？",
        text: "Phiếu đăng ký sẽ xóa bỏ tất cả thông tin đã nhập! / 登記表單將被刪除所有消息!",
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
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/AcceptRenewalsDocument',
                data: "{ 'RenewalCode': '" + $('#<%=hidRenewalCode.ClientID%>').val() + "','States': '" + $('#<%=hidStates.ClientID%>').val()
                    + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "','Person':'" + $('#<%=ddlDirecter.ClientID%>').val() + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListRenewalsDocument.aspx";
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
                url: 'Models/WebServiceDB.asmx/DeletedRenewalsDocument',
                data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListRenewalsDocument.aspx";
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
            url: 'Models/WebServiceDB.asmx/RejectRenewalsDocument',
            data: "{ 'RenewalCode': '" + $('#<%=hidRenewalCode.ClientID%>').val() + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "'}",
            async: true,
            success: function (data) {
                if ($.parseJSON(data.d) == 'SUCCESS') {
                    location.href = "ListRenewalsDocument.aspx";
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

    </script>

    <div class="panel panel-body border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300 bg-grey-300">
        <table class="col-md-12">
            <tr>
                <td class="col-md-6">
                    <img src="Images/fc_logo.png" alt="" height="40" />
                </td>
                <td class="col-md-6">
                    <label class="control-label text-size-large text-bold text-white">文件延期登記 / ĐĂNG KÝ GIA HẠN VĂN KIỆN</label>
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
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="select-border-color"></asp:DropDownList>
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
                        <label class="control-label col-lg-4 text-bold">申請廠區 <br /> (Application Site):</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlApplicationSite" runat="server" CssClass="select-border-color" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">生效日期 <br /> (Effective Date):</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-3 text-bold">
                            單據別 <br /> (Type of postponed closing application)
                        </label>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="select-border-color">
                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                <asp:ListItem Value="CODE">Xin mã văn kiện</asp:ListItem>
                                <asp:ListItem Value="EDIT">Xin sửa văn kiện</asp:ListItem>
                                <asp:ListItem Value="CHECK">Xin kiểm tra văn kiện</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單編號 <br /> (Application No.) </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationCode" runat="server" CssClass="form-control border-bottom-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-5 text-bold">文件編號 <br /> (Doc No.)</label>
                        <div class="col-lg-7">
                            <asp:TextBox ID="txtDocNO" runat="server" CssClass="form-control border-bottom-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-3 text-bold">
                            原應修訂文件 <br /> (Document to be revised)
                        </label>
                        <div class="col-lg-9">
                            <asp:TextBox ID="txtRevised" runat="server" CssClass="form-control border-bottom-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">原指定修訂人 <br /> (Assigned Revisor) </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtRevisor" runat="server" CssClass="form-control border-bottom-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">原預計結案日 <br /> (Estimated close date)</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtCloseDate" runat="server" CssClass="form-control border-bottom-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-3 text-bold">
                            申請原因 <br /> (Reason of Application)
                        </label>
                        <div class="col-lg-9">
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                            
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">[指定修訂人 <br /> (Assigned Revisor) </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtAfterRevisor" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">預計結案日 <br /> (Estimated close date)</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtAfterCloseDate" runat="server" CssClass="form-control border-bottom-blue-800 daterange-single"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr id="Directer">
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-3 text-bold">修改主管/ Chủ quản sửa</label>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="ddlDirecter" runat="server" CssClass="select-border-color"></asp:DropDownList>
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

            <asp:HiddenField ID="hidType" runat="server" />
            <asp:HiddenField ID="hidUserName" runat="server" />
            <asp:HiddenField ID="hidRenewalCode" runat="server" />
            <asp:HiddenField ID="hidStates" runat="server" />
            <asp:HiddenField ID="hidID" runat="server" />

            <div class="form-group text-center">

                <a id="btnLuu" href="javascript:void(0)" class="btn bg-green btn-rounded width-200" onclick="Luu();"><i class="icon-add position-left"></i>存儲 / Lưu</a>
                <a id="btnLamLai" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="LamLai();"><i class="icon-cancel-circle2 position-left"></i>重置 / Làm lại</a>
                <a id="btnXacNhan" href="javascript:void(0)" class="btn bg-pink btn-rounded width-200" onclick="XacNhan();">確認 / Xác nhận</a>
                <a id="btnHuyBo" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="HuyBo();">取消 / Hủy bỏ</a>
                <a id="btnTuChoi" href="javascript:void(0)" class="btn bg-indigo btn-rounded width-200" onclick="TuChoi();">拒絕 / Từ chối</a>

            </div>
        </div>

    </div>


</asp:Content>

