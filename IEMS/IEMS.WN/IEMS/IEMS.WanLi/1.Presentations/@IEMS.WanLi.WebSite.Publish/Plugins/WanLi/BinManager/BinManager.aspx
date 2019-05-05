<%@ page language="C#" autoeventwireup="true" inherits="Plugins_WanLi_BinManager_BinManager, IEMS.WanLi.WebSite" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>库位状态查询</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <style type="text/css">
        .x-grid-cell-mouseover {
            background-color: #0000ff !important;
        }
    </style>
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>

    <!--查询数据信息-->
    <script type="text/javascript">
        //列表刷新数据重载方法
        var gridMainFresh = function () {
            App.direct.GetStoreStatus();
            return false;
        }
    </script>

    <!--选择某个单元格-->
    <script type="text/javascript">
        var todata = function (data) {
            if ((!data) || ("" == data)) {
                return;
            }
            return eval(data)[0];
        }
        //列表刷新数据重载方法
        var setBinStaus = function (log) {
            App.txtStatusBar.setText(log);
            return false;
        }
    </script>

    <!--格式化网格颜色-->
    <script type="text/javascript">
        $(document).ready(function () {
            window.setInterval(gridMainFresh, 300000);
        });
        var columns_renderer = function (value, a) {
            try {
                var data = todata(value);
                var binStatus = data.BinStatus;
                var bizStatus = data.BinBizStatus;
                var showBinStatus = binStatus;
                if (data.MaterialNo) {
                    if (data.MaterialNo.indexOf("BPALLET") == 0) {
                        showBinStatus = "B";
                    }
                }
                if (data.UsedFlag != 1) {
                    a.style = "background-color:Red; !important;";
                } else {
                    if ("_" == binStatus) {
                        a.style = "background-color:#FFFFEE; !important;"
                    }
                    if ("I" == bizStatus) {
                        a.style = "background-color:#6699FF; !important;"
                        showBinStatus = "I";
                    }
                    if ("E" == bizStatus) {
                        a.style = "background-color:Magenta; !important;"
                        showBinStatus = "E";
                    }
                    if ("P" == bizStatus) {
                        a.style = "background-color:#FF99CC; !important;"
                        showBinStatus = "P";
                    }
                    if ("$" == binStatus) {
                        if ("O" == bizStatus) {
                            a.style = "background-color:Cyan; !important;"
                            showBinStatus = "O";
                        }
                        if ("T" == bizStatus) {
                            a.style = "background-color:Yellow; !important;"
                            showBinStatus = "T";
                        }
                        if ("C" == bizStatus) {
                            a.style = "background-color:#005577; !important;"
                            showBinStatus = "C";
                        }
                        if ("_" == bizStatus) {
                            if ("B" == showBinStatus) {
                                a.style = "background-color:#FF9900; !important;"
                            } else {
                                a.style = "background-color:#33CC33; !important;"
                            }
                        }
                    }
                }
                return showBinStatus;
            }
            catch (e) {
                return "";
            }
        };
    </script>

    <!--设置库位状态-->
    <script type="text/javascript">
        var oncellclick = function (e, data) {
            App.panelMainDetail.getForm().loadRecord({ data: data });
            App.btnShowOnOver.setText("鼠标点击显示");
            App.btnShowOnOver.setDisabled(false);
            if (data.UsedFlag) {
                App.btnBinDisable.setText("库位禁用");
            } else {
                App.btnBinDisable.setText("库位正常");
            }
            App.btnBinDisable.setDisabled(false);
        }
    </script>
    
    <!--显示库位状态-->
    <script type="text/javascript">
        var oncellover = function (e, data) {
            if (App.btnShowOnOver.disabled) {
                App.panelMainDetail.getForm().loadRecord({ data: data });
            }
            var html = "库位号：" + data.BinNo + "<br\>";
            html = html + "可用状态：" + data.UsedFlag + "<br\>";
            html = html + "库位状态：" + data.BinStatus + "<br\>";
            html = html + "业务状态：" + data.BinBizStatus + "<br\>";
            html = html + "使用设备：" + data.CrnNo + "<br\>";
            html = html + "物料编号：" + data.MaterNo + "<br\>";
            html = html + "工装编号1：" + data.PalletNo1 + "<br\>";
            html = html + "工装编号2：" + data.PalletNo2 + "<br\>";
            html = html + "数量：" + data.Qty + "<br\>";
            Ext.create("Ext.tip.ToolTip",
                {
                    target: e.currentTarget,
                    trackMouse: true,
                    html: html,
                    titile: "提示"
                }
            ).show();
        }
    </script>

    <!--查询库存状态-->
    <script type="text/javascript">
        var getdata = function (e) {
            var target = e.currentTarget;
            var cellIndex = target.cellIndex;
            var rowIndex = target.parentElement.rowIndex;
            var columns = App.gridMain.getView().getGridColumns();
            var column = columns[cellIndex];
            if (!column) {
                return;
            }
            if (!column.dataIndex) {
                return;
            }
            var store = App.gridMain.getStore();
            var record = store.data.items[rowIndex - 1]
            var data = record.data[column.dataIndex];
            return todata(data);
        }

        var mouseupevent = function (e) {
            var data = getdata(e);
            if (data) {
                oncellclick(e, data);
            }
            return false;
        }

        var lastTarget; var lastClassName;
        var mouseoverevent = function (e) {
            var target = e.currentTarget;
            if (lastTarget) {
                lastTarget.className = lastClassName;
            }
            lastTarget = target;
            lastClassName = lastTarget.className;
            if (e.currentTarget.cellIndex == 0) {
                return;
            }
            target.className = "x-grid-cell-mouseover";


            var data = getdata(e);
            oncellover(e, data);
            return false;
        }
        var timeout_setonmouseover = function () {
            var doms = Ext.query(".x-grid-cell");
            var len = doms.length;
            for (i = 0; i < len; i++) {
                doms[i].onmouseover = mouseoverevent;
                doms[i].onmouseup = mouseupevent;
            }
            setBinStaus("库位信息更新完成！");
        }
        var setonmouseover = function () {
            setBinStaus("库位信息查询完成！");
            setTimeout("timeout_setonmouseover()", 1000);
        }
    </script>
    
    <!--设置库位状态-->
    <script type="text/javascript">
        var postSetBinUseFalg = function (data) {
            App.direct.SetBinUseFalg(data.BinNo, data.UsedFlag,
                {
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
        //列表刷新数据重载方法
        var setBinUseFalg = function () {
            var data = App.panelMainDetail.getForm().getRecord().data;
            if (!data) {
                Ext.Msg.alert("提示", "请选择需要修改的库位");
            }
            var msg = "确定将库位【" + data.BinNo + "】" + "设置为【"
            if (data.UsedFlag) {
                msg = msg + "禁用";
            }
            else {
                msg = msg + "正常";
            }
            msg = msg + "】？";
            Ext.Msg.confirm("提示", msg, function (btn) {
                if (btn == "yes") {
                    postSetBinUseFalg(data);
                }
            });
            return false;
        }
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="border">
            <Items>
                <ext:Panel runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarSeparator />                                
                                <ext:Button runat="server" Icon="Find" Text="查询库位信息" ID="btnSearch">
                                    <Listeners>
                                        <Click Handler="gridMainFresh();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSpacer />
                                <ext:TextField ID="txtStoreWhNo" runat="server" Visible="false"></ext:TextField>
                                <ext:ToolbarFill />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:StatusBar runat="server" ID="txtStatusBar" Text="请进行库位信息查询！"></ext:StatusBar>
                    </Items>
                </ext:Panel>

                <ext:GridPanel ID="gridMain" runat="server" Region="Center" Floatable="false" ColumnLines="true" RowLines="true">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarFill />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="禁止：<span style='display:inline-block;background-color:Red;height:16px;width:16px;'>&nbsp;&nbsp;</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="空库：<span style='display:inline-block;background-color:#FFFFEE;height:16px;width:16px;'>&nbsp;_</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="库存：<span style='display:inline-block;background-color:#33CC33;height:16px;width:16px;'>&nbsp;$</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="预约入库：<span style='display:inline-block;background-color:#6699FF;height:16px;width:16px;'>&nbsp;I</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="预约出库：<span style='display:inline-block;background-color:Cyan;height:16px;width:16px;'>&nbsp;O</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="移库占用：<span style='display:inline-block;background-color:Yellow;height:16px;width:16px;'>&nbsp;T</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="空出库位：<span style='display:inline-block;background-color:Magenta;height:16px;width:16px;'>&nbsp;E</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="先入库位：<span style='display:inline-block;background-color:#FF99CC;height:16px;width:16px;'>&nbsp;P</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                                <ext:Label runat="server"
                                    Html="空笼库位：<span style='display:inline-block;background-color:#FF9900;height:16px;width:16px;'>&nbsp;B</span>">
                                </ext:Label>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSeparator />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store runat="server" PageSize="15">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="WH_NO" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="45" runat="server" />
                            <ext:Column runat="server" DataIndex="WH_NO" Text="请进行库位信息查询" Flex="1" Sortable="false" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CellSelectionModel runat="server" Mode="Single" />
                    </SelectionModel>
                    <View>
                        <ext:GridView runat="server" EnableTextSelection="true">
                        </ext:GridView>
                    </View>
                </ext:GridPanel>
     
		 <ext:FormPanel ID="panelMainDetail" runat="server" Region="East" BodyPadding="5" Width="300" Collapsible="true" Collapsed="false"
                    Title="详细信息" AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="鼠标滑动显示" ID="btnShowOnOver" Disabled="true">
                                    <Listeners>
                                        <Click Handler="App.btnShowOnOver.setDisabled(true);App.btnBinDisable.setDisabled(true);App.btnShowOnOver.setText('鼠标滑动显示'); return false;" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="LockKey" Text="库位禁用" ID="btnBinDisable" Disabled="true">
                                    <Listeners>
                                        <Click Handler="setBinUseFalg();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarSpacer />
                                <ext:ToolbarFill />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <FieldDefaults>
                        <CustomConfig>
                            <ext:ConfigItem Name="LabelWidth" Value="56" Mode="Raw" />
                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                        </CustomConfig>
                    </FieldDefaults>
                    <Items>
                        <ext:Container runat="server" Layout="FormLayout" Padding="5" ColumnWidth="1">
                            <Items>
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="库位编号" Name="BinNo" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="库位可用" Name="UsedFlag" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="库位状态" Name="BinStatus" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="业务状态" Name="BinBizStatus" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="使用设备" Name="CrnNo" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料编码" Name="MaterNo" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料名称" Name="MaterName" />
                                
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="大类" Name="MaterMkind" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="小类" Name="MaterType" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="等级" Name="Grade" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="规格" Name="MaterSpec" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="描述" Name="MaterDesc" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="货笼ID1" Name="PalletNo1" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="货笼ID2" Name="PalletNo2" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="批次号" Name="BatchNo" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="工装数量" Name="Qty" />
                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="生产时间" Name="ProductDate" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:FormPanel>      
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
