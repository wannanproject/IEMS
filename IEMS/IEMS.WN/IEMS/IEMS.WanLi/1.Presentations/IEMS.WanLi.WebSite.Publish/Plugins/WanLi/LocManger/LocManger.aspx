<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_LocManger_LocManger, IEMS.WanLi.WebSite" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>站台状态管理</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>

    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSearchSrmData.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSearchMinColData.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSearchMaxColData.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
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
            if (command.toLowerCase() == "inputenable") {
                changeLocStatus(record);
            }
        }
     </script>
    <!--设置堆垛机状态-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var changeLocStatus = function (record) {
            if (!record) {
                Ext.Msg.alert("提示", "请选择需要修改的站台");
                return;
            }
            var msg = "确定将站台【" + record.data.LOC_NO + "】" + "设置为【"
            if (record.data.LOC_ENABLE == 1) {
                msg = msg + "禁用";
            }
            else {
                msg = msg + "可用";
            }
            msg = msg + "】？";
            Ext.Msg.confirm("提示", msg, function (btn) {
                if (btn == "yes") {
                    var locNo = record.data.LOC_NO;
                    var enable = record.data.LOC_ENABLE;
                    App.direct.ChangeLocEnable(locNo, enable, {
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
            if ((record.get("LOC_ENABLE") == "0")) {
                return "x-grid-row-deleted";
            }
        }
        var template = '<span style="background:{0};width:70px;display:block;text-align:center;">{1}</span>';
        var pctChange = function (value) {
            return Ext.String.format(template, (value == 1) ? "green" : "red", value);
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
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="Button2">
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


                <ext:GridPanel ID="gridMain" runat="server" Region="Center">
                    <Store>
                        <ext:Store runat="server" PageSize="15">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="LOC_NO" />
                                        <ext:ModelField Name="LOC_PLC_NO" />
                                        <ext:ModelField Name="LOC_NAME" />
                                        <ext:ModelField Name="LOC_ENABLE" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" runat="server" />
                            <ext:Column runat="server" DataIndex="LOC_NO"           Text="站台编号"    Width="100" />
                            <ext:Column runat="server" DataIndex="LOC_PLC_NO"       Text="PLC编号"     Width="100" />
                            <ext:Column runat="server" DataIndex="LOC_NAME"         Text="站台名称"    Width="100" />
                            <ext:Column runat="server" DataIndex="LOC_ENABLE"       Text="站台状态"    Width="80">
                                <Renderer Fn="pctChange" />
                            </ext:Column>
                            <ext:CommandColumn runat="server" Width="100" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="LockKey" CommandName="inputenable" Text="改变状态">
                                        <ToolTip Text="禁用/启用" />
                                    </ext:GridCommand>
                                    <%--<ext:CommandSeparator />--%>
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
