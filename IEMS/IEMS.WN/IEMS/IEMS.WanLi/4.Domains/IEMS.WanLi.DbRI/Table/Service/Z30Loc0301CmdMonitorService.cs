using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// Z30_LOC_0301_CMD_MONITOR - 基础表操作接口
    /// </summary>
    public interface IZ30Loc0301CmdMonitorService : ITableViewService<Z30Loc0301CmdMonitor>
    {
    }

    /// <summary>
    /// Z30_LOC_0301_CMD_MONITOR - 基础表操作类
    /// </summary>
    internal class Z30Loc0301CmdMonitorService : TableViewService<Z30Loc0301CmdMonitor>, IZ30Loc0301CmdMonitorService
    {
        #region 构造方法
        public Z30Loc0301CmdMonitorService() : base() { }
        public Z30Loc0301CmdMonitorService(string dbName) : base(dbName) { }
        #endregion
    }
}