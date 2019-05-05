using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSP_ROLE_ACTION - 序列操作接口
    /// </summary>
    public interface ISeqSspRoleActionService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSP_ROLE_ACTION - 序列操作类
    /// </summary>
    internal class SeqSspRoleActionService : SequenceService, ISeqSspRoleActionService
    {
        #region 构造方法
        public SeqSspRoleActionService() : base("SEQ_SSP_ROLE_ACTION") { }
        public SeqSspRoleActionService(string dbName) : base(dbName, "SEQ_SSP_ROLE_ACTION") { }
        #endregion
    }
}