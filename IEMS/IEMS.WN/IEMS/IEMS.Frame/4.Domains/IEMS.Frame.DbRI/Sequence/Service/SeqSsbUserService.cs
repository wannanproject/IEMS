using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSB_USER - 序列操作接口
    /// </summary>
    public interface ISeqSsbUserService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSB_USER - 序列操作类
    /// </summary>
    internal class SeqSsbUserService : SequenceService, ISeqSsbUserService
    {
        #region 构造方法
        public SeqSsbUserService() : base("SEQ_SSB_USER") { }
        public SeqSsbUserService(string dbName) : base(dbName, "SEQ_SSB_USER") { }
        #endregion
    }
}