<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPageMenu.aspx.cs" Inherits="Plugins_Main_SysMenu_SetPageMenu" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设置系统菜单</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.OBJID;
            App.direct.commandcolumn_direct_edit(ObjID, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击恢复按钮
        var commandcolumn_direct_recover = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.OBJID;
            App.direct.commandcolumn_direct_recover(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.OBJID;
            App.direct.commandcolumn_direct_delete(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            if (command.toLowerCase() == "recover") {
                Ext.Msg.confirm("提示", '您确定需要恢复此条信息？', function (btn) { commandcolumn_direct_recover(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return;
                }
                try {
                    if (/^[\d]+$/.test(val)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (e) {
                    return false;
                }
            },
            integerText: "此填入项格式为正整数"
        });

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_obj_id.setValue("");
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //历史查询按钮点击列表刷新数据重载方法
        var pnlHistoryListFresh = function () {
            App.hidden_obj_id.setValue("");
            App.hidden_delete_flag.setValue("");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //历史查询根据DeleteFlag的值进行样式绑定
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("DELETE_FLAG") == "1") {
                return "x-grid-row-deleted";
            }
        }
        //历史查询的每行按钮准备加载
        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("DELETE_FLAG") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            } else {
                toolbar.items.getAt(4).hide();
            }
        };

    </script>
    <script>
        //树形结构点击刷新右侧方法
        var treePanelMenuPage = function (store, operation, options) {
            var node = operation.node;
            var nodeid = node.getId() || "";
            App.direct.treePanelMenuPageLoad(nodeid, {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    var data = Ext.decode(result);
                    if (data != "") {
                        node.appendChild(data, undefined, true);
                        node.expand();
                    }
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        };

        var treeMenuPageClick = function (record) {
            App.hidden_obj_id.setValue(record.getId());
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
        }

        var filterTree = function (tf, e) {
            var tree = App.treeDept,
                text = tf.getRawValue();

            tree.clearFilter();

            if (Ext.isEmpty(text, false)) {
                clearFilter();
                return;
            }

            if (e.getKey() === Ext.EventObject.ESC) {
                clearFilter();
            } else {
                var re = new RegExp(".*" + text + ".*", "i");

                tree.filterBy(function (node) {
                    return re.test(node.data.text);
                });
            }
        };

        var clearFilter = function () {
            var field = App.triggerField,
                tree = App.treeDept;

            field.setValue("");
            tree.clearFilter(true);
            tree.getView().focus();
        };
    </script>

</head>
<body>
    <form id="fmUnit" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Width="235" Layout="BorderLayout">
                    <Items>
                         <ext:TreePanel ID="treeDept" runat="server" Title="菜单列表" Region="Center"  Icon="FolderGo" AutoHeight="true" RootVisible="false">
                            <Store>
                                <ext:TreeStore ID="treeDeptStore" runat="server">
                                    <Proxy>
                                        <ext:PageProxy>
                                            <RequestConfig Method="GET" Type="Load" />
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Root>
                                        <ext:Node NodeID="root" Expanded="true" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <Listeners>
                                <BeforeLoad Fn="treePanelMenuPage" />
                                <ItemClick Handler="treeMenuPageClick(record)" />
                            </Listeners>
                        </ext:TreePanel>       
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_add_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                    <ext:ToolbarSeparator ID="toolbarSeparator_middle" />
                                    <ext:Button runat="server" Icon="Note" Text="历史查询" ID="btn_history_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行历史查询" />
                                    </ToolTips>
                                        <Listeners>
                                        <Click Fn="pnlHistoryListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                    <ext:ToolbarSeparator ID="toolbarSeparator_middle_2" />
                                    <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_obj_id" runat="server" Vtype="integer" FieldLabel="菜单编号" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_show_name" runat="server" FieldLabel="菜单名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_remark" runat="server" FieldLabel="备注" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="15">  
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="OBJID" />
                                        <ext:ModelField Name="MENU_LEVEL" />
                                        <ext:ModelField Name="SHOW_NAME" />
                                        <ext:ModelField Name="PAGE_URL"  />
                                        <ext:ModelField Name="ICO_NAME" />
                                        <ext:ModelField Name="IS_SHOW"  />
                                        <ext:ModelField Name="RECORD_USER_ID"  />
                                        <ext:ModelField Name="RECORD_TIME"  />
                                        <ext:ModelField Name="SEQ_INDEX"  />
                                        <ext:ModelField Name="DELETE_FLAG"  />
                                        <ext:ModelField Name="REMARK" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" />
                            <ext:Column     ID="obj_id"         runat="server" Text="菜单编号" DataIndex="OBJID"            Width="60"      />
                            <ext:Column     ID="menu_level"     runat="server" Text="菜单层级" DataIndex="MENU_LEVEL"       Width="60"      />
                            <ext:Column     ID="show_name"      runat="server" Text="菜单名称" DataIndex="SHOW_NAME"        Width="100"     />
                            <ext:Column     ID="page_url"       runat="server" Text="PAGE_URL" DataIndex="PAGE_URL"         Width="300"     />
                            <ext:Column     ID="ico_name"       runat="server" Text="展示图标" DataIndex="ICO_NAME"         Width="60"      />
                            <ext:Column     ID="is_show"        runat="server" Text="是否可见" DataIndex="IS_SHOW"          Width="60"      />
                            <ext:Column     ID="record_user_id" runat="server" Text="记录人员" DataIndex="RECORD_USER_ID"   Width="60"      Visible="false"  />
                            <ext:DateColumn ID="record_time"    runat="server" Text="记录时间" DataIndex="RECORD_TIME"      Width="140" Format="yyyy-MM-dd hh:mm:ss"   Visible="false"    />
                            <ext:Column     ID="seq_idx"        runat="server" Text="排列序号" DataIndex="SEQ_INDEX"          Width="60"      />
                            <ext:Column     ID="delete_flag"    runat="server" Text="删除标志" DataIndex="DELETE_FLAG"      Width="80"  Visible="false"  />
                            <ext:Column     ID="remark"         runat="server" Text="备注"     DataIndex="REMARK"           Width="60"      />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Accept" CommandName="Recover" Text="恢复">
                                        <ToolTip Text="恢复本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar Fn="prepareToolbar" />
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改菜单信息"
                    Width="500" Height="290" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="FormLayout">
                    <Items> 
                        <ext:FormPanel ID="pnlEdit" runat="server" BodyPadding="5" Layout="FormLayout">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="modify_obj_id"       runat="server" FieldLabel="菜单编号"    LabelAlign="Left" ReadOnly="true"   />
                                <ext:TextField ID="modify_menu_level"   runat="server" FieldLabel="菜单层级"    LabelAlign="Left"                   />
                                <ext:TextField ID="modify_show_name"    runat="server" FieldLabel="菜单名称"    LabelAlign="Left"                   />
                                <ext:TextField ID="modify_page_url"     runat="server" FieldLabel="URL地址"     LabelAlign="Left"                   />
                                <ext:TextField ID="modify_ico_name"     runat="server" FieldLabel="图标名称"    LabelAlign="Left"                   />
                                <ext:ComboBox  ID="modify_is_show"      runat="server" FieldLabel="是否可见"    LabelAlign="Left"                   />
                                <ext:TextField ID="modify_seq_idx"      runat="server" FieldLabel="排序编号"    LabelAlign="Left"                   />
                                <ext:TextField ID="modify_remark"       runat="server" FieldLabel="备注"        LabelAlign="Left"                   />
                            </Items> 
                            <Listeners>
                                <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifySave_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>

                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加菜单信息"
                Width="500" Height="290" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" MonitorValid="true" BodyPadding="5" Layout="FormLayout">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="add_menu_level"      runat="server" FieldLabel="菜单层级"    LabelAlign="Left"                   />
                                <ext:TextField ID="add_show_name"       runat="server" FieldLabel="菜单名称"    LabelAlign="Left"                   />
                                <ext:TextField ID="add_page_url"        runat="server" FieldLabel="URL地址"     LabelAlign="Left"                   />
                                <ext:TextField ID="add_ico_name"        runat="server" FieldLabel="图标名称"    LabelAlign="Left"                   />
                                <ext:ComboBox  ID="add_is_show"         runat="server" FieldLabel="是否可见"    LabelAlign="Left"                   />
                                <ext:TextField ID="add_seq_idx"         runat="server" FieldLabel="排序编号"    LabelAlign="Left"                   />
                                <ext:TextField ID="add_remark"          runat="server" FieldLabel="备注"        LabelAlign="Left"                   />
                            </Items>    
                        <Listeners>
                            <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="BtnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
            </ext:Window>
            <ext:Hidden ID="hidden_obj_id" runat="server"></ext:Hidden>
            <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
