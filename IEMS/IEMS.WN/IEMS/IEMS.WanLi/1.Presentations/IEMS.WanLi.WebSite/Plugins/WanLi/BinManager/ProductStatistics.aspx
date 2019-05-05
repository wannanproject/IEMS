<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductStatistics.aspx.cs" Inherits="Plugins_WanLi_BinManager_ProductStatistics" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>库存占有率统计</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //历史查询根据DeleteFlag的值进行样式绑定
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("ROW_NO") == "总计") {
                return "x-grid-row-collapsedGreen";
            }
        }
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="FitLayout">
            <Items>
                <ext:TextField ID="txtStoreWhNo" runat="server" Visible="false"></ext:TextField>
                <ext:GridPanel ID="gridMain" runat="server" Floatable="false" ColumnLines="true" RowLines="true">
                    <Store>
                        <ext:Store runat="server" PageSize="100">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ROW_NO" />
                                        <ext:ModelField Name="TOTAL_BIN" />
                                        <ext:ModelField Name="FG_BIN" />
                                        <ext:ModelField Name="RES_BIN" />
                                        <ext:ModelField Name="EMPTY_BIN" />
                                        <ext:ModelField Name="STO_RATE" />
                                        <ext:ModelField Name="USE_RATE" />
                                        <ext:ModelField Name="ERR_BIN" />
                                        <ext:ModelField Name="DISA_BIN" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="45" runat="server" />
                            <ext:Column runat="server" DataIndex="ROW_NO"       Text="库位行"      Flex="1" />
                            <ext:Column runat="server" DataIndex="TOTAL_BIN"    Text="库位总数"    Flex="1" />
                            <ext:Column runat="server" DataIndex="FG_BIN"       Text="物料库存数"  Flex="1" />
                            <ext:Column runat="server" DataIndex="RES_BIN"      Text="预约库存数"  Flex="1" />
                            <ext:Column runat="server" DataIndex="EMPTY_BIN"    Text="空库位数"    Flex="1" />
                            <ext:Column runat="server" DataIndex="STO_RATE"     Text="库存使用率"  Flex="1" />
                            <ext:Column runat="server" DataIndex="USE_RATE"     Text="库位占有率"  Flex="1" />
                            <ext:Column runat="server" DataIndex="ERR_BIN"      Text="库位异常数"  Flex="1" />
                            <ext:Column runat="server" DataIndex="DISA_BIN"     Text="禁用库位数"  Flex="1" />
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView runat="server" EnableTextSelection="true">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
