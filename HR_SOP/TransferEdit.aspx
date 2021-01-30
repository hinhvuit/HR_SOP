<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransferEdit.aspx.cs" Inherits="HR_SOP.TransferEdit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function Update() {

            var StatusEdit = '';
            if ($('#uniform-chkCheckEdit span').attr('class') == 'checked') {
                StatusEdit = '2';
            }
            else {
                StatusEdit = '1';
            }

            var url = $(location).attr('protocol') + '//' + $(location).attr('host');

            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/UpdateRegisterPublishDocumentByPublishDocument',
                data: "{ 'PublishDocument': '" + $('#<%=txtPublishDocument.ClientID%>').val() + "','UserEdit': '" + $('#<%=ddlPerson.ClientID%>').val()
                    + "','StatusEdit':'" + StatusEdit + "','Url':'"+url+"'}",
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
                    bootbox.alert('Error system : -' + er);
                }
            });

        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">文件修改人設定 / THIẾT LẬP NGƯỜI SỦA VĂN BẢN</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>
        <div class="panel-body">

            <div class="row">

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-5">
                            Mã đơn <br /> 文件編號 :</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtPublishDocument" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-7">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">
                            Người sửa <br /> 表單修改人 :</label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlPerson" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-md-1">
                    <div class="form-group">
                        <label class="checkbox-inline text-bold">
                            <input id="chkCheckEdit" type="checkbox" class="styled">
                            修改 / Sửa
                        </label>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="form-group">
                        <a href="javascript:void(0)" class="btn bg-pink btn-rounded width-200" onclick="Update();">確認 / Xác nhận</a>
                    </div>
                </div>

            </div>

        </div>

    </div>
</asp:Content>



