using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSP_PAGE_MENU - 序列操作接口
    /// </summary>
    public interface ISeqSspPageMenuService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSP_PAGE_MENU - 序列操作类
    /// </summary>
    internal class SeqSspPageMenuService : SequenceService, ISeqSspPageMenuService
    {
        #region 构造方法
        public SeqSspPageMenuService() : base("SEQ_SSP_PAGE_MENU") { }
        public SeqSspPageMenuService(string dbName) : base(dbName, "SEQ_SSP_PAGE_MENU") { }
        #endregion
    }
}