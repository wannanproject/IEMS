<%@ page language="C#" autoeventwireup="true" inherits="Plugins_Main_MainFrame, IEMS.Main.WebSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WMS带束层智能化物流管理系统</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("./") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script language="javascript" type="text/javascript" src="<%= Page.ResolveUrl("./") %>resources/Js/jquery-1.7.1.js">
    </script>
    <script language="javascript" type="text/javascript" src="<%= Page.ResolveUrl("./") %>resources/Js/main.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input runat="server" id="hiddenTaskCount" style="display: none" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="BorderPanelNorth" runat="server" Height="50" Region="North" Header="false"
                    Border="false" ContentEl="head_div">
                </ext:Panel>
                <ext:Panel ID="BorderPanelWest" runat="server" Collapsible="true" Layout="accordion"
                    Region="West" Split="true" Title="系统功能导航" Width="200" Icon="BrickLink">
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="24">
                        </ext:StatusBar>
                    </BottomBar>
                    <Items>
                        <ext:TreePanel ID="mainTreePanel" runat="server" Title="系统菜单" Icon="Cart"
                            SingleExpand="false" RootVisible="false" Border="false" TagString="0">
                            <Store>
                                <ext:TreeStore ID="mainTreeStore" runat="server" OnReadData="GetUserPageGroup">
                                    <Proxy>
                                        <ext:PageProxy>
                                            <RequestConfig Method="GET" Type="Load" />
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Root>
                                        <ext:Node NodeID="Root" Expanded="true" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <Listeners>
                                <BeforeLoad Fn="nodeLoad" />
                                <ItemClick Handler="loadPage(record,e)" />
                            </Listeners>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="p_center" runat="server" Layout="Fit" Region="Center">
                    <Items>
                        <ext:TabPanel ID="mainTabPanel" runat="server" Border="false" MinTabWidth="115">
                            <Plugins>
                                <ext:TabCloseMenu ID="TabCloseMenu1" runat="server" CloseTabText="关闭" CloseAllTabsText="关闭全部"
                                    CloseOthersTabsText="关闭其他">
                                </ext:TabCloseMenu>
                            </Plugins>
                            <Items>
                                <ext:Panel runat="server" Title="物流系统" Closable="false">
                                    <Loader runat="server" Url="MainTab/Index.html" Renderer="loader.renderer='frame';loader.target.reload(false);">
                                    </Loader>
                                </ext:Panel>
                            </Items>
                            <Listeners>
                                <BeforeRemove Fn="onTabsRemove">
                                </BeforeRemove>
                            </Listeners>
                        </ext:TabPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="p_south" runat="server" Height="30" Region="South" Header="false"
                    Border="false" ContentEl="foot_div">
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <div id="head_div" class='t'>
            <div class='h'>
                <div class='logo'>
                    <img alt="MESNAC.COM" src='<%= Page.ResolveUrl("./") %>resources/images/login_logo.png' />
                </div>
                <a style="float: right; cursor: default;"><em style="padding: 2px;"><span id="sp1"
                    onclick="setTheme('Gray');this.style.border='2px solid';document.getElementById('sp2').style.border='1px solid'"
                    style="background: #e5ecf0; width: 10px; height: 1px; border: 1px solid; font-size: 6px;">&nbsp;&nbsp;&nbsp; </span></em></a><a style="float: right;"><em style="padding: 2px; cursor: default;"><span id="sp2" onclick="setTheme('Default');this.style.border='2px solid';document.getElementById('sp1').style.border='1px solid'"
                        style="background: #28ddf6; width: 10px; height: 1px; border: 1px solid; font-size: 6px;">&nbsp;&nbsp;&nbsp; </span></em></a>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="head_menu" OnClientClick="logout(this);return false">退出系统</asp:LinkButton>
                <%-- <asp:LinkButton ID="LinkButton5" runat="server" CssClass="head_menu" OnClientClick="aboutRDTeam(this);return false">研发团队</asp:LinkButton>--%>
                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="head_menu" OnClientClick="clickMenu(this);return false">系统帮助</asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="head_menu" OnClientClick="user(this);return false">用户设置</asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="head_menu" OnClientClick="task(this);return false">待办任务</asp:LinkButton>
                <asp:LinkButton ID="LinkButton5" runat="server" CssClass="head_menu" OnClientClick="refresh(this);return false">刷新选项卡</asp:LinkButton>
            </div>
        </div>
        <div id="foot_div" class='foot'>
            <div  style="width:0px; height:0px;">
                <iframe src='http://172.20.254.21:8080/MesIndex.aspx?UserName=<%= getloginUser() %>' scrolling="no" style="width:0px; height:0px"></iframe>
            </div>
            软控股份有限公司 &copy;<%= DateTime.Now.ToString("yyyy") %> 智能装备系统事业部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;V&nbsp;<%= Vision() %>&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </form>
</body>
</html>
