<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_TaskManager_TaskAgvDelete, IEMS.WanLi.WebSite" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>AGV残留任务管理</title>
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
            if (command.toLowerCase() == "deleteconfim") {
                deleteAgvTask(record);
            }
        }
     </script>
    <!--设置堆垛机状态-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var deleteAgvTask = function (record) {
            if (!record) {
                Ext.Msg.alert("提示", "请选择需要删除的任务");
                return;
            }
            var msg = "确定将任务【" + record.data.TASK_NO + "】" + "删除吗？"
           Ext.Msg.confirm("提示", msg, function (btn) {
                if (btn == "yes") {
                    var taskNo = record.data.TASK_NO;
                    App.direct.DeleteAgvTask(taskNo, {
                        success: function (result) {
                            if (result == "") {
                                gridMainFresh();
                                Ext.Msg.alert('成功', "删除成功！");
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
                                <ext:Button runat="server" Icon="Find" Text="查询任务" ID="Button2">
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
                                        <ext:ModelField Name="TASK_NO" />
                                        <ext:ModelField Name="SLOC_NO" />
                                        <ext:ModelField Name="LOC_NAME" />
                                        <ext:ModelField Name="ELOC_NO" />
                                        <ext:ModelField Name="PALLET_NO" />
                                        <ext:ModelField Name="CREATION_DATE" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" runat="server" />
                            <ext:Column runat="server" DataIndex="TASK_NO"           Text="任务号"    Width="100" />
                            <ext:Column runat="server" DataIndex="SLOC_NO"       Text="起始站台"     Width="100" />
                            <ext:Column runat="server" DataIndex="LOC_NAME"       Text="站台名称"     Width="100" />
                            <ext:Column runat="server" DataIndex="ELOC_NO"         Text="目的站台"    Width="100" />
                            <ext:Column runat="server" DataIndex="PALLET_NO"       Text="工装编号"    Width="100"/>
                            <ext:Column runat="server" DataIndex="CREATION_DATE"       Text="创建时间"    Width="100"/>
                            <ext:CommandColumn runat="server" Width="100" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="LockKey" CommandName="deleteconfim" Text="删除">
                                        <ToolTip Text="删除" />
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
