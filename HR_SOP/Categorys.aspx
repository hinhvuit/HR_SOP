<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorys.aspx.cs" Inherits="HR_SOP.Categorys" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () { ListCategoryType(); }, 500);

            
            $('#fromCategorys').dialog({
                dialogClass: 'my_dialog',
                autoOpen: false,
                modal: true,
                width: 550,
                buttons: {
                    Submit: function () {

                        var CatType = $('#<%=hidType.ClientID%>').val();
                        if (CatType == 'CAT_TYPE') {

                            $.ajax({
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                url: 'Models/WebServiceDB.asmx/InsertOrUpdateCategoryType',
                                data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "', 'CatTypeCode': '" + $('#<%=hidCode.ClientID%>').val() + "', 'CatTypeName': '"
                                    + $('#<%=txtName.ClientID%>').val() + "','Orders': '" + $('#<%=txtOrders.ClientID%>').val() + "' }",
                                async: true,
                                success: function (data) {
                                    if ($.parseJSON(data.d) == 'ADD') {
                                        ListCategoryTypeDetail();
                                        $('#fromCategorys').dialog('close');
                                    }
                                    else if ($.parseJSON(data.d) == 'EDIT') {
                                        ListCategoryTypeDetail();
                                        $('#fromCategorys').dialog('close');
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
                        else {
                            $.ajax({
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                url: 'Models/WebServiceDB.asmx/InsertOrUpdateCategorys',
                                data: "{ 'ID': '" + $('#<%=hidID.ClientID%>').val() + "', 'CatCode': '" + $('#<%=hidCode.ClientID%>').val() + "', 'CatName': '"
                                    + $('#<%=txtName.ClientID%>').val() + "', 'CatTypeCode': '" + $('#<%=hidType.ClientID%>').val() + "', 'Orders': '" + $('#<%=txtOrders.ClientID%>').val() + "' }",
                                async: true,
                                success: function (data) {
                                    if ($.parseJSON(data.d) == 'ADD') {
                                        GetCategorysByCatTypeCode($('#<%=hidType.ClientID%>').val());
                                        $('#fromCategorys').dialog('close');
                                    }
                                    else if ($.parseJSON(data.d) == 'EDIT') {
                                        GetCategorysByCatTypeCode($('#<%=hidType.ClientID%>').val());
                                            $('#fromCategorys').dialog('close');
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

            $('.my_dialog .ui-button-text:contains(Cancel)').text('拒絕 / Từ chối');
            $('.my_dialog .ui-button-text:contains(Submit)').text('存儲 / Lưu');

        });

        function EditRow(row) {

            $('#tr_' + $(row).attr('value') + ' td').each(function (i) {
                if (i == 0) {
                    $('#<%=hidID.ClientID%>').val($(row).attr('value'));
                }
                else if (i == 1) {
                    $('#<%=hidCode.ClientID%>').val($(this).html());
                }
                else if (i == 2) {
                    $('#<%=txtName.ClientID%>').val($(this).html());
                }
                else if (i == 3) {
                    $('#<%=txtOrders.ClientID%>').val($(this).html());
                }
            });
    $('#fromCategorys').dialog('open');
}

function DeleteRow(row) {
    swal({
        title: "您是否操作？Bạn có chắc chắn muốn thực hiện？",
        text: "Check list 會被刪除 / Danh mục sẽ bị xóa khỏi hệ thống",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#FF7043",
        confirmButtonText: "同意 / Đồng ý",
        cancelButtonText: '拒絕 / Từ chối',
        showLoaderOnConfirm: true
    },
       function (isConfirm) {
           if (isConfirm) {

               var CatType = $('#<%=hidType.ClientID%>').val();
               if (CatType == 'CAT_TYPE') {
                   $.ajax({
                       type: 'POST',
                       contentType: 'application/json; charset=utf-8',
                       dataType: 'json',
                       url: 'Models/WebServiceDB.asmx/DeletedCategoryType',
                       data: "{ 'ID': '" + $(row).attr('value') + "'}",
                       async: true,
                       success: function (data) {
                           if ($.parseJSON(data.d) == 'SUCCESS') {
                               ListCategoryTypeDetail();
                           }
                           else {
                               bootbox.alert("操作過程發生錯誤，請重新操作！ / Lỗi trong quá trình thực hiện, vui lòng thực hiện lại!");
                           }
                       },
                       error: function (er) {
                           bootbox.alert('Error system : -' + er);
                       }
                   });
               } else {
                   $.ajax({
                       type: 'POST',
                       contentType: 'application/json; charset=utf-8',
                       dataType: 'json',
                       url: 'Models/WebServiceDB.asmx/DeletedCategorys',
                       data: "{ 'ID': '" + $(row).attr('value') + "'}",
                       async: true,
                       success: function (data) {
                           if ($.parseJSON(data.d) == 'SUCCESS') {
                               GetCategorysByCatTypeCode(CatType);
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
           }
       });
   }

   function ResetRow() {
       $('#<%=hidID.ClientID%>').val('');
       $('#<%=txtName.ClientID%>').val('');
       $('#<%=txtOrders.ClientID%>').val('');
   }

   function GetCodeCategory() {
       ResetRow();
       var CatType = $('#<%=hidType.ClientID%>').val();
       if (CatType == 'CAT_TYPE') {
           $.ajax({
               type: 'POST',
               contentType: 'application/json; charset=utf-8',
               dataType: 'json',
               url: 'Models/WebServiceDB.asmx/GetCodeAuto',
               data: "{ 'NameCol': 'CatTypeCode', 'NameTable': 'CategoryType' }",
               async: true,
               success: function (data) {
                   $('#<%=hidCode.ClientID%>').val($.parseJSON(data.d));
                   $('#fromCategorys').dialog('open');
               },
               error: function (er) {
                   bootbox.alert('Error system : -' + er);
               }
           });
       }
       else {

           $.ajax({
               type: 'POST',
               contentType: 'application/json; charset=utf-8',
               dataType: 'json',
               url: 'Models/WebServiceDB.asmx/GetCodeAuto',
               data: "{ 'NameCol': 'CatCode', 'NameTable': 'Categorys' }",
               async: true,
               success: function (data) {
                   $('#<%=hidCode.ClientID%>').val($.parseJSON(data.d));
                   $('#fromCategorys').dialog('open');
               },
               error: function (er) {
                   bootbox.alert('Error system : -' + er);
               }
           });
       }
       }

       function ListCategory(row) {
           $('#<%=hidType.ClientID%>').val($(row).attr('value'));
           $('#typeName').text($(row).text());
           var CatTypeCode = $(row).attr('value');

           GetCategorysByCatTypeCode(CatTypeCode);
       }

       function GetCategorysByCatTypeCode(CatTypeCode) {
           $.ajax({
               type: 'POST',
               contentType: 'application/json; charset=utf-8',
               dataType: 'json',
               url: 'Models/WebServiceDB.asmx/GetCategorysByCatTypeCode',
               data: "{ 'CatTypeCode': '" + CatTypeCode + "' }",
               async: true,
               success: function (data) {
                   $('#bodyApp').empty();
                   var html = '';
                   $.each($.parseJSON(data.d), function (i, v) {

                       html += '<tr id="tr_' + v.ID + '">';
                       html += '<td>' + (i + 1) + '</td>';
                       html += '<td>' + v.CatCode + '</td>';
                       html += '<td>' + v.CatName + '</td>';
                       html += '<td>' + v.Orders + '</td>';

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
                   bootbox.alert('Error system : -' + er);
               }
           });
       }

       function ListCategoryTypeDetail() {
           $.ajax({
               type: 'POST',
               contentType: 'application/json; charset=utf-8',
               dataType: 'json',
               url: 'Models/WebServiceDB.asmx/ListCategoryType',
               async: true,
               success: function (data) {
                   $('#bodyApp').empty();
                   var html = '';
                   $.each($.parseJSON(data.d), function (i, v) {

                       html += '<tr id="tr_' + v.ID + '">';
                       html += '<td>' + (i + 1) + '</td>';
                       html += '<td>' + v.CatTypeCode + '</td>';
                       html += '<td>' + v.CatTypeName + '</td>';
                       html += '<td>' + v.Orders + '</td>';

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
                   bootbox.alert('Error system : -' + er);
               }
           });
       }

       function GetListCategoryType(row) {
           $('#<%=hidType.ClientID%>').val($(row).attr('value'));
           ListCategoryTypeDetail();
       }

       function ListCategoryType() {
           $.ajax({
               type: 'POST',
               contentType: 'application/json; charset=utf-8',
               dataType: 'json',
               url: 'Models/WebServiceDB.asmx/ListCategoryType',
               async: true,
               success: function (data) {
                   $('#CategoryType').empty();
                   var html = '';
                   html = html + '<li>';
                   html = html + '     <a href="javascript:void(0)" value="CAT_TYPE" onclick="GetListCategoryType(this);"><i class="icon-grid"></i> <span class="text-bold">Catergory Type</span></a>';
                   html = html + '     <ul>';
                   $.each($.parseJSON(data.d), function (i, v) {
                       html = html + '         <li>';
                       html = html + '             <a href="javascript:void(0)" value="' + v.CatTypeCode + '" onclick="ListCategory(this);"><i class="glyphicon glyphicon-hand-right"></i><span>' + v.CatTypeName + '</span></a>';
                       html = html + '         </li>';
                   });
                   html = html + '     </ul>';
                   html = html + '</li>';
                   $('#CategoryType').append(html);
               },
               error: function (er) {
                   bootbox.alert('Error system : -' + er);
               }
           });
       }

    </script>

    <div class="row">

        <div class="col-md-3">
            <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">

                <div class="panel-heading">
                    <h5 class="panel-title"><span class="text-info text-bold">名目 / Danh mục</span></h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                            <li><a data-action="collapse"></a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="sidebar sidebar-default">
                        <ul id="CategoryType" class="navigation navigation-main navigation-accordion"></ul>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-md-9">
            <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
                <div class="panel-heading">
                    <h5 class="panel-title"><span id="typeName" class="text-blue"></span></h5>
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
                                <th class="text-bold  col-lg-1">序號 / STT</th>
                                <th class="text-bold col-lg-2">編號 / Mã</th>
                                <th class="text-bold col-lg-6">名稱 / Tên</th>
                                <th class="text-bold col-lg-1">Order </th>
                                <th class="col-lg-2"></th>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <a href="#" class="btn btn-info btn-sm legitRipple" onclick="GetCodeCategory();">
                                        <i class="icon-add-to-list position-left"></i> 新增 / Thêm mới
                                    </a>
                                </td>

                            </tr>
                        </thead>
                        <tbody id="bodyApp"></tbody>
                    </table>
                </div>
            </div>
        </div>


        <div id="fromCategorys" title="小組更新 / THÊM MỚI HOẶC CẬP NHẬT NHÓM">

            <div class="form-group">
                <label class="control-label col-sm-4 text-bold">小組名稱 / Tên nhóm:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-sm-4 text-bold">序號 / STT:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtOrders" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <asp:HiddenField ID="hidID" runat="server" />
            <asp:HiddenField ID="hidCode" runat="server" />
            <asp:HiddenField ID="hidType" runat="server" />

        </div>
    </div>
</asp:Content>

