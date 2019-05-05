<%@ page language="C#" autoeventwireup="true" inherits="Plugins_Main_SysUser_MyUser, IEMS.Main.WebSite" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统用户设置</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
    </script>

    <!--特殊-->
    <script type="text/javascript">
        var btnCancel_click = function () {
            window.location.href = window.location.href;
            parent.App.Manager_System_SysUser_MyUser_Window.close();
            return false;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="northToolbar">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="UserEdit" Text="确认修改" ID="btnEdit">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击确认修改" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnEdit_Click">
                                            <EventMask ShowMask="true" Msg="修改中，请稍候..." MinDelay="50" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                <ext:Button runat="server" Icon="UserAlert" Text="取消修改" ID="btnCancel">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击取消修改" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="btnCancel_click"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:Panel>
                <ext:Panel ID="panelNorthQuery" runat="server" Region="Center">
                    <Items>
                        <ext:Container ID="container_Query" runat="server" Layout="ColumnLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" Padding="5" ColumnWidth="1">
                                    <Items>
                                        <ext:TextField ID="txtUserCode" runat="server" FieldLabel="HR代码" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtUserName" runat="server" FieldLabel="用户名称" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtUserRealName" runat="server" FieldLabel="真实姓名" LabelAlign="Right" />
                                        <ext:BoxSplitter ID="BoxSplitter1" runat="server" Height="10" />
                                        <ext:TextField ID="txtUserPassword" InputType="Password" runat="server" FieldLabel="原密码" LabelAlign="Right" />
                                        <ext:TextField ID="txtUserNewPassword1" InputType="Password" runat="server" FieldLabel="新密码" LabelAlign="Right" />
                                        <ext:TextField ID="txtUserNewPassword2" InputType="Password" runat="server" FieldLabel="确认密码" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
