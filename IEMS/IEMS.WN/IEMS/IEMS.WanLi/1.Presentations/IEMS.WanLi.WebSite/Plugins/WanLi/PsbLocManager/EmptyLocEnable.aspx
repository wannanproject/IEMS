<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmptyLocEnable.aspx.cs" Inherits="Plugins_WanLi_PsbLocManager_EmptyLocEnable" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>空笼工位状态维护</title>
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
            if (command.toLowerCase() == "AutoEnable".toLowerCase()) {
                changeLocAutoEnable(record);
            }
        }
    </script>
    <!--设置空笼工位状态-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var changeLocAutoEnable = function (record) {
            if (!record) {
                Ext.Msg.alert("提示", "请选择需要修改的空笼工位");
                return;
            }
            var msg = "确定将工位【" + record.data.LOC_NO + "】" + "设置为【"
            if (record.data.AUTO_PALLET_REQUEST == 1) {
                msg = msg + "不允许自动出入库";
            }
            else {
                msg = msg + "允许自动出入库";
            }
            msg = msg + "】？";
            Ext.Msg.confirm("提示", msg, function (btn) {
                if (btn == "yes") {
                    var locNo = record.data.LOC_NO;
                    var enable = record.data.AUTO_PALLET_REQUEST;
                    App.direct.changeLocAutoEnable(locNo, enable, {
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
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("AUTO_PALLET_REQUEST") == "0") {
                return "x-grid-row-deleted";
            }
        }
        var template = '<span style="background:{0};width:70px;display:block;text-align:center;">{1}</span>';
        var pctChange = function (value) {
            return Ext.String.format(template, (value == "允许") ? "green" : "red", value);
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
                        <ext:Store runat="server" PageSize="21">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="LOC_NO" />
                                        <ext:ModelField Name="LOC_NAME" />
                                        <ext:ModelField Name="AUTO_PALLET_REQUEST" />
                                        <ext:ModelField Name="AUTO_PALLET_REQUEST1" />
                                        <ext:ModelField Name="PALLET_TYPE" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" runat="server" />
                            <ext:Column runat="server" DataIndex="LOC_NO" Text="站台号码" Width="80" />
                            <ext:Column runat="server" DataIndex="LOC_NAME" Text="站台名称" Width="150"/>
                            <ext:Column runat="server" DataIndex="AUTO_PALLET_REQUEST" Text="允许自动出入库标志" Width="120" />
                            <ext:Column runat="server" DataIndex="AUTO_PALLET_REQUEST1" Text="允许自动出入库标志" Width="120" >
                            <Renderer Fn="pctChange" />
                            </ext:Column>
                            <ext:Column runat="server" DataIndex="PALLET_TYPE" Text="工装类型" Width="100" />
                            <ext:CommandColumn runat="server" Width="220" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="LockKey" CommandName="AutoEnable" Text="改变自动出入库标志">
                                        <ToolTip Text="改变自动出入库标志" />
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
