using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// 系统基础资料-配置信息 - 基础表操作接口
    /// </summary>
    public interface ISsbConfigService : ITableViewService<SsbConfig>
    {
    }

    /// <summary>
    /// 系统基础资料-配置信息 - 基础表操作类
    /// </summary>
    internal class SsbConfigService : TableViewService<SsbConfig>, ISsbConfigService
    {
        #region 构造方法
        public SsbConfigService() : base() { }
        public SsbConfigService(string dbName) : base(dbName) { }
        #endregion
    }
}