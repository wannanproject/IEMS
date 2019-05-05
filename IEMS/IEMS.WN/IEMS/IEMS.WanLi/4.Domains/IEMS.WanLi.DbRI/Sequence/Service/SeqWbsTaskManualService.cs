using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_WBS_TASK_MANUAL - 序列操作接口
    /// </summary>
    public interface ISeqWbsTaskManualService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_WBS_TASK_MANUAL - 序列操作类
    /// </summary>
    internal class SeqWbsTaskManualService : SequenceService, ISeqWbsTaskManualService
    {
        #region 构造方法
        public SeqWbsTaskManualService() : base("SEQ_WBS_TASK_MANUAL") { }
        public SeqWbsTaskManualService(string dbName) : base(dbName, "SEQ_WBS_TASK_MANUAL") { }
        #endregion
    }
}