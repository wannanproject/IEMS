using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using IEMS.WanLi.Entity;
using IEMS.Frame.WebUI;
using MSTL.McJson;
using System;
using System.Collections.Generic;
using MSTL.DbAccess;
using System.Collections;
using MSTL.ResultStruct.McException;
using System.Data;
using IEMS.WanLi.DbCI;

public partial class Plugins_WanLi_TaskManager_TaskManager : IEMS.WanLi.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询任务信息 = new PageAction() { ActionId = 1, ActionName = "btnSearch" };
            强制取消 = new PageAction() { ActionId = 2, ActionName = "btnCancel" };
            强制完成 = new PageAction() { ActionId = 3, ActionName = "btnComplete" };
        }
        public PageAction 查询任务信息 { get; private set; } //必须为 public
        public PageAction 强制取消 { get; private set; } //必须为 public
        public PageAction 强制完成 { get; private set; } //必须为 public
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

    private object createParameterObject()
    {
        var param = new Hashtable(6);
        if (!string.IsNullOrWhiteSpace(this.txtTaskNo.Text))
        {
            param["TASK_NO"] = this.txtTaskNo.StringValue();
        }
        if (!string.IsNullOrWhiteSpace(this.txtPalletNo.Text))
        {
            param["PALLET_NO"] = this.txtPalletNo.StringValue();
        }
        return param;
    }

    /// <summary>
    /// 指令信息
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GetTaskCmd(string action, Dictionary<string, object> extraParams)
    {
        PageResult pageResult = new PageResult();
        try
        {
            var param = new Hashtable(6);
            if (!string.IsNullOrWhiteSpace(this.txtTaskNo.Text))
            {
                param["TASK_NO"] = this.txtTaskNo.StringValue();
            }
            if (!string.IsNullOrWhiteSpace(this.txtPalletNo.Text))
            {
                param["PALLET_NO"] = this.txtPalletNo.StringValue();
            }
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            if (prms.Limit <= 0)
            {
                return pageResult.Data;
            }
            pageResult.PageSize = prms.Limit;
            pageResult.PageIndex = prms.Page;
            pageResult.ParameterObject = param;
            pageResult.OrderString = "T1.CREATION_DATE ";

            var service = DbCIServiceFactory.CreateInstance<ITaskService>();
            pageResult = service.GetTaskData(pageResult);
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = ex.Message, Icon = MessageBox.Icon.WARNING });
        }
        return pageResult.Data;
    }

    /// <summary>
    /// 强制重发指令
    /// </summary>
    [DirectMethod]
    public string ForceResendCmd(string Objid)
    {
        if (string.IsNullOrWhiteSpace(Objid) || Objid.Equals("0"))
        {
            return "指令号不能为空！";
        }
        try
        {
            var manager = AppBizFactory.CreateInstance<ITaskManager>();
            var i = manager.ResendWbsTaskCmd(Objid);
            if (i > 0)
            {
                return string.Empty;
            }
            return "重发失败";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// 强制结束指令
    /// </summary>
    [DirectMethod]
    public string ForceFinishCmd(string Objid)
    {
        if (string.IsNullOrWhiteSpace(Objid) || Objid.Equals("0"))
        {
            return "指令号不能为空！";
        }
        try
        {
            var manager = AppBizFactory.CreateInstance<ITaskManager>();
            if(manager.FinishWbsTaskCmd(Objid))
            {
                return string.Empty;
            }
            return "结束失败";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// 强制删除指令
    /// </summary>
    [DirectMethod]
    public string ForceDeleteCmd(string Objid)
    {
        if (string.IsNullOrWhiteSpace(Objid) || Objid.Equals("0"))
        {
            return "指令号不能为空！";
        }
        try
        {
            var manager = AppBizFactory.CreateInstance<ITaskManager>();
            if (manager.DeleteWbsTaskCmd(Objid))
            {
                return string.Empty;
            }
            return "取消失败";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// 强制删除任务
    /// </summary>
    [DirectMethod]
    public string ForceDeleteTask(string taskNo)
    {
        if (string.IsNullOrWhiteSpace(taskNo))
        {
            return "任务号不能为空！";
        }
        try
        {
            var manager = AppBizFactory.CreateInstance<ITaskManager>();
            if (manager.DeleteWbsTask(taskNo))
            {
                return string.Empty;
            }
            return "取消失败";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [DirectMethod]
    public string RequestTaskCmd(string palletNo1,string palletNo2,string sLocNo)
    {
        if(string.IsNullOrEmpty(palletNo1) && string.IsNullOrEmpty(palletNo2) || string.IsNullOrEmpty(sLocNo))
        {
            return "信息缺失";
        }
        if(palletNo1.Equals(palletNo2))
        {
            return "条码信息重复";
        }
        try
        {
            var manager = AppBizFactory.CreateInstance<ITaskManager>();
            return manager.RequestTaskCmd(palletNo1, palletNo2, sLocNo);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
