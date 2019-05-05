using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_Z50_WBS_TASK - 序列操作接口
    /// </summary>
    public interface ISeqZ50WbsTaskService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_Z50_WBS_TASK - 序列操作类
    /// </summary>
    internal class SeqZ50WbsTaskService : SequenceService, ISeqZ50WbsTaskService
    {
        #region 构造方法
        public SeqZ50WbsTaskService() : base("SEQ_Z50_WBS_TASK") { }
        public SeqZ50WbsTaskService(string dbName) : base(dbName, "SEQ_Z50_WBS_TASK") { }
        #endregion
    }
}