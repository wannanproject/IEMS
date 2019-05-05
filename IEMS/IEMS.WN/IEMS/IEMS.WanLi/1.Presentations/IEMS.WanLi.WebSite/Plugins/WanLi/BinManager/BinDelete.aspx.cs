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

public partial class Plugins_WanLi_BinManager_BinDelete : IEMS.WanLi.WebUI.Page
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

    /// <summary>
    /// 删除库存
    /// </summary>
    [DirectMethod]
    public string DeleteBin(string binNo)
    {
        try
        {
            var manager = AppBizFactory.CreateInstance<IBinDataManager>();
            manager.DeleteBin(binNo);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }

   

    /// <summary>
    /// 库存信息
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GetBinData(string action, Dictionary<string, object> extraParams)
    {
        PageResult pageResult = new PageResult();
        try
        {
            var param = new Hashtable(6);
            if (!string.IsNullOrWhiteSpace(this.txtBinNo.Text))
            {
                param["BIN_NO"] = this.txtBinNo.StringValue();
            }
            if (!string.IsNullOrWhiteSpace(this.txtPalletNo.Text))
            {
                param["PALLET_NO"] = this.txtPalletNo.StringValue();
            }
            if (!string.IsNullOrWhiteSpace(this.txtMaterNo.Text))
            {
                param["MATERIAL_NO"] = this.txtMaterNo.StringValue();
            }
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            if (prms.Limit <= 0)
            {
                return pageResult.Data;
            }
            pageResult.PageSize = prms.Limit;
            pageResult.PageIndex = prms.Page;
            pageResult.ParameterObject = param;
            pageResult.OrderString = "T.creation_date ";

            var service = DbCIServiceFactory.CreateInstance<IBinDataService>();
            pageResult = service.GetBinData(pageResult);
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = ex.Message, Icon = MessageBox.Icon.WARNING });
        }
        return pageResult.Data;
    }
   
}
