using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 库存统计 - 基础表操作接口
    /// </summary>
    public interface IZ30BinStoreService : ITableViewService<Z30BinStore>
    {
    }

    /// <summary>
    /// 库存统计 - 基础表操作类
    /// </summary>
    internal class Z30BinStoreService : TableViewService<Z30BinStore>, IZ30BinStoreService
    {
        #region 构造方法
        public Z30BinStoreService() : base() { }
        public Z30BinStoreService(string dbName) : base(dbName) { }
        #endregion
    }
}