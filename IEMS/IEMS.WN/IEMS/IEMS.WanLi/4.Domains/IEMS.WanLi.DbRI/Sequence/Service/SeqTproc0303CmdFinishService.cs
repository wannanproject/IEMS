using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_TPROC_0303_CMD_FINISH - 序列操作接口
    /// </summary>
    public interface ISeqTproc0303CmdFinishService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_TPROC_0303_CMD_FINISH - 序列操作类
    /// </summary>
    internal class SeqTproc0303CmdFinishService : SequenceService, ISeqTproc0303CmdFinishService
    {
        #region 构造方法
        public SeqTproc0303CmdFinishService() : base("SEQ_TPROC_0303_CMD_FINISH") { }
        public SeqTproc0303CmdFinishService(string dbName) : base(dbName, "SEQ_TPROC_0303_CMD_FINISH") { }
        #endregion
    }
}