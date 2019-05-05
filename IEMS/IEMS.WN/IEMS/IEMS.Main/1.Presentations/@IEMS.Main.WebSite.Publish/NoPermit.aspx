<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无权使用此功�?/title>

    <!--特殊-->
    <script type="text/javascript">
        var dologout = function (btn) {
            var top = window;
            while (true) {
                if (top.parent) {
                    if (top == top.parent) {
                        break;
                    }
                    top = top.parent;
                } else {
                    break;
                }
            }
            top.top.location.href = "<%= Page.ResolveUrl("./") %>Logout.htm";
        }
        var closeTable = function (btn) {
            try {
                var tab = parent.App.mainTabPanel.getActiveTab();
                if (tab) {
                    tab.close();
                }
            }
            catch (ex) {
                dologout(btn);
            }
        }
        Ext.onReady(function () {
            Ext.Msg.alert('提示', '无权使用此功能，请确认权限！', function (btn) { closeTable(btn) });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
    </form>
</body>
</html>