using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// 系统基础资料-人员信息 - 基础表操作接口
    /// </summary>
    public interface ISsbUserService : ITableViewService<SsbUser>
    {
    }

    /// <summary>
    /// 系统基础资料-人员信息 - 基础表操作类
    /// </summary>
    internal class SsbUserService : TableViewService<SsbUser>, ISsbUserService
    {
        #region 构造方法
        public SsbUserService() : base() { }
        public SsbUserService(string dbName) : base(dbName) { }
        #endregion
    }
}