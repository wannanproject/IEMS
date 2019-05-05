using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using IEMS.WanLi.VoDto;
using MSTL.McJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Plugins_WanLi_BinManager_ProductStatistics : IEMS.WanLi.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            统计查看 = new PageAction() { ActionId = 1, ActionName = "btnSearch" };
        }
        public PageAction 统计查看 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化 Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            IniPageDataOnLoad();
        }
    }

    #region 初始化界面信息 IniPageFieldData
    private void IniPageFieldData()
    {
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
    private object createParameterObject()
    {
        var param = new Hashtable(20);
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
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        var manager = AppBizFactory.CreateInstance<IBinDataManager>();
        var data = manager.GetProductStatistics();
        int total = data.Rows.Count;
        return new { data, total };
    }
    #endregion
}
