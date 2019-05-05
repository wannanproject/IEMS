using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 订单请求参数表 - 基础表操作接口
    /// </summary>
    public interface ITproc0100TaskRequestService : ITableViewService<Tproc0100TaskRequest>
    {
    }

    /// <summary>
    /// 订单请求参数表 - 基础表操作类
    /// </summary>
    internal class Tproc0100TaskRequestService : TableViewService<Tproc0100TaskRequest>, ITproc0100TaskRequestService
    {
        #region 构造方法
        public Tproc0100TaskRequestService() : base() { }
        public Tproc0100TaskRequestService(string dbName) : base(dbName) { }
        #endregion
    }
}