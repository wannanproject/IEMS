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
    public interface IZ50WbsTaskCmdService : ITableViewService<Z50WbsTaskCmd>
    {
    }

    /// <summary>
    /// WBS_TASK_COMMAND - 基础表操作类
    /// </summary>
    internal class Z50WbsTaskCmdService : TableViewService<Z50WbsTaskCmd>, IZ50WbsTaskCmdService
    {
        #region 构造方法
        public Z50WbsTaskCmdService() : base() { }
        public Z50WbsTaskCmdService(string dbName) : base(dbName) { }
        #endregion
    }
}