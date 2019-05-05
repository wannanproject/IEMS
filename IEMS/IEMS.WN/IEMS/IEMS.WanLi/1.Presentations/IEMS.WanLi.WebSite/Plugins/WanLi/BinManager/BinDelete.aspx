<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BinDelete.aspx.cs" Inherits="Plugins_WanLi_BinManager_BinDelete" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>库存删除</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>

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
        var cmdcol_click_handle = function (command, record) {
            if (command.toLowerCase() == "forcedeletebin") {
                deleteBin(record);
            }
        }
     </script>
    <!--删除库存-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var deleteBin = function (record) {
            if (!record) {
                Ext.Msg.alert("提示", "请选择需要删除的库位库存");
                return;
            }
           var msg = "确定将库位【" + record.data.BIN_NO + "】" + "的库存资料删除吗？"
           Ext.Msg.confirm("提示", msg, function (btn) {
                if (btn == "yes") {
                    var binNo = record.data.BIN_NO;
                    App.direct.DeleteBin(binNo, {
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
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel runat="server" Layout="ColumnLayout" AutoHeight="true" Region="North">
                            <Items>
                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".3" Padding="1">
                                    <Items>
                                        <ext:TextField ID="txtBinNo" runat="server" FieldLabel="库位号" ReadOnly="false"
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
                                   <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".3" Padding="1">
                                    <Items>
                                        <ext:TextField ID="txtMaterNo" runat="server" FieldLabel="物料号" ReadOnly="false"
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
                                <ext:PageProxy DirectFn="App.direct.GetBinData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model1" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="BIN_NO" />
                                        <ext:ModelField Name="PALLET_NO" />
                                        <ext:ModelField Name="MATERIAL_NO" />
                                        <ext:ModelField Name="MATER_SPEC" />
                                        <ext:ModelField Name="QTY" />
                                        <ext:ModelField Name="PRODUCT_DATE" />
                                       
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="30" runat="server" />
                            <ext:Column runat="server"      DataIndex="BIN_NO"         Text="库位号"           Width="100" />
                            <ext:Column runat="server"      DataIndex="PALLET_NO"         Text="工装号"         Width="80" />
                            <ext:Column runat="server"      DataIndex="MATERIAL_NO"         Text="物料号"         Width="100" />
                            <ext:Column runat="server"      DataIndex="MATER_SPEC"         Text="物料名称"         Width="200" />
                            <ext:Column runat="server"      DataIndex="QTY"     Text="数量"     Width="100" />
                            <ext:Column runat="server"      DataIndex="PRODUCT_DATE"     Text="生产时间"     Width="100" />
                            <ext:CommandColumn runat="server" Width="350" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="Delete" CommandName="ForceDeleteBin" Text="库存删除">
                                        <ToolTip Text="库存删除" />
                                    </ext:GridCommand>
                                    <%--<ext:CommandSeparator />--%>
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
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>

