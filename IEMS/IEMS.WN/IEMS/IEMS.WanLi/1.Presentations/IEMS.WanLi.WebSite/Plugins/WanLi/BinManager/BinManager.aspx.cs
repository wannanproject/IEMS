using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.WanLi.AppBiz;
using IEMS.WanLi.VoDto;
using MSTL.McJson;
using System;
using System.Collections.Generic;
using System.Data;

public partial class Plugins_WanLi_BinManager_BinManager : IEMS.WanLi.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            状态查看 = new PageAction() { ActionId = 1, ActionName = "btnSearch" };
            库位禁用 = new PageAction() { ActionId = 2, ActionName = "btnBinDisable" };
        }
        public PageAction 状态查看 { get; private set; } //必须为 public
        public PageAction 库位禁用 { get; private set; } //必须为 public
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
        if (_.库位禁用.Permit == 0)
        {
            //this.btnBinDisable.Visible = false;
        }
        var manager = AppBizFactory.CreateInstance<IBinDataManager>();
        var storData = manager.GetStoreBinData(99);
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


    #region  获取行库存状态 GetStoreStatus
    private BinData getBinStatusData(StoreBinData store, int y, int z)
    {
        foreach (var data in store.AllBin)
        {
            if (data.Y == y
                && data.Z == z)
            {
                return data;
            }
        }
        return null;
    }
    [DirectMethod]
    public void GetStoreStatus()//string xIdx
    {
        #region 数据整理
        var idx = 0;
        var manager = AppBizFactory.CreateInstance<IBinDataManager>();

        //先读第一排的库存
        idx = 1;
        var storData = manager.GetStoreBinData(idx);
        var dataTable = new DataTable("StoreStatusData");

        for (int y = 0; y <= storData.MaxBin.Y; y++)
        {
            var columnName = "F" + y.ToString("D2");
            dataTable.Columns.Add(new DataColumn() { ColumnName = columnName });
        }
        for (var z = storData.MaxBin.Z; z > 0; z--)
        {
            var row = dataTable.NewRow();
            dataTable.Rows.Add(row);
            row["F" + 0.ToString("D2")] = z.ToString("D2");
            for (int y = 1; y <= storData.MaxBin.Y; y++)
            {
                var columnName = "F" + y.ToString("D2");
                var data = getBinStatusData(storData, y, z);
                if (data == null)
                {
                    row[columnName] = string.Empty;
                }
                else
                {
                    row[columnName] = McJsonConvert.SerializeObject(new BinData[] { data });
                }
            }
        }

        //插入一个空行
        var rownull = dataTable.NewRow();
        dataTable.Rows.Add(rownull);
        rownull["F" + 0.ToString("D2")] = "间隔";
        for (int y = 1; y <= storData.MaxBin.Y; y++)
        {
            var columnName = "F" + y.ToString("D2");
            rownull[columnName] = string.Empty;
        }

        //查询第二排的库存，插入到第一排后面
        idx = 2;
        var storData2 = manager.GetStoreBinData(idx);
        for (var z = storData2.MaxBin.Z; z > 0; z--)
        {
            var row = dataTable.NewRow();
            dataTable.Rows.Add(row);
            row["F" + 0.ToString("D2")] = z.ToString("D2");
            for (int y = 1; y <= storData2.MaxBin.Y; y++)
            {
                var columnName = "F" + y.ToString("D2");
                var data = getBinStatusData(storData2, y, z);
                if (data == null)
                {
                    row[columnName] = string.Empty;
                }
                else
                {
                    row[columnName] = McJsonConvert.SerializeObject(new BinData[] { data });
                }
            }
        }

        #endregion

        #region 页面数据
        var grid = this.gridMain;
        var store = grid.Store[0];
        store.Reader.Clear();
        grid.SelectionModel.Clear();
        grid.ColumnModel.Columns.Clear();
        store.Model.Clear();
        var model = new Model();
        for (int y = 0; y <= storData.MaxBin.Y; y++)
        {
            var columnName = "F" + y.ToString("D2");
            model.Fields.Add(new ModelField(columnName));
        }
        store.Model.Add(model);
        store.DataSource = dataTable;
        store.DataBind();
        #endregion

        #region 页面展示
        grid.SelectionModel.Add(new CellSelectionModel { Mode = SelectionMode.Single, });
        var columns = new List<ColumnBase>();
        columns.Add(new Column()
        {
            Sortable = false,
            Hideable = false,
            Draggable = false,
            Align = Alignment.Center,
            DataIndex = ("F" + 0.ToString("D2")),
            Text = 0.ToString("D2"),
            Resizable = false,
            Width = 30,
        });
        for (int y = 1; y <= storData.MaxBin.Y; y++)
        {
            columns.Add(new Column()
            {
                Renderer = new Renderer() { Fn = "columns_renderer" },
                Sortable = false,
                Hideable = false,
                Draggable = false,
                Align = Alignment.Center,
                DataIndex = ("F" + y.ToString("D2")),
                Text = y.ToString("D2"),
                Width = 26,
                Resizable = false
            });
        }
        grid.ColumnModel.Columns.Add(columns);
        grid.Render();
        #endregion

        X.Js.Call("setonmouseover");
    }
    #endregion

    #region 设置库位状态 SetBinDisable
    [DirectMethod]
    public string SetBinUseFalg(string binNo, string useFlag)
    {
        var manager = AppBizFactory.CreateInstance<IBinDataManager>();
        var flag = 0;
        int.TryParse(useFlag, out flag);
        if (flag == 0)
        {
            flag = 1;
        }
        else
        {
            flag = 0;
        }
        manager.ChanageBinUseFlag(binNo, flag);
        return string.Empty;
    }

    #endregion
}
