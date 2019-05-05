using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Plugins_WanLi_PsbLocManager_EmptyLocEnable : IEMS.WanLi.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            空笼站台管理 = new PageAction() { ActionId = 1, ActionName = "空笼站台管理" };
        }
        public PageAction  空笼站台管理 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    ///// <summary>
    ///// 行数据信息
    ///// </summary>
    ///// <param name="action"></param>
    ///// <param name="extraParams"></param>
    ///// <returns></returns>
    //[DirectMethod]
    //public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    //{
    //    try
    //    {
    //        var manager = AppBizFactory.CreateInstance<ILocAutoManager>();
    //        var data = manager.GetLocDataTable();
    //        var total = data.Rows.Count;
    //        return new { data = data, total };
    //    }
    //    catch (Exception ex)
    //    {
    //        X.MessageBox.Show(new MessageBoxConfig()
    //        {
    //            Title = "提示",
    //            Message = ex.Message.ToString(),
    //            Icon = MessageBox.Icon.WARNING
    //        });
    //    }
    //    return null;
    //}
    /// <summary>
    /// 行数据信息
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
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
            pageResult = AppBizFactory.CreateInstance<ILocAutoManager>().GetLocDataTable(pageResult);
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = ex.Message.ToString(), Icon = MessageBox.Icon.WARNING });
        }
        return pageResult.Data;
    }

    /// <summary>
    /// 修改空笼工位状态
    /// </summary>
    [DirectMethod]
    public string changeLocAutoEnable(string locNo, string enable)
    {
        try
        {
            var flag = 0;
            int.TryParse(enable, out flag);    //enable转换为int，flag值为enable转化为int型后的值
            if (flag == 0)
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            var manager = AppBizFactory.CreateInstance<ILocAutoManager>();

            manager.SetLocAutoEnable(locNo, flag);

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }
}