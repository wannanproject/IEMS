using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using IEMS.WanLi.VoDto;
using MSTL.McJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IEMS.WanLi.DbRI;
using IEMS.WanLi.Entity;
using MSTL.DbAccess;

public partial class Plugins_WanLi_CrnManager_CrnErrorBinHandle : IEMS.WanLi.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            堆垛机管理 = new PageAction() { ActionId = 1, ActionName = "堆垛机管理" };
        }
        public PageAction 堆垛机管理 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
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
            var manager = AppBizFactory.CreateInstance<ICrnManager>();
            var data = manager.GetCrnForkErrorStatus();
            var total = data.Rows.Count;
            return new { data = data, total };
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig()
            {
                Title = "提示",
                Message = ex.Message,
                Icon = MessageBox.Icon.WARNING
            });
        }
        return null;
    }

    /// <summary>
    /// 确认为异常 先入品、空出库
    /// </summary>
    [DirectMethod]
    public string ConfirmFirstInProduct(string crnForkNo, string taskNo)
    {
        try
        {
            int itaskNo = 0;
            if (!int.TryParse(taskNo, out itaskNo))
            {
                return "任务号不正确！";
            }

            var crnForkStatusService = TableViewServiceFactory.CreateInstance<IPemCrnForkStatusService>();
            var crnForkStatus =
                crnForkStatusService.GetEntityList(new PemCrnForkStatus { CrnForkNo = crnForkNo, TaskNo = itaskNo }).FirstOrDefault();
            if (crnForkStatus == null)
            {
                return "未找到对应的记录";
            }
            if (crnForkStatus.FipFlag == 1)
            {
                var param = new PemCrnForkStatus { FipFlag = 2};
                crnForkStatusService.Update(param, crnForkStatus);
            }
            else
            {
                return "当前状态无需确认";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }

}