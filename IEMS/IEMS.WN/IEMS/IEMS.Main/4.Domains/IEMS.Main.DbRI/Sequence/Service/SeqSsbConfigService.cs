using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSB_CONFIG - 序列操作接口
    /// </summary>
    public interface ISeqSsbConfigService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSB_CONFIG - 序列操作类
    /// </summary>
    internal class SeqSsbConfigService : SequenceService, ISeqSsbConfigService
    {
        #region 构造方法
        public SeqSsbConfigService() : base("SEQ_SSB_CONFIG") { }
        public SeqSsbConfigService(string dbName) : base(dbName, "SEQ_SSB_CONFIG") { }
        #endregion
    }
}