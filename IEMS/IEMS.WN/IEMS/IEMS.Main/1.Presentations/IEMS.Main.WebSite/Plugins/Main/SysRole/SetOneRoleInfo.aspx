<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetOneRoleInfo.aspx.cs" Inherits="Plugins_Main_SysRole_SetOneRoleInfo" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>设置角色权限</title>
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
        var getTreeSelectionData = function (node) {
            if (node.data.leaf) {
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
        var doSetRole = function (btn, roles, actions) {
            if (btn != "yes") {
                return;
            }
            before();
            App.direct.ResetRoleAction(roles, actions, {
                success: function (result) {
                    if (result.length == 0) {
                        Ext.Msg.alert('成功', "角色权限设置成功！");
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
            var actions = getTreeSelectionModel(App.TreePanel1.getRootNode().childNodes);
            var items = App.GridPanel1.getSelectionModel().selected.items;
            var roles = "|";
            for (var i = 0; i < items.length; i++) {
                roles = roles + items[i].data.ObjId + "|";
            }
            if ((!actions) || stringReplace(actions, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择权限！");
                return false;
            }
            if ((!roles) || stringReplace(roles, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定设置选择角色的权限吗？', function (btn) { doSetRole(btn, roles, actions) });
            return false;
        }


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
        var setNode = function (result, node) {
            if (!node.data.leaf) {
                node.set("checked", false);
                for (var i = 0; i < node.childNodes.length; i++) {
                    setNode(result, node.childNodes[i]);
                }
                return;
            }
            if (result.indexOf("|" + node.data.OBJID + "|") >= 0) {
                node.set("checked", true);
                setParentNode(node, true);
            }
            else {
                node.set("checked", false);
            }
        }
        var roleItemClick = function (view, record, node, index, event, fn) {
            App.Plugins_Main_SysRole_SetDeptOneRole_Window.setTitle("设置部门角色[" + record.data.RoleName + "]");
            App.Plugins_Main_SysRole_SetUserOneRole_Window.setTitle("设置用户角色[" + record.data.RoleName + "]");
            var role = record.data.ObjId || "";
            App.hiddenRoleID.setValue(role);
            App.direct.GetActionInfo(role, {
                success: function (result) {
                    App.TreePanel1.setTitle("权限信息[" + record.data.RoleName + "]");
                    setNode(result, App.TreePanel1.getRootNode());
                    App.TreePanel3.store.reload();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        }
    </script>

    <!--使用角色拷贝权限-->
    <script type="text/javascript">
        var McUI_SearchBox_SearchBoxSspRole_Request = function (role) {
            var items = App.GridPanel1.getSelectionModel().selected.items;
            var roleid = items[0].data.ObjId;
            before();
            App.direct.SetRoleActionByOther(roleid, role.data.OBJID, {
                success: function (result) {
                    after();
                    Ext.Msg.alert('成功', "权限设置成功！");
                },
                failure: function (errorMsg) {
                    after();
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
        }
        var showWindow = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.McUI_SearchBox_SearchBoxSspRole_Window.show();
        }
        var setRoleActionByOther = function () {
            var items = App.GridPanel1.getSelectionModel().selected.items;
            if (items.length <= 0) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            var roleid = items[0].data.ObjId;
            if (roleid == undefined) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            if (roleid.length == 0) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定复制当前角色的权限吗？', function (btn) { showWindow(btn) });
            return false;
        }
        Ext.create("Ext.window.Window", {
            id: "McUI_SearchBox_SearchBoxSspRole_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='<%= Page.ResolveUrl("~/") %>MCUI/SearchBox/SearchBoxSspRole.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            bodyPadding: 5,
            closable: true,
            title: "请选择角色",
            modal: true
        })


        Ext.create("Ext.window.Window", {
            id: "Plugins_Main_SysRole_SetDeptOneRole_Window",
            html: "<iframe src='/Plugins/Main/SysRole/SetDeptOneRole.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            bodyPadding: 5,
            height: 500,
            width: 600,
            hidden: true,
            closable: true,
            title: "设置部门角色",
            titleAlign: "center",
            modal: true
        })
        Ext.create("Ext.window.Window", {
            id: "Plugins_Main_SysRole_SetUserOneRole_Window",
            html: "<iframe src='/Plugins/Main/SysRole/SetUserOneRole.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            bodyPadding: 5,
            height: 500,
            width: 800,
            hidden: true,
            closable: true,
            title: "设置用户角色",
            titleAlign: "center",
            modal: true
        })
        var Plugins_Main_SysRole_SetDeptOneRole_Request = function () {
            App.TreePanel3.store.reload();
        }
        var SetDeptOneRole = function () {
            var roleid = App.hiddenRoleID.getValue();
            if (roleid.length == 0) {
                Ext.Msg.alert('提示', "请选择角色");
                return;
            }
            var window = App.Plugins_Main_SysRole_SetDeptOneRole_Window;
            var html = "<iframe src='SetDeptOneRole.aspx?RoleID=" + roleid + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (window.getBody()) {
                window.getBody().update(html);
            } else {
                window.html = html;
            }
            window.show();
        }
        var Plugins_Main_SysRole_SetUserOneRole_Request = function () {
            App.TreePanel3.store.reload();
        }
        var SetUserOneRole = function () {
            var roleid = App.hiddenRoleID.getValue();
            if (roleid.length == 0) {
                Ext.Msg.alert('提示', "请选择角色");
                return;
            }
            var window = App.Plugins_Main_SysRole_SetUserOneRole_Window;
            var html = "<iframe src='SetUserOneRole.aspx?RoleID=" + roleid + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (window.getBody()) {
                window.getBody().update(html);
            } else {
                window.html = html;
            }
            window.show();
        }
    </script>

    <script type="text/javascript">
        var onTreeCheckNoChange = function (node, checked, fn) {
            node.set("checked", !checked);
            return false;
        }
    </script>


    <script type="text/javascript">

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var UserId = record.data.ObjId;
            var RoleId = App.hiddenRoleID.getValue();
            App.direct.commandcolumn_direct_delete(RoleId, UserId, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                    if (result.indexOf("成功") > 0) {
                        App.TreePanel3.store.reload();
                    }
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Region="West" Collapsible="true" MultiSelect="false" FolderSort="true"
                            Title="角色信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="store" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindRoleData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjId" />
                                                <ext:ModelField Name="RoleName" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:Column ID="ObjID" DataIndex="ObjId" runat="server" Text="角色编码" Width="80" />
                                    <ext:Column ID="RoleName" DataIndex="RoleName" runat="server" Text="角色名称" Flex="1" />
                                    <ext:Column ID="Column1" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <ItemClick Fn="roleItemClick"></ItemClick>
                            </Listeners>
                        </ext:GridPanel>
                        <ext:Hidden ID="hiddenRoleID" runat="server"></ext:Hidden>
                        <ext:Panel ID="Panel1" runat="server" Region="Center" Flex="2" AutoHeight="true" Layout="BorderLayout" Collapsible="false">
                            <Items>
                                <ext:TreePanel ID="TreePanel1" runat="server" Flex="1" Region="North" Collapsible="true" RootVisible="false" MultiSelect="true"
                                    Title="权限信息" TitleAlign="Center">
                                    <TopBar>
                                        <ext:Toolbar runat="server" ID="toolbar1">
                                            <Items>
                                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                                <ext:Button runat="server" Icon="FolderWrench" Text="设置角色权限" ID="btnSetRole">
                                                    <Listeners>
                                                        <Click Fn="setRole"></Click>
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                                <ext:Button runat="server" Icon="UserBrown" Text="当前角色权限复制到" ID="btnRoleCopy">
                                                    <Listeners>
                                                        <Click Fn="setRoleActionByOther" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
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

                                <ext:Panel ID="Panel2" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                                    <Items>
                                        <ext:TreePanel ID="TreePanel2" runat="server" Flex="1" Region="West" Collapsible="false" RootVisible="false" MultiSelect="true"
                                            Title="部门信息" TitleAlign="Center">
                                            <TopBar>
                                                <ext:Toolbar runat="server" ID="toolbar2">
                                                    <Items>
                                                        <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                                        <ext:Button runat="server" Icon="FolderWrench" Text="绑定部门" ID="Button1">
                                                            <Listeners>
                                                                <Click Fn="SetDeptOneRole"></Click>
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:ToolbarSeparator ID="toolbarSeparator4" />
                                                        <ext:ToolbarSpacer runat="server" ID="toolbarSpacer1" />
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                            <Store>
                                                <ext:TreeStore ID="treeStoreUser" runat="server">
                                                    <Proxy>
                                                        <ext:PageProxy>
                                                            <RequestConfig Method="GET" Type="Load" />
                                                        </ext:PageProxy>
                                                    </Proxy>
                                                    <Root>
                                                        <ext:Node NodeID="Root" Expanded="true" />
                                                    </Root>
                                                </ext:TreeStore>
                                            </Store>
                                            <Listeners>
                                                <CheckChange Fn="onTreeCheckNoChange" />
                                            </Listeners>
                                        </ext:TreePanel>
                                        <ext:GridPanel ID="TreePanel3" runat="server" Flex="1" Region="Center" Collapsible="false" RootVisible="false" MultiSelect="true"
                                            Title="角色包含人员信息" TitleAlign="Center">
                                            <TopBar>
                                                <ext:Toolbar runat="server" ID="toolbar3">
                                                    <Items>
                                                        <ext:ToolbarSeparator ID="toolbarSeparator5" />
                                                        <ext:Button runat="server" Icon="FolderWrench" Text="绑定人员" ID="Button3">
                                                            <Listeners>
                                                                <Click Fn="SetUserOneRole"></Click>
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:ToolbarSeparator ID="toolbarSeparator7" />
                                                        <ext:ToolbarSpacer runat="server" ID="toolbarSpacer2" />
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                            <Store>
                                                <ext:Store ID="store1" runat="server">
                                                    <Proxy>
                                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindUserData" />
                                                    </Proxy>
                                                    <Model>
                                                        <ext:Model ID="model3" runat="server" IDProperty="EquipCode">
                                                            <Fields>
                                                                <ext:ModelField Name="ObjId" />
                                                                <ext:ModelField Name="HRCode" />
                                                                <ext:ModelField Name="UserName" />
                                                                <ext:ModelField Name="WorkBarcode" />
                                                                <ext:ModelField Name="Remark" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="columnModel1" runat="server">
                                                <Columns>
                                                    <ext:Column ID="UserObjId" DataIndex="ObjId" runat="server" Text="用户编码" Width="80" />
                                                    <ext:Column ID="UserName" DataIndex="UserName" runat="server" Text="用户名称" Flex="1" />
                                                    <ext:CommandColumn ID="commandCol" runat="server" Width="60" Text="操作" Align="Center">
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
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>

