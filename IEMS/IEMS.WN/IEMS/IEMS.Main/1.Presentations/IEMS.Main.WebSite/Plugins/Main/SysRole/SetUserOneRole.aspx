<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetUserOneRole.aspx.cs" Inherits="Plugins_Main_SysRole_SetUserOneRole" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>设置用户角色</title>
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
            if (node.data.DepCode) {
                return node.data.DepCode;
            }
            if (node.data.leaf) {
                return node.data.ObjID;
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

        var closeParent = function () {
            parent.Plugins_Main_SysRole_SetUserOneRole_Request();
            parent.App.Plugins_Main_SysRole_SetUserOneRole_Window.close();
            return false;
        }
        var doSetRole = function (btn, users, roles) {
            if (btn != "yes") {
                return;
            }
            before();
            App.direct.ResetRoleAction(users, roles, {
                success: function (result) {
                    if (result.length == 0) {
                        Ext.Msg.alert('成功', "用户角色设置成功！", function (btn) { closeParent() });
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
            var items = App.GridPanel1.getSelectionModel().selected.items;
            var users = "|";
            for (var i = 0; i < items.length; i++) {
                users = users + items[i].data.OBJID + "|";
            }
            var roles = "|";
            Ext.Msg.confirm("提示", '您确定设置选择用户的角色吗？', function (btn) { doSetRole(btn, users, roles) });
            return false;
        }
        var deptItemClick = function (view, record, node, index, event, fn) {
            App.hiddenDeptCode.setValue(record.data.ObjId);
            App.GridPanel1.setTitle("用户信息[" + record.data.DeptName + "]");
            App.GridPanel1.store.currentPage = 1;
            App.GridPanel1.store.reload();
        }
        var onGridPanel1Render = function (store) {
            var records = store.data.items;
            var arr = [];
            for (var i = 0; i < records.length; i++) {
                var record = records[i];
                if (record.data.IS_EMPLOYEE == "1") {
                    arr.push(record);
                }
            }
            App.CheckboxSelectionModel1.select(arr);
        }


        var pnlListUserFresh = function () {
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
                <ext:Hidden ID="hiddenHaveRole" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenDeptCode" runat="server"></ext:Hidden>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolbar1">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" /> 
                                <ext:TextField ID="txtUserName" runat="server" FieldLabel="姓名" LabelAlign="Right"  LabelWidth="40"/>
                                <ext:TextField ID="txtWorkBarcode" runat="server" FieldLabel="编码" LabelAlign="Right" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListUserFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator/>
                                <ext:ToolbarFill ID="toolbarFill_begin" />
                                <ext:ToolbarSeparator/>
                                <ext:Button runat="server" Icon="FolderWrench" Text="设定角色" ID="btnSetRole">
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
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="2" Region="West" Collapsible="false" RootVisible="false" MultiSelect="true"
                            Title="部门信息" TitleAlign="Center">
                            <Store>
                                <ext:TreeStore ID="TreeStore1" runat="server">
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjId" />
                                                <ext:ModelField Name="DeptName" />
                                                <ext:ModelField Name="HRCode" />
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
                                    <ext:TreeColumn ID="DepName" DataIndex="DeptName" runat="server" Sortable="false" Hideable="false" Text="部门名称" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <ItemClick Fn="deptItemClick"></ItemClick>
                            </Listeners>
                        </ext:TreePanel>
                        <ext:GridPanel ID="GridPanel1" runat="server" Flex="3" Region="Center" Collapsible="false" MultiSelect="true" FolderSort="true"
                            Title="用户信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="store" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model3" runat="server" IDProperty="EquipCode">
                                            <Fields>
                                                <ext:ModelField Name="OBJID" />
                                                <ext:ModelField Name="USER_NAME" />
                                                <ext:ModelField Name="WORK_BARCODE" />
                                                <ext:ModelField Name="DEPT_ID" />
                                                <ext:ModelField Name="IS_EMPLOYEE" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Listeners>
                                        <DataChanged Fn="onGridPanel1Render"></DataChanged>
                                    </Listeners>
                                    <Sorters>
                                        <ext:DataSorter Property="IS_EMPLOYEE" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:Column ID="OBJID" DataIndex="WORK_BARCODE" runat="server" Text="用户编码" Width="80" />
                                    <ext:Column ID="UserName" DataIndex="USER_NAME" runat="server" Text="用户名称" Width="100" />
                                    <ext:Column ID="UserRemark" DataIndex="DEPT_ID" runat="server" Text="部门名称" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" />
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>