using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_WBS_ORDER - 序列操作接口
    /// </summary>
    public interface ISeqWbsOrderService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_WBS_ORDER - 序列操作类
    /// </summary>
    internal class SeqWbsOrderService : SequenceService, ISeqWbsOrderService
    {
        #region 构造方法
        public SeqWbsOrderService() : base("SEQ_WBS_ORDER") { }
        public SeqWbsOrderService(string dbName) : base(dbName, "SEQ_WBS_ORDER") { }
        #endregion
    }
}