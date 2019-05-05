using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// 系统权限管理-系统功能 - 基础表操作接口
    /// </summary>
    public interface ISspPageMenuService : ITableViewService<SspPageMenu>
    {
    }

    /// <summary>
    /// 系统权限管理-系统功能 - 基础表操作类
    /// </summary>
    internal class SspPageMenuService : TableViewService<SspPageMenu>, ISspPageMenuService
    {
        #region 构造方法
        public SspPageMenuService() : base() { }
        public SspPageMenuService(string dbName) : base(dbName) { }
        #endregion
    }
}