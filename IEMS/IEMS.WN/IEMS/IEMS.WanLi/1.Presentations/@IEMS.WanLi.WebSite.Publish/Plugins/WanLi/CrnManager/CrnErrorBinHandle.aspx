<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_CrnManager_CrnErrorBinHandle, IEMS.WanLi.WebSite" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>堆垛机查询与维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <!--gridMain数据展示-->
    <script type="text/javascript">
        //$(document).ready(function () {
        //    window.setInterval(gridMainFresh, 3000);
        //});
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
            changeCrnInputEnable(record);
            gridMainFresh();
        }
    </script>
    <!--先入品确认操作-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var changeCrnInputEnable = function (record) {
            if (!record) {
                Ext.Msg.alert("提示", "请选择需要确认的堆垛机");
                return;
            }
            var msg = "确定为异常吗？";
            Ext.Msg.confirm("提示", msg, function (btn) {
                if (btn == "yes") {
                    var crnForkNo = record.data.CRN_FORK_NO;
                    var taskNo = record.data.TASK_NO;
                    App.direct.ConfirmFirstInProduct(crnForkNo, taskNo, {
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
            return Ext.String.format(template, (value == "可用") ? "green" : "red", value);
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
                                        <ext:ModelField Name="CRN_FORK_NO" />
                                        <ext:ModelField Name="CRN_NO" />
                                        <ext:ModelField Name="TASK_NO" />
                                        <ext:ModelField Name="TASK_TYPE" />
                                        <ext:ModelField Name="FROM_BIN" />
                                        <ext:ModelField Name="TO_BIN" />
                                        <ext:ModelField Name="ACT_POS_X" />
                                        <ext:ModelField Name="ACT_POS_Y" />
                                        <ext:ModelField Name="FAULT_NAME" />
                                        <ext:ModelField Name="FIP_FAULT_NO" />
                                        <ext:ModelField Name="FIP_DATE" />
                                        <ext:ModelField Name="FIP_HANDLE_RESULT" />
                                        <ext:ModelField Name="FIP_FLAG" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" runat="server" />
                            <ext:Column runat="server" DataIndex="CRN_FORK_NO"          Text="叉编号" Width="100" />
                            <ext:Column runat="server" DataIndex="CRN_NO"               Text="堆垛机编号" Width="100" />
                            <ext:Column runat="server" DataIndex="TASK_TYPE"            Text="任务类型" Width="80" />
                            <ext:Column runat="server" DataIndex="TASK_NO"              Text="任务号" Width="80"/>
                            <ext:Column runat="server" DataIndex="FROM_BIN"             Text="起始位置" Width="80"/>
                            <ext:Column runat="server" DataIndex="TO_BIN"               Text="目的目的" Width="80" />
                            <ext:Column runat="server" DataIndex="FIP_FAULT_NO"         Text="异常编号" Width="80" />
                            <ext:Column runat="server" DataIndex="FAULT_NAME"         Text="异常描述" Width="120" />
                            <ext:Column runat="server" DataIndex="FIP_FLAG"             Text="处理标志" Width="80" />
                            <ext:Column runat="server" DataIndex="FIP_HANDLE_RESULT"    Text="处理结果" Width="100" />
                            <ext:Column runat="server" DataIndex="FIP_DATE"             Text="处理时间" Width="140" />
                            <ext:CommandColumn runat="server" Width="120"               Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="LockKey" CommandName="FipConfirm" Text="确认为异常">
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
