<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RegisterPublishDocument.aspx.cs" Inherits="HR_SOP.RegisterPublishDocument" %>

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

            if ($('#<%=hidFileName.ClientID%>').val() != '') {
                $('#linkFile').show();
                $('#linkFile').text($('#<%=hidFileName.ClientID%>').val());
            }
            else {
                $('#linkFile').hide();
            }

            if ($('#<%=hidNeedRelease.ClientID%>').val() != '') {
                $('#linkFileNeedRelease').show();
                $('#linkFileNeedRelease').text($('#<%=hidNeedRelease.ClientID%>').val());
            }
            else {
                $('#linkFileNeedRelease').hide();
            }

            if ($('#<%=hidStates.ClientID%>').val() == 'C01' || $('#<%=hidStates.ClientID%>').val() == '') {
                $('#uniform-FileName').show();
                $('#uniform-FileNameNeedRelease').show();
            }
            else {
                $('#uniform-FileName').hide();
                $('#uniform-FileNameNeedRelease').hide();
            }


            var pubNO = $('#<%=hidPublishDocument.ClientID%>').val();
            if (pubNO != '') {
                $('#approval').show();

                setTimeout(function () {
                    ListApprovalSection(pubNO);
                    LoadPublishRefferent(pubNO);
                    setTimeout(function () {
                        ShowOrHide(pubNO);
                    }, 300);
                }, 300);
            }
            else {
                $('#btnLuu').show();
                $('#btnLamLai').show();
            }
            //LoadDepartment();
            LoadApplicableSite();
            //LoadApplicableBU();

            $('#<%=txtSearchPublishDocument.ClientID%>').keyup(function (e) {
                SearchRegisterPublishDocument($(this).val());
            });

           

            $('#FormAddPublishReff').dialog({
                dialogClass: 'my_dialog',
                autoOpen: false,
                modal: true,
                width: 600,
                buttons: {
                    Submit: function () {
                        if ($('#<%=txtFormNo.ClientID%>').val() == '') {
                            bootbox.alert('表單編號 / Nhập mã biểu đơn');
                            $('#<%=txtFormNo.ClientID%>').focus();
                            return;
                        }
                        else if ($('#<%=txtFormName.ClientID%>').val() == '') {
                            bootbox.alert('表單名稱 / Nhập tên biểu đơn');
                            $('#<%=txtFormName.ClientID%>').focus();
                            return;
                        }
                        else if ($('#<%=ddlPreservingDepartment.ClientID%>').val() == 'ALL' || $('#<%=ddlPreservingDepartment.ClientID%>').val() == ''
                            || $('#<%=ddlPreservingDepartment.ClientID%>').val() == null) {
                            bootbox.alert('保管單位 / Chọn bộ phận lưu trữ');
                            $('#<%=ddlPreservingDepartment.ClientID%>').focus();
                            return;
                        }
                        else if ($('#<%=txtStorage.ClientID%>').val() == '') {
                            bootbox.alert('保存期限 / Nhập thời gian lưu trữ');
                            $('#<%=txtStorage.ClientID%>').focus();
                            return;
                        }
                        else if ($('#filePublishAttach').val() == '') {
                            bootbox.alert('請選擇資料附件 / Vui lòng chọn file tài liệu');
                            $('#filePublishAttach').focus();
                            return;
                        }
                        else {
                            var files = $('#filePublishAttach').get(0).files;
                            var ext = files[0].name.replace(/^.*\./, '');
                            if (ext != 'doc' && ext != 'docx' && ext != 'xls' && ext != 'xlsx' && ext != 'pdf') {
                                bootbox.alert('Chỉ upload file (.doc,.docx,.xls,.xlsx,.pdf)');
                                return;
                            }
                            else if (files[0].size > 26214400) {
                                bootbox.alert('請選擇資料附件 / Vui lòng chọn file tài liệu <= 25MB');
                                return;
                            }
                            else {
                                var data = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    data.append("filePublishAttach", files[i]);
                                }
                                $.ajax({
                                    type: 'POST',
                                    contentType: false,
                                    processData: false,
                                    dataType: 'text',
                                    url: 'Models/WebServiceDB.asmx/SaveFilePublishRefferent',
                                    data: data,
                                    async: true,
                                    success: function (data) {
                                        var order = 0;
                                        var temp = $('#ContentPublishReff tr:last td').html();
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
                                            var storage = $('#<%=ddlStorageTime.ClientID%>').val();
                                            var ts = Number($('#<%=txtStorage.ClientID%>').val());

                                            var tomorrow = new Date();
                                            if (storage == 'C-00042') {
                                                tomorrow.setFullYear(tomorrow.getFullYear() + ts);
                                            }
                                            else if (storage == 'C-00043') {
                                                tomorrow.setMonth(tomorrow.getMonth() + ts);
                                            }
                                            else {
                                                tomorrow.setDate(tomorrow.getDate() + ts);
                                            }

                                            var month = tomorrow.getMonth() + 1 > 10 ? tomorrow.getMonth() + 1 : "0" + (tomorrow.getMonth() + 1);
                                            var day = tomorrow.getDate() > 10 ? tomorrow.getDate() : "0" + tomorrow.getDate();
                                            storage = tomorrow.getFullYear() + "/" + month + "/" + day;
                                            var html = '';
                                            html = '<tr id="tr_reff_' + order + '">';
                                            html += '<td>' + order + '</td>';
                                            html += '<td>' + $('#<%=txtFormNo.ClientID%>').val() + '</td>';
                                            html += '<td>' + $('#<%=txtFormName.ClientID%>').val() + '</td>';
                                            html += '<td value=' + $('#<%=ddlPreservingDepartment.ClientID%>').val() + '>' + $('#select2-MainContent_ddlPreservingDepartment-container').text() + '</td>';
                                            html += '<td>' + storage + '</td>';
                                            html += '<td> <a href="javascript:void(0)" value="' + ten + '" onclick="ShowPublishReff(this);">' + ten + '</a></td>';
                                            html += '<td class="text-center">';
                                            html += '    <ul class="icons-list">';
                                            html += '        <li class="text-danger-600"><a href="javascript:void(0)" value="' + order + '" onclick="DeletePublishReff(this);" title="Delete"><i class="icon-trash"></i></a></li>';
                                            html += '    </ul>';
                                            html += '</td>';
                                            html += '</tr>';
                                            $('#ContentPublishReff').append(html);

                                            $('#FormAddPublishReff').dialog('close');
                                        }
                                        else {
                                            bootbox.alert('Upload file error');
                                        }
                                    },
                                    error: function (er) {
                                        bootbox.alert('Error system : -' + er);
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


            $('.my_dialog .ui-button-text:contains(Cancel)').text('拒絕 / Từ chối');
            $('.my_dialog .ui-button-text:contains(Submit)').text('存儲 / Lưu');

            $('#addPublishReff').click(function () {
                
                $('#<%=txtFormName.ClientID%>').val('');
                $('#<%=ddlPreservingDepartment.ClientID%>').val('ALL');
                $('#select2-MainContent_ddlPreservingDepartment-container').text('---ALL---');
                $('#<%=ddlStorageTime.ClientID%>').val('C-00042');
                $('#<%=txtStorage.ClientID%>').val('1');
                $('.filename').text('');

                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    url: 'Models/WebServiceDB.asmx/CreateCodeFormAuto',
                    data: "{ 'Code_Dcc': '" + $('#<%=txtDocNO.ClientID%>').val() + "','NameCol':'" + $('#<%=ddlDepartment.ClientID%>').val() + "'}",
                    async: true,
                    success: function (data) {
                        $.each($.parseJSON(data.d), function (i, v) {
                            $('#<%=txtFormNo.ClientID%>').val(v.Code);
                        });
                        $('#FormAddPublishReff').dialog('open');
                    },
                    error: function (er) {
                        bootbox.alert("Error");
                    }
                });

            });

        });

        function ShowPublishReff(row) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckExistsFilePublishReff',
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

        function DeletePublishReff(row) {
            $('#tr_reff_' + $(row).attr('value') + ' td').each(function (i) {
                if (i == 5) {
                    $.ajax({
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        url: 'Models/WebServiceDB.asmx/DeleteFilePublishRefferent',
                        data: "{ 'FileName': '" + $(this).text() + "'}",
                        async: true,
                        success: function (data) {
                            $('#tr_reff_' + $(row).attr('value')).remove();
                        },
                        error: function (er) {
                            bootbox.alert("Error system : -" + er);
                        }
                    });
                }
            });
        }

        function LoadPublishRefferent(PublishDocument) {
            var states = $('#<%=hidStates.ClientID%>').val();
            if (states == 'C01' || states == 'C30' || states == '') {
                $('#addPublishReff').show();
            }
            else {
                $('#addPublishReff').hide();
            }

            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListPublishReffByPublishDocument',
                data: "{ 'PublishDocument': '" + PublishDocument + "'}",
                async: true,
                success: function (data) {
                    $('#ContentPublishReff').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {
                        html += '<tr id="tr_reff_' + (i + 1) + '">';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td>' + v.FormNo + '</td>';
                        html += '<td>' + v.FormName + '</td>';
                        html += '<td value=' + v.PreservingDepartment + '>' + v.DepartmentName + '</td>';
                        html += '<td>' + v.PreservingTime_Text + '</td>';
                        html += '<td> <a href="javascript:void(0)" value="' + v.Attachment + '" onclick="ShowPublishReff(this);">' + v.Attachment + '</a></td>';
                        html += '<td class="text-center">';
                        html += '    <ul class="icons-list">';
                        if (states == 'C01' || states == 'C30') {
                            html += '        <li class="text-danger-600"><a href="javascript:void(0)" value="' + (i + 1) + '" onclick="DeletePublishReff(this);" title="Delete"><i class="icon-trash"></i></a></li>';
                        }
                        else {
                            html += '        <li><a href="javascript:void(0)" title="Delete"><i class="icon-lock"></i></a></li>';
                        }
                        html += '    </ul>';
                        html += '</td>';
                        html += '</tr>';
                    })
                    $('#ContentPublishReff').append(html);
                },
                error: function (er) {
                    bootbox.alert("Error system : -" + er);
                }
            });
        }

        function ShowDoc() {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckExistsFilePublishDocument',
                data: "{ 'FileName': '" + $('#<%=hidFileName.ClientID%>').val().trim() + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'EXISTED') {
                        var url = $(location).attr('protocol') + '//' + $(location).attr('host') + '/Updatafile/PublishDoc/' + $('#<%=hidFileName.ClientID%>').val().trim();
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

        function ShowNeed() {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckExistsFilePublishDocumentNeed',
                data: "{ 'FileName': '" + $('#<%=hidNeedRelease.ClientID%>').val().trim() + "'}",
                async: true,
                success: function (data) {
                    if ($.parseJSON(data.d) == 'EXISTED') {
                        var url = $(location).attr('protocol') + '//' + $(location).attr('host') + '/Updatafile/PublishDoc/NeedRelease/' + $('#<%=hidNeedRelease.ClientID%>').val().trim();
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

        function ListApprovalSection(PublishDocument) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListApprovalSection',
                data: "{ 'CodeDocument': '" + PublishDocument + "'}",
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

        function ShowOrHide(PublishDocument) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/CheckDisplaySubmitPublishDocument',
                data: "{ 'PublishDocument': '" + PublishDocument + "'}",
                async: true,
                success: function (data) {
                    $.each($.parseJSON(data.d), function (i, v) {
                        var states = $('#<%=hidStates.ClientID%>').val();
                        if (states == 'C01') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnLuu').show();
                                $('#btnXacNhan').show();
                                $('#btnHuyBo').show();
                            }
                        }
                        else if (states == 'C03' || states == 'C05' || states == 'C10' || states == 'C15' || states == 'C20' || states == 'C25') {
                            if (v.UserName == $('#<%=hidUserName.ClientID%>').val()) {
                                $('#btnXacNhan').show();
                                $('#btnTuChoi').show();

                            }
                        }
                        else if (states == 'C30') {
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

<%--var listDepartment = '';
function LoadDepartment() {

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
                html += '       <input id="chk_' + v.CatCode + '" type="checkbox" value="' + v.CatCode + '" class="control-label fix" onclick="CheckRowDepartment(this);">' + v.CatName + '';
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
                        listDepartment = listDepartment + depart[i] + ',';
                    }
                }
            }
        },
        error: function (er) {
            console.log('err');
        }
    });

}--%>

//function CheckRowDepartment(row) {
//    if ($(row).is(':checked')) {
//        listDepartment += $(row).attr('value') + ',';
//    }
//    else {
//        listDepartment = listDepartment.replace($(row).attr('value') + ',', '');
//    }
//}

var listApplicableSite = '';
//var listApplicableBU = '';

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
                listApplicableSite = listApplicableSite + site[i] + ',';
            }
        }
    }
    else {
        $('#chk_C-00005').prop('checked', true);
        listApplicableSite = 'C-00005,';
    }
}

function CheckRowSite(row) {
    if ($(row).is(':checked')) {
        listApplicableSite += $(row).attr('value') + ',';
    }
    else {
        listApplicableSite = listApplicableSite.replace($(row).attr('value') + ',', '');
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
                listApplicableBU = listApplicableBU + bu[i] + ',';
            }
        }
    }
    else {
        $('#chk_C-00006').prop('checked', true);
        listApplicableBU = 'C-00006,';
    }
}--%>

//function CheckRowBU(row) {
//    if ($(row).is(':checked')) {
//        listApplicableBU += $(row).attr('value') + ',';
//    }
//    else {
//        listApplicableBU = listApplicableBU.replace($(row).attr('value') + ',', '');
//    }
//}


function Luu() {
   if ($('#FileName').val() == '' && $('#<%=hidFileName.ClientID%>').val() == '') {
        bootbox.alert("請選擇資料附件 / Vui lòng chọn file tài liệu");
        return;
    } else if ($('#FileNameNeedRelease').val() == '' && $('#<%=hidNeedRelease.ClientID%>').val() == '') {
        bootbox.alert("請選擇資料附件 / Vui lòng chọn file tài liệu");
        return;
    }
    else if ($('#<%=ddlDepartment.ClientID%>').val() == null) {
        bootbox.alert("選擇簽核主管 / Chọn chủ quản ký.");
        $('#<%=ddlDepartment.ClientID%>').focus();
        return;
    }
    else {
        var files = $('#FileName').get(0).files;
        var ext = '';
        if (files.length > 0) {
            ext = files[0].name.replace(/^.*\./, '');
            if (ext != 'doc' && ext != 'docx' && ext != 'xls' && ext != 'xlsx') {
                bootbox.alert('Chỉ upload file (.doc,.docx,.xls,.xlsx)');
                return;
            }
            else if (files[0].size > 26214400) {
                bootbox.alert('請選擇資料附件 / Vui lòng chọn file tài liệu <= 25MB');
                return;
            }
        }
        else {
            ext = 'doc';
        }

        var filesNeed = $('#FileNameNeedRelease').get(0).files;
        var extNeed = '';
        if (filesNeed.length > 0) {
            extNeed = filesNeed[0].name.replace(/^.*\./, '');
            if (filesNeed[0].size > 26214400) {
                bootbox.alert('請選擇資料附件 / Vui lòng chọn file tài liệu <= 25MB');
                return;
            }
        }
        else {
            extNeed = 'img';
        }

        var tenDoc = '';
        if (ext != '') {
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append("FileName", files[i]);
            }

            $.ajax({
                type: 'POST',
                contentType: false,
                processData: false,
                dataType: 'text',
                url: 'Models/WebServiceDB.asmx/SaveFilePublishDoc',
                data: data,
                async: true,
                success: function (data) {
                    tenDoc = data.replace("</string>", "").split('>');
                    if (tenDoc.length > 1) {
                        tenDoc = tenDoc[tenDoc.length - 1];
                    }
                },
                error: function (er) {
                    bootbox.alert("Error system : -" + er);
                }
            });
        }

        var nameNeed = '';
        if (extNeed != '') {
            var dataNeed = new FormData();
            for (var i = 0; i < filesNeed.length; i++) {
                dataNeed.append("FileNameNeedRelease", filesNeed[i]);
            }
            dataNeed.append('Version', $('#<%=txtREV.ClientID%>').val());

            $.ajax({
                type: 'POST',
                contentType: false,
                processData: false,
                dataType: 'text',
                url: 'Models/WebServiceDB.asmx/SaveFilePublishNeedRelease',
                data: dataNeed,
                async: true,
                success: function (data) {
                    nameNeed = data.replace("</string>", "").split('>');
                    if (nameNeed.length > 1) {
                        nameNeed = nameNeed[nameNeed.length - 1];
                    }
                },
                error: function (er) {
                    bootbox.alert("Error system : -" + er);
                }
            });
        }

        setTimeout(function () {

            var ID = $('#<%=hidID.ClientID%>').val();
            var PublishDocument = $('#<%=txtApplicationNO.ClientID%>').val();
            var ApplicationSite = $('#<%=ddlApplicationSite.ClientID%>').val();
            var sEffectiveDate = $('#<%=txtEffectiveDate.ClientID%>').val();
            var DocumentNo = $('#<%=txtDocNO.ClientID%>').val();

            var Rev = $('#<%=txtREV.ClientID%>').val();
            var DocumentName = $('#<%=txtDocName.ClientID%>').val();
            var DocumentType = '';<%-- $('#<%=ddlDocType.ClientID%>').val();--%>
            var RevisionApplication = ""; <%--$('#<%=txtRevisionApplication.ClientID%>').val();--%>
            var CheckingNotice = ""; <%--$('#<%=txtCheckingNotice.ClientID%>').val();--%>

            var DeletedDocumentOld = $('#<%=txtDocumentObsolete.ClientID%>').val();
            var ReferenceDocument = $('#<%=txtDocumentReference.ClientID%>').val();
            var IndexWord = ''; <%--$('#<%=txtWordKey.ClientID%>').val();--%>
            var ContentFile = tenDoc;
            var ContentFile_old = $('#<%=hidFileName.ClientID%>').val();
            var PublishReff = $('#ContentPublishReff').html();

            var ApplicableSite = listApplicableSite;
            var ApplicableBU ='' ;//listApplicableBU;

            var NeedReleaseFile = nameNeed;
            var NeedReleaseFile_old = $('#<%=hidNeedRelease.ClientID%>').val();
            var sApplicationDate = $('#<%=txtApplicationDate.ClientID%>').val();
            var DepartmentCheck = '';//listDepartment;
            var CodeDocument = $('#<%=hidCodeDocument.ClientID%>').val();
            var Department = $('#<%=ddlDepartment.ClientID%>').val();


            if (ContentFile == '' && ContentFile_old =='')
            {
                bootbox.alert("請選擇資料附件 / Vui lòng chọn file tài liệu");
                return;
            }
            else if (NeedReleaseFile == '' && NeedReleaseFile_old == '') {
                bootbox.alert("請選擇資料附件 / Vui lòng chọn file tài liệu");
                return;
            }
            else { 
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/InsertOrUpdateRegisterPublishDocument',
                data: "{ 'ID': '" + ID + "', 'PublishDocument': '" + PublishDocument + "', 'ApplicationSite': '" + ApplicationSite + "', 'sEffectiveDate': '" + sEffectiveDate + "', 'DocumentNo': '" + DocumentNo
                + "', 'Rev': '" + Rev + "', 'DocumentName': '" + DocumentName + "', 'DocumentType': '" + DocumentType + "', 'RevisionApplication': '" + RevisionApplication + "', 'CheckingNotice': '" + CheckingNotice
                + "','DeletedDocumentOld': '" + DeletedDocumentOld + "', 'ReferenceDocument': '" + ReferenceDocument + "', 'IndexWord': '" + IndexWord + "', 'ContentFile': '" + ContentFile + "','ContentFile_old': '" + ContentFile_old + "', 'PublishReff': '" + PublishReff
                + "', 'ApplicableSite': '" + ApplicableSite + "', 'ApplicableBU': '" + ApplicableBU + "', 'NeedReleaseFile': '" + NeedReleaseFile + "','NeedReleaseFile_old': '" + NeedReleaseFile_old + "', 'sApplicationDate': '" + sApplicationDate
                + "','DepartmentCheck': '" + DepartmentCheck + "','CodeDocument': '" + CodeDocument + "','Department' :'" + Department + "'}",
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
                                location.href = 'RegisterPublishDocument.aspx?PublishDocument=' + $.parseJSON(data.d);
                            }
                        });
                    }
                },
                error: function (er) {
                    bootbox.alert("Error system : -" + er);
                }
            });
            }
        }, 3000);

    }
}
function LamLai() {
    swal({
        title: "Bạn có chắc chắn muốn thực hiện?",
        text: "Phiếu đăng ký sẽ xóa bỏ tất cả thông tin đã nhập.",
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
        text: "表單呈簽下一步Đơn sẽ trình ký đến chủ quản tiếp theo.",
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

        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            url: 'Models/WebServiceDB.asmx/AcceptRegisterPublishDocument',
            data: "{ 'PublishDocument': '" + $('#<%=hidPublishDocument.ClientID%>').val() + "','States': '" + $('#<%=hidStates.ClientID%>').val()
                    + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "','DepartmentCheck':''}",
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
                url: 'Models/WebServiceDB.asmx/DeletedRegisterPublishDocument',
                data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "'}",
                    async: true,
                    success: function (data) {
                        $(light).unblock();
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
                url: 'Models/WebServiceDB.asmx/RejectRegisterPublishDocument',
                data: "{ 'PublishDocument': '" + $('#<%=hidPublishDocument.ClientID%>').val() + "','Comment': '" + isConfirm + "','Url':'" + $(location).attr('href') + "'}",
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


function ShowListPublishDocument(row) {
    $('#<%=txtSearchPublishDocument.ClientID%>').val('');
    type = $(row).attr('title');
    listOld = $('#<%=txtDocumentObsolete.ClientID%>').val();
    listRef = $('#<%=txtDocumentReference.ClientID%>').val();
    SearchRegisterPublishDocument($('#<%=txtSearchPublishDocument.ClientID%>').val());
}

function SearchRegisterPublishDocument(PublishDocument) {
    var ListPublishDocument = '';
    if (type == 'OLD') {
        ListPublishDocument = listOld;
    }
    else if (type == 'REF') {
        ListPublishDocument = listRef;
    }

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: 'Models/WebServiceDB.asmx/SearchRegisterPublishDocument',
        data: "{ 'DocumentNo': '" + PublishDocument + "', 'ListDocumentNo': '" + ListPublishDocument + "'}",
        async: true,
        success: function (data) {
            $('#bodyPublish').empty();
            var html = '';
            $.each($.parseJSON(data.d), function (i, v) {
                html += '<tr>';
                html += '   <td> <a href="javascript:void(0)" value="' + v.DocumentNo + '" onclick="AddDocument(this)">' + v.DocumentNo + ' </a></td>';
                html += '   <td>' + v.DocumentName + '</td>';
                html += '   <td>' + v.EffectiveDate + '</td>';
                html += '</tr>';
            });
            $('#bodyPublish').append(html);
        },
        error: function (er) {
            bootbox.alert("Error system : -" + er);
        }
    });
}

function AddDocument(row) {
    if (type == 'OLD') {
        listOld = listOld + $(row).attr('value') + ',';
        $('#<%=txtDocumentObsolete.ClientID%>').val(listOld);
        SearchRegisterPublishDocument($('#<%=txtSearchPublishDocument.ClientID%>').val());
    }
    else if (type == 'REF') {
        listRef = listRef + $(row).attr('value') + ',';
        $('#<%=txtDocumentReference.ClientID%>').val(listRef);
        SearchRegisterPublishDocument($('#<%=txtSearchPublishDocument.ClientID%>').val());
    }
}
    </script>

    <div class="panel panel-body border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300 bg-grey-300">
        <table class="col-md-12">
            <tr>
                <td class="col-md-6">
                    <img src="Images/fc_logo.png" alt="" height="40" />
                </td>
                <td class="col-md-6">
                    <label class="control-label text-size-large text-bold text-white">文件發行申請 / ĐƠN XIN PHÁT HÀNH VĂN BẢN</label>
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
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>

                <td class="col-lg-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請單編號 <br /> Mã đơn:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationNO" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請人 <br /> Người xin đơn :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">申請日期 <br /> Ngày xin đơn :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtApplicationDate" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">
                           申請廠區 <br /> Nhà xưởng xin đơn:</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlApplicationSite" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">
                            生效日期 <br /> Ngày có hiệu lực:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">
                            文件編號<br />Mã văn bản:
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtDocNO" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">
                            版本
                            <br />
                            Phiên bản :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtREV" runat="server" CssClass="form-control border-bottom-blue-800 text-blue-800" Text="A" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-2 text-bold">
                            文件名稱
                            <br />
                            Tên văn bản:</label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>

               <%-- <td>
                    <div class="form-group">
                        <label class="control-label col-lg-4 text-bold">
                            文件類別 <br /> Loại văn bản
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlDocType" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>--%>

            </tr>

            <%--   <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label text-bold">
                            關結程序規範修訂申請單編號 
                                Docuemnt number of revision application(The docuemnt will be closed when the new document is effective) 
                        </label>
                        <div>
                            <asp:TextBox ID="txtRevisionApplication" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label text-bold">
                            關結程序規範檢核申請單編號 
                                Docuemnt number of checking notice(The docuemnt will be closed when the new document is effective) 
                        </label>
                        <div>
                            <asp:TextBox ID="txtCheckingNotice" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>--%>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label text-bold">
                           文件生效後此編號文件即自動作廢 / Văn bản dưới đây sẽ bị hủy bỏ khi văn bản mới được phát hành
                                
                                <a href="#" class="btn btn-rounded btn-default" data-toggle="modal" title="OLD"
                                    data-target="#modal_theme_list_publish" onclick="ShowListPublishDocument(this);">
                                    <i class="icon-search4"></i>
                                </a>
                        </label>
                        <asp:TextBox ID="txtDocumentObsolete" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>

                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label text-bold">
                            參考文件 / Văn bản tham khảo
                                <a href="#" class="btn btn-rounded btn-default" data-toggle="modal" title="REF"
                                    data-target="#modal_theme_list_publish" onclick="ShowListPublishDocument(this);">
                                    <i class="icon-search4"></i>
                                </a>
                            
                        </label>
                        <asp:TextBox ID="txtDocumentReference" runat="server" class="form-control border-bottom-blue-800"></asp:TextBox>

                    </div>
                </td>
            </tr>

          <%--  <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label text-bold">
                           檢索字句﹕(名詞解釋標題及/或便於搜尋檢索的字句﹐各字句間請用逗號“﹐”區隔) <br />
                                Cụm từ tìm kiếm: (tiêu đề giải thích danh từ và/hoặc để tiện tìm kiếm cụm từ khóa, hãy sử dụng dấu “﹐” giữa các từ để phân biệt
                        </label>
                        <asp:TextBox ID="txtWordKey" runat="server" class="form-control border-bottom-blue-800"></asp:TextBox>

                    </div>
                </td>
            </tr>--%>

            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-3 text-bold">
                            請上傳Word檔副本 <br /> Vui lòng thêm bản word: <span class="text-danger">(*)</span>
                        </label>
                        <div class="col-lg-9">

                            <input id="FileName" name="FileName" type="file" class="file-styled-primary" accept=".doc,.docx,.xls,.xlsx">

                            <a id="linkFile" href="javascript:void(0)" onclick="ShowDoc();" title="Save File"></a>
                            <asp:HiddenField ID="hidFileName" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <table class="table table-bordered">
                        <tr>
                            <td colspan="7" class="text-bold">附件表單 / Phụ lục đính kèm </td>
                        </tr>
                        <tr>
                            <td class="text-bold text-bold col-md-1">序號 <br /> STT</td>
                            <td class="text-center text-bold col-md-2">附件表單編號 <br /> Mã phụ lục đính kèm</td>
                            <td class="text-center text-bold col-md-3">附件表單名稱 <br /> Tên phụ lục đính kèm</td>
                            <td class="text-center text-bold col-md-2">保管單位 <br /> Bộ phận lưu trữ </td>
                            <td class="text-center text-bold col-md-2">保存期限 <br /> Thời hạn lưu trữ </td>
                            <td class="text-center text-bold col-md-2">文件附檔 <br /> File đính kèm </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <a id="addPublishReff" href="javascript:void(0)" class="btn btn-primary">
                                    <i class="icon-add-to-list position-left"></i>新增 / Thêm mới
                                </a>
                            </td>
                        </tr>
                        <tbody id="ContentPublishReff">
                        </tbody>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="form-group" id="applicableSite">
                        <label class="control-label col-lg-2 text-bold">適用廠區 <br /> Nhà xưởng sử dụng: </label>
                        <label class="checkbox-inline">
                            <input type="checkbox">
                        </label>
                    </div>
                </td>
            </tr>

           <%-- <tr>
                <td colspan="2">
                    <div class="form-group">
                        <div class="form-group" id="applicableBU">
                            <label class="control-label col-lg-2 text-bold">適用BU / BU sửa dụng: </label>
                            <label class="checkbox-inline">
                                <input type="checkbox" class="styled">
                            </label>
                        </div>
                    </div>
                </td>
            </tr>--%>


            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-3 text-bold">
                           請上傳已簽核的紙檔副本 <br /> Vui lòng thêm bản PDF đã ký <span class="text-danger">(*)</span>:
                        </label>
                        <div class="col-lg-9">
                            <input id="FileNameNeedRelease" name="FileNameNeedRelease" type="file" class="file-styled-primary"  accept=".pdf">
                            <a id="linkFileNeedRelease" href="javascript:void(0)" onclick="ShowNeed();" title="Save File">
                               <%--<i class="glyphicon glyphicon-save"></i>class="file-styled-primary"--%>
                            </a>
                            <asp:HiddenField ID="hidNeedRelease" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>

<%--            <tr>
                <td colspan="2">
                    <div class="form-group">
                        <label class="control-label col-lg-12 text-bold">请勾選會簽單位 /	Vui lòng chọn đơn vị trình ký :</label>
                    </div>
                </td>
            </tr>

            <tr>
                <td id="department" colspan="2">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="checkbox-inline">
                                <input id="chk_" type="checkbox" class="control-label fix">供應鏈管理
                            </label>
                        </div>
                    </div>
                </td>
            </tr>--%>

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
                                <th class="text-bold">備註 <br /> Ghi chú </th>

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
            <asp:HiddenField ID="hidPublishDocument" runat="server" />
            <asp:HiddenField ID="hidStates" runat="server" />
            <asp:HiddenField ID="hidID" runat="server" />
            <asp:HiddenField ID="hidApplicableSite" runat="server" />
           <%-- <asp:HiddenField ID="hidApplicableBU" runat="server" />--%>
            <%--<asp:HiddenField ID="hidDepartmentCheck" runat="server" />--%>
            <asp:HiddenField ID="hidCodeDocument" runat="server" />


            <div class="form-group text-center">
                <a id="btnLuu" href="javascript:void(0)" class="btn bg-green btn-rounded width-200" onclick="Luu();"><i class="icon-add position-left"></i>存儲 / Lưu</a>
                <a id="btnLamLai" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="LamLai();"><i class="icon-cancel-circle2 position-left"></i>重置 / Làm lại</a>

                <a id="btnXacNhan" href="javascript:void(0)" class="btn bg-pink btn-rounded width-200" onclick="XacNhan();">確認 / Xác nhận</a>
                <a id="btnHuyBo" href="javascript:void(0)" class="btn bg-danger btn-rounded width-200" onclick="HuyBo();">取消 / Hủy bỏ</a>
                <a id="btnTuChoi" href="javascript:void(0)" class="btn bg-indigo btn-rounded width-200" onclick="TuChoi();">拒絕 / Từ chối</a>
            </div>
        </div>

    </div>


    <div id="modal_theme_list_publish" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-info">
                    <a href="javascript:void(0)" class="close" data-dismiss="modal">&times;</a>
                    <h6 class="modal-title">文件發行清單 / DANH SÁCH PHÁT HÀNH VĂN BẢN</h6>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="form-group has-success">
                            <asp:TextBox ID="txtSearchPublishDocument" runat="server" CssClass="form-control" placeholder="Input publish document"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <table class="table table-bordered">
                                <tr>
                                    <td class="text-bold">文件編碼 <br /> Mã văn bản</td>
                                    <td class="text-bold">文件名稱 <br />Tên văn bản</td>
                                    <td class="text-bold">有效日期 <br /> Ngày có hiệu lực</td>
                                </tr>
                                <tbody id="bodyPublish"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-12">
                        <a href="javascript:void(0)" class="btn btn-link" data-dismiss="modal">關閉 / ĐÓNG</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="FormAddPublishReff" title="添加附件表單 / THÊM PHỤ LỤC ĐÍNH KÈM">
        <table class="table table-borderless">
            <tr>
                <td>
                    <div class="form-group">
                        <label class="col-md-12 control-label text-bold">
                            附件表單編號 / Mã phụ lục đính kèm
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtFormNo" runat="server" CssClass="form-control border-bottom-blue-800" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="col-md-12 control-label text-bold">
                           附件表單名稱 / Tên phụ lục đính kèm
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtFormName" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="col-md-12 control-label text-bold">
                            保管單位 / Bộ phận bảo quản
                        </label>
                        <div class="col-md-12">
                            <asp:DropDownList ID="ddlPreservingDepartment" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">

                        <label class="col-md-12 control-label text-bold">
                            保存期限 / Thời gian lưu trữ
                        </label>
                        <div class="col-md-7">
                            <asp:DropDownList ID="ddlStorageTime" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>

                        <div class="col-md-5">
                            <asp:TextBox ID="txtStorage" runat="server" CssClass="touchspin-no-mousewheel  border-bottom-blue-800">5</asp:TextBox>
                        </div>

                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="form-group">
                        <label class="col-md-12 control-label text-bold">
                            文件附檔 / File tài liệu
                        </label>
                        <div class="col-md-12">
                            <input id="filePublishAttach" name="filePublishAttach" type="file" class="file-styled-primary" accept=".doc,.docx,.xls,.xlsx,.pdf">
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
