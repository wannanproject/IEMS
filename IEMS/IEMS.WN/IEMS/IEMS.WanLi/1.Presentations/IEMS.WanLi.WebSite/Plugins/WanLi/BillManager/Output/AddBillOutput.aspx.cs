using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IEMS.WanLi.AppBiz;
using Ext.Net;
using IEMS.WanLi.Entity;
using IEMS.Frame.WebUI;
using MSTL.McJson;

public partial class Plugins_WanLi_BillData_AddBillOutput : IEMS.Frame.WebUI.Page
{
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
        this.txtOrderNo.Text = billManager.GetNewBillNo();
        //查询品级信息
        PsbGrade grade = new PsbGrade();
        setgrade(this.txtAddLineGrade, billManager.GetGradeTable(grade));
        this.txtAddLineGrade.SelectFirst();
        setgrade(this.txtModifyLineGrade, billManager.GetGradeTable(grade));
        this.txtModifyLineGrade.SelectFirst();
    }
    #region  输出站台类型
    [DirectMethod]
    public string OutPutEquip(string typeNo)
    {
        try
        {
            var eqipzt = AppBizFactory.CreateInstance<IOutputBillManager>();
            var datas = eqipzt.GetEquipData(typeNo);
            var result = McJsonConvert.SerializeObject(datas);
            return result;
        }
        catch (Exception ex)
        {
        }
        return string.Empty;
    }
    #endregion

    /// <summary>
    /// 输出品级信息
    /// </summary>
    /// <param name="comboBox"></param>
    /// <param name="grade"></param>
    private void setgrade(ComboBox comboBox, PsbGrade[] grade)
    {
        comboBox.Items.Clear();
        foreach (var var in grade)
        {
            var item = new Ext.Net.ListItem();
            item.Value = var.GradeNo;
            item.Text = var.GradeDesc;
            comboBox.Items.Add(item);
        }
    }
    /// <summary>
    /// 输出MI
    /// </summary>
    /// <param name="comboBox"></param>
    /// <param name="grade"></param>
    private void setMino(ComboBox comboBox, PsbMino[] Mino)
    {
        comboBox.Items.Clear();
        foreach (var var in Mino)
        {
            var item = new Ext.Net.ListItem();
            item.Value = var.Mino;
            item.Text = var.Midsc;
            comboBox.Items.Add(item);
        }
    }

    #region 数据保存 SaveJsonInfo
    #region 数据整理
    private WbsOrder JsonToWbsOrder(McJsonObject headJson)
    {
        var result = new WbsOrder(); ///缺4个：OrderTypeNo、OrderTypeModule
        #region 数据处理
        //单号
        result.OrderNo = headJson.StringValue("ORDER_NO");
        //单据状态(0:未执行,1:出库中,2:已完成,3:已取消
        result.OrderStatus = headJson.IntValue("ORDER_STATUS");
        //优先级    数字越小优先级越高
        result.OrderPriority = headJson.IntValue("ORDER_PRIORITY");
        //生成方式
        result.SourceType = headJson.IntValue("SOURCE_TYPE");
        //单据类型
        result.OrderTypeNo = headJson.StringValue("ORDER_TYPE_NO");
        //单据类型模式
        result.OrderTypeModule = headJson.StringValue("ORDER_TYPE_MODULE");
        //创建日期
        result.CreationDate = DateTime.Now;
        //创建者
        result.CreatedBy = this.Data.User.UserId.ToString();
        #endregion
        return result;
    }
    private WbsOrderLine[] JsonToWbsOrderLine(WbsOrder header, McJsonObject headJson, McJsonObject[] bodyJson)
    {
        var result = new List<WbsOrderLine>();
        #region 数据处理
        foreach (var lineJson in bodyJson)
        {
            var line = new WbsOrderLine();
            //单号
            line.OrderNo = header.OrderNo;
            line.OrderLineGuid = Guid.NewGuid().ToString();
            //单LINE
            line.LineId = lineJson.IntValue("LINE_ID");
            //单据行状态
            line.LineStatus = lineJson.IntValue("LINE_STATUS");
            //物料信息
            line.MaterialNo = lineJson.StringValue("MATER_NO");
            //等级
            line.ProductGrade = lineJson.StringValue("PRODUCT_GRADE");
            //单据数量
            line.RequireQty = lineJson.IntValue("REQUIRE_QTY");
            //实际数量
            line.ActQty = lineJson.IntValue("ACT_QTY");
            //发货数量
            line.ShipQty = lineJson.IntValue("SHIP_QTY");
            //工装编号
            line.LimitPalletId = lineJson.StringValue("LIMIT_PALLET_ID");
            //开始站台
            line.LimitBinNo = lineJson.StringValue("LIMIT_BIN_NO");
            //目的站台
            line.ElocNo = lineJson.StringValue("ELOC_NO");
            //产品GUID
            line.LimitProductGuid = lineJson.StringValue("LIMIT_PRODUCT_GUID");
            //行任务分解
            line.LineTaskFlag = lineJson.IntValue("LINE_TASK_FLAG");
            //行项目唯一标示
            line.OrderLineGuid = lineJson.StringValue("ORDER_LINE_GUID");
            line.OrderLineGuid = Guid.NewGuid().ToString().ToUpper();
            //是否已删除
            line.DeleteFlag = "N";
            //此单发货顺序
            line.SortId = 1;
            //优先级
            line.LinePriority = 50;
            //目标锁定
            line.LockEndLoc = 1;
            result.Add(line);
        }
        #endregion
        return result.ToArray();
    }
    #endregion

    #region 数据保存
    /// <summary>
    /// 通过Json进行数据传输，并进行保存
    /// </summary>
    /// <param name="headerStr"></param>
    /// <param name="bodyStr"></param>
    /// <returns></returns>
    [DirectMethod]
    public string SaveJsonInfo(string headerStr, string bodyStr)
    {
        if (string.IsNullOrWhiteSpace(headerStr))
        {
            return "请填写单据主信息！";
        }
        var headJson = McJsonConvert.McDeserializeObject(headerStr);
        if (headJson == null)
        {
            return "请填写单据主信息！";
        }
        if (string.IsNullOrWhiteSpace(bodyStr))
        {
            return "请填写单据行信息！";
        }
        var bodyJson = McJsonConvert.McDeserializeObjects(bodyStr);
        if (bodyJson == null)
        {
            return "请填写单据行信息！";
        }
        if (bodyJson.Length == 0)
        {
            return "请填写单据行信息！";
        }
        var header = JsonToWbsOrder(headJson);
        var body = JsonToWbsOrderLine(header, headJson, bodyJson);
        var manager = AppBizFactory.CreateInstance<IOutputBillManager>();
        try
        {
            manager.SaveOutputBill(header, body.ToArray());
        }
        catch (Exception ex)
        {
            log.Error(ex);
            return ex.Message;
        }
        return string.Empty;
    }
    #endregion
    #endregion
}