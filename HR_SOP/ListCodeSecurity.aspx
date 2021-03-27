<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListCodeSecurity.aspx.cs" Inherits="HR_SOP.ListCodeSecurity" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var codeRole = '';
        $(document).ready(function () {
            <%--if($('#<%=hidCheckWaitCode.ClientID%>').val() == '1')
            {
                $('#uniform-chkCheckWaitCode span').attr('class', 'checked');
            }--%>
            setTimeout(function () {
                <%--var checkWait = '';
                if ($('#uniform-chkCheckWaitCode span').attr('class') == 'checked') {
                    checkWait = $('#<%=hidUserName.ClientID%>').val();
                }
                else {
                    checkWait = '';
                }--%>
                ListRegisterCodeDocument($('#<%=txtCodeDocument.ClientID%>').val(), $('#<%=txtCreateBy.ClientID%>').val(),
                    $('#<%=ddlDepartment.ClientID%>').val(),$('#<%=txtDocNoDCC.ClientID%>').val(),$('#<%=txtDocNameDCC.ClientID%>').val(),
                $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
            }, 500);

        });

        function ListRegisterCodeDocument(CodeDocument, CreatedBy,Department,DocNo,DocName, fApplicationDate, tApplicationDate) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListRegisterCodeSecurity',
                data: "{ 'CodeDocument': '" + CodeDocument + "', 'CreatedBy': '" + CreatedBy + "','States':'','CheckWait' :'','Type':'','Department':'"
                    + Department + "','DocNo':'" + DocNo + "','DocName':'" + DocName + "', 'fApplicationDate': '"
                    + fApplicationDate + "', 'tApplicationDate': '" + tApplicationDate + "' }",
                async: true,
                success: function (data) {
                    $('#bodyApp').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {

                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td> <a href="RegisterCodeSecurity.aspx?CodeDocument=' + v.CodeDocument + '">' + v.CodeDocument + '</a></td>';
                        html += '<td>' + v.DocNo + '</td>';
                        html += '<td>' + v.DocName + '</td>';
                        html += '<td>' + v.HoTen + '</td>';
                        html += '<td>' + v.Department_Text + '</td>';
                        html += '<td>' + v.States_Text + '</td>';
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
           <%-- var checkWait = '';
                if ($('#uniform-chkCheckWaitCode span').attr('class') == 'checked') {
                    checkWait = $('#<%=hidUserName.ClientID%>').val();
        014585
                }
                else {
                    checkWait = '';
                }--%>
            ListRegisterCodeDocument($('#<%=txtCodeDocument.ClientID%>').val(), $('#<%=txtCreateBy.ClientID%>').val(),
                    $('#<%=ddlDepartment.ClientID%>').val(),$('#<%=txtDocNoDCC.ClientID%>').val(),$('#<%=txtDocNameDCC.ClientID%>').val(),
                $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">文件編號清單 / DANH SÁCH MÃ VĂN BẢN </h5>
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
                        <label class="control-label text-bold col-md-3">Từ ngày
                            <br />
                            從日:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtFromApplicationDate" runat="server" CssClass="form-control pickadate-format border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Đến ngày
                            <br />
                            到日:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtToApplicationDate" runat="server" CssClass="form-control pickadate-format border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                 <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Người tạo
                            <br />
                            申請人:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Mã đơn
                            <br />
                            申請單編號:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtCodeDocument" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
               

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">
                            Mã văn bản
                            <br />
                            文件編號:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDocNoDCC" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">
                            Tên văn bản
                            <br />
                            文件名稱:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDocNameDCC" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Bộ phận xin
                            <br />
                            申請部門:</label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="select-border-color"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <%-- <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Tình trạng <br /> 狀況:</label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="cboStates" runat="server" data-placeholder="Select a states..." class="select-size-lg" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>--%>



                <%--<div class="col-md-4">
                    <div class="form-group">
                        <label class="checkbox-inline text-bold">
                            <input id="chkCheckWaitCode" type="checkbox" class="styled">
                            Check Wait
                        </label>
                        <asp:HiddenField ID="hidCheckWaitCode" runat="server" />
                    </div>
                </div>--%>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple" onclick="SearchRegisterCodeDocument();">尋找 / Tìm kiếm 
                            <i class="glyphicon glyphicon-search position-right"></i></a>
                            <asp:HiddenField ID="hidUserName" runat="server" />
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
                                            <th class="text-bold  col-lg-1">STT<br />序號</th>
                                            <th class="text-bold col-lg-1">Mã đơn<br /> 申請單編號</th>
                                            <th class="text-bold col-lg-1">Mã văn bản<br />文件編號</th>
                                            <th class="text-bold col-lg-2">Tên văn bản<br />文件名稱</th>
                                            <th class="text-bold col-lg-1">Người tạo<br />申請人</th>
                                            <th class="text-bold col-lg-2">Bộ phận xin <br /> 申請部門</th>
                                            <th class="text-bold col-lg-2">Tình trạng ký duyệt<br />簽核狀態</th>
                                            <th class="text-bold col-lg-2">Ngày tạo<br />申請日</th>
                                        </tr>
                                    </thead>
                                    <tbody id="bodyApp">
                                        <tr>
                                            <td colspan="8" class="text-center">
                                                <label id="spinner-light-8">
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

