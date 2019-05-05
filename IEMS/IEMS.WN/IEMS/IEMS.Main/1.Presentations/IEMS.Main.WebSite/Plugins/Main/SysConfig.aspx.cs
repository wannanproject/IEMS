using System;

using Ext.Net;
using MSTL.DbAccess;
using IEMS.Frame.McUI;
using IEMS.Frame.WebUI.Entity;

public partial class Plugins_Main_SysConfig :IEMS.Frame.WebUI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            清空数据库缓存 = new PageAction() { ActionId = 1, ActionName = "btnSqlCacheClear" };
            清空页面配置缓存 = new PageAction() { ActionId = 2, ActionName = "btnUiCacheClear" };
        }
        public PageAction 清空数据库缓存 { get; private set; } //必须为 public
        public PageAction 清空页面配置缓存 { get; private set; } //必须为 public
    }
    #endregion
    #region 删除数据库配置缓存
    /// <summary>
    /// 删除数据库配置缓存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [DirectMethod]
    protected void btnSqlCacheClear_Click(object sender, EventArgs e)
    {
        DbHelper.ClearCache();
    }
    #endregion
    #region 删除界面配置缓存
    /// <summary>
    /// 删除界面配置缓存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [DirectMethod]
    protected void btnUiCacheClear_Click(object sender, EventArgs e)
    {
        UiHelper.ClearCache();
    }
    #endregion
}