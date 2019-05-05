<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBillOutput.aspx.cs" Inherits="Plugins_WanLi_BillData_AddBillOutput" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加出库单</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>

    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>AddModifyBillOutput.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSearchLocData.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>TriggerFieldSearchMaterial.js?id=<%=DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
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
                                <ext:Button runat="server" Icon="Cancel" Text="取消保存" ID="Button1">
                                    <Listeners>
                                        <Click Handler="closeThisWindow();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
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
                                                <ext:TextField ID="txtOrderNo" runat="server" FieldLabel="出库单号" ReadOnly="true"
                                                    LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="txtBillStatus" runat="server" FieldLabel="单据状态" ReadOnly="true"
                                                    LabelAlign="Right" Text="0">
                                                    <Items>
                                                        <ext:ListItem Value="0" Text="未执行" />
                                                        <ext:ListItem Value="1" Text="出库中" />
                                                        <ext:ListItem Value="2" Text="已完成" />
                                                        <ext:ListItem Value="3" Text="已取消" />
                                                    </Items>
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
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ORDER_NO" />
                                        <ext:ModelField Name="ORDER_LINE_GUID" />
                                        <ext:ModelField Name="LINE_ID" />
                                        <ext:ModelField Name="REQUIRE_QTY" />
                                        <ext:ModelField Name="LINE_STATUS" />
                                        <ext:ModelField Name="MATER_NO" />
                                        <ext:ModelField Name="MATER_NAME" />
                                        <ext:ModelField Name="PRODUCT_GRADE" />
                                        <ext:ModelField Name="ELOC_NO" />
                                        <ext:ModelField Name="ELOC_NAME" />
                                        <ext:ModelField Name="DELETE_FLAG" />
                                        <ext:ModelField Name="ORDER_LINE_GUID" />
                                        <ext:ModelField Name="LIMIT_BATCH_NO" />
                                        <ext:ModelField Name="LIMIT_BIN_NO" />
                                        <ext:ModelField Name="LIMIT_PALLET_ID" />
                                        <ext:ModelField Name="LIMIT_PRODUCT_GUID" />
                                        <ext:ModelField Name="DELETE_FLAG" />
                                        <ext:ModelField Name="LIMIT_DATE_START" Type="Date" />
                                        <ext:ModelField Name="LIMIT_DATE_END" Type="Date" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="20" runat="server" />
                            <ext:Column runat="server" DataIndex="LINE_ID" Text="单据行号" Width="60" />
                            <ext:Column runat="server" DataIndex="ELOC_NO" Text="出库站台" Width="80" />
                            <ext:Column runat="server" DataIndex="ELOC_NAME" Text="出库站台" Width="100" />
                            <ext:Column runat="server" DataIndex="MATER_NO" Text="物料编码" Width="120" />
                            <ext:Column runat="server" DataIndex="MATER_NAME" Text="物料名称" Width="140" />
                            <ext:Column runat="server" DataIndex="PRODUCT_GRADE" Text="物料品级" Width="60" />
                            <ext:Column runat="server" DataIndex="REQUIRE_QTY" Text="单据数量" Width="80" />
                            <ext:Column runat="server" DataIndex="LIMIT_BATCH_NO"   Text="限定批次" Width="100" />
                            <ext:Column runat="server" DataIndex="LIMIT_BIN_NO" Text="限定库位" Width="80" />
                            <ext:Column runat="server" DataIndex="LIMIT_PALLET_ID" Text="限定工装" Width="100" />
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
                                        <ext:NumberField ID="txtAddLineLineId" runat="server" FieldLabel="单行行号" LabelAlign="right"
                                            Flex="1" IndicatorText="*" IndicatorCls="red-text" Text="0" />
                                        <ext:TextField ID="txtAddLineOrderNo" runat="server" FieldLabel="出库单号" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20" ReadOnly="true">
                                        </ext:TextField>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="出库站台" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtAddLineEndLocName" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchOutLocData" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddLineEndLocNo" runat="server" Width="120" ReadOnly="true"
                                                    IndicatorText="*" IndicatorCls="red-text" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="物料信息" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtAddLineMaterialName" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtAddLineMaterialNo" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="单据数量" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:NumberField ID="txtAddLineRequireQty" runat="server" LabelAlign="right"
                                                    Flex="1" />
                                                <ext:TextField ID="txtAddLineMaxQty" runat="server" Width="40" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:ComboBox ID="txtAddLineGrade" runat="server" Editable="false" FieldLabel="物料品级"
                                            LabelAlign="Right" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtAddLineBinNo" runat="server" FieldLabel="取货库位" LabelAlign="right" Flex="1" />
                                        <ext:TextField ID="txtAddLinePalletRfid" runat="server" FieldLabel="RFID编号" LabelAlign="right" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtAddLineStartDate" runat="server" FieldLabel="起始日期" LabelAlign="right" Flex="1" />
                                        <ext:DateField ID="txtAddLineEndDate" runat="server" FieldLabel="结束日期" LabelAlign="right" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtAddLineGuid" runat="server" FieldLabel="产品GUID" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20">
                                        </ext:TextField>
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
                                        <ext:TextField ID="txtModifyLineOrderNo" runat="server" FieldLabel="出库单号" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20" ReadOnly="true">
                                        </ext:TextField>
                                        <ext:TextField ID="txtModifyLineLineId" runat="server" FieldLabel="单行行号" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20" ReadOnly="true">
                                        </ext:TextField>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="出库站台" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtModifyLineEndLocName" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchOutLocData" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtModifyLineEndLocNo" runat="server" Width="120" ReadOnly="true"
                                                    IndicatorText="*" IndicatorCls="red-text" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="物料信息" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:TriggerField ID="txtModifyLineMaterialName" runat="server" Flex="1" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="TriggerFieldSearchMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="txtModifyLineMaterialNo" runat="server" Width="120" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:FieldContainer runat="server" FieldLabel="单据数量" Layout="HBox" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:NumberField ID="txtModifyLineRequireQty" runat="server" LabelAlign="right"
                                                    Flex="1" />
                                                <ext:TextField ID="txtModifyLineMaxQty" runat="server" Width="40" ReadOnly="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:ComboBox ID="txtModifyLineGrade" runat="server" Editable="false" FieldLabel="物料品级"
                                            LabelAlign="Right" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtModifyLineBinNo" runat="server" FieldLabel="取货库位" LabelAlign="right" Flex="1" />
                                        <ext:TextField ID="txtModifyLinePalletRfid" runat="server" FieldLabel="RFID编号" LabelAlign="right" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtModifyLineStartDate" runat="server" FieldLabel="起始日期" LabelAlign="right" Flex="1" />
                                        <ext:DateField ID="txtModifyLineEndDate" runat="server" FieldLabel="结束日期" LabelAlign="right" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtModifyLineGuid" runat="server" FieldLabel="产品GUID" LabelAlign="right"
                                            Flex="1" Enabled="true" MaxLength="20">
                                        </ext:TextField>
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
