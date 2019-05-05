<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_BillManager_Output_ModifyBillOutput, IEMS.WanLi.WebSite" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>出库单修改</title>
    <script type="text/javascript">
     var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";

     var SelecttxtAddLineEndEquipType = function () {
             var typeValue = App.txtAddLineEndEquipType.value;
             App.direct.OutPutEquip(typeValue, {
                 success: function (result) {
                     App.txtAddLineEndEquip.setValue("");
                     var store = App.txtAddLineEndEquip.store;
                     store.data.removeAll();
                     var mname = store.model.getName();
                     var vdatas = eval(result);
                     var arr = new Array();
                     for (var i = 0; i < vdatas.length; i++) {
                         var d = vdatas[i];
                         var r = {
                             field1: d.LocNo,
                             field2: d.LocName
                         }
                         arr.push(r);
                     }
                     store.loadData(arr, false);
                     if (arr.length > 0) {
                         App.txtAddLineEndEquip.setValue(arr[0].field1);
                     }
                 },

                 failure: function (errorMsg) {
                     //Ext.Msg.alert('操作', errorMsg);
                 }
             });
     }
     
     var SelecttxtModifyLineEndEquipType = function () {
         var typeValue = App.txtModifyLineEndEquipType.value;
         App.direct.OutPutEquip(typeValue, {
             success: function (result) {
                 App.txtModifyLineEndEquip.setValue("");
                 var store = App.txtModifyLineEndEquip.store;
                 store.data.removeAll();
                 var mname = store.model.getName();
                 var vdatas = eval(result);
                 var arr = new Array();
                 for (var i = 0; i < vdatas.length; i++) {
                     var d = vdatas[i];
                     var r = {
                         field1: d.LocNo,
                         field2: d.LocName
                     }

                     arr.push(r);
                 }
                 store.loadData(arr, false);
                 if (arr.length > 0) {
                     App.txtModifyLineEndEquip.setValue(arr[0].field1);
                 }
             },

             failure: function (errorMsg) {
                 //Ext.Msg.alert('操作', errorMsg);
             }
         });
     }
     </script>
     <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>AddModifyBillOutput.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
     <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSerchMaterial.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
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
                                <ext:Button runat="server" Icon="TableSave" Text="保存单据信息" ID="btnSaveBill">
                                    <Listeners>
                                        <Click Handler="postBillData();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Cancel" Text="取消保存">
                                    <Listeners>
                                        <Click Handler="closeThisWindow();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:TextField ID="txtStoreWhNo" runat="server" Visible="false"></ext:TextField>
                                <ext:ToolbarFill />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                      <Items>
                        <ext:Panel runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel runat="server" Layout="ColumnLayout" AutoHeight="true" >
                                    <Items>
                                       <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtListNo" runat="server" FieldLabel="出库单号" ReadOnly="true"
                                                    LabelAlign="Right"  />
                                            </Items>
                                        </ext:Container>
                                         <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                             <ext:ComboBox ID="txtBillStatus" runat="server" FieldLabel="单据状态" ReadOnly="true"
                                                    LabelAlign="Right" >
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                         <ext:Container runat="server" Layout="HBoxLayout" Hidden="true">
                                            <Items>
                                                <ext:Checkbox ID="Checkbox1" runat="server" FieldLabel="自动执行" LabelAlign="right" ReadOnly="true"
                                                    Flex="1" Checked="true" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                   </ext:Panel>
                <ext:GridPanel ID="gridMain" runat="server" Region="Center">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button runat="server" Icon="ApplicationAdd" Text="添加出库单明细信息" ID="btnAddBillLine">
                                    <Listeners>
                                        <Click Handler="showAddLineWindows();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store runat="server" PageSize="10">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ORDER_NO" />
                                        <ext:ModelField Name="LINE_STATUS" />
                                        <ext:ModelField Name="LIMIT_BIN_NO" />
                                        <ext:ModelField Name="ELOC_NO" />
                                        <ext:ModelField Name="PRODUCT_GRADE" />
                                        <ext:ModelField Name="LIMIT_PALLET_ID" />
                                        <ext:ModelField Name="LIMIT_PRODUCT_GUID" />
                                        <ext:ModelField Name="DELETE_FLAG" />
                                        <ext:ModelField Name="ORDER_LINE_GUID" />
                                        <ext:ModelField Name="LINE_ID" />
                                        <ext:ModelField Name="MATER_NAME" />
                                        <ext:ModelField Name="SORT_ID" />
                                        <ext:ModelField Name="MATERIAL_NO" />
                                        <ext:ModelField Name="REQUIRE_QTY" />
                                        <ext:ModelField Name="ACT_QTY" />
                                        <ext:ModelField Name="SHIP_QTY" />
                                        <ext:ModelField Name="LINE_PRIORITY" />
                                        <ext:ModelField Name="BIN_ERR_DESC" />
                                        <ext:ModelField Name="LINE_TASK_FLAG" />
                                        <ext:ModelField Name="LOCK_END_LOC" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="20" runat="server" />
                            <ext:Column runat="server" DataIndex="ORDER_NO" Text="单号" Width="120" />
                            <ext:Column runat="server" DataIndex="LINE_ID" Text="单行行号" Width="120" />
                            <ext:Column runat="server" DataIndex="LINE_STATUS" Text="单行行状态" Width="80" />
                            <ext:Column runat="server" DataIndex="ORDER_LINE_GUID" Text="行主键信息" Width="120" />
                            <ext:Column runat="server" DataIndex="LIMIT_BIN_NO" Text="发起站台" Width="80" />
                            <ext:Column runat="server" DataIndex="ELOC_NO" Text="目的站台" Width="80" />
                            <ext:Column runat="server" DataIndex="LIMIT_PALLET_ID" Text="RFID号" Width="100"/>
                            <ext:Column runat="server" DataIndex="MATER_NAME" Text="物料编码" Width="100"/>
                            <ext:Column runat="server" DataIndex="PRODUCT_GRADE" Text="品级" Width="60" />
                            <ext:Column runat="server" DataIndex="LIMIT_PRODUCT_GUID" Text="物料GUID" Width="100"  />
                            <ext:Column runat="server" DataIndex="DELETE_FLAG" Text="单据执行状态" Width="100"  />
                            <ext:Column runat="server" DataIndex="SORT_ID" Text="此单发货顺序" Width="100" Hidden="true"/>
                            <ext:Column runat="server" DataIndex="MATERIAL_NO" Text="物料信息" Width="100" Hidden="true"/>
                            <ext:Column runat="server" DataIndex="REQUIRE_QTY" Text="单据数量" Width="80" Hidden="true"/>
                            <ext:Column runat="server" DataIndex="ACT_QTY" Text="已生成任务数量" Width="120" Hidden="true"/>
                            <ext:Column runat="server" DataIndex="LINE_TASK_FLAG" Text="出库任务分解" Width="100"  Hidden="true"/>
                            <ext:Column runat="server" DataIndex="SHIP_QTY" Text="实发数量" Width="80" Hidden="true"/>
                            <ext:Column runat="server" DataIndex="LINE_PRIORITY" Text="优先级" Width="80"  Hidden="true"/>
                            <ext:Column runat="server" DataIndex="BIN_ERR_DESC" Text="库存异常信息" Width="100" Hidden="true"/>
                            <ext:Column runat="server" DataIndex="LOCK_END_LOC" Text="锁定目标" Width="60" Hidden="true"/>
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
                </ext:GridPanel>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加出库明细信息"
                    Width="600" Height="400" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                        <ext:TextField ID="txtAddLineListNo" runat="server" FieldLabel="出库单号" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20" ReadOnly="true">
                                        </ext:TextField>
                                        <ext:NumberField ID="txtAddLineLineId" runat="server" FieldLabel="单行行号" LabelAlign="right"
                                            Flex="1" IndicatorText="*" IndicatorCls="red-text" Text="0" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtAddLineBillStatus" runat="server" FieldLabel="单据状态" ReadOnly="true"
                                            Flex="1" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Value="0" Text="未执行" />
                                                <ext:ListItem Value="1" Text="出库中" />
                                                <ext:ListItem Value="2" Text="已完成" />
                                                <ext:ListItem Value="3" Text="已取消" />
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:NumberField ID="txtAddLineRequireQty" runat="server" FieldLabel="单据数量" LabelAlign="right"
                                            Flex="1" IndicatorText="*" IndicatorCls="red-text" Text="0" />
                                    </Items>
                                </ext:Container>
                                 <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="TxtAddLineBinNo" runat="server" FieldLabel="开始库位" LabelAlign="right"  LabelWidth="60" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtAddLineEndEquipType" runat="server" Editable="false" FieldLabel="目的站台类型"
                                                    LabelAlign="Right" Flex="1" Text="3000" >
                                             <Items>
                                                <ext:ListItem Value="7002" Text="PDOUT站台" />
                                                <ext:ListItem Value="5000" Text="异常口" />
                                                <ext:ListItem Value="4000" Text="叠盘机" />
                                                <ext:ListItem Value="3000" Text="线边库" />
                                                <ext:ListItem Value="2002" Text="产出工装工位" />
                                                <ext:ListItem Value="6002" Text="输送线出口" />
                                             </Items>
                                            <Listeners>
                                                 <Change Fn="SelecttxtAddLineEndEquipType" />
                                            </Listeners>
                                        </ext:ComboBox>
                                   </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtAddLineEndEquip" runat="server" Editable="false" FieldLabel="目的站台"
                                                    LabelAlign="Right" Flex="1"  />
                                   </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="物料信息" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtShowAddLineMaterial" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSerchMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddLineMaterial" runat="server" Width="120" ReadOnly="true"
                                                    IndicatorText="*" IndicatorCls="red-text" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                 <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtAddLineGuid" runat="server" FieldLabel="物料Guid" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20" >
                                        </ext:TextField>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtAddLineGrade" runat="server" Editable="false" FieldLabel="物料品级"
                                                    LabelAlign="Right" Flex="1"  />
                                        <ext:TextField ID="txtAddLinePalletRfid" runat="server" FieldLabel="RFID编号" LabelAlign="right"  LabelWidth="60" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5" Hidden="true">
                                    <Items>
                                        <ext:NumberField ID="txtAddLineShipQty" runat="server" FieldLabel="任务数量" LabelAlign="right" Text="0"
                                            ReadOnly="true" Flex="1" />
                                        <ext:NumberField ID="txtAddLineRcvQty" runat="server" FieldLabel="待发数量" LabelAlign="right" Text="0"
                                            ReadOnly="true" Flex="1" />
                                        <ext:NumberField ID="txtAddLineActQty" runat="server" FieldLabel="实发数量" LabelAlign="right" Text="0"
                                            ReadOnly="true" Flex="1" />
                                        <ext:NumberField ID="txtAddLineOutTaskFlag" runat="server" FieldLabel="行任务执行" LabelAlign="right" Text="1"
                                            Flex="1" />
                                        <ext:NumberField ID="txtAddLineLineGuid" runat="server" FieldLabel="行Guid" LabelAlign="right" 
                                            Flex="1" />
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
                                <Click Handler="saveAddLineWindows();" />
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
                 <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改出库明细信息"
                    Width="600" Height="400" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                        <ext:TextField ID="txtModifyLineListNo" runat="server" FieldLabel="出库单号" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20" ReadOnly="true">
                                        </ext:TextField>
                                        <ext:NumberField ID="txtModifyLineLineId" runat="server" FieldLabel="单行行号" LabelAlign="right"
                                            Flex="1" IndicatorText="*" IndicatorCls="red-text" Text="0" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtModifyLineBillStatus" runat="server" FieldLabel="单据状态" ReadOnly="true"
                                            Flex="1" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Value="0" Text="未执行" />
                                                <ext:ListItem Value="1" Text="出库中" />
                                                <ext:ListItem Value="2" Text="已完成" />
                                                <ext:ListItem Value="3" Text="已取消" />
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:NumberField ID="txtModifyLineRequireQty" runat="server" FieldLabel="单据数量" LabelAlign="right"
                                            Flex="1" IndicatorText="*" IndicatorCls="red-text" Text="0" />
                                    </Items>
                                </ext:Container>
                                 <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtModifyLineBinNo" runat="server" FieldLabel="开始库位" LabelAlign="right"  LabelWidth="60" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtModifyLineEndEquipType" runat="server" Editable="false" FieldLabel="目的站台类型"
                                                    LabelAlign="Right" Flex="1" Text="3000" >
                                             <Items>
                                                <ext:ListItem Value="7002" Text="PDOUT站台" />
                                                <ext:ListItem Value="5000" Text="异常口" />
                                                <ext:ListItem Value="4000" Text="叠盘机" />
                                                <ext:ListItem Value="3000" Text="线边库" />
                                                <ext:ListItem Value="2002" Text="产出工装工位" />
                                                <ext:ListItem Value="6002" Text="输送线出口" />
                                             </Items>
                                            <Listeners>
                                                 <Change Fn="SelecttxtModifyLineEndEquipType" />
                                            </Listeners>
                                        </ext:ComboBox>
                                   </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtModifyLineEndEquip" runat="server" Editable="false" FieldLabel="目的站台"
                                                    LabelAlign="Right" Flex="1"  />
                                   </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="物料信息" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtShowAddModifyLineMaterial" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSerchMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtModifyLineMaterial" runat="server" Width="120" ReadOnly="true"
                                                    IndicatorText="*" IndicatorCls="red-text" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                 <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtModifyLineGuid" runat="server" FieldLabel="物料Guid" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20" >
                                        </ext:TextField>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtModifyLineGrade" runat="server" Editable="false" FieldLabel="物料品级"
                                                    LabelAlign="Right" Flex="1"  />
                                        <ext:TextField ID="txtModifyLinePalletRfid" runat="server" FieldLabel="RFID编号" LabelAlign="right"  LabelWidth="60" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5" Hidden="true">
                                    <Items>
                                        <ext:NumberField ID="txtModifyLineShipQty" runat="server" FieldLabel="任务数量" LabelAlign="right" Text="0"
                                            ReadOnly="true" Flex="1" />
                                        <ext:NumberField ID="txtModifyLineRcvQty" runat="server" FieldLabel="待发数量" LabelAlign="right" Text="0"
                                            ReadOnly="true" Flex="1" />
                                        <ext:NumberField ID="txtModifyLineActQty" runat="server" FieldLabel="实发数量" LabelAlign="right" Text="0"
                                            ReadOnly="true" Flex="1" />
                                        <ext:NumberField ID="txtModifyLineOutTaskFlag" runat="server" FieldLabel="行任务执行" LabelAlign="right" Text="1"
                                            Flex="1" />
                                        <ext:NumberField ID="txtModifyLineLineGuid" runat="server" FieldLabel="行Guid" LabelAlign="right" 
                                            Flex="1" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="点击进行修改" />
                            </ToolTips>
                            <Listeners>
                                <Click Handler="saveModifyLineWindows();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="点击关闭窗口" />
                            </ToolTips>
                            <Listeners>
                                <Click Handler="App.winModify.close();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
