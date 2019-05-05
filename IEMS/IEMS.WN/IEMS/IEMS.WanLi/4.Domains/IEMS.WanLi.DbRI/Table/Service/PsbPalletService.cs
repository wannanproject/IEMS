using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PSB_PALLET - 基础表操作接口
    /// </summary>
    public interface IPsbPalletService : ITableViewService<PsbPallet>
    {
    }

    /// <summary>
    /// PSB_PALLET - 基础表操作类
    /// </summary>
    internal class PsbPalletService : TableViewService<PsbPallet>, IPsbPalletService
    {
        #region 构造方法
        public PsbPalletService() : base() { }
        public PsbPalletService(string dbName) : base(dbName) { }
        #endregion
    }
}