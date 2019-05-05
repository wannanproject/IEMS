using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSP_PAGE_METHOD - 序列操作接口
    /// </summary>
    public interface ISeqSspPageMethodService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSP_PAGE_METHOD - 序列操作类
    /// </summary>
    internal class SeqSspPageMethodService : SequenceService, ISeqSspPageMethodService
    {
        #region 构造方法
        public SeqSspPageMethodService() : base("SEQ_SSP_PAGE_METHOD") { }
        public SeqSspPageMethodService(string dbName) : base(dbName, "SEQ_SSP_PAGE_METHOD") { }
        #endregion
    }
}