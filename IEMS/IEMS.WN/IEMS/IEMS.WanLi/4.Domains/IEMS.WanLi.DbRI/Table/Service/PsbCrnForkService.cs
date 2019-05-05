using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 堆垛机叉设置表 - 基础表操作接口
    /// </summary>
    public interface IPsbCrnForkService : ITableViewService<PsbCrnFork>
    {
    }

    /// <summary>
    /// 堆垛机叉设置表 - 基础表操作类
    /// </summary>
    internal class PsbCrnForkService : TableViewService<PsbCrnFork>, IPsbCrnForkService
    {
        #region 构造方法
        public PsbCrnForkService() : base() { }
        public PsbCrnForkService(string dbName) : base(dbName) { }
        #endregion
    }
}