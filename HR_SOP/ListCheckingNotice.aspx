﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListCheckingNotice.aspx.cs" Inherits="HR_SOP.ListCheckingNotice" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var codeRole = '';
        $(document).ready(function () {
            if ($('#<%=hidCheckWaitPublish.ClientID%>').val() == '1') {
                $('#uniform-chkCheckWaitPublish span').attr('class', 'checked');
            }

            setTimeout(function () {
                var checkWait = '';
                if ($('#uniform-chkCheckWaitPublish span').attr('class') == 'checked') {
                    checkWait = $('#<%=hidUserName.ClientID%>').val();
                }
                else {
                    checkWait = '';
                }

                var Person = '';
                if ($('#uniform-chkPerson span').attr('class') == 'checked') {
                    Person = $('#<%=hidUserName.ClientID%>').val();
                }
                else {
                    Person = '';
                }

                ListCheckingNotice($('#<%=txtCodeDocument.ClientID%>').val(), $('#<%=txtCreateBy.ClientID%>').val(), $('#<%=cboStates.ClientID%>').val(),
                    checkWait, Person, $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
            }, 500);
        });

        function ListCheckingNotice(CodeCheck, CreatedBy, States, CheckWait, Person, fApplicationDate, tApplicationDate) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListCheckingNotice',
                data: "{ 'CodeCheck': '" + CodeCheck + "', 'CreatedBy': '" + CreatedBy + "','States':'" + States + "','CheckWait': '" + CheckWait
                    + "','Person': '" + Person + "','fApplicationDate': '" + fApplicationDate + "', 'tApplicationDate': '" + tApplicationDate + "' }",
                async: true,
                success: function (data) {
                    $('#bodyApp').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {

                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td> <a href="CheckingNotice.aspx?CodeCheck=' + v.CodeCheck + '">' + v.CodeCheck + '</a></td>';
                        html += '<td>' + v.DocumentNo + '</td>';
                        html += '<td>' + v.HoTen + '</td>';
                        html += '<td>' + v.States_Text1 + '</td>';
                        html += '<td>' + v.Person_Name + '</td>';
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

        function SearchListCheckingNotice() {
            var checkWait = '';
            if ($('#uniform-chkCheckWaitPublish span').attr('class') == 'checked') {
                checkWait = $('#<%=hidUserName.ClientID%>').val();
            }
            else {
                checkWait = '';
            }

            var Person = '';
            if ($('#uniform-chkPerson span').attr('class') == 'checked') {
                    Person = $('#<%=hidUserName.ClientID%>').val();
                }
                else {
                    Person = '';
                }

            ListCheckingNotice($('#<%=txtCodeDocument.ClientID%>').val(), $('#<%=txtCreateBy.ClientID%>').val(), $('#<%=cboStates.ClientID%>').val(),
                    checkWait, Person, $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
        }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">SOP 檢查名單 / DANH SÁCH KIỂM TRA SOP</h5>
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
                        <label class="control-label text-bold col-md-3">
                            Mã đơn:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtCodeDocument" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">
                            Người tạo
                            <br />
                            申請人:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">
                            Tình trạng
                            <br />
                            狀況:</label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="cboStates" runat="server" data-placeholder="Select a states..." class="select-size-lg" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">
                            Từ ngày <br /> 從日:

                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtFromApplicationDate" runat="server" CssClass="form-control pickadate-format"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">
                            Đến ngày <br /> 到日:

                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtToApplicationDate" runat="server" CssClass="form-control pickadate-format"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="form-group">
                        <label class="checkbox-inline text-bold">
                            <input id="chkCheckWaitPublish" type="checkbox" class="styled">
                            Đợi ký <br /> 待簽
                        </label>
                        <asp:HiddenField ID="hidCheckWaitPublish" runat="server" />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="checkbox-inline text-bold">
                            <input id="chkPerson" type="checkbox" class="styled">
                            Đợi sửa <br /> 待修
                        </label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple"
                                onclick="SearchListCheckingNotice();">尋找 / Tìm kiếm <i class="glyphicon glyphicon-search position-right"></i></a>
                            <asp:HiddenField ID="hidUserName" runat="server" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="table-responsive pre-scrollable">
                                <table class="table table-bordered">
                                    <thead>
                                        <tr class="bg-info">
                                            <th class="text-bold  col-lg-1">Order<br />序號</th>
                                            <th class="text-bold col-lg-2">Mã đơn<br />文件編號</th>
                                            <th class="text-bold col-lg-2">Mã văn kiện<br />文件編號</th>
                                            <th class="text-bold col-lg-1">Người tạo<br />申請人</th>
                                            <th class="text-bold col-lg-2">Đợi ký<br /> 待核准</th>
                                            <th class="text-bold col-lg-2">Đợi sửa<br />待修</th>
                                            <th class="text-bold col-lg-1">Tình trạng<br />狀況</th>
                                            <th class="text-bold col-lg-1">Ngày tạo<br />申請日</th>
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


