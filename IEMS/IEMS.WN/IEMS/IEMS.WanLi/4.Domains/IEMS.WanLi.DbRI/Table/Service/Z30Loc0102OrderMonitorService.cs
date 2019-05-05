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
    public interface IZ30Loc0102OrderMonitorService : ITableViewService<Z30Loc0102OrderMonitor>
    {
    }

    /// <summary>
    /// 订单监控 - 基础表操作类
    /// </summary>
    internal class Z30Loc0102OrderMonitorService : TableViewService<Z30Loc0102OrderMonitor>, IZ30Loc0102OrderMonitorService
    {
        #region 构造方法
        public Z30Loc0102OrderMonitorService() : base() { }
        public Z30Loc0102OrderMonitorService(string dbName) : base(dbName) { }
        #endregion
    }
}