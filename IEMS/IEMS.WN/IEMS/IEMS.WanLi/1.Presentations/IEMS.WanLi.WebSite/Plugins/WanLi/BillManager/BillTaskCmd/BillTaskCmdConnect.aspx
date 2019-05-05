<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillTaskCmdConnect.aspx.cs" Inherits="Plugins_WanLi_BillTaskCmd_BillTaskCmdConnect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单任务指令关联信息</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>

    <!--gridMain数据展示-->
    <script type="text/javascript">
        var gridMainFresh = function () {
            var store = App.gridMain.getStore();
            store.currentPage = 1;
            store.pageSize = 21;
            App.gridMainPagingToolbar.doRefresh();
            return false;
        }
    </script>
    <script type="text/javascript">
        var OrderClick = function (gridview, record) {
            var lineGuid = record.data.ORDER_LINE_GUID;
            App.txtSearchOrderLineGuid.setValue(lineGuid);
            App.gridTaskMainPagingToolbar.doRefresh();
            App.gridCmdMainPagingToolbar.doRefresh();
        }
    </script>
    <script type="text/javascript">
        var TriggerFieldClearData = function (a, b, c, d, e, f) {
            if (c == 0) {
                App.txtSearchLineStatus.setValue("");
            }
        }
    </script>
    <script type="text/javascript">
        var TriggerFieldClearTypeData = function (a, b, c, d, e, f) {
            if (c == 0) {
                App.txtSearchType.setValue("");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="OrderPanel" runat="server" Flex="1" Region="North" Collapsible="true" MultiSelect="false" FolderSort="true"
                            Title="订单信息" TitleAlign="Center" Layout="BorderLayout">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:ToolbarSeparator />
                                        <ext:Button runat="server" Icon="Find" Text="查询订单信息" ID="btnSearch">
                                            <Listeners>
                                                <Click Handler="gridMainFresh();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator />
                                        <ext:ToolbarSpacer />
                                        <ext:ToolbarFill />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:FormPanel runat="server" Layout="ColumnLayout" AutoHeight="true" Region="North">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtSearchLocName" runat="server" FieldLabel="目的站台" ReadOnly="false"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                                <ext:TextField ID="txtSearchMaterialNo" runat="server" FieldLabel="物料信息" ReadOnly="false"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtSearchOrderNo" runat="server" FieldLabel="订单编号" ReadOnly="false"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                                <ext:ComboBox ID="txtSearchLineStatus" runat="server" FieldLabel="执行情况" ReadOnly="false" Editable="false"
                                                    LabelAlign="Right">
                                                    <Items>
                                                        <ext:ListItem Value="-1" Text="所有" />
                                                        <ext:ListItem Value="0" Text="等待" />
                                                        <ext:ListItem Value="1" Text="下达" />
                                                        <ext:ListItem Value="2" Text="执行" />
                                                        <ext:ListItem Value="3" Text="完毕" />
                                                        <ext:ListItem Value="4" Text="取消" />
                                                    </Items>
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldClearData" />
                                                    </Listeners>
                                                </ext:ComboBox>

                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                                <ext:GridPanel ID="gridMain" runat="server" Region="Center">
                                    <Store>
                                        <ext:Store runat="server" PageSize="21" Height="100%">
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.GridPanelBindBillData" />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ORDER_NO" />
                                                        <ext:ModelField Name="LINE_ID" />
                                                        <ext:ModelField Name="LINE_STATUS" />
                                                        <ext:ModelField Name="DELETE_FLAG" />
                                                        <ext:ModelField Name="MATERIAL_NO" />
                                                        <ext:ModelField Name="PRODUCT_GRADE" />
                                                        <ext:ModelField Name="LIMIT_PRODUCT_GUID" />
                                                        <ext:ModelField Name="LIMIT_BIN_NO" />
                                                        <ext:ModelField Name="ELOC_NO" />
                                                        <ext:ModelField Name="ELOC_NAME" />
                                                        <ext:ModelField Name="LIMIT_PALLET_ID" />
                                                        <ext:ModelField Name="BIN_ERR_DESC" />
                                                        <ext:ModelField Name="LOCK_END_LOC" />
                                                        <ext:ModelField Name="LINE_TASK_DESC" />
                                                        <ext:ModelField Name="LINE_TASK_FLAG" />
                                                        <ext:ModelField Name="REQUIRE_QTY" />
                                                        <ext:ModelField Name="ACT_QTY" />
                                                        <ext:ModelField Name="SHIP_QTY" />
                                                        <ext:ModelField Name="ORDER_LINE_GUID" />
                                                        <ext:ModelField Name="CREATION_DATE" Type="Date" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="columnModel" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn Width="45" runat="server" />
                                            <ext:Column runat="server" DataIndex="ORDER_NO" Text="订单号" Width="120" />
                                            <ext:Column runat="server" DataIndex="ELOC_NO" Text="目的地编号" Width="120" />
                                            <ext:Column runat="server" DataIndex="ELOC_NAME" Text="目的地" Width="140" />
                                            <ext:Column runat="server" DataIndex="LIMIT_BIN_NO" Text="锁定库位" Width="120" />
                                            <ext:Column runat="server" DataIndex="MATERIAL_NO" Text="物料编码" Width="120" />
                                            <ext:Column runat="server" DataIndex="PRODUCT_GRADE" Text="物料等级" Width="80" />
                                            <ext:Column runat="server" DataIndex="REQUIRE_QTY" Text="单据数量" Width="80" />
                                            <ext:Column runat="server" DataIndex="ACT_QTY" Text="已生成任务数量" Width="120" />
                                            <ext:Column runat="server" DataIndex="SHIP_QTY" Text="实发数量" Width="80" />
                                            <ext:Column runat="server" DataIndex="LINE_TASK_DESC" Text="单据任务描述" Width="120" />
                                            <ext:DateColumn runat="server" DataIndex="CREATION_DATE" Text="开始日期" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                                            <ext:Column runat="server" DataIndex="LINE_STATUS" Text="单据行状态" Width="80" />
                                            <ext:Column runat="server" DataIndex="LINE_ID" Text="行号" Width="60" />
                                            <ext:Column runat="server" DataIndex="LIMIT_PALLET_ID" Text="托盘号" Width="100" />
                                            <ext:Column runat="server" DataIndex="LIMIT_BIN_NO" Text="起始地" Width="100" Hidden="true" />
                                            <ext:Column runat="server" DataIndex="LIMIT_PRODUCT_GUID" Text="货物GUID" Width="120" Hidden="true" />
                                            <ext:Column runat="server" DataIndex="DELETE_FLAG" Text="删除标志" Width="80" />
                                            <ext:Column runat="server" DataIndex="BIN_ERR_DESC" Text="库存异常信息" Width="120" />
                                            <ext:Column runat="server" DataIndex="LOCK_END_LOC" Text="锁定目标" Width="80" Hidden="true" />
                                            <ext:Column runat="server" DataIndex="LINE_TASK_FLAG" Text="出库任务分解" Width="80" Hidden="true" />
                                            <ext:Column runat="server" DataIndex="ORDER_LINE_GUID" Text="订单GUID" Width="120" Hidden="true" />
                                        </Columns>
                                    </ColumnModel>
                                    <Listeners>
                                        <ItemClick Fn="OrderClick"></ItemClick>
                                    </Listeners>
                                    <BottomBar>
                                        <ext:PagingToolbar ID="gridMainPagingToolbar" runat="server">
                                            <Plugins>
                                                <ext:ProgressBarPager runat="server" />
                                            </Plugins>
                                        </ext:PagingToolbar>
                                    </BottomBar>
                                    <View>
                                        <ext:GridView runat="server" StripeRows="true" TrackOver="true" />
                                    </View>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="Panel2" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                            <Items>
                                <ext:Panel ID="TaskPanel" runat="server" Flex="1" Region="West" Collapsible="true" RootVisible="false" MultiSelect="true" FolderSort="true"
                                    Title="任务信息" TitleAlign="Center" Layout="BorderLayout">
                                    <Items>
                                        <ext:FormPanel runat="server" Layout="ColumnLayout" AutoHeight="true" Hidden="true" Region="North">
                                            <Items>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                                    <Items>
                                                        <ext:TextField ID="txtSearchOrderLineGuid" runat="server" FieldLabel="订单Guid" ReadOnly="false"
                                                            LabelAlign="Right">
                                                        </ext:TextField>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FormPanel>
                                        <ext:GridPanel ID="GridPanel1" runat="server" Region="Center">
                                            <Store>
                                                <ext:Store runat="server" PageSize="8">
                                                    <Proxy>
                                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindTaskData" />
                                                    </Proxy>
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="TASK_NO" />
                                                                <ext:ModelField Name="TASK_GUID" />
                                                                <ext:ModelField Name="IO_TYPE" />
                                                                <ext:ModelField Name="TRANSFER_NO" />
                                                                <ext:ModelField Name="SLOC_NAME" />
                                                                <ext:ModelField Name="ELOC_NAME" />
                                                                <ext:ModelField Name="PALLET_NO" />
                                                                <ext:ModelField Name="MATERIAL_NO" />
                                                                <ext:ModelField Name="ORDER_LINE_GUID" />
                                                                <ext:ModelField Name="CREATION_DATE" Type="Date" />
                                                                <ext:ModelField Name="TASK_EXEC_START_DT" Type="Date" />
                                                                <ext:ModelField Name="TASK_EXEC_END_DT" Type="Date" />
                                                                <ext:ModelField Name="USE_QTY" />
                                                                <ext:ModelField Name="TASK_STEP" />
                                                                <ext:ModelField Name="FINISH_DESC" />
                                                                <ext:ModelField Name="EMERGENCY" />
                                                                <ext:ModelField Name="PACKAGE_GUID" />
                                                                <ext:ModelField Name="SORT_ID" />
                                                                <ext:ModelField Name="ROUTE_NOS" />
                                                                <ext:ModelField Name="UNEXE_ROUTE_NO" />

                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel runat="server">
                                                <Columns>
                                                    <ext:RowNumbererColumn Width="35" runat="server" />
                                                    <ext:Column runat="server" DataIndex="TASK_NO" Text="任务号" Width="80" />
                                                    <ext:Column runat="server" DataIndex="IO_TYPE" Text="出/入库" Width="60" />
                                                    <ext:Column runat="server" DataIndex="PALLET_NO" Text="托盘信息" Width="100" />
                                                    <ext:Column runat="server" DataIndex="TASK_STEP" Text="任务步骤" Width="100" />
                                                    <ext:Column runat="server" DataIndex="SLOC_NAME" Text="起始点" Width="100" />
                                                    <ext:Column runat="server" DataIndex="ELOC_NAME" Text="目的地" Width="100" />
                                                    <ext:Column runat="server" DataIndex="TRANSFER_NO" Text="输送设备" Width="100" />
                                                    <ext:Column runat="server" DataIndex="SORT_ID" Text="执行顺序" Width="80" />
                                                    <ext:Column runat="server" DataIndex="MATERIAL_NO" Text="物料信息" Width="100" />
                                                    <ext:Column runat="server" DataIndex="USE_QTY" Text="工装数量" Width="60" />
                                                    <ext:Column runat="server" DataIndex="FINISH_DESC" Text="结束描述" Width="80" />
                                                    <ext:Column runat="server" DataIndex="EMERGENCY" Text="优先级" Width="80" />
                                                    <ext:Column runat="server" DataIndex="ROUTE_NOS" Text="路径串" Width="100" />
                                                    <ext:Column runat="server" DataIndex="UNEXE_ROUTE_NO" Text="未生成指令路径" Width="100" />

                                                    <ext:DateColumn runat="server" DataIndex="CREATION_DATE" Text="开始日期" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                                                    <ext:DateColumn runat="server" DataIndex="TASK_EXEC_START_DT" Text="任务开始执行时间" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                                                    <ext:DateColumn runat="server" DataIndex="TASK_EXEC_END_DT" Text="任务结束时间" Width="140" Format="yyyy-MM-dd HH:mm:ss" />

                                                    <ext:Column runat="server" DataIndex="ORDER_LINE_GUID" Text="订单GUID" Width="100" Hidden="true" />
                                                    <ext:Column runat="server" DataIndex="TASK_GUID" Text="任务GUID" Width="100" Hidden="true" />
                                                    <ext:Column runat="server" DataIndex="PACKAGE_GUID" Text="包Guid" Width="120" Hidden="true" />
                                                    <ext:Column runat="server" DataIndex="LIMIT_PRODUCT_GUID" Text="货物GUID" Width="120" Hidden="true" />
                                                </Columns>
                                            </ColumnModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="gridTaskMainPagingToolbar" runat="server">
                                                    <Plugins>
                                                        <ext:ProgressBarPager runat="server" />
                                                    </Plugins>
                                                </ext:PagingToolbar>
                                            </BottomBar>
                                            <View>
                                                <ext:GridView runat="server" StripeRows="true" TrackOver="true" />
                                            </View>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="CmdPanel" runat="server" Flex="1" Region="Center" Collapsible="true" RootVisible="false" MultiSelect="true" FolderSort="true"
                                    Title="指令信息" TitleAlign="Center" Layout="BorderLayout">
                                    <Items>
                                        <ext:FormPanel runat="server" Layout="ColumnLayout" AutoHeight="true" Hidden="true" Region="North">
                                            <Items>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                                    <Items>
                                                        <ext:TextField ID="txtSearchOrderGuid" runat="server" FieldLabel="订单Guid" ReadOnly="false"
                                                            LabelAlign="Right" Hidden="true">
                                                        </ext:TextField>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FormPanel>
                                        <ext:GridPanel ID="GridPanel2" runat="server" Region="Center">
                                            <Store>
                                                <ext:Store runat="server" PageSize="8" Height="30%">
                                                    <Proxy>
                                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindCmdData" />
                                                    </Proxy>
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="OBJID" />
                                                                <ext:ModelField Name="TASK_GUID" />
                                                                <ext:ModelField Name="TASK_NO" />
                                                                <ext:ModelField Name="CMD_TYPE" />
                                                                <ext:ModelField Name="SLOC_PLC_NO" />
                                                                <ext:ModelField Name="ELOC_PLC_NO" />
                                                                <ext:ModelField Name="CREATION_DATE" Type="Date" />
                                                                <ext:ModelField Name="EXCUTE_DATE" Type="Date" />
                                                                <ext:ModelField Name="FINISH_DATE" Type="Date" />
                                                                <ext:ModelField Name="TRANSFER_TYPE" />
                                                                <ext:ModelField Name="ROUTE_NO" />
                                                                <ext:ModelField Name="TRANSFER_NO" />
                                                                <ext:ModelField Name="PACKAGE_GUID" />
                                                                <ext:ModelField Name="PALLET_NO" />
                                                                <ext:ModelField Name="ORDER_LINE_GUID" />
                                                                <ext:ModelField Name="CMD_STEP" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel runat="server">
                                                <Columns>
                                                    <ext:RowNumbererColumn Width="45" runat="server" />
                                                    <ext:Column runat="server" DataIndex="CMD_STEP" Text="指令步骤" Width="80" />
                                                    <ext:Column runat="server" DataIndex="OBJID" Text="指令号" Width="80" />
                                                    <ext:Column runat="server" DataIndex="CMD_TYPE" Text="指令类型" Width="80" />
                                                    <ext:Column runat="server" DataIndex="SLOC_PLC_NO" Text="起始点" Width="100" />
                                                    <ext:Column runat="server" DataIndex="ELOC_PLC_NO" Text="目的地" Width="100" />
                                                    <ext:Column runat="server" DataIndex="TASK_NO" Text="任务号" Width="80" />
                                                    <ext:Column runat="server" DataIndex="PALLET_NO" Text="RFID号" Width="100" />
                                                    <ext:Column runat="server" DataIndex="TRANSFER_TYPE" Text="输送设备类型" Width="100" />
                                                    <ext:Column runat="server" DataIndex="TRANSFER_NO" Text="输送设备" Width="100" />

                                                    <ext:Column runat="server" DataIndex="ROUTE_NO" Text="路径编号" Width="80" />

                                                    <ext:DateColumn runat="server" DataIndex="CREATION_DATE" Text="开始日期" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                                                    <ext:DateColumn runat="server" DataIndex="EXCUTE_DATE" Text="执行时间" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                                                    <ext:DateColumn runat="server" DataIndex="FINISH_DATE" Text="结束时间" Width="140" Format="yyyy-MM-dd HH:mm:ss" />

                                                    <ext:Column runat="server" DataIndex="TASK_GUID" Text="任务GUID" Width="100" Hidden="true" />
                                                    <ext:Column runat="server" DataIndex="PACKAGE_GUID" Text="包GUID" Width="80" Hidden="true" />
                                                    <ext:Column runat="server" DataIndex="ORDER_LINE_GUID" Text="订单GUID" Width="100" Hidden="true" />
                                                </Columns>
                                            </ColumnModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="gridCmdMainPagingToolbar" runat="server">
                                                    <Plugins>
                                                        <ext:ProgressBarPager runat="server" />
                                                    </Plugins>
                                                </ext:PagingToolbar>
                                            </BottomBar>
                                            <View>
                                                <ext:GridView runat="server" StripeRows="true" TrackOver="true" />
                                            </View>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
