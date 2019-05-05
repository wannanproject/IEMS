<%@ page language="C#" autoeventwireup="true" inherits="Plugins_Main_SysConfig, IEMS.Main.WebSite" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统管理</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" AutoScroll="true">
        <Items>
            <ext:Panel ID="FormPanel1" runat="server" Title="数据库缓存初始化" BodyPadding="5" PaddingSpec="5"
                Width="300" Height="220" Html="
                 &lt;br&gt;
                 清理数据库连接缓存数据。&lt;br&gt;&lt;br&gt;
                 清理缓存时会导致第一次使用数据库时耗用更多时间！。&lt;br&gt;&lt;br&gt;
                 只有数据库配置的SqlMap文件修改，请进行缓存清理工作。&lt;br&gt;&lt;br&gt;
                 ">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbRubberStorage">
                        <Items>
                            <ext:ToolbarSeparator ID="tsBegin1" />
                            <ext:Button runat="server" Icon="ControlRepeatBlue" Text="清理缓存" ID="btnSqlCacheClear">
                                <DirectEvents>
                                    <Click OnEvent="btnSqlCacheClear_Click" />
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator1" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd1" />
                            <ext:ToolbarFill ID="tfEnd1" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
            </ext:Panel> 
            <ext:Panel ID="Panel1" runat="server" Title="界面缓存初始化" BodyPadding="5" PaddingSpec="5"
                Width="300" Height="220" Html="
                 &lt;br&gt;
                 界面配置缓存清理。&lt;br&gt;&lt;br&gt;
                 清理缓存时会导致第一次使用数据库时耗用更多时间！。&lt;br&gt;&lt;br&gt;
                 修改了配置界面后需要进行缓存清理。&lt;br&gt;&lt;br&gt;
                 ">
                <TopBar>
                    <ext:Toolbar runat="server" ID="Toolbar1">
                        <Items>
                            <ext:ToolbarSeparator ID="tsBegin2" />
                            <ext:Button runat="server" Icon="ControlRepeatBlue" Text="清理缓存" ID="btnUiCacheClear">
                                <DirectEvents>
                                    <Click OnEvent="btnUiCacheClear_Click" />
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator2" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd2" />
                            <ext:ToolbarFill ID="tfEnd2" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
            </ext:Panel>
        </Items>
    </ext:Container>
    </form>
</body>
</html>