<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_TaskManager_TaskManager, IEMS.WanLi.WebSite" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>堆垛机任务维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>

    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSearchInLocDatas.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSearchPallet1Datas.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSearchPallet2Datas.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <!--gridMain数据展示-->
    <script type="text/javascript">
        var gridMainFresh = function () {
            var store = App.gridMain.getStore();
            store.currentPage = 1;
            store.pageSize = 100;
            App.gridMainPagingToolbar.doRefresh();
            return false;
        }
    </script>

    <script type="text/javascript">
        //按钮点击处理
        var cmdcol_click_handle = function (command, record) {

            if (command.toLowerCase() == "forceresendcmd") {
                var objId = record.data.OBJID;
                if (objId == 0) {
                    Ext.Msg.alert("错误", '该任务未生成指令');
                    return;
                }
                Ext.Msg.confirm("提示", '确定要重发指令【' + objId + '】吗？', function (btn) {
                    if (btn == "yes") {
                        App.direct.ForceResendCmd(objId, {
                            success: function (result) {
                                if (result == "") {
                                    Ext.Msg.alert('成功', "重发指令成功！");
                                    //站台状态刷新
                                    App.gridMainPagingToolbar.doRefresh();
                                } else {
                                    Ext.Msg.alert("指令重发失败", result);
                                }
                            },
                            failure: function (errorMsg) {
                                Ext.Msg.alert("错误", errorMsg);
                            }
                        });
                    }
                });
            }
            if (command.toLowerCase() == "forcefinishcmd") {
                var objId = record.data.OBJID;
                if (objId == 0) {
                    Ext.Msg.alert("错误", '该任务未生成指令');
                    return;
                }
                Ext.Msg.confirm("提示", '确定要强制完成指令【' + objId + '】吗？', function (btn) {
                    if (btn == "yes") {
                        App.direct.ForceFinishCmd(objId, {
                            success: function (result) {
                                if (result == "") {
                                    Ext.Msg.alert('成功', "完成指令成功！");
                                    //站台状态刷新
                                    App.gridMainPagingToolbar.doRefresh();
                                } else {
                                    Ext.Msg.alert("完成指令失败", result);
                                }
                            },
                            failure: function (errorMsg) {
                                Ext.Msg.alert("错误", errorMsg);
                            }
                        });
                    }
                });
            }
            if (command.toLowerCase() == "forcedeletecmd") {
                var objId = record.data.OBJID;
                if (objId == 0) {
                    Ext.Msg.alert("错误", '该任务未生成指令');
                    return;
                }
                Ext.Msg.confirm("提示", '确定要强制删除指令【' + objId + '】吗？', function (btn) {
                    if (btn == "yes") {
                        App.direct.ForceDeleteCmd(objId, {
                            success: function (result) {
                                if (result == "") {
                                    Ext.Msg.alert('成功', "删除指令成功！");
                                    //站台状态刷新
                                    App.gridMainPagingToolbar.doRefresh();
                                } else {
                                    Ext.Msg.alert("删除指令失败", result);
                                }
                            },
                            failure: function (errorMsg) {
                                Ext.Msg.alert("错误", errorMsg);
                            }
                        });
                    }
                });
            }
            if (command.toLowerCase() == "forcedeletetask") {
                var objId = record.data.OBJID;
                if (objId != 0) {
                    Ext.Msg.alert("错误", '该任务已生成指令,禁止删除!');
                    return;
                }
                var taskNo = record.data.TASK_NO;
                Ext.Msg.confirm("提示", '确定要强制删除任务【' + taskNo + '】吗？', function (btn) {
                    if (btn == "yes") {
                        App.direct.ForceDeleteTask(taskNo, {
                            success: function (result) {
                                if (result == "") {
                                    Ext.Msg.alert('成功', "删除任务成功！");
                                    //站台状态刷新
                                    App.gridMainPagingToolbar.doRefresh();
                                } else {
                                    Ext.Msg.alert("删除任务失败", result);
                                }
                            },
                            failure: function (errorMsg) {
                                Ext.Msg.alert("错误", errorMsg);
                            }
                        });
                    }
                });
            }
            return false;
        };
    </script>
     <script type="text/javascript">
         var saveAddWindows = function () {
             var palletNo1 = App.txtAddPallet1.getValue();
             var palletNo2 = App.txtAddPallet2.getValue();
             var sLocPlcNo = App.txtAddSLocNo.getValue();
             if (sLocPlcNo == null || sLocPlcNo == "") {
                 Ext.Msg.alert("错误", '请选择起始站点!');
                 return;
             }
             if ((palletNo1 == null || palletNo1 == "") && (palletNo2 == null || palletNo2 == "")) {
                 Ext.Msg.alert("错误", '请选择入库工装!');
                 return;
             }
            App.direct.RequestTaskCmd(palletNo1, palletNo2, sLocPlcNo, {
                success: function (result) {
                    if (result == "") {
                        Ext.Msg.alert('成功', "生成任务成功！");
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
        var showAddWindows = function () {
            App.txtAddPallet1.setValue(""); 
            App.txtAddPallet2.setValue(""); 
            App.txtAddSLocNo.setValue(""); 
            App.winAdd.show();
            return false;
        }
     </script>

</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
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
                                
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="Button3">
                                    <Listeners>
                                        <Click Handler="showAddWindows();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSpacer />
                                <ext:ToolbarFill />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel runat="server" Layout="ColumnLayout" AutoHeight="true" Region="North">
                            <Items>
                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".3" Padding="1">
                                    <Items>
                                        <ext:TextField ID="txtTaskNo" runat="server" FieldLabel="任务号" ReadOnly="false"
                                            LabelAlign="Right">
                                        </ext:TextField>
                                    </Items>
                                </ext:Container>
                                 <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".3" Padding="1">
                                    <Items>
                                        <ext:TextField ID="txtPalletNo" runat="server" FieldLabel="工装号" ReadOnly="false"
                                            LabelAlign="Right">
                                        </ext:TextField>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                        </Items>
                </ext:Panel>


                <ext:GridPanel ID="gridMain" runat="server" Region="Center">
                    <Store>
                        <ext:Store runat="server" PageSize="100" Height="100%">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GetTaskCmd" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model1" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="TASK_GUID" />
                                        <ext:ModelField Name="TASK_NO" />
                                        <ext:ModelField Name="IO_TYPE" />
                                        <ext:ModelField Name="TRANSFER_NO" />
                                        <ext:ModelField Name="SLOC_NO" />
                                        <ext:ModelField Name="ELOC_NO" />
                                        <ext:ModelField Name="PALLET_NO" />
                                        <ext:ModelField Name="CREATION_DATE" />
                                        <ext:ModelField Name="TASK_EXEC_END_DT" />
                                        <ext:ModelField Name="OBJID" />
                                        <ext:ModelField Name="SLOC_PLC_NO" />
                                        <ext:ModelField Name="ELOC_PLC_NO" />
                                        <ext:ModelField Name="EQUIP_NO" />
                                        <ext:ModelField Name="CMD_STEP" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="30" runat="server" />
                            <ext:Column runat="server"      DataIndex="TASK_NO"         Text="任务号"           Width="100" />
                            <ext:Column runat="server"      DataIndex="IO_TYPE"         Text="任务类型"         Width="80" />
                            <ext:Column runat="server"      DataIndex="SLOC_NO"         Text="起始位置"         Width="100" />
                            <ext:Column runat="server"      DataIndex="ELOC_NO"         Text="终点位置"         Width="100" />
                            <ext:Column runat="server"      DataIndex="SLOC_PLC_NO"     Text="当前指令起点"     Width="100" />
                            <ext:Column runat="server"      DataIndex="ELOC_PLC_NO"     Text="当前指令终点"     Width="100" />
                            <ext:Column runat="server"      DataIndex="TRANSFER_NO"     Text="输送设备"         Width="100" />
                            <ext:Column runat="server"      DataIndex="EQUIP_NO"        Text="指令锁定设备"     Width="100" />
                            <ext:Column runat="server"      DataIndex="CMD_STEP"        Text="指令步骤"     Width="100" />
                            <ext:DateColumn runat="server" DataIndex="CREATION_DATE"    Text="任务创建时间" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                            <ext:DateColumn runat="server" DataIndex="TASK_EXEC_END_DT" Text="任务执行时间" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                            <ext:CommandColumn runat="server" Width="350" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="Delete" CommandName="ForceDeleteTask" Text="任务删除">
                                        <ToolTip Text="任务删除" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="TableEdit" CommandName="ForceResendCmd" Text="指令重发">
                                        <ToolTip Text="指令重发" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Accept" CommandName="ForceFinishCmd" Text="指令完成">
                                        <ToolTip Text="指令完成" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="ForceDeleteCmd" Text="指令删除">
                                        <ToolTip Text="指令删除" />
                                    </ext:GridCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="return cmdcol_click_handle(command, record);" />
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
                        <ext:GridView runat="server" StripeRows="true" TrackOver="true" EnableTextSelection="true" />
                    </View>
                </ext:GridPanel>
                    

                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加任务信息"
                    Width="600" Height="350" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                        <ext:FieldContainer runat="server" FieldLabel="起始地址" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtAddSLocName" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchInLocDatas" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddSLocNo" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="工装编号1" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtAddPalletName1" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchPallet1Datas" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddPallet1" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="工装编号2" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtAddPalletName2" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchPallet2Datas" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddPallet2" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept">
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="点击进行添加" />
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
        </ext:Viewport>
    </form>
</body>
</html>

