<%@ page language="C#" autoeventwireup="true" inherits="Plugins_Main_Login, IEMS.Main.WebSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WMS智能化物流管理系统-登录</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("./") %>resources/css/login.css" />
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            var flag = false;
            var top = window;
            while (true) {
                if (top.parent) {
                    if (top == top.parent) {
                        break;
                    }
                    top = top.parent;
                    flag = true;
                } else {
                    break;
                }
            }
            if (flag) {
                top.location.href = "<%= Page.ResolveUrl("~/") %>Index.htm";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="content">
        <asp:ValidationSummary ID= "vs" runat="server" ShowMessageBox="false" ShowSummary="true" DisplayMode="SingleParagraph" CssClass="message" />
        <div class="cont">
            <div class="login">
                <table>
                    <tr>
                        <th>
                            用户工号<br />
                            WorkBarcode
                        </th>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="text" />
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="请输入用户名称!" Display="None" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            密 码<br />
                            PassWord
                        </th>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="text" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="请输入密码!" Display="None" />
                        </td>
                    </tr>
                    <tr id="trDbVersion" runat="server">
                        <th>
                            数据版本<br />
                            DataVersion
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlDbVersion" runat="server">
                            
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td  align="right">
                            <asp:CustomValidator ID="cv" runat="server" ControlToValidate="txtUserName" ErrorMessage="用户名称或密码错误!" Display="None" />
                            <asp:Button ID="btnSubmit" runat="server" Text="登 录 / Login" CssClass="btn" 
                                onclick="btnSubmit_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="cp">版权所有 &copy;2002 - 2013 软控股份有限公司</div>
        </div>
    </div>
    </form>
</body>
</html>
