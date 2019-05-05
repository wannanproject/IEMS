using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_WBS_ORDER_LINE - 序列操作接口
    /// </summary>
    public interface ISeqWbsOrderLineService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_WBS_ORDER_LINE - 序列操作类
    /// </summary>
    internal class SeqWbsOrderLineService : SequenceService, ISeqWbsOrderLineService
    {
        #region 构造方法
        public SeqWbsOrderLineService() : base("SEQ_WBS_ORDER_LINE") { }
        public SeqWbsOrderLineService(string dbName) : base(dbName, "SEQ_WBS_ORDER_LINE") { }
        #endregion
    }
}