using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// VIEW_PALLET - 基础表操作接口
    /// </summary>
    public interface IViewPalletService : ITableViewService<ViewPallet>
    {
    }

    /// <summary>
    /// VIEW_PALLET - 基础表操作类
    /// </summary>
    internal class ViewPalletService : TableViewService<ViewPallet>, IViewPalletService
    {
        #region 构造方法
        public ViewPalletService() : base() { }
        public ViewPalletService(string dbName) : base(dbName) { }
        #endregion
    }
}