using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 订单监控 - 基础表操作接口
    /// </summary>
    public interface IZ30Loc0101OrderMonitorService : ITableViewService<Z30Loc0101OrderMonitor>
    {
    }

    /// <summary>
    /// 订单监控 - 基础表操作类
    /// </summary>
    internal class Z30Loc0101OrderMonitorService : TableViewService<Z30Loc0101OrderMonitor>, IZ30Loc0101OrderMonitorService
    {
        #region 构造方法
        public Z30Loc0101OrderMonitorService() : base() { }
        public Z30Loc0101OrderMonitorService(string dbName) : base(dbName) { }
        #endregion
    }
}