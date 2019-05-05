<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_BillManager_Output_SearchBillOutput, IEMS.WanLi.WebSite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>查询单据信息</title>


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
            var html = "<iframe src='ModifyBillOutput.aspx?OrderNo="+ OrderNo + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
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
                                                <ext:DateField ID="txtBillBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" Editable="false" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                 <ext:ComboBox ID="txtBillStatus" runat="server" FieldLabel="单据状态" ReadOnly="false" Editable="false"
                                                    LabelAlign="Right">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clear();" />
                                                    </Listeners>
                                                    <Items>
                                                        <ext:ListItem Value="0" Text="未执行" />
                                                        <ext:ListItem Value="1" Text="出库中" />
                                                        <ext:ListItem Value="2" Text="已完成" />
                                                        <ext:ListItem Value="3" Text="已取消" />
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:DateField ID="txtBillEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" Editable="false" />
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
                        <ext:Store runat="server" PageSize="15">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ORDER_NO" />
                                        <ext:ModelField Name="STATUS_NAME" />
                                        <ext:ModelField Name="ORDER_TYPE_NAME" />
                                        <ext:ModelField Name="CREATION_DATE" Type="Date"  />
                                        <ext:ModelField Name="ORDER_PRIORITY" />
                                        <ext:ModelField Name="SOURCE_TYPE" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="45" runat="server" />
                            <ext:Column runat="server" DataIndex="ORDER_NO" Text="单号" Width="160"  />
                            <ext:Column runat="server" DataIndex="STATUS_NAME" Text="单据状态" Width="100" />
                            <ext:Column runat="server" DataIndex="ORDER_TYPE_NAME" Text="单据类型" Width="100" />
                            <ext:DateColumn runat="server" DataIndex="CREATION_DATE" Text="创建日期" Width="160" Format="yyyy-MM-dd HH:mm:ss" />
                            <ext:Column runat="server" DataIndex="ORDER_PRIORITY" Text="优先级" Width="80" />
                            <ext:Column runat="server" DataIndex="SOURCE_TYPE" Text="生成方式" Width="100" />
                            <ext:CommandColumn runat="server" Width="122" Text="操作" Align="Center">
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
