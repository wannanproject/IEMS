using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 堆垛机叉状态表 - 基础表操作接口
    /// </summary>
    public interface IPemCrnForkStatusService : ITableViewService<PemCrnForkStatus>
    {
    }

    /// <summary>
    /// 堆垛机叉状态表 - 基础表操作类
    /// </summary>
    internal class PemCrnForkStatusService : TableViewService<PemCrnForkStatus>, IPemCrnForkStatusService
    {
        #region 构造方法
        public PemCrnForkStatusService() : base() { }
        public PemCrnForkStatusService(string dbName) : base(dbName) { }
        #endregion
    }
}