using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using MSTL.DbAccess;
using System.Collections;
using MSTL.McJson;
using IEMS.Frame.WebUI.Entity;
using IEMS.Frame.WebUI;
using IEMS.WanLi.AppBiz;

public partial class Plugins_WanLi_MaterialManager_MaterialManager : IEMS.WanLi.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            任务添加 = new PageAction() { ActionId = 1, ActionName = "btnSaveBill" };
            任务删除 = new PageAction() { ActionId = 1, ActionName = "Delete" };
        }
        public PageAction 任务添加 { get; private set; } //必须为 public
        public PageAction 任务删除 { get; private set; } //必须为 public
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

    #region 页面初始化  IniPageDataOnLoad

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
    #endregion


    #region 行数据信息 GridPanelBindData

    private object createParameterObject()
    {
        var param = new Hashtable(6);
        if (!string.IsNullOrWhiteSpace(this.txtSearcheMaterNo.Text))
        {
            param["MATER_NO"] = this.txtSearcheMaterNo.StringValue();
        }
        //if (!string.IsNullOrWhiteSpace(this.txtSearchePalletNo.Text))
        //{
        //    param["PALLET_NO"] = this.txtSearchePalletNo.StringValue();
        //}
        return param;
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
            pageResult.ParameterObject = createParameterObject();
            pageResult = AppBizFactory.CreateInstance<IMaterialManager>().GetMaterialData(pageResult);
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = ex.Message.ToString(), Icon = MessageBox.Icon.WARNING });
        }
        return pageResult.Data;
    }
    #endregion


    #region 删除任务信息
    [DirectMethod]
    public string DeleteAgvTask(string taskNo)
    {
        try
        {
            var manager = AppBizFactory.CreateInstance<IAgvTaskManager>();
            var result = manager.DeleteAgvTask(taskNo);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }
    #endregion



    #region 添加任务信息
    [DirectMethod]
    public string AddMaterialInfo(string materialNo, string materialName)
    {
        try
        {
            var manager = AppBizFactory.CreateInstance<IMaterialManager>();
          
            var result = manager.AddMaterialInfo(materialNo, materialName);
            if (result > 0) { return "正确"; }
            else { return "错误"; }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }
    #endregion
}