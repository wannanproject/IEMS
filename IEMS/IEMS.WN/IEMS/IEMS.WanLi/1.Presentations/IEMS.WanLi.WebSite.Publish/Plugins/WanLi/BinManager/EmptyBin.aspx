<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_BinManager_EmptyBin, IEMS.WanLi.WebSite" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>异常库位维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>SearchErrOutLocData.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    
    <!--gridMain数据展示-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var gridMainFresh = function () {
            var store = App.gridMain.getStore();    
            store.currentPage = 1;
            store.pageSize = 50;
            App.gridMainPagingToolbar.doRefresh();
            return false;
        }
    </script>
    <script type="text/javascript">
        var commandcolumn_click = function (command, record) {
            if (command.toLowerCase() == "clear".toLowerCase()) {
                clearEmptyBin(record);
            }
            if (command.toLowerCase() == "add".toLowerCase()) {
                showAddWindows(record);
            }
        }
    </script>
    <script type="text/javascript">
        var clearEmptyBin = function (record) {
            if (!record) {
                Ext.Msg.alert("提示", "请选择需要清理状态的库位！");
                return;
            }
            var binBizStatus = record.data.BIN_BIZ_STATUS;
            if (binBizStatus != "E") {
                Ext.Msg.alert("错误", "仅可清除[空出库]状态,其他异常请执行出库操作！");
                return;
            }
            var msg = "确定将空出库工位【" + record.data.BIN_PLC_NO + "】" + "设置为空库位？";
            Ext.Msg.confirm("提示", msg, function (btn) {
                if (btn == "yes") {
                    var binNo = record.data.BIN_PLC_NO;
                    App.direct.ClearEmptyBin(binNo, {
                        success: function (result) {
                            if (result == "") {
                                gridMainFresh();
                                Ext.Msg.alert('成功', "数据保存成功！");
                            } else {
                                Ext.Msg.alert("失败", result);
                            }
                        },
                        failure: function (errorMsg) {
                            Ext.Msg.alert("错误", errorMsg);
                        }
                    });
                }
            });
            return false;
        }
    </script>

     <script type="text/javascript">
         var saveAddLineWindows = function () {
             
             return false;
         }
    </script>
    <script type="text/javascript">
        var showAddWindows = function (record) {
            if (!record) {
                Ext.Msg.alert("提示", "请选择需要异常出库的库位！");
                return;
            }
            var binNo = record.data.BIN_PLC_NO;
            var bizStatus = record.data.BIN_BIZ_STATUS;
            if (bizStatus == "E") {
                Ext.Msg.alert("提示", "空出库库位无法创建订单,请直接清除状态！");
                return;
            }
            App.direct.GetNewOrderNo( {
                        success: function (result) {
                            App.txtOrderNo.setValue(result);
                        },
                        failure: function (errorMsg) {
                            Ext.Msg.alert("错误", errorMsg);
                        }
                    });
            App.txtOrderLineId.setValue("1");
            App.txtAddEndLocNo.setValue("");
            App.txtAddBinNo.setValue(binNo);
            App.winAdd.show();
            return false;
        }
     </script>

    <script type="text/javascript">
        var saveAddWindows = function () {
            var orderId = App.txtOrderLineId.getValue();
            var orderNo = App.txtOrderNo.getValue();
            var binNo = App.txtAddBinNo.getValue();
            var eLocNo = App.txtAddEndLocNo.getValue();

            if (eLocNo == null || eLocNo == "") {
                Ext.Msg.alert("错误", '请选择目的站点!');
                return;
            }
            if ((binNo == null || binNo == "")) {
                Ext.Msg.alert("错误", '请勿删除库存编码!');
                return;
            }
            App.direct.CreatOrder(orderId, orderNo, binNo, eLocNo, {
                success: function (result) {
                    if (result == "") {
                        Ext.Msg.alert('成功', "生成出库订单成功！");
                    }
                    else {
                        Ext.Msg.alert('错误', result);
                    }
                    App.winAdd.close();
                    gridMainFresh();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('异常', errorMsg);
                }
            });

            return false;
        };
    </script>

    <script type="text/javascript">
        //历史查询根据DeleteFlag的值进行样式绑定
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if ((record.get("CRN_IN_ENABLE") == "0") || (record.get("CRN_OUT_ENABLE") == "0")) {
                return "x-grid-row-deleted";
            }
        }
        var template = '<span style="background:{0};width:70px;display:block;text-align:center;">{1}</span>';
        var pctChange = function (value) {
            return Ext.String.format(template, (value == "1") ? "green" : "red", value);
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
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
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
                </ext:Panel>

                <ext:Panel ID="Panel1" runat="server" Flex="1" Region="Center" Collapsible="true" RootVisible="false" MultiSelect="true" FolderSort="true"
                    Title="异常库位信息" TitleAlign="Center" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="gridMain" runat="server" Region="Center">
                            <Store>
                                <ext:Store runat="server" PageSize="50">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                                <ext:ModelField Name="BIN_NO" />
                                                <ext:ModelField Name="BIN_PLC_NO" />
                                                <ext:ModelField Name="USED_FLAG" />
                                                <ext:ModelField Name="BIN_SIZE" />
                                                <ext:ModelField Name="BIN_STATUS" />
                                                <ext:ModelField Name="BIN_BIZ_STATUS" />
                                                <ext:ModelField Name="BIN_WEIGHT" />
                                                <ext:ModelField Name="BIN_AREA" />
                                                <ext:ModelField Name="CRN_NO" />
                                                <ext:ModelField Name="GROUP_NO" />
                                                <ext:ModelField Name="ORDER_LINE_GUID" />
                                                <ext:ModelField Name="BIN_AGV_NO" />
                                                <ext:ModelField Name="ERR_NAME" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn Width="35" runat="server" />
                                    <ext:Column runat="server" DataIndex="BIN_PLC_NO" Text="库位编号" Width="80" />
                                    <ext:Column runat="server" DataIndex="USED_FLAG"  Text="使用标志" Width="80" >
                                    <Renderer Fn="pctChange" />
                                    </ext:Column>
                                    <ext:Column runat="server" DataIndex="BIN_STATUS"       Text="库存状态" Width="80" />
                                    <ext:Column runat="server" DataIndex="BIN_BIZ_STATUS"   Text="业务状态" Width="80" />
                                    <ext:Column runat="server" DataIndex="ERR_NAME"         Text="异常描述" Width="150" />
                                    <ext:CommandColumn runat="server" Width="180" Text="操作" Align="Center">
                                        <Commands>
                                            <ext:GridCommand Icon="ApplicationAdd" CommandName="add" Text="异常出库">
                                                <ToolTip Text="异常出库" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="clear" Text="清空库位">
                                                <ToolTip Text="清空库位" />
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
                                <ext:GridView runat="server" StripeRows="true" TrackOver="true" />
                            </View>
                        </ext:GridPanel>

                        <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加出库明细信息"
                            Width="450" Height="300" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                            BodyPadding="5" Layout="Form">

                            <Items>
                                <ext:FormPanel runat="server" BodyPadding="5" Border="false">
                                    <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                            <Items>
                                                <ext:NumberField ID="txtOrderLineId" runat="server" FieldLabel="单行行号" LabelAlign="right"
                                                    Flex="1" IndicatorText="*" IndicatorCls="red-text" Text="0" />
                                                <ext:TextField ID="txtOrderNo" runat="server" FieldLabel="出库单号" LabelAlign="right"
                                                    Flex="1" Enabled="true" MaxLength="20" ReadOnly="true">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                            <Items>
                                                <ext:FieldContainer runat="server" FieldLabel="出库站台" Layout="HBox" LabelAlign="Right" Flex="1">
                                                    <Items>
                                                        <ext:TriggerField ID="txtAddEndLocName" runat="server" Flex="1" Editable="false">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Search" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <TriggerClick Fn="TriggerFieldSearchErrOutLocDatas" />
                                                            </Listeners>
                                                        </ext:TriggerField>
                                                        <ext:TextField ID="txtAddEndLocNo" runat="server" Width="120" ReadOnly="true"
                                                            IndicatorText="*" IndicatorCls="red-text" />
                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:Container>

                                        <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtAddBinNo" runat="server" FieldLabel="取货库位" ReadOnly="true" LabelAlign="right" Flex="1" />
                                            </Items>
                                        </ext:Container>

                                    </Items>
                                </ext:FormPanel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept">
                                    <ToolTips>
                                        <ext:ToolTip runat="server" Html="点击创建异常库位出库单据" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="saveAddWindows();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                                    <ToolTips>
                                        <ext:ToolTip runat="server" Html="点击关闭窗口" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="App.winAdd.close();" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
                        </ext:Window>

                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
