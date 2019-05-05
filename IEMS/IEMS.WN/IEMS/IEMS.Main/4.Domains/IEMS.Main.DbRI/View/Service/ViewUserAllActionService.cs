using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// VIEW_USER_ALL_ACTION - 基础表操作接口
    /// </summary>
    public interface IViewUserAllActionService : ITableViewService<ViewUserAllAction>
    {
    }

    /// <summary>
    /// VIEW_USER_ALL_ACTION - 基础表操作类
    /// </summary>
    internal class ViewUserAllActionService : TableViewService<ViewUserAllAction>, IViewUserAllActionService
    {
        #region 构造方法
        public ViewUserAllActionService() : base() { }
        public ViewUserAllActionService(string dbName) : base(dbName) { }
        #endregion
    }
}