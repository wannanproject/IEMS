<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskAgvManager.aspx.cs" Inherits="Plugins_WanLi_TaskManager_TaskAgvManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>AGV任务管理</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>

    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TaskAgvManager.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    
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
                                <ext:Button runat="server" Icon="Find" Text="查询任务信息" ID="btnSaveBill">
                                    <Listeners>
                                        <Click Handler="gridMainFresh();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="TableSave" Text="添加任务信息">
                                    <Listeners>
                                        <Click Handler="showAddWindow();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSpacer />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel runat="server" Layout="Form" AutoHeight="true" BodyPadding="5" Border="false">
                                    <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:Container runat="server" Layout="HBoxLayout">
                                            <Items>
                                                <ext:FieldContainer runat="server" FieldLabel="任务号" Layout="HBox" LabelAlign="Right">
                                                    <Items>
                                                        <ext:TextField ID="txtSearcheTaskNo" runat="server" Width="120"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer runat="server" FieldLabel="工装号" Layout="HBox" LabelAlign="Right">
                                                    <Items>
                                                        <ext:TextField ID="txtSearchePalletNo" runat="server" Width="120"/>
                                                    </Items>
                                                </ext:FieldContainer>
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
                                        <ext:ModelField Name="TASK_NO" />
                                        <ext:ModelField Name="SLOC_NO" />
                                        <ext:ModelField Name="SLOC_PLC_CODE" />
                                        <ext:ModelField Name="SLOC_NAME" />
                                        <ext:ModelField Name="ELOC_NO" />
                                        <ext:ModelField Name="ELOC_PLC_CODE" />
                                        <ext:ModelField Name="ELOC_NAME" />
                                        <ext:ModelField Name="PALLET_NO" />
                                        <ext:ModelField Name="CMD_STEP" />
                                        <ext:ModelField Name="CREATION_DATE" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="45" runat="server" />
                            <ext:Column runat="server" DataIndex="TASK_NO" Text="任务编号" Width="60" />
                            <ext:Column runat="server" DataIndex="SLOC_NO" Text="起始站台" Width="80" />
                            <ext:Column runat="server" DataIndex="SLOC_PLC_CODE" Text="起始站台" Width="80" />
                            <ext:Column runat="server" DataIndex="SLOC_NAME" Text="起始站台" Flex="1" />
                            <ext:Column runat="server" DataIndex="ELOC_NO" Text="目标站台" Width="80" />
                            <ext:Column runat="server" DataIndex="ELOC_PLC_CODE" Text="目标站台" Width="80" />
                            <ext:Column runat="server" DataIndex="ELOC_NAME" Text="目标站台" Flex="1" />
                            <ext:Column runat="server" DataIndex="PALLET_NO" Text="工装编号" Width="140" />
                            <ext:Column runat="server" DataIndex="CMD_STEP" Text="指令步骤" Width="60" />
                            <ext:Column runat="server" DataIndex="CREATION_DATE" Text="创建时间" Width="140" />
                            <ext:CommandColumn runat="server" Width="60" Text="操作" Align="Center">
                                <Commands>
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
                </ext:GridPanel>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加AGV任务信息"
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
                                                        <TriggerClick Fn="TriggerFieldSearchAgvSLocData" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddSLocNo" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="目标地址" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtAddELocName" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchAgvELocData" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddELocNo" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="工装编号" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtAddPalletNo1" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchPallet1Datas" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddPalletName1" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                              <%--  <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="工装编号2" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtAddPalletNo2" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchPallet2Datas" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddPalletName2" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>--%>
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
