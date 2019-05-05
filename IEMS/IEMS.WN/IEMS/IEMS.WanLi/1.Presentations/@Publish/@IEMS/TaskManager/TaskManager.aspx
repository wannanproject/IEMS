<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_TaskManager_TaskManager, IEMS.WanLi.WebSite" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>


    <!--gridMain数据展示-->
    <script type="text/javascript">
        var gridMainFresh = function () {
            var store = App.gridMain.getStore();
            store.currentPage = 1;
            store.pageSize = 15;
            App.panelMainDetail.collapse();
            App.gridMainPagingToolbar.doRefresh();
            return false;
        }
    </script>

    <!--选择数据-->
    <script type="text/javascript">
        var gridPanelCellSelectionChange = function (grid, selected) {
            if (selected && selected.length > 0) {
                var record = selected[selected.length - 1];
                App.panelMainDetail.getForm().loadRecord(record);
            }
        }
    </script>


    <!--业务处理窗口-->
    <script type="text/javascript">
        var refreshRowNum = function (store) {
            for (var i = 0; i < store.data.items.length; i++) {
                store.data.items[i].index = i;
            }
            store.loadRecords(store.data.items, true);
        };
        var showManagerLine = function () {
            var selected = App.gridMain.getSelectionModel().selected.items;
            if (selected && selected.length > 0) {
                App.txtFinishDesc.setValue("");
                var record = selected[selected.length - 1];
                App.winPanelShow.getForm().loadRecord(record);
                return true;
            }
            return false;
        }
        var showManagerWindows = function (constructor) {
            if (!showManagerLine()) {
                Ext.Msg.alert("失败", "请选择需要处理的数据！");
                return;
            }
            var doText = constructor.text;
            App.btnManager.setIconCls(constructor.iconCls);
            App.btnManager.setText(doText);
            App.txtManagerMessage.setText("确定对以下业务信息进行【" + doText + "】处理？");
            var window = App.winManage;
            window.setTitle("业务信息处理：" + doText);
            window.show();
        }

        var closeThisWindow = function () {
            try {
                App.winManage.close();
                gridMainFresh();
            }
            catch (e) {
            }
        }
    </script>


    <!--业务处理-->
    <script type="text/javascript">
        var resultFn = function (command) {
            return {
                success: function (result) {
                    if (result == "") {
                        Ext.Msg.alert("[" + command + "]成功", "数据保存成功！", function (btn) { closeThisWindow() });                     
                    } else {
                        Ext.Msg.alert("[" + command + "失败", result);
                    }

                },
                failure: function (errorMsg) {
                    Ext.Msg.alert("错误", errorMsg);
                }               
            }
        }

        var selectedJsonData = function () {
            var arr = new Array();
            var selected = App.gridMain.getSelectionModel().selected.items;
            if (selected && selected.length > 0) {
                for (var i = 0; i < selected.length; i++) {
                    var item = selected[i];
                    arr.push(item.data);
                }
            }
            return Ext.encode(arr);
        }
        var doSelectedManage = function (constructor) {
            var command = constructor.text;
            debugger;
            var fd = App.txtFinishDesc.value;
            if (command.indexOf("强制取消") >= 0) {
                App.direct.CancelSelectTask(selectedJsonData(), fd , resultFn(command));
            }
            if (command.indexOf("强制完成") >= 0) {
                App.direct.CompleteSelectTask(selectedJsonData(), fd, resultFn(command));
            }
        }
    </script>
</head>
<body>
    <form runat="server">
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="border">
            <Items>
                <ext:Panel runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="查询任务信息" ID="btnSearch">
                                    <Listeners>
                                        <Click Handler="gridMainFresh();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageExcel" Text="导出查询结果" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSpacer />
                                <ext:ToolbarFill />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtSearchTaskNo" runat="server" FieldLabel="作业序号" ReadOnly="false"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                                <ext:TextField ID="txtSearchBinNo" runat="server" FieldLabel="库位编号" ReadOnly="false"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtSearchPalletId" runat="server" FieldLabel="工装编号" ReadOnly="false"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                                <ext:TextField ID="txtSearchListNo" runat="server" FieldLabel="单据编号" ReadOnly="false"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="gridMain" runat="server" Region="Center">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Stop" Text="强制取消" ID="btnCancel">
                                    <Listeners>
                                        <Click Fn="showManagerWindows" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="ControlStop" Text="强制完成" ID="btnComplete">
                                    <Listeners>
                                        <Click Fn="showManagerWindows" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store runat="server" PageSize="15">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="TASK_GUID" />
                                        <ext:ModelField Name="TASK_NO" />
                                        <ext:ModelField Name="EMERGENCY" />
                                        <ext:ModelField Name="IO_TYPE" />
                                        <ext:ModelField Name="TRANSFER_NO" />
                                        <ext:ModelField Name="SLOC_NO" />
                                        <ext:ModelField Name="ELOC_NO" />
                                        <ext:ModelField Name="PALLET_NO" />
                                        <ext:ModelField Name="MATERIAL_NO" />
                                        <ext:ModelField Name="PACKAGE_GUID" />
                                        <ext:ModelField Name="ORDER_LINE_GUID" />
                                        <ext:ModelField Name="SORT_ID" />
                                        <ext:ModelField Name="CREATION_DATE" />
                                        <ext:ModelField Name="TASK_EXEC_START_DT" />
                                        <ext:ModelField Name="TASK_EXEC_END_DT" />
                                        <ext:ModelField Name="LAST_UPDATE_DATE" />
                                        <ext:ModelField Name="FINISH_FLAG" />
                                        <ext:ModelField Name="ERR_NO" />
                                        <ext:ModelField Name="BIN_NO" />
                                        <ext:ModelField Name="USE_QTY" />
                                        <ext:ModelField Name="PALLET_VALID" />
                                        <ext:ModelField Name="TASK_STEP" />

                                        <ext:ModelField Name="STEP_NAME" />
                                        <ext:ModelField Name="ORDER_NO" />
                                        <ext:ModelField Name="LINE_ID" />
                                        <ext:ModelField Name="PACK_TYPE_NAME" />
                                        <ext:ModelField Name="TRANSFER_TYPE_NAME" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:Column runat="server" Text="任务编号" DataIndex="TASK_NO" Width="140" />
                            <ext:Column runat="server" Text="入/出库" DataIndex="IO_TYPE" Width="50" />
                            <ext:Column runat="server" Text="输送设备编号" DataIndex="TRANSFER_NO" Width="80" />
                            <ext:Column runat="server" Text="工装编号" DataIndex="PALLET_NO" Width="120" />
                            <ext:Column runat="server" Text="包装状态" DataIndex="PACK_TYPE_NAME" Width="120" />
                            <ext:Column runat="server" Text="作业步骤" DataIndex="STEP_NAME" Width="80" />
                            <ext:Column runat="server" Text="物料编码" DataIndex="MATERIAL_NO" Width="80" />
                            <ext:Column runat="server" Text="单号" DataIndex="ORDER_NO" Width="120" />
                            <ext:Column runat="server" Text="行号" DataIndex="LINE_ID" Width="40" />
                            <ext:Column runat="server" Text="异常信息" DataIndex="ERR_NAME" Width="80" />
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="gridMainPagingToolbar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel runat="server" Mode="Single" />
                    </SelectionModel>
                    <View>
                        <ext:GridView runat="server" EnableTextSelection="true">
                        </ext:GridView>
                    </View>
                    <Listeners>
                        <SelectionChange Fn="gridPanelCellSelectionChange" />
                    </Listeners>
                </ext:GridPanel>
                <ext:FormPanel ID="panelMainDetail" runat="server" Region="East" BodyPadding="5" Width="200" Collapsible="true" Collapsed="true"
                    Title="详细信息" AutoScroll="true">
                    <FieldDefaults>
                        <CustomConfig>
                            <ext:ConfigItem Name="LabelWidth" Value="56" Mode="Raw" />
                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                        </CustomConfig>
                    </FieldDefaults>
                    <Items>
                        <ext:Container runat="server" Layout="FormLayout" Padding="5" ColumnWidth="1">
                            <Items>
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="输送设备类型" Name="TRANSFER_TYPE_NAME" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="指令起始站台" Name="SLOC_NO" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="指令结束站台" Name="ELOC_NO" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="包号" Name="PACKAGE_GUID" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="库位编号" Name="BIN_NO" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="执行顺序" Name="SORT_ID" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="任务数量" Name="USE_QTY" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="优先级" Name="EMERGENCY" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="创建时间" Name="CREATION_DATE" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="执行时间" Name="TASK_EXEC_START_DT" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:FormPanel>
                <ext:Window ID="winManage" runat="server" Icon="MonitorEdit" Closable="false" Title="强制完成或取消处理"
                    Width="600" Height="450" Resizable="false" Hidden="true" Modal="true" BodyStyle="background-color:#fff;"
                    Layout="border">
                    <Items>
                        <ext:FormPanel runat="server" Region="North"
                            Collapsible="false" Collapsed="false" AutoScroll="false" AutoHeight="true">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:ToolbarSeparator />
                                        <ext:Button runat="server" Icon="Accept" Text="管理" ID="btnManager">
                                            <Listeners>
                                                <Click Fn="doSelectedManage" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator />
                                        <ext:ToolbarSeparator />
                                        <ext:Button runat="server" Icon="Cancel" Text="返回" ID="Button6">
                                            <Listeners>
                                                <Click Handler="closeThisWindow();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator />
                                        <ext:ToolbarSpacer />
                                        <ext:ToolbarFill />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:Container runat="server" Layout="FormLayout" Padding="10" ColumnWidth="1">
                                    <Items>
                                        <ext:Label runat="server" ID="txtManagerMessage" Flex="1" Text="确定需要进行处理" Cls="red-text" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>

                        <ext:FormPanel ID="winPanelShow" runat="server" Region="Center" BodyPadding="5" Width="200" Collapsible="false"
                            Title="任务信息" AutoScroll="true">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="90" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container runat="server" Layout="FormLayout" Padding="5" ColumnWidth="1">
                                    <Items>
                                        <ext:TextField runat="server" ID="txtFinishDesc" Flex="1" LabelAlign="Right" FieldLabel="操作原因描述" Name="FINISH_DESC" LabelStyle="color:red;background-color:yellow;" AllowBlank="false"  />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="任务编号" Name="TASK_NO" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="入/出库" Name="IO_TYPE" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="输送设备编号" Name="TRANSFER_NO" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="工装编号" Name="PALLET_NO" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="包装状态" Name="PACK_TYPE_NAME" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="作业步骤" Name="STEP_NAME" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料编码" Name="MATERIAL_NO" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="单号" Name="ORDER_NO" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="行号" Name="LINE_ID" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="创建时间" Name="CREATION_DATE" />
                                        <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="执行时间" Name="TASK_EXEC_START_DT" />                                
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
