using Ext.Net;
using IEMS.Frame.WebUI;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using IEMS.WanLi.Entity;
using System;
using System.Collections.Generic;

public partial class Plugins_WanLi_CrnManager_CrnEnable : IEMS.WanLi.WebUI.Page
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
            var manager = AppBizFactory.CreateInstance<ICrnManager>();
            var data = manager.GetCrnDataTable();
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
    public string ChangeCrnInputEnable(string crnNo, string enable)
    {
        try
        {
            var flag = 0;
            int.TryParse(enable, out flag);
            if (flag == 0)
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            var manager = AppBizFactory.CreateInstance<ICrnManager>();
            manager.SetCrnInputEnable(crnNo, flag);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }

    /// <summary>
    /// 设置出库
    /// </summary>
    [DirectMethod]
    public string ChangeCrnOutputEnable(string crnNo, string enable)
    {
        try
        {
            var flag = 0;
            int.TryParse(enable, out flag);
            if (flag == 0)
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            var manager = AppBizFactory.CreateInstance<ICrnManager>();
            manager.SetCrnOutputEnable(crnNo, flag);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }


    /// <summary>
    /// 设置禁用
    /// </summary>
    [DirectMethod]
    public string SetCrnDisEnable(string crnForkNo, string enable)
    {
        try
        {
            var flag = 0;
            int.TryParse(enable, out flag);
            if (flag == 0)
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            var manager = AppBizFactory.CreateInstance<ICrnManager>();
            manager.SetCrnForkEnable(crnForkNo, flag);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }
    /// <summary>
    /// 设置禁用
    /// </summary>
    [DirectMethod]
    public string UpdateSrmArea(string CrnNo, string MinCol,string MaxCol)
    {
        try
        {
            if (int.Parse(MinCol) >= int.Parse(MaxCol))
            {
                return "堆垛机工作范围设置错误！";
            }
            var manager = AppBizFactory.CreateInstance<ICrnManager>();
            manager.UpdateSrmArea(CrnNo, MinCol, MaxCol);
            return string.Empty;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}