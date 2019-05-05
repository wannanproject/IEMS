using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 库位类型 - 基础表操作接口
    /// </summary>
    public interface IPsbLocTypeService : ITableViewService<PsbLocType>
    {
    }

    /// <summary>
    /// 库位类型 - 基础表操作类
    /// </summary>
    internal class PsbLocTypeService : TableViewService<PsbLocType>, IPsbLocTypeService
    {
        #region 构造方法
        public PsbLocTypeService() : base() { }
        public PsbLocTypeService(string dbName) : base(dbName) { }
        #endregion
    }
}