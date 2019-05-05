using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 库位状态表 - 基础表操作接口
    /// </summary>
    public interface IPsbBinStatusService : ITableViewService<PsbBinStatus>
    {
    }

    /// <summary>
    /// 库位状态表 - 基础表操作类
    /// </summary>
    internal class PsbBinStatusService : TableViewService<PsbBinStatus>, IPsbBinStatusService
    {
        #region 构造方法
        public PsbBinStatusService() : base() { }
        public PsbBinStatusService(string dbName) : base(dbName) { }
        #endregion
    }
}