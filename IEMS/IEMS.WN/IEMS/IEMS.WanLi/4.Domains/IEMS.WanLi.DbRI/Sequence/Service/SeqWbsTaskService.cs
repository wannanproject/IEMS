using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_WBS_TASK - 序列操作接口
    /// </summary>
    public interface ISeqWbsTaskService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_WBS_TASK - 序列操作类
    /// </summary>
    internal class SeqWbsTaskService : SequenceService, ISeqWbsTaskService
    {
        #region 构造方法
        public SeqWbsTaskService() : base("SEQ_WBS_TASK") { }
        public SeqWbsTaskService(string dbName) : base(dbName, "SEQ_WBS_TASK") { }
        #endregion
    }
}