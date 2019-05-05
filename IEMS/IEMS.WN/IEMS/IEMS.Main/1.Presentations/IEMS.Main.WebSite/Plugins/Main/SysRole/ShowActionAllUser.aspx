<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowActionAllUser.aspx.cs" Inherits="Plugins_Main_SysRole_ShowActionAllUser" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>查看权限对应的所有用户</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>

    <script type="text/javascript">        //树节点选中
        var onTreeCheckChange = function (node, checked, fn) {
            node.set("checked", !checked);
            return false;
        }
    </script>
    <script type="text/javascript">
        var after = function () {
            App.waitProgressWindow.close();
        }
        var before = function () {
            App.waitProgressWindow.show();
        }
        var treePanelDept = function (store, operation, options) {
            var node = operation.node;
            var nodeid = node.getId() || "";
            App.direct.IniDeptTree(nodeid, {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    for (var i = 0; i < result.children.length; i++) {
                        var data = result.children[i];
                        node.appendChild(data, undefined, true);
                    }
                    node.expand();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        };
        var getTitle = function (node) {
            var Result = "";
            if (node.parentNode) {
                Result = Result + getTitle(node.parentNode);
            }
            Result = Result + node.data.SHOW_NAME + "-";
            return Result;
        }
        var deptItemClick = function (view, record, node, index, event, fn) {
            if (!record.data.leaf) {
                return;
            }
            App.txt_user_name.setValue("");
            App.hiddenAcitonId.setValue(record.data.OBJID);
            var title = getTitle(record);
            title = (title.substring(1, title.length - 1));
            App.GridPanel1.setTitle("用户信息[" + title + "]");
            App.GridPanel1.store.currentPage = 1;
            App.GridPanel1.store.reload();
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Hidden ID="hiddenAcitonId" runat="server"></ext:Hidden>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolbar1">
                            <Items> 
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_search_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="pnlUserQuery" runat="server" Region="North" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_user_name" runat="server" FieldLabel="用户姓名" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                        <ext:GridPanel ID="GridPanel1" runat="server" Flex="2" Region="Center" Collapsible="false" MultiSelect="false" FolderSort="true"
                            Title="用户信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="store" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model3" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjId" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="WorkBarcode" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:Column ID="obj_id" DataIndex="ObjId" runat="server" Text="用户编码" Width="80" />
                                    <ext:Column ID="user_name" DataIndex="UserName" runat="server" Text="用户名称" Flex="1" />
                                    <ext:Column ID="user_remark" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="2" Region="West" Collapsible="false" RootVisible="false" MultiSelect="false"
                            Title="权限信息" TitleAlign="Center">
                            <Store>
                                <ext:TreeStore ID="TreeStore2" runat="server">
                                    <Model>
                                        <ext:Model ID="model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="OBJID" />
                                                <ext:ModelField Name="SHOW_NAME" />
                                                <ext:ModelField Name="REMARK" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Root>
                                        <ext:Node NodeID="Root" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:TreeColumn ID="show_name" DataIndex="SHOW_NAME" runat="server" Sortable="false" Hideable="false" Text="权限名称" Width="200" />
                                    <ext:Column ID="power_remark" DataIndex="REMARK" runat="server" Sortable="false" Hideable="false" Text="权限备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <ItemClick Fn="deptItemClick"></ItemClick>
                            </Listeners>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
