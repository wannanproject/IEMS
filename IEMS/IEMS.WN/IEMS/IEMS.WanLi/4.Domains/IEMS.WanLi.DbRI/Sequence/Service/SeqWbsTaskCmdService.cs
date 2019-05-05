using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_WBS_TASK_CMD - 序列操作接口
    /// </summary>
    public interface ISeqWbsTaskCmdService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_WBS_TASK_CMD - 序列操作类
    /// </summary>
    internal class SeqWbsTaskCmdService : SequenceService, ISeqWbsTaskCmdService
    {
        #region 构造方法
        public SeqWbsTaskCmdService() : base("SEQ_WBS_TASK_CMD") { }
        public SeqWbsTaskCmdService(string dbName) : base(dbName, "SEQ_WBS_TASK_CMD") { }
        #endregion
    }
}