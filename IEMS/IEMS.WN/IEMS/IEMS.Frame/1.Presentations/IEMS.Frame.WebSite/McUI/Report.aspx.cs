using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Ext.Net;
using MSTL.DbAccess;
using IEMS.Frame.McUI;
using IEMS.Frame.WebUI.Entity;
using IEMS.Frame.McUI.ExtNet;

public partial class McUI_Report : IEMS.Frame.McUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询信息 = new PageAction() { ActionId = 1, ActionName = "btnSearch" };
            导出 = new PageAction() { ActionId = 2, ActionName = "btnExport" };
        }
        public PageAction 查询信息 { get; private set; } //必须为 public
        public PageAction 导出 { get; private set; } //必须为 public
    }
    #endregion
    #region 页面初始化
    protected override void McUI_Load()
    {
        IniFn();
        IniFormJs();
        PageIni pageini = new PageIni(ui, manager);
        pageini.IniWebPage(this);
        pageini.IniSelectMainPanel(this.containerQueryParam);
        pageini.IniSelectMainGrid(this.gridPanelMain);
        pageini.IniSelectMainDetailPanel(this.panelMainDetail);
        pageini.IniSelectMainDetailContainer(this.containerMainDetail);
        pageini.IniSelectDetailWindow(this.winDetail);
        pageini.IniSelectDetailGrid(this.gridPanelDetail);

    }
    /// <summary>
    /// 初始化修改 删除 恢复相关JS
    /// </summary>
    private void IniFormJs()
    {
    }
    /// <summary>
    /// 初始化界面相关绑定函数
    /// </summary>
    private void IniFn()
    {
        //数据绑定
        if (ui.Select.MainGrid.Columns.Count > 0)
        {
            this.gridPanelMainStore.Proxy.Add(new PageProxy() { DirectFn = "App.direct.GridPanelBindData" });
        }
        if (ui.Select.DetailGrid.Columns.Count > 0)
        {
            this.gridPanelDetailStore.Proxy.Add(new PageProxy() { DirectFn = "App.direct.GridPanelDetailBindData" });
        }
    }
    #endregion

    #region 查询
    private PageResult GetPageResultData(PageResult pageParams)
    {
        #region 判断是否进行全部查询
        var formParam= pdata.GetSelectParamFieldValue(this.Request.Form);
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
        #region 根据URL传递的参数进行查询
        foreach (string name in this.Request.QueryString.AllKeys)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                continue;
            }
            string value = this.Request.QueryString[name];
            bool isExeits = false;
            string key = string.Empty;
            foreach (KeyValuePair<string, object> keyvalue in formParam)
            {
                if (keyvalue.Key.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    key = keyvalue.Key;
                    formParam[key] = value;
                    isExeits = true;
                    break;
                }
            }
            if (!isExeits)
            {
                key = name;
                formParam.Add(key, value);
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                formParam.Remove(key);
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
        string tyreInforKey = "MANY_TYRE_NO_INFOR";
        var keys = new List<string>(formParam.Keys);
        foreach(var key in keys)
        {
            if (key.ToLower().StartsWith(tyreInforKey.ToLower()))
            {
                formParam[key] = getListValue(formParam[key].ToString());
            }
        }
        pageParams.ParameterObject = formParam;
        return manager.GetPageDataByReader(pageParams);
    }
    private string getListValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }
        var ss = value.Split(new char[]{',','，','*','#','+'});
        var result = new StringBuilder();
        foreach (var str in ss)
        {
            result.Append("'");
            result.Append(str.Trim().ToUpper()).Append("',");
        }
        return result.Remove(result.Length - 1,1).ToString();
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
        pageResult.TotalColumnIndex = ui.Select.MainGrid.TotalColumns.Index;
        pageResult.TotalFields = ui.Select.MainGrid.TotalColumns.Fields;
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
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult pageResult = new PageResult();
        pageResult.StatementId = "Select@" + ui.Name;
        pageResult.PageSize = 0;
        pageResult.PageIndex = 0;
        pageResult.OrderString = ui.Select.MainGrid.OrderString;
        pageResult = GetPageResultData(pageResult);

        DataTable data = new DataTable();
        if (pageResult == null)
        {
            return;
        }
        data = pageResult.ResultDataSet.Tables[0];


        int icolCount = 0;
        for (int i = 0; i < ui.Select.MainGrid.Columns.Count; i++)
        {
            GridColumn column = ui.Select.MainGrid.Columns[i];
            if (column.IsShow && data.Columns.Contains(column.ColumnName))
            {
                int idx = data.Columns.IndexOf(column.ColumnName);
                data.Columns[idx].ColumnName = ui.UiData.GetCaption(column.Caption, column.ColumnName).Value;
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
    #region 详细信息
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_ShowFieldsInfo(string ss)
    {
        string result = string.Empty;
        PageResult pageResult = new PageResult() { PageIndex = 0, PageSize = 0 };
        pageResult.StatementId = "Select@" + ui.Name + "@MainDetail";
        pageResult.OrderString = ui.Select.MainGrid.OrderString;
        pageResult.ParameterObject = getDetailParam(ss);

        pageResult = manager.GetPageDataByReader(pageResult);
        if (pageResult == null || pageResult.ResultDataSet == null
            || pageResult.ResultDataSet.Tables.Count == 0
            || pageResult.ResultDataSet.Tables[0].Rows.Count == 0)
        {
            return result;
        }
        DataRow row = pageResult.ResultDataSet.Tables[0].Rows[0];
        Dictionary<string, string> values = new Dictionary<string, string>();
        foreach (DataColumn dc in pageResult.ResultDataSet.Tables[0].Columns)
        {
            object obj = row[dc];
            string v = string.Empty;
            if (obj != null && obj != DBNull.Value)
            {
                if (obj.GetType() == typeof(DateTime))
                {
                    v = ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss");
                    v = v.Replace(" 00:00:00", string.Empty);
                }
                else
                {
                    v = obj.ToString();
                }
            }
            values.Add(dc.ColumnName, v);
        }
        pdata.RefreshSelectMainDetailData(values);
        panelMainDetail.Collapsed = false;
        return result;
    }
    #endregion
    #region 明细查询
    [DirectMethod]
    public object GridPanelDetailBindData(string action, Dictionary<string, object> extraParams)
    {
        #region 初始化条件
        DataTable data = new DataTable();
        int total = 0;
        string ss = this.hiddenDetailSearchParam.Text;
        if (string.IsNullOrWhiteSpace(ss))
        {
            return new { data, total };
        }
        #endregion

        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult pageResult = new PageResult();
        pageResult.StatementId = "Select@" + ui.Name + "@DetailGrid";
        pageResult.PageSize = prms.Limit;
        pageResult.PageIndex = prms.Page;
        pageResult.OrderString = ui.Select.DetailGrid.OrderString;
        pageResult.ParameterObject = getDetailParam(ss);
        pageResult = manager.GetPageDataByReader(pageResult);
        if (pageResult == null)
        {
            return new { data, total };
        }
        data = pageResult.ResultDataSet.Tables[0];
        total = pageResult.RecordCount;
        return new { data, total };
    }
    #endregion
}
