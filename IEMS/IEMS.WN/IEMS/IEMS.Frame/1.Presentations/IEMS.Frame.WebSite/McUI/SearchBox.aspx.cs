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

public partial class McUI_SearchBox : IEMS.Frame.McUI.Page
{
    #region 页面初始化
    protected override void McUI_Load()
    {
        IniFn();
        IniFormJs();
        PageIni pageini = new PageIni(ui, manager);
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
        js.Append(@" 
            var response = function (command, record) {
                parent.McUI_SearchBox_" + ui.Name + @"_Request(record);
                parent.App.McUI_SearchBox_" + ui.Name + @"_Window.close();
                return false;
            }
            ");
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
