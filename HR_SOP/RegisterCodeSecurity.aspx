<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterCodeSecurity.aspx.cs" Inherits="HR_SOP.RegisterCodeSecurity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <style type="text/css">
        .fix {
            width: 20px !important;
            height: 20px !important;
            margin: 0px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //$('#trOpinion').hide();
            $('#approval').hide();
            $('#btnLuu').hide();
            $('#btnHuyBo').hide();
            $('#btnLamLai').hide();
            $('#btnXacNhan').hide();
            $('#btnTuChoi').hide();
            $('#tr_dcc_ref').hide();
            var docNO = $('#<%=hidCodeDocument.ClientID%>').val();
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

            $('#FormAddDoc').dialog({
                dialogClass:'my_dialog',
                autoOpen: false,
                modal: true,
                width: 600,
                buttons: {
                    Submit: function () {
                        if ($('#<%=txtDocName.ClientID%>').val() == '') {
                            bootbox.alert('請登入姓名 \ Vui lòng nhập tên');
                            $('#<%=txtDocName.ClientID%>').focus();
                            return;
                        }
                        else if ($('#FileName').val() == '') {
                            bootbox.alert('請選擇資料附件 \ Vui lòng chọn file tài liệu');
                            $('#FileName').focus();
                            return;
                        }
                        else {
                            var files = $('#FileName').get(0).files;
                            var ext = files[0].name.replace(/^.*\./, '');
                            if (ext != 'doc' && ext != 'docx' && ext != 'xls' && ext != 'xlsx') {
                                bootbox.alert('Chỉ upload file (.doc,.docx,.xls,.xlsx)');
                                return;
                            }
                            else if (files[0].size > 26214400) {
                                bootbox.alert('請選擇資料附件 \ Vui lòng chọn file tài liệu <= 25MB');
                                return;
                            }
                            else {
                                var data = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    data.append("FileName", files[i]);
                                }
                                $.ajax({
                                    type: 'POST',
                                    contentType: false,
                                    processData: false,
                                    dataType: 'text',
                                    url: 'Models/WebServiceDB.asmx/SaveFileCodeDoc',
                                    data: data,
                                    async: true,
                                    success: function (data) {
                                        var order = 0;
                                        var temp = $('#ContentDoc tr:last td').html();
                                        if (!isFinite(temp) || temp == 'undefined') {
                                            order = 1;
                                        }
                                        else {
                                            order = parseInt(temp) + 1;
                                        }
                                        var ten = '';
                                        var ten1 = data.replace("</string>", "").split('>');
                                        if (ten1.length > 1) {
                                            ten = ten1[ten1.length - 1];
                                        }
                                        if (ten != '') {
                                            var html = '';
                                            html = '<tr id="tr_' + order + '">';
                                            html += '<td>' + order + '</td>';
                                            html += '<td>' + $('#<%=txtDocName.ClientID%>').val() + '</td>';
                                            html += '<td> <a href="javascript:void(0)" value="' + ten + '" onclick="ShowDoc(this);">' + ten + '</a></td>';
                                            <%--html += '<td>' + $('#<%=txtAssinedRevisor.ClientID%>').val() + '</td>';--%>
                                            html += '<td>' + $('#<%=txtCloseDate.ClientID%>').val() + '</td>';
                                            html += '<td class="text-center">';
                                            html += '    <ul class="icons-list">';
                                            html += '        <li class="text-danger-600"><a href="javascript:void(0)" value="' + order + '" onclick="DeleteRow(this);" title="Delete"><i class="icon-trash"></i></a></li>';
                                            html += '    </ul>';
                                            html += '</td>';
                                            html += '</tr>';
                                            $('#ContentDoc').append(html);
                                            $('#FormAddDoc').dialog('close');
                                        }
                                        else {
                                            bootbox.alert('上傳錯誤文檔 / Upload file lỗi');
                                        }
                                    },
                                    error: function (er) {
                                        bootbox.alert(er);
                                    }
                                });
                            }
                    }
                    },
                    Cancel: function () {
                        $(this).dialog('close');
                    }
                }
            });

            $('#addDoc').click(function () {
                $('#<%=txtDocName.ClientID%>').val('');
                <%--$('#<%=txtAssinedRevisor.ClientID%>').val('');--%>
                $('#<%=txtCloseDate.ClientID%>').val('');
                $('.filename').text('');
                $('#FormAddDoc').dialog('open');
            });



            $('#FormAddDCC').dialog({
                dialogClass: 'my_dialog',
                autoOpen: false,
                modal: true,
                width: 600,
                buttons: {
                    Submit: function () {
                        if ($('#<%=txtDCC_NO.ClientID%>').val() == '') {
                            bootbox.alert('請登入密編號 \ Vui lòng nhập mã');
                            $('#<%=txtDCC_NO.ClientID%>').focus();
                            return;
                        }
                        else if ($('#<%=txtDCC_NAME.ClientID%>').val() == '') {
                            bootbox.alert('請登入姓名 \ Vui lòng nhập tên');
                            $('#<%=txtDCC_NAME.ClientID%>').focus();
                            return;
                        }
                        else {
                            var insert = 'T';
                            $('#ContentDCC_Ref tr:last td').each(function (i) {
                                if (i == 1) {
                                    if ($(this).html() == $('#<%=txtDCC_NO.ClientID%>').val()) {
                                        bootbox.alert('DCC號已存在，請登入別的編號 \ Mã DCC đã tồn tại vui lòng nhập mã khác');
                                        insert = '';
                                        return;
                                    }
                                    else {
                                        insert = 'T';
                                    }
                                }
                            });
                            setTimeout(function () {
                                if (insert == 'T') {
                                    $.ajax({
                                        type: 'POST',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        url: 'Models/WebServiceDB.asmx/CheckExistDocNoSe',
                                        data: "{ 'DocNo': '" + $('#<%=txtDCC_NO.ClientID%>').val() + "'}",
                                        async: true,
                                        success: function (data) {
                                            $.each($.parseJSON(data.d), function (i, v) {
                                                if (v.SL > 0) {
                                                    bootbox.alert('DCC號已存在，請登入別的編號 \ Mã DCC đã tồn tại vui lòng nhập mã khác');
                                                    $('#<%=txtDCC_NO.ClientID%>').focus();
                                                    return;
                                                }
                                                else {
                                                    var order = 0;
                                                    var temp = $('#ContentDCC_Ref tr:last td').attr('order');
                                                    if (!isFinite(temp) || temp == 'undefined') {
                                                        order = 1;
                                                    }
                                                    else {
                                                        order = parseInt(temp) + 1;
                                                    }


                                                    var html = '';
                                                    html = '<tr id="tr_dcc_' + order + '">';
                                                    html += '<td value="' + $('#<%=hidID_DocumentRef.ClientID%>').val() + '" order="' + order + '" >' + order + ' - ' + $('#<%=hidOrder.ClientID%>').val() + '</td>';
                                                    html += '<td>' + $('#<%=txtDCC_NO.ClientID%>').val() + '</td>';
                                                    html += '<td>' + $('#<%=txtDCC_NAME.ClientID%>').val() + '</td>';
                                                    html += '<td class="text-center">';
                                                    html += '    <ul class="icons-list">';
                                                    html += '        <li class="text-danger-600"><a href="javascript:void(0)" value="' + order + '" onclick="DeleteRowDCC(this);" title="Delete"><i class="icon-trash"></i></a></li>';
                                                    html += '    </ul>';
                                                    html += '</td>';
                                                    html += '</tr>';

                                                    $('#ContentDCC_Ref').append(html);
                                                    $('#FormAddDCC').dialog('close');
                                                }
                                            });
                                        },
                                        error: function (er) {
                                            bootbox.alert(er);
                                        }
                                    });
                                }
                            }, 300);
                        }
                    },
                    Cancel: function () {
                        $(this).dialog('close');
                    }
                }
            });

            $('.my_dialog .ui-button-text:contains(Cancel)').text('拒絕 / Từ chối');
            $('.my_dialog .ui-button-text:contains(Submit)').text('存儲 / Lưu');

            LoadApplicableSite();
            //LoadApplicableBU();

            setTimeout(function () {
                LoadDocumentRef($('#<%=hidCodeDocument.ClientID%>').val());
                setTimeout(function () {
                    var states = $('#<%=hidStates.ClientID%>').val();
                    if (states == 'A10') {
                        $('#tr_dcc_ref').show();
                    }
                    else if (states == 'A15') {
                        $('#tr_dcc_ref').show();
                        LoadListDCC_Ref($('#<%=hidCodeDocument.ClientID%>').val());
                    }
                    else {
                        $('#tr_dcc_ref').hide();
                    }
                }, 400);
            }, 800);

        });

    function ShowAddDCC(row) {
        $('#<%=hidID_DocumentRef.ClientID%>').val($(row).attr('value').trim());
        $('#<%=hidOrder.ClientID%>').val($(row).attr('order').trim());
        $('#<%=txtDCC_NO.ClientID%>').val('');
        $('#<%=txtDCC_NAME.ClientID%>').val('');
        $('#FormAddDCC').dialog('open');
    }

        function ShowOrHide(CodeDocumnet) {
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            url: 'Models/WebServiceDB.asmx/CheckDisplaySubmitCodeSecurity',
            data: "{ 'CodeDocument': '" + CodeDocumnet + "'}",
            async: true,
            success: function (data) {
                $.each($.parseJSON(data.d), function (i, v) {
                    var states = $('#<%=hidStates.ClientID%>').val();
                    if (states == 'A01') {
                        console.log(v.UserName + $('#<%=hidUserName.ClientID%>').val());

                        if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                            //$('#trOpinion').show();
                            $('#btnLuu').show();
                            $('#btnXacNhan').show();
                            $('#btnHuyBo').show();
                        }
                    }
                    else if (states == 'A03' || states == 'A05' || states == 'A10') {
                        if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                            //$('#trOpinion').show();
                            $('#btnXacNhan').show();
                            $('#btnTuChoi').show();
                        }
                    }
                    else if (states == 'A20') {
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
                bootbox.alert("Error.");
            }
        });
}

function ShowDoc(row) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/CheckExistsFileCodeSecurity',
        data: "{ 'FileName': '" + $(row).attr('value').trim() + "'}",
        async: true,
        success: function (data) {
            if ($.parseJSON(data.d) == 'EXISTED') {
                var url = $(location).attr('protocol') + '//' + $(location).attr('host') + '/Updatafile/CodeDoc/' + $(row).attr('value').trim();
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

function LoadDocumentRef(CodeDocumnet) {
    var states = $('#<%=hidStates.ClientID%>').val();
    if (states == 'A03' || states == 'A05' || states == 'A10' || states == 'A15') {
        $('#addDoc').hide();
    }

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListSecurityRef',
        data: "{ 'CodeDocument': '" + CodeDocumnet + "'}",
        async: true,
        success: function (data) {
            $('#ContentDoc').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                html += '<tr id="tr_' + (i + 1) + '">';
                html += '<td>' + (i + 1) + '</td>';
                html += '<td>' + v.DocumentName + '</td>';
                html += '<td> <a href="javascript:void(0)" value="' + v.FileName + '" onclick="ShowDoc(this);">' + v.FileName + '</a></td>';
                //html += '<td>' + v.AssignedRevisor + '</td>';
                html += '<td>' + v.EstimatedCloseDate + '</td>';
                html += '<td class="text-center">';
                html += '    <ul class="icons-list">';
                if (states == 'A01' || states == 'A20') {
                    html += '        <li class="text-danger-600"><a href="javascript:void(0)" value="' + (i + 1) + '" onclick="DeleteRow(this);" title="Delete"><i class="icon-trash"></i></a></li>';
                }
                else if (states == 'A10') {
                    html += '        <li class="text-danger-600"><a href="javascript:void(0)" class="label label-info" order="' + (i + 1) + '" value="' + v.ID + '" onclick="ShowAddDCC(this);" title="Add Code DCC"><i class="icon-plus2"></i>DCC</a></li>';
                }
                else {
                    html += '        <li><a href="javascript:void(0)" title="Delete"><i class="icon-lock"></i></a></li>';
                }
                html += '    </ul>';
                html += '</td>';
                html += '</tr>';
            })

            $('#ContentDoc').append(html);

        },
        error: function (er) {
            bootbox.alert("Error.");
        }
    });
}

function DeleteRow(row) {
    $('#tr_' + $(row).attr('value') + ' td').each(function (i) {
        if (i == 2) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/DeleteFileCodeDoc',
                data: "{ 'FileName': '" + $(this).text() + "'}",
                async: true,
                success: function (data) {
                    $('#tr_' + $(row).attr('value')).remove();
                },
                error: function (er) {
                    bootbox.alert("Error");
                }
            });
        }
    });
}

function DeleteRowDCC(row) {
    $('#tr_dcc_' + $(row).attr('value')).remove();
}

function LoadListDCC_Ref(CodeDocumnet) {

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListDCC_RefSe',
        data: "{ 'CodeDocument': '" + CodeDocumnet + "','Status':''}",
        async: true,
        success: function (data) {
            $('#ContentDCC_Ref').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                html += '<tr id="tr_dcc_' + (i + 1) + '">';
                html += '<td>' +  (i + 1) + ' - ' + (i + 1) + '</td>';
                html += '<td>' + v.DocNo + '</td>';
                html += '<td>' + v.DocName + '</td>';
                html += '<td class="text-center">';
                html += '    <ul class="icons-list">';
                html += '        <li><a href="javascript:void(0)" title="Delete"><i class="icon-lock"></i></a></li>';
                html += '    </ul>';
                html += '</td>';
                html += '</tr>';
            })

            $('#ContentDCC_Ref').append(html);

        },
        error: function (er) {
            bootbox.alert("Error.");
        }
    });
}

var ApplicableSite = '';
var ApplicableBU = '';
var SeCheck = '';
function CheckSe(row) {
    if ($(row).is(':checked')) {
        SeCheck += $(row).attr('value') + ',';
    }
    else {
        SeCheck = SeCheck.replace($(row).attr('value') + ',', '');
    }
}

function LoadApplicableSite() {
    $('#applicableSite').empty();
    var html = '';
    html += '<label class="control-label col-lg-2 text-bold">適用廠區 <br /> Nhà xưởng sử dụng: </label>';
    $('#<%=ddlApplicationSite.ClientID%> option ').each(function (ind) {
        html += '<label class="checkbox-inline">';
        html += '<input id="chk_' + $(this).val() + '" value="' + $(this).val() + '" type="checkbox" class="control-label fix" onclick="CheckRowSite(this);">' + $(this).text() + '';
        html += '</label>';
    });
    $('#applicableSite').append(html);

    var site = $('#<%=hidApplicableSite.ClientID%>').val().split(',');
    if (site.length > 1) {
        for (var i = 0; i < site.length; i++) {
            if (site[i] != '') {
                $('#chk_' + site[i] + '').prop('checked', true);
                ApplicableSite = ApplicableSite + site[i] + ',';
            }
        }
    }
    else {
        $('#chk_C-00005').prop('checked', true);
        ApplicableSite = 'C-00005,';
    }
}

function CheckRowSite(row) {
    if ($(row).is(':checked')) {
        ApplicableSite += $(row).attr('value') + ',';
    }
    else {
        ApplicableSite = ApplicableSite.replace($(row).attr('value') + ',', '');
    }
}

<%--function LoadApplicableBU() {
    $('#applicableBU').empty();
    var html = '';
    html += '<label class="control-label col-lg-2 text-bold">適用BU / BU sửa dụng: </label>';
    $('#<%=ddlDocType.ClientID%> option ').each(function (ind) {
        html += '<label class="checkbox-inline">';
        html += '<input id="chk_' + $(this).val() + '" value="' + $(this).val() + '" type="checkbox" class="control-label fix" onclick="CheckRowSite(this);"> ' + $(this).text() + '';
        html += '</label>';
    });
    $('#applicableBU').append(html);

    var bu = $('#<%=hidApplicableBU.ClientID%>').val().split(',');
    if (bu.length > 1) {
        for (var i = 0; i < bu.length; i++) {
            if (bu[i] != '') {
                $('#chk_' + bu[i] + '').prop('checked', true);
                ApplicableBU = ApplicableBU + bu[i] + ',';
            }
        }
    }
    else {
        $('#chk_C-00006').prop('checked', true);
        ApplicableBU = 'C-00006,';
    }
}--%>


//function CheckRowBU(row) {
//    if ($(row).is(':checked')) {
//        ApplicableBU += $(row).attr('value') + ',';
//    }
//    else {
//        ApplicableBU = ApplicableBU.replace($(row).attr('value') + ',', '');
//    }
//}

function Luu() {
    if ($('#ContentDoc tr').length == 0) {
        bootbox.alert("請登入最少一個資料 / Vui lòng nhập ít nhật 1 tài liệu");
        return;
    }
    else if ($('#<%=ddlDepartment.ClientID%>').val() == null) {
        bootbox.alert("選擇簽核主管 / Chọn chủ quản ký");
        $('#<%=ddlDepartment.ClientID%>').focus();
        return;
    }
    else {
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            url: 'Models/WebServiceDB.asmx/InsertOrUpdateRegisterCodeSecurity',
            data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "', 'CodeDocument': '" + $('#<%=txtApplicationNO.ClientID%>').val() + "', 'ApplicationSite': '" + $('#<%=ddlApplicationSite.ClientID%>').val()
               + "', 'sEffectiveDate': '" + $('#<%=txtEffectiveDate.ClientID%>').val() + "', 'DocumentType': '', 'ReasonApplication': '"
               + $('#<%=txtReasonApplication.ClientID%>').val() + "', 'ApplicableSite': '" + ApplicableSite + "', 'ApplicableBU': '" + ApplicableBU + "', 'sApplicationDate': '"
                + $('#<%=txtApplicationDate.ClientID%>').val() + "','Ref':'" + $('#ContentDoc').html()
                + "','Department':'" + $('#<%=ddlDepartment.ClientID%>').val() + "','IsSe':'" + SeCheck + "'}",
            async: true,
            success: function (data) {
                if ($.parseJSON(data.d) == 'ERROR') {
                    swal({
                        title: "通報 / Thông báo",
                        text: "操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!",
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
                            location.href = 'RegisterCodeSecurity.aspx?CodeDocument=' + $.parseJSON(data.d);
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


function ListApprovalSection(CodeDocument) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/ListApprovalSectionSe',
        data: "{ 'CodeDocument': '" + CodeDocument + "'}",
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
            console.log('err');
        }
    });
}


function XacNhan() {
    swal({
        title: "您是否想操作？ / Bạn có chắc chắn  muốn thực hiện?",
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
        if (isConfirm === "") {
            swal.showInputError("意見輸入 / Nhập ý kiến");
            return false;
        }

         if ($('#ContentDCC_Ref tr').length == 0 && $('#<%=hidStates.ClientID%>').val() == 'A10') {
             setTimeout(function () {
                 swal({
                     title: "請登入資料 / Vui lòng nhập tài liệu!",
                     confirmButtonColor: "#2196F3"
                 });
             }, 1000);
                return false;
               
            }

            if ($('#ContentDCC_Ref tr').length != $('#ContentDoc tr').length && $('#<%=hidStates.ClientID%>').val() == 'A10') {
                setTimeout(function () {
                    swal({
                        title: "資料數量不符 / Số lượng tài liệu không khớp!",
                        confirmButtonColor: "#2196F3"
                    });
                }, 1000);
                return false;
            }

             $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    url: 'Models/WebServiceDB.asmx/AcceptRegisterCodeSecurity',
                    data: "{ 'CodeDocument': '" + $('#<%=hidCodeDocument.ClientID%>').val() + "','States': '" + $('#<%=hidStates.ClientID%>').val()
                                + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "','Ref':'" + $('#ContentDCC_Ref').html() + "'}",
                    async: true,
                    success: function (data) {
                        if ($.parseJSON(data.d) == 'SUCCESS') {
                            location.href = "ListCodeSecurity.aspx";
                        }
                        else {
                            swal({
                                title: "通報 / Thông báo",
                                text: "操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!",
                                confirmButtonColor: "#EF5350",
                                type: "error"
                            });
                        }
                    },
                    error: function (er) {
                        console.log(er);
                    }
                });


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
                url: 'Models/WebServiceDB.asmx/DeletedRegisterCodeSecurity',
                data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "'}",
                async: true,
                success: function (data) {
                    $(light).unblock();
                    if ($.parseJSON(data.d) == 'SUCCESS') {
                        location.href = "ListCodeSecurity.aspx";
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
                    url: 'Models/WebServiceDB.asmx/RejectRegisterCodeSecurity',
                    data: "{ 'CodeDocument': '" + $('#<%=hidCodeDocument.ClientID%>').val() + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "'}",
                    async: true,
                    success: function (data) {
                        $(light).unblock();
                        if ($.parseJSON(data.d) == 'SUCCESS') {
                            location.href = "ListCodeSecurity.aspx";
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
                    <label class="control-label text-size-large text-bold text-white">文件編號申請 / ĐƠN XIN MÃ VĂN BẢN </label>
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
                        <label class="control-label col-lg-4 text-bold">申請人 <br /> Người xin đơn:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請日期 <br /> Ngày xin đơn:</label>
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

           <%-- <tr>
                <td colspan="2" class="col-lg-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">文件類別 <br /> Loại văn bản: </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlDocType" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td colspan="2" class="col-lg-6"></td>

            </tr>--%>

            <tr>
                <td colspan="4">
                    <table class="table table-bordered">
                        <tr>
                            <td class="text-bold text-bold col-md-1">序號 <br /> STT</td>
                            <td class="text-center text-bold">文件名稱 <br /> Tên văn bản</td>
                            <td class="text-center text-bold">檔案名稱 <br /> Tên văn bản đi kèm</td>
                            <%--<td class="text-center text-bold">指定修訂人 <br /> Chỉ định người sửa </td>--%>
                            <td class="text-center text-bold">預計結案日 <br /> Ngày kết án dự tính</td>
                            <td class="col-md-1"></td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <a id="addDoc" href="javascript:void(0)" class="btn btn-primary">
                                    <i class="icon-add-to-list position-left"></i>新增 / Thêm mới
                                </a>
                            </td>
                        </tr>
                        <tbody id="ContentDoc">
                        </tbody>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="4">
                    <div class="form-group">
                        <label class="control-label col-lg-2 text-bold">申請原因 <br /> Nguyên nhân xin đơn: </label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtReasonApplication" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group" id="applicableSite">
                        <label class="control-label col-lg-2 text-bold">適用廠區 <br /> Nhà xưởng sử dụng: </label>
                        <label class="checkbox-inline">
                            <input type="checkbox" class="styled">
                            固定VN / Cố định VN
                        </label>
                    </div>
                </td>
                <td colspan="2 ">
                    <div class="form-group" id="applisecurity">
                        <label class="control-label col-lg-2 text-bold">文件保密<br /> Tài liệu Bảo mật: </label>
                        <label class="checkbox-inline">
                            <input type="checkbox" value="1" class="styled" id="inpSe" onclick="CheckSe(this)">
                            有  / Có
                        </label>
                    </div>
                </td>

            </tr>

            <%--<tr>
                <td colspan="4">
                    <div class="form-group" id="applicableBU">
                        <label class="control-label col-lg-2 text-bold">適用BU <br /> BU sửa dụng: </label>
                        <label class="checkbox-inline">
                            <input type="checkbox" class="styled" checked="checked">固定 越南周邊總處/ Cố định là level 3
                        </label>
                    </div>

                </td>
            </tr>--%>

            <tr id="tr_dcc_ref">
                <td colspan="4">
                    <table class="table table-bordered border-blue">
                        <tr>
                            <td class="text-bold text-bold col-md-1"> STT / 序號</td>
                            <td class="text-center text-bold col-md-4">  Mã Văn Bản / 文件編號 </td>
                            <td class="text-center text-bold col-md-6">  Tên Văn Bản / 文件名稱 </td>
                            <td class="col-md-1"></td>
                        </tr>
                        <tbody id="ContentDCC_Ref">
                        </tbody>
                    </table>
                </td>
            </tr>

            <%--<tr id="trOpinion">
                <td colspan="4">
                    <div class="form-group">
                        <label class="control-label col-lg-2 text-bold">Opinion <span class="text-danger">(*)</span> </label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtOpinion" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>--%>

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
                                <th class="text-bold">備註 <br />Ghi chú</th>

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
                <asp:HiddenField ID="hidCodeDocument" runat="server" />
                <asp:HiddenField ID="hidStates" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:HiddenField ID="hidOrder" runat="server" />
                <asp:HiddenField ID="hidApplicableSite" runat="server" />
               <%-- <asp:HiddenField ID="hidApplicableBU" runat="server" />--%>

                <a id="btnLuu" href="javascript:void(0)" class="btn bg-green btn-rounded width-200" onclick="Luu();"><i class="icon-add position-left"></i>存儲 / Lưu</a>
                <a id="btnLamLai" href="RegisterCodeSecurity.aspx" class="btn bg-danger btn-rounded width-200"><i class="icon-cancel-circle2 position-left"></i>重置 / Làm lại</a>

                <a id="btnXacNhan" href="javascript:void(0)" class="btn bg-pink btn-rounded width-200" onclick="XacNhan();">確認 / Xác nhận</a>
                <a id="btnHuyBo" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="HuyBo();">取消 / Hủy bỏ</a>
                <a id="btnTuChoi" href="javascript:void(0)" class="btn bg-indigo btn-rounded width-200" onclick="TuChoi();">拒絕 / Từ chối</a>

            </div>
        </div>

    </div>

    <div id="FormAddDoc" title="添加文件 / THÊM MỚI VĂN BẢN">
        <table class="table table-borderless">
            <tr>
                <td>
                    <div class="form-group">
                        <label class="col-lg-12 control-label text-bold">
                            文件名稱 / Tên văn bản
                        </label>
                        <div class="col-lg-12 has-success">
                            <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="form-group">
                        <label class="col-lg-12 control-label text-bold">
                           添加文件 / Thêm mới văn bản
                        </label>
                        <div class="col-lg-12">
                            <input id="FileName" name="FileName" type="file" class="file-styled-primary" accept=".doc,.docx,.xls,.xlsx">
                        </div>
                    </div>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <div class="form-group">
                        <label class="col-lg-12 control-label text-bold">
                            指定修訂人 / Chỉ định người sửa
                        </label>
                        <div class="col-lg-12 has-success">
                            <asp:TextBox ID="txtAssinedRevisor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <div class="form-group">
                        <label class="col-lg-12 control-label text-bold">
                            預計結案日 / Ngày kết án dự tính
                        </label>
                        <div class="col-lg-12 has-success">
                            <asp:TextBox ID="txtCloseDate" runat="server" CssClass="form-control pickadate-format"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <div id="FormAddDCC" title="  添加DCC / THÊM MỚI DCC">
        <table class="table table-borderless">
            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label text-bold">
                            文件編號 / Mã Văn Bản
                        </label>
                        <asp:HiddenField ID="hidID_DocumentRef" runat="server" />
                        <div class="col-md-12 has-success">
                            <asp:TextBox ID="txtDCC_NO" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label text-bold">
                            文件名稱 / Tên Văn Bản
                        </label>
                        <div class="col-md-12 has-success">
                            <asp:TextBox ID="txtDCC_NAME" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>


</asp:Content>
