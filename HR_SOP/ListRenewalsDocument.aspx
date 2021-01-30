﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListRenewalsDocument.aspx.cs" Inherits="HR_SOP.ListRenewalsDocument" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var codeRole = '';
        $(document).ready(function () {
            if($('#<%=hidCheckWaitCode.ClientID%>').val() == '1')
            {
                $('#uniform-chkCheckWaitCode span').attr('class', 'checked');
            }
            setTimeout(function () {
                var checkWait = '';
                if ($('#uniform-chkCheckWaitCode span').attr('class') == 'checked') {
                    checkWait = $('#<%=hidUserName.ClientID%>').val();
                }
                else {
                    checkWait = '';
                }
                ListRenewalsDocument($('#<%=txtRenewalCode.ClientID%>').val(), $('#<%=txtCreateBy.ClientID%>').val(),
                $('#<%=cboStates.ClientID%>').val(), checkWait,$('#<%=txtDCC_NO.ClientID%>').val(),
                $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
            }, 500);

        });

        function ListRenewalsDocument(RenewalCode, CreatedBy, States, CheckWait,DocumentNo, fApplicationDate, tApplicationDate) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                url: 'Models/WebServiceDB.asmx/ListRenewalsDocument',
                data: "{ 'RenewalCode': '" + RenewalCode + "', 'CreatedBy': '" + CreatedBy + "','States':'" + States + "','CheckWait' :'"
                    + CheckWait + "','DocumentNo':'" + DocumentNo + "', 'fApplicationDate': '" + fApplicationDate + "', 'tApplicationDate': '" + tApplicationDate + "' }",
                async: true,
                success: function (data) {
                    $('#bodyApp').empty();
                    var html = '';
                    $.each($.parseJSON(data.d), function (i, v) {

                        html += '<tr>';
                        html += '<td>' + (i + 1) + '</td>';
                        html += '<td> <a href="RenewalsDocument.aspx?RenewalCode=' + v.RenewalCode + '">' + v.RenewalCode + '</a></td>';
                        html += '<td>' + v.DCC_NO + '</td>';
                        html += '<td>' + v.HoTen + '</td>';
                        html += '<td>' + v.States_Text1 + '</td>';
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

        function SearchListRenewalsDocument() {
            var checkWait = '';
                if ($('#uniform-chkCheckWaitCode span').attr('class') == 'checked') {
                    checkWait = $('#<%=hidUserName.ClientID%>').val();
                }
                else {
                    checkWait = '';
                }
                ListRenewalsDocument($('#<%=txtRenewalCode.ClientID%>').val(), $('#<%=txtCreateBy.ClientID%>').val(),
                $('#<%=cboStates.ClientID%>').val(), checkWait,$('#<%=txtDCC_NO.ClientID%>').val(),
                $('#<%=txtFromApplicationDate.ClientID%>').val(), $('#<%=txtToApplicationDate.ClientID%>').val());
    }

    </script>

    <div class="panel panel-flat border-top-grey-300 border-right-grey-300 border-bottom-grey-300 border-left-grey-300">
        <div class="panel-heading">
            <h5 class="bg-info p-5">文件延期名單 / DANH SÁCH GIA HẠN VĂN KIỆN</h5>
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
                        <label class="control-label text-bold col-md-3">Mã đơn <br /> 申請單編碼:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtRenewalCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Mã văn kiện <br /> 文件編碼 :</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDCC_NO" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Người tạo <br /> 申請人:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Tình trạng <br /> 狀況:</label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="cboStates" runat="server" data-placeholder="Select a states..." class="select-size-lg" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Từ ngày <br /> 從日:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtFromApplicationDate" runat="server" CssClass="form-control pickadate-format"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label text-bold col-md-3">Đến ngày <br /> 到日:</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtToApplicationDate" runat="server" CssClass="form-control pickadate-format"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="checkbox-inline text-bold">
                            <input id="chkCheckWaitCode" type="checkbox" class="styled">
                            Đợi ký <br /> 待簽
                        </label>
                        <asp:HiddenField ID="hidCheckWaitCode" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <a href="javascript:void(0)" class="btn btn-info btn-sm legitRipple" onclick="SearchListRenewalsDocument();">尋找 / Tìm kiếm 
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
                        <div class="table-responsive pre-scrollable">
                            <table class="table table-bordered">
                                <thead>
                                    <tr class="bg-info">
                                        <th class="text-bold  col-lg-1">Order <br /> 序號</th>
                                        <th class="text-bold col-lg-2">Mã đơn <br /> 申請單編碼</th>
                                        <th class="text-bold col-lg-2">Mã văn kiện <br /> 申請單編碼</th>
                                        <th class="text-bold col-lg-1">Người tạo <br /> 申請人</th>
                                        <th class="text-bold col-lg-2">Đợi ký <br /> 待核准</th>
                                        <th class="text-bold col-lg-2">Tình trạng <br /> 狀況</th>
                                        <th class="text-bold col-lg-2">Ngày tạo <br /> 申請日</th>
                                    </tr>
                                </thead>
                                <tbody id="bodyApp">
                                    <tr>
                                        <td colspan="7" class="text-center">
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


