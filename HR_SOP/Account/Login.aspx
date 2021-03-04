<%@ Page Title="Log in" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="HR_SOP.Account.Login" Async="true" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Đăng nhập hệ thống</title>
    <!-- Global stylesheets -->
    <link href="../assets/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css">
    <link href="../assets/css/bootstrap.css" rel="stylesheet" type="text/css">
    <link href="../assets/css/core.css" rel="stylesheet" type="text/css">
    <link href="../assets/css/components.css" rel="stylesheet" type="text/css">
    <link href="../assets/css/colors.css" rel="stylesheet" type="text/css">


    <!-- /global stylesheets -->
    <!-- Core JS files -->
    <script type="text/javascript" src="../assets/js/plugins/loaders/pace.min.js"></script>
    <script type="text/javascript" src="../assets/js/core/libraries/jquery.min.js"></script>
    <script type="text/javascript" src="../assets/js/core/libraries/bootstrap.min.js"></script>
    <script type="text/javascript" src="../assets/js/plugins/loaders/blockui.min.js"></script>
    <!-- /core JS files -->
    <!-- Theme JS files -->
    <script type="text/javascript" src="../assets/js/core/app.js"></script>
    <script type="text/javascript" src="../assets/js/plugins/ui/ripple.min.js"></script>
    <!-- /theme JS files -->

    <script type="text/javascript">
        $(document).ready(function () {
            $('#error').hide();
            $('#<%=txtPassword.ClientID%>').keydown(function (e) {
                if (e.which == 13) {
                    Login();
                }
            })

        });

        function Login() {
            var user = $('#<%=txtUsername.ClientID%>').val();
            var pass = $('#<%=txtPassword.ClientID%>').val();

            if (user == '') {
                $('#error').show();
                $('#error_d').html('帳號 / Nhập tên đăng nhập');
            }
            else if (pass == '') {
                $('#error').show();
                $('#error_d').html('密碼 / Nhập mật khẩu');
            }
            else {
                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    url: '/Models/WebServiceDB.asmx/CheckLogin',
                    data: "{ 'Username': '" + user + "', 'Password': '" + pass + "' }",
                    async: true,
                    success: function (data) {
                        if ($.parseJSON(data.d) == 'SUCCESS') {
                            if ($(location).attr('search') == '') {
                                location.href = "/Default.aspx";
                            }
                            else {
                                location.href = $(location).attr('search').replace('?Url=', '');
                            }
                        }
                        else if ($.parseJSON(data.d) == 'ERROR') {
                            $('#error').show();
                            $('#error_d').html('帳號有錯誤 / Tên đăng nhập hoặc mật khẩu sai');
                            $('#<%=txtUsername.ClientID%>').focus();
                        }
                        else {
                            $('#error').show();
                            $('#error_d').html('Error - ' + data.d[0].Info);
                        }
                    },
                    error: function (er) {
                        console.log('err');
                    }
                });
        }
}
    </script>
</head>
<body class="login-container bg-blue-300">
    <form runat="server">
        <!-- Page container -->
        <div class="page-container">
            <!-- Page content -->
            <div class="page-content">
                <!-- Main content -->
                <div class="content-wrapper">
                    <!-- Content area -->
                    <div class="content">
                        <div class="panel panel-body login-form login-container width-390">
                            
                            <div class="col-sm-12">
                                <div class="text-center">
                                    <span style="font-size:20px !important;font-weight:bold !important;">越南廠區</span> 
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="text-center">
                                    <span style="font-size:20px !important;font-weight:bold !important;">中央SOP管理系統</span> 
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <table class="table table-borderless">
                                    <tr>
                                        <td colspan="2">
                                            <div id="error" class="alert alert-danger no-border">
                                                <span id="error_d" class="text-semibold"></span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th class="col-sm-4 text-right">
                                            <span style="font-size:12px !important;font-weight:bold !important;">賬號:</span> 
                                        </th>
                                        <td class="col-sm-8">
                                                <asp:TextBox ID="txtUsername" runat="server" 
                                                    class="form-control border-bottom-blue-800" 
                                                    placeholder="賬號"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-sm-4 text-right">
                                            <span style="font-size:12px !important;font-weight:bold !important;">密碼:</span> 
                                        </td>
                                        <td class="col-sm-8">
                                                <asp:TextBox ID="txtPassword" runat="server" class="form-control border-bottom-blue-800" 
                                                    placeholder="密碼" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div class="form-group col-sm-6">
                                                <a href="javascript:void(0)" class="btn btn-default" onclick="Login();">
                                                   <span style="font-size:14px !important;font-weight:bold !important;">登入 / Đăng nhập</span>
                                                </a>
                                            </div>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </div>

                        <!-- Footer -->
                        <div class="footer text-muted text-center text-white">
                            Download <a href="../Lib/IE11-Windows6.1-x64-en-us.exe" title="IE11"><i class="icon-download"></i>IE11-64</a>
                             Or <a href="../Lib/IE11-Windows6.1-x86-en-us.exe" title="IE11"><i class="icon-download"></i>IE11-86</a>
                            <br />
                            &copy; 2017. <a href="#">Limitless Web </a> by <a href="#" target="_blank">Foxconn</a>
                        </div>
                        <!-- /footer -->
                    </div>
                    <!-- /content area -->
                </div>
                <!-- /main content -->
            </div>
            <!-- /page content -->
        </div>
        <!-- /page container -->
    </form>
</body>
</html>

