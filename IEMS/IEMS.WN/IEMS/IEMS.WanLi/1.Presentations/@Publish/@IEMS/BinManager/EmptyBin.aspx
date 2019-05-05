<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_BinManager_EmptyBin, IEMS.WanLi.WebSite" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库位维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <!--gridMain数据展示-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var gridMainFresh = function () {
            var store = App.gridMain.getStore();
            store.currentPage = 1;
            store.pageSize = 100;
            App.gridMainPagingToolbar.doRefresh();
            return false;
        }
    </script>
    <script type="text/javascript">
        var commandcolumn_click = function (command, record) {
            if (command.toLowerCase() == "clear".toLowerCase()) {
                clearEmptyBin(record);
            }
        }
    </script>
    <script type="text/javascript">
        var clearEmptyBin = function (record) {
            if (!record) {
                Ext.Msg.alert("提示", "请选择需要清理库位的空出库！");
                return;
            }
            var msg = "确定将空出库工位【" + record.data.BIN_NO + "】" + "设置为空库位？";
            Ext.Msg.confirm("提示", msg, function (btn) {
                if (btn == "yes") {
                    var binNo = record.data.BIN_NO;
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
                <ext:GridPanel ID="gridMain" runat="server" Region="Center">
                    <Store>
                        <ext:Store runat="server" PageSize="15">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="BIN_NO" />
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
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" runat="server" />
                            <ext:Column runat="server" DataIndex="BIN_NO" Text="库位" Width="80" />
                            <ext:Column runat="server" DataIndex="USED_FLAG" Text="使用标志" Width="80" >
                                <Renderer Fn="pctChange" />
                            </ext:Column>
                            <ext:CommandColumn runat="server" Width="100" Text="操作" Align="Center">
                                <Commands>
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
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
