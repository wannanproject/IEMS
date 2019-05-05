using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// 系统权限管理-角色 - 基础表操作接口
    /// </summary>
    public interface ISspRoleService : ITableViewService<SspRole>
    {
    }

    /// <summary>
    /// 系统权限管理-角色 - 基础表操作类
    /// </summary>
    internal class SspRoleService : TableViewService<SspRole>, ISspRoleService
    {
        #region 构造方法
        public SspRoleService() : base() { }
        public SspRoleService(string dbName) : base(dbName) { }
        #endregion
    }
}