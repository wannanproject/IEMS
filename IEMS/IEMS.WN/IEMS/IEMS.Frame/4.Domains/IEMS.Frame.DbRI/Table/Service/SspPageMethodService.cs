using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;

    /// <summary>
    /// SSP_PAGE_METHOD - 基础表操作接口
    /// </summary>
    public interface ISspPageMethodService : ITableViewService<SspPageMethod>
    {
    }

    /// <summary>
    /// SSP_PAGE_METHOD - 基础表操作类
    /// </summary>
    internal class SspPageMethodService : TableViewService<SspPageMethod>, ISspPageMethodService
    {
        #region 构造方法
        public SspPageMethodService() : base() { }
        public SspPageMethodService(string dbName) : base(dbName) { }
        #endregion
    }
}