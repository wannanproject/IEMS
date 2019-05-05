using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 存储过程参数 - 数据日志备份 - 基础表操作接口
    /// </summary>
    public interface ITprocDataBackupService : ITableViewService<TprocDataBackup>
    {
    }

    /// <summary>
    /// 存储过程参数 - 数据日志备份 - 基础表操作类
    /// </summary>
    internal class TprocDataBackupService : TableViewService<TprocDataBackup>, ITprocDataBackupService
    {
        #region 构造方法
        public TprocDataBackupService() : base() { }
        public TprocDataBackupService(string dbName) : base(dbName) { }
        #endregion
    }
}