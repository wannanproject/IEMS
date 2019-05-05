using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSTL.DbAccess;
using System.Collections;
using IEMS.Frame.WebUI;
using MSTL.ResultStruct.McException;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.Entity;
using Ext.Net;
using IEMS.WanLi.AppBiz;
using MSTL.LogAgent;


public partial class Plugins_WanLi_BillData_SearchBillOutput : IEMS.Frame.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询单据信息 = new PageAction() { ActionId = 1, ActionName = "btnSearch" };
            添加单据信息 = new PageAction() { ActionId = 2, ActionName = "btnAddBill" };
        }
        public PageAction 查询单据信息 { get; private set; } //必须为 public
        public PageAction 添加单据信息 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        }
    }
    #region 查询条件
    private object createParameterObject()
    {
        var param = new Hashtable(4);
        //param["WhNo"] = "";
        if (!string.IsNullOrWhiteSpace(this.txtBillListNo.Text))
        {
            param["OrderNo"] = this.txtBillListNo.StringValue();
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
            pageResult = AppBizFactory.CreateInstance<IOutputBillManager>().GetBillOutputData(pageResult);
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = ex.Message.ToString(), Icon = MessageBox.Icon.WARNING });
        }
        return pageResult.Data;
    }

    #region  删除单据信息

    [DirectMethod]
    public string DeleteBillData(string OrderNo)
    {
        var manager = AppBizFactory.CreateInstance<IOutputBillManager>();
        try
        {
            manager.DeleteBillData(OrderNo);
            X.MessageBox.Alert("提示", "删除成功！").Show();
        }
        catch (Exception ex)
        {
            log.Error(ex);
            return ex.Message;
        }
        return string.Empty;
    }
    #endregion
}