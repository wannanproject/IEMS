using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Ext.Net;
using MSTL.DbAccess;
using IEMS.Frame.McUI;
using IEMS.Frame.McUI.ExtNet;
using IEMS.Frame.WebUI.Entity;

/// <summary>
/// 单表增删改查
/// </summary>
public partial class McUI_Crud : IEMS.Frame.McUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new PageAction() { ActionId = 1, ActionName = "btnAdd,btnAddSave" };
            查询 = new PageAction() { ActionId = 2, ActionName = "btnSearch" };
            历史查询 = new PageAction() { ActionId = 3, ActionName = "btnSearchAll" };
            导出 = new PageAction() { ActionId = 4, ActionName = "btnExport" };

            删除 = new PageAction() { ActionId = 5, ActionName = "Delete" };
            修改 = new PageAction() { ActionId = 6, ActionName = "Edit,btnModifySave" };
            恢复 = new PageAction() { ActionId = 7, ActionName = "Recover" };
        }
        public PageAction 添加 { get; private set; } //必须为 public
        public PageAction 查询 { get; private set; } //必须为 public
        public PageAction 历史查询 { get; private set; } //必须为 public
        public PageAction 导出 { get; private set; } //必须为 public
        public PageAction 删除 { get; private set; } //必须为 public
        public PageAction 修改 { get; private set; } //必须为 public
        public PageAction 恢复 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    protected override void McUI_Load()
    {
        IniFn();
        IniFormJs();
        PageIni pageini = new PageIni(ui, manager);
        pageini.IniInsert(this.winAdd, this.containerAddParam);
        pageini.IniUpdate(this.winUpdate, this.containerModifyParam);
        pageini.IniSelectMainPanel(this.containerQueryParam);
        pageini.IniSelectMainGrid(this.gridPanelMain);
    }
    /// <summary>
    /// 初始化修改 删除 恢复相关JS
    /// </summary>
    private void IniFormJs()
    {
        HtmlGenericControl jsstr = new HtmlGenericControl("script");
        jsstr.Attributes.Add("type", "text/javascript");
        StringBuilder js = new StringBuilder();
        string primarykey = ui.UiData.Crud.Primarykey.FieldName;
        #region js 脚本 点击修改按钮
        js.AppendLine(@"
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjId = record.data." + primarykey + @";
            App.direct.commandcolumn_direct_edit(ObjId, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }");
        #endregion

        #region js 脚本 点击恢复按钮
        js.AppendLine(@"
        //点击恢复按钮
        var commandcolumn_direct_recover = function (btn, record) {
            if (btn != 'yes') {
                return;
            }
            var ObjId = record.data." + primarykey + @";
            App.direct.commandcolumn_direct_recover(ObjId, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }");
        #endregion

        #region js 脚本 点击删除按钮
        js.AppendLine(@"
        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != 'yes') {
                return;
            }
            var ObjId = record.data." + primarykey + @";
            App.direct.commandcolumn_direct_delete(ObjId, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }");
        #endregion

        #region js 脚本 区分删除操作，并进行二次确认操作
        js.AppendLine(@"
        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == 'edit') {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == 'delete') {
                Ext.Msg.confirm('提示', '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            if (command.toLowerCase() == 'recover') {
                Ext.Msg.confirm('提示', '您确定需要恢复此条信息？', function (btn) { commandcolumn_direct_recover(btn, record) });
            }
            return false;
        };");
        #endregion

        #region js 脚本 根据按钮类别进行删除和编辑操作
        js.AppendLine(@"
        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };");
        #endregion

        #region js 脚本 删除
        string deleteField = ui.UiData.Crud.DeleteFlag;
        if (string.IsNullOrWhiteSpace(deleteField))
        {
            this.btnSearchAll.Hidden = true;

            #region js 脚本 历史查询的每行按钮准备加载
            js.AppendLine(@"
        //历史查询的每行按钮准备加载
        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
        };");
            #endregion

            #region js 脚本 历史查询根据删除标识的值进行样式绑定
            js.AppendLine(@"
        //历史查询根据删除标识的值进行样式绑定
        var setRowClass = function (record, rowIndex, rowParams, store) {
            
        }");
            #endregion
        }
        else
        {
            #region js 脚本 历史查询的每行按钮准备加载
            js.AppendLine(@"
        //历史查询的每行按钮准备加载
        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get('" + deleteField + @"') == '1') {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            } else {
                toolbar.items.getAt(4).hide();
            }
        }");
            #endregion

            #region js 脚本 历史查询根据删除标识的值进行样式绑定
            js.AppendLine(@"
        //历史查询根据删除标识的值进行样式绑定
        var setRowClass = function (record, rowIndex, rowParams, store) {
             if (record.get('" + deleteField + @"') == '1') {
                return 'x-grid-row-deleted';
            }
        }");
            #endregion

        }
        #endregion

        jsstr.InnerHtml = js.ToString();
        Page.Header.Controls.Add(jsstr);
    }
    /// <summary>
    /// 初始化界面相关绑定函数
    /// </summary>
    private void IniFn()
    {
        //数据绑定
        this.gridPanelMainStore.Proxy.Add(new PageProxy() { DirectFn = "App.direct.GridPanelBindData" });
    }
    #endregion

    #region 添加
    public void btnCancel_Click(object sender, EventArgs e)
    {
        this.winAdd.Close();
        this.winUpdate.Close();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //添加项初始化
        pdata.RefreshInsertParamData(null);
        //显示窗口
        this.winAdd.Show();
    }
    public void btnAddSave_Click(object sender, EventArgs e)
    {
        string statementId = "Insert@" + ui.Name;
        var formParam = pdata.GetInsertParamFieldValue(this.Request.Form);
        var commandParam = getCommandParam(ui.Insert, formParam);
        commandParam.StatementId = statementId;
        commandParam.SysParam.DeleteFlag = new ParamKeyValue(ui.UiData.Crud.DeleteFlag, 0);
        CommandResult result = ExecuteCommands(commandParam);
        if (result.isBreak)
        {
            return;
        }
        X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = "数据添加成功！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
        X.Js.AddScript("App.winAdd.close();gridPanelMainFresh();");
    }
    #endregion
    #region 删除
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string ObjId)
    {
        var commandParam = getCommandParam(ui.Delete, null);
        commandParam.SysParam.Primarykey = new ParamKeyValue(ui.UiData.Crud.Primarykey.FieldName, ObjId);
        commandParam.SysParam.DeleteFlag = new ParamKeyValue(ui.UiData.Crud.DeleteFlag, 1);
        CommandResult result = ExecuteCommands(commandParam);
        if (result.isBreak)
        {
            return result.sResult;
        }
        gridPanelMainPageToolbar.DoRefresh();
        return "数据删除成功！";
    }
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string ObjId)
    {
        CommandParam commandParam = getCommandParam(ui.Update, null);
        commandParam.SysParam.Primarykey = new ParamKeyValue(ui.UiData.Crud.Primarykey.FieldName, ObjId);
        commandParam.SysParam.DeleteFlag = new ParamKeyValue(ui.UiData.Crud.DeleteFlag, 0);
        CommandResult result = ExecuteCommand(commandParam);
        if (result.isBreak)
        {
            return result.sResult;
        }
        gridPanelMainPageToolbar.DoRefresh();
        return "数据恢复成功！";
    }
    #endregion
    #region 修改
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string ObjId)
    {
        // string statementId = "Select+Update@" + ui.Name;
        var primary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        string primarykey = ui.UiData.Crud.Primarykey.FieldName;
        primary.Add(primarykey, ObjId);
        DataTable dt = manager.GetSelectAndUpdateByUiName(ui.Name, primary);
            // manager.GetDataTableByStatement(statementId, primary);
        var dic = DataTableToDic(dt);
        pdata.RefreshUpdateParamData(dic);
        pdata.RefreshUpdatePrimarykeyData(primary);
        this.winUpdate.Show();
    }
    protected void btnModifySave_Click(object sender, DirectEventArgs e)
    {
        string statementId = "Update@" + ui.Name;
        var formParam = pdata.GetUpdateParamFieldValue(this.Request.Form);
        var commandParam = getCommandParam(ui.Update, formParam);
        object value = null;
        string primarykey = ui.UiData.Crud.Primarykey.FieldName;
        formParam.TryGetValue(primarykey, out value);
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = "修改错误：" + "无主键", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.WARNING });
        }
        commandParam.StatementId = statementId;
        commandParam.SysParam.Primarykey = new ParamKeyValue(primarykey, value);
        CommandResult result = ExecuteCommands(commandParam);
        if (result.isBreak)
        {
            return;
        }
        X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "数据修改成功！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
        X.Js.AddScript("App.winUpdate.close();");
        gridPanelMainPageToolbar.DoRefresh();
    }
    #endregion
    #region 查询
    private PageResult GetPageResultData(PageResult pageParams)
    {
        var formParam = pdata.GetSelectParamFieldValue(this.Request.Form);
        #region 判断是否进行全部查询
        string deleteFlag = ui.UiData.Crud.DeleteFlag;
        if (!string.IsNullOrWhiteSpace(deleteFlag))
        {
            if (formParam.ContainsKey(deleteFlag))
            {
                formParam[deleteFlag] = this.hiddenIsSearchAllInfo.Value;
            }
            else
            {
                formParam.Add(deleteFlag, this.hiddenIsSearchAllInfo.Value);
            }
            if (string.IsNullOrWhiteSpace(this.hiddenIsSearchAllInfo.Value.ToString()))
            {
                formParam.Remove(deleteFlag);
            }
        }
        #endregion
        #region 扩展功能
        var commandParam = getCommandParam(ui.Select, formParam);
        if (ExecuteCommands(commandParam).isBreak)
        {
            return null;
        }
        #endregion
        pageParams.ParameterObject = formParam;
        return manager.GetPageDataByReader(pageParams);
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult pageResult = new PageResult();
        pageResult.StatementId = "Select@" + ui.Name;
        pageResult.PageSize = prms.Limit;
        pageResult.PageIndex = prms.Page;
        pageResult.OrderString = ui.Select.MainGrid.OrderString;
        pageResult = GetPageResultData(pageResult);

        DataTable data = new DataTable();
        int total = 0;
        if (pageResult == null)
        {
            return new { data, total };
        }
        data = pageResult.ResultDataSet.Tables[0];

        data = updateDataColumnName(data);
        total = pageResult.RecordCount;
        return new { data, total };
    }
    #endregion
    #region 导出
    public void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult pageResult = new PageResult();
        pageResult.StatementId = "Select@" + ui.Name;
        pageResult.PageSize = 0;
        pageResult.OrderString = ui.Select.MainGrid.OrderString;
        pageResult = GetPageResultData(pageResult);
        if (pageResult == null)
        {
            return;
        }
        DataTable data = new DataTable();
        data = pageResult.ResultDataSet.Tables[0];


        int icolCount = 0;
        for (int i = 0; i < ui.Select.MainGrid.Columns.Count; i++)
        {
            GridColumn column = ui.Select.MainGrid.Columns[i];
            Caption caption = ui.UiData.GetCaption(column.Caption, column.ColumnName);
            if (column.IsShow && data.Columns.Contains(column.ColumnName))
            {
                int idx = data.Columns.IndexOf(column.ColumnName);
                data.Columns[idx].ColumnName = caption.Value;
                data.Columns[idx].SetOrdinal(icolCount);
                icolCount++;
            }
        }
        while (true)
        {
            if (data.Columns.Count <= icolCount)
            {
                break;
            }
            data.Columns.RemoveAt(icolCount);
        }
        this.ExcelFileDown(data, ui.WebPage.Title);
    }
    #endregion
}
