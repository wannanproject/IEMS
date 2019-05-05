using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 存储过程参数 - 任务执行完毕 - 基础表操作接口
    /// </summary>
    public interface ITproc0303CmdFinishService : ITableViewService<Tproc0303CmdFinish>
    {
    }

    /// <summary>
    /// 存储过程参数 - 任务执行完毕 - 基础表操作类
    /// </summary>
    internal class Tproc0303CmdFinishService : TableViewService<Tproc0303CmdFinish>, ITproc0303CmdFinishService
    {
        #region 构造方法
        public Tproc0303CmdFinishService() : base() { }
        public Tproc0303CmdFinishService(string dbName) : base(dbName) { }
        #endregion
    }
}