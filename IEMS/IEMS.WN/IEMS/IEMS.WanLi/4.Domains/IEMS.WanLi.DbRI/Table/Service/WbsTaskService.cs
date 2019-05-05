using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 工作表 - 基础表操作接口
    /// </summary>
    public interface IWbsTaskService : ITableViewService<WbsTask>
    {
    }

    /// <summary>
    /// 工作表 - 基础表操作类
    /// </summary>
    internal class WbsTaskService : TableViewService<WbsTask>, IWbsTaskService
    {
        #region 构造方法
        public WbsTaskService() : base() { }
        public WbsTaskService(string dbName) : base(dbName) { }
        #endregion
    }
}