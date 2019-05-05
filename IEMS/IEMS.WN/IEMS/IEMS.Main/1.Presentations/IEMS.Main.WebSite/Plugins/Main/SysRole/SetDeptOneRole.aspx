<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetDeptOneRole.aspx.cs" Inherits="Plugins_Main_SysRole_SetDeptOneRole" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>设置部门角色</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>

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
            return node.data.ObjId;
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
        var getTreeNoSelectionModel = function (nodes) {
            var Result = "|";
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                Result = Result + getTreeSelectionData(node) + "|";
                try {
                    if (node.childNodes) {
                        Result = Result + getTreeNoSelectionModel(node.childNodes) + "|";
                    }
                } catch (e) { }
            }
            return Result;
        }
        var closeParent = function () {
            parent.Plugins_Main_SysRole_SetDeptOneRole_Request();
            parent.App.Plugins_Main_SysRole_SetDeptOneRole_Window.close();
            return false;
        }
        var doSetRole = function (btn, depts, roles) {
            if (btn != "yes") {
                return;
            }
            before();
            App.direct.ResetRoleAction(depts, roles, {
                success: function (result) {
                    if (result.length == 0) {
                        Ext.Msg.alert('成功', "部门角色设置成功！", function (btn) { closeParent() });
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
            var depts = getTreeNoSelectionModel(App.TreePanel1.getRootNode().childNodes);
            var roles = getTreeSelectionModel(App.TreePanel1.getRootNode().childNodes);
            Ext.Msg.confirm("提示", '您确定设置选择部门的角色吗？', function (btn) { doSetRole(btn, depts, roles) });
            return false;
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Hidden ID="hiddenHaveRole" runat="server"></ext:Hidden>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolbar1">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="FolderWrench" Text="设置部门角色" ID="btnSetRole">
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
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="1" Region="Center" Collapsible="false" RootVisible="false" MultiSelect="true"
                            Title="部门信息" TitleAlign="Center">
                            <Store>
                                <ext:TreeStore ID="treeDeptStore" runat="server">
                                    <Model>
                                        <ext:Model ID="model" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjId" />
                                                <ext:ModelField Name="DeptName" />
                                                <ext:ModelField Name="HrCode" />
                                                <ext:ModelField Name="ParentId" />
                                                <ext:ModelField Name="DeleteFlag" />
                                                <ext:ModelField Name="Remark" />
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
                                    <ext:TreeColumn ID="DepName" DataIndex="DeptName" runat="server" Sortable="false" Hideable="false" Text="部门名称" Width="300" />
                                    <ext:Column ID="DepCode" DataIndex="ObjId" runat="server" Sortable="false" Hideable="false" Text="部门编号" Align="Center" Width="60" />
                                    <ext:Column ID="DeleteFlag" DataIndex="DeleteFlag" runat="server" Sortable="false" Hideable="false" Text="部门状态" Width="60" />
                                    <ext:Column ID="Remark" DataIndex="Remark" runat="server" Sortable="false" Hideable="false" Text="部门备注" Width="150" />
                                </Columns>
                            </ColumnModel>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
