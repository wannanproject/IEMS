using Ext.Net;
using IEMS.WanLi.AppBiz;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using IEMS.WanLi.VoDto;
using MSTL.McJson;

public partial class Plugins_WanLi_BinManager_EmptyBin : IEMS.WanLi.WebUI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            空出库管理 = new PageAction() { ActionId = 1, ActionName = "空出库管理" };
        }
        public PageAction 空出库管理 { get; private set; } //必须为 public
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
        //出库单号
        var billManager = AppBizFactory.CreateInstance<IOutputBillManager>();
        this.txtOrderNo.Text = "E" + billManager.GetNewBillNo();
    }

    [DirectMethod]
    public string GetNewOrderNo()
    {
        var billManager = AppBizFactory.CreateInstance<IOutputBillManager>();
        return "E" + billManager.GetNewBillNo();
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
            var manager = AppBizFactory.CreateInstance<IBinDataManager>();
            var data = manager.GetEmptyBin();
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
    public string ClearEmptyBin(string binNo)
    {
        try
        {
            var manager = AppBizFactory.CreateInstance<IBinDataManager>();
            manager.ClearEmptyBin(binNo);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }

    /// <summary>
    /// 生成出库订单
    /// </summary>
    [DirectMethod]
    public string CreatOrder(string orderId, string orderNo, string binNo, string eLocNo)
    {
        try
        {
            var manager = AppBizFactory.CreateInstance<IBinDataManager>();
            manager.CreatOrder(orderId, orderNo, binNo, eLocNo);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return string.Empty;
    }
}