using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;

    /// <summary>
    /// 增、删、改必须记录。 - 基础表操作接口
    /// </summary>
    public interface ISslWebLogService : ITableViewService<SslWebLog>
    {
    }

    /// <summary>
    /// 增、删、改必须记录。 - 基础表操作类
    /// </summary>
    internal class SslWebLogService : TableViewService<SslWebLog>, ISslWebLogService
    {
        #region 构造方法
        public SslWebLogService() : base() { }
        public SslWebLogService(string dbName) : base(dbName) { }
        #endregion
    }
}