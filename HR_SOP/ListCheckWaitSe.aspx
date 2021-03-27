<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListCheckWaitSe.aspx.cs" Inherits="HR_SOP.ListCheckWaitSe" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var codeRole = '';
        $(document).ready(function () {
           
            setTimeout(function () {
                var checkWait = '';
                if ($('#uniform-chkCheckWait span').attr('class') == 'checked') {
                    checkWait = $('#<%=hidUserName.ClientID%>').val();
                }
                else {
                    checkWait = '';
                }
                ListCheckWait($('#<%=txtDocNo.ClientID%>').val(), $('#<%=txtDcc.ClientID%>').val(),$('#<%=hidUserName.ClientID%>').val(),checkWait,
                $('#<%=txtCreateBy.ClientID%>').val(), $('#<%=txtFromApplicationDate.ClientID%>').val(),$('#<%=txtToApplicationDate.ClientID%>').val(),
                $('#<%=cboStates.ClientID%>').val());
            }, 500);
        });

        function ListCheckWait(Code, Dcc, Department, CheckWait, CreatedBy, sFromDate, sToDate, Type) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListCheckWaitSe',
                data: "{ 'Code': '" + Code + "', 'Dcc': '" + Dcc + "','Department':'" + Department + "','CheckWait': '" + CheckWait
                    + "','CreatedBy': '" + CreatedBy + "','sFromDate': '" + sFromDate + "','sToDate': '" + sToDate + "','Type': '" + Type + "' }",
                async: true,
                success: function (data) {
                    $('#bodyApp').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {

                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td>';

                        if (v.Code == 'CODE') {
                            html += '<td> <a href="RegisterCodeSecurity.aspx?CodeDocument=' + v.CodeDocument + '">' + v.CodeDocument + '</a></td>';
                        }
                        else if (v.Code == 'PUB'){
                            html += '<td> <a href="RegisterPublishSecurity.aspx?PublishDocument=' + v.CodeDocument + '">' + v.CodeDocument + '</a></td>';
                        }
                        else if (v.Code == 'EDIT') {
                            html += '<td> <a href="RegisterEditSecurity.aspx?EditDocument=' + v.CodeDocument + '">' + v.CodeDocument + '</a></td>';
                        }
                        else if (v.Code == 'CANCEL') {
                            html += '<td> <a href="RegisterCancelSecurity.aspx?CancelDocument=' + v.CodeDocument + '">' + v.CodeDocument + '</a></td>';
                        }
                        else if (v.Code == 'OBS') {
                            html += '<td> <a href="ApplicationObsoletedSecurity.aspx?ObsoletedDocument=' + v.CodeDocument + '">' + v.CodeDocument + '</a></td>';
                        }
                        else {
                            html += '<td>' + v.CodeDocument + '</td>';
                        }

                        html += '<td>' + v.DocumentNo + '</td>';
                        html += '<td>' + v.CreatedBy + '</td>';
                        html += '<td>' + v.Department_Name + '</td>';
                        html += '<td>' + v.CheckWait + '</td>';
                        html += '<td>' + v.CatName + '</td>';
                        html += '<td>' + v.CreatedDate + '</td>';
                        html += '<td>' + v.Code_Name + '</td>';
                        html += '</tr>';

                    });
                    $('#bodyApp').append(html);
                },
                error: function (er) {
                    console.log('err');
                }
            });
        }

        function SearchListCheckWait() {
            var checkWait = '';
            if ($('#uniform-chkCheckWait span').attr('class') == 'checked') {
                checkWait = $('#<%=hidUserName.ClientID%>').val();
           }else {
                checkWait = '';
            }
            ListCheckWait($('#<%=txtDocNo.ClientID%>').val(), $('#<%=txtDcc.ClientID%>').val(),$('#<%=hidUserName.ClientID%>').val(),checkWait,
                $('#<%=txtCreateBy.ClientID%>').val(), $('#<%=txtFromApplicationDate.ClientID%>').val(),$('#<%=txtToApplicationDate.ClientID%>').val(),
                $('#<%=cboStates.ClientID%>').val());
        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">待簽清單 / DANH SÁCH ĐỢI KÝ</h5>
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
                        <label class="control-label text-bold col-md-3">Mã đơn <br /> 申請單編號:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Mã văn bản <br /> 文件編號:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDcc" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>

                 <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Người tạo<br />申請人:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="form-control border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3"> Từ ngày <br /> 從日:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtFromApplicationDate" runat="server" CssClass="form-control pickadate-format border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Đến ngày <br /> 到日:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtToApplicationDate" runat="server" CssClass="form-control pickadate-format border-bottom-blue-800"></asp:TextBox>
                        </div>
                    </div>
                </div>


                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Loại văn bản <br /> 文件類型 </label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="cboStates" runat="server" data-placeholder="Select a states..." class="select-border-color" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="checkbox-inline text-bold">
                            <input id="chkCheckWait" type="checkbox" class="styled">
                            Đợi ký/ 待簽
                        </label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple"
                                onclick="SearchListCheckWait();">尋找 / Tìm kiếm <i class="glyphicon glyphicon-search position-right"></i></a>
                            <asp:HiddenField ID="hidUserName" runat="server" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-md-12">
                    <div class="form-group">
                    <div class="col-md-12">
                        <%--pre-scrollable--%>
                        <div class="table-responsive">
                            <table class="table table-bordered">
                                <thead>
                                    <tr class="bg-info">
                                        <th class="text-bold  col-lg-1">STT<br />序號</th>
                                        <th class="text-bold col-lg-1">Mã đơn<br />申請單編號 </th>
                                        <th class="text-bold col-lg-1">Mã văn bản<br />文件編號</th>
                                        <th class="text-bold col-lg-1">Người tạo<br /> 申請人</th>
                                        <th class="text-bold col-lg-2">Bộ phận xin <br /> 申請部門</th>
                                        <th class="text-bold col-lg-1">Đợi ký <br /> 待核准</th>
                                        <th class="text-bold col-lg-2">Tình trạng ký duyệt <br />簽核狀態</th>
                                        <th class="text-bold col-lg-1">Ngày tạo <br /> 申請日</th>
                                        <th class="text-bold col-lg-2">Loại văn bản <br /> 文件類型 </th>
                                    </tr>
                                </thead>
                                <tbody id="bodyApp">
                                    <tr>
                                        <td colspan="9" class="text-center">
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
