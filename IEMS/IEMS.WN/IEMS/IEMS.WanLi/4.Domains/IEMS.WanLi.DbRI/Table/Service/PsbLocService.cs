using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 站台信息维护 - 基础表操作接口
    /// </summary>
    public interface IPsbLocService : ITableViewService<PsbLoc>
    {
    }

    /// <summary>
    /// 站台信息维护 - 基础表操作类
    /// </summary>
    internal class PsbLocService : TableViewService<PsbLoc>, IPsbLocService
    {
        #region 构造方法
        public PsbLocService() : base() { }
        public PsbLocService(string dbName) : base(dbName) { }
        #endregion
    }
}