<%@ page language="C#" autoeventwireup="true" inherits="McUI_Crud, IEMS.Frame.WebSite" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <webpage></webpage>
    <!--通用-->
    <script type="text/javascript" src="../../resources/js/jquery-1.7.1.js"></script>
    <link rel="Stylesheet" type="text/css" href="../resources/css/Main.css" />
    <link rel="Stylesheet" type="text/css" href="../resources/css/ext-chinese-font.css" />
    <script type="text/javascript">
        //列表刷新数据重载方法
        var gridPanelMainFresh = function () {
            App.gridPanelMainStore.currentPage = 1;
            App.gridPanelMainPageToolbar.doRefresh();
            return false;
        }
        var search = function () {
            App.hiddenIsSearchAllInfo.setValue("0");
            gridPanelMainFresh();
            return false;
        }
        var searchAll = function () {
            App.hiddenIsSearchAllInfo.setValue("");
            gridPanelMainFresh();
            return false;
        }

        var _winAddShow = function () {
            for (var i = 0; i < App.viewport.items.length; i++) {
                App.viewport.getComponent(i).disable(true);
            }
            try {
                winAddShow();
            }
            catch (e) {
            }
            return false;
        }

        var _winAddHide = function () {
            for (var i = 0; i < App.viewport.items.length; i++) {
                App.viewport.getComponent(i).enable(true);
            }
            try {
                winAddHide();
            }
            catch (e) {
            }
            return false;
        }

        var _winAddAfterRender = function () {
            try {
                winAddAfterRender();
            }
            catch (e) {
            }
            return false;
        }

        var _winUpdateShow = function () {
            for (var i = 0; i < App.viewport.items.length; i++) {
                App.viewport.getComponent(i).disable(true);
            }
            try {
                winUpdateShow();
            }
            catch (e) {
            }
            return false;
        }

        var _winUpdateHide = function () {
            for (var i = 0; i < App.viewport.items.length; i++) {
                App.viewport.getComponent(i).enable(true);
            }
            try {
                winUpdateHide();
            }
            catch (e) {
            }
            return false;
        }

        var _winUpdateAfterRender = function () {
            try {
                winUpdateAfterRender();
            }
            catch (e) {
            }
            return false;
        }

        var _viewportAfterRender = function () {
            try {
                viewportAfterRender();
            }
            catch (e) {
            }
            return false;
        }
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
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="search">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行信息添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                
                                <ext:ToolbarSeparator ID="ToolbarSeparator1" />
                                <ext:Hidden runat="server" ID="hiddenIsSearchAllInfo" Text="0" />
                                <ext:Button runat="server" Icon="BasketDelete" Text="历史查询" ID="btnSearchAll">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行查询所有信息" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="searchAll">
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
                                <ext:ToolbarSeparator ID="tsEnd" />
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
                        <ext:Store ID="gridPanelMainStore" runat="server" PageSize="15">
                            <Model>
                                <ext:Model ID="gridPanelMainModel" runat="server">
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel" runat="server">
                        <Columns>
                            <ext:CommandColumn runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Accept" CommandName="Recover" Text="恢复本条数据" MinWidth="118">
                                        <ToolTip Text="恢复本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar Fn="prepareToolbar" />
                                <Listeners>
                                    <Command Handler="return commandcolumn_click (command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView runat="server" EnableTextSelection="true">
                            <GetRowClass Fn="setRowClass" />
                        </ext:GridView>
                    </View>
                    <BottomBar>
                        <ext:PagingToolbar ID="gridPanelMainPageToolbar" runat="server">
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>

                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Layout="FitLayout" Modal="false"
                    Height="400" Width="600">
                    <Items>
                        <ext:FormPanel ID="formPanelAdd" runat="server" MonitorValid="true" BodyPadding="5">
                            <Items>
                                <ext:Container ID="containerAddParam" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="btnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="保存中..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Fn="_winAddShow">
                        </Show>
                        <Hide Fn="_winAddHide">
                        </Hide>
                        <AfterRender Fn="_winAddAfterRender">
                        </AfterRender>
                    </Listeners>
                </ext:Window>

                <ext:Window ID="winUpdate" runat="server" Icon="MonitorEdit" Closable="false" Layout="FitLayout" Modal="false">
                    <Items>
                        <ext:FormPanel ID="formPanelModify" runat="server" MonitorValid="true" BodyPadding="5">
                            <Items>
                                <ext:Container ID="containerModifyParam" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="btnModifySave_Click">
                                    <EventMask ShowMask="true" Msg="保存中..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Fn="_winUpdateShow">
                        </Show>
                        <Hide Fn="_winUpdateHide">
                        </Hide>
                        <AfterRender Fn="_winUpdateAfterRender">
                        </AfterRender>
                    </Listeners>
                </ext:Window>
            </Items>
            <Listeners>
                <AfterRender Fn="_viewportAfterRender"></AfterRender>
            </Listeners>
        </ext:Viewport>
    </form>
</body>
</html>
