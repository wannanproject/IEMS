using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// 系统权限管理-用户角色 SunBQ@MESNAC.COM - 基础表操作接口
    /// </summary>
    public interface ISspUserRoleService : ITableViewService<SspUserRole>
    {
    }

    /// <summary>
    /// 系统权限管理-用户角色 SunBQ@MESNAC.COM - 基础表操作类
    /// </summary>
    internal class SspUserRoleService : TableViewService<SspUserRole>, ISspUserRoleService
    {
        #region 构造方法
        public SspUserRoleService() : base() { }
        public SspUserRoleService(string dbName) : base(dbName) { }
        #endregion
    }
}