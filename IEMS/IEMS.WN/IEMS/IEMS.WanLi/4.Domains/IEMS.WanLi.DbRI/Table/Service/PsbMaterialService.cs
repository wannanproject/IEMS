using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// 物料信息表 - 基础表操作接口
    /// </summary>
    public interface IPsbMaterialService : ITableViewService<PsbMaterial>
    {
    }

    /// <summary>
    /// 物料信息表 - 基础表操作类
    /// </summary>
    internal class PsbMaterialService : TableViewService<PsbMaterial>, IPsbMaterialService
    {
        #region 构造方法
        public PsbMaterialService() : base() { }
        public PsbMaterialService(string dbName) : base(dbName) { }
        #endregion
    }
}