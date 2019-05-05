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
    public interface IZ50WbsTaskService : ITableViewService<Z50WbsTask>
    {
    }

    /// <summary>
    /// 工作表 - 基础表操作类
    /// </summary>
    internal class Z50WbsTaskService : TableViewService<Z50WbsTask>, IZ50WbsTaskService
    {
        #region 构造方法
        public Z50WbsTaskService() : base() { }
        public Z50WbsTaskService(string dbName) : base(dbName) { }
        #endregion
    }
}