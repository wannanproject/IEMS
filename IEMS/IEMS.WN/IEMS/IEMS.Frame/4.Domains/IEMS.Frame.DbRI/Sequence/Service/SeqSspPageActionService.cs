using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSP_PAGE_ACTION - 序列操作接口
    /// </summary>
    public interface ISeqSspPageActionService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSP_PAGE_ACTION - 序列操作类
    /// </summary>
    internal class SeqSspPageActionService : SequenceService, ISeqSspPageActionService
    {
        #region 构造方法
        public SeqSspPageActionService() : base("SEQ_SSP_PAGE_ACTION") { }
        public SeqSspPageActionService(string dbName) : base(dbName, "SEQ_SSP_PAGE_ACTION") { }
        #endregion
    }
}