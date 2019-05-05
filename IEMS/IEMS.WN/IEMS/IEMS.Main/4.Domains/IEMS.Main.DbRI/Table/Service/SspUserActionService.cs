using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// 系统权限管理-用户权限 - 基础表操作接口
    /// </summary>
    public interface ISspUserActionService : ITableViewService<SspUserAction>
    {
    }

    /// <summary>
    /// 系统权限管理-用户权限 - 基础表操作类
    /// </summary>
    internal class SspUserActionService : TableViewService<SspUserAction>, ISspUserActionService
    {
        #region 构造方法
        public SspUserActionService() : base() { }
        public SspUserActionService(string dbName) : base(dbName) { }
        #endregion
    }
}