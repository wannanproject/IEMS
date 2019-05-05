using Ext.Net;
using IEMS.Frame.WebUI;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using IEMS.WanLi.Entity;
using System;
using System.Collections.Generic;

public partial class Plugins_WanLi_TaskManager_TaskAgvDelete : IEMS.WanLi.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            站台状态维护 = new PageAction() { ActionId = 1, ActionName = "堆垛机管理" };
        }
        public PageAction 站台状态维护 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            IniPageFieldData();
        }
    }

    private void IniPageFieldData()
    {
        

    }

    /// <summary>
    /// 行数据信息
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        try
        {
            var manager = AppBizFactory.CreateInstance<IAgvTaskManager>();
            var data = manager.GetAgvTaskDataTable();
            var total = data.Rows.Count;
            return new { data = data, total };
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig()
            {
                Title = "提示",
                Message = ex.Message.ToString(),
                Icon = MessageBox.Icon.WARNING
            });
        }
        return null;
    }

    /// <summary>
    /// 设置入库
    /// </summary>
    [DirectMethod]
    public string DeleteAgvTask(string taskNo)
    {
        try
        {
            
            var manager = AppBizFactory.CreateInstance<IAgvTaskManager>();
            manager.DeleteAgvTaskOnly(taskNo);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }
}