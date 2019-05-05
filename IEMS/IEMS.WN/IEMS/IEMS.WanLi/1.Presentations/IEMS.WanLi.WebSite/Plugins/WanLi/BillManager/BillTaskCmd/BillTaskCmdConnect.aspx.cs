using Ext.Net;
using IEMS.Frame.WebUI;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using MSTL.DbAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Plugins_WanLi_BillTaskCmd_BillTaskCmdConnect : IEMS.WanLi.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            订单信息查询 = new PageAction() { ActionId = 1, ActionName = "订单信息查询" };
        }
        public PageAction 订单信息查询 { get; private set; } //必须为 public
    }  
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            IniPageDataOnLoad();
        }
    }
    #region 页面初始化  IniPageDataOnLoad

    #region 初始化界面信息 IniPageFieldData

    private void IniPageFieldData()
    {
        this.txtSearchLineStatus.SelectFirst();
    }
    #endregion
    #region 
    private void IniPageDataOnLoad()
    {
        IniPageFieldData();
    }
    #endregion
    #endregion

    #region 行数据信息
    #region 查询条件
    private object createOrderParameterObject()
    {
        var param = new Hashtable(6);
        if (!string.IsNullOrWhiteSpace(this.txtSearchLocName.Text))
        {
            param["LOC_NAME"] = this.txtSearchLocName.StringValue();
        }
        if (!string.IsNullOrWhiteSpace(this.txtSearchMaterialNo.Text))
        {
            param["MATERIAL_NO"] = this.txtSearchMaterialNo.StringValue();
        }
        if (!string.IsNullOrWhiteSpace(this.txtSearchLineStatus.StringValue()))
        {
            param["LINE_STATUS"] = int.Parse(this.txtSearchLineStatus.StringValue());
        }
        if (!string.IsNullOrWhiteSpace(this.txtSearchOrderNo.Text))
        {
            param["ORDER_NO"] = this.txtSearchOrderNo.StringValue();
        }
        return param;
    }
    #endregion

    /// <summary>
    /// 行数据信息
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindBillData(string action, Dictionary<string, object> extraParams)
    {
        PageResult pageResult = new PageResult();
        try
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            if (prms.Limit <= 0)
            {
                return pageResult.Data;
            }
            pageResult.PageSize = prms.Limit;
            pageResult.PageIndex = prms.Page;
            pageResult.ParameterObject = createOrderParameterObject();
            pageResult.OrderString = "ta.creation_date";
            pageResult = AppBizFactory.CreateInstance<IBillTaskCmd>().GetBillDataTable(pageResult);
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = ex.Message.ToString(), Icon = MessageBox.Icon.WARNING });
        }
        return pageResult.Data;
    }

    #endregion

    #region 行数据信息
    #region 查询条件
    #region 行数据信息 GridPanelBindData

    #endregion
    private object createTaskParameterObject()
    {
        PageResult pageResult = new PageResult();
        var param = new Hashtable(6);
        if (!string.IsNullOrWhiteSpace(this.txtSearchOrderLineGuid.Text))
        {
            param["ORDER_LINE_GUID"] = this.txtSearchOrderLineGuid.StringValue();
        }
        else
        {
            param["ORDER_LINE_GUID"] = "Can not Find";
        }
        return param;
    }
    #endregion
    #endregion

    /// <summary>
    /// 任务信息
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindTaskData(string action, Dictionary<string, object> extraParams)
    {
        PageResult pageResult = new PageResult();
        try
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            if (prms.Limit <= 0)
            {
                return pageResult.Data;
            }
            pageResult.PageSize = prms.Limit;
            pageResult.PageIndex = prms.Page;
            pageResult.ParameterObject = createTaskParameterObject();
            pageResult.OrderString = "t.creation_date";
            pageResult = AppBizFactory.CreateInstance<IBillTaskCmd>().GetTaskDataTable(pageResult);
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = ex.Message.ToString(), Icon = MessageBox.Icon.WARNING });
        }
        return pageResult.Data;
    }

    #region 指令信息
    #region 查询条件
    #region 指令信息 GridPanelBindData

    #endregion
    private object createCmdParameterObject()
    {
        PageResult pageResult = new PageResult();
        var param = new Hashtable(6);
        if (!string.IsNullOrWhiteSpace(this.txtSearchOrderLineGuid.Text))
        {
            param["ORDER_LINE_GUID"] = this.txtSearchOrderLineGuid.StringValue();
        }
        else
        {
            param["ORDER_LINE_GUID"] = "Can not Find";
        }
        return param;
    }
    #endregion
    #endregion

    /// <summary>
    /// 行数据信息
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindCmdData(string action, Dictionary<string, object> extraParams)
    {
        PageResult pageResult = new PageResult();
        try
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            if (prms.Limit <= 0)
            {
                return pageResult.Data;
            }
            pageResult.PageSize = prms.Limit;
            pageResult.PageIndex = prms.Page;
            pageResult.ParameterObject = createCmdParameterObject();
            pageResult = AppBizFactory.CreateInstance<IBillTaskCmd>().GetCmdDataTable(pageResult);
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = ex.Message.ToString(), Icon = MessageBox.Icon.WARNING });
        }
        return pageResult.Data;
    }
}