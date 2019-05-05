using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PSB_MINO - 基础表操作接口
    /// </summary>
    public interface IPsbMinoService : ITableViewService<PsbMino>
    {
    }

    /// <summary>
    /// PSB_MINO - 基础表操作类
    /// </summary>
    internal class PsbMinoService : TableViewService<PsbMino>, IPsbMinoService
    {
        #region 构造方法
        public PsbMinoService() : base() { }
        public PsbMinoService(string dbName) : base(dbName) { }
        #endregion
    }
}