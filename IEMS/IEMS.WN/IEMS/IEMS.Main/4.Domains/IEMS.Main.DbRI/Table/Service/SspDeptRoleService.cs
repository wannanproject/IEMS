using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// 系统权限管理-部门角色表 - 基础表操作接口
    /// </summary>
    public interface ISspDeptRoleService : ITableViewService<SspDeptRole>
    {
    }

    /// <summary>
    /// 系统权限管理-部门角色表 - 基础表操作类
    /// </summary>
    internal class SspDeptRoleService : TableViewService<SspDeptRole>, ISspDeptRoleService
    {
        #region 构造方法
        public SspDeptRoleService() : base() { }
        public SspDeptRoleService(string dbName) : base(dbName) { }
        #endregion
    }
}