<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_BillData_SearchBillOutput, IEMS.WanLi.WebSite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查询单据信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--gridMain数据展示-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var gridMainFresh = function () {
            var store = App.gridMain.getStore();
            store.currentPage = 1;
            store.pageSize = 15;
            App.gridMainPagingToolbar.doRefresh();
            return false;
        }
    </script>
    <!--添加-->
    <script type="text/javascript">
        Ext.create("Ext.window.Window", {
            id: "Plugins_Wms_OutputWork_OutPutReceipt_AddOutPutReceipt_Window",
            html: "<iframe src='AddOutPutReceipt.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            bodyPadding: 5,
            maximized: true,
            hidden: true,
            draggable: false,
            resizable: false,
            closable: false,
            title: "添加出库单",
            titleAlign: "center",
            modal: true
        })
        var postBillData = function () {
            var window = App.Plugins_Wms_OutputWork_OutPutReceipt_AddOutPutReceipt_Window;
            window.setTitle("添加出库单");
            var html = "<iframe src='AddBillOutput.aspx?rand=" + Math.random() + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (window.getBody()) {
                window.getBody().update(html);
            } else {
                window.html = html;
            }
            window.show();
        }
    </script>
    <!--end 添加-->
    <script type="text/javascript">
        var commandcolumn_click = function (command, record) {
            if (command.toLowerCase() == "edit") {
                showModifyWindows(record);
            }
            if (command.toLowerCase() == "delete") {
                commandcolumn_click_confirm(command, record);
            }
            return false;
        };
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) {
                    if (btn == "yes") {
                        deleteRecordData(record)
                    }
                });
            }
            return false;
        };
    </script>
    <!--end 修改删除-->
    <!--修改删除-->
    <script type="text/javascript">
        var showModifyWindows = function (record) {
            var OrderNo = record.data.ORDER_NO;
            var window = App.Plugins_Wms_OutputWork_OutPutReceipt_AddOutPutReceipt_Window;
            window.setTitle("修改出库单");
            var html = "<iframe src='ModifyBillOutput.aspx?OrderNo=" + OrderNo + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (window.getBody()) {
                window.getBody().update(html);
            } else {
                window.html = html;
            }
            window.show();
        }
    </script>
    <script type="text/javascript">
        var deleteRecordData = function (record) {
            var OrderNo = record.data.ORDER_NO;
            var OrderStatus = record.data.OrderStatus;
            App.direct.DeleteBillData(OrderNo);
            App.gridMainPagingToolbar.doRefresh();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="border">
            <Items>
                <ext:Panel runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="TableSave" Text="添加单据信息" ID="btnAddBill">
                                    <Listeners>
                                        <Click Handler="postBillData();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Find" Text="查询单据信息" ID="btnSearch">
                                    <Listeners>
                                        <Click Handler="gridMainFresh();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSpacer />
                                <ext:TextField ID="txtStoreWhNo" runat="server" Visible="false"></ext:TextField>
                                <ext:ToolbarFill />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtBillListNo" runat="server" FieldLabel="出库单号" ReadOnly="false"
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
                    <Store>
                        <ext:Store runat="server" PageSize="100">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="STATUS_NAME" />
                                        <ext:ModelField Name="ORDER_NO" />
                                        <ext:ModelField Name="ORDER_STATUS" />
                                        <ext:ModelField Name="ORDER_TYPE_NO" />
                                        <ext:ModelField Name="ORDER_TYPE_MODULE" />
                                        <ext:ModelField Name="CREATION_DATE" Type="Date" />
                                        <ext:ModelField Name="CREATED_BY" />
                                        <ext:ModelField Name="FMEM" />
                                        <ext:ModelField Name="LOCK_LINE_FLAG" />
                                        <ext:ModelField Name="ORDER_PRIORITY" />
                                        <ext:ModelField Name="AUDIT_FLAG" />
                                        <ext:ModelField Name="SOURCE_TYPE" />
                                        <ext:ModelField Name="REQUEST_OBJID" />
                                        <ext:ModelField Name="ORDER_LINE_GUID" />
                                        <ext:ModelField Name="ORDER_NO" />
                                        <ext:ModelField Name="LINE_ID" />
                                        <ext:ModelField Name="LINE_STATUS" />
                                        <ext:ModelField Name="DELETE_FLAG" />
                                        <ext:ModelField Name="SORT_ID" />
                                        <ext:ModelField Name="MATERIAL_NO" />
                                        <ext:ModelField Name="PRODUCT_GRADE" />
                                        <ext:ModelField Name="SLOC_NO" />
                                        <ext:ModelField Name="ELOC_NO" />
                                        <ext:ModelField Name="REQUIRE_QTY" />
                                        <ext:ModelField Name="ACT_QTY" />
                                        <ext:ModelField Name="SHIP_QTY" />
                                        <ext:ModelField Name="LIMIT_DATE_START" Type="Date" />
                                        <ext:ModelField Name="LIMIT_DATE_END" Type="Date" />
                                        <ext:ModelField Name="LIMIT_BIN_NO" />
                                        <ext:ModelField Name="LIMIT_PRODUCT_GUID" />
                                        <ext:ModelField Name="LIMIT_BATCH_NO" />
                                        <ext:ModelField Name="LIMIT_PALLET_ID" />
                                        <ext:ModelField Name="LINE_PRIORITY" />
                                        <ext:ModelField Name="LINE_TASK_FLAG" />
                                        <ext:ModelField Name="LAST_TASK_TIME" />
                                        <ext:ModelField Name="BIN_ERR_DESC" />
                                        <ext:ModelField Name="FMEM" />
                                        <ext:ModelField Name="LOCK_END_LOC" />
                                        <ext:ModelField Name="SLOC_NAME" />
                                        <ext:ModelField Name="ELOC_NAME" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="45" runat="server" />
                            <ext:Column runat="server" DataIndex="ORDER_NO"         Text="单号" Width="120" />
                            <ext:Column runat="server" DataIndex="LINE_ID"          Text="行号" Width="60" />
                            <ext:Column runat="server" DataIndex="MATERIAL_NO"      Text="物料编码" Width="140" />
                            <ext:Column runat="server" DataIndex="PRODUCT_GRADE"    Text="物料等级" Width="60" />
                            <ext:Column runat="server" DataIndex="LIMIT_BATCH_NO"   Text="限定批次" Width="100" />
                            <ext:Column runat="server" DataIndex="LIMIT_BIN_NO"     Text="限定库位" Width="80" />
                            <ext:Column runat="server" DataIndex="LIMIT_PALLET_ID"  Text="限定工装" Width="80" />
                            
                            <ext:Column runat="server" DataIndex="ELOC_NAME"        Text="目标机台"  Width="120" />
                            <ext:Column runat="server" DataIndex="REQUIRE_QTY"      Text="需求数量" Width="80" />
                            <ext:Column runat="server" DataIndex="ACT_QTY"          Text="任务数量" Width="80" />
                            <ext:DateColumn runat="server" DataIndex="CREATION_DATE" Text="创建日期" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                            <ext:Column runat="server" DataIndex="BIN_ERR_DESC"     Text="库存提示" Width="120" />
                            <ext:CommandColumn runat="server" Width="122"           Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="gridMainPagingToolbar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                    <View>
                        <ext:GridView runat="server" EnableTextSelection="true">
                        </ext:GridView>
                    </View>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
