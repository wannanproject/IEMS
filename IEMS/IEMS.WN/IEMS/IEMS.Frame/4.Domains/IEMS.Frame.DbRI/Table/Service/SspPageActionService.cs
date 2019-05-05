using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;

    /// <summary>
    /// 系统权限管理-系统权限 - 基础表操作接口
    /// </summary>
    public interface ISspPageActionService : ITableViewService<SspPageAction>
    {
    }

    /// <summary>
    /// 系统权限管理-系统权限 - 基础表操作类
    /// </summary>
    internal class SspPageActionService : TableViewService<SspPageAction>, ISspPageActionService
    {
        #region 构造方法
        public SspPageActionService() : base() { }
        public SspPageActionService(string dbName) : base(dbName) { }
        #endregion
    }
}