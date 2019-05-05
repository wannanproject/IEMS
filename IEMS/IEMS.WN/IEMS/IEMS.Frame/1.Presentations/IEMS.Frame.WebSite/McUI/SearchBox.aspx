<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchBox.aspx.cs" Inherits="McUI_SearchBox" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!--通用-->
    <script Type="text/javascript" src="../../resources/js/jquery-1.7.1.js"></script>
    <link rel="Stylesheet" Type="text/css" href="../resources/css/Main.css" />
    <link rel="Stylesheet" Type="text/css" href="../resources/css/ext-chinese-font.css" />
    <script Type="text/javascript">
        //列表刷新数据重载方法
        var GridPanelCenterFresh = function () {
            App.gridPanelMainStore.currentPage = 1;
            App.gridPanelMainPageToolbar.doRefresh();
            return false;
        }
        var commandColumn_click = function (command, record) {
            return response(command, record);
        }
        var cellDblClick = function (Grid, td, tdindex, record, tr, trindex, e, fn) {
            return response('dblclick', record);
        }

        var _viewportAfterRender = function () {
            try {
                viewportAfterRender();
            }
            catch (e) {
            }
            return false;
        }

        window.onload = function () {
            if (parent && parent.document) {
                var iframes = parent.document.getElementsByTagName("iframe");
                if (iframes) {
                    for (var i = 0; i < iframes.length; i++) {
                        if (iframes[i].contentWindow == window) {
                            iframes[i].Height = iframes[i].parentElement.style.Height;
                        }
                    }
                }
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--数据导出-->
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="resourceManager" runat="server" />
    <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="northToolbar">
                        <Items>
                            <ext:ToolbarSpacer runat="server" ID="tspacerBegin" />
                            <ext:Hidden runat="server" ID="hiddenIsSearchAllInfo" Text="0" />
                            <ext:Button runat="server" Icon="Find" Text="查询信息" ID="btnSearch">
                                <ToolTips>
                                    <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="GridPanelCenterFresh">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="PageExcel" Text="导出查询结果" ID="btnExport">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="panelNorthQueryParam" runat="server" AutoHeight="true">
                        <Items>
                            <ext:Container ID="containerQueryParam" runat="server" Layout="ColumnLayout" AutoHeight="true">
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="gridPanelMain" runat="server" Region="Center">
                <Store>
                    <ext:Store ID="gridPanelMainStore" runat="server">
                        <Model>
                            <ext:Model ID="GridPanelmodel" runat="server">
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel" runat="server">
                    <Columns>
                        <ext:CommandColumn ID="commandColumn" runat="server" Width="60" Text="确认" Align="Center">
                            <Commands>
                                <ext:GridCommand Icon="accept" CommandName="Select" Text="确认">
                                    <ToolTip Text="确认使用本条数据" />
                                </ext:GridCommand>
                            </Commands>
                            <PrepareToolbar />
                            <Listeners>
                                <Command Handler="return commandColumn_click(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>
                    </Columns>
                </ColumnModel>
                <Listeners>
                    <CellDblClick Fn="cellDblClick" />
                </Listeners>
                <BottomBar>
                    <ext:PagingToolbar ID="gridPanelMainPageToolbar" runat="server">
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
        </Items>
        <Listeners>
            <AfterRender Fn="_viewportAfterRender"></AfterRender>
        </Listeners>
    </ext:Viewport>
    </form>
</body>
</html>
