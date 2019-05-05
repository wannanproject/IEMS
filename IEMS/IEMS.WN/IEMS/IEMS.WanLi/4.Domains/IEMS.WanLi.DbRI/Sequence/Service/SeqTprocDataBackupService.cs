using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_TPROC_DATA_BACKUP - 序列操作接口
    /// </summary>
    public interface ISeqTprocDataBackupService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_TPROC_DATA_BACKUP - 序列操作类
    /// </summary>
    internal class SeqTprocDataBackupService : SequenceService, ISeqTprocDataBackupService
    {
        #region 构造方法
        public SeqTprocDataBackupService() : base("SEQ_TPROC_DATA_BACKUP") { }
        public SeqTprocDataBackupService(string dbName) : base(dbName, "SEQ_TPROC_DATA_BACKUP") { }
        #endregion
    }
}