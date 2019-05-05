using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// TPROC_0200_CMD_REQUEST - 基础表操作接口
    /// </summary>
    public interface ITproc0200CmdRequestService : ITableViewService<Tproc0200CmdRequest>
    {
    }

    /// <summary>
    /// TPROC_0200_CMD_REQUEST - 基础表操作类
    /// </summary>
    internal class Tproc0200CmdRequestService : TableViewService<Tproc0200CmdRequest>, ITproc0200CmdRequestService
    {
        #region 构造方法
        public Tproc0200CmdRequestService() : base() { }
        public Tproc0200CmdRequestService(string dbName) : base(dbName) { }
        #endregion
    }
}