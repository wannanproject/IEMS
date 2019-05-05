using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// WBS_TASK_COMMAND - 基础表操作接口
    /// </summary>
    public interface IWbsTaskCmdService : ITableViewService<WbsTaskCmd>
    {
    }

    /// <summary>
    /// WBS_TASK_COMMAND - 基础表操作类
    /// </summary>
    internal class WbsTaskCmdService : TableViewService<WbsTaskCmd>, IWbsTaskCmdService
    {
        #region 构造方法
        public WbsTaskCmdService() : base() { }
        public WbsTaskCmdService(string dbName) : base(dbName) { }
        #endregion
    }
}