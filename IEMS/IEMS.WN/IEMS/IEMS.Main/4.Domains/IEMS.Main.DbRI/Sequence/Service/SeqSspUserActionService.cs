using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSP_USER_ACTION - 序列操作接口
    /// </summary>
    public interface ISeqSspUserActionService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSP_USER_ACTION - 序列操作类
    /// </summary>
    internal class SeqSspUserActionService : SequenceService, ISeqSspUserActionService
    {
        #region 构造方法
        public SeqSspUserActionService() : base("SEQ_SSP_USER_ACTION") { }
        public SeqSspUserActionService(string dbName) : base(dbName, "SEQ_SSP_USER_ACTION") { }
        #endregion
    }
}