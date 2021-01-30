<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListRegisterCancelDocument.aspx.cs" Inherits="HR_SOP.ListRegisterCancelDocument" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var codeRole = '';
        $(document).ready(function () {
            setTimeout(function () {
                ListRegisterCodeDocumentByDCC($('#<%=txtCodeDocument.ClientID%>').val(), $('#<%=txtDocNo.ClientID%>').val(),
                $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
            }, 500);
        });

        function ListRegisterCodeDocumentByDCC(CodeDocument, DocNo, fApplicationDate, tApplicationDate) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListRegisterCodeDocumentByDCC',
                data: "{ 'CodeDocument': '" + CodeDocument + "','DocNo':'" + DocNo
                    + "','StatusDCC' :'1','fApplicationDate': '" + fApplicationDate + "', 'tApplicationDate': '" + tApplicationDate + "' }",
                async: true,
                success: function (data) {
                    $('#bodyApp').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {
                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td>' + v.CodeDocument + '</td>';
                        html += '<td> <a href="RegisterCancelDocument.aspx?CodeDocument=' + v.CodeDocument + '&DocNo=' + v.DocNo + '">' + v.DocNo + '</a></td>';
                        html += '<td>' + v.DocName + '</td>';
                        html += '<td>' + v.HoTen + '</td>';
                        html += '<td>' + v.Department_Text + '</td>';
                        html += '<td>' + v.ApplicationDate_Text + '</td>';
                        html += '</tr>';

                    });
                    $('#bodyApp').append(html);
                },
                error: function (er) {
                    console.log('err');
                }
            });
        }

        function SearchRegisterCodeDocument() {
            ListRegisterCodeDocumentByDCC($('#<%=txtCodeDocument.ClientID%>').val(),$('#<%=txtDocNo.ClientID%>').val(),
                $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5"> 文件編號取消申請 / ĐƠN XIN HỦY MÃ VĂN BẢN</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>
        <div class="panel-body">
            <div class="row">
                 <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Từ ngày <br /> 從日:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtFromApplicationDate" runat="server" CssClass="form-control pickadate-format border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Đến ngày <br /> 到日:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtToApplicationDate" runat="server" CssClass="form-control pickadate-format border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Mã đơn <br /> 申請單編號:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtCodeDocument" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-4">Mã văn bản <br />文件編號:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
               <%-- <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Người tạo <br /> 申請人:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>--%>
               
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple" 
                            onclick="SearchRegisterCodeDocument();">尋找 / Tìm kiếm <i class="glyphicon glyphicon-search position-right"></i></a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-bordered">
                                <thead>
                                    <tr class="bg-info">
                                        <th class="text-bold  col-lg-1">STT <br /> 序號</th>
                                        <th class="text-bold col-lg-1">Mã đơn <br /> 申請單編號 </th>
                                        <th class="text-bold col-lg-2">Mã văn bản <br /> 文件編號</th>
                                        <th class="text-bold col-lg-3">Tên văn bản <br /> 文件名稱</th>
                                        <th class="text-bold col-lg-1">Người tạo <br /> 申請人</th>
                                        <th class="text-bold col-lg-2">Bộ phận xin <br /> 申請部門</th>
                                        <th class="text-bold col-lg-2">Ngày tạo <br /> 申請日</th>
                                    </tr>
                                </thead>
                                <tbody id="bodyApp">
                                    <tr>
                                        <td colspan="6" class="text-center">
                                            <label id="spinner-light-9">
                                                <i class="icon-spinner11 spinner position-left"></i>Loading...
                                            </label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                </div>
            </div>



        </div>

    </div>
</asp:Content>

