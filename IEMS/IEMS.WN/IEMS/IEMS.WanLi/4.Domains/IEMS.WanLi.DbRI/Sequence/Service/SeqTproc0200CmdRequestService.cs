using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_TPROC_0200_CMD_REQUEST - 序列操作接口
    /// </summary>
    public interface ISeqTproc0200CmdRequestService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_TPROC_0200_CMD_REQUEST - 序列操作类
    /// </summary>
    internal class SeqTproc0200CmdRequestService : SequenceService, ISeqTproc0200CmdRequestService
    {
        #region 构造方法
        public SeqTproc0200CmdRequestService() : base("SEQ_TPROC_0200_CMD_REQUEST") { }
        public SeqTproc0200CmdRequestService(string dbName) : base(dbName, "SEQ_TPROC_0200_CMD_REQUEST") { }
        #endregion
    }
}