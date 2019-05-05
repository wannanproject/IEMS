<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetUserAction.aspx.cs" Inherits="Manager_System_SetRoleAction_SetUserAction" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>设置用户权限</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>

    <script type="text/javascript">        //树节点选中
        var setParentNode = function (node, checked) {
            var pNode = node;
            while (checked) {
                pNode = pNode.parentNode;
                if (!pNode) {
                    break;
                }
                pNode.set("checked", checked);
            }
        }
        var setChildNode = function (node, checked) {
            var nodes = node.childNodes;
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                node.set("checked", checked);
                if (node.data.leaf) {
                    continue;
                }
                setChildNode(node, node.data.checked);
            }
        }
        var onTreeCheckChange = function (node, checked, fn) {
            setParentNode(node, checked);
            setChildNode(node, checked);
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

        var getTreeSelectionData = function (node) {
            if (node.data.leaf && node.data.OBJID) {
                return node.data.OBJID;
            }
            return "";
        }
        var getTreeSelectionModel = function (nodes) {
            var Result = "|";
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                if (node.data.checked) {
                    Result = Result + getTreeSelectionData(node) + "|";
                }
                try {
                    if (node.childNodes) {
                        Result = Result + getTreeSelectionModel(node.childNodes) + "|";
                    }
                } catch (e) { }
            }
            return Result;
        }
        var doSetRole = function (btn, users, actions) {
            if (btn != "yes") {
                return;
            }
            before();
            App.direct.ResetRoleAction(users, actions, {
                success: function (result) {
                    if (result.length == 0) {
                        Ext.Msg.alert('成功', "用户权限设置成功！");
                    }
                    else {
                        Ext.Msg.alert('提示', result);
                    }
                    after();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                    after();
                }
            });
            return false;
        }
        var stringReplace = function (sss, ss, s) {
            var str = sss;
            while (true) {
                if (str.indexOf(ss) < 0) {
                    return str;
                }
                str = str.replace(ss, s);
            }
        }
        var setRole = function () {
            var actions = getTreeSelectionModel(App.TreePanel2.getRootNode().childNodes);
            var items = App.GridPanel1.getSelectionModel().selected.items;
            var users = "|";
            for (var i = 0; i < items.length; i++) {
                users = users + items[i].data.ObjId + "|";
            }
            if ((!users) || stringReplace(users, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择用户！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定设置选择用户的权限吗？', function (btn) { doSetRole(btn, users, actions) });
            return false;
        }
        var deptItemClick = function (view, record, node, index, event, fn) {
            App.hiddenSearch.setValue("0");
            App.txtUserName.setValue("");
            App.txtWorkBarcode.setValue("");
            App.hiddenDeptCode.setValue(record.data.OBJID);
            App.GridPanel1.setTitle("用户信息[" + record.data.DEPT_NAME + "]");
            App.GridPanel1.store.currentPage = 1;
            App.GridPanel1.store.reload();
        }

        var pnlListUserFresh = function () {
            App.hiddenSearch.setValue("1");
            App.hiddenDeptCode.setValue("");
            App.GridPanel1.setTitle("用户信息");
            App.GridPanel1.store.currentPage = 1;
            App.GridPanel1.store.reload();
            return false;
        }

        //-yuany 2013年9月14日 关于点击用户展示用户权限信息
        var isInResult = function (result, node) {
            var id = node.data.OBJID;
            for (var i = 0; i < result.length; i++) {
                var data = result[i];
                if (data.ActionId == id) {
                    var ss = data.M.toString();
                    ss = stringReplace(ss, "0", "");
                    ss = stringReplace(ss, "1", "用户定义;");
                    ss = stringReplace(ss, "2", "用户角色;");
                    ss = stringReplace(ss, "3", "部门权限;");
                    ss = stringReplace(ss, "4", "部门角色;");
                    node.set("RemarkMode", ss);
                    return true;
                }
            }
            return false;
        }
        var setNode = function (result, node) {
            if (!node.data.leaf) {//不是最终叶子节点
                node.set("checked", false);
                for (var i = 0; i < node.childNodes.length; i++) {
                    setNode(result, node.childNodes[i]);
                }
                return;
            }
            if (isInResult(result, node)) {
                node.set("checked", true);
                setParentNode(node, true);
            }
            else {
                node.set("checked", false);
                node.set("RemarkMode", "");
            }
        }
        var roleItemClick = function (view, record, node, index, event, fn) {
            var role = record.data.ObjId || "";
            App.direct.GetActionInfo(role, {
                success: function (result) {
                    App.TreePanel2.setTitle("权限信息[" + record.data.UserName + "]");
                    setNode(result, App.TreePanel2.getRootNode());
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        }
        //-
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>

                <ext:Hidden ID="hiddenDeptCode" runat="server"></ext:Hidden>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolbar1">
                            <Items>
                                <ext:TextField ID="txtUserName" runat="server" FieldLabel="姓名" LabelAlign="Right" />
                                <ext:TextField ID="txtWorkBarcode" runat="server" FieldLabel="编码" LabelAlign="Right" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListUserFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill ID="toolbarFill_begin" />
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="FolderWrench" Text="设置用户权限" ID="btnSetRole">
                                    <Listeners>
                                        <Click Fn="setRole"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="3" Region="West" Collapsible="false" RootVisible="false" MultiSelect="true"
                            Title="部门信息" TitleAlign="Center">
                            <Store>
                                <ext:TreeStore ID="TreeStore1" runat="server">
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="OBJID" />
                                                <ext:ModelField Name="DEPT_NAME" />
                                                <ext:ModelField Name="DELETE_FLAG" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Listeners>
                                        <BeforeLoad Fn="treePanelDept" />
                                    </Listeners>
                                </ext:TreeStore>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:TreeColumn ID="DepName" DataIndex="DEPT_NAME" runat="server" Sortable="false" Hideable="false" Text="部门名称" Width="200" />
                                    <ext:Column ID="DepCode" DataIndex="OBJID" runat="server" Sortable="false" Hideable="false" Text="部门编号" Align="Center" Width="60" />
                                    <ext:Column ID="DeleteFlag" DataIndex="DELETE_FLAG" runat="server" Sortable="false" Hideable="false" Text="部门状态" Width="60" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <ItemClick Fn="deptItemClick"></ItemClick>
                            </Listeners>
                        </ext:TreePanel>
                        <ext:GridPanel ID="GridPanel1" runat="server" Flex="2" Region="Center" Collapsible="false" MultiSelect="true" FolderSort="true"
                            Title="用户信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="store" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model3" runat="server" IDProperty="ObjId">
                                            <Fields>
                                                <ext:ModelField Name="ObjId" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="WorkBarcode" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:Column ID="ObjId" DataIndex="ObjId" runat="server" Text="用户编码" Width="80" />
                                    <ext:Column ID="UserName" DataIndex="UserName" runat="server" Text="用户名称" Flex="1" />
                                    <ext:Column ID="WorkBarcode" DataIndex="WorkBarcode" runat="server" Text="用户工号" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" />
                            </SelectionModel>
                            <Listeners>
                                <ItemClick Fn="roleItemClick"></ItemClick>
                            </Listeners>
                        </ext:GridPanel>
                        <ext:TreePanel ID="TreePanel2" runat="server" Flex="2" Region="East" Collapsible="false" RootVisible="false" MultiSelect="true"
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
                                    <ext:TreeColumn ID="ShowName" DataIndex="SHOW_NAME" runat="server" Sortable="false" Hideable="false" Text="权限名称" Width="200" />
                                    <ext:Column ID="Remark2" DataIndex="REMARK" runat="server" Sortable="false" Hideable="false" Text="权限备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <CheckChange Fn="onTreeCheckChange" />
                            </Listeners>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>
                <ext:Hidden ID="hiddenSearch" runat="server" />
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
