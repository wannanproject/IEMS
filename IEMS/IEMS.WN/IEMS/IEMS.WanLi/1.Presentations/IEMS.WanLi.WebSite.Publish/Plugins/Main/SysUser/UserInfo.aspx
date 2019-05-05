<%@ page language="C#" autoeventwireup="true" inherits="Plugins_Main_SysUser_UserInfo, IEMS.Main.WebSite" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员基础信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var thisDirUrl = "<%= Page.ResolveUrl("./") %>";
        var thisRootUrl = "<%= Page.ResolveUrl("~/") %>";
    </script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //-------部门-----查询带回弹出框--BEGIN
        var McUI_SearchBox_SearchBoxSsbDept_Request = function (record) {//部门返回值处理
            if (!App.winAdd.hidden) {
                App.add_dept.setValue(record.data.DEPT_NAME);
                App.hidden_add_dept.setValue(record.data.OBJID);
            }
            else if (!App.winModify.hidden) {
                App.modify_dept.setValue(record.data.DEPT_NAME);
                App.hidden_modify_dept.setValue(record.data.OBJID);
            }
            else {
                App.txt_depart.getTrigger(0).show();
                App.txt_depart.setValue(record.data.DEPT_NAME);
                App.hidden_txt_dept.setValue(record.data.OBJID);
            }
        }

        var SelectDepartment = function (field, trigger, index) {//部门查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_txt_dept.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.McUI_SearchBox_SearchBoxSsbDept_Window.show();
                    break;
            }
        }

        var UpdateDepartment = function (field, trigger, index) {//部门查询
            App.McUI_SearchBox_SearchBoxSsbDept_Window.show();
        }
        Ext.create("Ext.window.Window", {//部门查询带回窗体
            id: "McUI_SearchBox_SearchBoxSsbDept_Window",
            height: 430,
            hidden: true,
            width: 360,
            html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchBoxSsbDept.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择部门",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------岗位-----查询带回弹出框--BEGIN
        var McUI_SearchBox_SearchBoxSsbWork_Request = function (record) {//部门返回值处理
            if (!App.winAdd.hidden) {
                App.add_work.setValue(record.data.WORK_NAME);
                App.hidden_add_work.setValue(record.data.OBJID);
            }
            else if (!App.winModify.hidden) {
                App.modify_work.setValue(record.data.WORK_NAME);
                App.hidden_modify_work.setValue(record.data.OBJID);
            }
            else {
                App.txt_work.setValue(record.data.WORK_NAME);
                App.hidden_txt_work.setValue(record.data.OBJID);
            }
        }

        var SelectWorkPosition = function () {//岗位查询
            App.McUI_SearchBox_SearchBoxSsbWork_Window.show();
        }

        Ext.create("Ext.window.Window", {//岗位查询带回窗体
            id: "McUI_SearchBox_SearchBoxSsbWork_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchBoxSsbWork.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择岗位",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------车间-----查询带回弹出框--BEGIN
        var McUI_SearchBox_SearchBoxSsbWorkshop_Request = function (record) {//车间返回值处理
            if (!App.winAdd.hidden) {
                App.add_workshop.setValue(record.data.WORKSHOP_NAME);
                App.hidden_add_workshop.setValue(record.data.OBJID);
            }
            else if (!App.winModify.hidden) {
                App.modify_workshop.setValue(record.data.WORKSHOP_NAME);
                App.hidden_modify_workshop.setValue(record.data.OBJID);
            }
            else {
                App.txt_workshop.setValue(record.data.WORKSHOP_NAME);
                App.hidden_txt_workshop.setValue(record.data.OBJID);
            }
        }

        var SelectWorkShop = function () {//车间查询
            App.McUI_SearchBox_SearchBoxSsbWorkshop_Window.show();
        }

        Ext.create("Ext.window.Window", {//车间查询带回窗体
            id: "McUI_SearchBox_SearchBoxSsbWorkshop_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchBoxSsbWorkShop.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择车间",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjId = record.data.OBJID;
            App.direct.commandcolumn_direct_edit(ObjId, {
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
            var ObjId = record.data.OBJID;
            App.direct.commandcolumn_direct_recover(ObjId, {
                success: function (result) {
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
            var ObjId = record.data.OBJID;
            App.direct.commandcolumn_direct_delete(ObjId, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }


        //点击删除密码按钮
        var commandcolumn_direct_deletepwd = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjId = record.data.OBJID;
            App.direct.commandcolumn_direct_deletepwd(ObjId, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击初始化密码按钮
        var commandcolumn_direct_initpwd = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjId = record.data.OBJID;
            App.direct.commandcolumn_direct_initpwd(ObjId, {
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
            if (command.toLowerCase() == "deletepwd") {
                Ext.Msg.confirm("提示", '您确定需要禁止该用户登录系统？', function (btn) { commandcolumn_direct_deletepwd(btn, record) });
            }
            if (command.toLowerCase() == "initpwd") {
                Ext.Msg.confirm("提示", '您确定需要初始化密码？', function (btn) { commandcolumn_direct_initpwd(btn, record) });
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
                    return true;
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
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //历史查询按钮点击列表刷新数据重载方法
        var pnlHistoryListFresh = function () {
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
        }
    </script>
    <script type="text/javascript">
        var LoginState = function (value) {
            return Ext.String.format((value > 0) ? "未禁止" : "已禁止");
        };
    </script>
</head>
<body>
    <form id="fmUser" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
            OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmUser" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlUserTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle" />
                                <ext:Button runat="server" Icon="Note" Text="历史查询" ID="btnHistorySearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行历史查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlHistoryListFresh">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Hidden ID="hidden_delete_flag" runat="server"  Text="0"/>
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle_2" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="pnlUserQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_work_barcode" Vtype="integer" runat="server" FieldLabel="用户工号"
                                                    LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_user_name" runat="server" FieldLabel="用户名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txt_depart" runat="server" FieldLabel="所属部门" LabelAlign="Right"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectDepartment" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:Hidden ID="hidden_txt_dept" runat="server" />
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
                        <ext:Store ID="store" runat="server" PageSize="10">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="OBJID" />
                                        <ext:ModelField Name="USER_NAME" />
                                        <ext:ModelField Name="REAL_NAME" />
                                        <ext:ModelField Name="SEX" />
                                        <ext:ModelField Name="TELEPHONE" />
                                        <ext:ModelField Name="WORK_BARCODE" />
                                        <ext:ModelField Name="DEPT_ID" />
                                        <ext:ModelField Name="WORK_ID" />
                                        <ext:ModelField Name="CLASS_ID" />
                                        <ext:ModelField Name="SHIFT_ID" />
                                        <ext:ModelField Name="WORKSHOP_ID" />
                                        <ext:ModelField Name="HR_CODE" />
                                        <ext:ModelField Name="ERP_CODE" />
                                        <ext:ModelField Name="DELETE_FLAG" />
                                        <ext:ModelField Name="REMARK" />
                                        <ext:ModelField Name="IS_EMPLOYEE" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" Width="45" runat="server" />
                            <ext:Column ID="ObjId" runat="server" Text="用户编号" DataIndex="OBJID" Width="60" />
                            <ext:Column ID="UserName" runat="server" Text="用户名称" DataIndex="USER_NAME" Width="65" />
                            <ext:Column ID="RealName" runat="server" Text="真实姓名" DataIndex="REAL_NAME" Width="65" />
                            <ext:Column ID="WorkBarcode" runat="server" Text="用户工号" DataIndex="WORK_BARCODE" Width="60" />
                            <ext:Column ID="Sex" runat="server" Text="性别" DataIndex="SEX" Width="40" />
                            <ext:Column ID="Telephone" runat="server" Text="手机号码" DataIndex="TELEPHONE" Width="90" />
                            <ext:Column ID="DeptId" runat="server" Text="部门" DataIndex="DEPT_ID" Width="150" />
                            <ext:Column ID="WorkId" runat="server" Text="岗位" DataIndex="WORK_ID" Width="70" />
                            <ext:Column ID="ClassId" runat="server" Text="班组" DataIndex="CLASS_ID" Width="40" />
                            <ext:Column ID="ShiftId" runat="server" Text="班次" DataIndex="SHIFT_ID" Width="40" />
                            <ext:Column ID="WorkShopId" runat="server" Text="车间" DataIndex="WORKSHOP_ID" Width="80" />
                            <ext:Column ID="IsEmployee" runat="server" Text="禁止登陆" DataIndex="IS_EMPLOYEE" Width="60">
                                <Renderer Fn="LoginState" />
                            </ext:Column>
                            <ext:Column ID="remark" runat="server" Hidden="true" Text="备注" DataIndex="Remark" Width="80" />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="292" Text="操作" Align="Center">
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
                                    <ext:GridCommand Icon="IpodNanoConnect" CommandName="InitPwd" Text="初始密码">
                                        <ToolTip Text="初始化密码" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="ApplicationDelete" CommandName="DeletePwd" Text="禁止登录">
                                        <ToolTip Text="禁止登录" />
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改用户基础信息"
                    Width="600" Height="350" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="modify_objid" runat="server" FieldLabel="用户编号" LabelAlign="Left"
                                                    ReadOnly="true" Enabled="true" />
                                                <ext:TextField ID="modify_user_name" runat="server" FieldLabel="用户名称" LabelAlign="Left"
                                                    Enabled="true" Editable="false" MaxLength="20"
                                                    IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                                <ext:ComboBox ID="modify_sex" runat="server" FieldLabel="性别" LabelAlign="Left" Enabled="true"
                                                    Editable="false" MaxLength="20" IndicatorText="*" IndicatorCls="red-text" />
                                                <ext:TextField ID="modify_user_code" runat="server" FieldLabel="用户工号" LabelAlign="Left"
                                                    Enabled="true" IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                                <ext:TriggerField ID="modify_work" runat="server" FieldLabel="岗位" LabelAlign="Left"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectWorkPosition" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:Hidden ID="hidden_modify_work" runat="server" />
                                                <ext:ComboBox ID="modify_class" runat="server" FieldLabel="班组" LabelAlign="Left">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clearValue();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:TextField ID="modify_remark" runat="server" FieldLabel="备注" LabelAlign="Left"
                                                    Enabled="true" MaxLength="100" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="modify_real_name" runat="server" FieldLabel="真实姓名" LabelAlign="Left"
                                                    Enabled="true" />
                                                <ext:TextField ID="modify_telNum" runat="server" FieldLabel="电话" Vtype="integer"
                                                    LabelAlign="Left" Enabled="true" MaxLength="11" />
                                                <ext:TriggerField ID="modify_dept" runat="server" FieldLabel="部门" LabelAlign="Left"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="UpdateDepartment" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:Hidden ID="hidden_modify_dept" runat="server" />
                                                <ext:TriggerField ID="modify_workshop" runat="server" FieldLabel="车间" LabelAlign="Left"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectWorkShop" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:Hidden ID="hidden_modify_workshop" runat="server" />
                                                <ext:ComboBox ID="modify_shift" runat="server" FieldLabel="班次" LabelAlign="right">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clearValue();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
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
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加用户基础信息"
                    Width="600" Height="350" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="add_user_name" runat="server" FieldLabel="用户名称" LabelAlign="right"
                                                    Enabled="true" MaxLength="20" Editable="false"
                                                    IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                                <ext:ComboBox ID="add_sex" runat="server" FieldLabel="性别" LabelAlign="right" Enabled="true"
                                                    IndicatorText="*" IndicatorCls="red-text" />
                                                <ext:TextField ID="add_user_code" runat="server" FieldLabel="用户工号" LabelAlign="right"
                                                    Enabled="true" MaxLength="20" Editable="false"
                                                    IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                                <ext:TriggerField ID="add_work" runat="server" FieldLabel="岗位" LabelAlign="right"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectWorkPosition" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:Hidden ID="hidden_add_work" runat="server" />
                                                <ext:ComboBox ID="add_class" runat="server" FieldLabel="班组" LabelAlign="right">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clearValue();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:TextField ID="add_remark" runat="server" FieldLabel="备注" LabelAlign="right" Enabled="true"
                                                    MaxLength="100" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="add_real_name" runat="server" FieldLabel="真实姓名" LabelAlign="right"
                                                    Enabled="true" MaxLength="20" />
                                                <ext:TextField ID="add_telNum" runat="server" FieldLabel="电话" Vtype="integer"
                                                    LabelAlign="right" Enabled="true" MaxLength="11" />
                                                <ext:TriggerField ID="add_dept" runat="server" FieldLabel="部门" LabelAlign="right"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="UpdateDepartment" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:Hidden ID="hidden_add_dept" runat="server" />
                                                <ext:TriggerField ID="add_workshop" runat="server" FieldLabel="车间" LabelAlign="right"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectWorkShop" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:Hidden ID="hidden_add_workshop" runat="server" />
                                                <ext:ComboBox ID="add_shift" runat="server" FieldLabel="班次" LabelAlign="right">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clearValue();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="保存中..." MinDelay="50" />
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
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
