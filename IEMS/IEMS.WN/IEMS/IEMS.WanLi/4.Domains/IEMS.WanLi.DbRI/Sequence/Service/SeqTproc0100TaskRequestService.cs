using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_TPROC_0100_TASK_REQUEST - 序列操作接口
    /// </summary>
    public interface ISeqTproc0100TaskRequestService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_TPROC_0100_TASK_REQUEST - 序列操作类
    /// </summary>
    internal class SeqTproc0100TaskRequestService : SequenceService, ISeqTproc0100TaskRequestService
    {
        #region 构造方法
        public SeqTproc0100TaskRequestService() : base("SEQ_TPROC_0100_TASK_REQUEST") { }
        public SeqTproc0100TaskRequestService(string dbName) : base(dbName, "SEQ_TPROC_0100_TASK_REQUEST") { }
        #endregion
    }
}