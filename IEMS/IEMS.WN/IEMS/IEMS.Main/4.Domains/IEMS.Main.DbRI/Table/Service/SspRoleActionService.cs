using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// 系统权限管理-角色权限 - 基础表操作接口
    /// </summary>
    public interface ISspRoleActionService : ITableViewService<SspRoleAction>
    {
    }

    /// <summary>
    /// 系统权限管理-角色权限 - 基础表操作类
    /// </summary>
    internal class SspRoleActionService : TableViewService<SspRoleAction>, ISspRoleActionService
    {
        #region 构造方法
        public SspRoleActionService() : base() { }
        public SspRoleActionService(string dbName) : base(dbName) { }
        #endregion
    }
}