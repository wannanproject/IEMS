using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSB_DEPT - 序列操作接口
    /// </summary>
    public interface ISeqSsbDeptService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSB_DEPT - 序列操作类
    /// </summary>
    internal class SeqSsbDeptService : SequenceService, ISeqSsbDeptService
    {
        #region 构造方法
        public SeqSsbDeptService() : base("SEQ_SSB_DEPT") { }
        public SeqSsbDeptService(string dbName) : base(dbName, "SEQ_SSB_DEPT") { }
        #endregion
    }
}